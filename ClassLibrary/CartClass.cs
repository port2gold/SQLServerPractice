using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class CartClass
    {
        /// <summary>
        /// Cart Class
        /// </summary>
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
