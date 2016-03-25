Public Class frmPurchases_monthly
    Dim Myconn As New connect
    Dim st As String
    Sub Fillgrd()
        Try
            Select Case cboSearch.SelectedIndex
                Case 0 ' حسب الفئة
                    If cbo_Group.SelectedIndex = -1 Then Return
                    Myconn.Filldataset("select sum(a.Public_Price * (a.Drug_Amount + a.Drug_Bonus)) as Public_price,sum(a.Total_Price_tax)  as Total,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month
                            from [dbo].[Drug_Purchases] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join  Drug_Groups c on b.GroupID = c.GroupID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,b.GroupID,c.GroupName
                            having  state = 'true'and b.GroupID = " & CInt(cbo_Group.ComboBox.SelectedValue) & "order by Pur_Month", "Drug_Purchases", Me)
                Case 1 ' حسب الشركة
                    Myconn.Filldataset("select sum(a.Public_Price * (a.Drug_Amount + a.Drug_Bonus)) as Public_price,sum(a.Total_Price_tax)  as Total,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month
                            from [dbo].[Drug_Purchases] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join  Supplier c on a.Supplier_ID = c.Supplier_ID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,a.Supplier_ID
                            having  state = 'true'and a.Supplier_ID = " & CInt(cbo_Supplier.ComboBox.SelectedValue) & "order by Pur_Month", "Drug_Purchases", Me)
                Case 2 ' حسب الصنف

                    Myconn.Filldataset("select sum(a.Public_Price * (a.Drug_Amount + a.Drug_Bonus)) as Public_price,sum(a.Total_Price_tax)  as Total,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month
                            from [dbo].[Drug_Purchases] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,a.Drug_ID
                            having  state = 'true'and a.Drug_ID = " & CInt(cbo_Drug.ComboBox.SelectedValue) & "order by Pur_Month", "Drug_Purchases", Me)
                Case 3 ' جميع المشتريات
                    Myconn.Filldataset("select sum(a.Public_Price * (a.Drug_Amount + a.Drug_Bonus)) as Public_price,sum(a.Total_Price_tax)  as Total,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month
                            from [dbo].[Drug_Purchases] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join  Drug_Groups c on b.GroupID = c.GroupID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state
                            having  state = 'true' order by Pur_Month", "Drug_Purchases", Me)
            End Select

            drg.Rows.Clear()
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Pur_Month")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Total")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Public_price")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Public_price") - Myconn.cur.Current("Total")
                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg(drg, 2, Label42, Label41)
            Myconn.Sum_drg(drg, 3, Label5, Label4)
            Myconn.Sum_drg(drg, 4, Label7, Label6)
        Catch ex As Exception
            MsgBox("هناك خطأ ما")
            Return
        End Try
    End Sub
    Private Sub frmPurchases_monthly_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        Myconn.Fillcombo("select * from Drug_Groups order by GroupName", "Drug_Groups", "GroupID", "GroupName", Me, cbo_Group.ComboBox)
        Myconn.Fillcombo("select * from Supplier order by Supplier_Name", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Supplier.ComboBox)
        Myconn.Fillcombo("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug.ComboBox)

    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Fillgrd()
    End Sub
    Private Sub cboSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSearch.SelectedIndexChanged
        Select Case cboSearch.SelectedIndex
            Case 0
                cbo_Group.Visible = True
                cbo_Drug.Visible = False
                cbo_Supplier.Visible = False

            Case 1
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                cbo_Supplier.Visible = True
            Case 2
                cbo_Group.Visible = False
                cbo_Drug.Visible = True
                cbo_Supplier.Visible = False

            Case 3
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                cbo_Supplier.Visible = False

        End Select
    End Sub
End Class