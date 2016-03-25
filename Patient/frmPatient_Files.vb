Imports System.Data.SqlClient
Imports System.IO



Public Class frmPatient_Files

    Dim imagecoll As New Collection() 'Image paths will be stored in this collection
    Dim Limglst As New ImageList() 'Large ImageList for our ListVie
    Dim SelectedDirectory As String
    Dim thmbNailWidth As Integer
    Dim thmbNailHeight As Integer
    Dim img, pimg As Bitmap
    Dim imgsCurnCnt As String
    Dim StartTime As DateTime
    Dim i As Integer = 0
    Dim disimg As Bitmap
    Dim myconn As New connect


    Dim fimage As String
    Dim S As Integer


    Dim fin As Boolean
    Dim fName As String
    Dim filebyte() As Byte

    '#Region "Function"
    '    Function GetIndexofImg() As Integer
    '        'If i < Me.ListView1.Items.Count Then
    '        'i += 1
    '        '    Return i
    '        'End If
    '    End Function
    '    Sub ImportImages()
    '        Dim op As New OpenFileDialog()
    '        op.Filter = "pdf Files (*.pdf)|"
    '        op.Multiselect = True
    '        op.Title = "Select pdf Files"
    '        If op.ShowDialog = DialogResult.OK Then
    '            'MsgBox(op.FileNames(0))
    '            Dim pth As String
    '            For Each pth In op.FileNames
    '                Dim f() As String
    '                f = Split(pth, "\") 'To get the file name
    '                Array.Reverse(f)
    '                Try
    '                    imagecoll.Add(pth, f(0)) 'Add filename as key to retrive later
    '                Catch
    '                End Try
    '                Try
    '                    If Directory.Exists(SelectedDirectory) Then

    '                        File.Copy(pth, SelectedDirectory & "\" & f(0))
    '                        'MsgBox(SelectedDirectory)
    '                    End If
    '                    'MsgBox(pth & "   " & SelectedDirectory)
    '                    'Pic.BackgroundImage = Image.FromFile(pth & "   " & SelectedDirectory)
    '                    S = drg.Rows.Count
    '                    drg.Rows.Add()
    '                    drg(0, S).Value = S + 1
    '                    drg(1, S).Value = pth & "   " & SelectedDirectory
    '                    drg(2, S).Value = Path.GetFileName(pth)
    '                    S += 1
    '                    'MsgBox(Path.GetFileName(pth))
    '                Catch ex As Exception
    '                    MsgBox(ex.Message, MsgBoxStyle.Critical)
    '                End Try
    '            Next
    '            btnCancel.Enabled = True
    '            btnSave.Enabled = True
    '            'Display the images
    '            'Display(op.FileNames)

    '        End If

    '        'Pic.Image = Image.FromFile(drg.CurrentRow.Cells(1).Value.ToString)

    '    End Sub
    '    Sub Display(ByVal files() As String)
    '        Cursor.Current = Cursors.WaitCursor
    '        Dim fnames() As String = files
    '        If thmbNailWidth <> 0 AndAlso thmbNailHeight <> 0 Then
    '            Limglst.ImageSize = New Size(thmbNailWidth, thmbNailHeight)
    '        Else

    '            'Default size
    '            thmbNailWidth = 160
    '            thmbNailHeight = 160
    '            Limglst.ImageSize = New Size(thmbNailWidth, thmbNailHeight)
    '        End If
    '        Limglst.ColorDepth = ColorDepth.Depth32Bit
    '        showimages(fnames)
    '        Try
    '            'Me.ListView1.LargeImageList = Limglst
    '        Catch
    '        End Try
    '        '--------------------------------------------------------------
    '        Dim fn As String
    '        For Each fn In fnames
    '            Try
    '                Dim picwh As New Bitmap(fn)
    '                Dim fname As New FileInfo(fn)
    '                'Me.ListView1.Items.Add(fname.Name & vbCrLf & picwh.Width & " x " & picwh.Height, GetIndexofImg)
    '                picwh.Dispose()
    '                'MsgBox(fn)
    '            Catch ex As Exception
    '                'MsgBox(ex.Message)
    '            End Try
    '        Next
    '        Cursor.Current = Cursors.Default
    '    End Sub
    '    Sub showimages(ByVal imgs() As String)
    '        Dim image As String
    '        For Each image In imgs
    '            Try
    '                pimg = New Bitmap(image)
    '                Limglst.Images.Add(pimg)
    '                'would occur if the file is not an image file
    '            Catch Ex As Exception
    '                'MsgBox(ex.Message)
    '            End Try
    '        Next
    '    End Sub
    '    'Sub ViewImageInEditor()


    '    '    'If IsNothing(Pic.Image) = False Then
    '    '    'Dim ed As New Form

    '    '    Dim lvitem As ListViewItem
    '    '    For Each lvitem In Me.ListView1.SelectedItems

    '    '        Dim n() As String
    '    '        n = Split(lvitem.Text, vbCrLf)

    '    '        Try

    '    '            disimg = New Bitmap(SelectedDirectory & "\" & n(0))
    '    '            Pic.Image = disimg
    '    '            Pic.SizeMode = PictureBoxSizeMode.CenterImage

    '    '            'ed.Text = n(0) & " - Image Editor v1.0"
    '    '        Catch Ex As Exception
    '    '            MsgBox(Ex.Message)
    '    '        End Try

    '    '        'To Make the Base = 1 instead of 0
    '    '        imgsCurnCnt = lvitem.Index + 1 & "/" & Me.ListView1.Items.Count

    '    '    Next

    '    '    StartTime = Now

    '    '    'ed.ShowDialog()
    '    '    disimg.Dispose()

    '    '    'Else
    '    '    '    MsgBox("Please Select the Image First !", MsgBoxStyle.Exclamation)
    '    '    'End If

    '    'End Sub
    '    Private Function ImageToStream(ByVal fileName As String) As Byte()
    '        Dim stream As New MemoryStream()
    'tryagain:
    '        Try
    '            Dim image As New Bitmap(fileName)
    '            image.Save(stream, Imaging.ImageFormat.Jpeg)
    '        Catch ex As Exception
    '            GoTo tryagain
    '        End Try

    '        Return stream.ToArray()
    '    End Function

    '#End Region

    Sub Filldrg()

        drg.Rows.Clear()
        myconn.Filldataset("select File_Name,VisitID,ID from patient_files where Patient_ID =" & CInt(cboPatient.SelectedValue), "patient_files", Me)
        For i As Integer = 0 To myconn.cur.Count - 1
            drg.Rows.Add(New String() {i + 1, myconn.cur.Current(0), myconn.cur.Current(1), myconn.cur.Current(2)})
            myconn.cur.Position += 1
        Next

    End Sub
    Sub Save_File()
        Dim sql As String = "INSERT INTO Patient_Files(Patient_ID,National_ID,Files_Date,RecordID,VisitID,login_DAte,File_Name,Patient_Files) VALUES(@Patient_ID,@National_ID,@Files_Date,@RecordID,@VisitID,@login_DAte,@File_Name,@Patient_Files)"
        myconn.cmd = New SqlCommand(sql, myconn.conn)
        myconn.cmd.Parameters.Add("@Patient_ID", SqlDbType.Int).Value = txtPatientID.Text
        myconn.cmd.Parameters.Add("@National_ID", SqlDbType.NVarChar).Value = txtPatientIDN.Text
        myconn.cmd.Parameters.Add("@Files_Date", SqlDbType.NChar).Value = Format(CDate(dtp.Text), "yyyy/MM/dd").ToString
        myconn.cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = txtRecordID.Text
        myconn.cmd.Parameters.Add("@VisitID", SqlDbType.Int).Value = cboVisit.SelectedValue
        myconn.cmd.Parameters.Add("@login_DAte", SqlDbType.NChar).Value = txtLogin_Date.Text
        myconn.cmd.Parameters.Add("@File_Name", SqlDbType.NVarChar).Value = fName
        myconn.cmd.Parameters.Add("@Patient_Files", SqlDbType.Image).Value = filebyte
        If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
        myconn.conn.Open()
        myconn.cmd.ExecuteNonQuery()
        myconn.conn.Close()
        filebyte = Nothing
    End Sub
    Sub Updat_File()
        Dim sql As String = "update Patient_Files set Patient_ID = @Patient_ID,National_ID=@National_ID,Files_Date=@Files_Date,RecordID=@RecordID,VisitID=@VisitID,login_DAte=@login_DAte,File_Name=@File_Name,Patient_Files=@Patient_Files where RecordID = @RecordID"
        myconn.cmd = New SqlCommand(sql, myconn.conn)
        myconn.cmd.Parameters.Add("@Patient_ID", SqlDbType.Int).Value = txtPatientID.Text
        myconn.cmd.Parameters.Add("@National_ID", SqlDbType.NVarChar).Value = txtPatientIDN.Text
        myconn.cmd.Parameters.Add("@Files_Date", SqlDbType.NChar).Value = Format(CDate(dtp.Text), "yyyy/MM/dd").ToString
        myconn.cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = txtRecordID.Text
        myconn.cmd.Parameters.Add("@VisitID", SqlDbType.Int).Value = cboVisit.SelectedValue
        myconn.cmd.Parameters.Add("@login_DAte", SqlDbType.NChar).Value = txtLogin_Date.Text
        myconn.cmd.Parameters.Add("@File_Name", SqlDbType.NVarChar).Value = fName
        myconn.cmd.Parameters.AddWithValue("@Patient_Files", filebyte)
        myconn.cmd.Parameters.AddWithValue("@RecordID", txtRecordID.Text)
        If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
        myconn.conn.Open()
        myconn.cmd.ExecuteNonQuery()
        myconn.conn.Close()
    End Sub

    Private Sub frmPatient_Files_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        fin = False
        myconn.Fillcombo3("select * from Patient", "Patient", "patient_ID", "PatientName", Me, cboPatient)
        fin = True
        btnSave.Enabled = False
        btnNew.Enabled = True
        btnCancel.Enabled = False
        adob.setShowToolbar(True)

    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Dim op As New OpenFileDialog()
        op.Filter = "pdf Files (*.pdf)|"
        op.Multiselect = False
        op.Title = "Select pdf Files"
        If op.ShowDialog = DialogResult.OK Then

            filebyte = File.ReadAllBytes(op.FileName)
        End If

        btnSave.Enabled = True
        adob.LoadFile(op.FileName)
        fName = Path.GetFileName(op.FileName)
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cboPatient.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboPatient, "أدخل اسم المريض")
            Return
        End If
        If txtPatientID.Text = "" Then
            ErrorProvider1.SetError(txtPatientID, "أدخل كود المريض")
            Return
        End If
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If cboVisit.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboVisit, "أدخل رقم الزيارة")
            Return
        End If


        Save_File()
        Filldrg()

        MsgBox("لقد تمت عملية الحفظ بنجاح")

        btnNew.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False

    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        fin = False
        cboPatient.SelectedIndex = -1

        cboVisit.SelectedIndex = -1
        fin = True
        txtPatientID.Text = ""
        txtPatientIDN.Text = ""
        txtRecordID.Text = ""
        txtLogin_Date.Text = ""
        dtp.Text = Date.Today
        btnNew.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        drg.Rows.Clear()
        S = 0

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Patient_Files", "ID", CInt(drg.CurrentRow.Cells(3).Value))

        End If
        Filldrg()

    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        If cboPatient.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboPatient, "أدخل اسم المريض")
            Return
        End If
        If txtPatientID.Text = "" Then
            ErrorProvider1.SetError(txtPatientID, "أدخل كود المريض")
            Return
        End If
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If cboVisit.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboVisit, "أدخل رقم الزيارة")
            Return
        End If
        Updat_File()
        Filldrg()

        If drg IsNot Nothing AndAlso drg.CurrentRow IsNot Nothing Then

            MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
        Else
            Return
        End If

    End Sub
    Private Sub cboPatient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPatient.SelectedIndexChanged
        ErrorProvider1.Clear()

        If Not fin Then Return
        txtPatientID.DataBindings.Clear()
        txtPatientID.DataBindings.Add("text", myconn.dv3, "patient_ID")

        txtPatientIDN.DataBindings.Clear()
        txtPatientIDN.DataBindings.Add("text", myconn.dv3, "National_ID")

        fin = False
        myconn.Fillcombo2("select * from Login_Patients  where patient_ID =" & cboPatient.SelectedValue, "Login_Patients", "VisitID", "VisitID", Me, cboVisit)
        fin = True
        Filldrg()

    End Sub
    Private Sub cboVisit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboVisit.SelectedIndexChanged
        If Not fin Then Return
        myconn.Filldataset1("select * from Login_Patients  where visitID =" & cboVisit.SelectedValue & "and patient_ID = " & cboPatient.SelectedValue, "Login_Patients", Me)

        txtRecordID.DataBindings.Clear()
        txtRecordID.DataBindings.Add("text", myconn.dv1, "RecordID")

        txtLogin_Date.DataBindings.Clear()
        txtLogin_Date.DataBindings.Add("text", myconn.dv1, "Login_Date")
    End Sub

    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        myconn.Filldataset("select * from Patient_Files where ID =" & CInt(drg.CurrentRow.Cells(3).Value), "Patient_Files", Me)
        cboPatient.SelectedValue = myconn.cur.Current("Patient_ID")

        cboVisit.SelectedValue = myconn.cur.Current("VisitID")
        '-----------------------------------------------------------------------------------------------------------------

        Dim pdfData() As Byte
        Dim sFileName As String = myconn.cur.Current("File_Name")
        pdfData = DirectCast(myconn.cur.Current("Patient_Files"), Byte())

        Dim strm As Stream = New MemoryStream(pdfData)

        Dim sTempFileName As String = Application.StartupPath & "\" & sFileName

        Using fstream As New FileStream(sFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite)

            fstream.Write(pdfData, 0, pdfData.Length)
            fstream.Flush()
            fstream.Close()

        End Using
        adob.LoadFile(sTempFileName)
        'File.Delete(sTempFileName)

    End Sub

    Private Sub cboPatient_Enter(sender As Object, e As EventArgs) Handles cboPatient.Enter
        myconn.langAR()

    End Sub

    Private Sub frmPatient_Files_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        DeleteFilesFromFolder(Application.StartupPath)
    End Sub
    Sub DeleteFilesFromFolder(Folder As String)
        If Directory.Exists(Folder) Then
            For Each _file As String In Directory.GetFiles(Folder, "*.pdf")
                File.Delete(_file)
            Next
            For Each _folder As String In Directory.GetDirectories(Folder)
                DeleteFilesFromFolder(_folder)
            Next
        End If
    End Sub
End Class