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
using ProjectManagementWpf.Pages;
using ProjectManagementWpf.Pages.Add;


namespace ProjectManagementWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProjectsPage.xaml
    /// </summary>
    public partial class ProjectsPage : Page
    {
        private readonly ApiServiceProjects _apiService;
        private List<Projects> _allProjects;

        public ProjectsPage()
        {
            InitializeComponent();
            _apiService = new ApiServiceProjects();
            LoadProjects();
        }

        private async void LoadProjects_Click(object sender, RoutedEventArgs e)
        {
            await LoadProjects();
        }

        private async Task LoadProjects()
        {
            try
            {
                _allProjects = await _apiService.GetProjectsAsync();
                ProjectsListView.ItemsSource = _allProjects;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки проектов: {ex.Message}");
            }
        }

        private void AddProjectButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProjectPage());
        }

        private void EditProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectsListView.SelectedItem is Projects selectedProject)
            {
                NavigationService.Navigate(new AddProjectPage(selectedProject));
            }
            else
            {
                MessageBox.Show("Выберите проект для редактирования.");
            }
        }

        private async void DeleteProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectsListView.SelectedItem is Projects selectedProject)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить проект {selectedProject.ProjectName}?",
                                             "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _apiService.DeleteProjectAsync(selectedProject.ProjectID);
                        MessageBox.Show("Проект успешно удален.");
                        await LoadProjects();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении проекта: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите проект для удаления.");
            }
        }

        private void TasksPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksPage());
        }

        private void EmployeesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeesPage());
        }
    }
}