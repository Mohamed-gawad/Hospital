Imports System.Data.SqlClient
Imports System.Globalization
Public Class frmOperatin_Reserve
    Dim myconn As New connect
    Dim fin As Boolean
    Dim x As Integer
    Dim st As String
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        myconn.ClearAllText(Me, GroupBox1)
        myconn.Filldataset("select  isnull(max(ReserveID),0) as ReserveID from Operation_Reserve where DetecteDate ='" & Format(CDate(dtp.Text), "yyyy/MM/dd").ToString & "' And Operation_Tool_ID = " & CInt(cboRoom.SelectedValue), "Operation_Reserve", Me)
        If myconn.dv.Count = 0 Then
            txtReserveID.Text = "1"
        Else
            txtReserveID.Text = (myconn.cur.Current("ReserveID") + 1).ToString
        End If
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Select Case x
            Case 0
                st = " where a.DetecteDate ='" & Format(CDate(dtp.Text), "yyyy/MM/dd") & "'"
            Case 1
                st = " where a.DetecteDate ='" & Format(CDate(txtSearchDate.TextBox.Text), "yyyy/MM/dd") & "' AND a.DoctorsID =" & CInt(cboSearchDoctor.ComboBox.SelectedValue)
            Case 2
                st = " where a.ReserveDate ='" & Format(CDate(Today.Date), "yyyy/MM/dd") & "'"
            Case 3
                st = " where a.DetecteDate ='" & Format(CDate(txtSearchDate.TextBox.Text), "yyyy/MM/dd") & "'"
            Case 4
                st = " where a.DetecteDate ='" & Format(CDate(dtp.Text), "yyyy/MM/dd") & "' AND a.Operation_Tool_ID =" & CInt(cboRoom.SelectedValue)
            Case 5
                st = " where a.ReserveDate ='" & Format(CDate(txtSearchDate.TextBox.Text), "yyyy/MM/dd") & "'"
            Case 6
                st = " where a.ReserveDate ='" & Format(CDate(txtSearchDate.TextBox.Text), "yyyy/MM/dd") & "' AND a.DoctorsID =" & CInt(cboSearchDoctor.ComboBox.SelectedValue)
            Case 7
                st = " where a.ReserveDate ='" & Format(CDate(dtp.Text), "yyyy/MM/dd") & "' AND a.Operation_Tool_ID =" & CInt(cboRoom.SelectedValue)
            Case 8
                st = " where a.ReserveDate ='" & Format(CDate(Today.Date), "yyyy/MM/dd") & "' AND a.Operation_Tool_ID =" & CInt(cboRoom.SelectedValue)

        End Select

        myconn.Filldataset("Select a.ReserveID, a.ReserveDate, a.ReserveTime,T.Operation_Tool_Name, a.DetecteDate, a.DetecteTime, a.PatientName, b.DoctorsName, c.CerviceName, a.ID  from Operation_Reserve a
                                  Left join Doctors b On a.doctorsID = b.DoctorsID 
                                  Left join Opreation_Tools T On a.Operation_Tool_ID = T.Operation_Tool_ID
                                  Left Join Cervices c On a.CerviceID = c.CerviceID" & st, "Operation_Reserve", Me)

        For i As Integer = 0 To myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = myconn.cur.Current("ReserveID")
            drg.Rows(i).Cells(2).Value = myconn.cur.Current("ReserveDate")
            drg.Rows(i).Cells(3).Value = CDate(myconn.cur.Current("ReserveTime")).ToString("hh: mm tt", CultureInfo.CreateSpecificCulture("ar-eg"))
            drg.Rows(i).Cells(4).Value = myconn.cur.Current("DetecteDate")
            drg.Rows(i).Cells(5).Value = CDate(myconn.cur.Current("DetecteTime")).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg"))
            drg.Rows(i).Cells(6).Value = myconn.cur.Current("PatientName")
            drg.Rows(i).Cells(7).Value = myconn.cur.Current("DoctorsName")
            drg.Rows(i).Cells(8).Value = myconn.cur.Current("CerviceName")
            drg.Rows(i).Cells(9).Value = myconn.cur.Current("ID")
            drg.Rows(i).Cells(10).Value = myconn.cur.Current("Operation_Tool_Name")
            myconn.cur.Position += 1
        Next
        myconn.DataGridview_MoveLast(drg, 2)
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        myconn.Filldataset("Select a.ReserveID, a.ReserveDate, a.ReserveTime,a.Operation_Tool_ID, a.DetecteDate, a.DetecteTime,a.specializationID, a.PatientName, b.DoctorsName, c.CerviceName, a.ID  from Operation_Reserve a
                                  Left join Doctors b On a.doctorsID = b.DoctorsID 
                                  Left join Opreation_Tools T On a.Operation_Tool_ID = T.Operation_Tool_ID
                                  Left Join Cervices c On a.CerviceID = c.CerviceID where a.ID = " & CInt(drg.CurrentRow.Cells(9).Value), "Operation_Reserve", Me)

        dtp.Value = Format(CDate(myconn.cur.Current("DetecteDate")), "yyyy/MM/dd")
        dtp_time.Text = CDate(myconn.cur.Current("DetecteTime")).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        cboClinic.SelectedValue = myconn.cur.Current("specializationID")
        txtPatient.Text = myconn.cur.Current("PatientName")
        txtReserveID.Text = myconn.cur.Current("ReserveID")
        cboRoom.SelectedValue = myconn.cur.Current("Operation_Tool_ID")
    End Sub
    Sub Save_record()
        Try
            Dim sql As String = "INSERT INTO  Operation_Reserve (ReserveID,PatientName,ReserveDate,ReserveTime,DetecteDate,DetecteTime,DoctorsID,specializationID,CerviceID,Operation_Tool_ID)
                                                        values (@ReserveID,@PatientName,@ReserveDate,@ReserveTime,@DetecteDate,@DetecteTime,@DoctorsID,@specializationID,@CerviceID,@Operation_Tool_ID)"
            myconn.cmd = New SqlCommand(sql, myconn.conn)
            With myconn.cmd.Parameters
                .AddWithValue("@ReserveID", txtReserveID.Text)
                .AddWithValue("@PatientName", txtPatient.Text)
                .AddWithValue("@ReserveDate", Format(CDate(Today.Date), "yyyy/MM/dd"))
                .AddWithValue("@ReserveTime", CDate(Label8.Text).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@DetecteDate", Format(CDate(dtp.Text), "yyyy/MM/dd"))
                .AddWithValue("@DetecteTime", CDate(dtp_time.Text).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@DoctorsID", cboDoctor.SelectedValue)
                .AddWithValue("@specializationID", cboClinic.SelectedValue)
                .AddWithValue("@CerviceID", cboReseve.SelectedValue)
                .AddWithValue("@Operation_Tool_ID", cboRoom.SelectedValue)
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
            Dim sql As String = "Update  Operation_Reserve set ReserveID=@ReserveID,PatientName=@PatientName,ReserveDate=@ReserveDate,DetecteDate=@DetecteDate,ReserveTime=@ReserveTime,DoctorsID=@DoctorsID,specializationID=@specializationID,CerviceID=@CerviceID,DetecteTime=@DetecteTime,Operation_Tool_ID=@Operation_Tool_ID where ID = @ID"
            myconn.cmd = New SqlCommand(sql, myconn.conn)
            With myconn.cmd.Parameters
                .AddWithValue("@ReserveID", txtReserveID.Text)
                .AddWithValue("@PatientName", txtPatient.Text)
                .AddWithValue("@ReserveDate", Format(CDate(Today.Date), "yyyy/MM/dd"))
                .AddWithValue("@ReserveTime", CDate(Label8.Text).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@DetecteDate", Format(CDate(dtp.Text), "yyyy/MM/dd"))
                .AddWithValue("@DetecteTime", CDate(dtp_time.Text).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@DoctorsID", cboDoctor.SelectedValue)
                .AddWithValue("@specializationID", cboClinic.SelectedValue)
                .AddWithValue("@CerviceID", cboReseve.Text)
                .AddWithValue("@Operation_Tool_ID", cboRoom.SelectedValue)
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
    Private Sub frmOperatin_Reserve_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label7.Left = 0
        Label7.Width = Me.Width
        Timer1.Start()
        Label9.Text = Format(Today.Date, "ddd dd MMM yyyy").ToString
        btnSave.Enabled = False
        btnCancel.Enabled = False
        btnDel.Enabled = False
        btnUpdat.Enabled = False
        txtPatient.Enabled = False
        myconn.Fillcombo5("select * from doctors ", "doctors", "doctorsID", "DoctorsName", Me, cboSearchDoctor.ComboBox)
        fin = False
        myconn.Fillcombo2("select * from specialization", "specialization", "specializationID", "specialization", Me, cboClinic)
        myconn.Fillcombo2("select * from Opreation_Tools", "Opreation_Tools", "Operation_Tool_ID", "Operation_Tool_Name", Me, cboRoom)
        fin = True
        x = 2
        Fillgrd()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        If cboClinic.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboClinic, "أدخل إسم القسم")
            Return
        End If
        If cboDoctor.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboDoctor, "أدخل إسم الطبيب")
            Return
        End If
        If cboReseve.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboReseve, "أدخل إسم العملية")
            Return
        End If
        If cboRoom.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cboRoom, "أدخل إسم الغرفة")
            Return
        End If
        NewRecord()
        btnSave.Enabled = True
        txtPatient.Enabled = True
        btnCancel.Enabled = True

        cboClinic.Enabled = False
        cboDoctor.Enabled = False
        cboRoom.Enabled = False
        cboReseve.Enabled = False
        dtp.Enabled = False
        dtp_time.Enabled = False
        btnUpdat.Enabled = False

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtReserveID.Text = "" Then
            ErrorProvider1.SetError(txtReserveID, "أدخل رقم الحجز")
            Return
        End If
        If txtPatient.Text = "" Then
            ErrorProvider1.SetError(txtPatient, "أدخل اسم المريض")
            Return
        End If
        Save_record()
        x = 4
        Fillgrd()
        myconn.ClearAllText(Me, GroupBox1)
        btnSave.Enabled = False
        btnCancel.Enabled = False
        txtPatient.Enabled = False

        cboClinic.Enabled = True
        cboDoctor.Enabled = True
        cboRoom.Enabled = True
        cboReseve.Enabled = True
        dtp.Enabled = True
        dtp_time.Enabled = True
        btnUpdat.Enabled = True
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Update_record()
        x = 0
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            btnDel.Enabled = False
            Return
        Else
            myconn.DeleteRecord("Operation_Reserve", "ID", CInt(drg.CurrentRow.Cells(9).Value))
            myconn.ClearAllText(Me, GroupBox1)
            x = 0
            Fillgrd()
            btnDel.Enabled = False
        End If
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick

        btnDel.Enabled = True
        btnUpdat.Enabled = True
        Binding()

    End Sub
    Private Sub cboClinic_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboClinic.SelectedIndexChanged
        ErrorProvider1.Clear()

        If Not fin Then Return
        myconn.Fillcombo3("select * from doctors where specializationID =" & CInt(cboClinic.SelectedValue), "doctors", "doctorsID", "DoctorsName", Me, cboDoctor)
        myconn.Fillcombo4("select * from Cervices where specializationID =" & CInt(cboClinic.SelectedValue), "Cervices", "CerviceID", "CerviceName", Me, cboReseve)
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txtSearchDate.TextBox.Text = "" Then
            MessageBox.Show("أدخل التاريخ", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        Select Case cbo_date.SelectedIndex
            Case 0 ' تاريخ العملية
                If txtSearchDate.TextBox.Text <> "" And cboSearchDoctor.ComboBox.SelectedIndex = -1 Then
                    x = 3
                    Fillgrd()
                Else
                    x = 1
                    Fillgrd()
                End If
            Case 1 ' تاريخ الفحص
                If txtSearchDate.TextBox.Text <> "" And cboSearchDoctor.ComboBox.SelectedIndex = -1 Then
                    x = 5
                    Fillgrd()
                Else
                    x = 6
                    Fillgrd()
                End If
        End Select
        btnSave.Enabled = False
        txtPatient.Enabled = False
        btnCancel.Enabled = False

        cboClinic.Enabled = True
        cboDoctor.Enabled = True
        cboRoom.Enabled = True
        cboReseve.Enabled = True
        dtp.Enabled = True
        dtp_time.Enabled = True
        btnUpdat.Enabled = True

    End Sub
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtPatient.Text = ""
        txtReserveID.Text = ""
        dtp.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
    End Sub
    Private Sub cboDoctor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDoctor.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtPatient_TextChanged(sender As Object, e As EventArgs) Handles txtPatient.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtTime_Reserve_TextChanged(sender As Object, e As EventArgs)
        ErrorProvider1.Clear()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label8.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
    Private Sub txtPatient_Enter(sender As Object, e As EventArgs) Handles txtPatient.Enter
        myconn.langAR()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If drg.Rows.Count = 0 Then Return

        Print_drg(drg)
    End Sub
    Sub Print_drg(dgr As DataGridView)
        Dim rpt As New rpt_Operation_reserve
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
        rpt.SetParameterValue("Report_label", "حجز العمليات ")
        rpt.SetParameterValue("Report_label02", "يوم  " & Format(CDate(drg.Rows(0).Cells(4).Value), "ddd dd MMM yyyy"))
        frmReportViewer.CrystalReportViewer1.ReportSource = rpt
        frmReportViewer.Show()
    End Sub

    Private Sub cboRoom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRoom.SelectedIndexChanged
        ErrorProvider1.Clear()

        If Not fin Then Return
        x = 4
        Fillgrd()

    End Sub
End Class
