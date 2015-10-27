Public Class LoginForm1

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.
    Dim mConexion As New MySql.Data.MySqlClient.MySqlConnection
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim mComando As MySql.Data.MySqlClient.MySqlCommand

        mComando = New MySql.Data.MySqlClient.MySqlCommand
        '        mConexion = New MySql.Data.MySqlClient.MySqlConnection


        If Len(PasswordTextBox.Text) > 0 Then
        Else
            PasswordTextBox.Focus()
            Exit Sub
        End If
        If Len(UsernameTextBox.Text) > 0 Then
            mLector = Consulta("select * from usuarios where no_empleado=" & UsernameTextBox.Text & " and palabra_ingreso=sha1('" & PasswordTextBox.Text & "')", mConexion)
            If mLector.Read Then
                iIdUsuario = mLector.Item("id")
            Else
                iIdUsuario = 0
            End If
            mLector.Close()
        Else
            iIdUsuario = 0
        End If
        If iIdUsuario = 0 Then
            PasswordTextBox.Text = ""
            Beep()
            UsernameTextBox.Focus()
            Exit Sub
        End If
        Try
            mComando.Connection = mConexion
            mComando.CommandText = "update usuarios set fecha_login=now(), version='" & sVersion & "' where id=" & iIdUsuario
            mComando.ExecuteNonQuery()
        Catch mierror As MySql.Data.MySqlClient.MySqlException
            ReportarError(mierror.ToString)
        End Try
        Dim sListaSuperUsuarios() As String
        Dim sSuperUsuario As String
        sListaSuperUsuarios = Split(sSuperUsuarios, ",")
        For Each sSuperUsuario In sListaSuperUsuarios
            If iIdUsuario = Val(sSuperUsuario) Then
                bSuperUsuario = True
            End If
        Next

        PasswordTextBox.Text = ""
        PasswordTextBox.Focus()
        Me.Close()

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        iIdUsuario = 0
        Me.Close()
    End Sub

    Private Sub LoginForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mConexion = NuevaConexion()
        Label1.Top = LogoPictureBox.Height - Label1.Height
        Label1.Left = 0
        Label1.Text = "Ver: " & sVersion
    End Sub

    Private Sub UsernameTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles UsernameTextBox.KeyPress
        Select Case e.KeyChar
            Case Chr(8)
            Case "0" To "9"
            Case Else
                e.KeyChar = ""
        End Select
    End Sub


    Private Sub PasswordTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles PasswordTextBox.KeyPress
        Select Case e.KeyChar
            Case "'"
                e.KeyChar = ""
        End Select
    End Sub

    Private Sub UsernameTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsernameTextBox.TextChanged

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class
