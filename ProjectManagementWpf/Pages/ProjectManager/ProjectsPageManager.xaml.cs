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
    /// Логика взаимодействия для ProjectsPageManager.xaml
    /// </summary>
    public partial class ProjectsPageManager : Page
    {
        private readonly ApiServiceProjects _apiService;
        private List<Projects> _allProjects;

        public ProjectsPageManager()
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

        private void EmployeesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeesPageManager());
        }
    }
}
