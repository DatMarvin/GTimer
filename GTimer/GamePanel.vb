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
    '  Dim check As CheckBox
    ' Dim tempLabel As Label

    Public summaryBar As PictureBox
    Public summaryBarLabel As Label

    Dim picInvUsed As Boolean = False

    Public Shared siz As New Size(200, 300)
    Public Shared baseTop As Integer = 75
    Public Shared baseLeft As Integer = Form1.dateRangeGroup.Width + Form1.dateRangeGroup.Left
    Public Shared baseSideMargin As Integer = 50
    Public Shared gap As Integer = 50
    Dim checkUpperMargin = 20
    Dim picSideMargin = 10
    Dim picUpperMargin = 20
    Dim picLowerMargin = 40

    Public Shared summaryBarBaseTop As Integer = 90
    Public Shared summaryBarHeight As Integer = 25
    Public Shared summaryBarGap As Integer = 10
    Public Shared summaryBarMargin As Integer = 30
    Public Shared summaryBarLabelBaseLeft As Integer = Form1.statsGroup.Left + summaryBarMargin + 10

    Public Sub New(game As Game)
        Me.game = game
    End Sub

    Sub init()
        group = New GroupBox()
        label = New Label()
        '  tempLabel = New Label()
        pic = New PictureBox()
        ' check = New CheckBox()
        summaryBar = New PictureBox()
        summaryBarLabel = New Label()

        group.Font = New Font(Form1.globalFont.Name, 12, FontStyle.Regular)
        group.ForeColor = Color.White
        group.Size = siz
        group.Location = New Point(baseLeft + baseSideMargin + game.id * (siz.Width + gap), baseTop)
        Form1.Controls.Add(group)

        'check.Location = New Point(group.Left + picSideMargin, group.Top + checkUpperMargin)
        'check.Text = ""
        'check.AutoSize = True
        'check.Checked = game.include
        'AddHandler check.Click, AddressOf checkClicked
        'Form1.Controls.Add(check)
        'check.BringToFront()

        setPicImage(Form1.resPath & game.logoPath)
        pic.BackgroundImageLayout = ImageLayout.Stretch
        pic.Size = New Size(siz.Width - 2 * picSideMargin, siz.Width - 2 * picSideMargin)
        pic.Location = New Point(group.Left + picSideMargin, group.Top + picUpperMargin + checkUpperMargin)
        pic.Cursor = Cursors.Hand
        AddHandler pic.Click, AddressOf picClicked
        Form1.Controls.Add(pic)
        pic.BringToFront()

        label.Font = New Font(Form1.globalFont.Name, 16, FontStyle.Regular)
        label.Location = New Point(group.Left + group.Width / 2 - label.Width / 2, group.Top + checkUpperMargin + picUpperMargin + picLowerMargin + pic.Height)
        label.ForeColor = Color.White
        label.AutoSize = True
        Form1.Controls.Add(label)
        label.BringToFront()

        ' tempLabel.Font = New Font("Georgia", 16, FontStyle.Regular)
        ' tempLabel.Location = New Point(group.Left + group.Width / 2 - tempLabel.Width / 2, group.Top + checkUpperMargin + check.Height + picUpperMargin + picLowerMargin + pic.Height + label.Height + 5)
        ' tempLabel.ForeColor = Color.White
        ' tempLabel.AutoSize = True
        '  tempLabel.BringToFront()
        '  Form1.Controls.Add(tempLabel)
        ' tempLabel.BringToFront()

        'summary
        summaryBar.Size = New Size(10, summaryBarHeight)
        summaryBar.BackColor = Color.Gray
        summaryBar.Location = New Point(Form1.statsGroup.Left + summaryBarMargin, Form1.statsGroup.Top + summaryBarBaseTop)
        Form1.Controls.Add(summaryBar)
        summaryBar.BringToFront()

        summaryBarLabel.Font = New Font(Form1.globalFont.Name, 12, FontStyle.Regular)
        summaryBarLabel.Location = New Point(summaryBarLabelBaseLeft, Form1.statsGroup.Top + summaryBarBaseTop)
        summaryBarLabel.ForeColor = Color.White
        summaryBarLabel.AutoSize = True
        Form1.Controls.Add(summaryBarLabel)
        summaryBarLabel.BringToFront()


        update()
    End Sub

    Sub update(Optional time As Long = 0)

        label.Text = dll.SecondsTodhmsString(CInt([Game].getTimeRatio(time)))

        label.Location = New Point(group.Left + group.Width / 2 - label.Width / 2, group.Top + picUpperMargin + picLowerMargin + pic.Height)

        If game.include And game.active And User.isMeSelected() Then
            If game.isPrioActiveGame() Then
                label.ForeColor = Form1.getFontColor(Form1.LabelMode.RUNNING)
            Else
                label.ForeColor = Form1.getFontColor(Form1.LabelMode.RUNNING_BLOCKED)
            End If
        ElseIf Not game.include And game.active And User.isMeSelected() Then
            If game.isPrioActiveGame() Then
                label.ForeColor = Form1.getFontColor(Form1.LabelMode.INACTIVE_RUNNING)
            Else
                label.ForeColor = Form1.getFontColor(Form1.LabelMode.INACTIVE_RUNNING_BLOCKED)
            End If
        ElseIf Not game.include And Not game.active And User.isMeSelected() Then
            label.ForeColor = Form1.getFontColor(Form1.LabelMode.INACTIVE)
        Else
            label.ForeColor = Form1.getFontColor(Form1.LabelMode.NORMAL)
        End If

        If game.include And picInvUsed Then
            picInvUsed = False
            If IO.File.Exists(Form1.resPath & game.logoPath) Then
                setPicImage(Form1.resPath & game.logoPath)
            End If
        ElseIf Not game.include And Not picInvUsed Then
            If Not picInvUsed Then
                picInvUsed = True
                setPicImage(Form1.resPath & game.logoInvPath)
            End If
        End If

    End Sub



    Sub setPicImage(path As String)
        If IO.File.Exists(path) Then
            pic.BackgroundImage = Image.FromFile(path)
        End If
    End Sub
    Sub checkClicked(sender As System.Object, e As EventArgs)

    End Sub

    Sub picClicked(sender As System.Object, e As EventArgs)
        game.include = Not game.include
        dll.iniWriteValue(game.section, "include", Math.Abs(CInt(game.include)))
        Form1.updateLabels(False)
        Form1.updateSummary()
    End Sub

    Sub updateSummary(index As Integer, totalTime As Long)

        summaryBar.Visible = game.include And totalTime > 0
        summaryBarLabel.Visible = game.include And totalTime > 0

        If totalTime = 0 Or Not game.include Then Return

        summaryBar.Top = Form1.statsGroup.Top + summaryBarBaseTop + index * (summaryBarHeight + summaryBarGap)
        Dim ratio As Double = (game.getTime(User.isMeSelected()) / totalTime)
        Dim summaryBarTotalWidth As Integer = (siz.Width + gap) * IIf(Form1.games.Count > 3, Form1.games.Count, 3) - gap - 2 * summaryBarMargin
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
        If Not Form1.patchNotesVisible Then summaryBarLabel.BringToFront()
    End Sub
End Class
