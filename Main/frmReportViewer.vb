Public Class frmReportViewer
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub New(x As String)

        ' This call is required by the designer.
        InitializeComponent()
        Me.Text = x
        Me.WindowState = FormWindowState.Normal
        Me.MdiParent = Main
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.FormBorderStyle = FormBorderStyle.FixedSingle

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class