Public Class frmBarcode_setting
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.Barcode_line1 = TextBox1.Text
        My.Settings.Barcode_line1_v = CheckBox1.Checked
        My.Settings.Barcode_line2_v = CheckBox2.Checked
        My.Settings.Barcode_line3_v = CheckBox3.Checked
        My.Settings.Barcode_line4_v = CheckBox4.Checked
        My.Settings.Save()
        MessageBox.Show("تم حفظ الإعدادات بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub

    Private Sub frmBarcode_setting_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox1.Text = My.Settings.Barcode_line1
        CheckBox1.Checked = My.Settings.Barcode_line1_v
        CheckBox2.Checked = My.Settings.Barcode_line2_v
        CheckBox3.Checked = My.Settings.Barcode_line3_v
        CheckBox4.Checked = My.Settings.Barcode_line4_v
    End Sub
End Class