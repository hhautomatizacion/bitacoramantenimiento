Public Class Form4
    Dim mConexion As New MySql.Data.MySqlClient.MySqlConnection
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim sDescripcion As String
        Dim sMarca As String
        Dim sModelo As String
        Dim sNumeroSerie As String
        Dim sFechaFabricacion As String
        Dim sUbicacion As String
        Dim sComentarios As String
        Dim sSQL As String
        Dim lId As Long
        Dim mComando As MySql.Data.MySqlClient.MySqlCommand
        mComando = New MySql.Data.MySqlClient.MySqlCommand

        sDescripcion = Strings.UCase(Strings.Left(TextBox1.Text, 30))
        sMarca = Strings.UCase(Strings.Left(TextBox2.Text, 20))
        sModelo = Strings.UCase(Strings.Left(TextBox3.Text, 20))
        sNumeroSerie = Strings.UCase(Strings.Left(TextBox4.Text, 20))
        sFechaFabricacion = DateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss")
        sUbicacion = Strings.UCase(Strings.Left(TextBox6.Text, 30))
        sComentarios = Strings.UCase(Strings.Left(TextBox7.Text, 40))

        sSQL = ""
        Try
            mComando.Connection = mConexion
            sSQL = "insert into maquinas values (0,'" & sDescripcion & "','" & sMarca & "','" & sModelo & "','" & sNumeroSerie & "','" & sFechaFabricacion & "',now(),'" & sUbicacion & "','" & sComentarios & "')"
            mComando.CommandText = sSQL
            mComando.ExecuteNonQuery()

        Catch mierror As MySql.Data.MySqlClient.MySqlException
            ReportarError(sSQL & "|" & mierror.ToString)
        End Try
        lId = Val(Obtener("select id from maquinas where descripcion='" & sDescripcion & "'", "id", mConexionSecundaria))
        If lId > 0 Then
            LimpiarForm()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress
        If e.KeyChar = "'" Then e.KeyChar = ""
    End Sub


    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        PictureBox1.Image = ImageList1.Images(0)
        Timer1.Enabled = False
        Timer1.Enabled = True

    End Sub

    Private Sub Form4_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GuardarPosicion(Me)
    End Sub

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mlector As MySql.Data.MySqlClient.MySqlDataReader
        'mConexion = New MySql.Data.MySqlClient.MySqlConnection
        mConexion = NuevaConexion()
        ColocarForm(Me)

        DateTimePicker1.Value = Now
        Timer1.Interval = 1000
        Timer2.Interval = 1000
        Timer1.Enabled = False
        Timer2.Enabled = False
        mlector = Consulta("select distinct marca from maquinas", mConexion)
        Do While mlector.Read
            TextBox2.AutoCompleteCustomSource.Add(mlector.Item("marca"))
        Loop
        TextBox2.AutoCompleteMode = AutoCompleteMode.Suggest
        TextBox2.AutoCompleteSource = AutoCompleteSource.CustomSource
        mlector.Close()
        mlector = Consulta("select distinct modelo from maquinas", mConexion)
        Do While mlector.Read
            TextBox3.AutoCompleteCustomSource.Add(mlector.Item("modelo"))
        Loop
        TextBox3.AutoCompleteMode = AutoCompleteMode.Suggest
        TextBox3.AutoCompleteSource = AutoCompleteSource.CustomSource
        mlector.Close()
        mlector = Consulta("select distinct ubicacion from maquinas", mConexion)
        Do While mlector.Read
            TextBox6.AutoCompleteCustomSource.Add(mlector.Item("ubicacion"))
        Loop
        TextBox6.AutoCompleteMode = AutoCompleteMode.Suggest
        TextBox6.AutoCompleteSource = AutoCompleteSource.CustomSource
        mlector.Close()
        PictureBox1.Image = ImageList1.Images(0)
        PictureBox2.Image = ImageList1.Images(0)

    End Sub


    Private Sub LimpiarForm()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        DateTimePicker1.Value = Now
        TextBox1.Focus()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        limpiarform()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim lTemp As Long

        Timer1.Enabled = False
        If Len(TextBox1.Text) Then
            lTemp = Val(Obtener("select id from maquinas where descripcion like '" & TextBox1.Text & "'", "id", mConexionSecundaria))
            If lTemp > 0 Then
                PictureBox1.Image = ImageList1.Images(3)
            Else
                PictureBox1.Image = ImageList1.Images(1)
            End If
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Dim lTemp As Long

        Timer2.Enabled = False
        If Len(TextBox4.Text) Then
            lTemp = Val(Obtener("select id from maquinas where no_serie like '" & TextBox4.Text & "'", "id", mConexionSecundaria))
            If lTemp > 0 Then
                PictureBox2.Image = ImageList1.Images(2)
            Else
                PictureBox2.Image = ImageList1.Images(1)
            End If
        End If

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        PictureBox2.Image = ImageList1.Images(0)
        Timer2.Enabled = False
        Timer2.Enabled = True

    End Sub
End Class