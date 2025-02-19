using ProjectManagementApi.Models;
using ProjectManagementWpf.Pages.Add;
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

namespace ProjectManagementWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для CompaniesPage.xaml
    /// </summary>
    public partial class CompaniesPage : Page
    {
        private readonly ApiServiceCompanies _apiService;
        private List<Companies> _allCompanies;

        public CompaniesPage()
        {
            InitializeComponent();
            _apiService = new ApiServiceCompanies();
            LoadCompanies();
        }

        private async void LoadCompanies_Click(object sender, RoutedEventArgs e)
        {
            await LoadCompanies();
        }

        private async Task LoadCompanies()
        {
            try
            {
                _allCompanies = await _apiService.GetCompaniesListAsync();
                CompaniesListView.ItemsSource = _allCompanies;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки компаний: {ex.Message}");
            }
        }

        private void AddCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCompanyPage());
        }

        private void EditCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            if (CompaniesListView.SelectedItem is Companies selectedCompany)
            {
                NavigationService.Navigate(new AddCompanyPage(selectedCompany));
            }
            else
            {
                MessageBox.Show("Выберите компанию для редактирования.");
            }
        }

        private async void DeleteCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            if (CompaniesListView.SelectedItem is Companies selectedCompany)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить компанию {selectedCompany.CompanyName}?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _apiService.DeleteCompanyAsync(selectedCompany.CompanyID);
                        MessageBox.Show("Компания успешно удалена.");
                        await LoadCompanies();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении компании: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите компанию для удаления.");
            }
        }

        private void EmployeesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeesPage());
        }

        private void DoljnostiPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DoljnostiPage());
        }
    }
}