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
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnGitPullSelected = New System.Windows.Forms.Button()
        Me.btnGitPushSelected = New System.Windows.Forms.Button()
        Me.btnCD = New System.Windows.Forms.Button()
        Me.btnGitPushNotSelected = New System.Windows.Forms.Button()
        Me.btnGitPullNotSelected = New System.Windows.Forms.Button()
        Me.chkRepeat = New System.Windows.Forms.CheckBox()
        Me.folderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.chkPushForce = New System.Windows.Forms.CheckBox()
        Me.chkDontClose = New System.Windows.Forms.CheckBox()
        Me.chkNoWait = New System.Windows.Forms.CheckBox()
        Me.grpGUI = New System.Windows.Forms.GroupBox()
        Me.chkDontShow = New System.Windows.Forms.CheckBox()
        Me.grpData = New System.Windows.Forms.GroupBox()
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
        Me.ContextMenuStripRepos.SuspendLayout
        Me.grpGUI.SuspendLayout
        Me.grpData.SuspendLayout
        Me.grpCredMan.SuspendLayout
        Me.SuspendLayout
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnExit.AutoSize = true
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Location = New System.Drawing.Point(449, 480)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(63, 23)
        Me.btnExit.TabIndex = 10
        Me.btnExit.Text = "Close"
        Me.btnExit.UseVisualStyleBackColor = true
        AddHandler Me.btnExit.Click, AddressOf Me.btnExit_Click
        '
        'btnGitPullAll
        '
        Me.btnGitPullAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnGitPullAll.Location = New System.Drawing.Point(266, 200)
        Me.btnGitPullAll.Name = "btnGitPullAll"
        Me.btnGitPullAll.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPullAll.TabIndex = 11
        Me.btnGitPullAll.Text = "Git Pull all"
        Me.btnGitPullAll.UseVisualStyleBackColor = true
        AddHandler Me.btnGitPullAll.Click, AddressOf Me.BtnGitPullAll_Click
        '
        'btnGitPushAll
        '
        Me.btnGitPushAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnGitPushAll.Location = New System.Drawing.Point(392, 200)
        Me.btnGitPushAll.Name = "btnGitPushAll"
        Me.btnGitPushAll.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPushAll.TabIndex = 12
        Me.btnGitPushAll.Text = "Git Push all"
        Me.btnGitPushAll.UseVisualStyleBackColor = true
        AddHandler Me.btnGitPushAll.Click, AddressOf Me.BtnGitPushAll_Click
        '
        'lstRepos
        '
        Me.lstRepos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
                        Or System.Windows.Forms.AnchorStyles.Left)  _
                        Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lstRepos.ContextMenuStrip = Me.ContextMenuStripRepos
        Me.lstRepos.FormattingEnabled = true
        Me.lstRepos.HorizontalScrollbar = true
        Me.lstRepos.IntegralHeight = false
        Me.lstRepos.Location = New System.Drawing.Point(12, 12)
        Me.lstRepos.Name = "lstRepos"
        Me.lstRepos.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstRepos.Size = New System.Drawing.Size(248, 491)
        Me.lstRepos.Sorted = true
        Me.lstRepos.TabIndex = 13
        AddHandler Me.lstRepos.DoubleClick, AddressOf Me.LstRepos_DoubleClick
        '
        'ContextMenuStripRepos
        '
        Me.ContextMenuStripRepos.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContextMenuStripReposOpenInExplorer, Me.ContextMenuStripReposOpenInCMD, Me.ContextMenuStripReposOpenInPS, Me.ContextMenuStripReposOpenInGitHub, Me.ContextMenuStripReposSeparator1, Me.ContextMenuStripReposOpenReadme, Me.ContextMenuStripReposOpenSLN, Me.ContextMenuStripReposSeparator2, Me.ContextMenuStripReposCopyRepoName, Me.ContextMenuStripReposCopyRepoPath})
        Me.ContextMenuStripRepos.Name = "contextMenuStripRepos"
        Me.ContextMenuStripRepos.Size = New System.Drawing.Size(273, 214)
        '
        'ContextMenuStripReposOpenInExplorer
        '
        Me.ContextMenuStripReposOpenInExplorer.Image = Global.GitUpdater.My.Resources.Resources.ExplorerSmall
        Me.ContextMenuStripReposOpenInExplorer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContextMenuStripReposOpenInExplorer.Name = "ContextMenuStripReposOpenInExplorer"
        Me.ContextMenuStripReposOpenInExplorer.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposOpenInExplorer.Text = "Open Repo in Windows Explorer"
        AddHandler Me.ContextMenuStripReposOpenInExplorer.Click, AddressOf Me.LstRepos_DoubleClick
        '
        'ContextMenuStripReposOpenInCMD
        '
        Me.ContextMenuStripReposOpenInCMD.Image = Global.GitUpdater.My.Resources.Resources.CmdSmall
        Me.ContextMenuStripReposOpenInCMD.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContextMenuStripReposOpenInCMD.Name = "ContextMenuStripReposOpenInCMD"
        Me.ContextMenuStripReposOpenInCMD.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposOpenInCMD.Text = "Open Repo in CMD"
        AddHandler Me.ContextMenuStripReposOpenInCMD.Click, AddressOf Me.ContextMenuStripReposOpenInCMD_Click
        '
        'ContextMenuStripReposOpenInPS
        '
        Me.ContextMenuStripReposOpenInPS.Image = Global.GitUpdater.My.Resources.Resources.PSSmall
        Me.ContextMenuStripReposOpenInPS.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ContextMenuStripReposOpenInPS.Name = "ContextMenuStripReposOpenInPS"
        Me.ContextMenuStripReposOpenInPS.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposOpenInPS.Text = "Open Repo in Windows PowerShell"
        AddHandler Me.ContextMenuStripReposOpenInPS.Click, AddressOf Me.ContextMenuStripReposOpenInPS_Click
        '
        'ContextMenuStripReposOpenInGitHub
        '
        Me.ContextMenuStripReposOpenInGitHub.Image = Global.GitUpdater.My.Resources.Resources.GitHubForWindowsMedium
        Me.ContextMenuStripReposOpenInGitHub.Name = "ContextMenuStripReposOpenInGitHub"
        Me.ContextMenuStripReposOpenInGitHub.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposOpenInGitHub.Text = "Open Repo in GitHub for Windows"
        AddHandler Me.ContextMenuStripReposOpenInGitHub.Click, AddressOf Me.ContextMenuStripReposOpenInGitHub_Click
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
        AddHandler Me.ContextMenuStripReposOpenReadme.Click, AddressOf Me.ContextMenuStripReposOpenReadme_Click
        '
        'ContextMenuStripReposOpenSLN
        '
        Me.ContextMenuStripReposOpenSLN.Image = Global.GitUpdater.My.Resources.Resources.VS_SLN2
        Me.ContextMenuStripReposOpenSLN.Name = "ContextMenuStripReposOpenSLN"
        Me.ContextMenuStripReposOpenSLN.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposOpenSLN.Text = "Open Repo SLN"
        AddHandler Me.ContextMenuStripReposOpenSLN.Click, AddressOf Me.ContextMenuStripReposOpenSLN_Click
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
        AddHandler Me.ContextMenuStripReposCopyRepoName.Click, AddressOf Me.ContextMenuStripReposCopyRepoName_Click
        '
        'ContextMenuStripReposCopyRepoPath
        '
        Me.ContextMenuStripReposCopyRepoPath.Image = Global.GitUpdater.My.Resources.Resources.EditCopy
        Me.ContextMenuStripReposCopyRepoPath.Name = "ContextMenuStripReposCopyRepoPath"
        Me.ContextMenuStripReposCopyRepoPath.Size = New System.Drawing.Size(272, 22)
        Me.ContextMenuStripReposCopyRepoPath.Text = "Copy Repo Path"
        AddHandler Me.ContextMenuStripReposCopyRepoPath.Click, AddressOf Me.ContextMenuStripReposCopyRepoPath_Click
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.AutoSize = true
        Me.btnRefresh.Location = New System.Drawing.Point(266, 480)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(63, 23)
        Me.btnRefresh.TabIndex = 14
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = true
        AddHandler Me.btnRefresh.Click, AddressOf Me.BtnRefresh_Click
        '
        'btnGitPullSelected
        '
        Me.btnGitPullSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnGitPullSelected.Location = New System.Drawing.Point(266, 229)
        Me.btnGitPullSelected.Name = "btnGitPullSelected"
        Me.btnGitPullSelected.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPullSelected.TabIndex = 15
        Me.btnGitPullSelected.Text = "Git Pull selected"
        Me.btnGitPullSelected.UseVisualStyleBackColor = true
        AddHandler Me.btnGitPullSelected.Click, AddressOf Me.BtnGitPullSelected_Click
        '
        'btnGitPushSelected
        '
        Me.btnGitPushSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnGitPushSelected.Location = New System.Drawing.Point(392, 229)
        Me.btnGitPushSelected.Name = "btnGitPushSelected"
        Me.btnGitPushSelected.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPushSelected.TabIndex = 16
        Me.btnGitPushSelected.Text = "Git Push selected"
        Me.btnGitPushSelected.UseVisualStyleBackColor = true
        AddHandler Me.btnGitPushSelected.Click, AddressOf Me.BtnGitPushSelected_Click
        '
        'btnCD
        '
        Me.btnCD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnCD.AutoSize = true
        Me.btnCD.Location = New System.Drawing.Point(335, 480)
        Me.btnCD.Name = "btnCD"
        Me.btnCD.Size = New System.Drawing.Size(108, 23)
        Me.btnCD.TabIndex = 17
        Me.btnCD.Text = "Change Directory..."
        Me.btnCD.UseVisualStyleBackColor = true
        AddHandler Me.btnCD.Click, AddressOf Me.BtnCD_Click
        '
        'btnGitPushNotSelected
        '
        Me.btnGitPushNotSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnGitPushNotSelected.Location = New System.Drawing.Point(392, 258)
        Me.btnGitPushNotSelected.Name = "btnGitPushNotSelected"
        Me.btnGitPushNotSelected.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPushNotSelected.TabIndex = 18
        Me.btnGitPushNotSelected.Text = "...all except selected"
        Me.btnGitPushNotSelected.UseVisualStyleBackColor = true
        AddHandler Me.btnGitPushNotSelected.Click, AddressOf Me.BtnGitPushNotSelected_Click
        '
        'btnGitPullNotSelected
        '
        Me.btnGitPullNotSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnGitPullNotSelected.Location = New System.Drawing.Point(266, 258)
        Me.btnGitPullNotSelected.Name = "btnGitPullNotSelected"
        Me.btnGitPullNotSelected.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPullNotSelected.TabIndex = 19
        Me.btnGitPullNotSelected.Text = "...all except selected"
        Me.btnGitPullNotSelected.UseVisualStyleBackColor = true
        AddHandler Me.btnGitPullNotSelected.Click, AddressOf Me.BtnGitPullNotSelected_Click
        '
        'chkRepeat
        '
        Me.chkRepeat.AutoSize = true
        Me.chkRepeat.BackColor = System.Drawing.Color.Transparent
        Me.chkRepeat.Location = New System.Drawing.Point(6, 42)
        Me.chkRepeat.Name = "chkRepeat"
        Me.chkRepeat.Size = New System.Drawing.Size(125, 17)
        Me.chkRepeat.TabIndex = 20
        Me.chkRepeat.Text = "Repeat until success"
        Me.chkRepeat.UseVisualStyleBackColor = false
        AddHandler Me.chkRepeat.CheckedChanged, AddressOf Me.ChkRepeat_CheckedChanged
        '
        'folderBrowserDialog
        '
        Me.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyDocuments
        Me.folderBrowserDialog.SelectedPath = "GitHub"
        '
        'chkPushForce
        '
        Me.chkPushForce.AutoSize = true
        Me.chkPushForce.BackColor = System.Drawing.Color.Transparent
        Me.chkPushForce.Location = New System.Drawing.Point(6, 19)
        Me.chkPushForce.Name = "chkPushForce"
        Me.chkPushForce.Size = New System.Drawing.Size(116, 17)
        Me.chkPushForce.TabIndex = 21
        Me.chkPushForce.Text = "Use push -f (Force)"
        Me.chkPushForce.UseVisualStyleBackColor = false
        AddHandler Me.chkPushForce.CheckedChanged, AddressOf Me.ChkPushForce_CheckedChanged
        '
        'chkDontClose
        '
        Me.chkDontClose.AutoSize = true
        Me.chkDontClose.Location = New System.Drawing.Point(6, 42)
        Me.chkDontClose.Name = "chkDontClose"
        Me.chkDontClose.Size = New System.Drawing.Size(223, 17)
        Me.chkDontClose.TabIndex = 22
        Me.chkDontClose.Text = "Don't close command window when done"
        Me.chkDontClose.UseVisualStyleBackColor = true
        AddHandler Me.chkDontClose.CheckedChanged, AddressOf Me.ChkDontClose_CheckedChanged
        '
        'chkNoWait
        '
        Me.chkNoWait.AutoSize = true
        Me.chkNoWait.BackColor = System.Drawing.Color.Transparent
        Me.chkNoWait.Location = New System.Drawing.Point(6, 19)
        Me.chkNoWait.Name = "chkNoWait"
        Me.chkNoWait.Size = New System.Drawing.Size(244, 17)
        Me.chkNoWait.TabIndex = 23
        Me.chkNoWait.Text = "Don't wait for cmd to close before starting next"
        Me.chkNoWait.UseVisualStyleBackColor = false
        AddHandler Me.chkNoWait.CheckedChanged, AddressOf Me.ChkNoWait_CheckedChanged
        '
        'grpGUI
        '
        Me.grpGUI.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grpGUI.Controls.Add(Me.chkDontShow)
        Me.grpGUI.Controls.Add(Me.chkNoWait)
        Me.grpGUI.Controls.Add(Me.chkDontClose)
        Me.grpGUI.Location = New System.Drawing.Point(266, 12)
        Me.grpGUI.Name = "grpGUI"
        Me.grpGUI.Size = New System.Drawing.Size(246, 88)
        Me.grpGUI.TabIndex = 25
        Me.grpGUI.TabStop = false
        Me.grpGUI.Text = "GUI Options"
        '
        'chkDontShow
        '
        Me.chkDontShow.AutoSize = true
        Me.chkDontShow.Location = New System.Drawing.Point(6, 65)
        Me.chkDontShow.Name = "chkDontShow"
        Me.chkDontShow.Size = New System.Drawing.Size(178, 17)
        Me.chkDontShow.TabIndex = 24
        Me.chkDontShow.Text = "Don't show command window(s)"
        Me.chkDontShow.UseVisualStyleBackColor = true
        AddHandler Me.chkDontShow.CheckedChanged, AddressOf Me.ChkDontShow_CheckedChanged
        '
        'grpData
        '
        Me.grpData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grpData.Controls.Add(Me.chkLog)
        Me.grpData.Controls.Add(Me.chkPushForce)
        Me.grpData.Controls.Add(Me.chkRepeat)
        Me.grpData.Location = New System.Drawing.Point(266, 106)
        Me.grpData.Name = "grpData"
        Me.grpData.Size = New System.Drawing.Size(246, 88)
        Me.grpData.TabIndex = 26
        Me.grpData.TabStop = false
        Me.grpData.Text = "Data Options"
        '
        'chkLog
        '
        Me.chkLog.AutoSize = true
        Me.chkLog.Location = New System.Drawing.Point(6, 65)
        Me.chkLog.Name = "chkLog"
        Me.chkLog.Size = New System.Drawing.Size(237, 17)
        Me.chkLog.TabIndex = 22
        Me.chkLog.Text = "Send output from operation to GitUpdater.log"
        Me.chkLog.UseVisualStyleBackColor = true
        AddHandler Me.chkLog.CheckedChanged, AddressOf Me.ChkLog_CheckedChanged
        '
        'ShellWorker
        '
        Me.ShellWorker.WorkerSupportsCancellation = true
        AddHandler Me.ShellWorker.DoWork, AddressOf Me.ShellWorker_DoWork
        '
        'progressBar
        '
        Me.progressBar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.progressBar.Location = New System.Drawing.Point(266, 287)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(165, 23)
        Me.progressBar.Step = 1
        Me.progressBar.TabIndex = 28
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(437, 287)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 29
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = true
        AddHandler Me.btnCancel.Click, AddressOf Me.BtnCancel_Click
        '
        'btnInsertCredentials
        '
        Me.btnInsertCredentials.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnInsertCredentials.AutoSize = true
        Me.btnInsertCredentials.Location = New System.Drawing.Point(6, 71)
        Me.btnInsertCredentials.Name = "btnInsertCredentials"
        Me.btnInsertCredentials.Size = New System.Drawing.Size(114, 23)
        Me.btnInsertCredentials.TabIndex = 30
        Me.btnInsertCredentials.Text = "Insert Credentials"
        Me.btnInsertCredentials.UseVisualStyleBackColor = true
        AddHandler Me.btnInsertCredentials.Click, AddressOf Me.BtnInsert_Click
        '
        'grpCredMan
        '
        Me.grpCredMan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grpCredMan.Controls.Add(Me.btnShowPass)
        Me.grpCredMan.Controls.Add(Me.lblHotkey)
        Me.grpCredMan.Controls.Add(Me.btnSave)
        Me.grpCredMan.Controls.Add(Me.lblPassword)
        Me.grpCredMan.Controls.Add(Me.txtPassword)
        Me.grpCredMan.Controls.Add(Me.btnHotkey)
        Me.grpCredMan.Controls.Add(Me.txtUsername)
        Me.grpCredMan.Controls.Add(Me.lblUsername)
        Me.grpCredMan.Controls.Add(Me.btnInsertCredentials)
        Me.grpCredMan.Location = New System.Drawing.Point(266, 345)
        Me.grpCredMan.Name = "grpCredMan"
        Me.grpCredMan.Size = New System.Drawing.Size(246, 129)
        Me.grpCredMan.TabIndex = 31
        Me.grpCredMan.TabStop = false
        Me.grpCredMan.Text = "Credentials Management"
        '
        'btnShowPass
        '
        Me.btnShowPass.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnShowPass.BackColor = System.Drawing.Color.White
        Me.btnShowPass.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnShowPass.FlatAppearance.BorderSize = 0
        Me.btnShowPass.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight
        Me.btnShowPass.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnShowPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowPass.ForeColor = System.Drawing.Color.Transparent
        Me.btnShowPass.Image = CType(resources.GetObject("btnShowPass.Image"),System.Drawing.Image)
        Me.btnShowPass.Location = New System.Drawing.Point(219, 47)
        Me.btnShowPass.Name = "btnShowPass"
        Me.btnShowPass.Size = New System.Drawing.Size(20, 16)
        Me.btnShowPass.TabIndex = 38
        Me.btnShowPass.UseVisualStyleBackColor = false
        AddHandler Me.btnShowPass.MouseDown, AddressOf Me.btnShowPass_MouseDown
        AddHandler Me.btnShowPass.MouseUp, AddressOf Me.btnShowPass_MouseUp
        '
        'lblHotkey
        '
        Me.lblHotkey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
                        Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblHotkey.Location = New System.Drawing.Point(126, 100)
        Me.lblHotkey.Name = "lblHotkey"
        Me.lblHotkey.Size = New System.Drawing.Size(114, 23)
        Me.lblHotkey.TabIndex = 37
        Me.lblHotkey.Text = "Current key(s): Alt"
        Me.lblHotkey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnSave.AutoSize = true
        Me.btnSave.Location = New System.Drawing.Point(126, 71)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(114, 23)
        Me.btnSave.TabIndex = 36
        Me.btnSave.Text = "Save Credentials"
        Me.btnSave.UseVisualStyleBackColor = true
        AddHandler Me.btnSave.Click, AddressOf Me.BtnSave_Click
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = true
        Me.lblPassword.Location = New System.Drawing.Point(6, 49)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 34
        Me.lblPassword.Text = "Password:"
        '
        'txtPassword
        '
        Me.txtPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
                        Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtPassword.Location = New System.Drawing.Point(70, 45)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtPassword.Size = New System.Drawing.Size(170, 20)
        Me.txtPassword.TabIndex = 33
        '
        'btnHotkey
        '
        Me.btnHotkey.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnHotkey.Location = New System.Drawing.Point(6, 100)
        Me.btnHotkey.Name = "btnHotkey"
        Me.btnHotkey.Size = New System.Drawing.Size(114, 23)
        Me.btnHotkey.TabIndex = 35
        Me.btnHotkey.Text = "Hotkey On"
        Me.btnHotkey.UseVisualStyleBackColor = true
        AddHandler Me.btnHotkey.Click, AddressOf Me.BtnHotkey_Click
        '
        'txtUsername
        '
        Me.txtUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
                        Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtUsername.Location = New System.Drawing.Point(70, 19)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(170, 20)
        Me.txtUsername.TabIndex = 32
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = true
        Me.lblUsername.Location = New System.Drawing.Point(6, 22)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(58, 13)
        Me.lblUsername.TabIndex = 31
        Me.lblUsername.Text = "Username:"
        '
        'timerKeyChecker
        '
        AddHandler Me.timerKeyChecker.Tick, AddressOf Me.TimerKeyChecker_Tick
        '
        'btnCloseCmd
        '
        Me.btnCloseCmd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnCloseCmd.AutoSize = true
        Me.btnCloseCmd.Location = New System.Drawing.Point(266, 316)
        Me.btnCloseCmd.Name = "btnCloseCmd"
        Me.btnCloseCmd.Size = New System.Drawing.Size(246, 23)
        Me.btnCloseCmd.TabIndex = 32
        Me.btnCloseCmd.Text = "Cancel foreground CMD window"
        Me.btnCloseCmd.UseVisualStyleBackColor = true
        AddHandler Me.btnCloseCmd.Click, AddressOf Me.BtnCloseCmd_Click
        '
        'GitUpdater
        '
        Me.AcceptButton = Me.btnGitPushSelected
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(524, 515)
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
        Me.HelpButton = true
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "GitUpdater"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GitUpdater"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        AddHandler Load, AddressOf Me.LoadGitUpdater
        Me.ContextMenuStripRepos.ResumeLayout(false)
        Me.grpGUI.ResumeLayout(false)
        Me.grpGUI.PerformLayout
        Me.grpData.ResumeLayout(false)
        Me.grpData.PerformLayout
        Me.grpCredMan.ResumeLayout(false)
        Me.grpCredMan.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout
    End Sub
    Private ContextMenuStripReposOpenSLN As System.Windows.Forms.ToolStripMenuItem
    Private ContextMenuStripReposSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private ContextMenuStripReposSeparator2 As System.Windows.Forms.ToolStripSeparator
    Private ContextMenuStripReposOpenReadme As System.Windows.Forms.ToolStripMenuItem
    Private ContextMenuStripReposCopyRepoPath As System.Windows.Forms.ToolStripMenuItem
    Private ContextMenuStripReposCopyRepoName As System.Windows.Forms.ToolStripMenuItem
    Private ContextMenuStripReposOpenInPS As System.Windows.Forms.ToolStripMenuItem
    Private ContextMenuStripRepos As System.Windows.Forms.ContextMenuStrip
    Private ContextMenuStripReposOpenInExplorer As System.Windows.Forms.ToolStripMenuItem
    Private ContextMenuStripReposOpenInGitHub As System.Windows.Forms.ToolStripMenuItem
    Private ContextMenuStripReposOpenInCMD As System.Windows.Forms.ToolStripMenuItem
    Private chkLog As System.Windows.Forms.CheckBox
    Private btnCloseCmd As System.Windows.Forms.Button
    Private timerKeyChecker As System.Windows.Forms.Timer
    Private btnShowPass As System.Windows.Forms.Button
    Private lblUsername As System.Windows.Forms.Label
    Private txtUsername As System.Windows.Forms.TextBox
    Private txtPassword As System.Windows.Forms.TextBox
    Private lblPassword As System.Windows.Forms.Label
    Private btnHotkey As System.Windows.Forms.Button
    Private btnSave As System.Windows.Forms.Button
    Private lblHotkey As System.Windows.Forms.Label
    Private grpCredMan As System.Windows.Forms.GroupBox
    Private btnInsertCredentials As System.Windows.Forms.Button
    Private btnCancel As System.Windows.Forms.Button
    Private progressBar As System.Windows.Forms.ProgressBar
    Private ShellWorker As System.ComponentModel.BackgroundWorker
    Private grpGUI As System.Windows.Forms.GroupBox
    Private grpData As System.Windows.Forms.GroupBox
    Private chkDontShow As System.Windows.Forms.CheckBox
    Private chkNoWait As System.Windows.Forms.CheckBox
    Private chkDontClose As System.Windows.Forms.CheckBox
    Private chkPushForce As System.Windows.Forms.CheckBox
    Private folderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Private chkRepeat As System.Windows.Forms.CheckBox
    Private btnGitPullNotSelected As System.Windows.Forms.Button
    Private btnGitPushNotSelected As System.Windows.Forms.Button
    Private btnCD As System.Windows.Forms.Button
    Private btnGitPushSelected As System.Windows.Forms.Button
    Private btnGitPullSelected As System.Windows.Forms.Button
    Private btnRefresh As System.Windows.Forms.Button
    Private lstRepos As System.Windows.Forms.ListBox
    Private btnGitPushAll As System.Windows.Forms.Button
    Private btnGitPullAll As System.Windows.Forms.Button
    Private btnExit As System.Windows.Forms.Button
End Class
