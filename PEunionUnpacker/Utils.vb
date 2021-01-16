Imports System.IO
Imports Microsoft.Win32
Imports System.Globalization
Imports System.IO.Compression
Imports System.Security.Cryptography

Public Class Utils
    Public Function GetPathFromType(ByVal type As Integer) As String
        Select Case type
            Case 1
                Return Path.GetTempPath
            Case 2 ' Downloads folder 
                Return Registry.GetValue("HKEY_C", "{374DE290-123F-4565-9164-39C4925E467B}", Nothing)
            Case 3
                Return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
            Case 4
                Return Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            Case 5
                Return AppDomain.CurrentDomain.BaseDirectory
            Case Else
                Return "<Unknown>"
        End Select
    End Function
    Public Function DecryptString(ByVal data As String) As String
        Return New String((From c In data.Substring(1) Select ChrW(AscW(c) Xor CByte(AscW(data(0))))).ToArray())
    End Function

    Public Function DecryptBytes(ByVal data As Byte()) As Byte()
        Try
            Dim array As Byte() = New Byte(15) {}
            Buffer.BlockCopy(data, 0, array, 0, 16)
            Dim rijndael As Rijndael = rijndael.Create()
            Dim symmetricAlgorithm As SymmetricAlgorithm = rijndael
            Dim symmetricAlgorithm2 As SymmetricAlgorithm = rijndael
            Dim array2 As Byte() = array
            Dim iv As Byte() = array2
            symmetricAlgorithm2.Key = array2
            symmetricAlgorithm.IV = iv
            Dim memoryStream As MemoryStream = New MemoryStream()
            Dim cryptoStream As CryptoStream = New CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write)
            cryptoStream.Write(data, 16, data.Length - 16)
            cryptoStream.Close()
            Return memoryStream.ToArray()
        Catch ex As Exception
            MessageBox.Show("Failed to decrypt given bytes!", "RBU", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function Decompress(ByVal data As Byte()) As Byte()
        Try
            Dim memoryStream As New MemoryStream()
            Dim num As Integer = BitConverter.ToInt32(data, 0)
            memoryStream.Write(data, 4, data.Length - 4)
            Dim array(num - 1) As Byte
            memoryStream.Position = 0L
            Call (New IO.Compression.GZipStream(memoryStream, IO.Compression.CompressionMode.Decompress)).Read(array, 0, array.Length)
            Return array
        Catch ex As Exception
            MessageBox.Show("Failed to decompress given bytes!", "RBU", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
End Class
