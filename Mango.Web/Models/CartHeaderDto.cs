﻿using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string? CuponCode { get; set; }
        public double OrderTotal { get; set; }
    }
}
