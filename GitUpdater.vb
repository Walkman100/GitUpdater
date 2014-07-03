Imports System
Imports System.IO

Public Class GitUpdater
    
    Dim usrProfile As String = Environment.GetEnvironmentVariable("HOMEPATH")
    Dim Dir As String = usrProfile & "\Documents\GitHub"
    Dim ForcePush As String = ""
    
    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        End
    End Sub
    
    Sub BtnSave_Click(sender As Object, e As EventArgs)
        My.Settings.Username = txtUsername.Text
        My.Settings.Password = txtPassword.Text
        lblSaved.Visible = True
        ' wait 2 seconds
        lblSaved.Visible = False
    End Sub
    
    Sub TimerKeyChecker_Tick(sender As Object, e As EventArgs)
        If My.Computer.Keyboard.CtrlKeyDown = True And My.Computer.Keyboard.ShiftKeyDown = True Then
            SendKeys.send(txtUsername.Text & "{ENTER}")
            SendKeys.send(txtPassword.Text & "{ENTER}")
        End If
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
        progressBar.Visible = True
        progressBar.Maximum = lstDirs.Items.Count
        If chkNoWait.Checked = False Then
            For i = 1 To lstDirs.Items.Count
                progressBar.Value = i
                Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True)
            Next
        Else
            For i = 1 To lstDirs.Items.Count
                progressBar.Value = i
                Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True, 1000)
            Next
        End If
        progressBar.Visible = False
        
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
        timerKeyChecker.Start
        progressBar.Visible = True
        progressBar.Maximum = lstDirs.Items.Count
        If chkPushForce.Checked = True Then ForcePush = "-f"
        If chkNoWait.Checked = False Then
            For i = 1 To lstDirs.Items.Count
                progressBar.Value = i
                Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " push " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & ForcePush, vbNormalFocus, True)
            Next
        Else
            For i = 1 To lstDirs.Items.Count
                progressBar.Value = i
                Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " push " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & ForcePush, vbNormalFocus, True, 1000)
            Next
        End If
        progressBar.Visible = False
        timerKeyChecker.Stop
    End Sub
    
    Sub BtnGitPullSelected_Click(sender As Object, e As EventArgs)
        timerKeyChecker.Start
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(lstDirs.SelectedIndex) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True, 1000)
        End If
        timerKeyChecker.Stop
    End Sub
    
    Sub BtnGitPushSelected_Click(sender As Object, e As EventArgs)
        timerKeyChecker.Start
        If chkPushForce.Checked = True Then ForcePush = "-f"
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(lstDirs.SelectedIndex) & " push " & chkRepeat.Checked & " " & chkDontClose.Checked & " " & ForcePush, vbNormalFocus, True, 1000)
        End If
        timerKeyChecker.Stop
    End Sub
    
    Sub BtnGitPullNotSelected_Click(sender As Object, e As EventArgs)
        timerKeyChecker.Start
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            progressBar.Visible = True
            progressBar.Maximum = lstDirs.Items.Count
            If chkNoWait.Checked = False Then
                For i = 1 To lstDirs.Items.Count
                    If i - 1 <> lstDirs.SelectedIndex Then
                        progressBar.Value = i
                        Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True)
                    End If
                Next
            Else
                For i = 1 To lstDirs.Items.Count
                    If i - 1 <> lstDirs.SelectedIndex Then
                        progressBar.Value = i
                        Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True, 1000)
                    End If
                Next
            End If
            progressBar.Visible = False
        End If
        timerKeyChecker.Stop
    End Sub
    
    Sub BtnGitPushNotSelected_Click(sender As Object, e As EventArgs)
        timerKeyChecker.Start
        If lstDirs.SelectedIndex = -1 Then
            MsgBox("No item selected")
        Else
            progressBar.Visible = True
            progressBar.Maximum = lstDirs.Items.Count
            If chkNoWait.Checked = False Then
                For i = 1 To lstDirs.Items.Count
                    If i - 1 <> lstDirs.SelectedIndex Then
                        progressBar.Value = i
                        Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True)
                    End If
                Next
            Else
                For i = 1 To lstDirs.Items.Count
                    If i - 1 <> lstDirs.SelectedIndex Then
                        progressBar.Value = i
                        Shell("GitUpdater.bat " & Dir & "\" & lstDirs.Items.Item(i - 1) & " pull " & chkRepeat.Checked & " " & chkDontClose.Checked, vbNormalFocus, True, 1000)
                    End If
                Next
            End If
            progressBar.Visible = False
        End If
        timerKeyChecker.Stop
    End Sub
    
End Class