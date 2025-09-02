using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Properties.API.Controllers;
using Properties.Application.Commands;

namespace TestMillionandup.Property;

[TestFixture]
public class OwnerControllerUnitTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public async Task Test_Owner_Post_Valid()
    {
        var mock = new Mock<IMediator>();
        CreateOwnerCommand owner = new("text", "test adress",  "1010211", "", DateTime.Now.AddYears(-20).ToString());
        mock.Setup(sp => sp.Send(owner, new CancellationToken())).Returns(Task.CompletedTask);

        OwnerController controller = new(mock.Object);
        var result = await controller.Post(owner);

        Assert.That(result, Is.InstanceOf<StatusCodeResult>());
    }
}
