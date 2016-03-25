Imports System.Globalization
Imports System.Data.SqlClient
Public Class frmEmployee_Salary
    Dim Myconn As New connect
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub New(x As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        GroupBox1.Enabled = False
        GroupBox3.Enabled = False
        btnDel.Enabled = False
        btnNew.Enabled = False
        btnUpdat.Enabled = False
        Me.MdiParent = Main
        'MsgBox(x)
        'drg.ClearSelection()
        'For W As Integer = 0 To drg.Rows.Count - 1
        '    If drg.Rows(W).Cells(2).Value.Equals(x) Then
        '        drg.Rows(W).Cells(2).Selected = True
        '        drg.CurrentCell = drg.SelectedCells(2)
        '        Exit For
        '    End If
        'Next

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.ClearAllControls(GroupBox1, True)
        Myconn.ClearAllControls(GroupBox3, True)
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select a.ID,w.Day_name,e.EmployeeName,a.EmployeeID,a.Ezn_number,a.Arda,a.Etyade,j.jobname,a.Employee_Salary,a.Work_hours,a.Shift_Begin,a.Shift_End from Employees_Salary a 
                            left join Week_days W on a.Week_end = W.Week_ID 
                            left join Employees e on a.EmployeeID = e.EmployeeID
                            left join jobs j on e.jobID = j.jobID ", "Employees_Salary", Me)

        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("EmployeeID")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("jobname")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Work_hours")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Employee_Salary")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Shift_Begin")
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Shift_End")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("ID")
            drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Ezn_number")
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Arda")
            drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Etyade")
            drg.Rows(i).Cells(12).Value = Myconn.cur.Current("Day_name")
            Myconn.cur.Position += 1
        Next
        Myconn.DataGridview_MoveLast(drg, 3)
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Dim Myfields() As String = {"Work_hours", "Employee_Salary", "Ezn_number", "Arda", "Etyade"}
        Dim Mytxt() As TextBox = {txtWork_hours, txtSalary, txtEzn, txtArda, txtetyade}
        Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
        Myconn.comboBinding("EmployeeID", cbo_Employee)
        Myconn.comboBinding("Week_end", cboWeek)
        Myconn.DateTPBinding("Shift_Begin", dtp_come_time)
        Myconn.DateTPBinding("Shift_End", dtp_go_time)
    End Sub
    Sub Save_Recod()
        Try
            Dim sql As String = "INSERT INTO Employees_Salary(EmployeeID,Employee_Salary,Work_hours,Shift_Begin,Shift_End,Ezn_number,Arda,Etyade,Week_end,State_ID) 
                                                       VALUES(@EmployeeID,@Employee_Salary,@Work_hours,@Shift_Begin,@Shift_End,@Ezn_number,@Arda,@Etyade,@Week_end,@State_ID)"

            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@EmployeeID", cbo_Employee.SelectedValue)
                .AddWithValue("@Employee_Salary", txtSalary.Text)
                .AddWithValue("@Work_hours", txtWork_hours.Text)
                .AddWithValue("@Shift_Begin", CDate(dtp_come_time.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Shift_End", CDate(dtp_go_time.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Ezn_number", txtEzn.Text)
                .AddWithValue("@Arda", txtArda.Text)
                .AddWithValue("@Etyade", txtetyade.Text)
                .AddWithValue("@Week_end", cboWeek.SelectedValue)
                .AddWithValue("@State_ID", 1)
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
    Sub Update_record()
        Try
            Dim sql As String = "Update  Employees_Salary set EmployeeID=@EmployeeID,Employee_Salary=@Employee_Salary,Work_hours=@Work_hours,Shift_Begin=@Shift_Begin,Shift_End=@Shift_End,Ezn_number=@Ezn_number,Arda=@Arda,Etyade=@Etyade,Week_end=@Week_end where ID=@ID"
            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@EmployeeID", cbo_Employee.SelectedValue)
                .AddWithValue("@Employee_Salary", txtSalary.Text)
                .AddWithValue("@Work_hours", txtWork_hours.Text)
                .AddWithValue("@Shift_Begin", CDate(dtp_come_time.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Shift_End", CDate(dtp_go_time.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Ezn_number", txtEzn.Text)
                .AddWithValue("@Arda", txtArda.Text)
                .AddWithValue("@Etyade", txtetyade.Text)
                .AddWithValue("@Week_end", cboWeek.SelectedValue)
                .AddWithValue("@ID", CInt(drg.CurrentRow.Cells(8).Value))
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
    Private Sub frmEmployee_Salary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        btnSave.Enabled = False
        Myconn.Fillcombo("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cbo_Employee)
        Myconn.Fillcombo("select * from Week_days", "Week_days", "Week_ID", "Day_name", Me, cboWeek)
        Timer1.Start()
        dtp_come_time.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        dtp_go_time.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        Fillgrd()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        btnSave.Enabled = True
        dtp_come_time.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        dtp_go_time.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))

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

        Myconn.Filldataset("select * from Employees_Salary where EmployeeID =" & CInt(cbo_Employee.SelectedValue), "Employees_Salary", Me)
        If Myconn.cur.Count > 0 Then
            MsgBox("هذا الموظف مسجل له مرتب من قبل")
            drg.ClearSelection()
            For W As Integer = 0 To drg.Rows.Count - 1

                If drg.Rows(W).Cells(2).Value.ToString.Equals(cbo_Employee.SelectedValue.ToString, StringComparison.CurrentCultureIgnoreCase) Then
                    drg.Rows(W).Cells(2).Selected = True
                    drg.CurrentCell = drg.SelectedCells(2)
                    Exit For
                End If
            Next
            Binding()
            Return
        End If
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

            Myconn.DeleteRecord("Employees_Salary", "ID", CInt(drg.CurrentRow.Cells(8).Value))
            drg.Rows.Remove(drg.SelectedRows(0))
            Myconn.ClearAllControls(GroupBox1, True)

        End If
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
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

        Update_record()

        Myconn.Filldataset("select a.ID,w.Day_name,e.EmployeeName,a.EmployeeID,a.Ezn_number,a.Arda,a.Etyade,j.jobname,a.Employee_Salary,a.Work_hours,a.Shift_Begin,a.Shift_End from Employees_Salary a 
                            left join Week_days W on a.Week_end = W.Week_ID 
                            left join Employees e on a.EmployeeID = e.EmployeeID
                            left join jobs j on e.jobID = j.jobID  where a.ID =" & CInt(drg.CurrentRow.Cells(8).Value), "Employees_Salary", Me)

        drg.CurrentRow.Cells(1).Value = Myconn.cur.Current("EmployeeName")
        drg.CurrentRow.Cells(2).Value = Myconn.cur.Current("EmployeeID")
        drg.CurrentRow.Cells(3).Value = Myconn.cur.Current("jobname")
        drg.CurrentRow.Cells(4).Value = Myconn.cur.Current("Work_hours")
        drg.CurrentRow.Cells(5).Value = Myconn.cur.Current("Employee_Salary")
        drg.CurrentRow.Cells(6).Value = Myconn.cur.Current("Shift_Begin")
        drg.CurrentRow.Cells(7).Value = Myconn.cur.Current("Shift_End")
        drg.CurrentRow.Cells(8).Value = Myconn.cur.Current("ID")
        drg.CurrentRow.Cells(9).Value = Myconn.cur.Current("Ezn_number")
        drg.CurrentRow.Cells(10).Value = Myconn.cur.Current("Arda")
        drg.CurrentRow.Cells(11).Value = Myconn.cur.Current("Etyade")
        drg.CurrentRow.Cells(12).Value = Myconn.cur.Current("Day_name")

        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub
    Public Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset("select * from Employees_Salary where ID =" & CInt(drg.CurrentRow.Cells(8).Value), "Employees_Salary3", Me)
        Binding()
        btnSave.Enabled = False
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label7.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub

    Private Sub dtp_come_time_TextChanged(sender As Object, e As EventArgs) Handles dtp_come_time.TextChanged
        Try

            dtp_go_time.Text = Format(CType(dtp_come_time.Text, DateTime).AddHours(Val(txtWork_hours.Text)), "hh:mm:ss tt")
        Catch ex As Exception
            Return
        End Try
    End Sub
End Class