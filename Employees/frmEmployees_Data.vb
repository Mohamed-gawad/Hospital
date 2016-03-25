Public Class frmEmployees_Data
    Dim Myconn As New connect

    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        If cbo_bian.SelectedIndex = -1 Then
            MessageBox.Show("من فضلك أدخل نوع البند", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        Select Case cbo_bian.SelectedIndex
            Case 0 ' وظيفة
                Myconn.Filldataset("select * from jobs", "jobs", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("jobID", "jobs", txtID, Me)
            Case 1 ' مدينة
                Myconn.Filldataset("select * from City", "City", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("CityID", "City", txtID, Me)

            Case 2 ' دولة
                Myconn.Filldataset("select * from Country", "Country", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("CountryID", "Country", txtID, Me)

            Case 3 ' جنس
                Myconn.Filldataset("select * from Gender", "Gender", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("GenderID", "Gender", txtID, Me)

            Case 4 ' حالة اجتماعية
                Myconn.Filldataset("select * from Status", "Status", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("StatusID", "Status", txtID, Me)

            Case 5 ' حالة موظف
                Myconn.Filldataset("select * from Employees_Status", "Employees_Status", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("State_ID", "Employees_Status", txtID, Me)
            Case 6 ' أجازة
                Myconn.Filldataset("select * from Holidays", "Holidays", Me)
                Myconn.ClearAllText(Me, GroupBox1)
                Myconn.Autonumber("Holiday_ID", "Holidays", txtID, Me)
        End Select


    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Select Case cbo_bian.SelectedIndex
            Case 0 ' وظيفة
                Myconn.Filldataset("select * from jobs", "jobs", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current("jobname"), Myconn.cur.Current("jobID")})
                    Myconn.cur.Position += 1
                Next
            Case 1 ' مدينة
                Myconn.Filldataset("select * from City", "City", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current("City"), Myconn.cur.Current("CityID")})
                    Myconn.cur.Position += 1
                Next
            Case 2 ' دولة
                Myconn.Filldataset("select * from Country", "Country", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current("Country"), Myconn.cur.Current("CountryID")})
                    Myconn.cur.Position += 1
                Next
            Case 3 ' جنس
                Myconn.Filldataset("select * from Gender", "Gender", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current("Gender"), Myconn.cur.Current("GenderID")})
                    Myconn.cur.Position += 1
                Next
            Case 4 ' حالة اجتماعية
                Myconn.Filldataset("select * from Status", "Status", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current("Status"), Myconn.cur.Current("StatusID")})
                    Myconn.cur.Position += 1
                Next
            Case 5 ' حالة موظف
                Myconn.Filldataset("select * from Employees_Status", "Employees_Status", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current("State_Name"), Myconn.cur.Current("State_ID")})
                    Myconn.cur.Position += 1
                Next
            Case 6 ' أجازة
                Myconn.Filldataset("select * from Holidays order by Holiday_name", "Holidays", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current("Holiday_name"), Myconn.cur.Current("Holiday_ID")})
                    Myconn.cur.Position += 1
                Next

        End Select


    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Select Case cbo_bian.SelectedIndex
            Case 0 ' وظيفة
                Dim Myfields() As String = {"jobID", "jobname"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Case 1 ' مدينة
                Dim Myfields() As String = {"CityID", "City"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)

            Case 2 ' دولة
                Dim Myfields() As String = {"CountryID", "Country"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)

            Case 3 ' جنس
                Dim Myfields() As String = {"GenderID", "Gender"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)

            Case 4 ' حالة اجتماعية
                Dim Myfields() As String = {"StatusID", "Status"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)

            Case 5 ' حالة موظف
                Dim Myfields() As String = {"State_ID", "State_Name"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
            Case 6 ' أجازة
                Dim Myfields() As String = {"Holiday_ID", "Holiday_name"}
                Dim Mytxt() As TextBox = {txtID, txtName}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
        End Select

    End Sub



    Private Sub frmEmployees_Data_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Left = 0
        Label3.Width = Me.Width
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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtName.Text = "" Then
            MessageBox.Show("من فضلك أدخل إسم الوظيفة", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        Select Case cbo_bian.SelectedIndex
            Case 0 ' وظيفة
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("jobs", XX)
            Case 1 ' مدينة
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("City", XX)

            Case 2 ' دولة
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("Country", XX)

            Case 3 ' جنس
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("Gender", XX)

            Case 4 ' حالة اجتماعية
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("Status", XX)

            Case 5 ' حالة موظف
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("Employees_Status", XX)
            Case 6 ' أجازة
                Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'"}
                Myconn.AddNewRecord("Holidays", XX)

        End Select

        Fillgrd()
        Myconn.ClearAllText(Me, GroupBox1)
        btnSave.Enabled = False
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Select Case cbo_bian.SelectedIndex
            Case 0 ' وظيفة
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"jobname"}
                Myconn.UpdateRecord("jobs", Mycolumes, Values, "jobID", txtID.Text)
            Case 1 ' مدينة
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"City"}
                Myconn.UpdateRecord("City", Mycolumes, Values, "CityID", txtID.Text)

            Case 2 ' دولة
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"Country"}
                Myconn.UpdateRecord("Country", Mycolumes, Values, "CountryID", txtID.Text)

            Case 3 ' جنس
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"Gender"}
                Myconn.UpdateRecord("Gender", Mycolumes, Values, "GenderID", txtID.Text)

            Case 4 ' حالة اجتماعية
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"Status"}
                Myconn.UpdateRecord("Status", Mycolumes, Values, "StatusID", txtID.Text)

            Case 5 ' حالة موظف
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"State_Name"}
                Myconn.UpdateRecord("Employees_Status", Mycolumes, Values, "State_ID", txtID.Text)
            Case 6 ' أجازة
                Dim Values() As String = {"'" & txtName.Text & "'"}
                Dim Mycolumes() As String = {"Holiday_name"}
                Myconn.UpdateRecord("Holidays", Mycolumes, Values, "Holiday_ID", txtID.Text)

        End Select
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Select Case cbo_bian.SelectedIndex
            Case 0 ' وظيفة
                Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
                If (result = DialogResult.No) Then
                    Return
                Else
                    Myconn.DeleteRecord("jobs", "jobID", CInt(drg.CurrentRow.Cells(2).Value))
                End If
            Case 1 ' مدينة
                Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
                If (result = DialogResult.No) Then
                    Return
                Else
                    Myconn.DeleteRecord("City", "CityID", CInt(drg.CurrentRow.Cells(2).Value))
                End If

            Case 2 ' دولة
                Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
                If (result = DialogResult.No) Then
                    Return
                Else
                    Myconn.DeleteRecord("Country", "CountryID", CInt(drg.CurrentRow.Cells(2).Value))
                End If

            Case 3 ' جنس
                Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
                If (result = DialogResult.No) Then
                    Return
                Else
                    Myconn.DeleteRecord("Gender", "GenderID", CInt(drg.CurrentRow.Cells(2).Value))
                End If

            Case 4 ' حالة اجتماعية
                Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
                If (result = DialogResult.No) Then
                    Return
                Else
                    Myconn.DeleteRecord("Status", "StatusID", CInt(drg.CurrentRow.Cells(2).Value))
                End If

            Case 5 ' حالة موظف
                Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
                If (result = DialogResult.No) Then
                    Return
                Else
                    Myconn.DeleteRecord("Employees_Status", "State_ID", CInt(drg.CurrentRow.Cells(2).Value))
                End If

        End Select
        Myconn.ClearAllText(Me, GroupBox1)
        Fillgrd()
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Select Case cbo_bian.SelectedIndex
            Case 0 ' وظيفة
                Myconn.Filldataset("select * from jobs where jobID =" & CInt(drg.CurrentRow.Cells(2).Value), "jobs", Me)

            Case 1 ' مدينة
                Myconn.Filldataset("select * from City where CityID =" & CInt(drg.CurrentRow.Cells(2).Value), "City", Me)

            Case 2 ' دولة
                Myconn.Filldataset("select * from Country where CountryID =" & CInt(drg.CurrentRow.Cells(2).Value), "Country", Me)

            Case 3 ' جنس
                Myconn.Filldataset("select * from Gender where GenderID =" & CInt(drg.CurrentRow.Cells(2).Value), "Gender", Me)

            Case 4 ' حالة اجتماعية
                Myconn.Filldataset("select * from Status where StatusID =" & CInt(drg.CurrentRow.Cells(2).Value), "StatusID", Me)

            Case 5 ' حالة موظف
                Myconn.Filldataset("select * from Employees_Status where State_ID =" & CInt(drg.CurrentRow.Cells(2).Value), "Employees_Status", Me)
            Case 6 ' أجازة
                Myconn.Filldataset("select * from Holidays where Holiday_ID =" & CInt(drg.CurrentRow.Cells(2).Value), "Holidays", Me)

        End Select
        Binding()
    End Sub


    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtName.Enter
        Myconn.langAR()
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        Myconn.Arabiconly(e)
    End Sub

    Private Sub cbo_bian_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_bian.SelectedIndexChanged
        Select Case cbo_bian.SelectedIndex
            Case 0 ' وظيفة
                drg.Columns(1).HeaderText = "الوظيفة"
            Case 1 ' مدينة
                drg.Columns(1).HeaderText = "المدينة"
            Case 2 ' دولة
                drg.Columns(1).HeaderText = "الدولة"
            Case 3 ' جنس
                drg.Columns(1).HeaderText = "الجنس"
            Case 4 ' حالة اجتماعية
                drg.Columns(1).HeaderText = "الحالة الاجتماعية"
            Case 5 ' حالة موظف
                drg.Columns(1).HeaderText = "حالة الموظف"
            Case 6 ' أجازة
                drg.Columns(1).HeaderText = "نوع الأجازة"

        End Select
        NewRecord()
        Fillgrd()
    End Sub
End Class