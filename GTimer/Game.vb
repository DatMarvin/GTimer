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
    Public timeTemp As Integer
    Public time As Long
    Public active As Boolean
    Public logoPath As String
    Public include As Boolean
    Public panel As GamePanel


    Sub New(id As Integer, section As String)
        Me.id = id
        Me.section = section
        loadSettings()
        panel = New GamePanel(Me)
        panel.init()
    End Sub

    Sub loadSettings()
        exe = dll.iniReadValue(section, "exe", "")
        logoPath = dll.iniReadValue(section, "logo", "")
        name = dll.iniReadValue(section, "name", section)
        include = dll.iniReadValue(section, "include", True)
    End Sub

    Sub trackerUpdate()
        Dim processes As List(Of Process) = Process.GetProcessesByName(exe).ToList
        If processes IsNot Nothing And processes.Count > 0 Then
            timeTemp += 1
            updatePanel(False)
            active = True
        Else
            updatePanel(active)
            active = False
        End If
    End Sub

    Sub updatePanel(reload As Boolean)
        If reload Then
            writeTemp()
        End If
        panel.update(getTime(reload))
    End Sub

    Function checksum() As Integer
        Dim totalTime As Long = getTime(True, False)
        Dim hash = totalTime.GetHashCode()
        Return hash
    End Function

    Function getTime(reload As Boolean, Optional addTemp As Boolean = True) As Long
        If Not reload And time > 0 Then
            Return time + IIf(addTemp, timeTemp, 0)
        End If

        Dim dates As List(Of String) = getAllTimeKeys()
        Dim times As List(Of String) = getAllTimeValues()
        Dim sum As Long = 0
        For i = 0 To times.Count - 1
            Dim dt As Date = Date.Parse(dates(i))

            Dim lowDiff = dll.GetDayDiff(dt.Date, Form1.startDate.Date)
            Dim highDiff = dll.GetDayDiff(dt.Date, Form1.endDate.Date)

            If lowDiff <= 0 And highDiff >= 0 Then
                sum += CInt(times(i))
            End If

        Next
        time = sum + IIf(addTemp, timeTemp, 0)
        Return time
    End Function

    Function getAllTimeValues() As List(Of String)
        Dim dates As List(Of String) = dll.iniGetAllKeysList(section)
        Dim times As List(Of String) = dll.iniGetAllValuesList(section)
        For i = dates.Count - 1 To 0 Step -1
            If Not Date.TryParse(dates(i), New Date()) Then
                dates.RemoveAt(i)
                times.RemoveAt(i)
            End If
        Next
        Return times
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





    Sub writeTemp()
        If timeTemp > 0 Then
            Dim currTime As Long = getTime(True, False)
            Dim prevChecksum As Integer = dll.iniReadValue(section, "checksum", 0)
            If prevChecksum <> 0 And prevChecksum = checksum() Or currTime = 0 Or True Then
                Dim newTime As Long = currTime + timeTemp
                Dim check As Integer = newTime.GetHashCode()
                dll.iniWriteValue(section, getToday(), newTime)
                ' dll.iniWriteValue(section, "checksum", check)
                timeTemp = 0
            Else
                '     MsgBox("Invalid checksum", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Shared Function getToday() As String
        Return Now.ToShortDateString()
    End Function

    Shared Function getTotalTime() As Long
        Dim gameSum As Long = 0
        For i = 0 To Form1.games.Count - 1
            If Form1.games(i).include Then
                gameSum += Form1.games(i).getTime(False)
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
            Dim diff As Long = x.getTime(False) - y.getTime(False)
            If diff < 0 Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class



End Class
