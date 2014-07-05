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
        Me.lstDirs = New System.Windows.Forms.ListBox()
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
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.grpOptions = New System.Windows.Forms.GroupBox()
        Me.grpCreds = New System.Windows.Forms.GroupBox()
        Me.btnShowPass = New System.Windows.Forms.Button()
        Me.lblSaved = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.timerKeyChecker = New System.Windows.Forms.Timer(Me.components)
        Me.grpOptions.SuspendLayout
        Me.grpCreds.SuspendLayout
        Me.SuspendLayout
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Location = New System.Drawing.Point(392, 328)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(120, 23)
        Me.btnExit.TabIndex = 10
        Me.btnExit.Text = "Close"
        Me.btnExit.UseVisualStyleBackColor = true
        AddHandler Me.btnExit.Click, AddressOf Me.btnExit_Click
        '
        'btnGitPullAll
        '
        Me.btnGitPullAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnGitPullAll.Location = New System.Drawing.Point(266, 12)
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
        Me.btnGitPushAll.Location = New System.Drawing.Point(392, 12)
        Me.btnGitPushAll.Name = "btnGitPushAll"
        Me.btnGitPushAll.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPushAll.TabIndex = 12
        Me.btnGitPushAll.Text = "Git Push all"
        Me.btnGitPushAll.UseVisualStyleBackColor = true
        AddHandler Me.btnGitPushAll.Click, AddressOf Me.BtnGitPushAll_Click
        '
        'lstDirs
        '
        Me.lstDirs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
                        Or System.Windows.Forms.AnchorStyles.Left)  _
                        Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lstDirs.FormattingEnabled = true
        Me.lstDirs.HorizontalScrollbar = true
        Me.lstDirs.IntegralHeight = false
        Me.lstDirs.Location = New System.Drawing.Point(12, 12)
        Me.lstDirs.Name = "lstDirs"
        Me.lstDirs.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstDirs.Size = New System.Drawing.Size(248, 339)
        Me.lstDirs.Sorted = true
        Me.lstDirs.TabIndex = 13
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(266, 299)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(120, 23)
        Me.btnRefresh.TabIndex = 14
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = true
        AddHandler Me.btnRefresh.Click, AddressOf Me.BtnRefresh_Click
        '
        'btnGitPullSelected
        '
        Me.btnGitPullSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnGitPullSelected.Location = New System.Drawing.Point(266, 41)
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
        Me.btnGitPushSelected.Location = New System.Drawing.Point(392, 41)
        Me.btnGitPushSelected.Name = "btnGitPushSelected"
        Me.btnGitPushSelected.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPushSelected.TabIndex = 16
        Me.btnGitPushSelected.Text = "Git Push selected"
        Me.btnGitPushSelected.UseVisualStyleBackColor = true
        AddHandler Me.btnGitPushSelected.Click, AddressOf Me.BtnGitPushSelected_Click
        '
        'btnCD
        '
        Me.btnCD.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnCD.Location = New System.Drawing.Point(392, 299)
        Me.btnCD.Name = "btnCD"
        Me.btnCD.Size = New System.Drawing.Size(120, 23)
        Me.btnCD.TabIndex = 17
        Me.btnCD.Text = "Change Directory..."
        Me.btnCD.UseVisualStyleBackColor = true
        AddHandler Me.btnCD.Click, AddressOf Me.BtnCD_Click
        '
        'btnGitPushNotSelected
        '
        Me.btnGitPushNotSelected.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnGitPushNotSelected.Location = New System.Drawing.Point(392, 70)
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
        Me.btnGitPullNotSelected.Location = New System.Drawing.Point(266, 70)
        Me.btnGitPullNotSelected.Name = "btnGitPullNotSelected"
        Me.btnGitPullNotSelected.Size = New System.Drawing.Size(120, 23)
        Me.btnGitPullNotSelected.TabIndex = 19
        Me.btnGitPullNotSelected.Text = "...all except selected"
        Me.btnGitPullNotSelected.UseVisualStyleBackColor = true
        AddHandler Me.btnGitPullNotSelected.Click, AddressOf Me.BtnGitPullNotSelected_Click
        '
        'chkRepeat
        '
        Me.chkRepeat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chkRepeat.AutoSize = true
        Me.chkRepeat.BackColor = System.Drawing.Color.Transparent
        Me.chkRepeat.Checked = true
        Me.chkRepeat.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRepeat.Location = New System.Drawing.Point(272, 338)
        Me.chkRepeat.Name = "chkRepeat"
        Me.chkRepeat.Size = New System.Drawing.Size(125, 17)
        Me.chkRepeat.TabIndex = 20
        Me.chkRepeat.Text = "Repeat until success"
        Me.chkRepeat.UseVisualStyleBackColor = false
        '
        'folderBrowserDialog
        '
        Me.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyDocuments
        Me.folderBrowserDialog.SelectedPath = "GitHub"
        '
        'chkPushForce
        '
        Me.chkPushForce.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.chkPushForce.AutoSize = true
        Me.chkPushForce.BackColor = System.Drawing.Color.Transparent
        Me.chkPushForce.Location = New System.Drawing.Point(272, 321)
        Me.chkPushForce.Name = "chkPushForce"
        Me.chkPushForce.Size = New System.Drawing.Size(116, 17)
        Me.chkPushForce.TabIndex = 21
        Me.chkPushForce.Text = "Use push -f (Force)"
        Me.chkPushForce.UseVisualStyleBackColor = false
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
        '
        'progressBar
        '
        Me.progressBar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.progressBar.Location = New System.Drawing.Point(266, 99)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(246, 18)
        Me.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.progressBar.TabIndex = 24
        Me.progressBar.Visible = false
        '
        'grpOptions
        '
        Me.grpOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grpOptions.Controls.Add(Me.chkNoWait)
        Me.grpOptions.Controls.Add(Me.chkDontClose)
        Me.grpOptions.Location = New System.Drawing.Point(266, 229)
        Me.grpOptions.Name = "grpOptions"
        Me.grpOptions.Size = New System.Drawing.Size(246, 64)
        Me.grpOptions.TabIndex = 25
        Me.grpOptions.TabStop = false
        Me.grpOptions.Text = "Options"
        '
        'grpCreds
        '
        Me.grpCreds.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
                        Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grpCreds.Controls.Add(Me.btnShowPass)
        Me.grpCreds.Controls.Add(Me.lblSaved)
        Me.grpCreds.Controls.Add(Me.btnSave)
        Me.grpCreds.Controls.Add(Me.txtPassword)
        Me.grpCreds.Controls.Add(Me.txtUsername)
        Me.grpCreds.Controls.Add(Me.lblPassword)
        Me.grpCreds.Controls.Add(Me.lblUsername)
        Me.grpCreds.Location = New System.Drawing.Point(266, 123)
        Me.grpCreds.Name = "grpCreds"
        Me.grpCreds.Size = New System.Drawing.Size(246, 100)
        Me.grpCreds.TabIndex = 26
        Me.grpCreds.TabStop = false
        Me.grpCreds.Text = "Credientials Management"
        '
        'btnShowPass
        '
        Me.btnShowPass.BackColor = System.Drawing.Color.White
        Me.btnShowPass.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnShowPass.FlatAppearance.BorderSize = 0
        Me.btnShowPass.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight
        Me.btnShowPass.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.btnShowPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowPass.ForeColor = System.Drawing.Color.Transparent
        Me.btnShowPass.Image = CType(resources.GetObject("btnShowPass.Image"),System.Drawing.Image)
        Me.btnShowPass.Location = New System.Drawing.Point(218, 47)
        Me.btnShowPass.Name = "btnShowPass"
        Me.btnShowPass.Size = New System.Drawing.Size(20, 16)
        Me.btnShowPass.TabIndex = 27
        Me.btnShowPass.UseVisualStyleBackColor = false
        AddHandler Me.btnShowPass.MouseDown, AddressOf Me.btnShowPass_MouseDown
        AddHandler Me.btnShowPass.MouseUp, AddressOf Me.btnShowPass_MouseUp
        '
        'lblSaved
        '
        Me.lblSaved.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblSaved.AutoSize = true
        Me.lblSaved.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblSaved.ForeColor = System.Drawing.Color.Green
        Me.lblSaved.Location = New System.Drawing.Point(112, 76)
        Me.lblSaved.Name = "lblSaved"
        Me.lblSaved.Size = New System.Drawing.Size(47, 13)
        Me.lblSaved.TabIndex = 5
        Me.lblSaved.Text = "Saved!"
        Me.lblSaved.Visible = false
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(165, 71)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = true
        AddHandler Me.btnSave.Click, AddressOf Me.BtnSave_Click
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(70, 45)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtPassword.Size = New System.Drawing.Size(170, 20)
        Me.txtPassword.TabIndex = 3
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(70, 19)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(170, 20)
        Me.txtUsername.TabIndex = 2
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = true
        Me.lblPassword.Location = New System.Drawing.Point(6, 48)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 1
        Me.lblPassword.Text = "Password:"
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = true
        Me.lblUsername.Location = New System.Drawing.Point(6, 22)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(58, 13)
        Me.lblUsername.TabIndex = 0
        Me.lblUsername.Text = "Username:"
        '
        'timerKeyChecker
        '
        AddHandler Me.timerKeyChecker.Tick, AddressOf Me.TimerKeyChecker_Tick
        '
        'GitUpdater
        '
        Me.AcceptButton = Me.btnGitPushSelected
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(524, 363)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnCD)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.grpCreds)
        Me.Controls.Add(Me.grpOptions)
        Me.Controls.Add(Me.progressBar)
        Me.Controls.Add(Me.chkPushForce)
        Me.Controls.Add(Me.btnGitPullNotSelected)
        Me.Controls.Add(Me.btnGitPushNotSelected)
        Me.Controls.Add(Me.btnGitPushSelected)
        Me.Controls.Add(Me.btnGitPullSelected)
        Me.Controls.Add(Me.lstDirs)
        Me.Controls.Add(Me.btnGitPushAll)
        Me.Controls.Add(Me.btnGitPullAll)
        Me.Controls.Add(Me.chkRepeat)
        Me.HelpButton = true
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "GitUpdater"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GitUpdater"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        AddHandler Load, AddressOf Me.GitUpdater_Load
        Me.grpOptions.ResumeLayout(false)
        Me.grpOptions.PerformLayout
        Me.grpCreds.ResumeLayout(false)
        Me.grpCreds.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout
    End Sub
    Private btnShowPass As System.Windows.Forms.Button
    Private timerKeyChecker As System.Windows.Forms.Timer
    Private lblUsername As System.Windows.Forms.Label
    Private lblPassword As System.Windows.Forms.Label
    Private txtUsername As System.Windows.Forms.TextBox
    Private txtPassword As System.Windows.Forms.TextBox
    Private btnSave As System.Windows.Forms.Button
    Private lblSaved As System.Windows.Forms.Label
    Private grpCreds As System.Windows.Forms.GroupBox
    Private grpOptions As System.Windows.Forms.GroupBox
    Private progressBar As System.Windows.Forms.ProgressBar
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
    Private lstDirs As System.Windows.Forms.ListBox
    Private btnGitPushAll As System.Windows.Forms.Button
    Private btnGitPullAll As System.Windows.Forms.Button
    Private btnExit As System.Windows.Forms.Button
End Class
