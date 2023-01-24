﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests
{
    public class OrderItemTests
    {
        [Fact]
        public void OrderItem_WithZeroCount_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                int count = 0;
                new OrderItem(1, count, 0);
            });
        }

        [Fact]
        public void OrderItem_WithNegativeCount_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                int count = -1;
                new OrderItem(1, count, 0);
            });
        }

        [Fact]
        public void OrderItem_WithPositiveCount_SetsCount()
        {
            OrderItem orderItem = new OrderItem(1, 2, 3);
            Assert.Equal(1, orderItem.BookId);
            Assert.Equal(2, orderItem.Count);
            Assert.Equal(3m, orderItem.Price);
        }

        [Fact]
        public void Count_WithNegativeValue_ThrowsArgumentOfRangeException()
        {
            var orderItem = new OrderItem(0, 5, 0m);
            Assert.Throws<ArgumentOutOfRangeException>(() => { orderItem.Count = -1; });
        }

        [Fact]
        public void Count_WithZeroValue_ThrowsArgumentOfRangeException()
        {
            var orderItem = new OrderItem(0, 5, 0m);
            Assert.Throws<ArgumentOutOfRangeException>(() => { orderItem.Count = 0; });
        }

        [Fact]
        public void Count_WithPositiveValue_SetsValue()
        {
            var orderItem = new OrderItem(0, 5, 0m);
            orderItem.Count = 10;

            Assert.Equal(10, orderItem.Count);
        }
    }
}