Module Module1
    Public mConexionModuloPrincipal As New MySql.Data.MySqlClient.MySqlConnection
    Public mConexionInicial As New MySql.Data.MySqlClient.MySqlConnection
    Public mConexionSecundaria As New MySql.Data.MySqlClient.MySqlConnection
    Public sServer As String
    Public sUser As String
    Public sPassword As String
    Public cVentanasEditor As Collection
    Public cProcesos As Collection
    Public iTamanioMaximoArchivo As Integer
    Public iAutoGuardarSegundos As Integer
    Public iIdUsuario As Integer
    Public iColorLeido As Integer
    Public iColorNoLeido As Integer
    Public iColorAgrupar As Integer
    Public lMesesACargar As Long
    Public sVersion As String
    Public sTempDir As String
    Public sLogDir As String
    Public lDiasConservarTemporales As Long
    Public sNombreFuente As String
    Public iTamanioFuente As Integer
    Public sEstiloFuente As String
    Public sSuperUsuarios As String
    Public bSuperUsuario As Boolean
    Public bAutoAbrirAdjuntos As Boolean
    Public bAutoAbrirRelacionados As Boolean
    Public bColorearRenglon As Boolean
    Public sFechaAnterior As String
    <Serializable()> Public Structure Entrada
        Public IdMaquina As Long
        Public RTF As String
        Public Adjuntos As Collection
        Public Referencias As Collection
        Public Responsables As Collection
    End Structure
    Public Structure Renglon
        Public Id As Integer
        Public Celdas() As String
        Public Colorear As Boolean
        Public TieneAdjuntos As Boolean
        Public Adjuntos As Integer
        Public Leido As Boolean

    End Structure
    Function ConexionEstablecida() As Boolean
        Dim bResultado As Boolean
        Dim mConexion As New MySql.Data.MySqlClient.MySqlConnection
        bResultado = True
        Try
            mConexion.ConnectionString = "server=" & sServer & ";" & "user=" & sUser & ";" & "password=" & sPassword & ";" & "database=bitacora"
            mConexion.Open()
            If Not mConexion.State = ConnectionState.Open Then
                bResultado = False
            End If
            mConexion.Close()
        Catch e As MySql.Data.MySqlClient.MySqlException
            Select Case e.Number
                Case 1042
                    If MsgBox("No se encuentra el servidor " & vbCrLf & sServer.ToUpper, MsgBoxStyle.RetryCancel) = MsgBoxResult.Cancel Then
                        End
                    End If
                Case Else
                    MsgBox(e.ToString, , "Error " & e.Number)
            End Select

            bResultado = False
        End Try
        Return bResultado
    End Function

    Function NuevaConexion(Optional ByVal Reintento As Integer = 0) As MySql.Data.MySqlClient.MySqlConnection
        Dim mConexion As New MySql.Data.MySqlClient.MySqlConnection



        If Reintento >= 5 Then
            If MsgBox("No se encuentra el servidor " & vbCrLf & sServer.ToUpper, MsgBoxStyle.RetryCancel) = MsgBoxResult.Cancel Then
                End
            Else
                mConexion = NuevaConexion(Reintento + 1)
            End If
        Else
            Try
                mConexion.ConnectionString = "server=" & sServer & ";" & "user=" & sUser & ";" & "password=" & sPassword & ";" & "database=bitacora"
                mConexion.Open()

            Catch mierror As MySql.Data.MySqlClient.MySqlException
                Select Case mierror.Number
                    Case 1042
                        Do
                        Loop Until ConexionEstablecida()
                    Case Else
                        MsgBox(mierror.ToString, , "Error " & mierror.Number)
                End Select
                mConexion = NuevaConexion(Reintento + 1)
            End Try
        End If

        Return mConexion
    End Function
    Function SiguienteEntrada() As Integer
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Conexion = NuevaConexion()
        mLector = Consulta("select count(id) from entradas", Conexion)
        If mLector.Read Then
            SiguienteEntrada = mLector.Item(0) + 1
        End If
        mLector.Close()
    End Function
    Function Ahora() As String
        Ahora = Now.ToString("yyyy-MM-dd HH:mm:ss")
    End Function
    Function FormatoBytes(ByVal Bytes As Long) As String
        Dim sUnidad() As String = {"bytes", "Kb", "Mb", "Gb", "Tb"}
        Dim sTemp As Single
        Dim iContador As Integer
        sTemp = Bytes
        If sTemp < 0 Then sTemp = 0
        iContador = 0
        Do While sTemp > 1024
            sTemp = sTemp / 1024
            iContador = iContador + 1
        Loop
        FormatoBytes = sTemp.ToString("0.00") & " " & sUnidad(iContador)
    End Function
    Sub GuardarEntrada(ByVal IdEntradaReferencia As Integer, ByVal IdMaquina As Integer, ByVal Descripcion As String, ByVal Contenido As String, ByVal IdJefeArea As Integer, ByVal Responsables As Collection, ByVal sFechaInicio As String, ByVal sFechaFin As String, ByVal Referencias As Collection, ByVal Adjuntos As Collection, ByVal mConexion As MySql.Data.MySqlClient.MySqlConnection)
        Dim sSQL As String
        Dim sArchivo As String
        Dim sReferencia As String
        Dim sResponsable As String
        Dim iIdEntradaNueva As Integer
        Dim mComando As MySql.Data.MySqlClient.MySqlCommand
        mComando = New MySql.Data.MySqlClient.MySqlCommand


        If mConexion.State = ConnectionState.Open Then
            sSQL = ""
            iIdEntradaNueva = SiguienteEntrada()
            mComando.Connection = mConexion
            If Len(Descripcion) > 0 Then
                Descripcion = Descripcion.Replace(vbCrLf, "")
                Descripcion = Descripcion.Replace("'", "")
                Descripcion = Left(Descripcion, 255)
            End If
            sSQL = "insert into entradas values (0,now()," & IdMaquina & ",'" & Descripcion & "',@Contenido," & Len(Contenido) & "," & IdJefeArea & ",'" & sFechaInicio & "','" & sFechaFin & "'," & iIdUsuario & ",'" & sVersion & "',0)"
            mComando.CommandText = sSQL
            mComando.Parameters.AddWithValue("@Contenido", Contenido)
            mComando.ExecuteNonQuery()
            mComando.Parameters.Clear()
            Try
                mComando.Connection = mConexion
                sSQL = "insert into leidos values (0," & iIdEntradaNueva & "," & iIdUsuario & ",now())"
                mComando.CommandText = sSQL
                mComando.ExecuteNonQuery()
            Catch mierror As MySql.Data.MySqlClient.MySqlException
                ReportarError(sSQL & "|" & mierror.ToString)
            End Try
            If IdEntradaReferencia > 0 Then
                Try
                    mComando.Connection = mConexion
                    sSQL = "insert into referenciaentradas values (0," & iIdEntradaNueva.ToString & "," & IdEntradaReferencia & ")"
                    mComando.CommandText = sSQL
                    mComando.ExecuteNonQuery()
                Catch mierror As MySql.Data.MySqlClient.MySqlException
                    ReportarError(sSQL & "|" & mierror.ToString)
                End Try
            End If
            For Each sReferencia In Referencias
                Try
                    mComando.Connection = mConexion
                    sSQL = "insert into referenciamaquinas values (0," & iIdEntradaNueva.ToString & "," & sReferencia & ")"
                    mComando.CommandText = sSQL
                    mComando.ExecuteNonQuery()
                Catch mierror As MySql.Data.MySqlClient.MySqlException
                    ReportarError(sSQL & "|" & mierror.ToString)
                End Try
            Next
            For Each sResponsable In Responsables
                Try
                    mComando.Connection = mConexion
                    sSQL = "insert into responsables values (0," & iIdEntradaNueva.ToString & "," & sResponsable & ")"
                    mComando.CommandText = sSQL
                    mComando.ExecuteNonQuery()
                Catch mierror As MySql.Data.MySqlClient.MySqlException
                    ReportarError(sSQL & "|" & mierror.ToString)
                End Try
            Next
            For Each sArchivo In Adjuntos
                If FileInUse(sArchivo) Then
                    MsgBox("El archivo '" & sArchivo.ToUpper & "' esta siendo ocupado por otro proceso.")
                Else
                    If FileSize(sArchivo) <= iTamanioMaximoArchivo Then
                        Try
                            mComando.Connection = mConexion
                            sArchivo = sArchivo.Replace(Chr(10), "-")
                            sSQL = "insert into adjuntos values (0," & iIdEntradaNueva.ToString & ",'" & FileName(sArchivo).ToLower & "', @FileDump," & FileSize(sArchivo) & ")"
                            mComando.CommandText = sSQL
                            mComando.Parameters.AddWithValue("@FileDump", FileDump(sArchivo))
                            mComando.ExecuteNonQuery()
                            mComando.Parameters.Clear()
                        Catch mierror As MySql.Data.MySqlClient.MySqlException
                            ReportarError(sSQL & "|" & mierror.ToString)
                        End Try
                    Else
                        MsgBox("El archivo '" & sArchivo.ToUpper & "' es demasiado grande. (Tamaño maximo " & FormatoBytes(iTamanioMaximoArchivo) & ")")
                    End If
                End If
            Next sArchivo
        Else
            mConexion.Open()
            GuardarEntrada(IdEntradaReferencia, IdMaquina, Descripcion, Contenido, IdJefeArea, Responsables, sFechaInicio, sFechaFin, Referencias, Adjuntos, mConexion)
        End If
    End Sub
    Function SeleccionarMaquina() As Integer
        Dim fForma As New Form3
        fForma.ShowDialog()
        SeleccionarMaquina = Val(fForma.Tag)
    End Function

    Function SeleccionarResponsable() As Integer
        Dim fForma As New Form6
        fForma.ShowDialog()
        SeleccionarResponsable = Val(fForma.Tag)
    End Function
    Function FileInUse(ByVal Archivo As String) As Boolean
        Dim fs As IO.FileStream
        FileInUse = False
        Try
            fs = New IO.FileStream(Archivo, IO.FileMode.Open, IO.FileAccess.Read)
            fs.Close()
        Catch
            FileInUse = True
        End Try
    End Function
    Function FileDump(ByVal Archivo As String) As Byte()
        Dim fs As IO.FileStream
        Dim iSize As Integer
        fs = New IO.FileStream(Archivo, IO.FileMode.Open, IO.FileAccess.Read)
        iSize = fs.Length
        FileDump = New Byte(iSize) {}
        fs.Read(FileDump, 0, iSize)
        fs.Close()
    End Function
    Function FileSize(ByVal Archivo As String) As Long
        Dim fs As IO.FileStream
        fs = New IO.FileStream(Archivo, IO.FileMode.Open, IO.FileAccess.Read)
        FileSize = fs.Length
        fs.Close()
    End Function
    Function FileName(ByVal Archivo As String) As String
        Dim fs As IO.FileStream
        fs = New IO.FileStream(Archivo, IO.FileMode.Open, IO.FileAccess.Read)
        FileName = fs.Name.Substring(fs.Name.LastIndexOf("\"))
        fs.Close()
    End Function
    Function Obtener(ByVal SQL As String, ByVal Campo As String, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection) As String

        Dim mOtroComando As New MySql.Data.MySqlClient.MySqlCommand
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader

        Obtener = ""
        Try
            mOtroComando.Connection = Conexion
            mOtroComando.CommandText = SQL
            mLector = mOtroComando.ExecuteReader
            Do While mLector.Read
                Obtener = mLector.Item(Campo)
            Loop
            mLector.Close()
        Catch
        End Try
    End Function
    Function Consulta(ByVal SQL As String, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection) As MySql.Data.MySqlClient.MySqlDataReader
        Dim mComando As MySql.Data.MySqlClient.MySqlCommand
        Dim mDataReader As MySql.Data.MySqlClient.MySqlDataReader
        Dim sTemporizador As New System.Diagnostics.Stopwatch

        mComando = New MySql.Data.MySqlClient.MySqlCommand
        If Conexion.State = ConnectionState.Open Then
            mComando.Connection = Conexion
            mComando.CommandText = SQL
            Try
                mDataReader = mComando.ExecuteReader
            Catch mierror As MySql.Data.MySqlClient.MySqlException
                ReportarError(SQL & " | " & mierror.ToString & "| ERROR #" & mierror.Number)

                mDataReader = Consulta(SQL, Conexion)
            End Try
        Else
            Conexion.Open()
            mDataReader = Consulta(SQL, Conexion)
        End If

        Consulta = mDataReader
        sTemporizador.Stop()
        Debug.Print("Consulta " & SQL.Substring(7, 15) & ":" & sTemporizador.Elapsed.ToString)
    End Function


    Sub ReportarError(ByVal DescripcionError As String)
        Dim iContador As Integer
        iContador = 0
        Debug.Print("Error: " & DescripcionError)
        Try
            Do
                iContador = iContador + 1
                If Len(DescripcionError) >= 45 Then
                    MiSaveSetting("BitacoraMantenimiento", "Errores " & sVersion, Ahora() & " - " & iContador.ToString("00"), DescripcionError.Substring(0, 44))
                    DescripcionError = DescripcionError.Remove(0, 44)
                Else
                    MiSaveSetting("BitacoraMantenimiento", "Errores " & sVersion, Ahora() & " - " & iContador.ToString("00"), DescripcionError)
                    DescripcionError = ""
                End If
            Loop Until Len(DescripcionError) = 0
        Catch ex As Exception
            Try
                Dim w As IO.StreamWriter
                w = New IO.StreamWriter(slogdir & "error bitacora " & Now.ToString("yyyy-MM-dd") & ".log", True)
                w.WriteLine(iIdUsuario.ToString & vbTab & sVersion & vbTab & Ahora() & vbTab & DescripcionError)
                w.Close()
            Catch
            End Try
        End Try
    End Sub
    Sub GuardarPosicion(ByVal Forma As Form)
        If Forma.WindowState = FormWindowState.Minimized Then
        Else
            If Forma.WindowState = FormWindowState.Normal Then
                SaveSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "Opacidad", Forma.Opacity.ToString("0.00"))
                SaveSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "Top", (Forma.Top / System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height).ToString("0.000"))
                SaveSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "Left", (Forma.Left / System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width).ToString("0.000"))
                SaveSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "Width", (Forma.Width / System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width).ToString("0.000"))
                SaveSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "Height", (Forma.Height / System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height).ToString("0.000"))
            End If
            SaveSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "Estado", Forma.WindowState.ToString)
        End If
    End Sub
    Sub ColocarForm(ByVal Forma As Form)
        Dim iTop As Single
        Dim iLeft As Single
        Dim iWidth As Single
        Dim iHeight As Single
        Dim sOpacidad As Single
        Forma.Visible = False
        sOpacidad = Val(Replace(GetSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "Opacidad", "1"), ",", "."))
        iTop = Val(Replace(GetSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "Top", "0"), ",", ".")) * System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height
        iLeft = Val(Replace(GetSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "Left", "0"), ",", ".")) * System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width
        iWidth = Val(Replace(GetSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "Width", ".5"), ",", ".")) * System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width
        iHeight = Val(Replace(GetSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "Height", ".5"), ",", ".")) * System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height
        If iLeft > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - iWidth Then iLeft = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - iWidth
        If iTop > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - iHeight Then iTop = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - iHeight
        If iLeft < 0 Then iLeft = 0
        If iTop < 0 Then iTop = 0
        Forma.Opacity = sOpacidad
        Forma.Top = iTop
        Forma.Left = iLeft
        Forma.Width = iWidth
        Forma.Height = iHeight
        If GetSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "Estado", "Normal").ToUpper = "NORMAL" Then
        Else
            Forma.WindowState = FormWindowState.Maximized
        End If
        Forma.Visible = True
    End Sub

    Sub BorrarAutosave()



        If System.IO.File.Exists(sTempDir & iIdUsuario & "-autosave.bak") Then
            Try
                My.Computer.FileSystem.DeleteFile(sTempDir & iIdUsuario & "-autosave.bak")
            Catch
                ReportarError("No se pudo borrar el archivo autosave " & sTempDir & iIdUsuario & "-autosave.bak")
            End Try
        End If


    End Sub
    Sub GuardarAnchoColumnas(ByVal Forma As Form, ByVal DataGrid As DataGridView)
        Dim iColumna As Integer
        For iColumna = 0 To DataGrid.Columns.Count - 1
            SaveSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "AnchoColumna" & iColumna, (DataGrid.Columns.Item(iColumna).Width / System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width).ToString)
        Next
    End Sub
    Function CollectionJoin(ByVal Coleccion As Collection) As String
        Dim iIter As Integer
        Dim sResultado As String
        sResultado = ""
        For iIter = 1 To Coleccion.Count - 1
            sResultado = sResultado & Coleccion.Item(iIter) & ","
        Next iIter
        sResultado = sResultado & Coleccion.Item(Coleccion.Count)
        CollectionJoin = sResultado
    End Function
    Sub TipoDeLetra(ByVal CajaDeTexto As TextBox)
        Try
            CajaDeTexto.Font = New Font(sNombreFuente, iTamanioFuente)
        Catch e As Exception
            CajaDeTexto.Font = New Font("ARIAL", "10")
        End Try
    End Sub

    Sub CargarAnchoColumnas(ByVal Forma As Form, ByVal DataGrid As DataGridView)
        Dim iColumna As Integer
        Dim iAncho As Integer
        Try
            DataGrid.Font = New Font(sNombreFuente, iTamanioFuente)
        Catch e As Exception
            sNombreFuente = "Arial"
            iTamanioFuente = 10
            DataGrid.Font = New Font(sNombreFuente, iTamanioFuente)
        End Try
        DataGrid.DefaultCellStyle.Font = New Font(sNombreFuente, iTamanioFuente)
        For iColumna = 0 To DataGrid.Columns.Count - 1
            iAncho = Val(Replace(GetSetting("BitacoraMantenimiento", "Ventanas", "Usuario" & iIdUsuario.ToString & Forma.Name & "AnchoColumna" & iColumna.ToString, ".1"), ",", ".")) * System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width
            If iAncho > 3000 Or iAncho < 10 Then iAncho = 100
            DataGrid.Columns.Item(iColumna).Width = iAncho
        Next iColumna
    End Sub
    Sub MarcarComoLeido(ByVal IdEntrada As Integer)
        Dim mComando As MySql.Data.MySqlClient.MySqlCommand
        mComando = New MySql.Data.MySqlClient.MySqlCommand
        Dim mConexion As New MySql.Data.MySqlClient.MySqlConnection
        Dim sSQL As String
        mConexion = NuevaConexion()
        Try
            mComando.Connection = mConexion
            sSQL = "insert into leidos values (0," & IdEntrada & "," & iIdUsuario & ",now())"
            mComando.CommandText = sSQL
            mComando.ExecuteNonQuery()
        Catch mierror As MySql.Data.MySqlClient.MySqlException
            ReportarError(mierror.ToString)
        End Try
        mConexion.Close()
    End Sub
    Sub AbrirEntrada(ByVal IdEntrada As Integer, Optional ByVal AbrirReferencias As Boolean = True)
        Dim cEntradas As Collection
        Dim cAdjuntos As Collection
        Dim iEntrada As Integer
        Dim iAdjunto As Integer
        Dim iIdReferencia As Integer
        Dim RawData() As Byte
        Dim iReferenciaPrincipal As Integer
        Dim sFecha As String
        Dim iUsuario As Integer
        Dim iFileSize As Integer

        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader

        Dim iContador As Integer
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Dim fEntrada As New Form5

        Conexion = NuevaConexion()


        fEntrada.Tag = IdEntrada
        sFecha = ""
        iIdReferencia = 0
        cEntradas = New Collection
        cAdjuntos = New Collection

        If bAutoAbrirRelacionados And AbrirReferencias Then
            mLector = Consulta("select identrada from referenciaentradas where idreferencia=" & IdEntrada, Conexion)
            Do While mLector.Read
                cEntradas.Add(mLector.Item("identrada"))
            Loop
            mLector.Close()
            If cEntradas.Count > 0 Then
                For Each iEntrada In cEntradas
                    If iEntrada <> IdEntrada Then
                        AbrirEntrada(iEntrada, False)
                    End If
                Next
            Else
                mLector = Consulta("select idreferencia from referenciaentradas where identrada=" & IdEntrada, Conexion)
                Do While mLector.Read
                    iIdReferencia = mLector.Item("idreferencia")
                Loop
                mLector.Close()
                If iIdReferencia > 0 Then
                    If iIdReferencia <> iEntrada Then
                        AbrirEntrada(iIdReferencia, False)
                    End If
                End If
            End If
        End If

        If bAutoAbrirAdjuntos Then
            mLector = Consulta("select id from adjuntos where identrada=" & IdEntrada, Conexion)
            Do While mLector.Read
                cAdjuntos.Add(mLector.Item("id"))
            Loop
            mLector.Close()
            iContador = 0
            For Each iAdjunto In cAdjuntos
                iContador = iContador + 1
                AbrirAdjunto(IdEntrada, iAdjunto, iContador.ToString)
            Next
        End If
        mLector = Consulta("select maquina,usuario,fecha,tamanio,contenido from entradas where id=" & IdEntrada.ToString, Conexion)
        Do While mLector.Read
            Try
                iReferenciaPrincipal = mLector.Item("maquina")
                iUsuario = mLector.Item("usuario")
                sFecha = CDate(mLector.Item("fecha")).ToString("ddd dd-MMM-yyyy HH:mm")
                iFileSize = mLector.Item("tamanio")
                RawData = New Byte(iFileSize) {}
                mLector.GetBytes(mLector.GetOrdinal("contenido"), 0, RawData, 0, iFileSize)
                fEntrada.RichTextBox1.Rtf = System.Text.Encoding.ASCII.GetString(RawData)
            Catch
            End Try
        Loop
        mLector.Close()
        Conexion.Close()
        't1.Stop()
        'Debug.Print("AbrirEntrada: ".PadRight(25, " ") & t1.Elapsed.ToString)
        fEntrada.Text = IdEntrada.ToString & " - " & Obtener("select descripcion from maquinas where id=" & iReferenciaPrincipal, "descripcion", mConexionModuloPrincipal) & " - " & sFecha & " (" & Obtener("select nombre from usuarios where id=" & iUsuario, "nombre", mConexionModuloPrincipal) & ")"
        fEntrada.Show()
        fEntrada.Focus()
    End Sub
    Public Function StringToStream(ByVal layoutString As String) As IO.Stream
        If String.IsNullOrEmpty(layoutString) Then Return Nothing
        Dim sw As IO.StreamWriter = New IO.StreamWriter(New IO.MemoryStream)
        sw.Write(layoutString)
        sw.Flush()
        Return sw.BaseStream
    End Function

    Sub AbrirAdjunto(ByVal IdEntrada As Integer, ByVal IdAdjunto As Integer, Optional ByVal Comentario As String = "")
        Dim mLector As MySql.Data.MySqlClient.MySqlDataReader
        Dim iFileSize As Integer
        Dim sFileName As String
        Dim sNombre As String
        Dim sExtension As String
        Dim RawData() As Byte
        Dim Conexion As New MySql.Data.MySqlClient.MySqlConnection
        Dim fs As IO.FileStream
        Dim sMensajeError As String
        Conexion = NuevaConexion()
        sFileName = ""
        sMensajeError = ""
        mLector = Consulta("select tamanio,nombre,datos,identrada from adjuntos where id=" & IdAdjunto.ToString, Conexion)
        Do While mLector.Read
            Try

                iFileSize = mLector.GetUInt32(mLector.GetOrdinal("tamanio"))

                sNombre = mLector.Item("nombre")
                sNombre = sNombre.Replace(Chr(10), "-")
                sExtension = IO.Path.GetExtension(sNombre)

                RawData = New Byte(iFileSize) {}

                mLector.GetBytes(mLector.GetOrdinal("datos"), 0, RawData, 0, iFileSize)

                sFileName = sTempDir & "Temp" & Now.ToString("yyyyMMddHHmmss") & "-" & mLector.Item("identrada") & "-" & Comentario & sExtension

                fs = New IO.FileStream(sFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.Write)

                fs.Write(RawData, 0, iFileSize)

                fs.Close()
            Catch mierror As Exception
                sMensajeError = sMensajeError & "'" & sFileName.ToUpper & "'" & vbCrLf
                ReportarError(mierror.ToString)
            End Try
        Loop
        mLector.Close()
        Conexion.Close()
        Try
            Dim p As New System.Diagnostics.Process
            Dim s As New System.Diagnostics.ProcessStartInfo(sFileName)
            s.UseShellExecute = True
            s.WindowStyle = ProcessWindowStyle.Normal
            p.StartInfo = s
            p.Start()
            cProcesos.Add(p)
        Catch mierror As Exception
            If Comentario = "retry" Then
                sMensajeError = sMensajeError & "'" & sFileName.ToUpper & "'" & vbCrLf
            Else
                AbrirAdjunto(IdEntrada, IdAdjunto, "retry")
            End If
        End Try
        If Len(sMensajeError) And Len(Comentario) = 0 Then
            MsgBox("Entrada " & IdEntrada & vbCrLf & vbCrLf & "No se pudo abrir archivo adjunto:" & vbCrLf & sMensajeError)
        End If
    End Sub
    Sub EspacioUtilizado(ByVal CajaTextoRico As RichTextBox)
        MsgBox("Caracteres:" & vbTab & Len(CajaTextoRico.Text) & vbCrLf & "Espacio utilizado:" & vbTab & FormatoBytes(Len(CajaTextoRico.Rtf)) & vbCrLf & "Espacio disponible:" & vbTab & FormatoBytes(65535 - Len(CajaTextoRico.Rtf)) & vbCrLf & vbCrLf & "Tamaño maximo adjuntos:" & vbTab & FormatoBytes(iTamanioMaximoArchivo), , "Tamaño del mensaje")
    End Sub

    Sub MiSaveSetting(ByVal Aplicacion As String, ByVal Seccion As String, ByVal Clave As String, ByVal Valor As String)
        Dim bEncontrado As Boolean
        Dim mOtraConexion As New MySql.Data.MySqlClient.MySqlConnection
        Dim mOtroComando As New MySql.Data.MySqlClient.MySqlCommand


        mOtraConexion.ConnectionString = "server=" & sServer & ";" & "user=" & sUser & ";" & "password=" & sPassword & ";" & "database=bitacora"
        mOtraConexion.Open()
        mOtroComando.Connection = mOtraConexion

        bEncontrado = False
        If Len(Obtener("select valor from preferencias where usuario=" & iIdUsuario & "  and seccion='" & Seccion.ToUpper & "' and clave='" & Clave.ToUpper & "'", "valor", mConexionModuloPrincipal)) > 0 Then
            bEncontrado = True
        End If




        If Len(Valor) > 45 Then
            Valor = Valor.Substring(0, 44)
        End If
        If bEncontrado Then
            mOtroComando.CommandText = "update preferencias set valor='" & Valor.ToUpper & "' where usuario=" & iIdUsuario & " and seccion='" & Seccion.ToUpper & "' and clave='" & Clave.ToUpper & "'"
        Else
            mOtroComando.CommandText = "insert into preferencias values (0," & iIdUsuario & ",'" & Seccion.ToUpper & "','" & Clave.ToUpper & "','" & Valor.ToUpper & "')"
        End If
        mOtroComando.ExecuteNonQuery()

        mOtraConexion.Close()
    End Sub
    Function MiGetSetting(ByVal Aplicacion As String, ByVal Seccion As String, ByVal Clave As String, ByVal ValorPorDefecto As String) As String
        'Dim mLector As MySql.Data.MySqlClient.MySqlDataReader

        Dim sResultado As String
        sResultado = Obtener("select valor from preferencias where usuario=" & iIdUsuario & "  and seccion='" & Seccion.ToUpper & "' and clave='" & Clave.ToUpper & "'", "valor", mConexionModuloPrincipal)
        If Len(sResultado) = 0 Then
            sResultado = ValorPorDefecto
        End If
        MiGetSetting = sResultado
    End Function

End Module
