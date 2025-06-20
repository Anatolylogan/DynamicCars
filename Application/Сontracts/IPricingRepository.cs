﻿using Application.Entities;

namespace Application.Сontracts
{
    public interface IPricingRepository : IRepository<Pricing>
    {
        decimal BasePrice { get; }
        decimal BrandMultiplier { get; }
        decimal CarpetCost { get; }
        decimal DiscountRate { get; }
    }
}