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
        btnGitPullAll.Enabled = True
        btnGitPushAll.Enabled = True
        btnGitPullSelected.Enabled = True
        btnGitPushSelected.Enabled = True
        btnGitPullNotSelected.Enabled = True
        btnGitPushNotSelected.Enabled = True
        btnCD.Enabled = True
        btnCancel.Enabled = False
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
        ElseIf MsgBox("A script is currently in progress! Refreshing repos might mess up the script. You can use the cancel button above to cancel operation." _
                & vbNewLine &vbNewLine & "Refresh anyway?", vbOKCancel, "Operation in progress") = vbOK
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
            MsgBox("A script is currently in progress! Changing directory will mess up the script. Please cancel using the button above first.", MsgBoxStyle.Critical)
        End If
    End Sub
    
    Sub LstRepos_DoubleClick(sender As Object, e As EventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex))
        Else
            Process.Start(Dir)
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
            If MsgBox("Couldn't find PowerShell script. This program can attempt to download it and put it in the right place, Continue?", vbYesNo, "OpenRepoInPS.bat not found!") = vbNo Then Exit Sub
            Try
                My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/OpenRepoInPS.bat", "OpenRepoInPS.bat")
                ContextMenuStripReposOpenInPS_Click(Nothing, Nothing) ' Essentially restart the sub (but not quite)
            Catch ex As Exception
                MsgBox("Could not automatically download the required file! Please download it manually. Click OK to open the download page.", MsgBoxStyle.Exclamation)
                Process.Start("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/OpenRepoInPS.bat")
            End Try
            
            ' using the zip file
            Try
                My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/PSScripts.zip", "PSScripts.zip")
                ' Remove the next two lines when UnZipping has been sorted out:
                MsgBox("ZIP file containing the required files has been downloaded, please extract it to the same folder as this program.")
                Process.Start(Environment.CurrentDirectory & "\PSScripts.zip")
            Catch ex As Exception
                MsgBox("Could not automatically download the zip folder containing the required files! Please download it manually. Click OK to open the download page.", MsgBoxStyle.Exclamation)
                Process.Start("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/PSScripts.zip")
                Exit Sub
            End Try
            'Try
            '    System.IO.Compression.ZipFile.ExtractToDirectory("PSScripts.zip", "./")
            '    IO.Compression.ZipFile.ExtractToDirectory("PSScripts.zip", "./")
            '    Compression.ZipFile.ExtractToDirectory("PSScripts.zip", "./")
            '    ZipFile.File.ExtractToDirectory("PSScripts.zip", "./")
            '    ExtractToDirectory("PSScripts.zip", "./")
            'Catch ex As Exception
            '    MsgBox("Could not automatically unzip the file containing the required files! Please extract it manually. Click OK to show it.", MsgBoxStyle.Exclamation)
            '    Process.Start("explorer.exe", Environment.CurrentDirectory & "\PSScripts.zip")
            '    Exit Sub
            'End Try
        End If
    End Sub
    
    Sub ContextMenuStripReposOpenInGitHub_Click(sender As Object, e As EventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            Try
                Process.Start("github-windows://openRepo/" & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex))
            Catch
                MsgBox("GitHub for windows protocol not found!", MsgBoxStyle.Critical)
            End Try
        Else
            Try
                Process.Start("github-windows://openRepo/" & Dir)
            Catch
                MsgBox("GitHub for windows protocol not found!", MsgBoxStyle.Critical)
            End Try
        End If
    End Sub
    
    Sub ContextMenuStripReposOpenReadme_Click(sender As Object, e As EventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            If lstRepos.Items.Item(lstRepos.SelectedIndex).ToString.EndsWith(".wiki") Then
                If File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\home.md") Then
                 Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\home.md")
                Else
                    MsgBox("No file found in wiki folder:" & vbNewLine & _
                        """" & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & """" & vbNewLine & _
                        "With filename: home.md", MsgBoxStyle.Critical)
                End If
            Else
                If File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme.md") Then
                 Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme.md")
                ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme.txt") Then
                     Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme.txt")
                ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme.htm") Then
                     Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme.htm")
                ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme.html") Then
                     Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme.html")
                ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme.markdown") Then
                     Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme.markdown")
                ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme.mkd") Then
                     Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme.mkd")
                ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme") Then
                     Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\readme")
                Else
                    MsgBox("No file found in repo:" & vbNewLine & _
                        """" & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & """" & vbNewLine & _
                        "With filename: readme.md, readme.txt, readme.htm, readme.html, readme.markdown, readme.mkd, or readme.", MsgBoxStyle.Critical)
                End If
            End If
        Else
            If File.Exists(Dir & "\readme.md") Then
             Process.Start(Dir & "\readme.md")
            ElseIf File.Exists(Dir & "\readme.txt") Then
                 Process.Start(Dir & "\readme.txt")
            ElseIf File.Exists(Dir & "\readme.htm") Then
                 Process.Start(Dir & "\readme.htm")
            ElseIf File.Exists(Dir & "\readme.html") Then
                 Process.Start(Dir & "\readme.html")
            ElseIf File.Exists(Dir & "\readme.markdown") Then
                 Process.Start(Dir & "\readme.markdown")
            ElseIf File.Exists(Dir & "\readme.mkd") Then
                 Process.Start(Dir & "\readme.mkd")
            ElseIf File.Exists(Dir & "\readme") Then
                 Process.Start(Dir & "\readme")
            Else
                MsgBox("No file found in folder:" & vbNewLine & _
                    """" & Dir & """" & vbNewLine & _
                    "With filename: readme.md, readme.txt, readme.htm, readme.html, readme.markdown, readme.mkd, or readme.", MsgBoxStyle.Critical)
            End If
        End If
    End Sub
    
    Sub ContextMenuStripReposOpenSLN_Click(sender As Object, e As EventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            If File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & ".sln") Then
             Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & ".sln")
            ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & ".sln") Then
                 Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & ".sln")
            'ElseIf File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & ".sln") Then
            '     Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & ".sln")
            Else
                MsgBox("No file found in locations:" & vbNewLine & _
                    """" & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & """" & vbNewLine & _
                    """" & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & """" & vbNewLine & _
                    "With filename: """ & lstRepos.Items.Item(lstRepos.SelectedIndex) & ".sln""", MsgBoxStyle.Critical)
            End If
        Else
            If File.Exists(Dir & "\" & "GitHub.sln") Then
             Process.Start(Dir & "\" & "GitHub.sln")
            ElseIf File.Exists(Dir & ".sln") Then
                 Process.Start(Dir & ".sln")
            Else
                MsgBox("No file found in locations:" & vbNewLine & _
                    """" & Dir & "\" & "GitHub.sln""" & vbNewLine & _
                    """" & Dir & ".sln""", MsgBoxStyle.Critical)
            End If
        End If
    End Sub
    
    Sub ContextMenuStripReposCopyRepoName_Click(sender As Object, e As EventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            Try
                Clipboard.SetText(lstRepos.Items.Item(lstRepos.SelectedIndex), TextDataFormat.UnicodeText)
                MsgBox(lstRepos.Items.Item(lstRepos.SelectedIndex) & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
            Catch ex As Exception
                MsgBox("Copy failed!" & vbNewLine & "Error: " & ex.ToString, MsgBoxStyle.Critical, "Copy failed!")
            End Try
        Else
            Try
                Clipboard.SetText(Dir, TextDataFormat.UnicodeText)
                MsgBox(Dir & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
            Catch ex As Exception
                MsgBox("Copy failed!" & vbNewLine & "Error: " & ex.ToString, MsgBoxStyle.Critical, "Copy failed!")
            End Try
        End If
    End Sub
    
    Sub ContextMenuStripReposCopyRepoPath_Click(sender As Object, e As EventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            Try
                Clipboard.SetText(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex), TextDataFormat.UnicodeText)
                MsgBox(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
            Catch ex As Exception
                MsgBox("Copy failed!" & vbNewLine & "Error: " & ex.ToString, MsgBoxStyle.Critical, "Copy failed!")
            End Try
        Else
            Try
                Clipboard.SetText(Dir, TextDataFormat.UnicodeText)
                MsgBox(Dir & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
            Catch ex As Exception
                MsgBox("Copy failed!" & vbNewLine & "Error: " & ex.ToString, MsgBoxStyle.Critical, "Copy failed!")
            End Try
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
        btnGitPullAll.Enabled = False
        btnGitPushAll.Enabled = False
        btnGitPullSelected.Enabled = False
        btnGitPushSelected.Enabled = False
        btnGitPullNotSelected.Enabled = False
        btnGitPushNotSelected.Enabled = False
        btnCD.Enabled = False
        btnCancel.Enabled = True
        
        
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
                    MsgBox("No item selected", MsgBoxStyle.Critical)
            Else
                progressBar.Maximum = 2
                progressBar.Value = 1
                Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & ForcePush, CmdStyle, True, Wait)
                progressBar.Value = progressBar.Maximum
            End If
            
        Case = "notselected"
            If lstRepos.SelectedIndex = -1 Then
                    MsgBox("No item selected", MsgBoxStyle.Critical)
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
                    MsgBox("No repo passed from command line", MsgBoxStyle.Critical)
            Else
                progressBar.Maximum = 2
                progressBar.Value = 1
                Shell("GitUpdater.bat " & Dir & "\" & cmdRepo & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & ForcePush, CmdStyle, True, Wait)
                progressBar.Value = progressBar.Maximum
            End If
            
        Case = "cmdnotselected"
            If cmdRepo = "" Then
                    MsgBox("No repo passed from command line", MsgBoxStyle.Critical)
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
        
        btnGitPullAll.Enabled = True
        btnGitPushAll.Enabled = True
        btnGitPullSelected.Enabled = True
        btnGitPushSelected.Enabled = True
        btnGitPullNotSelected.Enabled = True
        btnGitPushNotSelected.Enabled = True
        btnCD.Enabled = True
        btnCancel.Enabled = False
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
        System.Threading.Thread.Sleep(500)
        SendKeys.SendWait(txtUsername.Text & "~")
        SendKeys.SendWait(txtPassword.Text & "~")
        Me.WindowState = FormWindowState.Normal
        Me.BringToFront()
    End Sub
    
    Sub TimerKeyChecker_Tick(sender As Object, e As EventArgs)
        If btnHotkey.Text = "Hotkey Enabled!" Then
            btnHotkey.Text = "Disable Hotkey"
            timerKeyChecker.Interval = 100
        ElseIf btnHotkey.Text = "Hotkey Disabled!"
            btnHotkey.Text = "Enable Hotkey"
            timerKeyChecker.Stop
        End If
        If My.Computer.Keyboard.AltKeyDown = True Then
            SendKeys.send(txtUsername.Text & "~")
            SendKeys.send(txtPassword.Text & "~")
            ' See http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send(v=vs.110).aspx
        End If
    End Sub
    
    Sub BtnHotkey_Click(sender As Object, e As EventArgs)
        If btnHotkey.Text = "Enable Hotkey" Then
            btnHotkey.Text = "Hotkey Enabled!"
            timerKeyChecker.Interval = 1000
            timerKeyChecker.Start
        ElseIf btnHotkey.Text = "Disable Hotkey" Then
            btnHotkey.Text = "Hotkey Disabled!"
            timerKeyChecker.Interval = 1000
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
        System.Threading.Thread.Sleep(500)
        SendKeys.SendWait("Y")
        SendKeys.SendWait("~")
        Me.WindowState = FormWindowState.Normal
        Me.BringToFront()
    End Sub
    
    Sub LstRepos_MouseDown(sender As Object, e As MouseEventArgs)
        If lstRepos.SelectedIndex <> -1 Then
            If lstRepos.Items.Item(lstRepos.SelectedIndex).ToString.EndsWith(".wiki") Then
                ContextMenuStripReposOpenReadme.Text = "Open wiki Home.md"
            Else
                ContextMenuStripReposOpenReadme.Text = "Open Repo Readme"
            End If
        Else
            ContextMenuStripReposOpenReadme.Text = "Open Repo Readme"
        End If
    End Sub

End Class