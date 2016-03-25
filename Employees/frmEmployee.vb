Imports System.IO
Imports System.Data.SqlClient
Public Class frmEmployee
    Dim myconn As New connect
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        myconn.ClearAllControls(GroupBox1, True)
        myconn.Autonumber("RecipientID", "Recipient", txtID, Me)
        dToday.Text = Today.Date

    End Sub

    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        myconn.Filldataset("select j.jobname,* from Employees a left join Jobs j on a.jobID = j.jobID", "Employees", Me)
        For i As Integer = 0 To myconn.cur.Count - 1
            drg.Rows.Add(New String() {i + 1, myconn.cur.Current("EmployeeName"), myconn.cur.Current("EmployeeID"), myconn.cur.Current("EmployeeNID"), myconn.cur.Current("jobname")})
            myconn.cur.Position += 1
        Next
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        On Error Resume Next
        Dim Myfields() As String = {"EmployeeID", "EmployeeName", "EmployeeNID", "Age", "Children", "Address", "email", "Mobile1", "Mobile2", "Notes", "phone", "Certificate", "photo"}
        Dim Mytxt() As TextBox = {txtID, txtName, txtNid, txtAge, txtChild, txtAddress, txtEmail, txtMobil1, txtMobil2, txtNotes, txtPhon, txtCerf, TextBox1}

        myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
        myconn.comboBinding("State_ID", cboState)
        myconn.comboBinding("GenderID", cboGender)
        myconn.comboBinding("CityID", cboCity)
        myconn.comboBinding("CountryID", cboCountry)
        myconn.comboBinding("jobID", cboJob)
        myconn.comboBinding("StatusID", cboStatus)
        myconn.DateTPBinding("EmployeeBirth", dBirth)
        myconn.DateTPBinding("Edate", dToday)
    End Sub
    Sub Save_Employee()
        Try
            Dim sql As String = "INSERT INTO Employees(EmployeeID,Edate,EmployeeName,EmployeeNID,age,EmployeeBirth,GenderID,JobID,statusID,Certificate,State_ID,countryID,cityID,children,phone,mobile1,mobile2,Address,Email,notes) 
                            VALUES(@EmployeeID,@Edate,@EmployeeName,@EmployeeNID,@age,@EmployeeBirth,@GenderID,@JobID,@statusID,@Certificate,@State_ID,@countryID,@cityID,@children,@phone,@mobile1,@mobile2,@Address,@Email,@notes)"

            myconn.cmd = New SqlCommand(sql, myconn.conn)
            With myconn.cmd.Parameters
                .AddWithValue("@EmployeeID", txtID.Text)
                .AddWithValue("@Edate", Format(CDate(dToday.Text), "yyyy/MM/dd"))
                .AddWithValue("@EmployeeName", txtName.Text)
                .AddWithValue("@EmployeeNID", txtNid.Text)
                .AddWithValue("@age", txtAge.Text)
                .AddWithValue("@EmployeeBirth", Format(CDate(dBirth.Text), "yyyy/MM/dd"))
                .AddWithValue("@GenderID", cboGender.SelectedValue)
                .AddWithValue("@JobID", cboJob.SelectedValue)
                .AddWithValue("@statusID", cboStatus.SelectedValue)
                .AddWithValue("@Certificate", txtCerf.Text)
                .AddWithValue("@State_ID", cboState.SelectedValue)
                .AddWithValue("@countryID", cboCountry.SelectedValue)
                .AddWithValue("@cityID", cboCity.SelectedValue)
                .AddWithValue("@children", txtChild.Text)
                .AddWithValue("@phone", If(txtPhon.Text = Nothing, DBNull.Value, txtPhon.Text))
                .AddWithValue("@mobile1", txtMobil1.Text)
                .AddWithValue("@mobile2", If(txtMobil2.Text = Nothing, DBNull.Value, txtMobil2.Text))
                .AddWithValue("@Address", txtAddress.Text)
                .AddWithValue("@Email", If(txtEmail.Text = Nothing, DBNull.Value, txtEmail.Text))
                .AddWithValue("@notes", If(txtNotes.Text = Nothing, DBNull.Value, txtNotes.Text))
            End With
            If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
            myconn.conn.Open()
            myconn.cmd.ExecuteNonQuery()
            myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            Return
        End Try


    End Sub
    Private Sub frmEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label23.Left = 0
        Label23.Width = Me.Width

        myconn.Fillcombo("select * from Gender", "Gender", "GenderID", "Gender", Me, cboGender)
        myconn.Fillcombo("select * from Country", "Country", "CountryID", "Country", Me, cboCountry)
        myconn.Fillcombo("select * from City", "City", "CityID", "City", Me, cboCity)
        myconn.Fillcombo("select * from Jobs", "Jobs", "JobID", "jobname", Me, cboJob)
        myconn.Fillcombo("select * from Status", "Status", "StatusID", "Status", Me, cboStatus)
        myconn.Fillcombo("select * from Employees_Status", "Employees_Status", "State_ID", "State_Name", Me, cboState)
        myconn.ClearAllControls(GroupBox1, True)
        Fillgrd()
        myconn.Autocomplete("Employees", "EmployeeName", txtName)
        btnSave.Enabled = False
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        myconn.Autocomplete("Employees", "EmployeeName", txtName)
        NewRecord()
        btnSave.Enabled = True

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" And txt.Name IsNot "txtNotes" And txt.Name IsNot "txtEmail" And txt.Name IsNot "txtPhon" And txt.Name IsNot "txtMobil2" Then
                    ErrorProvider1.SetError(txt, "أكمل البيانات")
                    MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return
                End If
            ElseIf TypeOf txt Is ComboBox Then
                If txt.Text = "" Then
                    ErrorProvider1.SetError(txt, "أكمل البيانات")
                    MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return

                End If
            End If
        Next
        Save_Employee()

        Dim yy() As String = {txtID.Text, "'" & txtName.Text & "'", "'" & Label21.Text & "'"}
        myconn.AddNewRecord("Recipient", yy)

        Fillgrd()
        myconn.ClearAllControls(GroupBox1, True)
        btnSave.Enabled = False
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click

        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" And txt.Name IsNot "txtNotes" And txt.Name IsNot "txtEmail" And txt.Name IsNot "txtPhon" And txt.Name IsNot "txtMobil2" Then
                    ErrorProvider1.SetError(txt, "أكمل البيانات")
                    MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return
                End If
            ElseIf TypeOf txt Is ComboBox Then
                If txt.Text = "" And txt.Name IsNot "cboDoctor_trans" Then
                    ErrorProvider1.SetError(txt, "أكمل البيانات")
                    MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return

                End If
            End If
        Next


        Try
            Dim sql As String = "Update  Employees set Edate=@Edate,EmployeeName=@EmployeeName,EmployeeNID=@EmployeeNID,age=@age,EmployeeBirth=@EmployeeBirth,GenderID=@GenderID,JobID=@JobID,statusID=@statusID,Certificate=@Certificate,State_ID=@State_ID,countryID=@countryID,cityID=@cityID,children=@children,phone=@phone,mobile1=@mobile1,mobile2=@mobile2,Address=@Address,Email=@Email,notes=@notes where EmployeeID=@EmployeeID "
            myconn.cmd = New SqlCommand(sql, myconn.conn)
            With myconn.cmd.Parameters
                .AddWithValue("@Edate", Format(CDate(dToday.Text), "yyyy/MM/dd"))
                .AddWithValue("@EmployeeName", txtName.Text)
                .AddWithValue("@EmployeeNID", txtNid.Text)
                .AddWithValue("@age", txtAge.Text)
                .AddWithValue("@EmployeeBirth", Format(CDate(dBirth.Text), "yyyy/MM/dd"))
                .AddWithValue("@GenderID", cboGender.SelectedValue)
                .AddWithValue("@JobID", cboJob.SelectedValue)
                .AddWithValue("@statusID", cboStatus.SelectedValue)
                .AddWithValue("@Certificate", txtCerf.Text)
                .AddWithValue("@State_ID", cboState.SelectedValue)
                .AddWithValue("@countryID", cboCountry.SelectedValue)
                .AddWithValue("@cityID", cboCity.SelectedValue)
                .AddWithValue("@children", txtChild.Text)
                .AddWithValue("@phone", If(txtPhon.Text = Nothing, DBNull.Value, txtPhon.Text))
                .AddWithValue("@mobile1", txtMobil1.Text)
                .AddWithValue("@mobile2", If(txtMobil2.Text = Nothing, DBNull.Value, txtMobil2.Text))
                .AddWithValue("@Address", txtAddress.Text)
                .AddWithValue("@Email", If(txtEmail.Text = Nothing, DBNull.Value, txtEmail.Text))
                .AddWithValue("@notes", If(txtNotes.Text = Nothing, DBNull.Value, txtNotes.Text))
                .AddWithValue("@EmployeeID", txtID.Text)
            End With
            If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
            myconn.conn.Open()
            myconn.cmd.ExecuteNonQuery()
            myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            Return
        End Try
        Dim Values() As String = {"'" & txtName.Text & "'"}
        Dim Mycolumes() As String = {"RecipientName"}
        myconn.UpdateRecord("Recipient", Mycolumes, Values, "RecipientID", txtID.Text)

        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Employees", "EmployeeID", txtID.Text)
            myconn.ClearAllControls(GroupBox1, True)
        End If
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        drg.ClearSelection()
        For W As Integer = 0 To drg.Rows.Count - 1

            If drg.Rows(W).Cells(2).Value.ToString.Equals(txtSearch.Text, StringComparison.CurrentCultureIgnoreCase) Then
                drg.Rows(W).Cells(2).Selected = True
                drg.CurrentCell = drg.SelectedCells(2)
                Exit For
            End If
        Next

        If txtSearch.Text = "" Then
            drg.Rows(0).Cells(2).Selected = True
            drg.CurrentCell = drg.SelectedCells(2)
        End If
        If txtSearch.Text = "" Then Return
        drg_CellClick(Nothing, Nothing)
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        myconn.Filldataset("Select * from Employees where EmployeeID =" & CInt(drg.CurrentRow.Cells(2).Value), "Employees", Me)
        Binding()
        btnSave.Enabled = False
    End Sub

    Private Sub txtNid_Leave(sender As Object, e As EventArgs) Handles txtNid.Leave
        If txtNid.TextLength <> 14 Then
            MessageBox.Show("الرقم القومي الذي أدخلته غير صحيح", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        If btnSave.Enabled = True Then
            Try
                myconn.Filldataset("Select * from Employees where EmployeeNID =" & txtNid.Text, "Employees", Me)


                If myconn.dv.Count > 0 Then
                    MessageBox.Show("هذا الرقم موجود من قبل", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Binding()
                    btnSave.Enabled = False
                    Return
                End If

            Catch ex As Exception

            End Try
        Else

        End If

        Dim x As String
        If LSet(txtNid.Text, 1) = 2 Then
            x = "19" & Mid(txtNid.Text, 2, 2)
        Else
            x = "20" & Mid(txtNid.Text, 2, 2)
        End If
        If CInt(Mid(txtNid.Text, 4, 2)) > 12 OrElse CInt(Mid(txtNid.Text, 6, 2)) > 31 OrElse LSet(txtNid.Text, 1) > 3 OrElse LSet(txtNid.Text, 1) < 2 Then
            MessageBox.Show(" الرقم الذي أدخلته غير صحيح", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        dBirth.Text = x & "/" & Mid(txtNid.Text, 4, 2) & "/" & Mid(txtNid.Text, 6, 2) '..................................تاريخ الميلاد


        txtAge.Text = Today.Date.Year - CDate(dBirth.Text).Year '..........................................السن 

        cboCity.SelectedValue = Mid(txtNid.Text, 8, 2)

        If (Mid(txtNid.Text, 13, 1) Mod 2) = 0 Then
            cboGender.SelectedValue = 2
        Else
            cboGender.SelectedValue = 1
        End If

    End Sub
    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtName.Enter, txtAddress.Enter, txtNotes.Enter
        myconn.langAR()
    End Sub
    Private Sub txtEmail_Enter(sender As Object, e As EventArgs) Handles txtEmail.Enter, txtAge.Enter, txtMobil1.Enter, txtMobil2.Enter, txtPhon.Enter
        myconn.langEN()
    End Sub
    Private Sub txtAge_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAge.KeyPress, txtMobil1.KeyPress, txtMobil2.KeyPress, txtChild.KeyPress, txtNid.KeyPress, txtPhon.KeyPress, txtSearch.KeyPress
        myconn.NumberOnly(txtAge, e)
    End Sub
    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress, txtAddress.KeyPress
        myconn.Arabiconly(e)
    End Sub
    Private Sub txtEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmail.KeyPress
        myconn.EnglishOnly(e)
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        ErrorProvider1.Clear()

    End Sub

    Private Sub txtNid_TextChanged(sender As Object, e As EventArgs) Handles txtNid.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtCerf_TextChanged(sender As Object, e As EventArgs) Handles txtCerf.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub cboJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJob.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub cboCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCountry.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub cboCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCity.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtMobil1_TextChanged(sender As Object, e As EventArgs) Handles txtMobil1.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtAddress_TextChanged(sender As Object, e As EventArgs) Handles txtAddress.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtNotes_TextChanged(sender As Object, e As EventArgs) Handles txtNotes.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtAge_TextChanged(sender As Object, e As EventArgs) Handles txtAge.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub cboGender_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGender.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatus.SelectedIndexChanged, cboState.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtHours_TextChanged(sender As Object, e As EventArgs)
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtChild_TextChanged(sender As Object, e As EventArgs) Handles txtChild.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtPhon_TextChanged(sender As Object, e As EventArgs) Handles txtPhon.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtMobil2_TextChanged(sender As Object, e As EventArgs) Handles txtMobil2.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged
        ErrorProvider1.Clear()
    End Sub
End Class