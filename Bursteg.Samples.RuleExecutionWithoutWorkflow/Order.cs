//=====================================================================
// Rules Execution without Workflow Sample
//
// Guy Burstein
// http://blogs.microsoft.co.il/blogs/bursteg
//
// October 11th, 2006
//
//=====================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace Bursteg.Samples.RuleExecutionWithoutWorkflow
{
    /// <summary>
    /// A simple business entity
    /// </summary>
    public class Order
    {
        // Data Members
        private Guid id;
        private string customer;
        private DateTime orderDate;
        private int discount;
        private int amount;
        private decimal unitPrice;

        // Constructor
        public Order(string customer, int amount, decimal unitPrice)
        {
            this.customer = customer;
            this.amount = amount;
            this.id = Guid.NewGuid();
            this.discount = 0;
            this.unitPrice = unitPrice;
            this.orderDate = DateTime.Now;
        }

        // Methods
        public decimal GetTotalPrice()
        {
            return unitPrice * amount * (1 - (discount / 100M));
        }

        // Properties
        public Guid ID
        {
            get { return id; }
        }

        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }


        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public string Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }
    }
}
