Public Class frmBills_Add_Drage_Incubator

    Dim Myconn As New connect
    Dim st As String
    Dim StockID As Integer
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Try
            drg.Rows.Clear()
            Select Case cboSearch.SelectedIndex
                Case 0 ' رقم فاتورة
                    If txt1.Text = "" Then
                        MsgBox("أدخل رقم الفاتورة")
                        Return
                    End If
                    st = "having a.Stock_ID = " & StockID & " and a.Bill_ID =" & CInt(txt1.Text)
                Case 1 ' مجموعة فواتير
                If txt1.Text = "" Or txt2.Text = "" Then
                    MsgBox("أدخل رقم الفاتورة")
                    Return
                End If
                st = "having a.Stock_ID = " & StockID & " and a.Bill_ID between " & CInt(txt1.Text) & " and " & CInt(txt2.Text)
            Case 2 ' تاريخ محدد
                If txt1.Text = "" Then
                    MsgBox("أدخل التاريخ")
                    Return
                End If
                    st = "having a.Stock_ID = " & StockID & " and a.Bill_Date ='" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'"
                Case 3 ' فترة زمنية
                If txt1.Text = "" Or txt2.Text = "" Then
                    MsgBox("أدخل التاريخ")
                    Return
                End If
                    st = "having a.Stock_ID = " & StockID & " and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"

                Case 4 ' قيمة فاتورة
                If txt1.Text = "" Then
                    MsgBox("أدخل رقم الفاتورة")
                    Return
                End If
                    st = "having a.Stock_ID = " & StockID & " and (sum(a.Total_Price) - isnull(c.back,0)) =" & CDbl(txt1.Text)

                Case 5 ' قيم فواتير
                If txt1.Text = "" Or txt2.Text = "" Then
                    MsgBox("أدخل رقم الفاتورة")
                    Return
                End If
                    st = "having a.Stock_ID = " & StockID & " and (sum(a.Total_Price) - isnull(c.back,0)) between " & CDbl(txt1.Text) & " and " & CDbl(txt2.Text)

                Case 6 ' مسئول صرف
                    st = "having a.Stock_ID = " & StockID & " and a.EmployeeID =" & CInt(cboEmployee.ComboBox.SelectedValue)
                Case 7 'مسئول صرف خلال فترة
                If txt1.Text = "" Or txt2.Text = "" Then
                    MsgBox("أدخل التاريخ")
                    Return
                End If
                    st = "having a.Stock_ID = " & StockID & " and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'and a.EmployeeID =" & CInt(cboEmployee.ComboBox.SelectedValue)

                Case 8 'مستخدم
                    st = ",a.Users_ID having a.Stock_ID = " & StockID & " and a.Users_ID =" & CInt(cboEmployee.ComboBox.SelectedValue)

                Case 9 ' مستخدم وفترة
                If txt1.Text = "" Or txt2.Text = "" Then
                    MsgBox("أدخل التاريخ")
                    Return
                End If
                    st = ",a.Users_ID having a.Stock_ID = " & StockID & " and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'and a.Users_ID =" & CInt(cboEmployee.ComboBox.SelectedValue)

                Case 10 ' طبيب
                    st = ",a.DoctorsID having a.Stock_ID = " & StockID & " and a.DoctorsID =" & CInt(cboEmployee.ComboBox.SelectedValue)

                Case 11 ' طبيب وفترة
                If txt1.Text = "" Or txt2.Text = "" Then
                    MsgBox("أدخل التاريخ")
                    Return
                End If
                    st = ",a.DoctorsID having a.Stock_ID = " & StockID & " and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'and a.DoctorsID =" & CInt(cboEmployee.ComboBox.SelectedValue)

                Case 12 ' خدمة
                    st = ",a.CerviceID having a.Stock_ID = " & StockID & " and a.CerviceID =" & CInt(cboEmployee.ComboBox.SelectedValue)

                Case 13 ' خدمة وفترة
                If txt1.Text = "" Or txt2.Text = "" Then
                    MsgBox("أدخل التاريخ")
                    Return
                End If
                    st = ",a.CerviceID having a.Stock_ID = " & StockID & " and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'and a.CerviceID =" & CInt(cboEmployee.ComboBox.SelectedValue)

                Case 14 ' اسم المريض
                    st = "having a.Stock_ID = " & StockID & " and a.Patient_ID =" & CInt(cboEmployee.ComboBox.SelectedValue)
                Case 15 ' قسم
                    st = ",a.specializationID having a.Stock_ID = " & StockID & " and a.specializationID =" & CInt(cboEmployee.ComboBox.SelectedValue)

                Case 16 ' قسم وفترة
                If txt1.Text = "" Or txt2.Text = "" Then
                    MsgBox("أدخل التاريخ")
                    Return
                End If
                    st = ",a.specializationID having a.Stock_ID = " & StockID & " and a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'and a.specializationID =" & CInt(cboEmployee.ComboBox.SelectedValue)


                Case 17 ' كل الفواتير
                    st = " having a.Stock_ID = " & StockID & ""

            End Select
        Myconn.Filldataset("Select a.Bill_ID,b.PatientName,a.Patient_ID,a.bill_date,sum(a.Total_Price) As Total,count(Drug_ID) As count_Drug1,isnull(c.back,0) As back,isnull(c.count_Drug,0) As count_Drug,
                                (sum(a.Total_Price) - isnull(c.back,0)) as final, (e.EmployeeName) as Employee ,z.specialization,d.DoctorsName,s.CerviceName,(u.EmployeeName) as Users from [dbo].[Stocks_Sales] a
                                left join [dbo].[Patient] b on a.Patient_ID = b.Patient_ID
                                left join (select Bill_ID,State ,sum(Total_Price) as back,count(Drug_ID) as count_Drug from [dbo].[Stocks_Sales] group by Bill_ID,State,Stock_ID having State ='false' and Stock_ID = " & StockID & ") c
                                on a.Bill_ID = c.Bill_ID
                                left join Employees e on a.EmployeeID = e.EmployeeID
                                left join Doctors d on a.DoctorsID = d.DoctorsID
                                left join Employees U on a.Users_ID = U.EmployeeID
                                left join Cervices S on a.CerviceID = S.CerviceID
                                left join specialization z on a.specializationID = z.specializationID
                                group by a.Stock_ID,a.Bill_ID,b.PatientName,a.bill_date,c.back,c.count_Drug,a.Patient_ID,c.state,e.EmployeeName,a.EmployeeID,d.DoctorsName,u.EmployeeName,s.CerviceName,z.specialization " & st & "order by a.bill_date", "Stocks_Sales", Me)



            drg.Rows.Clear()
            If Myconn.cur.Count = 0 Then MsgBox("لا توجد نتائج")
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Bill_ID")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("bill_date")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("PatientName")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Patient_ID")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("count_Drug1")
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Total")
                drg.Rows(i).Cells(7).Value = Myconn.cur.Current("count_Drug")
                drg.Rows(i).Cells(8).Value = Myconn.cur.Current("back")
                drg.Rows(i).Cells(9).Value = Myconn.cur.Current("final")
                drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Employee")
                drg.Rows(i).Cells(11).Value = Myconn.cur.Current("specialization")
                drg.Rows(i).Cells(12).Value = Myconn.cur.Current("DoctorsName")
                drg.Rows(i).Cells(13).Value = Myconn.cur.Current("CerviceName")
                drg.Rows(i).Cells(14).Value = Myconn.cur.Current("Users")
                Myconn.cur.Position += 1
            Next

            Myconn.Sum_drg(drg, 9, Label42, Label41)
        Catch ex As Exception
        MsgBox("هناك خطأ في البيانات")
        End Try

    End Sub
    Private Sub cboSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSearch.SelectedIndexChanged
        Select Case cboSearch.SelectedIndex
            Case 0 ' رقم فاتورة
                cboEmployee.Visible = False
                txt1.Visible = True
                txt2.Visible = False
                Label1.Visible = False
            Case 1 ' مجموعة فواتير
                cboEmployee.Visible = False
                txt1.Visible = True
                txt2.Visible = True
                Label1.Visible = True
            Case 2 ' تاريخ محدد
                cboEmployee.Visible = False
                txt1.Visible = True
                txt2.Visible = False
                Label1.Visible = False
            Case 3 ' فترة زمنية
                cboEmployee.Visible = False
                txt1.Visible = True
                txt2.Visible = True
                Label1.Visible = True
            Case 4 ' قيمة فاتورة
                cboEmployee.Visible = False
                txt1.Visible = True
                txt2.Visible = False
                Label1.Visible = False
            Case 5 ' قيم فواتير
                cboEmployee.Visible = False
                txt1.Visible = True
                txt2.Visible = True
                Label1.Visible = True
            Case 6 ' مسئول صرف
                cboEmployee.Visible = True
                Myconn.Fillcombo1("select * from Employees order by EmployeeName", "Employees", "EmployeeID", "EmployeeName", Me, cboEmployee.ComboBox)
                txt1.Visible = False
                txt2.Visible = False
                Label1.Visible = False
            Case 7 'مسئول صرف خلال فترة
                cboEmployee.Visible = True
                Myconn.Fillcombo1("select * from Employees order by EmployeeName", "Employees", "EmployeeID", "EmployeeName", Me, cboEmployee.ComboBox)
                txt1.Visible = True
                txt2.Visible = True
                Label1.Visible = True
            Case 8 'مستخدم
                cboEmployee.Visible = True
                Myconn.Fillcombo1("select * from Employees order by EmployeeName", "Employees", "EmployeeID", "EmployeeName", Me, cboEmployee.ComboBox)
                txt1.Visible = False
                txt2.Visible = False
                Label1.Visible = False
            Case 9 ' مستخدم وفترة
                cboEmployee.Visible = True
                Myconn.Fillcombo1("select * from Employees order by EmployeeName", "Employees", "EmployeeID", "EmployeeName", Me, cboEmployee.ComboBox)
                txt1.Visible = True
                txt2.Visible = True
                Label1.Visible = True
            Case 10 ' طبيب
                cboEmployee.Visible = True
                Myconn.Fillcombo1("select * from Doctors order by DoctorsName", "Doctors", "DoctorsID", "DoctorsName", Me, cboEmployee.ComboBox)
                txt1.Visible = False
                txt2.Visible = False
                Label1.Visible = False
            Case 11 ' طبيب وفترة
                cboEmployee.Visible = True
                Myconn.Fillcombo1("select * from Doctors order by DoctorsName", "Doctors", "DoctorsID", "DoctorsName", Me, cboEmployee.ComboBox)
                txt1.Visible = True
                txt2.Visible = True
                Label1.Visible = True
            Case 12 ' خدمة
                cboEmployee.Visible = True
                Myconn.Fillcombo1("select * from Cervices order by CerviceName", "Cervices", "CerviceID", "CerviceName", Me, cboEmployee.ComboBox)
                txt1.Visible = False
                txt2.Visible = False
                Label1.Visible = False
            Case 13 ' خدمة وفترة
                cboEmployee.Visible = True
                Myconn.Fillcombo1("select * from Cervices order by CerviceName", "Cervices", "CerviceID", "CerviceName", Me, cboEmployee.ComboBox)
                txt1.Visible = True
                txt2.Visible = True
                Label1.Visible = True
            Case 14 ' اسم المريض
                cboEmployee.Visible = True
                Myconn.Fillcombo1("select * from Patient order by PatientName", "Patient", "patient_ID", "PatientName", Me, cboEmployee.ComboBox)
                txt1.Visible = False
                txt2.Visible = False
                Label1.Visible = False
            Case 15 ' قسم
                cboEmployee.Visible = True
                Myconn.Fillcombo1("select * from specialization  where Kind = 'k' order by specialization", "specialization", "specializationID", "specialization", Me, cboEmployee.ComboBox)
                txt1.Visible = False
                txt2.Visible = False
                Label1.Visible = False

            Case 16 ' قسم وفترة
                cboEmployee.Visible = True
                Myconn.Fillcombo1("select * from specialization  where Kind = 'k' order by specialization", "specialization", "specializationID", "specialization", Me, cboEmployee.ComboBox)
                txt1.Visible = True
                txt2.Visible = True
                Label1.Visible = True

            Case 17 ' كل الفواتير
                cboEmployee.Visible = False
                txt1.Visible = False
                txt2.Visible = False
                Label1.Visible = False
        End Select
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Fillgrd()
    End Sub
    Private Sub frmBills_Add_Drage_Incubator_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label5.Left = 0
        Label5.Width = Me.Width
        StockID = 4
    End Sub
    Private Sub frmBills_Add_Drage_Incubator_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        StockID = 4
    End Sub
End Class