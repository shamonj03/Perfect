﻿using CSharpFunctionalExtensions;
using Perfect.Application.Orders.Models;
using Perfect.Application.Orders.Queries;

namespace Perfect.Application.Orders.Interfaces
{
    public interface IOrderService
    {
        Result<OrderModel> GetOrder(GetOrderQuery query);
    }
}