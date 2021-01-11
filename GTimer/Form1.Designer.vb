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
        Dim TreeNode28 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Patch Notes", New System.Windows.Forms.TreeNode() {TreeNode27})
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
        Me.settingsGroup = New System.Windows.Forms.GroupBox()
        Me.rangeRightShiftPic = New System.Windows.Forms.PictureBox()
        Me.rangeLeftShiftPic = New System.Windows.Forms.PictureBox()
        Me.logoPic = New System.Windows.Forms.PictureBox()
        Me.statsGroup = New System.Windows.Forms.GroupBox()
        Me.totalTimeLabel = New System.Windows.Forms.Label()
        Me.totalTimeCaptionLabel = New System.Windows.Forms.Label()
        Me.patchTree = New System.Windows.Forms.TreeView()
        Me.patchNotesClosePic = New System.Windows.Forms.PictureBox()
        Me.iconTray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.settingsGroup.SuspendLayout()
        CType(Me.rangeRightShiftPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rangeLeftShiftPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.logoPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.statsGroup.SuspendLayout()
        CType(Me.patchNotesClosePic, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.optionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.optionButton.Location = New System.Drawing.Point(126, 143)
        Me.optionButton.Name = "optionButton"
        Me.optionButton.Size = New System.Drawing.Size(46, 42)
        Me.optionButton.TabIndex = 0
        Me.optionButton.TabStop = False
        Me.optionButton.UseVisualStyleBackColor = True
        '
        'versionLabel
        '
        Me.versionLabel.AutoSize = True
        Me.versionLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.versionLabel.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.versionLabel.ForeColor = System.Drawing.Color.White
        Me.versionLabel.Location = New System.Drawing.Point(1, 150)
        Me.versionLabel.Name = "versionLabel"
        Me.versionLabel.Size = New System.Drawing.Size(127, 23)
        Me.versionLabel.TabIndex = 2
        Me.versionLabel.Text = "GTimer v1.2.1"
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
        Me.radWeek.Size = New System.Drawing.Size(101, 22)
        Me.radWeek.TabIndex = 7
        Me.radWeek.Text = "Last Week"
        Me.radWeek.UseVisualStyleBackColor = True
        '
        'radMonth
        '
        Me.radMonth.AutoSize = True
        Me.radMonth.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radMonth.Location = New System.Drawing.Point(15, 173)
        Me.radMonth.Name = "radMonth"
        Me.radMonth.Size = New System.Drawing.Size(108, 22)
        Me.radMonth.TabIndex = 8
        Me.radMonth.Text = "Last Month"
        Me.radMonth.UseVisualStyleBackColor = True
        '
        'radYear
        '
        Me.radYear.AutoSize = True
        Me.radYear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radYear.Location = New System.Drawing.Point(15, 201)
        Me.radYear.Name = "radYear"
        Me.radYear.Size = New System.Drawing.Size(95, 22)
        Me.radYear.TabIndex = 9
        Me.radYear.Text = "Last Year"
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
        'settingsGroup
        '
        Me.settingsGroup.BackColor = System.Drawing.Color.Black
        Me.settingsGroup.Controls.Add(Me.rangeRightShiftPic)
        Me.settingsGroup.Controls.Add(Me.endDatePicker)
        Me.settingsGroup.Controls.Add(Me.startDatePicker)
        Me.settingsGroup.Controls.Add(Me.rangeLeftShiftPic)
        Me.settingsGroup.Controls.Add(Me.radCustom)
        Me.settingsGroup.Controls.Add(Me.radYear)
        Me.settingsGroup.Controls.Add(Me.radMonth)
        Me.settingsGroup.Controls.Add(Me.radWeek)
        Me.settingsGroup.Controls.Add(Me.rad3)
        Me.settingsGroup.Controls.Add(Me.radToday)
        Me.settingsGroup.Controls.Add(Me.radAlltime)
        Me.settingsGroup.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.settingsGroup.ForeColor = System.Drawing.Color.White
        Me.settingsGroup.Location = New System.Drawing.Point(6, 196)
        Me.settingsGroup.Name = "settingsGroup"
        Me.settingsGroup.Size = New System.Drawing.Size(162, 318)
        Me.settingsGroup.TabIndex = 3
        Me.settingsGroup.TabStop = False
        Me.settingsGroup.Text = "Date Range"
        '
        'rangeRightShiftPic
        '
        Me.rangeRightShiftPic.BackColor = System.Drawing.Color.Black
        Me.rangeRightShiftPic.BackgroundImage = CType(resources.GetObject("rangeRightShiftPic.BackgroundImage"), System.Drawing.Image)
        Me.rangeRightShiftPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.rangeRightShiftPic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rangeRightShiftPic.Location = New System.Drawing.Point(90, 19)
        Me.rangeRightShiftPic.Name = "rangeRightShiftPic"
        Me.rangeRightShiftPic.Size = New System.Drawing.Size(38, 38)
        Me.rangeRightShiftPic.TabIndex = 12
        Me.rangeRightShiftPic.TabStop = False
        '
        'rangeLeftShiftPic
        '
        Me.rangeLeftShiftPic.BackColor = System.Drawing.Color.Black
        Me.rangeLeftShiftPic.BackgroundImage = CType(resources.GetObject("rangeLeftShiftPic.BackgroundImage"), System.Drawing.Image)
        Me.rangeLeftShiftPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.rangeLeftShiftPic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rangeLeftShiftPic.Location = New System.Drawing.Point(28, 19)
        Me.rangeLeftShiftPic.Name = "rangeLeftShiftPic"
        Me.rangeLeftShiftPic.Size = New System.Drawing.Size(38, 38)
        Me.rangeLeftShiftPic.TabIndex = 6
        Me.rangeLeftShiftPic.TabStop = False
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
        Me.statsGroup.Controls.Add(Me.totalTimeLabel)
        Me.statsGroup.Controls.Add(Me.totalTimeCaptionLabel)
        Me.statsGroup.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.statsGroup.ForeColor = System.Drawing.Color.White
        Me.statsGroup.Location = New System.Drawing.Point(228, 485)
        Me.statsGroup.Name = "statsGroup"
        Me.statsGroup.Size = New System.Drawing.Size(649, 240)
        Me.statsGroup.TabIndex = 4
        Me.statsGroup.TabStop = False
        Me.statsGroup.Text = "Stats"
        '
        'totalTimeLabel
        '
        Me.totalTimeLabel.AutoSize = True
        Me.totalTimeLabel.Font = New System.Drawing.Font("Georgia", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.totalTimeLabel.ForeColor = System.Drawing.Color.White
        Me.totalTimeLabel.Location = New System.Drawing.Point(294, 43)
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
        Me.totalTimeCaptionLabel.Location = New System.Drawing.Point(244, 22)
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
        TreeNode28.Name = "top"
        TreeNode28.Text = "Patch Notes"
        Me.patchTree.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode28})
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
        Me.patchNotesClosePic.Location = New System.Drawing.Point(217, 140)
        Me.patchNotesClosePic.Name = "patchNotesClosePic"
        Me.patchNotesClosePic.Size = New System.Drawing.Size(40, 40)
        Me.patchNotesClosePic.TabIndex = 5
        Me.patchNotesClosePic.TabStop = False
        Me.patchNotesClosePic.Visible = False
        '
        'iconTray
        '
        Me.iconTray.Icon = CType(resources.GetObject("iconTray.Icon"), System.Drawing.Icon)
        Me.iconTray.Text = "GTimer"
        Me.iconTray.Visible = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(896, 740)
        Me.Controls.Add(Me.patchNotesClosePic)
        Me.Controls.Add(Me.patchTree)
        Me.Controls.Add(Me.statsGroup)
        Me.Controls.Add(Me.settingsGroup)
        Me.Controls.Add(Me.versionLabel)
        Me.Controls.Add(Me.logoPic)
        Me.Controls.Add(Me.optionButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "G-Time Tracker"
        Me.settingsGroup.ResumeLayout(False)
        Me.settingsGroup.PerformLayout()
        CType(Me.rangeRightShiftPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rangeLeftShiftPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.logoPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.statsGroup.ResumeLayout(False)
        Me.statsGroup.PerformLayout()
        CType(Me.patchNotesClosePic, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents settingsGroup As GroupBox
    Friend WithEvents logoPic As PictureBox
    Friend WithEvents statsGroup As GroupBox
    Friend WithEvents totalTimeCaptionLabel As Label
    Friend WithEvents totalTimeLabel As Label
    Friend WithEvents patchTree As TreeView
    Friend WithEvents patchNotesClosePic As PictureBox
    Friend WithEvents iconTray As NotifyIcon
    Friend WithEvents rangeRightShiftPic As PictureBox
    Friend WithEvents rangeLeftShiftPic As PictureBox
End Class
