Public Class frmPharm_Supplier_account
    Dim Myconn As New connect
    Dim st As String
    Dim x As Integer
    Dim fin As Boolean
    Sub Fillgrd() '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''لتعبئة الداتا جريد بالبيانات 

        drg.Rows.Clear()
        Select Case x
            Case 0
                st = "having a.Supplier_ID =" & CInt(cbo_Supplier.ComboBox.SelectedValue)
            Case 1
                st = "having a.Bill_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'And a.Supplier_ID = " & CInt(cbo_Supplier.ComboBox.SelectedValue)
        End Select

        Myconn.Filldataset("Select a.Bill_number,b.Supplier_Name,a.bill_date,sum(a.Total_Price_tax) As Total,count(Drug_ID) As count_Drug1,isnull(c.back,0) As back,isnull(c.count_Drug,0) As count_Drug,
                            (sum(a.Total_Price_tax) - isnull(c.back,0)) as final,a.Bill_ID from [dbo].[Drug_Purchases] a
                             left join [dbo].[Supplier] b on a.Supplier_ID = b.Supplier_ID
                            left join (select Bill_ID,State ,sum(Total_Price_tax) as back,count(Drug_ID) as count_Drug from [dbo].[Drug_Purchases] group by Bill_ID,State having State ='false') c
                            on a.Bill_ID = c.Bill_ID
                            group by a.Bill_number,b.Supplier_Name,a.bill_date,a.Bill_ID,c.back,c.count_Drug,a.Supplier_ID,c.state
                            " & st & "order by a.bill_date", "Drug_Sales", Me)
        Try
            drg.Rows.Clear()
            If Myconn.cur.Count = 0 Then MsgBox("لا توجد مبيعات للمورد")
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Bill_number")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("bill_date")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Supplier_Name")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("count_Drug1")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Total")
                drg.Rows(i).Cells(6).Value = Myconn.cur.Current("count_Drug")
                drg.Rows(i).Cells(7).Value = Myconn.cur.Current("back")
                drg.Rows(i).Cells(8).Value = Myconn.cur.Current("final")

                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg(drg, 8, Label1, Label2)
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
        End Try

        st = Nothing
    End Sub
    Sub Fillgrd2()
        drg2.Rows.Clear()
        Select Case x
            Case 0
                st = "and a.Supplier_ID =" & CInt(cbo_Supplier.ComboBox.SelectedValue)
            Case 1
                st = "and a.P_Date between '" & Format(CDate(txt1.Text), "yyyy/MM/dd").ToString & "' and '" & Format(CDate(txt2.Text), "yyyy/MM/dd").ToString & "'And a.Supplier_ID = " & CInt(cbo_Supplier.ComboBox.SelectedValue)
        End Select
        Myconn.Filldataset("Select * ,b.itemName,c.Supplier_Name,d.EmployeeName from Pharm_Safe_Payment a
                           left join payment_item b on a.itemID = b.paymentID
                           left join Supplier c on a.Supplier_ID = c.Supplier_ID 
                            left join Employees d on a.User_ID = d.EmployeeID 
							where State = 'True'" & st, "Pharm_Safe_recive", Me)

        Try
            For i As Integer = 0 To Myconn.cur.Count - 1
                drg2.Rows.Add()
                drg2.Rows(i).Cells(0).Value = i + 1
                drg2.Rows(i).Cells(1).Value = Myconn.cur.Current("itemName")
                drg2.Rows(i).Cells(2).Value = Myconn.cur.Current("P_Date")
                drg2.Rows(i).Cells(3).Value = Myconn.cur.Current("Supplier_Name")
                drg2.Rows(i).Cells(4).Value = Myconn.cur.Current("amount")
                drg2.Rows(i).Cells(5).Value = Myconn.cur.Current("amount_abc")
                drg2.Rows(i).Cells(6).Value = Myconn.cur.Current("Note")
                drg2.Rows(i).Cells(7).Value = Myconn.cur.Current("EmployeeName")
                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg(drg2, 4, Label6, Label7)
        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
        End Try
        st = Nothing
    End Sub
    Private Sub frmPharm_Supplier_account_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        fin = False
        Myconn.Fillcombo("select * from Supplier order by Supplier_Name", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Supplier.ComboBox)
        fin = True
    End Sub
    Private Sub cbo_Customers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Supplier.SelectedIndexChanged
        If Not fin Then Return
        If cbo_Supplier.SelectedIndex = -1 Then Return
        x = 0
        Fillgrd()
        Fillgrd2()

        Label9.Text = Val(Label1.Text) - Val(Label6.Text)
        Label10.Text = "( " & clsNumber.nTOword(Label9.Text) & "  )"
        Label10.Left = Label9.Left - (Label10.Width + 20)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txt1.Text = "" Or txt2.Text = "" Then
            MessageBox.Show("من فضلك أدخل التاريخ")
            Return
        Else
            x = 1
        End If
        Fillgrd()
        Fillgrd2()
        Label9.Text = Val(Label1.Text) - Val(Label6.Text)
        Label10.Text = "( " & clsNumber.nTOword(Label9.Text) & "  )"
        Label10.Left = Label9.Left - (Label10.Width + 20)
    End Sub
End Class