Imports System.Data.SqlClient
Imports System.Globalization
Public Class frmClinics
    Dim myconn As New connect
    Dim fin As Boolean
    Dim x As Integer
    Dim st As String
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم

        myconn.ClearAllText(Me, GroupBox1)
        myconn.Filldataset("select  isnull(max(ReserveID),0) as ReserveID from Clinics where DetecteDate ='" & Format(CDate(dtp.Text), "yyyy/MM/dd").ToString & "' AND DoctorsID =" & CInt(cboDoctor.SelectedValue), "Clinics", Me)
        If myconn.dv.Count = 0 Then
            txtReserveID.Text = "1"
        Else
            txtReserveID.Text = (myconn.cur.Current("ReserveID") + 1).ToString
        End If
        If drg.Rows.Count = 0 Then
            dtp_time.Text = TimeOfDay.ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        Else
            myconn.DataGridview_MoveLast(drg, 2)
            dtp_time.Text = CDate(drg.CurrentRow.Cells(5).Value).AddMinutes(10)
        End If
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Select Case x
            Case 0
                st = " where a.DetecteDate ='" & Format(CDate(dtp.Text), "yyyy/MM/dd") & "'AND a.DoctorsID =" & CInt(cboSearchDoctor.ComboBox.SelectedValue)
            Case 1
                st = " where a.DetecteDate ='" & Format(CDate(txtSearchDate.TextBox.Text), "yyyy/MM/dd") & "' AND a.DoctorsID =" & CInt(cboSearchDoctor.ComboBox.SelectedValue)
            Case 2
                st = " where a.ReserveDate ='" & Format(CDate(dtp.Text), "yyyy/MM/dd") & "'"
            Case 3
                st = " where a.DetecteDate ='" & Format(CDate(txtSearchDate.TextBox.Text), "yyyy/MM/dd") & "'"
            Case 4
                st = " where a.DetecteDate ='" & Format(CDate(dtp.Text), "yyyy/MM/dd") & "' AND a.DoctorsID =" & CInt(cboDoctor.SelectedValue)

        End Select
        myconn.Filldataset("select a.ReserveID, a.ReserveDate, a.ReserveTime, a.DetecteDate, a.Detecte_time,a.PatientName, b.DoctorsName, c.specialization, a.ReservKind, a.ID  from Clinics a" &
                                   " left join Doctors b on a.doctorsID = b.DoctorsID " &
                                   " left join specialization c on a.specializationID = c.specializationID " & st, "Clinics", Me)

        For i As Integer = 0 To myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = myconn.cur.Current("ReserveID")
            drg.Rows(i).Cells(2).Value = myconn.cur.Current("ReserveDate")
            drg.Rows(i).Cells(3).Value = CDate(myconn.cur.Current("ReserveTime")).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg"))
            drg.Rows(i).Cells(4).Value = myconn.cur.Current("DetecteDate")
            drg.Rows(i).Cells(5).Value = CDate(myconn.cur.Current("Detecte_time")).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg"))
            drg.Rows(i).Cells(6).Value = myconn.cur.Current("PatientName")
            drg.Rows(i).Cells(7).Value = myconn.cur.Current("DoctorsName")
            drg.Rows(i).Cells(8).Value = myconn.cur.Current("specialization")
            drg.Rows(i).Cells(9).Value = myconn.cur.Current("ReservKind")
            drg.Rows(i).Cells(10).Value = myconn.cur.Current("ID")

            If drg.Rows(i).Cells(9).Value.ToString.Equals("كشف") Then drg.Rows(i).DefaultCellStyle.BackColor = Color.Pink
            myconn.cur.Position += 1
        Next
        myconn.DataGridview_MoveLast(drg, 2)
    End Sub
    Sub Save_record()
        Try
            Dim sql As String = "INSERT INTO  Clinics (ReserveID,PatientName,ReserveDate,ReserveTime,DetecteDate,Detecte_time,DoctorsID,specializationID,ReservKind)
                                                values (@ReserveID,@PatientName,@ReserveDate,@ReserveTime,@DetecteDate,@Detecte_time,@DoctorsID,@specializationID,@ReservKind)"
            myconn.cmd = New SqlCommand(sql, myconn.conn)
            With myconn.cmd.Parameters
                .AddWithValue("@ReserveID", txtReserveID.Text)
                .AddWithValue("@PatientName", txtPatient.Text)
                .AddWithValue("@ReserveDate", Format(CDate(Today.Date), "yyyy/MM/dd"))
                .AddWithValue("@ReserveTime", CDate(Label8.Text).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@DetecteDate", Format(CDate(dtp.Text), "yyyy/MM/dd"))
                .AddWithValue("@Detecte_time", CDate(dtp_time.Text).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@DoctorsID", cboDoctor.SelectedValue)
                .AddWithValue("@specializationID", cboClinic.SelectedValue)
                .AddWithValue("@ReservKind", cboReseve.Text)
            End With
            If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
            myconn.conn.Open()
            myconn.cmd.ExecuteNonQuery()
            myconn.conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try
    End Sub
    Sub Update_record()
        Try
            Dim sql As String = "Update  Clinics set ReserveID=@ReserveID,PatientName=@PatientName,ReserveDate=@ReserveDate,DetecteDate=@DetecteDate,ReserveTime=@ReserveTime,DoctorsID=@DoctorsID,specializationID=@specializationID,ReservKind=@ReservKind,Detecte_time=@Detecte_time where ID = @ID"

            myconn.cmd = New SqlCommand(sql, myconn.conn)
            With myconn.cmd.Parameters
                .AddWithValue("@ReserveID", txtReserveID.Text)
                .AddWithValue("@PatientName", txtPatient.Text)
                .AddWithValue("@ReserveDate", Format(CDate(Today.Date), "yyyy/MM/dd"))
                .AddWithValue("@ReserveTime", CDate(Label8.Text).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@DetecteDate", Format(CDate(dtp.Text), "yyyy/MM/dd"))
                .AddWithValue("@Detecte_time", CDate(dtp_time.Text).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@DoctorsID", cboDoctor.SelectedValue)
                .AddWithValue("@specializationID", cboClinic.SelectedValue)
                .AddWithValue("@ReservKind", cboReseve.Text)
                .AddWithValue("@ID", CInt(drg.CurrentRow.Cells(10).Value))
            End With
            If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
            myconn.conn.Open()
            myconn.cmd.ExecuteNonQuery()
            myconn.conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try
    End Sub
    Private Sub frmClinics_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label7.Left = 0
        Label7.Width = Me.Width
        Timer1.Start()
        Label9.Text = Format(Today.Date, "ddd dd MMM yyyy").ToString
        btnSave.Enabled = False
        btnCancel.Enabled = False
        btnDel.Enabled = False
        btnUpdat.Enabled = False
        txtPatient.Enabled = False
        myconn.Fillcombo("select * from doctors ", "doctors", "doctorsID", "DoctorsName", Me, cboSearchDoctor.ComboBox)

        fin = False
        myconn.Fillcombo("select * from specialization", "specialization", "specializationID", "specialization", Me, cboClinic)
        fin = True
        cboReseve.Items.Add("كشف")
        cboReseve.Items.Add("اعادة")
        cboReseve.Items.Add("متابعة تبويض")
        x = 2
        Fillgrd()

    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        If cboClinic.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboClinic, "أدخل اسم القسم")
            Return
        End If
        If cboDoctor.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboDoctor, "أدخل اسم الطبيب")
            Return
        End If
        If cboReseve.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboReseve, "أدخل نوع الكشف")
            Return
        End If
        NewRecord()
        btnSave.Enabled = True
        btnCancel.Enabled = True
        cboClinic.Enabled = False
        cboDoctor.Enabled = False
        btnUpdat.Enabled = False
        cboReseve.Enabled = False
        txtPatient.Enabled = True
        dtp.Enabled = False
        x = 4
        Fillgrd()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtPatient.Text = "" Then
            ErrorProvider1.SetError(txtPatient, "أدخل نوع الكشف")
            Return
        End If
        Save_record()
        x = 4
        Fillgrd()
        myconn.ClearAllText(Me, GroupBox1)
        btnSave.Enabled = False
        btnUpdat.Enabled = True
        cboClinic.Enabled = True
        cboDoctor.Enabled = True
        btnUpdat.Enabled = True
        cboReseve.Enabled = True
        txtPatient.Enabled = False
        dtp.Enabled = True
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Update_record()
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            btnDel.Enabled = False
            Return
        Else
            myconn.DeleteRecord("Clinics", "ID", CInt(drg.CurrentRow.Cells(10).Value))
            myconn.ClearAllText(Me, GroupBox1)
            x = 0
            Fillgrd()
            btnDel.Enabled = False
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label8.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        btnDel.Enabled = True
        btnUpdat.Enabled = True
    End Sub
    Private Sub cboClinic_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClinic.SelectedIndexChanged
        ErrorProvider1.Clear()

        If Not fin Then Return
        fin = False
        myconn.Fillcombo("select * from doctors where specializationID =" & CInt(cboClinic.SelectedValue), "doctors", "doctorsID", "DoctorsName", Me, cboDoctor)
        fin = True
    End Sub
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If txtSearchDate.TextBox.Text = "" Then
            MessageBox.Show("أدخل التاريخ", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        If txtSearchDate.TextBox.Text <> "" And cboSearchDoctor.ComboBox.SelectedIndex = -1 Then
            x = 3
        Else
            x = 1
        End If
        Fillgrd()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtPatient.Text = ""
        txtReserveID.Text = ""
        dtp.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False

        cboReseve.Enabled = True
        cboClinic.Enabled = True
        cboDoctor.Enabled = True
        btnUpdat.Enabled = True

    End Sub

    Private Sub cboDoctor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDoctor.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        x = 4
        Fillgrd()

    End Sub

    Private Sub cboReseve_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboReseve.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtPatient_TextChanged(sender As Object, e As EventArgs) Handles txtPatient.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txtPatient_Enter(sender As Object, e As EventArgs) Handles txtPatient.Enter
        myconn.langAR()

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Print_drg(drg)
    End Sub
    Sub Print_drg(dgr As DataGridView)
        Dim rpt As New rpt_Clinics
        Dim table As New DataTable
        For i As Integer = 1 To drg.ColumnCount
            Dim x As String
            x = Format(i, "00")
            table.Columns.Add(x)
        Next

        For Each dr As DataGridViewRow In drg.Rows
            table.Rows.Add()
            For i As Integer = 0 To dgr.ColumnCount - 1
                table.Rows(dr.Index)(i) = dr.Cells(i).Value
            Next
        Next

        rpt.SetDataSource(table)
        rpt.SetParameterValue("Report_label", "كشوفات الدكتور " & cboDoctor.Text)
        rpt.SetParameterValue("Report_label02", "يوم  " & Format(CDate(dtp.Text), "ddd dd MMM yyyy"))
        frmReportViewer.CrystalReportViewer1.ReportSource = rpt
        frmReportViewer.Show()
    End Sub

End Class