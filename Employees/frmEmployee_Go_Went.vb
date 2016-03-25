
Imports System.Data.SqlClient
Imports System.Globalization
Public Class frmEmployee_Go_Went

    Dim Myconn As New connect
    Dim fin As Boolean
    Dim x As Integer
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub New(q As Integer, d As String)

        ' This call is required by the designer.
        InitializeComponent()
        Me.MdiParent = Main
        GroupBox1.Visible = False
        GroupBox3.Visible = False
        btnDel.Enabled = False
        btnNew.Enabled = False
        btnSave.Enabled = False
        btnSearch.Enabled = False
        btnUpdat.Enabled = False
        txtSearch.Enabled = False
        GroupBox2.Top = GroupBox1.Top
        Me.Height = Me.Height - (GroupBox1.Height + 10)



        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.ClearAllText(Me, GroupBox1)
        Myconn.ClearAllText(Me, GroupBox2)

        Myconn.Filldataset("select Day_Date from Employees_In_Out  where Day_Date = '" & Format(CDate(dtp_begin.Text), "yyyy/MM/dd") & "'  order by Day_Date", "Employees_In_Out", Me)
        If Myconn.cur.Count > 0 Then
            MsgBox("هذا اليوم سبق تسجيله من قبل")
            Return
        End If

        drg.Rows.Clear()
        Myconn.Filldataset("select e.EmployeeName,j.jobname,s.Employee_Salary,s.Work_hours,* from Employees_Salary a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where a.State_ID = 1 order by a.EmployeeID", "Employees_Salary", Me)

        If Myconn.cur.Count = 0 Then Return
        For i As Integer = 0 To Myconn.cur.Count - 1
            Try
                Dim sql As String = "INSERT INTO Employees_In_Out(EmployeeID,Day_Date,Clock_in,Date_in,Clock_out,Date_out,Work_Minute,Late,Fingerprint,Holiday_ID) 
                                                           VALUES(@EmployeeID,@Day_Date,@Clock_in,@Date_in,@Clock_out,@Date_out,@Work_Minute,@Late,@Fingerprint,@Holiday_ID)"
                Myconn.cmd = New SqlCommand(sql, Myconn.conn)
                With Myconn.cmd.Parameters
                    .AddWithValue("@EmployeeID", Myconn.cur.Current("EmployeeID"))
                    .AddWithValue("@Day_Date", Format(CDate(dtp_begin.Text), "yyyy/MM/dd"))
                    .AddWithValue("@Clock_in", DBNull.Value)
                    .AddWithValue("@Date_in", DBNull.Value)
                    .AddWithValue("@Clock_out", DBNull.Value)
                    .AddWithValue("@Date_out", DBNull.Value)
                    .AddWithValue("@Work_Minute", DBNull.Value)
                    .AddWithValue("@Late", DBNull.Value)
                    .AddWithValue("@Fingerprint", DBNull.Value)
                    .AddWithValue("@Holiday_ID", DBNull.Value)

                End With
                If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
                Myconn.conn.Open()
                Myconn.cmd.ExecuteNonQuery()
                Myconn.conn.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
                Return
            End Try
            Myconn.cur.Position += 1
        Next
        x = 5
        Fillgrd()

    End Sub
    Sub Fillgrd() ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Try


            Select Case x
                Case 0
                    drg.Rows.Clear()
                    Myconn.Filldataset("select h.Holiday_name,e.EmployeeName,j.jobname,s.Employee_Salary,s.Work_hours,a.EmployeeID,s.Work_hours,
                            a.Day_Date,a.Clock_in,a.Date_in,a.Clock_out,a.Date_out,a.Fingerprint,a.Late,s.Shift_Begin,s.Shift_End,a.Holiday_ID,
                            a.Work_Minute,a.ID from Employees_In_Out a
                           left join Holidays h on a.Holiday_ID = h.Holiday_ID
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID  order by a.EmployeeID", "Employees_In_Out", Me)


                    If Myconn.cur.Count = 0 Then Return
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        drg.Rows.Add()
                        drg.Rows(i).Cells(0).Value = i + 1
                        drg.Rows(i).Cells(1).Value = CDate(Myconn.cur.Current("Day_Date")).ToString("dddd", CultureInfo.CreateSpecificCulture("ar-eg"))
                        drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Day_Date")
                        drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeName")
                        drg.Rows(i).Cells(4).Value = Myconn.cur.Current("EmployeeID")
                        drg.Rows(i).Cells(5).Value = Myconn.cur.Current("jobname")
                        drg.Rows(i).Cells(6).Value = If(IsDBNull(Myconn.cur.Current("Shift_Begin")), DBNull.Value, Myconn.cur.Current("Shift_Begin"))
                        drg.Rows(i).Cells(7).Value = If(IsDBNull(Myconn.cur.Current("Shift_End")), DBNull.Value, Myconn.cur.Current("Shift_End"))
                        drg.Rows(i).Cells(8).Value = If(IsDBNull(Myconn.cur.Current("Work_hours")), DBNull.Value, Myconn.cur.Current("Work_hours"))
                        drg.Rows(i).Cells(9).Value = If(IsDBNull(Myconn.cur.Current("Clock_in")), DBNull.Value, CDate(Myconn.cur.Current("Clock_in")).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                        drg.Rows(i).Cells(10).Value = If(IsDBNull(Myconn.cur.Current("Date_in")), DBNull.Value, Myconn.cur.Current("Date_in"))
                        drg.Rows(i).Cells(11).Value = If(IsDBNull(Myconn.cur.Current("Clock_out")), DBNull.Value, Myconn.cur.Current("Clock_out"))
                        drg.Rows(i).Cells(12).Value = If(IsDBNull(Myconn.cur.Current("Date_out")), DBNull.Value, Myconn.cur.Current("Date_out"))
                        drg.Rows(i).Cells(13).Value = If(IsDBNull(Myconn.cur.Current("Late")), DBNull.Value, Myconn.cur.Current("Late"))
                        drg.Rows(i).Cells(14).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_name"))
                        drg.Rows(i).Cells(15).Value = Myconn.cur.Current("ID")
                        drg.Rows(i).Cells(16).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_ID"))

                        Myconn.cur.Position += 1
                    Next

                Case 1
                    Myconn.Filldataset("select h.Holiday_name,e.EmployeeName,j.jobname,s.Employee_Salary,s.Work_hours,a.EmployeeID,s.Work_hours,
                            a.Day_Date,a.Clock_in,a.Date_in,a.Clock_out,a.Date_out,a.Fingerprint,a.Late,s.Shift_Begin,s.Shift_End,a.Holiday_ID,
                            a.Work_Minute,a.ID from Employees_In_Out a
                           left join Holidays h on a.Holiday_ID = h.Holiday_ID
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where a.ID =" & CInt(drg.CurrentRow.Cells(15).Value), "Employees_In_Out", Me)

                    drg.CurrentRow.Cells(1).Value = CDate(Myconn.cur.Current("Day_Date")).ToString("dddd", CultureInfo.CreateSpecificCulture("ar-eg"))
                    drg.CurrentRow.Cells(2).Value = Myconn.cur.Current("Day_Date")
                    drg.CurrentRow.Cells(3).Value = Myconn.cur.Current("EmployeeName")
                    drg.CurrentRow.Cells(4).Value = Myconn.cur.Current("EmployeeID")
                    drg.CurrentRow.Cells(5).Value = Myconn.cur.Current("jobname")
                    drg.CurrentRow.Cells(6).Value = If(IsDBNull(Myconn.cur.Current("Shift_Begin")), DBNull.Value, Myconn.cur.Current("Shift_Begin"))
                    drg.CurrentRow.Cells(7).Value = If(IsDBNull(Myconn.cur.Current("Shift_End")), DBNull.Value, Myconn.cur.Current("Shift_End"))
                    drg.CurrentRow.Cells(8).Value = If(IsDBNull(Myconn.cur.Current("Work_hours")), DBNull.Value, Myconn.cur.Current("Work_hours"))
                    drg.CurrentRow.Cells(9).Value = If(IsDBNull(Myconn.cur.Current("Clock_in")), DBNull.Value, CDate(Myconn.cur.Current("Clock_in")).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                    drg.CurrentRow.Cells(10).Value = If(IsDBNull(Myconn.cur.Current("Date_in")), DBNull.Value, Myconn.cur.Current("Date_in"))
                    drg.CurrentRow.Cells(11).Value = If(IsDBNull(Myconn.cur.Current("Clock_out")), DBNull.Value, Myconn.cur.Current("Clock_out"))
                    drg.CurrentRow.Cells(12).Value = If(IsDBNull(Myconn.cur.Current("Date_out")), DBNull.Value, Myconn.cur.Current("Date_out"))
                    drg.CurrentRow.Cells(13).Value = If(IsDBNull(Myconn.cur.Current("Late")), DBNull.Value, Myconn.cur.Current("Late"))
                    drg.CurrentRow.Cells(14).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_name"))
                    drg.CurrentRow.Cells(15).Value = Myconn.cur.Current("ID")
                    drg.CurrentRow.Cells(16).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_ID"))

                Case 3
                    drg.Rows.Clear()
                    Myconn.Filldataset("select h.Holiday_name,e.EmployeeName,j.jobname,s.Employee_Salary,s.Work_hours,a.EmployeeID,s.Work_hours,
                            a.Day_Date,a.Clock_in,a.Date_in,a.Clock_out,a.Date_out,a.Fingerprint,a.Late,s.Shift_Begin,s.Shift_End,a.Holiday_ID,
                            a.Work_Minute,a.ID from Employees_In_Out a
                           left join Holidays h on a.Holiday_ID = h.Holiday_ID
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where Day_Date = '" & Format(CDate(txtSearch.Text), "yyyy/MM/dd") & "'  order by a.EmployeeID", "Employees_In_Out", Me)

                    If Myconn.cur.Count = 0 Then Return
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        drg.Rows.Add()
                        drg.Rows(i).Cells(0).Value = i + 1
                        drg.Rows(i).Cells(1).Value = CDate(Myconn.cur.Current("Day_Date")).ToString("dddd", CultureInfo.CreateSpecificCulture("ar-eg"))
                        drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Day_Date")
                        drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeName")
                        drg.Rows(i).Cells(4).Value = Myconn.cur.Current("EmployeeID")
                        drg.Rows(i).Cells(5).Value = Myconn.cur.Current("jobname")
                        drg.Rows(i).Cells(6).Value = If(IsDBNull(Myconn.cur.Current("Shift_Begin")), DBNull.Value, Myconn.cur.Current("Shift_Begin"))
                        drg.Rows(i).Cells(7).Value = If(IsDBNull(Myconn.cur.Current("Shift_End")), DBNull.Value, Myconn.cur.Current("Shift_End"))
                        drg.Rows(i).Cells(8).Value = If(IsDBNull(Myconn.cur.Current("Work_hours")), DBNull.Value, Myconn.cur.Current("Work_hours"))
                        drg.Rows(i).Cells(9).Value = If(IsDBNull(Myconn.cur.Current("Clock_in")), DBNull.Value, CDate(Myconn.cur.Current("Clock_in")).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                        drg.Rows(i).Cells(10).Value = If(IsDBNull(Myconn.cur.Current("Date_in")), DBNull.Value, Myconn.cur.Current("Date_in"))
                        drg.Rows(i).Cells(11).Value = If(IsDBNull(Myconn.cur.Current("Clock_out")), DBNull.Value, Myconn.cur.Current("Clock_out"))
                        drg.Rows(i).Cells(12).Value = If(IsDBNull(Myconn.cur.Current("Date_out")), DBNull.Value, Myconn.cur.Current("Date_out"))
                        drg.Rows(i).Cells(13).Value = If(IsDBNull(Myconn.cur.Current("Late")), DBNull.Value, Myconn.cur.Current("Late"))
                        drg.Rows(i).Cells(14).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_name"))
                        drg.Rows(i).Cells(15).Value = Myconn.cur.Current("ID")
                        drg.Rows(i).Cells(16).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_ID"))

                        Myconn.cur.Position += 1
                    Next
                Case 4
                    drg.Rows.Clear()
                    Myconn.Filldataset("select h.Holiday_name,e.EmployeeName,j.jobname,s.Employee_Salary,s.Work_hours,a.EmployeeID,s.Work_hours,
                            a.Day_Date,a.Clock_in,a.Date_in,a.Clock_out,a.Date_out,a.Fingerprint,a.Late,s.Shift_Begin,s.Shift_End,a.Holiday_ID,
                            a.Work_Minute,a.ID from Employees_In_Out a
                           left join Holidays h on a.Holiday_ID = h.Holiday_ID
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where Day_Date = '" & Format(CDate(Today.Date), "yyyy/MM/dd") & "'  order by a.EmployeeID", "Employees_In_Out", Me)

                    If Myconn.cur.Count = 0 Then Return
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        drg.Rows.Add()
                        drg.Rows(i).Cells(0).Value = i + 1
                        drg.Rows(i).Cells(1).Value = CDate(Myconn.cur.Current("Day_Date")).ToString("dddd", CultureInfo.CreateSpecificCulture("ar-eg"))
                        drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Day_Date")
                        drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeName")
                        drg.Rows(i).Cells(4).Value = Myconn.cur.Current("EmployeeID")
                        drg.Rows(i).Cells(5).Value = Myconn.cur.Current("jobname")
                        drg.Rows(i).Cells(6).Value = If(IsDBNull(Myconn.cur.Current("Shift_Begin")), DBNull.Value, Myconn.cur.Current("Shift_Begin"))
                        drg.Rows(i).Cells(7).Value = If(IsDBNull(Myconn.cur.Current("Shift_End")), DBNull.Value, Myconn.cur.Current("Shift_End"))
                        drg.Rows(i).Cells(8).Value = If(IsDBNull(Myconn.cur.Current("Work_hours")), DBNull.Value, Myconn.cur.Current("Work_hours"))
                        drg.Rows(i).Cells(9).Value = If(IsDBNull(Myconn.cur.Current("Clock_in")), DBNull.Value, CDate(Myconn.cur.Current("Clock_in")).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                        drg.Rows(i).Cells(10).Value = If(IsDBNull(Myconn.cur.Current("Date_in")), DBNull.Value, Myconn.cur.Current("Date_in"))
                        drg.Rows(i).Cells(11).Value = If(IsDBNull(Myconn.cur.Current("Clock_out")), DBNull.Value, Myconn.cur.Current("Clock_out"))
                        drg.Rows(i).Cells(12).Value = If(IsDBNull(Myconn.cur.Current("Date_out")), DBNull.Value, Myconn.cur.Current("Date_out"))
                        drg.Rows(i).Cells(13).Value = If(IsDBNull(Myconn.cur.Current("Late")), DBNull.Value, Myconn.cur.Current("Late"))
                        drg.Rows(i).Cells(14).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_name"))
                        drg.Rows(i).Cells(15).Value = Myconn.cur.Current("ID")
                        drg.Rows(i).Cells(16).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_ID"))

                        Myconn.cur.Position += 1
                    Next
                Case 5
                    drg.Rows.Clear()
                    Myconn.Filldataset("select h.Holiday_name,e.EmployeeName,j.jobname,s.Employee_Salary,s.Work_hours,a.EmployeeID,s.Work_hours,
                            a.Day_Date,a.Clock_in,a.Date_in,a.Clock_out,a.Date_out,a.Fingerprint,a.Late,s.Shift_Begin,s.Shift_End,a.Holiday_ID,
                            a.Work_Minute,a.ID from Employees_In_Out a
                           left join Holidays h on a.Holiday_ID = h.Holiday_ID
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where Day_Date = '" & Format(CDate(dtp_begin.Text), "yyyy/MM/dd") & "'  order by a.EmployeeID", "Employees_In_Out", Me)

                    If Myconn.cur.Count = 0 Then Return
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        drg.Rows.Add()
                        drg.Rows(i).Cells(0).Value = i + 1
                        drg.Rows(i).Cells(1).Value = CDate(Myconn.cur.Current("Day_Date")).ToString("dddd", CultureInfo.CreateSpecificCulture("ar-eg"))
                        drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Day_Date")
                        drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeName")
                        drg.Rows(i).Cells(4).Value = Myconn.cur.Current("EmployeeID")
                        drg.Rows(i).Cells(5).Value = Myconn.cur.Current("jobname")
                        drg.Rows(i).Cells(6).Value = If(IsDBNull(Myconn.cur.Current("Shift_Begin")), DBNull.Value, Myconn.cur.Current("Shift_Begin"))
                        drg.Rows(i).Cells(7).Value = If(IsDBNull(Myconn.cur.Current("Shift_End")), DBNull.Value, Myconn.cur.Current("Shift_End"))
                        drg.Rows(i).Cells(8).Value = If(IsDBNull(Myconn.cur.Current("Work_hours")), DBNull.Value, Myconn.cur.Current("Work_hours"))
                        drg.Rows(i).Cells(9).Value = If(IsDBNull(Myconn.cur.Current("Clock_in")), DBNull.Value, CDate(Myconn.cur.Current("Clock_in")).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                        drg.Rows(i).Cells(10).Value = If(IsDBNull(Myconn.cur.Current("Date_in")), DBNull.Value, Myconn.cur.Current("Date_in"))
                        drg.Rows(i).Cells(11).Value = If(IsDBNull(Myconn.cur.Current("Clock_out")), DBNull.Value, Myconn.cur.Current("Clock_out"))
                        drg.Rows(i).Cells(12).Value = If(IsDBNull(Myconn.cur.Current("Date_out")), DBNull.Value, Myconn.cur.Current("Date_out"))
                        drg.Rows(i).Cells(13).Value = If(IsDBNull(Myconn.cur.Current("Late")), DBNull.Value, Myconn.cur.Current("Late"))
                        drg.Rows(i).Cells(14).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_name"))
                        drg.Rows(i).Cells(15).Value = Myconn.cur.Current("ID")
                        drg.Rows(i).Cells(16).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_ID"))
                        Myconn.cur.Position += 1
                    Next
            End Select
            For x As Integer = 0 To drg.Rows.Count - 1
                Dim D As Integer = If(IsDBNull(drg.Rows(x).Cells(16).Value), 0, Val(drg.Rows(x).Cells(16).Value))
                If D = 6 Then
                    drg.Rows(x).DefaultCellStyle.BackColor = Color.Red
                ElseIf IsDBNull(drg.Rows(x).Cells(9).Value) And IsDBNull(drg.Rows(x).Cells(11).Value) Then
                    drg.Rows(x).DefaultCellStyle.BackColor = Color.LemonChiffon
                ElseIf Not IsDBNull(drg.Rows(x).Cells(9).Value) And Not IsDBNull(drg.Rows(x).Cells(11).Value)
                    drg.Rows(x).DefaultCellStyle.BackColor = Color.LightGreen
                ElseIf Not IsDBNull(drg.Rows(x).Cells(9).Value) And IsDBNull(drg.Rows(x).Cells(11).Value)
                    drg.Rows(x).DefaultCellStyle.BackColor = Color.Pink

                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Sub fillgrd2()
        drg.Rows.Clear()
        Myconn.Filldataset("select h.Holiday_name,e.EmployeeName,j.jobname,s.Employee_Salary,s.Work_hours,a.EmployeeID,s.Work_hours,
                            a.Day_Date,a.Clock_in,a.Date_in,a.Clock_out,a.Date_out,a.Fingerprint,a.Late,s.Shift_Begin,s.Shift_End,a.Holiday_ID,
                            a.Work_Minute,a.ID from Employees_In_Out a
                           left join Holidays h on a.Holiday_ID = h.Holiday_ID
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID 
                           where  cast(DATEPART(yyyy,a.Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Day_Date),'00') as varchar(2)) = '" & Format(CDate(frmEmployees_Report_Salary.Dat), "yyyy/MM") & "' and a.EmployeeID = " & frmEmployees_Report_Salary.EmpID & " and a.Holiday_ID = 7 order by a.Day_Date", "Employees_In_Out", Me)

        If Myconn.cur.Count = 0 Then Return
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = CDate(Myconn.cur.Current("Day_Date")).ToString("dddd", CultureInfo.CreateSpecificCulture("ar-eg"))
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Day_Date")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("EmployeeID")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("jobname")
            drg.Rows(i).Cells(6).Value = If(IsDBNull(Myconn.cur.Current("Shift_Begin")), DBNull.Value, Myconn.cur.Current("Shift_Begin"))
            drg.Rows(i).Cells(7).Value = If(IsDBNull(Myconn.cur.Current("Shift_End")), DBNull.Value, Myconn.cur.Current("Shift_End"))
            drg.Rows(i).Cells(8).Value = If(IsDBNull(Myconn.cur.Current("Work_hours")), DBNull.Value, Myconn.cur.Current("Work_hours"))
            drg.Rows(i).Cells(9).Value = If(IsDBNull(Myconn.cur.Current("Clock_in")), DBNull.Value, CDate(Myconn.cur.Current("Clock_in")).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
            drg.Rows(i).Cells(10).Value = If(IsDBNull(Myconn.cur.Current("Date_in")), DBNull.Value, Myconn.cur.Current("Date_in"))
            drg.Rows(i).Cells(11).Value = If(IsDBNull(Myconn.cur.Current("Clock_out")), DBNull.Value, Myconn.cur.Current("Clock_out"))
            drg.Rows(i).Cells(12).Value = If(IsDBNull(Myconn.cur.Current("Date_out")), DBNull.Value, Myconn.cur.Current("Date_out"))
            drg.Rows(i).Cells(13).Value = If(IsDBNull(Myconn.cur.Current("Late")), DBNull.Value, Myconn.cur.Current("Late"))
            drg.Rows(i).Cells(14).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_name"))
            drg.Rows(i).Cells(15).Value = Myconn.cur.Current("ID")
            drg.Rows(i).Cells(16).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_ID"))

            Myconn.cur.Position += 1
        Next
    End Sub
    Sub fillgrd3()
        drg.Rows.Clear()
        Myconn.Filldataset("select h.Holiday_name,e.EmployeeName,j.jobname,s.Employee_Salary,s.Work_hours,a.EmployeeID,s.Work_hours,
                            a.Day_Date,a.Clock_in,a.Date_in,a.Clock_out,a.Date_out,a.Fingerprint,a.Late,s.Shift_Begin,s.Shift_End,a.Holiday_ID,
                            a.Work_Minute,a.ID from Employees_In_Out a
                           left join Holidays h on a.Holiday_ID = h.Holiday_ID
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID 
                           where  cast(DATEPART(yyyy,a.Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Day_Date),'00') as varchar(2)) = '" & Format(CDate(frmEmployees_Report_Salary.Dat), "yyyy/MM") & "' and a.EmployeeID = " & frmEmployees_Report_Salary.EmpID & " and a.Holiday_ID = 6 order by a.Day_Date", "Employees_In_Out", Me)

        If Myconn.cur.Count = 0 Then Return
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = CDate(Myconn.cur.Current("Day_Date")).ToString("dddd", CultureInfo.CreateSpecificCulture("ar-eg"))
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Day_Date")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("EmployeeID")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("jobname")
            drg.Rows(i).Cells(6).Value = If(IsDBNull(Myconn.cur.Current("Shift_Begin")), DBNull.Value, Myconn.cur.Current("Shift_Begin"))
            drg.Rows(i).Cells(7).Value = If(IsDBNull(Myconn.cur.Current("Shift_End")), DBNull.Value, Myconn.cur.Current("Shift_End"))
            drg.Rows(i).Cells(8).Value = If(IsDBNull(Myconn.cur.Current("Work_hours")), DBNull.Value, Myconn.cur.Current("Work_hours"))
            drg.Rows(i).Cells(9).Value = If(IsDBNull(Myconn.cur.Current("Clock_in")), DBNull.Value, CDate(Myconn.cur.Current("Clock_in")).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
            drg.Rows(i).Cells(10).Value = If(IsDBNull(Myconn.cur.Current("Date_in")), DBNull.Value, Myconn.cur.Current("Date_in"))
            drg.Rows(i).Cells(11).Value = If(IsDBNull(Myconn.cur.Current("Clock_out")), DBNull.Value, Myconn.cur.Current("Clock_out"))
            drg.Rows(i).Cells(12).Value = If(IsDBNull(Myconn.cur.Current("Date_out")), DBNull.Value, Myconn.cur.Current("Date_out"))
            drg.Rows(i).Cells(13).Value = If(IsDBNull(Myconn.cur.Current("Late")), DBNull.Value, Myconn.cur.Current("Late"))
            drg.Rows(i).Cells(14).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_name"))
            drg.Rows(i).Cells(15).Value = Myconn.cur.Current("ID")
            drg.Rows(i).Cells(16).Value = If(IsDBNull(Myconn.cur.Current("Holiday_ID")), DBNull.Value, Myconn.cur.Current("Holiday_ID"))

            Myconn.cur.Position += 1
        Next
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Myconn.Filldataset("select h.Holiday_name,e.EmployeeName,j.jobname,s.Employee_Salary,s.Work_hours,
                            a.Day_Date,a.Clock_in,a.Date_in,a.Clock_out,a.Date_out,a.Fingerprint,a.Late,
                            a.Work_Minute,a.ID from Employees_In_Out a
                           left join Holidays h on a.Holiday_ID = h.Holiday_ID
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID 
						   where a.ID =" & CInt(drg.CurrentRow.Cells(15).Value) & " order by a.EmployeeID", "Employees_In_Out", Me)

        Dim Myfields() As String = {"EmployeeName", "EmployeeName", "Work_Minute"}
        Dim Mytxt() As TextBox = {txtEmployee_Come, txtEmployee_Go, txtMenits}
        Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
        'Myconn.DateTPBinding("Day_Date", dtp_begin)
        'Myconn.DateTPBinding("Day_Date", dtp_end)

    End Sub
    Sub Save_Recod_in()

        Try
            Dim sql As String = "Update  Employees_In_Out set Day_Date=@Day_Date,Clock_in=@Clock_in,Date_in=@Date_in,Holiday_ID=@Holiday_ID where ID = @ID"

            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@Day_Date", Format(CDate(dtp_begin.Text), "yyyy/MM/dd"))
                .AddWithValue("@Clock_in", CDate(dtp_come_time.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Date_in", Format(CDate(dtp_begin.Text), "yyyy/MM/dd"))
                .AddWithValue("@Holiday_ID", cbo_holiday.SelectedValue)
                .AddWithValue("@ID", CInt(drg.CurrentRow.Cells(15).Value))
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try
        drg.CurrentRow.DefaultCellStyle.BackColor = Color.Pink

    End Sub
    Sub Save_Recod_out()
        Try
            Dim starttime = DateTime.Parse(Format(CDate(dtp_begin.Text), "yyyy/MM/dd") & Space(1) & drg.CurrentRow.Cells(9).Value)
            Dim endtime = DateTime.Parse(Format(CDate(dtp_end.Text), "yyyy/MM/dd") & Space(1) & dtp_go_time.Text)
            Dim result = endtime - starttime
            txtMenits.Text = Math.Round(result.TotalMinutes)
            If Val(txtMenits.Text) < 0 Then
                txtMenits.Text = 0
            End If

        Catch ex As Exception

        End Try
        Try
            Dim sql As String = "Update  Employees_In_Out set Day_Date=@Day_Date,Clock_out=@Clock_out,Date_out=@Date_out,Work_Minute=@Work_Minute,Late=@Late where ID = @ID"

            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@Day_Date", Format(CDate(dtp_begin.Text), "yyyy/MM/dd"))
                .AddWithValue("@Clock_out", CDate(dtp_go_time.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Date_out", Format(CDate(dtp_end.Text), "yyyy/MM/dd"))
                .AddWithValue("@Work_Minute", txtMenits.Text)
                .AddWithValue("@Late", Val(CInt(drg.CurrentRow.Cells(8).Value) * 60) - Val(txtMenits.Text))
                .AddWithValue("@ID", CInt(drg.CurrentRow.Cells(15).Value))
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try
        drg.CurrentRow.DefaultCellStyle.BackColor = Color.Pink

    End Sub
    Private Sub frmEmployee_Go_Went_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label23.Left = 0
        Label23.Width = Me.Width
        fin = False
        Myconn.Fillcombo("select * from Holidays ", "Holidays", "Holiday_ID", "Holiday_name", Me, cbo_holiday)
        fin = True
        dtp_come_time.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        dtp_go_time.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        Timer1.Start()
        x = 4
        Fillgrd()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        btnSave.Enabled = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" And txt.Name <> "txtNotes" Then
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
        'Save_Recod()
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        NewRecord()

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else

            Myconn.DeleteRecord("Employees_Extra_work", "ID", CInt(drg.CurrentRow.Cells(11).Value))
            drg.Rows.Remove(drg.SelectedRows(0))
            Myconn.ClearAllControls(GroupBox1, True)

        End If
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" And txt.Name <> "txtNotes" Then
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
        Try
            Dim sql As String = "Update  Employees_Extra_work set EmployeeID=@EmployeeID,Work_date=@Work_date,Work_begin=@Work_begin,work_end=@work_end,Hours_number=@Hours_number,Notes=@Notes where ID=@ID"
            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                '.AddWithValue("@EmployeeID", cbo_Employee.SelectedValue)
                '.AddWithValue("@Work_date", Format(CDate(txtDate.Text), "yyyy/MM/dd"))
                .AddWithValue("@Work_begin", dtp_come_time.Text)
                .AddWithValue("@work_end", dtp_go_time.Text)
                .AddWithValue("@Hours_number", txtMenits.Text)
                .AddWithValue("@ID", CInt(drg.CurrentRow.Cells(15).Value))
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try
        Myconn.Filldataset("select e.EmployeeName,j.jobname,s.Employee_Salary,s.Work_hours,* from Employees_Extra_work a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where a.ID =" & CInt(drg.CurrentRow.Cells(11).Value), "Employees_Extra_work", Me)

        drg.CurrentRow.Cells(1).Value = Myconn.cur.Current("Work_date")
        drg.CurrentRow.Cells(2).Value = Myconn.cur.Current("EmployeeName")
        drg.CurrentRow.Cells(3).Value = Myconn.cur.Current("EmployeeID")
        drg.CurrentRow.Cells(4).Value = Myconn.cur.Current("jobname")
        drg.CurrentRow.Cells(5).Value = Myconn.cur.Current("Employee_Salary")
        drg.CurrentRow.Cells(6).Value = Myconn.cur.Current("Work_begin")
        drg.CurrentRow.Cells(7).Value = Myconn.cur.Current("work_end")
        drg.CurrentRow.Cells(8).Value = Myconn.cur.Current("Hours_number")
        drg.CurrentRow.Cells(9).Value = Math.Round((Val(Val(Myconn.cur.Current("Employee_Salary")) / (26 * Val(Myconn.cur.Current("Work_hours")) * 60)) * Val(Myconn.cur.Current("Hours_number"))), 2)
        drg.CurrentRow.Cells(10).Value = Myconn.cur.Current("Notes")
        drg.CurrentRow.Cells(11).Value = Myconn.cur.Current("ID")

        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Binding()
        btnSave.Enabled = False
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label8.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
    Private Sub btn_Go_Click(sender As Object, e As EventArgs) Handles btn_Go.Click
        Save_Recod_out()
        x = 1
        Fillgrd()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dtp_come_time.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        dtp_go_time.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
    Private Sub btn_Come_Click(sender As Object, e As EventArgs) Handles btn_Come.Click
        If cbo_holiday.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cbo_holiday, "اختار حالة الحضور بالنسبة للموظف")
            Return
        End If
        Save_Recod_in()
        x = 1
        Fillgrd()
        'MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

        'Dim x As String = CDate(dtp_come_time.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        'MsgBox(Format(CDate(dtp_end.Text), "yyyy/MM/dd").ToString & Space(1) & x)
        'MsgBox(Now)
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        x = 3
        Fillgrd()
    End Sub
    Private Sub cbo_holiday_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_holiday.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
End Class