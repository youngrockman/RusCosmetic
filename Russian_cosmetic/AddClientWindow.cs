using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Russian_cosmetic.Model;

namespace Russian_cosmetic;

public partial class AddClientWindow : Window
{
    public AddClientWindow()
    {
        InitializeComponent();
    }

    // Переключение видимости панелей при выборе типа клиента
    private void Type_Checked(object? sender, RoutedEventArgs e)
    {
        PanelIndividual.IsVisible = RbIndividual.IsChecked == true;
        PanelLegalentity.IsVisible = RbLegalentity.IsChecked == true;
        ErrorBlock.IsVisible = false;
    }

   
    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    
    private async void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        ErrorBlock.IsVisible = false;

        try
        {
            using var context = new User6Context();

            if (RbIndividual.IsChecked == true)
            {
                // Валидация обязательных полей
                if (string.IsNullOrWhiteSpace(TbIndClientCode.Text) ||
                    string.IsNullOrWhiteSpace(TbFullName.Text) ||
                    string.IsNullOrWhiteSpace(TbPassport.Text) ||
                    DpBirthDate.SelectedDate == null ||
                    string.IsNullOrWhiteSpace(TbIndEmail.Text) ||
                    string.IsNullOrWhiteSpace(PbIndPassword.Text))
                {
                    throw new InvalidOperationException("Заполните все поля физического лица.");
                }

                var ind = new Individual
                {
                    ClientCode = TbIndClientCode.Text.Trim(),
                    FullName = TbFullName.Text.Trim(),
                    PassportDetails = TbPassport.Text.Trim(),
                    BirthDate = DateOnly.FromDateTime(DpBirthDate.SelectedDate.Value.Date),
                    Address = TbIndAddress.Text.Trim(),
                    Email = TbIndEmail.Text.Trim(),
                    Password = PbIndPassword.Text
                };

                context.Individuals.Add(ind);
            }
            else
            {
                // Валидация полей юр. лица
                if (string.IsNullOrWhiteSpace(TbLegClientCode.Text) ||
                    string.IsNullOrWhiteSpace(TbCompanyName.Text) ||
                    string.IsNullOrWhiteSpace(TbInn.Text) ||
                    string.IsNullOrWhiteSpace(TbAccount.Text) ||
                    string.IsNullOrWhiteSpace(TbBik.Text) ||
                    string.IsNullOrWhiteSpace(TbDirector.Text) ||
                    string.IsNullOrWhiteSpace(TbContactPerson.Text) ||
                    string.IsNullOrWhiteSpace(TbContactPhone.Text) ||
                    string.IsNullOrWhiteSpace(TbLegEmail.Text) ||
                    string.IsNullOrWhiteSpace(PbLegPassword.Text) ||
                    string.IsNullOrWhiteSpace(AddresBox.Text))
                {
                    throw new InvalidOperationException("Заполните все поля юридического лица.");
                }

                var leg = new Legalentity
                {
                    ClientCode = TbLegClientCode.Text.Trim(),
                    CompanyName = TbCompanyName.Text.Trim(),
                    Inn = TbInn.Text.Trim(),
                    AccountNumber = TbAccount.Text.Trim(),
                    Bik = TbBik.Text.Trim(),
                    DirectorName = TbDirector.Text.Trim(),
                    ContactPerson = TbContactPerson.Text.Trim(),
                    ContactPhone = TbContactPhone.Text.Trim(),
                    Email = TbLegEmail.Text.Trim(),
                    Password = PbLegPassword.Text,
                    Address = AddresBox.Text.Trim()
                };

                context.Legalentities.Add(leg);
            }

            await context.SaveChangesAsync();
            Close();
        }
        catch (Exception ex)
        {
            ErrorBlock.Text = ex.InnerException?.Message ?? ex.Message;
            ErrorBlock.IsVisible = true;
        }
    }
}
