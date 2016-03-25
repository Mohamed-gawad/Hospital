Imports System.Data.SqlClient
Public Class frmEmployee_holiday
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
    Sub Fillgrd() ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select h.Holiday_name,e.EmployeeName,j.jobname,s.Employee_Salary,* from Employees_Holiday a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Holidays h on a.Holiday_ID = h.Holiday_ID
                           left join Jobs j on e.jobID = j.jobID where a.EmployeeID =" & CInt(cbo_Employee.SelectedValue) & "order by a.H_date_begin", "Employees_Holiday", Me)
        If Myconn.cur.Count = 0 Then Return
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Order_date")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeID")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("jobname")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Holiday_number")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Holiday_name")
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("H_date_begin")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("H_date_end")
            drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Notes")
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("ID")
            Myconn.cur.Position += 1
        Next
    End Sub
    Sub Fillgrd2() ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select h.Holiday_name,e.EmployeeName,j.jobname,s.Employee_Salary,* from Employees_Holiday a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Holidays h on a.Holiday_ID = h.Holiday_ID
                           left join Jobs j on e.jobID = j.jobID where a.EmployeeID =" & CInt(cbo_Employee.SelectedValue) &
                           " and cast(DATEPART(yyyy,a.H_date_begin) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.H_date_begin),'00') as varchar(2)) = '" & Format(CDate(frmEmployees_Report_Salary.Dat), "yyyy/MM") & "' order by a.H_date_begin", "Employees_Holiday", Me)
        If Myconn.cur.Count = 0 Then Return
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Order_date")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeID")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("jobname")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Holiday_number")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Holiday_name")
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("H_date_begin")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("H_date_end")
            drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Notes")
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("ID")
            Myconn.cur.Position += 1
        Next
    End Sub
    Sub NewRecord()
        Myconn.ClearAllControls(GroupBox1, True)
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Myconn.Filldataset("select h.Holiday_name,e.EmployeeName,j.jobname,* from Employees_Holiday a
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Holidays h on a.Holiday_ID = h.Holiday_ID
                           left join Jobs j on e.jobID = j.jobID where a.ID =" & CInt(drg.CurrentRow.Cells(10).Value), "Employees_Holiday", Me)
        Dim Myfields() As String = {"Holiday_number", "Notes"}
        Dim Mytxt() As TextBox = {txt_H_number, txtNotes}
        Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
        Myconn.DateTPBinding("Order_date", txtDate)
        Myconn.DateTPBinding("H_date_begin", date_begin)
        Myconn.DateTPBinding("H_date_end", date_end)
        Myconn.comboBinding("EmployeeID", cbo_Employee)
        Myconn.comboBinding("Holiday_ID", cbo_Holiday)
    End Sub
    Sub Save_Recod()
        Try
            Dim sql As String = "INSERT INTO Employees_Holiday(EmployeeID,Holiday_ID,Holiday_number,H_date_begin,H_date_end,Order_date,Notes) VALUES(@EmployeeID,@Holiday_ID,@Holiday_number,@H_date_begin,@H_date_end,@Order_date,@Notes)"
            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@EmployeeID", cbo_Employee.SelectedValue)
                .AddWithValue("@Order_date", Format(CDate(txtDate.Text), "yyyy/MM/dd"))
                .AddWithValue("@Holiday_ID", cbo_Holiday.SelectedValue)
                .AddWithValue("@H_date_begin", Format(CDate(date_begin.Text), "yyyy/MM/dd"))
                .AddWithValue("@H_date_end", Format(CDate(date_end.Text), "yyyy/MM/dd"))
                .AddWithValue("@Holiday_number", txt_H_number.Text)
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
    Private Sub frmEmployee_holiday_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label23.Left = 0
        Label23.Width = Me.Width
        fin = False
        Myconn.Fillcombo("select e.EmployeeName,* from Employees_Salary a left join Employees e on a.EmployeeID = e.EmployeeID ", "Employees", "EmployeeID", "EmployeeName", Me, cbo_Employee)
        Myconn.Fillcombo("select * from Holidays order by Holiday_ID", "Holidays", "Holiday_ID", "Holiday_name", Me, cbo_Holiday)
        fin = True
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
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

        Try
            Myconn.Filldataset("select isnull(sum(Holiday_number),0)  as Holiday_number from Employees_Holiday  where EmployeeID =" & CInt(cbo_Employee.SelectedValue) &
                               " and Holiday_ID = " & CInt(cbo_Holiday.SelectedValue) & " and DATEPART(yyyy,H_date_begin) = '" & CDate(date_begin.Text).Year & "'", "Employees_Holiday", Me)


            Myconn.Filldataset2("select *  from Employees_Salary where EmployeeID =" & CInt(cbo_Employee.SelectedValue), "Employees_Salary", Me)

            Select Case cbo_Holiday.SelectedIndex
                Case 0
                    MsgBox(" العوارض التي أخذها الموظف هي " & Myconn.cur.Current("Holiday_number") & " عارضة في عام " & CDate(date_begin.Text).Year)

                    If Val(Myconn.cur.Current("Holiday_number")) + Val(txt_H_number.Text) > Val(Myconn.cur2.Current("Arda")) Then
                        MsgBox("الرصيد لا يسمح")
                        Return
                    End If
                Case 1
                    MsgBox(" الاعتيادي الذي أخذه الموظف هو " & Myconn.cur.Current("Holiday_number") & " اعتيادي في عام " & CDate(date_begin.Text).Year)

                    If Val(Myconn.cur2.Current("Etyade")) = 0 Then
                        MsgBox("لا توجد أجازات اعتيادية للموظف")
                        Return
                    Else
                        If Val(Myconn.cur.Current("Holiday_number")) + Val(txt_H_number.Text) > Val(Myconn.cur2.Current("Etyade")) Then
                            MsgBox("الرصيد لا يسمح")
                            Return
                        End If
                    End If

            End Select
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

            Myconn.DeleteRecord("Employees_Holiday", "ID", CInt(drg.CurrentRow.Cells(10).Value))
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


        Try
            Myconn.Filldataset("select isnull(sum(Holiday_number),0)  as Holiday_number from Employees_Holiday  where EmployeeID =" & CInt(cbo_Employee.SelectedValue) &
                               " and Holiday_ID = " & CInt(cbo_Holiday.SelectedValue) & " and DATEPART(yyyy,H_date_begin) = '" & CDate(date_begin.Text).Year & "'", "Employees_Holiday", Me)


            Myconn.Filldataset2("select *  from Employees_Salary where EmployeeID =" & CInt(cbo_Employee.SelectedValue), "Employees_Salary", Me)

            Select Case cbo_Holiday.SelectedIndex
                Case 0
                    'MsgBox(" العوارض التي أخذها الموظف هي " & Myconn.cur.Current("Holiday_number") & " عارضة في عام " & CDate(date_begin.Text).Year)

                    If Val(Val(Myconn.cur.Current("Holiday_number")) - Val(drg.CurrentRow.Cells(5).Value)) + Val(txt_H_number.Text) > Val(Myconn.cur2.Current("Arda")) Then
                        MsgBox("الرصيد لا يسمح")
                        Return
                    End If
                Case 1
                    MsgBox(" الاعتيادي الذي أخذه الموظف هو " & Myconn.cur.Current("Holiday_number") & " اعتيادي في عام " & CDate(date_begin.Text).Year)

                    If Val(Myconn.cur2.Current("Etyade")) = 0 Then
                        MsgBox("لا توجد أجازات اعتيادية للموظف")
                        Return
                    Else
                        If Val(Val(Myconn.cur.Current("Holiday_number")) - Val(drg.CurrentRow.Cells(5).Value)) + Val(txt_H_number.Text) > Val(Myconn.cur2.Current("Etyade")) Then
                            MsgBox("الرصيد لا يسمح")
                            Return
                        End If
                    End If

            End Select
        Catch ex As Exception

        End Try

        Try
            Dim sql As String = "Update  Employees_Holiday set EmployeeID=@EmployeeID,Holiday_ID=@Holiday_ID,Holiday_number=@Holiday_number,H_date_begin=@H_date_begin,H_date_end=@H_date_end,Order_date=@Order_date,Notes=@Notes where ID=@ID"
            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@EmployeeID", cbo_Employee.SelectedValue)
                .AddWithValue("@Order_date", Format(CDate(txtDate.Text), "yyyy/MM/dd"))
                .AddWithValue("@Holiday_ID", cbo_Holiday.SelectedValue)
                .AddWithValue("@H_date_begin", Format(CDate(date_begin.Text), "yyyy/MM/dd"))
                .AddWithValue("@H_date_end", Format(CDate(date_end.Text), "yyyy/MM/dd"))
                .AddWithValue("@Holiday_number", txt_H_number.Text)
                .AddWithValue("@Notes", If(txtNotes.Text = Nothing, DBNull.Value, txtNotes.Text))
                .AddWithValue("@ID", CInt(drg.CurrentRow.Cells(10).Value))
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try
        Myconn.Filldataset("select h.Holiday_name,e.EmployeeName,j.jobname,s.Employee_Salary,* from Employees_Holiday a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees e on a.EmployeeID = e.EmployeeID 
                           left join Holidays h on a.Holiday_ID = h.Holiday_ID
                           left join Jobs j on e.jobID = j.jobID where a.ID =" & CInt(drg.CurrentRow.Cells(10).Value), "Employees_Holiday", Me)

        drg.CurrentRow.Cells(1).Value = Myconn.cur.Current("Order_date")
        drg.CurrentRow.Cells(2).Value = Myconn.cur.Current("EmployeeName")
        drg.CurrentRow.Cells(3).Value = Myconn.cur.Current("EmployeeID")
        drg.CurrentRow.Cells(4).Value = Myconn.cur.Current("jobname")
        drg.CurrentRow.Cells(5).Value = Myconn.cur.Current("Holiday_number")
        drg.CurrentRow.Cells(6).Value = Myconn.cur.Current("Holiday_name")
        drg.CurrentRow.Cells(7).Value = Myconn.cur.Current("H_date_begin")
        drg.CurrentRow.Cells(8).Value = Myconn.cur.Current("H_date_end")
        drg.CurrentRow.Cells(9).Value = Myconn.cur.Current("Notes")
        drg.CurrentRow.Cells(10).Value = Myconn.cur.Current("ID")
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
    Private Sub txtNotes_Enter(sender As Object, e As EventArgs) Handles txtNotes.Enter
        Myconn.langAR()
    End Sub
    Private Sub txt_H_number_TextChanged(sender As Object, e As EventArgs) Handles txt_H_number.TextChanged
        If txt_H_number.Text = Nothing Then Return


        date_end.Value = date_begin.Value.AddDays(CInt(txt_H_number.Text) - 1)
        If CDate(date_begin.Text).Year <> CDate(date_end.Text).Year Then
            MsgBox("بداية ونهاية الأجازة يجب أن تكون في نفس العام")
            txt_H_number.Text = Nothing
        End If
    End Sub
    Private Sub cbo_Holiday_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Holiday.SelectedIndexChanged
        If Not fin Then Return
        'If cbo_Holiday.SelectedIndex = -1 Then Return

    End Sub
End Class