Imports System
Imports System.IO

Public Class GitUpdater
    
    Dim usrProfile As String = Environment.GetEnvironmentVariable("HOMEPATH")
    Dim Dir As String = usrProfile & "\Documents\GitHub"
    
    Private Sub btnExit_Click(sender As Object, e As EventArgs)
        End
    End Sub
    
    Sub BtnPopulate_Click(sender As Object, e As EventArgs)
        For Each Repo As String In Directory.GetDirectories(Dir)
            lstDirs.Items.Add(Mid(Repo, Len(Dir) + 2))
        Next
    End Sub
End Class