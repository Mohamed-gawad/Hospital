

Public Class frmEmployee_follow
    Dim Myconn As New connect
    Sub Fillgrd() ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select a.EmployeeName,a.EmployeeID,j.jobname,S.Employee_Salary,s.Work_hours,isnull((isnull(N.late_In_Out,0) + isnull(z.late_Ezn,0) ),0) as Late,
                             isnull(g.Depit_number,0) as Depit_number ,isnull(b.Absent_em,0) as Absent_em,isnull(H.Holiday_number,0) as Holiday_number,
                             ISNULL(f.Employee_Gift,0) as Employee_Gift , ISNULL(l.Badal,0) as Badal,ISNULL(Y.Zyada,0) as Zyada,ISNULL(c.Insurance,0) as Insurance,
                             ISNULL(x.Extra_work,0) as Extra_work,ISNULL(Ezn_hours,0) as Ezn_hours
                             from Employees a
                            left join Jobs j on a.JobID = j.jobID
                            left join Employees_Salary S on a.EmployeeID = S.EmployeeID
                            left join (select isnull(sum(Late),0 ) as late_In_Out , EmployeeID from [dbo].[Employees_In_Out] group by EmployeeID,cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) having cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "') N
                            on a.EmployeeID = n.EmployeeID
                            left join (select isnull(sum(Ezn_late),0 ) as late_Ezn ,isnull(sum(Ezn_hours),0 ) as Ezn_hours, EmployeeID from [dbo].[Employees_Ezn] group by EmployeeID,cast(DATEPART(yyyy,Ezn_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Ezn_date),'00') as varchar(2)) having cast(DATEPART(yyyy,Ezn_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Ezn_date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "') Z
                            on a.EmployeeID = Z.EmployeeID
                            left join (select isnull(sum(Depit_number),0 ) as Depit_number , EmployeeID from [dbo].[Employees_Depit] group by EmployeeID,cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) having cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "' ) g
                            on a.EmployeeID = g.EmployeeID
                            left join (select isnull(count(Holiday_ID),0 ) as Absent_em , EmployeeID from [dbo].[Employees_In_Out] group by EmployeeID,Holiday_ID,cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2))  having Holiday_ID = 6 and  cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "' ) b
                            on a.EmployeeID = b.EmployeeID
                            left join (select isnull(sum(Holiday_number),0 ) as Holiday_number , EmployeeID from [dbo].[Employees_Holiday] group by EmployeeID,cast(DATEPART(yyyy,H_date_begin) as varchar(4)) + '/' + cast(format(DATEPART(MM,H_date_begin),'00') as varchar(2)) having cast(DATEPART(yyyy,H_date_begin) as varchar(4)) + '/' + cast(format(DATEPART(MM,H_date_begin),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "') H
                            on a.EmployeeID = h.EmployeeID
                            left join (select isnull(sum(Employee_Gift),0 ) as Employee_Gift , EmployeeID from [dbo].[Employees_Gift] group by EmployeeID,cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) having cast(DATEPART(yyyy,Day_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Day_Date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "' ) F
                            on a.EmployeeID = F.EmployeeID
                            left join (select isnull(sum(Badal),0 ) as Badal , EmployeeID from [dbo].[Employees_Badal] group by EmployeeID, cast(DATEPART(yyyy,Badal_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Badal_date),'00') as varchar(2)) having cast(DATEPART(yyyy,Badal_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Badal_date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "') L
                            on a.EmployeeID = L.EmployeeID
                            left join (select isnull(max(Zyada),0 ) as Zyada , EmployeeID from [dbo].[Employees_Zyada] group by EmployeeID,DATEPART(yyyy,Zyada_date) having DATEPART(yyyy,Zyada_date)  = '" & Format(CDate(txt1.Text), "yyyy") & "' ) Y
                            on a.EmployeeID = Y.EmployeeID
                            left join (select isnull(max(Insurance_amount),0 ) as Insurance , EmployeeID from [dbo].[Employees_Insurance] group by EmployeeID, DATEPART(yyyy,Insurance_Date) having DATEPART(yyyy,Insurance_Date) = '" & Format(CDate(txt1.Text), "yyyy") & "') C
                            on a.EmployeeID = C.EmployeeID
                            left join (select isnull(sum(Hours_number),0 ) as Extra_work , EmployeeID from [dbo].[Employees_Extra_work] group by EmployeeID,cast(DATEPART(yyyy,Work_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Work_date),'00') as varchar(2)) having cast(DATEPART(yyyy,Work_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Work_date),'00') as varchar(2)) = '" & Format(CDate(txt1.Text), "yyyy/MM") & "' ) X
                            on a.EmployeeID = X.EmployeeID where a.State_ID = 1", "Employees_In_Out", Me)

        If Myconn.cur.Count = 0 Then Return
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("EmployeeID")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("jobname")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Employee_Salary")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Work_hours")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Late") - Val(Val(If(IsDBNull(Myconn.cur.Current("Absent_em")), 0, Myconn.cur.Current("Absent_em")) * Val(If(IsDBNull(Myconn.cur.Current("Work_hours")), 0, Myconn.cur.Current("Work_hours"))) * 60))
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Depit_number")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Absent_em")
            drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Holiday_number")
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Ezn_hours")
            drg.Rows(i).Cells(11).Value = Myconn.cur.Current("Extra_work")
            drg.Rows(i).Cells(12).Value = Myconn.cur.Current("Employee_Gift")
            drg.Rows(i).Cells(13).Value = Myconn.cur.Current("Badal")
            drg.Rows(i).Cells(14).Value = Myconn.cur.Current("Zyada")
            drg.Rows(i).Cells(15).Value = Myconn.cur.Current("Insurance")
            Myconn.cur.Position += 1
        Next
    End Sub
    Private Sub frmEmployee_follow_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label23.Left = 0
        Label23.Width = Me.Width
        txt1.Text = Format(Now.Date, "yyyy/MM")

        Fillgrd()
        txt1.Text = Nothing

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Fillgrd()
    End Sub
End Class