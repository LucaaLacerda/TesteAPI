
using TesteAPI.Models;
using System.Text.Json;


namespace TesteAPI.Services
{
    public class ClienteService
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "https://jsonplaceholder.typicode.com/users"; 

        public ClienteService()
        {
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromMinutes(10)
            };
        }

        public async Task<List<Cliente>> GetClientesAsync(string Codigo=null)
        {
            try
            {
                //NetworkAccess accessType = Connectivity.Current.NetworkAccess;

                //if (accessType == NetworkAccess.Internet)
                //{
                //    // Connection to internet is available
                //}

                if (Codigo == null)
                {

                    var response = await _httpClient.GetAsync(baseUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var clientes = JsonSerializer.Deserialize<List<Cliente>>(json, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        return clientes ?? new List<Cliente>();
                    }
                }
                else
                {
                    var response = await _httpClient.GetAsync(baseUrl + "/" + Codigo);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var cliente = JsonSerializer.Deserialize<Cliente>(json, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                       var clientes =  new List<Cliente>();
                        clientes.Add(cliente);
                        return clientes;
                    }
                }

                return new List<Cliente>();

            }
            catch (Exception ex)
            {
            throw;
            }
        }
    }
}
