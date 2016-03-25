Imports System.Data.SqlClient
Imports System.Globalization

Public Class frmPharm_Sales
    Dim Myconn As New connect
    Dim fin, fin2 As Boolean
    Dim X, Y, U, V, G As Integer
    Dim A, UN, Amount As Double
    Dim st As String
    Dim ID As Integer
    'Dim Unit1 As String
    'Dim Unit2 As String

#Region "Function"
    Sub TextBindingdata(frm As Form, grb As GroupBox, Fields() As String, txt() As TextBox)

        For i As Integer = 0 To Fields.Count - 1
            txt(i).DataBindings.Clear()
            txt(i).DataBindings.Add("text", Myconn.dv1, Fields(i))
        Next
    End Sub
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.Autonumber("Bill_ID", "Drug_Sales", txtBill_ID, Me)
        txtBill_ID2.Text = 0

        If cboKind_Customer.SelectedIndex = 0 Then
            Myconn.Filldataset4("select  isnull(max(Bill_ID),0) as Bill_ID from Stocks_Purchases where Stock_ID =" & CInt(cbo_Stock.SelectedValue), "Stocks_Purchases", Me)
            txtBill_ID2.Text = Myconn.cur4.Current("Bill_ID") + 1
        End If
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        If cbo_Drug.SelectedIndex = -1 Then Return
        If Not fin Then Return
        Select Case U
            '-------------------------------------------------------------------------------------------------------------'
            Case 0
                A = 0
                Try
                    Myconn.Filldataset1("select a.Drug_ID,a.Drug_Name,a.Drug_Price,isnull(a.Real_Discound,0) as Real_Discound ,a.Parcod,a.Min_Unit_number,d.Max_Unit_Name,e.Min_Unit_Name,b.Drug_exp,isnull(b.Amount,0) as Drug_Purchases,c.Drug_exp,
                                        isnull(b.Amount,0) as Purchases, isnull(c.Amount_max,0) as Amount_max ,isnull(c.Amount_min,0) as Amount_min from Drugs a
                                         left join (select Drug_ID,Drug_exp,(sum(Drug_Amount) + sum(Drug_Bonus)) as Amount,state from [dbo].[Drug_Purchases] GROUP BY Drug_ID,Drug_exp,state  having state = 'true' )b
                                         on a.Drug_ID = b.Drug_ID
                                         left join (select Drug_ID,Drug_exp,sum(Amount_max) as Amount_max,sum(Amount_min) as Amount_min,state from [dbo].[Drug_Sales] GROUP BY Drug_ID,Drug_exp,state  having state = 'true' ) c
                                         on a.Drug_ID = c.Drug_ID and b.Drug_exp=c.Drug_exp
                                         left join Max_Unit d on a.Max_UnitID=d.Max_UnitID
                                         left join Min_Unit e on a.Min_UnitID=e.Min_UnitID
                                         GROUP BY b.Drug_exp,c.Drug_exp,a.Drug_ID,a.Drug_Name,b.Amount,c.Amount_max,c.Amount_min,a.Drug_Price,a.Real_Discound,a.Parcod,a.Min_Unit_number,d.Max_Unit_Name,e.Min_Unit_Name
                                         having  ((isnull(b.Amount,0) * a.Min_Unit_number) - (isnull(c.Amount_max,0) * a.Min_Unit_number + isnull(c.Amount_min,0))) > 0 and  a.Drug_ID =" & CInt(cbo_Drug.SelectedValue) & "order by b.Drug_exp", "Drugs", Me)

                    If Myconn.cur1.Count = 0 Then
                        drg_Exp.Rows.Clear()
                        txtDrug.Text = ""
                        txtPublic_Price.Text = ""
                        txtStock_amount.Text = ""
                        txtPharmacist_Discound.Text = ""
                        MsgBox(" .. الصنف غير متوفر")
                        Return
                    End If

                    Dim Myfields() As String = {"Drug_Name", "Drug_Price", "Real_Discound", "Parcod"}
                    Dim Mytxt() As TextBox = {txtDrug, txtPublic_Price, txtPharmacist_Discound, txtBarcode}
                    TextBindingdata(Me, GroupBox3, Myfields, Mytxt)

                    drg_Exp.Rows.Clear()
                    Amount = 0
                    For i As Integer = 0 To Myconn.cur1.Count - 1
                        G = (Val(Myconn.cur1.Current("Purchases")) * Val(Myconn.cur1.Current("Min_Unit_number"))) - Val(Val((Myconn.cur1.Current("Amount_max")) * Val(Myconn.cur1.Current("Min_Unit_number")) + Val(Myconn.cur1.Current("Amount_min"))))
                        drg_Exp.Rows.Add()
                        drg_Exp.Rows(i).Cells(0).Value = i + 1
                        drg_Exp.Rows(i).Cells(1).Value = Myconn.cur1.Current("Drug_Exp")
                        drg_Exp.Rows(i).Cells(2).Value = If(G Mod Val(Myconn.cur1.Current("Min_Unit_number")) = 0, Math.Truncate(G / Val(Myconn.cur1.Current("Min_Unit_number"))) & Space(1) & Myconn.cur1.Current("Max_Unit_Name"), Math.Truncate(G / Val(Myconn.cur1.Current("Min_Unit_number"))) & Space(1) & Myconn.cur1.Current("Max_Unit_Name") & " و " & G Mod Val(Myconn.cur1.Current("Min_Unit_number")) & Space(1) & Myconn.cur1.Current("Min_Unit_Name"))
                        drg_Exp.Rows(i).Cells(3).Value = G
                        Amount += G
                        Myconn.cur1.Position += 1
                    Next
                    txtStock_amount.Text = If(Amount Mod Val(Myconn.cur1.Current("Min_Unit_number")) = 0, Math.Truncate(Amount / Val(Myconn.cur1.Current("Min_Unit_number"))) & Space(1) & Myconn.cur1.Current("Max_Unit_Name"), Math.Truncate(Amount / Val(Myconn.cur1.Current("Min_Unit_number"))) & Space(1) & Myconn.cur1.Current("Max_Unit_Name") & " و " & Amount Mod Val(Myconn.cur1.Current("Min_Unit_number")) & Space(1) & Myconn.cur1.Current("Min_Unit_Name"))
                Catch ex As Exception
                    MsgBox(ex)
                End Try

            '---------------------------------------------------------------------------------------------------------------------------------'
            Case 1
                Myconn.Filldataset1("select a.Min_Unit_price,a.Min_UnitID,a.Max_UnitID,a.Min_Unit_price,a.Drug_Price,c.Min_Unit_Name,b.Max_Unit_Name,a.Min_Unit_number from Drugs a
                             left join Max_Unit b on a.Max_UnitID=b.Max_UnitID
                             left join Min_Unit c on a.Min_UnitID=c.Min_UnitID
                            where Drug_ID =" & CInt(cbo_Drug.SelectedValue), "Drugs", Me)

                Select Case cbo_Unit.SelectedIndex
                    Case 0
                        Dim Myfields() As String = {"Min_Unit_Name", "Min_Unit_price", "Min_UnitID"}
                        Dim Mytxt() As TextBox = {txtKind_Unit, txtPrice, txtUnit_ID}
                        TextBindingdata(Me, GroupBox3, Myfields, Mytxt)
                        txtUnit_Number.Text = 1
                    Case 1
                        Dim Myfields() As String = {"Max_Unit_Name", "Drug_Price", "Max_UnitID", "Min_Unit_number"}
                        Dim Mytxt() As TextBox = {txtKind_Unit, txtPrice, txtUnit_ID, txtUnit_Number}
                        TextBindingdata(Me, GroupBox3, Myfields, Mytxt)
                End Select
        End Select
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Try
            drg.Rows.Clear()
            Select Case Y
                Case 1

                    drg.Rows.Clear()
                    Myconn.Filldataset("select a.Discount,m.Max_Unit_Name,n.Min_Unit_Name,a.Time_Add, c.Co_Name, b.Drug_Name,a.Drug_ID,a.Drug_exp,
                                    a.Amount_max,a.Amount_min,a.unit,a.Unit_Kind,a.Drug_Price,a.Pharm_discound,a.Total_Price,a.Erning,(d.EmployeeName) As Users,
                                    (e.EmployeeName) As Employee,a.ID,a.state,R.Customer_Name,b.Min_Unit_number,a.buyer from Drug_Sales a
                           Left Join Drugs b on a.Drug_ID = b.Drug_ID
                           Left Join Employees d on a.Users_ID = d.EmployeeID
                           Left Join Employees e on a.EmployeeID = e.EmployeeID
                           Left Join Max_Unit M on a.Unit = M.Max_UnitID
                           Left Join Min_Unit n on a.Unit = n.Min_UnitID
                           Left Join Customers R on a.Customer_ID = R.Customer_ID
                           Left Join Co_Drug c On b.Co_ID = c.Co_ID where a.Bill_ID =" & CInt(txtBill_ID.Text), "Drug_Sales", Me)
                    Dim V2, V1 As Decimal
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        drg.Rows.Add()
                        drg.Rows(i).Cells(0).Value = i + 1
                        drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Time_Add")
                        drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Co_Name")
                        drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Drug_Name")
                        drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Drug_ID")
                        drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Drug_exp")
                        drg.Rows(i).Cells(6).Value = Math.Round(If(Myconn.cur.Current("Unit_Kind") = 0, Myconn.cur.Current("Amount_min"), Myconn.cur.Current("Amount_max")))
                        drg.Rows(i).Cells(7).Value = If(Myconn.cur.Current("Unit_Kind") = 0, Myconn.cur.Current("Min_Unit_Name"), Myconn.cur.Current("Max_Unit_Name"))
                        drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Drug_Price")
                        drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Discount")
                        drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Pharm_discound")
                        drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Total_Price")
                        drg.Rows(i).Cells(12).Value = Myconn.cur.Current("Erning")
                        drg.Rows(i).Cells(13).Value = If(IsDBNull(Myconn.cur.Current("Employee")), Myconn.cur.Current("Buyer"), Myconn.cur.Current("Employee"))
                        drg.Rows(i).Cells(14).Value = Myconn.cur.Current("Customer_Name")
                        drg.Rows(i).Cells(15).Value = Myconn.cur.Current("Users")
                        drg.Rows(i).Cells(16).Value = Myconn.cur.Current("Buyer")
                        drg.Rows(i).Cells(17).Value = Myconn.cur.Current("ID")
                        drg.Rows(i).Cells(18).Value = Myconn.cur.Current("State")

                        If drg.Rows(i).Cells(18).Value = True Then
                            drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                            V1 += CDec(drg.Rows(i).Cells(12).Value)
                        Else
                            drg.Rows(i).DefaultCellStyle.BackColor = Color.Red
                            V2 += CDec(drg.Rows(i).Cells(11).Value)
                        End If
                        Myconn.cur.Position += 1
                    Next
                    Myconn.Sum_drg2(drg, 11, Label39)
                    Label39.Text = Val(Label39.Text) - V2
                    Label38.Text = V2
                    Label43.Text = V1
                    U = 0
                    Binding()

                '----------------------------------------------------------------------------------------------------------- البحث'
                Case 2

                    Select Case cbo_search.SelectedIndex
                        Case 0 ' رقم الفاتورة
                            st = " where a.Bill_ID =" & CInt(txtSearch.Text)
                        Case 1 ' تاريخ الفاتورة
                            st = " where a.Bill_Date = '" & Format(CDate(txtSearch.Text), "yyyy/MM/dd") & "' order by a.Bill_Date"
                        Case 2 ' العميل
                            st = " where R.Customer_Name like '" & txtSearch.Text & "' order by a.Bill_Date"
                        Case 3 ' المشتري
                            st = " where a.buyer like '" & txtSearch.Text & "' order by a.Bill_Date"
                        Case 4 ' الموظف المستلم
                            st = " where e.EmployeeName like '" & txtSearch.Text & "' order by a.Bill_Date"
                    End Select
                    Myconn.Filldataset3("select a.buyer,a.Discount,a.Bill_Date,a.EmployeeID,a.Customer_ID,a.Bill_ID,m.Max_Unit_Name,n.Min_Unit_Name,a.Time_Add,
                                     c.Co_Name,a.Customer_Kind,b.Drug_Name,a.Drug_ID,a.Drug_exp,a.Amount_max,a.Amount_min,a.unit,a.Unit_Kind,a.Drug_Price,a.Pharm_discound,
                                     a.Total_Price,a.Erning,(d.EmployeeName) As Users,(e.EmployeeName) As Employee,a.ID,a.state,S.Drug_Sales_ID,
                                     isnull((s.Bill_ID),0) as Bill_Stock,R.Customer_Name,b.Min_Unit_number,a.buyer from Drug_Sales a
                                       Left Join Drugs b on a.Drug_ID = b.Drug_ID
                                       Left Join Employees d on a.Users_ID = d.EmployeeID
                                       Left Join Employees e on a.EmployeeID = e.EmployeeID
                                       Left Join Max_Unit M on a.Unit = M.Max_UnitID
                                       Left Join Min_Unit n on a.Unit = n.Min_UnitID
                                       Left Join Customers R on a.Customer_ID = R.Customer_ID
                                       left join Stocks_Purchases S on a.ID = S.Drug_Sales_ID
                                       Left Join Co_Drug c On b.Co_ID = c.Co_ID " & st, "Drug_Sales", Me)
                    If Myconn.cur3.Count = 0 Then
                        MessageBox.Show("هذه الرقم غير موجود ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                        txtBill_ID.Text = ""
                        txtBill_ID2.Text = ""
                        cbo_Emloyee.SelectedIndex = -1
                        cbo_Stock.SelectedIndex = -1
                        cboKind_Customer.SelectedIndex = -1
                        Return
                    End If
                    GroupBox1.Enabled = False
                    If IsDBNull(Myconn.cur3.Current("EmployeeID")) Then
                        cbo_Emloyee.Text = Myconn.cur3.Current("buyer")
                    Else
                        Myconn.comboBinding3("EmployeeID", cbo_Emloyee)
                    End If
                    dtp1.Text = Myconn.cur3.Current("Bill_Date")
                    txtBill_ID.Text = Myconn.cur3.Current("Bill_ID")
                    txtBill_ID2.Text = Myconn.cur3.Current("Bill_Stock")
                    cboKind_Customer.SelectedIndex = CInt(Myconn.cur3.Current("Customer_Kind"))
                    Myconn.comboBinding3("Customer_ID", cbo_Stock)

                    Dim V2, V1 As Decimal
                    For i As Integer = 0 To Myconn.cur3.Count - 1
                        drg.Rows.Add()
                        drg.Rows(i).Cells(0).Value = i + 1
                        drg.Rows(i).Cells(1).Value = Myconn.cur3.Current("Time_Add")
                        drg.Rows(i).Cells(2).Value = Myconn.cur3.Current("Co_Name")
                        drg.Rows(i).Cells(3).Value = Myconn.cur3.Current("Drug_Name")
                        drg.Rows(i).Cells(4).Value = Myconn.cur3.Current("Drug_ID")
                        drg.Rows(i).Cells(5).Value = Myconn.cur3.Current("Drug_exp")
                        drg.Rows(i).Cells(6).Value = Math.Round(If(Myconn.cur3.Current("Unit_Kind") = 0, Myconn.cur3.Current("Amount_min"), Myconn.cur3.Current("Amount_max")))
                        drg.Rows(i).Cells(7).Value = If(Myconn.cur3.Current("Unit_Kind") = 0, Myconn.cur3.Current("Min_Unit_Name"), Myconn.cur3.Current("Max_Unit_Name"))
                        drg.Rows(i).Cells(8).Value = Myconn.cur3.Current("Drug_Price")
                        drg.Rows(i).Cells(9).Value = Myconn.cur3.Current("Discount")
                        drg.Rows(i).Cells(10).Value = Myconn.cur3.Current("Pharm_discound")
                        drg.Rows(i).Cells(11).Value = Myconn.cur3.Current("Total_Price")
                        drg.Rows(i).Cells(12).Value = Myconn.cur3.Current("Erning")
                        drg.Rows(i).Cells(13).Value = If(IsDBNull(Myconn.cur3.Current("Employee")), Myconn.cur3.Current("Buyer"), Myconn.cur3.Current("Employee"))
                        drg.Rows(i).Cells(14).Value = Myconn.cur3.Current("Customer_Name")
                        drg.Rows(i).Cells(15).Value = Myconn.cur3.Current("Users")
                        drg.Rows(i).Cells(16).Value = Myconn.cur3.Current("buyer")
                        drg.Rows(i).Cells(17).Value = Myconn.cur3.Current("ID")
                        drg.Rows(i).Cells(18).Value = Myconn.cur3.Current("State")
                        If drg.Rows(i).Cells(18).Value = True Then
                            drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                            V1 += CDec(drg.Rows(i).Cells(12).Value)
                        Else
                            drg.Rows(i).DefaultCellStyle.BackColor = Color.Red
                            V2 += CDec(drg.Rows(i).Cells(11).Value)
                        End If
                        Myconn.cur3.Position += 1
                    Next
                    Myconn.Sum_drg2(drg, 11, Label39)
                    Label39.Text = Val(Label39.Text) - V2
                    Label38.Text = V2
                    Label43.Text = V1

            End Select
        Catch ex As Exception
            MsgBox("هناك خطأ ما ")
        End Try
    End Sub
    Sub Save_To_Drug_Sales()
        Dim sql As String = "INSERT INTO Drug_Sales(Bill_ID,Bill_Date,Time_Add,Drug_ID,Drug_exp,Drug_Price,Pharm_discound,Amount_max,Amount_min,Total_Price,Erning,Discount,Unit,Unit_Kind,Note,EmployeeID,Customer_ID,Users_ID, State,Customer_Kind,Buyer)
                                             VALUES(@Bill_ID,@Bill_Date,@Time_Add,@Drug_ID,@Drug_exp,@Drug_Price,@Pharm_discound,@Amount_max,@Amount_min,@Total_Price,@Erning,@Discount,@Unit,@Unit_Kind,@Note,@EmployeeID,@Customer_ID,@Users_ID,@State,@Customer_Kind,@Buyer)"

        Myconn.cmd = New SqlCommand(sql, Myconn.conn)
        With Myconn.cmd.Parameters
            .Add("@Bill_ID", SqlDbType.Int).Value = txtBill_ID.Text
            .Add("@Bill_Date", SqlDbType.NChar).Value = Format(CDate(dtp1.Text), "yyyy/MM/dd")
            .Add("@Time_Add", SqlDbType.NChar).Value = Label11.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
            .Add("@Drug_exp", SqlDbType.NChar).Value = drg_Exp.CurrentRow.Cells(1).Value
            .Add("@Drug_Price", SqlDbType.Decimal).Value = txtPrice.Text
            .Add("@Pharm_discound", SqlDbType.Decimal).Value = txtPharmacist_Discound.Text
            .Add("@Amount_max", SqlDbType.Int).Value = If(cbo_Unit.SelectedIndex = 1, CInt(txtAmount.Text), 0)
            .Add("@Amount_min", SqlDbType.Int).Value = If(cbo_Unit.SelectedIndex = 0, CInt(txtAmount.Text), 0)
            .Add("@Total_Price", SqlDbType.Decimal).Value = txtTotal_Price.Text
            .Add("@Erning", SqlDbType.Decimal).Value = Math.Round((Val(txtTotal_Price.Text) * Val(txtPharmacist_Discound.Text) / 100), 2)
            .Add("@Discount", SqlDbType.Decimal).Value = Math.Round(((Val(txtAmount.Text) * Val(txtPrice.Text)) - Val(txtTotal_Price.Text)), 2)
            .Add("@Unit", SqlDbType.Int).Value = txtUnit_ID.Text
            .Add("@Unit_Kind", SqlDbType.Int).Value = cbo_Unit.SelectedIndex
            .Add("@Note", SqlDbType.NVarChar).Value = If(txtNotes.Text = Nothing, DBNull.Value, txtNotes.Text)
            .Add("@EmployeeID", SqlDbType.Int).Value = If(cbo_Emloyee.SelectedIndex = -1, DBNull.Value, cbo_Emloyee.SelectedValue)
            .Add("@Customer_ID", SqlDbType.Int).Value = cbo_Stock.SelectedValue
            .Add("@Users_ID", SqlDbType.Int).Value = My.Settings.user_ID
            .Add("@State", SqlDbType.Bit).Value = 1
            .Add("@Customer_Kind", SqlDbType.Bit).Value = cboKind_Customer.SelectedIndex
            .Add("@Buyer", SqlDbType.NVarChar).Value = If(cbo_Emloyee.SelectedIndex > -1, DBNull.Value, cbo_Emloyee.Text)
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
        ID = 0
        Myconn.Filldataset5("select max(ID) as ID from Drug_Sales", "Drug_Sales", Me)
        ID = Myconn.cur5.Current("ID") '------------------------------------------------- ID المخزن
    End Sub
    Sub Save_To_Stock()
        Dim sql As String = "INSERT INTO Stocks_Purchases(Stock_ID,Bill_ID,Bill_Date,Bill_Time,Drug_ID,Drug_exp,Price,Amount_max,Amount_min,Unit,Unit_Kind,Total,EmployeeID,Supplier_ID,Drug_Sales_ID,Users_ID,State)
                                                             VALUES(@Stock_ID,@Bill_ID,@Bill_Date,@Bill_Time,@Drug_ID,@Drug_exp,@Price,@Amount_max,@Amount_min,@Unit,@Unit_Kind,@Total,@EmployeeID,@Supplier_ID,@Drug_Sales_ID,@Users_ID,@State)"

        Myconn.cmd = New SqlCommand(sql, Myconn.conn)
        With Myconn.cmd.Parameters
            .Add("@Stock_ID", SqlDbType.Int).Value = cbo_Stock.SelectedValue
            .Add("@Bill_ID", SqlDbType.Int).Value = txtBill_ID2.Text
            .Add("@Bill_Date", SqlDbType.NChar).Value = Format(CDate(dtp1.Text), "yyyy/MM/dd")
            .Add("@Bill_Time", SqlDbType.NChar).Value = Label11.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
            .Add("@Drug_exp", SqlDbType.NChar).Value = drg_Exp.CurrentRow.Cells(1).Value
            .Add("@Price", SqlDbType.Decimal).Value = txtPrice.Text
            .Add("@Amount_max", SqlDbType.Int).Value = If(cbo_Unit.SelectedIndex = 1, CInt(txtAmount.Text), 0)
            .Add("@Amount_min", SqlDbType.Int).Value = If(cbo_Unit.SelectedIndex = 0, CInt(txtAmount.Text), 0)
            .Add("@Unit", SqlDbType.Int).Value = txtUnit_ID.Text
            .Add("@Unit_Kind", SqlDbType.Int).Value = cbo_Unit.SelectedIndex
            .Add("@Total", SqlDbType.Decimal).Value = txtTotal_Price.Text
            .Add("@EmployeeID", SqlDbType.Int).Value = If(cbo_Emloyee.SelectedIndex = -1, DBNull.Value, cbo_Emloyee.SelectedValue)
            .Add("@Supplier_ID", SqlDbType.Int).Value = 1
            .Add("@Drug_Sales_ID", SqlDbType.Int).Value = ID
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
            Return
        End Try
    End Sub
    Sub Debit_From_Stock()
        Myconn.DeleteRecord("Stocks_Purchases", "Drug_Sales_ID", CInt(drg.CurrentRow.Cells(17).Value))
    End Sub
    Sub Updat_TO_Stock()
        Dim sql As String = "Update Stocks_Purchases set Stock_ID=@Stock_ID,Bill_ID=@Bill_ID,Bill_Date=@Bill_Date,Bill_Time=@Bill_Time,Drug_ID=@Drug_ID,Drug_exp=@Drug_exp,Price=@Price,Amount_max=@Amount_max,Amount_min=@Amount_min,Unit=@Unit,Unit_Kind=@Unit_Kind,Total=@Total,EmployeeID=@EmployeeID,Supplier_ID=@Supplier_ID,Users_ID=@Users_ID where Drug_Sales_ID =@Drug_Sales_ID"
        Myconn.cmd = New SqlCommand(sql, Myconn.conn)
        With Myconn.cmd.Parameters
            .Add("@Stock_ID", SqlDbType.Int).Value = cbo_Stock.SelectedValue
            .Add("@Bill_ID", SqlDbType.Int).Value = txtBill_ID2.Text
            .Add("@Bill_Date", SqlDbType.NChar).Value = Format(CDate(dtp1.Text), "yyyy/MM/dd")
            .Add("@Bill_Time", SqlDbType.NChar).Value = Label11.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
            .Add("@Drug_exp", SqlDbType.NChar).Value = drg_Exp.CurrentRow.Cells(1).Value
            .Add("@Price", SqlDbType.Decimal).Value = txtPrice.Text
            .Add("@Amount_max", SqlDbType.Int).Value = If(cbo_Unit.SelectedIndex = 1, CInt(txtAmount.Text), 0)
            .Add("@Amount_min", SqlDbType.Int).Value = If(cbo_Unit.SelectedIndex = 0, CInt(txtAmount.Text), 0)
            .Add("@Unit", SqlDbType.Int).Value = txtUnit_ID.Text
            .Add("@Unit_Kind", SqlDbType.Int).Value = cbo_Unit.SelectedIndex
            .Add("@Total", SqlDbType.Decimal).Value = txtTotal_Price.Text
            .Add("@EmployeeID", SqlDbType.Int).Value = If(cbo_Emloyee.SelectedIndex = -1, DBNull.Value, cbo_Emloyee.SelectedValue)
            .Add("@Supplier_ID", SqlDbType.Int).Value = 1
            .Add("@Users_ID", SqlDbType.Int).Value = My.Settings.user_ID
            .Add("@Drug_Sales_ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(16).Value)
        End With
        Try
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception

        End Try
    End Sub

#End Region
    Private Sub frmPharm_Sales_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label5.Left = 0
        Label5.Width = Me.Width
        Me.KeyPreview = True

        fin = False
        Myconn.Fillcombo2("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug)
        fin = True
        Timer1.Start()
    End Sub
    Private Sub frmPharm_Sales_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        'If e.KeyCode = Keys.Enter Then
        '    SendKeys.Send("{Tab}")
        'End If
    End Sub
#Region "Bttoun"
    Public Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        fin = False
        Y = 2
        Fillgrd()
        fin = True
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        For Each txt As Control In GroupBox1.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" And txt.Name IsNot "txtCustomer" And txt.Name IsNot "txtBill_ID" And txt.Name IsNot "txtNotes" Then
                    ErrorProvider1.SetError(txt, "أكمل البيانات")
                    MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return
                End If
            ElseIf TypeOf txt Is ComboBox Then
                If txt.Text = "" And txt.Enabled = True Then
                    ErrorProvider1.SetError(txt, "أكمل البيانات")
                    MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return
                End If
            End If
        Next
        drg.Rows.Clear()
        GroupBox1.Enabled = True
        fin2 = True
        NewRecord()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If CInt(txtBill_ID.Text) <= 0 Or Nothing Then
                ErrorProvider1.SetError(txtBill_ID, "أدخل رقم الفاتورة")
                Return
            End If
        Catch ex As Exception
            ErrorProvider1.SetError(txtBill_ID, "أدخل رقم الفاتورة")
            Return
        End Try


        If CInt(txtAmount.Text) <= 0 Then
            ErrorProvider1.SetError(txtAmount, "أدخل الكمية")
            Return
        End If

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
        If drg_Exp.Rows.Count = 0 Then
            MessageBox.Show("الكمية لا تسمح ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        ElseIf drg_Exp.CurrentRow.Cells(3).Value < Val(Val(txtAmount.Text) * Val(txtUnit_Number.Text)) Then
            MessageBox.Show("الكمية لا تسمح ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        Save_To_Drug_Sales()
        If cboKind_Customer.SelectedIndex = 0 Then Save_To_Stock()
        Y = 1
        Fillgrd()
        'MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        txtAmount.Text = 0
        txtDiscound.Text = 0
        GroupBox1.Enabled = False
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Myconn.DeleteRecord("Drug_Sales", "ID", CInt(drg.CurrentRow.Cells(17).Value))
            If cboKind_Customer.SelectedIndex = 0 Then Debit_From_Stock()
            Y = 1
            Fillgrd()
        End If
    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Dim sql As String = "Update  Drug_Sales set Bill_ID=@Bill_ID,Bill_Date=@Bill_Date,Time_Add=@Time_Add,Drug_ID=@Drug_ID,Amount_max=@Amount_max,Amount_min=@Amount_min
                                                        Drug_exp=@Drug_exp,Drug_Price=@Drug_Price,Pharm_discound=@Pharm_discound,Amount=@Amount,
                                                        Total_Price=@Total_Price,Erning=@Erning,Discount=@Discount,Unit=@Unit,Unit_Kind=@Unit_Kind,
                                                        Note=@Note,EmployeeID=@EmployeeID,Customer_ID=@Customer_ID,Users_ID=@Users_ID,Customer_Kind=@Customer_Kind,Buyer=@Buyer where ID =@ID"

        Myconn.cmd = New SqlCommand(sql, Myconn.conn)
        With Myconn.cmd.Parameters
            .Add("@Bill_ID", SqlDbType.Int).Value = txtBill_ID.Text
            .Add("@Bill_Date", SqlDbType.NChar).Value = Format(CDate(dtp1.Text), "yyyy/MM/dd")
            .Add("@Time_Add", SqlDbType.NChar).Value = Label11.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
            .Add("@Drug_exp", SqlDbType.NChar).Value = drg_Exp.CurrentRow.Cells(1).Value
            .Add("@Drug_Price", SqlDbType.Decimal).Value = txtPrice.Text
            .Add("@Pharm_discound", SqlDbType.Decimal).Value = txtPharmacist_Discound.Text
            .Add("@Amount_max", SqlDbType.Int).Value = If(cbo_Unit.SelectedIndex = 1, CInt(txtAmount.Text), 0)
            .Add("@Amount_min", SqlDbType.Int).Value = If(cbo_Unit.SelectedIndex = 0, CInt(txtAmount.Text), 0)
            .Add("@Total_Price", SqlDbType.Decimal).Value = txtTotal_Price.Text
            .Add("@Erning", SqlDbType.Decimal).Value = Math.Round((Val(txtTotal_Price.Text) * Val(txtPharmacist_Discound.Text) / 100), 2)
            .Add("@Discount", SqlDbType.Decimal).Value = Math.Round(((Val(txtAmount.Text) * Val(txtPrice.Text)) - Val(txtTotal_Price.Text)), 2)
            .Add("@Unit", SqlDbType.Int).Value = txtUnit_ID.Text
            .Add("@Unit_Kind", SqlDbType.Int).Value = cbo_Unit.SelectedIndex
            .Add("@Note", SqlDbType.NVarChar).Value = If(txtNotes.Text = Nothing, DBNull.Value, txtNotes.Text)
            .Add("@EmployeeID", SqlDbType.Int).Value = If(cbo_Emloyee.SelectedIndex = -1, DBNull.Value, cbo_Emloyee.SelectedValue)
            .Add("@Customer_ID", SqlDbType.Int).Value = cbo_Stock.SelectedValue
            .Add("@Users_ID", SqlDbType.Int).Value = My.Settings.user_ID
            .Add("@Customer_Kind", SqlDbType.Bit).Value = cboKind_Customer.SelectedIndex
            .Add("@Buyer", SqlDbType.NVarChar).Value = If(cbo_Emloyee.SelectedIndex > -1, DBNull.Value, cbo_Emloyee.Text)
            .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(17).Value)
        End With
        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()
        If cboKind_Customer.SelectedIndex = 0 Then Updat_TO_Stock()
        Y = 1
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim sql As String = "Update  Drug_Sales set State = @State where ID = @ID"
        Myconn.cmd = New SqlCommand(sql, Myconn.conn)

        If drg.CurrentRow.Cells(18).Value = True Then
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 0
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(17).Value)
            End With
        Else
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 1
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(17).Value)
            End With
        End If

        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()
        Y = 1
        Fillgrd()
    End Sub
#End Region

#Region "TextBox"
    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        ErrorProvider1.Clear()
        txtTotal_Price.Text = Math.Round((Val(Math.Round((Val(txtAmount.Text) * Val(txtPrice.Text)), 2)) * (1 - (Val(txtDiscound.Text) / 100))), 2)
        'UN = Math.Round((Val(txtAmount.Text) / Val(txtUnit_Number.Text)), 2)
    End Sub
    Private Sub txtAmount_Enter(sender As Object, e As EventArgs) Handles txtAmount.Enter
        txtAmount.Text = ""
    End Sub
    Private Sub txtAmount_KeyUp(sender As Object, e As KeyEventArgs) Handles txtAmount.KeyUp
        If e.KeyCode = Keys.Enter AndAlso e.Control = True Then
            btnSave_Click(Nothing, Nothing)
            cbo_Drug.Focus()
        ElseIf e.KeyCode = Keys.N AndAlso e.Control = True Then
            btnNew_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub txtDiscound_TextChanged(sender As Object, e As EventArgs) Handles txtDiscound.TextChanged
        txtTotal_Price.Text = Math.Round((Val(Math.Round((Val(txtAmount.Text) * Val(txtPrice.Text)), 2)) * (1 - (Val(txtDiscound.Text) / 100))), 2)
    End Sub
    Private Sub txtDiscound_KeyUp(sender As Object, e As KeyEventArgs) Handles txtDiscound.KeyUp
        If e.KeyCode = Keys.Enter AndAlso e.Control = True Then
            btnSave_Click(Nothing, Nothing)
            cbo_Drug.Focus()
        ElseIf e.KeyCode = Keys.N AndAlso e.Control = True Then
            btnNew_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub txtBarcode_Enter(sender As Object, e As EventArgs) Handles txtBarcode.Enter
        txtBarcode.Text = ""
    End Sub
    Private Sub txtBarcode_KeyUp(sender As Object, e As KeyEventArgs) Handles txtBarcode.KeyUp
        Try
            If e.KeyCode = Keys.Enter = True Then
                If txtBarcode.Text = "" Then
                    cbo_Drug.Focus()
                    Return
                End If
                Myconn.Filldataset2("select * from Drugs where Parcod =" & txtBarcode.Text, "Drugs", Me)
                Myconn.comboBinding2("Drug_ID", cbo_Drug)
                cbo_Unit.SelectedIndex = 0
                txtAmount.Focus()
            End If
        Catch ex As Exception
            Return
        End Try

    End Sub
    Private Sub txtBill_ID_TextChanged(sender As Object, e As EventArgs) Handles txtBill_ID.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtBarcode_TextChanged(sender As Object, e As EventArgs) Handles txtBarcode.TextChanged

    End Sub
#End Region

#Region "ComboBox"
    Private Sub cboKind_Customer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKind_Customer.SelectedIndexChanged
        ErrorProvider1.Clear()
        fin = False
        Myconn.Fillcombo5("select * from Customers where kind like '" & cboKind_Customer.SelectedItem & "' order by Customer_Name", "Customers", "Customer_ID", "Customer_Name", Me, cbo_Stock)
        Myconn.Fillcombo4("select * from Employees order by EmployeeName", "Employees", "EmployeeID", "EmployeeName", Me, cbo_Emloyee)
        fin = True


        Select Case cboKind_Customer.SelectedIndex
            Case 0 ' الداخلي
                Label4.Text = "المستلم"
                cbo_Emloyee.DropDownStyle = ComboBoxStyle.DropDown
                cbo_Emloyee.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Case 1 ' الخارجي
                Label4.Text = "المشتري"
                cbo_Emloyee.DropDownStyle = ComboBoxStyle.Simple
                cbo_Emloyee.AutoCompleteMode = AutoCompleteMode.None
        End Select
    End Sub
    Private Sub cbo_Stock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Stock.SelectedIndexChanged
        ErrorProvider1.Clear()
        ' اعطاء رقم لفاتورة المخزن العمليات او الاقامة او الطوارىء ....الخ
        If Not fin Or Not fin2 Then Return
        Dim d_table As DataTable = Myconn.LoadData("select isnull(max(Bill_ID),0) as Bill_ID from Stocks_Purchases where Stock_ID =" & CInt(cbo_Stock.SelectedValue))
        Dim row() As DataRow = d_table.Select
        If d_table.Rows.Count > 0 Then
            txtBill_ID2.Text = row(0)(0) + 1
        Else
            txtBill_ID2.Text = 0 + 1
        End If


        If Not fin Then Return
        X = CInt(cbo_Stock.SelectedValue)
    End Sub
    Private Sub cbo_Drug_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Drug.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        U = 0
        Binding()
        cbo_Unit_SelectedIndexChanged(Nothing, Nothing)
    End Sub
    Private Sub cbo_Emloyee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Emloyee.SelectedIndexChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub cbo_Drug_KeyUp(sender As Object, e As KeyEventArgs) Handles cbo_Drug.KeyUp
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub cbo_Unit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Unit.SelectedIndexChanged
        ErrorProvider1.Clear()
        U = 1
        Binding()
        txtTotal_Price.Text = Math.Round((Val(txtAmount.Text) * Val(txtPrice.Text)), 2)
        'V = cbo_Unit.SelectedIndex
        'UN = Math.Round((Val(txtAmount.Text) / Val(txtUnit_Number.Text)), 2)
    End Sub
    Private Sub cbo_Unit_KeyUp(sender As Object, e As KeyEventArgs) Handles cbo_Unit.KeyUp
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub cbo_Drug_Enter(sender As Object, e As EventArgs) Handles cbo_Drug.Enter
        Myconn.langAR()
    End Sub

#End Region
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label11.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        ' ملىء خانات النافذة عند الضغط على الداتا جريد
        Try
            Dim d_table As DataTable = Myconn.LoadData("Select * from Drug_Sales where ID =" & CInt(drg.CurrentRow.Cells(17).Value))
            Dim row() As DataRow = d_table.Select
            cbo_Drug.SelectedValue = row(0)(4)
        Catch ex As Exception
            Return
        End Try

    End Sub


End Class