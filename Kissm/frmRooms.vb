Public Class frmRooms
    Dim myconn As New connect

    Private Sub frmAnalysis_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.KeyPreview = True
        Label3.Left = 0
        Label3.Width = Me.Width
        Fillgrd()
        btnSave.Enabled = False
        btnCancel.Enabled = False
    End Sub
    Private Sub frmAnalysis_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        btnSave.Enabled = True
    End Sub
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        myconn.Filldataset("select * from Rooms", "Rooms", Me)

        myconn.ClearAllText(Me, GroupBox1)
        myconn.Autonumber("RoomNumber", "Rooms", txt_RoomID, Me)
        btnCancel.Enabled = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txt_RoomID.Text = "" Or txtPrice.Text = "" Then
            MessageBox.Show("من فضلك أدخل إسم الوظيفة", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        Dim XX() As String = {txt_RoomID.Text, txtPrice.Text}
        myconn.AddNewRecord("Rooms", XX)
        Fillgrd()
        myconn.ClearAllText(Me, GroupBox1)
        btnSave.Enabled = False
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        myconn.Filldataset("select RoomNumber,Price from Rooms", "Rooms", Me)
        For i As Integer = 0 To myconn.cur.Count - 1
            drg.Rows.Add(New String() {i + 1, myconn.cur.Current(0), myconn.cur.Current(1)})
            myconn.cur.Position += 1
        Next
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Dim Myfields() As String = {"RoomNumber", "Price"}
        Dim Mytxt() As TextBox = {txt_RoomID, txtPrice}
        myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        myconn.Filldataset("select * from Rooms where RoomNumber =" & CInt(drg.CurrentRow.Cells(1).Value), "Rooms", Me)
        Binding()
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Dim Values() As String = {txt_RoomID.Text, txtPrice.Text}
        Dim Mycolumes() As String = {"RoomNumber", "Price"}
        myconn.UpdateRecord("Rooms", Mycolumes, Values, "RoomNumber", CInt(drg.CurrentRow.Cells(1).Value))
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Rooms", "RoomNumber", CInt(drg.CurrentRow.Cells(1).Value))
            myconn.ClearAllText(Me, GroupBox1)
            Fillgrd()
        End If
    End Sub
    Private Sub txtName_Enter(sender As Object, e As EventArgs)
        myconn.langAR()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txt_RoomID.Text = ""

        txtPrice.Text = ""
        btnSave.Enabled = False
        btnNew.Enabled = True
        btnCancel.Enabled = False
    End Sub
End Class