Imports System.IO
Imports System.IO.Compression

Public Class Form1
    'v1.0

    Public dll As New Utils

    Public basePath As String = AppDomain.CurrentDomain.BaseDirectory
    Public iniPath As String = basePath & "gtimer.ini"
    Public resPath As String = basePath & "\res\"

    Public exeName = IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath)
    Public Const appName = "GTimer"
    Public Const version = "v1.3"
    Public Const minWidth As Integer = 1200
    Public Const minHeight As Integer = 750

    Public saveWinPosSize As Boolean
    Public autostartEnabled As Boolean 

    Public games As List(Of Game)
    Public lastOptionsState As OptionsForm.optionState
    Public globalMode As FetchMethod
    Public startDate As Date
    Public endDate As Date
    Public patchNotesVisible As Boolean
    Public globalFont As FontFamily

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

        MinimumSize = New Size(minWidth, minHeight)
        Hide()

        dll.inipath = iniPath

        saveWinPosSize = dll.iniReadValue("Config", "saveWinPosSize", 0)
        autostartEnabled = dll.iniReadValue("Config", "autostart", 0)
        Dim family As String = dll.iniReadValue("Config", "font", "Georgia")
        Try
            globalFont = New FontFamily(family)
        Catch ex As Exception
            globalFont = New FontFamily("Georgia")
        End Try

        Dim startDateValue As String = dll.iniReadValue("Config", "startDate", 0)
        Dim endDateValue As String = dll.iniReadValue("Config", "endDate", 0)

        dateConfigToDate(startDateValue, startDate)
        dateConfigToDate(endDateValue, endDate)
        setModeRadio()
        setViewRangeGUI()

        games = New List(Of Game)
        Dim secs As List(Of String) = dll.iniGetAllSectionsList()
        secs.Remove("Config")
        For i = 0 To secs.Count - 1
            games.Add(New Game(i, secs(i)))
        Next

        setControlFonts(Me)
        initSummaryPanel()
        updateLabels(False)

        If autostartEnabled And Not registryAutostartExists() Then
            registerAutostart()
        End If

        versionLabel.Text = appName & " " & version

        tracker.Start()
        tempWriter.Start()
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        For Each game In games
            Try
                game.writeTemp()
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub Form1_Click(sender As Object, e As EventArgs) Handles Me.Click
        If patchNotesVisible Then
            closePatchNotesOverlay()
        End If
    End Sub
    Sub dateConfigToDate(ByVal value As String, ByRef buffer As Date)
        If Not Date.TryParse(value, buffer) Then
            If Not Integer.TryParse(value, New Integer()) Then
                value = 0
            End If
            buffer = Date.Now.AddDays(value)
        End If
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
                dateConfigToDate(startDate, startDatePicker.Value)
                dateConfigToDate(endDate, endDatePicker.Value)
        End Select
    End Sub

    Sub setStartEndDate()
        If radCustom.Checked Then

            dateConfigToDate(dll.iniReadValue("Config", "startDate"), startDatePicker.Value)
            dateConfigToDate(dll.iniReadValue("Config", "endDate"), endDatePicker.Value)
            startDate = startDatePicker.Value
            endDate = endDatePicker.Value
        Else
            endDate = Now
            dll.iniWriteValue("Config", "endDate", 0)
            If radAlltime.Checked Then
                startDate = Now.AddDays(-1000)
                dll.iniWriteValue("Config", "startDate", -1000)
            ElseIf radToday.Checked Then
                startDate = Now.Date
                dll.iniWriteValue("Config", "startDate", 0)
            ElseIf rad3.Checked Then
                startDate = Now.AddDays(-2)
                dll.iniWriteValue("Config", "startDate", -2)
            ElseIf radWeek.Checked Then
                startDate = Now.AddDays(-6)
                dll.iniWriteValue("Config", "startDate", -6)
            ElseIf radMonth.Checked Then
                startDate = Now.AddDays(-29)
                dll.iniWriteValue("Config", "startDate", -29)
            ElseIf radYear.Checked Then
                startDate = Now.AddDays(-364)
                dll.iniWriteValue("Config", "startDate", -364)
            End If

        End If
    End Sub


    Private Sub tracker_Tick(sender As Object, e As EventArgs) Handles tracker.Tick
        For Each game In games
            game.trackerUpdate()
        Next
        updateSummary()
    End Sub


    Sub updateLabels(writeTemps As Boolean)
        For Each game In games
            game.updatePanel()
            If writeTemps Then
                game.writeTemp()
            End If
        Next
        updateSummary()
    End Sub

    Private Sub tempWriter_Tick(sender As Object, e As EventArgs) Handles tempWriter.Tick
        updateLabels(True)
    End Sub

    Private Sub optionButton_Click(sender As Object, e As EventArgs) Handles optionButton.Click
        OptionsForm.Show()
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
        setViewRangeGUI()
        updateLabels(False)
    End Sub

    Private Sub startDatePicker_ValueChanged(sender As Object, e As EventArgs) Handles startDatePicker.ValueChanged

    End Sub
    Private Sub startDatePicker_CloseUp(sender As Object, e As EventArgs) Handles startDatePicker.CloseUp
        dll.iniWriteValue("Config", "startDate", startDatePicker.Value.ToShortDateString())
        setStartEndDate()
        updateLabels(False)
    End Sub

    Private Sub endDatePicker_ValueChanged(sender As Object, e As EventArgs) Handles endDatePicker.ValueChanged

    End Sub
    Private Sub endDatePicker_CloseUp(sender As Object, e As EventArgs) Handles endDatePicker.CloseUp
        dll.iniWriteValue("Config", "endDate", endDatePicker.Value.ToShortDateString())
        setStartEndDate()
        updateLabels(False)
    End Sub

    Dim summaryPanelGap As Integer = 50
    Sub initSummaryPanel()
        Dim gameCount As Integer = 3
        If games.Count > 3 Then
            gameCount = games.Count
        End If
        statsGroup.Size = New Size((GamePanel.siz.Width + GamePanel.gap) * gameCount - GamePanel.gap, 300)
        statsGroup.Location = New Point(GamePanel.baseLeft + GamePanel.baseSideMargin, GamePanel.baseTop + GamePanel.siz.Height + summaryPanelGap)
        totalTimeCaptionLabel.Left = statsGroup.Width / 2 - totalTimeCaptionLabel.Width / 2
        totalTimeLabel.Left = statsGroup.Width / 2 - totalTimeLabel.Width / 2
    End Sub

    Sub updateSummary()
        Dim totalTime As Long = [Game].getTotalTimeForAllGames()
        totalTimeLabel.Text = dll.SecondsTodhmsString(totalTime, "ZERRO")
        If Game.isOneGameIncludedActive() Then
            totalTimeLabel.ForeColor = getFontColor(LabelMode.RUNNING)
        ElseIf Game.isOneGameIncluded() Then
            totalTimeLabel.ForeColor = getFontColor(LabelMode.NORMAL)
        Else
            totalTimeLabel.ForeColor = getFontColor(LabelMode.INACTIVE)
        End If
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

    Enum LabelMode
        NORMAL
        RUNNING
        INACTIVE
        INACTIVE_RUNNING
        RUNNING_BLOCKED
        INACTIVE_RUNNING_BLOCKED
    End Enum
    Function getFontColor(labelMode As LabelMode) As Color
        Select Case labelMode
            Case LabelMode.NORMAL
                Return Color.White
            Case LabelMode.RUNNING
                Return Color.Green
            Case LabelMode.INACTIVE
                Return Color.Gray
            Case LabelMode.INACTIVE_RUNNING
                Return Color.FromArgb(25, 75, 25)
            Case LabelMode.RUNNING_BLOCKED
                Return Color.FromArgb(100, 0, 0)
            Case LabelMode.INACTIVE_RUNNING_BLOCKED
                Return Color.FromArgb(50, 25, 25)
        End Select
    End Function

    Sub setControlFonts(container As Control)
        For Each c As Control In container.Controls
            If Not c.Text = "" Then
                If Not c.Equals(startDatePicker) And Not c.Equals(endDatePicker) Then
                    c.Font = New Font(globalFont, c.Font.Size)
                End If

            End If
            If Not c.Name = "" Then
                If c.Controls.Count > 0 Then
                    setControlFonts(c)
                End If

            End If
        Next
    End Sub


    Private Sub versionLabel_Click(sender As Object, e As EventArgs) Handles versionLabel.Click
        If Not patchNotesVisible Then
            patchTree.Size = New Size(700, 500)
            patchTree.Location = New Point(versionLabel.Left, versionLabel.Bottom + 5)
            patchTree.BringToFront()
            patchTree.Visible = True
            If patchTree.Nodes IsNot Nothing Then
                Dim currNode As TreeNode = getCurrVersionNode(patchTree.Nodes(0))
                currNode.Expand()
                currNode.EnsureVisible()
                patchTree.SelectedNode = currNode
            End If
            patchNotesClosePic.Location = New Point(patchTree.Left + patchTree.Width / 2 - patchNotesClosePic.Width / 2, patchTree.Top + 2)
            patchNotesClosePic.BackColor = patchTree.BackColor
            patchNotesClosePic.Visible = True
            patchNotesClosePic.BringToFront()
            patchNotesVisible = True
        Else
            closePatchNotesOverlay()
        End If
    End Sub
    Private Sub patchNotesClosePic_Click(sender As Object, e As EventArgs) Handles patchNotesClosePic.Click
        closePatchNotesOverlay()
    End Sub
    Sub closePatchNotesOverlay()
        patchNotesClosePic.Visible = False
        patchTree.Visible = False
        patchNotesVisible = False
    End Sub
    Function getCurrVersionNode(currNode As TreeNode) As TreeNode
        Dim res As TreeNode = currNode
        For Each n As TreeNode In currNode.Nodes
            If n.Text.Contains(version) Then
                Return n
            End If
            res = getCurrVersionNode(n)
        Next
        Return res
    End Function

    Private Sub Form1_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        saveWinPos()
    End Sub


    Private Sub Form1_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        saveWinSize()
    End Sub

    Sub saveWinPos()
        If WindowState = FormWindowState.Normal Then
            OptionsForm.labelWinPos.Text = "(" & Left & ", " & Top & ")"
            dll.iniWriteValue("Config", "winPos", IIf(Left < -Width + 5, 0, Left) & ";" & IIf(Top < -20, 0, Top))
        ElseIf WindowState = FormWindowState.Maximized Then
            OptionsForm.labelWinPos.Text = "(0, 0)"
        End If
    End Sub
    Sub saveWinSize()
        OptionsForm.labelWinSize.Text = "(" & Width & ", " & Height & ")"
        dll.iniWriteValue("Config", "winSize", IIf(Width < minWidth, minWidth, Width) & ";" & IIf(Height < minHeight, minHeight, Height))
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If saveWinPosSize Then
            loadWinPosSize()
        End If
    End Sub

    Sub loadWinPosSize()
        Dim x, y, w, h As Integer
        Dim siz As String = dll.iniReadValue("Config", "winSize", "0;0", iniPath)
        Try
            w = siz.Split(";")(0)
            h = siz.Split(";")(1)
            If w < minWidth Then w = minWidth
            If h < minHeight Then h = minHeight
        Catch ex As Exception
            w = minWidth : h = minHeight
        End Try
        Dim pos As String = dll.iniReadValue("Config", "winPos", "0;0", iniPath)
        Try
            x = pos.Split(";")(0)
            y = pos.Split(";")(1)
        Catch ex As Exception
            x = My.Computer.Screen.WorkingArea.Width / 2 - Width / 2
            y = My.Computer.Screen.WorkingArea.Height / 2 - Height / 2
        End Try
        Size = New Size(w, h)
        Location = New Point(x, y)
        Dim startState As Integer = dll.iniReadValue("Config", "startState", 1)
        If startState = 0 Then
            WindowState = FormWindowState.Minimized
            ShowInTaskbar = False
        ElseIf startState = 2 Then
            Show()
            WindowState = FormWindowState.Maximized

        Else
            Show()
            WindowState = FormWindowState.Normal
        End If
    End Sub

    Function registerAutostart() As Boolean
        Try
            My.Computer.Registry.LocalMachine.CreateSubKey("Software\Microsoft\Windows\CurrentVersion\Run\").SetValue(appName, basePath & exeName & ".exe")
        Catch ex As Exception
            Return False 'MsgBox("Failed to create key in registry")
        End Try
        Return True
    End Function

    Function unregisterAutostart() As Boolean
        Try
            My.Computer.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True).DeleteSubKey(appName)
        Catch ex As Exception
            Return False ' MsgBox("Failed to delete from registry")
        End Try
        Return True
    End Function

    Function registryAutostartExists() As Boolean
        Return Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run\" & appName) IsNot Nothing
    End Function

    Private Sub iconTray_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles iconTray.MouseDoubleClick
        Me.Show()
        ShowInTaskbar = True
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            iconTray.Visible = True
            Me.Hide()
        End If
    End Sub

    Private Sub startDatePicker_VisibleChanged(sender As Object, e As EventArgs) Handles startDatePicker.VisibleChanged
        setViewRangeGUI()
    End Sub

    Sub setViewRangeGUI()
        If startDatePicker.Visible Then
            settingsGroup.Height = 318
        Else
            settingsGroup.Height = 270
        End If
        rangeLeftShiftPic.Visible = Not radAlltime.Checked
        rangeRightShiftPic.Visible = Not radAlltime.Checked

    End Sub

    Private Sub rangeLeftShiftPic_Click(sender As Object, e As EventArgs) Handles rangeLeftShiftPic.Click
        shiftDateRange(-1)
    End Sub
    Private Sub rangeRightShiftPic_Click(sender As Object, e As EventArgs) Handles rangeRightShiftPic.Click
        shiftDateRange(1)
    End Sub

    Sub shiftDateRange(dir As Integer)
        Dim diff As Integer = dll.GetDayDiff(startDate.Date, endDate.date)
        startDate = startDate.AddDays(dir * (diff + 1))
        endDate = endDate.AddDays(dir * (diff + 1))

        setModeRadio()
        startDatePicker.Value = startDate
        endDatePicker.Value = endDate
        updateLabels(False)
    End Sub

End Class

