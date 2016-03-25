Public Class frmPharm_ezn_back
    Dim Myconn As New connect
    Dim x As Integer
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()

        Select Case cboBand.SelectedIndex
            Case 0
                Myconn.Filldataset("Select * ,b.itemName,c.Supplier_Name,d.EmployeeName,e.Permission_Type from Pharm_Safe_Payment a
                           left join payment_item b on a.itemID = b.paymentID
                           left join Supplier c on a.Supplier_ID = c.Supplier_ID 
                            left join Permission_Type e on a.PermissionID = e.PermissionID 
                            left join Employees d on a.User_ID = d.EmployeeID where a.state = 'False'", "Pharm_Safe_Payment", Me)

                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add()
                    drg.Rows(i).Cells(0).Value = i + 1
                    drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Permission_Type")
                    drg.Rows(i).Cells(2).Value = Myconn.cur.Current("itemName")
                    drg.Rows(i).Cells(3).Value = Myconn.cur.Current("P_Date")
                    drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Supplier_Name")
                    drg.Rows(i).Cells(5).Value = Myconn.cur.Current("amount")
                    drg.Rows(i).Cells(6).Value = Myconn.cur.Current("amount_abc")
                    drg.Rows(i).Cells(7).Value = Myconn.cur.Current("EmployeeName")
                    drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Note")
                    drg.Rows(i).Cells(9).Value = Myconn.cur.Current("State")
                    Myconn.cur.Position += 1
                Next
            Case 1
                Myconn.Filldataset("Select * ,b.itemName,c.Customer_Name,d.EmployeeName,e.Permission_Type from Pharm_Safe_recive a
                           left join receipt_item b on a.itemID = b.itemID
                           left join Customers c on a.Customer_ID = c.Customer_ID 
                            left join Permission_Type e on a.PermissionID = e.PermissionID 
                            left join Employees d on a.User_ID = d.EmployeeID where a.state = 'False'", "Pharm_Safe_recive", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add()
                    drg.Rows(i).Cells(0).Value = i + 1
                    drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Permission_Type")
                    drg.Rows(i).Cells(2).Value = Myconn.cur.Current("itemName")
                    drg.Rows(i).Cells(3).Value = Myconn.cur.Current("P_Date")
                    drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Customer_Name")
                    drg.Rows(i).Cells(5).Value = Myconn.cur.Current("amount")
                    drg.Rows(i).Cells(6).Value = Myconn.cur.Current("amount_abc")
                    drg.Rows(i).Cells(7).Value = Myconn.cur.Current("EmployeeName")
                    drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Note")
                    drg.Rows(i).Cells(9).Value = Myconn.cur.Current("State")
                    Myconn.cur.Position += 1
                Next

            Case 2
                Myconn.Filldataset("Select a.PermissionID,a.amount,a.amount_abc,a.Note,a.P_Date,a.P_ID,a.User_ID,a.State ,b.itemName,c.Customer_Name,d.EmployeeName,e.Permission_Type from Pharm_Safe_recive a
                           left join receipt_item b on a.itemID = b.itemID
                           left join Customers c on a.Customer_ID = c.Customer_ID 
                            left join Permission_Type e on a.PermissionID = e.PermissionID 
                            left join Employees d on a.User_ID = d.EmployeeID where a.state = 'False'
                            union all 
                            Select f.PermissionID,f.amount,f.amount_abc,f.Note,f.P_Date,f.P_ID,f.User_ID,f.State ,g.itemName,h.Supplier_Name,k.EmployeeName,j.Permission_Type from Pharm_Safe_Payment f
                           left join payment_item g on f.itemID = g.paymentID
                           left join Supplier h on f.Supplier_ID = h.Supplier_ID 
                            left join Permission_Type j on f.PermissionID = j.PermissionID 
                            left join Employees k on f.User_ID = k.EmployeeID where f.state = 'False' ", "Pharm_Safe_recive", Me)


                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add()
                    drg.Rows(i).Cells(0).Value = i + 1
                    drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Permission_Type")
                    drg.Rows(i).Cells(2).Value = Myconn.cur.Current("itemName")
                    drg.Rows(i).Cells(3).Value = Myconn.cur.Current("P_Date")
                    drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Customer_Name")
                    drg.Rows(i).Cells(5).Value = Myconn.cur.Current("amount")
                    drg.Rows(i).Cells(6).Value = Myconn.cur.Current("amount_abc")
                    drg.Rows(i).Cells(7).Value = Myconn.cur.Current("EmployeeName")
                    drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Note")
                    drg.Rows(i).Cells(9).Value = Myconn.cur.Current("State")
                    If Myconn.cur.Current("PermissionID") = 1 Then
                        drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                    Else
                        drg.Rows(i).DefaultCellStyle.BackColor = Color.Pink
                    End If
                    Myconn.cur.Position += 1
                Next

        End Select
    End Sub
    Private Sub frmPharm_ezn_back_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label5.Left = 0
        Label5.Width = Me.Width
        Me.KeyPreview = True
    End Sub
    Private Sub cboBand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBand.SelectedIndexChanged
        Fillgrd()
    End Sub
End Class