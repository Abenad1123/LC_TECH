Imports System.Data.SqlClient

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

    Private Function GetTableName(category As String) As String
        If category = "Peripherals" Then
            Return "LC_Peripherals"
        ElseIf category = "Components" Then
            Return "LC_Components"
        ElseIf category = "Accessories" Then
            Return "LC_Accesories"
        End If
        Return ""
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim name As String = TextBox1.Text.Trim()
        Dim sku As String = TextBox2.Text.Trim()
        Dim brand As String = TextBox3.Text.Trim()
        Dim description As String = TextBox4.Text.Trim()
        Dim price As Decimal = NumericUpDown1.Value
        Dim category As String = ComboBox4.SelectedItem?.ToString()

        If name = "" Or sku = "" Or brand = "" Or category = "" Then
            MessageBox.Show("Please fill all required fields.")
            Exit Sub
        End If

        Dim tableName As String = GetTableName(category)

        Using conn As New SqlConnection(Basic.conString)
            conn.Open()

            Dim checkQuery As String = "SELECT COUNT(*) FROM " & tableName & " WHERE SKU = @sku"
            Using checkCmd As New SqlCommand(checkQuery, conn)
                checkCmd.Parameters.AddWithValue("@sku", sku)

                Dim count As Integer = CInt(checkCmd.ExecuteScalar())

                If count > 0 Then
                    MessageBox.Show("SKU already exists! Please use a unique SKU.")
                    Exit Sub
                End If
            End Using

            Dim insertQuery As String = "
            INSERT INTO " & tableName & "
            (ProductName, SKU, Brand, Price, Description)
            VALUES
            (@name, @sku, @brand, @price, @desc)
            "

            Using cmd As New SqlCommand(insertQuery, conn)

                cmd.Parameters.AddWithValue("@name", name)
                cmd.Parameters.AddWithValue("@sku", sku)
                cmd.Parameters.AddWithValue("@brand", brand)
                cmd.Parameters.AddWithValue("@price", price)
                cmd.Parameters.AddWithValue("@desc", description)

                cmd.ExecuteNonQuery()

            End Using

        End Using

        MessageBox.Show("Product added successfully!")

        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        NumericUpDown1.Value = 0
        LoadAdminProducts()
    End Sub

    Public Sub LoadAdminProducts()

        Dim category As String = ComboBox1.SelectedItem?.ToString()
        If String.IsNullOrEmpty(category) Then Exit Sub

        Dim tableName As String = GetTableName(category)

        Dim query As String = "
    SELECT ProductID, ProductName, Brand, Price, SKU, Description
    FROM " & tableName

        Using conn As New SqlConnection(Basic.conString)
            Dim da As New SqlDataAdapter(query, conn)
            Dim dt As New DataTable()

            da.Fill(dt)
            DataGridView1.DataSource = dt
        End Using

        If DataGridView1.Columns.Contains("Price") Then
            DataGridView1.Columns("Price").DefaultCellStyle.Format = "₱#,0.00"
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        LoadAdminProducts()
    End Sub

    Private Sub Admin_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ComboBox1.Items.AddRange({"Peripherals", "Components", "Accessories"})
        ComboBox4.Items.AddRange({"Peripherals", "Components", "Accessories"})

        ComboBox1.SelectedIndex = 0
        ComboBox4.SelectedIndex = 0

        LoadAdminProducts()

        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
        DataGridView1.ReadOnly = True
        DataGridView1.AllowUserToAddRows = False

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If DataGridView1.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a product to delete.")
            Exit Sub
        End If

        Dim productID As Integer = CInt(DataGridView1.CurrentRow.Cells("ProductID").Value)
        Dim productName As String = DataGridView1.CurrentRow.Cells("ProductName").Value.ToString()

        Dim confirm = MessageBox.Show("Delete '" & productName & "'?", "Confirm", MessageBoxButtons.YesNo)

        If confirm = DialogResult.No Then Exit Sub

        Dim category As String = ComboBox1.SelectedItem?.ToString()
        Dim tableName As String = GetTableName(category)

        If tableName = "" Then
            MessageBox.Show("Invalid category.")
            Exit Sub
        End If

        Using conn As New SqlConnection(Basic.conString)
            conn.Open()

            Dim query As String = "DELETE FROM " & tableName & " WHERE ProductID = @id"

            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", productID)
                cmd.ExecuteNonQuery()
            End Using

        End Using

        MessageBox.Show("Product deleted successfully!")

        LoadAdminProducts()

    End Sub
End Class