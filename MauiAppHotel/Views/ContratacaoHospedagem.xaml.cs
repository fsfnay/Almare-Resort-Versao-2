namespace MauiAppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    private readonly Dictionary<string, (int Capacidade, decimal Diaria)> suites = new()
    {
        { "Suíte Standard", (2, 250m) },
        { "Suíte Deluxe", (3, 400m) },
        { "Suíte Premium", (4, 550m) }
    };

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        dtpck_checkin.Date = DateTime.Today;
        dtpck_checkout.Date = DateTime.Today.AddDays(1);

        AtualizarHospedes();
    }

    private int TotalHospedes =>
        (int)stp_adultos.Value + (int)stp_criancas.Value;

    private void OnHospedesChanged(object sender, ValueChangedEventArgs e)
    {
        AtualizarHospedes();
    }

    private void AtualizarHospedes()
    {
        int total = TotalHospedes;

        lbl_total_hospedes.Text =
            $"Total: {total} {(total == 1 ? "hóspede" : "hóspedes")}";

        string quartoAtual = pck_quarto.SelectedItem as string ?? "";

        var disponiveis = suites
            .Where(s => s.Value.Capacidade >= total)
            .Select(s => s.Key)
            .ToList();

        pck_quarto.ItemsSource = disponiveis;

        if (disponiveis.Contains(quartoAtual))
            pck_quarto.SelectedItem = quartoAtual;
        else
            pck_quarto.SelectedItem = null;

        AtualizarDescricao();
    }

    private void OnQuartoChanged(object sender, EventArgs e)
    {
        AtualizarDescricao();
    }

    private void AtualizarDescricao()
    {
        if (pck_quarto.SelectedItem is not string quarto)
        {
            lbl_descricao_quarto.Text =
                "Selecione uma acomodação para ver os detalhes.";

            pck_camas.IsVisible = false;
            return;
        }

        var dados = suites[quarto];

        lbl_descricao_quarto.Text =
            $"Capacidade: até {dados.Capacidade} hóspedes\n" +
            $"Diária: R$ {dados.Diaria:N2}";

        if (quarto == "Suíte Standard")
        {
            lbl_descricao_quarto.Text +=
                "\n1 cama de casal.";
            pck_camas.IsVisible = false;
        }
        else if (quarto == "Suíte Deluxe")
        {
            lbl_descricao_quarto.Text +=
                "\n1 cama de casal e 1 cama de solteiro.";
            pck_camas.IsVisible = false;
        }
        else
        {
            pck_camas.ItemsSource = new List<string>
            {
                "1 cama de casal + 2 camas de solteiro",
                "2 camas de casal"
            };

            pck_camas.SelectedIndex = -1;
            pck_camas.IsVisible = true;

            lbl_descricao_quarto.Text +=
                "\nEscolha a configuração das camas abaixo.";
        }
    }

    private async void OnAvancarClicked(object sender, EventArgs e)
    {
        if (TotalHospedes < 1)
        {
            await DisplayAlert("Atenção",
                "Informe pelo menos um hóspede.", "OK");
            return;
        }

        if (pck_quarto.SelectedItem is not string quarto)
        {
            await DisplayAlert("Atenção",
                "Selecione uma acomodação disponível.", "OK");
            return;
        }

        if (quarto == "Suíte Premium" &&
            pck_camas.SelectedItem is not string)
        {
            await DisplayAlert("Atenção",
                "Escolha a configuração das camas.", "OK");
            return;
        }

        if (dtpck_checkout.Date <= dtpck_checkin.Date)
        {
            await DisplayAlert("Atenção",
                "O check-out deve ser posterior ao check-in.", "OK");
            return;
        }

        int noites = (dtpck_checkout.Date -
                      dtpck_checkin.Date).Days;

        var dados = suites[quarto];

        if (TotalHospedes > dados.Capacidade)
        {
            await DisplayAlert("Atenção",
                "A acomodação não comporta essa quantidade de hóspedes.",
                "OK");
            return;
        }

        string camas = quarto switch
        {
            "Suíte Standard" => "1 cama de casal",
            "Suíte Deluxe" => "1 cama de casal + 1 cama de solteiro",
            _ => pck_camas.SelectedItem?.ToString() ?? ""
        };

        await Navigation.PushAsync(new HospedagemContratada(
            (int)stp_adultos.Value,
            (int)stp_criancas.Value,
            quarto,
            camas,
            dtpck_checkin.Date,
            dtpck_checkout.Date,
            noites,
            dados.Diaria
        ));
    }
}
