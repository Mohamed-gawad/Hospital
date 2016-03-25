Public Class frm_Add_Supplier
    Dim Myconn As New connect
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.Filldataset("select * from Supplier", "Supplier", Me)
        Myconn.ClearAllText(Me, GroupBox1)
        Myconn.Autonumber("Supplier_ID", "Supplier", txtID, Me)

    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select * from Supplier", "Supplier", Me)
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(2), Myconn.cur.Current(1)})
            Myconn.cur.Position += 1
        Next
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Dim Myfields() As String = {"Supplier_ID", "Supplier_Name"}
        Dim Mytxt() As TextBox = {txtID, txtName}
        Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)

    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset("select * from Supplier where Supplier_ID =" & CInt(drg.CurrentRow.Cells(2).Value), "Supplier", Me)
        Binding()
    End Sub
    Private Sub frm_Add_Supplier_Load(sender As Object, e As EventArgs) Handles Me.Load
        Fillgrd()
        btnSave.Enabled = False
        txtName.Focus()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        btnSave.Enabled = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtName.Text = "" Then
            MessageBox.Show("من فضلك أدخل إسم العميل", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
        Myconn.AddNewRecord("Supplier", XX)
        Fillgrd()
        Myconn.ClearAllText(Me, GroupBox1)
        btnSave.Enabled = False
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Dim Values() As String = {"'" & txtName.Text & "'"}
        Dim Mycolumes() As String = {"Supplier_Name"}
        Myconn.UpdateRecord("Supplier", Mycolumes, Values, "Supplier_ID", txtID.Text)
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Myconn.DeleteRecord("Supplier", "Supplier_ID", CInt(drg.CurrentRow.Cells(2).Value))
            Myconn.ClearAllText(Me, GroupBox1)
            Fillgrd()
        End If
    End Sub
    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtName.Enter
        Myconn.langAR()
    End Sub

End Class