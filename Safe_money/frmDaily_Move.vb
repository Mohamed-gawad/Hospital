
Imports System.Data.SqlClient
Imports System.Globalization
Public Class frmDaily_Move
    Dim Myconn As New connect
    Dim st As String
    Dim ds As New DataSet
    Dim cmd As New SqlCommand
    Dim dv As New DataView
    Dim da As New SqlDataAdapter
    Dim cur As CurrencyManager

    Sub Fillgrd2() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Try

            Dim SQL As String = "SELECT DayDate,c.Receipt_date,isnull(c.Wared,0) as Wared ,b.Payment_date,isnull(b.Sader,0) as Sader ,isnull(isnull(c.Wared,0) - isnull(b.Sader,0),0) as rest
                            FROM(SELECT DATEADD(DAY,ROW_NUMBER() OVER (ORDER BY (SELECT NULL))-1,DATEFROMPARTS(YEAR(@mydate2),MONTH(@mydate2),1)) as DayDate
                            FROM sys.objects s1 CROSS JOIN sys.objects s2) q
                            left join (select isnull(sum(Amount),0) as Sader ,Payment_date from [dbo].[Payment] group by Payment_date,sid having sid = 0) b
                            on  DayDate =b.Payment_date
                            left join (select isnull(sum(Amount),0) as Wared ,Receipt_date from [dbo].[Receipt] group by Receipt_date,itemID having itemID = 2) c
                            on  DayDate=c.Receipt_date
                            WHERE DayDate < = EOMONTH(@mydate2)"
            ds = New DataSet
            cmd = New SqlCommand(SQL, Myconn.conn)
            cmd.Parameters.Add("@mydate2", SqlDbType.Date).Value = Format(CDate(txt1.Text), "yyyy/MM/dd")
            da = New SqlDataAdapter(cmd)
            da.Fill(ds, "Daily_Safe")
            dv = New DataView(ds.Tables("Daily_Safe"))
            cur = CType(Me.BindingContext(dv), CurrencyManager)

            drg2.Rows.Clear()
            For i As Integer = 0 To cur.Count - 1
                drg2.Rows.Add()
                drg2.Rows(i).Cells(0).Value = i + 1
                drg2.Rows(i).Cells(1).Value = CDate(cur.Current("DayDate")).ToString("dddd", CultureInfo.CreateSpecificCulture("ar-eg"))
                drg2.Rows(i).Cells(2).Value = Format(CDate(cur.Current("DayDate")), "yyyy/MM/dd")
                drg2.Rows(i).Cells(3).Value = cur.Current("Wared")
                drg2.Rows(i).Cells(4).Value = cur.Current("Sader")
                drg2.Rows(i).Cells(5).Value = cur.Current("rest")
                cur.Position += 1
            Next

        Catch ex As Exception
            Return
        End Try
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Myconn.Filldataset("select a.State,a.payment_ID,a.payment_num,a.Payment_date,a.payment_time,a.Amount ,a.Amount_ab,a.Notes,a.PermissionID,P.Permission_Type ,a.Users_ID,a.sid,
                                    P.Permission_Type,(e.EmployeeName) as Employee,s.specialization,(r.RecipientName) as CerviceName ,i.itemName,r.RecipientName,(u.EmployeeName) as Users from  [dbo].[Payment] a
                                    left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                                    left join [dbo].[specialization] s on a.specializationID = s.specializationID
                                    left join [dbo].[payment_item] i on a.paymentID = i.paymentID
                                    left join [dbo].[Employees] u on a.Users_ID = u.EmployeeID
                                    left join [dbo].[Permission_Type] P on a.PermissionID = P.PermissionID 
                                    left join [dbo].[Recipient] r on a.RecipientID = r.RecipientID where a.sid = 0 and a.Payment_date = '" & Format(CDate(drg2.CurrentRow.Cells(2).Value), "yyyy/MM/dd") & "' 
							        union all
                                    Select a.State ,a.Receipt_ID,a.Receipt_num,a.Receipt_date,a.Receipt_time,a.Amount,a.Amount_ab,a.Notes,a.PermissionID,P.Permission_Type, a.Users_ID,a.itemID,
                                    P.Permission_Type,(e.EmployeeName) as Emplyee,s.specialization ,c.CerviceName,i.itemName,isnull(t.PatientName,a.Patient_name),(e.EmployeeName) as Users from  [dbo].[Receipt] a
                                    left join [dbo].[Cervices] c on a.CerviceID = c.CerviceID
                                    left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
							        left join [dbo].[Patient] t on a.Patient_ID = t.patient_ID
                                    left join [dbo].[receipt_item] i on a.itemID = i.itemID 
                                    left join [dbo].[Permission_Type] P on a.PermissionID = P.PermissionID 
                                    Left join [dbo].[Specialization] s on a.SpecializationID = s.SpecializationID where a.itemID = 2 and a.Receipt_date = '" & Format(CDate(drg2.CurrentRow.Cells(2).Value), "yyyy/MM/dd") & "'  order by a.Payment_date", "Receipt", Me)
        drg.Rows.Clear()
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Permission_Type")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("itemName")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("specialization")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("payment_ID")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("payment_num")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Payment_date")
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("payment_time")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Amount")
            drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Amount_ab")
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("RecipientName")
            drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Users")
            If Myconn.cur.Current("PermissionID") = 1 Then

                drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            Else
                drg.Rows(i).DefaultCellStyle.BackColor = Color.Pink

            End If
            Myconn.cur.Position += 1
        Next

    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txt1.Text = Nothing Then Return
        Fillgrd2()
    End Sub

    Private Sub drg2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg2.CellClick
        Fillgrd()
    End Sub
End Class