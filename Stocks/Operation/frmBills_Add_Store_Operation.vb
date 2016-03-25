Public Class frmBills_Add_Store_Operation
    Dim StockID As Integer
    Dim Myconn As New connect
    Dim st, AD As String

    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Try
            drg.Rows.Clear()
            Select Case cboSearch.ComboBox.SelectedIndex
                Case 0 ' مورد
                    st = "having a.Stock_ID = " & StockID & " and a.Supplier_ID =" & CInt(cbo_Supplier.ComboBox.SelectedValue)
                Case 1 ' مورد ورقم فاتورة
                    If txt1.Text = "" Then
                        MsgBox("أدخل رقم الفاتورة")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and a.Supplier_ID =" & CInt(cbo_Supplier.ComboBox.SelectedValue) & "and a.Bill_ID =" & CInt(txt1.Text)
                Case 2 ' مورد ومجموعة فواتير
                    If txt1.Text = "" Or txt2.Text = "" Then
                        MsgBox("أدخل رقم الفاتورة")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and a.Supplier_ID =" & CInt(cbo_Supplier.ComboBox.SelectedValue) & "and a.Bill_number between " & CInt(txt1.Text) & " and " & CInt(txt2.Text)
                Case 3 ' رقم فاتورة
                    If txt1.Text = "" Then
                        MsgBox("أدخل رقم الفاتورة")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and a.Bill_ID =" & CInt(txt1.Text)
                Case 4 ' مجموعة فواتير
                    If txt1.Text = "" Or txt2.Text = "" Then
                        MsgBox("أدخل رقم الفاتورة")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and a.Bill_ID between " & CInt(txt1.Text) & " and " & CInt(txt2.Text)
                Case 5 ' مورد وتاريخ محدد
                    If txt1.Text = "" Then
                        MsgBox("أدخل التاريخ")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and a.Bill_Date ='" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'" & "and a.Supplier_ID =" & CInt(cbo_Supplier.ComboBox.SelectedValue)
                Case 6 ' مورد وفترة زمنية
                    If txt1.Text = "" Or txt2.Text = "" Then
                        MsgBox("أدخل التاريخ")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'And a.Supplier_ID = " & CInt(cbo_Supplier.ComboBox.SelectedValue)
                Case 7 ' مورد وشهر محدد
                    If txt1.Text = "" Then
                        MsgBox("أدخل التاريخ")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(DATEPART(mm,a.Bill_Date) as varchar(2))  = '" & Format(CDate(txt1.Text), "yyyy/MM").ToString & "' and a.Supplier_ID =" & CInt(cbo_Supplier.ComboBox.SelectedValue)
                Case 8 ' تاريخ محدد
                    If txt1.Text = "" Then
                        MsgBox("أدخل التاريخ")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and a.Bill_Date ='" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'"
                Case 9 ' شهر محدد
                    If txt1.Text = "" Then
                        MsgBox("أدخل التاريخ")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))  = '" & Format(CDate(txt1.Text), "yyyy/MM").ToString & "'"
                Case 10 ' فترة زمنية
                    If txt1.Text = "" Or txt2.Text = "" Then
                        MsgBox("أدخل التاريخ")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"
                Case 11 ' مستلم
                    st = "having a.Stock_ID = " & StockID & " and a.EmployeeID =" & CInt(cboEmployee.ComboBox.SelectedValue)
                Case 12 ' مستلم وفترة
                    If txt1.Text = "" Or txt2.Text = "" Then
                        MsgBox("أدخل التاريخ")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'and a.EmployeeID =" & CInt(cboEmployee.ComboBox.SelectedValue)
                Case 13 ' قيمة فاتورة
                    If txt1.Text = "" Then
                        MsgBox("أدخل رقم الفاتورة")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and sum(a.Total) =" & CDbl(txt1.Text)
                Case 14 ' قيم فواتير
                    If txt1.Text = "" Or txt2.Text = "" Then
                        MsgBox("أدخل رقم الفاتورة")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and sum(a.Total) between " & CDbl(txt1.Text) & " and " & CDbl(txt2.Text)
                Case 15 ' فواتير المرتجعات
                    st = "having a.Stock_ID = " & StockID & " andc.State = 'false'"
                Case 16 ' المورد وفواتير المرتجعات
                    st = "having a.Stock_ID = " & StockID & " and c.State = 'false'" & "and a.Supplier_ID =" & CInt(cbo_Supplier.ComboBox.SelectedValue)
                Case 17 ' المورد وفواتير المرتجعات والتاريخ
                    st = "having a.Stock_ID = " & StockID & " and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'And a.Supplier_ID = " & CInt(cbo_Supplier.ComboBox.SelectedValue) & "and c.State = 'false'"
                Case 18 ' كل الفواتير
                    st = "having a.Stock_ID = " & StockID & ""
            End Select


            Myconn.Filldataset("Select a.Bill_ID,b.Supplier_Name,a.bill_date,sum(a.Total) As Total,count(Drug_ID) As count_Drug1,isnull(c.back,0) As back,isnull(c.count_Drug,0) As count_Drug,
                                (sum(a.Total) - isnull(c.back,0)) as final, e.EmployeeName from [dbo].[Stocks_Purchases] a
                                left join [dbo].[Supplier] b on a.Supplier_ID = b.Supplier_ID
                                left join (select Bill_ID,State ,sum(Total) as back,count(Drug_ID) as count_Drug from [dbo].[Stocks_Purchases] group by Bill_ID,State having State ='false') c
                                on a.Bill_ID = c.Bill_ID
                                left join Employees e on a.EmployeeID = e.EmployeeID
                                group by a.Bill_ID,b.Supplier_Name,a.bill_date,c.back,c.count_Drug,a.Supplier_ID,c.state,e.EmployeeName,a.EmployeeID,a.Stock_ID " & st & "order by a.bill_date", "Stocks_Purchases", Me)



            drg.Rows.Clear()
            If Myconn.cur.Count = 0 Then MsgBox("لا توجد نتائج")
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Bill_ID")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("bill_date")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Supplier_Name")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("count_Drug1")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Total")
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("count_Drug")
                drg.Rows(i).Cells(7).Value = Myconn.cur.Current("back")
                drg.Rows(i).Cells(8).Value = Myconn.cur.Current("final")
                drg.Rows(i).Cells(9).Value = Myconn.cur.Current("EmployeeName")
                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg(drg, 8, Label42, Label41)
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
        End Try


    End Sub
    Private Sub frmBills_Add_Store_Operation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label5.Left = 0
        Label5.Width = Me.Width
        StockID = 2
        Myconn.Fillcombo("select * from Supplier order by Supplier_Name", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Supplier.ComboBox)
        Myconn.Fillcombo1("select * from Employees order by EmployeeName", "Employees", "EmployeeID", "EmployeeName", Me, cboEmployee.ComboBox)
        cbo_Supplier.Visible = False
        cboEmployee.Visible = False
    End Sub
    Private Sub cboSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSearch.SelectedIndexChanged
        Select Case cboSearch.ComboBox.SelectedIndex

            Case 0 ' مورد
                cbo_Supplier.Visible = True
                cboEmployee.Visible = False
                Label1.Visible = False
                txt1.Visible = False
                txt2.Visible = False
            Case 1 ' مورد ورقم فاتورة
                cbo_Supplier.Visible = True
                cboEmployee.Visible = False
                Label1.Visible = False
                txt1.Visible = True
                txt2.Visible = False
            Case 2 ' مورد ومجموعة فواتير
                cbo_Supplier.Visible = True
                cboEmployee.Visible = False
                Label1.Visible = True
                txt1.Visible = True
                txt2.Visible = True
            Case 3 ' رقم فاتورة
                cbo_Supplier.Visible = False
                cboEmployee.Visible = False
                Label1.Visible = False
                txt1.Visible = True
                txt2.Visible = False
            Case 4 ' مجموعة فواتير
                cbo_Supplier.Visible = False
                cboEmployee.Visible = False
                Label1.Visible = True
                txt1.Visible = True
                txt2.Visible = True
            Case 5 ' مورد وتاريخ محدد
                cbo_Supplier.Visible = True
                cboEmployee.Visible = False
                Label1.Visible = False
                txt1.Visible = True
                txt2.Visible = False
            Case 6 ' مورد وفترة زمنية
                cbo_Supplier.Visible = True
                cboEmployee.Visible = False
                Label1.Visible = True
                txt1.Visible = True
                txt2.Visible = True
            Case 7 ' مورد وشهر محدد
                cbo_Supplier.Visible = True
                cboEmployee.Visible = False
                Label1.Visible = False
                txt1.Visible = True
                txt2.Visible = False
            Case 8 ' تاريخ محدد
                cbo_Supplier.Visible = False
                cboEmployee.Visible = False
                Label1.Visible = False
                txt1.Visible = True
                txt2.Visible = False
            Case 9 ' شهر محدد
                cbo_Supplier.Visible = False
                cboEmployee.Visible = False
                Label1.Visible = False
                txt1.Visible = True
                txt2.Visible = False
            Case 10 ' فترة زمنية
                cbo_Supplier.Visible = False
                cboEmployee.Visible = False
                Label1.Visible = True
                txt1.Visible = True
                txt2.Visible = True
            Case 11 ' مستلم
                cbo_Supplier.Visible = False
                cboEmployee.Visible = True
                txt1.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 12 ' مستلم وفترة
                cbo_Supplier.Visible = False
                cboEmployee.Visible = True
                Label1.Visible = True
                txt1.Visible = True
                txt2.Visible = True
            Case 13 ' قيمة فاتورة
                cbo_Supplier.Visible = False
                Label1.Visible = False
                txt1.Visible = True
                txt2.Visible = False
            Case 14 ' قيم فواتير
                cbo_Supplier.Visible = False
                Label1.Visible = True
                txt1.Visible = True
                txt2.Visible = True
            Case 15 ' فواتير المرتجعات
                cbo_Supplier.Visible = False
                txt1.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 16 ' المورد وفواتير المرتجعات
                cbo_Supplier.Visible = True
                Label1.Visible = False
                txt1.Visible = False
                txt2.Visible = False
            Case 17 ' المورد وفواتير المرتجعات والتاريخ
                cbo_Supplier.Visible = True
                Label1.Visible = True
                txt1.Visible = True
                txt2.Visible = True
            Case 18 ' كل الفواتير
                txt1.Visible = False
                cbo_Supplier.Visible = False
                Label1.Visible = False
                txt2.Visible = False
        End Select
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Fillgrd()
    End Sub
    Private Sub frmBills_Add_Store_Operation_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        StockID = 2
    End Sub
End Class