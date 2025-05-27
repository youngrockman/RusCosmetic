using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Russian_cosmetic.Model;
using Russian_cosmetic.Tools;


namespace Russian_cosmetic;

public partial class OrderFormWindow : Window
{
    private readonly int _employeeId;
    private List<Service> _selectedServices = new List<Service>();

    public OrderFormWindow()
    {
        InitializeComponent();
    }

    public OrderFormWindow(int employeeId)
    {
        InitializeComponent();
        _employeeId = employeeId;
        using var context = new User6Context();
        
        //выбор максимального id заказа для полсказки 
        var maxOrderId = context.Orders.Any() ? context.Orders.Max(o => o.OrderId) : 0;
        IdBox.Text = (maxOrderId + 1).ToString();

        var indidividuals = context.Individuals.ToList();
        var legalentity = context.Legalentities.ToList();
        //Объединяем всех юзеров в один список
        var individuals = context.Individuals.ToList();
        var legalentities = context.Legalentities.ToList();

        var allClients = individuals.Select(i => new ClientViewModel
        {
            DisplayName = $"Физ. лицо: {i.FullName}",
            Client = i
        }).Concat(legalentities.Select(l => new ClientViewModel
        {
            DisplayName = $"Юр. лицо: {l.CompanyName}",
            Client = l
        })).ToList();

        ClientsBox.ItemsSource = allClients;

        var services = context.Services.ToList();
        
        
        ServiceBox.ItemsSource = services.ToList();
        ClientsBox.ItemsSource = allClients;
        

    }
    
    
    
    private void ClientsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ClientsBox.SelectedItem is ClientViewModel viewModel)
        {
            if (viewModel.Client is Individual individual)
            {
                IndividualsBlock.IsVisible = true;
                LegalentityBlock.IsVisible = false;
                IndividualsBlock.Text = $"Физ. лицо: {individual.FullName}, Паспорт: {individual.PassportDetails}";
            }
            else if (viewModel.Client is Legalentity legalEntity)
            {
                IndividualsBlock.IsVisible = false;
                LegalentityBlock.IsVisible = true;
                LegalentityBlock.Text = $"Юр. лицо: {legalEntity.CompanyName}, ИНН: {legalEntity.Inn}";
            }
        }
        else
        {
            IndividualsBlock.IsVisible = false;
            LegalentityBlock.IsVisible = false;
        }

    }

    private void AddServiceButton_Click(object sender, RoutedEventArgs e)
    {
        if (ServiceBox.SelectedItem is Service selectedService)
        {
            _selectedServices.Add(selectedService);
            UpdateServiceListBox();
            CalculateTotal();
        }
    }
    
    private void CalculateTotal()
    {
        if (_selectedServices.Any())
        {
            var totalPrice = _selectedServices.Sum(s => s.StandardPrice);
            var totalTime = _selectedServices
                .Select(s => int.TryParse(s.ExecutionTime, out var time) ? time : 0)
                .Sum();

            ExecutionTimeBox.Text = totalTime.ToString();
            TotalPriceBox.Text = totalPrice.ToString("C");
        }
        else
        {
            ExecutionTimeBox.Text = string.Empty;
            TotalPriceBox.Text = string.Empty;
        }
    }
    
    
    private void UpdateServiceListBox()
    {
        ServiceListBox.ItemsSource = null;
        ServiceListBox.ItemsSource = _selectedServices;
    }
    
    
     private async void CreateOrderButton_Click(object sender, RoutedEventArgs e)
    {
        
        // Проверка на заполнение обязательных полей
        if (ClientsBox.SelectedItem == null || !_selectedServices.Any())
        {
            ClientServiceErrorBlock.IsVisible = true;
            return;
        }

        if (!int.TryParse(IdBox.Text, out var orderId))
        {
            IncorrentOrderNumberBlock.IsVisible = true;
            return;
        }
        
        try
        {
            using var context = new User6Context();
            
            var selectedClient = ClientsBox.SelectedItem as ClientViewModel;

            if (selectedClient == null)
            {
                throw new InvalidOperationException("Клиент не выбран или имеет неверный формат");
            }

            
            var clientCode = selectedClient.Client switch
            {
                Individual individual => individual.ClientCode,
                Legalentity legalEntity => legalEntity.ClientCode,
                _ => throw new InvalidOperationException("Неизвестный тип клиента")
            };

            var currentDate = DateTime.Now;
            var orderNumber = $"{clientCode}/{currentDate:dd.MM.yyyy}";

            var newOrder = new Order
            {
                OrderId = orderId,
                ClientCode = clientCode,
                EmployeeId = _employeeId,
                CreationDate = DateOnly.FromDateTime(currentDate),
                Status = "Новый",
                ExecutionTime = ExecutionTimeBox.Text,
                OrderNumber = orderNumber
            };

            context.Orders.Add(newOrder);
            
            
            // Добавляем связи между заказом и услугами
            foreach (var service in _selectedServices)
            {
                context.Orderservices.Add(new Orderservice
                {
                    OrderId = orderId,
                    ServiceId = service.ServiceId
                });
            }

            await context.SaveChangesAsync();
            
            CreateOrderBlock.IsVisible = true;
            await Task.Delay(1500); 
            Close();
        }
        catch (Exception ex)
        {
            var errorMessage = ex.InnerException?.Message ?? ex.Message;

            var errorWindow = new Window
            {
                Title = "Ошибка",
                Width = 400,
                Height = 200,
                Content = new TextBlock
                {
                    Text = $"Ошибка при создании заказа:\n{errorMessage}",
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(10)
                }
            };
            await errorWindow.ShowDialog(this);
        }

    }
       
  
    

    private void AddClient_ButtonClick(object? sender, RoutedEventArgs e)
    {
        AddClientWindow addClientWindow = new();
        addClientWindow.ShowDialog(this);
        
    }

    private void Back_ButtonClick(object? sender, RoutedEventArgs e)
    {
        AuthWindow authWindow = new();
        authWindow.Show();
        this.Close();
        
    }
    
}
       
