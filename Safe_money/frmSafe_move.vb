Imports System.Data.SqlClient
Public Class frmSafe_move
    Dim Myconn As New connect
    Dim st, st2 As String
    Dim x As Decimal = 0
    Dim y As Decimal = 0

    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        x = 0
        y = 0

        st = Nothing
        st2 = Nothing
        Select Case cboField.SelectedIndex
            Case -1
                st = Nothing
                st2 = Nothing
            Case 0 ' حسب البند
                st = "where a.itemID =" & CInt(cbo_Band.ComboBox.SelectedValue)
                st2 = "where a.paymentID =" & CInt(cbo_Band.ComboBox.SelectedValue)

            Case 1 ' البند والتاريخ
                st = "where a.itemID =" & CInt(cbo_Band.ComboBox.SelectedValue) & "and a.Receipt_date ='" & Format(CDate(txt2.Text), "yyyy/MM/dd") & "'"
                st2 = "where a.paymentID =" & CInt(cbo_Band.ComboBox.SelectedValue) & "and a.Payment_date ='" & Format(CDate(txt2.Text), "yyyy/MM/dd") & "'"

            Case 2
                st = "where a.itemID =" & CInt(cbo_Band.ComboBox.SelectedValue) & "and a.Receipt_date between'" & Format(CDate(txt2.Text), "yyyy/MM/dd") & "' and '" & Format(CDate(txt3.Text), "yyyy/MM/dd") & "'"
                st2 = "where a.paymentID =" & CInt(cbo_Band.ComboBox.SelectedValue) & "and a.Payment_date between'" & Format(CDate(txt2.Text), "yyyy/MM/dd") & "' and '" & Format(CDate(txt3.Text), "yyyy/MM/dd") & "'"

            Case 3 ' رقم الإذن
                st = "where a.Receipt_ID =" & CInt(txt2.Text)
                st2 = "where a.payment_ID =" & CInt(txt2.Text)

            Case 4 ' مجموعة أذونات
                st = "where a.Receipt_ID between " & CInt(txt2.Text) & " and " & CInt(txt3.Text)
                st2 = "where a.payment_ID between " & CInt(txt2.Text) & " and " & CInt(txt3.Text)

            Case 5 ' رقم ايصال
                st = "where a.Receipt_num =" & CInt(txt2.Text)
                st2 = "where a.payment_num =" & CInt(txt2.Text)

            Case 6 ' مجموعة ايصالات
                st = "where a.Receipt_num between " & CInt(txt2.Text) & " and " & CInt(txt3.Text)
                st2 = "where a.payment_num between " & CInt(txt2.Text) & " and " & CInt(txt3.Text)

            Case 7 ' التاريخ
                st = "where  a.Receipt_date ='" & Format(CDate(txt2.Text), "yyyy/MM/dd") & "'"
                st2 = "where  a.Payment_date ='" & Format(CDate(txt2.Text), "yyyy/MM/dd") & "'"

            Case 8  ' فترة محددة
                st = "where  a.Receipt_date between'" & Format(CDate(txt2.Text), "yyyy/MM/dd") & "' and '" & Format(CDate(txt3.Text), "yyyy/MM/dd") & "'"
                st2 = "where  a.Payment_date between'" & Format(CDate(txt2.Text), "yyyy/MM/dd") & "' and '" & Format(CDate(txt3.Text), "yyyy/MM/dd") & "'"

            Case 9 ' مبلغ محدد
                st = "where a.Amount =" & CDec(txt2.Text)
                st2 = "where a.Amount =" & CDec(txt2.Text)

            Case 10 ' مبلغ أكبر من
                st = "where a.Amount >" & CDec(txt2.Text)
                st2 = "where a.Amount >" & CDec(txt2.Text)

            Case 11 ' مبلغ أقل من
                st = "where a.Amount <" & CDec(txt2.Text)
                st2 = "where a.Amount <" & CDec(txt2.Text)

            Case 12  ' مجموعة مبالغ
                st = "where  a.Amount between '" & CDec(txt2.Text) & "' and '" & CDec(txt3.Text) & "'"
                st2 = "where  a.Amount between'" & CDec(txt2.Text) & "' and '" & CDec(txt3.Text) & "'"

            Case 13 ' المستخدم
                st = "where a.Users_ID =" & CInt(cbo_user.ComboBox.SelectedValue)
                st2 = "where a.Users_ID =" & CInt(cbo_user.ComboBox.SelectedValue)

        End Select

        Select Case cboEzn.SelectedIndex
            Case 0 ' استلام
                Myconn.Filldataset("Select a.Receipt_ID, a.ID, a.Receipt_num, a.Receipt_date, a.Receipt_time, a.Amount,a.Amount_ab, c.CerviceName, a.itemID, a.PermissionID, a.Patient_ID,P.Permission_Type ,a.Users_ID,
                            e.EmployeeName, a.Doctor_rate, s.specialization, a.Record_ID, (t.PatientName) As in_patient, (a.Patient_name) As out_patient, i.itemName, a.State from  [dbo].[Receipt] a
                            Left Join [dbo].[Cervices] c on a.CerviceID = c.CerviceID
                            Left Join [dbo].[Doctors] d on a.DoctorsID = d.DoctorsID
                            Left Join [dbo].[Doctors] r on a.Doctor_trans = r.DoctorsID
                            Left Join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                            Left Join [dbo].[Login_Patients] L on a.Record_ID = l.RecordID
                            Left Join [dbo].[Patient] t on a.Patient_ID = t.patient_ID
                            Left Join [dbo].[receipt_item] i on a.itemID = i.itemID 
                            left join [dbo].[Permission_Type] P on a.PermissionID = P.PermissionID 
                            Left Join [dbo].[Specialization] s On a.SpecializationID = s.SpecializationID " & st, "Receipt", Me)

                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add()
                    drg.Rows(i).Cells(0).Value = i + 1
                    drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Permission_Type")
                    drg.Rows(i).Cells(2).Value = Myconn.cur.Current("itemName")
                    drg.Rows(i).Cells(3).Value = Myconn.cur.Current("CerviceName")
                    drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Receipt_ID")
                    drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Receipt_num")
                    drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Receipt_date")
                    drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Receipt_time")
                    drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Amount")
                    drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Amount_ab")
                    drg.Rows(i).Cells(10).Value = If(IsDBNull(Myconn.cur.Current("patient_ID")), Myconn.cur.Current("out_patient"), Myconn.cur.Current("in_patient"))
                    drg.Rows(i).Cells(11).Value = Myconn.cur.Current("EmployeeName")

                    If Myconn.cur.Current("PermissionID") = 1 Then drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                    x += CDec(drg.Rows(i).Cells(8).Value)
                    Myconn.cur.Position += 1
                Next
            Case 1 ' دفع
                Myconn.Filldataset("select a.State,a.ID,a.payment_ID,a.payment_num,a.Payment_date,a.payment_time,a.Amount ,P.Permission_Type ,a.Amount_ab,a.PermissionID,a.paymentID,a.Users_ID,
                            e.EmployeeName,s.specialization,i.itemName,r.RecipientName,u.EmployeeName from  [dbo].[Payment] a
                            left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                            left join [dbo].[specialization] s on a.specializationID = s.specializationID
                            left join [dbo].[payment_item] i on a.paymentID = i.paymentID
                            left join [dbo].[Employees] u on a.Users_ID = u.EmployeeID
                            left join [dbo].[Permission_Type] P on a.PermissionID = P.PermissionID 
                            left join [dbo].[Recipient] r on a.RecipientID = r.RecipientID " & st2, "Payment", Me)
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
                    drg.Rows(i).Cells(11).Value = Myconn.cur.Current("EmployeeName")
                    If Myconn.cur.Current("PermissionID") = 2 Then drg.Rows(i).DefaultCellStyle.BackColor = Color.Pink
                    y += CDec(drg.Rows(i).Cells(8).Value)
                    Myconn.cur.Position += 1
                Next

            Case 2 '  الكل
                Myconn.Filldataset("select a.State,a.payment_ID,a.payment_num,a.Payment_date,a.payment_time,a.Amount ,a.Amount_ab,a.Notes,a.PermissionID,P.Permission_Type ,a.Users_ID,
                                    P.Permission_Type,(e.EmployeeName) as Employee,s.specialization,(r.RecipientName) as CerviceName ,i.itemName,r.RecipientName,(u.EmployeeName) as Users from  [dbo].[Payment] a
                                    left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
                                    left join [dbo].[specialization] s on a.specializationID = s.specializationID
                                    left join [dbo].[payment_item] i on a.paymentID = i.paymentID
                                    left join [dbo].[Employees] u on a.Users_ID = u.EmployeeID
                                    left join [dbo].[Permission_Type] P on a.PermissionID = P.PermissionID 
                                    left join [dbo].[Recipient] r on a.RecipientID = r.RecipientID " & st2 & "
							        union all
                                    Select a.State ,a.Receipt_ID,a.Receipt_num,a.Receipt_date,a.Receipt_time,a.Amount,a.Amount_ab,a.Notes,a.PermissionID,P.Permission_Type, a.Users_ID,
                                    P.Permission_Type,(e.EmployeeName) as Emplyee,s.specialization ,c.CerviceName,i.itemName,isnull(t.PatientName,a.Patient_name),(e.EmployeeName) as Users from  [dbo].[Receipt] a
                                    left join [dbo].[Cervices] c on a.CerviceID = c.CerviceID
                                    left join [dbo].[Employees] e on a.Users_ID = e.EmployeeID
							        left join [dbo].[Patient] t on a.Patient_ID = t.patient_ID
                                    left join [dbo].[receipt_item] i on a.itemID = i.itemID 
                                    left join [dbo].[Permission_Type] P on a.PermissionID = P.PermissionID 
                                    Left join [dbo].[Specialization] s on a.SpecializationID = s.SpecializationID " & st & "order by a.Payment_date", "Receipt", Me)
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
                        x += CDec(drg.Rows(i).Cells(8).Value)
                        drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                    Else
                        drg.Rows(i).DefaultCellStyle.BackColor = Color.Pink
                        y += CDec(drg.Rows(i).Cells(8).Value)
                    End If
                    Myconn.cur.Position += 1
                Next
        End Select
        sumgrd()
    End Sub
    Private Sub frmSafe_move_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label16.Left = 0
        Label16.Width = Me.Width

    End Sub
    Sub sumgrd()

        Label3.Text = x.ToString
        Label4.Text = y.ToString
        Label6.Text = Val(Label3.Text) - Val(Label4.Text)

        TextBox1.Text = "(  " & clsNumber.nTOword(Label3.Text) & "  )"
        TextBox2.Text = "(  " & clsNumber.nTOword(Label4.Text) & "  )"
        TextBox3.Text = "(  " & clsNumber.nTOword(Label6.Text) & "  )"
    End Sub
    Private Sub cboEzn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEzn.SelectedIndexChanged

        Select Case cboEzn.SelectedIndex
            Case 0
                Myconn.Fillcombo("select * from receipt_item", "receipt_item", "itemID", "itemName", Me, cbo_Band.ComboBox)

            Case 1
                Myconn.Fillcombo("select * from payment_item", "payment_item", "paymentID", "itemName", Me, cbo_Band.ComboBox)

            Case 2
                cbo_Band.Visible = False
        End Select
        Fillgrd()
    End Sub
    Private Sub cboField_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboField.SelectedIndexChanged
        txt2.Visible = False
        lbl3.Visible = False
        txt3.Visible = False
        lbl4.Visible = False
        cbo_user.Visible = False
        cbo_Band.Visible = False
        Select Case cboField.SelectedIndex
            Case 0  'البند
                cbo_Band.Visible = True

            Case 1 ' البند والتاريخ
                cbo_Band.Visible = True
                txt2.Visible = True
                lbl3.Text = "يوم"
            Case 2 ' البند لفترة محددة
                cbo_Band.Visible = True
                txt2.Visible = True
                txt3.Visible = True
                lbl3.Visible = True
                lbl3.Text = "من"
                lbl4.Visible = True
                lbl4.Text = "إلى"
            Case 3 ' رقم الإذن
                lbl3.Visible = True
                lbl3.Text = "الاذن رقم"
                txt2.Visible = True
            Case 4 ' مجموعة أذونات
                lbl3.Visible = True
                lbl3.Text = "من الاذن رقم"
                txt2.Visible = True
                lbl4.Visible = True
                lbl4.Text = "إلى الاذن رقم"
                txt3.Visible = True
            Case 5 ' رقم ايصال
                lbl3.Visible = True
                lbl3.Text = "الايصال رقم"
                txt2.Visible = True
            Case 6 ' مجموعة ايصالات
                lbl3.Visible = True
                lbl3.Text = "من الايصال رقم"
                txt2.Visible = True
                lbl4.Visible = True
                lbl4.Text = "إلى"
                txt3.Visible = True
            Case 7 ' التاريخ
                txt2.Visible = True
                lbl3.Visible = True
                lbl3.Text = "تاريخ"
            Case 8 ' فترة محددة
                lbl3.Visible = True
                lbl3.Text = "الفترة من"
                txt2.Visible = True
                lbl4.Visible = True
                lbl4.Text = "إلى"
                txt3.Visible = True
            Case 9 ' مبلغ محدد
                txt2.Visible = True
                lbl3.Visible = True
                lbl3.Text = "المبلغ"
            Case 10 ' مبلغ أكبر من
                txt2.Visible = True
                lbl3.Visible = True
                lbl3.Text = " مبلغ أكبر من"
            Case 11 ' مبلغ أقل من
                txt2.Visible = True
                lbl3.Visible = True
                lbl3.Text = "مبلغ أقل من"
            Case 12 ' مجموعة مبالغ
                lbl3.Visible = True
                lbl3.Text = "مبلغ من"
                txt2.Visible = True
                txt3.Visible = True
                lbl4.Visible = True
                lbl4.Text = "إلى"
                txt3.Visible = True
            Case 13 ' المستخدم
                Myconn.Fillcombo("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cbo_user.ComboBox)
                cbo_user.Visible = True
        End Select
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Fillgrd()
    End Sub

End Class