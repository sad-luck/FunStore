using FunStore.ValidationExceptions;

namespace FunStore.Persistence.Builders;

public class OrderBuilder
{
    private readonly Order _order = new();

    public OrderBuilder SetCustomer(Customer? customer)
    {
        _order.Customer = customer;
        return this;
    }

    public OrderBuilder AddItem(ProductBase product)
    {
        _order.AddItem(product);
        return this;
    }

    public Order Build()
    {
        if (_order.Customer is null)
            throw new ValidationException("Customer must be provided");

        if (_order.Items.Count == 0)
            throw new ValidationException("At least one item must be added");

        return _order;
    }
}