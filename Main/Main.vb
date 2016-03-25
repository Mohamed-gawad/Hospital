Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.ComponentModel


Public Class Main


    Private Sub مصروفات_Click(sender As Object, e As EventArgs)
        'Report.Visible = False
    End Sub

    Private Sub إضافةقسمToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Add_department.Click
        frmkissm.MdiParent = Me
        frmkissm.Show()

    End Sub
    Private Sub إضافةطبيبToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Add_doctor.Click
        frmdoctors.MdiParent = Me
        frmdoctors.Show()

    End Sub
    Private Sub إضافةبياناتمريضToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Add_Patient.Click
        frmPatient.MdiParent = Me
        frmPatient.Show()
    End Sub

    Private Sub الموظفين_Click(sender As Object, e As EventArgs) Handles Employees.Click

    End Sub
    Private Sub ToolStripTextBox1_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub حركةالخزنةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Safe_move.Click
        frmSafe_move.MdiParent = Me
        frmSafe_move.Show()

    End Sub
    Private Sub إضافةمسمىوظيفيToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Add_job.Click
        frmEmployees_Data.MdiParent = Me
        frmEmployees_Data.Show()

    End Sub
    Private Sub إضافةبياناتموظفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Add_employee.Click
        frmEmployee.MdiParent = Me
        frmEmployee.Show()

    End Sub

    Private Sub إعداداتالاتصالToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Setting_conn.Click
        frmconnection.MdiParent = Me
        frmconnection.Show()

    End Sub
    Private Sub بياناتالمؤسسةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles co_data.Click
        frmSetting.MdiParent = Me
        frmSetting.Show()

    End Sub
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Report.Visible = False
        Report_recives.Visible = False
        MenuStrip.Renderer = New clsMenuRenderer
        'Me.Text = My.Settings.H_Name & "  -  " & My.Settings.H_address
        bgColor()
        'For Each c As Control In Me.Controls
        '    If TypeOf c Is MdiClient Then
        '        AddHandler c.Paint, AddressOf myMdiControlPaint
        '        AddHandler c.SizeChanged, AddressOf myMdiControlResize
        '        Exit For
        '    End If
        'Next
        'MenuStrip.BackColor = Color.CornflowerBlue

        'For Each blah As ToolStripMenuItem In MenuStrip.Items

        '    blah.BackColor = Color.CornflowerBlue

        '    blah.ForeColor = Color.Black

        '    For Each meh As ToolStripMenuItem In blah.DropDownItems

        '        meh.BackColor = Color.Black

        '        meh.ForeColor = Color.White

        '        For Each lolCat As ToolStripMenuItem In meh.DropDownItems

        '            lolCat.BackColor = Color.Black

        '            lolCat.ForeColor = Color.White

        '        Next

        '    Next

        'Next

    End Sub



    Private Sub خدماتالأقسامToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Service_department.Click
        frmCervices.MdiParent = Me
        frmCervices.Show()

    End Sub

    Private Sub إضافةصنفToolStripMenuItem_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub فاتورةللمخزنToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Bill_store.Click
        frmAdd_Store_Operation.MdiParent = Me
        frmAdd_Store_Operation.Show()
        frmAdd_Store_Operation.Text = "مخزن العمليات - فاتورة مشتريات"

    End Sub

    Private Sub إضافةمستخدمجديدToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Add_user.Click
        frmUsers.MdiParent = Me
        frmUsers.Show()

    End Sub

    Private Sub فاتورةمستهلكةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles bill_oper_customer.Click
        frmDrage_Store_Operation.MdiParent = Me
        frmDrage_Store_Operation.Show()

    End Sub

    Private Sub الفواتيرالمستهلكةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles bills_oper_stores.Click
        frmBills_Add_Store_Operation.MdiParent = Me
        frmBills_Add_Store_Operation.Show()

    End Sub

    Private Sub الفواتيرالمستهلكةToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles bills_oper_customers.Click
        frmBills_Add_Drage_Operation.MdiParent = Me
        frmBills_Add_Drage_Operation.Show()

    End Sub

    Private Sub حركةصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles kind_oper_move.Click
        frmKind_Move_Store_Operation.MdiParent = Me
        frmKind_Move_Store_Operation.Show()

    End Sub

    Private Sub رصيدالمخزنToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Stock_oper.Click
        frmDrug_Operation_Stock.MdiParent = Me
        frmDrug_Operation_Stock.Show()

    End Sub

    Private Sub حركةالأصنافToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles kinds_oper_move.Click
        frmKinds_Move_Store_Operation.MdiParent = Me
        frmKinds_Move_Store_Operation.Show()
    End Sub

    Private Sub فاتورةللمخزنToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles فاتورةللمخزنToolStripMenuItem1.Click
        frmAdd_Store_Emergency.MdiParent = Me
        frmAdd_Store_Emergency.Show()

        frmAdd_Store_Emergency.Text = "مخزن الطوارىء - فاتورة مشتريات"
    End Sub

    Private Sub فاتورةمستهلكةToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles فاتورةمستهلكةToolStripMenuItem1.Click
        frmDrage_Store_Emergency.MdiParent = Me
        frmDrage_Store_Emergency.Show()

    End Sub

    Private Sub الفواتيرالمضافةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles الفواتيرالمضافةToolStripMenuItem.Click
        frmBills_Add_Store_Emergency.MdiParent = Me
        frmBills_Add_Store_Emergency.Show()

    End Sub

    Private Sub الفواتيرالمستهلكةToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles الفواتيرالمستهلكةToolStripMenuItem2.Click
        frmBills_Add_Drage_Emergency.MdiParent = Me
        frmBills_Add_Drage_Emergency.Show()

    End Sub

    Private Sub حركةصنفToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles حركةصنفToolStripMenuItem1.Click
        frmKind_Move_Store_Emergency.MdiParent = Me
        frmKind_Move_Store_Emergency.Show()


    End Sub

    Private Sub حركةالأصنافToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles حركةالأصنافToolStripMenuItem1.Click
        frmKinds_Move_Store_Emergency.MdiParent = Me
        frmKinds_Move_Store_Emergency.Show()


    End Sub

    Private Sub رصيدالمخزنToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles رصيدالمخزنToolStripMenuItem1.Click
        frmDrug_Emergency_Stock.MdiParent = Me
        frmDrug_Emergency_Stock.Show()


    End Sub

    Private Sub القسمالداخليToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Internal_Section.Click
        frmInternal_section.MdiParent = Me
        frmInternal_section.Show()

    End Sub

    Private Sub فاتورةللمخزنToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles فاتورةللمخزنToolStripMenuItem2.Click
        frmAdd_Store_Incubator.MdiParent = Me
        frmAdd_Store_Incubator.Show()

    End Sub

    Private Sub فاتورةمستهلكةToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles فاتورةمستهلكةToolStripMenuItem2.Click
        frmDrage_Store_Incubator.MdiParent = Me
        frmDrage_Store_Incubator.Show()

    End Sub

    Private Sub الفواتيرالمضافةToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles الفواتيرالمضافةToolStripMenuItem1.Click

        frmBills_Add_Store_incubator.MdiParent = Me
        frmBills_Add_Store_incubator.Show()
    End Sub

    Private Sub الفواتيرالمستهلكةToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles الفواتيرالمستهلكةToolStripMenuItem3.Click
        frmBills_Add_Drage_Incubator.MdiParent = Me
        frmBills_Add_Drage_Incubator.Show()
    End Sub

    Private Sub حركةصنفToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles حركةصنفToolStripMenuItem2.Click
        frmKind_Move_Store_Incubator.MdiParent = Me
        frmKind_Move_Store_Incubator.Show()
    End Sub

    Private Sub حركةالأصنافToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles حركةالأصنافToolStripMenuItem2.Click
        frmKinds_Move_Store_Incubator.MdiParent = Me
        frmKinds_Move_Store_Incubator.Show()
    End Sub

    Private Sub رصيدالمخزنToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles رصيدالمخزنToolStripMenuItem2.Click
        frmDrug_Incubator_Stock.MdiParent = Me
        frmDrug_Incubator_Stock.Show()
    End Sub

    Private Sub فاتورةللمخزنToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles فاتورةللمخزنToolStripMenuItem3.Click
        frmAdd_Store_Staying.MdiParent = Me
        frmAdd_Store_Staying.Show()
    End Sub

    Private Sub فاتورةمستهلكةToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles فاتورةمستهلكةToolStripMenuItem3.Click
        frmDrage_Store_Staying.MdiParent = Me
        frmDrage_Store_Staying.Show()
    End Sub

    Private Sub الفواتيرالمضافةToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles الفواتيرالمضافةToolStripMenuItem2.Click
        frmBills_Add_Store_Staying.MdiParent = Me
        frmBills_Add_Store_Staying.Show()
    End Sub

    Private Sub الفواتيرالمستهلكةToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles الفواتيرالمستهلكةToolStripMenuItem4.Click
        frmBills_Add_Drage_Staying.MdiParent = Me
        frmBills_Add_Drage_Staying.Show()
    End Sub

    Private Sub حركةصنفToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles حركةصنفToolStripMenuItem3.Click
        frmKind_Move_Store_Staying.MdiParent = Me
        frmKind_Move_Store_Staying.Show()
    End Sub

    Private Sub حركةالأصنافToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles حركةالأصنافToolStripMenuItem3.Click
        frmKinds_Move_Store_Staying.MdiParent = Me
        frmKinds_Move_Store_Staying.Show()
    End Sub

    Private Sub رصيدالمخزنToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles رصيدالمخزنToolStripMenuItem3.Click
        frmDrug_Staying_Stock.MdiParent = Me
        frmDrug_Staying_Stock.Show()
    End Sub

    Private Sub Emergency_Click(sender As Object, e As EventArgs) Handles Emergency.Click
        frmTry.MdiParent = Me
        frmTry.Show()

    End Sub

    Private Sub All_kinds_move_Click(sender As Object, e As EventArgs) Handles All_kinds_move.Click
        frmKinds_Move_Store_For_All.MdiParent = Me
        frmKinds_Move_Store_For_All.Show()

    End Sub
    Private Sub kinds_stores_move_Click(sender As Object, e As EventArgs) Handles kinds_stores_move.Click
        frmKind_Move_Store_For_All.MdiParent = Me
        frmKind_Move_Store_For_All.Show()
    End Sub
    Private Sub Operation_tools_Click(sender As Object, e As EventArgs) Handles Operation_tools.Click
        frmOperation_Tools.MdiParent = Me
        frmOperation_Tools.Show()
    End Sub
    Private Sub Car_tools_Click(sender As Object, e As EventArgs) Handles Car_tools.Click
        frmCar_Tools.MdiParent = Me
        frmCar_Tools.Show()
    End Sub
