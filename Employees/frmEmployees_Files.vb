Imports System.Data.SqlClient
Imports System.IO
Public Class frmEmployees_Files
    Dim myconn As New connect
    Dim fin As Boolean
    Dim fName As String
    Dim filebyte() As Byte
    Sub Filldrg()
        drg.Rows.Clear()
        myconn.Filldataset("select File_name,EmployeeID,ID from Employees_Files where EmployeeID =" & CInt(cboEmployee.SelectedValue), "Employees_Files", Me)
        For i As Integer = 0 To myconn.cur.Count - 1
            drg.Rows.Add(New String() {i + 1, myconn.cur.Current(0), myconn.cur.Current(1), myconn.cur.Current(2)})
            myconn.cur.Position += 1
        Next
    End Sub
    Sub Save_File()
        Dim sql As String = "INSERT INTO Employees_Files(EmployeeID,EmployeeNID,File_date,File_name,Employee_Files) VALUES(@EmployeeID,@EmployeeNID,@File_date,@File_name,@Employee_Files)"
        myconn.cmd = New SqlCommand(sql, myconn.conn)
        myconn.cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = txtEmployee_ID.Text
        myconn.cmd.Parameters.Add("@EmployeeNID", SqlDbType.NVarChar).Value = txtEmployee_NID.Text
        myconn.cmd.Parameters.Add("@File_date", SqlDbType.NChar).Value = Format(CDate(dtp.Text), "yyyy/MM/dd").ToString
        myconn.cmd.Parameters.Add("@File_name", SqlDbType.NVarChar).Value = fName
        myconn.cmd.Parameters.Add("@Employee_Files", SqlDbType.Image).Value = filebyte
        If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
        myconn.conn.Open()
        myconn.cmd.ExecuteNonQuery()
        myconn.conn.Close()
        filebyte = Nothing
    End Sub
    Sub Updat_File()
        Dim sql As String = "update Employees_Files set EmployeeID = @EmployeeID,EmployeeNID=@EmployeeNID,File_date=@File_date,File_name=@File_name,Employee_Files=@Employee_Files where ID = @ID"
        myconn.cmd = New SqlCommand(sql, myconn.conn)
        myconn.cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = txtEmployee_ID.Text
        myconn.cmd.Parameters.Add("@EmployeeNID", SqlDbType.NVarChar).Value = txtEmployee_NID.Text
        myconn.cmd.Parameters.Add("@File_date", SqlDbType.NChar).Value = Format(CDate(dtp.Text), "yyyy/MM/dd").ToString
        myconn.cmd.Parameters.Add("@File_name", SqlDbType.NVarChar).Value = fName
        myconn.cmd.Parameters.Add("@Employee_Files", SqlDbType.Image).Value = filebyte
        If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
        myconn.conn.Open()
        myconn.cmd.ExecuteNonQuery()
        myconn.conn.Close()
    End Sub
    Private Sub frmEmployees_Files_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        fin = False
        myconn.Fillcombo3("select * from Employees", "Employees_Files", "EmployeeID", "EmployeeName", Me, cboEmployee)
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
        If cboEmployee.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboEmployee, "أدخل اسم الموظف")
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
        cboEmployee.SelectedIndex = -1
        fin = True
        txtEmployee_ID.Text = ""
        txtEmployee_NID.Text = ""
        dtp.Text = Date.Today
        btnNew.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        drg.Rows.Clear()
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Employees_Files", "ID", CInt(drg.CurrentRow.Cells(3).Value))
        End If
        Filldrg()
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        If cboEmployee.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboEmployee, "أدخل اسم الموظف")
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
    Private Sub cboEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmployee.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        txtEmployee_ID.DataBindings.Clear()
        txtEmployee_ID.DataBindings.Add("text", myconn.dv3, "EmployeeID")
        txtEmployee_NID.DataBindings.Clear()
        txtEmployee_NID.DataBindings.Add("text", myconn.dv3, "EmployeeNID")
        Filldrg()
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        myconn.Filldataset("select * from Employees_Files where ID =" & CInt(drg.CurrentRow.Cells(3).Value), "Employees_Files", Me)
        cboEmployee.SelectedValue = myconn.cur.Current("EmployeeID")
        '-----------------------------------------------------------------------------------------------------------------
        Dim pdfData() As Byte
        Dim sFileName As String = myconn.cur.Current("File_name")
        pdfData = DirectCast(myconn.cur.Current("Employee_Files"), Byte())

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
    Private Sub cboEmployee_Enter(sender As Object, e As EventArgs) Handles cboEmployee.Enter
        myconn.langAR()
    End Sub

    Private Sub frmEmployees_Files_Closed(sender As Object, e As EventArgs) Handles Me.Closed
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