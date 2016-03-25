Public Class frmSetting
    Dim myconn As New connect
    Private Sub frmSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtName.Text = My.Settings.H_Name
        txtAddress.Text = My.Settings.H_address
        txtEmail.Text = My.Settings.H_Email
        txtFacebook.Text = My.Settings.H_Facebook
        txtFax.Text = My.Settings.H_Fax
        txtLicence.Text = My.Settings.H_licence
        txtOwner.Text = My.Settings.H_Owner
        txtTel.Text = My.Settings.H_tel
        picLogo.BackgroundImage = myconn.GetImageFromString(My.Settings.H_logo)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        My.Settings.H_Name = txtName.Text
        My.Settings.H_address = txtAddress.Text
        My.Settings.H_Email = txtEmail.Text
        My.Settings.H_Facebook = txtFacebook.Text
        My.Settings.H_Fax = txtFax.Text
        My.Settings.H_licence = txtLicence.Text
        My.Settings.H_Owner = txtOwner.Text
        My.Settings.H_logo = myconn.GetStringFromImage(picLogo.BackgroundImage)

        My.Settings.Save()
        MessageBox.Show("تم حفظ الإعدادات بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub picLogo_Click(sender As Object, e As EventArgs) Handles picLogo.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            picLogo.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName)

        End If
    End Sub
End Class