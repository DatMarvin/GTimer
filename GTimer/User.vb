Imports System.IO
Public Class User

    Public Shared count As Integer
    Public Const MAX_USERS = 4
    Public Const DEFAULT_NAME = "You"
    Public Const AFK_THRESHOLD = 300

    Public id As Integer
    Public name As String
    Public selected As Boolean

    Public backPanel As PictureBox
    Public nameLabel As Label
    Public statePic As PictureBox
    Public activeGameLabel As Label
    Public menuPic As PictureBox
    Public Shared addUser As PictureBox

    Public rankingTimeLabel As Label
    Public rankingBar As PictureBox
    Public rankingBarLabel As Label
    Public rankingBarNameLabel As Label

    Dim mouseHover As Boolean
    Public online As Boolean
    Public activeGame As String
    Public lastTempTime As Date

    Public Shared conUser As User

    Public groupedGroup As GroupBox
    Public groupedLabel As Label
    Public groupedSummaryBar As PictureBox
    Public groupedSummaryBarLabel As Label
    Public groupedMoreGamesLabel As Label

    Public games As New List(Of Game)
    Public activeGamePrioQueue As New List(Of Game)
    Public firstLogEntry As Date = Nothing
    Public ReadOnly Property getFirstLogEntry() As Date
        Get
            If firstLogEntry = Nothing Then
                Return getFirstLogEntryDate()
            Else
                Return firstLogEntry
            End If
        End Get
    End Property

    ReadOnly Property dll() As Utils
        Get
            Return Form1.dll
        End Get
    End Property
    Public ReadOnly Property iniPath() As String
        Get
            If isMe() Then
                Return Form1.iniPath
            Else
                Return Form1.sharedStatsPath & name & "\gtimer.ini"
            End If
        End Get
    End Property
    Public ReadOnly Property sharedResPath() As String
        Get
            Return Form1.sharedStatsPath & name & "\res\"
        End Get
    End Property

    Dim rankingAllUserTotalAlltimeRatioAverage As Integer
    Dim rankingTimeratio As Integer

    Public Sub New(id As Integer, name As String, selected As Boolean)
        Me.id = id
        Me.name = name
        Me.selected = selected
        loadGames()
        initPanel()
    End Sub

    Sub destroy()
        Form1.Controls.Remove(backPanel)
        Form1.Controls.Remove(nameLabel)
        Form1.Controls.Remove(statePic)
        Form1.Controls.Remove(activeGameLabel)
        Form1.Controls.Remove(menuPic)
        Form1.Controls.Remove(addUser)

        Form1.Controls.Remove(rankingBar)
        Form1.Controls.Remove(rankingBarLabel)
        Form1.Controls.Remove(rankingBarNameLabel)
        Form1.Controls.Remove(rankingTimeLabel)

        destroyGroupedControls()

        addUser = Nothing
    End Sub

    Sub destroyGroupedControls()
        Form1.Controls.Remove(groupedSummaryBar)
        Form1.Controls.Remove(groupedSummaryBarLabel)
        Form1.Controls.Remove(groupedLabel)
        Form1.Controls.Remove(groupedGroup)
        Form1.Controls.Remove(groupedMoreGamesLabel)
        groupedGroup = Nothing
        groupedLabel = Nothing
        groupedSummaryBar = Nothing
        groupedSummaryBarLabel = Nothing
        groupedMoreGamesLabel = Nothing
    End Sub

    Sub loadGames()
        games = New List(Of Game)
        Dim secs As List(Of String) = dll.iniGetAllSectionsList(iniPath)
        If secs Is Nothing Then
            Dim backoffAction As New Form1.IniBackoff(New Action(AddressOf loadGames))
        Else
            secs.Remove("Config")
            For i = 0 To secs.Count - 1
                games.Add(New Game(i, Me, secs(i)))
            Next
            Form1.toggleAddGamePic()
        End If
    End Sub

    Public topOffset As Integer = 15
    Public panelWidth As Integer = 200
    Public panelHeight As Integer = 50
    Public gap As Integer = 10
    Public nameLabelUpperMargin As Integer = 5
    Public gameLabelGap As Integer = 0
    Public nameLabelLeftMargin As Integer = -10
    Public menuRightMargin As Integer = 10
    Public addUserGap As Integer = 10
    Public rankingTimeLabelLeftOffset As Integer = 10
    Public rankingTimeLabelTopOffset As Integer = 70
    Public rankingBarHeight As Integer = 25
    Public rankingBarMargin As Integer = 25
    Public rankingBarGap As Integer = 10

    Sub initPanel()
        backPanel = New PictureBox
        backPanel.Size = New Size(panelWidth, panelHeight)
        backPanel.BackColor = getBackColorNormal()
        backPanel.Location = New Point(GamePanel.baseLeft + GamePanel.baseSideMargin + id * (panelWidth + gap), topOffset)
        backPanel.Cursor = Cursors.Hand
        AddHandler backPanel.MouseEnter, AddressOf mouseEnter
        AddHandler backPanel.MouseLeave, AddressOf mouseLeave
        AddHandler backPanel.Click, AddressOf panelClick
        Form1.Controls.Add(backPanel)
        backPanel.BringToFront()

        nameLabel = New Label()
        nameLabel.Text = name
        nameLabel.Font = New Font(Form1.globalFont.Name, 16, FontStyle.Regular)
        nameLabel.AutoSize = True
        nameLabel.Location = New Point(backPanel.Left + backPanel.Width / 2 - nameLabel.Width / 2 + nameLabelLeftMargin, backPanel.Top + backPanel.Height / 2 - nameLabel.Height / 2)
        nameLabel.ForeColor = Color.White
        nameLabel.BackColor = getBackColorHover()
        nameLabel.Cursor = Cursors.Hand
        AddHandler nameLabel.MouseEnter, AddressOf mouseEnter
        AddHandler nameLabel.MouseLeave, AddressOf mouseLeave
        AddHandler nameLabel.Click, AddressOf panelClick
        Form1.Controls.Add(nameLabel)
        nameLabel.BringToFront()

        statePic = New PictureBox
        statePic.Size = New Size(10, 10)
        statePic.Location = New Point(nameLabel.Left - 10 - statePic.Width, nameLabel.Top + nameLabel.Height / 2 - statePic.Height / 2)
        statePic.BackgroundImage = My.Resources.red
        statePic.BackgroundImageLayout = ImageLayout.Stretch
        statePic.Cursor = Cursors.Hand
        AddHandler statePic.MouseEnter, AddressOf mouseEnter
        AddHandler statePic.MouseLeave, AddressOf mouseLeave
        AddHandler statePic.Click, AddressOf panelClick
        Form1.Controls.Add(statePic)
        statePic.BringToFront()

        activeGameLabel = New Label()
        activeGameLabel.Text = "Playing Rocket League"
        activeGameLabel.Font = New Font(Form1.globalFont.Name, 9, FontStyle.Regular)
        activeGameLabel.AutoSize = True
        activeGameLabel.Location = New Point(backPanel.Left + backPanel.Width / 2 - nameLabel.Width / 2, backPanel.Top + nameLabelUpperMargin)
        activeGameLabel.ForeColor = Color.White
        activeGameLabel.BackColor = getBackColorHover()
        activeGameLabel.Cursor = Cursors.Hand
        AddHandler activeGameLabel.MouseEnter, AddressOf mouseEnter
        AddHandler activeGameLabel.MouseLeave, AddressOf mouseLeave
        AddHandler activeGameLabel.Click, AddressOf panelClick
        Form1.Controls.Add(activeGameLabel)
        activeGameLabel.BringToFront()

        menuPic = New PictureBox
        menuPic.Size = New Size(20, 20)
        menuPic.Location = New Point(backPanel.Right - menuRightMargin - menuPic.Width, backPanel.Top + backPanel.Height / 2 - menuPic.Height / 2)
        menuPic.BackgroundImage = My.Resources.menu_inv
        menuPic.BackgroundImageLayout = ImageLayout.Stretch
        menuPic.Cursor = Cursors.Hand
        AddHandler menuPic.MouseEnter, AddressOf mouseEnter
        AddHandler menuPic.MouseLeave, AddressOf mouseLeave
        AddHandler menuPic.Click, AddressOf menuClick
        menuPic.ContextMenuStrip = Form1.conUser
        Form1.Controls.Add(menuPic)
        menuPic.BringToFront()

        If addUser Is Nothing Then
            addUser = New PictureBox
            addUser.Size = New Size(panelHeight / 2, panelHeight / 2)
            addUser.Location = New Point(GamePanel.baseLeft + GamePanel.baseSideMargin + count * (panelWidth + gap) + addUserGap, backPanel.Top + backPanel.Height / 2 - addUser.Height / 2)
            addUser.BackgroundImage = My.Resources.add_inv
            addUser.BackgroundImageLayout = ImageLayout.Stretch
            addUser.Cursor = Cursors.Hand
            AddHandler addUser.Click, AddressOf addUserClick
            Form1.Controls.Add(addUser)
            addUser.BringToFront()
        End If

        rankingTimeLabel = New Label()
        rankingTimeLabel.Text = dll.SecondsTodhmsString(8640000)
        rankingTimeLabel.Font = New Font(Form1.globalFont.Name, 14, FontStyle.Regular)
        rankingTimeLabel.AutoSize = False
        rankingTimeLabel.Size = TextRenderer.MeasureText(rankingTimeLabel.Text, rankingTimeLabel.Font)
        rankingTimeLabel.Location = New Point(Form1.statsGroup.Left + rankingTimeLabelLeftOffset, Form1.statsGroup.Top + rankingTimeLabelTopOffset)
        rankingTimeLabel.ForeColor = Color.White
        Form1.Controls.Add(rankingTimeLabel)
        rankingTimeLabel.BringToFront()

        rankingBar = New PictureBox()
        rankingBar.Size = New Size(10, rankingBarHeight)
        rankingBar.BackColor = Color.Gray
        rankingBar.Location = New Point(rankingTimeLabel.Right, rankingTimeLabel.Top)
        rankingBar.Cursor = Cursors.Hand
        AddHandler rankingBar.Click, AddressOf rankingBarClick
        AddHandler rankingBar.MouseHover, AddressOf rankingBarHover
        Form1.Controls.Add(rankingBar)
        rankingBar.BringToFront()

        rankingBarLabel = New Label()
        rankingBarLabel.Font = New Font(Form1.globalFont.Name, 10, FontStyle.Regular)
        rankingBarLabel.Location = New Point(rankingTimeLabel.Right + rankingBarMargin, rankingTimeLabel.Top)
        rankingBarLabel.ForeColor = Color.White
        rankingBarLabel.AutoSize = True
        rankingBarLabel.Cursor = Cursors.Hand
        AddHandler rankingBarLabel.Click, AddressOf rankingBarClick
        AddHandler rankingBarLabel.MouseHover, AddressOf rankingBarHover
        Form1.Controls.Add(rankingBarLabel)
        rankingBarLabel.BringToFront()

        rankingBarnameLabel = New Label()
        rankingBarNameLabel.Font = New Font(Form1.globalFont.Name, 10, FontStyle.Regular)
        rankingBarNameLabel.Location = New Point(rankingTimeLabel.Right + rankingBarMargin, rankingTimeLabel.Top)
        rankingBarNameLabel.ForeColor = Color.White
        rankingBarNameLabel.AutoSize = True
        rankingBarNameLabel.Text = name
        rankingBarNameLabel.ForeColor = Color.White
        rankingBarNameLabel.BackColor = Color.Black
        rankingBarNameLabel.Cursor = Cursors.Hand
        AddHandler rankingBarNameLabel.Click, AddressOf rankingBarClick
        AddHandler rankingBarNameLabel.MouseHover, AddressOf rankingBarHover
        Form1.Controls.Add(rankingBarNameLabel)
        rankingBarNameLabel.BringToFront()

        updateUserInfo()
    End Sub

    Sub rankingBarHover()
        Dim compString As String = IIf(rankingTimeratio < rankingAllUserTotalAlltimeRatioAverage, "-   ", "+ ")
        Form1.tt.Show("Ø " & dll.SecondsTodhmsString(rankingAllUserTotalAlltimeRatioAverage, "ZERRO") & vbNewLine &
                compString & dll.SecondsTodhmsString(Math.Abs(rankingTimeratio - rankingAllUserTotalAlltimeRatioAverage), "ZERRO"), rankingBar, rankingBar.Width / 2, rankingBarHeight + 15, 2000)
    End Sub

    Sub rankingBarClick()
        Dim compString As String = IIf(rankingTimeratio < rankingAllUserTotalAlltimeRatioAverage, "-   ", "+ ")
        MsgBox(name & ": " & dll.SecondsTodhmsString(rankingTimeratio, "ZERRO") & vbNewLine & vbNewLine &
               "Ø " & dll.SecondsTodhmsString(rankingAllUserTotalAlltimeRatioAverage, "ZERRO") & vbNewLine &
                compString & dll.SecondsTodhmsString(Math.Abs(rankingTimeratio - rankingAllUserTotalAlltimeRatioAverage), "ZERRO"))
    End Sub
    Public Shared Sub updatePanels()
        For Each user In Form1.users
            user.updatePanel()
        Next
    End Sub

    Sub updatePanel()

        backPanel.Left = GamePanel.baseLeft + GamePanel.baseSideMargin + ((GamePanel.siz.Width + GamePanel.gap) * Game.maxGameCount - GamePanel.gap) / 2 - count * (panelWidth + gap) / 2 + id * (panelWidth + gap)
        addUser.Left = GamePanel.baseLeft + GamePanel.baseSideMargin + ((GamePanel.siz.Width + GamePanel.gap) * Game.maxGameCount - GamePanel.gap) / 2 - count * (panelWidth + gap) / 2 + count * (panelWidth + gap) + addUserGap
        nameLabel.Left = backPanel.Left + backPanel.Width / 2 - nameLabel.Width / 2 + nameLabelLeftMargin
        statePic.Left = nameLabel.Left - 10 - statePic.Width
        menuPic.Left = backPanel.Right - menuRightMargin - menuPic.Width

        If mouseHover And Not selected Then
            backPanel.BackColor = getBackColorHover()
        ElseIf Not mouseHover And Not selected Then
            backPanel.BackColor = getBackColorNormal()
        Else
            backPanel.BackColor = getBackColorSelected()
        End If
        nameLabel.BackColor = backPanel.BackColor
        statePic.BackColor = backPanel.BackColor
        activeGameLabel.BackColor = backPanel.BackColor
        menuPic.BackColor = backPanel.BackColor

        activeGameLabel.Visible = isGameActive()
        If isGameActive() Then
            nameLabel.Top = backPanel.Top + backPanel.Height / 2 - (nameLabel.Height + activeGameLabel.Height + gameLabelGap) / 2
            activeGameLabel.Text = "Playing " & activeGame
            activeGameLabel.Location = New Point(nameLabel.Left + nameLabel.Width / 2 - activeGameLabel.Width / 2 - gameLabelGap / 2 - statePic.Width / 2, backPanel.Top + backPanel.Height / 2 + (nameLabel.Height + activeGameLabel.Height + gameLabelGap) / 2 - activeGameLabel.Height)
            activeGameLabel.BringToFront()
        Else
            activeGameLabel.Text = ""
            nameLabel.Top = backPanel.Top + backPanel.Height / 2 - nameLabel.Height / 2
        End If
        statePic.Location = New Point(nameLabel.Left - 10 - statePic.Width, nameLabel.Top + nameLabel.Height / 2 - statePic.Height / 2 + 1)

        If online Then
            statePic.BackgroundImage = My.Resources.green
        Else
            If existsInSharedFolder() Then
                statePic.BackgroundImage = My.Resources.red
            Else
                statePic.BackgroundImage = My.Resources.black
            End If
        End If

        If Not isMeInitialized() And isMe() Then
            nameLabel.ForeColor = Color.Red
        Else
            nameLabel.ForeColor = Color.White
        End If
    End Sub

    Sub updateSummaryPanel(index As Integer, allUserTotalTimeAlltime As Long, allUserTotalRatio As Double)

        If allUserTotalRatio = 0 Then allUserTotalRatio = 1
        If Form1.users Is Nothing OrElse Form1.users.Count = 0 Then
            Return
        End If


        Dim totalTime As Long = getTotalTimeForAllGames()
        Dim timeRatio As Double = getGameTimeRatio(totalTime)

        rankingTimeLabel.Text = dll.SecondsTodhmsString(timeRatio, "      ZERRO", True)
        rankingTimeLabel.Top = Form1.statsGroup.Top + rankingTimeLabelTopOffset + index * (rankingTimeLabel.Height + rankingBarGap)
        If isOneGameIncludedActive() And Form1.dateRangeIncludeToday() And online Then
            rankingTimeLabel.ForeColor = Form1.getFontColor(Form1.LabelMode.RUNNING)
        Else
            rankingTimeLabel.ForeColor = Form1.getFontColor(Form1.LabelMode.NORMAL)
        End If

        rankingBar.Top = rankingTimeLabel.Top + rankingTimeLabel.Height / 2 - rankingBarHeight / 2
        Dim ratio As Double = (timeRatio / allUserTotalRatio)
        Dim rankingBarTotalWidth As Integer = (Form1.statsGroup.Width - rankingTimeLabel.Width - rankingTimeLabelLeftOffset - rankingBarMargin * 2)
        rankingBar.Width = rankingBarTotalWidth * ratio + 3

        Dim allUserTotalAlltimeRatio As Double = getAllUserAlltimeAverage(allUserTotalTimeAlltime)
        Dim allUserTotalAlltimeRatioAverage As Double = allUserTotalAlltimeRatio / Form1.users.Count

        rankingAllUserTotalAlltimeRatioAverage = allUserTotalAlltimeRatioAverage
        rankingTimeratio = timeRatio

        Dim userDeviationRatio As Double = timeRatio / allUserTotalAlltimeRatioAverage
        Dim red As Integer = 255 - userDeviationRatio * 122
        If red < 0 Then red = 0
        If red > 255 Then red = 255
        Dim green As Integer = userDeviationRatio * 122
        If green < 0 Then green = 0
        If green > 255 Then green = 255
        rankingBar.BackColor = Color.FromArgb(red, green, 0)

        rankingBarLabel.Text = Math.Round(ratio * 100, 1) & " % | " & Math.Round(userDeviationRatio * 100) & " %"
        Dim labelInsideBar As Boolean = rankingBar.Width > rankingBarLabel.Width + 5
        Dim nameLabelInsideBar As Boolean = rankingBar.Width > rankingBarTotalWidth - rankingBarNameLabel.Width
        rankingBarLabel.Left = IIf(labelInsideBar, IIf(nameLabelInsideBar, rankingBar.Right - rankingBarLabel.Width - 5 - rankingBarNameLabel.Width - 20, rankingBar.Right - rankingBarLabel.Width - 5), rankingBar.Left + rankingBar.Width + 5)
        rankingBarLabel.Top = rankingBar.Top + rankingBarHeight / 2 - rankingBarLabel.Height / 2

        rankingBarNameLabel.Location = New Point(IIf(nameLabelInsideBar, rankingBar.Right - rankingBarNameLabel.Width - 5, rankingBarLabel.Right + 20), rankingBarLabel.Top)


        If labelInsideBar Or nameLabelInsideBar Then
            rankingBarLabel.BackColor = rankingBar.BackColor
            If green >= 0.7 * 255 Then
                rankingBarLabel.ForeColor = Color.Black
            Else
                rankingBarLabel.ForeColor = Color.White
            End If
        Else
            rankingBarLabel.ForeColor = Color.White
            rankingBarLabel.BackColor = Color.Black
        End If
        If nameLabelInsideBar Then
            rankingBarNameLabel.BackColor = rankingBar.BackColor
            If green >= 0.7 * 255 Then
                rankingBarNameLabel.ForeColor = Color.Black
            Else
                rankingBarNameLabel.ForeColor = Color.White
            End If
        Else
            rankingBarNameLabel.ForeColor = Color.White
            rankingBarNameLabel.BackColor = Color.Black
        End If

    End Sub

    Sub updateUserInfo()
        activeGame = ""
        If isMe() Then
            online = isMeInitialized()
            If isOneGameActive() And isMeInitialized() Then
                activeGame = getPrioActiveGame().name
            End If
        Else
            activeGamePrioQueue.Clear()
            For Each game In games
                game.active = False
            Next
            Dim onlineValue As String = ""
            Try
                onlineValue = dll.iniReadValue("Config", "online", "", iniPath)
            Catch ex As Exception
                Form1.log("updateUserInfo() failed for key 'online': " & ex.Message)
            End Try

            online = False
            If onlineValue <> "" Then
                Dim dt As Date
                If Date.TryParse(onlineValue, dt) Then
                    If Now.Subtract(dt).TotalSeconds <= AFK_THRESHOLD Then
                        online = True
                    End If
                End If
            End If
            Dim activeGameSection As String = ""
            Try
                activeGameSection = dll.iniReadValue("Config", "active", "", iniPath)
            Catch ex As Exception
                Form1.log("updateUserInfo() failed for key 'active': " & ex.Message)
            End Try
            activeGame = ""
            If activeGameSection <> "" Then
                If dll.iniIsValidSection(activeGameSection, iniPath) Then
                    activeGame = "unknown game"
                    Try
                        activeGame = dll.iniReadValue(activeGameSection, "name", "unknown game", iniPath)
                        Dim gameRef As Game = getGameBySection(activeGameSection)
                        If gameRef IsNot Nothing Then
                            activeGamePrioQueue.Add(gameRef)
                            gameRef.active = True
                        End If
                    Catch ex As Exception
                        Form1.log("updateUserInfo() failed for key 'game/name': " & ex.Message)
                    End Try
                End If
            End If
            Dim lastTempVal = dll.iniReadValue("Config", "lastTemp", dll.dateNowFormat(), iniPath)
            Dim lastTempDt As Date
            If Date.TryParse(lastTempVal, lastTempDt) Then
                Dim diffSecs As Integer = Now.Subtract(lastTempDt).TotalSeconds
                lastTempTime = lastTempDt
            End If

        End If

        updatePanel()
    End Sub

    Sub updateUserTime()
        firstLogEntry = Nothing
        For Each game In games
            game.loadTime(iniPath)
        Next
        Form1.updateLabels(False)
    End Sub

    Sub selectUser()
        User.newUserSelected(Me)
        updatePanels()
        updateUserTime()
    End Sub

    Public Shared Sub newUserSelected(selected As User)
        Form1.SuspendLayout()
        For Each user As User In Form1.users
            user.selected = user.Equals(selected)
            If user.selected Then
                user.destroyGames()
                user.initGames()
                Form1.toggleAddGamePic()
            Else
                user.destroyGames()
            End If
        Next
        Form1.ResumeLayout()
    End Sub

    Sub initGames()
        reassignGameIds()
        For Each game In games
            game.panel.init()
        Next
    End Sub

    Function reassignGameIds() As Boolean
        Dim gameArray(games.Count - 1) As Game
        games.CopyTo(gameArray)
        Array.Sort(gameArray, New Game.GameSortingComparer(Form1.primarySort, Form1.secondarySort))
        games = gameArray.ToList()
        Dim change As Boolean = False
        For i = 0 To games.Count - 1
            If games(i).id <> i Then change = True
            games(i).id = i
        Next
        Return change
    End Function

    Sub destroyGames()
        For Each game In games
            game.destroy()
        Next
        destroyGroupedControls()
    End Sub

    Function getBackColorNormal() As Color
        Return Color.FromArgb(10, 10, 10)
    End Function
    Function getBackColorHover() As Color
        Return Color.FromArgb(25, 25, 25)
    End Function
    Function getBackColorSelected() As Color
        Return Color.FromArgb(60, 60, 60)
    End Function

    Sub panelClick()
        If isMe() And Not isMeInitialized() Then
            Dim input As String = InputBox("Type in your name")
            If input <> "" And Not input.ToLower() = DEFAULT_NAME.ToLower() Then
                If getOtherUsers(False, True).Contains(input.ToLower()) Then
                    MsgBox("Name is already taken. Please choose another one.", MsgBoxStyle.Information)
                Else
                    Dim prev As String = dll.iniReadValue("Config", "users")
                    prev = prev.Replace(DEFAULT_NAME, input)
                    If prev = "" Then prev = input
                    dll.iniWriteValue("Config", "users", prev)
                    dll.iniWriteValue("Config", "userName", input)
                    IO.Directory.CreateDirectory(Form1.sharedStatsPath & input)
                    Form1.reloadUsers()
                    Form1.updateUserInfos()
                End If
            End If
        Else
            updateUserInfo()
            selectUser()
        End If
    End Sub
    Public Function getGameBySection(section As String) As Game
        For Each game In games
            If game.section.ToLower() = section.ToLower() Then
                Return game
            End If
        Next
        Return Nothing
    End Function
    Sub menuClick(sender As Object, e As EventArgs)
        conUser = Me
        menuPic.ContextMenuStrip.Show(Cursor.Position)
    End Sub

    Function getOtherUsers(excludeExisting As Boolean, Optional toLower As Boolean = False) As List(Of String)
        Dim otherUsers As New List(Of String)
        If Not IO.Directory.Exists(Form1.sharedStatsPath) Then IO.Directory.CreateDirectory(Form1.sharedStatsPath)
        Dim dirs() As String = IO.Directory.GetDirectories(Form1.sharedStatsPath)
        If dirs IsNot Nothing Then
            For Each dir As String In dirs
                If IO.File.Exists(dir & "\" & "gtimer.ini") Then
                    Dim name As String = dir.Substring(dir.LastIndexOf("\") + 1)
                    If Not excludeExisting Or Not User.exists(name) Then
                        If toLower Then
                            otherUsers.Add(name.ToLower)
                        Else
                            otherUsers.Add(name)
                        End If
                    End If
                End If
            Next
        End If
        Return otherUsers
    End Function

    Function existsInSharedFolder() As Boolean
        If Not IO.Directory.Exists(Form1.sharedStatsPath) Then IO.Directory.CreateDirectory(Form1.sharedStatsPath)
        Dim dirs() As String = IO.Directory.GetDirectories(Form1.sharedStatsPath)
        If dirs IsNot Nothing Then
            For Each dir As String In dirs
                If IO.File.Exists(dir & "\" & "gtimer.ini") Then
                    Dim dirName As String = dir.Substring(dir.LastIndexOf("\") + 1)
                    If dirName.ToLower() = name.ToLower() Then Return True
                End If
            Next
        End If
        Return False
    End Function
    Sub addUserClick()
        If isMe() And Not isMeInitialized() Then
            panelClick()
        Else
            If Form1.users IsNot Nothing AndAlso Form1.users.Count < MAX_USERS Then
                Dim otherUsers As List(Of String) = getOtherUsers(True)
                Dim userString As String = ""
                For Each user In otherUsers
                    userString &= vbNewLine & user
                Next
                Dim def As String = ""
                If otherUsers.Count > 0 Then def = otherUsers(0)
                Dim input As String = InputBox("Add friend by name. Suggestions:" & IIf(userString = "", vbNewLine & "---", userString), , def)
                If input <> "" Then
                    If Not User.exists(input) Then
                        Dim prev As String = dll.iniReadValue("Config", "users")
                        prev &= ";" & input
                        dll.iniWriteValue("Config", "users", prev)
                        Form1.reloadUsers()
                        Form1.updateUserInfos()
                    End If
                End If
            Else
                MsgBox("You reached the maximum number of friends.")
            End If
        End If
    End Sub

    Sub mouseEnter()
        mouseHover = True
        updatePanel()
    End Sub
    Sub mouseLeave()
        mouseHover = isMouseInRect()
        updatePanel()
    End Sub

    Function isMouseInRect() As Boolean
        Return backPanel.Bounds.IntersectsWith(New Rectangle(New Point(Cursor.Position.X, Cursor.Position.Y), New Size(1, 1)))
    End Function
    Function isMe() As Boolean
        If Form1.userName Is Nothing Then Return False
        Return name.ToLower() = Form1.userName.ToLower()
    End Function

    Function isGameActive() As Boolean
        Return online And activeGame <> ""
    End Function
    Public Function getGameTimeRatio(time As Long) As Double
        Dim effectiveStart As Date = getEffectiveStartDate()
        Dim diff As Integer = Form1.dll.GetDayDiff(effectiveStart.Date, Form1.endDate.Date)
        diff = Math.Max(diff, 0)
        Dim sliceRatio As Double = 1
        Dim mode As Integer = Form1.viewMode
        If mode > 0 Then
            sliceRatio = (diff + 1) / mode
        End If

        Dim timeRatio As Double = time / sliceRatio
        Return timeRatio
    End Function

    Public Function getAllUserAlltimeAverage(time As Long) As Double
        Dim effectiveStart As Date = getEffectiveStartDate()
        Dim allDiff As Integer = Form1.dll.GetDayDiff(firstLogEntry.Date, Now.Date)
        Dim diff As Integer = Form1.dll.GetDayDiff(effectiveStart.Date, Form1.endDate.Date)
        diff = Math.Max(diff, 0)
        Dim sliceRatio As Double = 1
        Dim mode As Integer = Form1.viewMode
        If mode > 0 Then
            sliceRatio = ((diff + 1) / mode)
        End If


        Dim timeRatio As Double = (time / (allDiff + 1)) * (diff + 1) / sliceRatio
        Return timeRatio
    End Function

    Public Function getPrioActiveGame() As Game
        If games IsNot Nothing Then
            For Each game In games
                If game.isPrioActiveGame() Then
                    Return game
                End If
            Next
        End If
        Return Nothing
    End Function
    Function getTotalTimeForAllGames(Optional dateRangeMode As Form1.FetchMethod = Form1.FetchMethod.CUSTOM) As Long
        Dim gameSum As Long = 0
        For i = 0 To games.Count - 1
            If games(i).include Then
                gameSum += games(i).getTime(dateRangeMode)
            End If
        Next
        Return gameSum
    End Function
    Public Shared Function sortByGameTime() As List(Of User)
        Dim userArray(Form1.users.Count - 1) As User
        Form1.users.CopyTo(userArray)
        Array.Sort(userArray, New UserGameTimeComparer())
        Dim resList As List(Of User) = userArray.ToList()
        Return resList
    End Function

    Public Class UserGameTimeComparer
        Implements IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim u1 = DirectCast(x, User)
            Dim u2 = DirectCast(y, User)
            Dim totalTime As Long = u1.getTotalTimeForAllGames()
            Dim totalTime2 As Long = u2.getTotalTimeForAllGames()
            If totalTime - totalTime2 < 0 Then
                Return 1
            Else
                Return -1
            End If
            Dim diff As Long = x.getTime() - y.getTime()
            If diff < 0 Then
                Return 1
            Else
                Return -1
            End If
        End Function
    End Class

    Public Function isOneGameIncluded() As Boolean
        If games IsNot Nothing Then
            For Each g As Game In games
                If g.include Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Public Function isOneGameActive() As Boolean
        Return getPrioActiveGame() IsNot Nothing
    End Function

    Public Function isOneGameIncludedActive() As Boolean
        If games IsNot Nothing Then
            For Each g As Game In games
                If g.include Then
                    If g.active Then
                        Return True
                    End If
                End If
            Next
        End If
        Return False
    End Function

    Function getFirstLogEntryDate() As Date
        Dim res As Date = Nothing
        If games IsNot Nothing Then
            For Each g As Game In games
                Dim keys As List(Of String)
                keys = g.getAllTimeKeys(iniPath)
                If keys IsNot Nothing AndAlso keys.Count > 0 Then
                    Dim min As Date = keys.Min(Function(key)
                                                   Return Date.Parse(key)
                                               End Function)
                    If res = Nothing OrElse res.CompareTo(min) > 0 Then
                        res = min
                    End If
                End If
            Next
        End If
        firstLogEntry = res
        Return res
    End Function

    Public Function getEffectiveStartDate() As Date
        Dim effectiveStart As Date = Form1.startDate
        If getFirstLogEntry <> Nothing Then
            If firstLogEntry.CompareTo(effectiveStart) > 0 Then
                effectiveStart = firstLogEntry
            End If
        End If
        Return effectiveStart
    End Function

    Function getFirstGroupPanelGame() As Game
        For Each g In games
            If g.getGroupPanelIndex() = 0 Then
                Return g
            End If
        Next
        Return Nothing
    End Function

    Function getGroupedGamesCount() As Integer
        Return Math.Max(0, games.Count - Game.maxGameCount + 1)
    End Function

    Function getGroupPanelGames() As List(Of Game)
        Dim result As New List(Of Game)
        For Each g In games
            If g.isInGroupPanel() Then
                result.Add(g)
            End If
        Next
        Return result
    End Function

    Function isOneGroupedGameIncluded() As Boolean
        For Each g In getGroupPanelGames()
            If g.include Then Return True
        Next
        Return False
    End Function

    Function getGroupedGamesTime(includeExcluded As Boolean) As Long
        Dim time As Long
        For Each g In getGroupPanelGames()
            If g.include Or includeExcluded Then
                time += g.getTime()
            End If
        Next
        Return time
    End Function

    Public Shared Function isMeSelected() As Boolean
        If getMe() Is Nothing Then Return True
        Return getMe().selected
    End Function

    Public Shared Function getMe() As User
        If Form1.users IsNot Nothing Then
            For Each u In Form1.users
                If u.isMe() Then Return u
            Next
        End If
        Return Nothing
    End Function

    Public Shared Function exists(name As String) As Boolean
        If Form1.users IsNot Nothing Then
            For Each user In Form1.users
                If user.name.ToLower = name.ToLower() Then Return True
            Next
        End If
        Return False
    End Function
    Public Shared Function getSelected() As User
        If Form1.users IsNot Nothing Then
            For Each u In Form1.users
                If u.selected Then Return u
            Next
        End If
        Return Nothing
    End Function

    Public Shared Function getHovered() As User
        If Form1.users IsNot Nothing Then
            For Each u In Form1.users
                If u.isMouseInRect() Then Return u
            Next
        End If
        Return Nothing
    End Function

    Public Shared Function getByName(name As String) As User
        If Form1.users IsNot Nothing Then
            For Each u In Form1.users
                If u.name.ToLower() = name.ToLower() Then Return u
            Next
        End If
        Return Nothing
    End Function

    Shared Function isMeInitialized() As Boolean
        Dim meUser As User = User.getMe()
        If meUser IsNot Nothing Then
            If meUser.name.ToLower() <> DEFAULT_NAME.ToLower() Then
                Return True
            End If
        End If
        Return False
    End Function
End Class
