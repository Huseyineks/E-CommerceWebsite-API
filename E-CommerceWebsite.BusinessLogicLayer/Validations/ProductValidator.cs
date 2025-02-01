using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.BusinessLogicLayer.Validations
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator()
        {
            RuleFor(i => i.ProductPrice).NotEmpty().WithMessage("Lütfen ürün ücretini girin.");
            RuleFor(i => i.ProductName).NotEmpty().WithMessage("Lütfen ürün adını girin.");
            RuleFor(i => i.ProductImage).NotEmpty().WithMessage("Lütfen ürün görseli girin.");
        }
    }
}
