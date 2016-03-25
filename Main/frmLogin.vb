Imports System.Management
Public Class frmLogin
    Dim myconn As New connect
    Dim fin As Boolean
    Dim A As String
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim win32MgmtClass As ManagementClass
        win32MgmtClass = New ManagementClass("Win32_Processor")
        Dim processors As ManagementObjectCollection
        processors = win32MgmtClass.GetInstances()

        For Each processor As ManagementObject In processors
            'MessageBox.Show(processor("ProcessorID").ToString())
            A = processor("ProcessorID").ToString()
        Next

        Try
            myconn.Filldataset("select * from per_ID where ID = '" & A & "'", "per_ID", Me)
            If myconn.cur.Count = 0 Then
                MsgBox(" هذه النسخة غير مصرح باستخدامها قم بالاتصال بمصمم البرنامج  " & vbNewLine & "             على رقم 01125139439 لشراء حق استخدام البرنامج ")
                Close()
            End If
        Catch ex As Exception
            MsgBox("من فضلك قم بضيط اعدادات الاتصال")
            frmconnection.Show()
            Me.Close()
        End Try
        '-------------------------------------------------------------
        Try
            Me.KeyPreview = True
            fin = False
            myconn.Fillcombo1("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cboEmployees)
            fin = True
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Main_menu As ToolStripMenuItem
        Dim Sub_menu As ToolStripMenuItem

        If txtPass.Text = "" Then
            MsgBox("أدخل كلمة المرور")
            Return
        ElseIf txtPass.Text <> myconn.cur.Current("UserPassword") Then
            MsgBox("كلمة المرور غير صحيحة ...")
            txtPass.Text = ""
            Return
        End If

        If txtPass.Text = myconn.cur.Current("UserPassword") And myconn.cur.Current("Full_control") = "True" Then
            My.Settings.user_ID = cboEmployees.SelectedValue
            My.Settings.Save()
            Main.Show()
            Me.Hide()
        Else
            If txtPass.Text = myconn.cur.Current("UserPassword") Then
                For Each Main_menu In Main.MenuStrip.Items
                    Main_menu.Visible = False
                    For Each Sub_menu In Main_menu.DropDownItems
                        Sub_menu.Visible = False
                    Next
                Next

                For i As Integer = 0 To myconn.cur.Count - 1
                    For Each Main_menu In Main.MenuStrip.Items
                        If Main_menu.Name = myconn.cur.Current("Main_menu_Name").ToString Then
                            Main_menu.Visible = True
                            For Each Sub_menu In Main_menu.DropDownItems
                                If Sub_menu.Name = myconn.cur.Current("Sub_menu_Name").ToString Then
                                    Sub_menu.Visible = True
                                End If
                            Next
                        End If
                    Next
                    myconn.cur.Position += 1
                Next
                My.Settings.user_ID = cboEmployees.SelectedValue
                My.Settings.Save()
                Main.Show()
                Me.Hide()

            End If
        End If
    End Sub
    Private Sub cboEmployees_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmployees.SelectedIndexChanged
        If Not fin Then Return
        myconn.Filldataset("Select b.Main_menu_Name, c.Sub_menu_Name, a.ID, a.Add_oper, a.Main_menuID, a.Sub_menuID, a.UserPassword,a.Full_control from User_Permissions a
                            Left Join Main_menu b on a.Main_menuID = b.Main_menuID
                            Left Join Sub_menu c on a.Sub_menuID = c.Sub_menuID where a.EmployeeID =" & CInt(cboEmployees.SelectedValue), "User_Permissions", Me)

        If myconn.cur.Count = 0 Then
            MsgBox(" عفوا ليس لك صلاحيات للدخول")
            Return
        End If
    End Sub
    Private Sub txtPass_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPass.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button2_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmLogin_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Dim result = MessageBox.Show("هل تريد الخروج من البرنامج ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
            If (result = DialogResult.No) Then
                Return
            Else
                Close()
            End If

        End If
    End Sub
End Class