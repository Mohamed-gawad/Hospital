Public Class frmEsn_back
    Dim Myconn As New connect
    Dim x As Integer
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Select Case cboBand.SelectedIndex
            Case 0
                Myconn.Filldataset("select a.State,a.ID,a.payment_ID,a.payment_num,a.Payment_date,a.payment_time,a.Amount ,a.Amount_ab,a.Notes,
                            P.Permission_Type,(e.EmployeeName) as Employee,s.specialization,i.itemName,r.RecipientName,(u.EmployeeName) as Users from  [dbo].[Payment] a
                            left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                            left join [dbo].[specialization] s on a.specializationID = s.specializationID
                            left join [dbo].[payment_item] i on a.paymentID = i.paymentID
                            left join [dbo].[Employees] u on a.Users_ID = u.EmployeeID
                            left join [dbo].[Permission_Type] P on a.PermissionID = P.PermissionID 
                            left join [dbo].[Recipient] r on a.RecipientID = r.RecipientID where State = 'False'", "Payment", Me)

                If Myconn.cur.Count = 0 Then Return
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add()
                    drg.Rows(i).Cells(0).Value = i + 1
                    drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Permission_Type")
                    drg.Rows(i).Cells(2).Value = Myconn.cur.Current("payment_ID")
                    drg.Rows(i).Cells(3).Value = Myconn.cur.Current("payment_num")
                    drg.Rows(i).Cells(4).Value = Myconn.cur.Current("itemName")
                    drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Payment_date")
                    drg.Rows(i).Cells(6).Value = Myconn.cur.Current("RecipientName")
                    drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Amount")
                    drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Amount_ab")
                    drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Employee")
                    drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Users")
                    drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Notes")
                    drg.Rows(i).Cells(12).Value = Myconn.cur.Current("State")
                    Myconn.cur.Position += 1
                Next

            Case 1
                Myconn.Filldataset("Select a.State ,a.Receipt_ID,a.Receipt_num,a.Receipt_date,a.Receipt_time,a.Amount,a.Amount_ab,a.Notes,a.Patient_name,a.patient_ID,
                                    P.Permission_Type,(e.EmployeeName) as Emplyee,s.specialization ,i.itemName,t.PatientName,(e.EmployeeName) as Users from  [dbo].[Receipt] a
                                    left join [dbo].[Cervices] c on a.CerviceID = c.CerviceID
                                    left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
							        left join [dbo].[Patient] t on a.Patient_ID = t.patient_ID
                                    left join [dbo].[receipt_item] i on a.itemID = i.itemID 
                                    left join [dbo].[Permission_Type] P on a.PermissionID = P.PermissionID 
                                    Left join [dbo].[Specialization] s on a.SpecializationID = s.SpecializationID where State = 'False'", "Receipt", Me)
                If Myconn.cur.Count = 0 Then Return
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add()
                    drg.Rows(i).Cells(0).Value = i + 1
                    drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Permission_Type")
                    drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Receipt_ID")
                    drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Receipt_num")
                    drg.Rows(i).Cells(4).Value = Myconn.cur.Current("itemName")
                    drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Receipt_date")
                    drg.Rows(i).Cells(6).Value = If(IsDBNull(Myconn.cur.Current("patient_ID")), Myconn.cur.Current("Patient_name"), Myconn.cur.Current("PatientName"))
                    drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Amount")
                    drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Amount_ab")
                    drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Users")
                    drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Users")
                    drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Notes")
                    drg.Rows(i).Cells(12).Value = Myconn.cur.Current("State")
                    Myconn.cur.Position += 1
                Next

            Case 2
                Myconn.Filldataset("select a.State,a.payment_ID,a.payment_num,a.Payment_date,a.payment_time,a.Amount ,a.Amount_ab,a.Notes,a.PermissionID,
                                    P.Permission_Type,(e.EmployeeName) as Employee,s.specialization,i.itemName,r.RecipientName,(u.EmployeeName) as Users from  [dbo].[Payment] a
                                    left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                                    left join [dbo].[specialization] s on a.specializationID = s.specializationID
                                    left join [dbo].[payment_item] i on a.paymentID = i.paymentID
                                    left join [dbo].[Employees] u on a.Users_ID = u.EmployeeID
                                    left join [dbo].[Permission_Type] P on a.PermissionID = P.PermissionID 
                                    left join [dbo].[Recipient] r on a.RecipientID = r.RecipientID where State = 'False'
							        union all
                                    Select a.State ,a.Receipt_ID,a.Receipt_num,a.Receipt_date,a.Receipt_time,a.Amount,a.Amount_ab,a.Notes,a.PermissionID,
                                    P.Permission_Type,(e.EmployeeName) as Emplyee,s.specialization ,i.itemName,isnull(t.PatientName,a.Patient_name),(e.EmployeeName) as Users from  [dbo].[Receipt] a
                                    left join [dbo].[Cervices] c on a.CerviceID = c.CerviceID
                                    left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
							        left join [dbo].[Patient] t on a.Patient_ID = t.patient_ID
                                    left join [dbo].[receipt_item] i on a.itemID = i.itemID 
                                    left join [dbo].[Permission_Type] P on a.PermissionID = P.PermissionID 
                                    Left join [dbo].[Specialization] s on a.SpecializationID = s.SpecializationID where State = 'False'", "Receipt", Me)
                If Myconn.cur.Count = 0 Then Return

                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add()
                    drg.Rows(i).Cells(0).Value = i + 1
                    drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Permission_Type")
                    drg.Rows(i).Cells(2).Value = Myconn.cur.Current("payment_ID")
                    drg.Rows(i).Cells(3).Value = Myconn.cur.Current("payment_num")
                    drg.Rows(i).Cells(4).Value = Myconn.cur.Current("itemName")
                    drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Payment_date")
                    drg.Rows(i).Cells(6).Value = Myconn.cur.Current("RecipientName")
                    drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Amount")
                    drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Amount_ab")
                    drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Users")
                    drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Users")
                    drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Notes")
                    drg.Rows(i).Cells(12).Value = Myconn.cur.Current("State")

                    If Myconn.cur.Current("PermissionID") = 1 Then
                        drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                    Else
                        drg.Rows(i).DefaultCellStyle.BackColor = Color.Pink
                    End If
                    Myconn.cur.Position += 1
                Next

        End Select
    End Sub

    Private Sub frmEsn_back_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label5.Left = 0
        Label5.Width = Me.Width
        Me.KeyPreview = True
    End Sub
    Private Sub cboBand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBand.SelectedIndexChanged
        Fillgrd()
    End Sub
End Class