Imports System.Data.SqlClient
Imports System.Globalization
Public Class frmEmployee_Extra_work
    Dim Myconn As New connect
    Dim fin As Boolean
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub New(x As Integer, d As String)

        ' This call is required by the designer.
        InitializeComponent()
        Me.MdiParent = Main
        btnDel.Enabled = False
        btnSave.Enabled = False
        btnUpdat.Enabled = False
        btnNew.Enabled = False
        txtDate.Value = d

        GroupBox1.Enabled = False

    End Sub
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.ClearAllControls(GroupBox1, True)
        dtp_begin.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        dtp_end.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
    Sub Fillgrd() ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select e.EmployeeName,j.jobname,s.Employee_Salary,s.Work_hours,* from Employees_Extra_work a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where a.EmployeeID =" & CInt(cbo_Employee.SelectedValue) &
                           " and cast(DATEPART(yyyy,Work_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Work_date),'00') as varchar(2)) = '" & Format(CDate(txtDate.Text), "yyyy/MM") & "' order by a.Work_date", "Employees_Extra_work", Me)

        If Myconn.cur.Count = 0 Then Return
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Work_date")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeID")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("jobname")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Employee_Salary")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Work_begin")
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("work_end")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Hours_number")
            drg.Rows(i).Cells(9).Value = Math.Round((Val(Val(Myconn.cur.Current("Employee_Salary")) / (27 * Val(Myconn.cur.Current("Work_hours")) * 60)) * Val(Myconn.cur.Current("Hours_number"))), 2)
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Notes")
            drg.Rows(i).Cells(11).Value = Myconn.cur.Current("ID")
            Myconn.cur.Position += 1
        Next
        Myconn.Sum_drg2(drg, 8, Label11)
        Myconn.Sum_drg2(drg, 9, Label12)
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Myconn.Filldataset("select e.EmployeeName,j.jobname,s.Employee_Salary,* from Employees_Extra_work a
                            left join Employees_Salary s on a.EmployeeID = s.EmployeeID              
                            left join Employees e on a.EmployeeID = e.EmployeeID 
                            left join Jobs j on e.jobID = j.jobID where a.ID =" & CInt(drg.CurrentRow.Cells(11).Value), "Employees_Extra_work", Me)

        Dim Myfields() As String = {"Employee_Salary", "Hours_number", "Notes"}
        Dim Mytxt() As TextBox = {txtSalary, txtMenits, txtNotes}
        Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
        Myconn.DateTPBinding("Work_date", txtDate)
        Myconn.comboBinding("EmployeeID", cbo_Employee)
        Myconn.DateTPBinding("Work_begin", dtp_begin)
        Myconn.DateTPBinding("work_end", dtp_end)
    End Sub
    Sub Save_Recod()
        Try
            Dim sql As String = "INSERT INTO Employees_Extra_work(EmployeeID,Work_date,Work_begin,work_end,Hours_number,Notes) 
                                                           VALUES(@EmployeeID,@Work_date,@Work_begin,@work_end,@Hours_number,@Notes)"
            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@EmployeeID", cbo_Employee.SelectedValue)
                .AddWithValue("@Work_date", Format(CDate(txtDate.Text), "yyyy/MM/dd"))
                .AddWithValue("@Work_begin", CDate(dtp_begin.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@work_end", CDate(dtp_end.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Hours_number", txtMenits.Text)
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

    Private Sub frmEmployee_Extra_work_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label23.Left = 0
        Label23.Width = Me.Width
        fin = False
        Myconn.Fillcombo("select e.EmployeeName,* from Employees_Salary a left join Employees e on a.EmployeeID = e.EmployeeID ", "Employees", "EmployeeID", "EmployeeName", Me, cbo_Employee)
        fin = True
        dtp_begin.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        dtp_end.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
        Timer1.Start()
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
                .AddWithValue("@EmployeeID", cbo_Employee.SelectedValue)
                .AddWithValue("@Work_date", Format(CDate(txtDate.Text), "yyyy/MM/dd"))
                .AddWithValue("@Work_begin", CDate(dtp_begin.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@work_end", CDate(dtp_end.Text).ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg")))
                .AddWithValue("@Hours_number", txtMenits.Text)
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
        drg.CurrentRow.Cells(9).Value = Math.Round((Val(Val(Myconn.cur.Current("Employee_Salary")) / (27 * Val(Myconn.cur.Current("Work_hours")) * 60)) * Val(Myconn.cur.Current("Hours_number"))), 2)
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
        Myconn.Filldataset("select e.EmployeeName,* from Employees_Salary a left join Employees e on a.EmployeeID = e.EmployeeID where a.EmployeeID =" & CInt(cbo_Employee.SelectedValue), "Employees_Salary", Me)
        txtSalary.Text = Myconn.cur.Current("Employee_Salary")
        Fillgrd()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label8.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub

    Private Sub dtp_end_TextChanged(sender As Object, e As EventArgs) Handles dtp_end.TextChanged
        Try
            Dim starttime = DateTime.Parse(dtp_begin.Text)
            Dim endtime = DateTime.Parse(dtp_end.Text)
            Dim result = endtime - starttime
            txtMenits.Text = CInt(result.TotalMinutes)
            If Val(txtMenits.Text) < 0 Then
                txtMenits.Text = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class