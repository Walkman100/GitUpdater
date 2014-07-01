Imports System
Imports System.IO

Public Class GitUpdater
    
    Dim usrProfile As String = Environment.GetEnvironmentVariable("HOMEPATH")
    Dim Dir As String = usrProfile & "\Documents\GitHub"
    
    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        End
    End Sub
    
    Sub BtnPopulate_Click(sender As Object, e As EventArgs)
        lstDirs.Items.Clear
        For Each Repo As String In Directory.GetDirectories(Dir)
            lstDirs.Items.Add(Mid(Repo, Len(Dir) + 2))
        Next
    End Sub
    
    Sub BtnCD_Click(sender As Object, e As EventArgs)
        ' show file chooser dialog, set result as Dir
    End Sub
    
    Sub BtnGitPullAll_Click(sender As Object, e As EventArgs)
        For i = 1 To lstDirs.Items.Count
            'MsgBox("/k cd " & Dir & "\" & lstDirs.Items.Item(i - 1))
            Process.Start("cmd.exe", "/k cd " & Dir & "\" & lstDirs.Items.Item(i - 1))
            System.Threading.Thread.Sleep(100)
            'Shell("cmd.exe" & " /k cd " & Dir & "\" & lstDirs.Items.Item(i - 1) & "\git pull", , True, 9)
            sendkeys.send("git pull {ENTER}")
            System.Threading.Thread.Sleep(100)
            SendKeys.Send("exit {ENTER}")
            System.Threading.Thread.Sleep(100)
            'Process.Start("cmd.exe", "/k " & Dir & "\" & lstDirs.Items.Item(i - 1) & "\git pull")
            'Shell(Dir & "\" & lstDirs.Items.Item(i - 1) & "\git pull", , True, 9)
        Next
    End Sub
    
    Sub BtnGitPushAll_Click(sender As Object, e As EventArgs)
        For i = 1 To lstDirs.Items.Count
            Process.Start("cmd.exe", "/k cd " & Dir & "\" & lstDirs.Items.Item(i - 1))
            System.Threading.Thread.Sleep(100)
            sendkeys.send("git pull {ENTER}")
            System.Threading.Thread.Sleep(100)
            SendKeys.Send("exit {ENTER}")
            System.Threading.Thread.Sleep(100)
        Next
    End Sub
    
    Sub BtnGitPullSelected_Click(sender As Object, e As EventArgs)
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            Process.Start("cmd.exe", "/k cd " & Dir & "\" & lstDirs.Items.Item(lstDirs.SelectedIndex))
            System.Threading.Thread.Sleep(100)
            sendkeys.send("git pull {ENTER}")
            System.Threading.Thread.Sleep(100)
            SendKeys.Send("exit {ENTER}")
            System.Threading.Thread.Sleep(100)
        End If
    End Sub
    
    Sub BtnGitPushSelected_Click(sender As Object, e As EventArgs)
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            Shell(Dir & lstDirs.Items.Item(lstDirs.SelectedIndex) & "\git push", , True, 9)
        End If
        
    End Sub
    
    Sub BtnGitPullNotSelected_Click(sender As Object, e As EventArgs)
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            For i = 1 To lstDirs.Items.Count
                Shell(Dir & lstDirs.Items.Item(i - 1) & "\git pull", , True, 9)
            Next
        End If
    End Sub
    
    Sub BtnGitPushNotSelected_Click(sender As Object, e As EventArgs)
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            For i = 1 To lstDirs.Items.Count
                Shell(Dir & lstDirs.Items.Item(i - 1) & "\git push", , True, 9)
            Next
        End If
    End Sub
End Class