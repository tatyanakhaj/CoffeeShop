using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShop
{
    public class OrderBuilder
    {
        private Order order;

        public OrderBuilder()
        {
            order = new Order
            {
                Items = new List<OrderItem>(),
                Status = OrderStatus.Created
            };
        }

        public OrderBuilder SetId(int id)
        {
            order.Id = id;
            return this;
        }

        public OrderBuilder AddItem(OrderItem item)
        {
            order.Items.Add(item);
            return this;
        }

        public void Reset()
        {
            order = new Order
            {
                Items = new List<OrderItem>(),
                Status = OrderStatus.Created
            };
        }
        public Order Build()
        {
            return order;
        }
    }
}
