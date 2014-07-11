Imports System
Imports System.IO

Public Class GitUpdater
    
    Dim usrProfile As String = Environment.GetEnvironmentVariable("HOMEPATH")
    Dim Dir As String = usrProfile & "\Documents\GitHub"
    Dim cmdRepo As String = ""
    
    Dim count, GitCommand As String   'because the Worker doesn't support direct sub calling
    
    Dim ForcePush As String = ""
    Dim CmdStyle As AppWinStyle = vbMinimizedFocus   'window location of CMD
    Dim Wait As Integer = -1   'Wait until cmd closes
    
    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        End
    End Sub
    
    Sub LoadGitUpdater(sender As Object, e As EventArgs)
        RebuildRepoList
        ' apply settings to where they are changed
        txtUsername.Text = My.Settings.Username
        txtPassword.Text = My.Settings.Password
        'chkRememberBrowser.Checked = My.Settings.RememberBrowser

        ' apply settings to where they affect
        'none yet
        
        Dim rundir As Boolean = False
        Dim gitcmd As Boolean = False
        Dim fullRepoPath As Boolean = True
        Dim tmpGitCommand As String = ""
        'Command Line Args
        For Each s As String In My.Application.CommandLineArgs
            If gitcmd = False Then
                If rundir = False Then
                    If s = "-dir" Or s = "/dir" Or s = "\dir" Or s = "dir" Then
                        rundir = True
                    Else
                        tmpGitCommand = s
                        gitcmd = True
                    End If
                Else
                    If fullRepoPath = True Then
                        If File.Exists(s) Then
                            Dir = Strings.Left(s, Len(s)-Len(cmdRepo))
                            fullRepoPath = False
                        Else
                            cmdRepo = s
                            rundir = False
                        End If
                    Else
                        cmdRepo = s
                        rundir = False
                        fullRepoPath = True
                    End If
                End If
            Else
                If s = "all" Or s = "selected" Or s = "notselected" Or s = "cmdselected" Or s = "cmdnotselected" Then
                    count = s
                    GitCommand = tmpGitCommand
                    If ShellWorker.IsBusy = False Then
                        ShellWorker.RunWorkerAsync
                    Else
                        MsgBox("A script is currently in progress!")
                    End If
                    gitcmd = False
                End If
            End If
            
        Next
    End Sub
    
    ' to do with list of repos
    
    Sub RebuildRepoList
        lstRepos.Items.Clear
        For Each Repo As String In Directory.GetDirectories(Dir)
            lstRepos.Items.Add(Mid(Repo, Len(Dir) + 2))
        Next
    End Sub
    
    Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        If ShellWorker.IsBusy = False Then
            RebuildRepoList
        ElseIf MsgBox("A script is currently in progress! Refreshing repos might mess up the script. You can use the cancel button above to cancel operation." & vbNewLine &vbNewLine & "Refresh anyway?", vbOKCancel, "Operation in progress") = vbOK
            RebuildRepoList
        End If
    End Sub
    
    Sub BtnCD_Click(sender As Object, e As EventArgs)
        If ShellWorker.IsBusy = False Then
            ' show file chooser dialog, set result as Dir
            folderBrowserDialog.ShowDialog
            Dir = folderBrowserDialog.SelectedPath
            
            ' rebuild list automatically
            RebuildRepoList
        Else
            MsgBox("A script is currently in progress! Changing directory will mess up the script. Please cancel using the button above first.")
        End If
        
    End Sub
    
    ' how to run the shells
    
    Sub ChkNoWait_CheckedChanged(sender As Object, e As EventArgs)
        If chkNoWait.Checked = True Then Wait = 1000 Else Wait = -1
    End Sub
    
    Sub ChkDontShow_CheckedChanged(sender As Object, e As EventArgs)
        If chkDontShow.Checked = True Then
            CmdStyle = vbMinimizedNoFocus
            If ShellWorker.IsBusy = True Then
                Me.TopMost = False
            End If
        ElseIf chkDontShow.Checked = False Then
            CmdStyle = vbNormalFocus
            If ShellWorker.IsBusy = True Then
                Me.TopMost = True
            End If
        End If
    End Sub
    
    Sub ChkPushForce_CheckedChanged(sender As Object, e As EventArgs)
        If chkPushForce.Checked = True Then ForcePush = "-f" Else ForcePush = ""
    End Sub
    
    ' actual code that runs the shells
    
    Sub ShellWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
        If chkDontShow.Checked = False Then Me.TopMost = True
        progressBar.Maximum = lstRepos.Items.Count
        
        If count = "all" Then
            For i = 1 To lstRepos.Items.Count
                Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(i - 1) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked, CmdStyle, True, Wait)
                progressBar.Value = i
            Next
            
        ElseIf count = "selected" Then
            If lstRepos.SelectedIndex = -1 Then
                MsgBox("No item selected")
            Else
                progressBar.Maximum = 2
                progressBar.Value = 1
                Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked, CmdStyle, True, Wait)
                progressBar.Value = 2
            End If
            
        ElseIf count = "notselected" Then
            If lstRepos.SelectedIndex = -1 Then
                MsgBox("No item selected")
            Else
                For i = 1 To lstRepos.Items.Count
                    If i - 1 <> lstRepos.SelectedIndex Then
                        Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(i - 1) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked, CmdStyle, True, Wait)
                    End If
                    progressBar.Value = i
                Next
            End If
            
        ElseIf count = "cmdselected" Then
            If cmdRepo = "" Then
                MsgBox("No repo passed from command line")
            Else
                progressBar.Maximum = 2
                progressBar.Value = 1
                Shell("GitUpdater.bat " & Dir & "\" & cmdRepo & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked, CmdStyle, True, Wait)
                progressBar.Value = 2
            End If
            
        ElseIf count = "cmdnotselected" Then
            If cmdRepo = "" Then
                MsgBox("No repo passed from command line")
            Else
                For i = 1 To lstRepos.Items.Count
                    If lstRepos.Items.Item(i - 1) <> cmdRepo Then
                        Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(i - 1) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked, CmdStyle, True, Wait)
                        progressBar.Value = i
                    End If
                Next
            End If
            
        End If
        Me.TopMost = False
        MsgBox("Succesfully completed!")
    End Sub
    
    ' starting and stopping the thread
    
    Sub BtnCancel_Click(sender As Object, e As EventArgs)
        If ShellWorker.IsBusy = True Then
            If MsgBox("Are you sure you want to cancel operation? This requires restarting GitUpdater." & vbNewLine & vbNewLine & "This will not close the currently active CMD window. To do so, please click on the window and press 'Ctrl' + 'C', then 'Y', then 'Enter'.", vbYesNo, "Confirmation") = vbNo Then Exit Sub
            Application.Restart
        Else
            MsgBox("No git operation is currently in progress!")
        End If
        
    End Sub
    
    Sub BtnGitPullAll_Click(sender As Object, e As EventArgs)
        count = "all"
        GitCommand = "pull"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync
        Else
            MsgBox("A git operation is currently in progress!", , "Operation in progress")
        End If
    End Sub
    
    Sub BtnGitPushAll_Click(sender As Object, e As EventArgs)
        count = "all"
        GitCommand = "push"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync
        Else
            MsgBox("A git operation is currently in progress!", ,"Operation in progress")
        End If
    End Sub
    
    Sub BtnGitPullSelected_Click(sender As Object, e As EventArgs)
        count = "selected"
        GitCommand = "pull"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync
        Else
            MsgBox("A git operation is currently in progress!", ,"Operation in progress")
        End If
    End Sub
    
    Sub BtnGitPushSelected_Click(sender As Object, e As EventArgs)
        count = "selected"
        GitCommand = "push"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync
        Else
            MsgBox("A git operation is currently in progress!", ,"Operation in progress")
        End If
    End Sub
    
    Sub BtnGitPullNotSelected_Click(sender As Object, e As EventArgs)
        count = "notselected"
        GitCommand = "pull"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync
        Else
            MsgBox("A git operation is currently in progress!", ,"Operation in progress")
        End If
    End Sub
    
    Sub BtnGitPushNotSelected_Click(sender As Object, e As EventArgs)
        count = "notselected"
        GitCommand = "push"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync
        Else
            MsgBox("A git operation is currently in progress!", ,"Operation in progress")
        End If
    End Sub
    
    ' credentials-related stuff
    
    Sub BtnSave_Click(sender As Object, e As EventArgs)
        My.Settings.Username = txtUsername.Text
        My.Settings.Password = txtPassword.Text
        My.Settings.Save()
        MsgBox("Succesfully Saved!", , "Saved!")
    End Sub
    
    Sub BtnInsert_Click(sender As Object, e As EventArgs)
        Me.WindowState = FormWindowState.Minimized
        System.Threading.Thread.Sleep(1000)
        SendKeys.send(txtUsername.Text & "{ENTER}")
        SendKeys.send(txtPassword.Text & "{ENTER}")
        System.Threading.Thread.Sleep(1000)
        Me.WindowState = FormWindowState.Normal
    End Sub
    
    Sub TimerKeyChecker_Tick(sender As Object, e As EventArgs)
        If My.Computer.Keyboard.AltKeyDown = True Then
            SendKeys.send(txtUsername.Text & "{ENTER}")
            SendKeys.send(txtPassword.Text & "{ENTER}")
        End If
    End Sub
    
    Sub BtnHotkey_Click(sender As Object, e As EventArgs)
        If btnHotkey.Text = "Hotkey On" Then
            btnHotkey.Text = "Hotkey Off"
            timerKeyChecker.Start
        ElseIf btnHotkey.Text = "Hotkey Off" Then
            btnHotkey.Text = "Hotkey On"
            timerKeyChecker.Stop
        End If
    End Sub
    
    Sub btnShowPass_MouseDown(sender As Object, e As EventArgs)
        txtPassword.PasswordChar = ""
    End Sub
    
    Sub btnShowPass_MouseUp(sender As Object, e As EventArgs)
        txtPassword.PasswordChar = "●"
    End Sub
End Class