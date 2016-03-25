Public Class frmPharm_daily_Purchases
    Dim Myconn As New connect
    Dim st As String

    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Try
            drg.Rows.Clear()
            Select Case cboSearch.ComboBox.SelectedIndex

                Case 0
                    Myconn.Filldataset("Select count(DISTINCT(a.Bill_ID)) as col_num,a.bill_date,sum(a.Total_Price) As Total,count(Drug_ID) As count_Drug1,isnull(c.back,0) As back,isnull(c.count_Drug,0) As count_Drug,
                                       (sum(a.Erning) - isnull(c.Er_back,0)) as Erning,(sum(a.Total_Price) - isnull(c.back,0)) as final from [dbo].[Drug_Sales] a
                                        left join [dbo].[Customers] b on a.Customer_ID = b.Customer_ID			  
                                        left join (select State , bill_date,sum(Erning) as Er_back,sum(Total_Price) as back,count(Drug_ID) as count_Drug from [dbo].[Drug_Sales] group by bill_date,State having State ='false') c
                                        on a.bill_date = c.bill_date 
                                        group by a.bill_date,c.back,c.count_Drug,c.state,c.Er_back
                                        having a.bill_date ='" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'order by a.bill_date", "Drug_Purchases", Me)
                Case 1
                    Myconn.Filldataset("Select count(DISTINCT(a.Bill_ID)) as col_num,a.bill_date,sum(a.Total_Price) As Total,count(Drug_ID) As count_Drug1,isnull(c.back,0) As back,isnull(c.count_Drug,0) As count_Drug,
                                        (sum(a.Erning) - isnull(c.Er_back,0)) as Erning,(sum(a.Total_Price) - isnull(c.back,0)) as final from [dbo].[Drug_Sales] a
                                        left join [dbo].[Customers] b on a.Customer_ID = b.Customer_ID			  
                                        left join (select State , bill_date,sum(Erning) as Er_back,sum(Total_Price) as back,count(Drug_ID) as count_Drug from [dbo].[Drug_Sales] group by bill_date,State having State ='false') c
                                        on a.bill_date = c.bill_date 
                                        group by a.bill_date,c.back,c.count_Drug,c.state,c.Er_back
                                        having a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'order by a.bill_date", "Drug_Purchases", Me)

                Case 2
                    Myconn.Filldataset("Select count(DISTINCT(a.Bill_ID)) as col_num,a.bill_date,sum(a.Total_Price) As Total,count(Drug_ID) As count_Drug1,isnull(c.back,0) As back,isnull(c.count_Drug,0) As count_Drug,
                                        (sum(a.Erning) - isnull(c.Er_back,0)) as Erning,(sum(a.Total_Price) - isnull(c.back,0)) as final from [dbo].[Drug_Sales] a
                                        left join [dbo].[Customers] b on a.Customer_ID = b.Customer_ID			  
                                        left join (select State ,Customer_ID, bill_date,sum(Erning) as Er_back,sum(Total_Price) as back,count(Drug_ID) as count_Drug from [dbo].[Drug_Sales] group by bill_date,Customer_ID,State having State ='false') c
                                        on a.bill_date = c.bill_date and a.Customer_ID = c.Customer_ID
                                        group by a.bill_date,c.back,c.count_Drug,c.state,a.Customer_ID,c.Er_back
                                        having a.Customer_ID = " & CInt(cbo_Co.ComboBox.SelectedValue) & "order by a.bill_date", "Drug_Purchases", Me)
                Case 3
                    Myconn.Filldataset("Select count(DISTINCT(a.Bill_ID)) as col_num,a.bill_date,sum(a.Total_Price) As Total,count(Drug_ID) As count_Drug1,isnull(c.back,0) As back,isnull(c.count_Drug,0) As count_Drug,
                                       (sum(a.Erning) - isnull(c.Er_back,0)) as Erning,(sum(a.Total_Price) - isnull(c.back,0)) as final from [dbo].[Drug_Sales] a
                                        left join [dbo].[Customers] b on a.Customer_ID = b.Customer_ID			  
                                        left join (select State ,Customer_ID, bill_date,sum(Erning) as Er_back,sum(Total_Price) as back,count(Drug_ID) as count_Drug from [dbo].[Drug_Sales] group by bill_date,Customer_ID,State having State ='false') c
                                        on a.bill_date = c.bill_date and a.Customer_ID = c.Customer_ID
                                        group by a.bill_date,c.back,c.count_Drug,c.state,a.Customer_ID,c.Er_back
                                        having a.Bill_Date ='" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'And a.Customer_ID = " & CInt(cbo_Co.ComboBox.SelectedValue) & "order by a.bill_date", "Drug_Purchases", Me)
                Case 4
                    Myconn.Filldataset("Select count(DISTINCT(a.Bill_ID)) as col_num,a.bill_date,sum(a.Total_Price) As Total,count(Drug_ID) As count_Drug1,isnull(c.back,0) As back,isnull(c.count_Drug,0) As count_Drug,
                                        (sum(a.Erning) - isnull(c.Er_back,0)) as Erning,(sum(a.Total_Price) - isnull(c.back,0)) as final from [dbo].[Drug_Sales] a
                                        left join [dbo].[Customers] b on a.Customer_ID = b.Customer_ID			  
                                        left join (select State ,Customer_ID, bill_date,sum(Erning) as Er_back,sum(Total_Price) as back,count(Drug_ID) as count_Drug from [dbo].[Drug_Sales] group by bill_date,Customer_ID,State having State ='false') c
                                        on a.bill_date = c.bill_date and a.Customer_ID = c.Customer_ID
                                        group by a.bill_date,c.back,c.count_Drug,c.state,a.Customer_ID,c.Er_back
                                        having a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'And a.Customer_ID = " & CInt(cbo_Co.ComboBox.SelectedValue) & "order by a.bill_date", "Drug_Purchases", Me)

                Case 5
                    Myconn.Filldataset("Select count(DISTINCT(a.Bill_ID)) as col_num,a.bill_date,sum(a.Total_Price) As Total,count(Drug_ID) As count_Drug1,isnull(c.back,0) As back,isnull(c.count_Drug,0) As count_Drug,
                                        (sum(a.Erning) - isnull(c.Er_back,0)) as Erning,(sum(a.Total_Price) - isnull(c.back,0)) as final from [dbo].[Drug_Sales] a
                                        left join [dbo].[Customers] b on a.Customer_ID = b.Customer_ID			  
                                        left join (select State , bill_date,sum(Erning) as Er_back,sum(Total_Price) as back,count(Drug_ID) as count_Drug from [dbo].[Drug_Sales] group by bill_date,State having State ='false') c
                                        on a.bill_date = c.bill_date 
                                        group by a.bill_date,c.back,c.count_Drug,c.state,c.Er_back order by a.bill_date", "Drug_Purchases", Me)

            End Select


            drg.Rows.Clear()
            If Myconn.cur.Count = 0 Then MsgBox("لا توجد نتائج")
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("bill_date")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("col_num")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("count_Drug1")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Total")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("count_Drug")
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("back")
                drg.Rows(i).Cells(7).Value = Myconn.cur.Current("final")
                drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Erning")

                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg(drg, 7, Label42, Label41)
            Myconn.Sum_drg(drg, 8, Label4, Label2)
        Catch ex As Exception
        MsgBox("هناك خطأ في البيانات")
        End Try
    End Sub

    Private Sub frmPharm_daily_Purchases_Load(sender As Object, e As EventArgs) Handles Me.Load
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
                txt1.Visible = False
                cbo_Co.Visible = False
                Label1.Visible = False
                txt2.Visible = False

        End Select
    End Sub
End Class