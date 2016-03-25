Public Class frmReturns_Sales
    Dim Myconn As New connect
    Dim x As Integer
    Dim st As String
    Dim Unit2 As String


    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 

        drg.Rows.Clear()
        Select Case cboSearch.ComboBox.SelectedIndex
            Case 0 ' رقم الفاتورة
                st = "and a.Bill_ID =" & CInt(txt1.Text)
            Case 1 ' مجموعة فواتير
                st = "and a.Bill_ID between " & CInt(txt1.Text) & " and " & CInt(txt2.Text)
            Case 2 ' تاريخ محدد
                st = "and a.Bill_Date ='" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'"
            Case 3 ' فترة محددة
                st = "and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"
            Case 4 ' عميل
                st = "and a.Customer_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 5 ' عميل وتاريخ
                st = "and a.Bill_Date ='" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'and a.Customer_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 6 ' عميل وفترة محددة
                st = "and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'and a.Customer_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 7 ' صنف
                st = "and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue)
            Case 8 ' صنف وتاريخ
                st = "and a.Bill_Date ='" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue)
            Case 9 ' صنف وفترة
                st = "and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue)
            Case 10 ' صنف وعميل
                st = "and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue) & "and a.Customer_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 11 ' جميع المرتجعات
                st = Nothing
        End Select
        Try
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
                           Left Join Co_Drug c On b.Co_ID = c.Co_ID
                           where a.State = 'false'" & st & "order by a.bill_date", "Drug_Purchases", Me)



            drg.Rows.Clear()
            If Myconn.cur3.Count = 0 Then MsgBox("لا توجد نتائج")

            For i As Integer = 0 To Myconn.cur3.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur3.Current("Bill_ID")
                drg.Rows(i).Cells(2).Value = Myconn.cur3.Current("Bill_Date")
                drg.Rows(i).Cells(3).Value = Myconn.cur3.Current("Time_Add")
                drg.Rows(i).Cells(4).Value = Myconn.cur3.Current("Co_Name")
                drg.Rows(i).Cells(5).Value = Myconn.cur3.Current("Drug_Name")
                drg.Rows(i).Cells(6).Value = Myconn.cur3.Current("Drug_ID")
                drg.Rows(i).Cells(7).Value = Myconn.cur3.Current("Drug_exp")
                drg.Rows(i).Cells(8).Value = If(Myconn.cur3.Current("Unit_Kind") = 0, Myconn.cur3.Current("Amount_min"), Myconn.cur3.Current("Amount_max"))
                drg.Rows(i).Cells(9).Value = If(Myconn.cur3.Current("Unit_Kind") = 0, Myconn.cur3.Current("Min_Unit_Name"), Myconn.cur3.Current("Max_Unit_Name"))
                drg.Rows(i).Cells(10).Value = Myconn.cur3.Current("Drug_Price")
                drg.Rows(i).Cells(11).Value = Myconn.cur3.Current("Discount")
                drg.Rows(i).Cells(12).Value = Myconn.cur3.Current("Pharm_discound")
                drg.Rows(i).Cells(13).Value = Myconn.cur3.Current("Total_Price")
                drg.Rows(i).Cells(14).Value = Myconn.cur3.Current("Erning")
                drg.Rows(i).Cells(15).Value = Myconn.cur3.Current("Employee")
                drg.Rows(i).Cells(16).Value = Myconn.cur3.Current("Customer_Name")
                drg.Rows(i).Cells(17).Value = Myconn.cur3.Current("Users")
                drg.Rows(i).Cells(18).Value = Myconn.cur3.Current("buyer")
                drg.Rows(i).Cells(19).Value = Myconn.cur3.Current("ID")
                Myconn.cur3.Position += 1
            Next

            Myconn.Sum_drg(drg, 13, Label42, Label41)
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
        End Try
    End Sub

    Private Sub frmReturns_Sales_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        Myconn.Fillcombo("select * from Customers order by Customer_Name", "Customers", "Customer_ID", "Customer_Name", Me, cbo_Co.ComboBox)
        Myconn.Fillcombo2("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug.ComboBox)

    End Sub
    Private Sub cboSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSearch.SelectedIndexChanged
        Select Case cboSearch.ComboBox.SelectedIndex
            Case 0 ' رقم الفاتورة
                txt1.Visible = True
                cbo_Co.Visible = False
                cbo_Drug.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 1 ' مجموعة فواتير
                txt1.Visible = True
                cbo_Co.Visible = False
                cbo_Drug.Visible = False
                Label1.Visible = True
                txt2.Visible = True
            Case 2 ' تاريخ محدد
                txt1.Visible = True
                cbo_Co.Visible = False
                cbo_Drug.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 3 ' فترة محددة
                txt1.Visible = True
                cbo_Co.Visible = False
                cbo_Drug.Visible = False
                Label1.Visible = True
                txt2.Visible = True
            Case 4 ' عميل
                txt1.Visible = False
                cbo_Co.Visible = True
                cbo_Drug.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 5 ' عميل وتاريخ
                txt1.Visible = True
                cbo_Co.Visible = True
                cbo_Drug.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 6 ' عميل وفترة محددة
                txt1.Visible = True
                cbo_Co.Visible = True
                cbo_Drug.Visible = False
                Label1.Visible = True
                txt2.Visible = True
            Case 7 ' صنف
                txt1.Visible = False
                cbo_Co.Visible = False
                cbo_Drug.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 8 ' صنف وتاريخ
                txt1.Visible = True
                cbo_Co.Visible = False
                cbo_Drug.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 9 '  صنف وفترة
                txt1.Visible = True
                cbo_Co.Visible = False
                cbo_Drug.Visible = True
                Label1.Visible = True
                txt2.Visible = True
            Case 10 ' صنف  وعميل
                txt1.Visible = False
                cbo_Co.Visible = True
                cbo_Drug.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 11 ' صنف وعميل
                txt1.Visible = False
                cbo_Co.Visible = False
                cbo_Drug.Visible = False
                Label1.Visible = False
                txt2.Visible = False

        End Select
    End Sub
    Private Sub drg_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellDoubleClick
        frmPharm_Sales.MdiParent = Main
        frmPharm_Sales.Show()
        frmPharm_Sales.txtSearch.Text = drg.CurrentRow.Cells(1).Value
        frmPharm_Sales.cbo_search.ComboBox.SelectedIndex = 0
        frmPharm_Sales.btnSearch_Click(Nothing, Nothing)

    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Fillgrd()

    End Sub
End Class