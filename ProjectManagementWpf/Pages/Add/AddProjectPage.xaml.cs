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
    /// Логика взаимодействия для AddProjectPage.xaml
    /// </summary>
    public partial class AddProjectPage : Page
    {
        private readonly ApiServiceProjects _apiService;
        private readonly Projects _project;

        public AddProjectPage(Projects project = null)
        {
            InitializeComponent();
            _apiService = new ApiServiceProjects();
            _project = project ?? new Projects();

            // Заполняем поля, если редактируем существующий проект
            ProjectNameTextBox.Text = _project.ProjectName;
            CustomerCompanyIDTextBox.Text = _project.CustomerCompanyID.ToString();
            ContractorCompanyIDTextBox.Text = _project.ContractorCompanyID.ToString();
            PriorityTextBox.Text = _project.Priority.ToString();
            ProjectManagerIDTextBox.Text = _project.ProjectManagerID.ToString();

            if (_project.StartDate != DateTime.MinValue)
                StartDatePicker.SelectedDate = _project.StartDate;

            if (_project.EndDate != DateTime.MinValue)
                EndDatePicker.SelectedDate = _project.EndDate;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _project.ProjectName = ProjectNameTextBox.Text;

                // Проверяем, чтобы ID компаний и менеджера были корректными числами
                if (!int.TryParse(CustomerCompanyIDTextBox.Text, out int customerCompanyID) ||
                    !int.TryParse(ContractorCompanyIDTextBox.Text, out int contractorCompanyID) ||
                    !int.TryParse(PriorityTextBox.Text, out int priority) ||
                    !int.TryParse(ProjectManagerIDTextBox.Text, out int projectManagerID))
                {
                    MessageBox.Show("Ошибка: Проверьте корректность введенных данных (числовые значения).");
                    return;
                }

                _project.CustomerCompanyID = customerCompanyID;
                _project.ContractorCompanyID = contractorCompanyID;
                _project.Priority = priority;
                _project.ProjectManagerID = projectManagerID;

                // Проверяем, что даты выбраны
                if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Ошибка: Укажите даты начала и окончания проекта.");
                    return;
                }

                _project.StartDate = StartDatePicker.SelectedDate.Value;
                _project.EndDate = EndDatePicker.SelectedDate.Value;

                if (_project.ProjectID == 0)
                {
                    await _apiService.AddProjectAsync(_project);
                    MessageBox.Show("Проект успешно добавлен!");
                }
                else
                {
                    await _apiService.UpdateProjectAsync(_project.ProjectID, _project);
                    MessageBox.Show("Проект успешно обновлен!");
                }

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении проекта: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}