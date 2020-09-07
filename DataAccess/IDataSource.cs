using System;
using System.Collections.Generic;
using System.Text;

namespace DataSource
{
    public interface IDataSource
    {
        public void AddProduct(string productName, decimal costprice);


        public void AddCart(int Productid, int Qty);


        public void DeleteProduct(string productName);


        public void RemoveCart(int id);




    }
}
