Public Class Form7
    Dim Coleccion As Collection
    Dim mConexion As New MySql.Data.MySqlClient.MySqlConnection
    Private Sub Form7_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GuardarPosicion(Me)
        GuardarAnchoColumnas(Me, DataGridView1)
        Me.Tag = Coleccion
    End Sub

    Private Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ColocarForm(Me)
        mConexion = NuevaConexion()
        coleccion = New Collection
        Coleccion = Me.Tag
        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add("No.", "No.")
        DataGridView1.Columns.Add("Responsable", "Responsable")
        CargarAnchoColumnas(Me, DataGridView1)
        Actualizar()
    End Sub
    Sub Actualizar()
        Dim iContador As Integer
        Dim sElemento As String
        iContador = 0
        DataGridView1.RowCount = Coleccion.Count
        For Each sElemento In Coleccion
            DataGridView1.Item(0, iContador).Value = iContador + 1
            DataGridView1.Item(1, iContador).Value = Obtener("select nombre from usuarios where id=" & sElemento, "nombre", mConexion)
            iContador = iContador + 1
        Next

    End Sub


    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim iIndice As Integer
        If e.RowIndex >= 0 Then
            iIndice = Val(DataGridView1.Item(0, e.RowIndex).Value)
            If iIndice > 0 Then
                Coleccion.Remove(iIndice)
                Actualizar()
            End If
        End If
    End Sub
End Class