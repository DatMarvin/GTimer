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

    Dim mouseHover As Boolean
    Public online As Boolean
    Public activeGame As String

    Public Shared conUser As User


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
    Public Sub New(id As Integer, name As String)
        Me.id = id
        Me.name = name
        initPanel()
    End Sub
    Sub destroy()
        Form1.Controls.Remove(backPanel)
        Form1.Controls.Remove(nameLabel)
        Form1.Controls.Remove(statePic)
        Form1.Controls.Remove(activeGameLabel)
        Form1.Controls.Remove(menuPic)
        Form1.Controls.Remove(addUser)
        addUser = Nothing
    End Sub

    Public topOffset As Integer = 15
    Public panelWidth As Integer = 200
    Public panelHeight As Integer = 50
    Public gap As Integer = 10
    Public nameLabelUpperMargin As Integer = 10
    Public gameLabelGap As Integer = 0
    Public nameLabelLeftMargin As Integer = -10
    Public menuRightMargin As Integer = 10
    Public addUserGap As Integer = 10

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

        selected = isMe()
        updateUserInfo()
    End Sub

    Public Shared Sub updatePanels()
        For Each user In Form1.users
            user.updatePanel()
        Next
    End Sub

    Sub updatePanel()
        Dim gameCount As Integer = 3
        If Form1.games IsNot Nothing Then
            If Form1.games.Count > 3 Then
                gameCount = Form1.games.Count
            End If
        End If

        backPanel.Left = GamePanel.baseLeft + GamePanel.baseSideMargin + ((GamePanel.siz.Width + GamePanel.gap) * gameCount - GamePanel.gap) / 2 - count * (panelWidth + gap) / 2 + id * (panelWidth + gap)
        addUser.Left = GamePanel.baseLeft + GamePanel.baseSideMargin + ((GamePanel.siz.Width + GamePanel.gap) * gameCount - GamePanel.gap) / 2 - count * (panelWidth + gap) / 2 + count * (panelWidth + gap) + addUserGap
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

    Sub updateUserInfo()
        activeGame = ""
        If isMe() Then
            online = isMeInitialized()
            If Game.isOneGameActive() And isMeInitialized() Then
                activeGame = Game.getPrioActiveGame().name
            End If
        Else
            Dim onlineValue = dll.iniReadValue("Config", "online", "", iniPath)
            online = False
            If onlineValue <> "" Then
                Dim dt As Date
                If Date.TryParse(onlineValue, dt) Then
                    If Now.Subtract(dt).TotalSeconds <= AFK_THRESHOLD Then
                        online = True
                    End If
                End If
            End If
            Dim activeGameSection As String = dll.iniReadValue("Config", "active", "", iniPath)
            activeGame = ""
            If activeGameSection <> "" Then
                If dll.iniIsValidSection(activeGameSection, iniPath) Then
                    activeGame = dll.iniReadValue(activeGameSection, "name", "unknown game", iniPath)
                End If
            End If
        End If

        updatePanel()
    End Sub

    Sub selectUser()
        For Each user As User In Form1.users
            user.selected = user.Equals(Me)
        Next
        Game.firstLogEntry = Nothing
        updatePanels()
        For Each game In Form1.games
            game.loadTime(iniPath)
        Next
        Form1.updateLabels(False)
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
