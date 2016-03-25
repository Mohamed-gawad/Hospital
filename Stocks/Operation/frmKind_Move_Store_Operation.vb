
Public Class frmKind_Move_Store_Operation
    Dim Myconn As New connect
    Dim fin As Boolean
    Dim A, UN, Amount, max, min As Double
    Dim StockID As Integer
    Dim Unit1 As String
    Sub Fillgrd()
        Try
            Myconn.Filldataset("select  * ,c.Co_Name,m.Max_Unit_Name,n.Min_Unit_Name,b.Drug_Name,s.Supplier_Name,(u.EmployeeName) as users ,(e.EmployeeName) as Employee from Stocks_Purchases a
                           left join Drugs b on a.Drug_ID = b.Drug_ID
                           left join Supplier s on a.Supplier_ID = s.Supplier_ID
                           left join Co_Drug c on b.Co_ID = c.Co_ID
                           Left Join Max_Unit M on a.Unit = M.Max_UnitID
                           Left Join Min_Unit n on a.Unit = n.Min_UnitID
                           Left Join Employees U on a.Users_ID = U.EmployeeID
                           Left Join Employees e on a.EmployeeID = e.EmployeeID
                           where a.State = 'true' and Stock_ID = " & StockID & " and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue), "Drug_Purchases", Me)
            drg.Rows.Clear()
            If Myconn.cur.Count = 0 Then
                Label1.Text = 0

            End If
            Dim AM2 As Integer

            For i As Integer = 0 To Myconn.cur.Count - 1

                If Myconn.cur.Current("Unit_Kind") = 0 Then
                    Unit1 = Myconn.cur.Current("Min_Unit_Name")
                    AM2 = Myconn.cur.Current("Amount") * Myconn.cur.Current("Min_Unit_number")
                ElseIf Myconn.cur.Current("Unit_Kind") = 1 Then
                    Unit1 = Myconn.cur.Current("Max_Unit_Name")
                    AM2 = Myconn.cur.Current("Amount") * 1
                End If
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Supplier_Name")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Bill_ID")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("bill_date")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Co_Name")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Drug_Name")
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Drug_ID")
                drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Drug_exp")
                drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Price")
                drg.Rows(i).Cells(9).Value = Math.Round(AM2)
                drg.Rows(i).Cells(10).Value = Unit1
                drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Total")
                drg.Rows(i).Cells(12).Value = Myconn.cur.Current("Employee")
                drg.Rows(i).Cells(13).Value = Myconn.cur.Current("users")
                drg.Rows(i).Cells(14).Value = Myconn.cur.Current("Amount")
                Myconn.cur.Position += 1
            Next

            Myconn.Sum_drg2(drg, 14, Label1)
            '---------------------------------------------------------------------------------------------------------------

            Myconn.Filldataset("select  * ,c.Co_Name,m.Max_Unit_Name,n.Min_Unit_Name,b.Drug_Name,s.PatientName,(u.EmployeeName) as users ,
                            v.CerviceName,d.DoctorsName,z.specialization,(e.EmployeeName) as Employee from Stocks_Sales a
                           left join Drugs b on a.Drug_ID = b.Drug_ID
                           left join Patient s on a.patient_ID = s.patient_ID
                           left join Co_Drug c on b.Co_ID = c.Co_ID
                           Left Join Max_Unit M on a.Unit = M.Max_UnitID
                           Left Join Min_Unit n on a.Unit = n.Min_UnitID
                           Left Join Employees U on a.Users_ID = U.EmployeeID
                           Left Join Employees e on a.EmployeeID = e.EmployeeID
                           left join Doctors d on a.DoctorsID = d.DoctorsID
                           left join Cervices v on a.CerviceID = v.CerviceID
                           left join specialization z on a.specializationID = z.specializationID
                           where a.State = 'true' and Stock_ID = " & StockID & " and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue), "Stocks_Sales", Me)
            drg1.Rows.Clear()
            If Myconn.cur.Count = 0 Then
                Label2.Text = 0

            End If
            AM2 = 0
            For i As Integer = 0 To Myconn.cur.Count - 1

                If Myconn.cur.Current("Unit_Kind") = 0 Then
                    Unit1 = Myconn.cur.Current("Min_Unit_Name")
                    AM2 = Myconn.cur.Current("Amount") * Myconn.cur.Current("Min_Unit_number")
                ElseIf Myconn.cur.Current("Unit_Kind") = 1 Then
                    Unit1 = Myconn.cur.Current("Max_Unit_Name")
                    AM2 = Myconn.cur.Current("Amount") * 1
                End If
                drg1.Rows.Add()
                drg1.Rows(i).Cells(0).Value = i + 1
                drg1.Rows(i).Cells(1).Value = Myconn.cur.Current("PatientName")
                drg1.Rows(i).Cells(2).Value = Myconn.cur.Current("Bill_ID")
                drg1.Rows(i).Cells(3).Value = Myconn.cur.Current("bill_date")
                drg1.Rows(i).Cells(4).Value = Myconn.cur.Current("Time_Add")
                drg1.Rows(i).Cells(5).Value = Myconn.cur.Current("Co_Name")
                drg1.Rows(i).Cells(6).Value = Myconn.cur.Current("Drug_Name")
                drg1.Rows(i).Cells(7).Value = Myconn.cur.Current("Drug_ID")
                drg1.Rows(i).Cells(8).Value = Myconn.cur.Current("Drug_exp")
                drg1.Rows(i).Cells(9).Value = Math.Round(AM2)
                drg1.Rows(i).Cells(10).Value = Unit1
                drg1.Rows(i).Cells(11).Value = Myconn.cur.Current("Drug_Price")
                drg1.Rows(i).Cells(12).Value = Myconn.cur.Current("Total_Price")
                drg1.Rows(i).Cells(13).Value = Myconn.cur.Current("specialization")
                drg1.Rows(i).Cells(14).Value = Myconn.cur.Current("DoctorsName")
                drg1.Rows(i).Cells(15).Value = Myconn.cur.Current("CerviceName")
                drg1.Rows(i).Cells(16).Value = Myconn.cur.Current("Employee")
                drg1.Rows(i).Cells(17).Value = Myconn.cur.Current("users")
                drg1.Rows(i).Cells(18).Value = Myconn.cur.Current("Amount")
                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg2(drg1, 18, Label2)
            Amount = Val(Label1.Text) - Val(Label2.Text)
            Binding()
        Catch ex As Exception
            MsgBox("هناك خطأ ما")
            Return
        End Try

    End Sub
    Sub Binding()
        Myconn.Filldataset1("select *,b.Max_Unit_Name,c.Min_Unit_Name from Drugs a 
                            left join Max_Unit b on a.Max_UnitID = b.Max_UnitID
                            left join Min_Unit c on a.Min_UnitID = c.Min_UnitID
                            where Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue), "Drugs", Me)

        Dim C As Double, B, E As Integer
        If Amount = 0 Then
            Label4.Text = 0

        ElseIf Amount <> 0

            A = Math.Round(Amount, 2)
            B = Fix(A)
            C = Math.Round((Val(A) - Val(B)), 2)
            E = Myconn.cur1.Current("Min_Unit_number")

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

    End Sub
    Private Sub frmKind_Move_Store_Operation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        StockID = 2
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

    Private Sub frmKind_Move_Store_Operation_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        StockID = 2
    End Sub
End Class