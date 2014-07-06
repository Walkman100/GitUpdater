Imports System
Imports System.IO

Public Class GitUpdater
    
    Dim usrProfile As String = Environment.GetEnvironmentVariable("HOMEPATH")
    Dim Dir As String = usrProfile & "\Documents\GitHub"
    
    Dim Wait As Integer = -1   'Wait until cmd closes
    Dim CmdStyle As AppWinStyle = AppWinStyle.MinimizedFocus
    Dim ForcePush As String = ""
    
    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        End
    End Sub
    
    Sub GitUpdater_Load(sender As Object, e As EventArgs)
        lstDirs.Items.Clear
        For Each Repo As String In Directory.GetDirectories(Dir)
            lstDirs.Items.Add(Mid(Repo, Len(Dir) + 2))
        Next
        
        'Command Line Args
        For Each s As String In My.Application.CommandLineArgs
            
            RunShell(Strings.Right(s, Len(s)-4), Strings.Left(s, 4))
            
            ' code from SteamPlaceHolder:
'            If s = "hideGUI" Or s = "hidegui" Then
'                WindowState = FormWindowState.Minimized
'            ElseIf s <> "hideGUI"
'                ProgArg = s
'            End If
        Next
    End Sub
    
    Sub BtnRefresh_Click(sender As Object, e As EventArgs)
        lstDirs.Items.Clear
        For Each Repo As String In Directory.GetDirectories(Dir)
            lstDirs.Items.Add(Mid(Repo, Len(Dir) + 2))
        Next
    End Sub
    
    Sub BtnCD_Click(sender As Object, e As EventArgs)
        ' show file chooser dialog, set result as Dir
        folderBrowserDialog.ShowDialog
        Dir = folderBrowserDialog.SelectedPath
        
        ' rebuild list automatically
        lstDirs.Items.Clear
        For Each Repo As String In Directory.GetDirectories(Dir)
            lstDirs.Items.Add(Mid(Repo, Len(Dir) + 2))
        Next
    End Sub
    
    Sub RunShell(count As String, GitCommand As String)
        If chkNoWait.Checked = True Then Wait = 1000 Else Wait = -1
        If chkDontShow.Checked = True Then CmdStyle = AppWinStyle.MinimizedFocus Else CmdStyle = vbNormalFocus
        If chkPushForce.Checked = True Then ForcePush = "-f"
        
        If count = "all" Then
            For i = 1 To lstDirs.Items.Count
                Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked, CmdStyle, True, Wait)
            Next
        ElseIf count = "selected" Then
            If lstDirs.SelectedIndex = -1 Then
                MsgBox("No item selected")
            Else
                Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(lstDirs.SelectedIndex) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked, CmdStyle, True, Wait)
            End If
        ElseIf count = "notselected" Then
            If lstDirs.SelectedIndex = -1 Then
                MsgBox("No item selected")
            Else
                For i = 1 To lstDirs.Items.Count
                    If i - 1 <> lstDirs.SelectedIndex Then
                        Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " " & GitCommand & " " & chkRepeat.Checked & " " & chkDontClose.Checked, CmdStyle, True, Wait)
                    End If
                Next
            End If
        End If
    End Sub
    
    Sub BtnGitPullAll_Click(sender As Object, e As EventArgs)
        RunShell("all", "pull")
        
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
        RunShell("all", "push")
    End Sub
    
    Sub BtnGitPullSelected_Click(sender As Object, e As EventArgs)
        RunShell("selected", "pull")
    End Sub
    
    Sub BtnGitPushSelected_Click(sender As Object, e As EventArgs)
        RunShell("selected", "push")
    End Sub
    
    Sub BtnGitPullNotSelected_Click(sender As Object, e As EventArgs)
        RunShell("notselected", "pull")
    End Sub
    
    Sub BtnGitPushNotSelected_Click(sender As Object, e As EventArgs)
        RunShell("notselected", "push")
    End Sub
End Class