Public Class frmKinds_Move_Store_For_All
    Dim Myconn As New connect
    Dim SQL As String
    Dim fin As Boolean
    Sub filldrg()
        drg.Rows.Clear()
        Myconn.Filldataset(SQL, "Op_Store", Me)
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Drug_Name")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Drug_ID")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Drug_Price")

            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Pur2")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Sa2")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("rest2")

            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Pur3")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Sa3")
            drg.Rows(i).Cells(9).Value = Myconn.cur.Current("rest3")

            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Pur4")
            drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Sa4")
            drg.Rows(i).Cells(12).Value = Myconn.cur.Current("rest4")

            drg.Rows(i).Cells(13).Value = Myconn.cur.Current("Pur5")
            drg.Rows(i).Cells(14).Value = Myconn.cur.Current("Sa5")
            drg.Rows(i).Cells(15).Value = Myconn.cur.Current("rest5")

            drg.Rows(i).Cells(16).Value = Myconn.cur.Current("Pur")
            drg.Rows(i).Cells(17).Value = Myconn.cur.Current("Sa")
            drg.Rows(i).Cells(18).Value = Myconn.cur.Current("rest")

            drg.Rows(i).Cells(19).Value = Myconn.cur.Current("rest") * Myconn.cur.Current("Drug_Price")
            drg.Rows(i).Cells(20).Value = Myconn.cur.Current("GroupName")


            drg.Rows(i).Cells(4).Style.BackColor = Color.LightGreen
            drg.Rows(i).Cells(5).Style.BackColor = Color.LightGreen
            drg.Rows(i).Cells(6).Style.BackColor = Color.LightGreen

            drg.Rows(i).Cells(7).Style.BackColor = Color.LightSteelBlue
            drg.Rows(i).Cells(8).Style.BackColor = Color.LightSteelBlue
            drg.Rows(i).Cells(9).Style.BackColor = Color.LightSteelBlue

            drg.Rows(i).Cells(10).Style.BackColor = Color.Plum
            drg.Rows(i).Cells(11).Style.BackColor = Color.Plum
            drg.Rows(i).Cells(12).Style.BackColor = Color.Plum

            drg.Rows(i).Cells(13).Style.BackColor = Color.LightSalmon
            drg.Rows(i).Cells(14).Style.BackColor = Color.LightSalmon
            drg.Rows(i).Cells(15).Style.BackColor = Color.LightSalmon

            Myconn.cur.Position += 1
        Next
        Myconn.Sum_drg(drg, 19, Label39, Label40)

    End Sub
    Private Sub frmKinds_Move_Store_For_All_Load(sender As Object, e As EventArgs) Handles Me.Load
        fin = False
        Myconn.Fillcombo("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug.ComboBox)
        fin = True

        SQL = "select a.Drug_Name,a.Drug_ID ,a.Drug_Price,g.GroupName,
                    isnull(P1.A1,0) as Pur2 ,isnull(S1.A1,0) as Sa2 , (isnull(P1.A1,0) - isnull(S1.A1,0) ) as rest2, 
                    isnull(P2.A1,0) as Pur3 ,isnull(S2.A1,0) as Sa3 , (isnull(P2.A1,0) - isnull(S2.A1,0) ) as rest3,
                    isnull(P3.A1,0) as Pur4 ,isnull(S3.A1,0) as Sa4 , (isnull(P3.A1,0) - isnull(S3.A1,0) ) as rest4,
                    isnull(P4.A1,0) as Pur5 ,isnull(S4.A1,0) as Sa5 , (isnull(P4.A1,0) - isnull(S4.A1,0) ) as rest5,
                    (isnull(P1.A1,0) + isnull(P2.A1,0) + isnull(P3.A1,0) + isnull(P4.A1,0)) as Pur,
                    (isnull(S1.A1,0) +isnull(S2.A1,0) + isnull(S3.A1,0) + isnull(S4.A1,0)) as Sa,
                    ((isnull(P1.A1,0) - isnull(S1.A1,0) ) + (isnull(P2.A1,0) - isnull(S2.A1,0) ) + (isnull(P3.A1,0) - isnull(S3.A1,0) ) + (isnull(P4.A1,0) - isnull(S4.A1,0) )) as rest
                     from [dbo].[Drugs] a

                    left join (select sum(Amount) as A1,Drug_ID  from [dbo].[Stocks_Purchases] group by Drug_ID,State,Stock_ID having State = 'True' and Stock_ID = 2  ) P1
                    on a.Drug_ID = P1.Drug_ID
                    left join (select sum(Amount) as A1,Drug_ID  from [dbo].[Stocks_Sales] group by Drug_ID,State,Stock_ID having State = 'True' and Stock_ID = 2  ) S1
                    on a.Drug_ID = S1.Drug_ID

                    left join (select sum(Amount) as A1,Drug_ID  from [dbo].[Stocks_Purchases] group by Drug_ID,State,Stock_ID having State = 'True' and Stock_ID = 3  ) P2
                    on a.Drug_ID = P2.Drug_ID
                    left join (select sum(Amount) as A1,Drug_ID  from [dbo].[Stocks_Sales] group by Drug_ID,State,Stock_ID having State = 'True' and Stock_ID = 3  ) S2
                    on a.Drug_ID = S2.Drug_ID

                    left join (select sum(Amount) as A1,Drug_ID  from [dbo].[Stocks_Purchases] group by Drug_ID,State,Stock_ID having State = 'True' and Stock_ID = 4  ) P3
                    on a.Drug_ID = P3.Drug_ID
                    left join (select sum(Amount) as A1,Drug_ID  from [dbo].[Stocks_Sales] group by Drug_ID,State,Stock_ID having State = 'True' and Stock_ID = 4  ) S3
                    on a.Drug_ID = S3.Drug_ID

                    left join (select sum(Amount) as A1,Drug_ID  from [dbo].[Stocks_Purchases] group by Drug_ID,State,Stock_ID having State = 'True' and Stock_ID = 5  ) P4
                    on a.Drug_ID = P4.Drug_ID
                    left join (select sum(Amount) as A1,Drug_ID  from [dbo].[Stocks_Sales] group by Drug_ID,State,Stock_ID having State = 'True' and Stock_ID = 5  ) S4
                    on a.Drug_ID = S4.Drug_ID

                    left join [dbo].[Drug_Groups] g on a.GroupID = g.GroupID

                    where  (isnull(P1.A1,0) > 0 or isnull(S1.A1,0) > 0 ) or (isnull(P2.A1,0) > 0 or isnull(S2.A1,0) > 0 )
                         or(isnull(P3.A1,0) > 0 or isnull(S3.A1,0) > 0 ) or (isnull(P4.A1,0) > 0 or isnull(S4.A1,0) > 0 )"

        filldrg()

    End Sub
    Private Sub ToolStripButton12_Click(sender As Object, e As EventArgs) Handles ToolStripButton12.Click
        drg.ClearSelection()
        For W As Integer = 0 To drg.Rows.Count - 1

            If drg.Rows(W).Cells(2).Value.ToString.Equals(ToolStripTextBox1.Text, StringComparison.CurrentCultureIgnoreCase) Then
                drg.Rows(W).Cells(2).Selected = True
                drg.CurrentCell = drg.SelectedCells(2)
                Exit For
            End If
        Next

        If ToolStripTextBox1.Text = "" Then
            drg.Rows(0).Cells(6).Selected = True
            drg.CurrentCell = drg.SelectedCells(2)
        End If
        If ToolStripTextBox1.Text = "" Then Return

    End Sub

    Private Sub cbo_Drug_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Drug.SelectedIndexChanged
        If Not fin Then Return
        drg.ClearSelection()
        For W As Integer = 0 To drg.Rows.Count - 1

            If drg.Rows(W).Cells(1).Value.ToString.Equals(cbo_Drug.Text, StringComparison.CurrentCultureIgnoreCase) Then
                drg.Rows(W).Cells(2).Selected = True
                drg.CurrentCell = drg.SelectedCells(2)
                Exit For
            End If
        Next

        If cbo_Drug.SelectedIndex = -1 Then
            drg.Rows(0).Cells(6).Selected = True
            drg.CurrentCell = drg.SelectedCells(2)
        End If
        If cbo_Drug.SelectedIndex = -1 Then Return

    End Sub

    Private Sub cbo_Drug_Enter(sender As Object, e As EventArgs) Handles cbo_Drug.Enter
        Myconn.langAR()

    End Sub



    'Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
    '    Select Case ToolStripComboBox1.ComboBox.SelectedIndex

    '        Case 0
    '            SQL = "SELECT c.DrugName,c.DrugID,c.DrugPrice," & _
    '                                     "ISNULL( sum(a.Amount),0)as Total_Add1,isnull(o.total_Add,0) as TotalDrage1, " & _
    '                                     "(ISNULL( sum(a.Amount),0)-isnull(o.total_Add,0)) as Rest1,  " & _
    '                                     "ISNULL(E.total_Add,0)as Total_Add2,isnull(F.total_Add,0) as TotalDrage2, " & _
    '           "(ISNULL(E.total_Add,0)-isnull(F.total_Add,0)) as Rest2,  " & _
    '           "ISNULL(b.total_Add,0)as Total_Add3,isnull(m.total_Add,0) as TotalDrage3, " & _
    '           "(ISNULL(b.total_Add,0)-isnull(m.total_Add,0)) as Rest3, " & _
    '           "ISNULL(g.total_Add,0)as Total_Add4,isnull(h.total_Add,0) as TotalDrage4, " & _
    '           "(ISNULL(g.total_Add,0)-isnull(h.total_Add,0)) as Rest4, " & _
    '           "(ISNULL( sum(a.Amount),0)+ISNULL(E.total_Add,0)+ISNULL(b.total_Add,0)+ISNULL(g.total_Add,0)) as Final_Total_Add, " & _
    '           "(isnull(o.total_Add,0)+isnull(F.total_Add,0)+isnull(m.total_Add,0)+isnull(h.total_Add,0)) as Final_Total_Drage, " & _
    '           "((ISNULL( sum(a.Amount),0)+ISNULL(E.total_Add,0)+ISNULL(b.total_Add,0)+ISNULL(g.total_Add,0))-(isnull(o.total_Add,0)+isnull(F.total_Add,0)+isnull(m.total_Add,0)+isnull(h.total_Add,0))) as Rest5  " & _
    '           ",c.GroupName  " & _
    '           "FROM [dbo].[Operations_Store] c  " & _
    '           "LEFT JOIN [dbo].[Operations_Store_Bills_Add] a ON c.DrugID = a.DrugID  " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Operations_Store_Bills_Drage] GROUP BY DrugID) o  " & _
    '                                                          "ON c.DrugID = o.DrugID " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Incubator_Store_Bills_Add] GROUP BY DrugID) E " & _
    '                                                          "ON c.DrugID = E.DrugID  " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Incubator_Store_Bills_Drage] GROUP BY DrugID) F  " & _
    '                                                          "ON c.DrugID = F.DrugID " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Emergency_Store_Bills_Add] GROUP BY DrugID) b " & _
    '                                                          "ON c.DrugID = b.DrugID  " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Emergency_Store_Bills_Drage] GROUP BY DrugID) m " & _
    '                                                          "ON c.DrugID = m.DrugID " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Staying_Store_Bills_Add] GROUP BY DrugID) g " & _
    '                                                          "ON c.DrugID = g.DrugID  " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Staying_Store_Bills_Drage] GROUP BY DrugID) h " & _
    '                                                          "ON c.DrugID = h.DrugID " & _
    '           "GROUP BY c.DrugID,c.DrugName,c.GroupName,c.DrugPrice,a.DrugID,o.total_Add,b.DrugID ,m.total_Add,b.total_Add,F.total_Add,E.total_Add,h.total_Add,g.total_Add order by c.DrugName"
    '        Case 1
    '            SQL = "SELECT c.DrugName,c.DrugID,c.DrugPrice," & _
    '                                      "ISNULL( sum(a.Amount),0)as Total_Add1,isnull(o.total_Add,0) as TotalDrage1, " & _
    '                                      "(ISNULL( sum(a.Amount),0)-isnull(o.total_Add,0)) as Rest1,  " & _
    '                                      "ISNULL(E.total_Add,0)as Total_Add2,isnull(F.total_Add,0) as TotalDrage2, " & _
    '            "(ISNULL(E.total_Add,0)-isnull(F.total_Add,0)) as Rest2,  " & _
    '            "ISNULL(b.total_Add,0)as Total_Add3,isnull(m.total_Add,0) as TotalDrage3, " & _
    '            "(ISNULL(b.total_Add,0)-isnull(m.total_Add,0)) as Rest3, " & _
    '            "ISNULL(g.total_Add,0)as Total_Add4,isnull(h.total_Add,0) as TotalDrage4, " & _
    '            "(ISNULL(g.total_Add,0)-isnull(h.total_Add,0)) as Rest4, " & _
    '            "(ISNULL( sum(a.Amount),0)+ISNULL(E.total_Add,0)+ISNULL(b.total_Add,0)+ISNULL(g.total_Add,0)) as Final_Total_Add, " & _
    '            "(isnull(o.total_Add,0)+isnull(F.total_Add,0)+isnull(m.total_Add,0)+isnull(h.total_Add,0)) as Final_Total_Drage, " & _
    '            "((ISNULL( sum(a.Amount),0)+ISNULL(E.total_Add,0)+ISNULL(b.total_Add,0)+ISNULL(g.total_Add,0))-(isnull(o.total_Add,0)+isnull(F.total_Add,0)+isnull(m.total_Add,0)+isnull(h.total_Add,0))) as Rest5  " & _
    '            ",c.GroupName  " & _
    '            "FROM [dbo].[Operations_Store] c  " & _
    '            "LEFT JOIN [dbo].[Operations_Store_Bills_Add] a ON c.DrugID = a.DrugID  " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Operations_Store_Bills_Drage] GROUP BY DrugID) o  " & _
    '                                                           "ON c.DrugID = o.DrugID " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Incubator_Store_Bills_Add] GROUP BY DrugID) E " & _
    '                                                           "ON c.DrugID = E.DrugID  " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Incubator_Store_Bills_Drage] GROUP BY DrugID) F  " & _
    '                                                           "ON c.DrugID = F.DrugID " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Emergency_Store_Bills_Add] GROUP BY DrugID) b " & _
    '                                                           "ON c.DrugID = b.DrugID  " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Emergency_Store_Bills_Drage] GROUP BY DrugID) m " & _
    '                                                           "ON c.DrugID = m.DrugID " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Staying_Store_Bills_Add] GROUP BY DrugID) g " & _
    '                                                           "ON c.DrugID = g.DrugID  " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Staying_Store_Bills_Drage] GROUP BY DrugID) h " & _
    '                                                           "ON c.DrugID = h.DrugID " & _
    '            "GROUP BY c.DrugID,c.DrugName,c.GroupName,c.DrugPrice,a.DrugID,o.total_Add,b.DrugID ,m.total_Add,b.total_Add,F.total_Add,E.total_Add,h.total_Add,g.total_Add order by c.DrugID"
    '        Case 2
    '            SQL = "SELECT c.DrugName,c.DrugID,c.DrugPrice," & _
    '                                       "ISNULL( sum(a.Amount),0)as Total_Add1,isnull(o.total_Add,0) as TotalDrage1, " & _
    '                                       "(ISNULL( sum(a.Amount),0)-isnull(o.total_Add,0)) as Rest1,  " & _
    '                                       "ISNULL(E.total_Add,0)as Total_Add2,isnull(F.total_Add,0) as TotalDrage2, " & _
    '            "(ISNULL(E.total_Add,0)-isnull(F.total_Add,0)) as Rest2,  " & _
    '            "ISNULL(b.total_Add,0)as Total_Add3,isnull(m.total_Add,0) as TotalDrage3, " & _
    '            "(ISNULL(b.total_Add,0)-isnull(m.total_Add,0)) as Rest3, " & _
    '            "ISNULL(g.total_Add,0)as Total_Add4,isnull(h.total_Add,0) as TotalDrage4, " & _
    '            "(ISNULL(g.total_Add,0)-isnull(h.total_Add,0)) as Rest4, " & _
    '            "(ISNULL( sum(a.Amount),0)+ISNULL(E.total_Add,0)+ISNULL(b.total_Add,0)+ISNULL(g.total_Add,0)) as Final_Total_Add, " & _
    '            "(isnull(o.total_Add,0)+isnull(F.total_Add,0)+isnull(m.total_Add,0)+isnull(h.total_Add,0)) as Final_Total_Drage, " & _
    '            "((ISNULL( sum(a.Amount),0)+ISNULL(E.total_Add,0)+ISNULL(b.total_Add,0)+ISNULL(g.total_Add,0))-(isnull(o.total_Add,0)+isnull(F.total_Add,0)+isnull(m.total_Add,0)+isnull(h.total_Add,0))) as Rest5  " & _
    '            ",c.GroupName  " & _
    '            "FROM [dbo].[Operations_Store] c  " & _
    '            "LEFT JOIN [dbo].[Operations_Store_Bills_Add] a ON c.DrugID = a.DrugID  " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Operations_Store_Bills_Drage] GROUP BY DrugID) o  " & _
    '                           "ON c.DrugID = o.DrugID " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Incubator_Store_Bills_Add] GROUP BY DrugID) E " & _
    '                           "ON c.DrugID = E.DrugID  " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Incubator_Store_Bills_Drage] GROUP BY DrugID) F  " & _
    '                           "ON c.DrugID = F.DrugID " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Emergency_Store_Bills_Add] GROUP BY DrugID) b " & _
    '                           "ON c.DrugID = b.DrugID  " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Emergency_Store_Bills_Drage] GROUP BY DrugID) m " & _
    '                           "ON c.DrugID = m.DrugID " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Staying_Store_Bills_Add] GROUP BY DrugID) g " & _
    '                           "ON c.DrugID = g.DrugID  " & _
    '            "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Staying_Store_Bills_Drage] GROUP BY DrugID) h " & _
    '                           "ON c.DrugID = h.DrugID " & _
    '            "GROUP BY c.DrugID,c.DrugName,c.GroupName,c.DrugPrice,a.DrugID,o.total_Add,b.DrugID ,m.total_Add,b.total_Add,F.total_Add,E.total_Add,h.total_Add,g.total_Add order by c.DrugPrice"
    '        Case 3
    '            SQL = "SELECT c.DrugName,c.DrugID,c.DrugPrice," & _
    '                                     "ISNULL( sum(a.Amount),0)as Total_Add1,isnull(o.total_Add,0) as TotalDrage1, " & _
    '                                     "(ISNULL( sum(a.Amount),0)-isnull(o.total_Add,0)) as Rest1,  " & _
    '                                     "ISNULL(E.total_Add,0)as Total_Add2,isnull(F.total_Add,0) as TotalDrage2, " & _
    '           "(ISNULL(E.total_Add,0)-isnull(F.total_Add,0)) as Rest2,  " & _
    '           "ISNULL(b.total_Add,0)as Total_Add3,isnull(m.total_Add,0) as TotalDrage3, " & _
    '           "(ISNULL(b.total_Add,0)-isnull(m.total_Add,0)) as Rest3, " & _
    '           "ISNULL(g.total_Add,0)as Total_Add4,isnull(h.total_Add,0) as TotalDrage4, " & _
    '           "(ISNULL(g.total_Add,0)-isnull(h.total_Add,0)) as Rest4, " & _
    '           "(ISNULL( sum(a.Amount),0)+ISNULL(E.total_Add,0)+ISNULL(b.total_Add,0)+ISNULL(g.total_Add,0)) as Final_Total_Add, " & _
    '           "(isnull(o.total_Add,0)+isnull(F.total_Add,0)+isnull(m.total_Add,0)+isnull(h.total_Add,0)) as Final_Total_Drage, " & _
    '           "((ISNULL( sum(a.Amount),0)+ISNULL(E.total_Add,0)+ISNULL(b.total_Add,0)+ISNULL(g.total_Add,0))-(isnull(o.total_Add,0)+isnull(F.total_Add,0)+isnull(m.total_Add,0)+isnull(h.total_Add,0))) as Rest5  " & _
    '           ",c.GroupName  " & _
    '           "FROM [dbo].[Operations_Store] c  " & _
    '           "LEFT JOIN [dbo].[Operations_Store_Bills_Add] a ON c.DrugID = a.DrugID  " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Operations_Store_Bills_Drage] GROUP BY DrugID) o  " & _
    '                                                          "ON c.DrugID = o.DrugID " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Incubator_Store_Bills_Add] GROUP BY DrugID) E " & _
    '                                                          "ON c.DrugID = E.DrugID  " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Incubator_Store_Bills_Drage] GROUP BY DrugID) F  " & _
    '                                                          "ON c.DrugID = F.DrugID " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Emergency_Store_Bills_Add] GROUP BY DrugID) b " & _
    '                                                          "ON c.DrugID = b.DrugID  " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Emergency_Store_Bills_Drage] GROUP BY DrugID) m " & _
    '                                                          "ON c.DrugID = m.DrugID " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Staying_Store_Bills_Add] GROUP BY DrugID) g " & _
    '                                                          "ON c.DrugID = g.DrugID  " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Staying_Store_Bills_Drage] GROUP BY DrugID) h " & _
    '                                                          "ON c.DrugID = h.DrugID " & _
    '           "GROUP BY c.DrugID,c.DrugName,c.GroupName,c.DrugPrice,a.DrugID,o.total_Add,b.DrugID ,m.total_Add,b.total_Add,F.total_Add,E.total_Add,h.total_Add,g.total_Add order by c.GroupName"
    '        Case 4
    '            SQL = "SELECT c.DrugName,c.DrugID,c.DrugPrice," & _
    '                                     "ISNULL( sum(a.Amount),0)as Total_Add1,isnull(o.total_Add,0) as TotalDrage1, " & _
    '                                     "(ISNULL( sum(a.Amount),0)-isnull(o.total_Add,0)) as Rest1,  " & _
    '                                     "ISNULL(E.total_Add,0)as Total_Add2,isnull(F.total_Add,0) as TotalDrage2, " & _
    '           "(ISNULL(E.total_Add,0)-isnull(F.total_Add,0)) as Rest2,  " & _
    '           "ISNULL(b.total_Add,0)as Total_Add3,isnull(m.total_Add,0) as TotalDrage3, " & _
    '           "(ISNULL(b.total_Add,0)-isnull(m.total_Add,0)) as Rest3, " & _
    '           "ISNULL(g.total_Add,0)as Total_Add4,isnull(h.total_Add,0) as TotalDrage4, " & _
    '           "(ISNULL(g.total_Add,0)-isnull(h.total_Add,0)) as Rest4, " & _
    '           "(ISNULL( sum(a.Amount),0)+ISNULL(E.total_Add,0)+ISNULL(b.total_Add,0)+ISNULL(g.total_Add,0)) as Final_Total_Add, " & _
    '           "(isnull(o.total_Add,0)+isnull(F.total_Add,0)+isnull(m.total_Add,0)+isnull(h.total_Add,0)) as Final_Total_Drage, " & _
    '           "((ISNULL( sum(a.Amount),0)+ISNULL(E.total_Add,0)+ISNULL(b.total_Add,0)+ISNULL(g.total_Add,0))-(isnull(o.total_Add,0)+isnull(F.total_Add,0)+isnull(m.total_Add,0)+isnull(h.total_Add,0))) as Rest5  " & _
    '           ",c.GroupName  " & _
    '           "FROM [dbo].[Operations_Store] c  " & _
    '           "LEFT JOIN [dbo].[Operations_Store_Bills_Add] a ON c.DrugID = a.DrugID  " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Operations_Store_Bills_Drage] GROUP BY DrugID) o  " & _
    '                                                          "ON c.DrugID = o.DrugID " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Incubator_Store_Bills_Add] GROUP BY DrugID) E " & _
    '                                                          "ON c.DrugID = E.DrugID  " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Incubator_Store_Bills_Drage] GROUP BY DrugID) F  " & _
    '                                                          "ON c.DrugID = F.DrugID " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Emergency_Store_Bills_Add] GROUP BY DrugID) b " & _
    '                                                          "ON c.DrugID = b.DrugID  " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Emergency_Store_Bills_Drage] GROUP BY DrugID) m " & _
    '                                                          "ON c.DrugID = m.DrugID " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Staying_Store_Bills_Add] GROUP BY DrugID) g " & _
    '                                                          "ON c.DrugID = g.DrugID  " & _
    '           "LEFT JOIN (SELECT DrugID, SUM(Amount) AS total_Add  FROM [dbo].[Staying_Store_Bills_Drage] GROUP BY DrugID) h " & _
    '                                                          "ON c.DrugID = h.DrugID " & _
    '           "GROUP BY c.DrugID,c.DrugName,c.GroupName,c.DrugPrice,a.DrugID,o.total_Add,b.DrugID ,m.total_Add,b.total_Add,F.total_Add,E.total_Add,h.total_Add,g.total_Add order by Rest5"
    '    End Select
    '    filldrg()

    'End Sub
End Class