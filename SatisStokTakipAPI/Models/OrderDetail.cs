﻿namespace SatisStokTakipAPI.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }

}