#Region "Change Background Color of MDIParent : bgcolor()"
    Private Sub bgColor()
        Dim child As Control
        For Each child In Me.Controls
            If TypeOf child Is MdiClient Then
                child.BackColor = Color.LavenderBlush
                'child.BackgroundImage = Me.BackgroundImage

                Exit For
            End If
        Next
        child = Nothing
    End Sub
#End Region
    'Private Sub myMdiControlPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    '    e.Graphics.DrawImage(Me.PictureBox1.BackgroundImage, 0, 0, Me.Width, Me.Height)
    'End Sub

    'Private Sub myMdiControlResize(ByVal sender As Object, ByVal e As System.EventArgs)

    '    CType(sender, MdiClient).Invalidate()
    'End Sub

    Private Sub Reservation_clinic_Click(sender As Object, e As EventArgs) Handles Reservation_clinic.Click
        frmClinics.MdiParent = Me
        frmClinics.Show()
    End Sub
    Private Sub Reservation_operation_Click(sender As Object, e As EventArgs) Handles Reservation_operation.Click
        frmOperatin_Reserve.MdiParent = Me
        frmOperatin_Reserve.Show()
    End Sub
    Private Sub ملفالمريضToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Save_Patient_Files.Click
        frmPatient_Files.MdiParent = Me
        frmPatient_Files.Show()
    End Sub
    Private Sub out_Click(sender As Object, e As EventArgs) Handles out.Click
        End
    End Sub
    Private Sub Add_kind_stock_Click(sender As Object, e As EventArgs) Handles Add_kind_stock.Click
        frmAdd_Drug.MdiParent = Me
        frmAdd_Drug.Show()
    End Sub
    Private Sub Add_Stor_Click(sender As Object, e As EventArgs) Handles Add_Stor.Click
        frmAdd_store.MdiParent = Me
        frmAdd_store.Show()
    End Sub
    Private Sub Add_factory_Click(sender As Object, e As EventArgs) Handles Add_factory.Click
        frmAdd_Co.MdiParent = Me
        frmAdd_Co.Show()
    End Sub
    Private Sub Pharm_Add_buy_bill_Click(sender As Object, e As EventArgs) Handles Pharm_Add_buy_bill.Click
        frmPharm_Purchases_bill.MdiParent = Me
        frmPharm_Purchases_bill.Show()
    End Sub
    Private Sub About_programe_Click(sender As Object, e As EventArgs) Handles About_programe.Click
        frmTry.MdiParent = Me
        frmTry.Show()
    End Sub
    Private Sub Pharm_Add_sale_bill_Click(sender As Object, e As EventArgs) Handles Pharm_Add_sale_bill.Click
        frmPharm_Sales.MdiParent = Me
        frmPharm_Sales.Show()
    End Sub
    Private Sub Add_Customer_Click(sender As Object, e As EventArgs) Handles Add_Customer.Click
        frmCustomer.MdiParent = Me
        frmCustomer.Show()
    End Sub
    Private Sub Pharm_buy_bills_Click(sender As Object, e As EventArgs) Handles Pharm_buy_bills.Click
        frmPharm_Bill_Purchases.MdiParent = Me
        frmPharm_Bill_Purchases.Show()
    End Sub
    Private Sub Pharm_returned_buy_Click(sender As Object, e As EventArgs) Handles Pharm_returned_buy.Click
        frmReturns_Drug.MdiParent = Me
        frmReturns_Drug.Show()
    End Sub
    Private Sub Pharm_returned_buy_kinds_Click(sender As Object, e As EventArgs) Handles Pharm_returned_buy_kinds.Click
        frmPharm_Chart.MdiParent = Me
        frmPharm_Chart.Show()
    End Sub
    Private Sub Pharm_total_purchase_Click(sender As Object, e As EventArgs) Handles Pharm_total_purchase.Click
        frmPurchases_monthly.MdiParent = Me
        frmPurchases_monthly.Show()
    End Sub
    Private Sub Pharm_sale_bills_Click(sender As Object, e As EventArgs) Handles Pharm_sale_bills.Click
        frmSales_Bill.MdiParent = Me
        frmSales_Bill.Show()
    End Sub
    Private Sub Pharm_erad_Click(sender As Object, e As EventArgs) Handles Pharm_erad.Click
        frmPharm_daily_Purchases.MdiParent = Me
        frmPharm_daily_Purchases.Show()
    End Sub
    Private Sub Pharm_returned_kind_Click(sender As Object, e As EventArgs) Handles Pharm_returned_kind.Click
        frmReturns_Sales.MdiParent = Me
        frmReturns_Sales.Show()
    End Sub
    Private Sub Pharm_Total_month_Click(sender As Object, e As EventArgs) Handles Pharm_Total_month.Click
        frmSales_monthly.MdiParent = Me
        frmSales_monthly.Show()
    End Sub

    Private Sub Pharm_returned_kinds_Click(sender As Object, e As EventArgs) Handles Pharm_returned_kinds.Click
        frmPharm_Sales_Chart.MdiParent = Me
        frmPharm_Sales_Chart.Show()

    End Sub

    Private Sub drug_kind_move_Click(sender As Object, e As EventArgs) Handles drug_kind_move.Click
        frmPharm_Kind_Move.MdiParent = Me
        frmPharm_Kind_Move.Show()

    End Sub

    Private Sub drug_kinds_move_Click(sender As Object, e As EventArgs) Handles drug_kinds_move.Click
        frmPharm_Kinds_Move.MdiParent = Me
        frmPharm_Kinds_Move.Show()

    End Sub

    Private Sub Pharm_Shortage_Click(sender As Object, e As EventArgs) Handles Pharm_Shortage.Click
        frmPharm_Shot_kind.MdiParent = Me
        frmPharm_Shot_kind.Show()

    End Sub

    Private Sub ezn_recive_Click(sender As Object, e As EventArgs) Handles Pharm_ezn_recive.Click
        frmPharm_Safe_recive.MdiParent = Me
        frmPharm_Safe_recive.Show()

    End Sub

    Private Sub ezn_pay_Click(sender As Object, e As EventArgs) Handles Pharm_ezn_pay.Click
        frmPharm_Safe_Payment.MdiParent = Me
        frmPharm_Safe_Payment.Show()

    End Sub

    Private Sub ezn_returned_Click(sender As Object, e As EventArgs) Handles Pharm_ezn_returned.Click
        frmPharm_ezn_back.MdiParent = Me
        frmPharm_ezn_back.Show()

    End Sub

    Private Sub safe_stock_Click(sender As Object, e As EventArgs) Handles Pharm_safe_stock.Click
        frmPharm_Safe_move.MdiParent = Me
        frmPharm_Safe_move.Show()

    End Sub

    Private Sub تحليلبيانيلحركةالخزنةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Pharm_safe_chart.Click
        frmPharm_safe_Chart.MdiParent = Me
        frmPharm_safe_Chart.Show()

    End Sub

    Private Sub Pharm_add_data_Click(sender As Object, e As EventArgs) Handles Pharm_add_data.Click
        frmAdd_Data.MdiParent = Me
        frmAdd_Data.Show()

    End Sub

    Private Sub Pharm_add_customer_Click(sender As Object, e As EventArgs) Handles Pharm_add_customer.Click
        frmCustomer.MdiParent = Me
        frmCustomer.Show()

    End Sub

    Private Sub Pharm_Customer_kashf_Click(sender As Object, e As EventArgs) Handles Pharm_Customer_kashf.Click
        frmPharm_Customer_account.MdiParent = Me
        frmPharm_Customer_account.Show()

    End Sub

    Private Sub Pharm_Customer_account_Click(sender As Object, e As EventArgs) Handles Pharm_Customer_account.Click
        frmPharm_Customer_Report.MdiParent = Me
        frmPharm_Customer_Report.Show()

    End Sub

    Private Sub Pharm_add_supplier_Click(sender As Object, e As EventArgs) Handles Pharm_add_supplier.Click
        frm_Add_Supplier.MdiParent = Me
        frm_Add_Supplier.Show()

    End Sub

    Private Sub Add_Resource_Click(sender As Object, e As EventArgs) Handles Add_Resource.Click
        frm_Add_Supplier.MdiParent = Me
        frm_Add_Supplier.Show()
    End Sub

    Private Sub Pharm_supplier_kashf_Click(sender As Object, e As EventArgs) Handles Pharm_supplier_kashf.Click
        frmPharm_Supplier_account.MdiParent = Me
        frmPharm_Supplier_account.Show()

    End Sub

    Private Sub Pharm_Supplier_account_Click(sender As Object, e As EventArgs) Handles Pharm_Supplier_account.Click
        frmPharm_Supplier_Report.MdiParent = Me
        frmPharm_Supplier_Report.Show()

    End Sub

    Private Sub التحليلالبيانيلحركةالمستهلكاتToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles التحليلالبيانيلحركةالمستهلكاتToolStripMenuItem.Click
        frmStock_Chart.MdiParent = Me
        frmStock_Chart.Show()

    End Sub

    Private Sub Add_room_Click(sender As Object, e As EventArgs) Handles Add_room.Click
        frmRooms.MdiParent = Me
        frmRooms.Show()
    End Sub
    Private Sub Add_Data_Click(sender As Object, e As EventArgs) Handles Add_Data.Click
        frmSafe_data.MdiParent = Me
        frmSafe_data.Show()
    End Sub
    Private Sub Recive_ez_Click(sender As Object, e As EventArgs) Handles Recive_ez.Click
        frmrecive.MdiParent = Me
        frmrecive.Show()
    End Sub
    Private Sub Payment_Click(sender As Object, e As EventArgs) Handles Payment.Click
        frmPayment.MdiParent = Me
        frmPayment.Show()
    End Sub

    Private Sub Returned_ezn_Click(sender As Object, e As EventArgs) Handles Returned_ezn.Click
        frmEsn_back.MdiParent = Me
        frmEsn_back.Show()

    End Sub

    Private Sub Chart_Click(sender As Object, e As EventArgs) Handles Chart.Click
        frmSafr_Chart.MdiParent = Me
        frmSafr_Chart.Show()

    End Sub

    Private Sub Employees_salary_Click(sender As Object, e As EventArgs) Handles Employees_salary.Click
        frmEmployee_Salary.MdiParent = Me
        frmEmployee_Salary.Show()

    End Sub

    Private Sub Employees_report_salary_Click(sender As Object, e As EventArgs) Handles Employees_report_salary.Click
        frmEmployees_Report_Salary.MdiParent = Me
        frmEmployees_Report_Salary.Show()

    End Sub

    Private Sub Employee_Files_Click(sender As Object, e As EventArgs) Handles Employees_Files.Click
        frmEmployees_Files.MdiParent = Me
        frmEmployees_Files.Show()

    End Sub

    Private Sub Employee_State_Click(sender As Object, e As EventArgs) 
        frmEmployee_State.MdiParent = Me
        frmEmployee_State.Show()

    End Sub

    Private Sub Employee_depit_Click(sender As Object, e As EventArgs) Handles Employees_Depit.Click
        frmEmployee_Depit.MdiParent = Me
        frmEmployee_Depit.Show()

    End Sub

    Private Sub Employee_report_salary_Click(sender As Object, e As EventArgs) Handles Employee_report_salary.Click
        frmEmployee_Report.MdiParent = Me
        frmEmployee_Report.Show()

    End Sub


    Private Sub Employee_go_went_Click(sender As Object, e As EventArgs) Handles Employees_go_went.Click
        frmEmployee_Go_Went.MdiParent = Me
        frmEmployee_Go_Went.Show()

    End Sub

    Private Sub Employee_Gift_Click(sender As Object, e As EventArgs) Handles Employees_Gift.Click
        frmEmployee_Gift.MdiParent = Me
        frmEmployee_Gift.Show()
    End Sub

    Private Sub Employees_zyada_Click(sender As Object, e As EventArgs) Handles Employees_zyada.Click
        frmEmployee_Zyadate.MdiParent = Me
        frmEmployee_Zyadate.Show()

    End Sub

    Private Sub Employees_holiday_Click(sender As Object, e As EventArgs) Handles Employees_holiday.Click
        frmEmployee_holiday.MdiParent = Me
        frmEmployee_holiday.Show()


    End Sub

    Private Sub Employees_ezn_Click(sender As Object, e As EventArgs) Handles Employees_ezn.Click
        frmEmployee_Ezn.MdiParent = Me
        frmEmployee_Ezn.Show()

    End Sub

    Private Sub Employees_Qard_Click(sender As Object, e As EventArgs) Handles Employees_Qard.Click
        frmEmployee_Qard.MdiParent = Me
        frmEmployee_Qard.Show()

    End Sub

    Private Sub Employees_Badal_Click(sender As Object, e As EventArgs) Handles Employees_Badal.Click
        frmEmployee_Badal.MdiParent = Me
        frmEmployee_Badal.Show()

    End Sub

    Private Sub Employees_Insurance_Click(sender As Object, e As EventArgs) Handles Employees_Insurance.Click
        frmEmployee_insurance.MdiParent = Me
        frmEmployee_insurance.Show()

    End Sub

    Private Sub Employees_Follow_Click(sender As Object, e As EventArgs) Handles Employees_Follow.Click
        frmEmployee_follow.MdiParent = Me
        frmEmployee_follow.Show()

    End Sub

    Private Sub Employees_Extra_Work_Click(sender As Object, e As EventArgs) Handles Employees_Extra_Work.Click
        frmEmployee_Extra_work.MdiParent = Me
        frmEmployee_Extra_work.Show()

    End Sub

    Private Sub Employees_State_Change_Click(sender As Object, e As EventArgs) Handles Employees_State_Change.Click
        frmEmployee_State.MdiParent = Me
        frmEmployee_State.Show()

    End Sub

    Private Sub Reservation_stay_Click(sender As Object, e As EventArgs) Handles Reservation_stay.Click
        frmStaying_Reserve.MdiParent = Me
        frmStaying_Reserve.Show()

    End Sub

    Private Sub Barcode_Click(sender As Object, e As EventArgs) Handles Barcode.Click
        frmBarcode_setting.MdiParent = Me
        frmBarcode_setting.Show()

    End Sub

    Private Sub Pharmacy_Click(sender As Object, e As EventArgs) Handles Pharmacy.Click

    End Sub

    Private Sub Daily_Click(sender As Object, e As EventArgs) Handles Daily.Click
        frmDaily_Move.MdiParent = Me
        frmDaily_Move.Show()

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Close()

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class
