using Moq;
using Properties.Domain.Entities;
using Properties.Domain.Services;
using Shared.Domain.Base;
using Shared.Domain.Base.IRepository;

namespace TestMillionandup.Property;

[TestFixture]
public class OwnerDomainUnitTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test_OwnerDomain_Save()
    {
        string ownerStringt = "{  \"name\": \"Alex\",  \"address\": \"carrera 90 bis #8725\",  \"document\": \"1212521\",  \"photo\": \"\",  \"birthday\": \"02-10-1993\"}";
        Owner owner = Newtonsoft.Json.JsonConvert.DeserializeObject<Owner>(ownerStringt);
        Mock<IRepository<Owner>> mockDomainservice = new();
        mockDomainservice.Setup(sp => sp.Insert(owner));
        mockDomainservice.Setup(sp => sp.List(x => x.Document == owner.Document)).Returns(new List<Owner>());

        OwnerDomainService service = new(mockDomainservice.Object);
        ActionResult result = await service.SaveAsync(owner);

        Assert.That(result.IsSuccessful, Is.True);
    }

    [Test]
    public async Task Test_OwnerDomain_Save_Not_Valid()
    {
        string ownerStringt = "{  \"name\": \"Alex\",  \"address\": \"carrera 90 bis #8725\",  \"document\": \"1212521\",  \"photo\": \"\",  \"birthday\": \"02-10-1993\"}";
        Owner owner = Newtonsoft.Json.JsonConvert.DeserializeObject<Owner>(ownerStringt);
        Mock<IRepository<Owner>> mockDomainservice = new();
        mockDomainservice.Setup(sp => sp.Insert(owner));
        mockDomainservice.Setup(sp => sp.List(x => x.Document == owner.Document)).Returns(new List<Owner>() { new Owner { Document = owner.Document } });

        OwnerDomainService service = new(mockDomainservice.Object);
        ActionResult result = await service.SaveAsync(owner);

        Assert.That(!result.IsSuccessful, Is.True);
    }
}
