Public Class Game

    ReadOnly Property dll() As Utils
        Get
            Return Form1.dll
        End Get
    End Property

    Public id As Integer
    Public exe As String
    Public section As String
    Public timeTemp As Integer
    Public time As Long
    Public active As Boolean
    Public logoPath As String
    Dim panel As GamePanel


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
        panel.update(getTime(FetchMethod.ALL, reload) + timeTemp)
    End Sub

    Function getTime(method As FetchMethod, reload As Boolean)
        If Not reload And time > 0 Then
            Return time
        End If

        Dim dates As List(Of String) = getAllTimeKeys()
        Dim times As List(Of String) = getAllTimeValues()
        Dim sum As Long = 0
        For i = 0 To times.Count - 1
            Dim diff = dll.GetDayDiff(dates(i), getToday())
            Dim passflag As Boolean = method = FetchMethod.ALL Or method = FetchMethod.TODAY And diff = 0 Or method = FetchMethod.LAST_WEEK And diff <= 7 Or method = FetchMethod.LAST_MONTH And diff <= 30 Or method = FetchMethod.LAST_YEAR And diff <= 365
            If passflag Then
                sum += CInt(times(i))
            End If

        Next
        time = sum
        Return sum
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

    Enum FetchMethod
        ALL
        TODAY
        LAST_WEEK
        LAST_MONTH
        LAST_YEAR
    End Enum

    Sub writeTemp()
        If timeTemp > 0 Then
            Dim currTime As Long = getTime(FetchMethod.TODAY, True)
            dll.iniWriteValue(section, getToday(), currTime + timeTemp)
            timeTemp = 0
        End If

    End Sub

    Function getToday() As String
        Return Now.ToShortDateString()
    End Function
End Class
