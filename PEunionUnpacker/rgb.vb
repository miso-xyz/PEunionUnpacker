Imports Transitions
Public Class rgb
    Public isActive
    Dim a_ As New Timer()
    Dim c_ As New PictureBox()
    Dim targetObj
    Dim PropertyAffected
    Public currentColor As Color
    Dim b_
    Dim d_
    Dim delay
    Sub New(ByVal targetObj_ As Object, ByVal propertyAffected_ As String, ByVal delay_ As Integer, ByVal outputColor_ As Boolean)
        a_.Interval = 100
        If outputColor_ Then
            AddHandler a_.Tick, AddressOf Timer2_Tick
        Else
            AddHandler a_.Tick, AddressOf Timer1_Tick
        End If
        targetObj = targetObj_
        PropertyAffected = propertyAffected_
        delay = delay_
    End Sub

    Sub RGBStr()
        isActive = True
        a_.Start()
    End Sub

    Sub RGBStop()
        isActive = False
        a_.Stop()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim e_ As Color
        Select Case b_
            Case 0
                e_ = Color.Red
                b_ = 1
            Case 1
                e_ = Color.Magenta
                b_ = 2
            Case 2
                e_ = Color.Blue
                b_ = 3
            Case 3
                e_ = Color.Cyan
                b_ = 4
            Case 4
                e_ = Color.Green
                b_ = 5
            Case 5
                e_ = Color.Yellow
                b_ = 0
        End Select
        Transition.run(c_, PropertyAffected, e_, New TransitionType_Linear(delay))
        currentColor = e_
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case b_
            Case 0
                Transition.run(targetObj, PropertyAffected, Color.FromArgb(255, 0, 0), New TransitionType_Linear(delay))
                b_ = 1
            Case 1
                Transition.run(targetObj, PropertyAffected, Color.FromArgb(255, 0, 255), New TransitionType_Linear(delay))
                b_ = 2
            Case 2
                Transition.run(targetObj, PropertyAffected, Color.FromArgb(255, 0, 255), New TransitionType_Linear(delay))
                b_ = 3
            Case 3
                Transition.run(targetObj, PropertyAffected, Color.FromArgb(0, 255, 255), New TransitionType_Linear(delay))
                b_ = 4
            Case 4
                Transition.run(targetObj, PropertyAffected, Color.FromArgb(0, 255, 0), New TransitionType_Linear(delay))
                b_ = 5
            Case 5
                Transition.run(targetObj, PropertyAffected, Color.FromArgb(255, 255, 0), New TransitionType_Linear(delay))
                b_ = 0
        End Select
    End Sub
End Class
