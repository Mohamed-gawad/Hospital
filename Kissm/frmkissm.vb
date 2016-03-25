Public Class frmkissm
    Dim Myconn As New connect
    Private Sub frmkissm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label16.Left = 0
        Label16.Width = Me.Width
        Fillgrd()
        btnSave.Enabled = False
        txtName.Focus()
    End Sub
    Private Sub frmkissm_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        btnSave.Enabled = True
    End Sub
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.Filldataset("select * from specialization", "specialization", Me)
        Myconn.ClearAllText(Me, GroupBox1)
        Myconn.Autonumber("specializationID", "specialization", txtID, Me)
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtName.Text = "" Then
            MessageBox.Show("من فضلك أدخل إسم القسم", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'", "'" & Label3.Text & "'"}
        Myconn.AddNewRecord("specialization", XX)
        Fillgrd()
        Myconn.ClearAllText(Me, GroupBox1)
        btnSave.Enabled = False
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select * from specialization where kind = 'k'", "specialization", Me)
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(1), Myconn.cur.Current(0)})
            Myconn.cur.Position += 1
        Next
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Dim Myfields() As String = {"specializationID", "specialization"}
        Dim Mytxt() As TextBox = {txtID, txtName}
        Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset("select * from specialization where specializationID =" & CInt(drg.CurrentRow.Cells(2).Value), "specialization", Me)
        Binding()
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Dim Values() As String = {"'" & txtName.Text & "'"}
        Dim Mycolumes() As String = {"specialization"}
        Myconn.UpdateRecord("specialization", Mycolumes, Values, "specializationID", txtID.Text)
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Myconn.DeleteRecord("specialization", "specializationID", CInt(drg.CurrentRow.Cells(2).Value))
            Myconn.ClearAllText(Me, GroupBox1)
            Fillgrd()
        End If
    End Sub
    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtName.Enter
        Myconn.langAR()
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        Myconn.Arabiconly(e)
    End Sub
End Class