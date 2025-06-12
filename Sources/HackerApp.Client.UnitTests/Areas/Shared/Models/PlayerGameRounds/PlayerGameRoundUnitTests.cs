using FluentAssertions;
using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Models.PlayerGameRounds;
using Xunit;

namespace HackerApp.Client.UnitTests.Areas.Shared.Models.PlayerGameRounds
{
    public class PlayerGameRoundUnitTests
    {
        private readonly PlayerPenalty _penalty;
        private readonly Player _player;
        private readonly GameRoundPlayerResult _result;

        public PlayerGameRoundUnitTests()
        {
            _player = new Player("TestPlayer");
            _result = new GameRoundPlayerResult { ResultType = GameRoundPlayerResultType.None };
            _penalty = new PlayerPenalty("TestPlayer", 0, false);
        }

        [Fact]
        public void Calculating_GehacktGewonnen_Alleine()
        {
            // Arrange
            _result.ResultType = GameRoundPlayerResultType.HackedGewonnen;

            var sut = new PlayerGameRound(_player, _result, _penalty);

            var roundPot = new RoundPot(100);

            // Act
            var actualLossProfit = sut.CalculateLossProfit(roundPot, 1);

            // Assert
            actualLossProfit.Should().Be(100);
        }

        [Fact]
        public void Calculating_GehacktGewonnen_MitAnderen()
        {
            // Arrange
            _result.ResultType = GameRoundPlayerResultType.HackedGewonnen;
            var sut = new PlayerGameRound(_player, _result, _penalty);
            var roundPot = new RoundPot(100);

            // Act
            var actualLossProfit = sut.CalculateLossProfit(roundPot, 3);

            // Assert
            actualLossProfit.Should().Be(66.67);
        }

        [Fact]
        public void Calculating_MitGewonnen_Alleine()
        {
            // Arrange
            _result.ResultType = GameRoundPlayerResultType.MitgegangenGewonnen;

            var sut = new PlayerGameRound(_player, _result, _penalty);

            var roundPot = new RoundPot(100);

            // Act
            var actualLossProfit = sut.CalculateLossProfit(roundPot, 1);

            // Assert
            actualLossProfit.Should().Be(100);
        }

        [Fact]
        public void Calculating_MitGewonnen_MitAnderen()
        {
            // Arrange
            _result.ResultType = GameRoundPlayerResultType.MitgegangenGewonnen;

            var sut = new PlayerGameRound(_player, _result, _penalty);

            var roundPot = new RoundPot(100);

            // Act
            var actualLossProfit = sut.CalculateLossProfit(roundPot, 4);

            // Assert
            actualLossProfit.Should().Be(11.11);
        }
    }
}