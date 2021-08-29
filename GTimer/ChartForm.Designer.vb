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
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.diagramButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.chart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chart
        '
        ChartArea1.Name = "ChartArea1"
        Me.chart.ChartAreas.Add(ChartArea1)
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
        Me.radNewTime.Location = New System.Drawing.Point(88, 160)
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
        Me.radCumTime.Location = New System.Drawing.Point(88, 183)
        Me.radCumTime.Name = "radCumTime"
        Me.radCumTime.Size = New System.Drawing.Size(77, 17)
        Me.radCumTime.TabIndex = 23
        Me.radCumTime.Text = "Cumulative"
        Me.radCumTime.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Marv", "Chris", "Lui"})
        Me.ComboBox1.Location = New System.Drawing.Point(86, 116)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 25
        Me.ComboBox1.Text = "Marv"
        '
        'diagramButton
        '
        Me.diagramButton.BackgroundImage = Global.GTimer.My.Resources.Resources.diagram
        Me.diagramButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.diagramButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.diagramButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.diagramButton.Location = New System.Drawing.Point(119, 12)
        Me.diagramButton.Name = "diagramButton"
        Me.diagramButton.Size = New System.Drawing.Size(46, 42)
        Me.diagramButton.TabIndex = 26
        Me.diagramButton.TabStop = False
        Me.diagramButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(116, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Draw Chart"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(19, 121)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "User:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(19, 172)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Plot Mode:"
        '
        'ChartForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1327, 574)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.diagramButton)
        Me.Controls.Add(Me.ComboBox1)
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
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents diagramButton As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
