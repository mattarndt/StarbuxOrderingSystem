using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeShopLibrary
{
    public class Drink
    {
        private String title;
        private String description;
        private String size;
        private String type;
        private double basePrice;
        private double totalSales;
        private int totalQuantitySold;

        public Drink(String title, String description, String size, String type, double basePrice, double totalSales, int totalQuantitySold)
        {
            this.title = title;
            this.description = description;
            this.size = size;
            this.type = type;
            this.basePrice = basePrice;
            this.totalSales = totalSales;
            this.totalQuantitySold = totalQuantitySold;
        }

        public Drink()
        {

        }

        public String Title
        {
            get { return title; }
            set { title = value; }
        }
        public String Description
        {
            get { return description; }
            set { description = value; }
        }
        public String Size
        {
            get { return size; }
            set { size = value; }
        }
        public String Type
        {
            get { return type; }
            set { type = value; }
        }
        public double Price
        {
            get { return basePrice; }
            set { basePrice = value; }
        }
        public double TotalCost
        {
            get { return totalSales; }
            set { totalSales = value; }
        }
        public int Quantity
        {
            get { return totalQuantitySold; }
            set { totalQuantitySold = value; }
        }

        

       
    }
}