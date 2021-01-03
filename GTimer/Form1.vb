Imports System.IO
Imports System.IO.Compression

Public Class Form1
    'v1.0

    Public dll As New Utils
    Public iniPath As String = AppDomain.CurrentDomain.BaseDirectory & "\gtimer.ini"
    Public basePath As String = AppDomain.CurrentDomain.BaseDirectory

    Dim appName = "GTimer"

    Public games As List(Of Game)
    Public lastOptionsState As OptionsForm.optionState
    Public globalMode As FetchMethod
    Public startDate As Date
    Public endDate As Date

    Enum FetchMethod
        ALLTIME
        TODAY
        LAST_3_DAYS
        LAST_WEEK
        LAST_MONTH
        LAST_YEAR
        CUSTOM
    End Enum

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Application.CommandLineArgs.Count > 0 Then
            Dim para As String = My.Application.CommandLineArgs(0)
            If para.StartsWith("up") Then
                install()
            End If
        End If


        dll.inipath = iniPath

        Dim startDateValue As String = dll.iniReadValue("Config", "startDate", 0)
        Dim endDateValue As String = dll.iniReadValue("Config", "endDate", 0)

        If Not Date.TryParse(startDateValue, startDate) Then
            If Not Integer.TryParse(startDateValue, New Integer()) Then
                startDateValue = 0
            End If
            startDate = Date.Now.AddDays(startDateValue)
        End If
        If Not Date.TryParse(endDateValue, endDate) Then
            If Not Integer.TryParse(endDateValue, New Integer()) Then
                endDateValue = 0
            End If
            endDate = Date.Now.AddDays(endDateValue)
        End If
        setModeRadio()

        games = New List(Of Game)
        Dim secs As List(Of String) = dll.iniGetAllSectionsList()
        secs.Remove("Config")
        For i = 0 To secs.Count - 1
            games.Add(New Game(i, secs(i)))
        Next
        meMid(True)

        initSummaryPanel()
        updateLabels(True)

        Try
            My.Computer.Registry.LocalMachine.CreateSubKey("Software\Microsoft\Windows\CurrentVersion\Run").SetValue("GTimer", basePath & appName & ".exe")
        Catch ex As Exception

        End Try
        ' Me.WindowState = FormWindowState.Minimized
        ' Me.ShowInTaskbar = False

        tracker.Start()
        tempWriter.Start()
    End Sub

    Sub meMid(Optional setSize = False)
        If setSize Then
            Dim gamesCount As Integer = 3
            If games IsNot Nothing AndAlso games.Count > 3 Then
                gamesCount = games.Count
            End If
            Me.Width = GamePanel.baseLeft + GamePanel.baseSideMargin * 2 + games.Count * (GamePanel.siz.Width + GamePanel.gap)
        End If
        Me.Location = New Point(My.Computer.Screen.Bounds.Width / 2 - Me.Width / 2, My.Computer.Screen.Bounds.Height / 2 - Me.Height / 2)
    End Sub
    Function dateRangesToFetchMethod(Optional startDt As Date = Nothing, Optional endDt As Date = Nothing) As FetchMethod
        If startDt = Nothing Then startDt = startDate
        If endDt = Nothing Then endDt = endDate
        If dll.GetDayDiff(endDt, Now) = 0 Then
            Dim diff As Integer = dll.GetDayDiff(startDt, Now)
            If diff = 0 Then
                Return FetchMethod.TODAY
            ElseIf diff = 2 Then
                Return FetchMethod.LAST_3_DAYS
            ElseIf diff = 6 Then
                Return FetchMethod.LAST_WEEK
            ElseIf diff = 29 Then
                Return FetchMethod.LAST_MONTH
            ElseIf diff = 364 Then
                Return FetchMethod.LAST_YEAR
            ElseIf diff > 364 Then
                Return FetchMethod.ALLTIME
            End If
        End If
        Return FetchMethod.CUSTOM
    End Function

    Sub setModeRadio()
        Dim method As FetchMethod = dateRangesToFetchMethod()
        Select Case method
            Case FetchMethod.ALLTIME
                radAlltime.Checked = True
            Case FetchMethod.TODAY
                radToday.Checked = True
            Case FetchMethod.LAST_3_DAYS
                rad3.Checked = True
            Case FetchMethod.LAST_WEEK
                radWeek.Checked = True
            Case FetchMethod.LAST_MONTH
                radMonth.Checked = True
            Case FetchMethod.LAST_YEAR
                radYear.Checked = True
            Case FetchMethod.CUSTOM
                radCustom.Checked = True
        End Select
    End Sub

    Sub setStartEndDate()
        endDate = Now
        If radAlltime.Checked Then
            startDate = Now.AddDays(-1000)
        ElseIf radToday.Checked Then
            startDate = Now
        ElseIf rad3.Checked Then
            startDate = Now.AddDays(-2)
        ElseIf radWeek.Checked Then
            startDate = Now.AddDays(-6)
        ElseIf radMonth.Checked Then
            startDate = Now.AddDays(-29)
        ElseIf radYear.Checked Then
            startDate = Now.AddDays(-364)
        ElseIf radCustom.Checked Then
            startDate = startDatePicker.Value
            endDate = endDatePicker.Value
        End If
    End Sub


    Private Sub tracker_Tick(sender As Object, e As EventArgs) Handles tracker.Tick
        For Each game In games
            game.trackerUpdate()
        Next
        updateSummary()
    End Sub

    Sub updateLabels(reload As Boolean)
        For Each game In games
            game.updatePanel(reload)
        Next
        updateSummary()
    End Sub

    Private Sub tempWriter_Tick(sender As Object, e As EventArgs) Handles tempWriter.Tick
        updateLabels(True)
    End Sub

    Private Sub optionButton_Click(sender As Object, e As EventArgs) Handles optionButton.Click
        OptionsForm.ShowDialog()
    End Sub

    Sub install()
        killProc("GTimer", True)
        Dim currPath As String = My.Application.Info.DirectoryPath
        Dim copyPath As String = ""
        For i = 1 To My.Application.CommandLineArgs.Count - 1
            copyPath &= My.Application.CommandLineArgs(i) & IIf(i = My.Application.CommandLineArgs.Count - 1, "", " ")
        Next
        MsgBox("Starting Installation...")
