Imports System.Windows.Forms.DataVisualization.Charting

Public Class frmStock_Chart
    Dim Myconn As New connect
    Dim x, y As Integer
    Dim T As Title
    Dim fin As Boolean
    Dim st As String

    Sub Chart_Purchases()
        If cbo_Group.SelectedIndex = -1 Then Return
        Chart1.Series(0).Points.Clear()
        Chart1.Titles.Clear()
        Try
            Select Case cbo_Data.ComboBox.SelectedIndex
                Case 0 ' العمليات
                    Select Case cbo_Group.SelectedIndex
                        Case 0 ' المبيعات
                            Myconn.Filldataset("select sum(Total_Price) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Sales_Month
                            from [dbo].[Stocks_Sales] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,Stock_ID
                            having  state = 'true' and Stock_ID = 2" & st & "order by Sales_Month", "Stocks_Sales", Me)
                            For i As Integer = 0 To Myconn.cur.Count - 1
                                Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Sales_Month"), Myconn.cur.Current("Total"))
                                Myconn.cur.Position += 1
                            Next
                            Chart1.Series(0).Name = "مبيعات مخزن العمليات"
                            T = Chart1.Titles.Add("مبيعات مخزن العمليات")

                        Case 1 ' المشتريات
                            Myconn.Filldataset("select sum(Total) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Pur_Month
                            from [dbo].[Stocks_Purchases] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,Stock_ID
                            having  state = 'true' and Stock_ID = 2" & st & "order by Pur_Month", "Stocks_Purchases", Me)
                            For i As Integer = 0 To Myconn.cur.Count - 1
                                Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total"))
                                Myconn.cur.Position += 1
                            Next
                            Chart1.Series(0).Name = "مشتريات مخزن العمليات"
                            T = Chart1.Titles.Add("مشتريات مخزن العمليات")

                    End Select
                '------------------------------------------------------------------------------------------------------------------
                Case 1 ' الحضانات
                    Select Case cbo_Group.SelectedIndex
                        Case 0 ' المبيعات
                            Myconn.Filldataset("select sum(Total_Price) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Sales_Month
                            from [dbo].[Stocks_Sales] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,Stock_ID
                            having  state = 'true' and Stock_ID = 4" & st & "order by Sales_Month", "Stocks_Sales", Me)
                            For i As Integer = 0 To Myconn.cur.Count - 1
                                Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Sales_Month"), Myconn.cur.Current("Total"))
                                Myconn.cur.Position += 1
                            Next
                            Chart1.Series(0).Name = "مبيعات مخزن الحضانات"
                            T = Chart1.Titles.Add("مبيعات مخزن الحضانات")

                        Case 1 ' المشتريات
                            Myconn.Filldataset("select sum(Total) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Pur_Month
                            from [dbo].[Stocks_Purchases] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,Stock_ID
                            having  state = 'true' and Stock_ID = 4" & st & "order by Pur_Month", "Stocks_Purchases", Me)
                            For i As Integer = 0 To Myconn.cur.Count - 1
                                Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total"))
                                Myconn.cur.Position += 1
                            Next
                            Chart1.Series(0).Name = "مشتريات مخزن الحضانات"
                            T = Chart1.Titles.Add("مشتريات مخزن الحضانات")

                    End Select
                '------------------------------------------------------------------------------------------------------------------
                Case 2 ' الطوارىء
                    Select Case cbo_Group.SelectedIndex
                        Case 0 ' المبيعات
                            Myconn.Filldataset("select sum(Total_Price) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Sales_Month
                            from [dbo].[Stocks_Sales] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,Stock_ID
                            having  state = 'true' and Stock_ID = 3" & st & "order by Sales_Month", "Stocks_Sales", Me)
                            For i As Integer = 0 To Myconn.cur.Count - 1
                                Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Sales_Month"), Myconn.cur.Current("Total"))
                                Myconn.cur.Position += 1
                            Next
                            Chart1.Series(0).Name = "مبيعات مخزن الطوارىء"
                            T = Chart1.Titles.Add("مبيعات مخزن الطوارىء")

                        Case 1 ' المشتريات
                            Myconn.Filldataset("select sum(Total) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Pur_Month
                            from [dbo].[Stocks_Purchases] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,Stock_ID
                            having  state = 'true' and Stock_ID = 3" & st & "order by Pur_Month", "Stocks_Purchases", Me)
                            For i As Integer = 0 To Myconn.cur.Count - 1
                                Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total"))
                                Myconn.cur.Position += 1
                            Next
                            Chart1.Series(0).Name = "مشتريات مخزن الطوارىء"
                            T = Chart1.Titles.Add("مشتريات مخزن الطوارىء")

                    End Select
                '------------------------------------------------------------------------------------------------------------------
                Case 3 ' الاقامة
                    Select Case cbo_Group.SelectedIndex
                        Case 0 ' المبيعات
                            Myconn.Filldataset("select sum(Total_Price) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Sales_Month
                            from [dbo].[Stocks_Sales] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,Stock_ID
                            having  state = 'true' and Stock_ID = 5" & st & "order by Sales_Month", "Stocks_Sales", Me)
                            For i As Integer = 0 To Myconn.cur.Count - 1
                                Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Sales_Month"), Myconn.cur.Current("Total"))
                                Myconn.cur.Position += 1
                            Next
                            Chart1.Series(0).Name = "مبيعات مخزن الاقامة"
                            T = Chart1.Titles.Add("مبيعات مخزن الاقامة")

                        Case 1 ' المشتريات
                            Myconn.Filldataset("select sum(Total) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Pur_Month
                            from [dbo].[Stocks_Purchases] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,Stock_ID
                            having  state = 'true' and Stock_ID = 5" & st & "order by Pur_Month", "Stocks_Purchases", Me)
                            For i As Integer = 0 To Myconn.cur.Count - 1
                                Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total"))
                                Myconn.cur.Position += 1
                            Next
                            Chart1.Series(0).Name = "مشتريات مخزن الاقامة"
                            T = Chart1.Titles.Add("مشتريات مخزن الاقامة")

                    End Select
                '------------------------------------------------------------------------------------------------------------------
                Case 4 ' الكل
                    Select Case cbo_Group.SelectedIndex
                        Case 0 ' المبيعات
                            Myconn.Filldataset("select sum(Total_Price) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Sales_Month
                            from [dbo].[Stocks_Sales] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state
                            having  state = 'true'" & st & "order by Sales_Month", "Stocks_Sales", Me)
                            For i As Integer = 0 To Myconn.cur.Count - 1
                                Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Sales_Month"), Myconn.cur.Current("Total"))
                                Myconn.cur.Position += 1
                            Next
                            Chart1.Series(0).Name = "مبيعات المخازن"
                            T = Chart1.Titles.Add("مبيعات المخازن ")

                        Case 1 ' المشتريات
                            Myconn.Filldataset("select sum(Total) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Pur_Month
                            from [dbo].[Stocks_Purchases] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state
                            having  state = 'true'" & st & "order by Pur_Month", "Stocks_Purchases", Me)
                            For i As Integer = 0 To Myconn.cur.Count - 1
                                Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total"))
                                Myconn.cur.Position += 1
                            Next
                            Chart1.Series(0).Name = "مشتريات المخازن "
                            T = Chart1.Titles.Add("مشتريات المخازن ")

                    End Select
                    '------------------------------------------------------------------------------------------------------------------

            End Select
            Chart_Title()
            st = Nothing
        Catch ex As Exception
            MsgBox("هناك خطأ ")
            Return
        End Try
    End Sub
    Sub Chart_Title()
        '~~> Display Data Labels
        Chart1.Series(0).IsValueShownAsLabel = True
        'Chart1.Series("الأدوية").IsValueShownAsLabel = True
        '~~> Setting label's Fore Color
        Chart1.Series(0).LabelForeColor = Color.Red
        'Chart1.Series("الأدوية").LabelForeColor = Color.Red
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

            .BorderDashStyle = ChartDashStyle.DashDotDot '~~> Changing the Border Dash Style of the Title 
        End With
    End Sub
    Private Sub frmStock_Chart_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        fin = False
    End Sub
    Private Sub cbo_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Data.SelectedIndexChanged
        Chart_Purchases()
    End Sub
    Private Sub cbo_Group_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Group.SelectedIndexChanged
        Chart_Purchases()
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
                'Chart1.Series("الأدوية").ChartType = value
                Case 3
                    Dim value = DirectCast(Me.cbo_View.ComboBox.SelectedValue, ChartColorPalette)
                    Chart1.Palette = value
            End Select
        Catch ex As Exception
            MsgBox("قم باختيار شكل آخر ")
            Return
        End Try
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        st = "and cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)) between '" & Format(CDate(txt1.Text), "yyyy/MM") & "' and '" & Format(CDate(txt2.Text), "yyyy/MM") & "'"
        Chart_Purchases()
    End Sub

End Class