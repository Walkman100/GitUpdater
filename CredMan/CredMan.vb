Imports System
Imports System.IO

Public Class CredMan
    
    Sub CredMan_Load(sender As Object, e As EventArgs)
        txtUsername.Text = My.Settings.Username
        txtPassword.Text = My.Settings.Password
        timerKeyChecker.Start
    End Sub
    
    Sub BtnSave_Click(sender As Object, e As EventArgs)
        My.Settings.Username = txtUsername.Text
        My.Settings.Password = txtPassword.Text
        My.Settings.Save()
        MsgBox("Succesfully Saved!", , "Saved!")
    End Sub
    
    Sub TimerKeyChecker_Tick(sender As Object, e As EventArgs)
        
        'If  Then
            SendKeys.send(txtUsername.Text & "{ENTER}")
            SendKeys.send(txtPassword.Text & "{ENTER}")
        'End If
    End Sub
    
    Sub BtnChangeKeys_Click(sender As Object, e As EventArgs)
        MsgBox("Not implemented yet! Please change the keys in code")
        ' if Esc key pressed, cancel
        ' set lblKeys.Text to "Current keys: " & CurrentKeys
    End Sub
    
    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        timerKeyChecker.Stop
        End
    End Sub
    
    Sub btnShowPass_MouseDown(sender As Object, e As EventArgs)
        txtPassword.PasswordChar = ""
    End Sub
    
    Sub btnShowPass_MouseUp(sender As Object, e As EventArgs)
        txtPassword.PasswordChar = "●"
    End Sub
    
End Class