1:      Dim fils() As String = Nothing
        Try
            Dim sr As New StreamReader(currPath.Substring(0, currPath.LastIndexOf("\")) & "\releases")
            fils = sr.ReadToEnd().Split(";")
            sr.Close()
            For i = 0 To fils.Length - 1
                fils(i) = fils(i).Replace(";", "")
            Next
        Catch ex As Exception
            If MsgBox("Reading release manifest failed." & vbNewLine & vbNewLine &
                      currPath.Substring(0, currPath.LastIndexOf("\")) & "\releases" &
                      vbNewLine & vbNewLine & "Try again?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then
                GoTo 1
            Else
                Environment.Exit(0)
            End If
        End Try

        If fils IsNot Nothing Then
            Dim archiveEntries As New List(Of List(Of ZipArchiveEntry))
            For i = 0 To fils.Length - 1
                If CStr(currPath & "\" & fils(i)).EndsWith(".zip") Then
                    archiveEntries.Add(getArchiveEntries(currPath & "\" & fils(i)))
                End If
            Next

            Dim fileList As New List(Of String)
            For Each archive In archiveEntries
                For Each entry In archive
                    fileList.Add(entry.FullName)
                Next
            Next

            For Each fil As String In fileList
                File.Delete(copyPath & "\" & fil)
                File.Copy(currPath & "\" & fil, copyPath & "\" & fil)
            Next
            Try
                Dim wr As New StreamWriter(copyPath & "\version", False)
                wr.Write(currPath.Substring(currPath.LastIndexOf("\") + 8))
                wr.Close()
            Catch ex As Exception
            End Try
            Process.Start(copyPath & "\GTimer.exe")
            Environment.Exit(0)
        Else
            MsgBox("Release manifest is corrupted.")
        End If

    End Sub
    Function extractArchive(archivePath As String, destination As String)
        ZipFile.ExtractToDirectory(archivePath, destination)
        Return True
    End Function

    Function getArchiveEntries(archivePath As String) As List(Of ZipArchiveEntry)
        Dim archive As ZipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Read)
        Dim res As New List(Of ZipArchiveEntry)
        For Each entry As ZipArchiveEntry In archive.Entries
            res.Add(entry)
        Next
        Return res
    End Function

    Sub killProc(ByVal name As String, Optional excludeOwn As Boolean = False)
        Try
            For Each p As Process In Process.GetProcessesByName(name)
                If Not p.Id = Process.GetCurrentProcess().Id Or Not excludeOwn Then
                    p.Kill()
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub radMode_CheckedChanged(sender As Object, e As EventArgs) Handles radAlltime.CheckedChanged, radToday.CheckedChanged, rad3.CheckedChanged, radWeek.CheckedChanged, radMonth.CheckedChanged, radYear.CheckedChanged, radCustom.CheckedChanged
        startDatePicker.Visible = sender.Equals(radCustom)
        endDatePicker.Visible = sender.Equals(radCustom)
    End Sub

    Private Sub radMode_Click(sender As Object, e As EventArgs) Handles radAlltime.Click, radToday.Click, rad3.Click, radWeek.Click, radMonth.Click, radYear.Click, radCustom.Click
        setStartEndDate()
        updateLabels(True)

    End Sub

    Private Sub startDatePicker_ValueChanged(sender As Object, e As EventArgs) Handles startDatePicker.ValueChanged, endDatePicker.ValueChanged
        setStartEndDate()
        updateLabels(True)
    End Sub

    Dim summaryPanelGap As Integer = 50
    Sub initSummaryPanel()
        statsGroup.Size = New Size((GamePanel.siz.Width + GamePanel.gap) * 3 - GamePanel.gap, 300)
        statsGroup.Location = New Point(GamePanel.baseLeft + GamePanel.baseSideMargin, GamePanel.baseTop + GamePanel.siz.Height + summaryPanelGap)
        totalTimeCaptionLabel.Left = statsGroup.Width / 2 - totalTimeCaptionLabel.Width / 2
        totalTimeLabel.Left = statsGroup.Width / 2 - totalTimeLabel.Width / 2
    End Sub

    Sub updateSummary()
        Dim totalTime As Long = [Game].getTotalTime()
        totalTimeLabel.Text = dll.SecondsTodhmsString(totalTime)
        totalTimeLabel.Left = statsGroup.Width / 2 - totalTimeLabel.Width / 2

        Dim sortedGames As List(Of Game) = [Game].sortGamesByTime()
        Dim skipped As Integer = 0
        For i = 0 To sortedGames.Count - 1
            If Not sortedGames(i).include Then
                skipped += 1
            End If
            sortedGames(i).panel.updateSummary(i - skipped, totalTime)
        Next
    End Sub
End Class
