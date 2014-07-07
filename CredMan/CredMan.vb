Imports System
Imports System.IO

Public Class CredMan
    
    Sub CredMan_Load(sender As Object, e As EventArgs)
        txtUsername.Text = My.Settings.Username
        txtPassword.Text = My.Settings.Password
    End Sub
    
    Sub BtnSave_Click(sender As Object, e As EventArgs)
        My.Settings.Username = txtUsername.Text
        My.Settings.Password = txtPassword.Text
        My.Settings.Save()
        MsgBox("Succesfully Saved!", , "Saved!")
    End Sub
    
    Sub BtnInsert_Click(sender As Object, e As EventArgs)
        Me.WindowState = FormWindowState.Minimized
        System.Threading.Thread.Sleep(1000)
        SendKeys.send(txtUsername.Text & "{ENTER}")
        SendKeys.send(txtPassword.Text & "{ENTER}")
    End Sub
    
    Sub TimerKeyChecker_Tick(sender As Object, e As EventArgs)
        If My.Computer.Keyboard.AltKeyDown = True Then
            SendKeys.send(txtUsername.Text & "{ENTER}")
            SendKeys.send(txtPassword.Text & "{ENTER}")
        End If
    End Sub
    
    Sub BtnStartStop_Click(sender As Object, e As EventArgs)
        If btnStartStop.Text = "Hotkey On" Then
            btnStartStop.Text = "Hotkey Off"
            timerKeyChecker.Start
        ElseIf btnStartStop.Text = "Hotkey Off" Then
            btnStartStop.Text = "Hotkey On"
            timerKeyChecker.Stop
        End If
    End Sub
    
    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        End
    End Sub
    
    Sub btnShowPass_MouseDown(sender As Object, e As EventArgs)
        txtPassword.PasswordChar = ""
    End Sub
    
    Sub btnShowPass_MouseUp(sender As Object, e As EventArgs)
        txtPassword.PasswordChar = "●"
    End Sub
End Class