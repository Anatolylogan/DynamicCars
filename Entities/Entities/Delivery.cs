﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Delivery
    {
        public static int nextDeliveryId = 1;
        public int DeliveryId { get; set; }
        public string Address { get; set; }
        public Order Order { get; set; }
    }
}