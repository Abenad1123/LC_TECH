Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Menu

Public Class Catalog_Select_Menu
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
            tableName = "LC_Accesories"
        End If

        Dim query As String = "SELECT ProductID, ProductName, Price FROM " & tableName

        Using conn As New SqlConnection(Basic.conString)
            Dim cmd As New SqlCommand(query, conn)
            conn.Open()

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()

                Dim id As Integer = Convert.ToInt32(reader("ProductID"))
                Dim name As String = reader("ProductName").ToString()
                Dim price As Decimal = Convert.ToDecimal(reader("Price"))

                CreateItemCard(id, name, price, name)

            End While

        End Using

    End Sub

    Public Sub CreateItemCard(productID As Integer, productName As String, price As Decimal, imageName As String)

        Dim card As New Panel
        card.Width = 180
        card.Height = 230
        card.BorderStyle = BorderStyle.FixedSingle
        card.Margin = New Padding(10)

        ' Picture
        Dim pic As New PictureBox
        pic.Width = 160
        pic.Height = 100
        pic.Top = 10
        pic.Left = 10
        pic.SizeMode = PictureBoxSizeMode.Zoom

        ' Load image from My.Resources using string name
        Try
            Dim img = My.Resources.ResourceManager.GetObject(imageName)
            If img IsNot Nothing Then
                pic.Image = CType(img, Image)
            Else
                pic.Image = My.Resources.navigation_logo ' fallback
            End If
        Catch ex As Exception
            pic.Image = My.Resources.navigation_logo ' fallback
        End Try

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

        ' Button
        Dim btnAdd As New Button
        btnAdd.Text = "Add to Cart"
        btnAdd.Width = 160
        btnAdd.Height = 30
        btnAdd.Top = 175
        btnAdd.Left = 10
        Dim item As New CartItem With {
            .productID = productID,
            .productName = productName,
            .price = price,
            .Quantity = 1
        }

        btnAdd.Tag = item

        AddHandler btnAdd.Click, AddressOf AddToCart_Click

        card.Controls.Add(pic)
        card.Controls.Add(lblName)
        card.Controls.Add(lblPrice)
        card.Controls.Add(btnAdd)

        FlowLayoutPanel1.Controls.Add(card)

    End Sub

    Private Sub AddToCart_Click(sender As Object, e As EventArgs)

        Dim btn As Button = CType(sender, Button)
        Dim newItem As CartItem = CType(btn.Tag, CartItem)

        Dim existingItem = User.Cart.FirstOrDefault(Function(x) x.ProductID = newItem.ProductID)

        If existingItem IsNot Nothing Then
            existingItem.Quantity += 1
        Else
            User.Cart.Add(newItem)
        End If

        MessageBox.Show("Added to cart: " & newItem.ProductName)

    End Sub

End Class