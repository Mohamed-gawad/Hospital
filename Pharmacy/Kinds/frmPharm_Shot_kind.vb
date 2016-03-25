Public Class frmPharm_Shot_kind
    Dim Myconn As New connect
    Dim fin As Boolean
    Dim x As Integer

    Sub Fillgrd()
        Try
            Select Case x
                Case 0
                    Myconn.Filldataset("select a.Shortage,g.GroupName,d.Max_Unit_Name,e.Min_Unit_Name,a.Min_Unit_number,a.Drug_Name,a.Drug_ID,a.Drug_Price,isnull(b.Pucr_amount,0) as Pucr_amount,isnull((isnull(c.Amount_max,0) * a.Min_Unit_number + isnull(c.Amount_min,0)),0) as sales_amount,
                           ((isnull(b.Pucr_amount,0) * a.Min_Unit_number) - isnull((isnull(c.Amount_max,0) * a.Min_Unit_number + isnull(c.Amount_min,0)),0)) as rest from [dbo].[Drugs] a
                            left join (select Drug_ID,sum(Drug_Amount + Drug_Bonus) as Pucr_amount from [dbo].[Drug_Purchases] group by Drug_ID) b
                            on a.Drug_ID = b.Drug_ID
                            left join Drug_Groups g on a.GroupID = g.GroupID
                            left join Max_Unit d on a.Max_UnitID = d.Max_UnitID
                            left join Min_Unit e on a.Min_UnitID = e.Min_UnitID
                            left join (select Drug_ID,sum(Amount_max) as Amount_max,sum(Amount_min) as Amount_min from [dbo].[Drug_Sales] group by Drug_ID) c
                            on a.Drug_ID=c.Drug_ID where a.Shortage >= ((isnull(b.Pucr_amount,0) * a.Min_Unit_number) - isnull((isnull(c.Amount_max,0) * a.Min_Unit_number + isnull(c.Amount_min,0)),0)) ", "Drug_Purchases", Me)
                Case 1
                    Myconn.Filldataset("select a.Shortage,g.GroupName,d.Max_Unit_Name,e.Min_Unit_Name,a.Min_Unit_number,a.Drug_Name,a.Drug_ID,a.Drug_Price,isnull(b.Pucr_amount,0) as Pucr_amount,isnull((isnull(c.Amount_max,0) * a.Min_Unit_number + isnull(c.Amount_min,0)),0) as sales_amount,
                            ((isnull(b.Pucr_amount,0) * a.Min_Unit_number) - isnull((isnull(c.Amount_max,0) * a.Min_Unit_number + isnull(c.Amount_min,0)),0)) as rest from [dbo].[Drugs] a
                            left join (select Drug_ID,sum(Drug_Amount + Drug_Bonus) as Pucr_amount from [dbo].[Drug_Purchases] group by Drug_ID) b
                            on a.Drug_ID = b.Drug_ID
                            left join Drug_Groups g on a.GroupID = g.GroupID
                            left join Max_Unit d on a.Max_UnitID = d.Max_UnitID
                            left join Min_Unit e on a.Min_UnitID = e.Min_UnitID
                            left join (select Drug_ID,sum(Amount_max) as Amount_max,sum(Amount_min) as Amount_min from [dbo].[Drug_Sales] group by Drug_ID) c
                            on a.Drug_ID=c.Drug_ID  where a.Shortage >= ((isnull(b.Pucr_amount,0) * a.Min_Unit_number) - isnull((isnull(c.Amount_max,0) * a.Min_Unit_number + isnull(c.Amount_min,0)),0)) and a.GroupID =" & CInt(cbo_Group.ComboBox.SelectedValue), "Drug_Purchases", Me)
            End Select
            drg.Rows.Clear()
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Drug_Name")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Drug_ID")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Drug_Price")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Shortage")
                drg.Rows(i).Cells(5).Value = If(Myconn.cur.Current("rest") Mod Myconn.cur.Current("Min_Unit_number") = 0, Math.Truncate(Myconn.cur.Current("rest") / Myconn.cur.Current("Min_Unit_number")) & Space(1) & Myconn.cur.Current("Max_Unit_Name"), Math.Truncate(Myconn.cur.Current("rest") / Myconn.cur.Current("Min_Unit_number")) & Space(1) & Myconn.cur.Current("Max_Unit_Name") & " و " & Myconn.cur.Current("rest") Mod Myconn.cur.Current("Min_Unit_number") & Space(1) & Myconn.cur.Current("Min_Unit_Name"))
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("GroupName")
                Myconn.cur.Position += 1
            Next

        Catch ex As Exception
            MsgBox("هناك خطأ ما")
            Return
        End Try
    End Sub
    Private Sub frmPharm_Shot_kind_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        fin = False
        Myconn.Fillcombo("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug.ComboBox)
        Myconn.Fillcombo("select * from Drug_Groups order by GroupName", "Drug_Groups", "GroupID", "GroupName", Me, cbo_Group.ComboBox)
        fin = True
        x = 0
        Fillgrd()
    End Sub
    Private Sub cbo_Drug_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Drug.SelectedIndexChanged
        If Not fin Then Return
        drg.ClearSelection()
        For W As Integer = 0 To drg.Rows.Count - 1

            If drg.Rows(W).Cells(1).Value.ToString.Equals(cbo_Drug.Text, StringComparison.CurrentCultureIgnoreCase) Then
                drg.Rows(W).Cells(1).Selected = True
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
    Private Sub cbo_Group_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Group.SelectedIndexChanged
        If Not fin Then Return
        x = 1
        If cbo_Group.SelectedIndex = -1 Then
            x = 0
        End If
        Fillgrd()
    End Sub
    Private Sub cbo_Group_KeyUp(sender As Object, e As KeyEventArgs) Handles cbo_Group.KeyUp
        If e.KeyCode = Keys.Enter Then
            If cbo_Group.SelectedIndex = -1 Then
                x = 0
            End If
            Fillgrd()
        End If
    End Sub
    Private Sub drg_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellDoubleClick
        frmAdd_Drug.MdiParent = Main
        frmAdd_Drug.Show()
        frmAdd_Drug.txtSearch.Text = drg.CurrentRow.Cells(2).Value
        'frmPharm_Sales.Fillgrd()
        frmAdd_Drug.btnSearch_Click(Nothing, Nothing)
    End Sub
End Class