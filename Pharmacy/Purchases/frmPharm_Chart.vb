Imports System.Windows.Forms.DataVisualization.Charting

Public Class frmPharm_Chart
    Dim Myconn As New connect
    Dim y As Integer
    Dim T As Title
    Dim fin As Boolean
    Dim st As String
    Sub Chart_Purchases()
        Try
            Select Case y
                Case 0
                    Myconn.Filldataset("select sum(Total_Price_tax) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Pur_Month
                            from [dbo].[Drug_Purchases] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state
                            having  state = 'true'" & st & "order by Pur_Month", "Drug_Purchases", Me)
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = "كل المشتريات"
                    T = Chart1.Titles.Add("مشتريات الأدوية والمستلزمات")
                ' ------------------------------------------------------------------------------------------------------------'
                Case 1
                    Myconn.Filldataset("select sum(Total_Price_tax) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Pur_Month
                            from [dbo].[Drug_Purchases] a
                            left join [dbo].[Drugs] b on a.Drug_ID = b.Drug_ID
                            left join  Drug_Groups c on b.GroupID = c.GroupID
                            group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),state,b.GroupID
                            having  state = 'true' and b.GroupID = " & CInt(cbo_Group.ComboBox.SelectedValue) & st & "order by Pur_Month", "Drug_Purchases", Me)
                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = "مشتريات " & cbo_Group.Text
                    T = Chart1.Titles.Add(" مشتريات " & cbo_Group.Text)
                '-----------------------------------------------------------------------------------------------------------------------'
                Case 2
                    Myconn.Filldataset("select Supplier_ID,sum(Total_Price_tax) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Pur_Month
                                    from [dbo].[Drug_Purchases] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),Supplier_ID,state
                                    having  state = 'true' and Supplier_ID =" & CInt(cbo_Suplier.ComboBox.SelectedValue) & st & "order by Pur_Month", "Drug_Purchases", Me)

                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = " مشتريات شركة " & cbo_Suplier.Text
                    T = Chart1.Titles.Add(" مشتريات شركة " & cbo_Suplier.Text)
                '-----------------------------------------------------------------------------------------------------------------------
                Case 3
                    Myconn.Filldataset("select Drug_ID,sum(Total_Price_tax) as Total,(cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2))) as Pur_Month
                                    from [dbo].[Drug_Purchases] group by cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)),Drug_ID,state
                                    having state = 'true' and Drug_ID =" & CInt(cbo_Drug.ComboBox.SelectedValue) & st & "order by Pur_Month", "Drug_Purchases", Me)

                    For i As Integer = 0 To Myconn.cur.Count - 1
                        Me.Chart1.Series(0).Points.AddXY(Myconn.cur.Current("Pur_Month"), Myconn.cur.Current("Total"))
                        Myconn.cur.Position += 1
                    Next
                    Chart1.Series(0).Name = "مشتريات " & cbo_Drug.Text
                    T = Chart1.Titles.Add("مشتريات " & cbo_Drug.Text)
                    '--------------------------------------------------------------------------------------------------------------------
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
    Private Sub frmPharm_Chart_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Left = 0
        Label3.Width = Me.Width
        fin = False
        Myconn.Fillcombo("select * from Drug_Groups order by GroupName", "Drug_Groups", "GroupID", "GroupName", Me, cbo_Group.ComboBox)
        Myconn.Fillcombo("select * from Supplier order by Supplier_Name", "Supplier", "Supplier_ID", "Supplier_Name", Me, cbo_Suplier.ComboBox)
        Myconn.Fillcombo("select * from Drugs order by Drug_Name", "Drugs", "Drug_ID", "Drug_Name", Me, cbo_Drug.ComboBox)
        fin = True
    End Sub
    Private Sub cbo_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Data.SelectedIndexChanged
        Chart1.Series(0).Points.Clear()
        Chart1.Titles.Clear()

        Select Case cbo_Data.ComboBox.SelectedIndex
            Case 0 ' كل المشتريات
                cbo_Suplier.Visible = False
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                y = 0
                Chart_Purchases()

            Case 1 ' مشتريات حسب الفئة
                cbo_Suplier.Visible = False
                cbo_Group.Visible = True
                cbo_Drug.Visible = False
                y = 1

            Case 2 ' الشركة
                cbo_Suplier.Visible = True
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                y = 2

            Case 3 ' حسب الصنف
                cbo_Suplier.Visible = False
                cbo_Group.Visible = False
                cbo_Drug.Visible = True
                y = 3

        End Select

    End Sub
    Private Sub cbo_Suplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Suplier.SelectedIndexChanged
        If Not fin Then Return
        Chart1.Series(0).Points.Clear()
        Chart1.Titles.Clear()
        Chart_Purchases()
    End Sub
    Private Sub cbo_Group_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Group.SelectedIndexChanged
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
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Chart1.Series(0).IsVisibleInLegend = False
        'Chart1.ChartAreas(0).Visible = False



    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Chart1.Series(0).Points.Clear()
        Chart1.Titles.Clear()
        If txt1.Text = "" Or txt2.Text = "" Then
            MsgBox("من فضلك أدخل التاريخ")
            Return
        End If
        Select Case cbo_Data.ComboBox.SelectedIndex
            Case 0 ' كل المشتريات
                cbo_Suplier.Visible = False
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                y = 0


            Case 1 ' مشتريات حسب الفئة
                cbo_Suplier.Visible = False
                cbo_Group.Visible = True
                cbo_Drug.Visible = False
                y = 1

            Case 2 ' الشركة
                cbo_Suplier.Visible = True
                cbo_Group.Visible = False
                cbo_Drug.Visible = False
                y = 2

            Case 3 ' حسب الصنف
                cbo_Suplier.Visible = False
                cbo_Group.Visible = False
                cbo_Drug.Visible = True
                y = 3

        End Select
        st = "and cast(DATEPART(yyyy,Bill_Date) as varchar(4)) + '/' + cast(format(DATEPART(MM,Bill_Date),'00') as varchar(2)) between '" & Format(CDate(txt1.Text), "yyyy/MM") & "' and '" & Format(CDate(txt2.Text), "yyyy/MM") & "'"

        Chart_Purchases()
    End Sub

    Private Sub cbo_Drug_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_Drug.SelectedIndexChanged
        If Not fin Then Return
        Chart1.Series(0).Points.Clear()
        Chart1.Titles.Clear()
        Chart_Purchases()
    End Sub
End Class