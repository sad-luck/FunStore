using FluentAssertions;
using FunStore.Persistence;
using FunStore.Persistence.Builders;
using FunStore.ValidationExceptions;

namespace FunStore.Tests.Persistance.Builders;

[TestFixture]
public class OrderBuilderTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(GetTestCases))]
    public void BuildOrder_ThrowsException_WhenExpectedItemsMissed(Customer customer, ProductBase product, string expectedErrorMsg)
    {
        // Arrange 
        var orderBuilder = new OrderBuilder();
        orderBuilder.SetCustomer(customer);
        orderBuilder.AddItem(product);

        // Act
        var act = orderBuilder.Build;

        // Assert
        act.Should().Throw<ValidationException>().WithMessage(expectedErrorMsg);
    }

    [TestCaseSource(nameof(GetSuccessfullTestCases))]
    public void BuildOrder_ShouldBuildAndReturnOrder(Customer customer, ProductBase product)
    {
        // Arrange 
        var orderBuilder = new OrderBuilder();
        orderBuilder.SetCustomer(customer);
        orderBuilder.AddItem(product);

        // Act
        var orderBuilderResult = orderBuilder.Build();

        // Assert
        orderBuilderResult.Should().NotBeNull();

        orderBuilderResult.Items.Should().HaveCount(1);
        orderBuilderResult.Items.Should().Contain(product.Title);

        orderBuilderResult.Customer.Should().NotBeNull();
        orderBuilderResult.Customer.Should().Be(customer);
    }

    private static IEnumerable<object[]> GetTestCases()
    {
        yield return new object[] { new Customer(), null, "At least one item must be added" };
        yield return new object[] { null, new Video(), "Customer must be provided" };
    }

    private static IEnumerable<object[]> GetSuccessfullTestCases()
    {
        yield return new object[] { new Customer(), new Video() };
    }
}