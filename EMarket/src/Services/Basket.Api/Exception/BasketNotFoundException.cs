﻿using BuildingBlocks.Exceptions;

namespace Basket.Api.Exception;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string? userName) : base("Basket", $"{userName} - basket not found")
    {
    }


}
