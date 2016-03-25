Imports System.Windows.Forms.DataVisualization.Charting

Public Class frmPharm_Sales_Chart
    Dim Myconn As New connect
    Dim fin As Boolean
    Dim T As Title
    Dim x, y As Integer
    Dim st As String
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
    Sub Chart_Purchases()
        Try
            Select Case y
                Case 0
                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join  Drug_Groups c on b.GroupID = c.GroupID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state
                            having  state = 'true'" & st, "Drug_Purchases", Me)
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total_Price"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = "كل المبيعات"
                    T = Chart1.Titles.Add("مبيعات الأدوية والمستلزمات والمستحضرات")
                ' ------------------------------------------------------------------------------------------------------------'
                Case 1

                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join  Drug_Groups c on b.GroupID = c.GroupID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,b.GroupID
                            having  state = 'true' and b.GroupID =" & CInt(cbo_Group.ComboBox.SelectedValue) & st, "Drug_Sales", Me)
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total_Price"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = "مبيعات " & cbo_Group.Text
                    T = Chart1.Titles.Add("مبيعات " & cbo_Group.Text)


                '-----------------------------------------------------------------------------------------------------------------------'
                Case 2
                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join [dbo].[Customers]  c on a.Customer_ID = c.Customer_ID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,a.Customer_ID
                            having  state = 'true' and a.Customer_ID =" & CInt(cbo_Customer.ComboBox.SelectedValue) & st, "Drug_Sales", Me)
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total_Price"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = "مبيعات " & cbo_Customer.Text
                    T = Chart1.Titles.Add("مبيعات " & cbo_Customer.Text)

                '-----------------------------------------------------------------------------------------------------------------------
                Case 3
                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join [dbo].[Customers]  c on a.Customer_ID = c.Customer_ID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,a.Drug_ID
                            having  state = 'true' and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue) & st, "Drug_Sales", Me)
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total_Price"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = "مبيعات " & cbo_Drug.Text
                    T = Chart1.Titles.Add("مبيعات " & cbo_Drug.Text)

                '--------------------------------------------------------------------------------------------------------------------
                Case 4
                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join [dbo].[Customers]  c on a.Customer_ID = c.Customer_ID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,a.Customer_ID,a.Drug_ID
                            having  state = 'true' and  a.Customer_ID =" & CInt(cbo_Customer.ComboBox.SelectedValue) & "and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue) & st, "Drug_Sales", Me)
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total_Price"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = "مبيعات " & cbo_Drug.Text
                    T = Chart1.Titles.Add("مبيعات " & cbo_Drug.Text & " إلى العميل " & cbo_Customer.Text)
                '--------------------------------------------------------------------------------------------------------------------
                Case 5
                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join  Drug_Groups c on b.GroupID = c.GroupID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state
                            having  state = 'true'" & st, "Drug_Purchases", Me)
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("erning"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = "جميع الأرباح"
                    T = Chart1.Titles.Add("جميع الأرباح")
                '--------------------------------------------------------------------------------------------------------------------
                Case 6
                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join  Drug_Groups c on b.GroupID = c.GroupID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,b.GroupID
                            having  state = 'true' and  b.GroupID =" & CInt(cbo_Group.ComboBox.SelectedValue) & st, "Drug_Sales", Me)
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("erning"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = "أرباح " & cbo_Group.Text
                    T = Chart1.Titles.Add("أرباح " & cbo_Group.Text)
                '--------------------------------------------------------------------------------------------------------------------
                Case 7
                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join [dbo].[Customers]  c on a.Customer_ID = c.Customer_ID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,a.Customer_ID
                            having  state = 'true' and a.Customer_ID =" & CInt(cbo_Customer.ComboBox.SelectedValue) & st, "Drug_Sales", Me)
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("erning"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = "أرباح " & cbo_Customer.Text
                    T = Chart1.Titles.Add("أرباح " & cbo_Customer.Text)

                '--------------------------------------------------------------------------------------------------------------------
                Case 8
                    Myconn.Filldataset("select sum(a.Total_Price) as Total_Price,(cast(DATEPART(yyyy,a.Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,a.Bill_Date),'00') as varchar(2))) as Pur_Month,sum(a.Erning) as erning
                            from [dbo].[Drug_Sales] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join [dbo].[Customers]  c on a.Customer_ID = c.Customer_ID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,a.Drug_ID
                            having  state = 'true' and a.Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue) & st, "Drug_Sales", Me)
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("erning"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = "أرباح صنف " & cbo_Drug.Text
                    T = Chart1.Titles.Add("أرباح صنف " & cbo_Drug.Text)
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
    Private Sub frmPharm_Sales_Chart_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        fin = False
        Myconn.Fillcombo("select * from Drug_Groups order by GroupName", "Drug_Groups", "GroupID", "GroupName", Me, cbo_Group.ComboBox)
        Myconn.Fillcombo("select * from Customers order by Customer_Name", "Customers", "Customer_ID", "Customer_Name", Me, cbo_Customer.ComboBox)
        Myconn.Fillcombo("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug.ComboBox)
        fin = True

    End Sub
    Private Sub cbo_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Data.SelectedIndexChanged
        Chart1.Series(0).Points.Clear()
        Chart1.Titles.Clear()
        'S = cbo_Data.SelectedIndex
        Select Case cbo_Data.ComboBox.SelectedIndex
            Case 0 ' كل مبيعات
                cbo_Customer.Visible = False
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                y = 0
                Chart_Purchases()

            Case 1 ' مبيعات حسب الفئة
                cbo_Customer.Visible = False
                cbo_Group.Visible = True
                cbo_Drug.Visible = False
                y = 1

            Case 2 ' العميل
                cbo_Customer.Visible = True
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                fin = False
                fin = True
                y = 2

            Case 3 ' حسب الصنف
                cbo_Customer.Visible = False
                cbo_Group.Visible = False
                cbo_Drug.Visible = True
                y = 3

            Case 4 ' العميل والصنف
                cbo_Customer.Visible = True
                cbo_Group.Visible = False
                cbo_Drug.Visible = True
                y = 4
            Case 5 ' الأرباح
                cbo_Customer.Visible = False
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                y = 5
                Chart_Purchases()
            Case 6 ' الأرباح حسب الفئة
                cbo_Customer.Visible = False
                cbo_Group.Visible = True
                cbo_Drug.Visible = False
                y = 6
            Case 7
                cbo_Customer.Visible = True
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                y = 7
            Case 8
                cbo_Customer.Visible = False
                cbo_Group.Visible = False
                cbo_Drug.Visible = True
                y = 8
        End Select
        'cbo_Setting_SelectedIndexChanged(Nothing, Nothing)
    End Sub
    Private Sub cbo_Group_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Group.SelectedIndexChanged
        If Not fin Then Return
        Chart1.Series(0).Points.Clear()
        Chart1.Titles.Clear()

        Chart_Purchases()
    End Sub
    Private Sub cbo_Customer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Customer.SelectedIndexChanged
        If y = 4 Then
            cbo_Drug.Visible = True
            Return
        End If
        If Not fin Then Return
        Chart1.Series(0).Points.Clear()
        Chart1.Titles.Clear()

        Chart_Purchases()
    End Sub
    Private Sub cbo_Drug_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Drug.SelectedIndexChanged
        If Not fin Then Return
        Chart1.Series(0).Points.Clear()
        Chart1.Titles.Clear()
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
        Chart1.Series(0).Points.Clear()
        Chart1.Titles.Clear()
        'S = cbo_Data.SelectedIndex
        Select Case cbo_Data.ComboBox.SelectedIndex
            Case 0 ' كل مبيعات
                cbo_Customer.Visible = False
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                y = 0


            Case 1 ' مبيعات حسب الفئة
                cbo_Customer.Visible = False
                cbo_Group.Visible = True
                cbo_Drug.Visible = False
                y = 1

            Case 2 ' العميل
                cbo_Customer.Visible = True
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                fin = False
                fin = True
                y = 2

            Case 3 ' حسب الصنف
                cbo_Customer.Visible = False
                cbo_Group.Visible = False
                cbo_Drug.Visible = True
                y = 3

            Case 4 ' العميل والصنف
                cbo_Customer.Visible = True
                cbo_Group.Visible = False
                cbo_Drug.Visible = True
                y = 4
            Case 5 ' الأرباح
                cbo_Customer.Visible = False
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                y = 5
                Chart_Purchases()
            Case 6 ' الأرباح حسب الفئة
                cbo_Customer.Visible = False
                cbo_Group.Visible = True
                cbo_Drug.Visible = False
                y = 6
            Case 7
                cbo_Customer.Visible = True
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                y = 7
            Case 8
                cbo_Customer.Visible = False
                cbo_Group.Visible = False
                cbo_Drug.Visible = True
                y = 8
        End Select
        st = "and cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)) between '" & Format(CDate(txt1.Text), "yyyy/MM") & "' and '" & Format(CDate(txt2.Text), "yyyy/MM") & "'"

        Chart_Purchases()
    End Sub


End Class