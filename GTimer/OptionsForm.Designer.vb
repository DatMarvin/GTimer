<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OptionsForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptionsForm))
        Me.g1 = New System.Windows.Forms.GroupBox()
        Me.checkAutoUpdate = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.sharedFolderButton = New System.Windows.Forms.Button()
        Me.textSharedFolder = New System.Windows.Forms.TextBox()
        Me.labelLatestVersion = New System.Windows.Forms.Label()
        Me.groupVersion = New System.Windows.Forms.GroupBox()
        Me.listPublish = New System.Windows.Forms.ListBox()
        Me.publishButton = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.labelPublishedVersion = New System.Windows.Forms.Label()
        Me.publishAddButton = New System.Windows.Forms.Button()
        Me.publishRemButton = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DownloadLatestButton = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.labelCurrVersion = New System.Windows.Forms.Label()
        Me.listMenu = New System.Windows.Forms.ListBox()
        Me.g2 = New System.Windows.Forms.GroupBox()
        Me.exportPicLabel = New System.Windows.Forms.Label()
        Me.importPicLabel = New System.Windows.Forms.Label()
        Me.exportPic = New System.Windows.Forms.PictureBox()
        Me.importPic = New System.Windows.Forms.PictureBox()
        Me.g3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.checkAutostart = New System.Windows.Forms.CheckBox()
        Me.comboStartState = New System.Windows.Forms.ComboBox()
        Me.windowStateLabel = New System.Windows.Forms.Label()
        Me.groupUI = New System.Windows.Forms.GroupBox()
        Me.checkShowInTaskbar = New System.Windows.Forms.CheckBox()
        Me.fontLabel = New System.Windows.Forms.Label()
        Me.buttonFont = New System.Windows.Forms.Button()
        Me.labelWinPosString = New System.Windows.Forms.Label()
        Me.buttonResetWinPos = New System.Windows.Forms.Button()
        Me.checkSavePos = New System.Windows.Forms.CheckBox()
        Me.labelWinSize = New System.Windows.Forms.Label()
        Me.labelWinSizeString = New System.Windows.Forms.Label()
        Me.labelWinPos = New System.Windows.Forms.Label()
        Me.saveButton = New System.Windows.Forms.Button()
        Me.g4 = New System.Windows.Forms.GroupBox()
        Me.gameLocationRunButton = New System.Windows.Forms.Button()
        Me.gameLocationChooseButton = New System.Windows.Forms.Button()
        Me.gameLocationText = New System.Windows.Forms.TextBox()
        Me.gameLocationLabel = New System.Windows.Forms.Label()
        Me.checkIncludeGame = New System.Windows.Forms.CheckBox()
        Me.gamePicInvText = New System.Windows.Forms.TextBox()
        Me.gamePicText = New System.Windows.Forms.TextBox()
        Me.exeChooseButton = New System.Windows.Forms.Button()
        Me.listGames = New System.Windows.Forms.ListBox()
        Me.gameExeNameText = New System.Windows.Forms.TextBox()
        Me.gameExeLabel = New System.Windows.Forms.Label()
        Me.gamePicInv = New System.Windows.Forms.PictureBox()
        Me.gamePic = New System.Windows.Forms.PictureBox()
        Me.gameNameText = New System.Windows.Forms.TextBox()
        Me.gameNameLabel = New System.Windows.Forms.Label()
        Me.gameIdLabel = New System.Windows.Forms.Label()
        Me.gameIdText = New System.Windows.Forms.TextBox()
        Me.deleteGameButton = New System.Windows.Forms.Button()
        Me.addGameButton = New System.Windows.Forms.Button()
        Me.currentGamesLabel = New System.Windows.Forms.Label()
        Me.reloadAllButton = New System.Windows.Forms.PictureBox()
        Me.g5 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.listSortGames = New System.Windows.Forms.ListBox()
        Me.sortUpButton = New System.Windows.Forms.Button()
        Me.sortCombo = New System.Windows.Forms.ComboBox()
        Me.sortDownButton = New System.Windows.Forms.Button()
        Me.sortComboLabel = New System.Windows.Forms.Label()
        Me.sortingRad2 = New System.Windows.Forms.RadioButton()
        Me.sortingRad1 = New System.Windows.Forms.RadioButton()
        Me.g6 = New System.Windows.Forms.GroupBox()
        Me.inviteTimeoutLabel = New System.Windows.Forms.Label()
        Me.numInviteTimeout = New System.Windows.Forms.NumericUpDown()
        Me.checkInviteFlash = New System.Windows.Forms.CheckBox()
        Me.inviteBlacklistAddButton = New System.Windows.Forms.Button()
        Me.inviteBlacklistRemButton = New System.Windows.Forms.Button()
        Me.inviteBlacklistLabel = New System.Windows.Forms.Label()
        Me.listInviteBlacklist = New System.Windows.Forms.ListBox()
        Me.checkAllowInvites = New System.Windows.Forms.CheckBox()
        Me.g1.SuspendLayout()
        Me.groupVersion.SuspendLayout()
        Me.g2.SuspendLayout()
        CType(Me.exportPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.importPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.g3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.groupUI.SuspendLayout()
        Me.g4.SuspendLayout()
        CType(Me.gamePicInv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gamePic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.reloadAllButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.g5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.g6.SuspendLayout()
        CType(Me.numInviteTimeout, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'g1
        '
        Me.g1.Controls.Add(Me.checkAutoUpdate)
        Me.g1.Controls.Add(Me.Label1)
        Me.g1.Controls.Add(Me.sharedFolderButton)
        Me.g1.Controls.Add(Me.textSharedFolder)
        Me.g1.Controls.Add(Me.labelLatestVersion)
        Me.g1.Controls.Add(Me.groupVersion)
        Me.g1.Controls.Add(Me.Label7)
        Me.g1.Controls.Add(Me.DownloadLatestButton)
        Me.g1.Controls.Add(Me.Label12)
        Me.g1.Controls.Add(Me.labelCurrVersion)
        Me.g1.Location = New System.Drawing.Point(121, 33)
        Me.g1.Name = "g1"
        Me.g1.Size = New System.Drawing.Size(302, 256)
        Me.g1.TabIndex = 27
        Me.g1.TabStop = False
        Me.g1.Text = "Tracker Update"
        '
        'checkAutoUpdate
        '
        Me.checkAutoUpdate.AutoSize = True
        Me.checkAutoUpdate.Location = New System.Drawing.Point(104, 99)
        Me.checkAutoUpdate.Name = "checkAutoUpdate"
        Me.checkAutoUpdate.Size = New System.Drawing.Size(86, 17)
        Me.checkAutoUpdate.TabIndex = 29
        Me.checkAutoUpdate.Text = "Auto Update"
        Me.checkAutoUpdate.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 68
        Me.Label1.Text = "OneDrive Directory:"
        '
        'sharedFolderButton
        '
        Me.sharedFolderButton.Location = New System.Drawing.Point(262, 135)
        Me.sharedFolderButton.Name = "sharedFolderButton"
        Me.sharedFolderButton.Size = New System.Drawing.Size(26, 22)
        Me.sharedFolderButton.TabIndex = 64
        Me.sharedFolderButton.Text = "..."
        Me.sharedFolderButton.UseVisualStyleBackColor = True
        '
        'textSharedFolder
        '
        Me.textSharedFolder.Location = New System.Drawing.Point(14, 136)
        Me.textSharedFolder.Name = "textSharedFolder"
        Me.textSharedFolder.Size = New System.Drawing.Size(247, 20)
        Me.textSharedFolder.TabIndex = 67
        '
        'labelLatestVersion
        '
        Me.labelLatestVersion.AutoSize = True
        Me.labelLatestVersion.Location = New System.Drawing.Point(181, 39)
        Me.labelLatestVersion.Name = "labelLatestVersion"
        Me.labelLatestVersion.Size = New System.Drawing.Size(28, 13)
        Me.labelLatestVersion.TabIndex = 64
        Me.labelLatestVersion.Text = "v2.0"
        '
        'groupVersion
        '
        Me.groupVersion.Controls.Add(Me.listPublish)
        Me.groupVersion.Controls.Add(Me.publishButton)
        Me.groupVersion.Controls.Add(Me.Label8)
        Me.groupVersion.Controls.Add(Me.labelPublishedVersion)
        Me.groupVersion.Controls.Add(Me.publishAddButton)
        Me.groupVersion.Controls.Add(Me.publishRemButton)
        Me.groupVersion.Location = New System.Drawing.Point(5, 156)
        Me.groupVersion.Name = "groupVersion"
        Me.groupVersion.Size = New System.Drawing.Size(292, 88)
        Me.groupVersion.TabIndex = 63
        Me.groupVersion.TabStop = False
        '
        'listPublish
        '
        Me.listPublish.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listPublish.FormattingEnabled = True
        Me.listPublish.HorizontalScrollbar = True
        Me.listPublish.Location = New System.Drawing.Point(6, 13)
        Me.listPublish.Name = "listPublish"
        Me.listPublish.Size = New System.Drawing.Size(128, 69)
        Me.listPublish.TabIndex = 28
        '
        'publishButton
        '
        Me.publishButton.Location = New System.Drawing.Point(187, 49)
        Me.publishButton.Name = "publishButton"
        Me.publishButton.Size = New System.Drawing.Size(66, 27)
        Me.publishButton.TabIndex = 49
        Me.publishButton.Text = "Publish"
        Me.publishButton.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(165, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(117, 13)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "Publish current version:"
        '
        'labelPublishedVersion
        '
        Me.labelPublishedVersion.AutoSize = True
        Me.labelPublishedVersion.Location = New System.Drawing.Point(204, 33)
        Me.labelPublishedVersion.Name = "labelPublishedVersion"
        Me.labelPublishedVersion.Size = New System.Drawing.Size(28, 13)
        Me.labelPublishedVersion.TabIndex = 61
        Me.labelPublishedVersion.Text = "v2.0"
        '
        'publishAddButton
        '
        Me.publishAddButton.Location = New System.Drawing.Point(136, 20)
        Me.publishAddButton.Name = "publishAddButton"
        Me.publishAddButton.Size = New System.Drawing.Size(25, 25)
        Me.publishAddButton.TabIndex = 48
        Me.publishAddButton.Text = "+"
        Me.publishAddButton.UseVisualStyleBackColor = True
        '
        'publishRemButton
        '
        Me.publishRemButton.Location = New System.Drawing.Point(136, 47)
        Me.publishRemButton.Name = "publishRemButton"
        Me.publishRemButton.Size = New System.Drawing.Size(25, 25)
        Me.publishRemButton.TabIndex = 62
        Me.publishRemButton.Text = "-"
        Me.publishRemButton.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(161, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 13)
        Me.Label7.TabIndex = 50
        Me.Label7.Text = "Latest version:"
        '
        'DownloadLatestButton
        '
        Me.DownloadLatestButton.Location = New System.Drawing.Point(103, 62)
        Me.DownloadLatestButton.Name = "DownloadLatestButton"
        Me.DownloadLatestButton.Size = New System.Drawing.Size(75, 36)
        Me.DownloadLatestButton.TabIndex = 48
        Me.DownloadLatestButton.Text = "Download && Install"
        Me.DownloadLatestButton.UseMnemonic = False
        Me.DownloadLatestButton.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(46, 19)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(81, 13)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Current version:"
        '
        'labelCurrVersion
        '
        Me.labelCurrVersion.AutoSize = True
        Me.labelCurrVersion.Location = New System.Drawing.Point(70, 39)
        Me.labelCurrVersion.Name = "labelCurrVersion"
        Me.labelCurrVersion.Size = New System.Drawing.Size(28, 13)
        Me.labelCurrVersion.TabIndex = 20
        Me.labelCurrVersion.Text = "v2.0"
        '
        'listMenu
        '
        Me.listMenu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listMenu.FormattingEnabled = True
        Me.listMenu.HorizontalScrollbar = True
        Me.listMenu.ItemHeight = 16
        Me.listMenu.Items.AddRange(New Object() {"Version Update", "Configuration", "Window", "Games", "Sorting", "Invitations"})
        Me.listMenu.Location = New System.Drawing.Point(4, 11)
        Me.listMenu.Name = "listMenu"
        Me.listMenu.Size = New System.Drawing.Size(111, 148)
        Me.listMenu.TabIndex = 64
        '
        'g2
        '
        Me.g2.Controls.Add(Me.exportPicLabel)
        Me.g2.Controls.Add(Me.importPicLabel)
        Me.g2.Controls.Add(Me.exportPic)
        Me.g2.Controls.Add(Me.importPic)
        Me.g2.Location = New System.Drawing.Point(445, 33)
        Me.g2.Name = "g2"
        Me.g2.Size = New System.Drawing.Size(302, 256)
        Me.g2.TabIndex = 64
        Me.g2.TabStop = False
        Me.g2.Text = "Configuration"
        '
        'exportPicLabel
        '
        Me.exportPicLabel.AutoSize = True
        Me.exportPicLabel.Location = New System.Drawing.Point(178, 38)
        Me.exportPicLabel.Name = "exportPicLabel"
        Me.exportPicLabel.Size = New System.Drawing.Size(37, 13)
        Me.exportPicLabel.TabIndex = 65
        Me.exportPicLabel.Text = "Export"
        '
        'importPicLabel
        '
        Me.importPicLabel.AutoSize = True
        Me.importPicLabel.Location = New System.Drawing.Point(81, 38)
        Me.importPicLabel.Name = "importPicLabel"
        Me.importPicLabel.Size = New System.Drawing.Size(36, 13)
        Me.importPicLabel.TabIndex = 64
        Me.importPicLabel.Text = "Import"
        '
        'exportPic
        '
        Me.exportPic.BackgroundImage = CType(resources.GetObject("exportPic.BackgroundImage"), System.Drawing.Image)
        Me.exportPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.exportPic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.exportPic.Location = New System.Drawing.Point(171, 58)
        Me.exportPic.Name = "exportPic"
        Me.exportPic.Size = New System.Drawing.Size(50, 50)
        Me.exportPic.TabIndex = 1
        Me.exportPic.TabStop = False
        '
        'importPic
        '
        Me.importPic.BackgroundImage = CType(resources.GetObject("importPic.BackgroundImage"), System.Drawing.Image)
        Me.importPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.importPic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.importPic.Location = New System.Drawing.Point(74, 58)
        Me.importPic.Name = "importPic"
        Me.importPic.Size = New System.Drawing.Size(50, 50)
        Me.importPic.TabIndex = 0
        Me.importPic.TabStop = False
        '
        'g3
        '
        Me.g3.Controls.Add(Me.GroupBox1)
        Me.g3.Controls.Add(Me.groupUI)
        Me.g3.Location = New System.Drawing.Point(753, 33)
        Me.g3.Name = "g3"
        Me.g3.Size = New System.Drawing.Size(302, 256)
        Me.g3.TabIndex = 66
        Me.g3.TabStop = False
        Me.g3.Text = "Window Settings"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.checkAutostart)
        Me.GroupBox1.Controls.Add(Me.comboStartState)
        Me.GroupBox1.Controls.Add(Me.windowStateLabel)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 144)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(296, 94)
        Me.GroupBox1.TabIndex = 68
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Startup"
        '
        'checkAutostart
        '
        Me.checkAutostart.AutoSize = True
        Me.checkAutostart.Location = New System.Drawing.Point(5, 23)
        Me.checkAutostart.Name = "checkAutostart"
        Me.checkAutostart.Size = New System.Drawing.Size(189, 17)
        Me.checkAutostart.TabIndex = 27
        Me.checkAutostart.Text = "Start GTimer when Windows starts"
        Me.checkAutostart.UseVisualStyleBackColor = True
        '
        'comboStartState
        '
        Me.comboStartState.FormattingEnabled = True
        Me.comboStartState.Items.AddRange(New Object() {"Minimized", "Normal", "Maximized"})
        Me.comboStartState.Location = New System.Drawing.Point(87, 50)
        Me.comboStartState.Name = "comboStartState"
        Me.comboStartState.Size = New System.Drawing.Size(121, 21)
        Me.comboStartState.TabIndex = 28
        '
        'windowStateLabel
        '
        Me.windowStateLabel.AutoSize = True
        Me.windowStateLabel.Location = New System.Drawing.Point(6, 53)
        Me.windowStateLabel.Name = "windowStateLabel"
        Me.windowStateLabel.Size = New System.Drawing.Size(77, 13)
        Me.windowStateLabel.TabIndex = 27
        Me.windowStateLabel.Text = "Window State:"
        '
        'groupUI
        '
        Me.groupUI.Controls.Add(Me.checkShowInTaskbar)
        Me.groupUI.Controls.Add(Me.fontLabel)
        Me.groupUI.Controls.Add(Me.buttonFont)
        Me.groupUI.Controls.Add(Me.labelWinPosString)
        Me.groupUI.Controls.Add(Me.buttonResetWinPos)
        Me.groupUI.Controls.Add(Me.checkSavePos)
        Me.groupUI.Controls.Add(Me.labelWinSize)
        Me.groupUI.Controls.Add(Me.labelWinSizeString)
        Me.groupUI.Controls.Add(Me.labelWinPos)
        Me.groupUI.Location = New System.Drawing.Point(3, 13)
        Me.groupUI.Name = "groupUI"
        Me.groupUI.Size = New System.Drawing.Size(296, 130)
        Me.groupUI.TabIndex = 67
        Me.groupUI.TabStop = False
        '
        'checkShowInTaskbar
        '
        Me.checkShowInTaskbar.AutoSize = True
        Me.checkShowInTaskbar.Location = New System.Drawing.Point(5, 53)
        Me.checkShowInTaskbar.Name = "checkShowInTaskbar"
        Me.checkShowInTaskbar.Size = New System.Drawing.Size(183, 17)
        Me.checkShowInTaskbar.TabIndex = 28
        Me.checkShowInTaskbar.Text = "Show in Taskbar when minimized"
        Me.checkShowInTaskbar.UseVisualStyleBackColor = True
        '
        'fontLabel
        '
        Me.fontLabel.AutoSize = True
        Me.fontLabel.Location = New System.Drawing.Point(95, 104)
        Me.fontLabel.Name = "fontLabel"
        Me.fontLabel.Size = New System.Drawing.Size(106, 13)
        Me.fontLabel.TabIndex = 27
        Me.fontLabel.Text = "ABCabc0123456789"
        '
        'buttonFont
        '
        Me.buttonFont.Location = New System.Drawing.Point(9, 101)
        Me.buttonFont.Name = "buttonFont"
        Me.buttonFont.Size = New System.Drawing.Size(75, 23)
        Me.buttonFont.TabIndex = 4
        Me.buttonFont.Text = "Choose Font"
        Me.buttonFont.UseVisualStyleBackColor = True
        '
        'labelWinPosString
        '
        Me.labelWinPosString.AutoSize = True
        Me.labelWinPosString.Location = New System.Drawing.Point(135, 9)
        Me.labelWinPosString.Name = "labelWinPosString"
        Me.labelWinPosString.Size = New System.Drawing.Size(51, 13)
        Me.labelWinPosString.TabIndex = 21
        Me.labelWinPosString.Text = "Location:"
        '
        'buttonResetWinPos
        '
        Me.buttonResetWinPos.Location = New System.Drawing.Point(250, 10)
        Me.buttonResetWinPos.Name = "buttonResetWinPos"
        Me.buttonResetWinPos.Size = New System.Drawing.Size(43, 33)
        Me.buttonResetWinPos.TabIndex = 26
        Me.buttonResetWinPos.Text = "Reset"
        Me.buttonResetWinPos.UseVisualStyleBackColor = True
        '
        'checkSavePos
        '
        Me.checkSavePos.AutoSize = True
        Me.checkSavePos.Location = New System.Drawing.Point(5, 17)
        Me.checkSavePos.Name = "checkSavePos"
        Me.checkSavePos.Size = New System.Drawing.Size(129, 17)
        Me.checkSavePos.TabIndex = 0
        Me.checkSavePos.Text = "Save window settings"
        Me.checkSavePos.UseVisualStyleBackColor = True
        '
        'labelWinSize
        '
        Me.labelWinSize.AutoSize = True
        Me.labelWinSize.Location = New System.Drawing.Point(183, 27)
        Me.labelWinSize.Name = "labelWinSize"
        Me.labelWinSize.Size = New System.Drawing.Size(31, 13)
        Me.labelWinSize.TabIndex = 25
        Me.labelWinSize.Text = "(0, 0)"
        '
        'labelWinSizeString
        '
        Me.labelWinSizeString.AutoSize = True
        Me.labelWinSizeString.Location = New System.Drawing.Point(156, 27)
        Me.labelWinSizeString.Name = "labelWinSizeString"
        Me.labelWinSizeString.Size = New System.Drawing.Size(30, 13)
        Me.labelWinSizeString.TabIndex = 22
        Me.labelWinSizeString.Text = "Size:"
        '
        'labelWinPos
        '
        Me.labelWinPos.AutoSize = True
        Me.labelWinPos.Location = New System.Drawing.Point(183, 9)
        Me.labelWinPos.Name = "labelWinPos"
        Me.labelWinPos.Size = New System.Drawing.Size(31, 13)
        Me.labelWinPos.TabIndex = 24
        Me.labelWinPos.Text = "(0, 0)"
        '
        'saveButton
        '
        Me.saveButton.Enabled = False
        Me.saveButton.Location = New System.Drawing.Point(4, 163)
        Me.saveButton.Name = "saveButton"
        Me.saveButton.Size = New System.Drawing.Size(111, 29)
        Me.saveButton.TabIndex = 67
        Me.saveButton.Text = "Save"
        Me.saveButton.UseVisualStyleBackColor = True
        '
        'g4
        '
        Me.g4.Controls.Add(Me.gameLocationRunButton)
        Me.g4.Controls.Add(Me.gameLocationChooseButton)
        Me.g4.Controls.Add(Me.gameLocationText)
        Me.g4.Controls.Add(Me.gameLocationLabel)
        Me.g4.Controls.Add(Me.checkIncludeGame)
        Me.g4.Controls.Add(Me.gamePicInvText)
        Me.g4.Controls.Add(Me.gamePicText)
        Me.g4.Controls.Add(Me.exeChooseButton)
        Me.g4.Controls.Add(Me.listGames)
        Me.g4.Controls.Add(Me.gameExeNameText)
        Me.g4.Controls.Add(Me.gameExeLabel)
        Me.g4.Controls.Add(Me.gamePicInv)
        Me.g4.Controls.Add(Me.gamePic)
        Me.g4.Controls.Add(Me.gameNameText)
        Me.g4.Controls.Add(Me.gameNameLabel)
        Me.g4.Controls.Add(Me.gameIdLabel)
        Me.g4.Controls.Add(Me.gameIdText)
        Me.g4.Controls.Add(Me.deleteGameButton)
        Me.g4.Controls.Add(Me.addGameButton)
        Me.g4.Controls.Add(Me.currentGamesLabel)
        Me.g4.Location = New System.Drawing.Point(1080, 33)
        Me.g4.Name = "g4"
        Me.g4.Size = New System.Drawing.Size(302, 256)
        Me.g4.TabIndex = 69
        Me.g4.TabStop = False
        Me.g4.Text = "Games"
        '
        'gameLocationRunButton
        '
        Me.gameLocationRunButton.Location = New System.Drawing.Point(270, 130)
        Me.gameLocationRunButton.Name = "gameLocationRunButton"
        Me.gameLocationRunButton.Size = New System.Drawing.Size(29, 22)
        Me.gameLocationRunButton.TabIndex = 80
        Me.gameLocationRunButton.Text = "Go"
        Me.gameLocationRunButton.UseVisualStyleBackColor = True
        '
        'gameLocationChooseButton
        '
        Me.gameLocationChooseButton.Location = New System.Drawing.Point(270, 109)
        Me.gameLocationChooseButton.Name = "gameLocationChooseButton"
        Me.gameLocationChooseButton.Size = New System.Drawing.Size(29, 22)
        Me.gameLocationChooseButton.TabIndex = 77
        Me.gameLocationChooseButton.Text = "..."
        Me.gameLocationChooseButton.UseVisualStyleBackColor = True
        '
        'gameLocationText
        '
        Me.gameLocationText.Location = New System.Drawing.Point(141, 110)
        Me.gameLocationText.Multiline = True
        Me.gameLocationText.Name = "gameLocationText"
        Me.gameLocationText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.gameLocationText.Size = New System.Drawing.Size(127, 41)
        Me.gameLocationText.TabIndex = 79
        Me.gameLocationText.Text = "C:\Users\Marvin\Documents\Visual Studio 2017\Projects\GTimer\GTimer\Resources"
        '
        'gameLocationLabel
        '
        Me.gameLocationLabel.AutoSize = True
        Me.gameLocationLabel.Location = New System.Drawing.Point(110, 122)
        Me.gameLocationLabel.Name = "gameLocationLabel"
        Me.gameLocationLabel.Size = New System.Drawing.Size(32, 13)
        Me.gameLocationLabel.TabIndex = 78
        Me.gameLocationLabel.Text = "Start:"
        '
        'checkIncludeGame
        '
        Me.checkIncludeGame.AutoSize = True
        Me.checkIncludeGame.Location = New System.Drawing.Point(232, 38)
        Me.checkIncludeGame.Name = "checkIncludeGame"
        Me.checkIncludeGame.Size = New System.Drawing.Size(61, 17)
        Me.checkIncludeGame.TabIndex = 29
        Me.checkIncludeGame.Text = "Include"
        Me.checkIncludeGame.UseVisualStyleBackColor = True
        '
        'gamePicInvText
        '
        Me.gamePicInvText.Location = New System.Drawing.Point(209, 229)
        Me.gamePicInvText.Name = "gamePicInvText"
        Me.gamePicInvText.Size = New System.Drawing.Size(70, 20)
        Me.gamePicInvText.TabIndex = 76
        '
        'gamePicText
        '
        Me.gamePicText.Location = New System.Drawing.Point(130, 229)
        Me.gamePicText.Name = "gamePicText"
        Me.gamePicText.Size = New System.Drawing.Size(70, 20)
        Me.gamePicText.TabIndex = 75
        '
        'exeChooseButton
        '
        Me.exeChooseButton.Location = New System.Drawing.Point(270, 84)
        Me.exeChooseButton.Name = "exeChooseButton"
        Me.exeChooseButton.Size = New System.Drawing.Size(29, 22)
        Me.exeChooseButton.TabIndex = 69
        Me.exeChooseButton.Text = "..."
        Me.exeChooseButton.UseVisualStyleBackColor = True
        '
        'listGames
        '
        Me.listGames.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listGames.FormattingEnabled = True
        Me.listGames.HorizontalScrollbar = True
        Me.listGames.Location = New System.Drawing.Point(2, 39)
        Me.listGames.Name = "listGames"
        Me.listGames.Size = New System.Drawing.Size(101, 186)
        Me.listGames.TabIndex = 63
        '
        'gameExeNameText
        '
        Me.gameExeNameText.Location = New System.Drawing.Point(141, 85)
        Me.gameExeNameText.Name = "gameExeNameText"
        Me.gameExeNameText.Size = New System.Drawing.Size(127, 20)
        Me.gameExeNameText.TabIndex = 74
        '
        'gameExeLabel
        '
        Me.gameExeLabel.AutoSize = True
        Me.gameExeLabel.Location = New System.Drawing.Point(111, 89)
        Me.gameExeLabel.Name = "gameExeLabel"
        Me.gameExeLabel.Size = New System.Drawing.Size(30, 13)
        Me.gameExeLabel.TabIndex = 73
        Me.gameExeLabel.Text = ".exe:"
        '
        'gamePicInv
        '
        Me.gamePicInv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.gamePicInv.Cursor = System.Windows.Forms.Cursors.Hand
        Me.gamePicInv.Location = New System.Drawing.Point(209, 157)
        Me.gamePicInv.Name = "gamePicInv"
        Me.gamePicInv.Size = New System.Drawing.Size(70, 70)
        Me.gamePicInv.TabIndex = 71
        Me.gamePicInv.TabStop = False
        '
        'gamePic
        '
        Me.gamePic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.gamePic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.gamePic.Location = New System.Drawing.Point(130, 157)
        Me.gamePic.Name = "gamePic"
        Me.gamePic.Size = New System.Drawing.Size(70, 70)
        Me.gamePic.TabIndex = 70
        Me.gamePic.TabStop = False
        '
        'gameNameText
        '
        Me.gameNameText.Location = New System.Drawing.Point(141, 60)
        Me.gameNameText.Name = "gameNameText"
        Me.gameNameText.Size = New System.Drawing.Size(127, 20)
        Me.gameNameText.TabIndex = 69
        '
        'gameNameLabel
        '
        Me.gameNameLabel.AutoSize = True
        Me.gameNameLabel.Location = New System.Drawing.Point(103, 64)
        Me.gameNameLabel.Name = "gameNameLabel"
        Me.gameNameLabel.Size = New System.Drawing.Size(38, 13)
        Me.gameNameLabel.TabIndex = 68
        Me.gameNameLabel.Text = "Name:"
        '
        'gameIdLabel
        '
        Me.gameIdLabel.AutoSize = True
        Me.gameIdLabel.Location = New System.Drawing.Point(118, 39)
        Me.gameIdLabel.Name = "gameIdLabel"
        Me.gameIdLabel.Size = New System.Drawing.Size(21, 13)
        Me.gameIdLabel.TabIndex = 67
        Me.gameIdLabel.Text = "ID:"
        '
        'gameIdText
        '
        Me.gameIdText.Location = New System.Drawing.Point(141, 35)
        Me.gameIdText.Name = "gameIdText"
        Me.gameIdText.ReadOnly = True
        Me.gameIdText.Size = New System.Drawing.Size(78, 20)
        Me.gameIdText.TabIndex = 66
        '
        'deleteGameButton
        '
        Me.deleteGameButton.Location = New System.Drawing.Point(232, 11)
        Me.deleteGameButton.Name = "deleteGameButton"
        Me.deleteGameButton.Size = New System.Drawing.Size(58, 25)
        Me.deleteGameButton.TabIndex = 65
        Me.deleteGameButton.Text = "Delete"
        Me.deleteGameButton.UseVisualStyleBackColor = True
        '
        'addGameButton
        '
        Me.addGameButton.Location = New System.Drawing.Point(1, 226)
        Me.addGameButton.Name = "addGameButton"
        Me.addGameButton.Size = New System.Drawing.Size(103, 23)
        Me.addGameButton.TabIndex = 64
        Me.addGameButton.Text = "Add"
        Me.addGameButton.UseVisualStyleBackColor = True
        '
        'currentGamesLabel
        '
        Me.currentGamesLabel.AutoSize = True
        Me.currentGamesLabel.Location = New System.Drawing.Point(10, 22)
        Me.currentGamesLabel.Name = "currentGamesLabel"
        Me.currentGamesLabel.Size = New System.Drawing.Size(80, 13)
        Me.currentGamesLabel.TabIndex = 29
        Me.currentGamesLabel.Text = "Current Games:"
        '
        'reloadAllButton
        '
        Me.reloadAllButton.BackgroundImage = CType(resources.GetObject("reloadAllButton.BackgroundImage"), System.Drawing.Image)
        Me.reloadAllButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.reloadAllButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.reloadAllButton.Location = New System.Drawing.Point(29, 206)
        Me.reloadAllButton.Name = "reloadAllButton"
        Me.reloadAllButton.Size = New System.Drawing.Size(50, 50)
        Me.reloadAllButton.TabIndex = 66
        Me.reloadAllButton.TabStop = False
        '
        'g5
        '
        Me.g5.Controls.Add(Me.GroupBox2)
        Me.g5.Controls.Add(Me.sortingRad2)
        Me.g5.Controls.Add(Me.sortingRad1)
        Me.g5.Location = New System.Drawing.Point(116, 307)
        Me.g5.Name = "g5"
        Me.g5.Size = New System.Drawing.Size(302, 256)
        Me.g5.TabIndex = 77
        Me.g5.TabStop = False
        Me.g5.Text = "Sorting"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.listSortGames)
        Me.GroupBox2.Controls.Add(Me.sortUpButton)
        Me.GroupBox2.Controls.Add(Me.sortCombo)
        Me.GroupBox2.Controls.Add(Me.sortDownButton)
        Me.GroupBox2.Controls.Add(Me.sortComboLabel)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 42)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(292, 208)
        Me.GroupBox2.TabIndex = 69
        Me.GroupBox2.TabStop = False
        '
        'listSortGames
        '
        Me.listSortGames.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listSortGames.FormattingEnabled = True
        Me.listSortGames.HorizontalScrollbar = True
        Me.listSortGames.Location = New System.Drawing.Point(80, 63)
        Me.listSortGames.Name = "listSortGames"
        Me.listSortGames.Size = New System.Drawing.Size(106, 134)
        Me.listSortGames.TabIndex = 65
        '
        'sortUpButton
        '
        Me.sortUpButton.Location = New System.Drawing.Point(188, 82)
        Me.sortUpButton.Name = "sortUpButton"
        Me.sortUpButton.Size = New System.Drawing.Size(50, 36)
        Me.sortUpButton.TabIndex = 63
        Me.sortUpButton.Text = "Up"
        Me.sortUpButton.UseVisualStyleBackColor = True
        '
        'sortCombo
        '
        Me.sortCombo.FormattingEnabled = True
        Me.sortCombo.Items.AddRange(New Object() {"Game Time", "Alphabetical", "Manual Order", "Last Played"})
        Me.sortCombo.Location = New System.Drawing.Point(80, 36)
        Me.sortCombo.Name = "sortCombo"
        Me.sortCombo.Size = New System.Drawing.Size(106, 21)
        Me.sortCombo.TabIndex = 28
        '
        'sortDownButton
        '
        Me.sortDownButton.Location = New System.Drawing.Point(188, 123)
        Me.sortDownButton.Name = "sortDownButton"
        Me.sortDownButton.Size = New System.Drawing.Size(50, 36)
        Me.sortDownButton.TabIndex = 64
        Me.sortDownButton.Text = "Down"
        Me.sortDownButton.UseVisualStyleBackColor = True
        '
        'sortComboLabel
        '
        Me.sortComboLabel.AutoSize = True
        Me.sortComboLabel.Location = New System.Drawing.Point(77, 20)
        Me.sortComboLabel.Name = "sortComboLabel"
        Me.sortComboLabel.Size = New System.Drawing.Size(82, 13)
        Me.sortComboLabel.TabIndex = 27
        Me.sortComboLabel.Text = "Sorting Method:"
        '
        'sortingRad2
        '
        Me.sortingRad2.AutoSize = True
        Me.sortingRad2.Location = New System.Drawing.Point(155, 19)
        Me.sortingRad2.Name = "sortingRad2"
        Me.sortingRad2.Size = New System.Drawing.Size(76, 17)
        Me.sortingRad2.TabIndex = 0
        Me.sortingRad2.TabStop = True
        Me.sortingRad2.Text = "Secondary"
        Me.sortingRad2.UseVisualStyleBackColor = True
        '
        'sortingRad1
        '
        Me.sortingRad1.AutoSize = True
        Me.sortingRad1.Location = New System.Drawing.Point(59, 19)
        Me.sortingRad1.Name = "sortingRad1"
        Me.sortingRad1.Size = New System.Drawing.Size(59, 17)
        Me.sortingRad1.TabIndex = 0
        Me.sortingRad1.TabStop = True
        Me.sortingRad1.Text = "Primary"
        Me.sortingRad1.UseVisualStyleBackColor = True
        '
        'g6
        '
        Me.g6.Controls.Add(Me.inviteTimeoutLabel)
        Me.g6.Controls.Add(Me.numInviteTimeout)
        Me.g6.Controls.Add(Me.checkInviteFlash)
        Me.g6.Controls.Add(Me.inviteBlacklistAddButton)
        Me.g6.Controls.Add(Me.inviteBlacklistRemButton)
        Me.g6.Controls.Add(Me.inviteBlacklistLabel)
        Me.g6.Controls.Add(Me.listInviteBlacklist)
        Me.g6.Controls.Add(Me.checkAllowInvites)
        Me.g6.Location = New System.Drawing.Point(445, 307)
        Me.g6.Name = "g6"
        Me.g6.Size = New System.Drawing.Size(302, 256)
        Me.g6.TabIndex = 78
        Me.g6.TabStop = False
        Me.g6.Text = "Invitations"
        '
        'inviteTimeoutLabel
        '
        Me.inviteTimeoutLabel.AutoSize = True
        Me.inviteTimeoutLabel.Location = New System.Drawing.Point(8, 98)
        Me.inviteTimeoutLabel.Name = "inviteTimeoutLabel"
        Me.inviteTimeoutLabel.Size = New System.Drawing.Size(91, 13)
        Me.inviteTimeoutLabel.TabIndex = 66
        Me.inviteTimeoutLabel.Text = "Invite Timeout (s):"
        '
        'numInviteTimeout
        '
        Me.numInviteTimeout.Location = New System.Drawing.Point(11, 114)
        Me.numInviteTimeout.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.numInviteTimeout.Name = "numInviteTimeout"
        Me.numInviteTimeout.Size = New System.Drawing.Size(58, 20)
        Me.numInviteTimeout.TabIndex = 68
        Me.numInviteTimeout.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'checkInviteFlash
        '
        Me.checkInviteFlash.AutoSize = True
        Me.checkInviteFlash.Location = New System.Drawing.Point(11, 52)
        Me.checkInviteFlash.Name = "checkInviteFlash"
        Me.checkInviteFlash.Size = New System.Drawing.Size(128, 30)
        Me.checkInviteFlash.TabIndex = 67
        Me.checkInviteFlash.Text = "Flash taskbar window" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "on invite"
        Me.checkInviteFlash.UseVisualStyleBackColor = True
        '
        'inviteBlacklistAddButton
        '
        Me.inviteBlacklistAddButton.Location = New System.Drawing.Point(264, 42)
        Me.inviteBlacklistAddButton.Name = "inviteBlacklistAddButton"
        Me.inviteBlacklistAddButton.Size = New System.Drawing.Size(25, 25)
        Me.inviteBlacklistAddButton.TabIndex = 63
        Me.inviteBlacklistAddButton.Text = "+"
        Me.inviteBlacklistAddButton.UseVisualStyleBackColor = True
        '
        'inviteBlacklistRemButton
        '
        Me.inviteBlacklistRemButton.Location = New System.Drawing.Point(264, 69)
        Me.inviteBlacklistRemButton.Name = "inviteBlacklistRemButton"
        Me.inviteBlacklistRemButton.Size = New System.Drawing.Size(25, 25)
        Me.inviteBlacklistRemButton.TabIndex = 64
        Me.inviteBlacklistRemButton.Text = "-"
        Me.inviteBlacklistRemButton.UseVisualStyleBackColor = True
        '
        'inviteBlacklistLabel
        '
        Me.inviteBlacklistLabel.AutoSize = True
        Me.inviteBlacklistLabel.Location = New System.Drawing.Point(149, 16)
        Me.inviteBlacklistLabel.Name = "inviteBlacklistLabel"
        Me.inviteBlacklistLabel.Size = New System.Drawing.Size(49, 13)
        Me.inviteBlacklistLabel.TabIndex = 66
        Me.inviteBlacklistLabel.Text = "Blacklist:"
        '
        'listInviteBlacklist
        '
        Me.listInviteBlacklist.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listInviteBlacklist.FormattingEnabled = True
        Me.listInviteBlacklist.HorizontalScrollbar = True
        Me.listInviteBlacklist.Location = New System.Drawing.Point(152, 32)
        Me.listInviteBlacklist.Name = "listInviteBlacklist"
        Me.listInviteBlacklist.Size = New System.Drawing.Size(106, 69)
        Me.listInviteBlacklist.TabIndex = 66
        '
        'checkAllowInvites
        '
        Me.checkAllowInvites.AutoSize = True
        Me.checkAllowInvites.Location = New System.Drawing.Point(11, 20)
        Me.checkAllowInvites.Name = "checkAllowInvites"
        Me.checkAllowInvites.Size = New System.Drawing.Size(113, 17)
        Me.checkAllowInvites.TabIndex = 29
        Me.checkAllowInvites.Text = "Allow game invites"
        Me.checkAllowInvites.UseVisualStyleBackColor = True
        '
        'OptionsForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1566, 715)
        Me.Controls.Add(Me.g6)
        Me.Controls.Add(Me.g5)
        Me.Controls.Add(Me.reloadAllButton)
        Me.Controls.Add(Me.g4)
        Me.Controls.Add(Me.saveButton)
        Me.Controls.Add(Me.g3)
        Me.Controls.Add(Me.g2)
        Me.Controls.Add(Me.listMenu)
        Me.Controls.Add(Me.g1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "OptionsForm"
        Me.Text = "Options"
        Me.g1.ResumeLayout(False)
        Me.g1.PerformLayout()
        Me.groupVersion.ResumeLayout(False)
        Me.groupVersion.PerformLayout()
        Me.g2.ResumeLayout(False)
        Me.g2.PerformLayout()
        CType(Me.exportPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.importPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.g3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.groupUI.ResumeLayout(False)
        Me.groupUI.PerformLayout()
        Me.g4.ResumeLayout(False)
        Me.g4.PerformLayout()
        CType(Me.gamePicInv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gamePic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.reloadAllButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.g5.ResumeLayout(False)
        Me.g5.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.g6.ResumeLayout(False)
        Me.g6.PerformLayout()
        CType(Me.numInviteTimeout, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents g1 As GroupBox
    Friend WithEvents groupVersion As GroupBox
    Friend WithEvents listPublish As ListBox
    Friend WithEvents publishButton As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents labelPublishedVersion As Label
    Friend WithEvents publishAddButton As Button
    Friend WithEvents publishRemButton As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents DownloadLatestButton As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents labelCurrVersion As Label
    Friend WithEvents listMenu As ListBox
    Friend WithEvents g2 As GroupBox
    Friend WithEvents exportPic As PictureBox
    Friend WithEvents importPic As PictureBox
    Friend WithEvents exportPicLabel As Label
    Friend WithEvents importPicLabel As Label
    Friend WithEvents g3 As GroupBox
    Friend WithEvents groupUI As GroupBox
    Friend WithEvents labelWinPosString As Label
    Friend WithEvents buttonResetWinPos As Button
    Friend WithEvents checkSavePos As CheckBox
    Friend WithEvents labelWinSize As Label
    Friend WithEvents labelWinSizeString As Label
    Friend WithEvents labelWinPos As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents checkAutostart As CheckBox
    Friend WithEvents comboStartState As ComboBox
    Friend WithEvents windowStateLabel As Label
    Friend WithEvents labelLatestVersion As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents sharedFolderButton As Button
    Friend WithEvents textSharedFolder As TextBox
    Friend WithEvents fontLabel As Label
    Friend WithEvents buttonFont As Button
    Friend WithEvents checkShowInTaskbar As CheckBox
    Friend WithEvents checkAutoUpdate As CheckBox
    Friend WithEvents saveButton As Button
    Friend WithEvents g4 As GroupBox
    Friend WithEvents deleteGameButton As Button
    Friend WithEvents addGameButton As Button
    Friend WithEvents currentGamesLabel As Label
    Friend WithEvents listGames As ListBox
    Friend WithEvents gameExeNameText As TextBox
    Friend WithEvents gameExeLabel As Label
    Friend WithEvents gamePicInv As PictureBox
    Friend WithEvents gamePic As PictureBox
    Friend WithEvents gameNameText As TextBox
    Friend WithEvents gameNameLabel As Label
    Friend WithEvents gameIdLabel As Label
    Friend WithEvents gameIdText As TextBox
    Friend WithEvents exeChooseButton As Button
    Friend WithEvents gamePicInvText As TextBox
    Friend WithEvents gamePicText As TextBox
    Friend WithEvents checkIncludeGame As CheckBox
    Friend WithEvents reloadAllButton As PictureBox
    Friend WithEvents g5 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents sortUpButton As Button
    Friend WithEvents sortCombo As ComboBox
    Friend WithEvents sortDownButton As Button
    Friend WithEvents sortComboLabel As Label
    Friend WithEvents sortingRad2 As RadioButton
    Friend WithEvents sortingRad1 As RadioButton
    Friend WithEvents listSortGames As ListBox
    Friend WithEvents gameLocationRunButton As Button
    Friend WithEvents gameLocationChooseButton As Button
    Friend WithEvents gameLocationText As TextBox
    Friend WithEvents gameLocationLabel As Label
    Friend WithEvents g6 As GroupBox
    Friend WithEvents inviteTimeoutLabel As Label
    Friend WithEvents numInviteTimeout As NumericUpDown
    Friend WithEvents checkInviteFlash As CheckBox
    Friend WithEvents inviteBlacklistAddButton As Button
    Friend WithEvents inviteBlacklistRemButton As Button
    Friend WithEvents inviteBlacklistLabel As Label
    Friend WithEvents listInviteBlacklist As ListBox
    Friend WithEvents checkAllowInvites As CheckBox
End Class
