﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(string? Brand, string? type,string? sort) : base(x =>
            (string.IsNullOrWhiteSpace(Brand) || x.Brand == Brand) &&
            (string.IsNullOrWhiteSpace(type) || x.Type == type)
        )
        {           
            switch(sort)
            {
                case "priceAsc": AddOrderBy(x => x.Price);
                    break;
                case "priceDesc": AddOrderByDescending(x => x.Price); 
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;

            }
        }
    }
}
