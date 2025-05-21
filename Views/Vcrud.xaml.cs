using mchicaizaS6A.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace mchicaizaS6A.Views;

public partial class Vcrud : ContentPage
{
	private const string URL = "http://localhost:8080/api/products";
	private HttpClient HttpClient = new HttpClient();

	private ObservableCollection<Products> _ProductTem;

    public Vcrud()
	{
		InitializeComponent();
		
	}

	public async void MostrarEstudiantes() 
	{ 
		var content = await HttpClient.GetStringAsync(URL);
		List<Products> lista = JsonConvert.DeserializeObject<List<Products>>(content);
		_ProductTem = new ObservableCollection<Products>(lista);
		lvproducts.ItemsSource = _ProductTem;
	}

    protected override void OnAppearing()
    {
		base.OnAppearing();
		MostrarEstudiantes();
    }
    private void btnanadir_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new vAnadir());
    }

    private void lvproducts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		var obProducto = (Products)e.SelectedItem;
		Navigation.PushAsync(new vActualizarEliminar(obProducto));

    }
}