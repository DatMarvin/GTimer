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

    Sub New(id As Integer, section As String)
        Me.id = id
        Me.section = section
        loadSettings()
        timePairs = New List(Of KeyValuePair(Of String, Integer))
        initTimePairs()
        initTodayTime()
        panel = New GamePanel(Me)
        panel.init()
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
                Game.activeGamePrioQueue.add(Me)
            End If
            If isPrioActiveGame() Then
                todayTimeTemp += 1
            End If

            updatePanel()
                active = True
            Else
                If active Then
                writeTemp()
            End If
            updatePanel()
            Game.activeGamePrioQueue.remove(Me)
            active = False
        End If
    End Sub

    Sub updatePanel()
        panel.update(getTime())
    End Sub

    Function checksum() As Integer
        ' Dim totalTime As Long = getTime(True, False)
        ' Dim hash = totalTime.GetHashCode()
        '  Return hash
        Return 0
    End Function

    Function getTime() As Long
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
            sum += todayTimeTemp
        End If
        Return sum
    End Function

    Function loadTodayTime() As Long
        Dim valueString As String = dll.iniReadValue(section, Now.Date.ToShortDateString(), "")
        If valueString = "" Then
            Return 0
        Else
            Return CInt(valueString)
        End If
    End Function

    Function getAllTimeValues() As List(Of Integer)
        Dim dates As List(Of String) = dll.iniGetAllKeysList(section)
        Dim timesString As List(Of String) = dll.iniGetAllValuesList(section)
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
    Function getAllTimeKeys() As List(Of String)
        Dim dates As List(Of String) = dll.iniGetAllKeysList(section)
        For i = dates.Count - 1 To 0 Step -1
            If Not Date.TryParse(dates(i), New Date()) Then
                dates.RemoveAt(i)
            End If
        Next
        Return dates
    End Function

    Sub initTimePairs()
        Dim dates As List(Of String) = getAllTimeKeys()
        Dim times As List(Of Integer) = getAllTimeValues()
        For i = 0 To dates.Count - 1
            If Not dll.GetDayDiff(Date.Parse(dates(i)), Now.Date) = 0 Then
                timePairs.Add(New KeyValuePair(Of String, Integer)(dates(i), times(i)))
            End If
        Next
    End Sub

    Sub initTodayTime()
        todayTime = loadTodayTime()
    End Sub


    Sub writeTemp()
        If todayTimeTemp > 0 Then
            Dim currTime As Long = loadTodayTime()
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
        Return Game.activeGamePrioQueue.Count > 0 And Game.activeGamePrioQueue(0).Equals(Me)
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

    Shared Function getTotalTimeForAllGames() As Long
        Dim gameSum As Long = 0
        For i = 0 To Form1.games.Count - 1
            If Form1.games(i).include Then
                gameSum += Form1.games(i).getTime()
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
            Dim diff As Long = x.getTime() - y.getTime()
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

End Class
