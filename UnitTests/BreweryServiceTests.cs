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
    public class BreweryServiceTests
    {
        private readonly Mock<IBreweryRepository> _breweryRepositoryMock;
        private readonly BreweryService _breweryService;

        public BreweryServiceTests()
        {
            _breweryRepositoryMock = new Mock<IBreweryRepository>();
            _breweryService = new BreweryService(_breweryRepositoryMock.Object);
        }

        [Fact]
        public async Task GetBreweryByIdAsync_ValidId_ReturnsBrewery()
        {
            // Arrange
            int breweryId = 1;
            var expectedBrewery = new Brewery { Id = breweryId, Name = "Test Brewery" };

            _breweryRepositoryMock
                .Setup(repo => repo.GetByIdAsync(breweryId))
                .ReturnsAsync(expectedBrewery);

            // Act
            var result = await _breweryService.GetBreweryByIdAsync(breweryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(breweryId, result.Id);
            Assert.Equal("Test Brewery", result.Name);
        }

        [Fact]
        public async Task GetBreweryByIdAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            int breweryId = 1;
            _breweryRepositoryMock
                .Setup(repo => repo.GetByIdAsync(breweryId))
                .ReturnsAsync((Brewery)null);

            // Act
            var result = await _breweryService.GetBreweryByIdAsync(breweryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllBreweriesAsync_ReturnsAllBreweries()
        {
            // Arrange
            var breweries = new List<Brewery>
            {
                new Brewery { Id = 1, Name = "Brewery 1" },
                new Brewery { Id = 2, Name = "Brewery 2" }
            };

            _breweryRepositoryMock
                .Setup(repo => repo.GetAllBreweriesAsync())
                .ReturnsAsync(breweries);

            // Act
            var result = await _breweryService.GetAllBreweriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<Brewery>)result).Count);
        }

        [Fact]
        public async Task AddBreweryAsync_ValidBrewery_ReturnsAddedBrewery()
        {
            // Arrange
            var newBrewery = new Brewery { Id = 1, Name = "New Brewery" };
            _breweryRepositoryMock
                .Setup(repo => repo.AddBreweryAsync(newBrewery))
                .ReturnsAsync(newBrewery);

            // Act
            var result = await _breweryService.AddBreweryAsync(newBrewery);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newBrewery.Name, result.Name);
        }

        [Fact]
        public async Task UpdateBreweryAsync_ValidId_UpdatesBrewery()
        {
            // Arrange
            int breweryId = 1;
            var existingBrewery = new Brewery { Id = breweryId, Name = "Old Brewery" };
            var updatedBrewery = new Brewery { Id = breweryId, Name = "Updated Brewery" };

            _breweryRepositoryMock
                .Setup(repo => repo.GetByIdAsync(breweryId))
                .ReturnsAsync(existingBrewery);

            // Act
            await _breweryService.UpdateBreweryAsync(breweryId, updatedBrewery);

            // Assert
            Assert.Equal(updatedBrewery.Name, existingBrewery.Name);
            _breweryRepositoryMock.Verify(repo => repo.UpdateBreweryAsync(existingBrewery), Times.Once);
        }

        [Fact]
        public async Task UpdateBreweryAsync_InvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            int breweryId = 1;
            var updatedBrewery = new Brewery { Id = breweryId, Name = "Updated Brewery" };
            _breweryRepositoryMock
                .Setup(repo => repo.GetByIdAsync(breweryId))
                .ReturnsAsync((Brewery)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _breweryService.UpdateBreweryAsync(breweryId, updatedBrewery));
        }

        [Fact]
        public async Task UpdateBreweryAsync_IdMismatch_ThrowsArgumentException()
        {
            // Arrange
            int breweryId = 1;
            var updatedBrewery = new Brewery { Id = 2, Name = "Updated Brewery" };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _breweryService.UpdateBreweryAsync(breweryId, updatedBrewery));
        }

        [Fact]
        public async Task BreweryExistsAsync_ValidId_ReturnsTrue()
        {
            // Arrange
            int breweryId = 1;
            _breweryRepositoryMock
                .Setup(repo => repo.BreweryExistsAsync(breweryId))
                .ReturnsAsync(true);

            // Act
            var result = await _breweryService.BreweryExistsAsync(breweryId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task BreweryExistsAsync_InvalidId_ReturnsFalse()
        {
            // Arrange
            int breweryId = 1;
            _breweryRepositoryMock
                .Setup(repo => repo.BreweryExistsAsync(breweryId))
                .ReturnsAsync(false);

            // Act
            var result = await _breweryService.BreweryExistsAsync(breweryId);

            // Assert
            Assert.False(result);
        }
    }
}
