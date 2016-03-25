Public Class frmAdd_Co
    Dim Myconn As New connect
    Dim X As Integer

    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        If cbo_bian.SelectedIndex = -1 Then
            MessageBox.Show("من فضلك أدخل نوع البيان", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        Select Case cbo_bian.SelectedIndex
            Case 0
                Myconn.Filldataset("select * from Co_Drug", "Co_Drug", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("Co_ID", "Co_Drug", txtID, Me)
                Myconn.Autocomplete("Co_Drug", "Co_Name", txtName)
            Case 1
                Myconn.Filldataset("select * from Drug_Groups", "Drug_Groups", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("GroupID", "Drug_Groups", txtID, Me)
                Myconn.Autocomplete("Drug_Groups", "GroupName", txtName)
            Case 2
                Myconn.Filldataset("select * from Drug_Origin", "Drug_Origin", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("Origin_ID", "Drug_Origin", txtID, Me)
                Myconn.Autocomplete("Drug_Origin", "Origin_Name", txtName)
            Case 3
                Myconn.Filldataset("select * from Drug_State", "Drug_State", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("State_ID", "Drug_State", txtID, Me)
                Myconn.Autocomplete("Drug_State", "State_Name", txtName)
            Case 4
                Myconn.Filldataset("select * from Max_Unit", "Max_Unit", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("Max_UnitID", "Max_Unit", txtID, Me)
                Myconn.Autocomplete("Max_Unit", "Max_Unit_Name", txtName)
            Case 5
                Myconn.Filldataset("select * from Min_Unit", "Min_Unit", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("Min_UnitID", "Min_Unit", txtID, Me)
                Myconn.Autocomplete("Min_Unit", "Min_Unit_Name", txtName)

        End Select


        txtName.Focus()

    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Select Case cbo_bian.SelectedIndex
            Case 0
                Myconn.Filldataset("select Co_Name,Co_ID,CoID from Co_Drug order by Co_Name", "Co_Drug", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(1)})
                    Myconn.cur.Position += 1
                Next
            Case 1
                Myconn.Filldataset("select GroupName,GroupID,ID from Drug_Groups order by GroupName", "Drug_Groups", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(1)})
                    Myconn.cur.Position += 1
                Next
            Case 2
                Myconn.Filldataset("select Origin_Name,Origin_ID,ID from Drug_Origin order by Origin_Name", "Drug_Origin", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(1)})
                    Myconn.cur.Position += 1
                Next
            Case 3
                Myconn.Filldataset("select State_Name,State_ID,ID from Drug_State order by State_Name", "Drug_State", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(1)})
                    Myconn.cur.Position += 1
                Next
            Case 4
                Myconn.Filldataset("select Max_Unit_Name,Max_UnitID,ID from Max_Unit order by Max_Unit_Name", "Max_Unit", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(1)})
                    Myconn.cur.Position += 1
                Next
            Case 5
                Myconn.Filldataset("select Min_Unit_Name,Min_UnitID,ID from Min_Unit order by Min_Unit_Name", "Min_Unit", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(1)})
                    Myconn.cur.Position += 1
                Next

        End Select

    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Select Case cbo_bian.SelectedIndex
            Case 0
                Dim Myfields() As String = {"Co_ID", "Co_Name"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Case 1
                Dim Myfields() As String = {"GroupID", "GroupName"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Case 2
                Dim Myfields() As String = {"Origin_ID", "Origin_Name"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Case 3
                Dim Myfields() As String = {"State_ID", "State_Name"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Case 4
                Dim Myfields() As String = {"Max_UnitID", "Max_Unit_Name"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Case 5
                Dim Myfields() As String = {"Min_UnitID", "Min_Unit_Name"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
        End Select
    End Sub
    Private Sub frmAdd_Co_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Enter AndAlso e.Control = True Then
            btnSave_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.N AndAlso e.Control = True Then
            btnNew_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub frmAdd_Co_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        Me.KeyPreview = True
        btnSave.Enabled = False
        txtName.Focus()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        btnSave.Enabled = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtName.Text = "" Then
            MessageBox.Show("من فضلك أدخل إسم البيان", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        Select Case X
            Case 0
                Dim XX() As String = {"'" & txtName.Text & "'", txtID.Text}
                Myconn.AddNewRecord("Co_Drug", XX)
            Case 1
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("Drug_Groups", XX)
            Case 2
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("Drug_Origin", XX)
            Case 3
                Dim XX() As String = {"'" & txtName.Text & "'", txtID.Text}
                Myconn.AddNewRecord("Drug_State", XX)
            Case 4
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("Max_Unit", XX)
            Case 5
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("Min_Unit", XX)
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
                Dim Mycolumes() As String = {"Co_Name"}
                Myconn.UpdateRecord("Co_Drug", Mycolumes, Values, "Co_ID", txtID.Text)
            Case 1
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"GroupName"}
                Myconn.UpdateRecord("Drug_Groups", Mycolumes, Values, "GroupID", txtID.Text)
            Case 2
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"Origin_Name"}
                Myconn.UpdateRecord("Drug_Origin", Mycolumes, Values, "Origin_ID", txtID.Text)
            Case 3
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"State_Name"}
                Myconn.UpdateRecord("Drug_State", Mycolumes, Values, "State_ID", txtID.Text)
            Case 4
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"Max_Unit_Name"}
                Myconn.UpdateRecord("Max_Unit", Mycolumes, Values, "Max_UnitID", txtID.Text)
            Case 5
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"Min_Unit_Name"}
                Myconn.UpdateRecord("Min_Unit", Mycolumes, Values, "Min_UnitID", txtID.Text)
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
                    Myconn.DeleteRecord("Co_Drug", "Co_ID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)
                    Fillgrd()
                Case 1
                    Myconn.DeleteRecord("Drug_Groups", "GroupID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)
                    Fillgrd()
                Case 2
                    Myconn.DeleteRecord("Drug_Origin", "Origin_ID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)
                    Fillgrd()
                Case 3
                    Myconn.DeleteRecord("Drug_State", "State_ID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)
                    Fillgrd()
                Case 4
                    Myconn.DeleteRecord("Max_Unit", "Max_UnitID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)
                    Fillgrd()
                Case 5
                    Myconn.DeleteRecord("Min_Unit", "Min_UnitID", CInt(drg.CurrentRow.Cells(2).Value))
                    Myconn.ClearAllText(Me, GroupBox1)
                    Fillgrd()

            End Select

        End If
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Select Case cbo_bian.SelectedIndex
            Case 0
                Myconn.Filldataset("select * from Co_Drug where Co_ID =" & CInt(drg.CurrentRow.Cells(2).Value), "Co_Drug", Me)
            Case 1
                Myconn.Filldataset("select * from Drug_Groups where GroupID =" & CInt(drg.CurrentRow.Cells(2).Value), "Drug_Groups", Me)
            Case 2
                Myconn.Filldataset("select * from Drug_Origin where Origin_ID =" & CInt(drg.CurrentRow.Cells(2).Value), "Drug_Origin", Me)
            Case 3
                Myconn.Filldataset("select * from Drug_State where State_ID =" & CInt(drg.CurrentRow.Cells(2).Value), "Drug_State", Me)
            Case 4
                Myconn.Filldataset("select * from Max_Unit where Max_UnitID =" & CInt(drg.CurrentRow.Cells(2).Value), "Max_Unit", Me)
            Case 5
                Myconn.Filldataset("select * from Min_Unit where Min_UnitID =" & CInt(drg.CurrentRow.Cells(2).Value), "Min_Unit", Me)
        End Select
        Binding()
    End Sub
    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtName.Enter
        Myconn.langAR()
    End Sub
    Private Sub cbo_bian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_bian.SelectedIndexChanged
        Select Case cbo_bian.SelectedIndex
            Case 0
                drg.Columns(1).HeaderText = "المصنع"
            Case 1
                drg.Columns(1).HeaderText = "الفئة"
            Case 2
                drg.Columns(1).HeaderText = "المنشأ"
            Case 3
                drg.Columns(1).HeaderText = "طبيعة الدواء"
            Case 4
                drg.Columns(1).HeaderText = "الوحدة الكبرى"
            Case 5
                drg.Columns(1).HeaderText = "الوحدة الصغرى"
        End Select
        NewRecord()
        Fillgrd()
    End Sub
End Class