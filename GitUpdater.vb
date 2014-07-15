Imports System
Imports System.IO

Public Class GitUpdater
    
    Dim Dir As String = Environment.GetEnvironmentVariable("HOMEPATH") & "\Documents\GitHub"
    Dim cmdRepo As String = ""
    Dim count, GitCommand As String  ' because the Worker doesn't support direct sub calling
    Dim ExitWhenDone As Boolean = False
    
    Dim ForcePush As String
    Dim CmdStyle As AppWinStyle  ' window location of CMD
    Dim Wait As Integer  ' Wait until cmd closes
    
    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        End
    End Sub
    
    Sub LoadGitUpdater(sender As Object, e As EventArgs)
        If Not File.Exists("GitUpdater.bat") Then
            Try
                My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/GitUpdater.bat", "GitUpdater.bat")
            Catch ex As Exception
                MsgBox("Could not automatically download the required file! Please download it manually. Click OK to open the download page.", MsgBoxStyle.Exclamation)
                Process.Start("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/GitUpdater.bat")
            End Try
        End If
        RebuildRepoList
        ' apply settings to where they are changed
        txtUsername.Text = My.Settings.Username
        txtPassword.Text = My.Settings.Password
        chkNoWait.Checked = My.Settings.NoWait
        chkDontClose.Checked = My.Settings.DontClose
        chkDontShow.Checked = My.Settings.DontShow
        chkPushForce.Checked = My.Settings.PushForce
        chkRepeat.Checked = My.Settings.Repeat
        chkLog.Checked = My.Settings.Log
        
        ' apply settings to where they affect
        If My.Settings.SavedDir <> "" Then
            Dir = My.Settings.SavedDir
        End If
        If My.Settings.NoWait = True Then Wait = 1000 Else Wait = -1
        If My.Settings.DontShow = True Then
            CmdStyle = vbMinimizedNoFocus
            If ShellWorker.IsBusy = True Then
                Me.TopMost = False
            End If
        ElseIf My.Settings.DontShow = False Then
            CmdStyle = vbNormalFocus
            If ShellWorker.IsBusy = True Then
                Me.TopMost = True
            End If
        End If
        If My.Settings.PushForce = True Then ForcePush = "-f" Else ForcePush = ""
        
        ' command line args
        For Each s As String In My.Application.CommandLineArgs
            If s.ToLower.StartsWith("-gitcmd=") Then
                GitCommand = s.Remove(0, 8)
            End If
            If s.ToLower.StartsWith("-gitwhat=") Then
                count = s.Remove(0, 9)
            End If
            If s.ToLower.StartsWith("-dir=") Then
                Dir = s.Remove(0, 5)
                RebuildRepoList
            End If
            If s.ToLower.StartsWith("-repo=") Then
                cmdRepo = s.Remove(0, 6)
            End If
            If s.ToLower.StartsWith("run") Then
                If ShellWorker.IsBusy = False Then
                    ShellWorker.RunWorkerAsync
                Else
                    MsgBox("A script is currently in progress!")
                End If
            End If
            If s.ToLower.StartsWith("exitwhendone") Then
                ExitWhenDone = True
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
    
    Sub LstRepos_DoubleClick(sender As Object, e As EventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            Process.Start("explorer.exe", Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex))
        Else
            Process.Start("explorer.exe", Dir)
        End If
    End Sub
    
    Sub ContextMenuStripReposOpenInCMD_Click(sender As Object, e As EventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            Process.Start("cmd.exe", "/k cd " & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex))
        Else
            Process.Start("cmd.exe", "/k cd " & Dir)
        End If
    End Sub
    
    Sub ContextMenuStripReposOpenInPS_Click(sender As Object, e As EventArgs)
        If File.Exists("OpenRepoInPS.bat") Then
            If lstRepos.SelectedIndex <> -1 Then
                Process.Start("OpenRepoInPS.bat", """" & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & """ " & Environment.CurrentDirectory)
            Else
                Process.Start("OpenRepoInPS.bat", """" & Dir & """ " & Environment.CurrentDirectory)
            End If
        Else
            Try
                My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/OpenRepoInPS.bat", "OpenRepoInPS.bat")
                ContextMenuStripReposOpenInPS_Click(Nothing, Nothing) ' Essentially restart the sub (but not quite)
            Catch ex As Exception
                MsgBox("Could not automatically download the required file! Please download it manually. Click OK to open the download page.", MsgBoxStyle.Exclamation)
                Process.Start("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/OpenRepoInPS.bat")
            End Try
        End If
    End Sub
    
    Sub ContextMenuStripReposOpenInGitHub_Click(sender As Object, e As EventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            Process.Start("github-windows://openRepo/" & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex))
        Else
            Process.Start("github-windows://openRepo/" & Dir)
        End If
    End Sub
    
    Sub ContextMenuStripReposOpenReadme_Click(sender As Object, e As EventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            If File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme.md") Then
                Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme.md")
            ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme.txt") Then
                Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme.txt")
            ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme.htm") Then
                Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme.htm")
            ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme.html") Then
                Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme.html")
            ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme.markdown") Then
                Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme.markdown")
            ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme.mkd") Then
                Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme.mkd")
            ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme") Then
                Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\Readme")
            Else
                MsgBox("No readme found in repo:" & vbNewLine & """" & Dir & "\" & _
                lstRepos.Items.Item(lstRepos.SelectedIndex) & """" & vbNewLine & _
                "With filename: Readme.md, Readme.txt, Readme.htm, " & _
                "Readme.html, Readme.markdown, Readme.mkd, or Readme.")
            End If
        Else
            
        End If
    End Sub
    
    Sub ContextMenuStripReposCopyRepoName_Click(sender As Object, e As EventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            Clipboard.SetText(lstRepos.Items.Item(lstRepos.SelectedIndex))
        Else
            Clipboard.SetText(Dir)
        End If
    End Sub
    
    Sub ContextMenuStripReposCopyRepoPath_Click(sender As Object, e As EventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            Clipboard.SetText(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex))
        Else
            Clipboard.SetText(Dir)
        End If
    End Sub
    
    ' how to run the shells & changing settings
    
    Sub ChkNoWait_CheckedChanged(sender As Object, e As EventArgs)
        If chkNoWait.Checked = True Then Wait = 1000 Else Wait = -1
        My.Settings.NoWait = chkNoWait.Checked
        My.Settings.Save()
    End Sub
    
    Sub ChkDontClose_CheckedChanged(sender As Object, e As EventArgs)
        My.Settings.DontClose = chkDontClose.Checked
        My.Settings.Save()
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
        My.Settings.DontShow = chkDontShow.Checked
        My.Settings.Save()
    End Sub
    
    Sub ChkPushForce_CheckedChanged(sender As Object, e As EventArgs)
        If chkPushForce.Checked = True Then ForcePush = "-f" Else ForcePush = ""
        My.Settings.PushForce = chkPushForce.Checked
        My.Settings.Save()
    End Sub
    
    Sub ChkRepeat_CheckedChanged(sender As Object, e As EventArgs)
        My.Settings.Repeat = chkRepeat.Checked
        My.Settings.Save()
    End Sub
    
    Sub ChkLog_CheckedChanged(sender As Object, e As EventArgs)
        My.Settings.Log = chkLog.Checked
        My.Settings.Save()
    End Sub
    
    ' actual code that runs the shells
    
    Sub ShellWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
        If chkDontShow.Checked = False Then Me.TopMost = True
        progressBar.Maximum = lstRepos.Items.Count
        
        Select Case count
            
        Case = "all"
            For i = 1 To lstRepos.Items.Count
                Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(i - 1) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & ForcePush, CmdStyle, True, Wait)
                progressBar.Value = i
            Next
            
        Case = "selected"
            If lstRepos.SelectedIndex = -1 Then
                MsgBox("No item selected")
            Else
                progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
                Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & ForcePush, CmdStyle, True, Wait)
                progressBar.Style = System.Windows.Forms.ProgressBarStyle.Blocks
                progressBar.Value = progressBar.Maximum
            End If
            
        Case = "notselected"
            If lstRepos.SelectedIndex = -1 Then
                MsgBox("No item selected")
            Else
                For i = 1 To lstRepos.Items.Count
                    If i - 1 <> lstRepos.SelectedIndex Then
                        Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(i - 1) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & ForcePush, CmdStyle, True, Wait)
                    End If
                    progressBar.Value = i
                Next
            End If
            
        Case = "cmdselected"
            If cmdRepo = "" Then
                MsgBox("No repo passed from command line")
            Else
                progressBar.Maximum = 2
                progressBar.Value = 1
                Shell("GitUpdater.bat " & Dir & "\" & cmdRepo & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & ForcePush, CmdStyle, True, Wait)
                progressBar.Value = 2
            End If
            
        Case = "cmdnotselected"
            If cmdRepo = "" Then
                MsgBox("No repo passed from command line")
            Else
                For i = 1 To lstRepos.Items.Count
                    If lstRepos.Items.Item(i - 1) <> cmdRepo Then
                        Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(i - 1) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & ForcePush, CmdStyle, True, Wait)
                        progressBar.Value = i
                    End If
                Next
            End If
            
        End Select
        
        If ExitWhenDone = True Then
            End
        End If
        Me.TopMost = False
    End Sub
    
    ' starting and stopping the thread
    
    Sub BtnCancel_Click(sender As Object, e As EventArgs)
        If ShellWorker.IsBusy = True Then
            If MsgBox("Are you sure you want to cancel operation? This requires restarting GitUpdater." & vbNewLine & vbNewLine & "This will not close the currently active CMD window. To do so, please click on the window and press 'Ctrl' + 'C', then 'Y', then 'Enter'.", vbYesNo, "Confirmation") = vbNo Then Exit Sub
            Application.Restart
        Else
            MsgBox("No git operation is currently in progress!", MsgBoxStyle.Information)
        End If
        
    End Sub
    
    Sub BtnGitPullAll_Click(sender As Object, e As EventArgs)
        count = "all"
        GitCommand = "pull"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync
        Else
            MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
        End If
    End Sub
    
    Sub BtnGitPushAll_Click(sender As Object, e As EventArgs)
        count = "all"
        GitCommand = "push"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync
        Else
            MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
        End If
    End Sub
    
    Sub BtnGitPullSelected_Click(sender As Object, e As EventArgs)
        count = "selected"
        GitCommand = "pull"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync
        Else
            MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
        End If
    End Sub
    
    Sub BtnGitPushSelected_Click(sender As Object, e As EventArgs)
        count = "selected"
        GitCommand = "push"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync
        Else
            MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
        End If
    End Sub
    
    Sub BtnGitPullNotSelected_Click(sender As Object, e As EventArgs)
        count = "notselected"
        GitCommand = "pull"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync
        Else
            MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
        End If
    End Sub
    
    Sub BtnGitPushNotSelected_Click(sender As Object, e As EventArgs)
        count = "notselected"
        GitCommand = "push"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync
        Else
            MsgBox("A git operation is currently in progress!", MsgBoxStyle.Information, "Operation in progress")
        End If
    End Sub
    
    ' credentials-related stuff
    
    Sub BtnSave_Click(sender As Object, e As EventArgs)
        My.Settings.Username = txtUsername.Text
        My.Settings.Password = txtPassword.Text
        My.Settings.Save()
        MsgBox("Succesfully Saved!", MsgBoxStyle.Information, "Saved!")
    End Sub
    
    Sub BtnInsert_Click(sender As Object, e As EventArgs)
        Me.WindowState = FormWindowState.Minimized
        System.Threading.Thread.Sleep(1000)
        SendKeys.SendWait(txtUsername.Text & "~")
        SendKeys.SendWait(txtPassword.Text & "~")
        Me.WindowState = FormWindowState.Normal
    End Sub
    
    Sub TimerKeyChecker_Tick(sender As Object, e As EventArgs)
        If My.Computer.Keyboard.AltKeyDown = True Then
            SendKeys.send(txtUsername.Text & "~")
            SendKeys.send(txtPassword.Text & "~")
            ' See http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send(v=vs.110).aspx
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
    
    Sub BtnCloseCmd_Click(sender As Object, e As EventArgs)
        If ShellWorker.IsBusy = False Then
            If MsgBox("No git operation from this program is in progress, are you sure you want to insert commands to close a CMD window?", vbYesNo, "Confirmation") = vbNo Then Exit Sub
        End If
        Me.WindowState = FormWindowState.Minimized
        System.Threading.Thread.Sleep(500)
        SendKeys.SendWait("^C")
        SendKeys.SendWait("Y")
        SendKeys.SendWait("~")
        Me.WindowState = FormWindowState.Normal
    End Sub
End Class