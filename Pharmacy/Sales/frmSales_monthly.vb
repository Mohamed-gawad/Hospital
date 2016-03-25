Public Class frmSales_monthly
    Dim Myconn As New connect
    Sub Fillgrd()
        Try
            Select Case cboSearch.SelectedIndex
                Case 0 ' الفئة
                    If cbo_Group.SelectedIndex = -1 Then Return
                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join  Drug_Groups c on b.GroupID = c.GroupID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,b.GroupID
                            having  state = 'true' and b.GroupID =" & CInt(cbo_Group.ComboBox.SelectedValue) & "order by Pur_Month", "Drug_Sales", Me)
                Case 1 ' حسب العميل
                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                             left join [dbo].[Customers]  c on a.Customer_ID = c.Customer_ID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,a.Customer_ID
                            having  state = 'true' and a.Customer_ID =" & CInt(cbo_Customer.ComboBox.SelectedValue) & "order by Pur_Month", "Drug_Sales", Me)
                Case 2 ' حسب الصنف

                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,a.Drug_ID
                            having  state = 'true' and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue) & "order by Pur_Month", "Drug_Sales", Me)
                Case 3 '  جميع المبيعات
                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join  Drug_Groups c on b.GroupID = c.GroupID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state
                            having  state = 'true' order by Pur_Month", "Drug_Sales", Me)
            End Select

            drg.Rows.Clear()
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Pur_Month")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Total_Price") - Myconn.cur.Current("erning")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Total_Price")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("erning")
            Myconn.cur.Position += 1
        Next
        Myconn.Sum_drg(drg, 3, Label42, Label41)
            Myconn.Sum_drg(drg, 2, Label5, Label4)
            Myconn.Sum_drg(drg, 4, Label7, Label6)
        Catch ex As Exception
        MsgBox("هناك خطأ ما")
        Return
        End Try
    End Sub
    Private Sub frmSales_monthly_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        Myconn.Fillcombo("select * from Drug_Groups order by GroupName", "Drug_Groups", "GroupID", "GroupName", Me, cbo_Group.ComboBox)
        Myconn.Fillcombo("select * from Customers order by Customer_Name", "Customers", "Customer_ID", "Customer_Name", Me, cbo_Customer.ComboBox)
        Myconn.Fillcombo("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug.ComboBox)

    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Fillgrd()
    End Sub
    Private Sub cboSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSearch.SelectedIndexChanged
        Select Case cboSearch.SelectedIndex
            Case 0 '  حسب الفئة
                cbo_Group.Visible = True
                cbo_Customer.Visible = False
                cbo_Drug.Visible = False
            Case 1 ' حسب العميل
                cbo_Group.Visible = False
                cbo_Customer.Visible = True
                cbo_Drug.Visible = False

            Case 2 ' حسب الصنف
                cbo_Group.Visible = False
                cbo_Customer.Visible = False
                cbo_Drug.Visible = True
            Case 3 ' جيع المبيعات
                cbo_Group.Visible = False
                cbo_Customer.Visible = False
                cbo_Drug.Visible = False

        End Select
    End Sub
End Class