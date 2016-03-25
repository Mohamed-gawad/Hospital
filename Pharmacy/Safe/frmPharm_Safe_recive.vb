Imports System.Data.SqlClient

Public Class frmPharm_Safe_recive
    Dim Myconn As New connect
    Dim x As Integer
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.Autonumber("P_ID", "Pharm_Safe_recive", txtID, Me)
        txt_Date.Text = Today.Date
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        txtID.Text = Myconn.cur.Current("P_ID")
        txt_Date.Text = Format(CDate(Myconn.cur.Current("P_Date")), "ddd dd MMM yyyy")
        txtMoney.Text = Myconn.cur.Current("amount")
        txtMoney_abc.Text = Myconn.cur.Current("amount_abc")
        txtNote.Text = Myconn.cur.Current("Note")
        cbo_band.SelectedValue = Myconn.cur.Current("itemID")
        cbo_Customer.SelectedValue = Myconn.cur.Current("Customer_ID")
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Select Case x
            Case 0
                Myconn.Filldataset("Select * ,b.itemName,c.Customer_Name,d.EmployeeName from Pharm_Safe_recive a
                           left join receipt_item b on a.itemID = b.itemID
                           left join Customers c on a.Customer_ID = c.Customer_ID 
                            left join Employees d on a.User_ID = d.EmployeeID", "Pharm_Safe_recive", Me)

                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add()
                    drg.Rows(i).Cells(0).Value = i + 1
                    drg.Rows(i).Cells(1).Value = Myconn.cur.Current("itemName")
                    drg.Rows(i).Cells(2).Value = Myconn.cur.Current("P_Date")
                    drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Customer_Name")
                    drg.Rows(i).Cells(4).Value = Myconn.cur.Current("amount")
                    drg.Rows(i).Cells(5).Value = Myconn.cur.Current("amount_abc")
                    drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Note")
                    drg.Rows(i).Cells(7).Value = Myconn.cur.Current("ID")
                    drg.Rows(i).Cells(8).Value = Myconn.cur.Current("State")
                    drg.Rows(i).Cells(9).Value = Myconn.cur.Current("EmployeeName")
                    If drg.Rows(i).Cells(8).Value = True Then
                        drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                    Else
                        drg.Rows(i).DefaultCellStyle.BackColor = Color.Red
                    End If
                    Myconn.cur.Position += 1

                Next
            Case 1
                Myconn.Filldataset("Select * ,b.itemName,c.Customer_Name,d.EmployeeName from Pharm_Safe_recive a
                           left join receipt_item b on a.itemID = b.itemID
                           left join Customers c on a.Customer_ID = c.Customer_ID 
                            left join Employees d on a.User_ID = d.EmployeeID where a.P_ID =" & CInt(txtID.Text), "Pharm_Safe_recive", Me)
                drg.Rows.Add()
                Dim i As Integer = drg.Rows.Count - 1
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("itemName")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("P_Date")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Customer_Name")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("amount")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("amount_abc")
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Note")
                drg.Rows(i).Cells(7).Value = Myconn.cur.Current("ID")
                drg.Rows(i).Cells(8).Value = Myconn.cur.Current("State")
                drg.Rows(i).Cells(9).Value = Myconn.cur.Current("EmployeeName")
                If drg.Rows(i).Cells(8).Value = True Then
                    drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                Else
                    drg.Rows(i).DefaultCellStyle.BackColor = Color.Red
                End If
                Myconn.DataGridview_MoveLast(drg, 2)
            Case 2
                Myconn.Filldataset("Select * ,b.itemName,c.Customer_Name,d.EmployeeName from Pharm_Safe_recive a
                           left join receipt_item b on a.itemID = b.itemID
                           left join Customers c on a.Customer_ID = c.Customer_ID 
                            left join Employees d on a.User_ID = d.EmployeeID where a.ID =" & CInt(drg.CurrentRow.Cells(7).Value), "Pharm_Safe_recive", Me)

                drg.CurrentRow.Cells(1).Value = Myconn.cur.Current("itemName")
                drg.CurrentRow.Cells(2).Value = Myconn.cur.Current("P_Date")
                drg.CurrentRow.Cells(3).Value = Myconn.cur.Current("Customer_Name")
                drg.CurrentRow.Cells(4).Value = Myconn.cur.Current("amount")
                drg.CurrentRow.Cells(5).Value = Myconn.cur.Current("amount_abc")
                drg.CurrentRow.Cells(6).Value = Myconn.cur.Current("Note")
                drg.CurrentRow.Cells(7).Value = Myconn.cur.Current("ID")
                drg.CurrentRow.Cells(8).Value = Myconn.cur.Current("State")
                drg.CurrentRow.Cells(9).Value = Myconn.cur.Current("EmployeeName")
                If drg.CurrentRow.Cells(8).Value = True Then
                    drg.CurrentRow.DefaultCellStyle.BackColor = Color.LemonChiffon
                Else
                    drg.CurrentRow.DefaultCellStyle.BackColor = Color.Red
                End If
        End Select


    End Sub
    Sub SaveKind()
        Dim sql As String = "INSERT INTO Pharm_Safe_recive(P_ID,P_Date,itemID,amount,amount_abc,Note,State,PermissionID,Customer_ID,User_ID) 
                            VALUES(@P_ID,@P_Date,@itemID,@amount,@amount_abc,@Note,@State,@PermissionID,@Customer_ID,@User_ID)"

        Myconn.cmd = New SqlCommand(sql, Myconn.conn)

        With Myconn.cmd.Parameters
            .Add("@P_ID", SqlDbType.Int).Value = txtID.Text
            .Add("@P_Date", SqlDbType.NChar).Value = Format(CDate(txt_Date.Text), "yyyy/MM/dd")
            .Add("@itemID", SqlDbType.Int).Value = cbo_band.SelectedValue
            .Add("@amount", SqlDbType.Decimal).Value = txtMoney.Text
            .Add("@amount_abc", SqlDbType.Text).Value = txtMoney_abc.Text
            .Add("@Note", SqlDbType.Text).Value = txtNote.Text
            .Add("@State", SqlDbType.Bit).Value = 1
            .Add("@PermissionID", SqlDbType.TinyInt).Value = 1
            .Add("@Customer_ID", SqlDbType.TinyInt).Value = cbo_Customer.SelectedValue
            .Add("@User_ID", SqlDbType.Int).Value = My.Settings.user_ID
        End With
        Try
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            Return
        End Try

        x = 1
        Fillgrd()

    End Sub
    Private Sub frmPharm_Safe_recive_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label5.Left = 0
        Label5.Width = Me.Width
        Me.KeyPreview = True
        Myconn.Fillcombo("select * from receipt_item order by itemName", "receipt_item", "itemID", "itemName", Me, cbo_band)
        Myconn.Fillcombo("select * from Customers order by Customer_Name", "Customer_Name", "Customer_ID", "Customer_Name", Me, cbo_Customer)
        x = 0
        Fillgrd()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Clear()
        NewRecord()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" And txt.Name IsNot "txtNote" Then
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

        If txtMoney.Text <= 0 Then
            ErrorProvider1.SetError(txtMoney, "المبلغ غير صحيح")
            MessageBox.Show("من فضلك المبلغ غير صحيح ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        SaveKind()

        Clear()
        NewRecord()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Myconn.DeleteRecord("Pharm_Safe_recive", "ID", CInt(drg.CurrentRow.Cells(7).Value))
            x = 0
            Fillgrd()
        End If
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Dim sql As String = "Update  Pharm_Safe_recive set P_ID=@P_ID,P_Date=@P_Date,itemID=@itemID,amount=@amount,amount_abc=@amount_abc,Note=@Note,Customer_ID=@Customer_ID,User_ID=@User_ID where ID =@ID"

        Myconn.cmd = New SqlCommand(sql, Myconn.conn)

        With Myconn.cmd.Parameters
            .Add("@P_ID", SqlDbType.Int).Value = txtID.Text
            .Add("@P_Date", SqlDbType.NChar).Value = Format(CDate(txt_Date.Text), "yyyy/MM/dd")
            .Add("@itemID", SqlDbType.Int).Value = cbo_band.SelectedValue
            .Add("@amount", SqlDbType.Decimal).Value = txtMoney.Text
            .Add("@amount_abc", SqlDbType.Text).Value = txtMoney_abc.Text
            .Add("@Note", SqlDbType.Text).Value = txtNote.Text
            .Add("@Customer_ID", SqlDbType.Int).Value = cbo_Customer.SelectedValue
            .Add("@User_ID", SqlDbType.Int).Value = My.Settings.user_ID
            .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(7).Value)
        End With

        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()

        x = 2
        Fillgrd()

        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim sql As String = "Update  Pharm_Safe_recive set State = @State where ID = @ID"
        Myconn.cmd = New SqlCommand(sql, Myconn.conn)

        If drg.CurrentRow.Cells(8).Value = True Then
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 0
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(7).Value)
            End With
        Else
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 1
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(7).Value)
            End With
        End If
        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()
        x = 2
        Fillgrd()

    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset("Select * ,b.itemName,c.Customer_Name from Pharm_Safe_recive a
                           left join receipt_item b on a.itemID = b.itemID
                           left join Customers c on a.Customer_ID = c.Customer_ID where a.ID =" & CInt(drg.CurrentRow.Cells(7).Value), "Pharm_Safe_recive", Me)
        Binding()

    End Sub
    Private Sub txtMoney_TextChanged(sender As Object, e As EventArgs) Handles txtMoney.TextChanged
        txtMoney_abc.Text = clsNumber.nTOword(txtMoney.Text)
    End Sub
    Sub Clear()
        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                txt.Text = ""
            End If
        Next
    End Sub
End Class