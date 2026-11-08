namespace MauiAppHotel
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnContratarClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new Views.ContratacaoHospedagem());
        }

        private async void OnSobreClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new Views.Sobre());
        }
    }
}
