
Imports System.Data.SqlClient
Imports System.Globalization
Public Class frmStaying_Reserve
    Dim myconn As New connect
    Dim fin As Boolean
    Dim x, y As Integer
    Dim st, st2 As String
    Dim dd As String
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        myconn.ClearAllText(Me, GroupBox4)
        myconn.Filldataset("select  isnull(max(ReserveID),0) as ReserveID from Room_reserve where Rserve_date ='" & Format(CDate(Today.Date), "yyyy/MM/dd").ToString & "'", "Room_reserve", Me)
        If myconn.dv.Count = 0 Then
            txtReserveID.Text = "1"
        Else
            txtReserveID.Text = (myconn.cur.Current("ReserveID") + 1).ToString
        End If
    End Sub
    Sub Fillgrd_room() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg_room.Rows.Clear()
        Select Case y
            Case 0
                st2 = " where '" & Format((dtp.Value.AddDays(CInt(txtPeriod.Text - 1))), "yyyy/MM/dd") & "' between cast(Stay_date as date) and dateadd(day,Stay_count-1,cast(Stay_date as date)) "
            Case 1
                st2 = " where '" & Format((dtp.Value), "yyyy/MM/dd") & "' between cast(Stay_date as date) and dateadd(day,Stay_count-1,cast(Stay_date as date)) "
            Case 3
                st2 = " where  cast(Stay_date as date)  between '" & Format((dtp.Value), "yyyy/MM/dd") & "' and '" & Format((dtp.Value.AddDays(CInt(txtPeriod.Text - 1))), "yyyy/MM/dd") & "' "

        End Select
        myconn.Filldataset("select a.RoomNumber,a.Price,(b.RoomNumber) as Room,b.Stay_date,b.Stay_count,(dateadd(day,b.Stay_count-1,cast(b.Stay_date as date))) as End_stay from [dbo].[Rooms] a
                            left join (select RoomNumber,Stay_date,Stay_count from [dbo].[Room_reserve] " & st2 & " ) b
                            on a.RoomNumber = b.RoomNumber", "Rooms", Me)
        For i As Integer = 0 To myconn.cur.Count - 1
            drg_room.Rows.Add()
            drg_room.Rows(i).Cells(0).Value = i + 1
            drg_room.Rows(i).Cells(1).Value = myconn.cur.Current("RoomNumber")
            drg_room.Rows(i).Cells(2).Value = myconn.cur.Current("Price")
            drg_room.Rows(i).Cells(3).Value = If(IsDBNull(myconn.cur.Current("Room")), "متاحة", "مشغولة من  " & Format(CDate(myconn.cur.Current("Stay_date")), "yyyy/MM/dd") & "حتى " & Format(CDate(myconn.cur.Current("End_stay")), "yyyy/MM/dd"))
            drg_room.Rows(i).Cells(4).Value = If(IsDBNull(myconn.cur.Current("Stay_date")), 0, Format(CDate(myconn.cur.Current("End_stay")), "yyyy/MM/dd"))
            If IsDBNull(myconn.cur.Current("Room")) Then
                drg_room.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
            Else
                drg_room.Rows(i).DefaultCellStyle.BackColor = Color.Red
            End If
            myconn.cur.Position += 1
        Next
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Select Case x
            Case 0 ' حجز اليوم
                st = " where a.Rserve_date ='" & Format(CDate(Today.Date), "yyyy/MM/dd") & "'"
            Case 1 ' تاريخ الحجز 
                st = " where a.Rserve_date ='" & Format(CDate(txtSearchDate.TextBox.Text), "yyyy/MM/dd") & "'"
            Case 2 ' تاريخ الاقامة
                st = " where a.Stay_date ='" & Format(CDate(txtSearchDate.TextBox.Text), "yyyy/MM/dd") & "'"
        End Select
        myconn.Filldataset("select a.ReserveID, a.RoomNumber, a.Rserve_date, a.Rserve_time, a.Stay_date,a.Stay_time, b.DoctorsName, a.Stay_count,a.Stay_Cost,a.Room_Price,a.Patient_name,a.ReserveID,a.ID  from Room_reserve a
                                   left join Doctors b on a.doctorsID = b.DoctorsID " & st, "Clinics", Me)


        For i As Integer = 0 To myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = myconn.cur.Current("ReserveID")
            drg.Rows(i).Cells(2).Value = myconn.cur.Current("Rserve_date")
            drg.Rows(i).Cells(3).Value = myconn.cur.Current("Rserve_time")
            drg.Rows(i).Cells(4).Value = myconn.cur.Current("Stay_date")
            drg.Rows(i).Cells(5).Value = myconn.cur.Current("Stay_time")
            drg.Rows(i).Cells(6).Value = myconn.cur.Current("RoomNumber")
            drg.Rows(i).Cells(7).Value = myconn.cur.Current("Room_Price")
            drg.Rows(i).Cells(8).Value = myconn.cur.Current("Stay_count")
            drg.Rows(i).Cells(9).Value = myconn.cur.Current("Patient_name")
            drg.Rows(i).Cells(10).Value = myconn.cur.Current("DoctorsName")
            drg.Rows(i).Cells(11).Value = myconn.cur.Current("ID")
            myconn.cur.Position += 1
        Next
        myconn.DataGridview_MoveLast(drg, 2)
    End Sub
    Sub Save_record()
        Try
            Dim sql As String = "INSERT INTO  Room_reserve (ReserveID,RoomNumber,Rserve_date,Rserve_time,Stay_date,Stay_time,Stay_count,Stay_Cost,Room_Price,Patient_name,DoctorsID)
                                                values (@ReserveID,@RoomNumber,@Rserve_date,@Rserve_time,@Stay_date,@Stay_time,@Stay_count,@Stay_Cost,@Room_Price,@Patient_name,@DoctorsID)"
            myconn.cmd = New SqlCommand(sql, myconn.conn)
            With myconn.cmd.Parameters
                .AddWithValue("@ReserveID", txtReserveID.Text)
                .AddWithValue("@RoomNumber", drg_room.CurrentRow.Cells(1).Value)
                .AddWithValue("@Rserve_date", Format(CDate(Today.Date), "yyyy/MM/dd"))
                .AddWithValue("@Rserve_time", CDate(Label8.Text).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Stay_date", Format(CDate(dtp.Text), "yyyy/MM/dd"))
                .AddWithValue("@Stay_time", CDate(dtp_time.Text).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Stay_count", txtPeriod.Text)
                .AddWithValue("@Stay_Cost", txtPeriod.Text * drg_room.CurrentRow.Cells(2).Value)
                .AddWithValue("@Room_Price", drg_room.CurrentRow.Cells(2).Value)
                .AddWithValue("@Patient_name", txtPatient.Text)
                .AddWithValue("@DoctorsID", cboDoctor.SelectedValue)
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

    End Sub
    Private Sub frmStaying_Reserve_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label7.Left = 0
        Label7.Width = Me.Width
        Timer1.Start()
        Label9.Text = Format(Today.Date, "ddd dd MMM yyyy").ToString
        btnSave.Enabled = False
        btnCancel.Enabled = False

        btnUpdat.Enabled = False

        myconn.Fillcombo("select * from doctors ", "doctors", "doctorsID", "DoctorsName", Me, cboDoctor)
        y = 1
        Fillgrd_room()
        x = 0
        Fillgrd()

    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        If dd <> "0" Then
            MsgBox("هذه الغرفة غير متاحة حاليا")
            Return
        End If

        btnSave.Enabled = True
        btnDel.Enabled = False
        btnUpdat.Enabled = False
        btnCancel.Enabled = True
        GroupBox3.Enabled = False

        dtp.Enabled = False
        dtp_time.Enabled = False
        txtPeriod.Enabled = True
        cboDoctor.Enabled = True
        txtPatient.Enabled = True
        NewRecord()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" Then
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

        If drg_room.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen Then
            Save_record()
            GroupBox3.Enabled = True
            y = 1
            Fillgrd_room()
            x = 0
            Fillgrd()
            btnSave.Enabled = False
            btnCancel.Enabled = False
            btnDel.Enabled = True
            btnUpdat.Enabled = True

            dtp.Enabled = True
            dtp_time.Enabled = True
            txtPeriod.Enabled = True
            cboDoctor.Enabled = False
            txtPatient.Enabled = False
        Else
            MsgBox("الغرفة غير متاحة في هذا التاريخ")
        End If

    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        btnSave.Enabled = False
        btnCancel.Enabled = False
        btnDel.Enabled = True
        btnUpdat.Enabled = True
        GroupBox3.Enabled = True

        dtp.Enabled = True
        dtp_time.Enabled = True
        txtPeriod.Enabled = True
        cboDoctor.Enabled = False
        txtPatient.Enabled = False
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Room_reserve", "ID", CInt(drg.CurrentRow.Cells(11).Value))
            drg.Rows.Remove(drg.SelectedRows(0))
            y = 1
            Fillgrd_room()
        End If

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If cboSeach.SelectedIndex = -1 Then Return
        Select Case cboSeach.SelectedIndex
            Case 0 ' تاريخ الحجز
                x = 1
                Fillgrd()
            Case 1 ' تاريخ الاقامة
                x = 2
                Fillgrd()
        End Select
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label8.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
    Private Sub drg_room_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_room.CellClick
        dd = drg_room.CurrentRow.Cells(4).Value
    End Sub
    Private Sub dtp_ValueChanged(sender As Object, e As EventArgs) Handles dtp.ValueChanged
        y = 1
        Fillgrd_room()
    End Sub
    Private Sub txtPeriod_TextChanged(sender As Object, e As EventArgs) Handles txtPeriod.TextChanged
        If txtPeriod.Text = Nothing Then Return
        y = 3
        Fillgrd_room()
    End Sub

    Private Sub cboDoctor_Enter(sender As Object, e As EventArgs) Handles cboDoctor.Enter
        myconn.langAR()
    End Sub
    Private Sub txtPatient_Enter(sender As Object, e As EventArgs) Handles txtPatient.Enter
        myconn.langAR()
    End Sub
End Class