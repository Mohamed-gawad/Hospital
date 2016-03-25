Public Class frmconnection

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        My.Settings.server = TextBox1.Text
        My.Settings.Database = TextBox2.Text
        If RadioButton1.Checked = True Then
            My.Settings.Mode = "Win"
            My.Settings.security = True
        Else
            My.Settings.Mode = "SQl"
            My.Settings.security = False
        End If

        My.Settings.ID = TextBox3.Text
        My.Settings.Password = TextBox4.Text
        My.Settings.Save()
        MessageBox.Show("تم حفظ الإعدادات بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)


    End Sub

    Private Sub frmconnection_Load(sender As Object, e As EventArgs) Handles Me.Load


        TextBox1.Text = My.Settings.server
        TextBox2.Text = My.Settings.Database

        If My.Settings.Mode = "Win" Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If
        TextBox3.Text = My.Settings.ID
        TextBox4.Text = My.Settings.Password
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        TextBox3.ReadOnly = False
        TextBox4.ReadOnly = False
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        TextBox3.Clear()
        TextBox4.Clear()

        TextBox3.ReadOnly = True
        TextBox4.ReadOnly = True

    End Sub
End Class