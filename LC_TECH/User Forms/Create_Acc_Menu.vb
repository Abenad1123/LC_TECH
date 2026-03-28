Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class Create_Acc_Menu
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

    Private Sub Login_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.BackColor = ColorTranslator.FromHtml("#2626a6")
    End Sub

    Private Sub Create_Acc_HoverE(sender As Object, e As EventArgs) Handles Label5.MouseEnter
        Label5.ForeColor = ColorTranslator.FromHtml("#2626a6")
    End Sub

    Private Sub Create_Acc_HoverL(sender As Object, e As EventArgs) Handles Label5.MouseLeave
        Label5.ForeColor = ColorTranslator.FromHtml("#000000")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Full_Name As String = TextBox1.Text

        If Regex.IsMatch(Full_Name, "\d") Or Regex.IsMatch(Full_Name, "[^a-zA-Z0-9]") Then
            MessageBox.Show("Name must not contain numbers or symbols.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim Username As String = TextBox2.Text
        Dim User_Email As String = TextBox3.Text
        Dim User_Password As String = TextBox4.Text

        InsertUser(Full_Name, Username, User_Email, User_Password)
        MsgBox("Account created successfully!")
        OpenForm(Of Login_Menu)(Me)
    End Sub

    Sub InsertUser(fullname As String, username As String, email As String, password As String)

        Using con As New SqlConnection(Basic.conString)

            Dim query As String = "INSERT INTO Users (Fullname, Username, Email, Password) VALUES (@fullname, @username, @email, @pass)"

            Using cmd As New SqlCommand(query, con)

                cmd.Parameters.AddWithValue("@fullname", fullname)
                cmd.Parameters.AddWithValue("@username", username)
                cmd.Parameters.AddWithValue("@email", email)
                cmd.Parameters.AddWithValue("@pass", password)

                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

            End Using
        End Using

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        OpenForm(Of Login_Menu)(Me)
    End Sub
End Class