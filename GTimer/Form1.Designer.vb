<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.tracker = New System.Windows.Forms.Timer(Me.components)
        Me.tempWriter = New System.Windows.Forms.Timer(Me.components)
        Me.optionButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.logoPic = New System.Windows.Forms.PictureBox()
        Me.statsGroup = New System.Windows.Forms.GroupBox()
        Me.totalTimeLabel = New System.Windows.Forms.Label()
        Me.totalTimeCaptionLabel = New System.Windows.Forms.Label()
        Me.settingsGroup.SuspendLayout()
        CType(Me.logoPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.statsGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'tracker
        '
        Me.tracker.Interval = 1000
        '
        'tempWriter
        '
        Me.tempWriter.Interval = 600000
        '
        'optionButton
        '
        Me.optionButton.BackgroundImage = CType(resources.GetObject("optionButton.BackgroundImage"), System.Drawing.Image)
        Me.optionButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.optionButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.optionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.optionButton.Location = New System.Drawing.Point(121, 143)
        Me.optionButton.Name = "optionButton"
        Me.optionButton.Size = New System.Drawing.Size(46, 42)
        Me.optionButton.TabIndex = 0
        Me.optionButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(1, 150)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 23)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "GTimer v1.0"
        '
        'radAlltime
        '
        Me.radAlltime.AutoSize = True
        Me.radAlltime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radAlltime.Location = New System.Drawing.Point(15, 23)
        Me.radAlltime.Name = "radAlltime"
        Me.radAlltime.Size = New System.Drawing.Size(77, 22)
        Me.radAlltime.TabIndex = 4
        Me.radAlltime.TabStop = True
        Me.radAlltime.Text = "Alltime"
        Me.radAlltime.UseVisualStyleBackColor = True
        '
        'radToday
        '
        Me.radToday.AutoSize = True
        Me.radToday.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radToday.Location = New System.Drawing.Point(15, 51)
        Me.radToday.Name = "radToday"
        Me.radToday.Size = New System.Drawing.Size(71, 22)
        Me.radToday.TabIndex = 5
        Me.radToday.TabStop = True
        Me.radToday.Text = "Today"
        Me.radToday.UseVisualStyleBackColor = True
        '
        'rad3
        '
        Me.rad3.AutoSize = True
        Me.rad3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rad3.Location = New System.Drawing.Point(15, 79)
        Me.rad3.Name = "rad3"
        Me.rad3.Size = New System.Drawing.Size(109, 22)
        Me.rad3.TabIndex = 6
        Me.rad3.TabStop = True
        Me.rad3.Text = "Last 3 Days"
        Me.rad3.UseVisualStyleBackColor = True
        '
        'radWeek
        '
        Me.radWeek.AutoSize = True
        Me.radWeek.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radWeek.Location = New System.Drawing.Point(15, 107)
        Me.radWeek.Name = "radWeek"
        Me.radWeek.Size = New System.Drawing.Size(101, 22)
        Me.radWeek.TabIndex = 7
        Me.radWeek.TabStop = True
        Me.radWeek.Text = "Last Week"
        Me.radWeek.UseVisualStyleBackColor = True
        '
        'radMonth
        '
        Me.radMonth.AutoSize = True
        Me.radMonth.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radMonth.Location = New System.Drawing.Point(15, 135)
        Me.radMonth.Name = "radMonth"
        Me.radMonth.Size = New System.Drawing.Size(108, 22)
        Me.radMonth.TabIndex = 8
        Me.radMonth.TabStop = True
        Me.radMonth.Text = "Last Month"
        Me.radMonth.UseVisualStyleBackColor = True
        '
        'radYear
        '
        Me.radYear.AutoSize = True
        Me.radYear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radYear.Location = New System.Drawing.Point(15, 163)
        Me.radYear.Name = "radYear"
        Me.radYear.Size = New System.Drawing.Size(95, 22)
        Me.radYear.TabIndex = 9
        Me.radYear.TabStop = True
        Me.radYear.Text = "Last Year"
        Me.radYear.UseVisualStyleBackColor = True
        '
        'radCustom
        '
        Me.radCustom.AutoSize = True
        Me.radCustom.Cursor = System.Windows.Forms.Cursors.Hand
        Me.radCustom.Location = New System.Drawing.Point(15, 191)
        Me.radCustom.Name = "radCustom"
        Me.radCustom.Size = New System.Drawing.Size(92, 22)
        Me.radCustom.TabIndex = 10
        Me.radCustom.TabStop = True
        Me.radCustom.Text = "Custom..."
        Me.radCustom.UseVisualStyleBackColor = True
        '
        'startDatePicker
        '
        Me.startDatePicker.CalendarFont = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startDatePicker.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.startDatePicker.Location = New System.Drawing.Point(30, 216)
        Me.startDatePicker.Name = "startDatePicker"
        Me.startDatePicker.Size = New System.Drawing.Size(125, 26)
        Me.startDatePicker.TabIndex = 4
        '
        'endDatePicker
        '
        Me.endDatePicker.CalendarFont = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.endDatePicker.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.endDatePicker.Location = New System.Drawing.Point(30, 248)
        Me.endDatePicker.Name = "endDatePicker"
        Me.endDatePicker.Size = New System.Drawing.Size(125, 26)
        Me.endDatePicker.TabIndex = 11
        '
        'settingsGroup
        '
        Me.settingsGroup.BackColor = System.Drawing.Color.Black
        Me.settingsGroup.Controls.Add(Me.endDatePicker)
        Me.settingsGroup.Controls.Add(Me.startDatePicker)
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
        Me.settingsGroup.Size = New System.Drawing.Size(162, 284)
        Me.settingsGroup.TabIndex = 3
        Me.settingsGroup.TabStop = False
        Me.settingsGroup.Text = "Mode"
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
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1341, 740)
        Me.Controls.Add(Me.statsGroup)
        Me.Controls.Add(Me.settingsGroup)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.logoPic)
        Me.Controls.Add(Me.optionButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "G-Time Tracker"
        Me.settingsGroup.ResumeLayout(False)
        Me.settingsGroup.PerformLayout()
        CType(Me.logoPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.statsGroup.ResumeLayout(False)
        Me.statsGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tracker As Timer
    Friend WithEvents tempWriter As Timer
    Friend WithEvents optionButton As Button
    Friend WithEvents Label1 As Label
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
End Class
