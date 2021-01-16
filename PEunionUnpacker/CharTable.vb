Public Class CharTable
    Dim selected_id As Integer = -1
    Dim current_selected, previous_selected As Button
    Private Sub CharactableTable_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For x = 0 To 256
            Dim btn As New Button() With {.BackColor = Color.FromArgb(32, 32, 32), .ForeColor = Color.FromArgb(236, 236, 236), .FlatStyle = FlatStyle.Flat}
            btn.FlatAppearance.BorderColor = Color.FromArgb(20, 20, 20)
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(24, 24, 24)
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(28, 28, 28)
            btn.Text = ChrW(x)
            If x = 0 Then
                Clipboard.SetText(ChrW(x))
            End If
            btn.TabIndex = x
            TableLayoutPanel1.Controls.Add(btn)
            AddHandler btn.Click, AddressOf btn_Click
        Next
        StringDecryptor.Panel4.Hide()
        StringDecryptor.Panel1.Enabled = True
    End Sub

    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        previous_selected = current_selected
        current_selected = sender
        If selected_id <> -1 Then
            If current_selected.TabIndex <> previous_selected.TabIndex Then
                previous_selected.BackColor = Color.FromArgb(32, 32, 32)
            End If
        End If
        selected_id = current_selected.TabIndex
        If sender.BackColor = Color.DodgerBlue Then
            StringDecryptor.TextBox1.Text += sender.Text
            sender.BackColor = Color.FromArgb(32, 32, 32)
        Else
            If sender.TabIndex = 0 Then ' Character on the First button breaks the string, so its hardcoded
                StatusBar1.Text = """"" - Hex: " & Hex(AscW(sender.Text)) & " - Decimal: " & AscW(sender.Text)
            Else
                StatusBar1.Text = """" & sender.Text & """ - Hex: " & Hex(AscW(sender.Text)) & " - Decimal: " & AscW(sender.Text)
            End If
            sender.BackColor = Color.DodgerBlue
            current_selected = sender
        End If
    End Sub
End Class