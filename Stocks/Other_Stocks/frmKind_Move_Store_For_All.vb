Imports System.ComponentModel
Public Class frmKind_Move_Store_For_All
    Dim Myconn As New connect
    Dim fin As Boolean
    Dim A, UN, Amount, max, min As Double
    Dim U_Name As String
    Dim Unit1 As String
    Sub Binding()
        Myconn.Filldataset("select *,b.Max_Unit_Name,c.Min_Unit_Name from Drugs a 
                            left join Max_Unit b on a.Max_UnitID = b.Max_UnitID
                            left join Min_Unit c on a.Min_UnitID = c.Min_UnitID
                            where Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue), "Drugs", Me)

        Dim C As Double, B, E As Integer
        If Amount = 0 Then
            U_Name = 0

        ElseIf Amount <> 0

            A = Math.Round(Amount, 2)
            B = Math.Floor(Math.Abs(A))
            C = Math.Round((Val(A) - Val(B)), 2)
            E = Myconn.cur.Current("Min_Unit_number")

            If B > 0 And C = 0 Then
                U_Name = B & " " & Myconn.cur.Current("Max_Unit_Name")
                max = B * Myconn.cur.Current("Drug_Price")
                min = 0
            ElseIf B > 0 And C > 0
                U_Name = B & " " & Myconn.cur.Current("Max_Unit_Name") & " و " & Math.Round((C * E)) & " " & Myconn.cur.Current("Min_Unit_Name")
                max = B * Myconn.cur.Current("Drug_Price")
                min = Math.Round((C * E)) * (Myconn.cur.Current("Drug_Price") / E)
            ElseIf B = 0 And C > 0
                max = 0
                min = Math.Round((C * E)) * (Myconn.cur.Current("Drug_Price") / E)
                U_Name = Math.Round((C * E)) & " " & Myconn.cur.Current("Min_Unit_Name")
            ElseIf B < 0 And C = 0
                max = B * Myconn.cur.Current("Drug_Price")
                min = 0
                U_Name = B & " " & Myconn.cur.Current("Max_Unit_Name")
            ElseIf B < 0 And C < 0
                max = B * Myconn.cur.Current("Drug_Price")
                min = Math.Round((C * E)) * (Myconn.cur.Current("Drug_Price") / E)
                U_Name = B & " " & Myconn.cur.Current("Max_Unit_Name") & " و " & Math.Round((C * E)) & " " & Myconn.cur.Current("Min_Unit_Name")
            ElseIf B = 0 And C < 0
                max = 0
                min = Math.Round((C * E)) * (Myconn.cur.Current("Drug_Price") / E)
                U_Name = Math.Round((C * E)) & " " & Myconn.cur.Current("Min_Unit_Name")
            End If
        End If


    End Sub
    Sub Fillgrd()
        Dim X, Y As Integer
        If Not fin Then Return
        If cbo_Drug.ComboBox.SelectedIndex = -1 Then Return
        Myconn.Filldataset("select b.Min_Unit_number,a.Unit_Kind,c.GroupName,o.Stock_Name,a.Bill_ID,a.Bill_Date,a.Bill_Time ,b.Drug_Name,a.Drug_ID,a.Amount,x.Co_Name,b.Drug_Price,c.GroupName,d.Max_Unit_Name,e.Min_Unit_Name,s.Supplier_Name,(u.EmployeeName) as Users,
                            (y.EmployeeName) as Employee from [dbo].[Stocks_Purchases] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join [dbo].[Drug_Groups] c on b.GroupID = c.GroupID
                            left join [dbo].[Max_Unit] d on b.Max_UnitID = d.Max_UnitID
                            left join [dbo].[Min_Unit] e on b.Min_UnitID = e.Min_UnitID
                            left join [dbo].[Supplier] s on a.Supplier_ID = s.Supplier_ID
                            left join [dbo].[Employees] u on a.Users_ID = u.EmployeeID
                            left join [dbo].[Employees] y on a.EmployeeID = y.EmployeeID
                            left join [dbo].[Co_Drug] x on b.Co_ID = x.Co_ID
                            left join [dbo].[Stocks] o on a.Stock_ID = o.Stock_ID
                            where a.State = 'True' and a.Drug_ID = " & CInt(cbo_Drug.ComboBox.SelectedValue) & "order by a.Bill_Date", "Stocks_Purchases", Me)

        drg_Pur.Rows.Clear()
        For i As Integer = 0 To Myconn.cur.Count - 1
            Dim AM2 As Integer

            If Myconn.cur.Current("Unit_Kind") = 0 Then
                Unit1 = Myconn.cur.Current("Min_Unit_Name")
                AM2 = Myconn.cur.Current("Amount") * Myconn.cur.Current("Min_Unit_number")
            ElseIf Myconn.cur.Current("Unit_Kind") = 1 Then
                Unit1 = Myconn.cur.Current("Max_Unit_Name")
                AM2 = Myconn.cur.Current("Amount") * 1
            End If
            drg_Pur.Rows.Add()
            drg_Pur.Rows(i).Cells(0).Value = i + 1
            drg_Pur.Rows(i).Cells(1).Value = Myconn.cur.Current("Stock_Name")
            drg_Pur.Rows(i).Cells(2).Value = Myconn.cur.Current("Supplier_Name")
            drg_Pur.Rows(i).Cells(3).Value = Myconn.cur.Current("Bill_ID")
            drg_Pur.Rows(i).Cells(4).Value = Myconn.cur.Current("Bill_Date")
            drg_Pur.Rows(i).Cells(5).Value = Myconn.cur.Current("Bill_Time")
            drg_Pur.Rows(i).Cells(6).Value = Myconn.cur.Current("Co_Name")
            drg_Pur.Rows(i).Cells(7).Value = Myconn.cur.Current("Drug_Name")
            drg_Pur.Rows(i).Cells(8).Value = Myconn.cur.Current("Drug_ID")
            drg_Pur.Rows(i).Cells(9).Value = Myconn.cur.Current("Drug_Price")
            drg_Pur.Rows(i).Cells(10).Value = AM2
            drg_Pur.Rows(i).Cells(11).Value = Unit1
            drg_Pur.Rows(i).Cells(12).Value = Myconn.cur.Current("GroupName")
            drg_Pur.Rows(i).Cells(13).Value = Myconn.cur.Current("Employee")
            drg_Pur.Rows(i).Cells(14).Value = Myconn.cur.Current("Users")
            drg_Pur.Rows(i).Cells(15).Value = Myconn.cur.Current("Amount")

            If drg_Pur.Rows(i).Cells(1).Value.ToString.Equals("عمليات") Then drg_Pur.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
            If drg_Pur.Rows(i).Cells(1).Value.ToString.Equals("الحضانات") Then drg_Pur.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
            If drg_Pur.Rows(i).Cells(1).Value.ToString.Equals("الإقامة") Then drg_Pur.Rows(i).DefaultCellStyle.BackColor = Color.LightSalmon
            If drg_Pur.Rows(i).Cells(1).Value.ToString.Equals("الطوارىء") Then drg_Pur.Rows(i).DefaultCellStyle.BackColor = Color.Plum
            Myconn.cur.Position += 1
        Next
        Myconn.Sum_drg2(drg_Pur, 15, Label49)
        X = Label49.Text
        Amount = Label49.Text
        Binding()
        Label49.Text = U_Name
        '--------------------------------------------------------------------------------------------------------------------------
        If cbo_Drug.ComboBox.SelectedIndex = -1 Then Return
        Myconn.Filldataset("select  b.Min_Unit_number,a.Unit_Kind,c.GroupName,p.specialization,v.CerviceName,t.DoctorsName,o.Stock_Name,a.Bill_ID,a.Bill_Date,a.Time_Add ,b.Drug_Name,a.Drug_ID,a.Amount,x.Co_Name,b.Drug_Price,c.GroupName,d.Max_Unit_Name,e.Min_Unit_Name,s.PatientName,(u.EmployeeName) as Users,
                            (y.EmployeeName) as Employee from [dbo].[Stocks_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join [dbo].[Drug_Groups] c on b.GroupID = c.GroupID
                            left join [dbo].[Max_Unit] d on b.Max_UnitID = d.Max_UnitID
                            left join [dbo].[Min_Unit] e on b.Min_UnitID = e.Min_UnitID
                            left join [dbo].[Patient] s on a.Patient_ID = s.Patient_ID
                            left join [dbo].[Employees] u on a.Users_ID = u.EmployeeID
                            left join [dbo].[Employees] y on a.EmployeeID = y.EmployeeID
                            left join [dbo].[Co_Drug] x on b.Co_ID = x.Co_ID
                            left join [dbo].[Stocks] o on a.Stock_ID = o.Stock_ID
                            left join [dbo].[Doctors] t on a.DoctorsID = t.DoctorsID
                            left join [dbo].[Cervices] v on a.CerviceID = v.CerviceID
                            left join [dbo].[specialization] P on a.specializationID = p.specializationID
                            where a.State = 'True' and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue) & "order by a.Bill_Date", "Stocks_Purchases", Me)

        drg_Sales.Rows.Clear()
        For i As Integer = 0 To Myconn.cur.Count - 1
            Dim AM2 As Integer

            If Myconn.cur.Current("Unit_Kind") = 0 Then
                Unit1 = Myconn.cur.Current("Min_Unit_Name")
                AM2 = Myconn.cur.Current("Amount") * Myconn.cur.Current("Min_Unit_number")
            ElseIf Myconn.cur.Current("Unit_Kind") = 1 Then
                Unit1 = Myconn.cur.Current("Max_Unit_Name")
                AM2 = Myconn.cur.Current("Amount") * 1
            End If
            drg_Sales.Rows.Add()
            drg_Sales.Rows(i).Cells(0).Value = i + 1
            drg_Sales.Rows(i).Cells(1).Value = Myconn.cur.Current("Stock_Name")
            drg_Sales.Rows(i).Cells(2).Value = Myconn.cur.Current("PatientName")
            drg_Sales.Rows(i).Cells(3).Value = Myconn.cur.Current("Bill_ID")
            drg_Sales.Rows(i).Cells(4).Value = Myconn.cur.Current("Bill_Date")
            drg_Sales.Rows(i).Cells(5).Value = Myconn.cur.Current("Time_Add")
            drg_Sales.Rows(i).Cells(6).Value = Myconn.cur.Current("Co_Name")
            drg_Sales.Rows(i).Cells(7).Value = Myconn.cur.Current("Drug_Name")
            drg_Sales.Rows(i).Cells(8).Value = Myconn.cur.Current("Drug_ID")
            drg_Sales.Rows(i).Cells(9).Value = Myconn.cur.Current("Drug_Price")
            drg_Sales.Rows(i).Cells(10).Value = AM2
            drg_Sales.Rows(i).Cells(11).Value = Unit1
            drg_Sales.Rows(i).Cells(12).Value = Myconn.cur.Current("GroupName")
            drg_Sales.Rows(i).Cells(13).Value = Myconn.cur.Current("specialization")
            drg_Sales.Rows(i).Cells(14).Value = Myconn.cur.Current("DoctorsName")
            drg_Sales.Rows(i).Cells(15).Value = Myconn.cur.Current("CerviceName")
            drg_Sales.Rows(i).Cells(16).Value = Myconn.cur.Current("Employee")
            drg_Sales.Rows(i).Cells(17).Value = Myconn.cur.Current("Users")
            drg_Sales.Rows(i).Cells(18).Value = Myconn.cur.Current("Amount")

            If drg_Sales.Rows(i).Cells(1).Value.ToString.Equals("عمليات") Then drg_Sales.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
            If drg_Sales.Rows(i).Cells(1).Value.ToString.Equals("الحضانات") Then drg_Sales.Rows(i).DefaultCellStyle.BackColor = Color.LightSteelBlue
            If drg_Sales.Rows(i).Cells(1).Value.ToString.Equals("الإقامة") Then drg_Sales.Rows(i).DefaultCellStyle.BackColor = Color.LightSalmon
            If drg_Sales.Rows(i).Cells(1).Value.ToString.Equals("الطوارىء") Then drg_Sales.Rows(i).DefaultCellStyle.BackColor = Color.Plum
            Myconn.cur.Position += 1
        Next
        Myconn.Sum_drg2(drg_Sales, 18, Label50)
        Y = Label50.Text
        Amount = Label50.Text
        Binding()
        Label50.Text = U_Name

        Amount = X - Y
        Binding()
        Label52.Text = U_Name
    End Sub
    Private Sub frmKind_Move_Store_For_All_Load(sender As Object, e As EventArgs) Handles Me.Load
        fin = False
        Myconn.Fillcombo("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug.ComboBox)
        fin = True
    End Sub
    Private Sub cbo_Drug_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Drug.SelectedIndexChanged
        Fillgrd()
    End Sub
    Private Sub cbo_Drug_Enter(sender As Object, e As EventArgs) Handles cbo_Drug.Enter
        Myconn.langAR()

    End Sub
End Class