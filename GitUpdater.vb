Imports System.Windows.Shell

Public Class GitUpdater

    Dim Dir As String = Environment.GetEnvironmentVariable("USERPROFILE") & "\Documents\GitHub"
    Dim cmdRepo As String = ""
    Dim count, GitCommand As String  ' because the Worker doesn't support direct sub calling
    Dim ExitWhenDone As Boolean = False

    Dim CmdStyle As AppWinStyle  ' window location of CMD
    Dim Wait As Integer  ' Wait until cmd closes

    Friend Shared TaskbarProgress As New TaskbarItemInfo

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Sub LoadGitUpdater(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not File.Exists("GitUpdater.bat") Then
            Try
                My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/GitUpdater.bat", "GitUpdater.bat")
            Catch ex As Exception
                MsgBox("Could not automatically download the required file! Please download it manually. Click OK to open the download page.", MsgBoxStyle.Exclamation)
                Process.Start("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/GitUpdater.bat")
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
        If txtLogPath.Text = "" Then txtLogPath.Text = Dir & "\GitUpdater.log"
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
        For Each s As String In My.Application.CommandLineArgs
            If s.ToLower.StartsWith("-gitcmd=") Then
                GitCommand = s.Remove(0, 8)
            End If
            If s.ToLower.StartsWith("-gitwhat=") Then
                count = s.Remove(0, 9)
            End If
            If s.ToLower.StartsWith("-dir=") Then
                Dir = s.Remove(0, 5)
                RebuildRepoList()
            End If
            If s.ToLower.StartsWith("-repo=") Then
                cmdRepo = s.Remove(0, 6)
            End If
            If s.ToLower.StartsWith("run") Then
                If ShellWorker.IsBusy = False Then
                    ShellWorker.RunWorkerAsync()
                Else
                    MsgBox("A script is currently in progress!", MsgBoxStyle.Critical)
                End If
            End If
            If s.ToLower.StartsWith("exitwhendone") Then
                ExitWhenDone = True
            End If
            If s.ToLower.StartsWith("hidegui") Then
                Me.WindowState = FormWindowState.Minimized
            End If
        Next
    End Sub

    ' to do with list of repos

    Sub RebuildRepoList()
        btnRefresh.Enabled = False
        lstRepos.Items.Clear()
        For Each Repo As String In Directory.GetDirectories(Dir)
            lstRepos.Items.Add(Mid(Repo, Len(Dir) + 2))
        Next
        btnRefresh.Enabled = True
    End Sub

    Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If ShellWorker.IsBusy = False Then
            RebuildRepoList()
        ElseIf MsgBox("A script is currently in progress! Refreshing repos might mess up the script. You can use the cancel button above to cancel operation." _
                      & vbNewLine & vbNewLine & "Refresh anyway?", MsgBoxStyle.Critical & MsgBoxStyle.OkCancel, "Operation in progress") = vbOK Then
            RebuildRepoList()
        End If
    End Sub

    Sub BtnCD_Click(sender As Object, e As EventArgs) Handles btnCD.Click
        If ShellWorker.IsBusy = False Then
            ' show file chooser dialog, set result as Dir
            folderBrowserDialog.SelectedPath = Dir
            folderBrowserDialog.ShowDialog()
            Dir = folderBrowserDialog.SelectedPath
            My.Settings.SavedDir = Dir

            ' rebuild list automatically
            RebuildRepoList()
        Else
            MsgBox("A script is currently in progress! Changing directory will mess up the script. Please cancel using the button above first.", MsgBoxStyle.Critical)
        End If
    End Sub

    Sub LstRepos_DoubleClick(sender As Object, e As EventArgs) Handles lstRepos.DoubleClick, ContextMenuStripReposOpenInExplorer.Click
        If lstRepos.SelectedIndex <> -1 Then
            Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex))
        Else
            Process.Start(Dir)
        End If
    End Sub

    Sub ContextMenuStripReposOpenInCMD_Click(sender As Object, e As EventArgs) Handles ContextMenuStripReposOpenInCMD.Click
        If lstRepos.SelectedIndex <> -1 Then
            Process.Start("cmd.exe", "/k cd " & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex))
        Else
            Process.Start("cmd.exe", "/k cd " & Dir)
        End If
    End Sub

    Sub ContextMenuStripReposOpenInPS_Click(sender As Object, e As EventArgs) Handles ContextMenuStripReposOpenInPS.Click
        If File.Exists("OpenRepoInPS.bat") Then
            If lstRepos.SelectedIndex <> -1 Then
                Process.Start("OpenRepoInPS.bat", """" & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & """ " & Environment.CurrentDirectory)
            Else
                Process.Start("OpenRepoInPS.bat", """" & Dir & """ " & Environment.CurrentDirectory)
            End If
        Else
            If MsgBox("Couldn't find PowerShell script. This program can attempt to download it and put it in the right place, Continue?", MsgBoxStyle.Critical & MsgBoxStyle.YesNo, "OpenRepoInPS.bat not found!") = vbNo Then Exit Sub
            Try
                My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/Walkman100/GitUpdater/master/OpenRepoInPS.bat", "OpenRepoInPS.bat")
                ContextMenuStripReposOpenInPS_Click(Nothing, Nothing) ' Essentially restart the sub (but not quite)
            Catch ex As Exception
                MsgBox("Could not automatically download a required file! Please download it manually. Select OK to open the download page.", MsgBoxStyle.Exclamation)
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

    Sub ContextMenuStripReposOpenInGitHub_Click(sender As Object, e As EventArgs) Handles ContextMenuStripReposOpenInGitHub.Click
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

    Sub ContextMenuStripReposOpenReadme_Click(sender As Object, e As EventArgs) Handles ContextMenuStripReposOpenReadme.Click
        If lstRepos.SelectedIndex <> -1 Then
            If lstRepos.Items.Item(lstRepos.SelectedIndex).ToString.EndsWith(".wiki") Then
                If File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\home.md") Then
                 Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\home.md")
                Else
                    MsgBox("No file found in Wiki folder:" & vbNewLine & _
                        """" & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & """" & vbNewLine & _
                        "With filename: home.md", MsgBoxStyle.Critical)
                End If
            ElseIf lstRepos.Items.Item(lstRepos.SelectedIndex).ToString.EndsWith(".github.io") Or lstRepos.Items.Item(lstRepos.SelectedIndex).ToString.EndsWith(".github.com") Then
                If File.Exists(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\index.html") Then
                    Process.Start(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & "\index.html")
                Else
                    MsgBox("No file found in Site folder:" & vbNewLine & _
                        """" & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & """" & vbNewLine & _
                        "With filename: index.html", MsgBoxStyle.Critical)
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
    
    Sub ContextMenuStripReposOpenSLN_Click(sender As Object, e As EventArgs) Handles ContextMenuStripReposOpenSLN.Click
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

    Sub ContextMenuStripReposCopyRepoName_Click(sender As Object, e As EventArgs) Handles ContextMenuStripReposCopyRepoName.Click
        If lstRepos.SelectedIndex <> -1 Then
            Try
                Clipboard.SetText(lstRepos.Items.Item(lstRepos.SelectedIndex), TextDataFormat.UnicodeText)
                MsgBox(lstRepos.Items.Item(lstRepos.SelectedIndex) & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
            Catch ex As Exception
                MsgBox("Copy failed!" & vbNewLine & "Error: """ & ex.ToString, MsgBoxStyle.Critical & """", "Copy failed!")
            End Try
        Else
            Try
                Clipboard.SetText(Dir, TextDataFormat.UnicodeText)
                MsgBox(Dir & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
            Catch ex As Exception
                MsgBox("Copy failed!" & vbNewLine & "Error: """ & ex.ToString, MsgBoxStyle.Critical & """", "Copy failed!")
            End Try
        End If
    End Sub

    Sub ContextMenuStripReposCopyRepoPath_Click(sender As Object, e As EventArgs) Handles ContextMenuStripReposCopyRepoPath.Click
        If lstRepos.SelectedIndex <> -1 Then
            Try
                Clipboard.SetText(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex), TextDataFormat.UnicodeText)
                MsgBox(Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
            Catch ex As Exception
                MsgBox("Copy failed!" & vbNewLine & "Error: """ & ex.ToString, MsgBoxStyle.Critical & """", "Copy failed!")
            End Try
        Else
            Try
                Clipboard.SetText(Dir, TextDataFormat.UnicodeText)
                MsgBox(Dir & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
            Catch ex As Exception
                MsgBox("Copy failed!" & vbNewLine & "Error: """ & ex.ToString, MsgBoxStyle.Critical & """", "Copy failed!")
            End Try
        End If
    End Sub

    Private Sub ContextMenuStripReposRemoveEntry_Click(sender As Object, e As EventArgs) Handles ContextMenuStripReposRemoveEntry.Click
        If lstRepos.SelectedIndex <> -1 Then
            lstRepos.Items.RemoveAt(lstRepos.SelectedIndex)
        End If
    End Sub

    Private Sub ContextMenuStripReposCDHere_Click(sender As Object, e As EventArgs) Handles ContextMenuStripReposCDHere.Click
        If lstRepos.SelectedIndex <> -1 Then
            Dir = Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex)
            RebuildRepoList()
        Else
            Dir = Dir.Remove(Dir.LastIndexOf("\"))
            RebuildRepoList()
        End If
    End Sub

    ' how to run the shells & changing settings

    Sub ChkNoWait_CheckedChanged(sender As Object, e As EventArgs) Handles chkNoWait.CheckedChanged
        If chkNoWait.Checked = True Then Wait = 1000 Else Wait = -1
        My.Settings.NoWait = chkNoWait.Checked
        My.Settings.Save()
    End Sub

    Sub ChkDontClose_CheckedChanged(sender As Object, e As EventArgs) Handles chkDontClose.CheckedChanged
        My.Settings.DontClose = chkDontClose.Checked
        My.Settings.Save()
    End Sub

    Sub ChkDontShow_CheckedChanged(sender As Object, e As EventArgs) Handles chkDontShow.CheckedChanged
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

    Sub ChkRepeat_CheckedChanged(sender As Object, e As EventArgs) Handles chkRepeat.CheckedChanged
        My.Settings.Repeat = chkRepeat.Checked
        My.Settings.Save()
    End Sub

    Sub ChkLog_CheckedChanged(sender As Object, e As EventArgs) Handles chkLog.CheckedChanged
        My.Settings.Log = chkLog.Checked
        My.Settings.Save()
    End Sub

    Private Sub txtLogPath_TextChanged(sender As Object, e As EventArgs) Handles txtLogPath.TextChanged
        My.Settings.LogPath = txtLogPath.Text
        My.Settings.Save()
    End Sub

    Private Sub chkOpenLog_CheckedChanged(sender As Object, e As EventArgs) Handles chkOpenLog.CheckedChanged
        My.Settings.OpenLog = chkOpenLog.Checked
        My.Settings.Save()
    End Sub

    Private Sub chkShowErrors_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowErrors.CheckedChanged
        My.Settings.ShowErrors = chkShowErrors.Checked
        My.Settings.Save()
    End Sub

    Private Sub btnBrowseLog_Click(sender As Object, e As EventArgs) Handles btnBrowseLog.Click
        If SaveLogFileDialog.InitialDirectory = "" Then SaveLogFileDialog.InitialDirectory = Dir
        SaveLogFileDialog.ShowDialog()
        txtLogPath.Text = SaveLogFileDialog.FileName
        If File.Exists(txtLogPath.Text) Then MsgBox("File already exists! If you set the logfile to an already existing one, the log will be appended to the end of the file when the Git operation runs.", _
                                                    MsgBoxStyle.Information, "File already exists")
    End Sub

    ' actual code that runs the shells
    Sub ShellWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ShellWorker.DoWork
        Try
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
                Case "all"
                    For i = 1 To lstRepos.Items.Count
                        Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(i - 1) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & txtLogPath.Text, CmdStyle, True, Wait)
                        progressBar.Value = i
                        TaskbarInfoUpdate(TaskbarItemProgressState.Paused, i / lstRepos.Items.Count)
                    Next
                    TaskbarInfoUpdate(TaskbarItemProgressState.Normal)
                Case "selected"
                    If lstRepos.SelectedIndex = -1 Then
                        MsgBox("No item selected", MsgBoxStyle.Critical)
                    Else
                        progressBar.Maximum = 2
                        progressBar.Value = 1
                        TaskbarInfoUpdate(TaskbarItemProgressState.Indeterminate, 0.5)
                        Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(lstRepos.SelectedIndex) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & txtLogPath.Text, CmdStyle, True, Wait)
                        progressBar.Value = progressBar.Maximum
                        TaskbarInfoUpdate(TaskbarItemProgressState.Normal, 1)
                    End If
                Case "notselected"
                    If lstRepos.SelectedIndex = -1 Then
                        MsgBox("No item selected", MsgBoxStyle.Critical)
                    Else
                        For i = 1 To lstRepos.Items.Count
                            If i - 1 <> lstRepos.SelectedIndex Then
                                Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(i - 1) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & txtLogPath.Text, CmdStyle, True, Wait)
                            End If
                            progressBar.Value = i
                            TaskbarInfoUpdate(TaskbarItemProgressState.Normal, i / lstRepos.Items.Count)
                        Next
                        TaskbarInfoUpdate(TaskbarItemProgressState.Normal)
                    End If
                Case "cmdselected"
                    If cmdRepo = "" Then
                        MsgBox("No repo passed from command line", MsgBoxStyle.Critical)
                    Else
                        TaskbarInfoUpdate(TaskbarItemProgressState.Indeterminate)
                        progressBar.Maximum = 2
                        progressBar.Value = 1
                        TaskbarProgress.ProgressValue = 0.5
                        Shell("GitUpdater.bat " & Dir & "\" & cmdRepo & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & txtLogPath.Text, CmdStyle, True, Wait)
                        progressBar.Value = progressBar.Maximum
                        TaskbarInfoUpdate(TaskbarItemProgressState.Normal, 1)
                    End If
                Case "cmdnotselected"
                    If cmdRepo = "" Then
                        MsgBox("No repo passed from command line", MsgBoxStyle.Critical)
                    Else
                        For i = 1 To lstRepos.Items.Count
                            If lstRepos.Items.Item(i - 1) <> cmdRepo Then
                                Shell("GitUpdater.bat " & Dir & "\" & lstRepos.Items.Item(i - 1) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & chkLog.Checked & " " & txtLogPath.Text, CmdStyle, True, Wait)
                                progressBar.Value = i
                                TaskbarInfoUpdate(TaskbarItemProgressState.Paused, i / lstRepos.Items.Count)
                            End If
                        Next
                        TaskbarInfoUpdate(TaskbarItemProgressState.Normal)
                    End If
            End Select

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

    Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If ShellWorker.IsBusy = True Then
            If MsgBox("Are you sure you want to cancel operation? This requires restarting GitUpdater." & vbNewLine & vbNewLine & "This will not close the currently active CMD window. To do so, please click on the window and press 'Ctrl' + 'C', then 'Y', then 'Enter'.", MsgBoxStyle.Question & MsgBoxStyle.YesNo, "Confirmation") = vbNo Then Exit Sub
            Application.Restart()
        Else
            MsgBox("No git operation is currently in progress!", MsgBoxStyle.Information)
        End If

    End Sub

    Sub BtnGitPullAll_Click(sender As Object, e As EventArgs) Handles btnGitPullAll.Click
        count = "all"
        GitCommand = "pull"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync()
        Else
            MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
        End If
    End Sub

    Sub BtnGitPushAll_Click(sender As Object, e As EventArgs) Handles btnGitPushAll.Click
        count = "all"
        GitCommand = "push"
        If ShellWorker.IsBusy = False Then
            ShellWorker.RunWorkerAsync()
        Else
            MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
        End If
    End Sub

    Sub BtnGitPullSelected_Click(sender As Object, e As EventArgs) Handles btnGitPullSelected.Click, ContextMenuStripReposGitPullThis.Click
        If lstRepos.SelectedIndex = -1 Then
            MsgBox("No item selected", MsgBoxStyle.Critical)
        Else
            count = "selected"
            GitCommand = "pull"
            If ShellWorker.IsBusy = False Then
                ShellWorker.RunWorkerAsync()
            Else
                MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
            End If
        End If
    End Sub

    Sub BtnGitPushSelected_Click(sender As Object, e As EventArgs) Handles btnGitPushSelected.Click, ContextMenuStripReposGitPushThis.Click
        If lstRepos.SelectedIndex = -1 Then
            MsgBox("No item selected", MsgBoxStyle.Critical)
        Else
            count = "selected"
            GitCommand = "push"
            If ShellWorker.IsBusy = False Then
                ShellWorker.RunWorkerAsync()
            Else
                MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
            End If
        End If
    End Sub

    Sub BtnGitPullNotSelected_Click(sender As Object, e As EventArgs) Handles btnGitPullNotSelected.Click
        If lstRepos.SelectedIndex = -1 Then
            MsgBox("No item selected", MsgBoxStyle.Critical)
        Else
            count = "notselected"
            GitCommand = "pull"
            If ShellWorker.IsBusy = False Then
                ShellWorker.RunWorkerAsync()
            Else
                MsgBox("A git operation is currently in progress!", MsgBoxStyle.Exclamation, "Operation in progress")
            End If
        End If
    End Sub

    Sub BtnGitPushNotSelected_Click(sender As Object, e As EventArgs) Handles btnGitPushNotSelected.Click
        If lstRepos.SelectedIndex = -1 Then
            MsgBox("No item selected", MsgBoxStyle.Critical)
        Else
            count = "notselected"
            GitCommand = "push"
            If ShellWorker.IsBusy = False Then
                ShellWorker.RunWorkerAsync()
            Else
                MsgBox("A git operation is currently in progress!", MsgBoxStyle.Information, "Operation in progress")
            End If
        End If
    End Sub

    ' credentials-related stuff

    Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        My.Settings.Username = txtUsername.Text
        My.Settings.Password = txtPassword.Text
        My.Settings.Save()
        MsgBox("Succesfully Saved!", MsgBoxStyle.Information, "Saved!")
    End Sub

    Sub BtnInsert_Click(sender As Object, e As EventArgs) Handles btnInsertCredentials.Click
        Me.WindowState = FormWindowState.Minimized
        System.Threading.Thread.Sleep(500)
        SendKeys.SendWait(txtUsername.Text & "~")
        SendKeys.SendWait(txtPassword.Text & "~")
        Me.WindowState = FormWindowState.Normal
        Me.BringToFront()
    End Sub

    Sub TimerKeyChecker_Tick(sender As Object, e As EventArgs) Handles timerKeyChecker.Tick
        If btnHotkey.Text = "Hotkey Enabled!" Then
            btnHotkey.Text = "Disable Hotkey"
            timerKeyChecker.Interval = 100
        ElseIf btnHotkey.Text = "Hotkey Disabled!" Then
            btnHotkey.Text = "Enable Hotkey"
            timerKeyChecker.Stop()
        End If
        If My.Computer.Keyboard.AltKeyDown = True Then
            SendKeys.Send(txtUsername.Text & "~")
            SendKeys.Send(txtPassword.Text & "~")
            ' See http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send(v=vs.110).aspx
        End If
    End Sub

    Sub BtnHotkey_Click(sender As Object, e As EventArgs) Handles btnHotkey.Click
        If btnHotkey.Text = "Enable Hotkey" Then
            btnHotkey.Text = "Hotkey Enabled!"
            timerKeyChecker.Interval = 1000
            timerKeyChecker.Start()
        ElseIf btnHotkey.Text = "Disable Hotkey" Then
            btnHotkey.Text = "Hotkey Disabled!"
            timerKeyChecker.Interval = 1000
        End If
    End Sub

    Sub btnShowPass_MouseDown(sender As Object, e As EventArgs) Handles btnShowPass.MouseDown
        txtPassword.PasswordChar = ""
    End Sub

    Sub btnShowPass_MouseUp(sender As Object, e As EventArgs) Handles btnShowPass.MouseUp
        txtPassword.PasswordChar = "●"
    End Sub

    Sub BtnCloseCmd_Click(sender As Object, e As EventArgs) Handles btnCloseCmd.Click
        If ShellWorker.IsBusy = False Then
            If MsgBox("No git operation from this program is in progress, are you sure you want to insert commands to close a CMD window?", MsgBoxStyle.Question & MsgBoxStyle.YesNo, "Confirmation") = vbNo Then Exit Sub
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

    ' updating the interface

    Sub LstRepos_MouseDown(sender As Object, e As MouseEventArgs) Handles lstRepos.MouseDown
        If lstRepos.SelectedIndex <> -1 Then
            If lstRepos.Items.Item(lstRepos.SelectedIndex).ToString.EndsWith(".wiki") Then
                ContextMenuStripReposOpenInExplorer.Text = "Open Wiki in Windows Explorer"
                ContextMenuStripReposOpenInCMD.Text = "Open Wiki in CMD"
                ContextMenuStripReposOpenInPS.Text = "Open Wiki in Windows PowerShell"
                ContextMenuStripReposOpenInGitHub.Text = "Open Wiki in GitHub for Windows"
                ContextMenuStripReposSeparator1.Visible = True
                ContextMenuStripReposOpenReadme.Visible = True
                ContextMenuStripReposOpenReadme.Text = "Open Wiki home.md"
                ContextMenuStripReposOpenSLN.Visible = False
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
            ElseIf lstRepos.Items.Item(lstRepos.SelectedIndex).ToString.EndsWith(".github.io") Or lstRepos.Items.Item(lstRepos.SelectedIndex).ToString.EndsWith(".github.com") Then
                ContextMenuStripReposOpenInExplorer.Text = "Open Site Folder in Windows Explorer"
                ContextMenuStripReposOpenInCMD.Text = "Open Site Folder in CMD"
                ContextMenuStripReposOpenInPS.Text = "Open Site Folder in Windows PowerShell"
                ContextMenuStripReposOpenInGitHub.Text = "Open Site in GitHub for Windows"
                ContextMenuStripReposSeparator1.Visible = True
                ContextMenuStripReposOpenReadme.Visible = True
                ContextMenuStripReposOpenReadme.Text = "Open Site index.html"
                ContextMenuStripReposOpenSLN.Visible = False
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
                ContextMenuStripReposOpenInGitHub.Text = "Open Repo in GitHub for Windows"
                ContextMenuStripReposSeparator1.Visible = True
                ContextMenuStripReposOpenReadme.Visible = True
                ContextMenuStripReposOpenReadme.Text = "Open Repo Readme"
                ContextMenuStripReposOpenSLN.Visible = True
                ContextMenuStripReposOpenSLN.Text = "Open Repo SLN"
                ContextMenuStripReposSeparator2.Visible = True
                ContextMenuStripReposCopyRepoName.Visible = True
                ContextMenuStripReposCopyRepoName.Text = "Copy Repo Path"
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
            ContextMenuStripReposOpenInGitHub.Text = "Open Folder in GitHub for Windows"
            ContextMenuStripReposSeparator1.Visible = True
            ContextMenuStripReposOpenReadme.Visible = False
            ContextMenuStripReposOpenSLN.Visible = False
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

    Friend Shared Sub TaskbarInfoUpdate(ProgressState As TaskbarItemProgressState, Optional ProgressValue As Double = Nothing)
        Try
            GitUpdater.TaskbarProgress.ProgressState = ProgressState
            If ProgressValue <> Nothing Then
                GitUpdater.TaskbarProgress.ProgressValue = ProgressValue
            End If
        Catch ex As Exception
            If GitUpdater.chkShowErrors.Checked = True Then
                MsgBox("There was an error updating the TaskBarInfo! The error was:" & vbNewLine & ex.ToString, MsgBoxStyle.Critical)
            End If
        End Try
    End Sub
End Class