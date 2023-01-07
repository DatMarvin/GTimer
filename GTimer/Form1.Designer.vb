<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Stable Release")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v1.0 - 03.01.21", New System.Windows.Forms.TreeNode() {TreeNode1})
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Summary statistics are updated every second")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fixed 'Today' Mode")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fixed a bug where time was logged twice")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v1.1 - 03.01.21", New System.Windows.Forms.TreeNode() {TreeNode3, TreeNode4, TreeNode5})
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Time labels format changed")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Game panel titles removed")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Total time label font color adjusted")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Import Configuration option")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Game panel colorless logos added")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Resource file structure changed")
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Selected view mode is now saved after closing")
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v1.2 - 04.01.21", New System.Windows.Forms.TreeNode() {TreeNode7, TreeNode8, TreeNode9, TreeNode10, TreeNode11, TreeNode12, TreeNode13})
        Dim TreeNode15 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Clicking logos excludes game from summary statistics")
        Dim TreeNode16 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fixed a bug where too much time was logged")
        Dim TreeNode17 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Periodic time logging interval decreased to one minute")
        Dim TreeNode18 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v1.2.1 - 04.01.21", New System.Windows.Forms.TreeNode() {TreeNode15, TreeNode16, TreeNode17})
        Dim TreeNode19 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Patch Notes published")
        Dim TreeNode20 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Time logs are saved when closing GTimer")
        Dim TreeNode21 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Only one active game is counted at a time")
        Dim TreeNode22 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Window position and size can be saved")
        Dim TreeNode23 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Window start state can be assigned")
        Dim TreeNode24 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Label font family can be changed globally")
        Dim TreeNode25 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Date range can be now be shifted")
        Dim TreeNode26 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v1.3 - 11.01.21", New System.Windows.Forms.TreeNode() {TreeNode19, TreeNode20, TreeNode21, TreeNode22, TreeNode23, TreeNode24, TreeNode25})
        Dim TreeNode27 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Version 1.x", New System.Windows.Forms.TreeNode() {TreeNode2, TreeNode6, TreeNode14, TreeNode18, TreeNode26})
        Dim TreeNode28 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("New GTimer versions can now be installed in the Options")
        Dim TreeNode29 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Game times can now be averaged over a day, week, month and year")
        Dim TreeNode30 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Friend System introduced")
        Dim TreeNode31 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Stats from friends can be displayed")
        Dim TreeNode32 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v2.0 - 13.01.2021", New System.Windows.Forms.TreeNode() {TreeNode28, TreeNode29, TreeNode30, TreeNode31})
        Dim TreeNode33 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Displaying the window in the taskbar can be toggled when minimizing the window")
        Dim TreeNode34 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Only one instance of GTimer is allowed at a time")
        Dim TreeNode35 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Selecting a date range option indicates if it chooses another start date")
        Dim TreeNode36 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Automatic updates can now be enabled in the options")
        Dim TreeNode37 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v2.0.1 - 13.01.2021", New System.Windows.Forms.TreeNode() {TreeNode33, TreeNode34, TreeNode35, TreeNode36})
        Dim TreeNode38 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fixed the auto update feature")
        Dim TreeNode39 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Added a button to save option changes")
        Dim TreeNode40 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v2.0.2 - 14.01.2021", New System.Windows.Forms.TreeNode() {TreeNode38, TreeNode39})
        Dim TreeNode41 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("The application icon has been set to the GTimer logo")
        Dim TreeNode42 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v2.0.3 - 15.01.2021", New System.Windows.Forms.TreeNode() {TreeNode41})
        Dim TreeNode43 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Time ratios for games moved to game panels")
        Dim TreeNode44 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Ranking system among friends released")
        Dim TreeNode45 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fixed a bug where a friend appeared to be playing a game after closing it")
        Dim TreeNode46 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Added shared stats file access management")
        Dim TreeNode47 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Game time now ticks continuously for friends")
        Dim TreeNode48 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v2.1 - 25.01.2021", New System.Windows.Forms.TreeNode() {TreeNode43, TreeNode44, TreeNode45, TreeNode46, TreeNode47})
        Dim TreeNode49 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fixed a bug where the ranking average time was too high")
        Dim TreeNode50 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Game time ratio bars were redesigned. Percentages can be seen by mouse hovering")
        Dim TreeNode51 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Game panels are now sorted downwards by game time")
        Dim TreeNode52 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Name plates are now placed outside the ranking bars")
        Dim TreeNode53 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v2.2 - 26.01.2021", New System.Windows.Forms.TreeNode() {TreeNode49, TreeNode50, TreeNode51, TreeNode52})
        Dim TreeNode54 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fixed a bug where offline user times kept ticking")
        Dim TreeNode55 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Ranking name labels are displayed correctly")
        Dim TreeNode56 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v2.2.1 - 11.02.2021", New System.Windows.Forms.TreeNode() {TreeNode54, TreeNode55})
        Dim TreeNode57 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Added game management feature")
        Dim TreeNode58 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("The last game panel summarizes all least popular games")
        Dim TreeNode59 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("All data can be reloaded from the options window")
        Dim TreeNode60 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("A custom date range can now be entered manually")
        Dim TreeNode61 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v2.3 - 08.03.2021", New System.Windows.Forms.TreeNode() {TreeNode57, TreeNode58, TreeNode59, TreeNode60})
        Dim TreeNode62 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Version 2.x", New System.Windows.Forms.TreeNode() {TreeNode32, TreeNode37, TreeNode40, TreeNode42, TreeNode48, TreeNode53, TreeNode56, TreeNode61})
        Dim TreeNode63 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("GTimer Chart introduced")
        Dim TreeNode64 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Game sorting options added")
        Dim TreeNode65 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("When friends open your profile in their GTimer, they now see your game logos")
        Dim TreeNode66 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Resizing the window dynamically rearranges the game panels")
        Dim TreeNode67 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v3.0 - 29.08.2021", New System.Windows.Forms.TreeNode() {TreeNode63, TreeNode64, TreeNode65, TreeNode66})
        Dim TreeNode68 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Negative window starting coordinates are now allowed")
        Dim TreeNode69 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("The window frame can now be locked to hide the title bar")
        Dim TreeNode70 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("The tracking of game time can now be paused by a toggle button")
        Dim TreeNode71 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v3.1 - 21.09.2021", New System.Windows.Forms.TreeNode() {TreeNode68, TreeNode69, TreeNode70})
        Dim TreeNode72 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("The toggle behaviour of the window lock and pause icons were inverted")
        Dim TreeNode73 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Game panels have context menus that can be opened by right-click")
        Dim TreeNode74 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Games can be started in context menu and by left-clicking panel")
        Dim TreeNode75 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("The user can scroll through the list of games with the mouse wheel")
        Dim TreeNode76 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("The recorded G-Time can be adjusted down in the context menu")
        Dim TreeNode77 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Stats Chart: Game selection added")
        Dim TreeNode78 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Date Ranges (Week, Month, Year) can be aligned to their respective grid")
        Dim TreeNode79 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v3.2 - 06.01.2022", New System.Windows.Forms.TreeNode() {TreeNode72, TreeNode73, TreeNode74, TreeNode75, TreeNode76, TreeNode77, TreeNode78})
        Dim TreeNode80 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fixed the date range mode 'Week' with grid on")
        Dim TreeNode81 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Friends can be invited to join games")
        Dim TreeNode82 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v3.3 - 23.03.2022", New System.Windows.Forms.TreeNode() {TreeNode80, TreeNode81})
        Dim TreeNode83 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("New sorting method: Last Played")
        Dim TreeNode84 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Hovering over game displays last time played")
        Dim TreeNode85 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fixed the autostart function. GTimer now must be lauchned as administrator")
        Dim TreeNode86 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("v3.4 - 07.01.2023", New System.Windows.Forms.TreeNode() {TreeNode83, TreeNode84, TreeNode85})
        Dim TreeNode87 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Version 3.x", New System.Windows.Forms.TreeNode() {TreeNode67, TreeNode71, TreeNode79, TreeNode82, TreeNode86})
        Dim TreeNode88 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Patch Notes", New System.Windows.Forms.TreeNode() {TreeNode27, TreeNode62, TreeNode87})
        Me.tracker = New System.Windows.Forms.Timer(Me.components)
        Me.tempWriter = New System.Windows.Forms.Timer(Me.components)
        Me.optionButton = New System.Windows.Forms.Button()
        Me.versionLabel = New System.Windows.Forms.Label()
        Me.radAlltime = New System.Windows.Forms.RadioButton()
        Me.radToday = New System.Windows.Forms.RadioButton()
        Me.rad3 = New System.Windows.Forms.RadioButton()
        Me.radWeek = New System.Windows.Forms.RadioButton()
        Me.radMonth = New System.Windows.Forms.RadioButton()
        Me.radYear = New System.Windows.Forms.RadioButton()
        Me.radCustom = New System.Windows.Forms.RadioButton()
        Me.startDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.endDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.dateRangeGroup = New System.Windows.Forms.GroupBox()
        Me.alignToGridLinePic = New System.Windows.Forms.PictureBox()
        Me.rangeRightShiftPic = New System.Windows.Forms.PictureBox()
        Me.alignToGridPic = New System.Windows.Forms.PictureBox()
        Me.rangeLeftShiftPic = New System.Windows.Forms.PictureBox()
        Me.logoPic = New System.Windows.Forms.PictureBox()
        Me.statsGroup = New System.Windows.Forms.GroupBox()
        Me.diagramButton = New System.Windows.Forms.Button()
        Me.totalTimeLabel = New System.Windows.Forms.Label()
        Me.totalTimeCaptionLabel = New System.Windows.Forms.Label()
        Me.patchTree = New System.Windows.Forms.TreeView()
        Me.patchNotesClosePic = New System.Windows.Forms.PictureBox()
        Me.iconTray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.viewModeGroup = New System.Windows.Forms.GroupBox()
        Me.labelViewModeAverage = New System.Windows.Forms.Label()
        Me.radAvYear = New System.Windows.Forms.RadioButton()
        Me.radAvMonth = New System.Windows.Forms.RadioButton()
        Me.radAvWeek = New System.Windows.Forms.RadioButton()
        Me.radAvDay = New System.Windows.Forms.RadioButton()
        Me.radTotal = New System.Windows.Forms.RadioButton()
        Me.appNameLabel = New System.Windows.Forms.Label()
        Me.removeUser = New System.Windows.Forms.ToolStripMenuItem()
        Me.conUser = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ReloadStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReloadTimeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RejectInvitationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.fsw = New System.IO.FileSystemWatcher()
        Me.tt = New System.Windows.Forms.ToolTip(Me.components)
        Me.addGamePic = New System.Windows.Forms.PictureBox()
        Me.lockBarButton = New System.Windows.Forms.Button()
        Me.pauseButton = New System.Windows.Forms.Button()
        Me.closeButton = New System.Windows.Forms.Button()
        Me.fswBackoff = New System.Windows.Forms.Timer(Me.components)
        Me.conGamePanel = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.StartGameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GoToGameSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendInviteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IncludeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IncludeExclusivelyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowInDiagramToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdjustToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.inviteBackoffTimer = New System.Windows.Forms.Timer(Me.components)
        Me.dateRangeGroup.SuspendLayout()
        CType(Me.alignToGridLinePic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rangeRightShiftPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.alignToGridPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rangeLeftShiftPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.logoPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.patchNotesClosePic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.viewModeGroup.SuspendLayout()
        Me.conUser.SuspendLayout()
        CType(Me.fsw, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.addGamePic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.conGamePanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'tracker
        '
        Me.tracker.Interval = 1000
        '
        'tempWriter
        '
        Me.tempWriter.Interval = 60000
        '
        'optionButton
        '
        Me.optionButton.BackgroundImage = CType(resources.GetObject("optionButton.BackgroundImage"), System.Drawing.Image)
        Me.optionButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.optionButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.optionButton.FlatAppearance.BorderSize = 0
        Me.optionButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray
        Me.optionButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray
        Me.optionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.optionButton.Location = New System.Drawing.Point(12, 155)
        Me.optionButton.Name = "optionButton"
        Me.optionButton.Size = New System.Drawing.Size(37, 37)
        Me.optionButton.TabIndex = 0
        Me.optionButton.TabStop = False
        Me.tt.SetToolTip(Me.optionButton, "Open Options Menu")
        Me.optionButton.UseVisualStyleBackColor = True
        '
        'versionLabel
        '
        Me.versionLabel.AutoSize = True
        Me.versionLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.versionLabel.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.versionLabel.ForeColor = System.Drawing.Color.White
        Me.versionLabel.Location = New System.Drawing.Point(97, 127)
        Me.versionLabel.Name = "versionLabel"
        Me.versionLabel.Size = New System.Drawing.Size(56, 23)
        Me.versionLabel.TabIndex = 2
        Me.versionLabel.Text = "v1.2.1"
        Me.tt.SetToolTip(Me.versionLabel, "Open patch notes")
        '
        'radAlltime
        '
        Me.radAlltime.AutoSize = True
        Me.radAlltime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radAlltime.Location = New System.Drawing.Point(15, 61)
        Me.radAlltime.Name = "radAlltime"
        Me.radAlltime.Size = New System.Drawing.Size(77, 22)
        Me.radAlltime.TabIndex = 4
        Me.radAlltime.Text = "Alltime"
        Me.radAlltime.UseVisualStyleBackColor = True
        '
        'radToday
        '
        Me.radToday.AutoSize = True
        Me.radToday.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radToday.Location = New System.Drawing.Point(15, 89)
        Me.radToday.Name = "radToday"
        Me.radToday.Size = New System.Drawing.Size(71, 22)
        Me.radToday.TabIndex = 5
        Me.radToday.Text = "Today"
        Me.radToday.UseVisualStyleBackColor = True
        '
        'rad3
        '
        Me.rad3.AutoSize = True
        Me.rad3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rad3.Location = New System.Drawing.Point(15, 117)
        Me.rad3.Name = "rad3"
        Me.rad3.Size = New System.Drawing.Size(109, 22)
        Me.rad3.TabIndex = 6
        Me.rad3.Text = "Last 3 Days"
        Me.rad3.UseVisualStyleBackColor = True
        '
        'radWeek
        '
        Me.radWeek.AutoSize = True
        Me.radWeek.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radWeek.Location = New System.Drawing.Point(15, 145)
        Me.radWeek.Name = "radWeek"
        Me.radWeek.Size = New System.Drawing.Size(66, 22)
        Me.radWeek.TabIndex = 7
        Me.radWeek.Text = "Week"
        Me.radWeek.UseVisualStyleBackColor = True
        '
        'radMonth
        '
        Me.radMonth.AutoSize = True
        Me.radMonth.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radMonth.Location = New System.Drawing.Point(15, 173)
        Me.radMonth.Name = "radMonth"
        Me.radMonth.Size = New System.Drawing.Size(73, 22)
        Me.radMonth.TabIndex = 8
        Me.radMonth.Text = "Month"
        Me.radMonth.UseVisualStyleBackColor = True
        '
        'radYear
        '
        Me.radYear.AutoSize = True
        Me.radYear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radYear.Location = New System.Drawing.Point(15, 201)
        Me.radYear.Name = "radYear"
        Me.radYear.Size = New System.Drawing.Size(60, 22)
        Me.radYear.TabIndex = 9
        Me.radYear.Text = "Year"
        Me.radYear.UseVisualStyleBackColor = True
        '
        'radCustom
        '
        Me.radCustom.AutoSize = True
        Me.radCustom.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radCustom.Location = New System.Drawing.Point(15, 229)
        Me.radCustom.Name = "radCustom"
        Me.radCustom.Size = New System.Drawing.Size(92, 22)
        Me.radCustom.TabIndex = 10
        Me.radCustom.Text = "Custom..."
        Me.radCustom.UseVisualStyleBackColor = True
        '
        'startDatePicker
        '
        Me.startDatePicker.CalendarFont = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startDatePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.startDatePicker.Location = New System.Drawing.Point(25, 254)
        Me.startDatePicker.Name = "startDatePicker"
        Me.startDatePicker.Size = New System.Drawing.Size(130, 26)
        Me.startDatePicker.TabIndex = 4
        Me.startDatePicker.TabStop = False
        '
        'endDatePicker
        '
        Me.endDatePicker.CalendarFont = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.endDatePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.endDatePicker.Location = New System.Drawing.Point(25, 286)
        Me.endDatePicker.Name = "endDatePicker"
        Me.endDatePicker.Size = New System.Drawing.Size(130, 26)
        Me.endDatePicker.TabIndex = 11
        Me.endDatePicker.TabStop = False
        '
        'dateRangeGroup
        '
        Me.dateRangeGroup.BackColor = System.Drawing.Color.Black
        Me.dateRangeGroup.Controls.Add(Me.alignToGridLinePic)
        Me.dateRangeGroup.Controls.Add(Me.rangeRightShiftPic)
        Me.dateRangeGroup.Controls.Add(Me.endDatePicker)
        Me.dateRangeGroup.Controls.Add(Me.alignToGridPic)
        Me.dateRangeGroup.Controls.Add(Me.startDatePicker)
        Me.dateRangeGroup.Controls.Add(Me.rangeLeftShiftPic)
        Me.dateRangeGroup.Controls.Add(Me.radCustom)
        Me.dateRangeGroup.Controls.Add(Me.radYear)
        Me.dateRangeGroup.Controls.Add(Me.radMonth)
        Me.dateRangeGroup.Controls.Add(Me.radWeek)
        Me.dateRangeGroup.Controls.Add(Me.rad3)
        Me.dateRangeGroup.Controls.Add(Me.radToday)
        Me.dateRangeGroup.Controls.Add(Me.radAlltime)
        Me.dateRangeGroup.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateRangeGroup.ForeColor = System.Drawing.Color.White
        Me.dateRangeGroup.Location = New System.Drawing.Point(7, 193)
        Me.dateRangeGroup.Name = "dateRangeGroup"
        Me.dateRangeGroup.Size = New System.Drawing.Size(162, 318)
        Me.dateRangeGroup.TabIndex = 3
        Me.dateRangeGroup.TabStop = False
        Me.dateRangeGroup.Text = "Date Range"
        '
        'alignToGridLinePic
        '
        Me.alignToGridLinePic.BackColor = System.Drawing.Color.White
        Me.alignToGridLinePic.Location = New System.Drawing.Point(99, 150)
        Me.alignToGridLinePic.Name = "alignToGridLinePic"
        Me.alignToGridLinePic.Size = New System.Drawing.Size(1, 70)
        Me.alignToGridLinePic.TabIndex = 29
        Me.alignToGridLinePic.TabStop = False
        '
        'rangeRightShiftPic
        '
        Me.rangeRightShiftPic.BackColor = System.Drawing.Color.Black
        Me.rangeRightShiftPic.BackgroundImage = Global.GTimer.My.Resources.Resources.arrow_right
        Me.rangeRightShiftPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.rangeRightShiftPic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rangeRightShiftPic.Location = New System.Drawing.Point(90, 19)
        Me.rangeRightShiftPic.Name = "rangeRightShiftPic"
        Me.rangeRightShiftPic.Size = New System.Drawing.Size(38, 38)
        Me.rangeRightShiftPic.TabIndex = 12
        Me.rangeRightShiftPic.TabStop = False
        Me.tt.SetToolTip(Me.rangeRightShiftPic, "Shift the selected date range to the right")
        '
        'alignToGridPic
        '
        Me.alignToGridPic.BackgroundImage = Global.GTimer.My.Resources.Resources.no_grid
        Me.alignToGridPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.alignToGridPic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.alignToGridPic.Location = New System.Drawing.Point(108, 165)
        Me.alignToGridPic.Name = "alignToGridPic"
        Me.alignToGridPic.Size = New System.Drawing.Size(40, 40)
        Me.alignToGridPic.TabIndex = 29
        Me.alignToGridPic.TabStop = False
        '
        'rangeLeftShiftPic
        '
        Me.rangeLeftShiftPic.BackColor = System.Drawing.Color.Black
        Me.rangeLeftShiftPic.BackgroundImage = Global.GTimer.My.Resources.Resources.arrow_left
        Me.rangeLeftShiftPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.rangeLeftShiftPic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rangeLeftShiftPic.Location = New System.Drawing.Point(28, 19)
        Me.rangeLeftShiftPic.Name = "rangeLeftShiftPic"
        Me.rangeLeftShiftPic.Size = New System.Drawing.Size(38, 38)
        Me.rangeLeftShiftPic.TabIndex = 6
        Me.rangeLeftShiftPic.TabStop = False
        Me.tt.SetToolTip(Me.rangeLeftShiftPic, "Shift the selected date range to the left")
        '
        'logoPic
        '
        Me.logoPic.BackgroundImage = CType(resources.GetObject("logoPic.BackgroundImage"), System.Drawing.Image)
        Me.logoPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.logoPic.Location = New System.Drawing.Point(5, -10)
        Me.logoPic.Name = "logoPic"
        Me.logoPic.Size = New System.Drawing.Size(150, 150)
        Me.logoPic.TabIndex = 1
        Me.logoPic.TabStop = False
        '
        'statsGroup
        '
        Me.statsGroup.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.statsGroup.ForeColor = System.Drawing.Color.White
        Me.statsGroup.Location = New System.Drawing.Point(228, 485)
        Me.statsGroup.Name = "statsGroup"
        Me.statsGroup.Size = New System.Drawing.Size(649, 240)
        Me.statsGroup.TabIndex = 4
        Me.statsGroup.TabStop = False
        Me.statsGroup.Text = "Ranking"
        '
        'diagramButton
        '
        Me.diagramButton.BackgroundImage = Global.GTimer.My.Resources.Resources.diagram
        Me.diagramButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.diagramButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.diagramButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.diagramButton.Location = New System.Drawing.Point(202, 311)
        Me.diagramButton.Name = "diagramButton"
        Me.diagramButton.Size = New System.Drawing.Size(46, 42)
        Me.diagramButton.TabIndex = 19
        Me.diagramButton.TabStop = False
        Me.tt.SetToolTip(Me.diagramButton, "Open the stats diagram")
        Me.diagramButton.UseVisualStyleBackColor = True
        '
        'totalTimeLabel
        '
        Me.totalTimeLabel.AutoSize = True
        Me.totalTimeLabel.Font = New System.Drawing.Font("Georgia", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.totalTimeLabel.ForeColor = System.Drawing.Color.White
        Me.totalTimeLabel.Location = New System.Drawing.Point(400, 281)
        Me.totalTimeLabel.Name = "totalTimeLabel"
        Me.totalTimeLabel.Size = New System.Drawing.Size(28, 29)
        Me.totalTimeLabel.TabIndex = 6
        Me.totalTimeLabel.Text = "0"
        '
        'totalTimeCaptionLabel
        '
        Me.totalTimeCaptionLabel.AutoSize = True
        Me.totalTimeCaptionLabel.Font = New System.Drawing.Font("Georgia", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.totalTimeCaptionLabel.ForeColor = System.Drawing.Color.White
        Me.totalTimeCaptionLabel.Location = New System.Drawing.Point(366, 255)
        Me.totalTimeCaptionLabel.Name = "totalTimeCaptionLabel"
        Me.totalTimeCaptionLabel.Size = New System.Drawing.Size(101, 18)
        Me.totalTimeCaptionLabel.TabIndex = 5
        Me.totalTimeCaptionLabel.Text = "Total G Time:"
        '
        'patchTree
        '
        Me.patchTree.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.patchTree.Font = New System.Drawing.Font("Georgia", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.patchTree.ForeColor = System.Drawing.Color.White
        Me.patchTree.Location = New System.Drawing.Point(174, 173)
        Me.patchTree.Name = "patchTree"
        TreeNode1.Name = "Node2"
        TreeNode1.Text = "Stable Release"
        TreeNode2.Name = "Node8"
        TreeNode2.Text = "v1.0 - 03.01.21"
        TreeNode3.Name = "Node9"
        TreeNode3.Text = "Summary statistics are updated every second"
        TreeNode4.Name = "Node10"
        TreeNode4.Text = "Fixed 'Today' Mode"
        TreeNode5.Name = "Node11"
        TreeNode5.Text = "Fixed a bug where time was logged twice"
        TreeNode6.Name = "v1.1"
        TreeNode6.Text = "v1.1 - 03.01.21"
        TreeNode7.Name = "Node12"
        TreeNode7.Text = "Time labels format changed"
        TreeNode8.Name = "Node13"
        TreeNode8.Text = "Game panel titles removed"
        TreeNode9.Name = "Node14"
        TreeNode9.Text = "Total time label font color adjusted"
        TreeNode10.Name = "Node15"
        TreeNode10.Text = "Import Configuration option"
        TreeNode11.Name = "Node16"
        TreeNode11.Text = "Game panel colorless logos added"
        TreeNode12.Name = "Node17"
        TreeNode12.Text = "Resource file structure changed"
        TreeNode13.Name = "Node18"
        TreeNode13.Text = "Selected view mode is now saved after closing"
        TreeNode14.Name = "Node5"
        TreeNode14.Text = "v1.2 - 04.01.21"
        TreeNode15.Name = "Node19"
        TreeNode15.Text = "Clicking logos excludes game from summary statistics"
        TreeNode16.Name = "Node20"
        TreeNode16.Text = "Fixed a bug where too much time was logged"
        TreeNode17.Name = "Node21"
        TreeNode17.Text = "Periodic time logging interval decreased to one minute"
        TreeNode18.Name = "Node6"
        TreeNode18.Text = "v1.2.1 - 04.01.21"
        TreeNode19.Name = "Node22"
        TreeNode19.Text = "Patch Notes published"
        TreeNode20.Name = "Node0"
        TreeNode20.Text = "Time logs are saved when closing GTimer"
        TreeNode21.Name = "Node1"
        TreeNode21.Text = "Only one active game is counted at a time"
        TreeNode22.Name = "Node0"
        TreeNode22.Text = "Window position and size can be saved"
        TreeNode23.Name = "Node1"
        TreeNode23.Text = "Window start state can be assigned"
        TreeNode24.Name = "Node2"
        TreeNode24.Text = "Label font family can be changed globally"
        TreeNode25.Name = "Node0"
        TreeNode25.Text = "Date range can be now be shifted"
        TreeNode26.Name = "Node7"
        TreeNode26.Text = "v1.3 - 11.01.21"
        TreeNode27.Name = "v1.0"
        TreeNode27.Text = "Version 1.x"
        TreeNode28.Name = "Node2"
        TreeNode28.Text = "New GTimer versions can now be installed in the Options"
        TreeNode29.Name = "Node0"
        TreeNode29.Text = "Game times can now be averaged over a day, week, month and year"
        TreeNode30.Name = "Node0"
        TreeNode30.Text = "Friend System introduced"
        TreeNode31.Name = "Node1"
        TreeNode31.Text = "Stats from friends can be displayed"
        TreeNode32.Name = "Node1"
        TreeNode32.Text = "v2.0 - 13.01.2021"
        TreeNode33.Name = "Node1"
        TreeNode33.Text = "Displaying the window in the taskbar can be toggled when minimizing the window"
        TreeNode34.Name = "Node2"
        TreeNode34.Text = "Only one instance of GTimer is allowed at a time"
        TreeNode35.Name = "Node3"
        TreeNode35.Text = "Selecting a date range option indicates if it chooses another start date"
        TreeNode36.Name = "Node0"
        TreeNode36.Text = "Automatic updates can now be enabled in the options"
        TreeNode37.Name = "Node0"
        TreeNode37.Text = "v2.0.1 - 13.01.2021"
        TreeNode38.Name = "Node1"
        TreeNode38.Text = "Fixed the auto update feature"
        TreeNode39.Name = "Node2"
        TreeNode39.Text = "Added a button to save option changes"
        TreeNode40.Name = "Node0"
        TreeNode40.Text = "v2.0.2 - 14.01.2021"
        TreeNode41.Name = "Node1"
        TreeNode41.Text = "The application icon has been set to the GTimer logo"
        TreeNode42.Name = "Node0"
        TreeNode42.Text = "v2.0.3 - 15.01.2021"
        TreeNode43.Name = "Node1"
        TreeNode43.Text = "Time ratios for games moved to game panels"
        TreeNode44.Name = "Node2"
        TreeNode44.Text = "Ranking system among friends released"
        TreeNode45.Name = "Node1"
        TreeNode45.Text = "Fixed a bug where a friend appeared to be playing a game after closing it"
        TreeNode46.Name = "Node0"
        TreeNode46.Text = "Added shared stats file access management"
        TreeNode47.Name = "Node0"
        TreeNode47.Text = "Game time now ticks continuously for friends"
        TreeNode48.Name = "Node0"
        TreeNode48.Text = "v2.1 - 25.01.2021"
        TreeNode49.Name = "Node1"
        TreeNode49.Text = "Fixed a bug where the ranking average time was too high"
        TreeNode50.Name = "Node2"
        TreeNode50.Text = "Game time ratio bars were redesigned. Percentages can be seen by mouse hovering"
        TreeNode51.Name = "Node3"
        TreeNode51.Text = "Game panels are now sorted downwards by game time"
        TreeNode52.Name = "Node4"
        TreeNode52.Text = "Name plates are now placed outside the ranking bars"
        TreeNode53.Name = "Node0"
        TreeNode53.Text = "v2.2 - 26.01.2021"
        TreeNode54.Name = "Node1"
        TreeNode54.Text = "Fixed a bug where offline user times kept ticking"
        TreeNode55.Name = "Node2"
        TreeNode55.Text = "Ranking name labels are displayed correctly"
        TreeNode56.Name = "Node0"
        TreeNode56.Text = "v2.2.1 - 11.02.2021"
        TreeNode57.Name = "Node1"
        TreeNode57.Text = "Added game management feature"
        TreeNode58.Name = "Node2"
        TreeNode58.Text = "The last game panel summarizes all least popular games"
        TreeNode59.Name = "Node3"
        TreeNode59.Text = "All data can be reloaded from the options window"
        TreeNode60.Name = "Node0"
        TreeNode60.Text = "A custom date range can now be entered manually"
        TreeNode61.Name = "Node0"
        TreeNode61.Text = "v2.3 - 08.03.2021"
        TreeNode62.Name = "Node0"
        TreeNode62.Text = "Version 2.x"
        TreeNode63.Name = "Node0"
        TreeNode63.Text = "GTimer Chart introduced"
        TreeNode64.Name = "Node2"
        TreeNode64.Text = "Game sorting options added"
        TreeNode65.Name = "Node3"
        TreeNode65.Text = "When friends open your profile in their GTimer, they now see your game logos"
        TreeNode66.Name = "Node1"
        TreeNode66.Text = "Resizing the window dynamically rearranges the game panels"
        TreeNode67.Name = "Node1"
        TreeNode67.Text = "v3.0 - 29.08.2021"
        TreeNode68.Name = "Node1"
        TreeNode68.Text = "Negative window starting coordinates are now allowed"
        TreeNode69.Name = "Node2"
        TreeNode69.Text = "The window frame can now be locked to hide the title bar"
        TreeNode70.Name = "Node3"
        TreeNode70.Text = "The tracking of game time can now be paused by a toggle button"
        TreeNode71.Name = "Node0"
        TreeNode71.Text = "v3.1 - 21.09.2021"
        TreeNode72.Name = "Node1"
        TreeNode72.Text = "The toggle behaviour of the window lock and pause icons were inverted"
        TreeNode73.Name = "Node0"
        TreeNode73.Text = "Game panels have context menus that can be opened by right-click"
        TreeNode74.Name = "Node0"
        TreeNode74.Text = "Games can be started in context menu and by left-clicking panel"
        TreeNode75.Name = "Node0"
        TreeNode75.Text = "The user can scroll through the list of games with the mouse wheel"
        TreeNode76.Name = "Node1"
        TreeNode76.Text = "The recorded G-Time can be adjusted down in the context menu"
        TreeNode77.Name = "Node1"
        TreeNode77.Text = "Stats Chart: Game selection added"
        TreeNode78.Name = "Node0"
        TreeNode78.Text = "Date Ranges (Week, Month, Year) can be aligned to their respective grid"
        TreeNode79.Name = "Node0"
        TreeNode79.Text = "v3.2 - 06.01.2022"
        TreeNode80.Name = "Node1"
        TreeNode80.Text = "Fixed the date range mode 'Week' with grid on"
        TreeNode81.Name = "Node2"
        TreeNode81.Text = "Friends can be invited to join games"
        TreeNode82.Name = "Node0"
        TreeNode82.Text = "v3.3 - 23.03.2022"
        TreeNode83.Name = "Node2"
        TreeNode83.Text = "New sorting method: Last Played"
        TreeNode84.Name = "Node3"
        TreeNode84.Text = "Hovering over game displays last time played"
        TreeNode85.Name = "Node4"
        TreeNode85.Text = "Fixed the autostart function. GTimer now must be lauchned as administrator"
        TreeNode86.Name = "Node1"
        TreeNode86.Text = "v3.4 - 07.01.2023"
        TreeNode87.Name = "Node0"
        TreeNode87.Text = "Version 3.x"
        TreeNode88.Name = "top"
        TreeNode88.Text = "Patch Notes"
        Me.patchTree.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode88})
        Me.patchTree.ShowPlusMinus = False
        Me.patchTree.ShowRootLines = False
        Me.patchTree.Size = New System.Drawing.Size(125, 34)
        Me.patchTree.TabIndex = 0
        Me.patchTree.Visible = False
        '
        'patchNotesClosePic
        '
        Me.patchNotesClosePic.BackColor = System.Drawing.Color.Gray
        Me.patchNotesClosePic.BackgroundImage = CType(resources.GetObject("patchNotesClosePic.BackgroundImage"), System.Drawing.Image)
        Me.patchNotesClosePic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.patchNotesClosePic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.patchNotesClosePic.Location = New System.Drawing.Point(208, 127)
        Me.patchNotesClosePic.Name = "patchNotesClosePic"
        Me.patchNotesClosePic.Size = New System.Drawing.Size(40, 40)
        Me.patchNotesClosePic.TabIndex = 5
        Me.patchNotesClosePic.TabStop = False
        Me.tt.SetToolTip(Me.patchNotesClosePic, "Close the patch notes")
        Me.patchNotesClosePic.Visible = False
        '
        'iconTray
        '
        Me.iconTray.Icon = CType(resources.GetObject("iconTray.Icon"), System.Drawing.Icon)
        Me.iconTray.Text = "GTimer"
        Me.iconTray.Visible = True
        '
        'viewModeGroup
        '
        Me.viewModeGroup.BackColor = System.Drawing.Color.Black
        Me.viewModeGroup.Controls.Add(Me.labelViewModeAverage)
        Me.viewModeGroup.Controls.Add(Me.radAvYear)
        Me.viewModeGroup.Controls.Add(Me.radAvMonth)
        Me.viewModeGroup.Controls.Add(Me.radAvWeek)
        Me.viewModeGroup.Controls.Add(Me.radAvDay)
        Me.viewModeGroup.Controls.Add(Me.radTotal)
        Me.viewModeGroup.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewModeGroup.ForeColor = System.Drawing.Color.White
        Me.viewModeGroup.Location = New System.Drawing.Point(7, 515)
        Me.viewModeGroup.Name = "viewModeGroup"
        Me.viewModeGroup.Size = New System.Drawing.Size(162, 184)
        Me.viewModeGroup.TabIndex = 13
        Me.viewModeGroup.TabStop = False
        Me.viewModeGroup.Text = "View Mode"
        '
        'labelViewModeAverage
        '
        Me.labelViewModeAverage.AutoSize = True
        Me.labelViewModeAverage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.labelViewModeAverage.Font = New System.Drawing.Font("Georgia", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelViewModeAverage.ForeColor = System.Drawing.Color.White
        Me.labelViewModeAverage.Location = New System.Drawing.Point(45, 49)
        Me.labelViewModeAverage.Name = "labelViewModeAverage"
        Me.labelViewModeAverage.Size = New System.Drawing.Size(67, 18)
        Me.labelViewModeAverage.TabIndex = 14
        Me.labelViewModeAverage.Text = "Average:"
        '
        'radAvYear
        '
        Me.radAvYear.AutoSize = True
        Me.radAvYear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radAvYear.Location = New System.Drawing.Point(15, 154)
        Me.radAvYear.Name = "radAvYear"
        Me.radAvYear.Size = New System.Drawing.Size(89, 22)
        Me.radAvYear.TabIndex = 9
        Me.radAvYear.Text = "Per Year"
        Me.radAvYear.UseVisualStyleBackColor = True
        '
        'radAvMonth
        '
        Me.radAvMonth.AutoSize = True
        Me.radAvMonth.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radAvMonth.Location = New System.Drawing.Point(15, 126)
        Me.radAvMonth.Name = "radAvMonth"
        Me.radAvMonth.Size = New System.Drawing.Size(102, 22)
        Me.radAvMonth.TabIndex = 8
        Me.radAvMonth.Text = "Per Month"
        Me.radAvMonth.UseVisualStyleBackColor = True
        '
        'radAvWeek
        '
        Me.radAvWeek.AutoSize = True
        Me.radAvWeek.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radAvWeek.Location = New System.Drawing.Point(15, 98)
        Me.radAvWeek.Name = "radAvWeek"
        Me.radAvWeek.Size = New System.Drawing.Size(95, 22)
        Me.radAvWeek.TabIndex = 7
        Me.radAvWeek.Text = "Per Week"
        Me.radAvWeek.UseVisualStyleBackColor = True
        '
        'radAvDay
        '
        Me.radAvDay.AutoSize = True
        Me.radAvDay.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radAvDay.Location = New System.Drawing.Point(15, 70)
        Me.radAvDay.Name = "radAvDay"
        Me.radAvDay.Size = New System.Drawing.Size(83, 22)
        Me.radAvDay.TabIndex = 6
        Me.radAvDay.Text = "Per Day"
        Me.radAvDay.UseVisualStyleBackColor = True
        '
        'radTotal
        '
        Me.radTotal.AutoSize = True
        Me.radTotal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radTotal.Location = New System.Drawing.Point(15, 25)
        Me.radTotal.Name = "radTotal"
        Me.radTotal.Size = New System.Drawing.Size(63, 22)
        Me.radTotal.TabIndex = 4
        Me.radTotal.Text = "Total"
        Me.radTotal.UseVisualStyleBackColor = True
        '
        'appNameLabel
        '
        Me.appNameLabel.AutoSize = True
        Me.appNameLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.appNameLabel.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.appNameLabel.ForeColor = System.Drawing.Color.White
        Me.appNameLabel.Location = New System.Drawing.Point(25, 127)
        Me.appNameLabel.Name = "appNameLabel"
        Me.appNameLabel.Size = New System.Drawing.Size(76, 23)
        Me.appNameLabel.TabIndex = 14
        Me.appNameLabel.Text = "GTimer"
        '
        'removeUser
        '
        Me.removeUser.ForeColor = System.Drawing.Color.White
        Me.removeUser.Name = "removeUser"
        Me.removeUser.Size = New System.Drawing.Size(164, 22)
        Me.removeUser.Text = "Remove"
        '
        'conUser
        '
        Me.conUser.BackColor = System.Drawing.Color.Black
        Me.conUser.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.removeUser, Me.ReloadStatusToolStripMenuItem, Me.ReloadTimeToolStripMenuItem, Me.RejectInvitationsToolStripMenuItem})
        Me.conUser.Name = "conUser"
        Me.conUser.Size = New System.Drawing.Size(165, 92)
        '
        'ReloadStatusToolStripMenuItem
        '
        Me.ReloadStatusToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.ReloadStatusToolStripMenuItem.Name = "ReloadStatusToolStripMenuItem"
        Me.ReloadStatusToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.ReloadStatusToolStripMenuItem.Text = "Reload Status"
        '
        'ReloadTimeToolStripMenuItem
        '
        Me.ReloadTimeToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.ReloadTimeToolStripMenuItem.Name = "ReloadTimeToolStripMenuItem"
        Me.ReloadTimeToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.ReloadTimeToolStripMenuItem.Text = "Reload Time"
        '
        'RejectInvitationsToolStripMenuItem
        '
        Me.RejectInvitationsToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.RejectInvitationsToolStripMenuItem.Name = "RejectInvitationsToolStripMenuItem"
        Me.RejectInvitationsToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.RejectInvitationsToolStripMenuItem.Text = "Reject Invitations"
        '
        'fsw
        '
        Me.fsw.EnableRaisingEvents = True
        Me.fsw.SynchronizingObject = Me
        '
        'addGamePic
        '
        Me.addGamePic.BackgroundImage = CType(resources.GetObject("addGamePic.BackgroundImage"), System.Drawing.Image)
        Me.addGamePic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.addGamePic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.addGamePic.Location = New System.Drawing.Point(192, 213)
        Me.addGamePic.Name = "addGamePic"
        Me.addGamePic.Size = New System.Drawing.Size(80, 80)
        Me.addGamePic.TabIndex = 18
        Me.addGamePic.TabStop = False
        Me.tt.SetToolTip(Me.addGamePic, "Add a game")
        Me.addGamePic.Visible = False
        '
        'lockBarButton
        '
        Me.lockBarButton.BackColor = System.Drawing.Color.Transparent
        Me.lockBarButton.BackgroundImage = Global.GTimer.My.Resources.Resources.lock_inv
        Me.lockBarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.lockBarButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lockBarButton.FlatAppearance.BorderSize = 0
        Me.lockBarButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray
        Me.lockBarButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray
        Me.lockBarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lockBarButton.Location = New System.Drawing.Point(66, 158)
        Me.lockBarButton.Name = "lockBarButton"
        Me.lockBarButton.Size = New System.Drawing.Size(35, 30)
        Me.lockBarButton.TabIndex = 24
        Me.tt.SetToolTip(Me.lockBarButton, "Lock/Unlock the window frame")
        Me.lockBarButton.UseVisualStyleBackColor = False
        '
        'pauseButton
        '
        Me.pauseButton.BackColor = System.Drawing.Color.Transparent
        Me.pauseButton.BackgroundImage = Global.GTimer.My.Resources.Resources.pause
        Me.pauseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pauseButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pauseButton.FlatAppearance.BorderSize = 0
        Me.pauseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray
        Me.pauseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray
        Me.pauseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.pauseButton.Location = New System.Drawing.Point(128, 160)
        Me.pauseButton.Name = "pauseButton"
        Me.pauseButton.Size = New System.Drawing.Size(33, 28)
        Me.pauseButton.TabIndex = 25
        Me.tt.SetToolTip(Me.pauseButton, "Pause/Resume G-Time Tracking")
        Me.pauseButton.UseVisualStyleBackColor = False
        '
        'closeButton
        '
        Me.closeButton.BackgroundImage = Global.GTimer.My.Resources.Resources.close
        Me.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.closeButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.closeButton.FlatAppearance.BorderSize = 0
        Me.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkRed
        Me.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.closeButton.Location = New System.Drawing.Point(323, 282)
        Me.closeButton.Name = "closeButton"
        Me.closeButton.Size = New System.Drawing.Size(37, 37)
        Me.closeButton.TabIndex = 26
        Me.closeButton.TabStop = False
        Me.tt.SetToolTip(Me.closeButton, "Close the application")
        Me.closeButton.UseVisualStyleBackColor = True
        '
        'fswBackoff
        '
        Me.fswBackoff.Interval = 1000
        '
        'conGamePanel
        '
        Me.conGamePanel.BackColor = System.Drawing.Color.Black
        Me.conGamePanel.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StartGameToolStripMenuItem, Me.GoToGameSettingsToolStripMenuItem, Me.SendInviteToolStripMenuItem, Me.IncludeToolStripMenuItem, Me.IncludeExclusivelyToolStripMenuItem, Me.ShowInDiagramToolStripMenuItem, Me.AdjustToolStripMenuItem})
        Me.conGamePanel.Name = "conUser"
        Me.conGamePanel.Size = New System.Drawing.Size(201, 158)
        '
        'StartGameToolStripMenuItem
        '
        Me.StartGameToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.StartGameToolStripMenuItem.Image = Global.GTimer.My.Resources.Resources.play_green
        Me.StartGameToolStripMenuItem.Name = "StartGameToolStripMenuItem"
        Me.StartGameToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.StartGameToolStripMenuItem.Text = "Start Game"
        '
        'GoToGameSettingsToolStripMenuItem
        '
        Me.GoToGameSettingsToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GoToGameSettingsToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.GoToGameSettingsToolStripMenuItem.Image = Global.GTimer.My.Resources.Resources.gear_black
        Me.GoToGameSettingsToolStripMenuItem.Name = "GoToGameSettingsToolStripMenuItem"
        Me.GoToGameSettingsToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.GoToGameSettingsToolStripMenuItem.Text = "Go to Game Settings"
        '
        'SendInviteToolStripMenuItem
        '
        Me.SendInviteToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.SendInviteToolStripMenuItem.Image = Global.GTimer.My.Resources.Resources.invite_s
        Me.SendInviteToolStripMenuItem.Name = "SendInviteToolStripMenuItem"
        Me.SendInviteToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.SendInviteToolStripMenuItem.Text = "Send Game Invite"
        '
        'IncludeToolStripMenuItem
        '
        Me.IncludeToolStripMenuItem.BackColor = System.Drawing.Color.Black
        Me.IncludeToolStripMenuItem.Checked = True
        Me.IncludeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.IncludeToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.IncludeToolStripMenuItem.Name = "IncludeToolStripMenuItem"
        Me.IncludeToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.IncludeToolStripMenuItem.Text = "Include in Stats"
        '
        'IncludeExclusivelyToolStripMenuItem
        '
        Me.IncludeExclusivelyToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.IncludeExclusivelyToolStripMenuItem.Name = "IncludeExclusivelyToolStripMenuItem"
        Me.IncludeExclusivelyToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.IncludeExclusivelyToolStripMenuItem.Text = "Include exclusively"
        '
        'ShowInDiagramToolStripMenuItem
        '
        Me.ShowInDiagramToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.ShowInDiagramToolStripMenuItem.Image = Global.GTimer.My.Resources.Resources.diagram_black
        Me.ShowInDiagramToolStripMenuItem.Name = "ShowInDiagramToolStripMenuItem"
        Me.ShowInDiagramToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.ShowInDiagramToolStripMenuItem.Text = "Open in Diagram"
        '
        'AdjustToolStripMenuItem
        '
        Me.AdjustToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.AdjustToolStripMenuItem.Image = Global.GTimer.My.Resources.Resources.Edit_icon__the_Noun_Project_30184__svg
        Me.AdjustToolStripMenuItem.Name = "AdjustToolStripMenuItem"
        Me.AdjustToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.AdjustToolStripMenuItem.Text = "Adjust recorded G-Time"
        '
        'inviteBackoffTimer
        '
        Me.inviteBackoffTimer.Interval = 1000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(896, 740)
        Me.Controls.Add(Me.closeButton)
        Me.Controls.Add(Me.pauseButton)
        Me.Controls.Add(Me.lockBarButton)
        Me.Controls.Add(Me.diagramButton)
        Me.Controls.Add(Me.addGamePic)
        Me.Controls.Add(Me.totalTimeLabel)
        Me.Controls.Add(Me.totalTimeCaptionLabel)
        Me.Controls.Add(Me.appNameLabel)
        Me.Controls.Add(Me.viewModeGroup)
        Me.Controls.Add(Me.patchNotesClosePic)
        Me.Controls.Add(Me.patchTree)
        Me.Controls.Add(Me.statsGroup)
        Me.Controls.Add(Me.dateRangeGroup)
        Me.Controls.Add(Me.versionLabel)
        Me.Controls.Add(Me.logoPic)
        Me.Controls.Add(Me.optionButton)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "GTimer"
        Me.dateRangeGroup.ResumeLayout(False)
        Me.dateRangeGroup.PerformLayout()
        CType(Me.alignToGridLinePic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rangeRightShiftPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.alignToGridPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rangeLeftShiftPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.logoPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.patchNotesClosePic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.viewModeGroup.ResumeLayout(False)
        Me.viewModeGroup.PerformLayout()
        Me.conUser.ResumeLayout(False)
        CType(Me.fsw, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.addGamePic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.conGamePanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tracker As Timer
    Friend WithEvents tempWriter As Timer
    Friend WithEvents optionButton As Button
    Friend WithEvents versionLabel As Label
    Friend WithEvents radAlltime As RadioButton
    Friend WithEvents radToday As RadioButton
    Friend WithEvents rad3 As RadioButton
    Friend WithEvents radWeek As RadioButton
    Friend WithEvents radMonth As RadioButton
    Friend WithEvents radYear As RadioButton
    Friend WithEvents radCustom As RadioButton
    Friend WithEvents startDatePicker As DateTimePicker
    Friend WithEvents endDatePicker As DateTimePicker
    Friend WithEvents dateRangeGroup As GroupBox
    Friend WithEvents logoPic As PictureBox
    Friend WithEvents statsGroup As GroupBox
    Friend WithEvents totalTimeCaptionLabel As Label
    Friend WithEvents totalTimeLabel As Label
    Friend WithEvents patchTree As TreeView
    Friend WithEvents patchNotesClosePic As PictureBox
    Friend WithEvents iconTray As NotifyIcon
    Friend WithEvents rangeRightShiftPic As PictureBox
    Friend WithEvents rangeLeftShiftPic As PictureBox
    Friend WithEvents viewModeGroup As GroupBox
    Friend WithEvents radTotal As RadioButton
    Friend WithEvents labelViewModeAverage As Label
    Friend WithEvents radAvYear As RadioButton
    Friend WithEvents radAvMonth As RadioButton
    Friend WithEvents radAvWeek As RadioButton
    Friend WithEvents radAvDay As RadioButton
    Friend WithEvents appNameLabel As Label
    Friend WithEvents removeUser As ToolStripMenuItem
    Friend WithEvents conUser As ContextMenuStrip
    Friend WithEvents ReloadStatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReloadTimeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents fsw As IO.FileSystemWatcher
    Friend WithEvents tt As ToolTip
    Friend WithEvents fswBackoff As Timer
    Friend WithEvents addGamePic As PictureBox
    Friend WithEvents diagramButton As Button
    Friend WithEvents lockBarButton As Button
    Friend WithEvents pauseButton As Button
    Friend WithEvents closeButton As Button
    Friend WithEvents conGamePanel As ContextMenuStrip
    Friend WithEvents IncludeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GoToGameSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StartGameToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IncludeExclusivelyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowInDiagramToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AdjustToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents alignToGridPic As PictureBox
    Friend WithEvents alignToGridLinePic As PictureBox
    Friend WithEvents SendInviteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents inviteBackoffTimer As Timer
    Friend WithEvents RejectInvitationsToolStripMenuItem As ToolStripMenuItem
End Class
