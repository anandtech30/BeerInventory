using BeerInventory.Data.Repositories;
using BeerInventory.Models;
using BeerInventory.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BeerInventory.Tests.Services
{
    public class BeerServiceTests
    {
        private readonly Mock<IBeerRepository> _beerRepositoryMock;
        private readonly BeerService _beerService;

        public BeerServiceTests()
        {
            _beerRepositoryMock = new Mock<IBeerRepository>();
            _beerService = new BeerService(_beerRepositoryMock.Object);
        }

        [Fact]
        public async Task GetBeerByIdAsync_ValidId_ReturnsBeer()
        {
            // Arrange
            int beerId = 1;
            var expectedBeer = new Beer { Id = beerId, Name = "Test Beer", PercentageAlcoholByVolume = 5.0m };
            _beerRepositoryMock.Setup(repo => repo.GetByIdAsync(beerId))
                               .ReturnsAsync(expectedBeer);

            // Act
            var result = await _beerService.GetBeerByIdAsync(beerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(beerId, result.Id);
            Assert.Equal("Test Beer", result.Name);
        }

        [Fact]
        public async Task GetBeerByIdAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            int beerId = 1;
            _beerRepositoryMock.Setup(repo => repo.GetByIdAsync(beerId))
                               .ReturnsAsync((Beer)null);

            // Act
            var result = await _beerService.GetBeerByIdAsync(beerId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBeersAsync_ValidAlcoholRange_ReturnsBeers()
        {
            // Arrange
            var beers = new List<Beer>
            {
                new Beer { Id = 1, Name = "Beer 1", PercentageAlcoholByVolume = 4.5m },
                new Beer { Id = 2, Name = "Beer 2", PercentageAlcoholByVolume = 6.5m }
            };
            decimal? gtAlcohol = 4.0m;
            decimal? ltAlcohol = 7.0m;

            _beerRepositoryMock.Setup(repo => repo.GetBeersAsync(gtAlcohol, ltAlcohol))
                               .ReturnsAsync(beers);

            // Act
            var result = await _beerService.GetBeersAsync(gtAlcohol, ltAlcohol);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<Beer>)result).Count);
        }

        [Fact]
        public async Task AddBeerAsync_ValidBeer_ReturnsBeer()
        {
            // Arrange
            var newBeer = new Beer { Name = "New Beer", PercentageAlcoholByVolume = 5.5m };
            _beerRepositoryMock.Setup(repo => repo.AddBeerAsync(newBeer))
                               .ReturnsAsync(new Beer { Id = 3, Name = newBeer.Name, PercentageAlcoholByVolume = newBeer.PercentageAlcoholByVolume });

            // Act
            var result = await _beerService.AddBeerAsync(newBeer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Beer", result.Name);
            Assert.Equal(5.5m, result.PercentageAlcoholByVolume);
            Assert.Equal(3, result.Id);
        }

        [Fact]
        public async Task UpdateBeerAsync_BeerExists_UpdatesBeer()
        {
            // Arrange
            int beerId = 1;
            var existingBeer = new Beer { Id = beerId, Name = "Old Beer", PercentageAlcoholByVolume = 4.0m };
            var updatedBeer = new Beer { Id = beerId, Name = "Updated Beer", PercentageAlcoholByVolume = 5.5m };

            _beerRepositoryMock.Setup(repo => repo.GetByIdAsync(beerId))
                               .ReturnsAsync(existingBeer);
            _beerRepositoryMock.Setup(repo => repo.UpdateBeerAsync(existingBeer))
                               .Returns(Task.CompletedTask);

            // Act
            await _beerService.UpdateBeerAsync(beerId, updatedBeer);

            // Assert
            Assert.Equal(updatedBeer.Name, existingBeer.Name);
            Assert.Equal(updatedBeer.PercentageAlcoholByVolume, existingBeer.PercentageAlcoholByVolume);
            _beerRepositoryMock.Verify(repo => repo.UpdateBeerAsync(existingBeer), Times.Once);
        }

        [Fact]
        public async Task UpdateBeerAsync_BeerDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            int beerId = 1;
            var updatedBeer = new Beer { Id = beerId, Name = "Updated Beer", PercentageAlcoholByVolume = 5.5m };
            _beerRepositoryMock.Setup(repo => repo.GetByIdAsync(beerId))
                               .ReturnsAsync((Beer)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _beerService.UpdateBeerAsync(beerId, updatedBeer));
        }
    }
}
