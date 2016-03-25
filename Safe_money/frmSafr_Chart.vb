Imports System.Windows.Forms.DataVisualization.Charting
Public Class frmSafr_Chart
    Dim Myconn As New connect
    Dim fin As Boolean
    Dim T As Title
    Dim x, y As Integer
    Dim st, st2 As String
    Sub Chart_Title()
        '~~> Display Data Labels
        Chart1.Series(0).IsValueShownAsLabel = True
        Chart1.Series(1).IsValueShownAsLabel = True
        '~~> Setting label's Fore Color
        Chart1.Series(0).LabelForeColor = Color.Red
        Chart1.Series(1).LabelForeColor = Color.Red
        '~~> Formatting the Title
        With T
            .ForeColor = Color.Black            '~~> Changing the Fore Color of the Title 
            .BackColor = Color.Coral            '~~> Changing the Back Color of the Title 

            '~~> Setting Font, Font Size and Bold/Italicizing
            .Font = New Font("Times New Roman", 18.0F, FontStyle.Bold)
            .Font = New Font("Times New Roman", 18.0F, FontStyle.Underline)
            .BorderColor = Color.Black          '~~> Changing the Border Color of the Title 

            '~~> Setting label's Format to %age
            'Chart1.Series("المشتريات").LabelFormat = "0.00%"
            Chart1.Series(0).LabelForeColor = Color.Black
            Chart1.Series(1).LabelForeColor = Color.Black
            .BorderDashStyle = ChartDashStyle.DashDotDot '~~> Changing the Border Dash Style of the Title 
        End With
    End Sub
    Sub Chart_Sfe_Move()
        Chart1.Series(0).Points.Clear()
        Chart1.Series(1).Points.Clear()

        Chart1.Titles.Clear()
        Chart1.Series(1).IsVisibleInLegend = False
        Try
            Select Case y
                Case 0
                    If cbo_Ezn.SelectedIndex = 0 Then ' دفع
                        Myconn.Filldataset("Select state,(cast(DATEPART(yyyy,Payment_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Payment_date),'00') as varchar(2))) as Payment_Month,
                                        sum(amount) as amount from Payment group by (cast(DATEPART(yyyy,Payment_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Payment_date),'00') as varchar(2))),state
                                         having state ='True'" & st2, "Payment", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Payment_Month"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next
                        Chart1.Series(0).Name = "كل أذونات الدفع"

                        T = Chart1.Titles.Add("كل أذونات الدفع")

                    ElseIf cbo_Ezn.SelectedIndex = 1 Then ' استلام

                        Myconn.Filldataset("Select state,(cast(DATEPART(yyyy,Receipt_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Receipt_date),'00') as varchar(2))) as recive_Month,
                                        sum(amount) as amount from Receipt group by (cast(DATEPART(yyyy,Receipt_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Receipt_date),'00') as varchar(2))),state
                                         having state ='True'" & st, "Receipt", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("recive_Month"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next
                        Chart1.Series(0).Name = "كل أذونات الاستلام"
                        T = Chart1.Titles.Add("كل أذونات الاستلام")

                    ElseIf cbo_Ezn.SelectedIndex = 2 Then ' الكل

                        Myconn.Filldataset("Select state,(cast(DATEPART(yyyy,Receipt_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Receipt_date),'00') as varchar(2))) as recive_Month,
                                        sum(amount) as amount from Receipt group by (cast(DATEPART(yyyy,Receipt_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Receipt_date),'00') as varchar(2))),state
                                         having state ='True'" & st, "Receipt", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("recive_Month"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next

                        Chart1.Series(0).Name = "كل أذونات الاستلام"
                        T = Chart1.Titles.Add("كل أذونات الدفع والاستلام")

                        Myconn.Filldataset2("Select state,(cast(DATEPART(yyyy,Payment_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Payment_date),'00') as varchar(2))) as Payment_Month,
                                        sum(amount) as amount from Payment group by (cast(DATEPART(yyyy,Payment_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Payment_date),'00') as varchar(2))),state
                                         having state ='True'" & st2, "Payment", Me)

                        For i As Integer = 0 To Myconn.cur2.Count - 1
                            Me.Chart1.Series(1).Points.AddXY(Myconn.cur2.Current("Payment_Month"), Myconn.cur2.Current("amount"))
                            Myconn.cur2.Position += 1
                        Next
                        Chart1.Series(1).IsVisibleInLegend = True
                        Chart1.Series(1).Name = "كل أذونات الدفع "
                    End If
                ' ------------------------------------------------------------------------------------------------------------'
                Case 1 ' البند
                    If cbo_Ezn.SelectedIndex = 0 Then
                        Myconn.Filldataset("Select state,(cast(DATEPART(yyyy,Payment_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Payment_date),'00') as varchar(2))) as Payment_Month,
                                        sum(amount) as amount from Payment group by (cast(DATEPART(yyyy,Payment_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Payment_date),'00') as varchar(2))),state,PaymentID
                                         having state ='True' and PaymentID =" & CInt(cbo_Band.ComboBox.SelectedValue) & st2, "Payment", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Payment_Month"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next
                        Chart1.Series(0).Name = " أذونات الدفع لبند " & cbo_Band.Text

                        T = Chart1.Titles.Add(" أذونات الدفع لبند " & cbo_Band.Text)

                    ElseIf cbo_Ezn.SelectedIndex = 1 Then

                        Myconn.Filldataset("Select state,(cast(DATEPART(yyyy,Receipt_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Receipt_date),'00') as varchar(2))) as recive_Month,
                                        sum(amount) as amount from Receipt group by (cast(DATEPART(yyyy,Receipt_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Receipt_date),'00') as varchar(2))),state,itemID
                                         having state ='True' and itemID =" & CInt(cbo_Band.ComboBox.SelectedValue) & st, "Receipt", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("recive_Month"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next
                        Chart1.Series(0).Name = " أذونات الاستلام لبند " & cbo_Band.Text
                        T = Chart1.Titles.Add(" أذونات الاستلام لبند " & cbo_Band.Text)
                    End If
                '------------------------------------------------------------------------------------------
                Case 2 ' القسم
                    If cbo_Ezn.SelectedIndex = 0 Then
                        Myconn.Filldataset("Select state,(cast(DATEPART(yyyy,Payment_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Payment_date),'00') as varchar(2))) as Payment_Month,
                                        sum(amount) as amount from Payment group by (cast(DATEPART(yyyy,Payment_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Payment_date),'00') as varchar(2))),state,specializationID
                                         having state ='True' and specializationID =" & CInt(cbo_Band.ComboBox.SelectedValue) & st2, "Pharm_Safe_Payment", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Payment_Month"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next
                        Chart1.Series(0).Name = " أذونات الدفع لقسم " & cbo_Band.Text

                        T = Chart1.Titles.Add(" أذونات الدفع لقسم " & cbo_Band.Text)

                    ElseIf cbo_Ezn.SelectedIndex = 1 Then

                        Myconn.Filldataset("Select state,(cast(DATEPART(yyyy,Receipt_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Receipt_date),'00') as varchar(2))) as recive_Month,
                                        sum(amount) as amount from Receipt group by (cast(DATEPART(yyyy,Receipt_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Receipt_date),'00') as varchar(2))),state,specializationID
                                         having state ='True' and specializationID =" & CInt(cbo_Band.ComboBox.SelectedValue) & st, "Pharm_Safe_recive", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("recive_Month"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next
                        Chart1.Series(0).Name = " أذونات الاستلام لقسم " & cbo_Band.Text
                        T = Chart1.Titles.Add(" أذونات الاستلام لقسم " & cbo_Band.Text)
                    End If


            End Select
            Chart1.ChartAreas(0).Visible = True
            Chart1.Series(0).IsVisibleInLegend = True

            Chart_Title()
            st = Nothing
        Catch ex As Exception
            MsgBox("هناك خطأ ")
            Return
        End Try
    End Sub
    Private Sub frmSafr_Chart_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width

        Chart1.Series(0).IsVisibleInLegend = False
        Chart1.Series(1).IsVisibleInLegend = False
    End Sub

    Private Sub cbo_Setting_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Setting.SelectedIndexChanged
        Select Case cbo_Setting.SelectedIndex
            Case 0
                cbo_View.Visible = False
                If ColorDialog1.ShowDialog <> DialogResult.Cancel Then
                    Chart1.BackColor = ColorDialog1.Color
                End If
            Case 1
                cbo_View.Visible = False
                If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                    Chart1.ChartAreas(0).BackColor = ColorDialog1.Color
                End If
            Case 2
                cbo_View.Visible = True
                Me.cbo_View.ComboBox.DataSource = [Enum].GetValues(GetType(SeriesChartType))
            Case 3
                cbo_View.Visible = True
                Me.cbo_View.ComboBox.DataSource = [Enum].GetValues(GetType(ChartColorPalette))
            Case 4
                cbo_View.Visible = False
                Chart1.ChartAreas(0).Area3DStyle.Enable3D = True
            Case 5
                cbo_View.Visible = False
                Chart1.ChartAreas(0).Area3DStyle.Enable3D = False
        End Select
    End Sub

    Private Sub cbo_View_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_View.SelectedIndexChanged
        Try
            Select Case cbo_Setting.SelectedIndex
                Case 2
                    Dim value = DirectCast(Me.cbo_View.ComboBox.SelectedValue, SeriesChartType)
                    Chart1.Series(0).ChartType = value
                    Chart1.Series(1).ChartType = value
                Case 3
                    Dim value = DirectCast(Me.cbo_View.ComboBox.SelectedValue, ChartColorPalette)
                    Chart1.Palette = value
            End Select
        Catch ex As Exception
            MsgBox("قم باختيار شكل آخر ")
            Return
        End Try
    End Sub

    Private Sub cbo_Search_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Search.SelectedIndexChanged
        st = Nothing
        st2 = Nothing
        Select Case cbo_Search.SelectedIndex
            Case 0 ' الكل
                y = 0

                Chart_Sfe_Move()
            Case 1 ' البند
                If cbo_Ezn.SelectedIndex = 0 Then
                    cbo_Band.Visible = True
                    fin = False
                    Myconn.Fillcombo("select * from payment_item", "payment_item", "paymentID", "itemName", Me, cbo_Band.ComboBox)
                    fin = True
                ElseIf cbo_Ezn.SelectedIndex = 1 Then
                    cbo_Band.Visible = True
                    fin = False
                    Myconn.Fillcombo("select * from receipt_item", "receipt_item", "itemID", "itemName", Me, cbo_Band.ComboBox)
                    fin = True
                End If
                y = 1
            Case 2 ' القسم

                If cbo_Ezn.SelectedIndex = 0 Then
                    cbo_Band.Visible = True
                    fin = False
                    Myconn.Fillcombo("select * from specialization where kind = 'b'", "specialization", "specializationID", "specialization", Me, cbo_Band.ComboBox)
                    fin = True
                ElseIf cbo_Ezn.SelectedIndex = 1 Then
                    cbo_Band.Visible = True
                    fin = False
                    Myconn.Fillcombo("select * from specialization where kind = 'k'", "specialization", "specializationID", "specialization", Me, cbo_Band.ComboBox)
                    fin = True
                End If
                y = 2
            Case 3 ' المورد
                'If cbo_Ezn.SelectedIndex = 0 Then
                '    cbo_Band.Visible = True
                '    fin = False
                '    Myconn.Fillcombo("select * from Supplier order by Supplier_Name", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Band.ComboBox)
                '    fin = True
                'End If
                'y = 2
        End Select
    End Sub

    Private Sub cbo_Ezn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Ezn.SelectedIndexChanged
        y = 0
        Chart_Sfe_Move()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If txt1.Text.Length >= 7 And txt2.Text.Length >= 7 Then
            Select Case cbo_Search.SelectedIndex
                Case 0
                    y = 0
                Case 1
                    y = 1
                Case 2
                    y = 2
                Case 3
                    y = 2
            End Select
            st = "and (cast(DATEPART(yyyy,Receipt_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Receipt_date),'00') as varchar(2))) between '" & Format(CDate(txt1.Text), "yyyy/MM") & "' and '" & Format(CDate(txt2.Text), "yyyy/MM") & "'"
            st2 = "and (cast(DATEPART(yyyy,Payment_date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Payment_date),'00') as varchar(2))) between '" & Format(CDate(txt1.Text), "yyyy/MM") & "' and '" & Format(CDate(txt2.Text), "yyyy/MM") & "'"

            Chart_Sfe_Move()

        ElseIf txt1.Text.Length = 4 And txt2.Text.Length = 4 Then

            Select Case cbo_Search.SelectedIndex
                Case 0
                    y = 0
                Case 1
                    y = 1
                Case 2
                    y = 2
                Case 3
                    y = 2
            End Select
            st = "and cast(DATEPART(yyyy,Receipt_date) as varchar(4))  between '" & txt1.Text & "' and '" & txt2.Text & "'"
            st2 = "and cast(DATEPART(yyyy,Payment_date) as varchar(4))  between '" & txt1.Text & "' and '" & txt2.Text & "'"

            Chart_Sfe_Move_Year()
        End If

    End Sub

    Private Sub cbo_Band_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Band.SelectedIndexChanged
        If Not fin Then Return
        st = Nothing
        st2 = Nothing

        Chart_Sfe_Move()
    End Sub

    Sub Chart_Sfe_Move_Year()
        Chart1.Series(0).Points.Clear()
        Chart1.Series(1).Points.Clear()

        Chart1.Titles.Clear()
        Chart1.Series(1).IsVisibleInLegend = False
        Try
            Select Case y
                Case 0
                    If cbo_Ezn.SelectedIndex = 0 Then
                        Myconn.Filldataset("Select state,cast(DATEPART(yyyy,Payment_date) as varchar(4)) as Payment_Year,
                                        sum(amount) as amount from Payment group by cast(DATEPART(yyyy,P_Date) as varchar(4)),state
                                         having state ='True'" & st, "Payment", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Payment_Year"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next
                        Chart1.Series(0).Name = "كل أذونات الدفع"

                        T = Chart1.Titles.Add("كل أذونات الدفع")

                    ElseIf cbo_Ezn.SelectedIndex = 1 Then

                        Myconn.Filldataset("Select state,cast(DATEPART(yyyy,Receipt_date) as varchar(4)) as recive_Year,
                                        sum(amount) as amount from Receipt group by cast(DATEPART(yyyy,Receipt_date) as varchar(4)),state
                                         having state ='True'" & st, "Receipt", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("recive_Year"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next
                        Chart1.Series(0).Name = "كل أذونات الاستلام"
                        T = Chart1.Titles.Add("كل أذونات الاستلام")

                    ElseIf cbo_Ezn.SelectedIndex = 2 Then

                        Myconn.Filldataset("Select state,cast(DATEPART(yyyy,Receipt_date) as varchar(4)) as recive_Year,
                                        sum(amount) as amount from Receipt group by cast(DATEPART(yyyy,Receipt_date) as varchar(4)),state
                                         having state ='True'" & st, "Receipt", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("recive_Year"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next

                        Chart1.Series(0).Name = "كل أذونات الاستلام"
                        T = Chart1.Titles.Add("كل أذونات الدفع والاستلام")

                        Myconn.Filldataset2("Select state,cast(DATEPART(yyyy,Payment_date) as varchar(4)) as Payment_Year,
                                        sum(amount) as amount from Payment group by cast(DATEPART(yyyy,Payment_date) as varchar(4)),state
                                         having state ='True'" & st, "Payment", Me)

                        For i As Integer = 0 To Myconn.cur2.Count - 1
                            Me.Chart1.Series(1).Points.AddXY(Myconn.cur2.Current("Payment_Year"), Myconn.cur2.Current("amount"))
                            Myconn.cur2.Position += 1
                        Next
                        Chart1.Series(1).IsVisibleInLegend = True
                        Chart1.Series(1).Name = "كل أذونات الدفع "
                    End If
                ' ------------------------------------------------------------------------------------------------------------'
                Case 1 ' البند
                    If cbo_Ezn.SelectedIndex = 0 Then
                        Myconn.Filldataset("Select state,cast(DATEPART(yyyy,Payment_date) as varchar(4)) as Payment_Year,
                                        sum(amount) as amount from Payment group by cast(DATEPART(yyyy,Payment_date) as varchar(4)),state,PaymentID
                                         having state ='True' and PaymentID =" & CInt(cbo_Band.ComboBox.SelectedValue) & st2, "Payment", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Payment_Year"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next
                        Chart1.Series(0).Name = " أذونات الدفع لبند " & cbo_Band.Text

                        T = Chart1.Titles.Add(" أذونات الدفع لبند " & cbo_Band.Text)

                    ElseIf cbo_Ezn.SelectedIndex = 1 Then

                        Myconn.Filldataset("Select state,cast(DATEPART(yyyy,Receipt_date) as varchar(4)) as recive_Year,
                                        sum(amount) as amount from Receipt group by cast(DATEPART(yyyy,Receipt_date) as varchar(4)),state,itemID
                                         having state ='True' and itemID =" & CInt(cbo_Band.ComboBox.SelectedValue) & st, "Receipt", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("recive_Year"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next
                        Chart1.Series(0).Name = " أذونات الاستلام لبند " & cbo_Band.Text
                        T = Chart1.Titles.Add(" أذونات الاستلام لبند " & cbo_Band.Text)
                    End If
                '------------------------------------------------------------------------------------------
                Case 2 ' العميل أو المورد
                    If cbo_Ezn.SelectedIndex = 0 Then
                        Myconn.Filldataset("Select state,cast(DATEPART(yyyy,Payment_date) as varchar(4)) as Payment_Year,
                                        sum(amount) as amount from Payment group by cast(DATEPART(yyyy,Payment_date) as varchar(4)),state,specializationID
                                         having state ='True' and specializationID =" & CInt(cbo_Band.ComboBox.SelectedValue) & st2, "Payment", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Payment_Year"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next
                        Chart1.Series(0).Name = " أذونات الدفع لبند " & cbo_Band.Text

                        T = Chart1.Titles.Add(" أذونات الدفع لبند " & cbo_Band.Text)

                    ElseIf cbo_Ezn.SelectedIndex = 1 Then

                        Myconn.Filldataset("Select state,cast(DATEPART(yyyy,Receipt_date) as varchar(4)) as recive_Year,
                                        sum(amount) as amount from Receipt group by cast(DATEPART(yyyy,Receipt_date) as varchar(4)),state,Customer_ID
                                         having state ='True' and specializationID =" & CInt(cbo_Band.ComboBox.SelectedValue) & st, "Receipt", Me)

                        For i As Integer = 0 To Myconn.cur.Count - 1
                            Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("recive_Year"), Myconn.cur.Current("amount"))
                            Myconn.cur.Position += 1
                        Next
                        Chart1.Series(0).Name = " أذونات الاستلام لبند " & cbo_Band.Text
                        T = Chart1.Titles.Add(" أذونات الاستلام لبند " & cbo_Band.Text)
                    End If


            End Select
            Chart1.ChartAreas(0).Visible = True
            Chart1.Series(0).IsVisibleInLegend = True

            Chart_Title()
            st = Nothing
            st2 = Nothing
        Catch ex As Exception
            MsgBox("هناك خطأ ")
            Return
        End Try
    End Sub
End Class