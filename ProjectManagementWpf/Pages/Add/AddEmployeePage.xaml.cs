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
    /// Логика взаимодействия для AddEmployeePage.xaml
    /// </summary>
    public partial class AddEmployeePage : Page
    {
        private readonly ApiServiceEmployees _apiService;
        private readonly Employees _employee;

        public AddEmployeePage(Employees employee = null)
        {
            InitializeComponent();
            _apiService = new ApiServiceEmployees();
            _employee = employee ?? new Employees();

            FirstNameTextBox.Text = _employee.FirstName;
            LastNameTextBox.Text = _employee.LastName;
            MiddleNameTextBox.Text = _employee.MiddleName;
            EmailTextBox.Text = _employee.Email;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _employee.FirstName = FirstNameTextBox.Text;
                _employee.LastName = LastNameTextBox.Text;
                _employee.MiddleName = MiddleNameTextBox.Text;
                _employee.Email = EmailTextBox.Text;

                if (_employee.EmployeeID == 0)
                {
                    await _apiService.AddEmployeeAsync(_employee);
                    MessageBox.Show("Сотрудник успешно добавлен");
                }
                else
                {
                    await _apiService.UpdateEmployeeAsync(_employee.EmployeeID, _employee);
                    MessageBox.Show("Сотрудник успешно обновлен");
                }

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении сотрудника: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
