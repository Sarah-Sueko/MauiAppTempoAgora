using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora;

public partial class MainPage : ContentPage
{
    private readonly ApiClima _apiClima;

    public MainPage()
    {
        InitializeComponent();

        _apiClima = new ApiClima();
    }

    private async void btnBuscar_Clicked(
        object sender,
        EventArgs e)
    {
        string cidade = txtCidade.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(cidade))
        {
            await DisplayAlertAsync(
                "Atenção",
                "Digite o nome de uma cidade.",
                "OK");

            return;
        }

        try
        {
            loading.IsVisible = true;
            loading.IsRunning = true;

            resultado.IsVisible = false;

            Tempo? tempo =
                await _apiClima.GetPrevisao(cidade);

            lblCidade.Text = tempo.Nome;

            lblTemperatura.Text =
                $"{tempo.Main.Temperatura:F1} °C";

            if (tempo.Weather.Count > 0)
            {
                string descricao =
                    tempo.Weather[0].Descricao;

                lblDescricao.Text =
                    char.ToUpper(descricao[0]) +
                    descricao.Substring(1);
            }

            lblSensacao.Text =
                $"Sensação térmica: {tempo.Main.Sensacao:F1} °C";

            lblUmidade.Text =
                $"Umidade: {tempo.Main.Umidade}%";

            lblSensacao.Text =
                $"Sensação térmica: {tempo.Main.Sensacao:F1} °C";

            lblUmidade.Text =
                $"Umidade: {tempo.Main.Umidade}%";

            lblVento.Text =
                $"Vento: {tempo.Wind.Velocidade:F1} m/s";

            double visibilidadeKm =
                tempo.Visibility / 1000.0;

            lblVisibilidade.Text =
                $"Visibilidade: {visibilidadeKm:F1} km";

            resultado.IsVisible = true;
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Erro",
                ex.Message,
                "OK");
        }
        finally
        {
            loading.IsVisible = false;
            loading.IsRunning = false;
        }
    }
}