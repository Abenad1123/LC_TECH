Public Class User_Page_Menu
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
        If Not User.User_Login Then
            OpenForm(Of Login_Menu)(Me)
        Else
            OpenForm(Of User_Page_Menu)(Me)
        End If
    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        OpenForm(Of Main_Menu)(Me)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        OpenForm(Of User_Cart_Menu)(Me)
    End Sub

    Private Sub Product_DropBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Product_DropBox.SelectionChangeCommitted
        User.Selected_Catalog = Product_DropBox.SelectedItem.ToString()
        OpenForm(Of Catalog_Select_Menu)(Me)
    End Sub

    '------------ END OF INITIALIZATION ------------
    Private Sub SetUserData(sender As Object, e As EventArgs) Handles MyBase.Load
        Label6.Text = User.FullName
        Label7.Text = User.UserName
        Label8.Text = User.Email

        Label9.Text = New String("*"c, User.Password.Length)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        User.User_Login = False
        User.FullName = ""
        User.UserName = ""
        User.Email = ""
        User.Password = ""

        MsgBox("User logout successfully!")
    End Sub

    Dim showPassword As Boolean = False

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        showPassword = Not showPassword

        If showPassword Then
            Label9.Text = User.Password
            Button1.Text = "Hide"
        Else
            Label9.Text = New String("*"c, User.Password.Length)
            Button1.Text = "Show"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenForm(Of Reset_Password_Menu)(Me)
    End Sub

End Class