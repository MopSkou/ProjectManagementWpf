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
    /// Логика взаимодействия для TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        private readonly ApiServiceTasks _apiService;
        private List<Tasks> _allTasks;

        public TasksPage()
        {
            InitializeComponent();
            _apiService = new ApiServiceTasks();
            LoadTasks();
        }

        private async void LoadTasks_Click(object sender, RoutedEventArgs e)
        {
            await LoadTasks();
        }

        private async Task LoadTasks()
        {
            try
            {
                _allTasks = await _apiService.GetTasksAsync();
                TasksListView.ItemsSource = _allTasks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки задач: {ex.Message}");
            }
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text != "Поиск по названию...")
            {
                ApplyFiltersAndSorting();
            }
        }

        private void ApplyFiltersAndSorting()
        {
            if (_allTasks == null) return;

            var filteredTasks = _allTasks;

            // Фильтрация по статусу
            if (StatusFilterComboBox.SelectedItem is ComboBoxItem statusItem)
            {
                string selectedStatus = statusItem.Content.ToString();
                if (selectedStatus != "Все") // Если выбрано "Все", статус не фильтруем
                {
                    filteredTasks = filteredTasks.Where(t => t.Status == selectedStatus).ToList();
                }
            }

            // Фильтрация по названию
            string searchText = SearchTextBox.Text?.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(searchText) && searchText != "поиск по названию...")
            {
                filteredTasks = filteredTasks.Where(t => t.TaskName.ToLower().Contains(searchText)).ToList();
            }

            TasksListView.ItemsSource = filteredTasks;
        }

        
        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "Поиск по названию...")
            {
                SearchTextBox.Text = "";
                SearchTextBox.Foreground = Brushes.Black;
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                SearchTextBox.Text = "Поиск по названию...";
                SearchTextBox.Foreground = Brushes.Gray;
            }
        }


        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltersAndSorting();
        }
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddTaskPage());
        }

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListView.SelectedItem is Tasks selectedTask)
            {
                NavigationService.Navigate(new AddTaskPage(selectedTask));
            }
            else
            {
                MessageBox.Show("Выберите задачу для редактирования.");
            }
        }

        private async void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListView.SelectedItem is Tasks selectedTask)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить задачу {selectedTask.TaskName}?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _apiService.DeleteTaskAsync(selectedTask.TaskID);
                        MessageBox.Show("Задача успешно удалена.");
                        await LoadTasks();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении задачи: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления.");
            }
        }

        private void DoljnostiPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DoljnostiPage());
        }

        private void ProjectsPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectsPage());
        }
    }
}
