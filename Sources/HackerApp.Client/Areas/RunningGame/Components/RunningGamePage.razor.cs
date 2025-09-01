using BlazorBootstrap;
using HackerApp.Client.Areas.RunningGame.Dtos;
using HackerApp.Client.Areas.Shared.Models;
using HackerApp.Client.Areas.Shared.Models.Pgr;
using HackerApp.Client.Infrastructure.HttpRequests;
using HackerApp.Client.Infrastructure.State.Dtos;
using HackerApp.Client.Infrastructure.State.Services;
using HackerApp.Client.Shared.Modal;
using Microsoft.AspNetCore.Components;

namespace HackerApp.Client.Areas.RunningGame.Components
{
    public partial class RunningGamePage
    {
        public const string Path = "/games/run";

        private readonly IList<PlayerPenalty> _playerPenalties = new List<PlayerPenalty>();

        [Inject]
        public required IGameState GameState { get; set; }

        [Inject]
        public required IHttpClientProxy HttpClientProxy { get; set; }

        [Parameter]
        public required IReadOnlyCollection<Player> Players { get; set; }

        private double Einsatz { get; set; } = 0.50;

        private Game? Game { get; set; }
        private GameAnalysis GameAnalysisRef { get; set; } = null!;
        private GenericModal ModalRef { get; set; } = null!;
        private PlayerPayouts PlayerPayoutsRef { get; set; } = null!;
        private Button ShotsButtonRef { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            Game = await GameState.LoadAsync();
        }

        private async Task AddNewRoundAsync()
        {
            Game!.AddNewRound(Einsatz, _playerPenalties.ToList());
            _playerPenalties.Clear();
            await GameState.PersistAsync(Game);
        }

        private void AddPlayerPenalty(PlayerPenalty pen)
        {
            if (pen.ApplyPenaltyNextRound)
            {
                var existingPenalty = _playerPenalties.SingleOrDefault(f => f.PlayerName == pen.PlayerName);
                if (existingPenalty != null)
                {
                    _playerPenalties.Remove(existingPenalty);
                }

                _playerPenalties.Add(pen);
            }
            else
            {
                Game!.CurrentRound.AddPenalty(pen);
            }
        }

        private async Task CalculatePayoutAsync()
        {
            var payouts = Game!.CalculatePayouts();
            await PlayerPayoutsRef.ShowAsync(payouts);
        }

        private async Task DeleteLastRoundAsync()
        {
            var confirmed = await ModalRef.ShowConfirmationAsync("Löschen?", "Löschen?", "Ja", "Nein");
            if (confirmed)
            {
                Game!.DeleteLastRound();
                await GameState.PersistAsync(Game!);
            }
        }

        private async Task ShowGameAnalysisAsync()
        {
            ShotsButtonRef.ShowLoading();
            var game = await GameState.LoadDtoAsync();
            var analysisResult = await HttpClientProxy.PostAsync<GameAnalysisResultDto, GameDto>("api/game/analyze", game);
            ShotsButtonRef.HideLoading();
            await GameAnalysisRef.ShowAsync(analysisResult.Text);
        }
    }
}