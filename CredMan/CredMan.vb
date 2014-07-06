Imports System
Imports System.IO

Public Class CredMan
    
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
    
    Sub btnShowPass_MouseDown(sender As Object, e As EventArgs)
        txtPassword.PasswordChar = ""
    End Sub
    
    Sub btnShowPass_MouseUp(sender As Object, e As EventArgs)
        txtPassword.PasswordChar = "●"
    End Sub
    
    Sub TimerKeyChecker_Tick(sender As Object, e As EventArgs)
        If My.Computer.Keyboard.CtrlKeyDown = True And My.Computer.Keyboard.ShiftKeyDown = True Then
            SendKeys.send(txtUsername.Text & "{ENTER}")
            SendKeys.send(txtPassword.Text & "{ENTER}")
        End If
    End Sub
    
    Sub CredMan_Load(sender As Object, e As EventArgs)
        txtUsername.Text = My.Settings.Username
        txtPassword.Text = My.Settings.Password
    End Sub
End Class