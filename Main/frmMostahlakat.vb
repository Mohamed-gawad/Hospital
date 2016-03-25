Public Class frmMostahlakat
    Dim fin As Boolean
    Dim Myconn As New connect
    Sub NewRecord()                                           '''''''''''''''''''''''''''لعمل سجل جديد وإعطائه رقم
         Select TabControl1.SelectedIndex

            Case 0
                Myconn.Autonumber("DrugID", "Operations_Store", txtKind_ID, Me)
                txtKind_Name.Text = ""
                txtKind_Price.Text = ""
            Case 1
                Myconn.Autonumber("Bill_ID", "Operations_Store_Bills_Add", txtBill_ID, Me)
                txtPrice.Text = ""
                txtAmount.Text = ""
                cboEmployee.SelectedIndex = -1
                cboKind_Name.SelectedIndex = -1
                dtb.Text = Today
            Case 2
                Myconn.Autonumber("Bill_ID", "Operations_Store_Bills_Drage", txtBillDrag_ID, Me)
                txtPrice.Text = ""
                txtAmount.Text = ""
                cboEmployeeDrag.SelectedIndex = -1
                cbokind_Drag.SelectedIndex = -1
                cboDoctor.SelectedIndex = -1
                cboOperation.SelectedIndex = -1
                cboPatient.SelectedIndex = -1

                dtb2.Text = Today
        End Select

    End Sub
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 


        Select Case TabControl1.SelectedIndex

            Case 0
                drg.Rows.Clear()
                Myconn.Filldataset("select * from Operations_Store", "Operations_Store", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add(New String() {i + 1, Myconn.cur.Current(1), Myconn.cur.Current(0), Myconn.cur.Current(2), Myconn.cur.Current(3)})
                    Myconn.cur.Position += 1
                Next

            Case 1
                drg2.Rows.Clear()
                Myconn.Filldataset("select * from VStore_Bills WHERE Bill_id =" & txtBill_ID.Text & " order by ID", "VStore_Bills", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg2.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(7), Myconn.cur.Current(4), Myconn.cur.Current(5), Myconn.cur.Current(6), Myconn.cur.Current(9), Myconn.cur.Current(8), Myconn.cur.Current(11)})
                    Myconn.cur.Position += 1
                Next
                Myconn.DataGridview_MoveLast(drg2, 7)
                Myconn.Sum_drg(drg2, 6, Label35, Label34)
            Case 2
                drg3.Rows.Clear()
                Myconn.Filldataset("select * from VStore_Bills_Drage", "VStore_Bills_Drage", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg3.Rows.Add(New String() {Myconn.cur.Position.ToString + 1, Myconn.cur.Current(1), Myconn.cur.Current(2), Myconn.cur.Current(0), Myconn.cur.Current(16), Myconn.cur.Current(6), Myconn.cur.Current(7), Myconn.cur.Current(8), Myconn.cur.Current(9), Myconn.cur.Current(17)})
                    Myconn.cur.Position += 1
                Next
                Myconn.Sum_drg(drg3, 8, Label38, Label37)
                txtID.Text = Me.BindingContext(Myconn.dv).Current("ID")
                
            Case 3
                drg4.Rows.Clear()
                Myconn.Filldataset("select Bill_ID,Bill_Date,count(amount),sum(Total),EmployeeName from VStore_Bills  group by Bill_ID,Bill_Date,EmployeeName", "VStore_Bills", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg4.Rows.Add(New String() {Myconn.cur.Position.ToString + 1, Myconn.cur.Current(0), Myconn.cur.Current(1), Myconn.cur.Current(2), Myconn.cur.Current(3), Myconn.cur.Current(4)})
                    Myconn.cur.Position += 1
                Next
                Myconn.Sum_drg(drg4, 4, Label42, Label41)

            Case 4
                drg5.Rows.Clear()
                Myconn.Filldataset("select Bill_ID,Bill_Date,Name,patient_ID,count(amount),sum(Total),EmployeeName from VStore_Bills_Drage  group by Bill_ID,Bill_Date,EmployeeName,Name,patient_ID", "VStore_Bills", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg5.Rows.Add(New String() {Myconn.cur.Position.ToString + 1, Myconn.cur.Current(0), Myconn.cur.Current(1), Myconn.cur.Current(2), Myconn.cur.Current(3), Myconn.cur.Current(4), Myconn.cur.Current(5), Myconn.cur.Current(6)})
                    Myconn.cur.Position += 1
                Next
                Myconn.Sum_drg(drg5, 6, Label46, Label44)
            Case 5
              


            Case 6
                drg1.Rows.Clear()
                Myconn.Filldataset("select DrugName,DrugID,price, sum(amount) as totals, GroupName from Op_Store  group by DrugName,DrugID,GroupName,price", "Op_Store", Me)

                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg1.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(1), Myconn.cur.Current(2), Myconn.cur.Current(3), Myconn.cur.Current(2) * Myconn.cur.Current(3), Myconn.cur.Current(4)})
                    Myconn.cur.Position += 1
                Next
                Myconn.Sum_drg(drg1, 5, Label39, Label40)
        End Select

    End Sub
    Sub Binding() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة مربعات النصوص بالبيانات 
          Select TabControl1.SelectedIndex

            Case 0
                Dim Myfields() As String = {"DrugID", "DrugName", "DrugPrice"}
                Dim Mytxt() As TextBox = {txtKind_ID, txtKind_Name, txtKind_Price}
                Myconn.TextBindingdata(Me, GroupBox1, Myfields, Mytxt)
                cboGroup.DataBindings.Clear()
                cboGroup.DataBindings.Add("text", Myconn.dv, "GroupName")
            Case 1
                Myconn.Filldataset("select DrugName,DrugID,price, sum(amount) as totals, GroupName from Op_Store where DrugID =" & CInt(cboKind_Name.SelectedValue.ToString()) & " group by DrugName,DrugID,GroupName,price", "Op_Store", Me)

                If Myconn.dv.Count = 0 Then
                    txtStoreKind.Text = ""
                    txtStoreAmount.Text = ""
                    txtStorePrice.Text = ""
                End If
                Dim Myfields() As String = {"DrugName", "Price", "totals", "Price"}
                Dim Mytxt() As TextBox = {txtStoreKind, txtStorePrice, txtStoreAmount, txtPrice}
                Myconn.TextBindingdata(Me, GroupBox2, Myfields, Mytxt)
            Case 2
                Myconn.Filldataset("select DrugName,DrugID,price, sum(amount) as totals, GroupName from Op_Store where DrugID =" & CInt(cbokind_Drag.SelectedValue.ToString()) & " group by DrugName,DrugID,GroupName,price", "Op_Store", Me)
                If Myconn.dv.Count = 0 Then
                    txtKindStor_Drag.Text = ""
                    txtPriceStor_Drag.Text = ""
                    txtAmountStor_drag.Text = ""
                End If
                Dim Myfields() As String = {"DrugName", "Price", "totals", "Price"}
                Dim Mytxt() As TextBox = {txtKindStor_Drag, txtPriceStor_Drag, txtAmountStor_drag, txtPriceDrag}
                Myconn.TextBindingdata(Me, GroupBox3, Myfields, Mytxt)
        End Select
    End Sub
    Private Sub txtKind_Name_Enter(sender As Object, e As EventArgs) Handles txtKind_Name.Enter
        Myconn.langAR()
    End Sub
    Private Sub frmMostahlakat_Load(sender As Object, e As EventArgs) Handles Me.Load
        NewRecord()
        Fillgrd()
        Myconn.Autocomplete("Operations_Store", "DrugName", txtKind_Name)
        Myconn.Fillcombo("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cboEmployee)
        Myconn.Fillcombo("select * from Employees", "Employees", "EmployeeID", "EmployeeName", Me, cboEmployeeDrag)
        Myconn.Fillcombo("select * from Patient", "Patient", "patient_ID", "Name", Me, cboPatient)
        Myconn.Fillcombo("select * from Operations_Store", "Operations_Store", "DrugID", "DrugName", Me, cbokind_Drag)
        fin = False
        Myconn.Fillcombo("select * from specialization WHERE kind like 'k'", "specialization ", "specializationID", "specialization", Me, cboKissm)
        fin = True
        Timer1.Start()

        cboGroup.Items.Add("أدوية")
        cboGroup.Items.Add("مستهلكات")
    End Sub
    Private Sub btnNew_Drug_Click(sender As Object, e As EventArgs) Handles btnNew_Drug.Click
        NewRecord()
        btnSave_Drug.Enabled = True

    End Sub
    Private Sub btnSave_Drug_Click(sender As Object, e As EventArgs) Handles btnSave_Drug.Click
        Myconn.Filldataset("select * from Operations_Store", "Operations_Store", Me)
        If txtKind_ID.Text = "" OrElse txtKind_Name.Text = "" OrElse txtKind_Price.Text = "" OrElse cboGroup.SelectedIndex < 0 Then
            MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        Dim XX() As String = {txtKind_ID.Text, "'" & txtKind_Name.Text & "'", "'" & txtKind_Price.Text & "'", "'" & cboGroup.Text & "'"}
        Myconn.AddNewRecord("Operations_Store", XX)
        Fillgrd()
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        btnSave_Drug.Enabled = False
    End Sub

    Private Sub drg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg.CellClick
        Myconn.Filldataset("select * from Operations_Store where DrugID =" & CInt(drg.CurrentRow.Cells(2).Value), "Operations_Store", Me)
        Binding()
        btnSave_Drug.Enabled = False
    End Sub

    Private Sub btnDelete_Drug_Click(sender As Object, e As EventArgs) Handles btnDelete_Drug.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Myconn.DeleteRecord("Operations_Store", "DrugID", txtKind_ID.Text)
            Myconn.ClearAllText(Me, GroupBox1)
            Fillgrd()
        End If
    End Sub

    Private Sub btnUpdate_Drug_Click(sender As Object, e As EventArgs) Handles btnUpdate_Drug.Click
        Dim Values() As String = {"'" & txtKind_Name.Text & "'", "'" & txtKind_Price.Text & "'", "'" & cboGroup.Text & "'"}
        Dim Mycolumes() As String = {"DrugName", "DrugPrice", "GroupName"}
        Myconn.UpdateRecord("Operations_Store", Mycolumes, Values, "DrugID", txtKind_ID.Text)
        Fillgrd()
        MessageBox.Show("تمت عملية التعديل بنجاح", "رسالة تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

        Select Case TabControl1.SelectedIndex
            Case 0
                Myconn.Autocomplete("Operations_Store", "DrugName", txtKind_Name)
            Case 1

                fin = False
                Myconn.Fillcombo("select * from Operations_Store", "Operations_Store", "DrugID", "DrugName", Me, cboKind_Name)
                fin = True

                Myconn.Sum_drg(drg2, 6, Label35, Label34)

            Case 2
                Myconn.Fillcombo("select * from Operations_Store", "Operations_Store", "DrugID", "DrugName", Me, cbokind_Drag)

            Case 3
                Fillgrd()
            Case 4
                Fillgrd()
            Case 5
                fin = False
                Myconn.Fillcombo("select * from Operations_Store", "Operations_Store", "DrugID", "DrugName", Me, cboKindMove)
                fin = True
                Fillgrd()
            Case 6
                Fillgrd()

        End Select

    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        NewRecord()
        drg2.Rows.Clear()
        ToolStripTextBox1.Text = ""
        cboEmployee.Enabled = True
        dtb.Enabled = True
        Myconn.Sum_drg(drg2, 6, Label35, Label34)
        btnSave.Enabled = True
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Myconn.Filldataset("select * from Operations_Store_Bills_Add", "Operations_Store_Bills_Add", Me)
        If txtBill_ID.Text = "" OrElse txtAmount.Text = "" OrElse txtPrice.Text = "" OrElse cboEmployee.SelectedIndex < 0 OrElse cboKind_Name.SelectedIndex < 0 Then
            MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        Dim XX() As String = {txtBill_ID.Text, "'" & dtb.Text & "'", "'" & Label15.Text & "'", cboEmployee.SelectedValue, cboKind_Name.SelectedValue, "'" & txtPrice.Text & "'", txtAmount.Text, Label16.Text}
        Myconn.AddNewRecord("Operations_Store_Bills_Add", XX)

        Myconn.Filldataset("select * from VStore_Bills WHERE Bill_id =" & txtBill_ID.Text & " order by ID", "VStore_Bills", Me)
        Myconn.cur.Position = Myconn.cur.Count - 1
        drg2.Rows.Add(New String() {drg2.Rows.Count.ToString + 1, Myconn.cur.Current(0), Myconn.cur.Current(7), Myconn.cur.Current(4), Myconn.cur.Current(5), Myconn.cur.Current(6), Myconn.cur.Current(9), Myconn.cur.Current(8), Myconn.cur.Current(11)})

        Myconn.DataGridview_MoveLast(drg2, 7)
        txtID.Text = Me.BindingContext(Myconn.dv).Current("ID")
        OperationStroe()
        'Myconn.Filldataset("select DrugName,DrugID,price, sum(amount) as totals,price * sum(amount)  as b, GroupName from VStore_Bills where DrugID =" & CInt(cboKind_Name.SelectedValue.ToString()) & " group by DrugName,DrugID,GroupName,price ", "VStore_Bills", Me)
        Binding()
        
        cboEmployee.Enabled = False
        dtb.Enabled = False

        Myconn.Sum_drg(drg2, 6, Label35, Label34)
        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        btnSave_Drug.Enabled = False
        
        txtAmount.Text = ""
    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        Label16.Text = Val(txtAmount.Text) * Val(txtPrice.Text)
    End Sub

    Private Sub cboKind_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKind_Name.SelectedIndexChanged
        If Not fin Then Return
        If cboKind_Name.SelectedIndex = -1 Then Return
        Myconn.Filldataset("select * from Operations_Store where DrugID =" & CInt(cboKind_Name.SelectedValue.ToString()), "Operations_Store", Me)
        txtPrice.Text = Me.BindingContext(Myconn.dv).Current("DrugPrice")
        Binding()
    End Sub

    Private Sub ToolStripTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ToolStripTextBox1.TextChanged
        If ToolStripTextBox1.Text = "" Then
            drg2.Rows.Clear()
            Label35.Text = "0"
            Return
        End If
        Try
            Myconn.Filldataset("select * from VStore_Bills where Bill_ID =" & CInt(ToolStripTextBox1.Text), "VStore_Bills", Me)
            If Myconn.dv.Count = 0 Then
                MessageBox.Show("رقم فاتورة غير موجود", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                ToolStripTextBox1.Text = ""
                drg2.Rows.Clear()
                Return
            End If
        Catch ex As Exception

        End Try
        drg2.Rows.Clear()

        For i As Integer = 0 To Myconn.cur.Count - 1
            drg2.Rows.Add(New String() {i + 1, Myconn.cur.Current(0), Myconn.cur.Current(7), Myconn.cur.Current(4), Myconn.cur.Current(5), Myconn.cur.Current(6), Myconn.cur.Current(9), Myconn.cur.Current(8), Myconn.cur.Current(11)})
            Myconn.cur.Position += 1
        Next
        Myconn.DataGridview_MoveLast(drg2, 7)
        Myconn.Sum_drg(drg2, 6, Label35, Label34)
        txtBill_ID.Text = Me.BindingContext(Myconn.dv).Current("Bill_ID")
        txtID.Text = Me.BindingContext(Myconn.dv).Current("ID")
        cboEmployee.Text = Me.BindingContext(Myconn.dv).Current("EmployeeName")
        cboEmployee.Enabled = False
        dtb.Enabled = False
    End Sub

    Private Sub drg2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles drg2.CellClick
        Myconn.Filldataset("select * from VStore_Bills where ID =" & CInt(drg2.CurrentRow.Cells(8).Value), "VStore_Bills", Me)
        txtID.DataBindings.Clear()
        txtID.DataBindings.Add("text", Myconn.dv, "ID")
        'txtID.Text = Me.BindingContext(Myconn.dv).Current("ID")
        Myconn.comboBinding("EmployeeID", cboEmployee)
        Myconn.comboBinding("DrugID", cboKind_Name)
        btnSave.Enabled = False
    End Sub

    Private Sub btnNew_Drag_Click(sender As Object, e As EventArgs) Handles btnNew_Drag.Click
        NewRecord()
        cboEnabel_True(GroupBox3)
        dtb2.Enabled = True
        Myconn.Sum_drg(drg3, 8, Label38, Label37)
        txtSearch_Drag.Text = ""
    End Sub

    Private Sub cboKissm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKissm.SelectedIndexChanged
        If Not fin Then Return
        If cboKissm.SelectedIndex = -1 Then Return
        Myconn.Fillcombo("select * from Cervices where specializationID =" & cboKissm.SelectedValue, "Cervices", "CerviceID", "CerviceName", Me, cboOperation)
        Myconn.Fillcombo("select * from Doctors where specializationID =" & cboKissm.SelectedValue, "Doctors", "DoctorsID", "name", Me, cboDoctor)

    End Sub

    Private Sub btnSave_Drag_Click(sender As Object, e As EventArgs) Handles btnSave_Drag.Click
        Myconn.Filldataset("select * from Operations_Store_Bills_Drage", "Operations_Store_Bills_Drage", Me)
        If txtBillDrag_ID.Text = "" OrElse txtAmountDrage.Text = "" OrElse txtPriceDrag.Text = "" OrElse cboEmployeeDrag.SelectedIndex < 0 OrElse cbokind_Drag.SelectedIndex < 0 OrElse cboPatient.SelectedIndex < 0 OrElse cboDoctor.SelectedIndex < 0 OrElse cboKissm.SelectedIndex < 0 OrElse cboOperation.SelectedIndex < 0 Then
            MessageBox.Show("من فضلك أكمل البيانات ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return
        End If

        If txtAmountStor_drag.Text = "" Then
            MessageBox.Show("الصنف غير موجود بالمخزن ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            Return

        End If

        If txtAmountStor_drag.Text <= 0 Then
            MessageBox.Show("الكمية المتاحة لا تسمح ", "رسالة تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            txtAmountDrage.Text = ""
            Return

        End If
        Dim XX() As String = {txtBillDrag_ID.Text, "'" & dtb2.Text & "'", "'" & Label20.Text & "'", cboPatient.SelectedValue, cboEmployeeDrag.SelectedValue, cboDoctor.SelectedValue, cbokind_Drag.SelectedValue, "'" & txtPriceDrag.Text & "'", txtAmountDrage.Text, Label21.Text, cboOperation.SelectedValue, "NULL", cboKissm.SelectedValue}
        Myconn.AddNewRecord("Operations_Store_Bills_Drage", XX)

        'Fillgrd()
        OperationStroe()
        Myconn.Filldataset("select DrugName,DrugID,price, sum(amount) as totals, GroupName from Op_Store where DrugID =" & CInt(cbokind_Drag.SelectedValue.ToString()) & " group by DrugName,DrugID,GroupName,price", "Op_Store", Me)

        Binding()

        Myconn.Filldataset("select * from VStore_Bills_Drage", "VStore_Bills_Drage", Me)
        Myconn.cur.Position = Myconn.cur.Count - 1
        drg3.Rows.Add(New String() {drg3.Rows.Count + 1, Myconn.cur.Current(1), Myconn.cur.Current(2), Myconn.cur.Current(0), Myconn.cur.Current(16), Myconn.cur.Current(6), Myconn.cur.Current(7), Myconn.cur.Current(8), Myconn.cur.Current(9), Myconn.cur.Current(17)})
        drg3.Rows(drg3.Rows.Count - 1).Cells(7).Selected = True
        drg3.CurrentCell = drg3.SelectedCells(7)
        Myconn.Sum_drg(drg3, 8, Label38, Label37)

        cboEnable_False(GroupBox3)
        dtb2.Enabled = False
        cbokind_Drag.Enabled = True
        txtAmountDrage.Text = ""

        MessageBox.Show("تمت عملية الحفظ بنجاح", "رسالة", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        btnSearch_Drag.Enabled = False
        'cboKind_Name.SelectedIndex = -1


    End Sub

    Private Sub cbokind_Drag_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbokind_Drag.SelectedIndexChanged
        On Error Resume Next

        If Not fin Then Return
        If cbokind_Drag.SelectedIndex = -1 Then Return
        Myconn.Filldataset("select * from Operations_Store where DrugID =" & CInt(cboKind_Name.SelectedValue.ToString()), "Operations_Store", Me)
        txtPriceDrag.Text = Me.BindingContext(Myconn.dv).Current("DrugPrice")


        'Myconn.Filldataset("select * from Operations_Store where DrugID =" & CInt(cboKind_Name.SelectedValue.ToString()), "Operations_Store", Me)
       

        Binding()

    End Sub
    Sub OperationStroe()
        Myconn.Filldataset("delete from Op_Store", "Op_Store", Me)
        Myconn.Filldataset("insert into op_store select DrugID,DrugName,price, sum(amount)*-1 as totals,price * sum(amount)  as b,GroupName from  VStore_Bills_Drage group by DrugName,DrugID,GroupName,price", "Op_Store", Me)
        Myconn.Filldataset("insert into op_store select DrugID, DrugName,price, sum(amount) as totals,price * sum(amount)  as b, GroupName from  VStore_Bills group by DrugName,DrugID,GroupName,price", "Op_Store", Me)

    End Sub

    Private Sub txtSearch_Drag_TextChanged(sender As Object, e As EventArgs) Handles txtSearch_Drag.TextChanged
        If txtSearch_Drag.Text = "" Then
            drg3.Rows.Clear()
            Return
        End If

        Try
            Myconn.Filldataset("select * from VStore_Bills_Drage where Bill_ID =" & CInt(txtSearch_Drag.Text), "VStore_Bills_Drage", Me)


            If Myconn.dv.Count = 0 Then
                MessageBox.Show("رقم فاتورة غير موجود", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtSearch_Drag.Text = ""

                Return
            End If
        Catch ex As Exception

        End Try

        drg3.Rows.Clear()
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg3.Rows.Add(New String() {Myconn.cur.Position.ToString + 1, Myconn.cur.Current(1), Myconn.cur.Current(2), Myconn.cur.Current(0), Myconn.cur.Current(16), Myconn.cur.Current(6), Myconn.cur.Current(7), Myconn.cur.Current(8), Myconn.cur.Current(9), Myconn.cur.Current(17)})
            Myconn.cur.Position += 1
        Next i
        Myconn.Sum_drg(drg3, 8, Label38, Label37)

        txtBillDrag_ID.Text = Me.BindingContext(Myconn.dv).Current("Bill_ID")
        cboPatient.Text = Me.BindingContext(Myconn.dv).Current("name")
        cboEmployeeDrag.Text = Me.BindingContext(Myconn.dv).Current("EmployeeName")
        cboKissm.Text = Me.BindingContext(Myconn.dv).Current("specialization")
        'cboOperation.Text = Me.BindingContext(Myconn.dv).Current("CerviceName")

        cboDoctor.Text = Me.BindingContext(Myconn.dv).Current("expr1")

        cboEnable_False(GroupBox3)
        dtb2.Enabled = False
        cbokind_Drag.Enabled = True
    End Sub

    Private Sub txtAmountDrage_TextChanged(sender As Object, e As EventArgs) Handles txtAmountDrage.TextChanged
        Label21.Text = Val(txtAmountDrage.Text) * Val(txtPriceDrag.Text)
    End Sub



    Private Sub drg4_DoubleClick(sender As Object, e As EventArgs) Handles drg4.DoubleClick
        TabControl1.SelectedIndex = 1
        ToolStripTextBox1.Text = drg4.CurrentRow.Cells(1).Value
        cboEmployee.Enabled = False
        dtb.Enabled = False
    End Sub

    Private Sub btnSave_DoubleClick(sender As Object, e As EventArgs) Handles btnSave.DoubleClick
        btnSave.Enabled = True
    End Sub

    Private Sub drg5_DoubleClick(sender As Object, e As EventArgs) Handles drg5.DoubleClick
        TabControl1.SelectedIndex = 2
        txtSearch_Drag.Text = drg5.CurrentRow.Cells(1).Value

        cboEnable_False(GroupBox3)
        dtb2.Enabled = False
        cbokind_Drag.Enabled = True
    End Sub
    Sub cboEnable_False(gr As GroupBox)
        For Each cbo As Control In gr.Controls
            If TypeOf cbo Is ComboBox Then
                cbo.Enabled = False
            End If
        Next
    End Sub

    Sub cboEnabel_True(gr As GroupBox)
        For Each cbo As Control In gr.Controls
            If TypeOf cbo Is ComboBox Then
                cbo.Enabled = True
            End If
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label20.Text = TimeOfDay
        Label15.Text = TimeOfDay
    End Sub

    Private Sub cboKindMove_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKindMove.SelectedIndexChanged
        Dim X, Y As Integer

        If Not fin Then Return
        drg6.Rows.Clear()
        If cboKindMove.SelectedIndex = -1 Then Return
        Myconn.Filldataset("select Bill_ID,Bill_Date,Bill_Time,DrugName,DrugID,Amount,EmployeeName from VStore_Bills where DrugID =" & CInt(cboKindMove.SelectedValue.ToString()), "VStore_Bills", Me)
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg6.Rows.Add(New String() {Myconn.cur.Position.ToString + 1, Myconn.cur.Current(0), Myconn.cur.Current(1), Myconn.cur.Current(2), Myconn.cur.Current(3), Myconn.cur.Current(4), Myconn.cur.Current(5), Myconn.cur.Current(6)})
            Myconn.cur.Position += 1
            X += drg6.Rows(i).Cells(6).Value
        Next

        drg7.Rows.Clear()
        Myconn.Filldataset("select Bill_ID,Bill_Date,Bill_Time,DrugName,DrugID,Amount,EmployeeName from VStore_Bills_Drage where DrugID =" & CInt(cboKindMove.SelectedValue.ToString()), "VStore_Bills_Drage", Me)
        For i As Integer = 0 To Myconn.cur.Count - 1
            drg7.Rows.Add(New String() {Myconn.cur.Position.ToString + 1, Myconn.cur.Current(0), Myconn.cur.Current(1), Myconn.cur.Current(2), Myconn.cur.Current(3), Myconn.cur.Current(4), Myconn.cur.Current(5), Myconn.cur.Current(6)})
            Myconn.cur.Position += 1
            Y += drg7.Rows(i).Cells(6).Value
        Next
        'Label47.Text = "الوارد إلى مخزن العمليات من صنف  :" & cboKindMove.Text
        Label49.Text = X & "  قطعة"
        Label50.Text = Y & "  قطعة"
        Label52.Text = X - Y & "  قطعة"
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim result = MessageBox.Show("هل أنت متأكد من عملية الحذف ؟", "رسالة تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading)
        If (result = DialogResult.No) Then
            Return
        Else
            Myconn.DeleteRecord("Operations_Store_Bills_Add", "ID", txtID.Text)
            OperationStroe()
            'Myconn.ClearAllText(Me, GroupBox1)
            Fillgrd()
        End If

    End Sub

    Private Sub btnUpdat_Click(sender As Object, e As EventArgs) Handles btnUpdat.Click

    End Sub
End Class