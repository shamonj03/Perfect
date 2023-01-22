﻿namespace Perfect.Application.Orders.Models
{
    public record OrderModel(Guid Id, string Name, string Description, float Price, OrderUserModel User);
}
