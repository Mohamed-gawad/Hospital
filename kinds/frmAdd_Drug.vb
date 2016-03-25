
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing.Printing
Public Class frmAdd_Drug
    Private WithEvents pdPrint As PrintDocument
    Private PrintDocType As String = "Barcode"
    Private StrPrinterName As String = "Microsoft XPS Document Writer"
    Dim fin As Boolean
    Dim Myconn As New connect
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.Autonumber("Drug_ID", "Drugs", txtKind_ID, Me)
        txtKind_ID.Text = Format(CInt(txtKind_ID.Text), "00000000")

    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select *,b.Co_Name,c.Origin_Name,d.GroupName,e.State_Name from Drugs a
                           left join Co_Drug b on a.Co_ID = b.Co_ID
                           left join Drug_Origin c on a.Origin_ID = c.Origin_ID
                           left join Drug_State e on a.State_ID = e.State_ID
                           left join Drug_Groups d on a.GroupID = d.GroupID", "Drugs", Me)
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Co_Name")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Drug_Name")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Drug_ID")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Drug_Price")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Sales_tax")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Pharmacist_Price")
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Real_Discound")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Origin_Name")
            drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Parcod")
            'drg.Rows(i).Cells(10).Value = ImageFromStream(Myconn.cur.Current("Parcod_imag"))
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("GroupName")
            Myconn.cur.Position += 1
        Next
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Dim Myfields() As String = {"Drug_ID", "Drug_Name", "Drug_Price", "Min_Unit_number", "Min_Unit_price", "Sales_tax", "Pharmacist_Price", "Shortage"}
        Dim Mytxt() As TextBox = {txtKind_ID, txtKind_Name, txtKind_Price, txtItem_num, txtPrice_min_item, txtSales_Tax, txtPharmacist_Price, txtShortage}
        Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)

        Myconn.comboBinding("Co_ID", cbo_co)
        Myconn.comboBinding("GroupID", cboGroup)
        Myconn.comboBinding("Max_UnitID", cboMax_item)
        Myconn.comboBinding("Min_UnitID", cboMin_item)
        Myconn.comboBinding("Origin_ID", cbo_Origin)
        Myconn.comboBinding("State_ID", cboKind_State)
    End Sub
    Sub SaveKind()
        Dim sql As String = "INSERT INTO Drugs(Drug_ID,Drug_Name,Drug_Price,Co_ID,GroupID,Max_UnitID,Min_UnitID,Min_Unit_number,Min_Unit_price,Origin_ID,Sales_tax,Parcod,Note,State_ID,Pharmacist_Price,Amount,Real_Discound,Shortage) 
                            VALUES(@Drug_ID,@Drug_Name,@Drug_Price,@Co_ID,@GroupID,@Max_UnitID,@Min_UnitID,@Min_Unit_number,@Min_Unit_price,@Origin_ID,@Sales_tax,@Parcod,@Note,@State_ID,@Pharmacist_Price,@Amount,@Real_Discound,@Shortage)"

        'Dim content As Byte() = ImageToStream(pic_parcode)

        Myconn.cmd = New SqlCommand(sql, Myconn.conn)
        With Myconn.cmd.Parameters
            .Add("@Drug_ID", SqlDbType.Int).Value = txtKind_ID.Text
            .Add("@Drug_Name", SqlDbType.NVarChar).Value = txtKind_Name.Text
            .Add("@Drug_Price", SqlDbType.Decimal).Value = txtKind_Price.Text
            .Add("@Co_ID", SqlDbType.Int).Value = cbo_co.SelectedValue
            .Add("@GroupID", SqlDbType.Int).Value = cboGroup.SelectedValue
            .Add("@Max_UnitID", SqlDbType.Int).Value = cboMax_item.SelectedValue
            .Add("@Min_UnitID", SqlDbType.Int).Value = cboMin_item.SelectedValue
            .Add("@Min_Unit_number", SqlDbType.Int).Value = txtItem_num.Text
            .Add("@Min_Unit_price", SqlDbType.Decimal).Value = txtPrice_min_item.Text
            .Add("@Origin_ID", SqlDbType.Int).Value = cbo_Origin.SelectedValue
            .Add("@Sales_tax", SqlDbType.Decimal).Value = txtSales_Tax.Text
            .Add("@Parcod", SqlDbType.NVarChar).Value = txtParcode.Text
            .Add("@Note", SqlDbType.Text).Value = "Null"
            '.Add("@Parcod_imag", SqlDbType.Image).Value = content
            .Add("@State_ID", SqlDbType.Int).Value = cboKind_State.SelectedValue
            .Add("@Pharmacist_Price", SqlDbType.Decimal).Value = txtPharmacist_Price.Text
            .Add("@Amount", SqlDbType.Decimal).Value = 0
            .Add("@Real_Discound", SqlDbType.Decimal).Value = Math.Round((((Val(txtKind_Price.Text) - (Val(txtPharmacist_Price.Text) + Val(txtSales_Tax.Text))) / Val(txtKind_Price.Text)) * 100), 2)
            .Add("@Shortage", SqlDbType.Decimal).Value = txtShortage.Text
        End With
        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()

        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()
        Myconn.Filldataset("select *,b.Co_Name,c.Origin_Name,d.GroupName,e.State_Name from Drugs a
                           left join Co_Drug b on a.Co_ID = b.Co_ID
                           left join Drug_Origin c on a.Origin_ID = c.Origin_ID
                           left join Drug_State e on a.State_ID = e.State_ID
                           left join Drug_Groups d on a.GroupID = d.GroupID  where a.Drug_ID =" & CInt(txtKind_ID.Text), "Drugs", Me)
        drg.Rows.Add()
        Dim i As Integer = drg.Rows.Count - 1
        drg.Rows(i).Cells(0).Value = i + 1
        drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Co_Name")
        drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Drug_Name")
        drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Drug_ID")
        drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Drug_Price")
        drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Sales_tax")
        drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Pharmacist_Price")
        drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Real_Discound")
        drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Origin_Name")
        drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Parcod")
        'drg.Rows(i).Cells(10).Value = ImageFromStream(Myconn.cur.Current("Parcod_imag"))
        drg.Rows(i).Cells(10).Value = Myconn.cur.Current("GroupName")
    End Sub
    Private Function ImageToStream(pic As PictureBox) As Byte()
        Dim stream As New MemoryStream()
        Try
            pic.BackgroundImage.Save(stream, Imaging.ImageFormat.Jpeg)
        Catch ex As Exception
            MsgBox("failed")
        End Try
        Return stream.GetBuffer()
    End Function
    Private Function ImageFromStream(image_code As Object) As Image
        Dim re As Image
        Try
            Dim arr As Byte()
            arr = CType(image_code, Byte())
            Dim memory As New MemoryStream(arr)
            re = Image.FromStream(memory)
            Return re
        Catch ex As Exception
            MsgBox("failed")
        End Try

    End Function
    Private Sub frmAdd_Drug_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Enter AndAlso e.Control = True Then
            btnSave_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.N AndAlso e.Control = True Then
            btnNew_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub frmAdd_Drug_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Select Case WW

            Case 0
                Myconn.Fillcombo("select * from Co_Drug order by co_name", "Co_Drug", "Co_ID", "Co_Name", Me, cbo_co)
            Case 1
                Myconn.Fillcombo("select * from Drug_Groups", "Drug_Groups", "GroupID", "GroupName", Me, cboGroup)
            Case 2
                Myconn.Fillcombo("select * from Drug_Origin", "Drug_Origin", "Origin_ID", "Origin_Name", Me, cbo_Origin)
            Case 3
                Myconn.Fillcombo("select * from Drug_State order by State_Name", "Drug_State", "State_ID", "State_Name", Me, cboKind_State)
            Case 4
                Myconn.Fillcombo("select * from Max_Unit", "Max_Unit", "Max_UnitID", "Max_Unit_Name", Me, cboMax_item)
            Case 5
                Myconn.Fillcombo("select * from Min_Unit", "Min_Unit", "Min_UnitID", "Min_Unit_Name", Me, cboMin_item)

        End Select

    End Sub
    Private Sub frmAdd_Drug_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label5.Left = 0
        Label5.Width = Me.Width
        Me.KeyPreview = True
        Myconn.Autocomplete("Drugs", "Drug_Name", txtKind_Name)
        Myconn.Fillcombo("select * from Co_Drug order by co_name", "Co_Drug", "Co_ID", "Co_Name", Me, cbo_co)
        Myconn.Fillcombo("select * from Drug_Groups", "Drug_Groups", "GroupID", "GroupName", Me, cboGroup)
        Myconn.Fillcombo("select * from Max_Unit", "Max_Unit", "Max_UnitID", "Max_Unit_Name", Me, cboMax_item)
        Myconn.Fillcombo("select * from Min_Unit", "Min_Unit", "Min_UnitID", "Min_Unit_Name", Me, cboMin_item)
        Myconn.Fillcombo("select * from Drug_Origin", "Drug_Origin", "Origin_ID", "Origin_Name", Me, cbo_Origin)
        Myconn.Fillcombo("select * from Drug_State order by State_Name", "Drug_State", "State_ID", "State_Name", Me, cboKind_State)

        Fillgrd()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Myconn.ClearAllControls(GroupBox1, True)
        NewRecord()
        btnSave.Enabled = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" Then
                    ErrorProvider1.SetError(txt, "أكمل البيانات")
                    MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return
                End If
            ElseIf TypeOf txt Is ComboBox Then
                If txt.Text = "" Then
                    ErrorProvider1.SetError(txt, "أكمل البيانات")
                    MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return

                End If
            End If
        Next

        SaveKind()

        Myconn.DataGridview_MoveLast(drg, 3)
        Myconn.Autocomplete("Drugs", "Drug_Name", txtKind_Name)
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

        btnNew_Click(Nothing, Nothing)

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim x As String
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            x = Val(txtKind_ID.Text).ToString
            Myconn.DeleteRecord("Drugs", "Drug_ID", x)
            Myconn.ClearAllText(Me, GroupBox1)

            Fillgrd()
        End If
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Dim SQL As String
        'Dim content As Byte() = ImageToStream(pic_parcode)
        SQL = "Update Drugs set Drug_Name = @Drug_Name, Drug_Price = @Drug_Price, Co_ID = @Co_ID, GroupID = @GroupID,Max_UnitID = @Max_UnitID, Min_UnitID = @Min_UnitID, Min_Unit_number = @Min_Unit_number, Min_Unit_price = @Min_Unit_price, Origin_ID = @Origin_ID,  Sales_tax = @Sales_tax,Parcod = @Parcod, Note = @Note ,State_ID = @State_ID ,Pharmacist_Price = @Pharmacist_Price ,Shortage = @Shortage where Drug_ID = @Drug_ID"

        Myconn.cmd = New SqlCommand(SQL, Myconn.conn)
        With Myconn.cmd.Parameters
            .Add("@Drug_Name", SqlDbType.NVarChar).Value = txtKind_Name.Text
            .Add("@Drug_Price", SqlDbType.Decimal).Value = txtKind_Price.Text
            .Add("@Co_ID", SqlDbType.Int).Value = cbo_co.SelectedValue
            .Add("@GroupID", SqlDbType.Int).Value = cboGroup.SelectedValue
            .Add("@Max_UnitID", SqlDbType.Int).Value = cboMax_item.SelectedValue
            .Add("@Min_UnitID", SqlDbType.Int).Value = cboMin_item.SelectedValue
            .Add("@Min_Unit_number", SqlDbType.Int).Value = txtItem_num.Text
            .Add("@Min_Unit_price", SqlDbType.Decimal).Value = txtPrice_min_item.Text
            .Add("@Origin_ID", SqlDbType.Int).Value = cbo_Origin.SelectedValue
            .Add("@Sales_tax", SqlDbType.Decimal).Value = txtSales_Tax.Text
            .Add("@Parcod", SqlDbType.NVarChar).Value = CStr(txtParcode.Text)
            .Add("@Note", SqlDbType.Text).Value = "Null"
            '.Add("@Parcod_imag", SqlDbType.Image).Value = content
            .Add("@State_ID", SqlDbType.Int).Value = cboKind_State.SelectedValue
            .Add("@Pharmacist_Price", SqlDbType.Decimal).Value = txtPharmacist_Price.Text
            .Add("@Shortage", SqlDbType.Decimal).Value = txtShortage.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = CInt(txtKind_ID.Text)
        End With

        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()

        'Fillgrd()
        Myconn.Filldataset("select *,b.Co_Name,c.Origin_Name,d.GroupName,e.State_Name from Drugs a
                           left join Co_Drug b on a.Co_ID = b.Co_ID
                           left join Drug_Origin c on a.Origin_ID = c.Origin_ID
                           left join Drug_State e on a.State_ID = e.State_ID
                           left join Drug_Groups d on a.GroupID = d.GroupID  where a.Drug_ID =" & CInt(txtKind_ID.Text), "Drugs", Me)
        drg.CurrentRow.Cells(1).Value = Myconn.cur.Current("Co_Name")
        drg.CurrentRow.Cells(2).Value = Myconn.cur.Current("Drug_Name")
        drg.CurrentRow.Cells(3).Value = Myconn.cur.Current("Drug_ID")
        drg.CurrentRow.Cells(4).Value = Myconn.cur.Current("Drug_Price")
        drg.CurrentRow.Cells(5).Value = Myconn.cur.Current("Sales_tax")
        drg.CurrentRow.Cells(6).Value = Myconn.cur.Current("Pharmacist_Price")
        drg.CurrentRow.Cells(7).Value = Myconn.cur.Current("Real_Discound")
        drg.CurrentRow.Cells(8).Value = Myconn.cur.Current("Origin_Name")
        drg.CurrentRow.Cells(9).Value = Myconn.cur.Current("Parcod")
        drg.CurrentRow.Cells(10).Value = ImageFromStream(Myconn.cur.Current("Parcod_imag"))
        drg.CurrentRow.Cells(11).Value = Myconn.cur.Current("GroupName")

        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset("select * from Drugs where Drug_ID =" & CInt(drg.CurrentRow.Cells(3).Value), "Drugs", Me)
        Binding()
        btnSave.Enabled = False
    End Sub
    Private Sub txtKind_Name_Enter(sender As Object, e As EventArgs) Handles txtKind_Name.Enter
        Myconn.langAR()
    End Sub
    Private Sub txtKind_ID_TextChanged(sender As Object, e As EventArgs) Handles txtKind_ID.TextChanged
        If txtKind_ID.Text = "" Then Return
        ErrorProvider1.Clear()
        txtKind_ID.Text = Format(CInt(txtKind_ID.Text), "00000000")
        txtParcode.Text = Format(CInt(txtKind_ID.Text), "00000000")
    End Sub
    Private Sub txtParcode_TextChanged(sender As Object, e As EventArgs) Handles txtParcode.TextChanged
        ErrorProvider1.Clear()
        Barcode.Text = "*" & txtParcode.Text.Trim & "*"
    End Sub
    Private Sub txtItem_num_TextChanged(sender As Object, e As EventArgs) Handles txtItem_num.TextChanged
        ErrorProvider1.Clear()
        If txtItem_num.Text = "" Then
            txtPrice_min_item.Text = ""
            Return
        End If
        txtPrice_min_item.Text = Math.Round((Val(txtKind_Price.Text) / Val(txtItem_num.Text)), 2)
    End Sub
    Private Sub txtKind_Name_TextChanged(sender As Object, e As EventArgs) Handles txtKind_Name.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtKind_Price_TextChanged(sender As Object, e As EventArgs) Handles txtKind_Price.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub cbo_co_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_co.SelectedIndexChanged, cboKind_State.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub cboGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGroup.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub cboMax_item_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMax_item.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub cboMin_item_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMin_item.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtPrice_min_item_TextChanged(sender As Object, e As EventArgs) Handles txtPrice_min_item.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub cbo_Origin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Origin.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtReduce_pharm_TextChanged(sender As Object, e As EventArgs)
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtShortage_TextChanged(sender As Object, e As EventArgs) Handles txtShortage.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtSales_Tax_TextChanged(sender As Object, e As EventArgs) Handles txtSales_Tax.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtAdd_tax_TextChanged(sender As Object, e As EventArgs)
        ErrorProvider1.Clear()
    End Sub
    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        If txtKind_Price.Text = "" Or 0 Then
            MsgBox("أدخل سعر الجمهور")
            Return
        End If

        If txtSales_Tax.Text = "" Then
            MsgBox("أدخل ضريبة المبيعات")
            Return
        End If

        Dim x, y As String
        Dim A, B As Double
        x = InputBox("أدخل خصم الصيدلي", "حساب سعر الصيدلي")
        y = InputBox("أدخل الخصم الاضافي", "حساب سعر الصيدلي")
        A = ((Val(txtKind_Price.Text) - Val(txtSales_Tax.Text)) * ((100 - Val(x)) / 100))
        B = Math.Round((A - (Val(Val(y) / 100) * A)), 4)
        txtPharmacist_Price.Text = B

    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        'Try
        '    pdPrint = New PrintDocument
        '    pdPrint.PrinterSettings.PrinterName = StrPrinterName
        '    pdPrint.PrintController = New StandardPrintController
        '    If pdPrint.PrinterSettings.IsValid Then
        '        pdPrint.DocumentName = PrintDocType
        '        pdPrint.Print()
        '    End If
        'Catch ex As Exception
        'End Try


        Select Case cboPrint.SelectedIndex
            Case 0

            Case 1
                Dim frm As New frmReportViewer("الباركود")
                Dim rpt As New rpt_Barcode
                Dim table As New DataTable
                For i As Integer = 1 To 4
                    Dim x As String
                    x = Format(i, "00")
                    table.Columns.Add(x)
                Next
                For y As Integer = 0 To 1
                    table.Rows.Add()
                    table.Rows(y)(0) = My.Settings.Barcode_line1
                    table.Rows(y)(1) = "*" & txtParcode.Text.Trim & "*"
                    table.Rows(y)(2) = txtParcode.Text & Space(2) & "EXP : " & " - PT " & txtKind_Price.Text
                    table.Rows(y)(3) = txtKind_Name.Text
                Next
                rpt.SetDataSource(table)
                frm.CrystalReportViewer1.ReportSource = rpt
                frm.CrystalReportViewer1.Refresh()
                frm.Show()
                frm.CrystalReportViewer1.Zoom(300)
        End Select
    End Sub
    Private Sub pdPrint_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) Handles pdPrint.PrintPage
        ''Dim img As Image = pic_parcode.BackgroundImage
        'Dim ScaleFac As Integer = 100
        'While (ScaleFac * img.Width / img.HorizontalResolution > e.PageBounds.Width Or ScaleFac * img.Height / img.VerticalResolution > e.PageBounds.Height) And ScaleFac > 2
        '    ScaleFac -= 1
        'End While
        'Dim sz As New SizeF(ScaleFac * img.Width / img.HorizontalResolution, ScaleFac * img.Height / img.VerticalResolution)
        'Dim p As New PointF((e.PageBounds.Width - sz.Width) / 2, (e.PageBounds.Height - sz.Height) / 2)

        ''e.Graphics.DrawImage(img, 0, 0, e.PageBounds.Width, e.PageBounds.Height)

        'e.Graphics.DrawImage(img, p)
    End Sub
    Private Sub txtKind_Name_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKind_Name.KeyPress
        If e.KeyChar = "أ" OrElse e.KeyChar = "إ" OrElse e.KeyChar = "لإ" OrElse e.KeyChar = "لأ" Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtItem_num_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItem_num.KeyPress
        Myconn.NumberOnly(txtItem_num, e)
    End Sub
    Private Sub txtPharmacist_Price_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPharmacist_Price.KeyPress
        Myconn.NumberOnly(txtPharmacist_Price, e)
    End Sub
    Private Sub txtKind_Price_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKind_Price.KeyPress
        Myconn.NumberOnly(txtKind_Price, e)
    End Sub
    Private Sub txtShortage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtShortage.KeyPress
        Myconn.NumberOnly(txtShortage, e)
    End Sub
    Private Sub txtSales_Tax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSales_Tax.KeyPress
        Myconn.NumberOnly(txtSales_Tax, e)
    End Sub
    Private Sub txtPharmacist_Price_Leave(sender As Object, e As EventArgs) Handles txtPharmacist_Price.Leave
        If Val(txtPharmacist_Price.Text) > Val(txtKind_Price.Text) Then
            MsgBox("سعر الصيدلي الذي تم ادخاله غير صحيح")
            txtPharmacist_Price.Focus()
            Return
        End If
    End Sub

    Public Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        drg.ClearSelection()
        For W As Integer = 0 To drg.Rows.Count - 1

            If drg.Rows(W).Cells(3).Value.ToString.Equals(txtSearch.Text, StringComparison.CurrentCultureIgnoreCase) Then
                drg.Rows(W).Cells(3).Selected = True
                drg.CurrentCell = drg.SelectedCells(3)
                Exit For
            End If
        Next

        If txtSearch.Text = "" Then
            drg.Rows(0).Cells(3).Selected = True
            drg.CurrentCell = drg.SelectedCells(3)
        End If
        If txtSearch.Text = "" Then Return
        drg_CellClick(Nothing, Nothing)
    End Sub

    Private Sub txtKind_Name_KeyUp(sender As Object, e As KeyEventArgs) Handles txtKind_Name.KeyUp
        If e.KeyCode = Keys.Enter Then
            Myconn.Filldataset("select * from Drugs where Drug_Name = '" & txtKind_Name.Text & "'", "Drugs", Me)
            Binding()
        End If

    End Sub
End Class