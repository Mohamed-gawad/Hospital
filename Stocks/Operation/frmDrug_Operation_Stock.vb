Public Class frmDrug_Operation_Stock
    Dim Myconn As New connect
    Dim fin As Boolean
    Dim Label4 As New Label
    Dim V As Decimal
    Dim A, Amount, max, min As Double
    Dim StockID As Integer

    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Try
            drg.Rows.Clear()
            Myconn.Filldataset("Select c.Min_Unit_number,d.Max_Unit_Name,e.Min_Unit_Name,c.Drug_Name,a.Drug_ID,isnull(sum(a.Amount),0) As Amount_Pur ,c.Drug_Price ,isnull(b.Amount_sales,0) as Amount_sales,  
                                (isnull(sum(a.Amount),0) - isnull(b.Amount_sales,0)) as rest , ((isnull(sum(a.Amount),0) - isnull(b.Amount_sales,0)) * c.Drug_Price) as cost,
                                g.GroupName from [dbo].[Stocks_Purchases] a 
                                 left join (select sum(Amount) as Amount_sales,Drug_ID from [dbo].[Stocks_Sales] group by Drug_ID,State,stock_ID having State = 'True' and Stock_ID = " & StockID & ") b
                                on a.Drug_ID = b.Drug_ID
                                left join [dbo].[Drugs] c
                                on a.Drug_ID = c.Drug_ID
                                left join [dbo].[Drug_Groups] g
                                on c.GroupID = g.GroupID
                                left join Max_Unit d on c.Max_UnitID = d.Max_UnitID
                                left join Min_Unit e on c.Min_UnitID = e.Min_UnitID
                                group by a.Drug_ID,c.Drug_Price ,b.Amount_sales ,c.Drug_Name,a.State,a.Stock_ID,g.GroupName, d.Max_Unit_Name,e.Min_Unit_Name,c.Min_Unit_number
                                having a.State = 'True' and a.Stock_ID = " & StockID & "", "Stocks_Purchases", Me)

            If Myconn.cur.Count = 0 Then MsgBox("لا توجد نتائج")
            V = 0
            For i As Integer = 0 To Myconn.cur.Count - 1
                Amount = Myconn.cur.Current("rest")
                Dim C As Double, B, E As Integer
                If Amount = 0 Then
                    Label4.Text = 0

                ElseIf Amount <> 0

                    A = Math.Round(Amount, 2)
                    B = Math.Floor(Math.Abs(A))
                    C = Math.Round((Val(A) - Val(B)), 2)
                    E = Myconn.cur.Current("Min_Unit_number")

                    If B > 0 And C = 0 Then
                        Label4.Text = B & " " & Myconn.cur.Current("Max_Unit_Name")
                        max = B * Myconn.cur.Current("Drug_Price")
                        min = 0
                    ElseIf B > 0 And C > 0
                        Label4.Text = B & " " & Myconn.cur.Current("Max_Unit_Name") & " و " & Math.Round((C * E)) & " " & Myconn.cur.Current("Min_Unit_Name")
                        max = B * Myconn.cur.Current("Drug_Price")
                        min = Math.Round((C * E)) * (Myconn.cur.Current("Drug_Price") / E)
                    ElseIf B = 0 And C > 0
                        max = 0
                        min = Math.Round((C * E)) * (Myconn.cur.Current("Drug_Price") / E)
                        Label4.Text = Math.Round((C * E)) & " " & Myconn.cur.Current("Min_Unit_Name")
                    ElseIf B < 0 And C = 0
                        max = B * Myconn.cur.Current("Drug_Price")
                        min = 0
                        Label4.Text = B & " " & Myconn.cur.Current("Max_Unit_Name")
                    ElseIf B < 0 And C < 0
                        max = B * Myconn.cur.Current("Drug_Price")
                        min = Math.Round((C * E)) * (Myconn.cur.Current("Drug_Price") / E)
                        Label4.Text = B & " " & Myconn.cur.Current("Max_Unit_Name") & " و " & Math.Round((C * E)) & " " & Myconn.cur.Current("Min_Unit_Name")
                    ElseIf B = 0 And C < 0
                        max = 0
                        min = Math.Round((C * E)) * (Myconn.cur.Current("Drug_Price") / E)
                        Label4.Text = Math.Round((C * E)) & " " & Myconn.cur.Current("Min_Unit_Name")
                    End If
                End If
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Drug_Name")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Drug_ID")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Drug_Price")
                drg.Rows(i).Cells(4).Value = Label4.Text
                drg.Rows(i).Cells(5).Value = max + min
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("GroupName")

                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg(drg, 5, Label39, Label40)
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
        End Try
    End Sub
    Private Sub frmDrug_Operation_Stock_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label5.Left = 0
        Label5.Width = Me.Width
        StockID = 2
        fin = False
        Myconn.Fillcombo("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug.ComboBox)
        fin = True
        Fillgrd()

    End Sub

    Private Sub cbo_Drug_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Drug.SelectedIndexChanged
        If Not fin Then Return
        drg.ClearSelection()
        For W As Integer = 0 To drg.Rows.Count - 1

            If drg.Rows(W).Cells(1).Value.ToString.Equals(cbo_Drug.Text, StringComparison.CurrentCultureIgnoreCase) Then
                drg.Rows(W).Cells(2).Selected = True
                drg.CurrentCell = drg.SelectedCells(1)
                Exit For
            End If
        Next

        If cbo_Drug.Text = "" Then
            drg.Rows(0).Cells(1).Selected = True
            drg.CurrentCell = drg.SelectedCells(1)
        End If
        If cbo_Drug.Text = "" Then Return

    End Sub

    Private Sub frmDrug_Operation_Stock_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        StockID = 2
    End Sub
End Class