Imports System.Data.SqlClient
Imports System.Globalization
Public Class frmInternal_section
    Dim myconn As New connect
    Dim fin As Boolean
    Dim X, Y, V, AM2, B As Integer
    Dim StockID As Integer
    Dim V2, V1 As Decimal
    Dim st, Unit1 As String
    Dim A, Amount As Double
#Region "الدوال"
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        myconn.ClearAllText(Me, grbPatientData)
        myconn.Filldataset("select  isnull(max(RecordID),0) as RecordID from Login_Patients ", "Login_Patients", Me)
        If myconn.dv.Count = 0 Then
            txtRecordID.Text = "1"
        Else
            txtRecordID.Text = (myconn.cur.Current("RecordID") + 1).ToString
        End If


        fin = False
        cboPatientName.SelectedIndex = -1
        cboKissm.SelectedIndex = -1
        cboDoctor.SelectedIndex = -1
        cboCervice.SelectedIndex = -1
        dtp1.Text = Today.Date

        fin = True
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Select Case X
            Case 0 '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> زيارات المريض
                If Not fin Then Return
                drg_visits.Rows.Clear()
                myconn.Filldataset("select a.patient_ID,b.PatientName,a.VisitID,a.Login_Date,isnull(a.Out_Date,''),a.RecordID from Login_Patients a " &
                                   "left join Patient b on a.patient_ID=b.patient_ID where a.patient_ID =" & cboPatientName.SelectedValue, "Login_Patients", Me)

                For i As Integer = 0 To myconn.cur.Count - 1
                    drg_visits.Rows.Add(New String() {i + 1, myconn.cur.Current(1), myconn.cur.Current(2), myconn.cur.Current(3), myconn.cur.Current(4), myconn.cur.Current(5)})
                    myconn.cur.Position += 1
                Next
                myconn.DataGridview_MoveLast(drg_visits, 3)

            Case 1 '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> الأشعات
                drg_Xray.Rows.Clear()
                myconn.Filldataset6("select b.CerviceName, a.XrayID,X_ray_Date,a.X_ray_Price,a.ID,isnull(c.DoctorsName,''),isnull(a.Dorctors_Rate,0),isnull(a.Doctors_Value,0),(b.Cervice_Price) as XrayPrice from Patient_X_rays a " &
                                   "left join Cervices b on a.XrayID = b.CerviceID " &
                                   "left join Doctors c on a.DoctorsID = c.DoctorsID " &
                                   "where a.RecordID =" & txtRecordID.Text, "Patient_X_rays", Me)

                For i As Integer = 0 To myconn.cur6.Count - 1
                    drg_Xray.Rows.Add(New String() {i + 1, myconn.cur6.Current(0), myconn.cur6.Current(1), myconn.cur6.Current(2), myconn.cur6.Current(3), myconn.cur6.Current(4), myconn.cur6.Current(5), myconn.cur6.Current(6), myconn.cur6.Current(7)})
                    myconn.cur6.Position += 1
                Next
                myconn.DataGridview_MoveLast(drg_Xray, 1)
            Case 2 '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> التحاليل
                drg_Analysis.Rows.Clear()
                myconn.Filldataset5("select b.CerviceName, a.AnalysisID,a.Analysis_Date,a.Analysis_Price,a.ID,isnull(c.DoctorsName,''),isnull(a.Dorctors_Rate,0),isnull(a.Doctors_Value,0),(b.Cervice_Price) as AnalysisPrice from Patient_Analysis a " &
                                   "left join Cervices b on a.AnalysisID = b.CerviceID " &
                                   "left join Doctors c on a.DoctorsID = c.DoctorsID " &
                                   "where a.RecordID =" & txtRecordID.Text, "Patient_Analysis", Me)

                For i As Integer = 0 To myconn.cur5.Count - 1
                    drg_Analysis.Rows.Add(New String() {i + 1, myconn.cur5.Current(0), myconn.cur5.Current(1), myconn.cur5.Current(2), myconn.cur5.Current(3), myconn.cur5.Current(4), myconn.cur5.Current(5), myconn.cur5.Current(6), myconn.cur5.Current(7)})
                    myconn.cur5.Position += 1
                Next
                myconn.DataGridview_MoveLast(drg_Analysis, 1)

            Case 3 '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> أجهزة العمليات
                drg_Ope_tools.Rows.Clear()
                myconn.Filldataset6("select b.Operation_Tool_Name, a.Operation_Tool_ID,a.Operation_Tool_Date,a.Operation_Tool_Price,a.ID,(b.Operation_Tool_Price) as Operation_Tool_Price from Patient_Operation_Tools a " &
                                   "left join Opreation_Tools b on a.Operation_Tool_ID = b.Operation_Tool_ID where a.RecordID =" & txtRecordID.Text, "Patient_Operation_Tools", Me)

                For i As Integer = 0 To myconn.cur6.Count - 1
                    drg_Ope_tools.Rows.Add(New String() {i + 1, myconn.cur6.Current(0), myconn.cur6.Current(1), myconn.cur6.Current(2), myconn.cur6.Current(3), myconn.cur6.Current(4)})
                    myconn.cur6.Position += 1
                Next
                txtOperation.Text = ""

            Case 4 '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> أجهزة عناية
                drg_Care_tools.Rows.Clear()
                myconn.Filldataset7("select b.Car_Tool_Name, a.Car_Tool_ID,a.Car_Tool_Date,a.Car_Tool_Price,a.ID,(b.Car_Tool_Price) as Car_Tool_Price from Patient_Car_Tools a " &
                                   "left join Car_Tools b on a.Car_Tool_ID = b.Car_Tool_ID where a.RecordID =" & txtRecordID.Text, "Patient_Car_Tools", Me)

                For i As Integer = 0 To myconn.cur7.Count - 1
                    drg_Care_tools.Rows.Add(New String() {i + 1, myconn.cur7.Current(0), myconn.cur7.Current(1), myconn.cur7.Current(2), myconn.cur7.Current(3), myconn.cur7.Current(4)})
                    myconn.cur7.Position += 1
                Next
                txtCar_Price.Text = ""

            Case 5 '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> مستهلك أكسجين
                drg_O2.Rows.Clear()
                myconn.Filldataset7("select O2_Date,O2_Amount,O2_Price,ID  from Patient_O2 where RecordID =" & txtRecordID.Text, "Patient_O2", Me)
                For i As Integer = 0 To myconn.cur7.Count - 1
                    drg_O2.Rows.Add(New String() {i + 1, myconn.cur7.Current(0), myconn.cur7.Current(1), myconn.cur7.Current(2), myconn.cur7.Current(3)})
                    myconn.cur7.Position += 1
                Next
            Case 6 '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> المساعدين
                drg_Drugs.Rows.Clear()
                myconn.Filldataset7("select a.label,a.DoctorID,a.EmployeeID,a.ID,d.DoctorsName,e.EmployeeName from Patient_helper a
                                      left join Doctors d on a.DoctorID = d.DoctorsID
                                      left join Employees e on a.EmployeeID = e.EmployeeID where RecordID =" & txtRecordID.Text, "Patient_helper", Me)
                If myconn.cur7.Count = 0 Then Return
                Dim name, cod As String


                For i As Integer = 0 To myconn.cur7.Count - 1
                    If myconn.cur7.Current("DoctorID") = 0 Then
                        name = myconn.cur7.Current("EmployeeName")
                        cod = myconn.cur7.Current("EmployeeID")
                    Else
                        name = myconn.cur7.Current("DoctorsName")
                        cod = myconn.cur7.Current("DoctorID")
                    End If
                    drg_Drugs.Rows.Add()
                    drg_Drugs.Rows(i).Cells(0).Value = i + 1
                    drg_Drugs.Rows(i).Cells(1).Value = myconn.cur7.Current("label")
                    drg_Drugs.Rows(i).Cells(2).Value = name
                    drg_Drugs.Rows(i).Cells(3).Value = cod
                    drg_Drugs.Rows(i).Cells(4).Value = myconn.cur7.Current("ID")
                    myconn.cur7.Position += 1
                Next

            Case 7 ' >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> التخدير
                drg_Narcosis.Rows.Clear()
                myconn.Filldataset7("select b.DoctorsName,a.DoctorsID,a.Narcosis_Date,a.Narcosis_Price,a.ID  from Patient_Narcosis a " &
                                    "left join Doctors b on a.DoctorsID = b.DoctorsID " &
                                    "where RecordID =" & txtRecordID.Text, "Patient_Narcosis", Me)
                For i As Integer = 0 To myconn.cur7.Count - 1
                    drg_Narcosis.Rows.Add(New String() {i + 1, myconn.cur7.Current(0), myconn.cur7.Current(1), myconn.cur7.Current(2), myconn.cur7.Current(3), myconn.cur7.Current(4)})
                    myconn.cur7.Position += 1
                Next
                txtPrice_Narcosis.Text = ""

            Case 8
                drg_Add_bill.Rows.Clear()
                myconn.Filldataset7("select a.Add_Date,a.VisitID,a.Nurs,a.Technical,a.Tax,a.Service,a.Expenses_Admin,a.Other_Expenses,a.Resu,a.Incubator,a.Doctors_Rat ,b.DoctorsName,a.ID  from Patient_Add_To_Bill a " &
                                    "left join Doctors b on a.DoctorsID = b.DoctorsID " &
                                    "where RecordID =" & txtRecordID.Text, "Patient_Add_To_Bill", Me)
                For i As Integer = 0 To myconn.cur7.Count - 1
                    drg_Add_bill.Rows.Add(New String() {i + 1, myconn.cur7.Current(0), myconn.cur7.Current(1), "التمريض", myconn.cur7.Current(2), myconn.cur7.Current(12)})
                    drg_Add_bill.Rows.Add(New String() {i + 2, myconn.cur7.Current(0), myconn.cur7.Current(1), "الفني", myconn.cur7.Current(3), myconn.cur7.Current(12)})
                    drg_Add_bill.Rows.Add(New String() {i + 3, myconn.cur7.Current(0), myconn.cur7.Current(1), "الضريبة", myconn.cur7.Current(4), myconn.cur7.Current(12)})
                    drg_Add_bill.Rows.Add(New String() {i + 4, myconn.cur7.Current(0), myconn.cur7.Current(1), "متابعة طبيب", myconn.cur7.Current(10), myconn.cur7.Current(12)})
                    drg_Add_bill.Rows.Add(New String() {i + 5, myconn.cur7.Current(0), myconn.cur7.Current(1), "خدمة 10 %", myconn.cur7.Current(5), myconn.cur7.Current(12)})
                    drg_Add_bill.Rows.Add(New String() {i + 6, myconn.cur7.Current(0), myconn.cur7.Current(1), "مصاريف إدارية", myconn.cur7.Current(6), myconn.cur7.Current(12)})
                    drg_Add_bill.Rows.Add(New String() {i + 7, myconn.cur7.Current(0), myconn.cur7.Current(1), "مصاريف أخرى", myconn.cur7.Current(7), myconn.cur7.Current(12)})
                    drg_Add_bill.Rows.Add(New String() {i + 8, myconn.cur7.Current(0), myconn.cur7.Current(1), "إفاقة", myconn.cur7.Current(8), myconn.cur7.Current(12)})
                    drg_Add_bill.Rows.Add(New String() {i + 9, myconn.cur7.Current(0), myconn.cur7.Current(1), "الحضانة", myconn.cur7.Current(9), myconn.cur7.Current(12)})
                    myconn.cur7.Position += 1
                Next
                ClearAllText(Me, grbBill_Adds)
            Case 9 '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> اضافات على الإقامة
                drg_Add_staying.Rows.Clear()
                myconn.Filldataset7("select band,Sdate,band_Price,ID from patient_Add_To_Staying where RecordID =" & txtRecordID.Text, "patient_Add_To_Staying", Me)
                For i As Integer = 0 To myconn.cur7.Count - 1
                    drg_Add_staying.Rows.Add(New String() {i + 1, myconn.cur7.Current(0), myconn.cur7.Current(1), myconn.cur7.Current(2), myconn.cur7.Current(3)})
                    myconn.cur7.Position += 1
                Next
                txtStaying_Band.Text = ""
                txtStaying_Price.Text = ""
            Case 10 '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> خدمات الاستقبال
                drg_Reciption.Rows.Clear()
                myconn.Filldataset7("select band,Recp_Date,band_Price,ID from Patient_Add_To_Reception where RecordID =" & txtRecordID.Text, "Patient_Add_To_Reception", Me)
                For i As Integer = 0 To myconn.cur7.Count - 1
                    drg_Reciption.Rows.Add(New String() {i + 1, myconn.cur7.Current(0), myconn.cur7.Current(1), myconn.cur7.Current(2), myconn.cur7.Current(3)})
                    myconn.cur7.Position += 1
                Next
                txtRecp_Price.Text = ""
                txtService_Recp.Text = ""

            Case 11 '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> مستهلك الاقامة

            Case 12 '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> مستهلك العمليات

            Case 13 '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> مستهلك الحضانات

            Case 14
                myconn.Filldataset2("select a.Login_Date,a.VisitID,a.RecordID,a.DoctorPrice ,ISNULL(b.Total_Xray,0),ISNULL(Total_Analysis,0),
                        isnull(Operation_Tools,0),isnull(car_Tools,0),isnull(O2,0),isnull(Operation_Drag,0),
                        isnull(Staying_Drag,0),isnull(Incubator_Drag,0),isnull(Narcosis,0),isnull(Add_To_Bill,0),isnull(Add_To_Staying,0),isnull(Add_To_Reception,0)
                        from [dbo].[Login_Patients] a
                        left join (select RecordID, sum(X_ray_Price) as Total_Xray from [dbo].[Patient_X_rays] group by RecordID) b
                                                   on a.RecordID = b.RecordID
                        left join ( select RecordID, sum(Analysis_Price) as Total_Analysis from [dbo].[Patient_Analysis]  group by RecordID ) c
							                         on a.RecordID = c.RecordID
                        left join ( select RecordID,sum(Operation_Tool_Price) as Operation_Tools from [dbo].[Patient_Operation_Tools] group by RecordID ) d
							                         on a.RecordID = d.RecordID
                        left join ( select RecordID,sum(Car_Tool_Price) as car_Tools from [dbo].[Patient_Car_Tools] group by RecordID ) e
							                         on a.RecordID = e.RecordID
                        left join ( select RecordID,sum(O2_Price) as O2 from [dbo].[Patient_O2] group by RecordID ) f
							                         on a.RecordID = f.RecordID
                        left join ( select RecordID,sum(Total_Price) as Operation_Drag from [dbo].[Stocks_Sales] group by RecordID,Stock_ID,State having Stock_ID = 2 and State = 'True' ) h
							                         on a.RecordID = h.RecordID
                        left join ( select RecordID,sum(Total_Price) as Staying_Drag from [dbo].[Stocks_Sales] group by  RecordID,Stock_ID,State having Stock_ID = 5  and State = 'True') i
							                         on a.RecordID = i.RecordID
                        left join ( select RecordID,sum(Total_Price) as Incubator_Drag from [dbo].[Stocks_Sales] group by  RecordID,Stock_ID,State having Stock_ID = 4  and State = 'True') j
							                         on a.RecordID = j.RecordID
                        left join ( select RecordID,sum(Narcosis_Price) as Narcosis from [dbo].[Patient_Narcosis] group by RecordID ) k
							                         on a.RecordID = k.RecordID
                        left join ( select RecordID,(sum(Nurs)+sum(Technical)+sum(Tax)+sum(Service)+sum(Expenses_Admin)+sum(Other_Expenses)+sum(Resu)+sum(Incubator)+sum(Doctors_Rat)) as Add_To_Bill from [dbo].[Patient_Add_To_Bill] group by RecordID ) L
							                         on a.RecordID = L.RecordID
                        left join ( select RecordID,sum(band_Price) as Add_To_Staying from [dbo].[patient_Add_To_Staying] group by RecordID ) m
							                         on a.RecordID = m.RecordID
                        left join ( select RecordID,sum(band_Price) as Add_To_Reception from [dbo].[Patient_Add_To_Reception] group by RecordID ) n
							                         on a.RecordID = n.RecordID
                         where a.RecordID =" & CInt(txtRecordID.Text), "Patient_Account", Me)
                If myconn.dv2.Count = 0 Then
                    Return
                End If

                drg_Acount.Rows.Clear()
                For i As Integer = 0 To myconn.cur2.Count - 1
                    drg_Acount.Rows.Add(New String() {i + 1, myconn.cur2.Current(0), myconn.cur2.Current(1), "أجر الطبيب", myconn.cur2.Current(3)})
                    drg_Acount.Rows.Add(New String() {i + 2, myconn.cur2.Current(0), myconn.cur2.Current(1), "الأشعات", myconn.cur2.Current(4)})
                    drg_Acount.Rows.Add(New String() {i + 3, myconn.cur2.Current(0), myconn.cur2.Current(1), "التحاليل", myconn.cur2.Current(5)})
                    drg_Acount.Rows.Add(New String() {i + 4, myconn.cur2.Current(0), myconn.cur2.Current(1), "أجهزة عمليات", myconn.cur2.Current(6)})
                    drg_Acount.Rows.Add(New String() {i + 5, myconn.cur2.Current(0), myconn.cur2.Current(1), "أجهزة عناية", myconn.cur2.Current(7)})
                    drg_Acount.Rows.Add(New String() {i + 6, myconn.cur2.Current(0), myconn.cur2.Current(1), "أكسجين", myconn.cur2.Current(8)})
                    'drg_Acount.Rows.Add(New String() {i + 7, myconn.cur2.Current(0), myconn.cur2.Current(1), "حساب الصيدلية", myconn.cur2.Current(9)})
                    drg_Acount.Rows.Add(New String() {i + 8, myconn.cur2.Current(0), myconn.cur2.Current(1), "مستهلك عمليات", myconn.cur2.Current(9)})
                    drg_Acount.Rows.Add(New String() {i + 9, myconn.cur2.Current(0), myconn.cur2.Current(1), "مستهلك إقامة", myconn.cur2.Current(10)})
                    drg_Acount.Rows.Add(New String() {i + 10, myconn.cur2.Current(0), myconn.cur2.Current(1), "مستهلك حضانات", myconn.cur2.Current(11)})
                    drg_Acount.Rows.Add(New String() {i + 11, myconn.cur2.Current(0), myconn.cur2.Current(1), "التخدير", myconn.cur2.Current(12)})
                    drg_Acount.Rows.Add(New String() {i + 12, myconn.cur2.Current(0), myconn.cur2.Current(1), "إضافات على الفاتورة", myconn.cur2.Current(13)})
                    drg_Acount.Rows.Add(New String() {i + 13, myconn.cur2.Current(0), myconn.cur2.Current(1), "إضافات على الإقامة", myconn.cur2.Current(14)})
                    drg_Acount.Rows.Add(New String() {i + 15, myconn.cur2.Current(0), myconn.cur2.Current(1), "خدمات الاستقبال", myconn.cur2.Current(15)})
                    myconn.cur2.Position += 1
                Next i
                myconn.Sum_drg(drg_Acount, 4, Label49, Label48)

        End Select
    End Sub
    Sub Fillgrd_Stock() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg_Mos.Rows.Clear()
        Select Case V
            Case 0
                st = "where a.Bill_ID =" & CInt(txt_Bill_ID.Text) & "and a.Stock_ID = " & StockID & ""
            Case 1
                st = "where a.RecordID =" & CInt(txtRecordID.Text) & "and a.Stock_ID = " & StockID & ""

        End Select
        myconn.Filldataset("Select m.Max_Unit_Name,n.Min_Unit_Name,a.Time_Add,a.Bill_ID,a.Bill_Date ,c.Co_Name, b.Drug_Name,a.Drug_ID,a.Drug_exp,a.RecordID,
                                   a.Amount,a.unit,a.Unit_Kind,a.Drug_Price,a.Total_Price,(d.EmployeeName) As Users,g.GroupName,a.Stock_ID,a.EmployeeID,a.Patient_ID,
                                   (e.EmployeeName) As Employee,a.ID,a.state,R.PatientName,b.Min_Unit_number,a.specializationID,a.DoctorsID,a.CerviceID from Stocks_Sales a
                                   Left Join Drugs b on a.Drug_ID = b.Drug_ID
                                   Left Join Employees d on a.Users_ID = d.EmployeeID
                                   Left Join Employees e on a.EmployeeID = e.EmployeeID
                                   Left Join Drug_Groups g on b.GroupID = g.GroupID
                                   Left Join Max_Unit M on a.Unit = M.Max_UnitID
                                   Left Join Min_Unit n on a.Unit = n.Min_UnitID
                                   Left Join Patient R on a.Patient_ID = R.Patient_ID
                                   Left Join Co_Drug c On b.Co_ID = c.Co_ID " & st & " order by ID", "Stocks_Sales", Me)
        If myconn.cur.Count = 0 Then Return
        V1 = 0
        V2 = 0
        AM2 = 0
        For i As Integer = 0 To myconn.cur.Count - 1

            If myconn.cur.Current("Unit_Kind") = 0 Then
                Unit1 = myconn.cur.Current("Min_Unit_Name")
                AM2 = myconn.cur.Current("Amount") * myconn.cur.Current("Min_Unit_number")
            ElseIf myconn.cur.Current("Unit_Kind") = 1 Then
                Unit1 = myconn.cur.Current("Max_Unit_Name")
                AM2 = myconn.cur.Current("Amount") * 1
            End If
            drg_Mos.Rows.Add()
            drg_Mos.Rows(i).Cells(0).Value = i + 1
            drg_Mos.Rows(i).Cells(1).Value = myconn.cur.Current("Bill_Date")
            drg_Mos.Rows(i).Cells(2).Value = myconn.cur.Current("Time_Add")
            drg_Mos.Rows(i).Cells(3).Value = myconn.cur.Current("Co_Name")
            drg_Mos.Rows(i).Cells(4).Value = myconn.cur.Current("Drug_Name")
            drg_Mos.Rows(i).Cells(5).Value = myconn.cur.Current("Drug_ID")
            drg_Mos.Rows(i).Cells(6).Value = myconn.cur.Current("Drug_exp")
            drg_Mos.Rows(i).Cells(7).Value = Math.Round(AM2)
            drg_Mos.Rows(i).Cells(8).Value = Unit1
            drg_Mos.Rows(i).Cells(9).Value = myconn.cur.Current("Drug_Price")
            drg_Mos.Rows(i).Cells(10).Value = myconn.cur.Current("Total_Price")
            drg_Mos.Rows(i).Cells(11).Value = myconn.cur.Current("GroupName")
            drg_Mos.Rows(i).Cells(12).Value = myconn.cur.Current("Users")
            drg_Mos.Rows(i).Cells(13).Value = myconn.cur.Current("Employee")
            drg_Mos.Rows(i).Cells(14).Value = myconn.cur.Current("ID")
            drg_Mos.Rows(i).Cells(15).Value = myconn.cur.Current("State")

            If drg_Mos.Rows(i).Cells(15).Value = True Then
                drg_Mos.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                V1 += CDec(drg_Mos.Rows(i).Cells(10).Value)
            Else
                drg_Mos.Rows(i).DefaultCellStyle.BackColor = Color.Red
                V2 += CDec(drg_Mos.Rows(i).Cells(10).Value)
            End If
            myconn.cur.Position += 1
        Next
        Label129.Text = V1
        Label130.Text = "( " & clsNumber.nTOword(Label129.Text) & " )"
        Label130.Left = Label129.Left - (Label130.Width + 20)

        Label91.Text = V2
        Label107.Text = "( " & clsNumber.nTOword(Label91.Text) & " )"
        Label107.Left = Label8.Left - (Label107.Width + 20)

        myconn.DataGridview_MoveLast(drg_Mos, 7)
    End Sub
    Sub Binding_Stock() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        If Not fin Then Return
        Select Case B
            Case 0
                myconn.Filldataset("select a.Drug_ID,a.Drug_Name,a.Drug_Price ,a.Parcod,a.Min_Unit_number,d.Max_Unit_Name,e.Min_Unit_Name,isnull(b.Amount,0) as Drug_Purchases,
                                    (c.Drug_exp) as EXP_Sales,(b.Drug_exp) as EXP_Puer,isnull(c.Amount,0) as Sales,(isnull(b.Amount,0) - isnull(c.Amount,0)) as rest  from Drugs a
                                    left join (select Drug_ID,Drug_exp,sum(Amount) as Amount,state from [dbo].[Stocks_Purchases] GROUP BY Drug_ID,Drug_exp,state,Stock_ID  having state = 'true' and Stock_ID = " & StockID & ")b
                                    on a.Drug_ID = b.Drug_ID
                                    left join (select Drug_ID,Drug_exp,sum(Amount) as Amount,state from [dbo].[Stocks_Sales] GROUP BY Drug_ID,Drug_exp,state,Stock_ID  having state = 'true' and Stock_ID = " & StockID & ") c
                                    on a.Drug_ID = c.Drug_ID and b.Drug_exp=c.Drug_exp
                                    left join Max_Unit d on a.Max_UnitID=d.Max_UnitID
                                    left join Min_Unit e on a.Min_UnitID=e.Min_UnitID
                                    GROUP BY  a.Drug_ID,a.Drug_Name,b.Amount,c.Amount,a.Drug_Price,a.Parcod,c.Drug_exp,b.Drug_exp,a.Min_Unit_number,d.Max_Unit_Name,e.Min_Unit_Name
                                    having (isnull(b.Amount,0) - isnull(c.Amount,0)) >= 0 and a.Drug_ID =" & CInt(cbo_Drug.SelectedValue), "Stocks_Sales", Me)

                Dim Myfields() As String = {"Drug_Name", "Drug_Price", "Rest"}
                Dim Mytxt() As TextBox = {txtDrug, txtPublic_Price, txtStock_amount}
                myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)

                If myconn.cur.Current("rest") = 0 Then
                    txtStock_amount.BackColor = Color.Red
                Else
                    txtStock_amount.BackColor = Color.White
                    'MsgBox(" .. الصنف غير متوفر")

                End If

                drg_Exp.Rows.Clear()
                Amount = 0
                For i As Integer = 0 To myconn.cur.Count - 1
                    drg_Exp.Rows.Add()
                    drg_Exp.Rows(i).Cells(0).Value = i + 1
                    drg_Exp.Rows(i).Cells(1).Value = myconn.cur.Current("EXP_Puer")
                    drg_Exp.Rows(i).Cells(2).Value = myconn.cur.Current("rest")
                    Amount += myconn.cur.Current("rest")
                    myconn.cur.Position += 1
                Next

                Dim C As Double, B, E As Integer

                If Amount = 0 Then
                    txtStock_amount.Text = 0

                ElseIf Amount <> 0

                    A = Math.Round(Amount, 2)
                    B = Fix(A)
                    C = Math.Round((Val(A) - Val(B)), 2)
                    E = myconn.cur.Current("Min_Unit_number")

                    If B > 0 And C = 0 Then
                        txtStock_amount.Text = B & " " & myconn.cur.Current("Max_Unit_Name")
                    ElseIf B > 0 And C > 0
                        txtStock_amount.Text = B & " " & myconn.cur.Current("Max_Unit_Name") & " و " & Math.Round((C * E), 0) & " " & myconn.cur.Current("Min_Unit_Name")
                    ElseIf B = 0 And C > 0
                        txtStock_amount.Text = Math.Round((C * E), 0) & " " & myconn.cur.Current("Min_Unit_Name")

                    ElseIf B < 0 And C = 0
                        txtStock_amount.Text = B & " " & myconn.cur.Current("Max_Unit_Name")
                    ElseIf B < 0 And C < 0
                        txtStock_amount.Text = B & " " & myconn.cur.Current("Max_Unit_Name") & " و " & Math.Round((C * E), 0) & " " & myconn.cur.Current("Min_Unit_Name")
                    ElseIf B = 0 And C < 0
                        txtStock_amount.Text = Math.Round((C * E), 0) & " " & myconn.cur.Current("Min_Unit_Name")

                    End If

                End If

            Case 1
                myconn.Filldataset1("select a.Min_Unit_price,a.Min_UnitID,a.Max_UnitID,a.Min_Unit_price,a.Drug_Price,c.Min_Unit_Name,b.Max_Unit_Name,a.Min_Unit_number from Drugs a
                             left join Max_Unit b on a.Max_UnitID=b.Max_UnitID
                             left join Min_Unit c on a.Min_UnitID=c.Min_UnitID
                            where Drug_ID =" & CInt(cbo_Drug.SelectedValue), "Drugs", Me)
                Select Case cbo_Unit.SelectedIndex
                    Case 0
                        Dim Myfields() As String = {"Min_Unit_Name", "Min_Unit_price", "Min_UnitID", "Min_Unit_number"}
                        Dim Mytxt() As TextBox = {txtKind_Unit, txt_Drug_price, txtUnit_ID, txtUnit_Number}
                        TextBindingdata2(Me, GroupBox6, Myfields, Mytxt)
                    Case 1
                        Dim Myfields() As String = {"Max_Unit_Name", "Drug_Price", "Max_UnitID"}
                        Dim Mytxt() As TextBox = {txtKind_Unit, txt_Drug_price, txtUnit_ID}
                        TextBindingdata2(Me, GroupBox6, Myfields, Mytxt)
                        txtUnit_Number.Text = 1
                End Select
        End Select
    End Sub
    Sub TextBindingdata2(frm As Form, grb As GroupBox, Fields() As String, txt() As TextBox)

        For i As Integer = 0 To Fields.Count - 1
            txt(i).DataBindings.Clear()
            txt(i).DataBindings.Add("text", myconn.dv1, Fields(i))
        Next
    End Sub
    Sub Save_To_Stock()
        Dim sql As String = "INSERT INTO Stocks_Sales (Stock_ID,Bill_ID,Bill_Date,Time_Add,Drug_ID,Drug_exp,Drug_Price,Amount,Total_Price,Unit,Unit_Kind,EmployeeID,Patient_ID,DoctorsID,CerviceID,specializationID,RecordID,Users_ID,State)
                                                VALUES(@Stock_ID,@Bill_ID,@Bill_Date,@Time_Add,@Drug_ID,@Drug_exp,@Drug_Price,@Amount,@Total_Price,@Unit,@Unit_Kind,@EmployeeID,@Patient_ID,@DoctorsID,@CerviceID,@specializationID,@RecordID,@Users_ID,@State)"

        myconn.cmd = New SqlCommand(sql, myconn.conn)
        With myconn.cmd.Parameters
            .Add("@Stock_ID", SqlDbType.Int).Value = StockID
            .Add("@Bill_ID", SqlDbType.Int).Value = txt_Bill_ID.Text
            .Add("@Bill_Date", SqlDbType.NChar).Value = Format(CDate(txt_Date.Text), "yyyy/MM/dd")
            .Add("@Time_Add", SqlDbType.NChar).Value = Label59.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
            .Add("@Drug_exp", SqlDbType.NChar).Value = drg_Exp.CurrentRow.Cells(1).Value
            .Add("@Drug_Price", SqlDbType.Decimal).Value = txt_Drug_price.Text
            .Add("@Amount", SqlDbType.Decimal).Value = Math.Round((Val(txt_Amount_Drug.Text) / Val(txtUnit_Number.Text)), 2)
            .Add("@Unit", SqlDbType.Int).Value = txtUnit_ID.Text
            .Add("@Unit_Kind", SqlDbType.Int).Value = cbo_Unit.SelectedIndex
            .Add("@Total_Price", SqlDbType.Decimal).Value = Val(txt_Drug_price.Text) * Val(txt_Amount_Drug.Text)
            .Add("@EmployeeID", SqlDbType.Int).Value = cbo_Employees.SelectedValue
            .Add("@Patient_ID", SqlDbType.Int).Value = cboPatientName.SelectedValue
            .Add("@DoctorsID", SqlDbType.Int).Value = cboDoctor.SelectedValue
            .Add("@CerviceID", SqlDbType.Int).Value = cboCervice.SelectedValue
            .Add("@specializationID", SqlDbType.Int).Value = cboKissm.SelectedValue
            .Add("@RecordID", SqlDbType.Int).Value = txtRecordID.Text
            .Add("@Users_ID", SqlDbType.Int).Value = My.Settings.user_ID
            .Add("@State", SqlDbType.Bit).Value = 1
        End With
        Try
            If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
            myconn.conn.Open()
            myconn.cmd.ExecuteNonQuery()
            myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            'Return
        End Try
        B = 0
        Binding_Stock()

    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Select Case Y
            Case 0
                If drg_visits.Rows.Count = 0 Then Return
                myconn.Filldataset1("select * from Login_Patients where RecordID =" & CInt(drg_visits.CurrentRow.Cells(5).Value), "Login_Patients", Me)

                txtRecordID.DataBindings.Clear()
                txtRecordID.DataBindings.Add("text", myconn.dv1, "RecordID")

                txtVisitID.DataBindings.Clear()
                txtVisitID.DataBindings.Add("text", myconn.dv1, "VisitID")

                txtDoctor_price.DataBindings.Clear()
                txtDoctor_price.DataBindings.Add("text", myconn.dv1, "DoctorPrice")

                txtRoomPrice.DataBindings.Clear()
                txtRoomPrice.DataBindings.Add("text", myconn.dv1, "RoomPrice")

                dtp1.DataBindings.Clear()
                dtp1.DataBindings.Add("text", myconn.dv1, "Login_Date")

                cboKissm.DataBindings.Clear()
                cboKissm.DataBindings.Add("SelectedValue", myconn.dv1, "specializationID")

                cboPatientName.DataBindings.Clear()
                cboPatientName.DataBindings.Add("SelectedValue", myconn.dv1, "patient_ID")

                cboDoctor.DataBindings.Clear()
                cboDoctor.DataBindings.Add("SelectedValue", myconn.dv1, "DoctorsID")

                cboCervice.DataBindings.Clear()
                cboCervice.DataBindings.Add("SelectedValue", myconn.dv1, "CerviceID")

                cbo_Rooms.DataBindings.Clear()
                cbo_Rooms.DataBindings.Add("SelectedValue", myconn.dv1, "RoomNumber")

                cbo_Doctor2.DataBindings.Clear()
                cbo_Doctor2.DataBindings.Add("SelectedValue", myconn.dv1, "Doctor_trans")

                txt_Rate.DataBindings.Clear()
                txt_Rate.DataBindings.Add("text", myconn.dv1, "Doctors_rate")

        End Select
    End Sub
    Sub grbZL(grb As GroupBox, l As Integer, T As Integer, H As Integer, W As Integer, V As Boolean, lab As Label)
        For Each ctl As Control In GroupBox2.Controls
            If TypeOf ctl Is GroupBox Then
                If ctl.Text = "تسجيل دخول مريض  :" Or ctl.Text = "زيارات المريض  :" Or ctl.Text = "" Then
                    ctl.Visible = True
                Else
                    ctl.Visible = False
                End If

            End If
        Next ctl
        grb.Left = l
        grb.Top = T
        grb.Height = H
        grb.Width = W
        grb.Visible = V
        lab.Left = 1
        lab.Width = grb.Width - 3
        lab.Height = 5
        lab.Top = 38
    End Sub
    Sub drg_Location(drg As DataGridView, x As Integer, y As Integer, W As Integer, H As Integer)
        drg.Left = x
        drg.Top = y
        drg.Width = W
        drg.Height = H
    End Sub

#End Region

#Region "إجراءات النافذة"
    Private Sub frmInternal_section_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Start()
        btnSave.Enabled = False
        Me.KeyPreview = True
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label59.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
#End Region

#Region "تسجيل دخول مريض"
    Private Sub btnPatient_record_Click(sender As Object, e As EventArgs) Handles btnPatient_record.Click '>>>>>>>>>>>>>>>> تسجيل دخول المريض

        grbZL(grbPatientData, 568, 13, 331, 475, True, Label16)
        grbZL(grbPatientVisites, 568, 346, 262, 475, True, Label57)
        grbPatientData.Visible = True
        grbPatientVisites.Visible = True
        grbPatientData.Enabled = True
        grbPatientVisites.Enabled = True
        btnNew.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        btnDel.Enabled = True
        btnUpdat.Enabled = True
        drg_Location(drg_visits, 7, 18, 460, 236)
        myconn.Fillcombo1("select * from Doctors", "Doctors", "DoctorsID", "DoctorsName", Me, cbo_Doctor2)
        fin = False
        myconn.Fillcombo1("select * from Patient", "Patient", "patient_ID", "PatientName", Me, cboPatientName)
        fin = True

        fin = False
        myconn.Fillcombo("select * from specialization where kind ='k'", "specialization", "specializationID", "specialization", Me, cboKissm)
        fin = True

        fin = False
        myconn.Fillcombo2("select * from Rooms", "Rooms", "RoomNumber", "RoomNumber", Me, cbo_Rooms)
        fin = True
        X = 0
        fin = False
        Fillgrd()
        fin = True
        txtRecordID.Text = ""
        txtVisitID.Text = ""

    End Sub
    Private Sub cboPatientName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPatientName.SelectedIndexChanged
        ErrorProvider1.Clear()

        If Not fin Then Return
        txtPatientID.DataBindings.Clear()
        txtPatientID.DataBindings.Add("text", myconn.dv1, "patient_ID")

        txtPatientIDN.DataBindings.Clear()
        txtPatientIDN.DataBindings.Add("text", myconn.dv1, "National_ID")


        myconn.Filldataset("select  isnull(max(VisitID),0) as VisitID from Login_Patients  where patient_ID =" & cboPatientName.SelectedValue, "Login_Patients", Me)
        If myconn.dv.Count = 0 Then
            txtVisitID.Text = "1"
        Else
            txtVisitID.Text = (myconn.cur.Current("VisitID") + 1).ToString
        End If
        X = 0
        Fillgrd()
    End Sub

    Private Sub cboKissm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKissm.SelectedIndexChanged
        If Not fin Then Return
        If cboKissm.SelectedIndex = -1 Then Return
        myconn.Fillcombo("select * from Cervices where specializationID =" & cboKissm.SelectedValue, "Cervices", "CerviceID", "CerviceName", Me, cboCervice)
        myconn.Fillcombo("select * from Doctors where specializationID =" & cboKissm.SelectedValue, "Doctors", "DoctorsID", "DoctorsName", Me, cboDoctor)

        fin = True
    End Sub
    Private Sub cbo_Rooms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Rooms.SelectedIndexChanged
        If Not fin Then Return
        txtRoomPrice.DataBindings.Clear()
        txtRoomPrice.DataBindings.Add("text", myconn.dv2, "Price")
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        btnNew.Enabled = False
        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnDel.Enabled = False
        btnUpdat.Enabled = False
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtRecordID.Text = "" OrElse txtVisitID.Text = "" OrElse txtRoomPrice.Text = "" OrElse txtDoctor_price.Text = "" OrElse cboPatientName.SelectedIndex = -1 OrElse cboCervice.SelectedIndex = -1 OrElse cboDoctor.SelectedIndex = -1 OrElse cboKissm.SelectedIndex = -1 Then
            MessageBox.Show("أكمل البيانات المطلوبة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        Try
            Dim SQL As String = "INSERT INTO Login_Patients (RecordID,VisitID,patient_ID,Login_Date,Login_Time,National_ID,specializationID,DoctorsID,CerviceID,DoctorPrice,RoomNumber,RoomPrice,Out_Date,Out_Time,Doctor_trans,Doctors_rate)
                                                  values(@RecordID,@VisitID,@patient_ID,@Login_Date,@Login_Time,@National_ID,@specializationID,@DoctorsID,@CerviceID,@DoctorPrice,@RoomNumber,@RoomPrice,@Out_Date,@Out_Time,@Doctor_trans,@Doctors_rate) "

            With myconn.cmd.Parameters
                .AddWithValue("@RecordID", txtRecordID.Text)
                .AddWithValue("@VisitID", txtVisitID.Text)
                .AddWithValue("@patient_ID", cboPatientName.SelectedValue)
                .AddWithValue("@Login_Date", Format(CDate(dtp1.Text), "yyyy/MM/dd"))
                .AddWithValue("@Login_Time", Label59.Text)
                .AddWithValue("@National_ID", txtPatientIDN.Text)
                .AddWithValue("@specializationID", cboKissm.SelectedValue)
                .AddWithValue("@DoctorsID", cboDoctor.SelectedValue)
                .AddWithValue("@CerviceID", cboCervice.SelectedValue)
                .AddWithValue("@DoctorPrice", txtDoctor_price.Text)
                .AddWithValue("@RoomNumber", cbo_Rooms.SelectedValue)
                .AddWithValue("@RoomPrice", txtRoomPrice.Text)
                .AddWithValue("@Out_Date", DBNull.Value)
                .AddWithValue("@Out_Time", DBNull.Value)
                .AddWithValue("@Doctor_trans", If(cbo_Doctor2.SelectedIndex = -1, DBNull.Value, cbo_Doctor2.SelectedValue))
                .AddWithValue("@Doctors_rate", If(txt_Rate.Text = Nothing, DBNull.Value, txt_Rate.Text))
            End With
            If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
            myconn.conn.Open()
            myconn.cmd.ExecuteNonQuery()
            myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            Return
        End Try

        btnNew.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        btnDel.Enabled = True
        btnUpdat.Enabled = True
        X = 0
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        btnNew.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        btnDel.Enabled = True
        btnUpdat.Enabled = True
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Login_Patients", "RecordID", CInt(txtRecordID.Text))
            myconn.ClearAllText(Me, grbPatientData)
            X = 0

            Fillgrd()
        End If

        btnNew.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        btnDel.Enabled = True
        btnUpdat.Enabled = True
    End Sub
    Private Sub txtRecordID_TextChanged(sender As Object, e As EventArgs) Handles txtRecordID.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtVisitID_TextChanged(sender As Object, e As EventArgs) Handles txtVisitID.TextChanged
        ErrorProvider1.Clear()
    End Sub
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            myconn.Filldataset5("select * from patient where patient_ID =" & CInt(txtSearch.Text), "patient", Me)


            If myconn.dv5.Count = 0 Then
                MessageBox.Show("السجل المطلوب غير موجود", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If
        Catch ex As Exception
            Return
        End Try
        cboPatientName.DataBindings.Clear()
        cboPatientName.DataBindings.Add("SelectedValue", myconn.dv5, "patient_ID")
    End Sub

#End Region

#Region "زيارات المريض"
    Private Sub drg_visits_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_visits.CellClick '>>>>>>>>>>>>> زيارات المريض 
        btnNew.Enabled = True
        btnSave.Enabled = False
        btnCancel.Enabled = False
        btnDel.Enabled = True
        btnUpdat.Enabled = True
        Y = 0
        Binding()
        If grbX_ray.Visible = True Then
            X = 1
            Fillgrd()
        ElseIf grbAnalysis.Visible = True Then
            X = 2
            Fillgrd()
        ElseIf grbOperation_tools.Visible = True Then
            X = 3
            Fillgrd()
        ElseIf grbCare.Visible = True Then
            X = 4
            Fillgrd()
        ElseIf grbO2.Visible = True Then
            X = 5
            Fillgrd()
        ElseIf grbDrug.Visible = True Then
            X = 6
            Fillgrd()
        ElseIf grbNarcosis.Visible = True Then
            X = 7
            Fillgrd()
        ElseIf grbBill_Adds.Visible = True Then
            X = 8
            Fillgrd()
        End If
    End Sub

#End Region

#Region "الأشعات"
    Private Sub btnXray_Click(sender As Object, e As EventArgs) Handles btnXray.Click   ' >>>>>>>>>>>>>>>>>>>>>>> الأشعة
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If
        drg_Xray.Rows.Clear()

        grbZL(grbX_ray, 8, 13, 596, 554, True, Label17)
        drg_Location(drg_Xray, 5, 165, 543, 425)
        grbPatientData.Enabled = False
        grbPatientVisites.Enabled = False
        btnNew_Xray.Enabled = True
        btnSave_Xray.Enabled = False
        fin = False
        myconn.Fillcombo3("select * from Cervices where specializationID = 2", "Cervices", "CerviceID", "CerviceName", Me, cbo_Xray)
        myconn.Fillcombo4("select * from Doctors ", "Doctors", "DoctorsID", "DoctorsName", Me, cbo_Doctor_Xray_Trans)
        fin = True
        X = 1
        fin = False
        Fillgrd()
        fin = True
    End Sub
    Private Sub cbo_Xray_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Xray.SelectedIndexChanged, cbo_Doctor_Xray_Trans.SelectedIndexChanged
        If Not fin Then Return
        txtXrayPrice.DataBindings.Clear()
        txtXrayPrice.DataBindings.Add("text", myconn.dv3, "Cervice_Price")
    End Sub
    Private Sub btnNew_Xray_Click(sender As Object, e As EventArgs) Handles btnNew_Xray.Click
        btnSave_Xray.Enabled = True
        btnNew_Xray.Enabled = False
        btnDelet_Xray.Enabled = False
        btnUpdat_Xray.Enabled = False
        txtXrayPrice.Text = ""
        txtXray_Rate.Text = ""
        txtXray_Rate.Text = ""
        cbo_Doctor_Xray_Trans.SelectedIndex = -1
        cbo_Xray.SelectedIndex = -1
    End Sub
    Private Sub btnXray_Cancel_Click(sender As Object, e As EventArgs) Handles btnXray_Cancel.Click
        btnSave_Xray.Enabled = False
        btnNew_Xray.Enabled = True
        btnDelet_Xray.Enabled = True
        btnUpdat_Xray.Enabled = True
        txtXrayPrice.Text = ""
        txtXray_Rate.Text = ""
        txtXray_Rate.Text = ""
        cbo_Doctor_Xray_Trans.SelectedIndex = -1
        cbo_Xray.SelectedIndex = -1
    End Sub
    Private Sub btnSave_Xray_Click(sender As Object, e As EventArgs) Handles btnSave_Xray.Click
        Dim d As String
        myconn.Filldataset5("select * from Patient_X_rays ", "Patient_X_rays", Me)

        If txtRecordID.Text = "" OrElse txtVisitID.Text = "" OrElse txtRecordID.Text = "" OrElse txtXrayPrice.Text = "" OrElse txtPatientID.Text = "" OrElse cbo_Xray.SelectedIndex = -1 Then
            MessageBox.Show("أكمل البيانات المطلوبة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        If cbo_Doctor_Xray_Trans.SelectedIndex = -1 Then
            d = "NULL"
            txtXray_Rate.Text = "NULL"
            txtXray_Rate_Value.Text = "NULL"
        Else
            d = cbo_Doctor_Xray_Trans.SelectedValue
        End If
        Dim XX() As String = {txtPatientID.Text, txtVisitID.Text, cbo_Xray.SelectedValue, txtXrayPrice.Text, "'" & Format(CDate(dtp1.Text), "yyyy/MM/dd").ToString & "'", txtRecordID.Text, d, txtXray_Rate.Text, txtXray_Rate_Value.Text}
        myconn.AddNewRecord("Patient_X_rays", XX)
        btnSave_Xray.Enabled = False
        btnNew_Xray.Enabled = True
        txtXray_Rate.Text = ""
        txtXray_Rate_Value.Text = ""
        X = 1
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnDelet_Xray_Click(sender As Object, e As EventArgs) Handles btnDelet_Xray.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Patient_X_rays", "ID", CInt(drg_Xray.CurrentRow.Cells(5).Value))
            myconn.ClearAllText(Me, grbX_ray)
            X = 1

            Fillgrd()
        End If
    End Sub
    Private Sub btnUpdat_Xray_Click(sender As Object, e As EventArgs) Handles btnUpdat_Xray.Click
        Dim Values() As String = {cbo_Xray.SelectedValue, "'" & txtXrayPrice.Text & "'"}
        Dim Mycolumes() As String = {"XrayID", "X_ray_Price"}
        myconn.UpdateRecord("Patient_X_rays", Mycolumes, Values, "ID", CInt(drg_Xray.CurrentRow.Cells(5).Value))
        X = 1
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub txtXray_Rate_TextChanged(sender As Object, e As EventArgs) Handles txtXray_Rate.TextChanged
        txtXray_Rate_Value.Text = Math.Round(Val(txtXrayPrice.Text) * Val(Val(txtXray_Rate.Text) / 100), 2)
    End Sub
    Private Sub drg_Xray_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_Xray.CellClick
        myconn.Filldataset6("select * from Patient_X_rays where ID =" & CInt(drg_Xray.CurrentRow.Cells(5).Value), "Patient_X_rays", Me)
        txtXrayPrice.DataBindings.Clear()
        txtXrayPrice.DataBindings.Add("text", myconn.dv6, "X_ray_Price")
        txtXray_Rate.DataBindings.Clear()
        txtXray_Rate.DataBindings.Add("text", myconn.dv6, "Dorctors_Rate")
        txtXray_Rate_Value.DataBindings.Clear()
        txtXray_Rate_Value.DataBindings.Add("text", myconn.dv6, "Doctors_Value")
        cbo_Xray.DataBindings.Clear()
        cbo_Xray.DataBindings.Add("selectedvalue", myconn.dv6, "XrayID")
        cbo_Doctor_Xray_Trans.DataBindings.Clear()
        cbo_Doctor_Xray_Trans.DataBindings.Add("selectedvalue", myconn.dv6, "DoctorsID")
    End Sub
#End Region

#Region "التحاليل"
    Private Sub btnAnalysis_Click(sender As Object, e As EventArgs) Handles btnAnalysis.Click  ' >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> التحاليل

        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If
        drg_Analysis.Rows.Clear()

        grbZL(grbAnalysis, 8, 13, 596, 554, True, Label15)
        drg_Location(drg_Analysis, 5, 165, 543, 425)
        grbPatientData.Enabled = False
        grbPatientVisites.Enabled = False
        btnNew_Analysis.Enabled = True
        btnSave_Analysis.Enabled = False
        btnAnalysis_Cancel.Enabled = False

        fin = False
        myconn.Fillcombo6("select * from Cervices where specializationID = 1", "Cervices", "CerviceID", "CerviceName", Me, cboAnalysis)
        myconn.Fillcombo7("select * from Doctors ", "Doctors", "DoctorsID", "DoctorsName", Me, cbo_Doctor_Analysis_Trans)
        fin = True

        X = 2
        fin = False
        Fillgrd()
        fin = True
    End Sub
    Private Sub cboAnalysis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAnalysis.SelectedIndexChanged, cbo_Doctor_Analysis_Trans.SelectedIndexChanged
        If Not fin Then Return
        txtAnalysisPrice.DataBindings.Clear()
        txtAnalysisPrice.DataBindings.Add("text", myconn.dv6, "Cervice_Price")
    End Sub
    Private Sub btnNew_Analysis_Click(sender As Object, e As EventArgs) Handles btnNew_Analysis.Click
        btnSave_Analysis.Enabled = True
        btnNew_Analysis.Enabled = False
        btnUpdat_Analysis.Enabled = False
        btnAnalysis_Cancel.Enabled = True
        btnDelet_Analysis.Enabled = False
        txtAnalysisPrice.Text = ""
        txtAnalysis_Rate.Text = ""
        txtAnalysis_Rate_Value.Text = ""
        cbo_Doctor_Analysis_Trans.SelectedIndex = -1
        cboAnalysis.SelectedIndex = -1

    End Sub
    Private Sub btnSave_Analysis_Click(sender As Object, e As EventArgs) Handles btnSave_Analysis.Click
        Dim d As String
        myconn.Filldataset5("select * from Patient_Analysis ", "Patient_Analysis", Me)

        If txtRecordID.Text = "" OrElse txtVisitID.Text = "" OrElse txtRecordID.Text = "" OrElse txtAnalysisPrice.Text = "" OrElse txtPatientID.Text = "" OrElse cboAnalysis.SelectedIndex = -1 Then
            MessageBox.Show("أكمل البيانات المطلوبة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        If cbo_Doctor_Analysis_Trans.SelectedIndex = -1 Then
            d = "NULL"
            txtAnalysis_Rate.Text = "NULL"
            txtAnalysis_Rate_Value.Text = "NULL"
        Else
            d = cbo_Doctor_Analysis_Trans.SelectedValue
        End If
        Dim XX() As String = {txtPatientID.Text, txtVisitID.Text, cboAnalysis.SelectedValue, txtAnalysisPrice.Text, "'" & Format(CDate(dtp1.Text), "yyyy/MM/dd").ToString & "'", txtRecordID.Text, d, txtAnalysis_Rate.Text, txtAnalysis_Rate_Value.Text}
        myconn.AddNewRecord("Patient_Analysis", XX)
        btnSave_Analysis.Enabled = False
        btnAnalysis_Cancel.Enabled = False
        btnUpdat_Analysis.Enabled = True
        btnDelet_Analysis.Enabled = True
        btnNew_Analysis.Enabled = True
        X = 2
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnDelet_Analysis_Click(sender As Object, e As EventArgs) Handles btnDelet_Analysis.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Patient_Analysis", "ID", CInt(drg_Analysis.CurrentRow.Cells(5).Value))
            myconn.ClearAllText(Me, grbAnalysis)
            X = 2

            Fillgrd()
        End If
    End Sub
    Private Sub btnUpdat_Analysis_Click(sender As Object, e As EventArgs) Handles btnUpdat_Analysis.Click
        Dim Values() As String = {cboAnalysis.SelectedValue, "'" & txtAnalysisPrice.Text & "'"}
        Dim Mycolumes() As String = {"AnalysisID", "Analysis_Price"}
        myconn.UpdateRecord("Patient_Analysis", Mycolumes, Values, "ID", CInt(drg_Analysis.CurrentRow.Cells(5).Value))
        X = 2
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub txtAnalysis_Rate_TextChanged(sender As Object, e As EventArgs) Handles txtAnalysis_Rate.TextChanged
        txtAnalysis_Rate_Value.Text = Math.Round(Val(txtAnalysisPrice.Text) * Val(Val(txtAnalysis_Rate.Text) / 100), 2)
    End Sub
    Private Sub drg_Analysis_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_Analysis.CellClick
        myconn.Filldataset8("select * from Patient_Analysis where ID =" & CInt(drg_Analysis.CurrentRow.Cells(5).Value), "Patient_Analysis", Me)
        txtAnalysisPrice.DataBindings.Clear()
        txtAnalysisPrice.DataBindings.Add("text", myconn.dv8, "Analysis_Price")
        txtAnalysis_Rate.DataBindings.Clear()
        txtAnalysis_Rate.DataBindings.Add("text", myconn.dv8, "Dorctors_Rate")
        txtAnalysis_Rate_Value.DataBindings.Clear()
        txtAnalysis_Rate_Value.DataBindings.Add("text", myconn.dv8, "Doctors_Value")
        cboAnalysis.DataBindings.Clear()
        cboAnalysis.DataBindings.Add("selectedvalue", myconn.dv8, "AnalysisID")
        cbo_Doctor_Analysis_Trans.DataBindings.Clear()
        cbo_Doctor_Analysis_Trans.DataBindings.Add("selectedvalue", myconn.dv8, "DoctorsID")
    End Sub
    Private Sub btnAnalysis_Cancel_Click(sender As Object, e As EventArgs) Handles btnAnalysis_Cancel.Click
        btnSave_Analysis.Enabled = False
        btnAnalysis_Cancel.Enabled = False
        btnNew_Analysis.Enabled = True
        btnDelet_Analysis.Enabled = True
        btnUpdat_Analysis.Enabled = True
        txtAnalysisPrice.Text = ""
        txtAnalysis_Rate.Text = ""
        txtAnalysis_Rate_Value.Text = ""
        cbo_Doctor_Analysis_Trans.SelectedIndex = -1
        cboAnalysis.SelectedIndex = -1
    End Sub
#End Region

#Region "أجهزة العمليات"
    Private Sub btnOperation_tools_Click(sender As Object, e As EventArgs) Handles btnOperation_tools.Click  ' >>>>>>>>>>>>>> استخدام أجهزة عمليات 
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If
        drg_Ope_tools.Rows.Clear()

        grbZL(grbOperation_tools, 8, 13, 596, 554, True, Label18)
        drg_Location(drg_Ope_tools, 5, 105, 543, 485)
        grbPatientData.Enabled = False
        grbPatientVisites.Enabled = False
        btnNew_Operation.Enabled = True
        btnSave_Operation.Enabled = False
        btnOpreation_Cancel.Enabled = False
        fin = False
        myconn.Fillcombo7("select * from Opreation_Tools", "Opreation_Tools", "Operation_Tool_ID", "Operation_Tool_Name", Me, cbo_Operation)
        fin = True
        If txtRecordID.Text = "" Then Return
        X = 3
        fin = False
        Fillgrd()
        fin = True
    End Sub
    Private Sub btnNew_Operation_Click(sender As Object, e As EventArgs) Handles btnNew_Operation.Click
        btnNew_Operation.Enabled = False
        btnSave_Operation.Enabled = True
        btnOpreation_Cancel.Enabled = True
        btnDelet_Operation.Enabled = False
        btnUpdat_Operation.Enabled = False
        txtOperation.Text = ""
        cbo_Operation.SelectedIndex = -1
        txtOperation.Text = ""
    End Sub
    Private Sub btnSave_Operation_Click(sender As Object, e As EventArgs) Handles btnSave_Operation.Click
        myconn.Filldataset6("select * from Patient_Operation_Tools ", "Patient_Operation_Tools", Me)
        If txtRecordID.Text = "" OrElse txtVisitID.Text = "" OrElse txtOperation.Text = "" OrElse txtPatientID.Text = "" OrElse cbo_Operation.SelectedIndex = -1 Then
            MessageBox.Show("أكمل البيانات المطلوبة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        Dim XX() As String = {txtPatientID.Text, txtVisitID.Text, cbo_Operation.SelectedValue, txtOperation.Text, "'" & Format(CDate(dtp1.Text), "yyyy/MM/dd").ToString & "'", txtRecordID.Text}
        myconn.AddNewRecord("Patient_Operation_Tools", XX)
        btnSave_Operation.Enabled = False
        btnOpreation_Cancel.Enabled = False
        btnNew_Operation.Enabled = True
        btnDelet_Operation.Enabled = True
        btnUpdat_Operation.Enabled = True
        X = 3
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnDelet_Operation_Click(sender As Object, e As EventArgs) Handles btnDelet_Operation.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Patient_Operation_Tools", "ID", CInt(drg_Analysis.CurrentRow.Cells(5).Value))
            myconn.ClearAllText(Me, grbOperation_tools)
            X = 3
            Fillgrd()
        End If
    End Sub
    Private Sub btnUpdat_Operation_Click(sender As Object, e As EventArgs) Handles btnUpdat_Operation.Click
        Dim Values() As String = {cbo_Operation.SelectedValue, "'" & txtOperation.Text & "'"}
        Dim Mycolumes() As String = {"Operation_Tool_ID", "Operation_Tool_Price"}
        myconn.UpdateRecord("Patient_Operation_Tools", Mycolumes, Values, "ID", CInt(drg_Analysis.CurrentRow.Cells(5).Value))
        X = 3
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub cbo_Operation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Operation.SelectedIndexChanged
        If Not fin Then Return
        txtOperation.DataBindings.Clear()
        txtOperation.DataBindings.Add("text", myconn.dv7, "Operation_Tool_Price")
    End Sub
    Private Sub btnOpreation_Cancel_Click(sender As Object, e As EventArgs) Handles btnOpreation_Cancel.Click
        btnOpreation_Cancel.Enabled = False
        btnSave_Operation.Enabled = False
        btnNew_Operation.Enabled = True
        btnDelet_Operation.Enabled = True
        btnUpdat_Operation.Enabled = True
        txtOperation.Text = ""
        cbo_Operation.SelectedIndex = -1
    End Sub
    Private Sub drg_Ope_tools_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_Ope_tools.CellClick
        myconn.Filldataset6("select * from Patient_Operation_Tools where ID =" & CInt(drg_Ope_tools.CurrentRow.Cells(5).Value), "Patient_Operation_Tools", Me)
        txtOperation.DataBindings.Clear()
        txtOperation.DataBindings.Add("text", myconn.dv6, "Operation_Tool_Price")
        cbo_Operation.DataBindings.Clear()
        cbo_Operation.DataBindings.Add("selectedvalue", myconn.dv6, "Operation_Tool_ID")
    End Sub
#End Region

#Region "العناية"
    Private Sub btnCare_tools_Click(sender As Object, e As EventArgs) Handles btnCare_tools.Click  ' >>>>>>>>>>>>>>>>>> استخدام أجهزة عناية 
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If
        drg_Care_tools.Rows.Clear()

        grbZL(grbCare, 8, 13, 596, 554, True, Label21)
        drg_Location(drg_Care_tools, 5, 105, 543, 485)
        grbPatientData.Enabled = False
        grbPatientVisites.Enabled = False
        btnNew_Car.Enabled = True
        btnSave_Car.Enabled = False
        btnCancel_Car.Enabled = False
        btnDelet_Car.Enabled = True
        btnUpdat_Car.Enabled = True
        cboCervice.SelectedIndex = -1

        fin = False
        myconn.Fillcombo6("select * from Car_Tools", "Car_Tools", "Car_Tool_ID", "Car_Tool_Name", Me, cboCar)
        fin = True
        If txtRecordID.Text = "" Then Return
        X = 4
        fin = False
        Fillgrd()
        fin = True
    End Sub
    Private Sub cboCar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCar.SelectedIndexChanged
        If Not fin Then Return
        txtCar_Price.DataBindings.Clear()
        txtCar_Price.DataBindings.Add("text", myconn.dv6, "Car_Tool_Price")
    End Sub
    Private Sub btnNew_Car_Click(sender As Object, e As EventArgs) Handles btnNew_Car.Click
        btnSave_Car.Enabled = True
        btnNew_Car.Enabled = False
        btnCancel_Car.Enabled = True
        btnDelet_Car.Enabled = False
        btnUpdat_Car.Enabled = False
        txtCar_Price.Text = ""
        cboCervice.SelectedIndex = -1
    End Sub
    Private Sub btnSave_Car_Click(sender As Object, e As EventArgs) Handles btnSave_Car.Click
        myconn.Filldataset7("select * from Patient_car_Tools ", "Patient_car_Tools", Me)

        If txtRecordID.Text = "" OrElse txtVisitID.Text = "" OrElse txtCar_Price.Text = "" OrElse txtPatientID.Text = "" OrElse cboCar.SelectedIndex = -1 Then
            MessageBox.Show("أكمل البيانات المطلوبة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        Dim XX() As String = {txtPatientID.Text, txtVisitID.Text, cboCar.SelectedValue, txtCar_Price.Text, "'" & Format(CDate(dtp1.Text), "yyyy/MM/dd").ToString & "'", txtRecordID.Text}
        myconn.AddNewRecord("Patient_Car_Tools", XX)
        btnSave_Car.Enabled = False
        btnNew_Car.Enabled = True
        btnCancel_Car.Enabled = False
        btnDelet_Car.Enabled = True
        btnUpdat_Car.Enabled = True
        X = 4
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnUpdat_Car_Click(sender As Object, e As EventArgs) Handles btnUpdat_Car.Click
        Dim Values() As String = {cboCar.SelectedValue, "'" & txtCar_Price.Text & "'"}
        Dim Mycolumes() As String = {"Car_Tool_ID", "Car_Tool_Price"}
        myconn.UpdateRecord("Patient_Car_Tools", Mycolumes, Values, "ID", CInt(drg_Care_tools.CurrentRow.Cells(5).Value))
        X = 4
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub btnDelet_Car_Click(sender As Object, e As EventArgs) Handles btnDelet_Car.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Patient_Car_Tools", "ID", CInt(drg_Care_tools.CurrentRow.Cells(5).Value))
            myconn.ClearAllText(Me, grbOperation_tools)
            X = 4

            Fillgrd()
        End If
    End Sub
    Private Sub btnCancel_Car_Click(sender As Object, e As EventArgs) Handles btnCancel_Car.Click
        btnSave_Car.Enabled = False
        btnNew_Car.Enabled = True
        btnCancel_Car.Enabled = False
        btnDelet_Car.Enabled = True
        btnUpdat_Car.Enabled = True
        txtCar_Price.Text = ""
        cboCervice.SelectedIndex = -1
    End Sub
    Private Sub drg_Care_tools_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_Care_tools.CellClick
        myconn.Filldataset7("select * from Patient_Car_Tools where ID =" & CInt(drg_Care_tools.CurrentRow.Cells(5).Value), "Patient_Car_Tools", Me)
        txtCar_Price.DataBindings.Clear()
        txtCar_Price.DataBindings.Add("text", myconn.dv7, "Car_Tool_Price")
        cboCar.DataBindings.Clear()
        cboCar.DataBindings.Add("selectedvalue", myconn.dv7, "Car_Tool_ID")
    End Sub
#End Region

#Region "الأكسجين"
    Private Sub btnO2_Click(sender As Object, e As EventArgs) Handles btnO2.Click  ' >>>>>>>>>>>>>>>>>>>> استهلاك أكسجين
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If
        drg_O2.Rows.Clear()

        grbZL(grbO2, 8, 13, 596, 554, True, Label24)
        drg_Location(drg_O2, 5, 105, 543, 485)
        grbPatientData.Enabled = False
        grbPatientVisites.Enabled = False
        btnNew_O2.Enabled = True
        btnSave_o2.Enabled = False
        btnUpdat_O2.Enabled = True
        btnDelet_O2.Enabled = True
        btnCancel_O2.Enabled = False
        If txtRecordID.Text = "" Then Return
        X = 5
        fin = False
        Fillgrd()
        fin = True
    End Sub
    Private Sub btnNew_O2_Click(sender As Object, e As EventArgs) Handles btnNew_O2.Click
        btnNew_O2.Enabled = False
        btnDelet_O2.Enabled = False
        btnUpdat_O2.Enabled = False
        btnSave_o2.Enabled = True
        btnCancel_O2.Enabled = True
        txtO2.Text = ""
        txtPrice_O2.Text = ""
    End Sub
    Private Sub btnSave_o2_Click(sender As Object, e As EventArgs) Handles btnSave_o2.Click
        myconn.Filldataset7("select * from Patient_O2 ", "Patient_O2", Me)

        If txtRecordID.Text = "" OrElse txtVisitID.Text = "" OrElse txtPrice_O2.Text = "" OrElse txtPatientID.Text = "" OrElse txtO2.Text = "" Then
            MessageBox.Show("أكمل البيانات المطلوبة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        Dim XX() As String = {txtPatientID.Text, txtVisitID.Text, "'" & txtO2.Text & "'", txtPrice_O2.Text, "'" & Format(CDate(dtp1.Text), "yyyy/MM/dd").ToString & "'", txtRecordID.Text}
        myconn.AddNewRecord("Patient_O2", XX)
        btnSave_o2.Enabled = False
        btnCancel_O2.Enabled = False
        btnUpdat_O2.Enabled = True
        btnDelet_O2.Enabled = True
        btnNew_O2.Enabled = True
        txtO2.Text = ""
        txtPrice_O2.Text = ""
        X = 5
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnUpdat_O2_Click(sender As Object, e As EventArgs) Handles btnUpdat_O2.Click
        Dim Values() As String = {"'" & txtO2.Text & "'", "'" & txtPrice_O2.Text & "'"}
        Dim Mycolumes() As String = {"O2_Amount", "O2_Price"}
        myconn.UpdateRecord("Patient_O2", Mycolumes, Values, "ID", CInt(drg_O2.CurrentRow.Cells(4).Value))
        X = 5
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub btnDelet_O2_Click(sender As Object, e As EventArgs) Handles btnDelet_O2.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Patient_O2", "ID", CInt(drg_O2.CurrentRow.Cells(4).Value))
            myconn.ClearAllText(Me, grbO2)
            X = 5
            Fillgrd()
        End If
    End Sub
    Private Sub btnCancel_O2_Click(sender As Object, e As EventArgs) Handles btnCancel_O2.Click
        btnSave_o2.Enabled = False
        btnCancel_O2.Enabled = False
        btnUpdat_O2.Enabled = True
        btnDelet_O2.Enabled = True
        btnNew_O2.Enabled = True
        txtO2.Text = ""
        txtPrice_O2.Text = ""
    End Sub
    Private Sub drg_O2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_O2.CellClick
        myconn.Filldataset7("select * from Patient_O2 where ID =" & CInt(drg_O2.CurrentRow.Cells(4).Value), "Patient_O2", Me)
        txtO2.DataBindings.Clear()
        txtO2.DataBindings.Add("text", myconn.dv7, "O2_Amount")
        txtPrice_O2.DataBindings.Clear()
        txtPrice_O2.DataBindings.Add("text", myconn.dv7, "O2_Price")
    End Sub


#End Region

#Region "المساعدين"
    Private Sub btnDrugs_Click(sender As Object, e As EventArgs) Handles btnDrugs.Click  ' >>>>>>>>>>>>>>>>>>> استهلاك أدوية من الصيدلية
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If
        drg_Drugs.Rows.Clear()

        'frmInternal_section_Leave(Nothing, Nothing)
        grbZL(grbDrug, 8, 13, 596, 554, True, Label27)
        drg_Location(drg_Drugs, 5, 130, 543, 460)
        grbPatientData.Enabled = False
        grbPatientVisites.Enabled = False
        'myconn.Fillcombo6("select * from Drugs", "Drugs", "Drug_ID", "Drug_Name", Me, cboDrug)
        btnNew_Drug.Enabled = True
        btnSave_Drug.Enabled = False
        btnCancel_Drug.Enabled = False
        btnDelet_Drug.Enabled = True
        btnUpdat_Drug.Enabled = True
        X = 6
        Fillgrd()


    End Sub
    Private Sub cboHelper_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboHelper.SelectedIndexChanged
        If cboHelper.SelectedIndex < 3 Then
            myconn.Fillcombo7("select * from Doctors", "Doctors", "DoctorsID", "DoctorsName", Me, cboHelper_name)
        Else
            myconn.Fillcombo7("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cboHelper_name)
        End If
    End Sub
    Private Sub btnNew_Drug_Click(sender As Object, e As EventArgs) Handles btnNew_Drug.Click
        btnNew_Drug.Enabled = False
        btnSave_Drug.Enabled = True
        btnCancel_Drug.Enabled = True
        btnDelet_Drug.Enabled = False
        btnUpdat_Drug.Enabled = False
        'cboHelper.SelectedIndex = -1

    End Sub
    Private Sub btnSave_Drug_Click(sender As Object, e As EventArgs) Handles btnSave_Drug.Click
        If txtRecordID.Text = "" OrElse txtVisitID.Text = "" OrElse txtPatientID.Text = "" OrElse cboHelper.SelectedIndex = -1 OrElse cboHelper_name.SelectedIndex = -1 Then
            MessageBox.Show("أكمل البيانات المطلوبة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        Dim sql As String = "INSERT INTO Patient_helper(RecordID,Invoice_Date,patient_ID,VisitID,label,DoctorID,EmployeeID)
                                             VALUES(@RecordID,@Invoice_Date,@patient_ID,@VisitID,@label,@DoctorID,@EmployeeID)"

        Dim DC, EM As Integer
        If cboHelper.SelectedIndex < 3 Then
            DC = cboHelper_name.SelectedValue
            EM = Nothing
        Else
            DC = Nothing
            EM = cboHelper_name.SelectedValue
        End If

        myconn.cmd = New SqlCommand(sql, myconn.conn)
        With myconn.cmd.Parameters
            .Add("@RecordID", SqlDbType.Int).Value = txtRecordID.Text
            .Add("@Invoice_Date", SqlDbType.NChar).Value = Format(CDate(dtp1.Text), "yyyy/MM/dd").ToString
            .Add("@patient_ID", SqlDbType.Int).Value = txtPatientID.Text
            .Add("@VisitID", SqlDbType.Int).Value = txtVisitID.Text
            .Add("@label", SqlDbType.NVarChar).Value = cboHelper.Text
            .Add("@DoctorID", SqlDbType.Int).Value = DC
            .Add("@EmployeeID", SqlDbType.Int).Value = EM
        End With
        Try
            If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
            myconn.conn.Open()
            myconn.cmd.ExecuteNonQuery()
            myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            Return
        End Try

        btnNew_Drug.Enabled = True
        btnSave_Drug.Enabled = False
        btnCancel_Drug.Enabled = False
        btnDelet_Drug.Enabled = True
        btnUpdat_Drug.Enabled = True

        X = 6
        Fillgrd()

        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnUpdat_Drug_Click(sender As Object, e As EventArgs) Handles btnUpdat_Drug.Click
        Dim Values() As String = {txtRecordID.Text, "'" & Format(CDate(dtp1.Text), "yyyy/MM/dd").ToString & "'", txtPatientID.Text, txtVisitID.Text, cboHelper.SelectedValue}
        Dim Mycolumes() As String = {"RecordID", "Invoice_Date", "patient_ID", "VisitID", "Drug_ID", "Drug_Price", "Drug_amount", "Total_Price"}
        myconn.UpdateRecord("Patient_Drugs", Mycolumes, Values, "ID", CInt(drg_Drugs.CurrentRow.Cells(7).Value))
        X = 6
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)

    End Sub
    Private Sub btnDelet_Drug_Click(sender As Object, e As EventArgs) Handles btnDelet_Drug.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Patient_helper", "ID", CInt(drg_Drugs.CurrentRow.Cells(4).Value))
            myconn.ClearAllText(Me, grbDrug)
            X = 6
            Fillgrd()
        End If
    End Sub
    Private Sub btnCancel_Drug_Click(sender As Object, e As EventArgs) Handles btnCancel_Drug.Click
        btnNew_Drug.Enabled = True
        btnSave_Drug.Enabled = False
        btnCancel_Drug.Enabled = False
        btnDelet_Drug.Enabled = True
        btnUpdat_Drug.Enabled = True
        cboHelper.SelectedIndex = -1

    End Sub
    Private Sub drg_Drugs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_Drugs.CellClick
        myconn.Filldataset7("select * from Patient_helper where ID =" & CInt(drg_Drugs.CurrentRow.Cells(4).Value), "Patient_helper", Me)
    End Sub
    Private Sub cboDrug_KeyUp(sender As Object, e As KeyEventArgs) Handles cboHelper.KeyUp, cboHelper_name.KeyUp
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancel_Drug_Click(Nothing, Nothing)
            cboHelper.Focus()

        End If
    End Sub

    Private Sub cboDrug_Enter(sender As Object, e As EventArgs) Handles cboHelper.Enter, cboHelper_name.Enter
        myconn.langAR()
    End Sub

#End Region

#Region "مستهلك العمليات"
    Private Sub btnOpertion_Click(sender As Object, e As EventArgs) Handles btnOpertion.Click  ' >>>>>>>>>>>>>>>>>>>>> مستهلك عمليات
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If

        For Each ctl As Control In GroupBox2.Controls
            If TypeOf ctl Is GroupBox Then
                If ctl.Text = "" Then
                    ctl.Visible = True
                Else
                    ctl.Visible = False
                End If

            End If
        Next ctl
        StockID = 2
        grbZL(grbOperations_Drage, 7, 12, 596, 1036, True, Label89)
        drg_Location(drg_Mos, 6, 19, 1010, 276)
        grbOperations_Drage.Text = "مستهلك عمليات  :  "
        grbPatientData.Visible = False
        grbPatientVisites.Visible = False
        fin = False
        myconn.Fillcombo6("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cbo_Employees)
        myconn.Fillcombo6("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug)
        fin = True
        Label129.Text = 0
        Label130.Text = clsNumber.nTOword(Label129.Text)
        Label91.Text = 0
        Label107.Text = clsNumber.nTOword(Label91.Text)
        V = 1
        Fillgrd_Stock()
        If drg_Mos.Rows.Count = 0 Then
            myconn.Filldataset7("select  isnull(max(Bill_ID),0) as Bill_ID from Stocks_Sales where Stock_ID =" & CInt(StockID), "Stocks_Purchases", Me)
            txt_Bill_ID.Text = myconn.cur7.Current("Bill_ID") + 1
            GroupBox5.Enabled = True
        Else
            txt_Bill_ID.Text = myconn.cur.Current("Bill_ID")
            cbo_Employees.SelectedValue = myconn.cur.Current("EmployeeID")
            GroupBox5.Enabled = False
            Return
        End If

    End Sub
    Private Sub btnNew_Operations_Click(sender As Object, e As EventArgs) Handles btnNew_Operations.Click
        'btnNew_Operations.Enabled = False
        'btnSave_Operations.Enabled = True
        'btnCancel_Operations.Enabled = True
        'btnDelet_Operations.Enabled = False
        'btnUpdat_Operations.Enabled = False
        'btnPrint_Operations.Enabled = False

    End Sub
    Private Sub btnSave_Operations_Click(sender As Object, e As EventArgs) Handles btnSave_Operations.Click
        Try
            If CDbl(txt_Bill_ID.Text) <= 0 Or Nothing Then
                ErrorProvider1.SetError(txt_Bill_ID, "أدخل رقم الفاتورة")
                Return
            End If
        Catch ex As Exception
            ErrorProvider1.SetError(txt_Bill_ID, "أدخل رقم الفاتورة")
            Return
        End Try


        If CDbl(txt_Amount_Drug.Text) <= 0 Then
            ErrorProvider1.SetError(txt_Amount_Drug, "أدخل الكمية")
            Return
        End If
        For Each txt As Control In GroupBox6.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text = "" Then
                    ErrorProvider1.SetError(txt, "أكمل البيانات")
                    MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return
                End If
            ElseIf TypeOf txt Is ComboBox Then
                If txt.Text = "" Then
                    ErrorProvider1.SetError(txt, "أكمل البيانات")
                    MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                    Return
                End If
            End If
        Next
        If drg_Exp.Rows.Count = 0 Then
            MessageBox.Show("الكمية لا تسمح ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        ElseIf drg_Exp.CurrentRow.Cells(2).Value < Math.Round((Val(txt_Amount_Drug.Text) / Val(txtUnit_Number.Text)), 2) Then
            MessageBox.Show("الكمية لا تسمح ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        If cbo_Employees.SelectedIndex = -1 Then
            ErrorProvider1.SetError(cbo_Employees, "أكمل البيانات")
            MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return

        End If
        Save_To_Stock()

        V = 0
        Fillgrd_Stock()

        GroupBox5.Enabled = False

        txt_Amount_Drug.Text = 0

        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnCancel_Operations_Click(sender As Object, e As EventArgs) Handles btnCancel_Operations.Click
        Dim sql As String = "Update  Stocks_Sales set State = @State where ID = @ID"
        myconn.cmd = New SqlCommand(sql, myconn.conn)
        Try
            If drg_Mos.CurrentRow.Cells(15).Value = True Then
                With myconn.cmd.Parameters
                    .Add("@State", SqlDbType.Bit).Value = 0
                    .Add("@ID", SqlDbType.Int).Value = CInt(drg_Mos.CurrentRow.Cells(14).Value)
                End With
            Else
                With myconn.cmd.Parameters
                    .Add("@State", SqlDbType.Bit).Value = 1
                    .Add("@ID", SqlDbType.Int).Value = CInt(drg_Mos.CurrentRow.Cells(14).Value)
                End With
            End If

            If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
            myconn.conn.Open()
            myconn.cmd.ExecuteNonQuery()
            myconn.conn.Close()
            V = 0
            Fillgrd_Stock()
            B = 0
            Binding_Stock()

        Catch ex As Exception
            MsgBox("قم باختيار الصنف")
            Return
        End Try


    End Sub
    Private Sub btnDelet_Operations_Click(sender As Object, e As EventArgs) Handles btnDelet_Operations.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Try
                myconn.DeleteRecord("Stocks_Sales", "ID", drg_Mos.CurrentRow.Cells(14).Value)
                V = 0
                Fillgrd_Stock()
                B = 0
                Binding_Stock()

            Catch ex As Exception
                MsgBox("قم باختيار الصنف")
                Return
            End Try

        End If
    End Sub
    Private Sub btnUpdat_Operations_Click(sender As Object, e As EventArgs) Handles btnUpdat_Operations.Click
        Dim sql As String = "Update Stocks_Sales set Stock_ID=@Stock_ID,Bill_ID=@Bill_ID,Bill_Date=@Bill_Date,
                             Time_Add=@Time_Add,Drug_ID=@Drug_ID,Drug_exp=@Drug_exp,Drug_Price=@Drug_Price,Amount=@Amount,Total_Price=@Total_Price,
                             Unit=@Unit,Unit_Kind=@Unit_Kind,EmployeeID=@EmployeeID,Patient_ID=@Patient_ID,DoctorsID=@DoctorsID,CerviceID=@CerviceID,
                             specializationID=@specializationID,Users_ID=@Users_ID where ID =@ID"


        myconn.cmd = New SqlCommand(sql, myconn.conn)
        With myconn.cmd.Parameters
            .Add("@Stock_ID", SqlDbType.Int).Value = StockID
            .Add("@Bill_ID", SqlDbType.Int).Value = txt_Bill_ID.Text
            .Add("@Bill_Date", SqlDbType.NChar).Value = Format(CDate(txt_Date.Text), "yyyy/MM/dd")
            .Add("@Time_Add", SqlDbType.NChar).Value = Label20.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
            .Add("@Drug_exp", SqlDbType.NChar).Value = drg_Exp.CurrentRow.Cells(1).Value
            .Add("@Drug_Price", SqlDbType.Decimal).Value = txt_Drug_price.Text
            .Add("@Amount", SqlDbType.Decimal).Value = Math.Round((Val(txt_Amount_Drug.Text) / Val(txtUnit_Number.Text)), 2)
            .Add("@Unit", SqlDbType.Int).Value = txtUnit_ID.Text
            .Add("@Unit_Kind", SqlDbType.Int).Value = cbo_Unit.SelectedIndex
            .Add("@Total_Price", SqlDbType.Decimal).Value = Val(txt_Drug_price.Text) * Val(txt_Amount_Drug.Text)
            .Add("@EmployeeID", SqlDbType.Int).Value = cbo_Employees.SelectedValue
            .Add("@Patient_ID", SqlDbType.Int).Value = cboPatientName.SelectedValue
            .Add("@DoctorsID", SqlDbType.Int).Value = cboDoctor.SelectedValue
            .Add("@CerviceID", SqlDbType.Int).Value = cboCervice.SelectedValue
            .Add("@specializationID", SqlDbType.Int).Value = cboKissm.SelectedValue
            .Add("@Users_ID", SqlDbType.Int).Value = My.Settings.user_ID
            .Add("@ID", SqlDbType.Int).Value = drg_Mos.CurrentRow.Cells(14).Value
        End With
        Try
            If myconn.conn.State = ConnectionState.Open Then myconn.conn.Close()
            myconn.conn.Open()
            myconn.cmd.ExecuteNonQuery()
            myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            Return
        End Try
        B = 0
        Binding_Stock()

        V = 0
        Fillgrd_Stock()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub cbo_Drug_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Drug.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        If cbo_Drug.SelectedIndex = -1 Then Return
        B = 0
        Binding_Stock()

        cbo_Unit_SelectedIndexChanged(Nothing, Nothing)
    End Sub
    Private Sub cbo_Drug_Enter(sender As Object, e As EventArgs) Handles cbo_Drug.Enter
        myconn.langAR()
    End Sub
    Private Sub cbo_Drug_KeyUp(sender As Object, e As KeyEventArgs) Handles cbo_Drug.KeyUp
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.Escape Then
            cbo_Drug.SelectedIndex = -1
            cbo_Drug.Focus()
        End If
    End Sub
    Private Sub cbo_Unit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Unit.SelectedIndexChanged
        ErrorProvider1.Clear()
        B = 1
        Binding_Stock()
        txtTotal_Price.Text = Math.Round((Val(txt_Amount_Drug.Text) * Val(txt_Drug_price.Text)), 2)
    End Sub
    Private Sub cbo_Employees_TextChanged(sender As Object, e As EventArgs) Handles cbo_Employees.TextChanged
        ErrorProvider1.Clear()
    End Sub

    Private Sub txt_Amount_Drug_KeyUp(sender As Object, e As KeyEventArgs) Handles txt_Amount_Drug.KeyUp
        If e.KeyCode = Keys.Enter AndAlso e.Control = True Then
            btnSave_Operations_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.N AndAlso e.Control = True Then
            btnNew_Operations_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancel_Operations_Click(Nothing, Nothing)
            cbo_Drug.Focus()
        End If
    End Sub
    Private Sub txt_Amount_Drug_TextChanged(sender As Object, e As EventArgs) Handles txt_Amount_Drug.TextChanged
        txtTotal_Price.Text = Val(txt_Amount_Drug.Text) * Val(txt_Drug_price.Text)
    End Sub
    Private Sub drg_Mos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_Mos.CellClick
        myconn.Filldataset2("select * from Stocks_Sales where ID =" & CInt(drg_Mos.CurrentRow.Cells(14).Value), "Stocks_Sales", Me)
        cbo_Drug.SelectedValue = myconn.cur2.Current("Drug_ID")
    End Sub
#End Region

#Region "مستهلك الحضانات"
    Private Sub btnIncubator_Click(sender As Object, e As EventArgs) Handles btnIncubator.Click  ' >>>>>>>>>>>>>>>>>>> مستهلك حضانات
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If

        For Each ctl As Control In GroupBox2.Controls
            If TypeOf ctl Is GroupBox Then
                If ctl.Text = "" Then
                    ctl.Visible = True
                Else
                    ctl.Visible = False
                End If

            End If
        Next ctl

        StockID = 4
        grbZL(grbOperations_Drage, 7, 12, 596, 1036, True, Label89)
        drg_Location(drg_Mos, 6, 19, 1010, 276)
        grbOperations_Drage.Text = "مستهلك الحضانات  :  "
        grbPatientData.Visible = False
        grbPatientVisites.Visible = False
        fin = False
        myconn.Fillcombo6("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cbo_Employees)
        myconn.Fillcombo6("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug)
        fin = True
        Label129.Text = 0
        Label130.Text = clsNumber.nTOword(Label129.Text)
        Label91.Text = 0
        Label107.Text = clsNumber.nTOword(Label91.Text)
        V = 1
        Fillgrd_Stock()
        If drg_Mos.Rows.Count = 0 Then
            myconn.Filldataset7("select  isnull(max(Bill_ID),0) as Bill_ID from Stocks_Sales where Stock_ID =" & CInt(StockID), "Stocks_Purchases", Me)
            txt_Bill_ID.Text = myconn.cur7.Current("Bill_ID") + 1
            GroupBox5.Enabled = True
        Else
            txt_Bill_ID.Text = myconn.cur.Current("Bill_ID")
            cbo_Employees.SelectedValue = myconn.cur.Current("EmployeeID")
            GroupBox5.Enabled = False
            Return
        End If
    End Sub

#End Region

#Region "مستهلك الإقامة"
    Private Sub btnStaying_Click(sender As Object, e As EventArgs) Handles btnStaying.Click  ' >>>>>>>>>>>>>>>>>>>> مستهلك إقامة
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If

        For Each ctl As Control In GroupBox2.Controls
            If TypeOf ctl Is GroupBox Then
                If ctl.Text = "" Then
                    ctl.Visible = True
                Else
                    ctl.Visible = False
                End If
            End If
        Next ctl

        StockID = 5
        grbZL(grbOperations_Drage, 7, 12, 596, 1036, True, Label89)
        drg_Location(drg_Mos, 6, 19, 1010, 276)
        grbOperations_Drage.Text = "مستهلك الإقامة  :  "
        grbPatientData.Visible = False
        grbPatientVisites.Visible = False
        fin = False
        myconn.Fillcombo6("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cbo_Employees)
        myconn.Fillcombo6("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug)
        fin = True
        Label129.Text = 0
        Label130.Text = clsNumber.nTOword(Label129.Text)
        Label91.Text = 0
        Label107.Text = clsNumber.nTOword(Label91.Text)
        V = 1
        Fillgrd_Stock()
        If drg_Mos.Rows.Count = 0 Then
            myconn.Filldataset7("select  isnull(max(Bill_ID),0) as Bill_ID from Stocks_Sales where Stock_ID =" & CInt(StockID), "Stocks_Purchases", Me)
            txt_Bill_ID.Text = myconn.cur7.Current("Bill_ID") + 1
            GroupBox5.Enabled = True
        Else
            txt_Bill_ID.Text = myconn.cur.Current("Bill_ID")
            cbo_Employees.SelectedValue = myconn.cur.Current("EmployeeID")
            GroupBox5.Enabled = False
            Return
        End If

    End Sub
#End Region

#Region "التخدير"
    Private Sub btnNarcosis_Click(sender As Object, e As EventArgs) Handles btnNarcosis.Click  ' >>>>>>>>>>>>>>>>>>>>>> التخدير
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If
        drg_Narcosis.Rows.Clear()

        grbZL(grbNarcosis, 8, 13, 596, 554, True, Label38)
        drg_Location(drg_Narcosis, 5, 105, 543, 485)
        grbPatientData.Enabled = False
        grbPatientVisites.Enabled = False
        myconn.Fillcombo6("select * from Doctors ", "Doctors", "DoctorsID", "DoctorsName", Me, cboDoctors_Narcosis)
        btnNew_Narcosis.Enabled = True
        btnSave_Narcosis.Enabled = False
        btnCancel_Narcosis.Enabled = False
        btnDelet_Narcosis.Enabled = True
        btnUpdat_Narcosis.Enabled = True
        X = 7
        Fillgrd()
    End Sub
    Private Sub btnNew_Narcosis_Click(sender As Object, e As EventArgs) Handles btnNew_Narcosis.Click
        btnNew_Narcosis.Enabled = False
        btnSave_Narcosis.Enabled = True
        btnCancel_Narcosis.Enabled = True
        btnDelet_Narcosis.Enabled = False
        btnUpdat_Narcosis.Enabled = False
        cboDoctors_Narcosis.SelectedIndex = -1
        txtPrice_Narcosis.Text = ""
    End Sub
    Private Sub btnCancel_Narcosis_Click(sender As Object, e As EventArgs) Handles btnCancel_Narcosis.Click
        btnNew_Narcosis.Enabled = True
        btnSave_Narcosis.Enabled = False
        btnCancel_Narcosis.Enabled = False
        btnDelet_Narcosis.Enabled = True
        btnUpdat_Narcosis.Enabled = True
        cboDoctors_Narcosis.SelectedIndex = -1
        txtPrice_Narcosis.Text = ""
    End Sub
    Private Sub btnSave_Narcosis_Click(sender As Object, e As EventArgs) Handles btnSave_Narcosis.Click
        myconn.Filldataset7("select * from Patient_Narcosis ", "Patient_Narcosis", Me)

        If txtRecordID.Text = "" OrElse txtVisitID.Text = "" OrElse txtPrice_Narcosis.Text = "" OrElse txtPatientID.Text = "" OrElse cboDoctors_Narcosis.SelectedIndex = -1 Then
            MessageBox.Show("أكمل البيانات المطلوبة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        Dim XX() As String = {txtPatientID.Text, txtVisitID.Text, txtRecordID.Text, "'" & Format(CDate(dtp1.Text), "yyyy/MM/dd").ToString & "'", cboDoctors_Narcosis.SelectedValue, txtPrice_Narcosis.Text}
        myconn.AddNewRecord("Patient_Narcosis", XX)
        btnSave_Car.Enabled = False
        btnNew_Car.Enabled = True
        btnCancel_Car.Enabled = False
        btnDelet_Car.Enabled = True
        btnUpdat_Car.Enabled = True
        X = 7
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub btnDelet_Narcosis_Click(sender As Object, e As EventArgs) Handles btnDelet_Narcosis.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Patient_Narcosis", "ID", CInt(drg_Narcosis.CurrentRow.Cells(5).Value))
            myconn.ClearAllText(Me, grbNarcosis)
            X = 7
            Fillgrd()
        End If
    End Sub
    Private Sub btnUpdat_Narcosis_Click(sender As Object, e As EventArgs) Handles btnUpdat_Narcosis.Click
        Dim Values() As String = {cboDoctors_Narcosis.SelectedValue, "'" & txtPrice_Narcosis.Text & "'"}
        Dim Mycolumes() As String = {"Car_Tool_ID", "Car_Tool_Price"}
        myconn.UpdateRecord("Patient_Narcosis", Mycolumes, Values, "ID", CInt(drg_Narcosis.CurrentRow.Cells(5).Value))
        X = 4
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)

    End Sub
    Private Sub drg_Narcosis_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_Narcosis.CellClick
        myconn.Filldataset7("select * from Patient_Narcosis where ID =" & CInt(drg_Narcosis.CurrentRow.Cells(5).Value), "Patient_Car_Tools", Me)
        txtPrice_Narcosis.DataBindings.Clear()
        txtPrice_Narcosis.DataBindings.Add("text", myconn.dv7, "Narcosis_Price")
        cboDoctors_Narcosis.DataBindings.Clear()
        cboDoctors_Narcosis.DataBindings.Add("selectedvalue", myconn.dv7, "DoctorsID")
    End Sub
#End Region

#Region "إضافات على الفاتورة"
    Private Sub btnAdd_to_Bill_Click(sender As Object, e As EventArgs) Handles btnAdd_to_Bill.Click  ' >>>>>>>>>>>>>>>> إضافات على الفاتورة
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If
        drg_Add_bill.Rows.Clear()

        grbZL(grbBill_Adds, 8, 13, 596, 554, True, Label31)
        drg_Location(drg_Add_bill, 5, 190, 543, 400)
        grbPatientData.Enabled = False
        grbPatientVisites.Enabled = False
        myconn.Fillcombo6("select * from Doctors ", "Doctors", "DoctorsID", "DoctorsName", Me, cboDoctors_Fllower)

        btnNew_Add_Bill.Enabled = True
        btnSave_Add_Bill.Enabled = False
        btnCancel_Add_Bill.Enabled = False
        btnDelet_Add_Bill.Enabled = True
        btnUpdat_Add_Bill.Enabled = True
        ClearAllText(Me, grbBill_Adds)
        X = 8
        Fillgrd()

    End Sub
    Private Sub btnSave_Add_Bill_Click(sender As Object, e As EventArgs) Handles btnSave_Add_Bill.Click
        myconn.Filldataset7("select * from Patient_Add_To_Bill ", "Patient_Add_To_Bill", Me)

        If txtRecordID.Text = "" OrElse txtVisitID.Text = "" OrElse txtRecordID.Text = "" OrElse txtPatientID.Text = "" Then
            MessageBox.Show("أكمل البيانات المطلوبة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        Dim g As String
        If cboDoctors_Fllower.SelectedIndex = -1 Then
            g = "NULL"
        Else
            g = cboDoctors_Fllower.SelectedValue
        End If
        Dim XX() As String = {txtPatientID.Text, txtVisitID.Text, txtRecordID.Text, txtNurs.Text, txtTechnical.Text, txtTax.Text, txtService.Text, txtAdmen_exp.Text,
                              txtOther_expen.Text, txtResu.Text, txtIncubator.Text, g, txtDoctors_Fllower.Text, "'" & Format(CDate(dtp1.Text), "yyyy/MM/dd").ToString & "'", txtVisit_number.Text}
        myconn.AddNewRecord("Patient_Add_To_Bill", XX)
        btnNew_Add_Bill.Enabled = True
        btnSave_Add_Bill.Enabled = False
        btnCancel_Add_Bill.Enabled = False
        btnDelet_Add_Bill.Enabled = True
        btnUpdat_Add_Bill.Enabled = True
        myconn.ClearAllText(Me, grbBill_Adds)
        X = 8
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub btnNew_Add_Bill_Click(sender As Object, e As EventArgs) Handles btnNew_Add_Bill.Click
        btnNew_Add_Bill.Enabled = False
        btnSave_Add_Bill.Enabled = True
        btnCancel_Add_Bill.Enabled = True
        btnDelet_Add_Bill.Enabled = False
        btnUpdat_Add_Bill.Enabled = False
        ClearAllText(Me, grbBill_Adds)
    End Sub
    Private Sub btnDelet_Add_Bill_Click(sender As Object, e As EventArgs) Handles btnDelet_Add_Bill.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Patient_Add_To_Bill", "ID", CInt(drg_Add_bill.CurrentRow.Cells(5).Value))
            myconn.ClearAllText(Me, grbBill_Adds)
            X = 8
            Fillgrd()
        End If

    End Sub
    Private Sub btnCancel_Add_Bill_Click(sender As Object, e As EventArgs) Handles btnCancel_Add_Bill.Click
        btnNew_Add_Bill.Enabled = True
        btnSave_Add_Bill.Enabled = False
        btnCancel_Add_Bill.Enabled = False
        btnDelet_Add_Bill.Enabled = True
        btnUpdat_Add_Bill.Enabled = True
        ClearAllText(Me, grbBill_Adds)
    End Sub
    Private Sub btnUpdat_Add_Bill_Click(sender As Object, e As EventArgs) Handles btnUpdat_Add_Bill.Click
        Dim g As String
        If cboDoctors_Fllower.SelectedIndex = -1 Then
            g = "NULL"
        Else
            g = cboDoctors_Fllower.SelectedValue
        End If
        Dim Values() As String = {txtNurs.Text, txtTechnical.Text, txtTax.Text, txtService.Text, txtAdmen_exp.Text, txtOther_expen.Text, txtResu.Text, txtIncubator.Text, g, txtDoctors_Fllower.Text, txtVisit_number.Text}

        Dim Mycolumes() As String = {"Nurs", "Technical", "Tax", "Service", "Expenses_Admin", "Other_Expenses", "Resu", "Incubator", "DoctorsID", "Doctors_Rat", "Doctors_visit_number"}
        myconn.UpdateRecord("Patient_Add_To_Bill", Mycolumes, Values, "ID", CInt(drg_Narcosis.CurrentRow.Cells(5).Value))
        X = 4
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)

    End Sub
    Private Sub drg_Add_bill_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_Add_bill.CellClick
        myconn.Filldataset7("select * from Patient_Add_To_Bill where ID =" & CInt(drg_Add_bill.CurrentRow.Cells(5).Value), "Patient_Add_To_Bill", Me)

        Dim x() As TextBox = {txtNurs, txtTechnical, txtTax, txtService, txtAdmen_exp, txtOther_expen, txtResu, txtIncubator, txtDoctors_Fllower, txtVisit_number}
        Dim F() As String = {"Nurs", "Technical", "Tax", "Service", "Expenses_Admin", "Other_Expenses", "Resu", "Incubator", "Doctors_Rat", "Doctors_visit_number"}
        TextBindingdata(Me, grbBill_Adds, F, x)

        cboDoctors_Fllower.DataBindings.Clear()
        cboDoctors_Fllower.DataBindings.Add("selectedvalue", myconn.dv7, "DoctorsID")
    End Sub
    Sub TextBindingdata(frm As Form, grb As GroupBox, Fields() As String, txt() As TextBox)

        For i As Integer = 0 To Fields.Count - 1
            txt(i).DataBindings.Clear()
            txt(i).DataBindings.Add("text", myconn.dv7, Fields(i))
        Next
    End Sub
    Sub ClearAllText(frm As Form, grb As GroupBox)
        For Each crtl As Control In grb.Controls
            If TypeOf crtl Is TextBox Then
                crtl.Text = 0
            End If
        Next crtl
    End Sub
#End Region

#Region "اضافات على الإقامة"
    Private Sub btnAdd_to_staying_Click(sender As Object, e As EventArgs) Handles btnAdd_to_staying.Click  ' >>>>>>>>>>>>>>>> إضافات على الإقامة
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If
        drg_Add_staying.Rows.Clear()

        grbZL(grbBillAdds2, 8, 13, 596, 554, True, Label41)
        drg_Location(drg_Add_staying, 5, 105, 543, 485)
        grbPatientData.Enabled = False
        grbPatientVisites.Enabled = False
        btnNew_Staying.Enabled = True
        btnSave_Staying.Enabled = False
        btnCancel_Staying.Enabled = False
        btnDelet_Staying.Enabled = True
        btnUpdat_Staying.Enabled = True
        txtStaying_Band.Text = ""
        txtStaying_Price.Text = ""
        X = 9
        Fillgrd()
    End Sub
    Private Sub btnNew_Staying_Click(sender As Object, e As EventArgs) Handles btnNew_Staying.Click
        btnNew_Staying.Enabled = False
        btnSave_Staying.Enabled = True
        btnCancel_Staying.Enabled = True
        btnDelet_Staying.Enabled = False
        btnUpdat_Staying.Enabled = False
        txtStaying_Band.Text = ""
        txtStaying_Price.Text = ""
    End Sub
    Private Sub btnSave_Staying_Click(sender As Object, e As EventArgs) Handles btnSave_Staying.Click
        myconn.Filldataset7("select * from patient_Add_To_Staying ", "patient_Add_To_Staying", Me)
        If txtRecordID.Text = "" OrElse txtVisitID.Text = "" OrElse txtPatientID.Text = "" OrElse txtStaying_Band.Text = "" OrElse txtStaying_Price.Text = "" Then
            MessageBox.Show("أكمل البيانات المطلوبة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        Dim XX() As String = {txtPatientID.Text, txtVisitID.Text, txtRecordID.Text, "'" & Format(CDate(dtp1.Text), "yyyy/MM/dd").ToString & "'", "'" & txtStaying_Band.Text & "'", txtStaying_Price.Text}
        myconn.AddNewRecord("patient_Add_To_Staying", XX)
        btnSave_Car.Enabled = False
        btnNew_Car.Enabled = True
        btnCancel_Car.Enabled = False
        btnDelet_Car.Enabled = True
        btnUpdat_Car.Enabled = True
        X = 9
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnCancel_Staying_Click(sender As Object, e As EventArgs) Handles btnCancel_Staying.Click
        btnNew_Staying.Enabled = False
        btnSave_Staying.Enabled = True
        btnCancel_Staying.Enabled = True
        btnDelet_Staying.Enabled = False
        btnUpdat_Staying.Enabled = False
        txtStaying_Band.Text = ""
        txtStaying_Price.Text = ""
    End Sub
    Private Sub btnDelet_Staying_Click(sender As Object, e As EventArgs) Handles btnDelet_Staying.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("patient_Add_To_Staying", "ID", CInt(drg_Add_staying.CurrentRow.Cells(4).Value))
            myconn.ClearAllText(Me, grbBillAdds2)
            X = 9
            Fillgrd()
        End If

    End Sub
    Private Sub btnUpdat_Staying_Click(sender As Object, e As EventArgs) Handles btnUpdat_Staying.Click
        Dim Values() As String = {"'" & txtStaying_Band.Text & "'", txtStaying_Price.Text}
        Dim Mycolumes() As String = {"band", "band_Price"}
        myconn.UpdateRecord("patient_Add_To_Staying", Mycolumes, Values, "ID", CInt(drg_Add_staying.CurrentRow.Cells(4).Value))
        X = 9
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub drg_Add_staying_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_Add_staying.CellClick
        myconn.Filldataset7("select * from patient_Add_To_Staying where ID =" & CInt(drg_Add_staying.CurrentRow.Cells(4).Value), "patient_Add_To_Staying", Me)
        txtStaying_Price.DataBindings.Clear()
        txtStaying_Price.DataBindings.Add("text", myconn.dv7, "band_Price")

        txtStaying_Band.DataBindings.Clear()
        txtStaying_Band.DataBindings.Add("text", myconn.dv7, "band")
    End Sub


#End Region

#Region "اضافة قيمة على الاستقبال"
    Private Sub btnReciption_Click(sender As Object, e As EventArgs) Handles btnReciption.Click  ' >>>>>>>>>>>>>> خدمات الاستقبال
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If
        drg_Reciption.Rows.Clear()

        grbZL(grbReciption, 8, 13, 596, 554, True, Label44)
        drg_Location(drg_Reciption, 5, 105, 543, 485)
        grbPatientVisites.Enabled = False
        grbPatientData.Enabled = False
        btnNew_Recp.Enabled = True
        btnSave_Recp.Enabled = False
        btnCancel_Recp.Enabled = False
        btnDelet_Recp.Enabled = True
        btnUpdat_Recp.Enabled = True
        txtService_Recp.Text = ""
        txtStaying_Price.Text = ""
        X = 10
        Fillgrd()
    End Sub
    Private Sub btnNew_Recp_Click(sender As Object, e As EventArgs) Handles btnNew_Recp.Click
        btnNew_Recp.Enabled = False
        btnSave_Recp.Enabled = True
        btnCancel_Recp.Enabled = True
        btnDelet_Recp.Enabled = False
        btnUpdat_Recp.Enabled = False
        txtService_Recp.Text = ""
        txtStaying_Price.Text = ""
    End Sub
    Private Sub btnSave_Recp_Click(sender As Object, e As EventArgs) Handles btnSave_Recp.Click
        myconn.Filldataset7("select * from Patient_Add_To_Reception ", "Patient_Add_To_Reception", Me)
        If txtRecordID.Text = "" OrElse txtVisitID.Text = "" OrElse txtPatientID.Text = "" OrElse txtService_Recp.Text = "" OrElse txtRecp_Price.Text = "" Then
            MessageBox.Show("أكمل البيانات المطلوبة", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If
        Dim XX() As String = {txtPatientID.Text, txtVisitID.Text, txtRecordID.Text, "'" & Format(CDate(dtp1.Text), "yyyy/MM/dd").ToString & "'", "'" & txtService_Recp.Text & "'", txtRecp_Price.Text}
        myconn.AddNewRecord("Patient_Add_To_Reception", XX)
        btnNew_Recp.Enabled = True
        btnSave_Recp.Enabled = False
        btnCancel_Recp.Enabled = False
        btnDelet_Recp.Enabled = True
        btnUpdat_Recp.Enabled = True
        txtRecp_Price.Text = ""
        txtService_Recp.Text = ""
        X = 10
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnCancel_Recp_Click(sender As Object, e As EventArgs) Handles btnCancel_Recp.Click
        btnNew_Recp.Enabled = True
        btnSave_Recp.Enabled = False
        btnCancel_Recp.Enabled = False
        btnDelet_Recp.Enabled = True
        btnUpdat_Recp.Enabled = True
        txtRecp_Price.Text = ""
        txtService_Recp.Text = ""
    End Sub
    Private Sub btnDelet_Recp_Click(sender As Object, e As EventArgs) Handles btnDelet_Recp.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            myconn.DeleteRecord("Patient_Add_To_Reception", "ID", CInt(drg_Reciption.CurrentRow.Cells(4).Value))
            myconn.ClearAllText(Me, grbReciption)
            X = 10
            Fillgrd()
        End If

    End Sub
    Private Sub btnUpdat_Recp_Click(sender As Object, e As EventArgs) Handles btnUpdat_Recp.Click
        Dim Values() As String = {"'" & txtService_Recp.Text & "'", txtRecp_Price.Text}
        Dim Mycolumes() As String = {"band", "band_Price"}
        myconn.UpdateRecord("Patient_Add_To_Reception", Mycolumes, Values, "ID", CInt(drg_Reciption.CurrentRow.Cells(4).Value))
        X = 10
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
    End Sub
    Private Sub drg_Reciption_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg_Reciption.CellClick
        myconn.Filldataset7("select * from Patient_Add_To_Reception where ID =" & CInt(drg_Reciption.CurrentRow.Cells(4).Value), "Patient_Add_To_Reception", Me)
        txtRecp_Price.DataBindings.Clear()
        txtRecp_Price.DataBindings.Add("text", myconn.dv7, "band_Price")

        txtService_Recp.DataBindings.Clear()
        txtService_Recp.DataBindings.Add("text", myconn.dv7, "band")

    End Sub
#End Region

#Region "تسجيل خروج المريض"
    Private Sub btnPatient_out_Click(sender As Object, e As EventArgs) Handles btnPatient_out.Click  ' >>>>>>>>>>>>> تسجيل خروج مريض
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If

        grbZL(grbOutPatient, 8, 13, 596, 554, True, Label50)
        grbPatientData.Enabled = False
        grbPatientVisites.Enabled = False
        btnNew_Out.Enabled = True
        btnSave_Out.Enabled = False
        btnCancel_Out.Enabled = False
        btnDelet_Out.Enabled = True
        btnUpdat_Out.Enabled = True
        dtp2.Enabled = False
        txtTime_Out.Enabled = False
        txtTime_Out.Text = TimeOfDay
        dtp2.Text = Date.Today
    End Sub
    Private Sub btnNew_Out_Click(sender As Object, e As EventArgs) Handles btnNew_Out.Click
        btnNew_Out.Enabled = False
        btnSave_Out.Enabled = True
        btnCancel_Out.Enabled = True
        btnDelet_Out.Enabled = False
        btnUpdat_Out.Enabled = False
        dtp2.Enabled = True
        txtTime_Out.Enabled = True
        txtTime_Out.Text = TimeOfDay
        dtp2.Text = Date.Today
    End Sub
    Private Sub btnSave_Out_Click(sender As Object, e As EventArgs) Handles btnSave_Out.Click
        Dim Values() As String = {"'" & Format(CDate(dtp2.Text), "yyyy/MM/dd").ToString & "'", "'" & txtTime_Out.Text & "'"}
        Dim Mycolumes() As String = {"Out_Date", "Out_Time"}
        myconn.UpdateRecord("Login_Patients", Mycolumes, Values, "ID", CInt(drg_visits.CurrentRow.Cells(5).Value))
        X = 0
        Fillgrd()
        btnNew_Out.Enabled = True
        btnSave_Out.Enabled = False
        btnCancel_Out.Enabled = False
        btnDelet_Out.Enabled = True
        btnUpdat_Out.Enabled = True
        dtp2.Enabled = False
        txtTime_Out.Enabled = False
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnCancel_Out_Click(sender As Object, e As EventArgs) Handles btnCancel_Out.Click
        btnNew_Out.Enabled = True
        btnSave_Out.Enabled = False
        btnCancel_Out.Enabled = False
        btnDelet_Out.Enabled = True
        btnUpdat_Out.Enabled = True
        txtTime_Out.Text = TimeOfDay
        dtp2.Text = Date.Today
        dtp2.Enabled = False
        txtTime_Out.Enabled = False
    End Sub
    Private Sub btnDelet_Out_Click(sender As Object, e As EventArgs) Handles btnDelet_Out.Click
        Dim Values() As String = {"NULL", "NULL"}
        Dim Mycolumes() As String = {"Out_Date", "Out_Time"}
        myconn.UpdateRecord("Login_Patients", Mycolumes, Values, "ID", CInt(drg_visits.CurrentRow.Cells(5).Value))
        X = 0
        Fillgrd()
        btnNew_Out.Enabled = True
        btnSave_Out.Enabled = False
        btnCancel_Out.Enabled = False
        btnDelet_Out.Enabled = True
        btnUpdat_Out.Enabled = True
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub btnUpdat_Out_Click(sender As Object, e As EventArgs) Handles btnUpdat_Out.Click
        Dim Values() As String = {"'" & Format(CDate(dtp2.Text), "yyyy/MM/dd").ToString & "'", "'" & txtTime_Out.Text & "'"}
        Dim Mycolumes() As String = {"Out_Date", "Out_Time"}
        myconn.UpdateRecord("Login_Patients", Mycolumes, Values, "ID", CInt(drg_visits.CurrentRow.Cells(5).Value))
        X = 0
        Fillgrd()
        btnNew_Out.Enabled = True
        btnSave_Out.Enabled = False
        btnCancel_Out.Enabled = False
        btnDelet_Out.Enabled = True
        btnUpdat_Out.Enabled = True
        dtp2.Enabled = False
        txtTime_Out.Enabled = False
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub


#End Region

#Region "حساب المريض"
    Private Sub btnAccount_Click(sender As Object, e As EventArgs) Handles btnAccount.Click  ' >>>>>>>>>>>>> حساب المريض
        If txtRecordID.Text = "" Then
            ErrorProvider1.SetError(txtRecordID, "أدخل رقم السجل")
            Return
        End If
        If txtVisitID.Text = "" Then
            ErrorProvider1.SetError(txtVisitID, "أدخل رقم الزيارة")
            Return
        End If
        If cboPatientName.SelectedValue = -1 Then
            ErrorProvider1.SetError(cboPatientName, "أدخل اسم المريض")
            Return
        End If

        grbZL(grbPatientAcount, 8, 13, 596, 554, True, Label58)
        drg_Location(drg_Acount, 5, 15, 543, 500)
        grbPatientData.Enabled = False
        grbPatientVisites.Enabled = False
        X = 14
        Fillgrd()

    End Sub




#End Region
End Class