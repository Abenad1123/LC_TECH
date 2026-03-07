Imports System.Data.SqlClient

Public Class Catalog_Select_Menu
    Private Sub Form_Load_Standard(sender As Object, e As EventArgs) Handles MyBase.Load
        Product_DropBox.SelectedItem = User.Selected_Catalog
        SetPlaceholder()
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

    Private Sub Product_DropBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Product_DropBox.SelectionChangeCommitted
        User.Selected_Catalog = Product_DropBox.SelectedItem.ToString()
        OpenForm(Of Catalog_Select_Menu)(Me)
        LoadProducts(User.Selected_Catalog)
    End Sub

    '------------ END OF INITIALIZATION ------------

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProducts(User.Selected_Catalog)
    End Sub

    Public Sub LoadProducts(category As String)

        FlowLayoutPanel1.Controls.Clear()

        Dim tableName As String = ""

        If category = "Peripherals" Then
            tableName = "LC_Peripherals"
        ElseIf category = "Components" Then
            tableName = "LC_Components"
        ElseIf category = "Accessories" Then
            tableName = "LC_Accesorries"
        End If

        Dim query As String = "SELECT ProductID, ProductName, Price FROM " & tableName

        Using conn As New SqlConnection(Basic.ProductsInfoConnectionString)
            Dim cmd As New SqlCommand(query, conn)
            conn.Open()

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()

                Dim id As Integer = Convert.ToInt32(reader("ProductID"))
                Dim name As String = reader("ProductName").ToString()
                Dim price As Decimal = Convert.ToDecimal(reader("Price"))

                CreateItemCard(id, name, price)

            End While

        End Using

    End Sub

    Public Sub CreateItemCard(productID As Integer, productName As String, price As Decimal)

        Dim card As New Panel
        card.Width = 180
        card.Height = 230
        card.BorderStyle = BorderStyle.FixedSingle
        card.Margin = New Padding(10)

        ' Picture
        Dim pic As New PictureBox
        pic.Image = My.Resources.navigation_logo
        pic.Width = 160
        pic.Height = 100
        pic.Top = 10
        pic.Left = 10
        pic.SizeMode = PictureBoxSizeMode.Zoom

        ' Name
        Dim lblName As New Label
        lblName.Text = productName
        lblName.Top = 120
        lblName.Left = 10
        lblName.Width = 160
        lblName.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        ' Price
        Dim lblPrice As New Label
        lblPrice.Text = "₱" & price.ToString("N0")
        lblPrice.Top = 145
        lblPrice.Left = 10
        lblPrice.Width = 160
        lblPrice.ForeColor = Color.Green

        ' Add to Cart Button
        Dim btnAdd As New Button
        btnAdd.Text = "Add to Cart"
        btnAdd.Width = 160
        btnAdd.Height = 30
        btnAdd.Top = 175
        btnAdd.Left = 10
        btnAdd.Tag = productID

        AddHandler btnAdd.Click, AddressOf AddToCart_Click

        card.Controls.Add(pic)
        card.Controls.Add(lblName)
        card.Controls.Add(lblPrice)
        card.Controls.Add(btnAdd)

        FlowLayoutPanel1.Controls.Add(card)

    End Sub

    Private Sub AddToCart_Click(sender As Object, e As EventArgs)

        Dim btn As Button = CType(sender, Button)
        Dim productID As Integer = CInt(btn.Tag)

        MessageBox.Show("Product added to cart! ID: " & productID)

    End Sub

End Class