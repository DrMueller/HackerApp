using FluentAssertions;
using HackerApp.Client.Areas.Shared.Models;
using Xunit;

namespace HackerApp.Client.UnitTests.Areas.Shared.Models
{
    public class PlayerLossProfitCalculationsUnitTests
    {
        [Fact]
        public void CalculatePayments_CalculatesPayments1()
        {
            // Arrange
            var profitLosses = new List<PlayerLossProfitCalculation>
            {
                new(new Player("Player1"), 10),
                new(new Player("Player2"), -10),
                new(new Player("Player3"), 20),
                new(new Player("Player4"), -20)
            };

            var sut = new PlayerLossProfitCalculations(profitLosses);

            // Act
            var actualPayments = sut.CalculatePayments();

            // Assert
            actualPayments.Should().ContainSingle(p => p.From.Name == "Player2" && p.To.Name == "Player1" && Math.Abs(p.Amount - 10) == 0);
            actualPayments.Should().ContainSingle(p => p.From.Name == "Player4" && p.To.Name == "Player3" && Math.Abs(p.Amount - 20) == 0);
        }

        [Fact]
        public void CalculatePayments_CalculatesPayments2()
        {
            // Arrange
            var profitLosses = new List<PlayerLossProfitCalculation>
            {
                new(new Player("Player1"), -100),
                new(new Player("Player2"), 30.5),
                new(new Player("Player3"), 20.5),
                new(new Player("Player4"), 49)
            };

            var sut = new PlayerLossProfitCalculations(profitLosses);

            // Act
            var actualPayments = sut.CalculatePayments();

            // Assert
            actualPayments.Should().ContainSingle(p => p.From.Name == "Player1" && p.To.Name == "Player2" && Math.Abs(p.Amount - 30.5) == 0);
            actualPayments.Should().ContainSingle(p => p.From.Name == "Player1" && p.To.Name == "Player3" && Math.Abs(p.Amount - 20.5) == 0);
            actualPayments.Should().ContainSingle(p => p.From.Name == "Player1" && p.To.Name == "Player4" && Math.Abs(p.Amount - 49) == 0);
        }

        [Fact]
        public void CalculatePayments_CalculatesPayments3()
        {
            // Arrange
            var profitLosses = new List<PlayerLossProfitCalculation>
            {
                new(new Player("Player1"), -20),
                new(new Player("Player2"), 19.5),
                new(new Player("Player3"), 0.5)
            };

            var sut = new PlayerLossProfitCalculations(profitLosses);

            // Act
            var actualPayments = sut.CalculatePayments();

            // Assert
            actualPayments.Should().ContainSingle(p => p.From.Name == "Player1" && p.To.Name == "Player2" && Math.Abs(p.Amount - 19.5) == 0);
            actualPayments.Should().ContainSingle(p => p.From.Name == "Player1" && p.To.Name == "Player3" && Math.Abs(p.Amount - 0.5) == 0);
        }

        [Fact]
        public void CalculatePayments_WithoutProfitLoss_ReturnsEmptyList()
        {
            // Arrange
            var sut = new PlayerLossProfitCalculations([]);

            // Act
            var actualPayments = sut.CalculatePayments();

            // Assert
            actualPayments.Should().BeEmpty();
        }
    }
}