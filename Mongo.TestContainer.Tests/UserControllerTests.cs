using Microsoft.AspNetCore.Mvc;
using Mongo.TestContainer.Controllers;
using Mongo.TestContainer.Services.Interfaces;
using MongoDB.Bson;
using Moq;

namespace Mongo.TestContainer.Tests;

public class UserControllerTests
{
    private readonly UserController _controller;
    private readonly Mock<IMongoRepository<BsonDocument>> _repositoryMock;

    public UserControllerTests()
    {
        _repositoryMock = new Mock<IMongoRepository<BsonDocument>>();
        _controller = new UserController(_repositoryMock.Object);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Get_ShouldReturnOkResult()
    {
        List<BsonDocument> expectedResults = GetExpectedData();
        _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedResults);

        var result = await _controller.GetUserList();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    private static List<BsonDocument> GetExpectedData()
    {
        return
        [
            new() { { "Jane", "John" } },
            new() { { "John", "Jane" } }
        ];
    }
}
