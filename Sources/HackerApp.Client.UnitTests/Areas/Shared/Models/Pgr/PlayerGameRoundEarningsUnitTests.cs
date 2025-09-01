using FluentAssertions;
using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Models.Pgr;
using Xunit;

namespace HackerApp.Client.UnitTests.Areas.Shared.Models.Pgr
{
    public partial class PlayerGameRoundEarningsUnitTests
    {
        [Fact]
        public void Calculating_GehacktGewonnen_Alleine()
        {
            // Arrange
            var roundPot = new RoundPot(90);

            var pgr1 = CreatePgr(GameRoundPlayerResultType.HackedGewonnen);

            var pgr = new PlayerGameRounds([pgr1]);

            var sut = new PlayerGameRoundEarnings(
                roundPot,
                pgr,
                pgr1.Player);

            // Act
            var actualLossProfit = sut.CalculateLossProfit();

            // Assert
            actualLossProfit.Should().Be(roundPot.Value);
        }

        [Fact]
        public void Calculating_MitGewonnen_Alleine()
        {
            // Arrange
            var roundPot = new RoundPot(90);

            var pgr2 = CreatePgr(GameRoundPlayerResultType.MitgegangenGewonnen);

            var pgr = new PlayerGameRounds([pgr2]);

            var sut = new PlayerGameRoundEarnings(
                roundPot,
                pgr,
                pgr2.Player);

            // Act
            var actualLossProfit = sut.CalculateLossProfit();

            // Assert
            actualLossProfit.Should().Be(roundPot.Value);
        }

        [Fact]
        public void Calculating_MitGewonnen_HackerGewonnen_AlleineMit()
        {
            // Arrange
            var roundPot = new RoundPot(90);

            var pgr1 = CreatePgr(GameRoundPlayerResultType.HackedGewonnen);
            var pgr2 = CreatePgr(GameRoundPlayerResultType.MitgegangenGewonnen);

            var pgr = new PlayerGameRounds([pgr1, pgr2]);

            var sut = new PlayerGameRoundEarnings(
                roundPot,
                pgr,
                pgr2.Player);

            // Act
            var actualLossProfit = sut.CalculateLossProfit();

            // Assert
            actualLossProfit.Should().Be(30);
        }

        [Fact]
        public void Calculating_MitGewonnen_HackerGewonnen_MitAnderenMit()
        {
            // Arrange
            var roundPot = new RoundPot(90);

            var pgr1 = CreatePgr(GameRoundPlayerResultType.HackedGewonnen);
            var pgr2 = CreatePgr(GameRoundPlayerResultType.MitgegangenGewonnen);
            var pgr3 = CreatePgr(GameRoundPlayerResultType.MitgegangenGewonnen);
            var pgr4 = CreatePgr(GameRoundPlayerResultType.MitgegangenGewonnen);

            var pgr = new PlayerGameRounds([pgr1, pgr2, pgr3, pgr4]);

            var sut = new PlayerGameRoundEarnings(
                roundPot,
                pgr,
                pgr2.Player);

            // Act
            var actualLossProfit = sut.CalculateLossProfit();

            // Assert
            actualLossProfit.Should().Be(10);
        }

        [Fact]
        public void Calculating_MitGewonnen_HackerVerloren_Alleine()
        {
        }

        [Fact]
        public void Calculating_MitGewonnen_HackerVerloren_MitAnderen()
        {
            // Arrange
            var roundPot = new RoundPot(90);

            var pgr1 = CreatePgr(GameRoundPlayerResultType.HackedVerloren);
            var pgr2 = CreatePgr(GameRoundPlayerResultType.MitgegangenGewonnen);
            var pgr3 = CreatePgr(GameRoundPlayerResultType.MitgegangenGewonnen);
            var pgr4 = CreatePgr(GameRoundPlayerResultType.MitgegangenGewonnen);

            var pgr = new PlayerGameRounds([pgr1, pgr2, pgr3, pgr4]);

            var sut = new PlayerGameRoundEarnings(
                roundPot,
                pgr,
                pgr2.Player);

            // Act
            var actualLossProfit = sut.CalculateLossProfit();

            // Assert
            actualLossProfit.Should().Be(30);
        }
    }
}