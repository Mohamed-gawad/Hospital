Public Class frmPharm_Kind_Move
    Dim Myconn As New connect
    Dim fin As Boolean
    Dim Amount As Double
    Sub Fillgrd()
        Try
            Myconn.Filldataset("select  * ,c.Co_Name,b.Drug_Name,s.Supplier_Name from Drug_Purchases a
                           left join Drugs b on a.Drug_ID = b.Drug_ID
                           left join Supplier s on a.Supplier_ID = s.Supplier_ID
                           left join Co_Drug c on b.Co_ID = c.Co_ID
                           where a.State = 'true' and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue), "Drug_Purchases", Me)
            drg.Rows.Clear()
            If Myconn.cur.Count = 0 Then
                Label1.Text = 0

            End If
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Supplier_Name")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Bill_number")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("bill_date")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Co_Name")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Drug_Name")
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Drug_ID")
                drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Drug_exp")
                drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Public_Price")
                drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Drug_Amount")
                drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Drug_Bonus")
                drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Sales_tax")
                drg.Rows(i).Cells(12).Value = (Myconn.cur.Current("Drug_Amount") + Myconn.cur.Current("Drug_Bonus")) * Myconn.cur.Current("Sales_tax")
                drg.Rows(i).Cells(13).Value = Myconn.cur.Current("Pharmacist_discount")
                drg.Rows(i).Cells(14).Value = Myconn.cur.Current("Pharmacist_Price")
                drg.Rows(i).Cells(15).Value = drg.Rows(i).Cells(14).Value + drg.Rows(i).Cells(11).Value
                drg.Rows(i).Cells(16).Value = drg.Rows(i).Cells(14).Value * Myconn.cur.Current("Drug_Amount")
                drg.Rows(i).Cells(17).Value = drg.Rows(i).Cells(16).Value + drg.Rows(i).Cells(12).Value
                drg.Rows(i).Cells(18).Value = (Myconn.cur.Current("Drug_Amount") + Myconn.cur.Current("Drug_Bonus")) * Myconn.cur.Current("Public_Price")
                drg.Rows(i).Cells(19).Value = Myconn.cur.Current("Earnings")
                drg.Rows(i).Cells(20).Value = Math.Round((((Myconn.cur.Current("Public_Price") - drg.Rows(i).Cells(15).Value) / Myconn.cur.Current("Public_Price")) * 100), 2)
                drg.Rows(i).Cells(21).Value = Math.Round((((Myconn.cur.Current("Public_Price") - drg.Rows(i).Cells(15).Value) / drg.Rows(i).Cells(15).Value) * 100), 2)
                drg.Rows(i).Cells(22).Value = Myconn.cur.Current("Drug_Amount") + Myconn.cur.Current("Drug_Bonus")
                Myconn.cur.Position += 1
            Next

            Myconn.Sum_drg2(drg, 22, Label1)
            ' ------------------------------------------------------------------------------------------------


            Myconn.Filldataset("select a.Discount,m.Max_Unit_Name,n.Min_Unit_Name,a.Time_Add, c.Co_Name, b.Drug_Name,a.Drug_ID,a.Drug_exp,
                                    a.Bill_ID,a.Bill_Date,a.Amount_max,a.Amount_min,a.unit,a.Unit_Kind,a.Drug_Price,a.Pharm_discound,a.Total_Price,a.Erning,(d.EmployeeName) As Users,
                                    (e.EmployeeName) As Employee,a.ID,a.state,R.Customer_Name,b.Min_Unit_number,a.buyer from Drug_Sales a
                           Left Join Drugs b on a.Drug_ID = b.Drug_ID
                           Left Join Employees d on a.Users_ID = d.EmployeeID
                           Left Join Employees e on a.EmployeeID = e.EmployeeID
                           Left Join Max_Unit M on a.Unit = M.Max_UnitID
                           Left Join Min_Unit n on a.Unit = n.Min_UnitID
                           Left Join Customers R on a.Customer_ID = R.Customer_ID
                           Left Join Co_Drug c On b.Co_ID = c.Co_ID where a.State = 'True' and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue), "Drug_Sales", Me)
            drg1.Rows.Clear()
            If Myconn.cur.Count = 0 Then
                Label2.Text = 0

            End If

            For i As Integer = 0 To Myconn.cur.Count - 1
                drg1.Rows.Add()
                drg1.Rows(i).Cells(0).Value = i + 1
                drg1.Rows(i).Cells(1).Value = Myconn.cur.Current("Customer_Name")
                drg1.Rows(i).Cells(2).Value = Myconn.cur.Current("Bill_ID")
                drg1.Rows(i).Cells(3).Value = Myconn.cur.Current("Bill_Date")
                drg1.Rows(i).Cells(4).Value = Myconn.cur.Current("Time_Add")
                drg1.Rows(i).Cells(5).Value = Myconn.cur.Current("Co_Name")
                drg1.Rows(i).Cells(6).Value = Myconn.cur.Current("Drug_Name")
                drg1.Rows(i).Cells(7).Value = Myconn.cur.Current("Drug_ID")
                drg1.Rows(i).Cells(8).Value = Myconn.cur.Current("Drug_exp")
                drg1.Rows(i).Cells(9).Value = If(Myconn.cur.Current("Unit_Kind") = 0, Myconn.cur.Current("Amount_min"), Myconn.cur.Current("Amount_max"))
                drg1.Rows(i).Cells(10).Value = If(Myconn.cur.Current("Unit_Kind") = 0, Myconn.cur.Current("Min_Unit_Name"), Myconn.cur.Current("Max_Unit_Name"))
                drg1.Rows(i).Cells(11).Value = Myconn.cur.Current("Drug_Price")
                drg1.Rows(i).Cells(12).Value = Myconn.cur.Current("Discount")
                drg1.Rows(i).Cells(13).Value = Myconn.cur.Current("Pharm_discound")
                drg1.Rows(i).Cells(14).Value = Myconn.cur.Current("Total_Price")
                drg1.Rows(i).Cells(15).Value = Myconn.cur.Current("Erning")
                drg1.Rows(i).Cells(16).Value = Myconn.cur.Current("Employee")
                drg1.Rows(i).Cells(17).Value = Myconn.cur.Current("Customer_Name")
                drg1.Rows(i).Cells(18).Value = Myconn.cur.Current("Users")
                drg1.Rows(i).Cells(19).Value = Myconn.cur.Current("Buyer")
                drg1.Rows(i).Cells(20).Value = If(Myconn.cur.Current("Unit_Kind") = 0, Myconn.cur.Current("Amount_min"), Myconn.cur.Current("Amount_max") * Myconn.cur.Current("Min_Unit_number"))
                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg2(drg1, 20, Label2)
            Amount = Val(Val(Label1.Text) * Val(Myconn.cur.Current("Min_Unit_number"))) - Val(Label2.Text)
            Binding()
        Catch ex As Exception
            MsgBox("هناك خطأ ما")
            Return
        End Try
    End Sub
    Sub Binding()
        Myconn.Filldataset("select *,b.Max_Unit_Name,c.Min_Unit_Name from Drugs a 
                            left join Max_Unit b on a.Max_UnitID = b.Max_UnitID
                            left join Min_Unit c on a.Min_UnitID = c.Min_UnitID
                            where Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue), "Drugs", Me)
        Label4.Text = If(Amount Mod Val(Myconn.cur.Current("Min_Unit_number")) = 0, Math.Truncate(Amount / Val(Myconn.cur.Current("Min_Unit_number"))) & Space(1) & Myconn.cur.Current("Max_Unit_Name"), Math.Truncate(Amount / Val(Myconn.cur.Current("Min_Unit_number"))) & Space(1) & Myconn.cur.Current("Max_Unit_Name") & " و " & Amount Mod Val(Myconn.cur.Current("Min_Unit_number")) & Space(1) & Myconn.cur.Current("Min_Unit_Name"))
    End Sub
    Private Sub frmPharm_Kind_Move_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        fin = False
        Myconn.Fillcombo("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug.ComboBox)
        fin = True
    End Sub
    Private Sub cbo_Drug_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Drug.SelectedIndexChanged
        If Not fin Then Return
        Fillgrd()
    End Sub
    Private Sub cbo_Drug_Enter(sender As Object, e As EventArgs) Handles cbo_Drug.Enter
        Myconn.langAR()
    End Sub
End Class