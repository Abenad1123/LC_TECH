Public Class Product_Control_Menu
    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) 
        If Not User.User_Login Then
            OpenForm(Of Login_Menu)(Me)
        Else
            OpenForm(Of User_Page_Menu)(Me)
        End If
    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        OpenForm(Of Main_Menu)(Me)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) 
        OpenForm(Of User_Cart_Menu)(Me)
    End Sub

    '------------ END OF INITIALIZATION ------------
End Class