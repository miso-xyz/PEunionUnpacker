Public Class AppInfo
    Dim Utils As New Utils()
    Dim filepath
    Private Sub AppInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        filepath = ToolsPrompt.filepath
        Text = "Info about """ & IO.Path.GetFileName(filepath) & """"
        getinfo()
    End Sub

    Function getinfo()
        Dim patchedApp As dnlib.DotNet.ModuleDef = dnlib.DotNet.ModuleDefMD.Load(filepath) 'sets app in memory
        Dim mainType ', appName, args, packedSize, isCompressed, isEncrypted, hasArgs, pathType, hasExecParams, UsesRunas, searchSandboxie, searchWireshark, searchProcessMonitor, searchEmulator, unpackSize
        Dim hasmsg = False
        For x = 0 To patchedApp.Types.Count - 1
            Try
                If patchedApp.Types(x).FindMethod("Main").Body.Instructions.Count Then
                    mainType = x ' gets main type by finding if one of the types in the application has "Main", which is the entrypoint, however since all of the code of the generated application is present in only 1 type, this is therefore the main type.
                    Exit For
                End If
            Catch ex As Exception
            End Try
        Next
        If mainType = Nothing Then 'if it cannot find "Main" at all, this shows that it is not a valid PEunion-Packed Executable, this is an error that very unlikely happens and cannot be used to find if the application is actually packed by PEunion, it can however draw the line between a .NET app and a Native app
            MessageBox.Show("Failed to find main type, are you sure this is a valid PEunion-Packed executable?", "PEunion Unpacker", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        For x = 0 To patchedApp.Types(mainType).FindMethod(".cctor").Body.Instructions.Count - 1 ' goes thru all instructions of ".cctor" present in the main type since this is where the properties are stored
            Dim calc, opcodeType
            Select Case patchedApp.Types(mainType).FindMethod(".cctor").Body.Instructions(x).OpCode.ToString()
                Case "ldstr"
                    opcodeType = "string"
                    Try 'tries to decrypt any strings it finds
                        calc = Utils.DecryptString(patchedApp.Types(mainType).FindMethod(".cctor").Body.Instructions(x).Operand.ToString())
                    Catch ex As Exception
                        calc = "<Failed To Decrypt>"
                    End Try
                Case Else
                    calc = patchedApp.Types(mainType).FindMethod(".cctor").Body.Instructions(x).OpCode.ToString().Replace("ldc.i4.", Nothing) 'if the current instruction's opcode is (for example) ldc.i4.1, it will suppress the "ldc.i4." part if it finds it, making the instruction Boolean-compatible
            End Select
            Select Case x
                Case 5
                    Dim packedType = patchedApp.Types(mainType).FindMethod(".cctor").Body.Instructions(x).Operand.ToString().Replace("System.Void ", Nothing).Replace("::.ctor()", Nothing)
                    Dim typeList As New List(Of String)
                    For x2 As Integer = 0 To patchedApp.Types.Count - 1
                        typeList.Add(patchedApp.Types(x2).Name)
                    Next
                    If typeList.Contains(packedType) Then
                        Select Case patchedApp.Types(typeList.IndexOf(packedType)).Fields.Count - 1
                            Case 3
                                packedType = patchedApp.Types(mainType).FindMethod(".cctor").Body.Instructions(25).Operand.ToString().Replace("System.Void ", Nothing).Replace("::.ctor()", Nothing)
                                If patchedApp.Types(typeList.IndexOf(packedType)).Fields.Count - 1 = 12 Then
                                    TextBox6.Text = "File present in-app (With Message box)"
                                ElseIf patchedApp.Types(typeList.IndexOf(packedType)).Fields.Count - 1 = 10 Then
                                    TextBox6.Text = "Downloaded file (With Message box)"
                                Else
                                    TextBox6.Text = "??? (has Message box)"
                                End If
                                hasmsg = True
                            Case 12
                                TextBox6.Text = "File present in-app"
                            Case 10
                                TextBox6.Text = "Downloaded file"
                            Case Else
                                TextBox6.Text = "???"
                        End Select
                    End If
                Case If(hasmsg, 28, 8) 'Packed App Name
                    TextBox1.Text = calc
                Case If(hasmsg, 32, 12) 'is Compressed
                    CheckedListBox2.SetItemChecked(1, CBool(calc))
                Case If(hasmsg, 35, 15) 'is Encrypted
                    CheckedListBox2.SetItemChecked(0, CBool(calc))
                Case If(hasmsg, 38, 18) 'packed app has parameters
                    CheckedListBox3.SetItemChecked(0, CBool(calc))
                Case If(hasmsg, 41, 21) 'pathtype
                    TextBox3.Text = Utils.GetPathFromType(calc) & " (" & calc & ")"
                Case If(hasmsg, 44, 24) 'has execution parameters
                    CheckedListBox3.SetItemChecked(1, CBool(calc))
                Case If(hasmsg, 47, 27) 'uses runas
                    If CBool(calc) Then
                        Label7.Text = "Does use Run As on launch"
                    Else
                        Label7.Text = "Doesn't use Run As on launch"
                    End If
                Case If(hasmsg, 50, 30) 'args
                    TextBox4.Text = calc
                Case If(hasmsg, 54, 34) 'check for sandboxie
                    CheckedListBox1.SetItemChecked(0, CBool(calc))
                Case If(hasmsg, 57, 37) 'check for wireshark
                    CheckedListBox1.SetItemChecked(2, CBool(calc))
                Case If(hasmsg, 60, 40) 'check for process monitor
                    CheckedListBox1.SetItemChecked(1, CBool(calc))
                Case If(hasmsg, 63, 43) 'check for emulator
                    CheckedListBox1.SetItemChecked(3, CBool(calc))
                Case If(hasmsg, 66, 46) 'packed size (bytes)
                    TextBox2.Text = patchedApp.Types(mainType).FindMethod(".cctor").Body.Instructions(x).Operand.ToString() & "bytes"
            End Select
        Next
    End Function
End Class