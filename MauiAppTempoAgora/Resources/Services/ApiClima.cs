using System.Net.Http;
using System.Text.Json;
using MauiAppTempoAgora.Models;

namespace MauiAppTempoAgora.Services;

public class ApiClima
{
    private readonly HttpClient _httpClient;

    private const string ApiKey = "a3316e2e1bc73c3f546930cc446b866f";

    public ApiClima()
    {
        _httpClient = new HttpClient();
    }

    public async Task<Tempo?> GetPrevisao(string cidade)
    {
        string url =
            $"https://api.openweathermap.org/data/2.5/weather" +
            $"?q={Uri.EscapeDataString(cidade)}" +
            $"&appid={ApiKey}" +
            $"&units=metric" +
            $"&lang=pt_br";

        try
        {
            using HttpResponseMessage resposta =
                await _httpClient.GetAsync(url);

            string json =
                await resposta.Content.ReadAsStringAsync();

    
            if (resposta.StatusCode ==
                System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception(
                    "Cidade não encontrada. " +
                    "Verifique o nome digitado e tente novamente.");
            }

            
            if (resposta.StatusCode ==
                System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception(
                    "A chave da API é inválida ou ainda não foi ativada.");
            }

            if (!resposta.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Erro ao consultar a API. " +
                    $"Código HTTP: {(int)resposta.StatusCode}");
            }

            Tempo? tempo =
                JsonSerializer.Deserialize<Tempo>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (tempo == null)
            {
                throw new Exception(
                    "Não foi possível interpretar os dados do clima.");
            }

            return tempo;
        }
        catch (HttpRequestException)
        {
            throw new Exception(
                "Não foi possível conectar à internet. " +
                "Verifique sua conexão e tente novamente.");
        }
    }
}