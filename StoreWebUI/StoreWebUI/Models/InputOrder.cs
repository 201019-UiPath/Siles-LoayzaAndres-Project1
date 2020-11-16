using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreWebUI.Models
{
    public class InputOrder
    {
        [Required]
        public Address DestinationAddress { get; set; }
    }
}
