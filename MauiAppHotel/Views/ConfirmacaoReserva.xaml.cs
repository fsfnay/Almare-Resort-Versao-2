using System.Globalization;

namespace MauiAppHotel.Views;

public partial class ConfirmacaoReserva : ContentPage
{
    private readonly CultureInfo cultura =
        new CultureInfo("pt-BR");

    public ConfirmacaoReserva(
        string suite,
        string camas,
        int adultos,
        int criancas,
        DateTime checkin,
        DateTime checkout,
        int noites,
        decimal diaria,
        decimal total)
    {
        InitializeComponent();

        lblSuite.Text = $"Acomodação: {suite}";
        lblCamas.Text = $"Configuração: {camas}";

        lblHospedes.Text =
            $"Hóspedes: {adultos} adultos e {criancas} crianças";

        lblCheckin.Text = $"Check-in: {checkin:dd/MM/yyyy}";
        lblCheckout.Text = $"Check-out: {checkout:dd/MM/yyyy}";

        lblNoites.Text = $"Quantidade de noites: {noites}";

        lblDiaria.Text =
            $"Valor da diária: R$ {diaria.ToString("N2", cultura)}";

        lblTotal.Text =
            $"Total: R$ {total.ToString("N2", cultura)}";
    }

    private async void OnConfirmarClicked(
        object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert(
            "Confirmar reserva",
            "Deseja realmente confirmar esta hospedagem?",
            "Sim",
            "Não"
        );

        if (confirmar)
        {
            await DisplayAlert(
                "Reserva confirmada!",
                "Sua reserva no Almare foi realizada com sucesso!",
                "OK"
            );

            btnConfirmar.Text = "RESERVA CONFIRMADA";
            btnConfirmar.IsEnabled = false;
        }
    }

    private async void OnVoltarClicked(
        object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
