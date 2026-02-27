Module FormHelper
    Public Sub OpenForm(Of T As {Form, New})(currentForm As Form)
        Dim frm As New T()
        frm.Show()
        currentForm.Close()
    End Sub

End Module