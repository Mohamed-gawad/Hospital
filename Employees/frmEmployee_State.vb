Imports System.Data.SqlClient

Public Class frmEmployee_State
    Dim Myconn As New connect
    Dim fin As Boolean
    Private Sub frmEmployee_State_Load(sender As Object, e As EventArgs) Handles Me.Load
        fin = False
        Myconn.Fillcombo("select EmployeeID,EmployeeName from  Employees order by EmployeeName", "Employees", "EmployeeID", "EmployeeName", Me, cbo_Employee)
        Myconn.Fillcombo("select * from  Employees_Status order by State_Name", "Employees_Status", "State_ID", "State_Name", Me, cboState)
        fin = True
    End Sub

    Private Sub cbo_Employee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Employee.SelectedIndexChanged
        If Not fin Then Return
        Myconn.Filldataset("select e.EmployeeID,s.State_Name from  Employees e left join Employees_Status S on e.State_ID = s.State_ID where e.EmployeeID =" & CInt(cbo_Employee.SelectedValue), "Employees", Me)
        txtEmployee_ID.Text = Myconn.cur.Current("EmployeeID")
        txtState_name.Text = Myconn.cur.Current("State_Name")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim sql As String = "Update  Employees set State_ID=@State_ID where EmployeeID=@EmployeeID"
            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@State_ID", cboState.SelectedValue)
                .AddWithValue("@EmployeeID", CInt(txtEmployee_ID.Text))
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try

        Try
            Dim sql As String = "Update  Employees_Salary set State_ID=@State_ID where EmployeeID=@EmployeeID"
            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@State_ID", cboState.SelectedValue)
                .AddWithValue("@EmployeeID", CInt(txtEmployee_ID.Text))
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Return
        End Try
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
End Class