using ProjectManagementApi.Models;
using ProjectManagementWpf.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectManagementWpf.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для AddCompanyPage.xaml
    /// </summary>
    public partial class AddCompanyPage : Page
    {
        private readonly ApiServiceCompanies _apiService;
        private readonly Companies _company;

        public AddCompanyPage(Companies company = null)
        {
            InitializeComponent();
            _apiService = new ApiServiceCompanies();
            _company = company ?? new Companies();

            CompanyNameTextBox.Text = _company.CompanyName;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _company.CompanyName = CompanyNameTextBox.Text;

                if (_company.CompanyID == 0)
                {
                    await _apiService.AddCompanyAsync(_company);
                    MessageBox.Show("Компания успешно добавлена");
                }
                else
                {
                    await _apiService.UpdateCompanyAsync(_company.CompanyID, _company);
                    MessageBox.Show("Компания успешно обновлена");
                }

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении компании: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
