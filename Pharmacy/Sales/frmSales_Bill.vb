Public Class frmSales_Bill
    Dim Myconn As New connect
    Dim st As String
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Try
            drg.Rows.Clear()
            Select Case cboSearch.ComboBox.SelectedIndex

                Case 0
                    st = "having a.Customer_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
                Case 1
                    st = "having a.Customer_ID =" & CInt(cbo_Co.ComboBox.SelectedValue) & "and a.Bill_ID =" & CInt(txt1.Text)
                Case 2
                    st = "having a.Customer_ID =" & CInt(cbo_Co.ComboBox.SelectedValue) & "and a.Bill_ID between " & CInt(txt1.Text) & " and " & CInt(txt2.Text)
                Case 3
                    st = "having a.Bill_ID =" & CInt(txt1.Text)
                Case 4
                    st = "having a.Bill_ID between " & CInt(txt1.Text) & " and " & CInt(txt2.Text)
                Case 5
                    st = "having a.Bill_Date ='" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'" & "and a.Customer_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
                Case 6
                    st = "having a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'And a.Customer_ID = " & CInt(cbo_Co.ComboBox.SelectedValue)
                Case 7
                    st = "having cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(DATEPART(mm,a.Bill_Date) as varchar(2))  = '" & Format(CDate(txt1.Text), "yyyy/MM").ToString & "'" & "and a.Customer_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
                Case 8
                    st = "having a.Bill_Date ='" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'"
                Case 9
                    st = "having cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))  = '" & Format(CDate(txt1.Text), "yyyy/MM").ToString & "'"
                Case 10
                    st = "having a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"
                Case 11
                    st = "having sum(a.Total_Price) =" & CDbl(txt1.Text)
                Case 12
                    st = "having sum(a.Total_Price) between " & CDbl(txt1.Text) & " and " & CDbl(txt2.Text)
                Case 13
                    st = "having c.State = 'false'"
                Case 14
                    st = "having c.State = 'false'" & "and a.Customer_ID =" & CInt(cbo_Co.ComboBox.SelectedValue)
                Case 15
                    st = "having a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'And a.Customer_ID = " & CInt(cbo_Co.ComboBox.SelectedValue) & "and c.State = 'false'"
                Case 16
                    st = Nothing
            End Select


            Myconn.Filldataset("Select a.Bill_ID,b.Customer_Name,e.EmployeeName,a.bill_date,sum(a.Total_Price) As Total,count(Drug_ID) As count_Drug1,isnull(c.back,0) As back,isnull(c.count_Drug,0) As count_Drug,
                                   (sum(a.Erning) - isnull(c.Er_back,0)) as Erning,(sum(a.Total_Price) - isnull(c.back,0)) as final from [dbo].[Drug_Sales] a
                                   left join [dbo].[Customers] b on a.Customer_ID = b.Customer_ID
								   left join [dbo].[Employees] e on a.EmployeeID = e.EmployeeID
                                   left join (select Bill_ID,State ,sum(Total_Price) as back,sum(Erning) as Er_back,count(Drug_ID) as count_Drug from [dbo].[Drug_Sales] group by Bill_ID,State having State ='false') c
                                   on a.Bill_ID = c.Bill_ID
                                   group by a.Bill_ID,b.Customer_Name,e.EmployeeName,a.bill_date,a.Bill_ID,c.Er_back,c.back,c.count_Drug,a.Customer_ID,c.state
                                   " & st & "order by a.bill_date", "Drug_Sales", Me)
            drg.Rows.Clear()
            If Myconn.cur.Count = 0 Then MsgBox("لا توجد نتائج")
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Bill_ID")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("bill_date")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Customer_Name")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("EmployeeName")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("count_Drug1")
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Total")
                drg.Rows(i).Cells(7).Value = Myconn.cur.Current("count_Drug")
                drg.Rows(i).Cells(8).Value = Myconn.cur.Current("back")
                drg.Rows(i).Cells(9).Value = Myconn.cur.Current("final")
                drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Erning")
                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg(drg, 9, Label42, Label41)
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
        End Try


    End Sub

    Private Sub frmSales_Bill_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        Myconn.Fillcombo("select * from Customers order by Customer_Name", "Customers", "Customer_ID", "Customer_Name", Me, cbo_Co.ComboBox)

    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Fillgrd()
    End Sub
    Private Sub cboSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSearch.SelectedIndexChanged
        Select Case cboSearch.ComboBox.SelectedIndex

            Case 0
                txt1.Visible = False
                cbo_Co.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 1
                txt1.Visible = True
                cbo_Co.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 2
                txt1.Visible = True
                cbo_Co.Visible = True
                Label1.Visible = True
                txt2.Visible = True
            Case 3
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 4
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = True
                txt2.Visible = True
            Case 5
                txt1.Visible = True
                cbo_Co.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 6
                txt1.Visible = True
                cbo_Co.Visible = True
                Label1.Visible = True
                txt2.Visible = True
            Case 7
                txt1.Visible = True
                cbo_Co.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 8
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 9
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 10
                txt1.Visible = True
                cbo_Co.Visible = False
                Label1.Visible = True
                txt2.Visible = True
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
                txt1.Visible = False
                cbo_Co.Visible = False
                Label1.Visible = False
                txt2.Visible = False
            Case 14
                txt1.Visible = False
                cbo_Co.Visible = True
                Label1.Visible = False
                txt2.Visible = False
            Case 15
                txt1.Visible = True
                cbo_Co.Visible = True
                Label1.Visible = True
                txt2.Visible = True
            Case 16
                txt1.Visible = False
                cbo_Co.Visible = False
                Label1.Visible = False
                txt2.Visible = False
        End Select
    End Sub

    Private Sub drg_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellDoubleClick
        frmPharm_Sales.MdiParent = Main
        frmPharm_Sales.Show()
        frmPharm_Sales.cbo_search.ComboBox.SelectedIndex = 0
        frmPharm_Sales.txtSearch.Text = drg.CurrentRow.Cells(1).Value
        'frmPharm_Sales.Fillgrd()
        frmPharm_Sales.btnSearch_Click(Nothing, Nothing)
    End Sub
End Class