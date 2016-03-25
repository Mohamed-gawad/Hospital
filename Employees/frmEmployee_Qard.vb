
Imports System.Data.SqlClient
Public Class frmEmployee_Qard
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
        GroupBox1.Enabled = False
        btnDel.Enabled = False
        btnNew.Enabled = False
        btnSave.Enabled = False
        btnUpdat.Enabled = False
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.ClearAllControls(GroupBox1, True)
        Myconn.Autonumber("Qard_ID", "Employees_Qard", txtID, Me)
    End Sub
    Sub Fillgrd() ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select e.EmployeeName,j.jobname,s.Employee_Salary,* from Employees_Qard a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where a.EmployeeID =" & CInt(cbo_Employee.SelectedValue) & "order by a.Qard_date", "Employees_Qard", Me)

        If Myconn.cur.Count = 0 Then Return

        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Qard_date")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeID")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("jobname")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Employee_Salary")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Qard_ID")
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Qard_amount")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Qard_kist")
            drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Date_first_kist")
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Notes")
            drg.Rows(i).Cells(11).Value = Myconn.cur.Current("ID")
            Myconn.cur.Position += 1
        Next
        Myconn.DataGridview_MoveLast(drg, 2)
    End Sub
    Sub Fillgrd2()
        Try
            drg2.Rows.Clear()
            If drg.Rows.Count = 0 Then Return
            Myconn.Filldataset("select * from Employees_Qard_Kist  where Qard_ID =" & CInt(drg.CurrentRow.Cells(6).Value) & "order by Qard_Kist_date", "Employees_Qard_Kist", Me)
            If Myconn.cur.Count = 0 Then Return
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg2.Rows.Add()
                drg2.Rows(i).Cells(0).Value = i + 1
                drg2.Rows(i).Cells(1).Value = Myconn.cur.Current("Qard_ID")
                drg2.Rows(i).Cells(2).Value = Myconn.cur.Current("Qard_Kist_date")
                drg2.Rows(i).Cells(3).Value = Myconn.cur.Current("Kist_amount")
                Myconn.cur.Position += 1
            Next
        Catch ex As Exception
            MsgBox("Error")
        End Try

    End Sub

    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Myconn.Filldataset("select e.EmployeeName,j.jobname,s.Employee_Salary,* from Employees_Qard a
                            left join Employees_Salary s on a.EmployeeID = s.EmployeeID              
                            left join Employees e on a.EmployeeID = e.EmployeeID 
                            left join Jobs j on e.jobID = j.jobID where a.ID =" & CInt(drg.CurrentRow.Cells(11).Value), "Employees_Qard", Me)

        Dim Myfields() As String = {"Qard_ID", "Employee_Salary", "Qard_amount", "Qard_kist", "Notes"}
        Dim Mytxt() As TextBox = {txtID, txtSalary, txtQard, txtQard_kist, txtNotes}
        Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
        Myconn.DateTPBinding("Qard_date", dtp_Date)
        Myconn.DateTPBinding("Date_first_kist", dtpFirst_kist)
        Myconn.comboBinding("EmployeeID", cbo_Employee)

    End Sub
    Sub Save_Recod()
        Try
            Dim sql As String = "INSERT INTO Employees_Qard(Qard_ID,EmployeeID,Qard_date,Qard_amount,Qard_kist,Date_first_kist,Notes) VALUES(@Qard_ID,@EmployeeID,@Qard_date,@Qard_amount,@Qard_kist,@Date_first_kist,@Notes)"
            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@Qard_ID", txtID.Text)
                .AddWithValue("@EmployeeID", cbo_Employee.SelectedValue)
                .AddWithValue("@Qard_date", Format(CDate(dtp_Date.Text), "yyyy/MM/dd"))
                .AddWithValue("@Qard_amount", txtQard.Text)
                .AddWithValue("@Qard_kist", txtQard_kist.Text)
                .AddWithValue("@Date_first_kist", Format(CDate(dtpFirst_kist.Text), "yyyy/MM/dd"))
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
    Sub Save_Recod_Qard_Kist()
        Try
            For i As Integer = 0 To txtQard_kist.Text - 1


                Dim sql As String = "INSERT INTO Employees_Qard_Kist(Qard_ID,EmployeeID,Qard_Kist_date,Kist_amount) VALUES(@Qard_ID,@EmployeeID,@Qard_Kist_date,@Kist_amount)"
                Myconn.cmd = New SqlCommand(sql, Myconn.conn)

                With Myconn.cmd.Parameters
                    .AddWithValue("@Qard_ID", txtID.Text)
                    .AddWithValue("@EmployeeID", cbo_Employee.SelectedValue)
                    .AddWithValue("@Qard_Kist_date", Format(CDate(dtpFirst_kist.Text).AddMonths(i), "yyyy/MM/dd"))
                    .AddWithValue("@Kist_amount", Math.Round((Val(txtQard.Text) / Val(txtQard_kist.Text)), 2))
                End With
                If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
                Myconn.conn.Open()
                Myconn.cmd.ExecuteNonQuery()
                Myconn.conn.Close()
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try
    End Sub
    Private Sub frmEmployee_Qard_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label23.Left = 0
        Label23.Width = Me.Width
        btnSave.Enabled = False
        'btnUpdat.Enabled = False
        fin = False
        Myconn.Fillcombo("select e.EmployeeName,* from Employees_Salary a left join Employees e on a.EmployeeID = e.EmployeeID ", "Employees", "EmployeeID", "EmployeeName", Me, cbo_Employee)
        fin = True
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
        Save_Recod_Qard_Kist()
        Fillgrd()
        Fillgrd2()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        NewRecord()

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else

            Myconn.DeleteRecord("Employees_Qard", "ID", CInt(drg.CurrentRow.Cells(11).Value))
            Myconn.DeleteRecord("Employees_Qard_Kist", "Qard_ID", CInt(drg.CurrentRow.Cells(6).Value))
            drg.Rows.Remove(drg.SelectedRows(0))
            drg2.Rows.Clear()

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
            Dim sql As String = "Update  Employees_Qard set Qard_ID=@Qard_ID,EmployeeID=@EmployeeID,Qard_date=@Qard_date,Qard_amount=@Qard_amount,Qard_kist=@Qard_kist,Date_first_kist=@Date_first_kist,Notes=@Notes where ID=@ID"
            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@Qard_ID", txtID.Text)
                .AddWithValue("@EmployeeID", cbo_Employee.SelectedValue)
                .AddWithValue("@Qard_date", Format(CDate(dtp_Date.Text), "yyyy/MM/dd"))
                .AddWithValue("@Qard_amount", txtQard.Text)
                .AddWithValue("@Qard_kist", txtQard_kist.Text)
                .AddWithValue("@Date_first_kist", Format(CDate(dtpFirst_kist.Text), "yyyy/MM/dd"))
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
        Myconn.Filldataset("select e.EmployeeName,j.jobname,s.Employee_Salary,* from Employees_Qard a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Jobs j on e.jobID = j.jobID where a.ID =" & CInt(drg.CurrentRow.Cells(11).Value), "Employees_Qard", Me)

        drg.CurrentRow.Cells(1).Value = Myconn.cur.Current("Qard_date")
        drg.CurrentRow.Cells(2).Value = Myconn.cur.Current("EmployeeName")
        drg.CurrentRow.Cells(3).Value = Myconn.cur.Current("EmployeeID")
        drg.CurrentRow.Cells(4).Value = Myconn.cur.Current("jobname")
        drg.CurrentRow.Cells(5).Value = Myconn.cur.Current("Employee_Salary")
        drg.CurrentRow.Cells(6).Value = Myconn.cur.Current("Qard_ID")
        drg.CurrentRow.Cells(7).Value = Myconn.cur.Current("Qard_amount")
        drg.CurrentRow.Cells(8).Value = Myconn.cur.Current("Qard_kist")
        drg.CurrentRow.Cells(9).Value = Myconn.cur.Current("Date_first_kist")
        drg.CurrentRow.Cells(10).Value = Myconn.cur.Current("Notes")
        drg.CurrentRow.Cells(11).Value = Myconn.cur.Current("ID")

        Myconn.DeleteRecord("Employees_Qard_Kist", "Qard_ID", CInt(drg.CurrentRow.Cells(6).Value))
        drg2.Rows.Clear()
        Save_Recod_Qard_Kist()
        Fillgrd2()

        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub

    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Binding()
        btnSave.Enabled = False
        Fillgrd2()

    End Sub
    Private Sub cbo_Employee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Employee.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        If cbo_Employee.SelectedIndex = -1 Then Return
        Myconn.Filldataset("select e.EmployeeName,* from Employees_Salary a left join Employees e on a.EmployeeID = e.EmployeeID where a.EmployeeID =" & CInt(cbo_Employee.SelectedValue), "Employees_Salary", Me)
        txtSalary.Text = Myconn.cur.Current("Employee_Salary")

        Fillgrd()
        Fillgrd2()
    End Sub
End Class