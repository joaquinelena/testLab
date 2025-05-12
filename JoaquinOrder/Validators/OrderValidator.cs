using FluentValidation;
using JoaquinOrder.Models;

namespace JoaquinOrder.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.CustomerId).NotEmpty().WithMessage("El ID del cliente es requerido");
            RuleFor(order => order.Items).NotEmpty().WithMessage("La orden debe tener al menos un ítem");
            RuleForEach(order => order.Items).SetValidator(new OrderItemValidator());
        }
    }

    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(item => item.ProductId).NotEmpty().WithMessage("El ID del producto es requerido");
            RuleFor(item => item.Quantity).GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero");
        }
    }
}