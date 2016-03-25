Public Class frmPharm_Safe_move
    Dim Myconn As New connect
    Dim x As Integer
    Dim st, st1 As String
    Dim S, W As Double
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 
        drg.Rows.Clear()
        S = 0
        W = 0
        Select Case x
            Case 0
                Myconn.Filldataset("Select * ,b.itemName,c.Supplier_Name,d.EmployeeName,e.Permission_Type from Pharm_Safe_Payment a
                           left join payment_item b on a.itemID = b.paymentID
                           left join Supplier c on a.Supplier_ID = c.Supplier_ID 
                            left join Permission_Type e on a.PermissionID = e.PermissionID 
                            left join Employees d on a.User_ID = d.EmployeeID " & st, "Pharm_Safe_Payment", Me)

                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add()
                    drg.Rows(i).Cells(0).Value = i + 1
                    drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Permission_Type")
                    drg.Rows(i).Cells(2).Value = Myconn.cur.Current("itemName")
                    drg.Rows(i).Cells(3).Value = Myconn.cur.Current("P_Date")
                    drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Supplier_Name")
                    drg.Rows(i).Cells(5).Value = Myconn.cur.Current("amount")
                    drg.Rows(i).Cells(6).Value = Myconn.cur.Current("amount_abc")
                    drg.Rows(i).Cells(7).Value = Myconn.cur.Current("EmployeeName")
                    drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Note")
                    drg.Rows(i).Cells(9).Value = Myconn.cur.Current("State")
                    S += Myconn.cur.Current("amount")
                    Myconn.cur.Position += 1
                Next
            Case 1
                Myconn.Filldataset("Select * ,b.itemName,c.Customer_Name,d.EmployeeName,e.Permission_Type from Pharm_Safe_recive a
                           left join receipt_item b on a.itemID = b.itemID
                           left join Customers c on a.Customer_ID = c.Customer_ID 
                            left join Permission_Type e on a.PermissionID = e.PermissionID 
                            left join Employees d on a.User_ID = d.EmployeeID " & st, "Pharm_Safe_recive", Me)
                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add()
                    drg.Rows(i).Cells(0).Value = i + 1
                    drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Permission_Type")
                    drg.Rows(i).Cells(2).Value = Myconn.cur.Current("itemName")
                    drg.Rows(i).Cells(3).Value = Myconn.cur.Current("P_Date")
                    drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Customer_Name")
                    drg.Rows(i).Cells(5).Value = Myconn.cur.Current("amount")
                    drg.Rows(i).Cells(6).Value = Myconn.cur.Current("amount_abc")
                    drg.Rows(i).Cells(7).Value = Myconn.cur.Current("EmployeeName")
                    drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Note")
                    drg.Rows(i).Cells(9).Value = Myconn.cur.Current("State")
                    W += Myconn.cur.Current("amount")
                    Myconn.cur.Position += 1
                Next

            Case 2
                Myconn.Filldataset("Select a.PermissionID,a.amount,a.amount_abc,a.Note,a.P_Date,a.P_ID,a.User_ID,a.State ,b.itemName,c.Customer_Name,d.EmployeeName,e.Permission_Type from Pharm_Safe_recive a
                           left join receipt_item b on a.itemID = b.itemID
                           left join Customers c on a.Customer_ID = c.Customer_ID 
                            left join Permission_Type e on a.PermissionID = e.PermissionID 
                            left join Employees d on a.User_ID = d.EmployeeID " & st & "
                            union all 
                            Select f.PermissionID,f.amount,f.amount_abc,f.Note,f.P_Date,f.P_ID,f.User_ID,f.State ,g.itemName,h.Supplier_Name,k.EmployeeName,j.Permission_Type from Pharm_Safe_Payment f
                           left join payment_item g on f.itemID = g.paymentID
                           left join Supplier h on f.Supplier_ID = h.Supplier_ID 
                            left join Permission_Type j on f.PermissionID = j.PermissionID 
                            left join Employees k on f.User_ID = k.EmployeeID " & st1, "Pharm_Safe_recive", Me)


                For i As Integer = 0 To Myconn.cur.Count - 1
                    drg.Rows.Add()
                    drg.Rows(i).Cells(0).Value = i + 1
                    drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Permission_Type")
                    drg.Rows(i).Cells(2).Value = Myconn.cur.Current("itemName")
                    drg.Rows(i).Cells(3).Value = Myconn.cur.Current("P_Date")
                    drg.Rows(i).Cells(4).Value = Myconn.cur.Current("Customer_Name")
                    drg.Rows(i).Cells(5).Value = Myconn.cur.Current("amount")
                    drg.Rows(i).Cells(6).Value = Myconn.cur.Current("amount_abc")
                    drg.Rows(i).Cells(7).Value = Myconn.cur.Current("EmployeeName")
                    drg.Rows(i).Cells(8).Value = Myconn.cur.Current("Note")
                    drg.Rows(i).Cells(9).Value = Myconn.cur.Current("State")
                    If Myconn.cur.Current("PermissionID") = 1 Then
                        drg.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
                        W += Myconn.cur.Current("amount")
                    Else
                        drg.Rows(i).DefaultCellStyle.BackColor = Color.Pink
                        S += Myconn.cur.Current("amount")
                    End If
                    Myconn.cur.Position += 1
                Next

        End Select
        Label4.Text = W
        Label6.Text = clsNumber.nTOword(Label4.Text)
        Label6.Left = Label4.Left - (Label6.Width + 20)
        Label7.Text = S
        Label8.Text = clsNumber.nTOword(Label7.Text)
        Label8.Left = Label7.Left - (Label8.Width + 20)
        Label9.Text = W - S
        Label10.Text = clsNumber.nTOword(Label9.Text)
        Label10.Left = Label9.Left - (Label10.Width + 20)
    End Sub
    Private Sub frmPharm_Safe_move_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label5.Left = 0
        Label5.Width = Me.Width
        Me.KeyPreview = True
        Myconn.Fillcombo("select * from payment_item", "payment_item", "paymentID", "itemName", Me, cbo_band.ComboBox)
    End Sub

    Private Sub cbo_ezn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_ezn.SelectedIndexChanged
        Select Case cbo_ezn.SelectedIndex
            Case 0 ' اذن دفع
                x = 0
                st = Nothing
                Myconn.Fillcombo("select * from payment_item", "payment_item", "paymentID", "itemName", Me, cbo_band.ComboBox)

            Case 1 ' اذن استلام
                x = 1
                st = Nothing
                Myconn.Fillcombo("select * from receipt_item", "receipt_item", "itemID", "itemName", Me, cbo_band.ComboBox)

            Case 2 ' كل الاذونات
                x = 2
                st = Nothing
                cbo_band.Visible = False
        End Select
        Fillgrd()
    End Sub

    Private Sub cbo_Search_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Search.SelectedIndexChanged
        txt1.Visible = False
        txt2.Visible = False
        cbo_band.Visible = False
        Select Case cbo_Search.SelectedIndex
            Case 0 ' تاريخ
                If cbo_ezn.SelectedIndex = 0 Then
                    x = 0
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    x = 1
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    x = 2
                End If
                txt1.Visible = True
                txt2.Visible = False
                lab1.Visible = False
            Case 1 ' فترة
                If cbo_ezn.SelectedIndex = 0 Then
                    x = 0
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    x = 1
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    x = 2
                End If
                txt2.Visible = True
                txt1.Visible = True
                lab1.Visible = True
            Case 2 ' البند
                If cbo_ezn.SelectedIndex = 0 Then
                    cbo_band.Visible = True
                    Myconn.Fillcombo("select * from payment_item", "payment_item", "paymentID", "itemName", Me, cbo_band.ComboBox)
                    x = 0
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    cbo_band.Visible = True
                    Myconn.Fillcombo("select * from receipt_item", "receipt_item", "itemID", "itemName", Me, cbo_band.ComboBox)
                    x = 1
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    cbo_band.Visible = False
                    x = 2
                End If
                txt2.Visible = False
                txt1.Visible = False
                lab1.Visible = False
            Case 3 ' بند وتاريخ
                If cbo_ezn.SelectedIndex = 0 Then
                    txt2.Visible = False
                    txt1.Visible = True
                    cbo_band.Visible = True
                    Myconn.Fillcombo("select * from payment_item", "payment_item", "paymentID", "itemName", Me, cbo_band.ComboBox)

                    x = 0
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    txt2.Visible = False
                    txt1.Visible = True
                    cbo_band.Visible = True
                    Myconn.Fillcombo("select * from receipt_item", "receipt_item", "itemID", "itemName", Me, cbo_band.ComboBox)

                    x = 1
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    txt2.Visible = False
                    txt1.Visible = False
                    cbo_band.Visible = False
                    x = 2
                End If
                lab1.Visible = False
            Case 4 ' بند وفترة
                If cbo_ezn.SelectedIndex = 0 Then
                    txt2.Visible = True
                    txt1.Visible = True
                    lab1.Visible = True
                    cbo_band.Visible = True
                    Myconn.Fillcombo("select * from payment_item", "payment_item", "paymentID", "itemName", Me, cbo_band.ComboBox)

                    x = 0
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    txt2.Visible = True
                    txt1.Visible = True
                    lab1.Visible = True
                    cbo_band.Visible = True
                    Myconn.Fillcombo("select * from receipt_item", "receipt_item", "itemID", "itemName", Me, cbo_band.ComboBox)

                    x = 1
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    txt2.Visible = False
                    txt1.Visible = False
                    lab1.Visible = False
                    cbo_band.Visible = False
                    x = 2
                End If
            Case 5 ' عميل
                If cbo_ezn.SelectedIndex = 0 Then
                    txt2.Visible = False
                    txt1.Visible = False
                    lab1.Visible = False
                    cbo_band.Visible = False
                    x = 0
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    txt2.Visible = False
                    txt1.Visible = False
                    lab1.Visible = False
                    cbo_band.Visible = True
                    'Myconn.Fillcombo("select * from Supplier", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_band.ComboBox)
                    Myconn.Fillcombo("select * from Customers", "Customers", "Customer_ID", "Customer_Name", Me, cbo_band.ComboBox)
                    x = 1
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    txt2.Visible = False
                    txt1.Visible = False
                    lab1.Visible = False
                    cbo_band.Visible = False
                    x = 2
                End If
            Case 6 ' عميل وفترة
                If cbo_ezn.SelectedIndex = 0 Then
                    txt2.Visible = False
                    txt1.Visible = False
                    lab1.Visible = False
                    cbo_band.Visible = False
                    x = 0
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    txt2.Visible = True
                    txt1.Visible = True
                    lab1.Visible = True
                    cbo_band.Visible = True
                    'Myconn.Fillcombo("select * from Supplier", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_band.ComboBox)
                    Myconn.Fillcombo("select * from Customers", "Customers", "Customer_ID", "Customer_Name", Me, cbo_band.ComboBox)
                    x = 1
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    txt2.Visible = False
                    txt1.Visible = False
                    lab1.Visible = False
                    cbo_band.Visible = False
                    x = 2
                End If

            Case 7 ' مورد
                If cbo_ezn.SelectedIndex = 0 Then
                    txt2.Visible = False
                    txt1.Visible = False
                    lab1.Visible = False
                    cbo_band.Visible = True
                    Myconn.Fillcombo("select * from Supplier", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_band.ComboBox)
                    'Myconn.Fillcombo("select * from Customers", "Customers", "Customer_ID", "Customer_Name", Me, cbo_band.ComboBox)
                    x = 0
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    txt2.Visible = False
                    txt1.Visible = False
                    lab1.Visible = False
                    cbo_band.Visible = False
                    x = 1
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    txt2.Visible = False
                    txt1.Visible = False
                    lab1.Visible = False
                    cbo_band.Visible = False
                    x = 2
                End If
            Case 8 ' مورد وفترة
                If cbo_ezn.SelectedIndex = 0 Then
                    txt2.Visible = True
                    txt1.Visible = True
                    lab1.Visible = True
                    cbo_band.Visible = True
                    Myconn.Fillcombo("select * from Supplier", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_band.ComboBox)
                    'Myconn.Fillcombo("select * from Customers", "Customers", "Customer_ID", "Customer_Name", Me, cbo_band.ComboBox)
                    x = 0
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    txt2.Visible = False
                    txt1.Visible = False
                    lab1.Visible = False
                    cbo_band.Visible = False
                    x = 1
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    txt2.Visible = False
                    txt1.Visible = False
                    lab1.Visible = False
                    cbo_band.Visible = False
                    x = 2
                End If
        End Select


    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Select Case cbo_Search.SelectedIndex
            Case 0 ' تاريخ
                If cbo_ezn.SelectedIndex = 0 Then
                    st = "where a.P_Date = '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'"
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    st = "where a.P_Date = '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'"
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    st = "where a.P_Date = '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'"
                    st1 = "where f.P_Date = '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'"
                End If
            Case 1 ' فترة
                If cbo_ezn.SelectedIndex = 0 Then
                    st = "where a.P_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' And '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    st = "where a.P_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' And '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    st = "where a.P_Date between'" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' And '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"
                    st1 = "where f.P_Date between'" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' And '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"
                End If
            Case 2 ' البند
                If cbo_ezn.SelectedIndex = 0 Then
                    st = "where a.itemID =" & CInt(cbo_band.ComboBox.SelectedValue)
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    st = "where a.itemID =" & CInt(cbo_band.ComboBox.SelectedValue)
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    st = Nothing
                    st1 = Nothing
                End If
            Case 3 ' بند وتاريخ
                If cbo_ezn.SelectedIndex = 0 Then
                    st = "where a.itemID =" & CInt(cbo_band.ComboBox.SelectedValue) & "and a.P_Date = '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'"
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    st = "where a.itemID =" & CInt(cbo_band.ComboBox.SelectedValue) & "and a.P_Date = '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "'"
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    st = Nothing
                    st1 = Nothing
                End If
            Case 4 ' بند وفترة
                If cbo_ezn.SelectedIndex = 0 Then
                    st = "where a.itemID =" & CInt(cbo_band.ComboBox.SelectedValue) & "and a.P_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' And '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    st = "where a.itemID =" & CInt(cbo_band.ComboBox.SelectedValue) & "and a.P_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' And '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    st = Nothing
                    st1 = Nothing
                End If
            Case 5 ' عميل
                If cbo_ezn.SelectedIndex = 0 Then
                    st = Nothing
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    st = "where a.Customer_ID =" & CInt(cbo_band.ComboBox.SelectedValue)
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    st = Nothing
                    st1 = Nothing
                End If
            Case 6 'عميل وفترة
                If cbo_ezn.SelectedIndex = 0 Then
                    st = Nothing
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    st = "where a.Customer_ID =" & CInt(cbo_band.ComboBox.SelectedValue) & "and a.P_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' And '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    st = Nothing
                    st1 = Nothing
                End If
            Case 7 ' مورد
                If cbo_ezn.SelectedIndex = 0 Then
                    st = "where a.Supplier_ID =" & CInt(cbo_band.ComboBox.SelectedValue)
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    st = Nothing
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    st = Nothing
                    st1 = Nothing
                End If
            Case 8 ' مورد وفترة
                If cbo_ezn.SelectedIndex = 0 Then
                    st = "where a.Supplier_ID =" & CInt(cbo_band.ComboBox.SelectedValue) & "and a.P_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' And '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'"
                ElseIf cbo_ezn.SelectedIndex = 1 Then
                    st = Nothing
                ElseIf cbo_ezn.SelectedIndex = 2 Then
                    st = Nothing
                    st1 = Nothing
                End If
        End Select
        Fillgrd()
    End Sub
End Class