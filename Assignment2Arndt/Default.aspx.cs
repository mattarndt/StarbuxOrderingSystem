using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

using Utilities;
using CoffeeShopLibrary;
using System.Data;
using System.Collections;

namespace Project2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DBConnect objDB = new DBConnect();
        Validator v = new Validator();
        List<Drink> drinkOrderList = new List<Drink>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //load drink and put them in proper gridviews
                DBConnect objDB = new DBConnect();
                DataSet myData = objDB.GetDataSet("SELECT * FROM Drinks WHERE Category = 'coffee'");
                DataSet myData2 = objDB.GetDataSet("SELECT * FROM Drinks WHERE Category = 'tea'");

                coffee.DataSource = myData;
                coffee.DataBind();
                tea.DataSource = myData2;
                tea.DataBind();

                DBConnect objDB2 = new DBConnect();
                DataSet drinkData = objDB2.GetDataSet("SELECT * FROM Drinks ORDER BY TotalSales desc");
                DataSet rewardsData = objDB2.GetDataSet("SELECT * FROM RewardsAccounts");

                drinkReport.DataSource = drinkData;
                drinkReport.DataBind();
                rewardReport.DataSource = rewardsData;
                rewardReport.DataBind();
            }
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            bool hasRewardsAccount = false;
            //check if the user has a rewards account
            DBConnect objDB = new DBConnect();
            String rewardsNumber = txtrewards.Text;
            DataSet rewardsData = objDB.GetDataSet("SELECT * FROM RewardsAccounts WHERE RewardAccountID = '" + rewardsNumber +"'");

            if (rewardsData.Tables[0].Rows.Count != 0)
            {
                hasRewardsAccount = true;
            }


            RewardsForChecking.DataSource = rewardsData;
            RewardsForChecking.DataBind();
                  
            //get values for items at the top of the page.
            String name = txtName.Text;
            String phoneNumber = txtphonenumber.Text;
            String deliveryOption = options.SelectedValue;
            //validate that the user entered in a name and phone number
            if(!(v.checkNameAndPhoneNumber(name, phoneNumber)))
            {
                String incorrect = "You must enter a name and phone number";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + incorrect + "');", true);
            }
            else
            {
                //used for tracking throughout the process.
                int count = 0; //total drink sale count
                double totalOrderCost = 0;
                bool coffeeCheckPresent = false;
                bool teaCheckPresent = false;

                //goes through coffee gridview
                foreach (GridViewRow row in coffee.Rows)
                {
                    if (((CheckBox)row.FindControl("chkSelect")).Checked) //if a row was selected
                    {
                        coffeeCheckPresent = true;
                        double thisDrinkPrice;
                        //get all the values in the row that was selected and assign them to variables to store in a drink object later
                        String title = row.Cells[1].Text;
                        String desc = row.Cells[2].Text;
                        String strbasePrice = row.Cells[3].Text;
                        strbasePrice = strbasePrice.Remove(0, 1);
                        DropDownList ddlDrinkSize = (DropDownList)row.Cells[4].FindControl("size");
                        DropDownList ddlTemperature = (DropDownList)row.Cells[5].FindControl("temperature");
                        TextBox txtQuantity = (TextBox)row.Cells[6].FindControl("txtQuantity");

                        try
                        {
                            DrinkUpdates du = new DrinkUpdates();
                            //converts every data item to proper variable type
                            int quantity = Convert.ToInt32(txtQuantity.Text);
                            double basePrice = Convert.ToDouble(strbasePrice);
                            count = count + quantity;
                            String drinkSize = Convert.ToString(ddlDrinkSize.Text);
                            String temperature = Convert.ToString(ddlTemperature.Text);

                            //changes the price for this individual drink based on size
                            thisDrinkPrice = du.calculateSale(title, drinkSize, deliveryOption);

                            double totalSales = thisDrinkPrice * quantity; //sets the total sales of that specific item selected based on size and quantity

                            totalSales = Math.Truncate(totalSales * 100) / 100; //format to two decimals
                            Drink drink = new Drink(title, desc, drinkSize, "coffee", basePrice, totalSales, quantity);
                            drinkOrderList.Add(drink);

                            totalOrderCost = totalOrderCost + totalSales; //used for footer
                            if (rewardsData.Tables[0].Rows.Count != 0) //if the rewards account is valid
                            {
                                hasRewardsAccount = true;
                                totalOrderCost = totalOrderCost * .9;
                                totalOrderCost = Math.Truncate(totalOrderCost * 100) / 100;//format to two decimals
                                du.updateRewardsAccount(rewardsNumber, totalOrderCost);
                            }
                            du.updateDrinkQuantity(title, quantity, "coffee", totalOrderCost);
                        }
                        catch (Exception)
                        {
                            String incorrect = "Incorrect input, try again. You must select a quantity for all drinks selected";
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + incorrect + "');", true);
                        }
                    }
                }
                //goes through the tea gridview
                foreach (GridViewRow row in tea.Rows)
                {
                    if (((CheckBox)row.FindControl("chkSelect")).Checked) //if a row was selected
                    {
                        teaCheckPresent = true;
                        double thisDrinkPrice;
                        //get all the values in the row that was selected and assign them to variables to store in a drink object later
                        String title = row.Cells[1].Text;
                        String desc = row.Cells[2].Text;
                        String strbasePrice = row.Cells[3].Text;
                        strbasePrice = strbasePrice.Remove(0, 1);
                        DropDownList ddlDrinkSize = (DropDownList)row.Cells[4].FindControl("size");
                        DropDownList ddlTemperature = (DropDownList)row.Cells[5].FindControl("temperature");
                        TextBox txtQuantity = (TextBox)row.Cells[6].FindControl("txtQuantity");

                        try
                        {
                            DrinkUpdates du = new DrinkUpdates();
                            //converts every data item to proper variable type
                            int quantity = Convert.ToInt32(txtQuantity.Text);
                            double basePrice = Convert.ToDouble(strbasePrice);
                            count = count + quantity;
                            String drinkSize = Convert.ToString(ddlDrinkSize.Text);
                            String temperature = Convert.ToString(ddlTemperature.Text);

                            //changes the price for this individual drink based on size
                            thisDrinkPrice = du.calculateSale(title, drinkSize, deliveryOption);

                            double totalSales = thisDrinkPrice * quantity; //sets the total sales of that specific item selected based on size and quantity

                            totalSales = Math.Truncate(totalSales * 100) / 100; //format to two digits
                            Drink drink = new Drink(title, desc, drinkSize, "tea", basePrice, totalSales, quantity);
                            drinkOrderList.Add(drink);

                            totalOrderCost = totalOrderCost + totalSales; //used for footer
                            if (rewardsData.Tables[0].Rows.Count != 0) //if the rewards account is valid
                            {
                                hasRewardsAccount = true;
                                totalOrderCost = totalOrderCost * .9;
                                du.updateRewardsAccount(rewardsNumber, totalOrderCost);
                            }
                            du.updateDrinkQuantity(title, quantity, "tea", totalOrderCost);
                        }
                        catch (Exception)
                        {
                            String incorrect = "Incorrect input, try again";
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + incorrect + "');", true);
                        }
                    }
                }

                if (!coffeeCheckPresent && !teaCheckPresent)
                {
                    String myStringVariable = "You must select at least one drink";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
                }
                else
                {
                    lblYourOrder.Visible = true;
                    gvDisplayOrder.Visible = true;
                    lblDelivery.Visible = true;

                    gvDisplayOrder.Columns[0].FooterText = "Totals:";
                    gvDisplayOrder.Columns[5].FooterText = count.ToString();
                    gvDisplayOrder.Columns[6].FooterText = totalOrderCost.ToString("C2");//footer totalCost

                    gvDisplayOrder.DataSource = drinkOrderList;
                    gvDisplayOrder.DataBind();
                }
            }
            
        }

        protected void submitManagementReport_Click(object sender, EventArgs e)
        {
            //hiding and displaying everything when the user selects hide/view management report
            if (drinkReport.Visible == false)
            {
                drinkReport.Visible = true;
                rewardReport.Visible = true;
                gvDisplayOrder.Visible = false;
                coffee.Visible = false;
                tea.Visible = false;
                lblCoffees.Visible = false;
                lblTeas.Visible = false;
                lblYourOrder.Visible = false;
                lblDrinkReport.Visible = true;
                lblRewardsReport.Visible = true;
                submitButton.Visible = false;

                lblName.Visible = false;
                lblPhoneNumber.Visible = false;
                lblRewards.Visible = false;
                lblSelectDelivery.Visible = false;
                txtphonenumber.Visible = false;
                txtrewards.Visible = false;
                txtName.Visible = false;
                options.Visible = false;
            }
            else
            { //turn the original gridview back on
                drinkReport.Visible = false;
                rewardReport.Visible = false;
                gvDisplayOrder.Visible = true;
                coffee.Visible = true;
                tea.Visible = true;
                lblCoffees.Visible = true;
                lblTeas.Visible = true;
                lblDrinkReport.Visible = false;
                lblRewardsReport.Visible = false;
                submitButton.Visible = true;

                lblName.Visible = true;
                lblPhoneNumber.Visible = true;
                lblRewards.Visible = true;
                lblSelectDelivery.Visible = true;
                txtphonenumber.Visible = true;
                txtrewards.Visible = true;
                txtName.Visible = true;
                options.Visible = true;
            }
            
            
        }
        

    }
}