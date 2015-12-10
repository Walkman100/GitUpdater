Public Class GitUpdater

    Dim Dir As String = Environment.GetEnvironmentVariable("USERPROFILE") & "\Documents\GitHub\"
    Dim cmdRepo As String = ""
    Dim ExitWhenDone As Boolean = False
    Dim notInserted As Boolean = False
    Dim LineIsOrigin, LineIsUpstream As Boolean
    Dim PSFiles() As String = {"CheckVersion.ps1", "GitPrompt.ps1", "GitTabExpansion.ps1", "GitUtils.ps1", "TortoiseGit.ps1", "Utils.ps1", "posh-git.psm1", "profile.example.ps1"}

    Dim CmdStyle As AppWinStyle  ' window location of CMD
    Dim Wait As Integer  ' Wait until cmd closes

    Private Sub btnExit_Click() Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub LoadGitUpdater() Handles MyBase.Load
        If Not File.Exists("GitUpdater.bat") Then
            Try
                My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/GitUpdater.bat", "GitUpdater.bat")
            Catch
                MsgBox("Could not automatically download the required file! Please download it manually. Click OK to open the download page.", MsgBoxStyle.Exclamation)
                Try
                    Process.Start("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/GitUpdater.bat")
                Catch
                    If MsgBox("Unable to launch URL, copy to clipboard instead?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then Clipboard.SetText("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/GitUpdater.bat")
                End Try
            End Try
        End If
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
        chkRepeat.Checked = My.Settings.Repeat
        chkLog.Checked = My.Settings.Log
        txtLogPath.Text = My.Settings.LogPath
        If txtLogPath.Text = "" Then txtLogPath.Text = Dir & "GitUpdater.log"
        chkOpenLog.Checked = My.Settings.OpenLog
        chkShowErrors.Checked = My.Settings.ShowErrors

        ' apply settings to where they affect
        If My.Settings.SavedDir <> "" Then
            Dir = My.Settings.SavedDir
        End If
        RebuildRepoList()
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

        ' command line args
        Dim cmdGitCommand As String = ""
        Dim count As String = ""
        For Each s As String In My.Application.CommandLineArgs
            Select Case s.ToLower
                Case "run"
                    
                Case "exitwhendone"
                    ExitWhenDone = True
                Case "hidegui"
                    Me.WindowState = FormWindowState.Minimized
                Case Else
                    If s.ToLower.StartsWith("-gitcmd=") Then
                        cmdGitCommand = s.Substring(8)
                    End If
                    If s.ToLower.StartsWith("-gitwhat=") Then
                        count = s.Substring(9)
                    End If
                    If s.ToLower.StartsWith("-dir=") Then
                        Dir = s.Substring(5)
                        RebuildRepoList()
                    End If
                    If s.ToLower.StartsWith("-repo=") Then
                        cmdRepo = s.Substring(6)
                    End If
            End Select
        Next
        If My.Application.CommandLineArgs.Contains("run") Then
            If ShellWorker.IsBusy = False Then
                ShellWorker.RunWorkerAsync({count, cmdGitCommand})
            Else
                MsgBox("A script is currently in progress!", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    ' to do with list of repos

    Private Sub RebuildRepoList()
        btnRefresh.Enabled = False
        lstRepos.Items.Clear()
        For Each Repo As String In Directory.GetDirectories(Dir)
            lstRepos.Items.Add(Mid(Repo, Len(Dir) + 1))
        Next
        btnRefresh.Enabled = True
    End Sub

    Private Sub btnRefresh_Click() Handles btnRefresh.Click
        If ShellWorker.IsBusy = False Then
            RebuildRepoList()
        ElseIf MsgBox("A script is currently in progress! Refreshing repos might mess up the script. You can use the cancel button above to cancel operation." _
                      & vbNewLine & vbNewLine & "Refresh anyway?", MsgBoxStyle.Critical + MsgBoxStyle.OkCancel, "Operation in progress") = vbOK Then
            RebuildRepoList()
        End If
    End Sub

    Private Sub btnCD_Click() Handles btnCD.Click
        If ShellWorker.IsBusy = False Then
            ' show file chooser dialog, set result as Dir
            folderBrowserDialog.SelectedPath = Dir
            folderBrowserDialog.ShowDialog()
            Dir = folderBrowserDialog.SelectedPath & "\"
            My.Settings.SavedDir = Dir

            ' rebuild list automatically
            RebuildRepoList()
        Else
            MsgBox("A script is currently in progress! Changing directory will mess up the script. Please cancel using the button above first.", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub LstRepos_DoubleClick() Handles lstRepos.DoubleClick, ContextMenuStripReposOpenInExplorer.Click
        If lstRepos.SelectedIndex <> -1 Then
            Process.Start(Dir & lstRepos.SelectedItem)
        Else
            Process.Start(Dir)
        End If
    End Sub

    Private Sub ContextMenuStripReposOpenInCMD_Click() Handles ContextMenuStripReposOpenInCMD.Click
        If lstRepos.SelectedIndex <> -1 Then
            Process.Start("cmd.exe", "/k cd " & Dir & lstRepos.SelectedItem)
        Else
            Process.Start("cmd.exe", "/k cd " & Dir)
        End If
    End Sub

    Private Sub ContextMenuStripReposOpenInPS_Click() Handles ContextMenuStripReposOpenInPS.Click
        If Not File.Exists("PS\CheckVersion.ps1") Or Not File.Exists("PS\GitPrompt.ps1") Or Not File.Exists("PS\GitTabExpansion.ps1") _
                Or Not File.Exists("PS\GitUtils.ps1") Or Not File.Exists("PS\TortoiseGit.ps1") Or Not File.Exists("PS\Utils.ps1") _
                Or Not File.Exists("PS\posh-git.psm1") Or Not File.Exists("PS\profile.example.ps1") Or Not File.Exists("OpenRepoInPS.bat") Then

            cmdRepo = "A file required to start a PowerShell CLI wasn't found! Missing File(s): " & vbNewLine
                If Not File.Exists("PS\CheckVersion.ps1") Then cmdRepo &= "PS\CheckVersion.ps1" & vbNewLine
                If Not File.Exists("PS\GitPrompt.ps1") Then cmdRepo &= "PS\GitPrompt.ps1" & vbNewLine
                If Not File.Exists("PS\GitTabExpansion.ps1") Then cmdRepo &= "PS\GitTabExpansion.ps1" & vbNewLine
                If Not File.Exists("PS\GitUtils.ps1") Then cmdRepo &= "PS\GitUtils.ps1" & vbNewLine
                If Not File.Exists("PS\TortoiseGit.ps1") Then cmdRepo &= "PS\TortoiseGit.ps1" & vbNewLine
                If Not File.Exists("PS\Utils.ps1") Then cmdRepo &= "PS\Utils.ps1" & vbNewLine
                If Not File.Exists("PS\posh-git.psm1") Then cmdRepo &= "PS\posh-git.psm1" & vbNewLine
                If Not File.Exists("PS\profile.example.ps1") Then cmdRepo &= "PS\profile.example.ps1" & vbNewLine
                If Not File.Exists("OpenRepoInPS.bat") Then cmdRepo &= "OpenRepoInPS.bat" & vbNewLine
            If MsgBox(cmdRepo & "Attempt to download missing files?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical) = MsgBoxResult.No Then
                If MsgBox("Attempt to run script anyway?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub
                If lstRepos.SelectedIndex <> -1 Then
                    Process.Start("OpenRepoInPS.bat", """" & Dir & lstRepos.SelectedItem & """ " & Application.StartupPath)
                Else
                    Process.Start("OpenRepoInPS.bat", """" & Dir & """ " & Application.StartupPath)
                End If
                Exit Sub
            End If
            cmdRepo = ""

            ' PS folder
            For Each file In PSFiles
                If Not IO.File.Exists("PS\" & file) Then
                    Try
                        My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/PS/" & file, "PS\" & file)
                    Catch
                        MsgBox("Could not automatically download the file PS\" & file & "! Please download it manually. Click OK to open the download page.", MsgBoxStyle.Exclamation)
                        Try
                            Process.Start("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/PS/" & file)
                        Catch
                            If MsgBox("Unable to launch URL, copy to clipboard instead?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then Clipboard.SetText("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/PS/" & file)
                        End Try
                    End Try
                End If
            Next
            ' Main Batfile
            If Not File.Exists("OpenRepoInPS.bat") Then
                Try
                    My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/OpenRepoInPS.bat", "OpenRepoInPS.bat")
                    MsgBox("File OpenRepoInPS.bat downloaded successfully!", MsgBoxStyle.Information)
                Catch
                    MsgBox("Could not automatically download the PowerShell batchfile! Please download it manually. Click OK to open the download page.", MsgBoxStyle.Exclamation)
                    Try
                        Process.Start("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/OpenRepoInPS.bat")
                    Catch
                        If MsgBox("Unable to launch URL, copy to clipboard instead?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then Clipboard.SetText("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/OpenRepoInPS.bat")
                    End Try
                End Try
            End If
            MsgBox("File download attempts complete! Please try your action again.", MsgBoxStyle.Information)
        Else
            If lstRepos.SelectedIndex <> -1 Then
                Process.Start("OpenRepoInPS.bat", """" & Dir & lstRepos.SelectedItem & """ " & Application.StartupPath)
            Else
                Process.Start("OpenRepoInPS.bat", """" & Dir & """ " & Application.StartupPath)
            End If
        End If
    End Sub
    
    Private Sub ContextMenuStripReposOpenInBash_Click() Handles ContextMenuStripReposOpenInBash.Click
        If lstRepos.SelectedIndex <> -1 Then
            Process.Start("OpenRepoInBash.bat", """" & Dir & lstRepos.SelectedItem & """")
        Else
            Process.Start("OpenRepoInBash.bat", """" & Dir & """")
        End If
    End Sub

    Private Sub ContextMenuStripReposOpenInGitHub_Click() Handles ContextMenuStripReposOpenInGitHub.Click
        If lstRepos.SelectedIndex <> -1 Then
            Try
                Process.Start("github-windows://openRepo/" & Dir & lstRepos.SelectedItem)
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

    Private Sub ContextMenuStripReposOpenInSmartGit_Click() Handles ContextMenuStripReposOpenInSmartGit.Click
        If lstRepos.SelectedIndex <> -1 Then
            Process.Start(Environment.GetEnvironmentVariable("ProgramFiles") & "\SmartGit\bin\smartgit.exe", "-open """ & Dir & lstRepos.SelectedItem & """")
        Else
            Process.Start(Environment.GetEnvironmentVariable("ProgramFiles") & "\SmartGit\bin\smartgit.exe", "-open """ & Dir & """")
        End If
    End Sub

    Private Sub ContextMenuStripReposOpenReadme_Click() Handles ContextMenuStripReposOpenReadme.Click
        If lstRepos.SelectedIndex <> -1 Then
            If lstRepos.SelectedItem.ToString.EndsWith(".wiki") Then
                If File.Exists(Dir & lstRepos.SelectedItem & "\home.md") Then
                    Process.Start(Dir & lstRepos.SelectedItem & "\home.md")
                Else
                    MsgBox("No file found in Wiki folder:" & vbNewLine & _
                        """" & Dir & lstRepos.SelectedItem & """" & vbNewLine & _
                        "With filename: home.md", MsgBoxStyle.Critical)
                End If
            ElseIf lstRepos.SelectedItem.ToString.EndsWith(".github.io") Or lstRepos.SelectedItem.ToString.EndsWith(".github.com") Then
                If File.Exists(Dir & lstRepos.SelectedItem & "\index.html") Then
                    Process.Start(Dir & lstRepos.SelectedItem & "\index.html")
                Else
                    MsgBox("No file found in Site folder:" & vbNewLine & _
                        """" & Dir & lstRepos.SelectedItem & """" & vbNewLine & _
                        "With filename: index.html", MsgBoxStyle.Critical)
                End If
            Else
                If     File.Exists(Dir & lstRepos.SelectedItem & "\readme.md") Then
                     Process.Start(Dir & lstRepos.SelectedItem & "\readme.md")
                ElseIf File.Exists(Dir & lstRepos.SelectedItem & "\readme.txt") Then
                     Process.Start(Dir & lstRepos.SelectedItem & "\readme.txt")
                ElseIf File.Exists(Dir & lstRepos.SelectedItem & "\readme.htm") Then
                     Process.Start(Dir & lstRepos.SelectedItem & "\readme.htm")
                ElseIf File.Exists(Dir & lstRepos.SelectedItem & "\readme.html") Then
                     Process.Start(Dir & lstRepos.SelectedItem & "\readme.html")
                ElseIf File.Exists(Dir & lstRepos.SelectedItem & "\readme.markdown") Then
                     Process.Start(Dir & lstRepos.SelectedItem & "\readme.markdown")
                ElseIf File.Exists(Dir & lstRepos.SelectedItem & "\readme.mkd") Then
                     Process.Start(Dir & lstRepos.SelectedItem & "\readme.mkd")
                ElseIf File.Exists(Dir & lstRepos.SelectedItem & "\readme") Then
                     Process.Start(Dir & lstRepos.SelectedItem & "\readme")
                Else
                    MsgBox("No file found in repo:" & vbNewLine & _
                        """" & Dir & lstRepos.SelectedItem & """" & vbNewLine & _
                        "With filename: readme.md, readme.txt, readme.htm, readme.html, readme.markdown, readme.mkd, or readme.", MsgBoxStyle.Critical)
                End If
            End If
        Else
            If     File.Exists(Dir & "readme.md") Then
                 Process.Start(Dir & "readme.md")
            ElseIf File.Exists(Dir & "readme.txt") Then
                 Process.Start(Dir & "readme.txt")
            ElseIf File.Exists(Dir & "readme.htm") Then
                 Process.Start(Dir & "readme.htm")
            ElseIf File.Exists(Dir & "readme.html") Then
                 Process.Start(Dir & "readme.html")
            ElseIf File.Exists(Dir & "readme.markdown") Then
                 Process.Start(Dir & "readme.markdown")
            ElseIf File.Exists(Dir & "readme.mkd") Then
                 Process.Start(Dir & "readme.mkd")
            ElseIf File.Exists(Dir & "readme") Then
                 Process.Start(Dir & "readme")
            Else
                MsgBox("No file found in folder:" & vbNewLine & _
                    """" & Dir & """" & vbNewLine & _
                    "With filename: readme.md, readme.txt, readme.htm, readme.html, readme.markdown, readme.mkd, or readme.", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Sub ContextMenuStripReposOpenSLN_Click() Handles ContextMenuStripReposOpenSLN.Click
        If lstRepos.SelectedIndex <> -1 Then
            If     File.Exists(Dir & lstRepos.SelectedItem & "\" & lstRepos.SelectedItem & ".sln") Then
                 Process.Start(Dir & lstRepos.SelectedItem & "\" & lstRepos.SelectedItem & ".sln")
            ElseIf File.Exists(Dir & lstRepos.SelectedItem & "\" & lstRepos.SelectedItem & "\" & lstRepos.SelectedItem & ".sln") Then
                 Process.Start(Dir & lstRepos.SelectedItem & "\" & lstRepos.SelectedItem & "\" & lstRepos.SelectedItem & ".sln")
           'ElseIf File.Exists(Dir & lstRepos.SelectedItem & "\" & lstRepos.SelectedItem & ".sln") Then
           '     Process.Start(Dir & lstRepos.SelectedItem & "\" & lstRepos.SelectedItem & ".sln")
            Else
                MsgBox("No file found in locations:" & vbNewLine & _
                    """" & Dir & lstRepos.SelectedItem & """" & vbNewLine & _
                    """" & Dir & lstRepos.SelectedItem & "\" & lstRepos.SelectedItem & """" & vbNewLine & _
                    "With filename: """ & lstRepos.SelectedItem & ".sln""", MsgBoxStyle.Critical)
            End If
        Else
            If    File.Exists(Dir & "GitHub.sln") Then
                Process.Start(Dir & "GitHub.sln")
            ElseIf File.Exists(Dir & ".sln") Then
                 Process.Start(Dir & ".sln")
            Else
                MsgBox("No file found in locations:" & vbNewLine & _
                    """" & Dir & "GitHub.sln""" & vbNewLine & _
                    """" & Dir & ".sln""", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Sub ContextMenuStripReposOpenURL_Click() Handles ContextMenuStripReposOpenURL.Click
        LineIsOrigin = False
        LineIsUpstream = False

        '[remote "origin"] 'Repo location
        '   url = https://github.com/Walkman100/Dashy.git
        '[remote "upstream"] '[Fork] repo that current one was forked from
        '   url = https://github.com/deavmi/Dashy.git
        '[submodule "YTVL"] 'submodule url in git format
        '   url = git://github.com/Walkman100/YTVL.git/
        '[submodule "github-watchers-button"] 'submodule url in https format
        '   url = https://github.com/addyosmani/github-watchers-button.git
        For Each line In File.ReadLines(Dir & lstRepos.SelectedItem & "\.git\config")
            If LineIsOrigin Then
                cmdRepo = line.Substring(line.IndexOf("https://")) ' cmdRepo just because it's a string that would be unused by this point
                If cmdRepo.EndsWith(".git") Then cmdRepo = cmdRepo.Remove(cmdRepo.Length - 4)
                Try
                    Process.Start(cmdRepo)
                Catch
                    If MsgBox("Unable to launch URL, copy to clipboard instead?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then _
                      Clipboard.SetText(cmdRepo)
                End Try
                LineIsOrigin = False
            End If
            If line = "[remote ""origin""]" Then
                LineIsOrigin = True
            End If
            
            If LineIsUpstream Then
                If MsgBox("Fork detected, open fork origin too?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    cmdRepo = line.Substring(line.IndexOf("https://"))
                    If cmdRepo.EndsWith(".git") Then cmdRepo = cmdRepo.Remove(cmdRepo.Length - 4)
                    Try
                        Process.Start(cmdRepo)
                    Catch
                        If MsgBox("Unable to launch URL, copy to clipboard instead?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then _
                          Clipboard.SetText(cmdRepo)
                    End Try
                End If
                LineIsUpstream = False
            End If
            If line = "[remote ""upstream""]" Then
                LineIsUpstream = True
            End If
        Next
    End Sub

    Private Sub ContextMenuStripReposCopyRepoName_Click() Handles ContextMenuStripReposCopyRepoName.Click
        If lstRepos.SelectedIndex <> -1 Then
            Try
                Clipboard.SetText(lstRepos.SelectedItem, TextDataFormat.UnicodeText)
                MsgBox(lstRepos.SelectedItem & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
            Catch ex As Exception
                MsgBox("Copy failed!" & vbNewLine & "Error: """ & ex.ToString & """", MsgBoxStyle.Critical, "Copy failed!")
            End Try
        Else
            Try
                Clipboard.SetText(Dir, TextDataFormat.UnicodeText)
                MsgBox(Dir & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
            Catch ex As Exception
                MsgBox("Copy failed!" & vbNewLine & "Error: """ & ex.ToString & """", MsgBoxStyle.Critical, "Copy failed!")
            End Try
        End If
    End Sub

    Private Sub ContextMenuStripReposCopyRepoPath_Click() Handles ContextMenuStripReposCopyRepoPath.Click
        If lstRepos.SelectedIndex <> -1 Then
            Try
                Clipboard.SetText(Dir & lstRepos.SelectedItem, TextDataFormat.UnicodeText)
                MsgBox(Dir & lstRepos.SelectedItem & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
            Catch ex As Exception
                MsgBox("Copy failed!" & vbNewLine & "Error: """ & ex.ToString & """", MsgBoxStyle.Critical, "Copy failed!")
            End Try
        Else
            Try
                Clipboard.SetText(Dir, TextDataFormat.UnicodeText)
                MsgBox(Dir & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
            Catch ex As Exception
                MsgBox("Copy failed!" & vbNewLine & "Error: """ & ex.ToString & """", MsgBoxStyle.Critical, "Copy failed!")
            End Try
        End If
    End Sub

    Private Sub ContextMenuStripReposRemoveEntry_Click() Handles ContextMenuStripReposRemoveEntry.Click
        If lstRepos.SelectedIndex <> -1 Then
            lstRepos.Items.RemoveAt(lstRepos.SelectedIndex)
        End If
    End Sub

    Private Sub ContextMenuStripReposCDHere_Click() Handles ContextMenuStripReposCDHere.Click
        If lstRepos.SelectedIndex <> -1 Then
            Dir = Dir & lstRepos.SelectedItem & "\"
            My.Settings.SavedDir = Dir
            RebuildRepoList()
        Else
            Try
                Dir = Dir.Remove(Dir.Length - 1)
                Dir = Dir.Remove(Dir.LastIndexOf("\"))
                Dir = Dir & "\"
                My.Settings.SavedDir = Dir
                RebuildRepoList()
            Catch ex As System.ArgumentOutOfRangeException
                If MsgBox("Cannot go higher than the root of a drive! Please use the CD button to change drives." & vbNewLine & vbNewLine & _
                  "Open CD dialog now?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, "Trying to go up from a drive root") = vbYes Then
                    btnCD_Click()
                End If
            End Try
        End If
    End Sub

    ' how to run the shells & changing settings

    Private Sub chkNoWait_CheckedChanged() Handles chkNoWait.CheckedChanged
        If chkNoWait.Checked = True Then Wait = 1000 Else Wait = -1
        My.Settings.NoWait = chkNoWait.Checked
        My.Settings.Save()
    End Sub

    Private Sub chkDontClose_CheckedChanged() Handles chkDontClose.CheckedChanged
        My.Settings.DontClose = chkDontClose.Checked
        My.Settings.Save()
    End Sub

    Private Sub chkDontShow_CheckedChanged() Handles chkDontShow.CheckedChanged
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

    Private Sub chkRepeat_CheckedChanged() Handles chkRepeat.CheckedChanged
        My.Settings.Repeat = chkRepeat.Checked
        My.Settings.Save()
    End Sub

    Private Sub chkLog_CheckedChanged() Handles chkLog.CheckedChanged
        My.Settings.Log = chkLog.Checked
        My.Settings.Save()
    End Sub

    Private Sub txtLogPath_TextChanged() Handles txtLogPath.TextChanged
        My.Settings.LogPath = txtLogPath.Text
        My.Settings.Save()
    End Sub

    Private Sub chkOpenLog_CheckedChanged() Handles chkOpenLog.CheckedChanged
        My.Settings.OpenLog = chkOpenLog.Checked
        My.Settings.Save()
    End Sub

    Private Sub chkShowErrors_CheckedChanged() Handles chkShowErrors.CheckedChanged, chkShowErrors.Click
        My.Settings.ShowErrors = chkShowErrors.Checked
        My.Settings.Save()
    End Sub

    Private Sub btnBrowseLog_Click() Handles btnBrowseLog.Click
        If SaveLogFileDialog.InitialDirectory = "" Then SaveLogFileDialog.InitialDirectory = Dir
        If SaveLogFileDialog.ShowDialog() = DialogResult.OK Then
            txtLogPath.Text = SaveLogFileDialog.FileName
            If File.Exists(txtLogPath.Text) Then _
            MsgBox("File already exists! If you set the logfile to an already existing one, the log will be appended to the end of the file when the Git operation runs.", _
                   MsgBoxStyle.Information, "File already exists")
        End If
    End Sub

    ' actual code that runs the shells
    Private Sub ShellWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ShellWorker.DoWork
        Try
            progressBar.ShowInTaskbar = True
            btnGitPullAll.Enabled = False
            btnGitPushAll.Enabled = False
            btnGitPullSelected.Enabled = False
            btnGitPushSelected.Enabled = False
            btnGitPullNotSelected.Enabled = False
            btnGitPushNotSelected.Enabled = False
            btnCD.Enabled = False
            btnCancel.Enabled = True
            
            notInserted = True
            If e.Argument(1).ToLower = "push" Then timerAutoInsert.Start
            If chkDontShow.Checked = False Then Me.TopMost = True
            progressBar.Maximum = lstRepos.Items.Count
            
            Select Case e.Argument(0).ToLower
                Case "all"
                    For i = 1 To lstRepos.Items.Count
                        If chkGitRepoOnly.Checked = True Then
                            If File.Exists(Dir & lstRepos.Items.Item(i - 1) & "\.git\config") = False Then
                                Continue For
                            End If
                        End If
                        notInserted = True
                        Shell("GitUpdater.bat """ & Dir & lstRepos.Items.Item(i - 1) & """ " & e.Argument(1) & " " & _
                          chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & txtLogPath.Text, CmdStyle, True, Wait)
                        progressBar.Value = i
                    Next
                Case "selected"
                    If lstRepos.SelectedIndex = -1 Then
                        MsgBox("No item selected", MsgBoxStyle.Critical)
                    Else
                        progressBar.Maximum = 2
                        progressBar.Value = 1
                        notInserted = True
                        Shell("GitUpdater.bat " & Dir & lstRepos.SelectedItem & " " & e.Argument(1) & " " & _
                          chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & txtLogPath.Text, vbNormalFocus, True, Wait)
                    End If
                Case "notselected"
                    If lstRepos.SelectedIndex = -1 Then
                        MsgBox("No item selected", MsgBoxStyle.Critical)
                    Else
                        For i = 1 To lstRepos.Items.Count
                            If i - 1 <> lstRepos.SelectedIndex Then
                                If chkGitRepoOnly.Checked = True Then
                                    If File.Exists(Dir & lstRepos.Items.Item(i - 1) & "\.git\config") = False Then
                                        Continue For
                                    End If
                                End If
                                notInserted = True
                                Shell("GitUpdater.bat " & Dir & lstRepos.Items.Item(i - 1) & " " & e.Argument(1) & " " & _
                                  chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & txtLogPath.Text, CmdStyle, True, Wait)
                                progressBar.Value = i
                            End If
                        Next
                    End If
                Case "cmdselected"
                    If cmdRepo = "" Then
                        MsgBox("No repo passed from command line", MsgBoxStyle.Critical)
                    Else
                        progressBar.Maximum = 2
                        progressBar.Value = 1
                        notInserted = True
                        Shell("GitUpdater.bat " & Dir & cmdRepo & " " & e.Argument(1) & " " & _
                          chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & txtLogPath.Text, vbNormalFocus, True, Wait)
                    End If
                Case "cmdnotselected"
                    If cmdRepo = "" Then
                        MsgBox("No repo passed from command line", MsgBoxStyle.Critical)
                    Else
                        For i = 1 To lstRepos.Items.Count
                            If lstRepos.Items.Item(i - 1) <> cmdRepo Then
                                If chkGitRepoOnly.Checked = True Then
                                    If File.Exists(Dir & lstRepos.Items.Item(i - 1) & "\.git\config") = False Then
                                        Continue For
                                    End If
                                End If
                                notInserted = True
                                Shell("GitUpdater.bat " & Dir & lstRepos.Items.Item(i - 1) & " " & e.Argument(1) & " " & _
                                  chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & txtLogPath.Text, CmdStyle, True, Wait)
                                progressBar.Value = i
                            End If
                        Next
                    End If
            End Select
            
            progressBar.Value = progressBar.Maximum
            progressBar.ShowInTaskbar = False
            
            If chkOpenLog.Checked = True Then
                Try
                    Process.Start(txtLogPath.Text)
                Catch ex As Exception
                    MsgBox("Failed to open log!", MsgBoxStyle.Critical)
                End Try
            End If
            
            If ExitWhenDone = True Then
                Application.Exit()
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
        Catch ex As Exception
            MsgBox("There was an error executing the Git operation! The error was:" & vbNewLine & ex.ToString, MsgBoxStyle.Critical)
            btnGitPullAll.Enabled = True
            btnGitPushAll.Enabled = True
            btnGitPullSelected.Enabled = True
            btnGitPushSelected.Enabled = True
            btnGitPullNotSelected.Enabled = True
            btnGitPushNotSelected.Enabled = True
            btnCD.Enabled = True
            btnCancel.Enabled = False
        End Try
    End Sub

    ' starting and stopping the thread

    Private Sub btnCancel_Click() Handles btnCancel.Click
        If ShellWorker.IsBusy = True Then
            If MsgBox("Are you sure you want to cancel operation? This requires restarting GitUpdater." & vbNewLine & vbNewLine & _
              "This will not close the currently active CMD window. To do so, please click on the window and press 'Ctrl' + 'C', then 'Y', then 'Enter'.", _
              MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirmation") = MsgBoxResult.Yes Then Application.Restart()
        Else
            MsgBox("No git operation is currently in progress!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub btnGitPullAll_Click() Handles btnGitPullAll.Click
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync({"all", "pull"})
        Else
            MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
        End If
    End Sub

    Private Sub btnGitPushAll_Click() Handles btnGitPushAll.Click
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync({"all", "push"})
        Else
            MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
        End If
    End Sub

    Private Sub btnGitPullSelected_Click() Handles btnGitPullSelected.Click, ContextMenuStripReposGitPullThis.Click
        If lstRepos.SelectedIndex = -1 Then
            MsgBox("No item selected", MsgBoxStyle.Critical)
        Else
            If ShellWorker.IsBusy = False Then
                ShellWorker.RunWorkerAsync({"selected", "pull"})
            Else
                MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
            End If
        End If
    End Sub

    Private Sub btnGitPushSelected_Click() Handles btnGitPushSelected.Click, ContextMenuStripReposGitPushThis.Click
        If lstRepos.SelectedIndex = -1 Then
            MsgBox("No item selected", MsgBoxStyle.Critical)
        Else
            If ShellWorker.IsBusy = False Then
                ShellWorker.RunWorkerAsync({"selected", "push"})
            Else
                MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
            End If
        End If
    End Sub

    Private Sub btnGitPullNotSelected_Click() Handles btnGitPullNotSelected.Click
        If lstRepos.SelectedIndex = -1 Then
            MsgBox("No item selected", MsgBoxStyle.Critical)
        Else
            If ShellWorker.IsBusy = False Then
                ShellWorker.RunWorkerAsync({"notselected", "pull"})
            Else
                MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
            End If
        End If
    End Sub

    Private Sub btnGitPushNotSelected_Click() Handles btnGitPushNotSelected.Click
        If lstRepos.SelectedIndex = -1 Then
            MsgBox("No item selected", MsgBoxStyle.Critical)
        Else
            If ShellWorker.IsBusy = False Then
                ShellWorker.RunWorkerAsync({"notselected", "push"})
            Else
                MsgBox("A git operation is currently in progress!", MsgBoxStyle.Information, "Operation in progress")
            End If
        End If
    End Sub

    Private Sub btnCloseCmd_Click() Handles btnCloseCmd.Click
        If ShellWorker.IsBusy = False Then
            If MsgBox("No git operation from this program is in progress, are you sure you want to insert commands to close a CMD window?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirmation") = vbNo Then Exit Sub
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

    ' credentials-related stuff

    Private Sub btnSave_Click() Handles btnSave.Click
        My.Settings.Username = txtUsername.Text
        My.Settings.Password = txtPassword.Text
        My.Settings.Save()
        MsgBox("Succesfully Saved!", MsgBoxStyle.Information, "Saved!")
    End Sub

    Private Sub btnInsertCredentials_Click() Handles btnInsertCredentials.Click
        Me.WindowState = FormWindowState.Minimized
        System.Threading.Thread.Sleep(500)
        SendKeys.SendWait(txtUsername.Text & "~")
        SendKeys.SendWait(txtPassword.Text & "~")
        Me.WindowState = FormWindowState.Normal
        Me.BringToFront()
    End Sub

    Private Sub timerKeyChecker_Tick() Handles timerKeyChecker.Tick
        If btnHotkey.Text = "Hotkey Enabled!" Then
            btnHotkey.Text = "Disable Hotkey (Alt)"
            timerKeyChecker.Interval = 100
        ElseIf btnHotkey.Text = "Hotkey Disabled!" Then
            btnHotkey.Text = "Enable Hotkey (Alt)"
            timerKeyChecker.Stop()
        End If
        If My.Computer.Keyboard.AltKeyDown = True Then
            SendKeys.Send(txtUsername.Text & "~")
            SendKeys.Send(txtPassword.Text & "~")
            ' See https://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send(v=vs.110).aspx?cs-lang=vb#Anchor_2
        End If
    End Sub

    Private Sub timerAutoInsert_Tick() Handles timerAutoInsert.Tick
        If chkAutoInsert.Checked = True Then
            If chkDontShow.Checked = False Then
                If notInserted Then
                    SendKeys.Send(txtUsername.Text & "~" & txtPassword.Text & "~")
                    notInserted = False
                End If
            End If
        End If
        timerAutoInsert.Stop
    End Sub

    Private Sub btnHotkey_Click() Handles btnHotkey.Click
        If btnHotkey.Text = "Enable Hotkey (Alt)" Then
            btnHotkey.Text = "Hotkey Enabled!"
            timerKeyChecker.Interval = 1000
            timerKeyChecker.Start()
        ElseIf btnHotkey.Text = "Disable Hotkey (Alt)" Then
            btnHotkey.Text = "Hotkey Disabled!"
            timerKeyChecker.Interval = 1000
        End If
    End Sub

    Private Sub btnShowPass_MouseDown() Handles btnShowPass.MouseDown
        txtPassword.PasswordChar = ""
    End Sub

    Private Sub btnShowPass_MouseUp() Handles btnShowPass.MouseUp
        txtPassword.PasswordChar = "●"
    End Sub

    ' updating the interface

    Private Sub lstRepos_MouseDown() Handles lstRepos.MouseDown
        If lstRepos.SelectedIndex <> -1 Then
            If lstRepos.SelectedItem.ToString.EndsWith(".wiki") Then
                ContextMenuStripReposOpenInExplorer.Text = "Open Wiki in Windows Explorer"
                ContextMenuStripReposOpenInCMD.Text = "Open Wiki in CMD"
                ContextMenuStripReposOpenInPS.Text = "Open Wiki in Windows PowerShell"
                ContextMenuStripReposOpenInBash.Text = "Open Wiki in Git Bash"
                ContextMenuStripReposOpenInGitHub.Text = "Open Wiki in GitHub for Windows"
                ContextMenuStripReposOpenInSmartGit.Text = "Open Wiki in SmartGit"
                ContextMenuStripReposSeparator1.Visible = True
                ContextMenuStripReposOpenReadme.Visible = True
                ContextMenuStripReposOpenReadme.Text = "Open Wiki home.md"
                ContextMenuStripReposOpenSLN.Visible = False
                ContextMenuStripReposOpenURL.Text = "Open Wiki URL"
                ContextMenuStripReposSeparator2.Visible = True
                ContextMenuStripReposCopyRepoName.Visible = True
                ContextMenuStripReposCopyRepoName.Text = "Copy Wiki Name"
                ContextMenuStripReposCopyRepoPath.Visible = True
                ContextMenuStripReposCopyRepoPath.Text = "Copy Wiki Path"
                ContextMenuStripReposSeparator3.Visible = True
                ContextMenuStripReposRemoveEntry.Visible = True
                ContextMenuStripReposCDHere.Visible = True
                ContextMenuStripReposCDHere.Text = "CD Here..."
                ContextMenuStripReposSeparator4.Visible = True
                ContextMenuStripReposGitPullThis.Visible = True
                ContextMenuStripReposGitPushThis.Visible = True
            ElseIf lstRepos.SelectedItem.ToString.EndsWith(".github.io") Or lstRepos.SelectedItem.ToString.EndsWith(".github.com") Then
                ContextMenuStripReposOpenInExplorer.Text = "Open Site Folder in Windows Explorer"
                ContextMenuStripReposOpenInCMD.Text = "Open Site Folder in CMD"
                ContextMenuStripReposOpenInPS.Text = "Open Site Folder in Windows PowerShell"
                ContextMenuStripReposOpenInBash.Text = "Open Site Folder in Git Bash"
                ContextMenuStripReposOpenInGitHub.Text = "Open Site in GitHub for Windows"
                ContextMenuStripReposOpenInSmartGit.Text = "Open Site Folder in SmartGit"
                ContextMenuStripReposSeparator1.Visible = True
                ContextMenuStripReposOpenReadme.Visible = True
                ContextMenuStripReposOpenReadme.Text = "Open Site index.html"
                ContextMenuStripReposOpenSLN.Visible = False
                ContextMenuStripReposOpenURL.Text = "Open Site GitHub URL"
                ContextMenuStripReposSeparator2.Visible = True
                ContextMenuStripReposCopyRepoName.Visible = True
                ContextMenuStripReposCopyRepoName.Text = "Copy Site Name"
                ContextMenuStripReposCopyRepoPath.Visible = True
                ContextMenuStripReposCopyRepoPath.Text = "Copy Site Path"
                ContextMenuStripReposSeparator3.Visible = True
                ContextMenuStripReposRemoveEntry.Visible = True
                ContextMenuStripReposCDHere.Visible = True
                ContextMenuStripReposCDHere.Text = "CD Here..."
                ContextMenuStripReposSeparator4.Visible = True
                ContextMenuStripReposGitPullThis.Visible = True
                ContextMenuStripReposGitPushThis.Visible = True
            Else
                ContextMenuStripReposOpenInExplorer.Text = "Open Repo in Windows Explorer"
                ContextMenuStripReposOpenInCMD.Text = "Open Repo in CMD"
                ContextMenuStripReposOpenInPS.Text = "Open Repo in Windows PowerShell"
                ContextMenuStripReposOpenInBash.Text = "Open Repo in Git Bash"
                ContextMenuStripReposOpenInGitHub.Text = "Open Repo in GitHub for Windows"
                ContextMenuStripReposOpenInSmartGit.Text = "Open Repo in SmartGit"
                ContextMenuStripReposSeparator1.Visible = True
                ContextMenuStripReposOpenReadme.Visible = True
                ContextMenuStripReposOpenReadme.Text = "Open Repo Readme"
                ContextMenuStripReposOpenSLN.Visible = True
                ContextMenuStripReposOpenSLN.Text = "Open Repo SLN"
                ContextMenuStripReposOpenURL.Text = "Open Repo URL"
                ContextMenuStripReposSeparator2.Visible = True
                ContextMenuStripReposCopyRepoName.Visible = True
                ContextMenuStripReposCopyRepoName.Text = "Copy Repo Name"
                ContextMenuStripReposCopyRepoPath.Visible = True
                ContextMenuStripReposCopyRepoPath.Text = "Copy Repo Path"
                ContextMenuStripReposSeparator3.Visible = True
                ContextMenuStripReposRemoveEntry.Visible = True
                ContextMenuStripReposCDHere.Visible = True
                ContextMenuStripReposCDHere.Text = "CD Here..."
                ContextMenuStripReposSeparator4.Visible = True
                ContextMenuStripReposGitPullThis.Visible = True
                ContextMenuStripReposGitPushThis.Visible = True
            End If
        Else
            ContextMenuStripReposOpenInExplorer.Text = "Open Folder in Windows Explorer"
            ContextMenuStripReposOpenInCMD.Text = "Open Folder in CMD"
            ContextMenuStripReposOpenInPS.Text = "Open Folder in Windows PowerShell"
            ContextMenuStripReposOpenInBash.Text = "Open Folder in Git Bash"
            ContextMenuStripReposOpenInGitHub.Text = "Open Folder in GitHub for Windows"
            ContextMenuStripReposOpenInSmartGit.Text = "Open Folder in SmartGit"
            ContextMenuStripReposSeparator1.Visible = True
            ContextMenuStripReposOpenReadme.Visible = False
            ContextMenuStripReposOpenSLN.Visible = False
            ContextMenuStripReposOpenURL.Text = "Open Folder URL"
            ContextMenuStripReposSeparator2.Visible = False
            ContextMenuStripReposCopyRepoName.Visible = False
            ContextMenuStripReposCopyRepoPath.Visible = True
            ContextMenuStripReposCopyRepoPath.Text = "Copy Folder Path"
            ContextMenuStripReposSeparator3.Visible = False
            ContextMenuStripReposRemoveEntry.Visible = False
            ContextMenuStripReposCDHere.Visible = True
            ContextMenuStripReposCDHere.Text = "Up a level ↩"
            ContextMenuStripReposSeparator4.Visible = True
            ContextMenuStripReposGitPullThis.Visible = True
            ContextMenuStripReposGitPushThis.Visible = True
        End If
    End Sub
End Class
