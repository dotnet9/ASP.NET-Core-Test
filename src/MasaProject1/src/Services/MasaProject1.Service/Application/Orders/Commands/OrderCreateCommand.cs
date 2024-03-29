﻿namespace MasaProject1.Service.Application.Orders.Commands;

public record OrderCreateCommand : DomainCommand
{
    public List<OrderItem> Items { get; set; } = new ();
}