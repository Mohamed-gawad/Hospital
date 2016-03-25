Imports System.IO
Imports System.Data.SqlClient
Public Class frmTry
    Dim Myconn As New connect
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Myconn.Saveimage(PictureBox1, "Employees", "EmployeeID", TextBox1.Text)
        MsgBox("ok")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Dim lines As String() = ListView1(",")
        'lines = lines.Where(Function(s) s.Trim() <> String.Empty).ToArray()




        'If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '    For Each selectedimages As String In OpenFileDialog1.FileNames


        '    Next
        'PictureBox1.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName)

        'End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Myconn.Retrieve_Image(PictureBox2, "Employees", "EmployeeID", TextBox1.Text, Me)
    End Sub

    Private Sub frmTry_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
     
    End Sub

    Private Sub frmTry_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.T AndAlso e.Control = True Then
            MsgBox("Ctrl + T")
        End If
    End Sub

    Private Sub frmTry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frmInternal_section.txtRoomPrice.Text = "ok"
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Handles btnBrowse2.Click
        ' Use an OpenFileDialog to enable the user to find an image to save to the 
        ' database. Provide a pipe-delimited set of pipe-delimited pairs of file 
        ' types that will appear in the dialog. Set the FilterIndex to the default 
        ' file type.
        With OpenFileDialog1
            .Multiselect = True
            .InitialDirectory = "C:\"
            .Filter = "All Files|*.*|Bitmaps|*.bmp|GIFs|*.gif|JPEGs|*.jpg"
            .FilterIndex = 2
        End With
        ' When the user clicks the Open button (DialogResult.OK is the only option;
        ' there is not DialogResult.Open), display the image centered in the 
        ' PictureBox and display the full path of the image.
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            With PictureBox1
                .Image = Image.FromFile(OpenFileDialog1.FileName)
                .SizeMode = PictureBoxSizeMode.CenterImage
                ' .BorderStyle = BorderStyle.Fixed3D
            End With
            '   lblFilePath.Text = OpenFileDialog1.FileName
            Label2.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        GroupBox1.Visible = False
        MsgBox(TextBox2.Text)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ComboBox1.Items.Add("5")
        ComboBox1.Items.Add("6")
        Dim x As String
        GroupBox1.Visible = True
        If IsNothing(ComboBox1.SelectedValue) Then
            x = "NULL"
        Else
            x = ComboBox1.SelectedValue.ToString

        End If
        MsgBox(x)
    End Sub

    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        Dim x As String
        'txt1.Text = Math.Round((Val(Math.Round(CDbl(txt1.Text), 2)) - Math.Round((Val(txtStrip.Text) / Val(txt4.Text)), 2)), 2)
        'txt2.Text = Fix(txt1.Text)
        'x = Math.Round((Val(txt1.Text) - Val(txt2.Text)), 2)
        'y = txt1.Text.Substring(txt1.Text.Length - 2, 2)
        'txt3.Text = Val(txt1.Text.Substring(txt1.Text.Length - 2, 2))
        'y = Math.Ceiling(CDec(txt1.Text))
        x = CalculateDecimals(CDbl(txt1.Text))

        'txt3.Text =
        MsgBox(x)
        'txt5.Text = txt2.Text & " علبة " & " و " & Math.Round((CDbl(txt3.Text) * CInt(txt4.Text)), 0) & " شريط "
    End Sub


    Function CalculateDecimals(input As Double)
        Dim positiveInput As Double
        positiveInput = Math.Abs(input)
        Return positiveInput - Math.Floor(positiveInput)
    End Function

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        txt3.Text = Math.Round((Val(txt1.Text) * Val(txt2.Text)), 0)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim sql As String = "INSERT INTO TBL(ID,rec) values(@ID,@rec)"

        Myconn.cmd = New SqlCommand(sql, Myconn.conn)
        With Myconn.cmd.Parameters
            .AddWithValue("@ID", If(txt1.Text = Nothing, DBNull.Value, txt1.Text))
            .AddWithValue("@rec", If(txt2.Text = Nothing, DBNull.Value, txt2.Text))
        End With
        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()

        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()
        MsgBox("ok")
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        MsgBox(Math.Truncate(CDec(txt1.Text)))

        MsgBox(Math.Truncate(Val(txt1.Text) / Val(txt2.Text)))

        MsgBox(Val(txt1.Text) Mod Val(txt2.Text))
    End Sub

    Private Sub frmTry_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If (e.Button = Windows.Forms.MouseButtons.Right) Then
            ContextMenuStrip1.Show(Me, e.Location)
        End If
    End Sub
End Class