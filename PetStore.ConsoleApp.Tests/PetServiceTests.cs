using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using PetStore.ConsoleApp.Models;
using System.Linq;
using Moq;

namespace PetStore.ConsoleApp.Tests
{
    public class PetServiceTests
    {
        Mock<IPetService> petServiceMock;
        List<Pet> availablePets;

        [SetUp]
        public void Setup()
        {
            petServiceMock = new Mock<IPetService>();

        }

        //1. Test that the method returns an empty collection when there are no available pets:
        [Test]
        public async Task GetAllAvailablePets_NoAvailablePets_ReturnsEmptyCollection()
        {
            // Arrange
            availablePets = new List<Pet>();
            var allAvailablePetsByCategory = availablePets.GroupBy(x => x.Category?.Name);

            petServiceMock.Setup(x => x.GetAllAvailablePetsByCategories()).ReturnsAsync(allAvailablePetsByCategory);

            // Act
            var result = await petServiceMock.Object.GetAllAvailablePetsByCategories();

            // Assert
            Assert.IsFalse(result.Any());
        }

        //2. Test that the method returns the correct collection of available pets when there are available pets:
        [Test]
        public async Task GetAllAvailablePets_AvailablePets_ReturnsCorrectCollection()
        {
            // Arrange
            availablePets = new List<Pet>
            {
                new Pet { Id = 1, Name = "Dog", Status = "available", Category = new ModelBase{ Name = "Mammal"} },
                new Pet { Id = 2, Name = "Cat", Status = "available", Category = new ModelBase{ Name = "Mammal"} },
                new Pet { Id = 3, Name = "Bird", Status = "available", Category = new ModelBase{ Name = "Mammal"} },
                new Pet { Id = 4, Name = "Fish", Status = "available", Category = new ModelBase {Name =  "Aquatic"} }
            };
            var allAvailablePetsByCategory = availablePets.GroupBy(x => x.Category?.Name);

            petServiceMock.Setup(x => x.GetAllAvailablePetsByCategories()).ReturnsAsync(allAvailablePetsByCategory);

            // Act
            var result = await petServiceMock.Object.GetAllAvailablePetsByCategories();

            // Assert
            CollectionAssert.AreEqual(allAvailablePetsByCategory.Select(x => x.Key).Distinct() , result.Select(x => x.Key).Distinct());
        }
    }
}