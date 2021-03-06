﻿Imports System.Reflection
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

    Public arguments() As String
    Public state As optionState

    Public coreFiles As List(Of String)

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
            Case optionState.NONE
                MsgBox("No option mode selected")
        End Select
        Cursor = Cursors.Default
    End Sub

    Function saveChanges() As Boolean 'true if form must not be closed
        dll.inipath = inipath
        Select Case state
            Case optionState.UPDATE
                updateSharedPath()

        End Select
        Return False
    End Function
    Private Sub OptionsForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If saveChanges() Then
            e.Cancel = True
            Return
        End If
        Form1.lastOptionsState = state
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
            Case Else : Return -1
        End Select
    End Function
    Sub selectIndex(ByVal state As optionState)
        listMenu.SelectedIndex = stateToIndex(state)
    End Sub


#Region "UI"

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


#End Region

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
    Function isValidFilePath(ByVal s As String, Optional ByVal ext As String = "") As Boolean
        Return s.Length > 3 AndAlso Not s.Contains("\\") AndAlso Not s.EndsWith(" ") AndAlso Not s.StartsWith(" ") AndAlso s.Substring(1, 2) = ":\" AndAlso Not s.EndsWith("\") AndAlso s.Contains("\") AndAlso Not s.EndsWith(".") AndAlso IIf(ext = "", True, s.EndsWith("." & ext))
    End Function

    Function isValidDirectoryPath(ByVal s As String) As Boolean
        Return s.Length >= 3 AndAlso Not s.Contains("\\") AndAlso Not s.EndsWith(" ") AndAlso Not s.StartsWith(" ") AndAlso s.Substring(1, 2) = ":\" AndAlso s.EndsWith("\")
    End Function

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

    Private Sub checkSavePos_CheckedChanged(sender As Object, e As EventArgs) Handles checkSavePos.CheckedChanged
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
                MsgBox("Failed to insert GTimer into Autostart. Please do it manually." & vbNewLine & "Registry Subkey:" & vbNewLine & "Software\Microsoft\Windows\CurrentVersion\Run")
                sender.checked = False
            End If
        Else
            If Form1.registryAutostartExists() Then
                If Not Form1.unregisterAutostart() Then
                    MsgBox("Failed to remove GTimer from Autostart. Please do it manually." & vbNewLine & "Registry Subkey:" & vbNewLine & "Software\Microsoft\Windows\CurrentVersion\Run")
                    sender.checked = True
                End If
            End If

        End If
        Form1.autostartEnabled = sender.checked
        dll.iniWriteValue("Config", "autostart", Math.Abs(CInt(sender.checked)), inipath)
    End Sub

    Private Sub comboStartState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboStartState.SelectedIndexChanged
        dll.iniWriteValue("Config", "startState", comboStartState.SelectedIndex)
        changes()
    End Sub

    Private Sub exportPic_Click(sender As Object, e As EventArgs) Handles exportPic.Click
        MsgBox("Coming soon")
    End Sub

    Private Sub checkShowInTaskbar_CheckedChanged(sender As Object, e As EventArgs) Handles checkShowInTaskbar.CheckedChanged
        changes()
    End Sub

    Private Sub checkAutoUpdate_CheckedChanged(sender As Object, e As EventArgs) Handles checkAutoUpdate.CheckedChanged

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
        End If
    End Sub

    Private Sub textSharedFolder_TextChanged(sender As Object, e As EventArgs) Handles textSharedFolder.TextChanged
        changes()
    End Sub


#End Region

End Class