Imports System.Globalization
Imports System.Data.SqlClient
Public Class frmPayment
    Dim fin As Boolean
    Dim Myconn As New connect
    Dim x, y As Integer
    Dim st As String
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.ClearAllControls(GroupBox1, True)
        Myconn.Autonumber("Payment_ID", "Payment", txtID, Me)
        Myconn.Filldataset("select isnull(max(payment_num),0) as payment_num from Payment", "Payment", Me)
        If Myconn.cur.Current("payment_num") = 0 Then
            txtNum.ReadOnly = False
        Else
            txtNum.ReadOnly = True
            txtNum.Text = Myconn.cur.Current("payment_num") + 1
        End If
        dtb.Text = Today
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Try
            drg.Rows.Clear()
            Select Case x
            Case 0
                st = "where a.Payment_date ='" & Format(Today.Date, "yyyy/MM/dd") & "'"
            Case 1
                st = "where a.payment_ID =" & CInt(txtSearch.Text)
            Case 2
                st = "where a.payment_num =" & CInt(txtSearch.Text)
            Case 3
                st = "where a.Payment_date ='" & Format(CDate(txtSearch.Text), "yyyy/MM/dd") & "'"
        End Select

        Myconn.Filldataset("select a.State,a.ID,a.payment_ID,a.payment_num,a.Payment_date,a.payment_time,a.Amount ,a.Sid,
                            e.EmployeeName,s.specialization,i.itemName,r.RecipientName,u.EmployeeName from  [dbo].[Payment] a
                            left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                            left join [dbo].[specialization] s on a.specializationID = s.specializationID
                            left join [dbo].[payment_item] i on a.paymentID = i.paymentID
                            left join [dbo].[Employees] u on a.Users_ID = u.EmployeeID
                            left join [dbo].[Recipient] r on a.RecipientID = r.RecipientID " & st, "Payment", Me)

        If Myconn.cur.Count = 0 Then Return
        Dim V1 As Decimal
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = If(Myconn.cur.Current("Sid") = 0, "الايراد", "الخزنة")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("itemName")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("payment_ID")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Payment_date")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("payment_time")
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("payment_num")
                drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Amount")
                drg.Rows(i).Cells(8).Value = Myconn.cur.Current("specialization")
                drg.Rows(i).Cells(9).Value = Myconn.cur.Current("RecipientName")
                drg.Rows(i).Cells(10).Value = Myconn.cur.Current("EmployeeName")
                drg.Rows(i).Cells(11).Value = Myconn.cur.Current("State")
                drg.Rows(i).Cells(12).Value = Myconn.cur.Current("ID")

                If drg.Rows(i).Cells(11).Value = True Then
                    drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                    V1 += CDec(drg.Rows(i).Cells(6).Value)
                Else
                    drg.Rows(i).DefaultCellStyle.BackColor = Color.Red
                End If
                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg(drg, 7, Label14, Label15)
        Catch ex As Exception
            MsgBox("هناك خطأ")
        End Try
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Try
            Dim Myfields() As String = {"Payment_ID", "payment_num", "amount", "amount_ab", "notes"}
            Dim Mytxt() As TextBox = {txtID, txtNum, txtAmount, txtAB, txtNotes}
            Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Myconn.DateTPBinding("Payment_date", dtb)
            Myconn.comboBinding("paymentID", cboItem)
            Myconn.comboBinding("RecipientID", cboPreson)
            Myconn.comboBinding("specializationID", cboKissm)
            Myconn.comboBinding("EmployeeID", cboEmloyee)

        Catch ex As Exception

        End Try
    End Sub
    Sub Save_Recod()
        Try
            Dim sql As String = "INSERT INTO Payment(Payment_date,Payment_time,Payment_ID,Payment_num,Amount,Amount_ab,Notes,PermissionID,PaymentID,EmployeeID,EmployeeNID,RecipientID,SpecializationID,Users_ID,State,sid) 
                            VALUES(@Payment_date,@Payment_time,@Payment_ID,@Payment_num,@Amount,@Amount_ab,@Notes,@PermissionID,@PaymentID,@EmployeeID,@EmployeeNID,@RecipientID,@SpecializationID,@Users_ID,@State,@sid)"

            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@Payment_ID", txtID.Text)
                .AddWithValue("@Payment_date", Format(CDate(dtb.Text), "yyyy/MM/dd"))
                .AddWithValue("@Payment_time", Label22.Text)
                .AddWithValue("@Payment_num", txtNum.Text)
                .AddWithValue("@SpecializationID", cboKissm.SelectedValue)
                .AddWithValue("@EmployeeNID", If(txtNid.Text = Nothing, DBNull.Value, txtNid.Text))
                .AddWithValue("@EmployeeID", cboEmloyee.SelectedValue)
                .AddWithValue("@Amount", txtAmount.Text)
                .AddWithValue("@Amount_ab", txtAB.Text)
                .AddWithValue("@PermissionID", 2)
                .AddWithValue("@PaymentID", cboItem.SelectedValue)
                .AddWithValue("@RecipientID", cboPreson.SelectedValue)
                .AddWithValue("@Users_ID", My.Settings.user_ID)
                .AddWithValue("@Notes", If(txtNotes.Text = Nothing, DBNull.Value, txtNotes.Text))
                .AddWithValue("@State", 1)
                .AddWithValue("@sid", cbo_Sid.SelectedIndex)
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            Return
        End Try
    End Sub
    Sub Add_one_row()
        Try

            Select Case y
                Case 0 ' Save recodr
                    Myconn.Filldataset("select a.State,a.ID,a.payment_ID,a.payment_num,a.Payment_date,a.payment_time,a.Amount ,a.Sid,
                            e.EmployeeName,s.specialization,i.itemName,r.RecipientName,u.EmployeeName from  [dbo].[Payment] a
                            left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                            left join [dbo].[specialization] s on a.specializationID = s.specializationID
                            left join [dbo].[payment_item] i on a.paymentID = i.paymentID
                            left join [dbo].[Employees] u on a.Users_ID = u.EmployeeID
                            left join [dbo].[Recipient] r on a.RecipientID = r.RecipientID where payment_ID =" & CInt(txtID.Text), "Payment", Me)

                    drg.Rows.Add()
                    drg.Rows(drg.Rows.Count - 1).Cells(0).Value = drg.Rows.Count
                    drg.Rows(drg.Rows.Count - 1).Cells(1).Value = If(Myconn.cur.Current("Sid") = 0, "الايراد", "الخزنة")
                    drg.Rows(drg.Rows.Count - 1).Cells(2).Value = Myconn.cur.Current("itemName")
                    drg.Rows(drg.Rows.Count - 1).Cells(3).Value = Myconn.cur.Current("payment_ID")
                    drg.Rows(drg.Rows.Count - 1).Cells(4).Value = Myconn.cur.Current("Payment_date")
                    drg.Rows(drg.Rows.Count - 1).Cells(5).Value = Myconn.cur.Current("payment_time")
                    drg.Rows(drg.Rows.Count - 1).Cells(6).Value = Myconn.cur.Current("payment_num")
                    drg.Rows(drg.Rows.Count - 1).Cells(7).Value = Myconn.cur.Current("Amount")
                    drg.Rows(drg.Rows.Count - 1).Cells(8).Value = Myconn.cur.Current("specialization")
                    drg.Rows(drg.Rows.Count - 1).Cells(9).Value = Myconn.cur.Current("RecipientName")
                    drg.Rows(drg.Rows.Count - 1).Cells(10).Value = Myconn.cur.Current("EmployeeName")
                    drg.Rows(drg.Rows.Count - 1).Cells(11).Value = Myconn.cur.Current("State")
                    drg.Rows(drg.Rows.Count - 1).Cells(12).Value = Myconn.cur.Current("ID")

                    Myconn.DataGridview_MoveLast(drg, 3)

                Case 1 ' UpdateRecord
                    Myconn.Filldataset("select a.State,a.ID,a.payment_ID,a.payment_num,a.Payment_date,a.payment_time,a.Amount ,a.Sid,
                            e.EmployeeName,s.specialization,i.itemName,r.RecipientName,u.EmployeeName from  [dbo].[Payment] a
                            left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                            left join [dbo].[specialization] s on a.specializationID = s.specializationID
                            left join [dbo].[payment_item] i on a.paymentID = i.paymentID
                            left join [dbo].[Employees] u on a.Users_ID = u.EmployeeID
                            left join [dbo].[Recipient] r on a.RecipientID = r.RecipientID where payment_ID =" & CInt(drg.CurrentRow.Cells(12).Value), "Receipt", Me)

                    drg.CurrentRow.Cells(1).Value = If(Myconn.cur.Current("Sid") = 0, "الايراد", "الخزنة")
                    drg.CurrentRow.Cells(2).Value = Myconn.cur.Current("itemName")
                    drg.CurrentRow.Cells(3).Value = Myconn.cur.Current("payment_ID")
                    drg.CurrentRow.Cells(4).Value = Myconn.cur.Current("Payment_date")
                    drg.CurrentRow.Cells(5).Value = Myconn.cur.Current("payment_time")
                    drg.CurrentRow.Cells(6).Value = Myconn.cur.Current("payment_num")
                    drg.CurrentRow.Cells(7).Value = Myconn.cur.Current("Amount")
                    drg.CurrentRow.Cells(8).Value = Myconn.cur.Current("specialization")
                    drg.CurrentRow.Cells(9).Value = Myconn.cur.Current("RecipientName")
                    drg.CurrentRow.Cells(10).Value = Myconn.cur.Current("EmployeeName")
                    drg.CurrentRow.Cells(11).Value = Myconn.cur.Current("State")
                    drg.CurrentRow.Cells(12).Value = Myconn.cur.Current("ID")

                    If drg.CurrentRow.Cells(11).Value = True Then
                        drg.CurrentRow.DefaultCellStyle.BackColor = Color.LemonChiffon
                    Else
                        drg.CurrentRow.DefaultCellStyle.BackColor = Color.Red
                    End If
            End Select
            Myconn.Sum_drg(drg, 7, Label14, Label15)
        Catch ex As Exception
            MsgBox("هناك خطأ")
        End Try
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset("select * from Payment where ID =" & CInt(drg.CurrentRow.Cells(12).Value), "Payment", Me)
        cbo_Sid.SelectedIndex = Myconn.cur.Current("Sid")
        Binding()
        btnSave.Enabled = False
    End Sub
    Private Sub frmPayment_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub frmPayment_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label16.Left = 0
        Label16.Width = Me.Width
        Myconn.Filldataset("Select * from User_Permissions where EmployeeID = " & CInt(My.Settings.user_ID), "User_Permissions", Me)
        If Myconn.cur.Current("Full_control") = False Then
            btnSave.Enabled = Myconn.cur.Current("Add_oper")
            btnPrint.Enabled = Myconn.cur.Current("print_oper")
            btnDel.Enabled = Myconn.cur.Current("delet_oper")
            btnUpdat.Enabled = Myconn.cur.Current("updat_oper")
            btnSearch.Enabled = Myconn.cur.Current("Search_oper")
            btnCancel.Enabled = Myconn.cur.Current("Cancel_oper")
        End If
        Timer1.Start()
        x = 0
        Fillgrd()
        Myconn.Fillcombo("select * from payment_item", "payment_item", "paymentID", "itemName", Me, cboItem)
        Myconn.Fillcombo("select * from specialization", "specialization", "specializationID", "specialization", Me, cboKissm)
        Myconn.Fillcombo("select * from Recipient", "Recipient", "RecipientID", "RecipientName", Me, cboPreson)

        fin = False
        Myconn.Fillcombo("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cboEmloyee)
        fin = True
        Myconn.ClearAllControls(GroupBox1, True)
        dtb.Text = Today
        Myconn.DataGridview_MoveLast(drg, 2)
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Myconn.Filldataset("Select * from Payment where payment_num =" & txtNum.Text, "Payment", Me)
            If Myconn.dv.Count > 0 Then
                MessageBox.Show("رقم الإيصال مكرر", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

                Return
            End If

        Catch ex As Exception

        End Try

        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" And txt.Name IsNot "txtNotes" Then
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
        y = 0
        Add_one_row()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        NewRecord()

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click

        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            drg.Rows.Remove(drg.SelectedRows(0))
            Myconn.DeleteRecord("Payment", "ID", txtID.Text)
            Myconn.ClearAllControls(GroupBox1, True)

        End If
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim sql As String = "Update  Payment set State = @State where ID = @ID"
        Myconn.cmd = New SqlCommand(sql, Myconn.conn)

        If drg.CurrentRow.Cells(10).Value = True Then
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 0
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(11).Value)
            End With
        Else
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 1
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(11).Value)
            End With
        End If

        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()
        y = 1
        Add_one_row()
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Try
            Dim sql As String = "Update  Payment set Payment_date=@Payment_date,Payment_time=@Payment_time,Payment_ID=@Payment_ID,Payment_num=@Payment_num,Amount=@Amount,Amount_ab=@Amount_ab,
                                                     Notes=@Notes,PermissionID=@PermissionID,PaymentID=@PaymentID,EmployeeID=@EmployeeID,EmployeeNID=@EmployeeNID,RecipientID=@RecipientID,SpecializationID=@SpecializationID,Users_ID=@Users_ID,Sid=@Sid where ID=@ID"

            Myconn.cmd = New SqlCommand(sql, Myconn.conn)
            With Myconn.cmd.Parameters
                .AddWithValue("@Payment_ID", txtID.Text)
                .AddWithValue("@Payment_date", Format(CDate(dtb.Text), "yyyy/MM/dd"))
                .AddWithValue("@Payment_time", Label22.Text)
                .AddWithValue("@Payment_num", txtNum.Text)
                .AddWithValue("@SpecializationID", cboKissm.SelectedValue)
                .AddWithValue("@EmployeeNID", If(txtNid.Text = Nothing, DBNull.Value, txtNid.Text))
                .AddWithValue("@EmployeeID", cboEmloyee.SelectedValue)
                .AddWithValue("@Amount", txtAmount.Text)
                .AddWithValue("@Amount_ab", txtAB.Text)
                .AddWithValue("@PermissionID", 2)
                .AddWithValue("@PaymentID", cboItem.SelectedValue)
                .AddWithValue("@RecipientID", cboPreson.SelectedValue)
                .AddWithValue("@Users_ID", My.Settings.user_ID)
                .AddWithValue("@Notes", If(txtNotes.Text = Nothing, DBNull.Value, txtNotes.Text))
                .AddWithValue("@Sid", cbo_Sid.SelectedIndex)
                .AddWithValue("@ID", CInt(drg.CurrentRow.Cells(12).Value))
            End With
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            Return
        End Try
        y = 1
        Add_one_row()

        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Select Case cboSearch.SelectedIndex
            Case 0
                x = 1
            Case 1
                x = 2
            Case 2
                x = 3
        End Select
        Fillgrd()

    End Sub

    Private Sub txtNotes_Enter(sender As Object, e As EventArgs) Handles txtNotes.Enter
        Myconn.langAR()
    End Sub

    Private Sub txtNotes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNotes.KeyPress
        Myconn.Arabiconly(e)
    End Sub

    Private Sub txtAB_TextChanged(sender As Object, e As EventArgs) Handles txtAB.TextChanged

    End Sub

    Private Sub txtAmount_TextChanged_1(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        txtAB.Text = clsNumber.nTOword(txtAmount.Text)
    End Sub
    Private Sub cboEmloyee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmloyee.SelectedIndexChanged
        If Not fin Then Return
        If cboEmloyee.SelectedIndex = -1 Then Return
        Myconn.Filldataset("Select * from Employees where EmployeeID =" & CInt(cboEmloyee.SelectedValue), "Employees", Me)
        txtNid.DataBindings.Clear()
        txtNid.DataBindings.Add("text", Myconn.dv, "EmployeeNID")
    End Sub
    Private Sub txtRecive_Enter(sender As Object, e As EventArgs) Handles txtNotes.Enter
        Myconn.langAR()
    End Sub
    Private Sub txtNum_Leave(sender As Object, e As EventArgs) Handles txtNum.Leave
        If txtNum.Text = "" Then Return
        If btnSave.Enabled = True Then
            Try
                Myconn.Filldataset("Select * from Payment where payment_num =" & txtNum.Text, "Payment", Me)
                If Myconn.dv.Count > 0 Then
                    MessageBox.Show("رقم الإيصال مكرر", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    txtNum.Text = ""
                    txtNum.Focus()
                    Return
                End If

            Catch ex As Exception

            End Try
        Else

        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label22.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
End Class