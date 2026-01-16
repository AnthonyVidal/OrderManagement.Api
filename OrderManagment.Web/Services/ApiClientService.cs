using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using OrderManagement.Web.Models;
using OrderManagement.Web.Models.ApiResponses;
using OrderManagement.Web.Exceptions;

namespace OrderManagement.Web.Services
{
    public class ApiClientService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiClientService(
            HttpClient client,
            IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
        }

        // ============================
        // Helpers
        // ============================
        private void AddJwtToken()
        {
            var token = _httpContextAccessor.HttpContext?
                .Session.GetString("JWT");

            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // ============================
        // Login
        // ============================
        public async Task<string?> LoginAsync(LoginViewModel model)
        {
            var response = await _client.PostAsJsonAsync("auth/login", model);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content
                .ReadFromJsonAsync<LoginResponse>(
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return result?.Token;
        }

        // ============================
        // Orders - Listar
        // ============================
        public async Task<List<OrderViewModel>> GetOrdersAsync()
        {
            AddJwtToken();

            var response = await _client.GetAsync("orders");

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<List<OrderViewModel>>() ?? [];
        }

        // ============================
        // Orders - Obtener por Id
        // ============================
        public async Task<OrderViewModel?> GetOrderByIdAsync(Guid id)
        {
            AddJwtToken();

            var response = await _client.GetAsync($"orders/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content
                .ReadFromJsonAsync<OrderViewModel>();
        }

        // ============================
        // Orders - Crear
        // ============================
        public async Task<Guid> CreateOrderAsync(CreateOrderViewModel model)
        {
            AddJwtToken();

            var response = await _client.PostAsJsonAsync("orders", model);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new BusinessException($"Error API CreateOrder: {error}");
            }

            var id = await response.Content.ReadFromJsonAsync<Guid>();
            var json = JsonSerializer.Serialize(model);
            Console.WriteLine(json);

            return id;
        }

        // ============================
        // Orders - Actualizar
        // ============================
        public async Task UpdateOrderAsync(Guid id, OrderViewModel model)
        {
            AddJwtToken();

            var response = await _client.PutAsJsonAsync($"orders/{id}", model);

            response.EnsureSuccessStatusCode();
        }

        // ============================
        // Orderes - Eliminar
        // ============================
        public async Task DeleteOrderAsync(Guid id)
        {
            AddJwtToken();

            var response = await _client.DeleteAsync($"orders/{id}");

            response.EnsureSuccessStatusCode();
        }
    }
}
