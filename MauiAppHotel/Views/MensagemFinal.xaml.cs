namespace MauiAppHotel.Views;

public partial class MensagemFinal : ContentPage
{
    public MensagemFinal()
    {
        InitializeComponent();
    }

    private async void OnInicioClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
