namespace MauiAppHotel.Views;

public partial class HospedagemContratada : ContentPage
{
    private readonly int adultos;
    private readonly int criancas;
    private readonly string quarto;
    private readonly string camas;
    private readonly DateTime checkin;
    private readonly DateTime checkout;
    private readonly int noites;
    private readonly decimal diaria;
    private readonly decimal total;

    public HospedagemContratada(
        int adultos,
        int criancas,
        string quarto,
        string camas,
        DateTime checkin,
        DateTime checkout,
        int noites,
        decimal diaria)
    {
        InitializeComponent();

        this.adultos = adultos;
        this.criancas = criancas;
        this.quarto = quarto;
        this.camas = camas;
        this.checkin = checkin;
        this.checkout = checkout;
        this.noites = noites;
        this.diaria = diaria;

        total = diaria * noites;

        ExibirResumo();
    }

    private string TextoHospedes()
    {
        return $"{adultos} adultos e {criancas} crianças";
    }

    private string TextoPeriodo()
    {
        return $"{checkin:dd/MM/yyyy} até {checkout:dd/MM/yyyy}";
    }

    private void ExibirResumo()
    {
        lbl_quarto.Text = quarto;
        lbl_camas.Text = camas;
        lbl_hospedes.Text = TextoHospedes();
        lbl_periodo.Text = TextoPeriodo();

        lbl_noites.Text =
            $"{noites} {(noites == 1 ? "diária" : "diárias")}";

        lbl_diaria.Text = $"Valor da diária: R$ {diaria:N2}";
        lbl_total.Text = $"R$ {total:N2}";
    }

    private async void OnConfirmarClicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert(
            "Confirmar reserva",
            "Deseja confirmar esta hospedagem?",
            "Sim, confirmar",
            "Revisar");

        if (!confirmar)
            return;

        // Identificador demonstrativo da reserva.
        string codigo = Guid.NewGuid().ToString("N")
            .Substring(0, 8).ToUpper();

        lbl_codigo.Text = $"Código da reserva: {codigo}";
        lbl_quartoFinal.Text = quarto;
        lbl_camasFinal.Text = camas;
        lbl_hospedesFinal.Text = TextoHospedes();
        lbl_periodoFinal.Text = TextoPeriodo();

        lbl_noitesFinal.Text =
            $"{noites} {(noites == 1 ? "diária" : "diárias")}";

        lbl_totalFinal.Text = $"R$ {total:N2}";

        layoutResumo.IsVisible = false;
        layoutConfirmacao.IsVisible = true;

        Title = "Reserva confirmada";
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnInicioClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
