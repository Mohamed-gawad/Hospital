﻿Public Class frmPharm_Supplier_Report
    Dim Myconn As New connect
    Sub fillgrd()

        Myconn.Filldataset("Select a.Supplier_ID,b.Supplier_Name,isnull(sum(a.Total_Price_tax),0) As Total ,isnull(d.amount,0) as amount,
                             (isnull(sum(a.Total_Price_tax),0) - isnull(d.amount,0) ) as Rest from [dbo].[Drug_Purchases] a
                             left join [dbo].[Supplier] b on a.Supplier_ID = b.Supplier_ID
		                     left join (select State,Supplier_ID,sum(amount) as amount from [dbo].[Pharm_Safe_Payment] group by Supplier_ID,State having State ='true') d
		                     on a.Supplier_ID = d.Supplier_ID
		                     group by b.Supplier_Name,a.Supplier_ID,a.State,d.amount having a.State = 'True'
		                     order by a.Supplier_ID", "Drug_Sales", Me)
        Try
            drg.Rows.Clear()

            For i As Integer = 0 To Myconn.cur.Count - 1
                drg.Rows.Add()
                drg.Rows(i).Cells(0).Value = i + 1
                drg.Rows(i).Cells(1).Value = Myconn.cur.Current("Supplier_Name")
                drg.Rows(i).Cells(2).Value = Myconn.cur.Current("Supplier_ID")
                drg.Rows(i).Cells(3).Value = Myconn.cur.Current("Total")
                drg.Rows(i).Cells(4).Value = Myconn.cur.Current("amount")
                drg.Rows(i).Cells(5).Value = Myconn.cur.Current("Rest")
                drg.Rows(i).Cells(6).Value = clsNumber.nTOword(Myconn.cur.Current("Rest"))

                If Myconn.cur.Current("Rest") > 0 Then
                    drg.Rows(i).Cells(7).Value = "مدين"
                    drg.Rows(i).DefaultCellStyle.BackColor = Color.Pink
                ElseIf Myconn.cur.Current("Rest") = 0 Then
                    drg.Rows(i).Cells(7).Value = "خالص"
                ElseIf Myconn.cur.Current("Rest") < 0 Then
                    drg.Rows(i).Cells(7).Value = "دائن"
                End If

                Myconn.cur.Position += 1
            Next
            Myconn.Sum_drg(drg, 5, Label2, Label3)

        Catch ex As Exception
            MsgBox("هناك خطأ في البيانات")
        End Try

    End Sub

    Private Sub frmPharm_Supplier_Report_Load(sender As Object, e As EventArgs) Handles Me.Load
        fillgrd()

    End Sub
End Class