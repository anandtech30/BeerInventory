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
    public class BarServiceTests
    {
        private readonly Mock<IBarRepository> _barRepositoryMock;
        private readonly BarService _barService;

        public BarServiceTests()
        {
            _barRepositoryMock = new Mock<IBarRepository>();
            _barService = new BarService(_barRepositoryMock.Object);
        }

        [Fact]
        public async Task GetBarByIdAsync_ValidId_ReturnsBar()
        {
            // Arrange
            int barId = 1;
            var expectedBar = new Bar { Id = barId, Name = "Test Bar", Address = "123 Test St" };
            _barRepositoryMock.Setup(repo => repo.GetByIdAsync(barId))
                               .ReturnsAsync(expectedBar);

            // Act
            var result = await _barService.GetBarByIdAsync(barId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(barId, result.Id);
            Assert.Equal("Test Bar", result.Name);
        }

        [Fact]
        public async Task GetBarByIdAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            int barId = 1;
            _barRepositoryMock.Setup(repo => repo.GetByIdAsync(barId))
                               .ReturnsAsync((Bar)null);

            // Act
            var result = await _barService.GetBarByIdAsync(barId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllBarsAsync_ReturnsListOfBars()
        {
            // Arrange
            var bars = new List<Bar>
            {
                new Bar { Id = 1, Name = "Bar 1", Address = "Address 1" },
                new Bar { Id = 2, Name = "Bar 2", Address = "Address 2" }
            };
            _barRepositoryMock.Setup(repo => repo.GetAllBarsAsync())
                               .ReturnsAsync(bars);

            // Act
            var result = await _barService.GetAllBarsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddBarAsync_ValidBar_ReturnsBar()
        {
            // Arrange
            var newBar = new Bar { Name = "New Bar", Address = "456 New St" };
            _barRepositoryMock.Setup(repo => repo.AddBarAsync(newBar))
                               .ReturnsAsync(new Bar { Id = 3, Name = newBar.Name, Address = newBar.Address });

            // Act
            var result = await _barService.AddBarAsync(newBar);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Bar", result.Name);
            Assert.Equal("456 New St", result.Address);
            Assert.Equal(3, result.Id);
        }

        [Fact]
        public async Task UpdateBarAsync_BarExists_UpdatesBar()
        {
            // Arrange
            int barId = 1;
            var existingBar = new Bar { Id = barId, Name = "Old Bar", Address = "123 Old St" };
            var updatedBar = new Bar { Id = barId, Name = "Updated Bar", Address = "789 Updated St" };

            _barRepositoryMock.Setup(repo => repo.GetByIdAsync(barId))
                               .ReturnsAsync(existingBar);
            _barRepositoryMock.Setup(repo => repo.UpdateBarAsync(existingBar))
                               .Returns(Task.CompletedTask);

            // Act
            await _barService.UpdateBarAsync(barId, updatedBar);

            // Assert
            Assert.Equal(updatedBar.Name, existingBar.Name);
            Assert.Equal(updatedBar.Address, existingBar.Address);
            _barRepositoryMock.Verify(repo => repo.UpdateBarAsync(existingBar), Times.Once);
        }

        [Fact]
        public async Task UpdateBarAsync_BarDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            int barId = 1;
            var updatedBar = new Bar { Id = barId, Name = "Updated Bar", Address = "789 Updated St" };
            _barRepositoryMock.Setup(repo => repo.GetByIdAsync(barId))
                               .ReturnsAsync((Bar)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _barService.UpdateBarAsync(barId, updatedBar));
        }

        [Fact]
        public async Task UpdateBarAsync_IdMismatch_ThrowsArgumentException()
        {
            // Arrange
            int barId = 1;
            var updatedBar = new Bar { Id = 2, Name = "Updated Bar", Address = "789 Updated St" };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _barService.UpdateBarAsync(barId, updatedBar));
        }

        [Fact]
        public async Task BarExistsAsync_BarExists_ReturnsTrue()
        {
            // Arrange
            int barId = 1;
            _barRepositoryMock.Setup(repo => repo.BarExistsAsync(barId))
                               .ReturnsAsync(true);

            // Act
            var result = await _barService.BarExistsAsync(barId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task BarExistsAsync_BarDoesNotExist_ReturnsFalse()
        {
            // Arrange
            int barId = 1;
            _barRepositoryMock.Setup(repo => repo.BarExistsAsync(barId))
                               .ReturnsAsync(false);

            // Act
            var result = await _barService.BarExistsAsync(barId);

            // Assert
            Assert.False(result);
        }
    }
}
