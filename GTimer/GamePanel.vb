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
    Dim picErrLabel As Label
    '  Dim check As CheckBox
    ' Dim tempLabel As Label

    Public summaryBar As PictureBox
    Public summaryBarLabel As Label

    Dim picInvUsed As Boolean = False

    Public Shared siz As New Size(200, 310)
    Public Shared baseTop As Integer = 75
    Public Shared baseLeft As Integer = Form1.dateRangeGroup.Width + Form1.dateRangeGroup.Left
    Public Shared baseSideMargin As Integer = 50
    Public Shared gap As Integer = 50
    Dim checkUpperMargin = 20
    Dim picSideMargin = 10
    Dim picUpperMargin = 10
    Dim picLowerMargin = 40

    Public Shared summaryBarBaseTop As Integer = 20
    Public Shared summaryBarHeight As Integer = 20
    Public Shared summaryBarGap As Integer = 10
    Public Shared summaryBarMargin As Integer = 15


    Public Sub New(game As Game)
        Me.game = game
    End Sub

    Sub destroy()
        Form1.Controls.Remove(label)
        Form1.Controls.Remove(group)
        Form1.Controls.Remove(summaryBar)
        Form1.Controls.Remove(summaryBarLabel)
        Form1.Controls.Remove(pic)
    End Sub

    Sub init()
        group = New GroupBox()
        label = New Label()
        '  tempLabel = New Label()
        pic = New PictureBox()
        picErrLabel = New Label()
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

        picErrLabel.Font = New Font(Form1.globalFont.Name, 13, FontStyle.Regular)
        picErrLabel.Location = New Point(pic.Left + pic.Width / 2 - picErrLabel.Width / 2, pic.Top + pic.Height / 2 - picErrLabel.Height / 2)
        picErrLabel.ForeColor = Color.White
        picErrLabel.AutoSize = True
        'picErrLabel.Width = siz.Width - 2 * picSideMargin
        Form1.Controls.Add(picErrLabel)
        picErrLabel.Hide()


        label.Font = New Font(Form1.globalFont.Name, 16, FontStyle.Regular)
        label.Location = New Point(group.Left + group.Width / 2 - label.Width / 2, group.Top + checkUpperMargin + picUpperMargin + picLowerMargin + pic.Height)
        label.ForeColor = Color.White
        label.AutoSize = True
        AddHandler label.Click, AddressOf labelClick
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
        summaryBar.Name = "summaryBar-" & game.name & "-" & game.user.name
        summaryBar.Size = New Size(10, summaryBarHeight)
        AddHandler summaryBar.Click, AddressOf rankClick
        summaryBar.BackColor = Color.Gray
        summaryBar.Location = New Point(group.Left + summaryBarMargin, label.Bottom + summaryBarBaseTop)
        Form1.Controls.Add(summaryBar)
        summaryBar.BringToFront()

        summaryBarLabel.Font = New Font(Form1.globalFont.Name, 10, FontStyle.Regular)
        summaryBarLabel.Location = New Point(group.Left + summaryBarMargin + 10 + game.id * (gap + group.Width), label.Bottom + summaryBarBaseTop)
        summaryBarLabel.ForeColor = Color.White
        summaryBarLabel.AutoSize = True
        Form1.Controls.Add(summaryBarLabel)
        summaryBarLabel.BringToFront()

        update(0, True)
    End Sub

    Sub rankClick()
        MsgBox(summaryBar.Name)
    End Sub
    Sub labelClick()
        MsgBox(label.Name)
    End Sub
    Sub update(Optional time As Long = 0, Optional initUpdate As Boolean = False)

        Dim transformedTime As Integer = CInt(game.user.getGameTimeRatio(time))
        label.Text = dll.SecondsTodhmsString(transformedTime)

        label.Location = New Point(group.Left + group.Width / 2 - label.Width / 2, group.Top + picUpperMargin + picLowerMargin + pic.Height)

        summaryBar.Visible = transformedTime > 0 And game.include
        summaryBarLabel.Visible = transformedTime > 0 And game.include

        updateSummary(game.user.getTotalTimeForAllGames())

        If Not Form1.patchNotesVisible Then summaryBarLabel.BringToFront()

        If game.include And game.active And Form1.dateRangeIncludeToday() Then
            If game.isPrioActiveGame() Then
                label.ForeColor = Form1.getFontColor(Form1.LabelMode.RUNNING)
            Else
                label.ForeColor = Form1.getFontColor(Form1.LabelMode.RUNNING_BLOCKED)
            End If
        ElseIf Not game.include And game.active Then
            If game.isPrioActiveGame() Then
                label.ForeColor = Form1.getFontColor(Form1.LabelMode.INACTIVE_RUNNING)
            Else
                label.ForeColor = Form1.getFontColor(Form1.LabelMode.INACTIVE_RUNNING_BLOCKED)
            End If
        ElseIf Not game.include And Not game.active Then
            label.ForeColor = Form1.getFontColor(Form1.LabelMode.INACTIVE)
        Else
            label.ForeColor = Form1.getFontColor(Form1.LabelMode.NORMAL)
        End If

        If game.include And (picInvUsed Or initUpdate) Then
            picInvUsed = False
            setPicImage(Form1.resPath & game.logoPath)
        ElseIf Not game.include And (Not picInvUsed Or initUpdate) Then
            picInvUsed = True
            setPicImage(Form1.resPath & game.logoInvPath)
        End If

    End Sub



    Sub setPicImage(path As String)
        If IO.File.Exists(path) Then
            pic.BackgroundImage = Image.FromFile(path)
            picErrLabel.Text = ""
            picErrLabel.Hide()
        Else
            pic.BackgroundImage = Nothing
            picErrLabel.Show()
            picErrLabel.BringToFront()
            picErrLabel.Text = game.name
            picErrLabel.Location = New Point(pic.Left + pic.Width / 2 - picErrLabel.Width / 2, pic.Top + pic.Height / 2 - picErrLabel.Height / 2)
        End If
    End Sub
    Sub checkClicked(sender As System.Object, e As EventArgs)

    End Sub

    Sub picClicked(sender As System.Object, e As EventArgs)
        game.include = Not game.include
        If game.user.isMe() Then dll.iniWriteValue(game.section, "include", Math.Abs(CInt(game.include)))
        Form1.updateLabels(False)
        Form1.updateSummary()
    End Sub

    Sub updateSummary(totalTime As Long)

        If totalTime = 0 Or Not game.include Then Return

        summaryBar.Top = label.Bottom + summaryBarBaseTop
        Dim ratio As Double = (game.getTime() / totalTime)
        Dim summaryBarTotalWidth As Integer = (siz.Width - summaryBarMargin * 2)
        summaryBar.Width = summaryBarTotalWidth * ratio + 3
        Dim red As Integer = 255 - ratio * 255
        If red < 0 Or red > 255 Then red = 0
        Dim green As Integer = ratio * 255
        If green < 0 Or green > 255 Then green = 0
        summaryBar.BackColor = Color.FromArgb(red, green, 0)

        summaryBarLabel.Text = Math.Round(ratio * 100, 1) & " %"
        Dim labelInsideBar As Boolean = summaryBar.Width > summaryBarLabel.Width + 5

        summaryBarLabel.Left = IIf(labelInsideBar, summaryBar.Left + summaryBar.Width - summaryBarLabel.Width - 5, summaryBar.Left + summaryBar.Width + 5)
        summaryBarLabel.Top = label.Bottom + summaryBarBaseTop + summaryBarHeight / 2 - summaryBarLabel.Height / 2
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

    End Sub
End Class
