Imports System.IO
Imports System.Security.Cryptography
Imports Microsoft.Win32
Public Class StringDecryptor
    Dim Utils As New Utils()

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        TextBox3.Text = Hex(NumericUpDown1.Value)
        TextBox5.Text = ChrW(NumericUpDown1.Value)
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Try
            TextBox2.Text = Utils.DecryptString(TextBox1.Text)
            TextBox4.Text = BitConverter.ToString(System.Text.Encoding.UTF32.GetBytes(Utils.DecryptString(TextBox1.Text))).Replace("-", " ")
        Catch ex As Exception
            If TextBox2.Text = Nothing Then
                TextBox2.Text = Nothing
                TextBox4.Text = Nothing
            Else
                TextBox2.Text = "Failed to decrypt!"
                TextBox4.Text = Nothing
            End If
            
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox1.Text += TextBox5.Text
    End Sub

    Private Sub NumericUpDown1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles NumericUpDown1.KeyUp
        TextBox3.Text = Hex(NumericUpDown1.Value)
        TextBox5.Text = ChrW(NumericUpDown1.Value)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Panel4.Show()
        Panel1.Enabled = False
        Panel4.Update()
        CharTable.Show()
    End Sub

    Private Sub StringDecryptor_FormClosed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosed
        If CharTable.Visible Then
            CharTable.Close()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Text = Nothing
        TextBox2.Text = Nothing
        TextBox4.Text = Nothing
    End Sub
End Class
