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
    /// Логика взаимодействия для AddTaskPage.xaml
    /// </summary>
    public partial class AddTaskPage : Page
    {
        private readonly ApiServiceTasks _apiService;
        private readonly Tasks _task;

        public AddTaskPage(Tasks task = null)
        {
            InitializeComponent();
            _apiService = new ApiServiceTasks();
            _task = task ?? new Tasks();

            TaskNameTextBox.Text = _task.TaskName;
            AuthorIDTextBox.Text = _task.AuthorID.ToString();
            PerformerIDTextBox.Text = _task.PerformerID.ToString();
            ProjectIDTextBox.Text = _task.ProjectID.ToString();
            StatusTextBox.Text = _task.Status;
            CommentTextBox.Text = _task.Comment;
            PriorityTextBox.Text = _task.Priority.ToString();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _task.TaskName = TaskNameTextBox.Text;
                _task.AuthorID = int.Parse(AuthorIDTextBox.Text);
                _task.PerformerID = int.Parse(PerformerIDTextBox.Text);
                _task.ProjectID = int.Parse(ProjectIDTextBox.Text);
                _task.Status = StatusTextBox.Text;
                _task.Comment = CommentTextBox.Text;
                _task.Priority = int.Parse(PriorityTextBox.Text);

                if (_task.TaskID == 0)
                {
                    await _apiService.AddTaskAsync(_task);
                    MessageBox.Show("Задача успешно добавлена");
                }
                else
                {
                    await _apiService.UpdateTaskAsync(_task.TaskID, _task);
                    MessageBox.Show("Задача успешно обновлена");
                }

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении задачи: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
