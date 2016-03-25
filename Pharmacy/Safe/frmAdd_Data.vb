Public Class frmAdd_Data
    Dim Myconn As New connect
    Dim X As Integer
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        If cbo_bian.SelectedIndex = -1 Then
            MessageBox.Show("من فضلك أدخل نوع البند", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        Select Case cbo_bian.SelectedIndex
            Case 0
                Myconn.Filldataset("select * from receipt_item", "receipt_item", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("itemID", "receipt_item", txtID, Me)
                Myconn.Autocomplete("receipt_item", "itemName", txtName)
            Case 1
                Myconn.Filldataset("select * from payment_item", "payment_item", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("paymentID", "payment_item", txtID, Me)
                Myconn.Autocomplete("payment_item", "itemName", txtName)
        End Select
        txtName.Focus()
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Select Case cbo_bian.SelectedIndex
            Case 0
                Myconn.Filldataset("select itemName,itemID from receipt_item order by itemName", "receipt_item", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(1)})
                    Myconn.cur.Position += 1
                Next
            Case 1
                Myconn.Filldataset("select itemName,paymentID from payment_item order by itemName", "payment_item", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(1)})
                    Myconn.cur.Position += 1
                Next
        End Select
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Select Case cbo_bian.SelectedIndex
            Case 0
                Dim Myfields() As String = {"itemID", "itemName"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Case 1
                Dim Myfields() As String = {"paymentID", "itemName"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
        End Select
    End Sub
    Private Sub frmAdd_Data_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        'If e.KeyCode = Keys.Enter AndAlso e.Control = True Then
        '    btnSave_Click(Nothing, Nothing)
        'ElseIf e.KeyCode = Keys.N AndAlso e.Control = True Then
        '    btnNew_Click(Nothing, Nothing)
        'ElseIf e.KeyCode = Keys.Enter Then
        '    SendKeys.Send("{Tab}")
        'End If
    End Sub

    Private Sub frmAdd_Data_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        Me.KeyPreview = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtName.Text = "" Then
            MessageBox.Show("من فضلك أدخل إسم البيان", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        Select Case cbo_bian.SelectedIndex
            Case 0
                Dim XX() As String = {"'" & txtName.Text & "'", txtID.Text}
                Myconn.AddNewRecord("receipt_item", XX)
            Case 1
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("payment_item", XX)

        End Select

        Fillgrd()
        Myconn.ClearAllText(Me, GroupBox1)
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
        NewRecord()
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Select Case cbo_bian.SelectedIndex
            Case 0
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"itemName"}
                Myconn.UpdateRecord("receipt_item", Mycolumes, Values, "itemID", txtID.Text)
            Case 1
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"itemName"}
                Myconn.UpdateRecord("payment_item", Mycolumes, Values, "paymentID", txtID.Text)

        End Select
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Select Case cbo_bian.SelectedIndex
                Case 0
                    Myconn.DeleteRecord("receipt_item", "itemID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)
                    Fillgrd()
                Case 1
                    Myconn.DeleteRecord("payment_item", "paymentID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)
                    Fillgrd()
            End Select
        End If
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Select Case cbo_bian.SelectedIndex
            Case 0
                Myconn.Filldataset("select * from receipt_item where itemID =" & CInt(drg.CurrentRow.Cells(2).Value), "receipt_item", Me)
            Case 1
                Myconn.Filldataset("select * from payment_item where paymentID =" & CInt(drg.CurrentRow.Cells(2).Value), "payment_item", Me)

        End Select
        Binding()
    End Sub
    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtName.Enter
        Myconn.langAR()
    End Sub
    Private Sub cbo_bian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_bian.SelectedIndexChanged
        Select Case cbo_bian.SelectedIndex
            Case 0
                drg.Columns(1).HeaderText = "بند استلام"
            Case 1
                drg.Columns(1).HeaderText = "بند دفع"
        End Select
        NewRecord()
        Fillgrd()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
    End Sub

End Class