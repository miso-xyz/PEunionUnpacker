Imports System.Globalization
Imports System.IO
Imports System.Security.Cryptography
Public Class RBU
    Dim Utils As New Utils()
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim inf
        If CheckBox1.Checked Then
            inf = AppInfo.getinfo(False).Split(",")
        Else
            inf = (CheckBox2.Checked.ToString & "," & CheckBox3.Checked.ToString).Split(",")
        End If
        Dim file = IIf(inf(0) = True, Utils.Decompress(If(inf(1) = "True", Utils.DecryptBytes(ReformatData(TextBox1.Text)), ReformatData(TextBox1.Text))), If(inf(1) = "True", Utils.DecryptBytes(ReformatData(TextBox1.Text)), ReformatData(TextBox1.Text)))
        Dim sfd As New SaveFileDialog With {.Filter = "Executable Application|*.exe|DLL Files|*.dll|Any Files|*.*"}
        If sfd.ShowDialog = vbOK Then
            Try
                IO.File.WriteAllBytes(sfd.FileName, file)
            Catch ex As Exception
                MessageBox.Show("Failed to save file!", "RBU", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Function ReformatData(ByVal data As String) As Byte()
        Dim pck As New List(Of Byte)
        Dim a_() As String = data.ToLower.Replace(" ", Nothing).Replace("byte.maxvalue", "255").Replace("byte.minvalue", "0").Replace(vbTab, Nothing).Split(",")
        For x = 0 To a_.Count - 1
            pck.AddRange(ConvertHexStringToByteArray(Hex(a_(x))))
        Next
        Return pck.ToArray
    End Function
    Private Function ConvertHexStringToByteArray(ByVal hexString As String) As Byte()
        If hexString.Length Mod 2 <> 0 Then
            hexString = "0" & hexString
            'Throw New ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString))
        End If

        Dim data As Byte() = New Byte(hexString.Length / 2 - 1) {}

        For index As Integer = 0 To data.Length - 1
            Dim byteValue As String = hexString.Substring(index * 2, 2)
            data(index) = Byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture)
        Next
        Return data
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MessageBox.Show("This is the RBU (Raw Bytes Unpacker), you will need to enter the encrypted bytes in a decimal form, the application will then try to decrypt the given bytes. (a showcase of it is available on the repository)" & vbCrLf & vbCrLf & "If the 'Auto' checkbox isn't checkable, make sure you have an input file set" & vbCrLf & vbCrLf & "(supports both compressed & encrypted files)", "PEunion Unpacker - RBU", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            CheckBox2.Enabled = False
            CheckBox3.Enabled = False
        Else
            CheckBox2.Enabled = True
            CheckBox3.Enabled = True
        End If
    End Sub

    Private Sub ext_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ToolsPrompt.checkFilepath()
    End Sub
End Class