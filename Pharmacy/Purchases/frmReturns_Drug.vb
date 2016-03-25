Public Class frmReturns_Drug
    Dim Myconn As New connect
    Dim x As Integer
    Dim st As String
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 

        drg.Rows.Clear()
        Select Case cboSearch.ComboBox.SelectedIndex
            Case 0 ' مسلسل
                st = "and a.Bill_ID =" & CInt(txt1.Text)
            Case 1 ' مجموعة مسلسلات
                st = "and a.Bill_ID between " & CInt(txt1.Text) & " and " & CInt(txt2.Text)
            Case 2 ' شركة
                st = "and a.Supplier_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 3 ' شركة + مسلسل
                st = "and a.Bill_ID =" & CInt(txt1.Text) & "and a.Supplier_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 4 ' شركة + مجموعة مسلسلات
                st = "and a.Bill_ID between " & CInt(txt1.Text) & " and " & CInt(txt2.Text) & "and a.Supplier_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 5 ' شركة + رقم فاتورة
                st = "and a.Bill_number =" & CInt(txt1.Text) & "and a.Supplier_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 6 ' شركة وجموعة ارقام فواتير
                st = "and a.Bill_number between " & CInt(txt1.Text) & " and " & CInt(txt2.Text) & "and a.Supplier_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 7 ' تاريخ
                st = "and cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(DATEPART(mm,a.Bill_Date) as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM").ToString & "'"
            Case 8 ' فترة محددة
                st = "and cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(DATEPART(mm,a.Bill_Date) as varchar(2)) between '" & Format(CDate(txt1.Text), "yyyy/MM").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM").ToString & "'"
            Case 9 ' شركة وتاريخ (شهر)
                st = "and cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(DATEPART(mm,a.Bill_Date) as varchar(2)) ='" & Format(CDate(txt1.Text), "yyyy/MM").ToString & "'And a.Supplier_ID = " & CInt(cbo_Co.ComboBox.SelectedValue)

            Case 10 ' شركة وفترة
                st = "and cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(DATEPART(mm,a.Bill_Date) as varchar(2)) between '" & Format(CDate(txt1.Text), "yyyy/MM").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM").ToString & "'And a.Supplier_ID = " & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 11 ' صنف
                st = "and  a.Drug_ID = " & CInt(cbo_Drug.ComboBox.SelectedValue)
            Case 12 ' صنف وشركة
                st = "and  a.Drug_ID = " & CInt(cbo_Drug.ComboBox.SelectedValue) & "And a.Supplier_ID = " & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 13
                st = Nothing
        End Select

        Try
            Myconn.Filldataset("select a.Bill_ID,a.bill_date,b.Supplier_Name,a.Drug_ID,d.Drug_Name,a.Public_Price,a.Drug_Amount,a.Drug_Bonus,a.Public_Price,a.Pharmacist_Price,a.Sales_tax,a.Total_Price_tax,a.Bill_number,b.Supplier_Name,a.bill_date
                                from [dbo].[Drug_Purchases] a
                                left join [dbo].[Supplier] b on a.Supplier_ID = b.Supplier_ID
                                left join [dbo].[Drugs] d on a.Drug_ID=d.Drug_ID
                                group by d.Drug_Name,a.Drug_ID,a.Public_Price,a.Bill_number,b.Supplier_Name,a.bill_date,a.Bill_ID ,a.Drug_Amount,a.State,a.Drug_Bonus,a.Public_Price,a.Pharmacist_Price,a.Sales_tax,a.Total_Price_tax,a.bill_date,a.Supplier_ID 
                                having a.State = 'false'" & st & "order by a.bill_date", "Drug_Purchases", Me)

            drg.Rows.Clear()
            If Myconn.cur.Count = 0 Then MsgBox("لا توجد نتائج")
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Bill_ID")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Bill_number")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("bill_date")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Supplier_Name")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Drug_Name")
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Drug_ID")
                drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Pharmacist_Price")
                drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Sales_tax")
                drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Public_Price")
                drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Drug_Amount")
                drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Drug_Bonus")
                drg.Rows(i).Cells(12).Value = Myconn.cur.Current("Total_Price_tax")
                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg(drg, 12, Label42, Label41)
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
        End Try
    End Sub
    Private Sub frmReturns_Drug_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        Myconn.Fillcombo("select * from Supplier order by Supplier_Name", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Co.ComboBox)
        Myconn.Fillcombo2("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug.ComboBox)
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Fillgrd()
    End Sub
    Private Sub cboSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSearch.SelectedIndexChanged
        Select Case cboSearch.ComboBox.SelectedIndex
            Case 0 ' مسلسل
                txt1.Visible = True
                cbo_Co.Visible = False
                cbo_Drug.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 1 ' مجموعة مسلسلات
                txt1.Visible = True
                cbo_Co.Visible = False
                cbo_Drug.Visible = False
                Label1.Visible = True
                txt2.Visible = True
            Case 2 ' شركة
                txt1.Visible = False
                cbo_Co.Visible = True
                cbo_Drug.Visible = False
                'Myconn.Fillcombo("select * from Supplier", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Co.ComboBox)
                Label1.Visible = False
                txt2.Visible = False
            Case 3 ' شركة + مسلسل
                txt1.Visible = True
                cbo_Co.Visible = True
                cbo_Drug.Visible = False
                'Myconn.Fillcombo("select * from Supplier", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Co.ComboBox)
                Label1.Visible = False
                txt2.Visible = False
            Case 4 ' شركة + مجموعة مسلسلات
                txt1.Visible = True
                cbo_Co.Visible = True
                cbo_Drug.Visible = False
                'Myconn.Fillcombo("select * from Supplier", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Co.ComboBox)
                Label1.Visible = True
                txt2.Visible = True
            Case 5 ' شركة + رقم فاتورة
                txt1.Visible = True
                cbo_Co.Visible = True
                cbo_Drug.Visible = False
                'Myconn.Fillcombo("select * from Supplier", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Co.ComboBox)
                Label1.Visible = False
                txt2.Visible = False
            Case 6 ' شركة وجموعة ارقام فواتير
                txt1.Visible = True
                cbo_Co.Visible = True
                cbo_Drug.Visible = False
                'Myconn.Fillcombo("select * from Supplier", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Co.ComboBox)
                Label1.Visible = True
                txt2.Visible = True
            Case 7 ' تاريخ
                txt1.Visible = True
                cbo_Co.Visible = False
                cbo_Drug.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 8 ' فترة محددة
                txt1.Visible = True
                cbo_Co.Visible = False
                cbo_Drug.Visible = False
                Label1.Visible = True
                txt2.Visible = True
            Case 9 ' شركة وتاريخ (شهر)
                txt1.Visible = True
                cbo_Co.Visible = True
                cbo_Drug.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 10 ' شركة وفترة
                txt1.Visible = True
                cbo_Co.Visible = True
                cbo_Drug.Visible = False
                Label1.Visible = True
                txt2.Visible = True
            Case 11 ' صنف
                txt1.Visible = False
                cbo_Co.Visible = False
                cbo_Drug.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 12 ' صنف وشركة
                txt1.Visible = False
                cbo_Co.Visible = True
                cbo_Drug.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 13
                txt1.Visible = False
                cbo_Co.Visible = False
                cbo_Drug.Visible = False
                Label1.Visible = False
                txt2.Visible = False
        End Select
    End Sub
    Private Sub drg_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellDoubleClick
        frmPharm_Purchases_bill.MdiParent = Main
        frmPharm_Purchases_bill.Show()
        frmPharm_Purchases_bill.txtSearch.Text = drg.CurrentRow.Cells(1).Value
        frmPharm_Purchases_bill.cbo_search.ComboBox.SelectedIndex = 0
        frmPharm_Purchases_bill.Fillgrd()

    End Sub
End Class