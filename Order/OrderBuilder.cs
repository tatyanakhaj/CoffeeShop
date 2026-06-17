using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShop
{
    internal class OrderBuilder
    {
        private Order order = new Order();

        public OrderBuilder SetId(int id)
        {
            order.Id = id;
            return this;
        }

        public OrderBuilder SetStatus(OrderStatus status)
        {
            order.Status = status;
            return this;
        }

        public OrderBuilder AddItem(OrderItem item)
        {
            if (order.Items == null)
            {
                order.Items = new List<OrderItem>();
            }


            order.Items.Add(item);
            return this;
        }

        public Order Build()
        {
            return order;
        }
    }

}

