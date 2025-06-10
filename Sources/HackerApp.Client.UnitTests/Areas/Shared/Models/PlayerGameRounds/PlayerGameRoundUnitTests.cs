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
            var actualLossProfit = sut.CalculateLossProfit(roundPot, 0.50, 1);

            // Assert
            actualLossProfit.Should().Be(100);
        }

        [Fact]
        public void Calculating_GehacktGewonnen_MinPot()
        {
            // Arrange
            _result.ResultType = GameRoundPlayerResultType.HackedGewonnen;
            var sut = new PlayerGameRound(_player, _result, _penalty);
            var roundPot = new RoundPot(2);

            // Act
            var actualLossProfit = sut.CalculateLossProfit(roundPot, 0.50, 3);

            // Assert
            actualLossProfit.Should().Be(1);
        }

        [Fact]
        public void Calculating_GehacktGewonnen_MitAnderen()
        {
            // Arrange
            _result.ResultType = GameRoundPlayerResultType.HackedGewonnen;
            var sut = new PlayerGameRound(_player, _result, _penalty);
            var roundPot = new RoundPot(100);

            // Act
            var actualLossProfit = sut.CalculateLossProfit(roundPot, 0.50, 3);

            // Assert
            actualLossProfit.Should().Be(67.00);
        }

        [Fact]
        public void Calculating_MitGewonnen_Alleine()
        {
            // Arrange
            _result.ResultType = GameRoundPlayerResultType.MitgegangenGewonnen;

            var sut = new PlayerGameRound(_player, _result, _penalty);

            var roundPot = new RoundPot(100);

            // Act
            var actualLossProfit = sut.CalculateLossProfit(roundPot, 0.50, 2);

            // Assert
            actualLossProfit.Should().Be(33.50);
        }

        [Fact]
        public void Calculating_MitGewonnen_MinPot()
        {
            // Arrange
            _result.ResultType = GameRoundPlayerResultType.MitgegangenGewonnen;
            var sut = new PlayerGameRound(_player, _result, _penalty);
            var roundPot = new RoundPot(2);

            // Act
            var actualLossProfit = sut.CalculateLossProfit(roundPot, 0.50, 3);

            // Assert
            actualLossProfit.Should().Be(0.50);
        }

        [Fact]
        public void Calculating_MitGewonnen_MitAnderen()
        {
            // Arrange
            _result.ResultType = GameRoundPlayerResultType.MitgegangenGewonnen;

            var sut = new PlayerGameRound(_player, _result, _penalty);

            var roundPot = new RoundPot(100);

            // Act
            var actualLossProfit = sut.CalculateLossProfit(roundPot, 0.50, 4);

            // Assert
            actualLossProfit.Should().Be(11.50);
        }
    }
}