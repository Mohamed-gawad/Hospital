
Imports System.Data.SqlClient
Imports System.IO
Public Class frmPharm_Purchases_bill
    Dim fin As Boolean
    Dim fin2 As Boolean
    Dim Myconn As New connect
    Dim x, y, Q As Integer
    Dim A, R As Double
    Dim S As Boolean
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.Autonumber("Bill_ID", "Drug_Purchases", txtBill_ID, Me)
    End Sub
    Sub TextBindingdata(frm As Form, grb As GroupBox, Fields() As String, txt() As TextBox)

        For i As Integer = 0 To Fields.Count - 1
            txt(i).DataBindings.Clear()
            txt(i).DataBindings.Add("text", Myconn.dv1, Fields(i))
        Next
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        A = 0
        If cbo_Drug.SelectedIndex = -1 Then Return
        If Not fin Then Return
        Try
            Myconn.Filldataset1("select a.Drug_ID,a.Drug_Name,a.Drug_Price,a.Sales_tax,a.Pharmacist_Price,isnull(a.Real_Discound, 0) As Real_Discound ,a.Parcod,a.Min_Unit_number,d.Max_Unit_Name,e.Min_Unit_Name,isnull(b.Pucr_amount, 0) As Drug_Purchases,isnull((isnull(c.Amount_max,0) * a.Min_Unit_number + isnull(c.Amount_min,0)),0) As Sales,
                                        ((isnull(b.Pucr_amount,0) * a.Min_Unit_number) - isnull((isnull(c.Amount_max,0) * a.Min_Unit_number + isnull(c.Amount_min,0)),0))  as rest  from Drugs a
                                         Left Join(select Drug_ID,(sum(Drug_Amount) +  sum(Drug_Bonus)) As Pucr_amount,state from [dbo].[Drug_Purchases] GROUP BY Drug_ID,state  having state = 'true' )b
                                         On a.Drug_ID = b.Drug_ID
                                         Left Join(select Drug_ID, sum(Amount_max) as Amount_max,sum(Amount_min) as Amount_min, state from [dbo].[Drug_Sales] GROUP BY Drug_ID,  state  having state = 'true' ) c
                                         On a.Drug_ID = c.Drug_ID 
                                         Left Join Max_Unit d on a.Max_UnitID=d.Max_UnitID
                                         Left Join Min_Unit e on a.Min_UnitID=e.Min_UnitID
                                         GROUP BY A.Drug_ID, A.Drug_Name, b.Pucr_amount, c.Amount_max,c.Amount_min, A.Drug_Price, A.Real_Discound, A.Parcod, A.Min_Unit_number, d.Max_Unit_Name, e.Min_Unit_Name,a.Sales_tax,a.Pharmacist_Price
                                         having ((isnull(b.Pucr_amount,0) * a.Min_Unit_number) - isnull((isnull(c.Amount_max,0) * a.Min_Unit_number + isnull(c.Amount_min,0)),0))  >= 0 And a.Drug_ID =" & CInt(cbo_Drug.SelectedValue), "Drugs", Me)

            Dim Myfields() As String = {"Drug_Name", "Drug_Price", "Sales_tax", "Pharmacist_Price", "Real_Discound"}
            Dim Mytxt() As TextBox = {txtDrug, txtPublic_Price, txtSales_tax, txtPharmacist_Price1, txtReal_Discound}
            TextBindingdata(Me, GroupBox3, Myfields, Mytxt)

            txtPharm_discount1.Text = Math.Round((((Val(txtPublic_Price.Text) - (Val(txtSales_tax.Text) + Val(txtPharmacist_Price1.Text))) / Val(txtPublic_Price.Text)) * 100), 2)
            Check1_CheckedChanged(Nothing, Nothing)

            R = Myconn.cur1.Current("Real_Discound")
            txtStock_amount.Text = If(Myconn.cur1.Current("rest") Mod Myconn.cur1.Current("Min_Unit_number") = 0, Math.Truncate(Myconn.cur1.Current("rest") / Myconn.cur1.Current("Min_Unit_number")) & Space(1) & Myconn.cur1.Current("Max_Unit_Name"), Math.Truncate(Myconn.cur1.Current("rest") / Myconn.cur1.Current("Min_Unit_number")) & Space(1) & Myconn.cur1.Current("Max_Unit_Name") & " و " & Myconn.cur1.Current("rest") Mod Myconn.cur1.Current("Min_Unit_number") & Space(1) & Myconn.cur1.Current("Min_Unit_Name"))

        Catch ex As Exception
            MsgBox(ex)
        End Try
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Select Case x
            Case 0
                If txtSearch.Text = Nothing Then Return
                Myconn.Filldataset("select  * ,c.Co_Name,b.Drug_Name,d.EmployeeName from Drug_Purchases a
                           left join Drugs b on a.Drug_ID = b.Drug_ID
                            left join Employees d on a.User_ID = d.EmployeeID
                           left join Co_Drug c on b.Co_ID = c.Co_ID where a.Bill_ID =" & CInt(txtSearch.Text), "Drug_Purchases", Me)
                If Myconn.cur.Count = 0 Then
                    MessageBox.Show("هذا المسلسل غير موجود ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return
                End If
                txtBill_ID.Text = Myconn.cur.Current("Bill_ID")
                txtBill_num.Text = Myconn.cur.Current("Bill_number")
                dtp1.Text = Myconn.cur.Current("Today_Date")
                bill_date.Text = Myconn.cur.Current("bill_date")
                cbo_Supplier.SelectedValue = Myconn.cur.Current("Supplier_ID")
            Case 1
                If txtSearch.Text = Nothing Then Return
                Myconn.Filldataset("select  * ,c.Co_Name,b.Drug_Name,d.EmployeeName from Drug_Purchases a
                           left join Drugs b on a.Drug_ID = b.Drug_ID
                            left join Employees d on a.User_ID = d.EmployeeID
                           left join Co_Drug c on b.Co_ID = c.Co_ID where a.Bill_number =" & "'" & txtSearch.Text & "'", "Drug_Purchases", Me)
                If Myconn.cur.Count = 0 Then
                    MessageBox.Show("هذه الرقم غير موجود ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return
                End If
                txtBill_ID.Text = Myconn.cur.Current("Bill_ID")
                txtBill_num.Text = Myconn.cur.Current("Bill_number")
                dtp1.Text = Myconn.cur.Current("Today_Date")
                bill_date.Text = Myconn.cur.Current("bill_date")
                cbo_Supplier.SelectedValue = Myconn.cur.Current("Supplier_ID")
            Case 2
                Myconn.Filldataset("select  * ,c.Co_Name,b.Drug_Name,d.EmployeeName,b.Parcod from Drug_Purchases a
                           left join Drugs b on a.Drug_ID = b.Drug_ID
                            left join Employees d on a.User_ID = d.EmployeeID
                           left join Co_Drug c on b.Co_ID = c.Co_ID where a.Bill_ID =" & CInt(txtBill_ID.Text), "Drug_Purchases", Me)
        End Select

        Dim V As Decimal
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Co_Name")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Drug_Name")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Drug_ID")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Drug_exp")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Public_Price")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Drug_Amount")
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Drug_Bonus")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Sales_tax")
            drg.Rows(i).Cells(9).Value = (Myconn.cur.Current("Drug_Amount") + Myconn.cur.Current("Drug_Bonus")) * Myconn.cur.Current("Sales_tax")
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Pharmacist_discount")
            drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Pharmacist_Price")
            drg.Rows(i).Cells(12).Value = drg.Rows(i).Cells(11).Value + drg.Rows(i).Cells(8).Value
            drg.Rows(i).Cells(13).Value = drg.Rows(i).Cells(11).Value * Myconn.cur.Current("Drug_Amount")
            drg.Rows(i).Cells(14).Value = drg.Rows(i).Cells(13).Value + drg.Rows(i).Cells(9).Value
            drg.Rows(i).Cells(15).Value = (Myconn.cur.Current("Drug_Amount") + Myconn.cur.Current("Drug_Bonus")) * Myconn.cur.Current("Public_Price")
            drg.Rows(i).Cells(16).Value = Myconn.cur.Current("Earnings")
            drg.Rows(i).Cells(17).Value = Math.Round((((Myconn.cur.Current("Public_Price") - drg.Rows(i).Cells(12).Value) / Myconn.cur.Current("Public_Price")) * 100), 2)
            drg.Rows(i).Cells(18).Value = Math.Round((((Myconn.cur.Current("Public_Price") - drg.Rows(i).Cells(12).Value) / drg.Rows(i).Cells(12).Value) * 100), 2)
            drg.Rows(i).Cells(19).Value = Myconn.cur.Current("ID")
            drg.Rows(i).Cells(20).Value = Myconn.cur.Current("State")
            drg.Rows(i).Cells(21).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(22).Value = Myconn.cur.Current("Parcod")
            If drg.Rows(i).Cells(20).Value = True Then
                drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            Else
                drg.Rows(i).DefaultCellStyle.BackColor = Color.Red
                V += CDec(drg.Rows(i).Cells(14).Value)
            End If
            Myconn.cur.Position += 1
        Next

        Myconn.Sum_drg2(drg, 15, Label39)
        Myconn.Sum_drg2(drg, 13, Label40)
        Myconn.Sum_drg2(drg, 9, Label41)
        Myconn.Sum_drg2(drg, 14, Label42)
        Myconn.Sum_drg2(drg, 16, Label43)
        Label38.Text = V
        Label44.Text = Math.Round(((Val(Label43.Text) / Val(Label39.Text)) * 100), 2) & " %"
        Label45.Text = Math.Round(((Val(Label43.Text) / Val(Label42.Text)) * 100), 2) & " %"
        Label47.Text = "(  " & clsNumber.nTOword(Label42.Text) & "  )"
        Label47.Left = (Label46.Left - Label47.Width) - 10
        Binding()

    End Sub
    Sub SaveKind()
        Dim sql As String = "INSERT INTO Drug_Purchases(Bill_ID,Drug_ID,Public_Price,Drug_Amount,Pharmacist_discount,Sales_tax,Pharmacist_Price,Total_Price_tax,Earnings,Bill_number,bill_date,Drug_exp,Supplier_ID,Drug_Bonus,Today_Date,State,User_ID) 
                            VALUES(@Bill_ID,@Drug_ID,@Public_Price,@Drug_Amount,@Pharmacist_discount,@Sales_tax,@Pharmacist_Price,@Total_Price_tax,@Earnings,@Bill_number,@bill_date,@Drug_exp,@Supplier_ID,@Drug_Bonus,@Today_Date,@State,@User_ID)"

        Myconn.cmd = New SqlCommand(sql, Myconn.conn)

        With Myconn.cmd.Parameters
            .Add("@Bill_ID", SqlDbType.Int).Value = txtBill_ID.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
            .Add("@Public_Price", SqlDbType.Decimal).Value = txtPublic_Price.Text
            .Add("@Drug_Amount", SqlDbType.Int).Value = txtAmount.Text
            .Add("@Pharmacist_discount", SqlDbType.Decimal).Value = If(Check1.Checked = True, Math.Round(Val(txtPharm_discount1.Text), 2), Math.Round(Val(txtPharm_discount.Text), 2))
            .Add("@Sales_tax", SqlDbType.Decimal).Value = txtSales_tax.Text
            .Add("@Pharmacist_Price", SqlDbType.Decimal).Value = If(Check1.Checked = True, Math.Round(Val(txtPharmacist_Price1.Text), 4), Math.Round(Val(txtPharmacist_Price.Text), 4))
            .Add("@Total_Price_tax", SqlDbType.Decimal).Value = txtTotal.Text
            .Add("@Earnings", SqlDbType.Decimal).Value = ((Val(txtAmount.Text) + Val(txtBonus.Text)) * Val(txtPublic_Price.Text)) - Val(txtTotal.Text)
            .Add("@Bill_number", SqlDbType.NVarChar).Value = txtBill_num.Text
            .Add("@bill_date", SqlDbType.NChar).Value = Format(CDate(bill_date.Text), "yyyy/MM/dd")
            .Add("@Drug_exp", SqlDbType.NChar).Value = Format(CDate(Durg_exp.Text), "yyyy/MM")
            .Add("@Supplier_ID", SqlDbType.Int).Value = cbo_Supplier.SelectedValue
            .Add("@Drug_Bonus", SqlDbType.Int).Value = txtBonus.Text
            .Add("@Today_Date", SqlDbType.NChar).Value = Format(CDate(dtp1.Text), "yyyy/MM/dd")
            .Add("@State", SqlDbType.Bit).Value = 1
            .Add("@User_ID", SqlDbType.Int).Value = My.Settings.user_ID
        End With
        Try
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            Return
        End Try


    End Sub
    Sub Real_Discound()
        Dim sql As String = "Update  Drugs set Real_Discound = @Real_Discound where Drug_ID =@Drug_ID"

        Myconn.cmd = New SqlCommand(sql, Myconn.conn)

        With Myconn.cmd.Parameters
            .Add("@Real_Discound", SqlDbType.Decimal).Value = txtReal_Discound.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
        End With
        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()
    End Sub

#Region "Form"

    Private Sub frmPharm_Purchases_bill_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Myconn.Fillcombo2("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug)

    End Sub
    Private Sub frmPharm_Purchases_bill_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label5.Left = 0
        Label5.Width = Me.Width
        Me.KeyPreview = True
        txtPharmacist_Price.Enabled = False
        Myconn.Fillcombo("select * from Supplier order by Supplier_name", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Supplier)
        fin = False
        Myconn.Fillcombo2("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug)
        fin = True
        Check1.Checked = True
        Check1_CheckedChanged(Nothing, Nothing)
    End Sub
    Private Sub frmPharm_Purchases_bill_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp



    End Sub
#End Region

#Region "Botton"

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        drg.Rows.Clear()
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
        For Each txt As Control In GroupBox4.Controls
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
        If txtAmount.Text <= 0 Then
            ErrorProvider1.SetError(txtAmount, "الكمية غير مناسبة")
            MessageBox.Show("من فضلك الكمية المناسبة ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        'Try
        SaveKind()
            Real_Discound()
            x = 2
            Fillgrd()
            Myconn.DataGridview_MoveLast(drg, 3)

            'MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            For Each crl As Control In GroupBox4.Controls
                If TypeOf crl Is TextBox Then
                    crl.Text = 0
                End If
            Next
            cbo_Drug.Focus()
        'Catch ex As Exception
        '    MsgBox(ex)
        '    'Return
        'End Try


    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Myconn.DeleteRecord("Drug_Purchases", "ID", CInt(drg.CurrentRow.Cells(19).Value))
            x = 2
            Fillgrd()
        End If
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Dim sql As String = "Update  Drug_Purchases set Bill_ID = @Bill_ID,Drug_ID = @Drug_ID,Public_Price = @Public_Price,Drug_Amount =@Drug_Amount,Pharmacist_discount = @Pharmacist_discount,Sales_tax = @Sales_tax,Pharmacist_Price = @Pharmacist_Price,Total_Price_tax = @Total_Price_tax,Earnings = @Earnings,Bill_number = @Bill_number,bill_date = @bill_date,Drug_exp =@Drug_exp,Supplier_ID = @Supplier_ID,Drug_Bonus = @Drug_Bonus,Today_Date = @Today_Date ,User_ID=@User_ID where ID =@ID"

        Myconn.cmd = New SqlCommand(sql, Myconn.conn)

        With Myconn.cmd.Parameters
            .Add("@Bill_ID", SqlDbType.Int).Value = txtBill_ID.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
            .Add("@Public_Price", SqlDbType.Decimal).Value = txtPublic_Price.Text
            .Add("@Drug_Amount", SqlDbType.Int).Value = txtAmount.Text
            .Add("@Pharmacist_discount", SqlDbType.Decimal).Value = If(Check1.Checked = True, Math.Round(Val(txtPharm_discount1.Text), 2), Math.Round(Val(txtPharm_discount.Text), 2))
            .Add("@Sales_tax", SqlDbType.Decimal).Value = txtSales_tax.Text
            .Add("@Pharmacist_Price", SqlDbType.Decimal).Value = If(Check1.Checked = True, Math.Round(Val(txtPharmacist_Price1.Text), 4), Math.Round(Val(txtPharmacist_Price.Text), 4))
            .Add("@Total_Price_tax", SqlDbType.Decimal).Value = txtTotal.Text
            .Add("@Earnings", SqlDbType.Decimal).Value = (Val(txtAmount.Text) * Val(txtPublic_Price.Text)) - Val(txtTotal.Text)
            .Add("@Bill_number", SqlDbType.Int).Value = txtBill_num.Text
            .Add("@bill_date", SqlDbType.NChar).Value = Format(CDate(bill_date.Text), "yyyy/MM/dd")
            .Add("@Drug_exp", SqlDbType.NChar).Value = Format(CDate(Durg_exp.Text), "yyyy/MM")
            .Add("@Supplier_ID", SqlDbType.Int).Value = cbo_Supplier.SelectedValue
            .Add("@Drug_Bonus", SqlDbType.Int).Value = txtBonus.Text
            .Add("@Today_Date", SqlDbType.NChar).Value = Format(CDate(dtp1.Text), "yyyy/MM/dd")
            .Add("@User_ID", SqlDbType.Int).Value = My.Settings.user_ID
            .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(19).Value)
        End With

        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()
        Real_Discound()
        Binding()
        x = 2
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim sql As String = "Update  Drug_Purchases set State = @State where ID = @ID"
        Myconn.cmd = New SqlCommand(sql, Myconn.conn)

        If drg.CurrentRow.Cells(20).Value = True Then
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 0
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(19).Value)
            End With
        Else
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 1
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(19).Value)
            End With
        End If
        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()
        x = 2
        Fillgrd()
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Select Case cbo_search.SelectedIndex
            Case 0
                x = 0
            Case 1
                x = 1
        End Select
        Fillgrd()
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Select Case cboPrint.SelectedIndex
            Case 0 ' طباعة الفاتورة

            Case 1 ' طباعة الباركود لصنف واحد
                Dim frm As New frmReportViewer("الباركود")
                Dim rpt As New rpt_Barcode
                Dim table As New DataTable
                For i As Integer = 1 To 4
                    Dim x As String
                    x = Format(i, "00")
                    table.Columns.Add(x)
                Next
                For y As Integer = 0 To drg.CurrentRow.Cells(6).Value - 1
                    table.Rows.Add()
                    table.Rows(y)(0) = My.Settings.Barcode_line1
                    table.Rows(y)(1) = "*" & drg.CurrentRow.Cells(22).Value.Trim & "*"
                    table.Rows(y)(2) = drg.CurrentRow.Cells(22).Value & Space(2) & "EXP : " & drg.CurrentRow.Cells(4).Value & " - PT " & drg.CurrentRow.Cells(5).Value
                    table.Rows(y)(3) = drg.CurrentRow.Cells(2).Value
                Next
                rpt.SetDataSource(table)
                frm.CrystalReportViewer1.ReportSource = rpt
                frm.CrystalReportViewer1.Refresh()
                frm.Show()
                frm.CrystalReportViewer1.Zoom(300)
            Case 2 ' طباعة الباركود لجميع أصناف الفاتورة
                Dim frm As New frmReportViewer("الباركود")
                Dim rpt As New rpt_Barcode
                Dim table As New DataTable
                Dim N As Integer = 0
                For i As Integer = 1 To 4
                    Dim x As String
                    x = Format(i, "00")
                    table.Columns.Add(x)
                Next
                For f As Integer = 0 To drg.Rows.Count - 1
                    drg.Rows(f).Cells(6).Selected = True
                    For r As Integer = 0 To CInt(drg.Rows(f).Cells(6).Value) - 1
                        table.Rows.Add()
                        table.Rows(N)(0) = My.Settings.Barcode_line1
                        table.Rows(N)(1) = "*" & drg.Rows(f).Cells(22).Value.Trim & "*"
                        table.Rows(N)(2) = drg.Rows(f).Cells(22).Value & Space(2) & "EXP : " & drg.Rows(f).Cells(4).Value & " - PT " & drg.Rows(f).Cells(5).Value
                        table.Rows(N)(3) = drg.Rows(f).Cells(2).Value
                        N += 1
                    Next
                Next
                rpt.SetDataSource(table)
                frm.CrystalReportViewer1.ReportSource = rpt
                frm.CrystalReportViewer1.Refresh()
                frm.Show()
                frm.CrystalReportViewer1.Zoom(300)
        End Select
    End Sub

#End Region

#Region "TextBox"
    Private Sub txtPharmacist_Price_TextChanged(sender As Object, e As EventArgs)
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtPharm_discount_TextChanged(sender As Object, e As EventArgs) Handles txtPharm_discount.TextChanged
        If cbo_Drug.SelectedIndex = -1 Then Return
        If Check1.Checked = False And RB1.Checked = True Then
            If txtAmount.Text = "" Or txtAmount.Text = 0 Then
                MessageBox.Show("من فضلك أدخل الكمية ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                txtAmount.Focus()
                Return
            End If

            txtPharmacist_Price.Text = Math.Round(Val(txtPublic_Price.Text) * ((100 - Val(txtPharm_discount.Text)) / 100), 4)
            txtTotal_one.Text = Val(txtPharmacist_Price.Text) + Val(txtSales_tax.Text)
            txtTotal_Pharm.Text = Val(txtPharmacist_Price.Text) * Val(txtAmount.Text)
            txtTotal.Text = Val(txtTotal_one.Text) * Val(txtAmount.Text)
            Dim M, K As Double
            'التكلفة المرجحة للقطعة
            M = (((Val(A) * ((100 - R) / 100) * Val(txtPublic_Price.Text)) + Val(txtTotal.Text)) / (Val(A) + Val(txtAmount.Text)))
            ' الخصم المرجح
            K = Math.Round((((Val(txtPublic_Price.Text) - M) / Val(txtPublic_Price.Text)) * 100), 2)
            txtReal_Discound.Text = K
        End If
    End Sub
    Private Sub txtPharmacist_Price_TextChanged_1(sender As Object, e As EventArgs) Handles txtPharmacist_Price.TextChanged
        If cbo_Drug.SelectedIndex = -1 Then Return
        If Check1.Checked = False And RB2.Checked = True Then
            If txtAmount.Text = "" Or txtAmount.Text = 0 Then
                MessageBox.Show("من فضلك أدخل الكمية ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

                txtAmount.Focus()

                Return
            End If

            txtPharm_discount.Text = Math.Round(((((Val(txtPublic_Price.Text) - Val(txtPharmacist_Price.Text)) / Val(txtPublic_Price.Text))) * 100), 2)
            txtTotal_one.Text = Val(txtPharmacist_Price.Text) + Val(txtSales_tax.Text)
            txtTotal_Pharm.Text = Val(txtPharmacist_Price.Text) * Val(txtAmount.Text)
            txtTotal.Text = Val(txtTotal_one.Text) * Val(txtAmount.Text)
            Dim M, K As Double
            'التكلفة المرجحة للقطعة
            M = (((Val(A) * ((100 - R) / 100) * Val(txtPublic_Price.Text)) + Val(txtTotal.Text)) / (Val(A) + Val(txtAmount.Text)))
            ' الخصم المرجح
            K = Math.Round((((Val(txtPublic_Price.Text) - M) / Val(txtPublic_Price.Text)) * 100), 2)
            txtReal_Discound.Text = K
        End If

    End Sub
    Private Sub txtBonus_Enter(sender As Object, e As EventArgs) Handles txtBonus.Enter
        If txtAmount.Text <= 0 Or txtAmount.Text = "" Then
            MsgBox("أدخل الكمية الأساسية أولا")
            txtAmount.Focus()


        End If
    End Sub
    Private Sub txtBonus_KeyUp(sender As Object, e As KeyEventArgs) Handles txtBonus.KeyUp
        If e.KeyCode = Keys.Enter AndAlso e.Control = True Then
            btnSave_Click(Nothing, Nothing)
            cbo_Drug.Focus()
        ElseIf e.KeyCode = Keys.N AndAlso e.Control = True Then
            btnNew_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub txtBonus_TextChanged(sender As Object, e As EventArgs) Handles txtBonus.TextChanged
        ErrorProvider1.Clear()
        txtTotal.Text = (Val(txtTotal_one.Text) * Val(txtAmount.Text)) + (Val(txtBonus.Text) * Val(txtSales_tax.Text))
        Dim M, K As Double
        'التكلفة المرجحة للقطعة
        M = (((Val(A) * ((100 - R) / 100) * Val(txtPublic_Price.Text)) + Val(txtTotal.Text)) / (Val(A) + Val(txtAmount.Text) + Val(txtBonus.Text)))
        ' الخصم المرجح
        K = Math.Round((((Val(txtPublic_Price.Text) - M) / Val(txtPublic_Price.Text)) * 100), 2)
        txtReal_Discound.Text = K
    End Sub
    Private Sub txtBonus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBonus.KeyPress
        Myconn.NumberOnly(txtBonus, e)
    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        ErrorProvider1.Clear()
        If Check1.Checked = False And RB1.Checked = True Then
            txtPharm_discount.Text = 0
            txtPharmacist_Price.Text = 0
        End If

        If Check1.Checked = False And RB2.Checked = True Then
            txtPharm_discount.Text = 0
            txtPharmacist_Price.Text = 0
        End If

        If Check1.Checked = True Then
            txtTotal_one.Text = Val(txtPharmacist_Price1.Text) + Val(txtSales_tax.Text)
            txtTotal_Pharm.Text = Val(txtPharmacist_Price1.Text) * Val(txtAmount.Text)
            txtTotal.Text = Val(txtTotal_one.Text) * Val(txtAmount.Text)
            Dim M, K As Double
            'التكلفة المرجحة للقطعة
            M = (((Val(A) * ((100 - R) / 100) * Val(txtPublic_Price.Text)) + Val(txtTotal.Text)) / (Val(A) + Val(txtAmount.Text)))
            ' الخصم المرجح
            K = Math.Round((((Val(txtPublic_Price.Text) - M) / Val(txtPublic_Price.Text)) * 100), 2)
            txtReal_Discound.Text = K
        End If
    End Sub
    Private Sub txtAmount_KeyUp(sender As Object, e As KeyEventArgs) Handles txtAmount.KeyUp
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        Myconn.NumberOnly(txtAmount, e)
    End Sub
    Private Sub txtAmount_Enter(sender As Object, e As EventArgs) Handles txtAmount.Enter
        If cbo_Drug.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cbo_Drug, "أدخل الصنف")
            'MsgBox("أدخل الصنف")
            cbo_Drug.Focus()
        End If
        txtAmount.Text = ""
    End Sub
#End Region

#Region "ComboBox"
    Private Sub cbo_Drug_KeyUp(sender As Object, e As KeyEventArgs) Handles cbo_Drug.KeyUp
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub cbo_Drug_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Drug.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        Binding()
    End Sub
    Private Sub cbo_Drug_Enter(sender As Object, e As EventArgs) Handles cbo_Drug.Enter
        Myconn.langAR()
    End Sub

#End Region

    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset("Select * from Drug_Purchases where ID =" & CInt(drg.CurrentRow.Cells(19).Value), "Drug_Purchases", Me)

        Myconn.comboBinding("Drug_ID", cbo_Drug)
    End Sub
    Private Sub Check1_CheckedChanged(sender As Object, e As EventArgs) Handles Check1.CheckedChanged
        If Check1.Checked = True Then
            txtSales_tax.Enabled = True
            txtPharmacist_Price1.Enabled = True
            txtPharm_discount1.Enabled = True
            txtPharm_discount.Enabled = False
            txtPharmacist_Price.Enabled = False
            txtPharm_discount.Text = 0
            txtPharmacist_Price.Text = 0
            RB1.Enabled = False
            RB2.Enabled = False
        Else
            txtSales_tax.Enabled = False
            txtPharmacist_Price1.Enabled = False
            txtPharm_discount1.Enabled = False
            txtSales_tax.Text = 0
            txtPharmacist_Price1.Text = 0
            txtPharm_discount1.Text = 0
            txtPharm_discount.Enabled = True
            txtPharmacist_Price.Enabled = False
            RB1.Enabled = True
            RB1.Checked = True
            RB2.Enabled = True
        End If
    End Sub
    Private Sub RB1_CheckedChanged(sender As Object, e As EventArgs) Handles RB1.CheckedChanged
        If RB1.Checked = True Then
            txtPharm_discount.Enabled = True
        Else
            txtPharm_discount.Enabled = False
        End If
    End Sub


    Private Sub RB2_CheckedChanged(sender As Object, e As EventArgs) Handles RB2.CheckedChanged
        If RB2.Checked = True Then
            txtPharmacist_Price.Enabled = True
        Else
            txtPharmacist_Price.Enabled = False
        End If
    End Sub

    Private Sub Durg_exp_KeyUp(sender As Object, e As KeyEventArgs) Handles Durg_exp.KeyUp
        If e.KeyCode = Keys.Enter AndAlso e.Control = True Then
            btnSave_Click(Nothing, Nothing)
            cbo_Drug.Focus()
        ElseIf e.KeyCode = Keys.N AndAlso e.Control = True Then
            btnNew_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub Durg_exp_Enter(sender As Object, e As EventArgs) Handles Durg_exp.Enter
        If txtAmount.Text <= 0 Or txtAmount.Text = "" Then
            ErrorProvider1.SetError(txtAmount, "أدخل الكمية")
            MsgBox("أدخل الكمية أولا")
            txtAmount.Focus()


        End If
    End Sub


End Class