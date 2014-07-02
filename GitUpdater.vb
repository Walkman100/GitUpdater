Imports System
Imports System.IO

Public Class GitUpdater
    
    Dim usrProfile As String = Environment.GetEnvironmentVariable("HOMEPATH")
    Dim Dir As String = usrProfile & "\Documents\GitHub"
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
    
    Sub BtnGitPullAll_Click(sender As Object, e As EventArgs)
        For i = 1 To lstDirs.Items.Count
' another way to do a comment block: http://forums.asp.net/post/4414215.aspx
'            'MsgBox("/k cd " & Dir & "\" & lstDirs.Items.Item(i - 1))
'            Process.Start(cmdPath, "/k cd " & Dir & "\" & lstDirs.Items.Item(i - 1))
'            System.Threading.Thread.Sleep(200)
'            'Shell(cmdPath & " /k cd " & Dir & "\" & lstDirs.Items.Item(i - 1) & "\git pull", , True, 9)
'            sendkeys.send("git pull {ENTER}")
'            System.Threading.Thread.Sleep(100)
'            If chkDontClose.Checked = False Then
'                SendKeys.Send("exit {ENTER}")
'            End If
'            System.Threading.Thread.Sleep(100)
'            'Process.Start(cmdPath, "/k " & Dir & "\" & lstDirs.Items.Item(i - 1) & "\git pull")
'            'Shell(Dir & "\" & lstDirs.Items.Item(i - 1) & "\git pull", , True, 9)
            
            Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, , True, 9)
            System.Threading.Thread.Sleep(100)
        Next
    End Sub
    
    Sub BtnGitPushAll_Click(sender As Object, e As EventArgs)
        If chkPushForce.Checked = True Then ForcePush = "-f"
        For i = 1 To lstDirs.Items.Count
            Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " push " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & ForcePush, , True, 9)
            System.Threading.Thread.Sleep(100)
        Next
    End Sub
    
    Sub BtnGitPullSelected_Click(sender As Object, e As EventArgs)
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(lstDirs.SelectedIndex) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, , True, 9)
        End If
    End Sub
    
    Sub BtnGitPushSelected_Click(sender As Object, e As EventArgs)
        If chkPushForce.Checked = True Then ForcePush = "-f"
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(lstDirs.SelectedIndex) & " push " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & ForcePush, , True, 9)
        End If
        
    End Sub
    
    Sub BtnGitPullNotSelected_Click(sender As Object, e As EventArgs)
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            For i = 1 To lstDirs.Items.Count
                If i - 1 <> lstDirs.SelectedIndex
                    Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, , True, 9)
                End If
                System.Threading.Thread.Sleep(100)
            Next
        End If
    End Sub
    
    Sub BtnGitPushNotSelected_Click(sender As Object, e As EventArgs)
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            For i = 1 To lstDirs.Items.Count
                If i - 1 <> lstDirs.SelectedIndex
                    Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " push " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & ForcePush, , True, 9)
                End If
                System.Threading.Thread.Sleep(100)
            Next
        End If
    End Sub
End Class