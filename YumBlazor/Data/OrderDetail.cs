﻿using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace YumBlazor.Data
{
	public class OrderDetail
	{
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int Count { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }= new List<OrderDetail>();

    }
}
