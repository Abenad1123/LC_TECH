Imports System.Data.SqlClient

Public Class Reset_Password_Menu
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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Input_Email As String = TextBox1.Text
        Dim Input_Old_Pass As String = TextBox2.Text
        Dim Input_New_Pass As String = TextBox3.Text

        If ResetPassword(Input_Email, Input_Old_Pass, Input_New_Pass) Then
            MsgBox("Password Changed Successfully!")
        Else
            MsgBox("Invalid Email or Old Password")
        End If

    End Sub

    Function ResetPassword(email As String, oldPassword As String, newPassword As String) As Boolean

        Using con As New SqlConnection(Basic.UserConnectionString)

            Dim query As String = "UPDATE Users SET Password = @NewPassword WHERE Email = @Email AND Password = @OldPassword"

            Using cmd As New SqlCommand(query, con)

                cmd.Parameters.AddWithValue("@Email", email)
                cmd.Parameters.AddWithValue("@OldPassword", oldPassword)
                cmd.Parameters.AddWithValue("@NewPassword", newPassword)

                con.Open()

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    Return True
                Else
                    Return False
                End If

            End Using
        End Using

    End Function
End Class