Imports System.Reflection
Imports System.Net
Imports System.IO
Imports System.IO.Compression
Public Class OptionsForm
    ReadOnly Property dll() As Utils
        Get
            Return Form1.dll
        End Get
    End Property

    ReadOnly Property inipath As String
        Get
            Return Form1.iniPath
        End Get
    End Property
    ReadOnly Property publishPath As String
        Get
            Return Form1.publishPath
        End Get
    End Property
    ReadOnly Property args As List(Of String)
        Get
            If arguments Is Nothing Then Return New List(Of String)
            Return arguments.ToList()
        End Get
    End Property

    ReadOnly Property isOneDriveInstalled() As Boolean
        Get
            Return Not publishPath.Contains("%")
        End Get
    End Property

    Public Shared AUTOSTART_REGISTRY_KEY As String = "SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run\" '"Software\Microsoft\Windows\CurrentVersion\Run"

    Public arguments() As String
    Public state As optionState

    Public coreFiles As List(Of String)
    Dim changesPending As Boolean

    Private Sub OptionsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Size = New Size(325 + listMenu.Width, 300)
        Me.Location = New Point(Form1.Left + Form1.Width / 2 - Me.Width / 2, Form1.Top + Form1.Height / 2 - Me.Height / 2)

        initCoreFiles()
        colorForm()

        If state = optionState.NONE Then
            If Form1.lastOptionsState = optionState.NONE Then
                init(0)
            Else
                init(Form1.lastOptionsState)
            End If
        Else

            init(state)
        End If

        changesPending = False
        saveButton.Enabled = changesPending
        reloadAllButton.Visible = True
    End Sub

    Sub initCoreFiles()
        coreFiles = New List(Of String) From {"GTimer.exe"}
        Dim resFiles() As String = dll.GetAllFiles(Form1.resPath, True)
        If resFiles IsNot Nothing Then
            For Each file In resFiles
                coreFiles.Add("res\" & file)
            Next
        End If
    End Sub
    Public Enum optionState As Integer
        NONE
        UPDATE
        CONFIG
        WINDOW
        GAMES
        SORTING
        INVITATIONS
    End Enum

    Public Sub addArguments(arguments() As String)
        If arguments IsNot Nothing Then
            For Each s As String In arguments
                dll.ExtendArray(Me.arguments, s)
            Next
        End If
    End Sub

    Sub init(ByVal state As optionState)
        Cursor = Cursors.WaitCursor
        Me.state = state

        For Each c As Control In Controls
            If TypeOf c Is GroupBox Then
                c.Visible = False
            End If
        Next
        selectIndex(state)

        If state > 0 Then
            ' listMenu.Location = New Point(1, Me.DisplayRectangle.Height / 2 - listMenu.Height / 2)
            listMenu.Location = New Point(1, 10)
            saveButton.Location = New Point(listMenu.Left, listMenu.Top + listMenu.Height + 5)
            If Controls.ContainsKey("g" & stateToIndex(state) + 1) Then
                Controls("g" & stateToIndex(state) + 1).Location = New Point(listMenu.Right + 1, 1)
                Controls("g" & stateToIndex(state) + 1).Visible = True
            End If
        End If

        Select Case state

            Case optionState.UPDATE

                labelCurrVersion.Text = Form1.version
                labelLatestVersion.Text = dll.getLatestVersion()
                labelPublishedVersion.Text = ""
                textSharedFolder.Text = Form1.sharedPath
                checkAutoUpdate.Checked = Form1.autoUpdate

            Case optionState.CONFIG

            Case optionState.WINDOW
                '  checkDarkTheme.Checked = Form1.darkTheme
                checkSavePos.Checked = Form1.saveWinPosSize
                checkAutostart.Checked = Form1.autostartEnabled
                checkShowInTaskbar.Checked = Form1.showMinimizedInTaskbar
                comboStartState.SelectedIndex = dll.iniReadValue("Config", "startState", 1)
                fontLabel.Font = New Font(Form1.globalFont.Name, 12)
                '   trackbarBalance.Value = Form1.balance
                '  trackbarPlayRate.Value = scaleToNum(Form1.playRate)
                ''  labelBalance.Text = "Balance: " & trackbarBalance.Value
                '  labelPlayRate.Text = "Play Rate: " & numToScale(trackbarPlayRate.Value)
                '  checkRandomNextTrack.Checked = Form1.randomNextTrack
                '  checkPlaylistHistory.Checked = Form1.savePlaylistHistory
                '  checkRemoveTrackFromList.Checked = Form1.removeNextTrack

                Form1.saveWinPos()
                Form1.saveWinSize()

            Case optionState.GAMES
                toggleGameInfoVisible(False)
                fillGameList()
                selectGameByArgument()

            Case optionState.SORTING
                sortingRad1.Checked = True
                loadCurrSorting()

            Case optionState.INVITATIONS
                checkAllowInvites.Checked = Form1.invitesAllowed
                checkInviteFlash.Checked = Form1.inviteFlashEnabled
                numInviteTimeout.Value = Form1.inviteTimeout
                fillBlacklist()
            Case optionState.NONE
                MsgBox("No option mode selected")
        End Select
        Cursor = Cursors.Default
    End Sub

    Function saveChanges() As Boolean 'true if form must not be closed
        If changesPending Then
            dll.inipath = inipath
            Select Case state
                Case optionState.UPDATE
                    updateSharedPath()
                Case optionState.GAMES
                    If listGames.SelectedIndex > -1 Then
                        Dim game As Game = listGames.SelectedItem
                        game.exe = gameExeNameText.Text
                        If game.name <> gameNameText.Text Then
                            game.name = gameNameText.Text
                            Dim sel As Integer = listGames.SelectedIndex
                            listGames.Items.RemoveAt(sel)
                            listGames.Items.Insert(sel, game)
                            listGames.SelectedIndex = sel
                        End If
                        game.logoPath = gamePicText.Text
                        setPicImage(game.logoPath)
                        game.logoInvPath = gamePicInvText.Text
                        setPicInvImage(game.logoInvPath)
                        game.include = checkIncludeGame.Checked
                        game.locationStartExe = gameLocationText.Text
                        dll.iniWriteValue(game.section, "exe", game.exe, inipath)
                        dll.iniWriteValue(game.section, "name", game.name, inipath)
                        dll.iniWriteValue(game.section, "logo", game.logoPath, inipath)
                        dll.iniWriteValue(game.section, "logo_inv", game.logoInvPath, inipath)
                        dll.iniWriteValue(game.section, "include", Math.Abs(CInt(game.include)), inipath)
                        dll.iniWriteValue(game.section, "locationStartExe", game.locationStartExe, inipath)
                    End If
                    Form1.reloadAll()
                Case optionState.INVITATIONS
                    Form1.inviteTimeout = numInviteTimeout.Value
                    dll.iniWriteValue("Config", "inviteTimeout", Form1.inviteTimeout, inipath)

                    If Form1.inviteBlacklist IsNot Nothing Then
                        Dim s As String = ""
                        For i = 0 To Form1.inviteBlacklist.Count - 1
                            s &= Form1.inviteBlacklist(i)
                            If i < Form1.inviteBlacklist.Count - 1 Then s &= ";"
                        Next
                        dll.iniWriteValue("Config", "inviteBlacklist", s, inipath)
                    End If

            End Select
        End If
        Return False
    End Function
    Private Sub OptionsForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If saveChanges() Then
            e.Cancel = True
            Return
        End If
        Form1.lastOptionsState = state
    End Sub

    Private Sub reloadAll_Click(sender As Object, e As EventArgs) Handles reloadAllButton.Click
        Form1.reloadAll()
        Close()
    End Sub

    Sub colorForm() '06.08.19
        If inipath = "" Then Return
        Dim inverted As Boolean = 1
        Dim lightCol As Color = IIf(inverted, Color.FromArgb(50, 50, 50), Color.White)
        Dim darkCol As Color = IIf(inverted, Color.FromArgb(20, 20, 20), Color.FromArgb(255, 240, 240, 240))

        Dim invLightCol As Color = IIf(Not inverted, Color.Black, Color.White)
        Dim invDarkCol As Color = IIf(Not inverted, Color.Black, Color.FromArgb(255, 240, 240, 240))

        Dim elements As New List(Of Control)
        elements.Add(Me)
        For Each c As Control In Me.Controls
            elements.Add(c)
            For Each subControl As Control In c.Controls
                elements.Add(subControl)
                For Each subSubControl As Control In subControl.Controls
                    elements.Add(subSubControl)
                    For Each subSubSubControl As Control In subSubControl.Controls
                        elements.Add(subSubSubControl)
                    Next
                Next
            Next
        Next

        For Each c As Control In elements
            If TypeOf c Is ListBox Then
                c.BackColor = lightCol
                c.ForeColor = invLightCol
            ElseIf TypeOf c Is Button Then
                CType(c, Button).FlatStyle = FlatStyle.System
                c.BackColor = lightCol
                c.ForeColor = invLightCol
            Else
                c.BackColor = darkCol
                c.ForeColor = invDarkCol
            End If

        Next
    End Sub
    Private Sub listMenu_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles listMenu.MouseClick
        Dim it As Integer = sender.IndexFromPoint(New Point(Cursor.Position.X - sender.PointToScreen(New Point(sender.Left, sender.Top)).X + sender.Left, Cursor.Position.Y - sender.PointToScreen(New Point(sender.Left, sender.Top)).Y + sender.top))
        If it > -1 Then
            If Not state = indexToState(listMenu.SelectedIndex) Then
                If Not saveChanges() Then
                    init(listMenu.SelectedIndex)
                Else
                    selectIndex(state)
                End If
            End If
        End If
    End Sub

    Private Sub listMenu_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles listMenu.MouseDown
        Dim it As Integer = listMenu.SelectedIndex
    End Sub
    Private Sub listMenu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listMenu.SelectedIndexChanged

    End Sub
    Function indexToState(ByVal index As Integer) As optionState
        Select Case index
            Case 0 : Return optionState.UPDATE
            Case 1 : Return optionState.CONFIG
            Case 2 : Return optionState.WINDOW
            Case 3 : Return optionState.GAMES
            Case 4 : Return optionState.SORTING
            Case 5 : Return optionState.INVITATIONS
            Case Else : Return optionState.NONE
        End Select
    End Function
    Sub init(ByVal listIndex As Integer)
        init(indexToState(listIndex))
        listMenu.SelectedIndex = listIndex
    End Sub
    Function stateToIndex(ByVal state As optionState) As Integer
        Select Case state
            Case optionState.UPDATE : Return 0
            Case optionState.CONFIG : Return 1
            Case optionState.WINDOW : Return 2
            Case optionState.GAMES : Return 3
            Case optionState.SORTING : Return 4
            Case optionState.INVITATIONS : Return 5
            Case Else : Return -1
        End Select
    End Function
    Sub selectIndex(ByVal state As optionState)
        listMenu.SelectedIndex = stateToIndex(state)
    End Sub


#Region "UI"
    Function isValidDirectoryPath(ByVal s As String) As Boolean
        Return s.Length >= 3 AndAlso Not s.Contains("\\") AndAlso Not s.EndsWith(" ") AndAlso Not s.StartsWith(" ") AndAlso s.Substring(1, 2) = ":\" AndAlso s.EndsWith("\")
    End Function
    Private Sub publishRemButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles publishRemButton.Click
        Dim selIndex As Integer = listPublish.SelectedIndex
        If selIndex > -1 Then
            dll.publishFileList.Remove(listPublish.SelectedItem)
            listPublish.Items.RemoveAt(selIndex)
            listPublish.SelectedIndex = IIf(selIndex < listPublish.Items.Count, selIndex, IIf(listPublish.Items.Count > 0, 0, -1))
            dll.iniWriteValue("Config", "ftpPublish", getPublishFiles(True), inipath)
        End If
    End Sub
    Private Sub publishAddButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles publishAddButton.Click
        Dim res() As String = getFilesDialog(My.Application.Info.DirectoryPath & "\")
        If res IsNot Nothing Then
            Dim err As String = ""
            'publishfilelist extra files not working
            For Each s As String In res
                Try
                    Dim name As String = s.Substring(s.LastIndexOf("\") + 1)
                    If Not coreFiles.Contains(name) Then
                        If Not s = My.Application.Info.DirectoryPath & s.Substring(s.LastIndexOf("\")) Then
                            IO.File.Copy(s, My.Application.Info.DirectoryPath & s.Substring(s.LastIndexOf("\")), True)
                        End If
                        If Not listPublish.Items.Contains(name) Then
                            listPublish.Items.Add(name)
                        End If
                        If Not dll.publishFileList.Contains(name) Then
                            dll.publishFileList.Add(name)
                        End If
                    End If

                Catch ex As Exception
                    err &= s & vbNewLine
                End Try
            Next
            If Not err = "" Then
                MsgBox(err = "Failed to add following files to publishing list:" & vbNewLine & vbNewLine & err)
            Else
                dll.iniWriteValue("Config", "ftpPublish", getPublishFiles(True), inipath)
            End If
        End If
    End Sub

    Function getPublishFiles(ByVal excludeCore As Boolean) As String
        Dim res As String = ""
        For i = 0 To dll.publishFileList.Count - 1
            If Not excludeCore Then

            ElseIf Not coreFiles.Contains(dll.publishFileList(i)) Then
                res &= dll.publishFileList(i) & IIf(i = dll.publishFileList.Count - 1, "", ";")
            End If
        Next
        Return res
    End Function



    Private Sub publishButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles publishButton.Click
        If True OrElse InputBox("pw") = "asd" Then
            If isValidDirectoryPath(publishPath) Then
                addCoreFiles()
                For Each pubItem As String In listPublish.Items
                    dll.publishFileList.Add(pubItem)
                Next
                Dim pub As String = dll.publishTracker(publishPath)
                If pub = "" Then
                    labelPublishedVersion.Text = Form1.version
                ElseIf pub = "user" Then
                Else
                    MsgBox("Publishing failed. Error list:" & vbNewLine & pub)
                End If
            Else
                MsgBox("No valid sharing directory." & vbNewLine & publishPath)
            End If
        End If
    End Sub

    Sub addCoreFiles()
        If dll.publishFileList Is Nothing Then
            dll.publishFileList = New List(Of String)
        End If
        dll.publishFileList.Clear()
        For Each s As String In coreFiles
            dll.publishFileList.Add(s)
        Next
    End Sub

    Private Sub DownloadLatestButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadLatestButton.Click
        updateSharedPath()
        labelLatestVersion.Text = dll.getLatestVersion()
        Dim updateVersion = dll.checkTrackerUpdate(True)
        If updateVersion <> "" Then
            dll.updateTracker(updateVersion)
        End If
    End Sub

    Private Sub checkAutoUpdate_Click(sender As Object, e As EventArgs) Handles checkAutoUpdate.Click
        dll.iniWriteValue("Config", "autoUpdate", Math.Abs(CInt(sender.checked)), inipath)
        Form1.autoUpdate = sender.checked
        changes()
    End Sub

    Private Sub sharedFolderButton_Click(sender As Object, e As EventArgs) Handles sharedFolderButton.Click
        Dim dir As String = getDirectoryDialog(Form1.sharedPath)
        If Not dir = "" Then
            textSharedFolder.Text = dir
            updateSharedPath()
        End If
    End Sub

    Sub updateSharedPath()
        Form1.sharedPath = textSharedFolder.Text
        dll.iniWriteValue("Config", "sharedPath", Form1.sharedPath)
        labelLatestVersion.Text = dll.getLatestVersion()
    End Sub


    Public Function getFileDialog(Optional ByVal initDir As String = "", Optional ByVal ext As String = "") As String
        Dim op As New OpenFileDialog
        op.Multiselect = False
        If Not initDir = "" Then
            Try
                Do
                    initDir = initDir.Substring(0, initDir.LastIndexOf("\"))
                Loop Until initDir.Count(Function(c) c = "\") <= 1 Or IO.Directory.Exists(initDir)
                op.InitialDirectory = initDir
                ' op.FileName = def.Substring(def.LastIndexOf("\") + 1)
            Catch ex As Exception
            End Try
        End If
        If Not ext = "" Then op.Filter = "(*." & ext & ")|*." & ext
        op.ShowDialog()

        If Not op.FileName = "" Then
            Return op.FileName
        End If
        Return ""
    End Function

    Public Function getFilesDialog(Optional ByVal initDir As String = "", Optional ByVal ext As String = "") As String()
        Dim op As New OpenFileDialog
        op.Multiselect = True
        If Not initDir = "" Then
            Try
                op.InitialDirectory = initDir.Substring(0, initDir.LastIndexOf("\"))
            Catch ex As Exception
            End Try
        End If
        If Not ext = "" Then op.Filter = "(*." & ext & ")|*." & ext
        If op.ShowDialog() = DialogResult.Cancel Then Return Nothing
        Return op.FileNames
    End Function
    Public Function getDirectoryDialog(Optional ByVal def As String = "") As String
        Dim op As New FolderBrowserDialog

        op.ShowNewFolderButton = True

        If Not def = "" Then
            Try
                op.SelectedPath = def
            Catch ex As Exception
            End Try
        End If
        op.ShowDialog()
        If Not op.SelectedPath = "" Then
            Return op.SelectedPath & IIf(op.SelectedPath.EndsWith("\"), "", "\")
        End If
        Return ""
    End Function



#End Region



#Region "CONFIG"
    Private Sub importPic_Click(sender As Object, e As EventArgs) Handles importPic.Click
        If importConfig() Then
            changes()
        End If
    End Sub

    Function importConfig() As Boolean
        Dim filePath As String = getFileDialog(inipath, "ini")
        If filePath <> "" Then
            If filePath.ToLower() <> inipath.ToLower() Then

                Dim backupPath As String = inipath & ".backup"
                Try
                    If IO.File.Exists(backupPath) Then
                        IO.File.Delete(backupPath)
                    End If
                Catch ex As Exception
                    If MsgBox("Failed to create backup file. Step code: 0. Continue anyway?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        Return postImport(False)
                    End If
                End Try
                Try
                    IO.File.Copy(inipath, backupPath)
                Catch ex As Exception
                    If MsgBox("Failed to create backup file. Step code: 1. Continue anyway?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        Return postImport(False)
                    End If
                End Try

                Dim backupSucceeded As Boolean = IO.File.Exists(backupPath)

                If Not dll.iniIsValidSection("Config", filePath) Then
                    If MsgBox("Selected file has no section [Config]. Continue anyway?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        Return postImport(False)
                    End If
                End If

                Try
                    Dim configPairs As List(Of KeyValuePair(Of String, String)) = dll.iniGetAllPairs("Config", filePath)
                    For Each pair As KeyValuePair(Of String, String) In configPairs
                        dll.iniWriteValue("Config", pair.Key, pair.Value, inipath)
                    Next
                Catch ex As Exception
                    MsgBox("Failed to import section [Config]", MsgBoxStyle.Critical)
                    If backupSucceeded Then rollbackImport(backupPath)
                    Return postImport(False)
                End Try


                Dim secs As List(Of String) = dll.iniGetAllSectionsList(filePath)
                secs.Remove("Config")

                For Each section In secs
                    Try
                        Dim gamesConfigPairs As List(Of KeyValuePair(Of String, String)) = dll.iniGetAllPairs(section, filePath)
                        If hasConfigTimeKeys(gamesConfigPairs) Then
                            If MsgBox("Config for game [" & section & "] contains time values. Importing could potentially overwrite existing time logs. Proceed anyway?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                Continue For
                            End If
                        End If
                        For Each pair As KeyValuePair(Of String, String) In gamesConfigPairs
                            dll.iniWriteValue(section, pair.Key, pair.Value, inipath)
                        Next
                    Catch ex As Exception
                        MsgBox("Failed to import config for game [" & section & "]", MsgBoxStyle.Critical)
                        If backupSucceeded Then rollbackImport(backupPath)
                        Return postImport(False)
                    End Try
                Next

                MsgBox("Configuration import succeeded.", MsgBoxStyle.Information)

            End If
        End If
        Return postImport(True)
    End Function

    Function postImport(result As Boolean) As Boolean
        dll.inipath = inipath
        Return result
    End Function

    Function hasConfigTimeKeys(pairs As List(Of KeyValuePair(Of String, String))) As Boolean
        For Each pair In pairs
            If Date.TryParse(pair.Key, New Date()) Then
                Return True
            End If
        Next
        Return False
    End Function

    Function rollbackImport(backupPath As String) As Boolean
        Try
            IO.File.Delete(inipath)
        Catch ex As Exception
            MsgBox("Rollback step failed: 'delete'. Please rollback manually.", MsgBoxStyle.Critical)
            Return False
        End Try
        Try
            IO.File.Move(backupPath, inipath)
        Catch ex As Exception
            MsgBox("Rollback step failed: 'move'. Please rollback manually.", MsgBoxStyle.Critical)
            Return False
        End Try
        MsgBox("Import process failed. Rolled back to previous file...", MsgBoxStyle.Exclamation)
        Return True
    End Function

#End Region

#Region "WINDOW"

    Private Sub buttonFont_Click(sender As Object, e As EventArgs) Handles buttonFont.Click
        Dim newFont As Font = getFontDialogResult(New Font(Form1.globalFont, 12))
        If Form1.globalFont.Name <> newFont.FontFamily.Name Then
            Form1.globalFont = newFont.FontFamily
            fontLabel.Font = New Font(Form1.globalFont, 12)
            Form1.setControlFonts(Form1)
            dll.iniWriteValue("Config", "font", Form1.globalFont.Name)
            changes()
        End If
    End Sub


    Function getFontDialogResult(Optional prevFont As Font = Nothing) As Font
        Dim fd As New FontDialog
        fd.AllowScriptChange = False
        fd.ShowEffects = False
        If prevFont IsNot Nothing Then fd.Font = New Font(Form1.globalFont, 12)
        fd.ShowDialog()
        Return fd.Font
    End Function

    Private Sub checkSavePos_CheckedChanged(sender As Object, e As EventArgs) Handles checkSavePos.Click
        changes()
    End Sub

    Private Sub checkSavePos_Click(sender As Object, e As EventArgs) Handles checkSavePos.Click
        dll.iniWriteValue("Config", "SaveWinPosSize", Math.Abs(CInt(sender.checked)), inipath)
        Form1.saveWinPosSize = sender.checked
    End Sub
    Private Sub checkShowInTaskbar_Click(sender As Object, e As EventArgs) Handles checkShowInTaskbar.Click
        dll.iniWriteValue("Config", "showInTaskbar", Math.Abs(CInt(sender.checked)), inipath)
        Form1.showMinimizedInTaskbar = sender.checked
    End Sub

    Private Sub buttonResetWinPos_Click(sender As Object, e As EventArgs) Handles buttonResetWinPos.Click
        Form1.WindowState = FormWindowState.Normal
        Form1.Size = New Size(Form1.minWidth, Form1.minHeight)
        Form1.Location = New Point(My.Computer.Screen.WorkingArea.Width / 2 - Form1.Width / 2, My.Computer.Screen.WorkingArea.Height / 2 - Form1.Height / 2)

    End Sub

    Private Sub checkAutostart_CheckedChanged(sender As Object, e As EventArgs) Handles checkAutostart.CheckedChanged
        changes()
    End Sub

    Private Sub checkAutostart_Click(sender As Object, e As EventArgs) Handles checkAutostart.Click
        If sender.checked Then
            If Not Form1.registerAutostart() Then
                MsgBox("Failed to insert GTimer into Autostart. Please do it manually." & vbNewLine & "Registry Subkey:" & vbNewLine & AUTOSTART_REGISTRY_KEY)
                sender.checked = False
            End If
        Else
            If Form1.registryAutostartExists() Then
                If Not Form1.unregisterAutostart() Then
                    MsgBox("Failed to remove GTimer from Autostart. Please do it manually." & vbNewLine & "Registry Subkey:" & vbNewLine & AUTOSTART_REGISTRY_KEY)
                    sender.checked = True
                End If
            End If

        End If
        Form1.autostartEnabled = sender.checked
        dll.iniWriteValue("Config", "autostart", Math.Abs(CInt(sender.checked)), inipath)
    End Sub

    Private Sub comboStartState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboStartState.SelectedIndexChanged
        dll.iniWriteValue("Config", "startState", comboStartState.SelectedIndex)
    End Sub

    Private Sub comboStartState_event(sender As Object, e As EventArgs) Handles comboStartState.DropDown
        changes()
    End Sub

    Private Sub exportPic_Click(sender As Object, e As EventArgs) Handles exportPic.Click
        MsgBox("Coming soon")
    End Sub

    Private Sub checkShowInTaskbar_CheckedChanged(sender As Object, e As EventArgs) Handles checkShowInTaskbar.Click, checkAutoUpdate.Click
        changes()
    End Sub



    Private Sub saveButton_Click(sender As Object, e As EventArgs) Handles saveButton.Click
        If Not saveChanges() Then
            saveButton.Enabled = False
            MsgBox("Options saved!!!", MsgBoxStyle.Information)

        End If
    End Sub



    Sub changes()
        If Not saveButton.Enabled Then
            saveButton.Enabled = True
            changesPending = True
        End If
    End Sub

    Private Sub textSharedFolder_TextChanged(sender As Object, e As EventArgs) Handles textSharedFolder.KeyPress
        changes()
    End Sub


#End Region

#Region "GAMES"

    Sub fillGameList()
        listGames.Items.Clear()
        Dim meUser As User = User.getMe()
        If meUser IsNot Nothing Then
            For Each g In meUser.games
                listGames.Items.Add(g)
            Next
        End If

    End Sub

    Sub selectGameByArgument()
        If args.Count > 0 Then
            For i = 0 To listGames.Items.Count
                If listGames.Items(i).id = args(0) Then
                    listGames.SelectedIndex = i
                    Return
                End If
            Next
        End If
    End Sub

    Private Sub addGameButton_Click(sender As Object, e As EventArgs) Handles addGameButton.Click
1:      Dim id As String = InputBox("Type in personal game id." & vbNewLine & vbNewLine & "Note: The id will not be visible in the UI")
        If id IsNot Nothing AndAlso id <> "" AndAlso Not String.IsNullOrWhiteSpace(id) And Not id.ToLower() = "config" Then
            Dim user As User = User.getMe()
            If user IsNot Nothing Then
                saveChanges()
                For Each g As Game In user.games
                    If id.ToLower() = g.section.ToLower() Then
                        MsgBox("Id '" & id & "' is already taken. Please choose another one", MsgBoxStyle.Information)
                        GoTo 1
                    End If
                Next
                dll.iniWriteValue(id, "name", "", inipath)
                Form1.reloadAll()

                user = User.getByName(user.name)
                If user Is Nothing Then
                    MsgBox("Please restart GTimer", MsgBoxStyle.Exclamation)
                    Return
                End If
                Dim newGame As Game = user.getGameBySection(id)
                If newGame Is Nothing Then
                    MsgBox("Reloading failed. Please restart GTimer.", MsgBoxStyle.Exclamation)
                    Return
                End If

                listGames.Items.Add(newGame)
                listGames.SelectedItem = newGame

            End If
        End If
    End Sub

    Private Sub listGames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listGames.SelectedIndexChanged
        If listGames.SelectedIndex > -1 Then
            toggleGameInfoVisible(True)
            loadGameInfo(listGames.SelectedItem)
        Else
            toggleGameInfoVisible(False)
        End If
    End Sub

    Sub toggleGameInfoVisible(toState As Boolean)
        gameIdLabel.Visible = toState
        gameIdText.Visible = toState
        gameNameText.Visible = toState
        gameNameLabel.Visible = toState
        gameExeNameText.Visible = toState
        gameExeLabel.Visible = toState
        gamePicText.Visible = toState
        gamePicInvText.Visible = toState
        gamePic.Visible = toState
        gamePicInv.Visible = toState
        deleteGameButton.Visible = toState
        exeChooseButton.Visible = toState
        checkIncludeGame.Visible = toState
        gameLocationChooseButton.Visible = toState
        gameLocationLabel.Visible = toState
        gameLocationRunButton.Visible = toState
        gameLocationText.Visible = toState
    End Sub

    Sub loadGameInfo(game As Game)


        gameIdText.Text = game.section
        gameNameText.Text = game.name
        gameExeNameText.Text = game.exe

        gamePicText.Text = game.logoPath
        gamePicInvText.Text = game.logoInvPath

        setPicImage(game.logoPath)
        setPicInvImage(game.logoInvPath)

        checkIncludeGame.Checked = game.include

        gameLocationText.Text = game.locationStartExe
    End Sub

    Sub setPicImage(logoPath As String)
        Dim path As String = logoPath
        If Not path.Contains(":\") Then path = Form1.resPath & logoPath
        If IO.File.Exists(path) Then
            gamePic.BackgroundImageLayout = ImageLayout.Stretch
            Try
                gamePic.BackgroundImage = Image.FromFile(path)
            Catch ex As Exception
                gamePic.BackgroundImageLayout = ImageLayout.Center
                gamePic.BackgroundImage = gamePic.ErrorImage
            End Try
        Else
            gamePic.BackgroundImageLayout = ImageLayout.Center
            gamePic.BackgroundImage = gamePic.ErrorImage
        End If
    End Sub

    Sub setPicInvImage(logoPath As String)
        Dim path As String = logoPath
        If Not path.Contains(":\") Then path = Form1.resPath & logoPath
        If IO.File.Exists(path) Then
            gamePicInv.BackgroundImageLayout = ImageLayout.Stretch
            Try
                gamePicInv.BackgroundImage = Image.FromFile(path)
            Catch ex As Exception
                gamePicInv.BackgroundImageLayout = ImageLayout.Center
                gamePicInv.BackgroundImage = gamePic.ErrorImage
            End Try
        Else
            gamePicInv.BackgroundImageLayout = ImageLayout.Center
            gamePicInv.BackgroundImage = gamePic.ErrorImage
        End If
    End Sub

    Private Sub exeChooseButton_Click(sender As Object, e As EventArgs) Handles exeChooseButton.Click
        Dim file As String = getFileDialog(, "exe")
        If Not file = "" Then
            gameExeNameText.Text = file.Replace(".exe", "").Substring(file.LastIndexOf("\") + 1)
            changes()
        End If
    End Sub

    Private Sub gameLocationChooseButton_Click(sender As Object, e As EventArgs) Handles gameLocationChooseButton.Click
        Dim file As String = getFileDialog(, "exe")
        If Not file = "" Then
            gameLocationText.Text = file
            changes()
        End If
    End Sub

    Private Sub gameLocationRunButton_Click(sender As Object, e As EventArgs) Handles gameLocationRunButton.Click
        startGameWithPrompt(gameLocationText.Text)
    End Sub

    Public Function startGameWithPrompt(gameExePath As String) As Integer
        Dim res As Integer = startGame(gameExePath)
        If res = 1 Then
            MsgBox("Failed to start program. Check if the path to the executable exists and that you have permission to execute the file.", MsgBoxStyle.Critical)
        ElseIf res = 2 Then
            MsgBox("No startup file has been set. Please enter the complete path into the 'Start' field.", MsgBoxStyle.Exclamation)
        ElseIf res = 3 Then
            MsgBox("The file to execute must end with '.exe'. or be a valid steam link.", MsgBoxStyle.Exclamation)
        End If
        Return res
    End Function
    Public Function startGame(gameExePath As String) As Integer
        If gameExePath.EndsWith(".exe") Or gameExePath.Contains("://") Then
            Try
                Process.Start(gameExePath)
                Return 0
            Catch ex As Exception
                Return 1
            End Try
        ElseIf gameExePath = "" Then
            Return 2
        Else
            Return 3
        End If
    End Function

    Public Sub openGameSettings(game As Game)
        state = OptionsForm.optionState.GAMES
        arguments = {game.id}
        Show()
    End Sub

    Private Sub gamePic_Click(sender As Object, e As EventArgs) Handles gamePic.Click
        Dim currPath As String = gamePicText.Text
        If Not currPath.Contains(":\") Then currPath = Form1.resPath & gamePicText.Text
        Dim file As String = getFileDialog(currPath)
        If Not file = "" Then
            If Not Path.GetDirectoryName(file) & "\" = Form1.resPath Then
                If IO.File.Exists(Form1.resPath & Path.GetFileName(file)) Then
                    If MsgBox("File already exists. Overwrite existing file?", MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation) = MsgBoxResult.No Then
                        Return
                    End If
                End If
                IO.File.Copy(file, Form1.resPath & Path.GetFileName(file), True)
            End If
            gamePicText.Text = Path.GetFileName(file)
            setPicImage(gamePicText.Text)
            changes()
        End If
    End Sub

    Private Sub gamePicInv_Click(sender As Object, e As EventArgs) Handles gamePicInv.Click
        Dim currPath As String = gamePicInvText.Text
        If Not currPath.Contains(":\") Then currPath = Form1.resPath & gamePicInvText.Text
        Dim file As String = getFileDialog(currPath)
        If Not file = "" Then
            If Not Path.GetDirectoryName(file) & "\" = Form1.resPath Then
                If IO.File.Exists(Form1.resPath & Path.GetFileName(file)) Then
                    If MsgBox("File already exists. Overwrite existing file?", MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation) = MsgBoxResult.No Then
                        Return
                    End If
                End If
                IO.File.Copy(file, Form1.resPath & Path.GetFileName(file), True)
            End If
            gamePicInvText.Text = Path.GetFileName(file)
            setPicInvImage(gamePicInvText.Text)
            changes()
        End If
    End Sub

    Private Sub deleteGameButton_Click(sender As Object, e As EventArgs) Handles deleteGameButton.Click
        If listGames.SelectedIndex > -1 Then
            Dim game As Game = listGames.SelectedItem
            If game.active Then
                MsgBox("Game is active. Please close the game and try again.", MsgBoxStyle.Exclamation)
                Return
            End If
            If MsgBox("Deleting the game will erase all game time for this game. Are you sure to continue?", MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then
                Form1.tracker.Stop()
                game.todayTimeTemp = 0
                dll.iniDeleteSection(game.section)
                listGames.Items.RemoveAt(listGames.SelectedIndex)
                changes()
                Form1.reloadAll()
                Form1.tracker.Start()
            End If
        End If
    End Sub

    Private Sub gameNameText_TextChanged(sender As Object, e As EventArgs) Handles gameNameText.KeyPress, gameExeNameText.KeyPress, gamePicText.KeyPress, gamePicInvText.KeyPress, gameLocationText.KeyPress
        changes()
    End Sub

    Private Sub checkIncludeGame_Click(sender As Object, e As EventArgs) Handles checkIncludeGame.Click
        changes()
    End Sub

    Private Sub gameNameText_TextChanged(sender As Object, e As KeyPressEventArgs) Handles gamePicText.KeyPress, gamePicInvText.KeyPress, gameNameText.KeyPress, gameExeNameText.KeyPress, gameLocationText.KeyPress

    End Sub





#End Region

#Region "SORTING"

    Public Sub loadCurrSorting()
        If sortingRad1.Checked Then
            sortCombo.SelectedIndex = Form1.primarySort
        Else
            sortCombo.SelectedIndex = Form1.secondarySort
        End If

        toggleSortGameList(False)
        If sortCombo.SelectedIndex = Form1.SortingMethod.MANUAL Then
            toggleSortGameList(True)
        End If
    End Sub

    Sub toggleSortGameList(toState As Boolean)
        listSortGames.Visible = toState
        sortDownButton.Visible = toState
        sortUpButton.Visible = toState
        If toState Then
            fillSortGameList()
        End If
    End Sub

    Sub fillSortGameList()
        listSortGames.Items.Clear()
        Dim meUser As User = User.getMe()

        If meUser IsNot Nothing Then
            Dim iniVal As String = dll.iniReadValue("Config", "sortingOrder", "", inipath)
            If Not iniVal = "" Then
                Dim split() As String = iniVal.Split(";")
                If split IsNot Nothing Then
                    For Each val As String In split
                        Dim game As Game = meUser.getGameBySection(val)
                        If game IsNot Nothing Then
                            listSortGames.Items.Add(game)
                        End If
                    Next
                End If
            End If

            For Each g In meUser.games
                If Not listSortGames.Items.Contains(g) Then
                    listSortGames.Items.Add(g)
                End If
            Next
        End If

    End Sub
    Private Sub sortingRad_CheckedChanged(sender As Object, e As EventArgs) Handles sortingRad1.Click, sortingRad2.Click
        loadCurrSorting()
        changes()
    End Sub

    Private Sub sortCombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles sortCombo.SelectedIndexChanged
        If sortCombo.SelectedIndex > -1 Then
            If sortingRad1.Checked Then
                Form1.primarySort = sortCombo.SelectedIndex
            Else
                Form1.secondarySort = sortCombo.SelectedIndex
            End If

            dll.iniWriteValue("Config", "primarySort", Form1.primarySort, inipath)
            dll.iniWriteValue("Config", "secondarySort", Form1.secondarySort, inipath)

            toggleSortGameList(False)
            If sortCombo.SelectedIndex = Form1.SortingMethod.MANUAL Then
                toggleSortGameList(True)
            End If
        End If
    End Sub

    Private Sub sortCombo_DropDownClosed(sender As Object, e As EventArgs) Handles sortCombo.DropDownClosed
        changes()
    End Sub

    Private Sub sortUpButton_Click(sender As Object, e As EventArgs) Handles sortUpButton.Click
        If listSortGames.SelectedIndex > 0 Then
            Dim temp As Integer = listSortGames.SelectedIndex
            Dim tempGame As Game = listSortGames.SelectedItem
            listSortGames.Items.RemoveAt(temp)
            listSortGames.Items.Insert(temp - 1, tempGame)
            listSortGames.SelectedIndex = temp - 1
            saveManualSortOrder()
            changes()
        End If
    End Sub

    Private Sub sortDownButton_Click(sender As Object, e As EventArgs) Handles sortDownButton.Click
        If listSortGames.SelectedIndex > -1 And listSortGames.SelectedIndex < listSortGames.Items.Count - 1 Then
            Dim temp As Integer = listSortGames.SelectedIndex
            Dim tempGame As Game = listSortGames.SelectedItem
            listSortGames.Items.RemoveAt(temp)
            listSortGames.Items.Insert(temp + 1, tempGame)
            listSortGames.SelectedIndex = temp + 1
            saveManualSortOrder()
            changes()
        End If
    End Sub

    Sub saveManualSortOrder()
        Dim iniValue = ""
        For i = 0 To listSortGames.Items.Count - 1
            iniValue &= listSortGames.Items(i).section
            If i < listSortGames.Items.Count - 1 Then
                iniValue &= ";"
            End If
        Next
        dll.iniWriteValue("Config", "sortingOrder", iniValue, inipath)
    End Sub

    Private Sub OptionsForm_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus

    End Sub

#End Region

#Region "INVITATIONS"



    Sub fillBlacklist()
        listInviteBlacklist.Items.Clear()
        If Form1.inviteBlacklist IsNot Nothing Then
            For Each user As String In Form1.inviteBlacklist
                listInviteBlacklist.Items.Add(user)
            Next
        End If
    End Sub


    Private Sub checkAllowInvites_Click(sender As Object, e As EventArgs) Handles checkAllowInvites.Click
        dll.iniWriteValue("Config", "invitesAllowed", Math.Abs(CInt(sender.checked)), inipath)
        Form1.invitesAllowed = sender.checked
    End Sub


    Private Sub checkInviteFlash_Click(sender As Object, e As EventArgs) Handles checkInviteFlash.Click
        dll.iniWriteValue("Config", "inviteFlash", Math.Abs(CInt(sender.checked)), inipath)
        Form1.inviteFlashEnabled = sender.checked
    End Sub

    Private Sub inviteBlacklistAddButton_Click(sender As Object, e As EventArgs) Handles inviteBlacklistAddButton.Click
        Dim input As String = InputBox("Type in name of person you automatically reject invites from.")
        If input <> "" Then
            For Each s As String In listInviteBlacklist.Items
                If s.ToLower() = input.ToLower() Then
                    Return
                End If
            Next
            listInviteBlacklist.Items.Add(input)
            If Form1.inviteBlacklist Is Nothing Then
                Form1.inviteBlacklist = New List(Of String)
            End If
            Form1.inviteBlacklist.Add(input)
            changes()
        End If
    End Sub

    Private Sub inviteBlacklistRemButton_Click(sender As Object, e As EventArgs) Handles inviteBlacklistRemButton.Click
        If listInviteBlacklist.SelectedIndex > -1 Then
            Form1.inviteBlacklist.Remove(listInviteBlacklist.SelectedItem)
            '  Dim prev As String = dll.iniReadValue("Config", "inviteBlacklist")
            '  prev = prev.Replace(listInviteBlacklist.SelectedItem, "")
            '  prev = prev.Replace(";;", ";")
            '  If prev.StartsWith(";") Then prev = prev.Substring(1)
            '  If prev.EndsWith(";") Then prev = prev.Substring(0, prev.Length - 1)
            '  dll.iniWriteValue("Config", "inviteBlacklist", prev)
            listInviteBlacklist.Items.Remove(listInviteBlacklist.SelectedItem)
            changes()
        End If
    End Sub

    Private Sub numInviteTimeout_ValueChanged(sender As Object, e As EventArgs) Handles numInviteTimeout.ValueChanged, checkAllowInvites.CheckedChanged, checkInviteFlash.CheckedChanged
        changes()
    End Sub

    Private Sub checkAutostart_CheckedChanged_1(sender As Object, e As EventArgs) Handles checkAutostart.CheckedChanged

    End Sub


#End Region
End Class