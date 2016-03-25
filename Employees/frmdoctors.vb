Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmdoctors
    Dim Myconn As New connect
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.Filldataset("select * from Recipient", "Recipient", Me)
        Myconn.ClearAllText(Me, GroupBox1)
        Myconn.Autonumber("RecipientID", "Recipient", txtID, Me)
        cbo.SelectedValue = 0

    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Myconn.Filldataset("select * from Doctor", "Doctor", Me)
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(1), Myconn.cur.Current(0), Myconn.cur.Current(2)})
            Myconn.cur.Position += 1
        Next
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Dim Myfields() As String = {"DoctorsID", "DoctorsName"}
        Dim Mytxt() As TextBox = {txtID, txtName}
        Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
        Myconn.comboBinding("specializationID", cbo)
    End Sub
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset("select * from Doctors where DoctorsID =" & CInt(drg.CurrentRow.Cells(2).Value), "Doctors", Me)
        Binding()
    End Sub
    Private Sub frmdoctors_Load(sender As Object, e As EventArgs) Handles Me.Load
        Fillgrd()
        btnSave.Enabled = False
        txtName.Focus()
        Myconn.Fillcombo("select * from specialization where kind ='k'", "specialization", "specializationID", "specialization", Me, cbo)
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        btnSave.Enabled = True
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtName.Text = "" Then
            MessageBox.Show("من فضلك أدخل إسم القسم", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        Dim XX() As String = {txtID.Text, "'" & txtName.Text & "'", "'" & cbo.SelectedValue & "'"}
        Myconn.AddNewRecord("Doctors", XX)

        Dim yy() As String = {txtID.Text, "'" & txtName.Text & "'", "'" & Label4.Text & "'"}
        Myconn.AddNewRecord("Recipient", yy)

        Fillgrd()
        Myconn.ClearAllText(Me, GroupBox1)
        btnSave.Enabled = False
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Dim Values() As String = {"'" & txtName.Text & "'", "'" & cbo.SelectedValue & "'"}
        Dim Mycolumes() As String = {"DoctorsName", "specializationID"}
        Myconn.UpdateRecord("Doctors", Mycolumes, Values, "DoctorsID", txtID.Text)
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click

        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then

            Return

        Else
            Myconn.DeleteRecord("Doctors", "DoctorsID", CInt(drg.CurrentRow.Cells(2).Value))
            Myconn.ClearAllText(Me, GroupBox1)
            Fillgrd()
        End If


    End Sub
    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtName.Enter
        Myconn.langAR()
    End Sub
    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        Myconn.Arabiconly(e)

    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Print_drg(drg)
    End Sub
    Sub Print_drg(dgr As DataGridView)
        Dim rpt As New rpt_Doctors
        Dim table As New DataTable
        For i As Integer = 1 To drg.ColumnCount
            Dim x As String
            x = Format(i, "00")
            table.Columns.Add(x)
        Next

        For Each dr As DataGridViewRow In drg.Rows
            table.Rows.Add()
            For i As Integer = 0 To dgr.ColumnCount - 1
                table.Rows(dr.Index)(i) = dr.Cells(i).Value
            Next
        Next

        rpt.SetDataSource(table)
        rpt.SetParameterValue("Report_label", My.Settings.H_Name)
        rpt.PrintOptions.PrinterName = "Microsoft Print to PDF"
        'frmReportViewer.CrystalReportViewer1.ReportSource = rpt
        'frmReportViewer.Show()
        rpt.PrintToPrinter(1, False, 0, 0)
    End Sub


End Class