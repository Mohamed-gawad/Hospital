Public Class frmUsers
    Dim myconn As New connect
    Dim fin As Boolean
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        myconn.Filldataset("select b.Main_menu_Text,c.Sub_menu_Text,a.ID,a.Add_oper,a.delet_oper,a.Updat_oper,a.Print_oper,a.Search_oper,a.Cancel_oper,a.Main_menuID,a.Sub_menuID,a.UserPassword,a.Full_control from User_Permissions a
                            left join Main_menu b on a.Main_menuID = b.Main_menuID
                            left join Sub_menu c on a.Sub_menuID = c.Sub_menuID where a.EmployeeID =" & CInt(cboEmployees.SelectedValue), "User_Permissions", Me)
        For i As Integer = 0 To myconn.cur.Count - 1
            drg.Rows.Add(New String() {i + 1, myconn.cur.Current(0), myconn.cur.Current(1), myconn.cur.Current(2), myconn.cur.Current(3), myconn.cur.Current(4), myconn.cur.Current(5), myconn.cur.Current(6), myconn.cur.Current(7), myconn.cur.Current(8)})
            myconn.cur.Position += 1
        Next
    End Sub
    Private Sub frmUsers_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label5.Left = 0
        Label5.Width = Me.Width
        myconn.Filldataset("Select * from User_Permissions where EmployeeID = " & CInt(My.Settings.user_ID), "User_Permissions", Me)
        If myconn.cur.Current("Full_control") = False Then
            btnSave.Enabled = myconn.cur.Current("Add_oper")
            btnPrint.Enabled = myconn.cur.Current("print_oper")
            btnDel.Enabled = myconn.cur.Current("delet_oper")
            btnUpdat.Enabled = myconn.cur.Current("updat_oper")
            btnCancel.Enabled = myconn.cur.Current("Cancel_oper")
        End If
        myconn.Filldataset("select * from User_Permissions", "User_Permissions", Me)
        fin = False
        myconn.Fillcombo1("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cboEmployees)
        myconn.Fillcombo2("select * from Main_menu", "Main_menu", "Main_menuID", "Main_menu_Text", Me, cboMain_menu)
        fin = True
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        cboEmployees.SelectedIndex = -1
        cboMain_menu.SelectedIndex = -1
        cboSub_menu.Text = ""
        txtPass.Text = ""
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        cboEmployees.SelectedIndex = -1
        cboMain_menu.SelectedIndex = -1
        cboSub_menu.Text = ""
        txtPass.Text = ""
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cboEmployees.SelectedIndex = -1 Then
            MessageBox.Show("من فضلك أدخل المستخدم", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If

        If txtPass.Text = "" Then
            MessageBox.Show("من فضلك أدخل كلمة المرور", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If
        If cboMain_menu.SelectedIndex = -1 Then
            MessageBox.Show("من فضلك أدخل القائمة الرئيسية", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If

        If cboSub_menu.SelectedIndex = -1 Then
            MessageBox.Show("من فضلك أدخل القائمة الفرعية", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Return
        End If

        Dim XX() As String = {cboEmployees.SelectedValue, txtPass.Text, cboMain_menu.SelectedValue, cboSub_menu.SelectedValue, "'" & CheckDelet.Checked.ToString & "'", "'" & CheckUpdat.Checked.ToString & "'", "'" & CheckPrint.Checked.ToString & "'", "'" & CheckAdd.Checked.ToString & "'", "'" & CheckSearch.Checked.ToString & "'", "'" & CheckFull.Checked.ToString & "'", "'" & CheckCancel.Checked.ToString & "'"}
        myconn.AddNewRecord("User_Permissions", XX)
        Fillgrd()

        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Try
            Dim Values() As String = {cboEmployees.SelectedValue, txtPass.Text, cboMain_menu.SelectedValue, cboSub_menu.SelectedValue, "'" & CheckDelet.Checked.ToString & "'", "'" & CheckUpdat.Checked.ToString & "'", "'" & CheckPrint.Checked.ToString & "'", "'" & CheckAdd.Checked.ToString & "'", "'" & CheckSearch.Checked.ToString & "'", "'" & CheckFull.Checked.ToString & "'", "'" & CheckCancel.Checked.ToString & "'"}
            Dim Mycolumes() As String = {"EmployeeID", "UserPassword", "Main_menuID", "Sub_menuID", "delet_oper", "Updat_oper", "Print_oper", "Add_oper", "Search_oper", "Full_control", "Cancel_oper"}
            myconn.UpdateRecord("User_Permissions", Mycolumes, Values, "ID", CInt(drg.CurrentRow.Cells(3).Value))
            Fillgrd()
            MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        Catch ex As Exception
            Return
        End Try
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("User_Permissions", "ID", CInt(drg.CurrentRow.Cells(3).Value))
            myconn.ClearAllText(Me, GroupBox1)
            Fillgrd()
        End If
    End Sub

    Private Sub cboMain_menu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMain_menu.SelectedIndexChanged
        If Not fin Then Return
        myconn.Fillcombo3("select * from Sub_menu where Main_menuID =" & CInt(cboMain_menu.SelectedValue), "Sub_menu", "Sub_menuID", "Sub_menu_Text", Me, cboSub_menu)
    End Sub
    Private Sub cboEmployees_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmployees.SelectedIndexChanged
        If Not fin Then Return
        Fillgrd()
        If myconn.dv.Count = 0 Then
            txtPass.Text = ""
            Return
        End If
        txtPass.Text = Me.BindingContext(myconn.dv).Current("UserPassword")
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        myconn.Filldataset("select * from User_Permissions where ID =" & CInt(drg.CurrentRow.Cells(3).Value), "User_Permissions", Me)
        CheckAdd.Checked = Me.BindingContext(myconn.dv).Current("Add_oper").ToString
        CheckDelet.Checked = Me.BindingContext(myconn.dv).Current("delet_oper").ToString
        CheckPrint.Checked = Me.BindingContext(myconn.dv).Current("Print_oper").ToString
        CheckUpdat.Checked = Me.BindingContext(myconn.dv).Current("Updat_oper").ToString
        CheckSearch.Checked = Me.BindingContext(myconn.dv).Current("Search_oper").ToString
        CheckFull.Checked = Me.BindingContext(myconn.dv).Current("Full_control").ToString
        CheckCancel.Checked = Me.BindingContext(myconn.dv).Current("Cancel_oper").ToString
        cboMain_menu.SelectedValue = myconn.cur.Current("Main_menuID")
        cboSub_menu.SelectedValue = myconn.cur.Current("Sub_menuID")
    End Sub
    Private Sub CheckFull_CheckedChanged(sender As Object, e As EventArgs) Handles CheckFull.CheckedChanged
        Try

            If CheckFull.Checked = True Then
                CheckAdd.Checked = True
                CheckDelet.Checked = True
                CheckPrint.Checked = True
                CheckUpdat.Checked = True
                CheckSearch.Checked = True
                CheckCancel.Checked = True

                CheckAdd.Enabled = False
                CheckDelet.Enabled = False
                CheckPrint.Enabled = False
                CheckUpdat.Enabled = False
                CheckSearch.Enabled = False
                CheckCancel.Enabled = False

                cboMain_menu.SelectedIndex = 13
                cboSub_menu.SelectedIndex = 0
                GroupBox4.Enabled = False
            Else
                CheckAdd.Checked = False
                CheckDelet.Checked = False
                CheckPrint.Checked = False
                CheckUpdat.Checked = False
                CheckSearch.Checked = False
                CheckCancel.Checked = False

                CheckAdd.Enabled = True
                CheckDelet.Enabled = True
                CheckPrint.Enabled = True
                CheckUpdat.Enabled = True
                CheckSearch.Enabled = True
                CheckCancel.Enabled = True

                cboSub_menu.SelectedIndex = -1
                cboMain_menu.SelectedIndex = -1

                GroupBox4.Enabled = True
            End If

        Catch ex As Exception

        End Try
    End Sub


End Class