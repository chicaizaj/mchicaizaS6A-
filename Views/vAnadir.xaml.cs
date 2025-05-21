using System.Net;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace mchicaizaS6A.Views;

public partial class vAnadir : ContentPage
{
    public vAnadir()
    {
        InitializeComponent();
    }

    private async void btnagregar_Clicked(object sender, EventArgs e)
    {
        try
        {
            double precio = double.Parse(txtprice.Text, CultureInfo.InvariantCulture);

            var producto = new
            {
                name = txtnombre.Text,
                price = precio
            };

            var json = JsonSerializer.Serialize(producto);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            using var cliente = new HttpClient();

            var url = "http://10.10.51.35:8080/api/products"; 
            var respuesta = await cliente.PostAsync(url, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                await Navigation.PushAsync(new Vcrud());
            }
            else
            {
                var error = await respuesta.Content.ReadAsStringAsync();
                await DisplayAlert("Error servidor", error, "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }
}