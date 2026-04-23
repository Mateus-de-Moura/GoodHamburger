using GoodHamburger.Models;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace GoodHamburger.Services
{
    public class ProductApiService(HttpClient httpClient)
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<List<ProductModel>> GetProductsAsync(CancellationToken ct = default)
        {
            var products = await httpClient.GetFromJsonAsync<List<ProductModel>>("products", JsonOptions, ct);
            return products ?? [];
        }

        public async Task<PagedListModel<ProductModel>> GetPagedProductsAsync(
            int pageNumber,
            int pageSize,
            CancellationToken ct = default)
        {
            var response = await httpClient.GetFromJsonAsync<PagedListModel<ProductModel>>(
                $"products/paged?pageNumber={pageNumber}&pageSize={pageSize}",
                JsonOptions,
                ct);

            return response ?? new PagedListModel<ProductModel>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = 0,
                CurrentItems = []
            };
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync(CancellationToken ct = default)
        {
            var categories = await httpClient.GetFromJsonAsync<List<CategoryModel>>("categories", JsonOptions, ct);
            return categories ?? [];
        }

        public async Task<MenuModel> GetMenuAsync(CancellationToken ct = default)
        {
            var menu = await httpClient.GetFromJsonAsync<MenuModel>("menu", JsonOptions, ct);
            return menu ?? new MenuModel();
        }

        public async Task<List<OrderModel>> GetOrdersAsync(CancellationToken ct = default)
        {
            using var response = await httpClient.GetAsync("orders", ct);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return [];

            var content = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException(ExtractMessage(content, $"Falha na chamada da API ({(int)response.StatusCode})."));

            if (string.IsNullOrWhiteSpace(content))
                return [];

            return JsonSerializer.Deserialize<List<OrderModel>>(content, JsonOptions) ?? [];
        }

        public async Task<ApiActionResult> CreateOrderAsync(IEnumerable<Guid> productIds, CancellationToken ct = default)
        {
            var payload = new
            {
                ProductIds = productIds.Distinct().ToList()
            };

            using var response = await httpClient.PostAsJsonAsync("orders", payload, JsonOptions, ct);
            return await ToActionResultAsync(response, "Pedido cadastrado com sucesso.", ct);
        }

        public async Task<ApiDataResult<OrderPreviewModel>> PreviewOrderAsync(IEnumerable<Guid> productIds, CancellationToken ct = default)
        {
            var payload = new
            {
                ProductIds = productIds.Distinct().ToList()
            };

            using var response = await httpClient.PostAsJsonAsync("orders/preview", payload, JsonOptions, ct);
            var content = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                return new ApiDataResult<OrderPreviewModel>(
                    false,
                    ExtractMessage(content, $"Falha na chamada da API ({(int)response.StatusCode})."),
                    null);
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                return new ApiDataResult<OrderPreviewModel>(
                    true,
                    "Pré-visualização atualizada.",
                    new OrderPreviewModel());
            }

            var data = JsonSerializer.Deserialize<OrderPreviewModel>(content, JsonOptions);
            return new ApiDataResult<OrderPreviewModel>(true, "Pré-visualização atualizada.", data);
        }

        public async Task<ApiActionResult> CreateProductAsync(
            CreateProductRequest request,
            byte[]? imageBytes,
            string? imageName,
            string? imageContentType,
            CancellationToken ct = default)
        {
            using var content = BuildCreateContent(request, imageBytes, imageName, imageContentType);
            using var response = await httpClient.PostAsync("products", content, ct);
            return await ToActionResultAsync(response, "Produto cadastrado com sucesso.", ct);
        }

        public async Task<ApiActionResult> UpdateProductAsync(
            UpdateProductRequest request,
            byte[]? imageBytes,
            string? imageName,
            string? imageContentType,
            CancellationToken ct = default)
        {
            using var content = BuildUpdateContent(request, imageBytes, imageName, imageContentType);
            using var response = await httpClient.PutAsync("products", content, ct);
            return await ToActionResultAsync(response, "Produto atualizado com sucesso.", ct);
        }

        public async Task<ApiActionResult> DeleteProductAsync(Guid id, CancellationToken ct = default)
        {
            using var response = await httpClient.DeleteAsync($"products/{id}", ct);
            return await ToActionResultAsync(response, "Produto removido com sucesso.", ct);
        }

        private static async Task<ApiActionResult> ToActionResultAsync(
            HttpResponseMessage response,
            string successMessage,
            CancellationToken ct)
        {
            var content = await response.Content.ReadAsStringAsync(ct);

            if (response.IsSuccessStatusCode)
            {
                return new ApiActionResult(true, ExtractMessage(content, successMessage));
            }

            return new ApiActionResult(false, ExtractMessage(content, $"Falha na chamada da API ({(int)response.StatusCode})."));
        }

        private static MultipartFormDataContent BuildCreateContent(
            CreateProductRequest request,
            byte[]? imageBytes,
            string? imageName,
            string? imageContentType)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(request.Name), nameof(request.Name) },
                { new StringContent(request.Description), nameof(request.Description) },
                { new StringContent(request.Price.ToString(CultureInfo.CurrentCulture)), nameof(request.Price) },
                { new StringContent(request.CategoryId.ToString()), nameof(request.CategoryId) }
            };

            AddImageContent(content, imageBytes, imageName, imageContentType);

            return content;
        }

        private static MultipartFormDataContent BuildUpdateContent(
            UpdateProductRequest request,
            byte[]? imageBytes,
            string? imageName,
            string? imageContentType)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(request.Id.ToString()), nameof(request.Id) },
                { new StringContent(request.Name), nameof(request.Name) },
                { new StringContent(request.Description), nameof(request.Description) },
                { new StringContent(request.Price.ToString(CultureInfo.CurrentCulture)), nameof(request.Price) },
                { new StringContent(request.CategoryId.ToString()), nameof(request.CategoryId) }
            };

            AddImageContent(content, imageBytes, imageName, imageContentType);

            return content;
        }

        private static void AddImageContent(
            MultipartFormDataContent content,
            byte[]? imageBytes,
            string? imageName,
            string? imageContentType)
        {
            if (imageBytes is null || imageBytes.Length == 0)
                return;

            var byteArrayContent = new ByteArrayContent(imageBytes);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(
                string.IsNullOrWhiteSpace(imageContentType) ? "application/octet-stream" : imageContentType);
            content.Add(byteArrayContent, "Image", string.IsNullOrWhiteSpace(imageName) ? "upload.bin" : imageName);
        }

        private static string ExtractMessage(string? content, string fallback)
        {
            if (string.IsNullOrWhiteSpace(content))
                return fallback;

            var payload = content.Trim();

            if (payload.StartsWith('"') && payload.EndsWith('"'))
                return payload.Trim('"');

            try
            {
                using var document = JsonDocument.Parse(payload);

                if (document.RootElement.ValueKind == JsonValueKind.String)
                    return document.RootElement.GetString() ?? fallback;

                if (document.RootElement.ValueKind == JsonValueKind.Array)
                {
                    var messages = document.RootElement
                        .EnumerateArray()
                        .Where(x => x.ValueKind == JsonValueKind.String)
                        .Select(x => x.GetString())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToList();

                    return messages.Count > 0
                        ? string.Join(" | ", messages)
                        : fallback;
                }

                if (document.RootElement.ValueKind != JsonValueKind.Object)
                    return fallback;

                if (document.RootElement.TryGetProperty("message", out var messageElement))
                {
                    var message = messageElement.GetString();
                    if (!string.IsNullOrWhiteSpace(message))
                        return message;
                }

                if (document.RootElement.TryGetProperty("errors", out var errorsElement) &&
                    errorsElement.ValueKind == JsonValueKind.Array)
                {
                    var errors = errorsElement
                        .EnumerateArray()
                        .Where(x => x.ValueKind == JsonValueKind.String)
                        .Select(x => x.GetString())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToList();

                    if (errors.Count > 0)
                        return string.Join(" | ", errors);
                }

                return fallback;
            }
            catch (JsonException)
            {
                return payload;
            }
        }
    }
}
