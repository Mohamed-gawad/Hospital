Public Class frmSafe_data
    Dim Myconn As New connect
    Dim X As Integer
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        If cbo_bian.SelectedIndex = -1 Then
            MessageBox.Show("من فضلك أدخل نوع البند", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        Select Case cbo_bian.SelectedIndex
            Case 0 ' بند استلام
                Myconn.Filldataset("select * from receipt_item", "receipt_item", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("itemID", "receipt_item", txtID, Me)
                Myconn.Autocomplete("receipt_item", "itemName", txtName)
            Case 1 ' بند دفع
                Myconn.Filldataset("select * from payment_item", "payment_item", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("paymentID", "payment_item", txtID, Me)
                Myconn.Autocomplete("payment_item", "itemName", txtName)
            Case 2 ' اضافة قسم
                Myconn.Filldataset("select * from specialization", "specialization", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("specializationID", "specialization", txtID, Me)
                Myconn.Autocomplete("specialization", "specialization", txtName)
            Case 3 ' اضافة مستلم
                Myconn.Filldataset("select * from Recipient", "Recipient", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("RecipientID", "Recipient", txtID, Me)
                Myconn.Autocomplete("Recipient", "RecipientName", txtName)
            Case 4 ' اضفة بنك
                Myconn.Filldataset("select * from Banks", "Banks", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("Bank_ID", "Banks", txtID, Me)
                Myconn.Autocomplete("Banks", "Bank_name", txtName)
        End Select
        txtName.Focus()
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Select Case cbo_bian.SelectedIndex
            Case 0 ' بند استلام
                Myconn.Filldataset("select itemName,itemID from receipt_item order by itemName", "receipt_item", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(1)})
                    Myconn.cur.Position += 1
                Next
            Case 1 ' بند دفع
                Myconn.Filldataset("select itemName,paymentID from payment_item order by itemName", "payment_item", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(1)})
                    Myconn.cur.Position += 1
                Next
            Case 2 ' اضافة قسم
                Myconn.Filldataset("select * from specialization where kind = 'b'", "specialization", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(1), Myconn.cur.Current(0)})
                    Myconn.cur.Position += 1
                Next
            Case 3 ' اضافة مستلم
                Myconn.Filldataset("select * from Recipient where kind = 'M'", "Recipient", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(1), Myconn.cur.Current(0)})
                    Myconn.cur.Position += 1
                Next
            Case 4
                Myconn.Filldataset("select * from Banks", "Banks", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(1), Myconn.cur.Current(0)})
                    Myconn.cur.Position += 1
                Next

        End Select
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Select Case cbo_bian.SelectedIndex
            Case 0 ' بند استلام
                Dim Myfields() As String = {"itemID", "itemName"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Case 1 ' بند دفع
                Dim Myfields() As String = {"paymentID", "itemName"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Case 2 ' اضافة قسم
                Dim Myfields() As String = {"specializationID", "specialization"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Case 3 ' اضافة مستلم
                Dim Myfields() As String = {"RecipientID", "RecipientName"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Case 4 ' اضافة بنك
                Dim Myfields() As String = {"Bank_ID", "Bank_name"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)

        End Select
    End Sub

    Private Sub frmSafe_data_Load(sender As Object, e As EventArgs) Handles Me.Load
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
            Case 0 ' بند استلام
                Dim XX() As String = {"'" & txtName.Text & "'", txtID.Text}
                Myconn.AddNewRecord("receipt_item", XX)
            Case 1 ' بند دفع
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("payment_item", XX)
            Case 2 ' اضافة قسم
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'", "'b'"}
                Myconn.AddNewRecord("specialization", XX)
            Case 3 ' اضافة مستلم
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'", "'M'"}
                Myconn.AddNewRecord("Recipient", XX)
            Case 4 ' اضافة بنك
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("Banks", XX)

        End Select

        Fillgrd()
        Myconn.ClearAllText(Me, GroupBox1)
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
        NewRecord()
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Select Case cbo_bian.SelectedIndex
            Case 0 ' بند استلام
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"itemName"}
                Myconn.UpdateRecord("receipt_item", Mycolumes, Values, "itemID", txtID.Text)
            Case 1 ' بند دفع
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"itemName"}
                Myconn.UpdateRecord("payment_item", Mycolumes, Values, "paymentID", txtID.Text)
            Case 2 ' اضافة قسم
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"specialization"}
                Myconn.UpdateRecord("specialization", Mycolumes, Values, "specializationID", txtID.Text)
            Case 3 ' اضافة مستلم
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"RecipientName"}
                Myconn.UpdateRecord("Recipient", Mycolumes, Values, "RecipientID", txtID.Text)
            Case 4 ' اضافة بنك
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"Bank_name"}
                Myconn.UpdateRecord("Banks", Mycolumes, Values, "Bank_ID", txtID.Text)

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
                Case 0 ' بند استلام
                    Myconn.DeleteRecord("receipt_item", "itemID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)
                Case 1 ' بند دفع
                    Myconn.DeleteRecord("payment_item", "paymentID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)
                Case 2 ' اضافة قسم
                    Myconn.DeleteRecord("specialization", "specializationID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)
                Case 3 ' اضافة مستلم
                    Myconn.DeleteRecord("Recipient", "RecipientID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)
                Case 4 ' اضافة بنك
                    Myconn.DeleteRecord("Banks", "Bank_ID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)

            End Select
        End If
        Fillgrd()
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Select Case cbo_bian.SelectedIndex
            Case 0 ' بند استلام
                Myconn.Filldataset("select * from receipt_item where itemID =" & CInt(drg.CurrentRow.Cells(2).Value), "receipt_item", Me)
            Case 1 ' بند دفع
                Myconn.Filldataset("select * from payment_item where paymentID =" & CInt(drg.CurrentRow.Cells(2).Value), "payment_item", Me)
            Case 2 ' اضافة قسم
                Myconn.Filldataset("select * from specialization where specializationID =" & CInt(drg.CurrentRow.Cells(2).Value), "specialization", Me)
            Case 3 ' اضافة مستلم
                Myconn.Filldataset("select * from Recipient where RecipientID =" & CInt(drg.CurrentRow.Cells(2).Value), "Recipient", Me)
            Case 4 ' اضافة بنك
                Myconn.Filldataset("select * from Banks where Bank_ID =" & CInt(drg.CurrentRow.Cells(2).Value), "Banks", Me)

        End Select
        Binding()
    End Sub
    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtName.Enter
        Myconn.langAR()
    End Sub
    Private Sub cbo_bian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_bian.SelectedIndexChanged
        Select Case cbo_bian.SelectedIndex
            Case 0 ' بند استلام
                drg.Columns(1).HeaderText = "بند استلام"
            Case 1 ' بند دفع
                drg.Columns(1).HeaderText = "بند دفع"
            Case 2 ' إضافة قسم دفع
                drg.Columns(1).HeaderText = "القسم"
            Case 3 ' إضافة مستلم
                drg.Columns(1).HeaderText = "المستلم"
            Case 4 ' إضافة بنك
                drg.Columns(1).HeaderText = "البنك"

        End Select
        NewRecord()
        Fillgrd()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
    End Sub
    Private Sub cbo_bian_Enter(sender As Object, e As EventArgs) Handles cbo_bian.Enter
        Myconn.langAR()
    End Sub
End Class