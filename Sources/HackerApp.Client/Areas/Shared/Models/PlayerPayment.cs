using HackerApp.Client.Infrastructure;

namespace HackerApp.Client.Areas.Shared.Models
{
    public record PlayerPayment(Player From, Player To, double Amount)
    {
        public string PaymentDescription => $"{From.Name} -> {To.Name}: {Amount.ToFranken()}";
    }
}