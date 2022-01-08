<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChartForm
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChartForm))
        Me.chart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.radNewTime = New System.Windows.Forms.RadioButton()
        Me.radCumTime = New System.Windows.Forms.RadioButton()
        Me.userCombo = New System.Windows.Forms.ComboBox()
        Me.diagramButton = New System.Windows.Forms.Button()
        Me.chartButtonLabel = New System.Windows.Forms.Label()
        Me.userLabel = New System.Windows.Forms.Label()
        Me.plotModeLabel = New System.Windows.Forms.Label()
        Me.gameList = New System.Windows.Forms.CheckedListBox()
        Me.gameSelectCheck = New System.Windows.Forms.CheckBox()
        Me.gamesLabel = New System.Windows.Forms.Label()
        CType(Me.chart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chart
        '
        ChartArea1.Name = "ChartArea1"
        Me.chart.ChartAreas.Add(ChartArea1)
        Me.chart.Cursor = System.Windows.Forms.Cursors.Cross
        Legend1.Name = "Legend1"
        Me.chart.Legends.Add(Legend1)
        Me.chart.Location = New System.Drawing.Point(327, 12)
        Me.chart.Name = "chart"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.chart.Series.Add(Series1)
        Me.chart.Size = New System.Drawing.Size(988, 550)
        Me.chart.TabIndex = 20
        Me.chart.Text = "Chart1"
        '
        'radNewTime
        '
        Me.radNewTime.AutoSize = True
        Me.radNewTime.Checked = True
        Me.radNewTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.radNewTime.ForeColor = System.Drawing.Color.White
        Me.radNewTime.Location = New System.Drawing.Point(88, 327)
        Me.radNewTime.Name = "radNewTime"
        Me.radNewTime.Size = New System.Drawing.Size(47, 17)
        Me.radNewTime.TabIndex = 22
        Me.radNewTime.TabStop = True
        Me.radNewTime.Text = "New"
        Me.radNewTime.UseVisualStyleBackColor = True
        '
        'radCumTime
        '
        Me.radCumTime.AutoSize = True
        Me.radCumTime.ForeColor = System.Drawing.Color.White
        Me.radCumTime.Location = New System.Drawing.Point(88, 350)
        Me.radCumTime.Name = "radCumTime"
        Me.radCumTime.Size = New System.Drawing.Size(77, 17)
        Me.radCumTime.TabIndex = 23
        Me.radCumTime.Text = "Cumulative"
        Me.radCumTime.UseVisualStyleBackColor = True
        '
        'userCombo
        '
        Me.userCombo.BackColor = System.Drawing.Color.Black
        Me.userCombo.ForeColor = System.Drawing.Color.White
        Me.userCombo.FormattingEnabled = True
        Me.userCombo.Location = New System.Drawing.Point(86, 116)
        Me.userCombo.Name = "userCombo"
        Me.userCombo.Size = New System.Drawing.Size(121, 21)
        Me.userCombo.TabIndex = 25
        '
        'diagramButton
        '
        Me.diagramButton.BackgroundImage = Global.GTimer.My.Resources.Resources.diagram
        Me.diagramButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.diagramButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.diagramButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.diagramButton.Location = New System.Drawing.Point(119, 53)
        Me.diagramButton.Name = "diagramButton"
        Me.diagramButton.Size = New System.Drawing.Size(46, 42)
        Me.diagramButton.TabIndex = 26
        Me.diagramButton.TabStop = False
        Me.diagramButton.UseVisualStyleBackColor = True
        '
        'chartButtonLabel
        '
        Me.chartButtonLabel.AutoSize = True
        Me.chartButtonLabel.ForeColor = System.Drawing.Color.White
        Me.chartButtonLabel.Location = New System.Drawing.Point(2, 68)
        Me.chartButtonLabel.Name = "chartButtonLabel"
        Me.chartButtonLabel.Size = New System.Drawing.Size(75, 13)
        Me.chartButtonLabel.TabIndex = 27
        Me.chartButtonLabel.Text = "Refresh Chart:"
        '
        'userLabel
        '
        Me.userLabel.AutoSize = True
        Me.userLabel.ForeColor = System.Drawing.Color.White
        Me.userLabel.Location = New System.Drawing.Point(45, 119)
        Me.userLabel.Name = "userLabel"
        Me.userLabel.Size = New System.Drawing.Size(32, 13)
        Me.userLabel.TabIndex = 28
        Me.userLabel.Text = "User:"
        '
        'plotModeLabel
        '
        Me.plotModeLabel.AutoSize = True
        Me.plotModeLabel.ForeColor = System.Drawing.Color.White
        Me.plotModeLabel.Location = New System.Drawing.Point(19, 339)
        Me.plotModeLabel.Name = "plotModeLabel"
        Me.plotModeLabel.Size = New System.Drawing.Size(58, 13)
        Me.plotModeLabel.TabIndex = 29
        Me.plotModeLabel.Text = "Plot Mode:"
        '
        'gameList
        '
        Me.gameList.BackColor = System.Drawing.Color.Black
        Me.gameList.CheckOnClick = True
        Me.gameList.ForeColor = System.Drawing.Color.White
        Me.gameList.FormattingEnabled = True
        Me.gameList.Location = New System.Drawing.Point(86, 178)
        Me.gameList.Name = "gameList"
        Me.gameList.Size = New System.Drawing.Size(120, 139)
        Me.gameList.TabIndex = 31
        '
        'gameSelectCheck
        '
        Me.gameSelectCheck.AutoSize = True
        Me.gameSelectCheck.ForeColor = System.Drawing.Color.White
        Me.gameSelectCheck.Location = New System.Drawing.Point(89, 159)
        Me.gameSelectCheck.Name = "gameSelectCheck"
        Me.gameSelectCheck.Size = New System.Drawing.Size(70, 17)
        Me.gameSelectCheck.TabIndex = 32
        Me.gameSelectCheck.Text = "Select All"
        Me.gameSelectCheck.UseVisualStyleBackColor = True
        '
        'gamesLabel
        '
        Me.gamesLabel.AutoSize = True
        Me.gamesLabel.ForeColor = System.Drawing.Color.White
        Me.gamesLabel.Location = New System.Drawing.Point(34, 180)
        Me.gamesLabel.Name = "gamesLabel"
        Me.gamesLabel.Size = New System.Drawing.Size(43, 13)
        Me.gamesLabel.TabIndex = 33
        Me.gamesLabel.Text = "Games:"
        '
        'ChartForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1327, 574)
        Me.Controls.Add(Me.gamesLabel)
        Me.Controls.Add(Me.gameSelectCheck)
        Me.Controls.Add(Me.gameList)
        Me.Controls.Add(Me.plotModeLabel)
        Me.Controls.Add(Me.userLabel)
        Me.Controls.Add(Me.chartButtonLabel)
        Me.Controls.Add(Me.diagramButton)
        Me.Controls.Add(Me.userCombo)
        Me.Controls.Add(Me.radCumTime)
        Me.Controls.Add(Me.radNewTime)
        Me.Controls.Add(Me.chart)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ChartForm"
        Me.Text = "GTimer Chart (Beta)"
        CType(Me.chart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chart As DataVisualization.Charting.Chart
    Friend WithEvents radNewTime As RadioButton
    Friend WithEvents radCumTime As RadioButton
    Friend WithEvents userCombo As ComboBox
    Friend WithEvents diagramButton As Button
    Friend WithEvents chartButtonLabel As Label
    Friend WithEvents userLabel As Label
    Friend WithEvents plotModeLabel As Label
    Friend WithEvents gameList As CheckedListBox
    Friend WithEvents gameSelectCheck As CheckBox
    Friend WithEvents gamesLabel As Label
End Class
