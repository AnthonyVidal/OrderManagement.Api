using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace OrderManagement.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator
    : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Cliente).NotEmpty();
            RuleFor(x => x.Details).NotEmpty();
        }
    }

}
