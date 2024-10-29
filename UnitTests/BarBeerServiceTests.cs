using BeerInventory.Data.Repositories;
using BeerInventory.Models;
using BeerInventory.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BeerInventory.Tests.Services
{
    public class BarBeerServiceTests
    {
        private readonly Mock<IBarBeerRepository> _barBeerRepositoryMock;
        private readonly BarBeerService _barBeerService;

        public BarBeerServiceTests()
        {
            _barBeerRepositoryMock = new Mock<IBarBeerRepository>();
            _barBeerService = new BarBeerService(_barBeerRepositoryMock.Object);
        }

        [Fact]
        public async Task GetBarWithBeersAsync_BarExists_ReturnsBarWithBeers()
        {
            // Arrange
            int barId = 1;
            var bar = new Bar
            {
                Id = barId,
                Name = "Test Bar",
                Beers = new List<Beer> { new Beer { Id = 1, Name = "Test Beer", PercentageAlcoholByVolume = 5.0M } }
            };
            _barBeerRepositoryMock.Setup(repo => repo.GetBarWithBeersAsync(barId))
                                  .ReturnsAsync(bar);

            // Act
            var result = await _barBeerService.GetBarWithBeersAsync(barId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Bar", result.Name);
            Assert.Single(result.Beers);
            Assert.Equal("Test Beer", result.Beers.First().Name);
        }

        [Fact]
        public async Task GetBarWithBeersAsync_BarDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            int barId = 1;
            _barBeerRepositoryMock.Setup(repo => repo.GetBarWithBeersAsync(barId))
                                  .ReturnsAsync((Bar)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _barBeerService.GetBarWithBeersAsync(barId));
        }

        [Fact]
        public async Task GetAllBarsWithBeersAsync_ReturnsAllBarsWithBeers()
        {
            // Arrange
            var bars = new List<Bar>
            {
                new Bar { Id = 1, Name = "Bar 1", Beers = new List<Beer> { new Beer { Id = 1, Name = "Beer 1", PercentageAlcoholByVolume = 5.0M } } },
                new Bar { Id = 2, Name = "Bar 2", Beers = new List<Beer> { new Beer { Id = 2, Name = "Beer 2", PercentageAlcoholByVolume = 6.0M } } }
            };
            _barBeerRepositoryMock.Setup(repo => repo.GetAllBarsWithBeersAsync())
                                  .ReturnsAsync(bars);

            // Act
            var result = await _barBeerService.GetAllBarsWithBeersAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Bar 1", result.First().Name);
            Assert.Equal("Beer 1", result.First().Beers.First().Name);
        }

        [Fact]
        public async Task LinkBeerToBarAsync_BarAndBeerExist_AddsBeerToBar()
        {
            // Arrange
            int barId = 1;
            int beerId = 1;
            _barBeerRepositoryMock.Setup(repo => repo.LinkBeerToBarAsync(barId, beerId))
                                  .ReturnsAsync(true);

            // Act
            await _barBeerService.LinkBeerToBarAsync(barId, beerId);

            // Assert
            _barBeerRepositoryMock.Verify(repo => repo.LinkBeerToBarAsync(barId, beerId), Times.Once);
        }

        [Fact]
        public async Task LinkBeerToBarAsync_BarOrBeerDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            int barId = 1;
            int beerId = 2;
            _barBeerRepositoryMock.Setup(repo => repo.LinkBeerToBarAsync(barId, beerId))
                                  .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _barBeerService.LinkBeerToBarAsync(barId, beerId));
        }
    }
}
