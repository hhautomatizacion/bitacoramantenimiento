Public Class Form3
    Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection

    Private Sub Actualizar()
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim iRenglon As Integer
        Dim cDescripciones As Collection
        Dim sDescripcion As String
        Dim sSQL As String
        cDescripciones = New Collection
        If Len(TextBox1.Text) = 0 Then
            sSQL = "select distinct descripcion from maquinas order by descripcion"
        Else
            sSQL = "select distinct descripcion from maquinas where descripcion like '%" & TextBox1.Text & "%'order by descripcion"
        End If
        mLector = Consulta(sSQL, Conexion)
        Do While mLector.Read
            cDescripciones.Add(mLector.Item("descripcion"))
        Loop
        mLector.Close()

        DataGridView1.RowCount = 0
        For Each sDescripcion In cDescripciones
            sSQL = "select id,descripcion,ubicacion,marca,modelo,no_serie,fecha_fabricacion,fecha_registro from maquinas where descripcion ='" & sDescripcion & "' order by fecha_registro desc"
            mLector = Consulta(sSQL, Conexion)
            If mLector.Read Then
                DataGridView1.RowCount = iRenglon + 1
                DataGridView1.Item(0, iRenglon).Value = mLector.Item("id")
                DataGridView1.Item(1, iRenglon).Value = mLector.Item("descripcion")
                DataGridView1.Item(2, iRenglon).Value = mLector.Item("ubicacion")
                DataGridView1.Item(3, iRenglon).Value = mLector.Item("marca")
                DataGridView1.Item(4, iRenglon).Value = mLector.Item("modelo")
                DataGridView1.Item(5, iRenglon).Value = mLector.Item("no_serie")
                DataGridView1.Item(6, iRenglon).Value = mLector.Item("fecha_fabricacion")
                DataGridView1.Item(7, iRenglon).Value = mLector.Item("fecha_registro")
                iRenglon = iRenglon + 1
            End If
            mLector.Close()
        Next sDescripcion
    End Sub

    Private Sub Form3_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GuardarPosicion(Me)
        GuardarAnchoColumnas(Me, DataGridView1)
    End Sub

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Conexion = New MySql.Data.MySqlClient.MySqlConnection
        Conexion = NuevaConexion()
        ColocarForm(Me)
        DataGridView1.Top = TextBox1.Height
        DataGridView1.Left = 0
        DataGridView1.Width = Me.ClientRectangle.Width
        DataGridView1.Height = Me.ClientRectangle.Height - TextBox1.Height

        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add("Id", "Id")
        DataGridView1.Columns.Add("Descripcion", "Descripcion")
        DataGridView1.Columns.Add("Ubicacion", "Ubicacion")
        DataGridView1.Columns.Add("Marca", "Marca")
        DataGridView1.Columns.Add("Modelo", "Modelo")
        DataGridView1.Columns.Add("N/S", "N/S")
        DataGridView1.Columns.Add("FechaFab", "FechaFab")
        DataGridView1.Columns.Add("FechaReg", "FechaReg")
        TipoDeLetra(TextBox1)
        CargarAnchoColumnas(Me, DataGridView1)
        Actualizar()
        Timer1.Interval = 500
        Timer1.Enabled = False
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                e.Handled = True
                Me.Close()

            Case Keys.Up
                If DataGridView1.RowCount > 0 Then
                    If DataGridView1.CurrentRow.Index > 0 Then
                        DataGridView1.CurrentCell = DataGridView1.Item(0, DataGridView1.CurrentRow.Index - 1)
                    End If
                End If
                e.Handled = True
            Case Keys.Down
                If DataGridView1.RowCount > 0 Then
                    If DataGridView1.CurrentRow.Index < DataGridView1.RowCount - 1 Then
                        DataGridView1.CurrentCell = DataGridView1.Item(0, DataGridView1.CurrentRow.Index + 1)
                    End If
                End If
                e.Handled = True
        End Select
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Select Case e.KeyChar
            Case vbCr
                If DataGridView1.RowCount > 0 Then
                    Me.Tag = Val(DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value.ToString)
                    Me.Close()
                End If
            Case "'"
                e.KeyChar = ""
        End Select
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        'Actualizar()
        Timer1.Enabled = False
        Timer1.Enabled = True
    End Sub

 



    Private Sub Form3_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        'DataGridView1.Top = TextBox1.Height
        'DataGridView1.Left = 0
        'DataGridView1.Width = Me.ClientRectangle.Width
        'DataGridView1.Height = Me.ClientRectangle.Height - TextBox1.Height
        TextBox1.Focus()
    End Sub


  

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            Me.Tag = Val(DataGridView1.Item(0, e.RowIndex).Value.ToString)
            Me.Close()
        End If
    End Sub

    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGridView1.KeyPress
        If e.KeyChar = vbCr Then
        Else
            TextBox1.SelectedText = e.KeyChar.ToString
            TextBox1.Focus()
        End If
    End Sub



    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            If DataGridView1.RowCount > 0 Then
                Me.Tag = Val(DataGridView1.Item(0, DataGridView1.SelectedRows.Item(0).Index).Value.ToString)
                Me.Close()
            End If
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
        End If
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If

    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Actualizar()
    End Sub
End Class