Public Class frmEmployee_Report
    Dim Myconn As New connect
    Dim st As String
    Dim X As Integer
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Select Case X
            Case 0
                If cboState.SelectedIndex < 0 Then
                    st = " where a.jobID =" & CInt(cboJobs.ComboBox.SelectedValue)
                Else
                    st = " where a.jobID =" & CInt(cboJobs.ComboBox.SelectedValue) & " and a.State_ID =" & CInt(cboState.ComboBox.SelectedValue)
                End If

            Case 1
                If cboState.SelectedIndex < 0 Then
                    st = Nothing
                Else
                    st = " where  a.State_ID =" & CInt(cboState.ComboBox.SelectedValue)
                End If

        End Select
        Myconn.Filldataset("select e.State_Name,j.jobname,s.Employee_Salary,s.Work_hours,a.State_ID,
                           a.EmployeeID,a.EmployeeName,a.EmployeeNID,a.Certificate from Employees a
                           left join Employees_Salary s on a.EmployeeID = s.EmployeeID   
                           left join Employees_Status e on a.State_ID = e.State_ID 
                           left join Jobs j on a.jobID = j.jobID " & st & " order by a.EmployeeID", "Employees", Me)

        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("EmployeeName")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("EmployeeID")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("EmployeeNID")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Certificate")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("jobname")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Employee_Salary")
            drg.Rows(i).Cells(7).Value = Myconn.cur.Current("Work_hours")
            drg.Rows(i).Cells(8).Value = Myconn.cur.Current("State_Name")

            If Myconn.cur.Current("State_ID") = 1 Then
                drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            ElseIf Myconn.cur.Current("State_ID") = 2 Then
                drg.Rows(i).DefaultCellStyle.BackColor = Color.Orange
            ElseIf Myconn.cur.Current("State_ID") = 3 Then
                drg.Rows(i).DefaultCellStyle.BackColor = Color.Red
            ElseIf Myconn.cur.Current("State_ID") = 5 Then
                drg.Rows(i).DefaultCellStyle.BackColor = Color.DeepPink
            ElseIf Myconn.cur.Current("State_ID") = 4 Then
                drg.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
            End If
            Myconn.cur.Position += 1
        Next
    End Sub
    Private Sub frmEmployee_Report_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label23.Left = 0
        Label23.Width = Me.Width
        Myconn.Fillcombo("select * from Jobs order by jobname", "jobs", "jobID", "jobname", Me, cboJobs.ComboBox)
        Myconn.Fillcombo("select * from Employees_Status order by State_Name", "Employees_Status", "State_ID", "State_Name", Me, cboState.ComboBox)
    End Sub
    Private Sub cboEmp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmp.SelectedIndexChanged
        Select Case cboEmp.SelectedIndex
            Case 0
                cboJobs.Visible = True
                X = 0
            Case 1
                cboJobs.Visible = False
                X = 1
        End Select
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Fillgrd()

    End Sub
End Class