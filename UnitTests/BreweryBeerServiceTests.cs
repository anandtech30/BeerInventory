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
    public class BreweryBeerServiceTests
    {
        private readonly Mock<IBreweryBeerRepository> _breweryBeerRepositoryMock;
        private readonly BreweryBeerService _breweryBeerService;

        public BreweryBeerServiceTests()
        {
            _breweryBeerRepositoryMock = new Mock<IBreweryBeerRepository>();
            _breweryBeerService = new BreweryBeerService(_breweryBeerRepositoryMock.Object);
        }

        [Fact]
        public async Task GetBreweryWithBeersAsync_ValidId_ReturnsBreweryWithBeers()
        {
            // Arrange
            int breweryId = 1;
            var expectedBrewery = new Brewery
            {
                Id = breweryId,
                Name = "Test Brewery",
                Beers = new List<Beer>
                {
                    new Beer { Id = 1, Name = "Beer 1", PercentageAlcoholByVolume = 5.0m },
                    new Beer { Id = 2, Name = "Beer 2", PercentageAlcoholByVolume = 6.0m }
                }
            };

            _breweryBeerRepositoryMock
                .Setup(repo => repo.GetBreweryWithBeersAsync(breweryId))
                .ReturnsAsync(expectedBrewery);

            // Act
            var result = await _breweryBeerService.GetBreweryWithBeersAsync(breweryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(breweryId, result.Id);
            Assert.Equal("Test Brewery", result.Name);
            Assert.Equal(2, result.Beers.Count);
        }

        [Fact]
        public async Task GetBreweryWithBeersAsync_InvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            int breweryId = 1;
            _breweryBeerRepositoryMock
                .Setup(repo => repo.GetBreweryWithBeersAsync(breweryId))
                .ReturnsAsync((Brewery)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _breweryBeerService.GetBreweryWithBeersAsync(breweryId));
        }

        [Fact]
        public async Task GetAllBreweriesWithBeersAsync_ReturnsAllBreweriesWithBeers()
        {
            // Arrange
            var breweries = new List<Brewery>
            {
                new Brewery { Id = 1, Name = "Brewery 1", Beers = new List<Beer>() },
                new Brewery { Id = 2, Name = "Brewery 2", Beers = new List<Beer>() }
            };

            _breweryBeerRepositoryMock
                .Setup(repo => repo.GetAllBreweriesWithBeersAsync())
                .ReturnsAsync(breweries);

            // Act
            var result = await _breweryBeerService.GetAllBreweriesWithBeersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<Brewery>)result).Count);
        }

        [Fact]
        public async Task AddBeerToBreweryAsync_ValidBeer_ReturnsAddedBeer()
        {
            // Arrange
            var beer = new Beer { Id = 1, Name = "New Beer", BreweryId = 1, PercentageAlcoholByVolume = 5.5m };
            var addedBeer = new Beer { Id = 1, Name = "New Beer", BreweryId = 1, PercentageAlcoholByVolume = 5.5m };

            _breweryBeerRepositoryMock
                .Setup(repo => repo.AddBeerToBreweryAsync(beer))
                .ReturnsAsync(addedBeer);

            // Act
            var result = await _breweryBeerService.AddBeerToBreweryAsync(beer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(beer.Name, result.Name);
            Assert.Equal(beer.PercentageAlcoholByVolume, result.PercentageAlcoholByVolume);
        }

        [Fact]
        public async Task AddBeerToBreweryAsync_InvalidBrewery_ThrowsKeyNotFoundException()
        {
            // Arrange
            var beer = new Beer { Id = 1, Name = "New Beer", BreweryId = 1, PercentageAlcoholByVolume = 5.5m };
            _breweryBeerRepositoryMock
                .Setup(repo => repo.AddBeerToBreweryAsync(beer))
                .ReturnsAsync((Beer)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _breweryBeerService.AddBeerToBreweryAsync(beer));
        }
    }
}
