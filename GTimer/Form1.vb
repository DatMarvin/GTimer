Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.InteropServices

Public Class Form1

    Public dll As New Utils

    Public basePath As String = AppDomain.CurrentDomain.BaseDirectory
    Public iniPath As String = basePath & "gtimer.ini"
    Public resPath As String = basePath & "res\"
    Public sharedPath As String
    Public logPath As String = basePath & "log"

    Public exeName = IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath)
    Public Const appName = "GTimer"
    Public Const version = "v3.4"
    Public Const minWidth As Integer = 950
    Public Const minHeight As Integer = 750

    Public saveWinPosSize As Boolean
    Public autostartEnabled As Boolean
    Public showMinimizedInTaskbar As Boolean
    Public autoUpdate As Boolean
    Public isFrameLocked As Boolean
    Public alignToGrid As Boolean

    Public lastOptionsState As OptionsForm.optionState
    Public firstLoad As Boolean
    Public users As List(Of User)
    Public fswFlag As Boolean
    Public fswType As FileSystemEventArgs
    Public gamePanelCount As Integer

    Public globalMode As FetchMethod
    Public startDate As Date
    Public endDate As Date
    Public patchNotesVisible As Boolean
    Public globalFont As FontFamily
    Public viewMode As ViewModeAgg
    Public userName As String
    Public primarySort As SortingMethod
    Public secondarySort As SortingMethod
    Public invitesAllowed As Boolean
    Public inviteFlashEnabled As Boolean
    Public inviteTimeout As Integer
    Public inviteBlacklist As List(Of String)


    Public ReadOnly Property publishPath() As String
        Get
            Return sharedPath & "Releases\"
        End Get
    End Property
    Public ReadOnly Property sharedStatsPath() As String
        Get
            Return sharedPath & "stats\"
        End Get
    End Property
    Public ReadOnly Property getActiveUser() As User
        Get
            For Each user In users
                If user.selected Then Return user
            Next
            Return User.getMe()
        End Get
    End Property
    Public ReadOnly Property games() As List(Of Game)
        Get
            If getActiveUser() Is Nothing Then Return Nothing
            Return getActiveUser().games
        End Get
    End Property
    Enum FetchMethod
        ALLTIME
        TODAY
        LAST_3_DAYS
        LAST_WEEK
        LAST_MONTH
        LAST_YEAR
        CUSTOM
    End Enum

    Enum ViewModeAgg
        TOTAL = 0
        DAY = 1
        WEEK = 7
        MONTH = 30
        YEAR = 365
    End Enum

    Enum SortingMethod
        TIME
        ALPHABET
        MANUAL
        LAST_PLAYED
    End Enum

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Application.CommandLineArgs.Count > 0 Then
            Dim para As String = My.Application.CommandLineArgs(0)
            If para.StartsWith("up") Then
                install()
            End If
        End If

        attainSupremacy()

        MinimumSize = New Size(minWidth, minHeight)
        Hide()

        dll.inipath = iniPath

        sharedPath = Environment.ExpandEnvironmentVariables(dll.iniReadValue("Config", "sharedPath", "%OneDrive%\GTimer\"))

        saveWinPosSize = dll.iniReadValue("Config", "saveWinPosSize", 0)
        autostartEnabled = dll.iniReadValue("Config", "autostart", 0)
        showMinimizedInTaskbar = dll.iniReadValue("Config", "showInTaskbar", 0)
        autoUpdate = dll.iniReadValue("Config", "autoUpdate", 0)
        primarySort = dll.iniReadValue("Config", "primarySort", 0)
        secondarySort = dll.iniReadValue("Config", "secondarySort", 2)
        gamePanelCount = dll.iniReadValue("Config", "gamePanelCount", 4)
        isFrameLocked = dll.iniReadValue("Config", "frameLocked", 0)
        alignToGrid = dll.iniReadValue("Config", "alignToGrid", 1)


        Dim family As String = dll.iniReadValue("Config", "font", "Georgia")
        Try
            globalFont = New FontFamily(family)
        Catch ex As Exception
            globalFont = New FontFamily("Georgia")
        End Try
        viewMode = dll.iniReadValue("Config", "viewMode", "0")

        Dim startDateValue As String = dll.iniReadValue("Config", "startDate", 0)
        Dim endDateValue As String = dll.iniReadValue("Config", "endDate", 0)

        dateConfigToDate(startDateValue, startDate)
        dateConfigToDate(endDateValue, endDate)
        setViewRangeRadio()
        setViewRangeGUI()
        setAlignToGridGUI()

        loadInvitationSettings()

        loadUsers()

        setFrameLock()
        setControlFonts(Me)
        updateSummaryPanelUI()
        updateLabels(False)


        setViewModeGUI()
        setViewModeRadio()

        If autostartEnabled And Not registryAutostartExists() Then
            registerAutostart()
        End If

        appNameLabel.Text = appName
        setVersionLabel()

        publishStats(,, True)
        updateUserInfos()

        fsw.Path = sharedStatsPath
        fsw.IncludeSubdirectories = True
        fsw.Filter = "gtimer.ini"
        fsw.SynchronizingObject = Me

        tracker.Start()
        tempWriter.Start()

        firstLoad = True
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If WindowState = FormWindowState.Maximized Then
            dll.iniWriteValue("Config", "startState", 2)
        ElseIf WindowState = FormWindowState.Minimized Then
            dll.iniWriteValue("Config", "startState", 0)
        Else
            dll.iniWriteValue("Config", "startState", 1)
        End If
        writeTemps()
        publishStats(True)
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
        If dll.GetDayDiff(endDt.Date, Now.Date) = 0 Then
            Dim diff As Integer = dll.GetDayDiff(startDt.Date, Now.Date)
            If diff = 0 Then
                Return FetchMethod.TODAY
            ElseIf diff = 2 Then
                Return FetchMethod.LAST_3_DAYS
            ElseIf diff = 6 Or (alignToGrid And diff < 10 And startDt.DayOfWeek = 1 And endDt.DayOfWeek = 7) Then
                Return FetchMethod.LAST_WEEK
            ElseIf diff = 29 Or (alignToGrid And diff < 50 And startDt.Day = 1 And endDt.Day = Date.DaysInMonth(endDt.Year, endDt.Month)) Then
                Return FetchMethod.LAST_MONTH
            ElseIf diff = 364 Or (alignToGrid And diff < 500 And startDt.Day = 1 And startDt.Month = 1 And endDt.Day = Date.DaysInMonth(endDt.Year, 12)) And endDt.Month = 12 Then
                Return FetchMethod.LAST_YEAR
            ElseIf diff > 364 Then
                Return FetchMethod.ALLTIME
            End If
        End If

        Return getViewRangeAlignment(startDt, endDt)
    End Function

    Function getViewRangeAlignment(Optional startDt As Date = Nothing, Optional endDt As Date = Nothing) As FetchMethod
        If startDt = Nothing Then startDt = startDate
        If endDt = Nothing Then endDt = endDate
        Dim diff As Integer = dll.GetDayDiff(startDt, endDt)
        If diff < 10 And startDt.DayOfWeek = 1 And endDt.DayOfWeek = 7 Then
            Return FetchMethod.LAST_WEEK
        ElseIf diff < 50 And startDt.Day = 1 And endDt.Day = Date.DaysInMonth(endDt.Year, endDt.Month) Then
            Return FetchMethod.LAST_MONTH
        ElseIf diff < 500 And startDt.Day = 1 And startDt.Month = 1 And endDt.Day = Date.DaysInMonth(endDt.Year, 12) And endDt.Month = 12 Then
            Return FetchMethod.LAST_YEAR
        End If
        Return FetchMethod.CUSTOM
    End Function

    Sub setViewRangeRadio()
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
    Sub setViewModeRadio()

        Select Case viewMode
            Case ViewModeAgg.TOTAL
                radTotal.Checked = True
            Case ViewModeAgg.DAY
                If getViewModeRadEnabled(radAvDay) Then
                    radAvDay.Checked = True
                Else
                    viewMode = ViewModeAgg.TOTAL
                    radTotal.Checked = True
                End If
            Case ViewModeAgg.WEEK
                If getViewModeRadEnabled(radAvWeek) Then
                    radAvWeek.Checked = True
                Else
                    viewMode = ViewModeAgg.TOTAL
                    radTotal.Checked = True
                End If
            Case ViewModeAgg.MONTH
                If getViewModeRadEnabled(radAvMonth) Then
                    radAvMonth.Checked = True
                Else
                    viewMode = ViewModeAgg.TOTAL
                    radTotal.Checked = True
                End If
            Case ViewModeAgg.YEAR
                If getViewModeRadEnabled(radAvYear) Then
                    radAvYear.Checked = True
                Else
                    viewMode = ViewModeAgg.TOTAL
                    radTotal.Checked = True
                End If
        End Select
    End Sub

    Sub setStartEndDate()
        If radCustom.Checked Then

            ' dateConfigToDate(dll.iniReadValue("Config", "startDate"), startDatePicker.Value)
            ' dateConfigToDate(dll.iniReadValue("Config", "endDate"), endDatePicker.Value)
            startDate = startDatePicker.Value
            endDate = endDatePicker.Value
        Else

            If Not isAlignToGridInEffect() Then
                endDate = Now.Date
                dll.iniWriteValue("Config", "endDate", 0)
            End If

            If radAlltime.Checked Then
                Dim diff As Integer = -Now.Date.Subtract(getActiveUser().getFirstLogEntry.Date).TotalDays
                setStartDateHelper(radAlltime, diff)
            ElseIf radToday.Checked Then
                setStartDateHelper(radToday, 0)
            ElseIf rad3.Checked Then
                setStartDateHelper(rad3, -2)
            ElseIf radWeek.Checked Then
                setStartDateHelper(radWeek, -6)
            ElseIf radMonth.Checked Then
                setStartDateHelper(radMonth, -29)
            ElseIf radYear.Checked Then
                setStartDateHelper(radYear, -364)
            End If

        End If
    End Sub

    Sub setAlignToGridGUI()
        If alignToGrid Then
            alignToGridPic.BackgroundImage = My.Resources.grid
        Else
            alignToGridPic.BackgroundImage = My.Resources.no_grid
        End If
    End Sub

    Function isAlignToGridInEffect() As Boolean
        Return alignToGrid And (radWeek.Checked Or radMonth.Checked Or radYear.Checked)
    End Function
    Private Sub setStartDateHelper(rad As RadioButton, days As Integer)
        If isAlignToGridInEffect() Then
            alignStartEndToGrid(Now.Date)
        Else
            startDate = Now.Date.AddDays(days)
            dll.iniWriteValue("Config", "startDate", days)
        End If

        Dim effectiveStartDate As Date = getActiveUser().getEffectiveStartDate()
        tt.Show("Date Range:" & vbNewLine & effectiveStartDate.ToShortDateString() & " - " & endDate.ToShortDateString(), rad, 150, 0, 1500)
    End Sub
    Sub alignStartEndToGrid(refDate As Date)
        If radWeek.Checked Then
            Dim dayOfWeek = refDate.DayOfWeek
            If dayOfWeek = 0 Then dayOfWeek = 7
            startDate = refDate.AddDays(-(dayOfWeek - 1))
            endDate = refDate.AddDays(7 - dayOfWeek)
        ElseIf radMonth.Checked Then
            startDate = refDate.AddDays(-(refDate.Day - 1))
            endDate = refDate.AddDays(Date.DaysInMonth(refDate.Year, refDate.Month) - refDate.Day)
        ElseIf radYear.Checked Then
            startDate = refDate.AddMonths(-(refDate.Month - 1)).AddDays(-(refDate.Day - 1))
            endDate = refDate.AddMonths(12 - refDate.Month).AddDays(Date.DaysInMonth(refDate.Year, 12) - refDate.Day)
        End If
        dll.iniWriteValue("Config", "startDate", startDate.ToShortDateString)
        dll.iniWriteValue("Config", "endDate", endDate.ToShortDateString)
    End Sub

    Function dateRangeIncludeToday() As Boolean
        Dim todayLowDiff = dll.GetDayDiff(Now.Date, startDate.Date)
        Dim todayHighDiff = dll.GetDayDiff(Now.Date, endDate.Date)
        Return todayLowDiff <= 0 And todayHighDiff >= 0
    End Function


    Private Sub tracker_Tick(sender As Object, e As EventArgs) Handles tracker.Tick
        For Each game In User.getMe().games
            game.trackerUpdate()
        Next
        updateLabels(False)
        updateSummary()
    End Sub

    Sub writeTemps()
        If games IsNot Nothing Then
            For Each game In games
                Try
                    game.writeTemp()
                Catch ex As Exception
                End Try
            Next
        End If
    End Sub

    Sub updateLabels(writeTemps As Boolean)
        checkRearrangeGamePanels()
        For Each game In games

            game.updatePanel()

            If writeTemps Then
                game.writeTemp()
            End If
        Next

        updateSummary()
    End Sub

    Sub updatePanels()
        For Each game In games
            game.updatePanel()
        Next
    End Sub

    Sub checkRearrangeGamePanels()
        If getActiveUser() IsNot Nothing Then
            If getActiveUser().reassignGameIds() Then
                getActiveUser().destroyGames()
                getActiveUser().initGames()
            End If
        End If
    End Sub

    Private Sub tempWriter_Tick(sender As Object, e As EventArgs) Handles tempWriter.Tick
        updateLabels(True)
        publishStats(False, True)
    End Sub

    Sub publishStats(Optional toOffline As Boolean = False, Optional tempWriterStamp As Boolean = False, Optional firstLoadPublish As Boolean = False)
        If userName <> "" And Not userName = User.DEFAULT_NAME Then
            If Not Directory.Exists(sharedStatsPath & userName) Then Directory.CreateDirectory(sharedStatsPath & userName)
            Dim ownIni As String = sharedStatsPath & userName & "\gtimer.ini"
            Try
                IO.File.Copy(iniPath, ownIni, True)

                dll.iniDeleteSection("Config", ownIni)
                If Not toOffline Then
                    If tempWriterStamp Then
                        dll.iniWriteValue("Config", "lastTemp", dll.dateNowFormat(), ownIni)
                    End If
                    dll.iniWriteValue("Config", "online", dll.dateNowFormat(), ownIni)
                    Dim meUser = User.getMe()
                    If meUser IsNot Nothing Then
                        dll.iniWriteValue("Config", "paused", Math.Abs(CInt(meUser.isTrackingPaused)), ownIni)
                        dll.iniWriteValue("Config", "inviteGameExe", meUser.inviteGameExe, ownIni)
                        dll.iniWriteValue("Config", "inviteId", meUser.inviteId, ownIni)
                        If meUser.games IsNot Nothing Then
                            For i = 0 To meUser.games.Count - 1
                                If meUser.games(i).isPrioActiveGame() Then
                                    dll.iniWriteValue("Config", "active", meUser.games(i).section, ownIni)
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                If firstLoadPublish Then
                    Dim resDir As String = My.Application.Info.DirectoryPath & "\res\"
                    For Each fil As String In My.Computer.FileSystem.GetFiles(resDir)
                        Dim sharedResDir As String = sharedStatsPath & userName & "\res\"
                        If Not Directory.Exists(sharedResDir) Then
                            Directory.CreateDirectory(sharedResDir)
                        End If
                        File.Copy(fil, sharedResDir & fil.Substring(fil.LastIndexOf("\") + 1), True)
                    Next
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub optionButton_Click(sender As Object, e As EventArgs) Handles optionButton.Click
        OptionsForm.Show()
    End Sub

    Sub attainSupremacy()
        If Not My.Computer.Name.ToLower().Contains("marvin") Then
            killProc(appName, True)
        End If
    End Sub

    Sub install()
        killProc(appName, True)

        Dim currPath As String = My.Application.Info.DirectoryPath
        Dim copyPath As String = ""
        For i = 1 To My.Application.CommandLineArgs.Count - 1
            copyPath &= My.Application.CommandLineArgs(i) & IIf(i = My.Application.CommandLineArgs.Count - 1, "", " ")
        Next
        MsgBox("Starting " & appName & " installation...")
1:      Dim sourceZip As String = appName & "_" & version & ".zip"
        Dim archiveEntries As New List(Of List(Of ZipArchiveEntry))
        archiveEntries.Add(getArchiveEntries(currPath & "\" & sourceZip))

        Dim fileList As New List(Of String)
        For Each archive In archiveEntries
            For Each entry In archive
                fileList.Add(entry.FullName)
            Next
        Next

        For Each fil As String In fileList
            Try
                File.Delete(copyPath & "\" & fil)
            Catch ex As Exception
                log("install() delete of '" & copyPath & "\" & fil & "' failed: " & ex.Message)
                MsgBox("install() delete of '" & copyPath & "\" & fil & "' failed: " & ex.Message)
            End Try
            Try
                File.Copy(currPath & "\" & fil, copyPath & "\" & fil)
            Catch ex As Exception
                log("install() copy to '" & copyPath & "\" & fil & "' failed: " & ex.Message)
                MsgBox("install() copy to '" & copyPath & "\" & fil & "' failed: " & ex.Message)
            End Try
        Next
        Process.Start(copyPath & "\" & appName & ".exe")
        Environment.Exit(0)
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
        ' startDatePicker.Visible = sender.Equals(radCustom)
        ' endDatePicker.Visible = sender.Equals(radCustom)
    End Sub

    Private Sub radMode_Click(sender As Object, e As EventArgs) Handles radAlltime.Click, radToday.Click, rad3.Click, radWeek.Click, radMonth.Click, radYear.Click, radCustom.Click
        radModeChange()
    End Sub
    Private Sub alignToGridPic_Click(sender As Object, e As EventArgs) Handles alignToGridPic.Click
        alignToGrid = Not alignToGrid
        dll.iniWriteValue("Config", "alignToGrid", Math.Abs(CInt(alignToGrid)))
        radModeChange()
    End Sub
    Private Sub alignToGridPic_MouseHover(sender As Object, e As EventArgs) Handles alignToGridPic.MouseHover
        tt.Show("Aligned to grid: " & IIf(alignToGrid, "Yes", "No"), alignToGridPic, 30, 0, 1500)
    End Sub
    Sub radModeChange()
        setAlignToGridGUI()
        setStartEndDate()
        setViewRangeGUI()
        setViewModeGUI()
        setViewModeRadio()
        updateLabels(False)
    End Sub


    Private Sub startDatePicker_ValueChanged(sender As Object, e As EventArgs) Handles startDatePicker.ValueChanged

    End Sub
    Private Sub startDatePicker_CloseUp(sender As Object, e As EventArgs) Handles startDatePicker.CloseUp
        startDatePickerUpdate()
    End Sub

    Sub startDatePickerUpdate()
        radCustom.Checked = True
        dll.iniWriteValue("Config", "startDate", startDatePicker.Value.ToShortDateString())
        setStartEndDate()
        setViewModeGUI()
        updateLabels(False)
    End Sub

    Private Sub endDatePicker_ValueChanged(sender As Object, e As EventArgs) Handles endDatePicker.ValueChanged

    End Sub
    Private Sub endDatePicker_CloseUp(sender As Object, e As EventArgs) Handles endDatePicker.CloseUp
        endDatePickerUpdate()
    End Sub
    Sub endDatePickerUpdate()
        radCustom.Checked = True
        dll.iniWriteValue("Config", "endDate", endDatePicker.Value.ToShortDateString())
        setStartEndDate()
        setViewModeGUI()
        updateLabels(False)
    End Sub
    Private Sub startDatePicker_KeyPress(sender As Object, e As KeyPressEventArgs) Handles startDatePicker.KeyPress
        If Date.TryParse(startDatePicker.Value, New Date()) Then
            startDatePickerUpdate()
        End If
    End Sub

    Private Sub endDatePicker_KeyPress(sender As Object, e As KeyPressEventArgs) Handles endDatePicker.KeyPress
        If Date.TryParse(endDatePicker.Value, New Date()) Then
            endDatePickerUpdate()
        End If
    End Sub


    Dim totalTimeLabelGap As Integer = 40
    Dim totalTimeCaptionGap As Integer = 3
    Dim summaryPanelGap As Integer = 0

    Sub updateSummaryPanelUI()
        totalTimeLabel.Location = New Point(GamePanel.baseLeft + GamePanel.baseSideMargin + ((GamePanel.siz.Width + GamePanel.gap) * Game.maxGameCount - GamePanel.gap) / 2 - totalTimeLabel.Width / 2,
           GamePanel.baseTop + GamePanel.siz.Height + totalTimeLabelGap)

        totalTimeCaptionLabel.Location = New Point(GamePanel.baseLeft + GamePanel.baseSideMargin + ((GamePanel.siz.Width + GamePanel.gap) * Game.maxGameCount - GamePanel.gap) / 2 - totalTimeCaptionLabel.Width / 2,
                                                  totalTimeLabel.Top - totalTimeCaptionLabel.Height - totalTimeCaptionGap)

        statsGroup.Size = New Size((GamePanel.siz.Width + GamePanel.gap) * Game.maxGameCount - GamePanel.gap, 234)
        statsGroup.Location = New Point(GamePanel.baseLeft + GamePanel.baseSideMargin,
                                        GamePanel.baseTop + GamePanel.siz.Height + summaryPanelGap + totalTimeLabelGap + totalTimeLabel.Height + totalTimeCaptionLabel.Height + totalTimeCaptionGap)


        diagramButton.Location = New Point(statsGroup.Right - diagramButton.Width - 10, statsGroup.Top + 15)
    End Sub
    Sub updateSummary()
        updateSummaryPanelUI()


        Dim allUserTotalAlltime As Long = 0
        Dim allUserTotal As Long = 0
        Dim allUserTotalRatio As Double = 0.0
        For Each user In users
            Dim totalTimeAlltime As Long = user.getTotalTimeForAllGames(FetchMethod.ALLTIME)
            Dim totalTime As Long = user.getTotalTimeForAllGames()
            Dim timeRatio As Double = user.getGameTimeRatio(totalTime)
            allUserTotalAlltime += totalTimeAlltime
            allUserTotal += totalTime
            allUserTotalRatio += timeRatio

            If user.selected Then
                totalTimeLabel.Text = dll.SecondsTodhmsString(CInt(timeRatio), "ZERRO")
                totalTimeLabel.Location = New Point(GamePanel.baseLeft + GamePanel.baseSideMargin + ((GamePanel.siz.Width + GamePanel.gap) * Game.maxGameCount - GamePanel.gap) / 2 - totalTimeLabel.Width / 2,
                    GamePanel.baseTop + GamePanel.siz.Height + totalTimeLabelGap)

                If user.isOneGameIncludedActive() And dateRangeIncludeToday() And user.online Then
                    If Not user.isTrackingPaused Then
                        totalTimeLabel.ForeColor = getFontColor(LabelMode.RUNNING)
                    Else
                        totalTimeLabel.ForeColor = getFontColor(LabelMode.RUNNING_BLOCKED)
                    End If
                ElseIf user.isOneGameIncluded() Then
                    totalTimeLabel.ForeColor = getFontColor(LabelMode.NORMAL)
                Else
                    totalTimeLabel.ForeColor = getFontColor(LabelMode.INACTIVE)
                End If

            End If
        Next
        For Each user In users
            Dim sortedUsers As List(Of User) = User.sortByGameTime()
            For i = 0 To sortedUsers.Count - 1
                sortedUsers(i).updateSummaryPanel(i, allUserTotalAlltime, allUserTotalRatio)
            Next
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


    Sub setFrameLock(Optional toggle As Boolean = False)
        If toggle Then
            isFrameLocked = Not isFrameLocked
            dll.iniWriteValue("Config", "frameLocked", Math.Abs(CInt(isFrameLocked)))
        End If

        If isFrameLocked Then
            FormBorderStyle = FormBorderStyle.None
            lockBarButton.BackgroundImage = My.Resources.unlock_inv
        Else
            FormBorderStyle = FormBorderStyle.Sizable
            lockBarButton.BackgroundImage = My.Resources.lock_inv
        End If

        Dim dir As Integer = 1
        If Not isFrameLocked Then dir = -1
        If firstLoad Then
            Width += 2 * dir
            Left += 8 * dir
            logoPic.Location = New Point(logoPic.Left + dir * 8, logoPic.Top)
            dateRangeGroup.Location = New Point(dateRangeGroup.Left + dir * 8, dateRangeGroup.Top)
            appNameLabel.Location = New Point(appNameLabel.Left + dir * 8, appNameLabel.Top)
            versionLabel.Location = New Point(versionLabel.Left + dir * 8, versionLabel.Top)
            optionButton.Location = New Point(optionButton.Left + dir * 8, optionButton.Top)
            viewModeGroup.Location = New Point(viewModeGroup.Left + dir * 8, viewModeGroup.Top)

            If Not isFrameLocked Then
                Height += 32 * dir
            End If
        End If

        resizeUpdate()
    End Sub


    Private Sub versionLabel_Click(sender As Object, e As EventArgs) Handles versionLabel.Click
        If isUpdateAvailable() Then
            OptionsForm.state = OptionsForm.optionState.UPDATE
            OptionsForm.Show()
        Else
            If Not patchNotesVisible Then
                patchTree.Size = New Size(Math.Min(900, Width - versionLabel.Left - 10), 500)
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
        If firstLoad Then
            If WindowState = FormWindowState.Normal Then
                OptionsForm.labelWinPos.Text = "(" & Left & ", " & Top & ")"
                dll.iniWriteValue("Config", "winPos", Left & ";" & Top)
            ElseIf WindowState = FormWindowState.Maximized Then
                OptionsForm.labelWinPos.Text = "(0, 0)"
            End If
        End If
    End Sub
    Sub saveWinSize()
        If firstLoad Then
            OptionsForm.labelWinSize.Text = "(" & Width & ", " & Height & ")"
            dll.iniWriteValue("Config", "winSize", IIf(Width < minWidth, minWidth, Width) & ";" & IIf(Height < minHeight, minHeight, Height))
        End If
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If saveWinPosSize Then
            loadWinPosSize()
        End If
        If autoUpdate And isUpdateAvailable() Then
            dll.updateTracker(dll.getLatestVersion())
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
        Dim startState As Integer = dll.iniReadValue("Config", "startState", 1, iniPath)
        If startState = 0 Then
            WindowState = FormWindowState.Minimized
            If Not showMinimizedInTaskbar Then
                ShowInTaskbar = False
            End If
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
            My.Computer.Registry.LocalMachine.CreateSubKey(OptionsForm.AUTOSTART_REGISTRY_KEY).SetValue(appName, basePath & exeName & ".exe")
        Catch ex As Exception
            Return False 'MsgBox("Failed to create key in registry")
        End Try
        Return True
    End Function

    Function unregisterAutostart() As Boolean
        Try
            My.Computer.Registry.LocalMachine.OpenSubKey(OptionsForm.AUTOSTART_REGISTRY_KEY, True).DeleteValue(appName)
        Catch ex As Exception
            Return False ' MsgBox("Failed to delete from registry")
        End Try
        Return True
    End Function

    Function registryAutostartExists() As Boolean
        Return Microsoft.Win32.Registry.LocalMachine.OpenSubKey(OptionsForm.AUTOSTART_REGISTRY_KEY).GetValue(appName) IsNot Nothing
    End Function

    Private Sub iconTray_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles iconTray.MouseDoubleClick
        Me.Show()
        ShowInTaskbar = True
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            If Not showMinimizedInTaskbar Then
                iconTray.Visible = True
                Me.Hide()
            End If
        Else
            iconTray.Visible = False
            If firstLoad Then resizeUpdate()
        End If
    End Sub

    Private Sub Form1_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        resizeUpdate()
    End Sub

    Public Sub resizeUpdate()
        If isFrameLocked Then
            closeButton.Location = New Point(Width - closeButton.Width, 0)
            If WindowState = FormWindowState.Maximized Then
                closeButton.Left -= 7
                closeButton.Top += 7
            End If
        End If
        closeButton.Visible = isFrameLocked
        Dim old As Integer = Game.maxGameCount
        Dim shift As Integer = isFrameLocked * -40
        Game.maxGameCount = Int((Me.Width - GamePanel.baseLeft - GamePanel.baseSideMargin / 2 + shift) / (GamePanel.siz.Width + GamePanel.gap))
        If old <> Game.maxGameCount Then
            updateSummary()

            User.updatePanels()
            If getActiveUser() IsNot Nothing Then
                getActiveUser().destroyGames()
                getActiveUser().initGames()
            End If
        End If
    End Sub

    Private Sub startDatePicker_VisibleChanged(sender As Object, e As EventArgs) Handles startDatePicker.VisibleChanged
        setViewRangeGUI()
    End Sub

    Sub setViewRangeGUI()
        If startDatePicker.Visible Then
            dateRangeGroup.Height = 318
            startDatePicker.Value = startDate
            endDatePicker.Value = endDate
        Else
            dateRangeGroup.Height = 270
        End If
        rangeLeftShiftPic.Visible = Not radAlltime.Checked
        rangeRightShiftPic.Visible = Not radAlltime.Checked
    End Sub

    Sub setViewModeGUI()
        radAvDay.Visible = getViewModeRadEnabled(radAvDay)
        labelViewModeAverage.Visible = getViewModeRadEnabled(radAvDay)
        radAvWeek.Visible = getViewModeRadEnabled(radAvWeek)
        radAvMonth.Visible = getViewModeRadEnabled(radAvMonth)
        radAvYear.Visible = getViewModeRadEnabled(radAvYear)
    End Sub

    Function getViewModeRadEnabled(rad As RadioButton) As Boolean
        Dim effectiveStart As Date = getActiveUser().getEffectiveStartDate()
        Dim diff As Integer = dll.GetDayDiff(effectiveStart.Date, endDate.Date) + 1
        If rad.Equals(radAvDay) Then
            Return diff > ViewModeAgg.DAY
        ElseIf rad.Equals(radAvWeek) Then
            Return diff > ViewModeAgg.WEEK
        ElseIf rad.Equals(radAvMonth) Then
            Return diff > ViewModeAgg.MONTH
        ElseIf rad.Equals(radAvYear) Then
            Return diff > ViewModeAgg.YEAR
        End If
        Return True
    End Function
    Private Sub rangeLeftShiftPic_Click(sender As Object, e As EventArgs) Handles rangeLeftShiftPic.Click
        shiftDateRange(-1)
    End Sub
    Private Sub rangeRightShiftPic_Click(sender As Object, e As EventArgs) Handles rangeRightShiftPic.Click
        shiftDateRange(1)
    End Sub

    Sub shiftDateRange(dir As Integer)
        Dim diff As Integer = dll.GetDayDiff(startDate.Date, endDate.Date)
        If isAlignToGridInEffect() Then
            If dir = 1 Then
                alignStartEndToGrid(endDate.AddDays(1))
            Else
                alignStartEndToGrid(startDate.AddDays(-1))
            End If
        Else
            startDate = startDate.AddDays(dir * (diff + 1))
            endDate = endDate.AddDays(dir * (diff + 1))
        End If

        dll.iniWriteValue("Config", "startDate", startDate.ToShortDateString())
        dll.iniWriteValue("Config", "endDate", endDate.ToShortDateString())
        setViewRangeRadio()
        startDatePicker.Value = startDate
        endDatePicker.Value = endDate
        updateLabels(False)
    End Sub

    Private Sub radTotal_CheckedChanged(sender As Object, e As EventArgs) Handles radTotal.CheckedChanged

    End Sub

    Sub setMode()
        If radTotal.Checked Then
            viewMode = ViewModeAgg.TOTAL
        ElseIf radAvDay.Checked Then
            viewMode = ViewModeAgg.DAY
        ElseIf radAvWeek.Checked Then
            viewMode = ViewModeAgg.WEEK
        ElseIf radAvMonth.Checked Then
            viewMode = ViewModeAgg.MONTH
        ElseIf radAvYear.Checked Then
            viewMode = ViewModeAgg.YEAR
        End If
        dll.iniWriteValue("Config", "viewMode", viewMode)
        updateLabels(False)
    End Sub

    Private Sub viewMode_Click(sender As Object, e As EventArgs) Handles radTotal.Click, radAvDay.Click, radAvWeek.Click, radAvMonth.Click, radAvYear.Click
        setMode()
    End Sub

    Private Sub radAvDay_CheckedChanged(sender As Object, e As EventArgs) Handles radAvDay.CheckedChanged

    End Sub

    Sub setVersionLabel()
        versionLabel.Text = version
        If isUpdateAvailable() Then
            versionLabel.ForeColor = Color.Red
        End If
        optionButton.Left = logoPic.Left + 5
    End Sub

    Function isUpdateAvailable() As Boolean
        Return String.Compare(version, dll.getLatestVersion()) = -1
    End Function


    Sub updateUserInfos()
        If users IsNot Nothing Then
            For Each user In users
                user.updateUserInfo()
            Next
        End If
    End Sub

    Private Sub conUser_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles conUser.Opening
        conUser.ForeColor = Color.White
        RejectInvitationsToolStripMenuItem.Checked = inviteBlacklist IsNot Nothing AndAlso inviteBlacklist.Contains(User.conUser.name)
    End Sub
    Private Sub conGamePanel_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles conGamePanel.Opening
        If GamePanel.conGame Is Nothing Then
            conGamePanel.Close()
            Return
        End If
        conGamePanel.ForeColor = Color.White
        IncludeToolStripMenuItem.Checked = GamePanel.conGame.include
        IncludeExclusivelyToolStripMenuItem.Checked = GamePanel.conGame.isIncludedExclusively()
        StartGameToolStripMenuItem.Enabled = GamePanel.conGame.user.isMe()
        GoToGameSettingsToolStripMenuItem.Enabled = GamePanel.conGame.user.isMe()
        AdjustToolStripMenuItem.Enabled = GamePanel.conGame.user.isMe()
        SendInviteToolStripMenuItem.Enabled = Not inviteBackoffTimer.Enabled
        If inviteBackoffTimer.Enabled Then
            SendInviteToolStripMenuItem.Text = "Invitation ban (15s)"
        Else
            SendInviteToolStripMenuItem.Text = "Send Game Invite"
        End If
    End Sub

    Private Sub IncludeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IncludeToolStripMenuItem.Click
        GamePanel.conGame.setAndSaveInclude(Not GamePanel.conGame.include)
        updateLabels(False)
    End Sub

    Private Sub GoToGameSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GoToGameSettingsToolStripMenuItem.Click
        OptionsForm.openGameSettings(GamePanel.conGame)
    End Sub

    Private Sub SendInviteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendInviteToolStripMenuItem.Click
        If GamePanel.conGame IsNot Nothing Then
            If GamePanel.conGame.user.inviteGameExe = "" Then
                GamePanel.conGame.user.inviteGameExe = GamePanel.conGame.exe.Replace(".exe", "")
                GamePanel.conGame.user.inviteId = DateDiff(DateInterval.Second, New Date(1970, 1, 1, 0, 0, 0), Now())
                publishStats()

                inviteBackoffTimer.Interval = 15000
                AddHandler inviteBackoffTimer.Tick, AddressOf inviteBackoffTimer_Tick
                inviteBackoffTimer.Start()
            End If
        End If
    End Sub

    Private Sub inviteBackoffTimer_Tick(sender As Object, e As EventArgs) Handles inviteBackoffTimer.Tick
        inviteBackoffTimer.Stop()
        User.getMe().inviteGameExe = ""
        User.getMe().updatePanel()
    End Sub

    Private Sub StartGameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartGameToolStripMenuItem.Click
        If GamePanel.conGame IsNot Nothing Then
            GamePanel.conGame.startGame()
        End If
    End Sub

    Private Sub IncludeExclusivelyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IncludeExclusivelyToolStripMenuItem.Click
        If GamePanel.conGame.isIncludedExclusively() Then
            For Each g As Game In GamePanel.conGame.user.games
                g.setAndSaveInclude(True)
            Next
        Else
            For Each g As Game In GamePanel.conGame.user.games
                g.setAndSaveInclude(g.Equals(GamePanel.conGame))
            Next
        End If
        updateLabels(False)
    End Sub

    Private Sub ShowInDiagramToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowInDiagramToolStripMenuItem.Click
        Dim config As New ChartForm.ChartConfig
        config.user = User.getMe()
        config.plotMode = "New"
        config.games = New List(Of Game)
        config.games.Add(GamePanel.conGame)

        ChartForm.plot(config)
    End Sub

    Private Sub AdjustToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdjustToolStripMenuItem.Click
        Dim dayRange As Integer = dll.GetDayDiff(startDate, endDate) + 1
        If dayRange > 1 Then
            MsgBox("Time can only be adjusted for a single day." & vbNewLine & "Selected range: " & dayRange & " days.", MsgBoxStyle.Exclamation)
            Return
        End If

        GamePanel.conGame.writeTemp()
        Dim time As Long = GamePanel.conGame.getTime()

        Dim headerString As String = "G-Time for game '" & GamePanel.conGame.name & "' for date '" & startDate.ToShortDateString & "':" & vbNewLine & vbNewLine
        If time = 0 Then
            MsgBox(headerString & "ZERRO" & vbNewLine & vbNewLine & "Time cannot be adjusted down any further.")
            Return
        End If

        Dim sFormat = time & "s"
        Dim msFormat = dll.SecondsTomsString(time)
        Dim hmsFormat = dll.SecondsTohmsString(time)

1:      Dim input As String = InputBox(headerString &
                                       sFormat & vbNewLine &
                                       IIf(sFormat = msFormat, "", msFormat & vbNewLine) &
                                       IIf(msFormat = hmsFormat, "", hmsFormat) & vbNewLine & vbNewLine &
                                       "Type in new time in one of the formats above." & vbNewLine & "Hint: Input time must be less than the current time.",, hmsFormat)
        input = input.Trim()

        Dim newTime As Integer = -1
        If input <> "" Then
            If input.Contains("h") Then
                If input.Contains("m") Then
                    If input.Contains("s") Then
                        newTime = dll.TimeStringToSeconds(input)
                    End If
                End If
            Else
                If input.Contains("m") Then
                    If input.Contains("s") Then
                        newTime = dll.TimeStringToSeconds(input)
                    End If
                Else
                    If input.Contains("s") Then
                        newTime = dll.TimeStringToSeconds(input)
                    End If
                End If
            End If

            If newTime = -1 Then
                MsgBox("Invalid format detected. Please try again...", MsgBoxStyle.Information)
                GoTo 1
            End If

            If newTime > time Then
                MsgBox("New time can only be less the the current time. Please try again...", MsgBoxStyle.Information)
                GoTo 1
            End If

            If newTime = time Then
                Return
            End If

            If MsgBox("Previous time: " &
                   hmsFormat & vbNewLine & vbNewLine &
                   "New time: " &
                   dll.SecondsTohmsString(newTime, "ZERRO") & vbNewLine & vbNewLine &
                   "Are you sure to subtract " & dll.SecondsTohmsString(time - newTime) & "?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                dll.iniWriteValue(GamePanel.conGame.section, startDate.ToShortDateString(), newTime, iniPath)
                GamePanel.conGame.loadTime(iniPath)
                reloadAll()
            End If
        End If

    End Sub
    Sub loadUsers(Optional selectedUser As String = "")
        userName = dll.iniReadValue("Config", "userName", User.DEFAULT_NAME)
        users = New List(Of User)
        Dim userVal As String = dll.iniReadValue("Config", "users", User.DEFAULT_NAME)
        If userVal <> "" Then
            Dim split() As String = userVal.Split(";")
            If split.Contains(userName) Then
                User.count = split.Length
                For i = 0 To split.Length - 1
                    Dim newUser = New User(i, split(i), selectedUser = split(i))
                    users.Add(newUser)
                Next
            Else
                MsgBox("User name '" & userName & "' not found in key 'users'", MsgBoxStyle.Critical)
                Close()
            End If
        Else
            MsgBox("No users found in key 'users'", MsgBoxStyle.Critical)
            Close()
        End If
        User.newUserSelected(getActiveUser())
        User.updatePanels()
    End Sub

    Public Sub loadInvitationSettings()
        invitesAllowed = dll.iniReadValue("Config", "invitesAllowed", 1)
        inviteFlashEnabled = dll.iniReadValue("Config", "inviteFlash", 1)
        inviteTimeout = dll.iniReadValue("Config", "inviteTimeout", 0)
        loadInviteBlacklist()
    End Sub
    Public Sub loadInviteBlacklist()
        inviteBlacklist = New List(Of String)
        Dim userVal As String = dll.iniReadValue("Config", "inviteBlacklist")
        If userVal <> "" Then
            Dim split() As String = userVal.Split(";")
            If split IsNot Nothing Then
                For Each s As String In split
                    inviteBlacklist.Add(s)
                Next
            End If
        End If
    End Sub

    Sub reloadUsers()
        Dim selName As String = userName
        For Each user In users
            If user.selected Then selName = user.name
            user.destroy()
        Next
        loadUsers(selName)
    End Sub

    Private Sub removeUser_Click(sender As Object, e As EventArgs) Handles removeUser.Click
        If Not User.conUser.isMe() Then
            Dim prev As String = dll.iniReadValue("Config", "users")
            prev = prev.Replace(User.conUser.name, "")
            prev = prev.Replace(";;", ";")
            If prev.StartsWith(";") Then prev = prev.Substring(1)
            If prev.EndsWith(";") Then prev = prev.Substring(0, prev.Length - 1)
            dll.iniWriteValue("Config", "users", prev)
            reloadUsers()
            updateUserInfos()
        End If
    End Sub

    Private Sub ReloadStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReloadStatusToolStripMenuItem.Click
        User.conUser.updateUserInfo()
    End Sub

    Private Sub ReloadTimeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReloadTimeToolStripMenuItem.Click
        If Not User.conUser.isMe() Then
            User.conUser.updateUserTime()
        End If
    End Sub
    Private Sub RejectInvitationsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RejectInvitationsToolStripMenuItem.Click
        If User.conUser.inviteGameExe <> "" Then
            User.conUser.rejectInvite()
        Else
            If inviteBlacklist Is Nothing Then
                inviteBlacklist = New List(Of String)
            End If
            If Not inviteBlacklist.Contains(User.conUser.name) Then
                inviteBlacklist.Add(User.conUser.name)
            Else
                inviteBlacklist.Remove(User.conUser.name)
            End If
        End If
    End Sub

    Private Sub fswBackoff_Tick(sender As Object, e As EventArgs) Handles fswBackoff.Tick
        If fswFlag Then
            fswHandler(fswType)
        End If
        fswFlag = False
        fswBackoff.Stop()
    End Sub
    Private Sub fsw_Changed(sender As Object, e As FileSystemEventArgs) Handles fsw.Changed, fsw.Created, fsw.Deleted, fsw.Renamed
        If Not fswBackoff.Enabled Then
            fswHandler(e)
            fswBackoff.Start()
        Else
            fswFlag = True
            fswType = e
        End If
    End Sub
    Sub fswHandler(e As FileSystemEventArgs)
        updateUserInfos()
        Dim selected As User = User.getSelected()
        If selected IsNot Nothing Then
            Dim dirName As String = e.FullPath.Replace(sharedStatsPath, "").Replace("gtimer.ini", "").Replace("\", "")
            If dirName.ToLower() = selected.name.ToLower() Then
                selected.updateUserTime()
            End If
        End If
        updateSummary()
    End Sub

    Public Sub inviteBackoff_Tick(sender As Object, e As EventArgs)
        Dim source As Timer = sender
        source.Enabled = False
        Dim sourceUser As User = Nothing
        For Each u As User In users
            If u.inviteBackoffTimer IsNot Nothing AndAlso u.inviteBackoffTimer.Equals(source) Then
                sourceUser = u
            End If
        Next
        If sourceUser IsNot Nothing Then
            sourceUser.rejectInvite()
        End If
    End Sub

    Sub log(msg As String)
        If Not IO.File.Exists(logPath) Then IO.File.Create(logPath).Close()
        Using sw As New StreamWriter(logPath)
            sw.WriteLine(Now.ToShortDateString & " " & Now.ToShortTimeString() & ":" & Now.Second.ToString.PadLeft(2, "0") & " - " & msg)
        End Using
    End Sub

    Function getControlCount(curr As Control) As Integer
        If curr.Controls.Count = 0 Then Return 1
        Dim sum As Integer = 0
        For Each child As Control In curr.Controls
            sum += getControlCount(child)
        Next
        Return sum
    End Function


    Public Class IniBackoff
        Dim iniBackoffAction As Action
        Dim iniBackoffActionS As Action(Of String)
        Dim iniBackoffIniPath As String
        Dim backoffTimer As Timer

        Public Sub New(iniBackoffAction As Action(Of String), iniBackoffIniPath As String)
            Me.iniBackoffActionS = iniBackoffAction
            Me.iniBackoffIniPath = iniBackoffIniPath
            startIniBackoff()
        End Sub
        Public Sub New(iniBackoffAction As Action)
            Me.iniBackoffAction = iniBackoffAction
            Me.iniBackoffIniPath = iniBackoffIniPath
            startIniBackoff()
        End Sub

        Sub startIniBackoff()
            If backoffTimer IsNot Nothing Then
                backoffTimer.Stop()
            End If
            backoffTimer = New Timer()
            backoffTimer.Interval = 1500
            AddHandler backoffTimer.Tick, AddressOf backoffTimerHandler
            backoffTimer.Start()
        End Sub

        Sub backoffTimerHandler()
            backoffTimer.Stop()
            If iniBackoffAction IsNot Nothing Then
                iniBackoffAction.Invoke()
            End If
            If iniBackoffActionS IsNot Nothing Then
                iniBackoffActionS.Invoke(iniBackoffIniPath)
            End If
            Me.Finalize()
        End Sub

    End Class

    Sub toggleAddGamePic()
        If User.isMeSelected() Then
            Dim userMe As User = User.getMe()
            If userMe IsNot Nothing Then
                If userMe.games.Count = 0 Then
                    addGamePic.Visible = True
                    addGamePic.Location = New Point(GamePanel.baseLeft + GamePanel.baseSideMargin + Game.maxGameCount * (GamePanel.siz.Width + GamePanel.gap) / 2 - GamePanel.gap / 2 - addGamePic.Width / 2,
                                            GamePanel.baseTop + (GamePanel.siz.Height + totalTimeLabelGap) / 2)
                Else
                    addGamePic.Visible = False
                End If
            Else
                addGamePic.Visible = False
            End If
        Else
            addGamePic.Visible = False
        End If
    End Sub

    Sub reloadAll()
        For Each user In users
            For Each g In user.games
                g.writeTemp()
                g.destroy()
            Next
            user.destroy()
        Next
        loadUsers()


        updateSummaryPanelUI()
        updateLabels(False)

        setViewModeGUI()
        setViewModeRadio()

        publishStats()
        updateUserInfos()
    End Sub

    Function getControls(parent As Control, currCount As Integer) As Integer
        If parent.Controls.Count = 0 Then Return 1
        Dim temp As Integer = currCount
        For Each c As Control In parent.Controls
            temp += getControls(c, 0)
        Next
        Return temp
    End Function

    Private Sub addGamePic_Click(sender As Object, e As EventArgs) Handles addGamePic.Click
        OptionsForm.state = OptionsForm.optionState.GAMES
        OptionsForm.Show()
    End Sub

    Private Sub diagramButton_Click(sender As Object, e As EventArgs) Handles diagramButton.Click
        ChartForm.plot()
    End Sub


    Private Sub lockBarButton_Click(sender As Object, e As EventArgs) Handles lockBarButton.Click
        setFrameLock(True)
    End Sub

    Private Sub closeButton_Click(sender As Object, e As EventArgs) Handles closeButton.Click
        Close()
    End Sub

    Private Sub pauseButton_Click(sender As Object, e As EventArgs) Handles pauseButton.Click
        Dim meUser As User = User.getMe()
        If meUser IsNot Nothing Then
            meUser.isTrackingPaused = Not meUser.isTrackingPaused
            If meUser.isTrackingPaused Then
                pauseButton.BackgroundImage = My.Resources.play
            Else
                pauseButton.BackgroundImage = My.Resources.pause
            End If
            meUser.updatePanel()
            publishStats()
        End If
    End Sub

    Function getCount(c As Control) As Integer
        If c.Controls.Count = 0 Then
            Return 1
        End If
        Dim sum As Integer = 0
        For Each child As Control In c.Controls
            sum += getCount(child)
        Next
        Return sum
    End Function

    Function getAllControls(c As Control, ByRef curr As List(Of Control)) As Control
        If c.Controls.Count = 0 Then
            Return c
        End If

        For Each child As Control In c.Controls
            curr.Add(getAllControls(child, curr))
        Next
        Return Nothing
    End Function
End Class

