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
        Me.groupVersion = New System.Windows.Forms.GroupBox()
        Me.publishPathButton = New System.Windows.Forms.Button()
        Me.listPublish = New System.Windows.Forms.ListBox()
        Me.publishButton = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.labelPublishedVersion = New System.Windows.Forms.Label()
        Me.publishAddButton = New System.Windows.Forms.Button()
        Me.publishRemButton = New System.Windows.Forms.Button()
        Me.searchHomeIpButton = New System.Windows.Forms.Button()
        Me.tftpUser = New System.Windows.Forms.TextBox()
        Me.tftpPw = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tftpIp = New System.Windows.Forms.TextBox()
        Me.labelftpCurrProg = New System.Windows.Forms.Label()
        Me.labelftpTotalProg = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pBar2 = New System.Windows.Forms.ProgressBar()
        Me.pBar = New System.Windows.Forms.ProgressBar()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DownloadLatestButton = New System.Windows.Forms.Button()
        Me.checkUpdatesButton = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.labelCurrVersion = New System.Windows.Forms.Label()
        Me.listMenu = New System.Windows.Forms.ListBox()
        Me.g2 = New System.Windows.Forms.GroupBox()
        Me.importPic = New System.Windows.Forms.PictureBox()
        Me.exportPic = New System.Windows.Forms.PictureBox()
        Me.importPicLabel = New System.Windows.Forms.Label()
        Me.exportPicLabel = New System.Windows.Forms.Label()
        Me.g1.SuspendLayout()
        Me.groupVersion.SuspendLayout()
        Me.g2.SuspendLayout()
        CType(Me.importPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.exportPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'g1
        '
        Me.g1.Controls.Add(Me.groupVersion)
        Me.g1.Controls.Add(Me.searchHomeIpButton)
        Me.g1.Controls.Add(Me.tftpUser)
        Me.g1.Controls.Add(Me.tftpPw)
        Me.g1.Controls.Add(Me.Label10)
        Me.g1.Controls.Add(Me.Label11)
        Me.g1.Controls.Add(Me.Label9)
        Me.g1.Controls.Add(Me.tftpIp)
        Me.g1.Controls.Add(Me.labelftpCurrProg)
        Me.g1.Controls.Add(Me.labelftpTotalProg)
        Me.g1.Controls.Add(Me.Label13)
        Me.g1.Controls.Add(Me.pBar2)
        Me.g1.Controls.Add(Me.pBar)
        Me.g1.Controls.Add(Me.Label7)
        Me.g1.Controls.Add(Me.DownloadLatestButton)
        Me.g1.Controls.Add(Me.checkUpdatesButton)
        Me.g1.Controls.Add(Me.Label12)
        Me.g1.Controls.Add(Me.labelCurrVersion)
        Me.g1.Location = New System.Drawing.Point(121, 33)
        Me.g1.Name = "g1"
        Me.g1.Size = New System.Drawing.Size(302, 256)
        Me.g1.TabIndex = 27
        Me.g1.TabStop = False
        Me.g1.Text = "Tracker Update"
        '
        'groupVersion
        '
        Me.groupVersion.Controls.Add(Me.publishPathButton)
        Me.groupVersion.Controls.Add(Me.listPublish)
        Me.groupVersion.Controls.Add(Me.publishButton)
        Me.groupVersion.Controls.Add(Me.Label8)
        Me.groupVersion.Controls.Add(Me.labelPublishedVersion)
        Me.groupVersion.Controls.Add(Me.publishAddButton)
        Me.groupVersion.Controls.Add(Me.publishRemButton)
        Me.groupVersion.Location = New System.Drawing.Point(136, 108)
        Me.groupVersion.Name = "groupVersion"
        Me.groupVersion.Size = New System.Drawing.Size(162, 146)
        Me.groupVersion.TabIndex = 63
        Me.groupVersion.TabStop = False
        '
        'publishPathButton
        '
        Me.publishPathButton.Location = New System.Drawing.Point(97, 43)
        Me.publishPathButton.Name = "publishPathButton"
        Me.publishPathButton.Size = New System.Drawing.Size(26, 27)
        Me.publishPathButton.TabIndex = 63
        Me.publishPathButton.Text = "..."
        Me.publishPathButton.UseVisualStyleBackColor = True
        '
        'listPublish
        '
        Me.listPublish.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listPublish.FormattingEnabled = True
        Me.listPublish.HorizontalScrollbar = True
        Me.listPublish.Location = New System.Drawing.Point(4, 75)
        Me.listPublish.Name = "listPublish"
        Me.listPublish.Size = New System.Drawing.Size(128, 69)
        Me.listPublish.TabIndex = 28
        '
        'publishButton
        '
        Me.publishButton.Location = New System.Drawing.Point(32, 43)
        Me.publishButton.Name = "publishButton"
        Me.publishButton.Size = New System.Drawing.Size(66, 27)
        Me.publishButton.TabIndex = 49
        Me.publishButton.Text = "Publish"
        Me.publishButton.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(19, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(117, 13)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "Publish current version:"
        '
        'labelPublishedVersion
        '
        Me.labelPublishedVersion.AutoSize = True
        Me.labelPublishedVersion.Location = New System.Drawing.Point(24, 27)
        Me.labelPublishedVersion.Name = "labelPublishedVersion"
        Me.labelPublishedVersion.Size = New System.Drawing.Size(109, 13)
        Me.labelPublishedVersion.TabIndex = 61
        Me.labelPublishedVersion.Text = "2017.08.16_16.09.11"
        '
        'publishAddButton
        '
        Me.publishAddButton.Location = New System.Drawing.Point(134, 82)
        Me.publishAddButton.Name = "publishAddButton"
        Me.publishAddButton.Size = New System.Drawing.Size(25, 25)
        Me.publishAddButton.TabIndex = 48
        Me.publishAddButton.Text = "+"
        Me.publishAddButton.UseVisualStyleBackColor = True
        '
        'publishRemButton
        '
        Me.publishRemButton.Location = New System.Drawing.Point(134, 109)
        Me.publishRemButton.Name = "publishRemButton"
        Me.publishRemButton.Size = New System.Drawing.Size(25, 25)
        Me.publishRemButton.TabIndex = 62
        Me.publishRemButton.Text = "-"
        Me.publishRemButton.UseVisualStyleBackColor = True
        '
        'searchHomeIpButton
        '
        Me.searchHomeIpButton.Location = New System.Drawing.Point(209, 14)
        Me.searchHomeIpButton.Name = "searchHomeIpButton"
        Me.searchHomeIpButton.Size = New System.Drawing.Size(67, 23)
        Me.searchHomeIpButton.TabIndex = 57
        Me.searchHomeIpButton.Text = "Search"
        Me.searchHomeIpButton.UseVisualStyleBackColor = True
        '
        'tftpUser
        '
        Me.tftpUser.Location = New System.Drawing.Point(199, 66)
        Me.tftpUser.Name = "tftpUser"
        Me.tftpUser.Size = New System.Drawing.Size(87, 20)
        Me.tftpUser.TabIndex = 53
        Me.tftpUser.Text = "updateplayer"
        '
        'tftpPw
        '
        Me.tftpPw.Location = New System.Drawing.Point(199, 88)
        Me.tftpPw.Name = "tftpPw"
        Me.tftpPw.Size = New System.Drawing.Size(87, 20)
        Me.tftpPw.TabIndex = 52
        Me.tftpPw.Text = "huan"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(134, 69)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 13)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "Username:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(136, 91)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 13)
        Me.Label11.TabIndex = 56
        Me.Label11.Text = "Password:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(137, 42)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(54, 13)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "Server IP:"
        '
        'tftpIp
        '
        Me.tftpIp.Location = New System.Drawing.Point(199, 39)
        Me.tftpIp.Name = "tftpIp"
        Me.tftpIp.Size = New System.Drawing.Size(87, 20)
        Me.tftpIp.TabIndex = 47
        Me.tftpIp.Text = "127.0.0.1"
        '
        'labelftpCurrProg
        '
        Me.labelftpCurrProg.AutoSize = True
        Me.labelftpCurrProg.Location = New System.Drawing.Point(5, 198)
        Me.labelftpCurrProg.Name = "labelftpCurrProg"
        Me.labelftpCurrProg.Size = New System.Drawing.Size(30, 13)
        Me.labelftpCurrProg.TabIndex = 60
        Me.labelftpCurrProg.Text = "0 / 0"
        '
        'labelftpTotalProg
        '
        Me.labelftpTotalProg.AutoSize = True
        Me.labelftpTotalProg.Location = New System.Drawing.Point(38, 238)
        Me.labelftpTotalProg.Name = "labelftpTotalProg"
        Me.labelftpTotalProg.Size = New System.Drawing.Size(33, 13)
        Me.labelftpTotalProg.TabIndex = 58
        Me.labelftpTotalProg.Text = "0 / 0 "
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(5, 238)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(31, 13)
        Me.Label13.TabIndex = 57
        Me.Label13.Text = "Files:"
        '
        'pBar2
        '
        Me.pBar2.Location = New System.Drawing.Point(7, 177)
        Me.pBar2.Name = "pBar2"
        Me.pBar2.Size = New System.Drawing.Size(123, 16)
        Me.pBar2.TabIndex = 53
        '
        'pBar
        '
        Me.pBar.Location = New System.Drawing.Point(8, 216)
        Me.pBar.Name = "pBar"
        Me.pBar.Size = New System.Drawing.Size(123, 16)
        Me.pBar.TabIndex = 52
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 118)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(123, 13)
        Me.Label7.TabIndex = 50
        Me.Label7.Text = "Download latest version:"
        '
        'DownloadLatestButton
        '
        Me.DownloadLatestButton.Location = New System.Drawing.Point(34, 136)
        Me.DownloadLatestButton.Name = "DownloadLatestButton"
        Me.DownloadLatestButton.Size = New System.Drawing.Size(75, 36)
        Me.DownloadLatestButton.TabIndex = 48
        Me.DownloadLatestButton.Text = "Download"
        Me.DownloadLatestButton.UseVisualStyleBackColor = True
        '
        'checkUpdatesButton
        '
        Me.checkUpdatesButton.Location = New System.Drawing.Point(34, 52)
        Me.checkUpdatesButton.Name = "checkUpdatesButton"
        Me.checkUpdatesButton.Size = New System.Drawing.Size(75, 34)
        Me.checkUpdatesButton.TabIndex = 47
        Me.checkUpdatesButton.Text = "Check for Updates"
        Me.checkUpdatesButton.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(16, 19)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(81, 13)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Current version:"
        '
        'labelCurrVersion
        '
        Me.labelCurrVersion.AutoSize = True
        Me.labelCurrVersion.Location = New System.Drawing.Point(16, 36)
        Me.labelCurrVersion.Name = "labelCurrVersion"
        Me.labelCurrVersion.Size = New System.Drawing.Size(109, 13)
        Me.labelCurrVersion.TabIndex = 20
        Me.labelCurrVersion.Text = "2017.07.28_23.54.44"
        '
        'listMenu
        '
        Me.listMenu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listMenu.FormattingEnabled = True
        Me.listMenu.HorizontalScrollbar = True
        Me.listMenu.ItemHeight = 16
        Me.listMenu.Items.AddRange(New Object() {"Version Update", "Configuration"})
        Me.listMenu.Location = New System.Drawing.Point(4, 33)
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
        'importPicLabel
        '
        Me.importPicLabel.AutoSize = True
        Me.importPicLabel.Location = New System.Drawing.Point(81, 38)
        Me.importPicLabel.Name = "importPicLabel"
        Me.importPicLabel.Size = New System.Drawing.Size(36, 13)
        Me.importPicLabel.TabIndex = 64
        Me.importPicLabel.Text = "Import"
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
        'OptionsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(807, 358)
        Me.Controls.Add(Me.g2)
        Me.Controls.Add(Me.listMenu)
        Me.Controls.Add(Me.g1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "OptionsForm"
        Me.Text = "Options"
        Me.g1.ResumeLayout(False)
        Me.g1.PerformLayout()
        Me.groupVersion.ResumeLayout(False)
        Me.groupVersion.PerformLayout()
        Me.g2.ResumeLayout(False)
        Me.g2.PerformLayout()
        CType(Me.importPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.exportPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents g1 As GroupBox
    Friend WithEvents groupVersion As GroupBox
    Friend WithEvents publishPathButton As Button
    Friend WithEvents listPublish As ListBox
    Friend WithEvents publishButton As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents labelPublishedVersion As Label
    Friend WithEvents publishAddButton As Button
    Friend WithEvents publishRemButton As Button
    Friend WithEvents searchHomeIpButton As Button
    Friend WithEvents tftpUser As TextBox
    Friend WithEvents tftpPw As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents tftpIp As TextBox
    Friend WithEvents labelftpCurrProg As Label
    Friend WithEvents labelftpTotalProg As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents pBar2 As ProgressBar
    Friend WithEvents pBar As ProgressBar
    Friend WithEvents Label7 As Label
    Friend WithEvents DownloadLatestButton As Button
    Friend WithEvents checkUpdatesButton As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents labelCurrVersion As Label
    Friend WithEvents listMenu As ListBox
    Friend WithEvents g2 As GroupBox
    Friend WithEvents exportPic As PictureBox
    Friend WithEvents importPic As PictureBox
    Friend WithEvents exportPicLabel As Label
    Friend WithEvents importPicLabel As Label
End Class
