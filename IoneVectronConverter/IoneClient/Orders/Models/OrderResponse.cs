﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order2VPos.Core.IoneApi.Orders
{
    public class OrderResponse : ApiResponse
    {
        public OrderItemData Data { get; set; }
    }
}
