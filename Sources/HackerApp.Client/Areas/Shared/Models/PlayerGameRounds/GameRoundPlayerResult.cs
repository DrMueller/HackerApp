﻿namespace HackerApp.Client.Areas.Shared.Models.PlayerGameRounds
{
    public class GameRoundPlayerResult
    {
        public bool HasWon =>
            ResultType == GameRoundPlayerResultType.HackedGewonnen ||
            ResultType == GameRoundPlayerResultType.MitgegangenGewonnen;

        public GameRoundPlayerResultType ResultType { get; set; } = GameRoundPlayerResultType.None;
    }
}