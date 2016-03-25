Public Class frmCervices
    Dim Myconn As New connect
    Dim fin As Boolean
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.Filldataset2("select * from Cervices", "Cervices", Me)
        Myconn.ClearAllText(Me, GroupBox1)
        Myconn.Autonumber("CerviceID", "Cervices", txtID, Me)
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select * from Cervices where specializationID =" & CInt(cbo_Kissm.SelectedValue), "Cervices", Me)
        If Myconn.cur.Count = 0 Then Return

        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("CerviceName")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("CerviceID")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Cervice_Price")
            Myconn.cur.Position += 1
        Next
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        'Myconn.Filldataset("select * from Cervices where CerviceID =" & CInt(drg.CurrentRow.Cells(2).Value), "Cervices", Me)
        txtID.Text = Myconn.cur.Current("CerviceID")
        txtName.Text = Myconn.cur.Current("CerviceName")
        txtPrice.Text = If(IsDBNull(Myconn.cur.Current("Cervice_Price")), 0, Myconn.cur.Current("Cervice_Price"))
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset("select * from Cervices where CerviceID =" & CInt(drg.CurrentRow.Cells(2).Value), "Cervices", Me)
        Binding()
    End Sub

    Private Sub frmCervices_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label5.Left = 0
        Label5.Width = Me.Width

        btnSave.Enabled = False
        txtName.Focus()
        fin = False
        Myconn.Fillcombo1("select * from specialization where kind ='k'", "specialization", "specializationID", "specialization", Me, cbo_Kissm)
        fin = True
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        btnSave.Enabled = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtName.Text = "" Then
            MessageBox.Show("من فضلك أدخل إسم الخدمة", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        If txtPrice.Text = "" Then
            MessageBox.Show("من فضلك أدخل إسم سعر الخدمة", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        If cbo_Kissm.SelectedIndex = -1 Then
            MessageBox.Show("من فضلك أدخل إسم القسم", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'", cbo_Kissm.SelectedValue, "'" & txtPrice.Text & "'"}
        Myconn.AddNewRecord("Cervices", XX)

        Fillgrd()
        Myconn.ClearAllText(Me, GroupBox1)
        btnSave.Enabled = False
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        If txtName.Text = "" Then
            MessageBox.Show("من فضلك أدخل إسم الخدمة", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        If txtPrice.Text = "" Then
            MessageBox.Show("من فضلك أدخل إسم سعر الخدمة", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        If cbo_Kissm.SelectedIndex = -1 Then
            MessageBox.Show("من فضلك أدخل إسم القسم", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        Dim Values() As String = {"'" & txtName.Text & "'", cbo_Kissm.SelectedValue, "'" & txtPrice.Text & "'"}
        Dim Mycolumes() As String = {"CerviceName", "specializationID", "Cervice_Price"}
        Myconn.UpdateRecord("Cervices", Mycolumes, Values, "CerviceID", txtID.Text)

        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Myconn.DeleteRecord("Cervices", "CerviceID", CInt(drg.CurrentRow.Cells(2).Value))
            Myconn.ClearAllText(Me, GroupBox1)
            Fillgrd()
        End If
    End Sub
    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtName.Enter, txtPrice.Enter
        Myconn.langAR()
    End Sub

    Private Sub cbo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Kissm.SelectedIndexChanged
        If Not fin Then Return
        Fillgrd()
    End Sub
    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        Myconn.NumberOnly(txtPrice, e)
    End Sub
End Class