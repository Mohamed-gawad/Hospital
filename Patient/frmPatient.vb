Public Class frmPatient
    Dim myconn As New connect
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        myconn.ClearAllControls(GroupBox1, True)
        myconn.Filldataset("select * from Patient", "Patient", Me)
        myconn.Autonumber("patient_ID", "Patient", txtID, Me)
        dToday.Text = Today.Date
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Dim Myfields() As String = {"patient_ID", "patientName", "National_ID", "Age", "Children", "Address", "email", "Mobile1", "Mobile2", "Notes", "phone"}
        Dim Mytxt() As TextBox = {txtID, txtName, txtNid, txtAge, txtChild, txtAddress, txtEmail, txtMobil1, txtMobil2, txtNotes, txtPhon}

        myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)

        myconn.comboBinding("GenderID", cboGender)
        myconn.comboBinding("CityID", cboCity)
        myconn.comboBinding("CountryID", cboCountry)
        myconn.comboBinding("jobID", cboJob)
        myconn.comboBinding("StatusID", cboStatus)
        myconn.DateTPBinding("BirthDate", dBirth)
        myconn.DateTPBinding("FVDate", dToday)
    End Sub

    Private Sub frmPatient_Load(sender As Object, e As EventArgs) Handles Me.Load
        btnSave.Enabled = False
        txtName.Focus()
        Label19.Left = 0
        Label19.Width = Me.Width
        myconn.Fillcombo("select * from Gender", "Gender", "GenderID", "Gender", Me, cboGender)
        myconn.Fillcombo("select * from Country", "Country", "CountryID", "Country", Me, cboCountry)
        myconn.Fillcombo("select * from City", "City", "CityID", "City", Me, cboCity)
        myconn.Fillcombo("select * from Jobs", "Jobs", "JobID", "jobname", Me, cboJob)
        myconn.Fillcombo("select * from Status", "Status", "StatusID", "Status", Me, cboStatus)
        cboCity.SelectedIndex = -1
        cboCountry.SelectedIndex = -1
        cboGender.SelectedIndex = -1
        cboJob.SelectedIndex = -1
        cboStatus.SelectedIndex = -1
        txtNid.Text = ""
        myconn.Autocomplete("Patient", "PatientName", txtName)
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        myconn.Autocomplete("Patient", "PatientName", txtName)
        btnSave.Enabled = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" And txt.Name IsNot "txtNotes" And txt.Name IsNot "txtEmail" And txt.Name IsNot "txtMobil2" And txt.Name IsNot "txtPhon" Then
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

        Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'", txtNid.Text, "'" & dBirth.Text & "'", txtAge.Text, cboGender.SelectedValue,
        cboJob.SelectedValue, txtChild.Text, cboStatus.SelectedValue, "'" & dToday.Text & "'", cboCountry.SelectedValue, cboCity.SelectedValue,
        "'" & txtAddress.Text & "'", "'" & txtEmail.Text & "'", "'" & txtMobil1.Text & "'", "'" & txtMobil2.Text & "'", "'" & txtNotes.Text & "'", "'" & txtPhon.Text & "'"}
        myconn.AddNewRecord("Patient", XX)
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        myconn.ClearAllControls(GroupBox1, True)
        btnSave.Enabled = False
        myconn.Autocomplete("Patient", "PatientName", txtName)
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Dim Values() As String = {"'" & txtName.Text & "'", txtNid.Text, "'" & dBirth.Text & "'", txtAge.Text, cboGender.SelectedValue,
        cboJob.SelectedValue, txtChild.Text, cboStatus.SelectedValue, "'" & dToday.Text & "'", cboCountry.SelectedValue, cboCity.SelectedValue,
        "'" & txtAddress.Text & "'", "'" & txtEmail.Text & "'", "'" & txtMobil1.Text & "'", "'" & txtMobil2.Text & "'", "'" & txtNotes.Text & "'", "'" & txtPhon.Text & "'"}

        Dim Mycolumes() As String = {"name", "National_ID", "BirthDate", "Age", "GenderID", "JobID", "Children", "StatusID", "FVDate", "CountryID", "CityID", "Address", "email", "Mobile1", "Mobile2", "Notes", "phone"}

        myconn.UpdateRecord("patient", Mycolumes, Values, "patient_ID", txtID.Text)

        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click

        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("patient", "patient_ID", txtID.Text)
            myconn.ClearAllText(Me, GroupBox1)

        End If
    End Sub
    Private Sub txtNid_Leave(sender As Object, e As EventArgs) Handles txtNid.Leave
        If txtNid.TextLength <> 14 Then
            MessageBox.Show("الرقم القومي الذي أدخلته غير صحيح", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        If btnSave.Enabled = True Then
            Try
                myconn.Filldataset("select * from patient where National_ID =" & txtNid.Text, "patient", Me)


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

    Private Sub txtAge_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAge.KeyPress, txtMobil1.KeyPress, txtMobil2.KeyPress, txtChild.KeyPress, txtNid.KeyPress, txtPhon.KeyPress, ToolStripTextBox1.KeyPress
        myconn.NumberOnly(txtAge, e)
    End Sub
    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress, txtAddress.KeyPress
        myconn.Arabiconly(e)

    End Sub
    Private Sub txtEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmail.KeyPress
        myconn.EnglishOnly(e)
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Try
            myconn.Filldataset("select * from patient where patient_ID =" & CInt(ToolStripTextBox1.Text), "patient", Me)


            If myconn.dv.Count = 0 Then
                MessageBox.Show("السجل المطلوب غير موجود", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If
        Catch ex As Exception

        End Try

        Binding()
    End Sub
    Private Sub txtName_KeyUp(sender As Object, e As KeyEventArgs) Handles txtName.KeyUp
        If e.KeyCode = Keys.Enter Then
            myconn.Filldataset("select * from patient where PatientName = '" & txtName.Text & "'", "patient", Me)
            Binding()
        End If

    End Sub



#Region "Error"
    Private Sub txtID_TextChanged(sender As Object, e As EventArgs) Handles txtID.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtAge_TextChanged(sender As Object, e As EventArgs) Handles txtAge.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub cboGender_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGender.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub cboJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJob.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboStatus.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub cboCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCountry.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub cboCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCity.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtChild_TextChanged(sender As Object, e As EventArgs) Handles txtChild.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtMobil1_TextChanged(sender As Object, e As EventArgs) Handles txtMobil1.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtAddress_TextChanged(sender As Object, e As EventArgs) Handles txtAddress.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtMobil2_TextChanged(sender As Object, e As EventArgs) Handles txtMobil2.TextChanged
        ErrorProvider1.Clear()
    End Sub

#End Region
End Class