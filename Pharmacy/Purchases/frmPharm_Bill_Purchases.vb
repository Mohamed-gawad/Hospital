Public Class frmPharm_Bill_Purchases
    Dim Myconn As New connect
    Dim st As String
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 

        drg.Rows.Clear()
        Select Case cboSearch.ComboBox.SelectedIndex
            Case 0
                st = "having a.Bill_ID =" & CInt(txt1.Text)
            Case 1
                st = "having a.Bill_ID between " & CInt(txt1.Text) & " and " & CInt(txt2.Text)
            Case 2
                st = "having a.Supplier_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 3
                st = "having a.Supplier_ID =" & CInt(cbo_Co.ComboBox.SelectedValue) & "and a.Bill_number =" & CInt(txt1.Text)
            Case 4
                st = "having a.Supplier_ID =" & CInt(cbo_Co.ComboBox.SelectedValue) & "and a.Bill_number between " & CInt(txt1.Text) & " and " & CInt(txt2.Text)
            Case 5
                st = "having a.Bill_number =" & CInt(txt1.Text)
            Case 6
                st = "having a.Bill_number between " & CInt(txt1.Text) & " and " & CInt(txt2.Text)
            Case 7
                st = "having a.Bill_Date ='" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'" & "and a.Supplier_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 8
                st = "having a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'And a.Supplier_ID = " & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 9
                st = "having cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(DATEPART(mm,a.Bill_Date) as varchar(2))  = '" & Format(CDate(txt1.Text), "yyyy/MM").ToString & "'" & "and a.Supplier_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 10
                st = "having a.Bill_Date ='" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'"
            Case 11
                st = "having cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))  = '" & Format(CDate(txt1.Text), "yyyy/MM").ToString & "'"
            Case 12
                st = "having a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"
            Case 13
                st = "having sum(a.Total_Price_tax) =" & CDbl(txt1.Text)
            Case 14
                st = "having sum(a.Total_Price_tax) between " & CDbl(txt1.Text) & " and " & CDbl(txt2.Text)
            Case 15
                st = "having c.State = 'false'"
            Case 16
                st = "having c.State = 'false'" & "and a.Supplier_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
            Case 17
                st = "having a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'And a.Supplier_ID = " & CInt(cbo_Co.ComboBox.SelectedValue) & "and c.State = 'false'"

            Case 18
                st = Nothing
        End Select

        Try
            Myconn.Filldataset("Select a.Bill_number,b.Supplier_Name,a.bill_date,sum(a.Total_Price_tax) As Total,count(Drug_ID) As count_Drug1,isnull(c.back,0) As back,isnull(c.count_Drug,0) As count_Drug,
                                   (sum(a.Total_Price_tax) - isnull(c.back,0)) as final,a.Bill_ID from [dbo].[Drug_Purchases] a
                                   left join [dbo].[Supplier] b on a.Supplier_ID = b.Supplier_ID
                                   left join (select Bill_ID,State ,sum(Total_Price_tax) as back,count(Drug_ID) as count_Drug from [dbo].[Drug_Purchases] group by Bill_ID,State having State ='false') c
                                   on a.Bill_ID = c.Bill_ID
                                   group by a.Bill_number,b.Supplier_Name,a.bill_date,a.Bill_ID,c.back,c.count_Drug,a.Supplier_ID,c.state
                                   " & st & "order by a.bill_date", "Drug_Purchases", Me)
            drg.Rows.Clear()
            If Myconn.cur.Count = 0 Then MsgBox("لا توجد نتائج")
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Bill_ID")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Bill_number")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("bill_date")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Supplier_Name")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("count_Drug1")
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Total")
                drg.Rows(i).Cells(7).Value = Myconn.cur.Current("count_Drug")
                drg.Rows(i).Cells(8).Value = Myconn.cur.Current("back")
                drg.Rows(i).Cells(9).Value = Myconn.cur.Current("final")
                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg(drg, 9, Label42, Label41)
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
        End Try


    End Sub

    Private Sub frmPharm_Bill_Purchases_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        Myconn.Fillcombo("select * from Supplier order by Supplier_Name", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Co.ComboBox)
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Fillgrd()
    End Sub
    Private Sub cboSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSearch.SelectedIndexChanged
        Select Case cboSearch.ComboBox.SelectedIndex
            Case 0
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 1
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = True
                txt2.Visible = True
            Case 2
                txt1.Visible = False
                cbo_Co.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 3
                txt1.Visible = True
                cbo_Co.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 4
                txt1.Visible = True
                cbo_Co.Visible = True
                Label1.Visible = True
                txt2.Visible = True
            Case 5
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 6
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = True
                txt2.Visible = True
            Case 7
                txt1.Visible = True
                cbo_Co.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 8
                txt1.Visible = True
                cbo_Co.Visible = True
                Label1.Visible = True
                txt2.Visible = True
            Case 9
                txt1.Visible = True
                cbo_Co.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 10
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 11
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 12
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = True
                txt2.Visible = True
            Case 13
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 14
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = True
                txt2.Visible = True
            Case 15
                txt1.Visible = False
                cbo_Co.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 16
                txt1.Visible = False
                cbo_Co.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 17
                txt1.Visible = True
                cbo_Co.Visible = True
                Label1.Visible = True
                txt2.Visible = True
            Case 18
                txt1.Visible = False
                cbo_Co.Visible = False
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