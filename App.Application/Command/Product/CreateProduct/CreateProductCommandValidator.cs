using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Command.Product.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Quantity)
                    .NotEmpty()
                        .WithErrorCode("required")
                        .WithMessage("Quantity is required.")
                    .Must(BeGreaterThanZero)
                        .WithErrorCode("mustBeGreaterThan0")
                        .WithMessage("Must be greater than 0");

            RuleFor(x => x.UnitPrice)
               .NotEmpty()
               .WithErrorCode("required")
               .WithMessage("Unit Price is required.")
           .Must(BeGreaterThanZeroValue)
               .WithErrorCode("mustBeGreaterThan0")
               .WithMessage("Must be greater than 0");

            RuleFor(x => x.SellingUnitPrice)
                .NotEmpty()
                   .WithErrorCode("required")
                   .WithMessage("Selling Unit Price is required.")
                .Must(BeGreaterThanZeroValue)
                   .WithErrorCode("mustBeGreaterThan0")
                   .WithMessage("Must be greater than 0");


            RuleFor(x => x.Name)
                .NotEmpty()
                   .WithErrorCode("required")
                   .WithMessage("Name is required.");

            RuleFor(x => x.Description)
                .NotEmpty()
                   .WithErrorCode("required")
                   .WithMessage("Description is required.");


        }

        private bool BeGreaterThanZeroValue(decimal value)
        {
            if (value <= 0)
                return false;
            return true;
        }

        private bool BeGreaterThanZero(int quantity)
        {
            if (quantity <= 0)
                return false;
            return true;
        }
    }
}
