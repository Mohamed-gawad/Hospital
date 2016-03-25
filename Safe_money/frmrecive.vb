Imports System.Globalization
Imports System.Data.SqlClient
Public Class frmrecive
    Dim fin As Boolean
    Dim Myconn As New connect
    Dim x, y As Integer
    Dim st As String
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.Autonumber("Receipt_ID", "Receipt", txtID, Me)
        Myconn.Filldataset("select isnull(max(Receipt_num),0) as Receipt_num from Receipt", "Receipt", Me)
        If Myconn.cur.Current("Receipt_num") = 0 Then
            txtNum.ReadOnly = False
        Else
            txtNum.ReadOnly = True
            txtNum.Text = Myconn.cur.Current("Receipt_num") + 1
        End If
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Try
            drg.Rows.Clear()
            Select Case X
                Case 0
                    st = "where a.Receipt_date ='" & Format(Today.Date, "yyyy/MM/dd") & "'"
                Case 1
                    st = "where a.Receipt_ID =" & CInt(txtSearch.Text)
                Case 2
                    st = "where a.Receipt_num =" & CInt(txtSearch.Text)
                Case 3
                    st = "where a.Receipt_date ='" & Format(CDate(txtSearch.Text), "yyyy/MM/dd") & "'"
            End Select
            Myconn.Filldataset("Select a.Receipt_ID,a.ID,a.Receipt_num,a.Receipt_date,a.Receipt_time,a.Amount,c.CerviceName,d.DoctorsName,(r.DoctorsName) As Doctor_trans,a.Patient_ID,a.State,
                            e.EmployeeName,a.Doctor_rate,s.specialization,a.Record_ID,(p.PatientName) as in_patient,(a.Patient_name) as out_patient,i.itemName from [dbo].[Receipt] a
                            left join [dbo].[Cervices] c on a.CerviceID = c.CerviceID
                            left join [dbo].[Doctors] d on a.DoctorsID = d.DoctorsID
                            left join [dbo].[Doctors] r on a.Doctor_trans = r.DoctorsID
                            left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                            left join [dbo].[Login_Patients] L on a.Record_ID = l.RecordID
                            left join [dbo].[Patient] p on a.Patient_ID = p.patient_ID
                            left join [dbo].[receipt_item] i on a.itemID = i.itemID 
                            Left join [dbo].[Specialization] s on a.SpecializationID = s.SpecializationID " & st, "Receipt", Me)
            If Myconn.cur.Count = 0 Then Return
            Dim V1 As Decimal
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("itemName")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Receipt_ID")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Receipt_date")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Receipt_time")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Receipt_num")
                drg.Rows(i).Cells(6).Value = If(IsDBNull(Myconn.cur.Current("Patient_ID")), Myconn.cur.Current("out_patient"), Myconn.cur.Current("in_patient"))
                drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Amount")
                drg.Rows(i).Cells(8).Value = Myconn.cur.Current("specialization")
                drg.Rows(i).Cells(9).Value = Myconn.cur.Current("DoctorsName")
                drg.Rows(i).Cells(10).Value = Myconn.cur.Current("CerviceName")
                drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Doctor_trans")
                drg.Rows(i).Cells(12).Value = Myconn.cur.Current("Doctor_rate")
                drg.Rows(i).Cells(13).Value = Myconn.cur.Current("EmployeeName")
                drg.Rows(i).Cells(14).Value = Myconn.cur.Current("ID")
                drg.Rows(i).Cells(15).Value = Myconn.cur.Current("State")

                If drg.Rows(i).Cells(15).Value = True Then
                    drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                    V1 += CDec(drg.Rows(i).Cells(7).Value)
                Else
                    drg.Rows(i).DefaultCellStyle.BackColor = Color.Red
                End If
                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg(drg, 7, Label24, Label23)
        Catch ex As Exception
            Return
        End Try
    End Sub
    Sub Add_one_row()

        Select Case y
            Case 0 ' Save recodr
                Myconn.Filldataset("select a.Receipt_ID,a.ID,a.Receipt_num,a.Receipt_date,a.Receipt_time,a.Amount,c.CerviceName,d.DoctorsName,(r.DoctorsName) as Doctor_trans,a.Patient_ID,
                            e.EmployeeName,a.Doctor_rate,s.specialization,a.Record_ID,(p.PatientName) as in_patient,(a.Patient_name) as out_patient,i.itemName,a.State from  [dbo].[Receipt] a
                            left join [dbo].[Cervices] c on a.CerviceID = c.CerviceID
                            left join [dbo].[Doctors] d on a.DoctorsID = d.DoctorsID
                            left join [dbo].[Doctors] r on a.Doctor_trans = r.DoctorsID
                            left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                            left join [dbo].[Login_Patients] L on a.Record_ID = l.RecordID
                            left join [dbo].[Patient] p on a.Patient_ID = p.patient_ID
                            left join [dbo].[receipt_item] i on a.itemID = i.itemID 
                            Left join [dbo].[Specialization] s on a.SpecializationID = s.SpecializationID
                            where a.Receipt_ID = " & CInt(txtID.Text), "Receipt", Me)

                drg.Rows.Add()
                drg.Rows(drg.Rows.Count - 1).Cells(0).Value = drg.Rows.Count
                drg.Rows(drg.Rows.Count - 1).Cells(1).Value = Myconn.cur.Current("itemName")
                drg.Rows(drg.Rows.Count - 1).Cells(2).Value = Myconn.cur.Current("Receipt_ID")
                drg.Rows(drg.Rows.Count - 1).Cells(3).Value = Myconn.cur.Current("Receipt_date")
                drg.Rows(drg.Rows.Count - 1).Cells(4).Value = Myconn.cur.Current("Receipt_time")
                drg.Rows(drg.Rows.Count - 1).Cells(5).Value = Myconn.cur.Current("Receipt_num")
                drg.Rows(drg.Rows.Count - 1).Cells(6).Value = If(IsDBNull(Myconn.cur.Current("Patient_ID")), Myconn.cur.Current("out_patient"), Myconn.cur.Current("in_patient"))
                drg.Rows(drg.Rows.Count - 1).Cells(7).Value = Myconn.cur.Current("Amount")
                drg.Rows(drg.Rows.Count - 1).Cells(8).Value = Myconn.cur.Current("specialization")
                drg.Rows(drg.Rows.Count - 1).Cells(9).Value = Myconn.cur.Current("DoctorsName")
                drg.Rows(drg.Rows.Count - 1).Cells(10).Value = Myconn.cur.Current("CerviceName")
                drg.Rows(drg.Rows.Count - 1).Cells(11).Value = Myconn.cur.Current("Doctor_trans")
                drg.Rows(drg.Rows.Count - 1).Cells(12).Value = Myconn.cur.Current("Doctor_rate")
                drg.Rows(drg.Rows.Count - 1).Cells(13).Value = Myconn.cur.Current("EmployeeName")
                drg.Rows(drg.Rows.Count - 1).Cells(14).Value = Myconn.cur.Current("ID")
                drg.Rows(drg.Rows.Count - 1).Cells(15).Value = Myconn.cur.Current("State")

                Myconn.DataGridview_MoveLast(drg, 3)

            Case 1 ' UpdateRecord
                Myconn.Filldataset("select a.Receipt_ID,a.ID,a.Receipt_num,a.Receipt_date,a.Receipt_time,a.Amount,c.CerviceName,d.DoctorsName,(r.DoctorsName) as Doctor_trans,a.Patient_ID,
                            e.EmployeeName,a.Doctor_rate,s.specialization,a.Record_ID,(p.PatientName) as in_patient,(a.Patient_name) as out_patient,i.itemName,a.State from  [dbo].[Receipt] a
                            left join [dbo].[Cervices] c on a.CerviceID = c.CerviceID
                            left join [dbo].[Doctors] d on a.DoctorsID = d.DoctorsID
                            left join [dbo].[Doctors] r on a.Doctor_trans = r.DoctorsID
                            left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                            left join [dbo].[Login_Patients] L on a.Record_ID = l.RecordID
                            left join [dbo].[Patient] p on a.Patient_ID = p.patient_ID
                            left join [dbo].[receipt_item] i on a.itemID = i.itemID 
                            Left join [dbo].[Specialization] s on a.SpecializationID = s.SpecializationID
                            where a.ID =" & CInt(drg.CurrentRow.Cells(14).Value), "Receipt", Me)


                drg.CurrentRow.Cells(1).Value = Myconn.cur.Current("itemName")
                drg.CurrentRow.Cells(2).Value = Myconn.cur.Current("Receipt_ID")
                drg.CurrentRow.Cells(3).Value = Myconn.cur.Current("Receipt_date")
                drg.CurrentRow.Cells(4).Value = Myconn.cur.Current("Receipt_time")
                drg.CurrentRow.Cells(5).Value = Myconn.cur.Current("Receipt_num")
                drg.CurrentRow.Cells(6).Value = If(IsDBNull(Myconn.cur.Current("Patient_ID")), Myconn.cur.Current("out_patient"), Myconn.cur.Current("in_patient"))
                drg.CurrentRow.Cells(7).Value = Myconn.cur.Current("Amount")
                drg.CurrentRow.Cells(8).Value = Myconn.cur.Current("specialization")
                drg.CurrentRow.Cells(9).Value = Myconn.cur.Current("DoctorsName")
                drg.CurrentRow.Cells(10).Value = Myconn.cur.Current("CerviceName")
                drg.CurrentRow.Cells(11).Value = Myconn.cur.Current("Doctor_trans")
                drg.CurrentRow.Cells(12).Value = Myconn.cur.Current("Doctor_rate")
                drg.CurrentRow.Cells(13).Value = Myconn.cur.Current("EmployeeName")
                drg.CurrentRow.Cells(14).Value = Myconn.cur.Current("ID")
                drg.CurrentRow.Cells(15).Value = Myconn.cur.Current("State")

                If drg.CurrentRow.Cells(15).Value = True Then
                    drg.CurrentRow.DefaultCellStyle.BackColor = Color.LemonChiffon
                Else
                    drg.CurrentRow.DefaultCellStyle.BackColor = Color.Red
                End If
        End Select
        Myconn.Sum_drg(drg, 7, Label24, Label23)
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Try
            Myconn.Filldataset("select a.National_ID,a.Receipt_ID,a.ID,a.Receipt_num,a.Receipt_date,a.Receipt_time,a.Amount,a.CerviceID,a.DoctorsID,a.Doctor_trans,a.Patient_ID,a.notes,a.Visit_ID,
                            e.EmployeeName,a.Doctor_rate,a.SpecializationID,a.Record_ID,(p.PatientName) as in_patient,(a.Patient_name) as out_patient,a.itemID,a.State from  [dbo].[Receipt] a
                            left join [dbo].[Cervices] c on a.CerviceID = c.CerviceID
                            left join [dbo].[Doctors] d on a.DoctorsID = d.DoctorsID
                            left join [dbo].[Doctors] r on a.Doctor_trans = r.DoctorsID
                            left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                            left join [dbo].[Login_Patients] L on a.Record_ID = l.RecordID
                            left join [dbo].[Patient] p on a.Patient_ID = p.patient_ID
                            left join [dbo].[receipt_item] i on a.itemID = i.itemID 
                            Left join [dbo].[Specialization] s on a.SpecializationID = s.SpecializationID
                            where a.ID = " & CInt(drg.CurrentRow.Cells(14).Value), "Receipt", Me)

            Dim Myfields() As String = {"Receipt_ID", "receipt_num", "National_ID", "amount", "notes"}
            Dim Mytxt() As TextBox = {txtID, txtNum, txtNid, txtAmount, txtNotes}
            Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Myconn.DateTPBinding("Receipt_date", dtb)

        cboItem.SelectedValue = Myconn.cur.Current("itemID")
        cboKsm.SelectedValue = Myconn.cur.Current("specializationID")
        If Not fin Then Return
        cboDoctor.SelectedValue = Myconn.cur.Current("DoctorsID")
        cboCervice.SelectedValue = Myconn.cur.Current("CerviceID")

        If IsDBNull(Myconn.cur.Current("Doctor_trans")) Then
            cboDoctor_trans.SelectedIndex = -1
            txtRate.Text = ""
        Else
            cboDoctor_trans.SelectedValue = Myconn.cur.Current("Doctor_trans")
            txtRate.Text = Myconn.cur.Current("Doctor_rate")
        End If


        If IsDBNull(Myconn.cur.Current("patient_ID")) Then
                cbo_section.SelectedIndex = 0
                txtNid.Text = Myconn.cur.Current("National_ID")
                cbopatient.Text = Myconn.cur.Current("out_patient")
                txtPatient_cod.Text = ""
                txtVisit_Date.Text = ""
                txtRecordID.Text = ""
                cbo_Visit_ID.SelectedIndex = -1
            Else
                cbo_section.SelectedIndex = 1
                cbopatient.SelectedValue = Myconn.cur.Current("patient_ID")
                cbo_Visit_ID.SelectedValue = Myconn.cur.Current("Visit_ID")
                txtRecordID.Text = Myconn.cur.Current("Record_ID")
            End If
        Catch ex As Exception
        MsgBox("هناك خطأ")
        End Try
    End Sub
    Sub Save_Recod()
        Try
            Dim sql As String = "INSERT INTO Receipt(Receipt_ID,Receipt_date,Receipt_time,Receipt_num,DoctorsID,SpecializationID,Patient_ID,National_ID,Amount,Amount_ab,PermissionID,itemID,Users_ID,CerviceID,Visit_ID,Record_ID,Visit_date,Doctor_trans,Doctor_rate,Notes,Patient_name,State) 
                            VALUES(@Receipt_ID,@Receipt_date,@Receipt_time,@Receipt_num,@DoctorsID,@SpecializationID,@Patient_ID,@National_ID,@Amount,@Amount_ab,@PermissionID,@itemID,@Users_ID,@CerviceID,@Visit_ID,@Record_ID,@Visit_date,@Doctor_trans,@Doctor_rate,@Notes,@Patient_name,@State)"

            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@Receipt_ID", txtID.Text)
                .AddWithValue("@Receipt_date", Format(CDate(dtb.Text), "yyyy/MM/dd"))
                .AddWithValue("@Receipt_time", Label22.Text)
                .AddWithValue("@Receipt_num", txtNum.Text)
                .AddWithValue("@DoctorsID", cboDoctor.SelectedValue)
                .AddWithValue("@SpecializationID", cboKsm.SelectedValue)
                .AddWithValue("@Patient_ID", If(cbo_section.SelectedIndex = 0, DBNull.Value, cbopatient.SelectedValue))
                .AddWithValue("@National_ID", If(txtNid.Text = Nothing, DBNull.Value, txtNid.Text))
                .AddWithValue("@Amount", txtAmount.Text)
                .AddWithValue("@Amount_ab", txtAB.Text)
                .AddWithValue("@PermissionID", 1)
                .AddWithValue("@itemID", cboItem.SelectedValue)
                .AddWithValue("@Users_ID", My.Settings.user_ID)
                .AddWithValue("@CerviceID", cboCervice.SelectedValue)
                .AddWithValue("@Visit_ID", If(cbo_Visit_ID.SelectedValue = Nothing, DBNull.Value, cbo_Visit_ID.SelectedValue))
                .AddWithValue("@Record_ID", If(txtRecordID.Text = Nothing, DBNull.Value, txtRecordID.Text))
                .AddWithValue("@Visit_date", If(txtVisit_Date.Text = Nothing, DBNull.Value, txtVisit_Date.Text))
                .AddWithValue("@Doctor_trans", If(cboDoctor_trans.SelectedValue = Nothing, DBNull.Value, cboDoctor_trans.SelectedValue))
                .AddWithValue("@Doctor_rate", If(txtRate.Text = Nothing, DBNull.Value, txtRate.Text))
                .AddWithValue("@Notes", If(txtNotes.Text = Nothing, DBNull.Value, txtNotes.Text))
                .AddWithValue("@Patient_name", If(cbopatient.Text = Nothing, DBNull.Value, cbopatient.Text))
                .AddWithValue("@State", 1)
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            Return
        End Try
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset("select * from Receipt where ID =" & CInt(drg.CurrentRow.Cells(14).Value), "Receipt", Me)
        Binding()
    End Sub
#Region "Form"
    Private Sub frmrecive_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        'If e.KeyCode = Keys.Enter Then
        '    SendKeys.Send("{Tab}")
        'End If
    End Sub
    Private Sub frmrecive_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label16.Left = 0
        Label16.Width = Me.Width
        Myconn.Filldataset("Select * from User_Permissions where EmployeeID = " & CInt(My.Settings.user_ID), "User_Permissions", Me)
        If Myconn.cur.Current("Full_control") = False Then
            btnSave.Enabled = Myconn.cur.Current("Add_oper")
            btnPrint.Enabled = Myconn.cur.Current("print_oper")
            btnDel.Enabled = Myconn.cur.Current("delet_oper")
            btnUpdat.Enabled = Myconn.cur.Current("updat_oper")
            btnSearch.Enabled = Myconn.cur.Current("Search_oper")
            btnCancel.Enabled = Myconn.cur.Current("Cancel_oper")
        End If
        Timer1.Start()

        fin = False
        Myconn.Fillcombo("Select * from patient", "patient", "patient_ID", "patientName", Me, cbopatient)
        fin = True
        Myconn.Fillcombo("Select * from receipt_item", "receipt_item", "itemID", "itemName", Me, cboItem)
        fin = False
        Myconn.Fillcombo("Select * from specialization", "specialization", "specializationID", "specialization", Me, cboKsm)
        fin = True
        Myconn.Fillcombo("Select * from Doctors", "Doctors", "DoctorsID", "DoctorsName", Me, cboDoctor_trans)
        Myconn.Fillcombo("Select * from Doctors", "Doctors", "DoctorsID", "DoctorsName", Me, cboDoctor)
        Myconn.Fillcombo("Select * from Cervices", "Cervices", "CerviceID", "CerviceName", Me, cboCervice)
        Myconn.ClearAllControls(GroupBox3)
        x = 0
        Fillgrd()
        dtb.Text = Today
    End Sub

#End Region

#Region "button"
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        fin = False
        Myconn.ClearAllControls(GroupBox3, True)
        Myconn.ClearAllControls(GroupBox1, True)
        NewRecord()
        fin = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        For Each txt As Control In GroupBox3.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" And txt.Name IsNot "txtPatient_cod" And txt.Name IsNot "txtRate" And txt.Name IsNot "txtRecordID" And txt.Name IsNot "txtVisit_Date" Then
                    ErrorProvider1.SetError(txt, "أكمل البيانات")
                    MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return
                End If
            ElseIf TypeOf txt Is ComboBox Then
                If txt.Text = "" And txt.Name IsNot "cbo_Visit_ID" Then
                    ErrorProvider1.SetError(txt, "أكمل البيانات")
                    MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return

                End If
            End If
        Next
        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" And txt.Name IsNot "txtNotes" And txt.Name IsNot "txtRate" Then
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
        Save_Recod()
        y = 0
        Add_one_row()
        btnNew_Click(Nothing, Nothing)
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Myconn.DeleteRecord("Receipt", "Receipt_ID", CInt(drg.CurrentRow.Cells(2).Value))
            drg.Rows.Remove(drg.SelectedRows(0))
            Myconn.ClearAllControls(GroupBox1, True)
            Myconn.ClearAllControls(GroupBox3, True)
        End If
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Try
            Dim sql As String = "Update  Receipt Set Receipt_ID=@Receipt_ID, Receipt_date =@Receipt_date, Receipt_time =@Receipt_time, Receipt_num =@Receipt_num,
                                 DoctorsID =@DoctorsID, SpecializationID =@SpecializationID, Patient_ID =@Patient_ID, National_ID =@National_ID,
                                 Amount =@Amount, Amount_ab =@Amount_ab, PermissionID =@PermissionID, itemID =@itemID, Users_ID =@Users_ID, CerviceID =@CerviceID,
                                 Visit_ID =@Visit_ID, Record_ID =@Record_ID, Visit_date =@Visit_date, Doctor_trans =@Doctor_trans, Doctor_rate =@Doctor_rate,
                                 Notes =@Notes, Patient_name =@Patient_name where ID =@ID"

            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@Receipt_ID", txtID.Text)
                .AddWithValue("@Receipt_date", Format(CDate(dtb.Text), "yyyy/MM/dd"))
                .AddWithValue("@Receipt_time", Label22.Text)
                .AddWithValue("@Receipt_num", txtNum.Text)
                .AddWithValue("@DoctorsID", cboDoctor.SelectedValue)
                .AddWithValue("@SpecializationID", cboKsm.SelectedValue)
                .AddWithValue("@Patient_ID", If(cbopatient.SelectedValue = Nothing, DBNull.Value, cbopatient.SelectedValue))
                .AddWithValue("@National_ID", If(txtNid.Text = Nothing, DBNull.Value, txtNid.Text))
                .AddWithValue("@Amount", txtAmount.Text)
                .AddWithValue("@Amount_ab", txtAB.Text)
                .AddWithValue("@PermissionID", 1)
                .AddWithValue("@itemID", cboItem.SelectedValue)
                .AddWithValue("@Users_ID", My.Settings.user_ID)
                .AddWithValue("@CerviceID", cboCervice.SelectedValue)
                .AddWithValue("@Visit_ID", If(cbo_Visit_ID.SelectedValue = Nothing, DBNull.Value, cbo_Visit_ID.SelectedValue))
                .AddWithValue("@Record_ID", If(txtRecordID.Text = Nothing, DBNull.Value, txtRecordID.Text))
                .AddWithValue("@Visit_date", If(txtVisit_Date.Text = Nothing, DBNull.Value, txtVisit_Date.Text))
                .AddWithValue("@Doctor_trans", If(cboDoctor_trans.SelectedValue = Nothing, DBNull.Value, cboDoctor_trans.SelectedValue))
                .AddWithValue("@Doctor_rate", If(txtRate.Text = Nothing, DBNull.Value, txtRate.Text))
                .AddWithValue("@Notes", If(txtNotes.Text = Nothing, DBNull.Value, txtNotes.Text))
                .AddWithValue("@Patient_name", If(cbopatient.Text = Nothing, DBNull.Value, cbopatient.Text))
                .AddWithValue("@ID", CInt(drg.CurrentRow.Cells(14).Value))
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()

        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
        Return
        End Try
        y = 1
        Add_one_row()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim sql As String = "Update  Receipt set State = @State where ID = @ID"
        Myconn.cmd = New SqlCommand(sql, Myconn.conn)

        If drg.CurrentRow.Cells(15).Value = True Then
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 0
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(14).Value)
            End With
        Else
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 1
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(14).Value)
            End With
        End If

        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()
        y = 1
        Add_one_row()


    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If cboSearch.SelectedIndex = -1 Then Return
        Select Case cboSearch.SelectedIndex
            Case 0
                x = 1
            Case 1
                x = 2
            Case 2
                x = 3
        End Select
        Fillgrd()
    End Sub

#End Region

#Region "ComboBox"
    Private Sub cbopatient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbopatient.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        If cbopatient.SelectedIndex = -1 Then Return

        If cbo_section.SelectedIndex = 1 Then
            Try
                Myconn.Filldataset3("Select * from Login_Patients  where patient_ID =" & CInt(cbopatient.SelectedValue), "Login_Patients", Me)
                txtNid.Text = Myconn.cur3.Current("National_ID")
                txtPatient_cod.Text = Myconn.cur3.Current("patient_ID")
                fin = False
                Myconn.Fillcombo2("Select * from Login_Patients  where patient_ID =" & CInt(cbopatient.SelectedValue), "Login_Patients", "VisitID", "VisitID", Me, cbo_Visit_ID)
                fin = True
            Catch ex As Exception
                MsgBox("من فضلك قم بتسجيل المريض في القسم الداخلي")
                Return
            End Try
        End If



    End Sub
    Private Sub cbo_Visit_ID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Visit_ID.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        If cbo_Visit_ID.SelectedIndex = -1 Then Return
        Try
            Myconn.Filldataset1("Select * from Login_Patients  where visitID =" & CInt(cbo_Visit_ID.SelectedValue) & "And patient_ID = " & CInt(cbopatient.SelectedValue), "Login_Patient", Me)
            txtRecordID.Text = Myconn.cur1.Current("RecordID")
            txtVisit_Date.Text = Myconn.cur1.Current("Login_Date")
        Catch ex As Exception
            MsgBox("من فضلك قم بتسجيل المريض في القسم الداخلي")
            Return
        End Try
    End Sub
    Private Sub cbo_section_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_section.SelectedIndexChanged
        ErrorProvider1.Clear()
        Select Case cbo_section.SelectedIndex
            Case 0 ' الخارجي
                cbopatient.DropDownStyle = ComboBoxStyle.Simple
                txtPatient_cod.Enabled = False
                txtPatient_cod.Text = Nothing
                txtNid.ReadOnly = False
                txtNid.Text = Nothing
                cbo_Visit_ID.Enabled = False
                cbo_Visit_ID.Text = Nothing
                txtRecordID.Enabled = False
                txtRecordID.Text = Nothing
                txtVisit_Date.Enabled = False
                txtVisit_Date.Text = Nothing
                cbopatient.AutoCompleteMode = AutoCompleteMode.None
            'Myconn.Autocomplete_combo("Receipt", "Patient_name", cbopatient)
            Case 1 ' الداخلي
                cbopatient.DropDownStyle = ComboBoxStyle.DropDown
                txtPatient_cod.Enabled = True
                txtNid.ReadOnly = True
                cbo_Visit_ID.Enabled = True
                txtRecordID.Enabled = True
                txtRecordID.ReadOnly = True
                txtVisit_Date.Enabled = True
                cbopatient.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        End Select
    End Sub
    Private Sub cboKsm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKsm.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        If cboKsm.SelectedIndex = -1 Then Return
        Myconn.Fillcombo1("Select * from Cervices where specializationID =" & cboKsm.SelectedValue, "Cervices", "CerviceID", "CerviceName", Me, cboCervice)
        Myconn.Fillcombo2("Select * from Doctors where specializationID =" & cboKsm.SelectedValue, "Doctors", "DoctorsID", "name", Me, cboDoctor)
    End Sub
    Private Sub cbopatient_Enter(sender As Object, e As EventArgs) Handles cbopatient.Enter
        Myconn.langAR()
    End Sub
    Private Sub cbopatient_KeyUp(sender As Object, e As KeyEventArgs) Handles cbopatient.KeyUp
        'If cbo_section.SelectedIndex = 1 Then Return
        'If e.KeyCode = Keys.Enter Then
        '    If cbo_section.SelectedIndex = 0 Then
        '        Try
        '            Myconn.Filldataset3("Select * from Receipt  where Patient_name = '" & cbopatient.Text & "'", "Receipt", Me)
        '            txtNid.Text = Myconn.cur3.Current("National_ID")
        '        Catch ex As Exception
        '            txtNid.Text = ""
        '            Return
        '        End Try

        '    End If
        'End If
    End Sub
    Private Sub cboItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboItem.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub cboDoctor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDoctor.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub cboCervice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCervice.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
#End Region

#Region "TextBox"

    Private Sub txtNotes_Enter(sender As Object, e As EventArgs) Handles txtNotes.Enter
        Myconn.langAR()
    End Sub
    Private Sub txtNotes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNotes.KeyPress
        Myconn.Arabiconly(e)
    End Sub
    Private Sub txtAmount_TextChanged_1(sender As Object, e As EventArgs) Handles txtAmount.TextChanged, txtRate.TextChanged
        ErrorProvider1.Clear()
        txtAB.Text = clsNumber.nTOword(txtAmount.Text)
    End Sub
    Private Sub txtNum_Leave(sender As Object, e As EventArgs) Handles txtNum.Leave
        'If txtNum.Text = "" Then Return
        'If btnSave.Enabled = True Then
        '    Try
        '        Myconn.Filldataset("Select * from Receipt where receipt_num =" & txtNum.Text, "Receipt", Me)


        '        If Myconn.dv.Count > 0 Then
        '            MessageBox.Show("رقم الإيصال مكرر", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        '            txtNum.Text = ""
        '            txtNum.Focus()
        '            Return
        '        End If
        '    Catch ex As Exception
        '    End Try
        'Else
        'End If
    End Sub
    Private Sub txtPatient_cod_TextChanged(sender As Object, e As EventArgs) Handles txtPatient_cod.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtNid_TextChanged(sender As Object, e As EventArgs) Handles txtNid.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtPatient_cod_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPatient_cod.KeyPress
        Myconn.NumberOnly(txtPatient_cod, e)
    End Sub
    Private Sub txtNid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNid.KeyPress
        Myconn.NumberOnly(txtNid, e)
    End Sub
    Private Sub txtRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRate.KeyPress
        Myconn.NumberOnly(txtRate, e)
    End Sub

    Private Sub txtID_TextChanged(sender As Object, e As EventArgs) Handles txtID.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtNum_TextChanged(sender As Object, e As EventArgs) Handles txtNum.TextChanged
        ErrorProvider1.Clear()
    End Sub


#End Region

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label22.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub


End Class
