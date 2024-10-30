﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YumBlazor.Data
{
	public class OrderHeader
	{
        public int Id { get; set; }
		[Required]
		public string UserId { get; set; }
        [Required]
		[Display(Name ="Order Total")]
        public double OrderTotal { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string  Status { get; set; }
        [Required]
        public string  Name { get; set; }
        [Required]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
		[Display(Name = "Email")]
		public string Email { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        //stripe column
        public string?  SessionId{ get; set; } 
        public string?  IntentId{ get; set; } 
    }
}