<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GitUpdater
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GitUpdater))
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnGitPullAll = New System.Windows.Forms.Button()
        Me.btnGitPushAll = New System.Windows.Forms.Button()
        Me.lstRepos = New System.Windows.Forms.ListBox()
        Me.ContextMenuStripRepos = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ContextMenuStripReposOpenInExplorer = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStripReposOpenInCMD = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStripReposOpenInPS = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStripReposOpenInGitHub = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStripReposSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ContextMenuStripReposOpenReadme = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStripReposOpenSLN = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStripReposSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ContextMenuStripReposCopyRepoName = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStripReposCopyRepoPath = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStripReposSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ContextMenuStripReposRemoveEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnGitPullSelected = New System.Windows.Forms.Button()
        Me.btnGitPushSelected = New System.Windows.Forms.Button()
        Me.btnCD = New System.Windows.Forms.Button()
        Me.btnGitPushNotSelected = New System.Windows.Forms.Button()
        Me.btnGitPullNotSelected = New System.Windows.Forms.Button()
        Me.chkRepeat = New System.Windows.Forms.CheckBox()
        Me.folderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.chkDontClose = New System.Windows.Forms.CheckBox()
        Me.chkNoWait = New System.Windows.Forms.CheckBox()
        Me.grpGUI = New System.Windows.Forms.GroupBox()
        Me.chkDontShow = New System.Windows.Forms.CheckBox()
        Me.grpData = New System.Windows.Forms.GroupBox()
        Me.btnBrowseLog = New System.Windows.Forms.Button()
        Me.txtLogPath = New System.Windows.Forms.TextBox()
        Me.chkOpenLog = New System.Windows.Forms.CheckBox()
        Me.chkLog = New System.Windows.Forms.CheckBox()
        Me.ShellWorker = New System.ComponentModel.BackgroundWorker()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnInsertCredentials = New System.Windows.Forms.Button()
        Me.grpCredMan = New System.Windows.Forms.GroupBox()
        Me.btnShowPass = New System.Windows.Forms.Button()
        Me.lblHotkey = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.btnHotkey = New System.Windows.Forms.Button()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.timerKeyChecker = New System.Windows.Forms.Timer(Me.components)
        Me.btnCloseCmd = New System.Windows.Forms.Button()
        Me.SaveLogFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.ContextMenuStripRepos.SuspendLayout()
        Me.grpGUI.SuspendLayout()
        Me.grpData.SuspendLayout()
        Me.grpCredMan.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.AutoSize = True
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Location = New System.Drawing.Point(449, 508)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(63, 23)
        Me.btnExit.TabIndex = 10
        Me.btnExit.Text = "Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnGitPullAll
        '
        Me.btnGitPullAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGitPullAll.Location = New System.Drawing.Point(266, 228)
        Me.btnGitPullAll.Name = "btnGitPullAll"
        Me.btnGitPullAll.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPullAll.TabIndex = 11
        Me.btnGitPullAll.Text = "Git Pull all"
        Me.btnGitPullAll.UseVisualStyleBackColor = True
        '
        'btnGitPushAll
        '
        Me.btnGitPushAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGitPushAll.Location = New System.Drawing.Point(392, 228)
        Me.btnGitPushAll.Name = "btnGitPushAll"
        Me.btnGitPushAll.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPushAll.TabIndex = 12
        Me.btnGitPushAll.Text = "Git Push all"
        Me.btnGitPushAll.UseVisualStyleBackColor = True
        '
        'lstRepos
        '
        Me.lstRepos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstRepos.ContextMenuStrip = Me.ContextMenuStripRepos
        Me.lstRepos.FormattingEnabled = True
        Me.lstRepos.HorizontalScrollbar = True
        Me.lstRepos.IntegralHeight = False
        Me.lstRepos.Location = New System.Drawing.Point(12, 12)
        Me.lstRepos.Name = "lstRepos"
        Me.lstRepos.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstRepos.Size = New System.Drawing.Size(248, 519)
        Me.lstRepos.Sorted = True
        Me.lstRepos.TabIndex = 13
        '
        'ContextMenuStripRepos
        '
        Me.ContextMenuStripRepos.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContextMenuStripReposOpenInExplorer, Me.ContextMenuStripReposOpenInCMD, Me.ContextMenuStripReposOpenInPS, Me.ContextMenuStripReposOpenInGitHub, Me.ContextMenuStripReposSeparator1, Me.ContextMenuStripReposOpenReadme, Me.ContextMenuStripReposOpenSLN, Me.ContextMenuStripReposSeparator2, Me.ContextMenuStripReposCopyRepoName, Me.ContextMenuStripReposCopyRepoPath, Me.ContextMenuStripReposSeparator3, Me.ContextMenuStripReposRemoveEntry})
        Me.ContextMenuStripRepos.Name = "contextMenuStripRepos"
        Me.ContextMenuStripRepos.Size = New System.Drawing.Size(273, 242)
        '
        'ContextMenuStripReposOpenInExplorer
        '
        Me.ContextMenuStripReposOpenInExplorer.Image = Global.GitUpdater.My.Resources.Resources.ExplorerSmall
        Me.ContextMenuStripReposOpenInExplorer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContextMenuStripReposOpenInExplorer.Name = "ContextMenuStripReposOpenInExplorer"
        Me.ContextMenuStripReposOpenInExplorer.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposOpenInExplorer.Text = "Open Repo in Windows Explorer"
        '
        'ContextMenuStripReposOpenInCMD
        '
        Me.ContextMenuStripReposOpenInCMD.Image = Global.GitUpdater.My.Resources.Resources.CmdSmall
        Me.ContextMenuStripReposOpenInCMD.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContextMenuStripReposOpenInCMD.Name = "ContextMenuStripReposOpenInCMD"
        Me.ContextMenuStripReposOpenInCMD.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposOpenInCMD.Text = "Open Repo in CMD"
        '
        'ContextMenuStripReposOpenInPS
        '
        Me.ContextMenuStripReposOpenInPS.Image = Global.GitUpdater.My.Resources.Resources.PSSmall
        Me.ContextMenuStripReposOpenInPS.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContextMenuStripReposOpenInPS.Name = "ContextMenuStripReposOpenInPS"
        Me.ContextMenuStripReposOpenInPS.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposOpenInPS.Text = "Open Repo in Windows PowerShell"
        '
        'ContextMenuStripReposOpenInGitHub
        '
        Me.ContextMenuStripReposOpenInGitHub.Image = Global.GitUpdater.My.Resources.Resources.GitHubForWindowsMedium
        Me.ContextMenuStripReposOpenInGitHub.Name = "ContextMenuStripReposOpenInGitHub"
        Me.ContextMenuStripReposOpenInGitHub.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposOpenInGitHub.Text = "Open Repo in GitHub for Windows"
        '
        'ContextMenuStripReposSeparator1
        '
        Me.ContextMenuStripReposSeparator1.Name = "ContextMenuStripReposSeparator1"
        Me.ContextMenuStripReposSeparator1.Size = New System.Drawing.Size(269, 6)
        '
        'ContextMenuStripReposOpenReadme
        '
        Me.ContextMenuStripReposOpenReadme.Image = Global.GitUpdater.My.Resources.Resources.text_x_readme
        Me.ContextMenuStripReposOpenReadme.Name = "ContextMenuStripReposOpenReadme"
        Me.ContextMenuStripReposOpenReadme.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposOpenReadme.Text = "Open Repo Readme"
        '
        'ContextMenuStripReposOpenSLN
        '
        Me.ContextMenuStripReposOpenSLN.Image = Global.GitUpdater.My.Resources.Resources.VS_SLN2
        Me.ContextMenuStripReposOpenSLN.Name = "ContextMenuStripReposOpenSLN"
        Me.ContextMenuStripReposOpenSLN.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposOpenSLN.Text = "Open Repo SLN"
        '
        'ContextMenuStripReposSeparator2
        '
        Me.ContextMenuStripReposSeparator2.Name = "ContextMenuStripReposSeparator2"
        Me.ContextMenuStripReposSeparator2.Size = New System.Drawing.Size(269, 6)
        '
        'ContextMenuStripReposCopyRepoName
        '
        Me.ContextMenuStripReposCopyRepoName.Image = Global.GitUpdater.My.Resources.Resources.EditCopy
        Me.ContextMenuStripReposCopyRepoName.Name = "ContextMenuStripReposCopyRepoName"
        Me.ContextMenuStripReposCopyRepoName.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposCopyRepoName.Text = "Copy Repo Name"
        '
        'ContextMenuStripReposCopyRepoPath
        '
        Me.ContextMenuStripReposCopyRepoPath.Image = Global.GitUpdater.My.Resources.Resources.EditCopy
        Me.ContextMenuStripReposCopyRepoPath.Name = "ContextMenuStripReposCopyRepoPath"
        Me.ContextMenuStripReposCopyRepoPath.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposCopyRepoPath.Text = "Copy Repo Path"
        '
        'ContextMenuStripReposSeparator3
        '
        Me.ContextMenuStripReposSeparator3.Name = "ContextMenuStripReposSeparator3"
        Me.ContextMenuStripReposSeparator3.Size = New System.Drawing.Size(269, 6)
        '
        'ContextMenuStripReposRemoveEntry
        '
        Me.ContextMenuStripReposRemoveEntry.Name = "ContextMenuStripReposRemoveEntry"
        Me.ContextMenuStripReposRemoveEntry.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposRemoveEntry.Text = "Remove Entry"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.AutoSize = True
        Me.btnRefresh.Location = New System.Drawing.Point(266, 508)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(63, 23)
        Me.btnRefresh.TabIndex = 14
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnGitPullSelected
        '
        Me.btnGitPullSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGitPullSelected.Location = New System.Drawing.Point(266, 257)
        Me.btnGitPullSelected.Name = "btnGitPullSelected"
        Me.btnGitPullSelected.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPullSelected.TabIndex = 15
        Me.btnGitPullSelected.Text = "Git Pull selected"
        Me.btnGitPullSelected.UseVisualStyleBackColor = True
        '
        'btnGitPushSelected
        '
        Me.btnGitPushSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGitPushSelected.Location = New System.Drawing.Point(392, 257)
        Me.btnGitPushSelected.Name = "btnGitPushSelected"
        Me.btnGitPushSelected.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPushSelected.TabIndex = 16
        Me.btnGitPushSelected.Text = "Git Push selected"
        Me.btnGitPushSelected.UseVisualStyleBackColor = True
        '
        'btnCD
        '
        Me.btnCD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCD.AutoSize = True
        Me.btnCD.Location = New System.Drawing.Point(335, 508)
        Me.btnCD.Name = "btnCD"
        Me.btnCD.Size = New System.Drawing.Size(108, 23)
        Me.btnCD.TabIndex = 17
        Me.btnCD.Text = "Change Directory..."
        Me.btnCD.UseVisualStyleBackColor = True
        '
        'btnGitPushNotSelected
        '
        Me.btnGitPushNotSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGitPushNotSelected.Location = New System.Drawing.Point(392, 286)
        Me.btnGitPushNotSelected.Name = "btnGitPushNotSelected"
        Me.btnGitPushNotSelected.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPushNotSelected.TabIndex = 18
        Me.btnGitPushNotSelected.Text = "...all except selected"
        Me.btnGitPushNotSelected.UseVisualStyleBackColor = True
        '
        'btnGitPullNotSelected
        '
        Me.btnGitPullNotSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGitPullNotSelected.Location = New System.Drawing.Point(266, 286)
        Me.btnGitPullNotSelected.Name = "btnGitPullNotSelected"
        Me.btnGitPullNotSelected.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPullNotSelected.TabIndex = 19
        Me.btnGitPullNotSelected.Text = "...all except selected"
        Me.btnGitPullNotSelected.UseVisualStyleBackColor = True
        '
        'chkRepeat
        '
        Me.chkRepeat.AutoSize = True
        Me.chkRepeat.BackColor = System.Drawing.Color.Transparent
        Me.chkRepeat.Location = New System.Drawing.Point(6, 19)
        Me.chkRepeat.Name = "chkRepeat"
        Me.chkRepeat.Size = New System.Drawing.Size(125, 17)
        Me.chkRepeat.TabIndex = 20
        Me.chkRepeat.Text = "Repeat until success"
        Me.chkRepeat.UseVisualStyleBackColor = False
        '
        'chkDontClose
        '
        Me.chkDontClose.AutoSize = True
        Me.chkDontClose.Location = New System.Drawing.Point(6, 42)
        Me.chkDontClose.Name = "chkDontClose"
        Me.chkDontClose.Size = New System.Drawing.Size(223, 17)
        Me.chkDontClose.TabIndex = 22
        Me.chkDontClose.Text = "Don't close command window when done"
        Me.chkDontClose.UseVisualStyleBackColor = True
        '
        'chkNoWait
        '
        Me.chkNoWait.AutoSize = True
        Me.chkNoWait.BackColor = System.Drawing.Color.Transparent
        Me.chkNoWait.Location = New System.Drawing.Point(6, 19)
        Me.chkNoWait.Name = "chkNoWait"
        Me.chkNoWait.Size = New System.Drawing.Size(244, 17)
        Me.chkNoWait.TabIndex = 23
        Me.chkNoWait.Text = "Don't wait for cmd to close before starting next"
        Me.chkNoWait.UseVisualStyleBackColor = False
        '
        'grpGUI
        '
        Me.grpGUI.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpGUI.Controls.Add(Me.chkDontShow)
        Me.grpGUI.Controls.Add(Me.chkNoWait)
        Me.grpGUI.Controls.Add(Me.chkDontClose)
        Me.grpGUI.Location = New System.Drawing.Point(266, 12)
        Me.grpGUI.Name = "grpGUI"
        Me.grpGUI.Size = New System.Drawing.Size(246, 88)
        Me.grpGUI.TabIndex = 25
        Me.grpGUI.TabStop = False
        Me.grpGUI.Text = "GUI Options"
        '
        'chkDontShow
        '
        Me.chkDontShow.AutoSize = True
        Me.chkDontShow.Location = New System.Drawing.Point(6, 65)
        Me.chkDontShow.Name = "chkDontShow"
        Me.chkDontShow.Size = New System.Drawing.Size(178, 17)
        Me.chkDontShow.TabIndex = 24
        Me.chkDontShow.Text = "Don't show command window(s)"
        Me.chkDontShow.UseVisualStyleBackColor = True
        '
        'grpData
        '
        Me.grpData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpData.Controls.Add(Me.btnBrowseLog)
        Me.grpData.Controls.Add(Me.txtLogPath)
        Me.grpData.Controls.Add(Me.chkOpenLog)
        Me.grpData.Controls.Add(Me.chkLog)
        Me.grpData.Controls.Add(Me.chkRepeat)
        Me.grpData.Location = New System.Drawing.Point(266, 106)
        Me.grpData.Name = "grpData"
        Me.grpData.Size = New System.Drawing.Size(246, 116)
        Me.grpData.TabIndex = 26
        Me.grpData.TabStop = False
        Me.grpData.Text = "Data Options"
        '
        'btnBrowseLog
        '
        Me.btnBrowseLog.Location = New System.Drawing.Point(165, 87)
        Me.btnBrowseLog.Name = "btnBrowseLog"
        Me.btnBrowseLog.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseLog.TabIndex = 25
        Me.btnBrowseLog.Text = "Browse..."
        Me.btnBrowseLog.UseVisualStyleBackColor = True
        '
        'txtLogPath
        '
        Me.txtLogPath.Location = New System.Drawing.Point(6, 65)
        Me.txtLogPath.Name = "txtLogPath"
        Me.txtLogPath.Size = New System.Drawing.Size(234, 20)
        Me.txtLogPath.TabIndex = 24
        '
        'chkOpenLog
        '
        Me.chkOpenLog.AutoSize = True
        Me.chkOpenLog.Location = New System.Drawing.Point(6, 91)
        Me.chkOpenLog.Name = "chkOpenLog"
        Me.chkOpenLog.Size = New System.Drawing.Size(125, 17)
        Me.chkOpenLog.TabIndex = 23
        Me.chkOpenLog.Text = "Open log when done"
        Me.chkOpenLog.UseVisualStyleBackColor = True
        '
        'chkLog
        '
        Me.chkLog.AutoSize = True
        Me.chkLog.Location = New System.Drawing.Point(6, 42)
        Me.chkLog.Name = "chkLog"
        Me.chkLog.Size = New System.Drawing.Size(169, 17)
        Me.chkLog.TabIndex = 22
        Me.chkLog.Text = "Send output from operation to:"
        Me.chkLog.UseVisualStyleBackColor = True
        '
        'ShellWorker
        '
        Me.ShellWorker.WorkerSupportsCancellation = True
        '
        'progressBar
        '
        Me.progressBar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.progressBar.Location = New System.Drawing.Point(266, 315)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(165, 23)
        Me.progressBar.Step = 1
        Me.progressBar.TabIndex = 28
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(437, 315)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 29
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnInsertCredentials
        '
        Me.btnInsertCredentials.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnInsertCredentials.AutoSize = True
        Me.btnInsertCredentials.Location = New System.Drawing.Point(6, 71)
        Me.btnInsertCredentials.Name = "btnInsertCredentials"
        Me.btnInsertCredentials.Size = New System.Drawing.Size(114, 23)
        Me.btnInsertCredentials.TabIndex = 30
        Me.btnInsertCredentials.Text = "Insert Credentials"
        Me.btnInsertCredentials.UseVisualStyleBackColor = True
        '
        'grpCredMan
        '
        Me.grpCredMan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpCredMan.Controls.Add(Me.btnShowPass)
        Me.grpCredMan.Controls.Add(Me.lblHotkey)
        Me.grpCredMan.Controls.Add(Me.btnSave)
        Me.grpCredMan.Controls.Add(Me.lblPassword)
        Me.grpCredMan.Controls.Add(Me.txtPassword)
        Me.grpCredMan.Controls.Add(Me.btnHotkey)
        Me.grpCredMan.Controls.Add(Me.txtUsername)
        Me.grpCredMan.Controls.Add(Me.lblUsername)
        Me.grpCredMan.Controls.Add(Me.btnInsertCredentials)
        Me.grpCredMan.Location = New System.Drawing.Point(266, 373)
        Me.grpCredMan.Name = "grpCredMan"
        Me.grpCredMan.Size = New System.Drawing.Size(246, 129)
        Me.grpCredMan.TabIndex = 31
        Me.grpCredMan.TabStop = False
        Me.grpCredMan.Text = "Credentials Management"
        '
        'btnShowPass
        '
        Me.btnShowPass.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShowPass.BackColor = System.Drawing.Color.White
        Me.btnShowPass.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnShowPass.FlatAppearance.BorderSize = 0
        Me.btnShowPass.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight
        Me.btnShowPass.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnShowPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowPass.ForeColor = System.Drawing.Color.Transparent
        Me.btnShowPass.Image = CType(resources.GetObject("btnShowPass.Image"), System.Drawing.Image)
        Me.btnShowPass.Location = New System.Drawing.Point(219, 47)
        Me.btnShowPass.Name = "btnShowPass"
        Me.btnShowPass.Size = New System.Drawing.Size(20, 16)
        Me.btnShowPass.TabIndex = 38
        Me.btnShowPass.UseVisualStyleBackColor = False
        '
        'lblHotkey
        '
        Me.lblHotkey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHotkey.Location = New System.Drawing.Point(126, 100)
        Me.lblHotkey.Name = "lblHotkey"
        Me.lblHotkey.Size = New System.Drawing.Size(114, 23)
        Me.lblHotkey.TabIndex = 37
        Me.lblHotkey.Text = "Current key(s): Alt"
        Me.lblHotkey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.AutoSize = True
        Me.btnSave.Location = New System.Drawing.Point(126, 71)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(114, 23)
        Me.btnSave.TabIndex = 36
        Me.btnSave.Text = "Save Credentials"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(6, 49)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 34
        Me.lblPassword.Text = "Password:"
        '
        'txtPassword
        '
        Me.txtPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPassword.Location = New System.Drawing.Point(70, 45)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtPassword.Size = New System.Drawing.Size(170, 20)
        Me.txtPassword.TabIndex = 33
        '
        'btnHotkey
        '
        Me.btnHotkey.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHotkey.Location = New System.Drawing.Point(6, 100)
        Me.btnHotkey.Name = "btnHotkey"
        Me.btnHotkey.Size = New System.Drawing.Size(114, 23)
        Me.btnHotkey.TabIndex = 35
        Me.btnHotkey.Text = "Enable Hotkey"
        Me.btnHotkey.UseVisualStyleBackColor = True
        '
        'txtUsername
        '
        Me.txtUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUsername.Location = New System.Drawing.Point(70, 19)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(170, 20)
        Me.txtUsername.TabIndex = 32
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(6, 22)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(58, 13)
        Me.lblUsername.TabIndex = 31
        Me.lblUsername.Text = "Username:"
        '
        'timerKeyChecker
        '
        '
        'btnCloseCmd
        '
        Me.btnCloseCmd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCloseCmd.AutoSize = True
        Me.btnCloseCmd.Location = New System.Drawing.Point(266, 344)
        Me.btnCloseCmd.Name = "btnCloseCmd"
        Me.btnCloseCmd.Size = New System.Drawing.Size(246, 23)
        Me.btnCloseCmd.TabIndex = 32
        Me.btnCloseCmd.Text = "Cancel foreground CMD window"
        Me.btnCloseCmd.UseVisualStyleBackColor = True
        '
        'SaveLogFileDialog
        '
        Me.SaveLogFileDialog.DefaultExt = "log"
        Me.SaveLogFileDialog.FileName = "GitUpdater.log"
        Me.SaveLogFileDialog.Filter = "Log files|*.log"
        Me.SaveLogFileDialog.OverwritePrompt = False
        Me.SaveLogFileDialog.Title = "Choose where to save the log to:"
        '
        'GitUpdater
        '
        Me.AcceptButton = Me.btnGitPushSelected
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(524, 543)
        Me.Controls.Add(Me.btnCloseCmd)
        Me.Controls.Add(Me.grpCredMan)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.progressBar)
        Me.Controls.Add(Me.grpData)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnCD)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.grpGUI)
        Me.Controls.Add(Me.btnGitPullNotSelected)
        Me.Controls.Add(Me.btnGitPushNotSelected)
        Me.Controls.Add(Me.btnGitPushSelected)
        Me.Controls.Add(Me.btnGitPullSelected)
        Me.Controls.Add(Me.lstRepos)
        Me.Controls.Add(Me.btnGitPushAll)
        Me.Controls.Add(Me.btnGitPullAll)
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "GitUpdater"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GitUpdater"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ContextMenuStripRepos.ResumeLayout(False)
        Me.grpGUI.ResumeLayout(False)
        Me.grpGUI.PerformLayout()
        Me.grpData.ResumeLayout(False)
        Me.grpData.PerformLayout()
        Me.grpCredMan.ResumeLayout(False)
        Me.grpCredMan.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents ContextMenuStripReposOpenSLN As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ContextMenuStripReposSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents ContextMenuStripReposSeparator2 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents ContextMenuStripReposOpenReadme As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ContextMenuStripReposCopyRepoPath As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ContextMenuStripReposCopyRepoName As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ContextMenuStripReposOpenInPS As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ContextMenuStripRepos As System.Windows.Forms.ContextMenuStrip
    Private WithEvents ContextMenuStripReposOpenInExplorer As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ContextMenuStripReposOpenInGitHub As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ContextMenuStripReposOpenInCMD As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents chkLog As System.Windows.Forms.CheckBox
    Private WithEvents btnCloseCmd As System.Windows.Forms.Button
    Private WithEvents timerKeyChecker As System.Windows.Forms.Timer
    Private WithEvents btnShowPass As System.Windows.Forms.Button
    Private WithEvents lblUsername As System.Windows.Forms.Label
    Private WithEvents txtUsername As System.Windows.Forms.TextBox
    Private WithEvents txtPassword As System.Windows.Forms.TextBox
    Private WithEvents lblPassword As System.Windows.Forms.Label
    Private WithEvents btnHotkey As System.Windows.Forms.Button
    Private WithEvents btnSave As System.Windows.Forms.Button
    Private WithEvents lblHotkey As System.Windows.Forms.Label
    Private WithEvents grpCredMan As System.Windows.Forms.GroupBox
    Private WithEvents btnInsertCredentials As System.Windows.Forms.Button
    Private WithEvents btnCancel As System.Windows.Forms.Button
    Private WithEvents progressBar As System.Windows.Forms.ProgressBar
    Private WithEvents ShellWorker As System.ComponentModel.BackgroundWorker
    Private WithEvents grpGUI As System.Windows.Forms.GroupBox
    Private WithEvents grpData As System.Windows.Forms.GroupBox
    Private WithEvents chkDontShow As System.Windows.Forms.CheckBox
    Private WithEvents chkNoWait As System.Windows.Forms.CheckBox
    Private WithEvents chkDontClose As System.Windows.Forms.CheckBox
    Private WithEvents folderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Private WithEvents chkRepeat As System.Windows.Forms.CheckBox
    Private WithEvents btnGitPullNotSelected As System.Windows.Forms.Button
    Private WithEvents btnGitPushNotSelected As System.Windows.Forms.Button
    Private WithEvents btnCD As System.Windows.Forms.Button
    Private WithEvents btnGitPushSelected As System.Windows.Forms.Button
    Private WithEvents btnGitPullSelected As System.Windows.Forms.Button
    Private WithEvents btnRefresh As System.Windows.Forms.Button
    Private WithEvents lstRepos As System.Windows.Forms.ListBox
    Private WithEvents btnGitPushAll As System.Windows.Forms.Button
    Private WithEvents btnGitPullAll As System.Windows.Forms.Button
    Private WithEvents btnExit As System.Windows.Forms.Button
    Private WithEvents chkOpenLog As System.Windows.Forms.CheckBox
    Private WithEvents btnBrowseLog As System.Windows.Forms.Button
    Private WithEvents txtLogPath As System.Windows.Forms.TextBox
    Private WithEvents SaveLogFileDialog As System.Windows.Forms.SaveFileDialog
    Private WithEvents ContextMenuStripReposSeparator3 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents ContextMenuStripReposRemoveEntry As System.Windows.Forms.ToolStripMenuItem
End Class
