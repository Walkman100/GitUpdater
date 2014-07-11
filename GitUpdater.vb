Imports System
Imports System.IO

Public Class GitUpdater
    
    Dim usrProfile As String = Environment.GetEnvironmentVariable("HOMEPATH")
    Dim Dir As String = usrProfile & "\Documents\GitHub"
    Dim cmdRepo As String = ""
    
    Dim Wait As Integer = -1   'Wait until cmd closes
    Dim CmdStyle As AppWinStyle = AppWinStyle.MinimizedFocus   'window location of CMD
    Dim ForcePush As String = ""
    
    Dim count, GitCommand As String   'because the Worker doesn't support direct sub calling
    
    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        End
    End Sub
    
    Sub GitUpdater_Load(sender As Object, e As EventArgs)
        RebuildRepoList
        
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
    
    Sub ShellWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
        If chkNoWait.Checked = True Then Wait = 1000 Else Wait = -1
        If chkDontShow.Checked = True Then
            CmdStyle = vbMinimizedNoFocus
        Else
            CmdStyle = vbNormalFocus
            Me.TopMost = True
        End If
        If chkPushForce.Checked = True Then ForcePush = "-f"
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
        
' another way to do a comment block: http://forums.asp.net/post/4414215.aspx
'            'MsgBox("/k cd " & Dir & "\" & lstDirs.Items.Item(i - 1))
'            Process.Start(cmdPath, "/k cd " & Dir & "\" & lstDirs.Items.Item(i - 1))
'            System.Threading.Thread.Sleep(200)
'            'Shell(cmdPath & " /k cd " & Dir & "\" & lstDirs.Items.Item(i - 1) & "\git pull", vbNormalFocus, True)
'            sendkeys.send("git pull {ENTER}")
'            System.Threading.Thread.Sleep(100)
'            If chkDontClose.Checked = False Then
'                SendKeys.Send("exit {ENTER}")
'            End If
'            System.Threading.Thread.Sleep(100)
'            'Process.Start(cmdPath, "/k " & Dir & "\" & lstDirs.Items.Item(i - 1) & "\git pull")
'            'Shell(Dir & "\" & lstDirs.Items.Item(i - 1) & "\git pull", vbNormalFocus, True)
        
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
End Class