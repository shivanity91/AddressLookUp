using System.Threading.Tasks;
using System.Threading;
using Moq;
using Xunit;
using AddressLookUp.Aggregator.Api.Controllers;
using AddressLookUp.Aggregator.Api.Services;
using Api.Common.Contracts;

namespace AddressLookUp.Aggregator.Api.Tests.Controllers;

public class AddressLookUpControllerTests
{
    private readonly Mock<IAddressLookUpService> _mockIAddressLookUpService;
    private readonly AddressLookUpController _addressLookUpController;

    public AddressLookUpControllerTests()
    {
        _mockIAddressLookUpService = new Mock<IAddressLookUpService>();
        _addressLookUpController = new AddressLookUpController(_mockIAddressLookUpService.Object);
    }

    [Fact]
    public async void GetAddressLookUpAsync_Success_Result()
    {
        // Arrange
        var testDomain = "google.com";

        // Act
        var addressResult = new AddressLookUpResult();
        _mockIAddressLookUpService.Setup(e => e.GetAddressLookUpAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(addressResult));
        
        // Assert
        Assert.NotNull(addressResult);

    }
}
