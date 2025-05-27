using Avalonia;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using Russian_cosmetic;
using Russian_cosmetic.Model;
using Russian_cosmetic.Tools;
using Moq;

namespace LibaryTests;

[TestFixture]
public class OrderFormWindowTests
{
  
    private OrderFormWindow _window;
    private List<Service> _testServices;
    private List<ClientViewModel> _testClients;

    [OneTimeSetUp]
    public void InitAvalonia()
    {
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .SetupWithoutStarting();
    }
    
    [SetUp]
    public void Setup()
    {
        
        _testServices = new List<Service>
        {
            new Service { ServiceId = 1, ServiceName = "Услуга 1", StandardPrice = 1000, ExecutionTime = "2" },
            new Service { ServiceId = 2, ServiceName = "Услуга 2", StandardPrice = 2000, ExecutionTime = "3" }
        };

        _testClients = new List<ClientViewModel>
        {
            new ClientViewModel
            {
                DisplayName = "Физ. лицо: Иванов Иван",
                Client = new Individual { ClientCode = "IND1", FullName = "Иванов Иван" }
            }
        };
        
        _window = new OrderFormWindow(1)
        {
            ClientsBox = { ItemsSource = _testClients },
            ServiceBox = { ItemsSource = _testServices }
        };
    }

    

    [Test]
    public void CreateOrder_WithoutClient_ShouldShowError()
    {
        
        _window.ClientsBox.SelectedItem = null;
        _window.ServiceBox.SelectedItem = _testServices.First();

   
        _window.CreateOrderButton_Click(null, new RoutedEventArgs());

       
        Assert.IsTrue(_window.ClientServiceErrorBlock.IsVisible);
    }

    [Test]
    public void CreateOrder_WithoutServices_ShouldShowError()
    {
       
        _window.ClientsBox.SelectedItem = _testClients.First();
        _window._selectedServices.Clear();

        
        _window.CreateOrderButton_Click(null, new RoutedEventArgs());

       
        Assert.IsTrue(_window.ClientServiceErrorBlock.IsVisible);
    }

    [Test]
    public void CreateOrder_WithInvalidOrderId_ShouldShowError()
    {
       
        _window.ClientsBox.SelectedItem = _testClients.First();
        _window.ServiceBox.SelectedItem = _testServices.First();
        _window.IdBox.Text = "invalid_id";

        
        _window.CreateOrderButton_Click(null, new RoutedEventArgs());

       
        Assert.IsTrue(_window.IncorrentOrderNumberBlock.IsVisible);
    }

    [Test]
    public void CalculateTotal_WithMultipleServices_ShouldReturnCorrectSum()
    {
        
        _window._selectedServices.AddRange(_testServices);

        
        _window.CalculateTotal();

    
        Assert.AreEqual("3 000,00 ₽", _window.TotalPriceBox.Text.Replace('\u00A0', ' '));
        Assert.AreEqual("5", _window.ExecutionTimeBox.Text);
    }
    
    
    [Test]
    public void CreateOrder_WithValidData_ShouldNotShowError()
    {
        
        _window.ClientsBox.SelectedItem = _testClients.First();

      
        _window._selectedServices.Clear();
        _window._selectedServices.Add(_testServices.First());

      
        _window.IdBox.Text = "123";

       
        _window.CreateOrderButton_Click(null, new RoutedEventArgs());

      
        Assert.IsFalse(_window.ClientServiceErrorBlock.IsVisible, "Ожидалось отсутствие ошибки клиента/услуг");
        Assert.IsFalse(_window.IncorrentOrderNumberBlock.IsVisible, "Ожидалось отсутствие ошибки номера заказа");
    }

}
