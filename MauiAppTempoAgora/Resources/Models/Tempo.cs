using System.Text.Json.Serialization;

namespace MauiAppTempoAgora.Models
{
    public class Tempo
    {
        [JsonPropertyName("name")]
        public string Nome { get; set; } = string.Empty;

        [JsonPropertyName("main")]
        public Main Main { get; set; } = new();

        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; } = new();

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; } = new();

        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }
    }

    public class Main
    {
        [JsonPropertyName("temp")]
        public double Temperatura { get; set; }

        [JsonPropertyName("feels_like")]
        public double Sensacao { get; set; }

        [JsonPropertyName("humidity")]
        public int Umidade { get; set; }
    }

    public class Weather
    {
        [JsonPropertyName("description")]
        public string Descricao { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icone { get; set; } = string.Empty;
    }

    public class Wind
    {
        [JsonPropertyName("speed")]
        public double Velocidade { get; set; }
    }
}