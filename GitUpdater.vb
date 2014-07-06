Imports System
Imports System.IO

Public Class GitUpdater
    
    Dim usrProfile As String = Environment.GetEnvironmentVariable("HOMEPATH")
    Dim Dir As String = usrProfile & "\Documents\GitHub"
    
    Dim Wait As String = "-1"   'Wait until cwd closes
    Dim DontShow As String = ""
    Dim ForcePush As String = ""
    
    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        End
    End Sub
    
    Sub GitUpdater_Load(sender As Object, e As EventArgs)
        lstDirs.Items.Clear
        For Each Repo As String In Directory.GetDirectories(Dir)
            lstDirs.Items.Add(Mid(Repo, Len(Dir) + 2))
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
    
    Sub RunShell(count As String, cmd As String)
        If chkDontShow.Checked = True Then DontShow = vbNormalFocus
        If chkPushForce.Checked = True Then ForcePush = "-f"
        
        If count = "all" Then
            
        ElseIf count = "selected" Then
            
        ElseIf count = "notselected" Then
            
        End If
    End Sub
    
    Sub BtnGitPullAll_Click(sender As Object, e As EventArgs)
        If chkNoWait.Checked = False Then
            For i = 1 To lstDirs.Items.Count
                Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True)
            Next
        Else
            For i = 1 To lstDirs.Items.Count
                Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True, 1000)
            Next
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
        If chkPushForce.Checked = True Then ForcePush = "-f"
        If chkNoWait.Checked = False Then
            For i = 1 To lstDirs.Items.Count
                Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " push " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & ForcePush, vbNormalFocus, True)
            Next
        Else
            For i = 1 To lstDirs.Items.Count
                Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " push " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & ForcePush, vbNormalFocus, True, 1000)
            Next
        End If
    End Sub
    
    Sub BtnGitPullSelected_Click(sender As Object, e As EventArgs)
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(lstDirs.SelectedIndex) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True, 1000)
        End If
    End Sub
    
    Sub BtnGitPushSelected_Click(sender As Object, e As EventArgs)
        If chkPushForce.Checked = True Then ForcePush = "-f"
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(lstDirs.SelectedIndex) & " push " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & ForcePush, vbNormalFocus, True, 1000)
        End If
    End Sub
    
    Sub BtnGitPullNotSelected_Click(sender As Object, e As EventArgs)
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            If chkNoWait.Checked = False Then
                For i = 1 To lstDirs.Items.Count
                    If i - 1 <> lstDirs.SelectedIndex Then
                        Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True)
                    End If
                Next
            Else
                For i = 1 To lstDirs.Items.Count
                    If i - 1 <> lstDirs.SelectedIndex Then
                        Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True, 1000)
                    End If
                Next
            End If
        End If
    End Sub
    
    Sub BtnGitPushNotSelected_Click(sender As Object, e As EventArgs)
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            If chkNoWait.Checked = False Then
                For i = 1 To lstDirs.Items.Count
                    If i - 1 <> lstDirs.SelectedIndex Then
                        Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True)
                    End If
                Next
            Else
                For i = 1 To lstDirs.Items.Count
                    If i - 1 <> lstDirs.SelectedIndex Then
                        Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True, 1000)
                    End If
                Next
            End If
        End If
    End Sub
End Class