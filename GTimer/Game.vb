Public Class Game

    ReadOnly Property dll() As Utils
        Get
            Return Form1.dll
        End Get
    End Property
    Public ReadOnly Property iniPath() As String
        Get
            If user.isMe() Then
                Return Form1.iniPath
            Else
                Return Form1.sharedStatsPath & user.name & "\gtimer.ini"
            End If
        End Get
    End Property

    Public Const MIN_GAME_COUNT = 3
    Public Shared maxGameCount As Integer = MIN_GAME_COUNT

    Public id As Integer
    Public name As String
    Public exe As String
    Public section As String
    Public todayTimeTemp As Integer
    Public todayTime As Long
    Public active As Boolean
    Public logoPath As String
    Public logoInvPath As String
    Public include As Boolean
    Public timePairs As List(Of KeyValuePair(Of String, Integer))

    Public user As User
    Public panel As GamePanel



    Sub New(id As Integer, user As User, section As String)
        Me.id = id
        Me.section = section
        Me.user = user
        loadSettings()
        timePairs = New List(Of KeyValuePair(Of String, Integer))
        loadTime(iniPath)

        panel = New GamePanel(Me)
        If user.firstLogEntry = Nothing Then user.firstLogEntry = user.getFirstLogEntryDate()
    End Sub

    Sub destroy()
        panel.destroy()
    End Sub

    Sub loadSettings()
        exe = dll.iniReadValue(section, "exe", "", iniPath)
        logoPath = dll.iniReadValue(section, "logo", "", iniPath)
        logoInvPath = dll.iniReadValue(section, "logo_inv", logoPath, iniPath)
        name = dll.iniReadValue(section, "name", section, iniPath)
        include = dll.iniReadValue(section, "include", True, iniPath)
    End Sub

    Sub trackerUpdate()
        Dim processes As List(Of Process) = Process.GetProcessesByName(exe).ToList
        If processes IsNot Nothing And processes.Count > 0 Then
            If Not active Then
                user.activeGamePrioQueue.Add(Me)
                Form1.publishStats()
            End If
            If isPrioActiveGame() And Not user.isTrackingPaused Then
                todayTimeTemp += 1
            End If

            active = True
        Else
            user.activeGamePrioQueue.Remove(Me)
            If active Then
                writeTemp()
                Form1.publishStats()
            End If
            active = False
        End If
    End Sub

    Sub updatePanel()
        Dim time As Long
        If isInGroupPanel() Then
            time = user.getGroupedGamesTime(False)
        Else
            time = getTime()
        End If
        panel.update(time)
    End Sub

    Function checksum() As Integer
        ' Dim totalTime As Long = getTime(True, False)
        ' Dim hash = totalTime.GetHashCode()
        '  Return hash
        Return 0
    End Function

    Function getTime(Optional dateRangeMode As Form1.FetchMethod = Form1.FetchMethod.CUSTOM) As Long
        Dim sum As Long = 0
        For i = 0 To timePairs.Count - 1
            Dim dt As Date = Date.Parse(timePairs(i).Key)

            Dim lowDiff As Integer
            If dateRangeMode = Form1.FetchMethod.CUSTOM Then
                lowDiff = dll.GetDayDiff(dt.Date, Form1.startDate.Date)
            Else
                lowDiff = dll.GetDayDiff(dt.Date, user.getFirstLogEntry)
            End If
            Dim highDiff
            If dateRangeMode = Form1.FetchMethod.CUSTOM Then
                highDiff = dll.GetDayDiff(dt.Date, Form1.endDate.Date)
            Else
                highDiff = dll.GetDayDiff(dt.Date, Now.Date)
            End If
            If lowDiff <= 0 And highDiff >= 0 Then
                sum += timePairs(i).Value
            End If
        Next

        If Form1.dateRangeIncludeToday() Then

            'If addTemp Then
            If user.isMe() Then
                '   If User.isMeSelected() Then
                sum += todayTime
                sum += todayTimeTemp
                ' End If
            Else
                sum += loadTodayTime(iniPath)
                If isPrioActiveGame() And user.online And Not user.isTrackingPaused Then
                    Dim diffSecs As Integer = Now.Subtract(user.lastTempTime).TotalSeconds
                    sum += diffSecs
                End If
            End If
            ' End If
        End If
        '
        Return sum
    End Function


    Function loadTodayTime(iniPath As String) As Long
        Try
            Dim valueString As String = dll.iniReadValue(section, Now.Date.ToShortDateString(), "", iniPath)
            If valueString = "" Then
                Return 0
            Else
                Return CInt(valueString)
            End If
        Catch ex As Exception
            Form1.log("loadTodayTime() failed: " & ex.Message)
            Return 0
        End Try
    End Function

    Function getAllTimeValues(iniPath As String) As List(Of Integer)
        Dim dates As List(Of String) = dll.iniGetAllKeysList(section, iniPath)
        Dim timesString As List(Of String) = dll.iniGetAllValuesList(section, iniPath)
        If dates Is Nothing OrElse timesString Is Nothing Then Return Nothing
        For i = dates.Count - 1 To 0 Step -1
            If Not Date.TryParse(dates(i), New Date()) Then
                dates.RemoveAt(i)
                timesString.RemoveAt(i)
            End If
        Next
        Dim res As New List(Of Integer)
        For Each time In timesString
            res.Add(CInt(time))
        Next
        Return res
    End Function
    Function getAllTimeKeys(iniPath As String) As List(Of String)
        Dim dates As List(Of String) = dll.iniGetAllKeysList(section, iniPath)
        If dates Is Nothing Then Return Nothing
        For i = dates.Count - 1 To 0 Step -1
            If Not Date.TryParse(dates(i), New Date()) Then
                dates.RemoveAt(i)
            End If
        Next
        Return dates
    End Function

    Sub initTimePairs(iniPath As String)
        Dim dates As List(Of String) = getAllTimeKeys(iniPath)
        Dim times As List(Of Integer) = getAllTimeValues(iniPath)
        If dates Is Nothing OrElse times Is Nothing Then
            Dim backoffAction As New Form1.IniBackoff(New Action(Of String)(AddressOf initTimePairs), iniPath)
        Else
            timePairs.Clear()
            For i = 0 To dates.Count - 1
                If Not dll.GetDayDiff(Date.Parse(dates(i)), Now.Date) = 0 Then
                    timePairs.Add(New KeyValuePair(Of String, Integer)(dates(i), times(i)))
                End If
            Next
        End If
    End Sub


    Sub initTodayTime(iniPath As String)
        todayTime = loadTodayTime(iniPath)
    End Sub

    Sub loadTime(iniPath As String)
        initTimePairs(iniPath)
        initTodayTime(iniPath)
    End Sub

    Sub writeTemp()
        If todayTimeTemp > 0 Then
            Dim currTime As Long = loadTodayTime(Form1.iniPath)
            ' Dim prevChecksum As Integer = dll.iniReadValue(section, "checksum", 0)
            ' If prevChecksum <> 0 And prevChecksum = checksum() Or currTime = 0 Or True Then
            Dim newTime As Long = currTime + todayTimeTemp
            ' Dim check As Integer = newTime.GetHashCode()
            dll.iniWriteValue(section, getToday(), newTime)
            ' dll.iniWriteValue(section, "checksum", check)
            todayTime = newTime
            todayTimeTemp = 0
            ' Else
            '     MsgBox("Invalid checksum", MsgBoxStyle.Exclamation)
            'End If
        End If
    End Sub

    Function isPrioActiveGame() As Boolean
        Return user.activeGamePrioQueue.Count > 0 AndAlso user.activeGamePrioQueue(0).Equals(Me)
    End Function

    Shared Function getToday() As String
        Return Now.ToShortDateString()
    End Function

    Function getTotalTime(Optional addTemp As Boolean = True) As Long
        Dim sum As Long = 0
        For Each pair In timePairs
            sum += pair.Value
        Next
        sum += todayTime
        If addTemp Then
            sum += todayTimeTemp
        End If
        Return sum
    End Function

    Function isInGroupPanel() As Boolean
        Return id >= maxGameCount - 1
    End Function

    Function getGroupPanelIndex() As Integer
        Return (id + 1) - maxGameCount
    End Function

    Public Overrides Function ToString() As String
        If String.IsNullOrWhiteSpace(name) Then Return "[" & section & "]"
        Return name
    End Function


    Public Class GameSortingComparer
        Implements IComparer

        Dim primary As Form1.SortingMethod
        Dim secondary As Form1.SortingMethod
        Dim order As New List(Of Game)

        Public Sub New(primary As Form1.SortingMethod, secondary As Form1.SortingMethod)
            Me.primary = primary
            Me.secondary = secondary
            Dim iniVal As String = Form1.dll.iniReadValue("Config", "sortingOrder", "", Form1.iniPath)
            If Not iniVal = "" Then
                Dim split() As String = iniVal.Split(";")
                If split IsNot Nothing Then
                    For Each val As String In split
                        Dim game As Game = User.getMe().getGameBySection(val)
                        If game IsNot Nothing Then
                            order.Add(game)
                        End If
                    Next
                End If
            End If
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim primaryRes As Integer = compareSwitch(primary, x, y)
            If Not primaryRes = 0 Then
                Return primaryRes
            End If
            Return compareSwitch(secondary, x, y)
        End Function

        Private Function compareSwitch(type As Form1.SortingMethod, ByVal x As Object, ByVal y As Object) As Integer
            Dim res As Integer
            Select Case type
                Case Form1.SortingMethod.TIME
                    res = compareTime(x, y)
                Case Form1.SortingMethod.ALPHABET
                    res = compareAlphabet(x, y)
                Case Form1.SortingMethod.MANUAL
                    res = compareManual(x, y)
            End Select
            Return res
        End Function

        Private Function compareTime(ByVal x As Object, ByVal y As Object) As Integer
            Dim diff As Long = x.getTime() - y.getTime()
            If diff < 0 Then
                Return 1
            ElseIf diff > 0 Then
                Return -1
            Else
                Return 0
            End If
        End Function

        Private Function compareAlphabet(ByVal x As Object, ByVal y As Object) As Integer
            Return CStr(x.ToString()).CompareTo(CStr(y.ToString()))
        End Function

        Private Function compareManual(ByVal x As Object, ByVal y As Object) As Integer
            For Each orderedGame In order
                If orderedGame.section.ToLower() = x.section.ToLower() Then
                    Return -1
                ElseIf orderedGame.section.ToLower() = y.section.ToLower() Then
                    Return 1
                End If
            Next
            Return 0
        End Function
    End Class


    Public Class GameTimeComparer
        Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim diff As Long = x.getTime() - y.getTime()
            If diff < 0 Then
                Return 1
            ElseIf diff > 0 Then
                Return -1
            Else
                Return CStr(x.ToString()).CompareTo(CStr(y.ToString()))
            End If
        End Function
    End Class


End Class
