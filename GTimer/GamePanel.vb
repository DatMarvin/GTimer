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
    Public picErrLabel As Label

    '  Dim check As CheckBox
    ' Dim tempLabel As Label

    Public summaryBar As PictureBox
    Public summaryBarLabel As Label

    Dim picInvUsed As Boolean = False
    Dim summaryBarRatio As Double

    Public Shared siz As New Size(200, 300)
    Public Shared baseTop As Integer = 75
    Public Shared baseLeft As Integer = Form1.dateRangeGroup.Width + Form1.dateRangeGroup.Left
    Public Shared baseSideMargin As Integer = 30
    Public Shared gap As Integer = 50
    Dim checkUpperMargin = 20
    Dim picSideMargin = 10
    Dim picUpperMargin = 10
    Dim picLowerMargin = 40
    Dim groupedPicGap = 4

    Public Shared summaryBarBaseTop As Integer = 10
    Public Shared summaryBarHeight As Integer = 10
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
        Form1.Controls.Remove(picErrLabel)
    End Sub

    Sub init()

        If Not game.isInGroupPanel() Then
            group = New GroupBox()
            group.Font = New Font(Form1.globalFont.Name, 12, FontStyle.Regular)
            group.ForeColor = Color.White
            group.Size = siz
            Dim offset As Integer = 0
            If game.user.games.Count < Game.maxGameCount Then
                offset = ((siz.Width + gap) / 2) * (Game.maxGameCount - game.user.games.Count)
            End If
            group.Location = New Point(baseLeft + baseSideMargin + game.id * (siz.Width + gap) + offset, baseTop)
            Form1.Controls.Add(group)
        Else
            If game.user.groupedGroup Is Nothing Then
                game.user.groupedGroup = New GroupBox()
                game.user.groupedGroup.Font = New Font(Form1.globalFont.Name, 12, FontStyle.Regular)
                game.user.groupedGroup.ForeColor = Color.White
                game.user.groupedGroup.Size = siz
                game.user.groupedGroup.Location = New Point(baseLeft + baseSideMargin + game.id * (siz.Width + gap), baseTop)
                Form1.Controls.Add(game.user.groupedGroup)
            End If
            group = game.user.groupedGroup
        End If



        pic = New PictureBox()
        picErrLabel = New Label()
        setPicImage(game.logoPath)
        pic.BackgroundImageLayout = ImageLayout.Stretch
        If Not game.isInGroupPanel() Or game.user.getGroupedGamesCount() = 1 Then
            pic.Size = New Size(siz.Width - 2 * picSideMargin, siz.Width - 2 * picSideMargin)
            pic.Location = New Point(group.Left + picSideMargin, group.Top + picUpperMargin + checkUpperMargin)
        ElseIf game.getGroupPanelIndex() < 3 Or game.getGroupPanelIndex() = 3 And game.user.getGroupedGamesCount() = 4 Then
            Dim halfSize As Integer = (siz.Width - 2 * picSideMargin) / 2
            Dim halfSizeOffset As Integer = 0
            If game.user.getGroupedGamesCount() = 2 Then
                halfSizeOffset = (halfSize + groupedPicGap) / 2
            End If
            pic.Size = New Size(halfSize - groupedPicGap / 2, halfSize - groupedPicGap / 2)
            pic.Location = New Point(group.Left + picSideMargin + game.getGroupPanelIndex() * (halfSize + groupedPicGap) - Int(game.getGroupPanelIndex() / 2) * (halfSize + groupedPicGap) * 2,
                                     group.Top + picUpperMargin + checkUpperMargin + Int(game.getGroupPanelIndex() / 2) * (halfSize + groupedPicGap) + halfSizeOffset)
        Else
            pic.BackgroundImage = Nothing
            pic.Hide()
        End If
        pic.Cursor = Cursors.Hand
        AddHandler pic.MouseHover, AddressOf groupedGameHover
        AddHandler pic.Click, AddressOf picClicked
        Form1.Controls.Add(pic)
        pic.BringToFront()

        If game.isInGroupPanel() And game.user.groupedMoreGamesLabel Is Nothing Then
            game.user.groupedMoreGamesLabel = New Label()
            game.user.groupedMoreGamesLabel.Font = New Font(Form1.globalFont.Name, 20, FontStyle.Regular)
            Dim halfSize As Integer = (siz.Width - 2 * picSideMargin) / 2
            If game.user.getGroupedGamesCount() - 3 > 0 Then
                game.user.groupedMoreGamesLabel.Text = "+" & game.user.getGroupedGamesCount() - 3
            End If
            game.user.groupedMoreGamesLabel.AutoSize = True
            game.user.groupedMoreGamesLabel.Location = New Point(group.Left + picSideMargin + halfSize + groupedPicGap + halfSize / 2 - game.user.groupedMoreGamesLabel.Width / 4,
                                         group.Top + picUpperMargin + checkUpperMargin + halfSize + groupedPicGap + halfSize / 2 - game.user.groupedMoreGamesLabel.Height / 2)
            game.user.groupedMoreGamesLabel.ForeColor = Color.White
            game.user.groupedMoreGamesLabel.Cursor = Cursors.Hand
            AddHandler game.user.groupedMoreGamesLabel.MouseHover, AddressOf moreGamesHover
            Form1.Controls.Add(game.user.groupedMoreGamesLabel)
            game.user.groupedMoreGamesLabel.BringToFront()
        End If



        Dim fs As FontStyle = FontStyle.Regular
        If Not game.include Then fs = FontStyle.Strikeout
        picErrLabel.Font = New Font(Form1.globalFont.Name, 13, fs)
        picErrLabel.Location = New Point(pic.Left + pic.Width / 2 - picErrLabel.Width / 2, pic.Top + pic.Height / 2 - picErrLabel.Height / 2)
        picErrLabel.ForeColor = Color.White
        picErrLabel.AutoSize = True
        picErrLabel.Cursor = Cursors.Hand
        'picErrLabel.Width = siz.Width - 2 * picSideMargin
        AddHandler picErrLabel.MouseHover, AddressOf groupedGameHover
        AddHandler picErrLabel.Click, AddressOf picClicked
        Form1.Controls.Add(picErrLabel)
        picErrLabel.Hide()


        If Not game.isInGroupPanel() Then
            label = New Label()
            label.Font = New Font(Form1.globalFont.Name, 16, FontStyle.Regular)
            label.Location = New Point(group.Left + group.Width / 2 - label.Width / 2, group.Top + checkUpperMargin + picUpperMargin + picLowerMargin + pic.Height)
            label.ForeColor = Color.White
            label.AutoSize = True
            Form1.Controls.Add(label)
            label.BringToFront()
        Else
            If game.user.groupedLabel Is Nothing Then
                game.user.groupedLabel = New Label()
                game.user.groupedLabel.Font = New Font(Form1.globalFont.Name, 16, FontStyle.Regular)
                game.user.groupedLabel.Location = New Point(group.Left + group.Width / 2 - game.user.groupedLabel.Width / 2, group.Top + checkUpperMargin + picUpperMargin + picLowerMargin + pic.Height)
                game.user.groupedLabel.ForeColor = Color.White
                game.user.groupedLabel.AutoSize = True
                Form1.Controls.Add(game.user.groupedLabel)
                game.user.groupedLabel.BringToFront()
            End If
            label = game.user.groupedLabel
        End If


        ' tempLabel.Font = New Font("Georgia", 16, FontStyle.Regular)
        ' tempLabel.Location = New Point(group.Left + group.Width / 2 - tempLabel.Width / 2, group.Top + checkUpperMargin + check.Height + picUpperMargin + picLowerMargin + pic.Height + label.Height + 5)
        ' tempLabel.ForeColor = Color.White
        ' tempLabel.AutoSize = True
        '  tempLabel.BringToFront()
        '  Form1.Controls.Add(tempLabel)
        ' tempLabel.BringToFront()

        'summary
        If Not game.isInGroupPanel() Then
            summaryBar = New PictureBox()
            summaryBar.Size = New Size(10, summaryBarHeight)
            summaryBar.BackColor = Color.Gray
            summaryBar.Location = New Point(group.Left + summaryBarMargin, label.Bottom + summaryBarBaseTop)
            summaryBar.Cursor = Cursors.Hand
            AddHandler summaryBar.MouseHover, AddressOf summaryBarHover
            Form1.Controls.Add(summaryBar)
            summaryBar.BringToFront()
        Else
            If game.user.groupedSummaryBar Is Nothing Then
                game.user.groupedSummaryBar = New PictureBox()
                game.user.groupedSummaryBar.Size = New Size(10, summaryBarHeight)
                game.user.groupedSummaryBar.BackColor = Color.Gray
                game.user.groupedSummaryBar.Location = New Point(group.Left + summaryBarMargin, label.Bottom + summaryBarBaseTop)
                game.user.groupedSummaryBar.Cursor = Cursors.Hand
                AddHandler game.user.groupedSummaryBar.MouseHover, AddressOf summaryBarHover
                Form1.Controls.Add(game.user.groupedSummaryBar)
                game.user.groupedSummaryBar.BringToFront()
            End If
            summaryBar = game.user.groupedSummaryBar
        End If

        If Not game.isInGroupPanel() Then
            summaryBarLabel = New Label()
            summaryBarLabel.Font = New Font(Form1.globalFont.Name, 10, FontStyle.Regular)
            summaryBarLabel.Location = New Point(group.Left + summaryBarMargin + 10 + game.id * (gap + group.Width), label.Bottom + summaryBarBaseTop)
            summaryBarLabel.ForeColor = Color.White
            summaryBarLabel.AutoSize = True
            summaryBarLabel.Cursor = Cursors.Hand
            ' AddHandler summaryBarLabel.MouseHover, AddressOf summaryBarLabelHover
            '  Form1.Controls.Add(summaryBarLabel)
            summaryBarLabel.BringToFront()
        Else
            If game.user.groupedSummaryBarLabel Is Nothing Then
                game.user.groupedSummaryBarLabel = New Label()
                game.user.groupedSummaryBarLabel.Font = New Font(Form1.globalFont.Name, 10, FontStyle.Regular)
                game.user.groupedSummaryBarLabel.Location = New Point(group.Left + summaryBarMargin + 10 + game.id * (gap + group.Width), label.Bottom + summaryBarBaseTop)
                game.user.groupedSummaryBarLabel.ForeColor = Color.White
                game.user.groupedSummaryBarLabel.AutoSize = True
                game.user.groupedSummaryBarLabel.Cursor = Cursors.Hand
                ' AddHandler summaryBarLabel.MouseHover, AddressOf summaryBarLabelHover
                '  Form1.Controls.Add(summaryBarLabel)
                game.user.groupedSummaryBarLabel.BringToFront()
            End If
            summaryBarLabel = game.user.groupedSummaryBarLabel
        End If


        update(0, True)
    End Sub

    Public Function getPos() As String
        Return pic.Left & " " & pic.Top & " " & pic.Visible
    End Function

    Sub summaryBarHover()
        Form1.tt.Show(Math.Round(summaryBarRatio * 100, 1) & " %", summaryBar, summaryBar.Width + 3, 0, 1000)
    End Sub

    Sub groupedGameHover()
        If game.isInGroupPanel() Then
            Form1.tt.Show(game.name & ":   " & dll.SecondsTodhmsString(game.getTime(), "ZERRO"), pic, pic.Width + 3, 0, 2000)
        End If
    End Sub

    Sub moreGamesHover()
        Dim moreGamesString = ""
        For Each g In game.user.getGroupPanelGames()
            If g.getGroupPanelIndex() > 2 Then
                moreGamesString &= g.name & ":  " & dll.SecondsTodhmsString(g.getTime(), "ZERRO") & vbNewLine
            End If
        Next
        Form1.tt.Show(moreGamesString, game.user.groupedMoreGamesLabel, game.user.groupedMoreGamesLabel.Width + 3, 0, 2000)
    End Sub

    Sub update(Optional time As Long = 0, Optional initUpdate As Boolean = False)

        Dim panelLabel As Label = label
        If game.isInGroupPanel() Then panelLabel = game.user.groupedLabel
        Dim transformedTime As Integer = CInt(game.user.getGameTimeRatio(time))
        panelLabel.Text = dll.SecondsTodhmsString(transformedTime)

        panelLabel.Location = New Point(group.Left + group.Width / 2 - panelLabel.Width / 2, group.Top + picUpperMargin + picLowerMargin + siz.Width - 2 * picSideMargin)

        summaryBar.Visible = transformedTime > 0 And (game.include Or (game.isInGroupPanel And game.user.isOneGroupedGameIncluded()))
        summaryBarLabel.Visible = transformedTime > 0 And (game.include Or (game.isInGroupPanel And game.user.isOneGroupedGameIncluded()))

        updateSummary(game.user.getTotalTimeForAllGames())

        If Not Form1.patchNotesVisible Then summaryBarLabel.BringToFront()

        If game.include And game.active And Form1.dateRangeIncludeToday() And game.user.online Or
            (Not game.isPrioActiveGame() And game.isInGroupPanel() And game.user.activeGamePrioQueue.Count > 0 AndAlso
            game.user.activeGamePrioQueue(0).isInGroupPanel() AndAlso game.user.activeGamePrioQueue(0).active AndAlso game.user.activeGamePrioQueue(0).include) Then

            If game.isPrioActiveGame() Or game.user.activeGamePrioQueue(0).isInGroupPanel() Then
                panelLabel.ForeColor = Form1.getFontColor(Form1.LabelMode.RUNNING)
            Else
                panelLabel.ForeColor = Form1.getFontColor(Form1.LabelMode.RUNNING_BLOCKED)
            End If
        ElseIf Not game.include And game.active Then
            If game.isPrioActiveGame() Or game.user.activeGamePrioQueue(0).isInGroupPanel() Then
                panelLabel.ForeColor = Form1.getFontColor(Form1.LabelMode.INACTIVE_RUNNING)
            Else
                panelLabel.ForeColor = Form1.getFontColor(Form1.LabelMode.INACTIVE_RUNNING_BLOCKED)
            End If
        ElseIf Not game.include And Not game.active And Not game.isInGroupPanel() Then
            panelLabel.ForeColor = Form1.getFontColor(Form1.LabelMode.INACTIVE)
        Else
            panelLabel.ForeColor = Form1.getFontColor(Form1.LabelMode.NORMAL)
        End If


        If game.include And (picInvUsed Or initUpdate) Then
            picInvUsed = False
            setPicImage(game.logoPath)

        ElseIf Not game.include And (Not picInvUsed Or initUpdate) Then
            picInvUsed = True
            setPicImage(game.logoInvPath)
        Else
            Dim fs As FontStyle = FontStyle.Regular
            If Not game.include Then fs = FontStyle.Strikeout
            picErrLabel.Font = New Font(picErrLabel.Font, fs)
        End If

    End Sub



    Sub setPicImage(logoPath As String)
        Dim basePath As String = game.user.sharedResPath
        Dim path As String = logoPath
        If Not path.Contains(":\") Then path = basePath & logoPath
        If game.user.isMe() OrElse Not IO.File.Exists(path) Then
            path = Form1.resPath & logoPath
        End If
        If IO.File.Exists(path) Then
            pic.BackgroundImage = Image.FromFile(path)
            picErrLabel.Text = ""
            picErrLabel.Hide()
        Else
            pic.BackgroundImage = Nothing
            Dim fs As FontStyle = FontStyle.Regular
            If Not game.include Then fs = FontStyle.Strikeout
            picErrLabel.Font = New Font(picErrLabel.Font, fs)
            If game.getGroupPanelIndex() <= 2 Or game.getGroupPanelIndex() = 3 And game.user.getGroupedGamesCount() = 4 Then
                picErrLabel.Show()
                picErrLabel.BringToFront()
            End If
            picErrLabel.Text = game.ToString()

            picErrLabel.Location = New Point(pic.Left + pic.Width / 2 - picErrLabel.Width / 2, pic.Top + pic.Height / 2 - picErrLabel.Height / 2)

        End If

    End Sub
    Sub checkClicked(sender As System.Object, e As EventArgs)

    End Sub

    Sub picClicked(sender As System.Object, e As EventArgs)
        game.include = Not game.include
        If game.user.isMe() Then dll.iniWriteValue(game.section, "include", Math.Abs(CInt(game.include)))
        Form1.updateLabels(False)
    End Sub

    Sub updateSummary(totalTime As Long)

        If totalTime = 0 Or Not game.include Then Return

        summaryBar.Top = label.Bottom + summaryBarBaseTop

        Dim currBarTime As Long = game.getTime()
        If game.isInGroupPanel() Then
            currBarTime = game.user.getGroupedGamesTime(False)
        End If
        Dim ratio As Double = (currBarTime / totalTime)
        Dim summaryBarTotalWidth As Integer = (siz.Width - summaryBarMargin * 2)
        summaryBar.Width = summaryBarTotalWidth * ratio + 3
        Dim red As Integer = 255 - ratio * 255
        If red < 0 Or red > 255 Then red = 0
        Dim green As Integer = ratio * 255
        If green < 0 Or green > 255 Then green = 0
        summaryBar.BackColor = Color.FromArgb(50, 50, 50) ' Color.FromArgb(red, green, 0)

        summaryBarRatio = ratio
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
