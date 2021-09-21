Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.InteropServices

Public Class Form1

    '<DllImport("Dwmapi.dll")>
    'Shared Function DwmIsCompositionEnabled(ByRef enabled As Boolean) As Integer
    'End Function

    '<StructLayout(LayoutKind.Sequential)>
    'Public Structure MARGINS
    '    Public cxLeftWidth, cxRightWidth, cyTopHeight, cyBottomHeight As Integer
    '    Public Sub New(ByVal left As Integer, ByVal right As Integer, ByVal top As Integer, ByVal bottom As Integer)
    '        cxLeftWidth = left
    '        cyTopHeight = top
    '        cxRightWidth = right
    '        cyBottomHeight = bottom
    '    End Sub
    'End Structure

    '<DllImport("Dwmapi.dll")>
    'Shared Function DwmExtendFrameIntoClientArea(ByVal hwnd As IntPtr, ByRef margins As MARGINS) As Integer
    'End Function

    '<DllImport("Dwmapi.dll")>
    'Shared Function DwmDefWindowProc(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr, ByRef plResult As IntPtr) As Boolean
    'End Function

    'Const FRAME_MARGIN_TOP = 30
    'Const FRAME_MARGIN_SIDES = 0
    'Protected Overrides Sub WndProc(ByRef m As Message)
    '    MyBase.WndProc(m)
    '    Return
    '    If DwmDefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam, m.Result) Then
    '        Return
    '    End If

    '    If m.Msg = &H6 Then
    '        Dim margins As MARGINS

    '        margins.cxLeftWidth = FRAME_MARGIN_SIDES
    '        margins.cxRightWidth = FRAME_MARGIN_SIDES
    '        margins.cyBottomHeight = FRAME_MARGIN_SIDES
    '        margins.cyTopHeight = FRAME_MARGIN_TOP

    '        Dim hr = DwmExtendFrameIntoClientArea(m.HWnd, margins)
    '        hr = hr
    '    ElseIf m.Msg = &H83 Then
    '        If m.WParam = 0 Then
    '            MyBase.WndProc(m)
    '        End If
    '    ElseIf m.Msg = &H84 Then
    '        m.LParam.ToInt32()
    '        Dim p As Point = PointToClient(New Point(m.LParam.ToInt32()))
    '        Dim hitResult As Integer = getHitResult(p)

    '        If hitResult > 0 Then
    '            m.Result = New IntPtr(hitResult)
    '        End If

    '    ElseIf m.Msg = &H1 Then
    '        Form1_Resize(Me, Nothing)
    '    ElseIf m.Msg = &H86 Then
    '        Form1_Resize(Me, Nothing)
    '        MyBase.WndProc(m)
    '    ElseIf m.Msg = &H8 Then
    '        m = m
    '    ElseIf m.Msg = &H46 Then 'winposchanging
    '        m = m
    '    Else
    '        MyBase.WndProc(m)
    '    End If
    'End Sub

    'Function getHitResult(p As Point) As Integer
    '    If isPointInMargin(p.X, Me.Width) And isPointInMargin(p.Y, Height) Then
    '        Return 17
    '    ElseIf isPointInMargin(p.X, 0) And isPointInMargin(p.Y, Height) Then
    '        Return 16
    '    ElseIf isPointInMargin(p.Y, Height) Then
    '        Return 15
    '    ElseIf isPointInMargin(p.X, Width) And isPointInMargin(p.Y, 0) Then
    '        Return 14
    '    ElseIf isPointInMargin(p.X, 0) And isPointInMargin(p.Y, 0) Then
    '        Return 13
    '    ElseIf isPointInMargin(p.Y, 0) Then
    '        Return 12
    '    ElseIf isPointInMargin(p.X, Width) Then
    '        Return 11
    '    ElseIf isPointInMargin(p.X, 0) Then
    '        Return 10
    '    End If
    '    If isPointInMargin(p.Y, 30, 30) Then
    '        Return 2
    '    End If
    '    Return 0
    'End Function

    'Function isPointInMargin(p As Integer, bounds As Integer, Optional margin As Integer = 3) As Boolean
    '    If bounds = 0 Then
    '        Return p >= bounds And p - margin <= bounds
    '    Else
    '        Return p <= bounds And p + margin >= bounds
    '    End If
    'End Function


    Public dll As New Utils

    Public basePath As String = AppDomain.CurrentDomain.BaseDirectory
    Public iniPath As String = basePath & "gtimer.ini"
    Public resPath As String = basePath & "res\"
    Public sharedPath As String
    Public logPath As String = basePath & "log"

    Public exeName = IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath)
    Public Const appName = "GTimer"
    Public Const version = "v3.1"
    Public Const minWidth As Integer = 950
    Public Const minHeight As Integer = 750

    Public saveWinPosSize As Boolean
    Public autostartEnabled As Boolean
    Public showMinimizedInTaskbar As Boolean
    Public autoUpdate As Boolean
    Public isFrameLocked As Boolean


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

            dateConfigToDate(dll.iniReadValue("Config", "startDate"), startDatePicker.Value)
            dateConfigToDate(dll.iniReadValue("Config", "endDate"), endDatePicker.Value)
            startDate = startDatePicker.Value
            endDate = endDatePicker.Value
        Else
            endDate = Now.Date
            dll.iniWriteValue("Config", "endDate", 0)
            If radAlltime.Checked Then
                setStartDateHelper(radAlltime, -1000)
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

    Private Sub setStartDateHelper(rad As RadioButton, days As Integer)
        startDate = Now.Date.AddDays(days)
        dll.iniWriteValue("Config", "startDate", days)
        Dim effectiveStartDate As Date = getActiveUser().getEffectiveStartDate()
        If dll.GetDayDiff(effectiveStartDate, startDate) < 0 Then
            tt.Show("Effective Date Range:" & vbNewLine & effectiveStartDate.ToShortDateString() & " - " & endDate.ToShortDateString(), rad, 75, 0, 1500)
        End If
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
        startDatePicker.Visible = sender.Equals(radCustom)
        endDatePicker.Visible = sender.Equals(radCustom)
    End Sub

    Private Sub radMode_Click(sender As Object, e As EventArgs) Handles radAlltime.Click, radToday.Click, rad3.Click, radWeek.Click, radMonth.Click, radYear.Click, radCustom.Click
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
            lockBarButton.BackgroundImage = My.Resources.lock_inv
        Else
            FormBorderStyle = FormBorderStyle.Sizable
            lockBarButton.BackgroundImage = My.Resources.unlock_inv
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
        startDate = startDate.AddDays(dir * (diff + 1))
        endDate = endDate.AddDays(dir * (diff + 1))
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
        ChartForm.Show()
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
                pauseButton.BackgroundImage = My.Resources.pause
            Else
                pauseButton.BackgroundImage = My.Resources.play
            End If
            meUser.updatePanel()
            publishStats()
        End If
    End Sub
End Class

