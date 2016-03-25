Imports System.Data.SqlClient
Imports System.Globalization
Public Class frmDrage_Store_Incubator
    Dim fin As Boolean
    Dim Myconn As New connect
    Dim x, Y, AM2, V As Integer
    Dim StockID As Integer
    Dim V2, V1 As Decimal
    Dim st, Unit1 As String
    Dim A, Amount As Double
    Sub TextBindingdata(frm As Form, grb As GroupBox, Fields() As String, txt() As TextBox)

        For i As Integer = 0 To Fields.Count - 1
            txt(i).DataBindings.Clear()
            txt(i).DataBindings.Add("text", Myconn.dv1, Fields(i))
        Next
    End Sub
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
        Myconn.Filldataset4("select  isnull(max(Bill_ID),0) as Bill_ID from Stocks_Sales where Stock_ID =" & CInt(StockID), "Stocks_Purchases", Me)
        txtBill_ID.Text = Myconn.cur4.Current("Bill_ID") + 1
        drg.Rows.Clear()
    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        Select Case Y
            Case 0
                st = "where a.Bill_ID =" & CInt(txtBill_ID.Text) & "and a.Stock_ID = " & StockID & ""
            Case 1
                st = "where a.Bill_ID =" & CInt(txtSearch.Text) & "and a.Stock_ID = " & StockID & ""

        End Select
        Myconn.Filldataset("Select m.Max_Unit_Name,n.Min_Unit_Name,a.Time_Add,a.Bill_ID,a.Bill_Date ,c.Co_Name, b.Drug_Name,a.Drug_ID,a.Drug_exp,
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
        If Myconn.cur.Count = 0 Then Return
        V1 = 0
        V2 = 0
        AM2 = 0
        For i As Integer = 0 To Myconn.cur.Count - 1

            If Myconn.cur.Current("Unit_Kind") = 0 Then
                Unit1 = Myconn.cur.Current("Min_Unit_Name")
                AM2 = Myconn.cur.Current("Amount") * Myconn.cur.Current("Min_Unit_number")
            ElseIf Myconn.cur.Current("Unit_Kind") = 1 Then
                Unit1 = Myconn.cur.Current("Max_Unit_Name")
                AM2 = Myconn.cur.Current("Amount") * 1
            End If
            drg.Rows.Add()
            drg.Rows(i).Cells(0).Value = i + 1
            drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Bill_Date")
            drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Time_Add")
            drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Co_Name")
            drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Drug_Name")
            drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Drug_ID")
            drg.Rows(i).Cells(6).Value = Myconn.cur.Current("Drug_exp")
            drg.Rows(i).Cells(7).Value = Math.Round(AM2)
            drg.Rows(i).Cells(8).Value = Unit1
            drg.Rows(i).Cells(9).Value = Myconn.cur.Current("Drug_Price")
            drg.Rows(i).Cells(10).Value = Myconn.cur.Current("Total_Price")
            drg.Rows(i).Cells(11).Value = Myconn.cur.Current("GroupName")
            drg.Rows(i).Cells(12).Value = Myconn.cur.Current("Users")
            drg.Rows(i).Cells(13).Value = Myconn.cur.Current("Employee")
            drg.Rows(i).Cells(14).Value = Myconn.cur.Current("ID")
            drg.Rows(i).Cells(15).Value = Myconn.cur.Current("State")

            If drg.Rows(i).Cells(15).Value = True Then
                drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                V1 += CDec(drg.Rows(i).Cells(10).Value)
            Else
                drg.Rows(i).DefaultCellStyle.BackColor = Color.Red
                V2 += CDec(drg.Rows(i).Cells(10).Value)
            End If
            Myconn.cur.Position += 1
        Next
        Label38.Text = V1
        Label37.Text = "( " & clsNumber.nTOword(Label38.Text) & " )"
        Label37.Left = Label38.Left - (Label37.Width + 20)

        Label8.Text = V2
        Label3.Text = "( " & clsNumber.nTOword(Label8.Text) & " )"
        Label3.Left = Label8.Left - (Label3.Width + 20)

        Myconn.DataGridview_MoveLast(drg, 7)
    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
        Select Case x

            Case 0
                Myconn.Filldataset("select a.Drug_ID,a.Drug_Name,a.Drug_Price ,a.Parcod,a.Min_Unit_number,d.Max_Unit_Name,e.Min_Unit_Name,isnull(b.Amount,0) as Drug_Purchases,
                                    (c.Drug_exp) as EXP_Sales,(b.Drug_exp) as EXP_Puer,isnull(c.Amount,0) as Sales,(isnull(b.Amount,0) - isnull(c.Amount,0)) as rest  from Drugs a
                                    left join (select Drug_ID,Drug_exp,sum(Amount) as Amount,state from [dbo].[Stocks_Purchases] GROUP BY Drug_ID,Drug_exp,state,Stock_ID having state = 'true' and Stock_ID = " & StockID & ")b
                                    on a.Drug_ID = b.Drug_ID
                                    left join (select Drug_ID,Drug_exp,sum(Amount) as Amount,state from [dbo].[Stocks_Sales] GROUP BY Drug_ID,Drug_exp,state,Stock_ID having state = 'true' and Stock_ID = " & StockID & " ) c
                                    on a.Drug_ID = c.Drug_ID and b.Drug_exp=c.Drug_exp
                                    left join Max_Unit d on a.Max_UnitID=d.Max_UnitID
                                    left join Min_Unit e on a.Min_UnitID=e.Min_UnitID
                                    GROUP BY  a.Drug_ID,a.Drug_Name,b.Amount,c.Amount,a.Drug_Price,a.Parcod,c.Drug_exp,b.Drug_exp,a.Min_Unit_number,d.Max_Unit_Name,e.Min_Unit_Name
                                    having (isnull(b.Amount,0) - isnull(c.Amount,0)) >= 0 and a.Drug_ID =" & CInt(cbo_Drug.SelectedValue), "Stocks_Sales", Me)

                Dim Myfields() As String = {"Drug_Name", "Drug_Price", "Rest"}
                Dim Mytxt() As TextBox = {txtDrug, txtPublic_Price, txtStock_amount}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)

                If Myconn.cur.Current("rest") = 0 Then
                    txtStock_amount.BackColor = Color.Red
                Else
                    txtStock_amount.BackColor = Color.White
                    'MsgBox(" .. الصنف غير متوفر")

                End If

                drg_Exp.Rows.Clear()
                Amount = 0
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg_Exp.Rows.Add()
                    drg_Exp.Rows(i).Cells(0).Value = i + 1
                    drg_Exp.Rows(i).Cells(1).Value = Myconn.cur.Current("EXP_Puer")
                    drg_Exp.Rows(i).Cells(2).Value = Myconn.cur.Current("rest")
                    Amount += Myconn.cur.Current("rest")
                    Myconn.cur.Position += 1
                Next

                Dim C As Double, B, E As Integer

                If Amount = 0 Then
                    txtStock_amount.Text = 0

                ElseIf Amount <> 0

                    A = Math.Round(Amount, 2)
                    B = Fix(A)
                    C = Math.Round((Val(A) - Val(B)), 2)
                    E = Myconn.cur.Current("Min_Unit_number")

                    If B > 0 And C = 0 Then
                        txtStock_amount.Text = B & " " & Myconn.cur.Current("Max_Unit_Name")
                    ElseIf B > 0 And C > 0
                        txtStock_amount.Text = B & " " & Myconn.cur.Current("Max_Unit_Name") & " و " & Math.Round((C * E), 0) & " " & Myconn.cur.Current("Min_Unit_Name")
                    ElseIf B = 0 And C > 0
                        txtStock_amount.Text = Math.Round((C * E), 0) & " " & Myconn.cur.Current("Min_Unit_Name")

                    ElseIf B < 0 And C = 0
                        txtStock_amount.Text = B & " " & Myconn.cur.Current("Max_Unit_Name")
                    ElseIf B < 0 And C < 0
                        txtStock_amount.Text = B & " " & Myconn.cur.Current("Max_Unit_Name") & " و " & Math.Round((C * E), 0) & " " & Myconn.cur.Current("Min_Unit_Name")
                    ElseIf B = 0 And C < 0
                        txtStock_amount.Text = Math.Round((C * E), 0) & " " & Myconn.cur.Current("Min_Unit_Name")

                    End If

                End If

            Case 1
                Myconn.Filldataset1("select a.Min_Unit_price,a.Min_UnitID,a.Max_UnitID,a.Min_Unit_price,a.Drug_Price,c.Min_Unit_Name,b.Max_Unit_Name,a.Min_Unit_number from Drugs a
                             left join Max_Unit b on a.Max_UnitID=b.Max_UnitID
                             left join Min_Unit c on a.Min_UnitID=c.Min_UnitID
                            where Drug_ID =" & CInt(cbo_Drug.SelectedValue), "Drugs", Me)
                Select Case V
                    Case 0
                        Dim Myfields() As String = {"Min_Unit_Name", "Min_Unit_price", "Min_UnitID", "Min_Unit_number"}
                        Dim Mytxt() As TextBox = {txtKind_Unit, txtPrice, txtUnit_ID, txtUnit_Number}
                        TextBindingdata(Me, GroupBox3, Myfields, Mytxt)
                    Case 1
                        Dim Myfields() As String = {"Max_Unit_Name", "Drug_Price", "Max_UnitID"}
                        Dim Mytxt() As TextBox = {txtKind_Unit, txtPrice, txtUnit_ID}
                        TextBindingdata(Me, GroupBox3, Myfields, Mytxt)
                        txtUnit_Number.Text = 1
                End Select
        End Select
    End Sub
    Sub Save_To_Stock()
        Dim sql As String = "INSERT INTO Stocks_Sales (Stock_ID,Bill_ID,Bill_Date,Time_Add,Drug_ID,Drug_exp,Drug_Price,Amount,Total_Price,Unit,Unit_Kind,EmployeeID,Patient_ID,DoctorsID,CerviceID,specializationID,RecordID,Users_ID,State)
                                                VALUES(@Stock_ID,@Bill_ID,@Bill_Date,@Time_Add,@Drug_ID,@Drug_exp,@Drug_Price,@Amount,@Total_Price,@Unit,@Unit_Kind,@EmployeeID,@Patient_ID,@DoctorsID,@CerviceID,@specializationID,@RecordID,@Users_ID,@State)"

        Dim w As Integer = Nothing
        Myconn.cmd = New SqlCommand(sql, Myconn.conn)
        With Myconn.cmd.Parameters
            .Add("@Stock_ID", SqlDbType.Int).Value = StockID
            .Add("@Bill_ID", SqlDbType.Int).Value = txtBill_ID.Text
            .Add("@Bill_Date", SqlDbType.NChar).Value = Format(CDate(dtb.Text), "yyyy/MM/dd")
            .Add("@Time_Add", SqlDbType.NChar).Value = Label20.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
            .Add("@Drug_exp", SqlDbType.NChar).Value = drg_Exp.CurrentRow.Cells(1).Value
            .Add("@Drug_Price", SqlDbType.Decimal).Value = txtPrice.Text
            .Add("@Amount", SqlDbType.Decimal).Value = Math.Round((Val(txtAmount.Text) / Val(txtUnit_Number.Text)), 2)
            .Add("@Unit", SqlDbType.Int).Value = txtUnit_ID.Text
            .Add("@Unit_Kind", SqlDbType.Int).Value = cbo_Unit.SelectedIndex
            .Add("@Total_Price", SqlDbType.Decimal).Value = Val(txtPrice.Text) * Val(txtAmount.Text)
            .Add("@EmployeeID", SqlDbType.Int).Value = cboEmployee.SelectedValue
            .Add("@Patient_ID", SqlDbType.Int).Value = cboPatient.SelectedValue
            .Add("@DoctorsID", SqlDbType.Int).Value = cboDoctor.SelectedValue
            .Add("@CerviceID", SqlDbType.Int).Value = cboOperation.SelectedValue
            .Add("@specializationID", SqlDbType.Int).Value = cboKissm.SelectedValue
            .Add("@RecordID", SqlDbType.Int).Value = w
            .Add("@Users_ID", SqlDbType.Int).Value = My.Settings.user_ID
            .Add("@State", SqlDbType.Bit).Value = 1
        End With
        Try
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            'Return
        End Try
        x = 0
        Binding()
    End Sub
    Private Sub frmDrage_Store_Incubator_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Myconn.Fillcombo("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug)
        StockID = 4
    End Sub
    Private Sub frmDrage_Store_Incubator_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Enter AndAlso e.Control = True Then
            btnSave_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.N AndAlso e.Control = True Then
            btnNew_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub
    Private Sub frmDrage_Store_Incubator_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label1.Left = 0
        Label1.Width = Me.Width
        Me.KeyPreview = True
        StockID = 4
        fin = False
        Myconn.Fillcombo1("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cboEmployee)
        Myconn.Fillcombo2("select * from Patient", "Patient", "patient_ID", "PatientName", Me, cboPatient)
        Myconn.Fillcombo3("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug)
        fin = False
        Myconn.Fillcombo4("select * from specialization WHERE kind like 'k'", "specialization ", "specializationID", "specialization", Me, cboKissm)
        fin = True
        Timer1.Start()
    End Sub
#Region "TextBox"
    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs)
        Label21.Text = Val(txtAmount.Text) * Val(txtPrice.Text)
    End Sub

#End Region
#Region "Button"
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        GroupBox1.Enabled = True
        txtPrice.Text = ""
        txtAmount.Text = ""
        cboEmployee.SelectedIndex = -1
        cbo_Drug.SelectedIndex = -1
        cboDoctor.SelectedIndex = -1
        cboOperation.SelectedIndex = -1
        cboPatient.SelectedIndex = -1
        cboKissm.SelectedIndex = -1
        dtb.Text = Today

        drg.Rows.Clear()
        txtSearch.Text = ""


    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If CDbl(txtBill_ID.Text) <= 0 Or Nothing Then
                ErrorProvider1.SetError(txtBill_ID, "أدخل رقم الفاتورة")
                Return
            End If
        Catch ex As Exception
            ErrorProvider1.SetError(txtBill_ID, "أدخل رقم الفاتورة")
            Return
        End Try


        If CDbl(txtAmount.Text) <= 0 Then
            ErrorProvider1.SetError(txtAmount, "أدخل الكمية")
            Return
        End If

        For Each txt As Control In GroupBox4.Controls
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
        ElseIf drg_Exp.CurrentRow.Cells(2).Value < Math.Round((Val(txtAmount.Text) / Val(txtUnit_Number.Text)), 2) Then
            MessageBox.Show("الكمية لا تسمح ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        Save_To_Stock()
        Y = 0
        Fillgrd()
        'Myconn.Sum_drg(drg, 8, Label38, Label37)
        GroupBox1.Enabled = False

        txtAmount.Text = 0
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub
    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click
        Dim sql As String = "Update Stocks_Sales set Stock_ID=@Stock_ID,Bill_ID=@Bill_ID,Bill_Date=@Bill_Date,
                             Time_Add=@Time_Add,Drug_ID=@Drug_ID,Drug_exp=@Drug_exp,Drug_Price=@Drug_Price,Amount=@Amount,Total_Price=@Total_Price,
                             Unit=@Unit,Unit_Kind=@Unit_Kind,EmployeeID=@EmployeeID,Patient_ID=@Patient_ID,DoctorsID=@DoctorsID,CerviceID=@CerviceID,
                             specializationID=@specializationID,Users_ID=@Users_ID where ID =@ID"


        Myconn.cmd = New SqlCommand(sql, Myconn.conn)
        With Myconn.cmd.Parameters
            .Add("@Stock_ID", SqlDbType.Int).Value = StockID
            .Add("@Bill_ID", SqlDbType.Int).Value = txtBill_ID.Text
            .Add("@Bill_Date", SqlDbType.NChar).Value = Format(CDate(dtb.Text), "yyyy/MM/dd")
            .Add("@Time_Add", SqlDbType.NChar).Value = Label20.Text
            .Add("@Drug_ID", SqlDbType.Int).Value = cbo_Drug.SelectedValue
            .Add("@Drug_exp", SqlDbType.NChar).Value = drg_Exp.CurrentRow.Cells(1).Value
            .Add("@Drug_Price", SqlDbType.Decimal).Value = txtPrice.Text
            .Add("@Amount", SqlDbType.Decimal).Value = Math.Round((Val(txtAmount.Text) / Val(txtUnit_Number.Text)), 2)
            .Add("@Unit", SqlDbType.Int).Value = txtUnit_ID.Text
            .Add("@Unit_Kind", SqlDbType.Int).Value = cbo_Unit.SelectedIndex
            .Add("@Total_Price", SqlDbType.Decimal).Value = Val(txtPrice.Text) * Val(txtAmount.Text)
            .Add("@EmployeeID", SqlDbType.Int).Value = cboEmployee.SelectedValue
            .Add("@Patient_ID", SqlDbType.Int).Value = cboPatient.SelectedValue
            .Add("@DoctorsID", SqlDbType.Int).Value = cboDoctor.SelectedValue
            .Add("@CerviceID", SqlDbType.Int).Value = cboOperation.SelectedValue
            .Add("@specializationID", SqlDbType.Int).Value = cboKissm.SelectedValue
            .Add("@Users_ID", SqlDbType.Int).Value = My.Settings.user_ID
            .Add("@ID", SqlDbType.Int).Value = drg.CurrentRow.Cells(14).Value
        End With
        Try
            If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
            Myconn.conn.Open()
            Myconn.cmd.ExecuteNonQuery()
            Myconn.conn.Close()
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
            'Return
        End Try
        x = 0
        Binding()
        Y = 0
        Fillgrd()


        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Myconn.DeleteRecord("Stocks_Sales", "ID", drg.CurrentRow.Cells(14).Value)

            Y = 0
            Fillgrd()
            x = 0
            Binding()

        End If
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Dim sql As String = "Update  Stocks_Sales set State = @State where ID = @ID"
        Myconn.cmd = New SqlCommand(sql, Myconn.conn)

        If drg.CurrentRow.Cells(15).Value = True Then
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 0
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(14).Value)
            End With
        Else
            With Myconn.cmd.Parameters
                .Add("@State", SqlDbType.Bit).Value = 1
                .Add("@ID", SqlDbType.Int).Value = CInt(drg.CurrentRow.Cells(14).Value)
            End With
        End If

        If Myconn.conn.State = ConnectionState.Open Then Myconn.conn.Close()
        Myconn.conn.Open()
        Myconn.cmd.ExecuteNonQuery()
        Myconn.conn.Close()
        Y = 0
        Fillgrd()
        x = 0
        Binding()
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txtSearch.Text = "" Then
            drg.Rows.Clear()
            Return
        End If
        Y = 1
        Fillgrd()
        If Myconn.cur.Count = 0 Then Return
        txtBill_ID.Text = Myconn.cur.Current("Bill_ID")
        dtb.Text = Myconn.cur.Current("Bill_Date")
        cboEmployee.SelectedValue = Myconn.cur.Current("EmployeeID")
        cboPatient.SelectedValue = Myconn.cur.Current("Patient_ID")
        cboKissm.SelectedValue = Myconn.cur.Current("specializationID")
        cboDoctor.SelectedValue = Myconn.cur.Current("DoctorsID")
        cboOperation.SelectedValue = Myconn.cur.Current("CerviceID")
        GroupBox1.Enabled = False
    End Sub
#End Region
#Region "ComboBox"
    Private Sub cboKissm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKissm.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        If cboKissm.SelectedIndex = -1 Then Return
        Myconn.Fillcombo5("select * from Doctors where specializationID =" & CInt(cboKissm.SelectedValue), "Doctors", "DoctorsID", "DoctorsName", Me, cboDoctor)
        Myconn.Fillcombo6("select * from Cervices where specializationID =" & CInt(cboKissm.SelectedValue), "Cervices", "CerviceID", "CerviceName", Me, cboOperation)
        fin = True
    End Sub
    Private Sub cbo_Drug_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Drug.SelectedIndexChanged
        ErrorProvider1.Clear()
        If Not fin Then Return
        If cbo_Drug.SelectedIndex = -1 Then Return
        x = 0
        Binding()
        cbo_Unit_SelectedIndexChanged(Nothing, Nothing)
    End Sub
    Private Sub cbo_Unit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Unit.SelectedIndexChanged
        ErrorProvider1.Clear()
        V = cbo_Unit.SelectedIndex
        x = 1
        Binding()
        'UN = Math.Round((Val(txtAmount.Text) / Val(txtUnit_Number.Text)), 2)
        txtTotal_Price.Text = Math.Round((Val(txtAmount.Text) * Val(txtPrice.Text)), 2)
    End Sub
    Private Sub cbo_Drug_Enter(sender As Object, e As EventArgs) Handles cbo_Drug.Enter
        Myconn.langAR()
    End Sub
#End Region
    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset2("select * from Stocks_Sales where ID =" & CInt(drg.CurrentRow.Cells(14).Value), "Stocks_Sales", Me)
        cbo_Drug.SelectedValue = Myconn.cur2.Current("Drug_ID")
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label20.Text = TimeOfDay.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-eg"))
    End Sub
End Class