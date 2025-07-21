using TesteAPI.Services;

namespace TesteAPI
{
    public partial class MainPage : ContentPage
    {
        private readonly ClienteService _clienteService;

        public MainPage()
        {
            InitializeComponent();
            _clienteService = new ClienteService();
            CarregarClientes();
        }


        private async void CarregarClientes(string Codigo = null)
        {

            try
            {
                var clientes = await _clienteService.GetClientesAsync(Codigo);
                ClientesListView.ItemsSource = clientes;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao carregar clientes: {ex.Message}", "OK");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            string codigo = Codigo.Text?.Trim();

            if (string.IsNullOrEmpty(codigo))
            {
                DisplayAlert("Aviso", "O valor não pode ser nulo!", "OK");
                return;
            }
            else
            {
                if (!int.TryParse(codigo, out int numero))
                {
                    DisplayAlert("Aviso", "O código deve ser número", "OK");
                    return;
                }
                else
                {
                    if (numero > 10 || numero < 1)
                    {
                        DisplayAlert("Aviso", "Usuario não existe, digite de 1 a 10", "OK");
                        return;
                    }
                }
            }

            CarregarClientes(Codigo.Text);

        }
    }
}