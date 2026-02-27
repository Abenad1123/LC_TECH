Public Class Login_Menu
    Private Sub Form_Load_Standard(sender As Object, e As EventArgs) Handles MyBase.Load
        SetPlaceholder()
        Product_DropBox.SelectedIndex = 0
        Footer_Main.BackColor = ColorTranslator.FromHtml("#eae9ec")
        Panel3.BackColor = ColorTranslator.FromHtml("#565454")
    End Sub

    Private Sub Form_Shown_Standard(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.ActiveControl = Nothing
    End Sub

    Private Sub SearchBar_Text_Enter(sender As Object, e As EventArgs) Handles SearchBar_Text.Enter
        If SearchBar_Text.Text = " Search for products" Then
            SearchBar_Text.Text = ""
            SearchBar_Text.ForeColor = Color.Black
        End If
    End Sub

    Private Sub SearchBar_Text_Leave(sender As Object, e As EventArgs) Handles SearchBar_Text.Leave
        If SearchBar_Text.Text.Trim() = "" Then SetPlaceholder()
    End Sub

    Private Sub SetPlaceholder()
        SearchBar_Text.Text = " Search for products"
        SearchBar_Text.ForeColor = ColorTranslator.FromHtml("#adadad")
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        OpenForm(Of Login_Menu)(Me)
    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        OpenForm(Of Main_Menu)(Me)
    End Sub

    '------------ END OF INITIALIZATION ------------

    Private Sub Login_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.BackColor = ColorTranslator.FromHtml("#2626a6")
    End Sub

    Private Sub Create_Acc_HoverE(sender As Object, e As EventArgs) Handles Label5.MouseEnter
        Label5.ForeColor = ColorTranslator.FromHtml("#2626a6")
    End Sub

    Private Sub Create_Acc_HoverL(sender As Object, e As EventArgs) Handles Label5.MouseLeave
        Label5.ForeColor = ColorTranslator.FromHtml("#000000")
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        OpenForm(Of Create_Acc_Menu)(Me)
    End Sub
End Class

