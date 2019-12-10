<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Project2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Matthew Arndt - Project 2</title>
    <script src="validation.js" lang="javascript" type="text/javascript"></script>
    <link href="style.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <h1>Welcome to Starbux</h1>
    <form id="form1" runat="server">
        <asp:Label ID="lblName" Text="Enter your name*" runat="server"></asp:Label>
        <asp:TextBox ID="txtName" name="txtname" runat="server"></asp:TextBox>
        <br /> <br />
        <asp:Label ID="lblPhoneNumber" Text="Enter your phone number*" runat="server"></asp:Label>
        <asp:TextBox name="txtphonenumber" ID="txtphonenumber" runat="server"></asp:TextBox>
        <br /> <br />
        <asp:Label ID="lblRewards" Text="Enter your rewards account number (to get a 10% discount on your final order)" runat="server"></asp:Label>
        <asp:TextBox name="txtrewards" ID="txtrewards" runat="server"></asp:TextBox>
        <br /> <br />
        
        <asp:Label ID="lblSelectDelivery" runat="server">Select your delivery option.</asp:Label>
        <asp:RadioButtonList ID="options" runat="server">
            <asp:ListItem Value="instore" Selected="True">In-Store Pickup</asp:ListItem>
            <asp:ListItem Value="curb">Curb-Side Delivery</asp:ListItem>
            <asp:ListItem Value="delivery">Delivery</asp:ListItem>
        </asp:RadioButtonList>
        <br />

        <asp:Label Text="<h3>Coffees</h3>" runat="server" ID="lblCoffees"></asp:Label>
        <asp:GridView ID="coffee" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="Select Product">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="BasePrice" DataFormatString="{0:c}" HeaderText="Base Price" />
                <asp:TemplateField HeaderText="Size">
                    <ItemTemplate>
                        <asp:DropDownList ID="size" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="Small" />
                            <asp:ListItem Text="Tall" />
                            <asp:ListItem Text="Grande" />
                            <asp:ListItem Text="Venti" />
                            <asp:ListItem Text="Trenta" />
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type (iced/hot)">
                    <ItemTemplate>
                        <asp:DropDownList ID="temperature" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="Iced" />
                            <asp:ListItem Text="Hot" />
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <br />

        <asp:Label Text="<h3>Teas</h3>" runat="server" ID="lblTeas"></asp:Label>
        <asp:GridView ID="tea" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="Select Product">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="BasePrice" DataFormatString="{0:c}" HeaderText="Base Price" />
                <asp:TemplateField HeaderText="Size">
                    <ItemTemplate>
                        <asp:DropDownList ID="size" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="Small" />
                            <asp:ListItem Text="Tall" />
                            <asp:ListItem Text="Grande" />
                            <asp:ListItem Text="Venti" />
                            <asp:ListItem Text="Trenta" />
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type (iced/hot)">
                    <ItemTemplate>
                        <asp:DropDownList ID="temperature" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="Iced" />
                            <asp:ListItem Text="Hot" />
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="submitButton" runat="server" OnClick="submitButton_Click" Text="Submit Order" Visible="true"/>
        <br />
        <br /><asp:Button ID="reportButton" runat="server" OnClick="submitManagementReport_Click" Text="Show/Hide Management Report" />
        <br />

        <asp:Label runat="server" Visible="false" ID="lblYourOrder">Your order:</asp:Label>
        <br />
        <asp:Label runat="server" Visible="false" ID="lblDelivery" Text=""></asp:Label>
        <asp:GridView ID="gvDisplayOrder" Visible="false" runat="server" AutoGenerateColumns ="False" showfooter="True" Width="1090px">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" ReadOnly="True" />
                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True"  />
                <asp:BoundField DataField="Size" HeaderText="Size" ReadOnly="True"  />
                <asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="True" />
                <asp:BoundField DataField="Price" HeaderText="Price" ReadOnly="True"  DataFormatString="{0:c}"/>
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True" />
                <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" ReadOnly="True" DataFormatString="{0:c}"/>
            </Columns>
        </asp:GridView>
        <br />
        <!--THIS ONE STAYS HIDDEN -->
        <asp:GridView ID="RewardsForChecking" Visible="False" runat="server" AutoGenerateColumns ="True" showfooter="True" Width="1090px">
            <Columns>
                <asp:BoundField DataField="RewardAccountID" HeaderText="Title" ReadOnly="True" />
                <asp:BoundField DataField="CustomerName" HeaderText="Description" ReadOnly="True"  />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Size" ReadOnly="True"  />
                <asp:BoundField DataField="GrossSales" HeaderText="Type" ReadOnly="True" />
                <asp:BoundField DataField="TotalOrders" HeaderText="Price" ReadOnly="True"  DataFormatString="{0:c}"/>                
            </Columns>
        </asp:GridView>
        <!-- User for drink report-->
        <asp:Label runat="server" ID="lblDrinkReport" Text="<h5>Drink Report:</h5> (in descending order)" Visible="false"></asp:Label>
        <asp:GridView ID="drinkReport" Visible="False" runat="server" AutoGenerateColumns ="True" showfooter="True" Width="1090px">
            <Columns>
                <asp:BoundField DataField="ItemID" HeaderText="ItemID" ReadOnly="True" />
                <asp:BoundField DataField="Title" HeaderText="Title" ReadOnly="True"  />
                <asp:BoundField DataField="Category" HeaderText="Category" ReadOnly="True"  />
                <asp:BoundField DataField="TotalSales" HeaderText="Total Sales" ReadOnly="True" />
                <asp:BoundField DataField="TotalQuantitySold" HeaderText="Total Quantity Sold" ReadOnly="True"  DataFormatString="{0:c}"/>                
            </Columns>
        </asp:GridView>
        <br />
        <!-- User for rewards report-->
        <asp:Label runat="server" ID="lblRewardsReport" Text="<h5>Rewards Report:</h5>" Visible="false"></asp:Label>
        <asp:GridView ID="rewardReport" Visible="False" runat="server" AutoGenerateColumns ="True" showfooter="True" Width="1090px">
            <Columns>
                <asp:BoundField DataField="RewardAccountID" HeaderText="Title" ReadOnly="True" />
                <asp:BoundField DataField="CustomerName" HeaderText="Description" ReadOnly="True"  />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Size" ReadOnly="True"  />
                <asp:BoundField DataField="GrossSales" HeaderText="Type" ReadOnly="True" />
                <asp:BoundField DataField="TotalOrders" HeaderText="Price" ReadOnly="True"  DataFormatString="{0:c}"/>                
            </Columns>
        </asp:GridView>

    </form>
</body>
</html>
