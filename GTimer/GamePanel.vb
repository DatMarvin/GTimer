Public Class GamePanel

    ReadOnly Property dll() As Utils
        Get
            Return Form1.dll
        End Get
    End Property

    Dim game As Game

    Dim group As GroupBox
    Dim label As Label
    Dim pic As PictureBox
    Dim check As CheckBox

    Public summaryBar As PictureBox
    Public summaryBarLabel As Label

    Public Shared siz As New Size(200, 300)


    Public Shared baseTop As Integer = 50
    Public Shared baseLeft As Integer = Form1.settingsGroup.Width + Form1.settingsGroup.Left
    Public Shared baseSideMargin As Integer = 50
    Public Shared gap As Integer = 50
    Dim checkUpperMargin = 20
    Dim picSideMargin = 10
    Dim picUpperMargin = 20
    Dim picLowerMargin = 20

    Public Shared summaryBarBaseTop As Integer = 90
    Public Shared summaryBarHeight As Integer = 25
    Public Shared summaryBarGap As Integer = 10
    Public Shared summaryBarMargin As Integer = 30
    Public Shared summaryBarTotalWidth As Integer = (siz.Width + gap) * 3 - gap - 2 * summaryBarMargin
    Public Shared summaryBarLabelBaseLeft As Integer = Form1.statsGroup.Left + summaryBarMargin + 10

    Public Sub New(game As Game)
        Me.game = game
    End Sub

    Sub init()
        group = New GroupBox()
        label = New Label()
        pic = New PictureBox()
        check = New CheckBox()
        summaryBar = New PictureBox()
        summaryBarLabel = New Label()

        group.Font = New Font("Georgia", 12, FontStyle.Regular)
        group.ForeColor = Color.White
        group.Text = game.name
        group.Size = siz
        group.Location = New Point(baseLeft + baseSideMargin + game.id * (siz.Width + gap), baseTop)
        Form1.Controls.Add(group)

        check.Location = New Point(group.Left + picSideMargin, group.Top + checkUpperMargin)
        check.Text = ""
        check.AutoSize = True
        check.Checked = game.include
        AddHandler check.Click, AddressOf checkClicked
        Form1.Controls.Add(check)
        check.BringToFront()

        pic.BackgroundImage = Image.FromFile(Form1.basePath & game.logoPath)
        pic.BackgroundImageLayout = ImageLayout.Stretch
        pic.Size = New Size(siz.Width - 2 * picSideMargin, siz.Width - 2 * picSideMargin)
        pic.Location = New Point(group.Left + picSideMargin, group.Top + picUpperMargin + checkUpperMargin)
        Form1.Controls.Add(pic)
        pic.BringToFront()

        label.Font = New Font("Georgia", 16, FontStyle.Regular)
        label.Location = New Point(group.Left + group.Width / 2 - label.Width / 2, group.Top + checkUpperMargin + check.Height + picUpperMargin + picLowerMargin + pic.Height)
        label.ForeColor = Color.White
        label.AutoSize = True
        label.BringToFront()
        Form1.Controls.Add(label)
        label.BringToFront()

        'summary
        summaryBar.Size = New Size(10, summaryBarHeight)
        summaryBar.BackColor = Color.Gray
        summaryBar.Location = New Point(Form1.statsGroup.Left + summaryBarMargin, Form1.statsGroup.Top + summaryBarBaseTop)
        Form1.Controls.Add(summaryBar)
        summaryBar.BringToFront()

        summaryBarLabel.Font = New Font("Georgia", 12, FontStyle.Regular)
        summaryBarLabel.Location = New Point(summaryBarLabelBaseLeft, Form1.statsGroup.Top + summaryBarBaseTop)
        summaryBarLabel.ForeColor = Color.White
        summaryBarLabel.AutoSize = True
        Form1.Controls.Add(summaryBarLabel)
        summaryBarLabel.BringToFront()


        update()
    End Sub

    Sub update(Optional time As Long = 0)
        label.Text = dll.SecondsTodhmsString(time)
        label.Location = New Point(group.Left + group.Width / 2 - label.Width / 2, group.Top + picUpperMargin + picLowerMargin + pic.Height)

        If game.active Then
            label.ForeColor = Color.Green
        Else
            label.ForeColor = Color.White
        End If
    End Sub

    Sub checkClicked(sender As System.Object, e As EventArgs)
        game.include = check.Checked
        dll.iniWriteValue(game.section, "include", Math.Abs(CInt(check.Checked)))
        Form1.updateSummary()
    End Sub

    Sub updateSummary(index As Integer, totalTime As Long)

        summaryBar.Visible = game.include And totalTime > 0
        summaryBarLabel.Visible = game.include And totalTime > 0

        If totalTime = 0 Or Not game.include Then Return

        summaryBar.Top = Form1.statsGroup.Top + summaryBarBaseTop + index * (summaryBarHeight + summaryBarGap)
        Dim ratio As Double = (game.getTime(False) / totalTime)
        summaryBar.Width = summaryBarTotalWidth * ratio + 3
        summaryBar.BackColor = Color.FromArgb(255 - ratio * 255, ratio * 255, 0)

        summaryBarLabel.Text = Math.Round(ratio * 100, 1) & " % - " & game.name
        Dim labelInsideBar As Boolean = summaryBar.Width > summaryBarLabel.Width + 5

        summaryBarLabel.Left = IIf(labelInsideBar, summaryBar.Left + summaryBar.Width - summaryBarLabel.Width - 5, summaryBar.Left + summaryBar.Width + 5)
        summaryBarLabel.Top = Form1.statsGroup.Top + summaryBarBaseTop + index * (summaryBarHeight + summaryBarGap) + summaryBarHeight / 2 - summaryBarLabel.Height / 2
        If labelInsideBar Then
            summaryBarLabel.BackColor = summaryBar.BackColor
            If ratio >= 0.8 Then
                summaryBarLabel.ForeColor = Color.Black
            Else
                summaryBarLabel.ForeColor = Color.White
            End If
        Else
            summaryBarLabel.BackColor = Color.Black
        End If
        summaryBarLabel.BringToFront()
    End Sub
End Class
