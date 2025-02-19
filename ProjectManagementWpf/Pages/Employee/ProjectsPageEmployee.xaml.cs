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
using ProjectManagementWpf.Pages;


namespace ProjectManagementWpf.Pages.Employee
{
    /// <summary>
    /// Логика взаимодействия для ProjectsPageEmployee.xaml
    /// </summary>
    public partial class ProjectsPageEmployee : Page
    {
        private readonly ApiServiceProjects _apiService;
        private List<Projects> _allProjects;
        public ProjectsPageEmployee()
        {
            InitializeComponent(); // Сначала загружаем компоненты
            _apiService = new ApiServiceProjects(); // Затем инициализируем API сервис
            Loaded += ProjectsPageEmployee_Loaded;
        }

        private async void ProjectsPageEmployee_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadProjects();
        }


        private async Task LoadProjects()
        {
            try
            {
                _allProjects = await _apiService.GetProjectsAsync();
                ProjectsListView1.ItemsSource = _allProjects;
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
            if (ProjectsListView1.SelectedItem is Projects selectedProject)
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
            if (ProjectsListView1.SelectedItem is Projects selectedProject)
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
            NavigationService.Navigate(new TasksPageEmployee());
        }
    }
}
