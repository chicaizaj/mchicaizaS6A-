using mchicaizaS6A.Models;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;

namespace mchicaizaS6A.Views;

public partial class vActualizarEliminar : ContentPage
{
	public vActualizarEliminar(Products datos)
	{
		InitializeComponent();
        txtCodigo.Text = datos.id.ToString();
        txtNombre.Text = datos.name;
        txtprice.Text = datos.price.ToString();
	}

    private async void btnactualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var producto = new
            {
                id = int.Parse(txtCodigo.Text),
                name = txtNombre.Text,
                price = double.Parse(txtprice.Text)
            };

            var json = JsonSerializer.Serialize(producto);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            using var cliente = new HttpClient();
            var url = $"http://10.10.51.35:8080/api/products/{producto.id}";

            var respuesta = await cliente.PutAsync(url, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Producto actualizado correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                var error = await respuesta.Content.ReadAsStringAsync();
                await DisplayAlert("Error del servidor", error, "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Cerrar");
        }
    }

    private async void btneliminar_Clicked(object sender, EventArgs e)
    {
        bool confirmacion = await DisplayAlert("Confirmar", "¿Estás seguro de eliminar este producto?", "Sí", "No");
        if (!confirmacion) return;

        try
        {
            int id = int.Parse(txtCodigo.Text);

            using var cliente = new HttpClient();
            var url = $"http://10.10.51.35:8080/api/products/{id}";

            var respuesta = await cliente.DeleteAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Producto eliminado correctamente", "OK");
                await Navigation.PopAsync();
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
