Imports System.Data.SqlClient
Imports System.Globalization
Public Class frmEmployee_Ezn
    Dim Myconn As New connect
    Dim fin As Boolean
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub New(x As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        Me.MdiParent = Main
        GroupBox1.Visible = False
        GroupBox3.Visible = False
        GroupBox2.Top = GroupBox1.Top
        Me.Height = Me.Height - (GroupBox1.Height + 10)
        btnDel.Enabled = False
        btnNew.Enabled = False
        btnSave.Enabled = False
        btnUpdat.Enabled = False
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.ClearAllControls(GroupBox1, True)
        Myconn.ClearAllControls(GroupBox3, True)
        dtp_begin.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        dtp_end.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        dtp_Back.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
    Sub Fillgrd() ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select e.EmployeeName,j.jobname,s.Employee_Salary,* from Employees_Ezn a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where a.EmployeeID =" & CInt(cbo_Employee.SelectedValue) & "order by a.Ezn_date", "Employees_Ezn", Me)

        If Myconn.cur.Count = 0 Then Return

        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Ezn_date")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeID")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("jobname")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Ezn_hours")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Ezn_begin")
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Ezn_end")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Ezn_Back")
            drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Ezn_late")
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Notes")
            drg.Rows(i).Cells(11).Value = Myconn.cur.Current("ID")
            Myconn.cur.Position += 1
        Next
    End Sub
    Sub Fillgrd2() ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select e.EmployeeName,j.jobname,s.Employee_Salary,* from Employees_Ezn a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where a.EmployeeID =" & CInt(cbo_Employee.SelectedValue) &
                           "  and cast(DATEPART(yyyy,a.Ezn_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Ezn_date),'00') as varchar(2)) = '" & Format(CDate(frmEmployees_Report_Salary.Dat), "yyyy/MM") & "' order by a.Ezn_date", "Employees_Ezn", Me)

        If Myconn.cur.Count = 0 Then Return

        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Ezn_date")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeID")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("jobname")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Ezn_hours")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Ezn_begin")
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Ezn_end")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Ezn_Back")
            drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Ezn_late")
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Notes")
            drg.Rows(i).Cells(11).Value = Myconn.cur.Current("ID")
            Myconn.cur.Position += 1
        Next
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Myconn.Filldataset("select e.EmployeeName,j.jobname,s.Employee_Salary,* from Employees_Ezn a
                            left join Employees_Salary s on a.EmployeeID = s.EmployeeID              
                            left join Employees e on a.EmployeeID = e.EmployeeID 
                            left join Jobs j on e.jobID = j.jobID where a.ID =" & CInt(drg.CurrentRow.Cells(11).Value), "Employees_Ezn", Me)

        Dim Myfields() As String = {"Ezn_hours", "Ezn_late", "Notes"}
        Dim Mytxt() As TextBox = {txtEzn_houes, txtLate, txtNotes}
        Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
        Myconn.DateTPBinding("Ezn_date", txtDate)
        Myconn.comboBinding("EmployeeID", cbo_Employee)

        Myconn.DateTPBinding("Ezn_begin", dtp_begin)
        Myconn.DateTPBinding("Ezn_end", dtp_end)
        Myconn.DateTPBinding("Ezn_Back", dtp_Back)
    End Sub
    Sub Save_Recod()
        Try
            Dim sql As String = "INSERT INTO Employees_Ezn(EmployeeID,Ezn_date,Ezn_hours,Ezn_begin,Ezn_end,Ezn_Back,Ezn_late,Notes) VALUES(@EmployeeID,@Ezn_date,@Ezn_hours,@Ezn_begin,@Ezn_end,@Ezn_Back,@Ezn_late,@Notes)"
            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@EmployeeID", cbo_Employee.SelectedValue)
                .AddWithValue("@Ezn_date", Format(CDate(txtDate.Text), "yyyy/MM/dd"))
                .AddWithValue("@Ezn_hours", txtEzn_houes.Text)
                .AddWithValue("@Ezn_begin", CDate(dtp_begin.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Ezn_end", CDate(dtp_end.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Ezn_Back", DBNull.Value)
                .AddWithValue("@Ezn_late", DBNull.Value)
                .AddWithValue("@Notes", If(txtNotes.Text = Nothing, DBNull.Value, txtNotes.Text))
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try

    End Sub
    Private Sub frmEmployee_Ezn_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label23.Left = 0
        Label23.Width = Me.Width
        fin = False
        Myconn.Fillcombo("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cbo_Employee)
        fin = True
        Timer1.Start()
        Label8.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        dtp_begin.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        dtp_end.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        dtp_Back.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        btnSave.Enabled = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" And txt.Name <> "txtEzn_back" And txt.Name <> "txtLate" And txt.Name <> "txtNotes" Then
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
            Myconn.Filldataset("select isnull(sum(Ezn_hours),0)  as Ezn_hours from Employees_Ezn  where EmployeeID =" & CInt(cbo_Employee.SelectedValue) &
                                " and (cast(DATEPART(yyyy,Ezn_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Ezn_date),'00') as varchar(2)))  ='" & Format(CDate(txtDate.Text), "yyyy/MM") & "'", "Employees_Ezn", Me)

            Myconn.Filldataset2("select *  from Employees_Salary where EmployeeID =" & CInt(cbo_Employee.SelectedValue), "Employees_Salary", Me)

            MsgBox(" ساعات الاذن  التي أخذها الموظف  " & Myconn.cur.Current("Ezn_hours") & " في شهر " & Format(CDate(txtDate.Text), "yyyy/MM"))

            If Val(Myconn.cur.Current("Ezn_hours")) + Val(txtEzn_houes.Text) > Val(Myconn.cur2.Current("Ezn_number")) Then
                MsgBox("الرصيد لا يسمح")
                Return
            End If
        Catch ex As Exception

        End Try
        Save_Recod()
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        NewRecord()
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else

            Myconn.DeleteRecord("Employees_Ezn", "ID", CInt(drg.CurrentRow.Cells(11).Value))
            drg.Rows.Remove(drg.SelectedRows(0))
            Myconn.ClearAllControls(GroupBox1, True)

        End If
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" And txt.Name <> "txtEzn_back" And txt.Name <> "txtLate" And txt.Name <> "txtNotes" Then
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
            Myconn.Filldataset("select isnull(sum(Ezn_hours),0)  as Ezn_hours from Employees_Ezn  where EmployeeID =" & CInt(cbo_Employee.SelectedValue) &
                                " and (cast(DATEPART(yyyy,Ezn_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Ezn_date),'00') as varchar(2)))  ='" & Format(CDate(txtDate.Text), "yyyy/MM") & "'", "Employees_Ezn", Me)

            Myconn.Filldataset2("select *  from Employees_Salary where EmployeeID =" & CInt(cbo_Employee.SelectedValue), "Employees_Salary", Me)

            'MsgBox(" ساعات الاذن  التي أخذها الموظف  " & Myconn.cur.Current("Ezn_hours") & " في شهر " & Format(CDate(txtDate.Text), "yyyy/MM"))

            If Val(Val(Myconn.cur.Current("Ezn_hours")) - Val(drg.CurrentRow.Cells(5).Value)) + Val(txtEzn_houes.Text) > Val(Myconn.cur2.Current("Ezn_number")) Then
                MsgBox("الرصيد لا يسمح")
                Return
            End If
        Catch ex As Exception

        End Try

        Try
            Dim sql As String = "Update  Employees_Ezn set EmployeeID=@EmployeeID,Ezn_date=@Ezn_date,Ezn_hours=@Ezn_hours,Ezn_begin=@Ezn_begin,Ezn_end=@Ezn_end,Ezn_Back=@Ezn_Back,Ezn_late=@Ezn_late,Notes=@Notes where ID=@ID"
            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@EmployeeID", cbo_Employee.SelectedValue)
                .AddWithValue("@Ezn_date", Format(CDate(txtDate.Text), "yyyy/MM/dd"))
                .AddWithValue("@Ezn_hours", txtEzn_houes.Text)
                .AddWithValue("@Ezn_begin", CDate(dtp_begin.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Ezn_end", CDate(dtp_end.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Ezn_Back", CDate(dtp_Back.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Ezn_late", If(txtLate.Text = Nothing, DBNull.Value, txtLate.Text))
                .AddWithValue("@Notes", If(txtNotes.Text = Nothing, DBNull.Value, txtNotes.Text))
                .AddWithValue("@ID", CInt(drg.CurrentRow.Cells(11).Value))
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try

        Myconn.Filldataset("select e.EmployeeName,j.jobname,s.Employee_Salary,* from Employees_Ezn a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where a.ID =" & CInt(drg.CurrentRow.Cells(11).Value), "Employees_Ezn", Me)

        drg.CurrentRow.Cells(1).Value = Myconn.cur.Current("Ezn_date")
        drg.CurrentRow.Cells(2).Value = Myconn.cur.Current("EmployeeName")
        drg.CurrentRow.Cells(3).Value = Myconn.cur.Current("EmployeeID")
        drg.CurrentRow.Cells(4).Value = Myconn.cur.Current("jobname")
        drg.CurrentRow.Cells(5).Value = Myconn.cur.Current("Ezn_hours")
        drg.CurrentRow.Cells(6).Value = Myconn.cur.Current("Ezn_begin")
        drg.CurrentRow.Cells(7).Value = Myconn.cur.Current("Ezn_end")
        drg.CurrentRow.Cells(8).Value = Myconn.cur.Current("Ezn_Back")
        drg.CurrentRow.Cells(9).Value = Myconn.cur.Current("Ezn_late")
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
    Private Sub cbo_Employee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Employee.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        If cbo_Employee.SelectedIndex = -1 Then Return
        Fillgrd()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label8.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
    Private Sub txtEzn_houes_TextChanged(sender As Object, e As EventArgs) Handles txtEzn_houes.TextChanged
        Try
            dtp_end.Text = CDate(dtp_begin.Text).AddHours(Val(txtEzn_houes.Text))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim sql As String = "Update  Employees_Ezn set Ezn_Back=@Ezn_Back,Ezn_late=@Ezn_late where ID=@ID"
            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@Ezn_Back", CDate(dtp_Back.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Ezn_late", If(txtLate.Text = Nothing, DBNull.Value, txtLate.Text))
                .AddWithValue("@ID", CInt(drg.CurrentRow.Cells(11).Value))
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try
        Myconn.Filldataset("select e.EmployeeName,j.jobname,s.Employee_Salary,* from Employees_Ezn a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where a.ID =" & CInt(drg.CurrentRow.Cells(11).Value), "Employees_Ezn", Me)

        drg.CurrentRow.Cells(1).Value = Myconn.cur.Current("Ezn_date")
        drg.CurrentRow.Cells(2).Value = Myconn.cur.Current("EmployeeName")
        drg.CurrentRow.Cells(3).Value = Myconn.cur.Current("EmployeeID")
        drg.CurrentRow.Cells(4).Value = Myconn.cur.Current("jobname")
        drg.CurrentRow.Cells(5).Value = Myconn.cur.Current("Ezn_hours")
        drg.CurrentRow.Cells(6).Value = Myconn.cur.Current("Ezn_begin")
        drg.CurrentRow.Cells(7).Value = Myconn.cur.Current("Ezn_end")
        drg.CurrentRow.Cells(8).Value = Myconn.cur.Current("Ezn_Back")
        drg.CurrentRow.Cells(9).Value = Myconn.cur.Current("Ezn_late")
        drg.CurrentRow.Cells(10).Value = Myconn.cur.Current("Notes")
        drg.CurrentRow.Cells(11).Value = Myconn.cur.Current("ID")
        MessageBox.Show("تمت عملية تسجيل العودة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub

    Private Sub dtp_Back_TextChanged(sender As Object, e As EventArgs) Handles dtp_Back.TextChanged
        Try
            Dim starttime = DateTime.Parse(dtp_end.Text)
            Dim endtime = DateTime.Parse(dtp_Back.Text)
            Dim result = endtime - starttime
            txtLate.Text = CInt(result.TotalMinutes)
            If Val(txtLate.Text) < 0 Then
                txtLate.Text = 0
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class