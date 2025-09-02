using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Properties.API.Controllers;
using Properties.Application.Commands;

namespace TestMillionandup.Property;

[TestFixture]
public class PropertyControllerUnitTest
{
    private Mock<IMediator> mock;

    [SetUp]
    public void Setup()
    {
        mock = new Mock<IMediator>();
    }

    [Test]
    public async Task Test_Property_Valid()
    {
        string propertyObj = "{  \"name\": \"Word Trade center\",  \"address\": \"carrera 90 bis #83c - 28\",  \"price\": 556.0,  \"codeInternal\": \"54455545\",  \"year\": 1995,  \"ownerDocument\": \"1010211905\"}";

        CreatePropertyCommand property = Newtonsoft.Json.JsonConvert.DeserializeObject<CreatePropertyCommand>(propertyObj);
        mock.Setup(c => c.Send(property, new CancellationToken())).Returns(Task.CompletedTask);

        PropertyController controller = new(mock.Object);
        var dto = await controller.Post(property);

        Assert.That(((StatusCodeResult)dto).StatusCode, Is.EqualTo(200), "Propiedad no se pudo insertar");
    }

    [Test]
    public async Task Test_Property_Put_Valid()
    {
        string propertyObj = "{  \"name\": \"Word Trade center\",  \"address\": \"carrera 90 bis #83c - 28\",  \"price\": 556.0,  \"codeInternal\": \"54455545\",  \"year\": 1995,  \"ownerDocument\": \"1010211905\"}";
        UpdatePropertyCommand property = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdatePropertyCommand>(propertyObj);
        mock.Setup(c => c.Send(property, new CancellationToken())).Returns(Task.CompletedTask);

        PropertyController controller = new(mock.Object);
        var dto = await controller.Put(property);

        Assert.That(((StatusCodeResult)dto).StatusCode == 200, Is.True, "Propiedad no Actualizada");
    }

    [Test]
    public async Task Test_Property_Patch_Valid()
    {
        var price = new UpdatePropertyPriceCommand("12233", 20);
        mock.Setup(c => c.Send(price, new CancellationToken())).Returns(Task.CompletedTask);

        PropertyController controller = new(mock.Object);
        var dto = await controller.UpdatePrice(price);

        Assert.That(((StatusCodeResult)dto).StatusCode, Is.EqualTo(200), "no se pudo actualizar el precio");
    }
}
