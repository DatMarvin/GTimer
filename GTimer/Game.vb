Public Class Game

    ReadOnly Property dll() As Utils
        Get
            Return Form1.dll
        End Get
    End Property

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

    Public panel As GamePanel

    Public Shared activeGamePrioQueue As New List(Of Game)
    Public Shared firstLogEntry As Date = Nothing
    Public Shared ReadOnly Property getFirstLogEntry() As Date
        Get
            If firstLogEntry = Nothing Then
                Return getFirstLogEntryDate()
            Else
                Return firstLogEntry
            End If
        End Get
    End Property

    Sub New(id As Integer, section As String)
        Me.id = id
        Me.section = section
        loadSettings()
        timePairs = New List(Of KeyValuePair(Of String, Integer))
        loadTime(Form1.iniPath)

        panel = New GamePanel(Me)
        panel.init()
        If firstLogEntry = Nothing Then firstLogEntry = getFirstLogEntryDate()
    End Sub

    Sub loadSettings()
        exe = dll.iniReadValue(section, "exe", "")
        logoPath = dll.iniReadValue(section, "logo", "")
        logoInvPath = dll.iniReadValue(section, "logo_inv", logoPath)
        name = dll.iniReadValue(section, "name", section)
        include = dll.iniReadValue(section, "include", True)
    End Sub

    Sub trackerUpdate()
        Dim processes As List(Of Process) = Process.GetProcessesByName(exe).ToList
        If processes IsNot Nothing And processes.Count > 0 Then
            If Not active Then
                Game.activeGamePrioQueue.Add(Me)
                Form1.publishStats()
            End If
            If isPrioActiveGame() Then
                todayTimeTemp += 1
            End If

            updatePanel()
            active = True
        Else
            If active Then
                writeTemp()
                Form1.publishStats()
            End If
            updatePanel()
            Game.activeGamePrioQueue.remove(Me)
            active = False
        End If
    End Sub

    Sub updatePanel()
        panel.update(getTime(User.isMeSelected()))
    End Sub

    Function checksum() As Integer
        ' Dim totalTime As Long = getTime(True, False)
        ' Dim hash = totalTime.GetHashCode()
        '  Return hash
        Return 0
    End Function

    Function getTime(addTemp As Boolean) As Long
        Dim sum As Long = 0
        For i = 0 To timePairs.Count - 1
            Dim dt As Date = Date.Parse(timePairs(i).Key)

            Dim lowDiff = dll.GetDayDiff(dt.Date, Form1.startDate.Date)
            Dim highDiff = dll.GetDayDiff(dt.Date, Form1.endDate.Date)

            If lowDiff <= 0 And highDiff >= 0 Then
                sum += timePairs(i).Value
            End If
        Next
        Dim todayLowDiff = dll.GetDayDiff(Now.Date, Form1.startDate.Date)
        Dim todayHighDiff = dll.GetDayDiff(Now.Date, Form1.endDate.Date)
        If todayLowDiff <= 0 And todayHighDiff >= 0 Then
            sum += todayTime
            If addTemp Then sum += todayTimeTemp
        End If
        Return sum
    End Function


    Function loadTodayTime(iniPath As String) As Long
        Dim valueString As String = dll.iniReadValue(section, Now.Date.ToShortDateString(), "", iniPath)
        If valueString = "" Then
            Return 0
        Else
            Return CInt(valueString)
        End If
    End Function

    Function getAllTimeValues(iniPath As String) As List(Of Integer)
        Dim dates As List(Of String) = dll.iniGetAllKeysList(section, iniPath)
        Dim timesString As List(Of String) = dll.iniGetAllValuesList(section, iniPath)
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
        For i = dates.Count - 1 To 0 Step -1
            If Not Date.TryParse(dates(i), New Date()) Then
                dates.RemoveAt(i)
            End If
        Next
        Return dates
    End Function

    Sub initTimePairs(iniPath As String)
        timePairs.Clear()
        Dim dates As List(Of String) = getAllTimeKeys(iniPath)
        Dim times As List(Of Integer) = getAllTimeValues(iniPath)
        For i = 0 To dates.Count - 1
            If Not dll.GetDayDiff(Date.Parse(dates(i)), Now.Date) = 0 Then
                timePairs.Add(New KeyValuePair(Of String, Integer)(dates(i), times(i)))
            End If
        Next
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
        Return Game.activeGamePrioQueue.Count > 0 AndAlso Game.activeGamePrioQueue(0).Equals(Me)
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
    Public Shared Function getTimeRatio(time As Long) As Double
        Dim effectiveStart As Date = Form1.startDate
        If [Game].getFirstLogEntry <> Nothing Then
            If [Game].firstLogEntry.CompareTo(effectiveStart) > 0 Then
                effectiveStart = [Game].firstLogEntry
            End If
        End If
        Dim diff As Integer = Form1.dll.GetDayDiff(effectiveStart.Date, Form1.endDate.Date)
        diff = Math.Max(diff, 0)
        Dim sliceRatio As Double = 1
        Dim mode As Integer = Form1.viewMode
        If mode > 0 Then
            sliceRatio = (diff + 1) / mode
        End If

        Dim timeRatio As Double = time / sliceRatio
        Return timeRatio
    End Function

    Public Shared Function getPrioActiveGame() As Game
        If Form1.games IsNot Nothing Then
            For Each game In Form1.games
                If game.isPrioActiveGame() Then
                    Return game
                End If
            Next
        End If
        Return Nothing
    End Function
    Shared Function getTotalTimeForAllGames() As Long
        Dim gameSum As Long = 0
        For i = 0 To Form1.games.Count - 1
            If Form1.games(i).include Then
                gameSum += Form1.games(i).getTime(User.isMeSelected())
            End If
        Next
        Return gameSum
    End Function
    Public Shared Function sortGamesByTime() As List(Of Game)
        Dim gameArray(Form1.games.Count - 1) As Game
        Form1.games.CopyTo(gameArray)
        Array.Sort(gameArray, New GameTimeComparer())
        Dim resList As List(Of Game) = gameArray.ToList()
        For i = resList.Count - 1 To 0 Step -1
            If Not resList(i).include Then
                '   resList.RemoveAt(i)
            End If
        Next
        Return resList
    End Function

    Class GameTimeComparer
        Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim diff As Long = x.getTime(User.isMeSelected()) - y.getTime(User.isMeSelected())
            If diff < 0 Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class

    Public Shared Function isOneGameIncluded() As Boolean
        If Form1.games IsNot Nothing Then
            For Each g As Game In Form1.games
                If g.include Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Public Shared Function isOneGameActive() As Boolean
        Return getPrioActiveGame() IsNot Nothing
    End Function

    Public Shared Function isOneGameIncludedActive() As Boolean
        If Form1.games IsNot Nothing Then
            For Each g As Game In Form1.games
                If g.include Then
                    If g.active Then
                        Return True
                    End If
                End If
            Next
        End If
        Return False
    End Function

    Shared Function getFirstLogEntryDate() As Date
        Dim res As Date = Nothing
        If Form1.games IsNot Nothing Then
            For Each g As Game In Form1.games
                Dim keys As List(Of String)
                If User.getSelected() IsNot Nothing Then
                    keys = g.getAllTimeKeys(User.getSelected().iniPath)
                Else
                    keys = g.getAllTimeKeys(Form1.iniPath)
                End If
                If keys.Count > 0 Then
                    Dim min As Date = keys.Min(Function(key)
                                                   Return Date.Parse(key)
                                               End Function)
                    If res = Nothing OrElse res.CompareTo(min) > 0 Then
                        res = min
                    End If
                End If
            Next
        End If
        firstLogEntry = res
        Return res
    End Function
End Class
