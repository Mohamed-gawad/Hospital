Public Class frmEmployees_Report_Salary
    Dim Myconn As New connect
    Public Property EmpID As Integer
    Public Property Dat As String

    Sub Fillgrd() ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        Dat = txt1.Text
        drg.Rows.Clear()
        Myconn.Filldataset("select a.EmployeeName,a.EmployeeID,j.jobname,S.Employee_Salary,s.Work_hours,isnull((isnull(N.late_In_Out,0) + isnull(z.late_Ezn,0) ),0) as Late,
                             isnull(g.Depit_amount,0) as Depit_amount ,isnull(b.Absent_em,0) as Absent_em,isnull(H.Holiday_number,0) as Holiday_number,
                             ISNULL(f.Employee_Gift,0) as Employee_Gift , ISNULL(l.Badal,0) as Badal,ISNULL(Y.Zyada,0) as Zyada,ISNULL(c.Insurance,0) as Insurance,
                             ISNULL(x.Extra_work,0) as Extra_work,ISNULL(Ezn_hours,0) as Ezn_hours , ISNULL(Qard_Kist,0) as Qard_Kist
                             from Employees a
                            left join Jobs j on a.JobID = j.jobID
                            left join Employees_Salary S on a.EmployeeID = S.EmployeeID
                            left join (select isnull(sum(Late),0 ) as late_In_Out , EmployeeID from [dbo].[Employees_In_Out] group by EmployeeID,cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) having cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "') N
                            on a.EmployeeID = n.EmployeeID
                            left join (select isnull(sum(Ezn_late),0 ) as late_Ezn ,isnull(sum(Ezn_hours),0 ) as Ezn_hours, EmployeeID from [dbo].[Employees_Ezn] group by EmployeeID,cast(DATEPART(yyyy,Ezn_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Ezn_date),'00') as varchar(2)) having cast(DATEPART(yyyy,Ezn_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Ezn_date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "') Z
                            on a.EmployeeID = Z.EmployeeID
                            left join (select isnull(sum(Depit_amount),0 ) as Depit_amount , EmployeeID from [dbo].[Employees_Depit] group by EmployeeID,cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) having cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "' ) g
                            on a.EmployeeID = g.EmployeeID
                            left join (select isnull(count(Holiday_ID),0 ) as Absent_em , EmployeeID from [dbo].[Employees_In_Out] group by EmployeeID,Holiday_ID,cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2))  having Holiday_ID = 6 and  cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "' ) b
                            on a.EmployeeID = b.EmployeeID
                            left join (select isnull(sum(Holiday_number),0 ) as Holiday_number , EmployeeID from [dbo].[Employees_Holiday] group by EmployeeID,cast(DATEPART(yyyy,H_date_begin) as varchar(4)) + '/' + cast(format(DATEPART(MM,H_date_begin),'00') as varchar(2)) having cast(DATEPART(yyyy,H_date_begin) as varchar(4)) + '/' + cast(format(DATEPART(MM,H_date_begin),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "') H
                            on a.EmployeeID = h.EmployeeID
                            left join (select isnull(sum(Employee_Gift),0 ) as Employee_Gift , EmployeeID from [dbo].[Employees_Gift] group by EmployeeID,cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) having cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "' ) F
                            on a.EmployeeID = F.EmployeeID
                            left join (select isnull(sum(Badal),0 ) as Badal , EmployeeID from [dbo].[Employees_Badal] group by EmployeeID, cast(DATEPART(yyyy,Badal_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Badal_date),'00') as varchar(2)) having cast(DATEPART(yyyy,Badal_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Badal_date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "') L
                            on a.EmployeeID = L.EmployeeID
                            left join (select isnull(sum(Zyada),0 ) as Zyada , EmployeeID from [dbo].[Employees_Zyada] group by EmployeeID ) Y
                            on a.EmployeeID = Y.EmployeeID
                            left join (select isnull(max(Insurance_amount),0 ) as Insurance , EmployeeID from [dbo].[Employees_Insurance] group by EmployeeID, DATEPART(yyyy,Insurance_Date) having DATEPART(yyyy,Insurance_Date) = '" & Format(CDate(txt1.Text), "yyyy") & "') C
                            on a.EmployeeID = C.EmployeeID
                            left join (select isnull(sum(Hours_number),0 ) as Extra_work , EmployeeID from [dbo].[Employees_Extra_work] group by EmployeeID,cast(DATEPART(yyyy,Work_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Work_date),'00') as varchar(2)) having cast(DATEPART(yyyy,Work_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Work_date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "' ) X
                            on a.EmployeeID = X.EmployeeID 
                            left join (select isnull(sum(Kist_amount),0 ) as Qard_Kist , EmployeeID from [dbo].[Employees_Qard_Kist] group by EmployeeID,cast(DATEPART(yyyy,Qard_Kist_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Qard_Kist_date),'00') as varchar(2)) having cast(DATEPART(yyyy,Qard_Kist_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Qard_Kist_date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "' ) Q
                            on a.EmployeeID = Q.EmployeeID 

                            where a.State_ID = 1", "Employees_In_Out", Me)

        If Myconn.cur.Count = 0 Then Return
        For i As Integer = 0 To Myconn.cur.Count - 1
            Dim Minute_amount As Double = 0
            drg.Rows.Add()
            Minute_amount = Val(Myconn.cur.Current("Employee_Salary")) / Val(Val(Myconn.cur.Current("Work_hours")) * 60 * 27)
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("EmployeeID")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("jobname")
            '-------------------------------------------------------------------------------------- مستحقات
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Employee_Salary")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Badal")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Employee_Gift")
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Zyada")
            drg.Rows(i).Cells(8).Value = Math.Round(Myconn.cur.Current("Extra_work") * Minute_amount, 2)

            drg.Rows(i).Cells(4).Style.BackColor = Color.LightGreen
            drg.Rows(i).Cells(5).Style.BackColor = Color.LightGreen
            drg.Rows(i).Cells(6).Style.BackColor = Color.LightGreen
            drg.Rows(i).Cells(7).Style.BackColor = Color.LightGreen
            drg.Rows(i).Cells(8).Style.BackColor = Color.LightGreen
            '---------------------------------------------------------------------------------------------
            '--------------------------------------------------------------------------------------------- استقطاعات
            drg.Rows(i).Cells(9).Value = Math.Round((Val(Myconn.cur.Current("Late") - Val(Val(If(IsDBNull(Myconn.cur.Current("Absent_em")), 0, Myconn.cur.Current("Absent_em")) * Val(If(IsDBNull(Myconn.cur.Current("Work_hours")), 0, Myconn.cur.Current("Work_hours"))) * 60))) * Minute_amount), 2)
            drg.Rows(i).Cells(10).Value = Math.Round(Myconn.cur.Current("Depit_amount"), 2)
            drg.Rows(i).Cells(11).Value = Math.Round((Val(Myconn.cur.Current("Absent_em") * Myconn.cur.Current("Employee_Salary")) / 27), 2)
            drg.Rows(i).Cells(12).Value = Myconn.cur.Current("Insurance")
            drg.Rows(i).Cells(13).Value = Myconn.cur.Current("Qard_Kist")

            drg.Rows(i).Cells(9).Style.BackColor = Color.Pink
            drg.Rows(i).Cells(10).Style.BackColor = Color.Pink
            drg.Rows(i).Cells(11).Style.BackColor = Color.Pink
            drg.Rows(i).Cells(12).Style.BackColor = Color.Pink
            drg.Rows(i).Cells(13).Style.BackColor = Color.Pink
            '-----------------------------------------------------------------------------------------------

            drg.Rows(i).Cells(14).Value = Math.Round((Myconn.cur.Current("Extra_work") + Myconn.cur.Current("Employee_Salary") + Myconn.cur.Current("Badal") + Myconn.cur.Current("Employee_Gift") + Myconn.cur.Current("Zyada")), 2)
            drg.Rows(i).Cells(14).Style.BackColor = Color.LightGreen
            drg.Rows(i).Cells(15).Value = Math.Round((drg.Rows(i).Cells(9).Value + drg.Rows(i).Cells(10).Value + drg.Rows(i).Cells(11).Value + drg.Rows(i).Cells(12).Value + drg.Rows(i).Cells(13).Value), 2)
            drg.Rows(i).Cells(15).Style.BackColor = Color.Pink

            drg.Rows(i).Cells(16).Value = Math.Round((drg.Rows(i).Cells(14).Value - drg.Rows(i).Cells(15).Value), 2)

            Myconn.cur.Position += 1
        Next
        Myconn.Sum_drg(drg, 16, Label1, Label2)
        GroupBox2.Text = "إجمالي المرتبات خلال شهر " & Format(CDate(Dat), "MMM yyyy") & " : "
    End Sub
    Private Sub frmEmployees_Report_Salary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label23.Left = 0
        Label23.Width = Me.Width
        txt1.Text = Format(Now.Date, "yyyy/MM")

        Fillgrd()
        txt1.Text = Nothing
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Fillgrd()
    End Sub

    Private Sub drg_MouseClick(sender As Object, e As MouseEventArgs) Handles drg.MouseClick
        If (e.Button = Windows.Forms.MouseButtons.Right) Then
            ContextMenuStrip1.Show(Me, e.Location)
        End If
    End Sub

    Private Sub Employee_Salary_Click(sender As Object, e As EventArgs) Handles Employee_Salary.Click
        Dim frm As New frmEmployee_Salary(Me.EmpID)
        frm.Show()
        frm.drg.ClearSelection()
        For W As Integer = 0 To frm.drg.Rows.Count - 1
            If frm.drg.Rows(W).Cells(2).Value.Equals(Me.EmpID) Then
                frm.drg.Rows(W).Cells(2).Selected = True
                frm.drg.CurrentCell = frm.drg.SelectedCells(2)
                Exit For
            End If
        Next
        frm.drg_CellClick(Nothing, Nothing)
    End Sub

    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        EmpID = drg.CurrentRow.Cells(2).Value
    End Sub

    Private Sub Employee_Extra_Click(sender As Object, e As EventArgs) Handles Employee_Extra.Click
        Dim frm As New frmEmployee_Extra_work(EmpID, Dat)
        frm.Show()
        frm.cbo_Employee.SelectedValue = EmpID
    End Sub

    Private Sub Employee_In_Out_Click(sender As Object, e As EventArgs) Handles Employee_In_Out.Click
        Dim frm As New frmEmployee_Go_Went(EmpID, Dat)
        frm.Text = "حضور وانصراف الموظف " & drg.CurrentRow.Cells(1).Value & " خلال شهر " & Format(CDate(Dat), "MMM yyyy")
        frm.Show()
        frm.fillgrd2()

    End Sub

    Private Sub Employee_Badal_Click(sender As Object, e As EventArgs) Handles Employee_Badal.Click
        Dim frm As New frmEmployee_Badal(EmpID)

        frm.Text = "بدلات الموظف " & drg.CurrentRow.Cells(1).Value & " خلال شهر " & Format(CDate(Dat), "MMM yyyy")
        frm.Show()
        frm.cbo_Employee.SelectedValue = EmpID
        frm.fillgrd2()
    End Sub

    Private Sub Employee_Gift_Click(sender As Object, e As EventArgs) Handles Employee_Gift.Click
        Dim frm As New frmEmployee_Gift(EmpID)

        frm.Text = "حوافز الموظف " & drg.CurrentRow.Cells(1).Value & " خلال شهر " & Format(CDate(Dat), "MMM yyyy")
        frm.Show()
        frm.cbo_Employee.SelectedValue = EmpID
        frm.fillgrd2()
    End Sub

    Private Sub Employee_Zyzda_Click(sender As Object, e As EventArgs) Handles Employee_Zyzda.Click
        Dim frm As New frmEmployee_Zyadate(EmpID)
        frm.Text = "زيادات مرتب الموظف " & drg.CurrentRow.Cells(1).Value
        frm.Show()
        frm.cbo_Employee.SelectedValue = EmpID
        frm.Fillgrd()
    End Sub

    Private Sub Employee_EZN_Click(sender As Object, e As EventArgs) Handles Employee_EZN.Click
        Dim frm As New frmEmployee_Ezn(EmpID)
        frm.Text = "أذونات الموظف " & drg.CurrentRow.Cells(1).Value & " خلال شهر " & Format(CDate(Dat), "MMM yyyy")
        frm.Show()
        frm.cbo_Employee.SelectedValue = EmpID
        frm.Fillgrd2()
    End Sub

    Private Sub Employee_Absent_Click(sender As Object, e As EventArgs) Handles Employee_Absent.Click
        Dim frm As New frmEmployee_Go_Went(EmpID, Dat)
        frm.Text = "غياب الموظف " & drg.CurrentRow.Cells(1).Value & " خلال شهر " & Format(CDate(Dat), "MMM yyyy")
        frm.Show()
        frm.fillgrd3()
    End Sub

    Private Sub Employee_Holiday_Click(sender As Object, e As EventArgs) Handles Employee_Holiday.Click
        Dim frm As New frmEmployee_holiday(EmpID)
        frm.Text = "اجازات الموظف " & drg.CurrentRow.Cells(1).Value & " خلال شهر " & Format(CDate(Dat), "MMM yyyy")
        frm.Show()
        frm.cbo_Employee.SelectedValue = EmpID
        frm.fillgrd2()
    End Sub

    Private Sub Employee_Geza_Click(sender As Object, e As EventArgs) Handles Employee_Geza.Click
        Dim frm As New frmEmployee_Depit(EmpID)
        frm.Text = "جزاءات الموظف " & drg.CurrentRow.Cells(1).Value & " خلال شهر " & Format(CDate(Dat), "MMM yyyy")
        frm.Show()
        frm.cbo_Employee.SelectedValue = EmpID
        frm.Fillgrd2()
    End Sub

    Private Sub Employee_Insurance_Click(sender As Object, e As EventArgs) Handles Employee_Insurance.Click
        Dim frm As New frmEmployee_insurance(EmpID)
        frm.Text = "تأمينات الموظف " & drg.CurrentRow.Cells(1).Value & " خلال عام " & Format(CDate(Dat), "yyyy")
        frm.Show()
        frm.cbo_Employee.SelectedValue = EmpID
        frm.Fillgrd2()
    End Sub

End Class