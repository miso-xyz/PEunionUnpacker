Imports dnlib
Public Class ToolsPrompt
    Dim Utils As New Utils()
    Public filepath As String
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        AppInfo.Show()
        'getInfo(True)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        RBU.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        StringDecryptor.Show()
    End Sub
    Sub setInputFile()
        Dim opf As New OpenFileDialog With {.Filter = "Executable Application|*.exe|DLL Files|*.dll|Any Files|*.*"}
        opf.ShowDialog()
        filepath = opf.FileName
        checkFilepath()
    End Sub

    Sub checkFilepath()
        If filepath = Nothing Then            Button2.Text = "An input file is required!"
            Button2.Enabled = False
            RBU.CheckBox1.Enabled = False
            RBU.CheckBox1.Checked = False
            RBU.CheckBox2.Enabled = True
            RBU.CheckBox3.Enabled = True
            MenuItem9.Enabled = False
        Else
            Button2.Text = "Get App Info"
            Button2.Enabled = True
            RBU.CheckBox1.Enabled = True
            RBU.CheckBox1.Checked = True
            RBU.CheckBox2.Enabled = False
            RBU.CheckBox3.Enabled = False
            MenuItem9.Enabled = True
        End If
    End Sub

    Private Sub MenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem8.Click
        setInputFile()
    End Sub

    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem9.Click
        filepath = Nothing
        checkFilepath()
    End Sub

    Private Sub ToolsPrompt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim gp1_rgb As New rgb(Label1, "ForeColor", 150, False)
        Dim gp2_rgb As New rgb(Label2, "ForeColor", 150, False)
        gp1_rgb.RGBStr()
        gp2_rgb.RGBStr()
    End Sub

    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        Process.Start("https://github.com/miso-xyz/PEunionUnpacker")
    End Sub
End Class