using JetBrains.Annotations;

namespace HackerApp.Infrastructure.SemKer.Models
{
    [PublicAPI]
    public record Spielrunde(
        int RundenNummer,
        double RundenSpieleinsatz,
        IReadOnlyCollection<SpielerSpielrunde> SpielerSpielrunden);

    [PublicAPI]
    public record SpielerSpielrunde(
        string SpielerName,
        double SpielerGewonnenOderVerlorenMenge,
        double SpielerBusseMenge,
        bool SpielerHatGehackt,
        bool SpielerIstMitgegangen,
        bool SpielerHatNichtMitgespielt);

    [PublicAPI]
    public record SpielerStatistik(
        string SpielerName,
        double GesamtGewonnenVerloren,
        double GesamteBussen);

    [PublicAPI]
    public record GameToAnalyze(IReadOnlyCollection<SpielerStatistik> Statistik, IReadOnlyCollection<Spielrunde> Runden);
}