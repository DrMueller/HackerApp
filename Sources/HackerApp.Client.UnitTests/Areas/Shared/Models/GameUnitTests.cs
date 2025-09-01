using FluentAssertions;
using HackerApp.Client.Areas.Shared.Models;
using Xunit;

namespace HackerApp.Client.UnitTests.Areas.Shared.Models
{
    public class GameUnitTests
    {
        [Fact]
        public void DeletingLastRound_DeletesLastAddedRound()
        {
            // Arrange
            var sut = new Game([]);

            sut.AddNewRound(1, []);
            sut.AddNewRound(2, []);
            sut.AddNewRound(3, []);

            // Act
            sut.DeleteLastRound();

            // Assert
            sut.GameRounds.Should().HaveCount(2);
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            sut.GameRounds.Should().NotContain(f => f.RoundEinsatz == 3);
        }

        [Fact]
        public void DeletingLastRound_WithoutExistingRounds_DoesntDeleteRounds()
        {
            // Arrange
            var sut = new Game([]);

            // Act
            sut.DeleteLastRound();

            // Assert
            sut.GameRounds.Should().HaveCount(0);
        }
    }
}