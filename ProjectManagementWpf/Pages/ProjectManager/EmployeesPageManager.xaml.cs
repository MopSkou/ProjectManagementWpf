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

namespace ProjectManagementWpf.Pages.ProjectManager
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPageManager.xaml
    /// </summary>
    public partial class EmployeesPageManager : Page
    {
        private readonly ApiServiceEmployees _apiService;
        private List<Employees> _allEmployees;
        public EmployeesPageManager()
        {
            InitializeComponent();
            _apiService = new ApiServiceEmployees();
            LoadEmployees();
        }

        private async void LoadEmployees_Click(object sender, RoutedEventArgs e)
        {
            await LoadEmployees();
        }

        private async Task LoadEmployees()
        {
            try
            {
                _allEmployees = await _apiService.GetEmployeesAsync();
                EmployeesListView.ItemsSource = _allEmployees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки сотрудников: {ex.Message}");
            }
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmployeePage());
        }

        private void EditEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesListView.SelectedItem is Employees selectedEmployee)
            {
                NavigationService.Navigate(new AddEmployeePage(selectedEmployee));
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для редактирования.");
            }
        }

        private async void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesListView.SelectedItem is Employees selectedEmployee)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить сотрудника {selectedEmployee.FirstName} {selectedEmployee.LastName}?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _apiService.DeleteEmployeeAsync(selectedEmployee.EmployeeID);
                        MessageBox.Show("Сотрудник успешно удален.");
                        await LoadEmployees();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении сотрудника: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления.");
            }
        }

  
        private void TasksPageManager_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksPageManager());
        }
    }
}