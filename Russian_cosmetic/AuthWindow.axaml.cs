using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Russian_cosmetic.Model;


namespace Russian_cosmetic;

public partial class AuthWindow : Window
{
    public AuthWindow()
    {
        InitializeComponent();
    }

    private void Auth_ButtonClick(object? sender, RoutedEventArgs e)
    {
       
        if (string.IsNullOrWhiteSpace(LoginBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Text))
        {
            ErrorBlock.IsVisible = true;
        }

        using var context = new User6Context();

       
        var employee = context.Employees.FirstOrDefault(e => e.Login == LoginBox.Text);

        if (employee != null && employee.Password == PasswordBox.Text)
        {
            
            var orderFormWindow = new OrderFormWindow(employee.EmployeeId);
            orderFormWindow.Show();
            this.Close();
        }
        else
        {
            ErrorBlock.IsVisible = true;
        }
    }

    private void Exit_ButtonClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}


