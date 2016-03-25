Public Class frmOperation_Tools
    Dim myconn As New connect

    Private Sub frmOperation_Tools_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub frmOperation_Tools_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.KeyPreview = True
        Label5.Left = 0
        Label5.Width = Me.Width
        Fillgrd()
        btnCancel.Enabled = False
        btnSave.Enabled = False
        txtName.Focus()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        btnSave.Enabled = True
    End Sub
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        myconn.Filldataset("select * from Opreation_Tools", "Opreation_Tools", Me)

        Myconn.ClearAllText(Me, GroupBox1)
        myconn.Autonumber("Operation_Tool_ID", "Opreation_Tools", txtID, Me)
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtName.Text = "" Then
            MessageBox.Show("من فضلك أدخل إسم الوظيفة", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        Dim XX() As String = {txtID.Text, txtPrice.Text, "'" & txtName.Text & "'"}
        myconn.AddNewRecord("Opreation_Tools", XX)
        Fillgrd()
        Myconn.ClearAllText(Me, GroupBox1)
        btnCancel.Enabled = False
        btnSave.Enabled = False
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        myconn.Filldataset("select Operation_Tool_Name,Operation_Tool_ID,Operation_Tool_Price from Opreation_Tools", "Opreation_Tools", Me)
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(1), Myconn.cur.Current(2)})
            Myconn.cur.Position += 1
        Next
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Dim Myfields() As String = {"Operation_Tool_ID", "Operation_Tool_Name", "Operation_Tool_Price"}
        Dim Mytxt() As TextBox = {txtID, txtName, txtPrice}
        Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        myconn.Filldataset("select * from Opreation_Tools where Operation_Tool_ID =" & CInt(drg.CurrentRow.Cells(2).Value), "Opreation_Tools", Me)
        Binding()
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Dim Values() As String = {"'" & txtName.Text & "'", "'" & txtPrice.Text & "'"}
        Dim Mycolumes() As String = {"Operation_Tool_Name", "Operation_Tool_Price"}
        myconn.UpdateRecord("Opreation_Tools", Mycolumes, Values, "Operation_Tool_ID", txtID.Text)
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Opreation_Tools", "Operation_Tool_ID", CInt(drg.CurrentRow.Cells(2).Value))
            Myconn.ClearAllText(Me, GroupBox1)
            Fillgrd()
        End If
    End Sub
    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtName.Enter
        Myconn.langAR()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        btnCancel.Enabled = False
        btnSave.Enabled = False
        txtID.Text = ""
        txtName.Text = ""
        txtPrice.Text = ""
    End Sub
End Class