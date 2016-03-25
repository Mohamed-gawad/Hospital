Imports System.Data.SqlClient
Imports System.Globalization
Public Class frmAdd_Store_Operation
    Dim fin As Boolean
    Dim Myconn As New connect
    Dim X, Y, V, U, AM2 As Integer
    Dim UN As Double
    Dim StockID As Integer
    Dim V2, V1 As Decimal
    Dim st, Unit1 As String
    Sub TextBindingdata(frm As Form, grb As GroupBox, Fields() As String, txt() As TextBox)

        For i As Integer = 0 To Fields.Count - 1
            txt(i).DataBindings.Clear()
            txt(i).DataBindings.Add("text", Myconn.dv1, Fields(i))
        Next
    End Sub
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.Filldataset4("select  isnull(max(Bill_ID),0) as Bill_ID from Stocks_Purchases where Stock_ID =" & CInt(StockID), "Stocks_Purchases", Me)
        txtBill_ID.Text = Myconn.cur4.Current("Bill_ID") + 1

        drg.Rows.Clear()
        txtPrice.Text = ""
        txtAmount.Text = ""
        cboEmployee.SelectedIndex = -1
        cbo_Drug.SelectedIndex = -1
        cbo_Supplier.SelectedIndex = -1

        dtb.Enabled = True
        dtb.Text = Today
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Select Case X
            Case 0
                st = "where a.Bill_ID =" & CInt(txtBill_ID.Text) & "and a.Stock_ID = " & StockID & ""
            Case 1
                st = "where a.Bill_ID =" & CInt(txtSearch.Text) & "and a.Stock_ID = " & StockID & ""
        End Select
        Myconn.Filldataset("Select m.Max_Unit_Name,n.Min_Unit_Name,a.Bill_Time,a.Bill_ID,a.Bill_Date ,c.Co_Name, b.Drug_Name,a.Drug_ID,a.Drug_exp,
                                    a.Amount,a.unit,a.Unit_Kind,a.Price,a.Total,(d.EmployeeName) As Users,g.GroupName,a.Stock_ID,a.EmployeeID,a.Supplier_ID,
                                    (e.EmployeeName) As Employee,a.ID,a.state,R.Supplier_Name,b.Min_Unit_number,a.State from Stocks_Purchases a
                                   Left Join Drugs b on a.Drug_ID = b.Drug_ID
                                   Left Join Employees d on a.Users_ID = d.EmployeeID
                                   Left Join Employees e on a.EmployeeID = e.EmployeeID
                                   Left Join Drug_Groups g on b.GroupID = g.GroupID
                                   Left Join Max_Unit M on a.Unit = M.Max_UnitID
                                   Left Join Min_Unit n on a.Unit = n.Min_UnitID
                                   Left Join Supplier R on a.Supplier_ID = R.Supplier_ID
                                   Left Join Co_Drug c On b.Co_ID = c.Co_ID " & st & " order by ID", "Stocks_Purchases", Me)
        If Myconn.cur.Count = 0 Then Return
        V1 = 0
        V2 = 0
        AM2 = 0
        For i As Integer = 0 To Myconn.cur.Count - 1

            If Myconn.cur.Current("Unit_Kind") = 0 Then
                Unit1 = Myconn.cur.Current("Min_Unit_Name")
                AM2 = Myconn.cur.Current("Amount") * Myconn.cur.Current("Min_Unit_number")
            ElseIf Myconn.cur.Current("Unit_Kind") = 1 Then
                Unit1 = Myconn.cur.Current("Max_Unit_Name")
                AM2 = Myconn.cur.Current("Amount") * 1
            End If
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Bill_Time")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Co_Name")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Drug_Name")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Drug_ID")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Price")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Drug_exp")
            drg.Rows(i).Cells(7).Value = Math.Round(AM2)
            drg.Rows(i).Cells(8).Value = Unit1
            drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Total")
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("GroupName")
            drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Users")
            drg.Rows(i).Cells(12).Value = Myconn.cur.Current("Employee")
            drg.Rows(i).Cells(13).Value = Myconn.cur.Current("ID")
            drg.Rows(i).Cells(14).Value = Myconn.cur.Current("State")

            If drg.Rows(i).Cells(14).Value = True Then
                drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                V1 += CDec(drg.Rows(i).Cells(9).Value)
            Else
                drg.Rows(i).DefaultCellStyle.BackColor = Color.Red
                V2 += CDec(drg.Rows(i).Cells(9).Value)
            End If
            Myconn.cur.Position += 1
        Next
        Label35.Text = V1
        Label34.Text = "( " & clsNumber.nTOword(Label35.Text) & " )"
        Label34.Left = Label35.Left - (Label34.Width + 20)

        Label10.Text = V2
        Label8.Text = "( " & clsNumber.nTOword(Label10.Text) & " )"
        Label8.Left = Label10.Left - (Label8.Width + 20)

        Myconn.DataGridview_MoveLast(drg, 7)

    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Select Case Y
            Case 0
                Myconn.Filldataset("select a.Drug_ID,a.Drug_Name,a.Drug_Price ,a.Parcod,a.Min_Unit_number,d.Max_Unit_Name,e.Min_Unit_Name,isnull(b.Amount,0) as Drug_Purchases,isnull(c.Amount,0) as Sales,
                                        (isnull(b.Amount,0) - isnull(c.Amount,0)) as rest  from Drugs a
                                         left join (select Drug_ID,sum(Amount) as Amount,state from [dbo].[Stocks_Purchases] GROUP BY Drug_ID,state,Stock_ID  having state = 'true' and Stock_ID = " & StockID & " )b
                                         on a.Drug_ID = b.Drug_ID
                                         left join (select Drug_ID,sum(Amount) as Amount,state from [dbo].[Stocks_Sales] GROUP BY Drug_ID,state,Stock_ID  having state = 'true' and Stock_ID = " & StockID & " ) c
                                         on a.Drug_ID = c.Drug_ID
                                         left join Max_Unit d on a.Max_UnitID=d.Max_UnitID
                                         left join Min_Unit e on a.Min_UnitID=e.Min_UnitID
                                         GROUP BY a.Drug_ID,a.Drug_Name,b.Amount,c.Amount,a.Drug_Price,a.Parcod,a.Min_Unit_number,d.Max_Unit_Name,e.Min_Unit_Name
                                         having (isnull(b.Amount,0) - isnull(c.Amount,0)) >= 0 and a.Drug_ID =" & CInt(cbo_Drug.SelectedValue), "Drugs", Me)

                If Myconn.cur.Current("rest") = 0 Then
                    txtStock_amount.BackColor = Color.Red
                Else
                    txtStock_amount.BackColor = Color.White
                    'MsgBox(" .. الصنف غير متوفر")

                End If

                Dim Myfields() As String = {"Drug_Name", "Drug_Price", "Rest"}
                Dim Mytxt() As TextBox = {txtDrug, txtPublic_Price, txtStock_amount}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)

                Dim C, Amount, A As Double, B, E As Integer
                Amount = Myconn.cur.Current("rest")
                If Amount = 0 Then
                    txtStock_amount.Text = 0

                ElseIf Amount <> 0

                    A = Math.Round(Amount, 2)
                    B = Fix(A)
                    C = Math.Round((Val(A) - Val(B)), 2)
                    E = Myconn.cur.Current("Min_Unit_number")

                    If B > 0 And C = 0 Then
                        txtStock_amount.Text = B & " " & Myconn.cur.Current("Max_Unit_Name")
                    ElseIf B > 0 And C > 0
                        txtStock_amount.Text = B & " " & Myconn.cur.Current("Max_Unit_Name") & " و " & Math.Round((C * E), 0) & " " & Myconn.cur.Current("Min_Unit_Name")
                    ElseIf B = 0 And C > 0
                        txtStock_amount.Text = Math.Round((C * E), 0) & " " & Myconn.cur.Current("Min_Unit_Name")

                    ElseIf B < 0 And C = 0
                        txtStock_amount.Text = B & " " & Myconn.cur.Current("Max_Unit_Name")
                    ElseIf B < 0 And C < 0
                        txtStock_amount.Text = B & " " & Myconn.cur.Current("Max_Unit_Name") & " و " & Math.Round((C * E), 0) & " " & Myconn.cur.Current("Min_Unit_Name")
                    ElseIf B = 0 And C < 0
                        txtStock_amount.Text = Math.Round((C * E), 0) & " " & Myconn.cur.Current("Min_Unit_Name")

                    End If

                End If
            Case 1
                Myconn.Filldataset1("select a.Min_Unit_price,a.Min_UnitID,a.Max_UnitID,a.Min_Unit_price,a.Drug_Price,c.Min_Unit_Name,b.Max_Unit_Name,a.Min_Unit_number from Drugs a
                             left join Max_Unit b on a.Max_UnitID=b.Max_UnitID
                             left join Min_Unit c on a.Min_UnitID=c.Min_UnitID
                            where Drug_ID =" & CInt(cbo_Drug.SelectedValue), "Drugs", Me)
                Select Case cbo_Unit.SelectedIndex
                    Case 0
                        Dim Myfields() As String = {"Min_Unit_Name", "Min_Unit_price", "Min_UnitID", "Min_Unit_number"}
                        Dim Mytxt() As TextBox = {txtKind_Unit, txtPrice, txtUnit_ID, txtUnit_Number}
                        TextBindingdata(Me, GroupBox3, Myfields, Mytxt)
                    Case 1
                        Dim Myfields() As String = {"Max_Unit_Name", "Drug_Price", "Max_UnitID"}
                        Dim Mytxt() As TextBox = {txtKind_Unit, txtPrice, txtUnit_ID}
                        TextBindingdata(Me, GroupBox3, Myfields, Mytxt)
                        txtUnit_Number.Text = 1
                End Select
        End Select
    End Sub
    Sub Save_To_Stock()
        Dim sql As String = "INSERT INTO Stocks_Purchases(Stock_ID,Bill_ID,Bill_Date,Bill_Time,Drug_ID,Drug_exp,Price,Amount,Unit,Unit_Kind,Total,EmployeeID,Supplier_ID,Drug_Sales_ID,Users_ID,State)
                                                             VALUES(@Stock_ID,@Bill_ID,@Bill_Date,@Bill_Time,@Drug_ID,@Drug_exp,@Price,@Amount,@Unit,@Unit_Kind,@Total,@EmployeeID,@Supplier_ID,@Drug_Sales_ID,@Users_ID,@State)"

        Dim x As Integer = Nothing
        Myconn.cmd = New SqlCommand(sql, Myconn.conn)
        With Myconn.cmd.Parameters
            .Add("@Stock_ID", SqlDbType.Int).Value = StockID
            .Add("@Bill_ID", SqlDbType.Int).Value = txtBill_ID.Text
            .Add("@Bill_Date", SqlDbType.NChar).Value = Format(CDate(dtb.Text), "yyyy/MM/dd")
            .Add("@Bill_Time", SqlDbType.NChar).Value = Label15.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
            .Add("@Drug_exp", SqlDbType.NChar).Value = Format(CDate(Exp_date.Text), "yyyy/MM")
            .Add("@Price", SqlDbType.Decimal).Value = txtPrice.Text
            .Add("@Amount", SqlDbType.Decimal).Value = Math.Round((Val(txtAmount.Text) / Val(txtUnit_Number.Text)), 2)
            .Add("@Unit", SqlDbType.Int).Value = txtUnit_ID.Text
            .Add("@Unit_Kind", SqlDbType.Int).Value = cbo_Unit.SelectedIndex
            .Add("@Total", SqlDbType.Decimal).Value = Val(txtPrice.Text) * Val(txtAmount.Text)
            .Add("@EmployeeID", SqlDbType.Int).Value = cboEmployee.SelectedValue
            .Add("@Supplier_ID", SqlDbType.Int).Value = cbo_Supplier.SelectedValue
            .Add("@Drug_Sales_ID", SqlDbType.Int).Value = x ' رقم السجل في حالة الاضافة من الصيدلية من اجل الحذف والتعديل من الصيدلية
            .Add("@Users_ID", SqlDbType.Int).Value = My.Settings.user_ID
            .Add("@State", SqlDbType.Bit).Value = 1
        End With
        Try
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            'Return
        End Try
        Y = 0
        Binding()

    End Sub
    Private Sub frmAdd_Store_Operation_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        StockID = 2
        fin = False
        Myconn.Fillcombo("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug)
        Myconn.Fillcombo("select * from Supplier  where Supplier_ID <> 1 order by Supplier_Name", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Supplier)
        Myconn.Fillcombo("select * from Employees order by EmployeeName", "Employees", "EmployeeID", "EmployeeName", Me, cboEmployee)
        fin = True
    End Sub

    Private Sub frmAdd_Store_Operation_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Enter AndAlso e.Control = True Then
            btnSave_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.N AndAlso e.Control = True Then
            btnNew_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub frmAdd_Store_Operation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label1.Left = 0
        Label1.Width = Me.Width
        Me.KeyPreview = True
        StockID = 2
        fin = False
        Myconn.Fillcombo("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug)
        Myconn.Fillcombo("select * from Supplier where Supplier_ID <> 1 order by Supplier_Name", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Supplier)
        Myconn.Fillcombo("select * from Employees order by EmployeeName", "Employees", "EmployeeID", "EmployeeName", Me, cboEmployee)
        fin = True
        Timer1.Start()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        GroupBox5.Enabled = True
        txtSearch.Text = ""
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If CDbl(txtBill_ID.Text) <= 0 Or Nothing Then
                ErrorProvider1.SetError(txtBill_ID, "أدخل رقم الفاتورة")
                Return
            End If
        Catch ex As Exception
            ErrorProvider1.SetError(txtBill_ID, "أدخل رقم الفاتورة")
            Return
        End Try


        If CDbl(txtAmount.Text) <= 0 Then
            ErrorProvider1.SetError(txtAmount, "أدخل الكمية")
            Return
        End If
        For Each txt As Control In GroupBox5.Controls
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
        Save_To_Stock()
        X = 0
        Fillgrd()
        Myconn.Sum_drg(drg, 9, Label35, Label34)
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        GroupBox5.Enabled = False
        txtAmount.Text = 0
        cbo_Drug.SelectedIndex = -1
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Dim sql As String = "Update Stocks_Purchases set Stock_ID=@Stock_ID,Bill_ID=@Bill_ID,Bill_Date=@Bill_Date,Bill_Time=@Bill_Time,
                             Drug_ID=@Drug_ID,Drug_exp=@Drug_exp,Price=@Price,Amount=@Amount,Unit=@Unit,Unit_Kind=@Unit_Kind,Total=@Total,
                             EmployeeID=@EmployeeID,Supplier_ID=@Supplier_ID,Users_ID=@Users_ID where ID =@ID"

        Myconn.cmd = New SqlCommand(sql, Myconn.conn)
        With Myconn.cmd.Parameters
            .Add("@Stock_ID", SqlDbType.Int).Value = StockID
            .Add("@Bill_ID", SqlDbType.Int).Value = txtBill_ID.Text
            .Add("@Bill_Date", SqlDbType.NChar).Value = Format(CDate(dtb.Text), "yyyy/MM/dd")
            .Add("@Bill_Time", SqlDbType.NChar).Value = Label15.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
            .Add("@Drug_exp", SqlDbType.NChar).Value = Format(CDate(Exp_date.Text), "yyyy/MM")
            .Add("@Price", SqlDbType.Decimal).Value = txtPrice.Text
            .Add("@Amount", SqlDbType.Decimal).Value = Math.Round((Val(txtAmount.Text) / Val(txtUnit_Number.Text)), 2)
            .Add("@Unit", SqlDbType.Int).Value = txtUnit_ID.Text
            .Add("@Unit_Kind", SqlDbType.Int).Value = cbo_Unit.SelectedIndex
            .Add("@Total", SqlDbType.Decimal).Value = Val(txtPrice.Text) * Val(txtAmount.Text)
            .Add("@EmployeeID", SqlDbType.Int).Value = cboEmployee.SelectedValue
            .Add("@Supplier_ID", SqlDbType.Int).Value = cbo_Supplier.SelectedValue
            .Add("@Users_ID", SqlDbType.Int).Value = My.Settings.user_ID
            .Add("@ID", SqlDbType.Int).Value = drg.CurrentRow.Cells(13).Value
        End With
        Try
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            'Return
        End Try
        '--------------------------------------------------------------------------------------------------------------------------------
        Y = 0
        Binding()

        X = 0
        Fillgrd()

        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Myconn.DeleteRecord("Stocks_Purchases", "ID", drg.CurrentRow.Cells(13).Value)
            X = 0
            Fillgrd()
            Y = 0
            Binding()
            txtAmount.Text = ""
        End If
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim sql As String = "Update  Stocks_Purchases set State = @State where ID = @ID"
        Myconn.cmd = New SqlCommand(sql, Myconn.conn)

        If drg.CurrentRow.Cells(14).Value = True Then
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 0
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(13).Value)
            End With
        Else
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 1
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(13).Value)
            End With
        End If

        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()
        X = 0
        Fillgrd()
        Y = 0
        Binding()
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txtSearch.Text = "" Then Return
        X = 1
        Fillgrd()
        If Myconn.cur.Count = 0 Then Return
        dtb.Text = Me.BindingContext(Myconn.dv).Current("Bill_Date")
        txtBill_ID.Text = Me.BindingContext(Myconn.dv).Current("Bill_ID")
        cbo_Supplier.SelectedValue = Myconn.cur.Current("Supplier_ID")
        cboEmployee.SelectedValue = Myconn.cur.Current("EmployeeID")
        GroupBox5.Enabled = False
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset2("Select * from Stocks_Purchases where ID =" & CInt(drg.CurrentRow.Cells(13).Value), "Stocks_Purchases", Me)
        Y = 0
        cbo_Drug.DataBindings.Clear()
        cbo_Drug.DataBindings.Add("SelectedValue", Myconn.dv2, "Drug_ID")
        Binding()
    End Sub
    Private Sub cbo_Drug_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Drug.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        If cbo_Drug.SelectedIndex = -1 Then Return
        Y = 0
        Binding()
        cbo_Unit_SelectedIndexChanged(Nothing, Nothing)
    End Sub
    Private Sub cbo_Supplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Supplier.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub cboEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmployee.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub cbo_Unit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Unit.SelectedIndexChanged
        ErrorProvider1.Clear()
        Y = 1
        Binding()
        txtTotal_Price.Text = Math.Round((Val(txtAmount.Text) * Val(txtPrice.Text)), 2)
    End Sub
    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        ErrorProvider1.Clear()
        txtTotal_Price.Text = Math.Round((Val(txtAmount.Text) * Val(txtPrice.Text)), 2)
    End Sub
    Private Sub cbo_Drug_Enter(sender As Object, e As EventArgs) Handles cbo_Drug.Enter
        Myconn.langAR()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label15.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
    Private Sub txtBill_ID_TextChanged(sender As Object, e As EventArgs) Handles txtBill_ID.TextChanged
        ErrorProvider1.Clear()
    End Sub

End Class