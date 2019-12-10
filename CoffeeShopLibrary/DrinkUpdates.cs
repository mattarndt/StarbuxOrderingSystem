using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilities;
using System.Data.SqlClient;

namespace CoffeeShopLibrary
{
    public class DrinkUpdates
    {
        public static DBConnect GetConnection()
        {
            return new DBConnect();
        }

        public double calculateSale(String title, String size, String deliveryOption)
        {
            DBConnect objDB = GetConnection();//make a new object within the method
            SqlConnection conStr = objDB.GetConnection();//connection string from Student_Connection class method
            conStr.Open(); //openConnection 

            String selectStatement = "SELECT BasePrice FROM Drinks WHERE Title = '" + title + "'";//query string to get basePrice from DB
            var strCommand = new SqlCommand(selectStatement, conStr);
            
            double basePrice = (double)objDB.ExecuteScalarFunction(strCommand);//get the basePrice from the DPB
            double adjustedPrice = basePrice;

            if (size == "Tall")
            {
                adjustedPrice = adjustedPrice * .5 + adjustedPrice;
            }
            if (size == "Grande")
            {
                adjustedPrice = adjustedPrice * 1 + adjustedPrice;
            }
            if (size == "Venti")
            {
                adjustedPrice = adjustedPrice * 1.5 + adjustedPrice;
            }
            if (size == "Trenta")
            {
                adjustedPrice = adjustedPrice * 2 + adjustedPrice;
            }
            
            objDB.CloseConnection();
            return adjustedPrice;
        }

        public void updateDrinkQuantity(String title, int quant, String drinkType, double sale)
        {
            DBConnect objDB = GetConnection();//make a new object within the method

            string tsStr = "SELECT TotalSales FROM Drinks WHERE Title = '" + title + "'";//query string to get basePrice from DB
            objDB.GetDataSet(tsStr);
            double totalSales = (double)objDB.GetField("TotalSales", 0);
            
            string strr = "UPDATE Drinks SET TotalSales = '" + (sale + totalSales) + "' WHERE Title = '" + title + "'";
            string str2 = "UPDATE Drinks SET TotalQuantitySold = TotalQuantitySold + '" + quant + "' WHERE Title = '" + title + "'";
            objDB.DoUpdate(strr);
            objDB.DoUpdate(str2);

        }

        public void updateRewardsAccount(String accountNum, double sale)
        {
            DBConnect objDB = GetConnection();//make a new object within the method

            string tsStr = "SELECT * FROM RewardsAccounts WHERE RewardAccountID = '" + accountNum + "'";//query string to get basePrice from DB
            objDB.GetDataSet(tsStr);
            double grossSales = (double)objDB.GetField("GrossSales", 0);
            int totalOrders = (int)objDB.GetField("TotalOrders", 0);
            totalOrders++;

            double totalSales = grossSales + sale;

            string str1 = "UPDATE RewardsAccounts SET GrossSales = GrossSales+'" + sale + "' WHERE RewardAccountID = '" + accountNum + "'";
            string str2 = "UPDATE RewardsAccounts SET TotalOrders = '" + totalOrders + "' WHERE RewardAccountID = '" + accountNum + "'";
            objDB.DoUpdate(str1);
            objDB.DoUpdate(str2);
        }

    }
}