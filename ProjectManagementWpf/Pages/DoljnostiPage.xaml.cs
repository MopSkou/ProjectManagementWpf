using ProjectManagementApi.Models;
using ProjectManagementWpf.Pages;
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

namespace ProjectManagementWpf
{
    /// <summary>
    /// Логика взаимодействия для DoljnostiPage.xaml
    /// </summary>
    public partial class DoljnostiPage : Page
    {
        private readonly ApiServiceDoljnosti _apiService;
        private List<DoljnostiDto> _allDoljnosti;

        public DoljnostiPage()
        {
            InitializeComponent();
            _apiService = new ApiServiceDoljnosti();
            LoadDoljnosti();
        }

        private async void LoadDoljnosti_Click(object sender, RoutedEventArgs e)
        {
            await LoadDoljnosti();
        }

        private async Task LoadDoljnosti()
        {
            try
            {
                _allDoljnosti = await _apiService.GetDoljnostiListAsync();
                DoljnostiListView.ItemsSource = _allDoljnosti;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки должностей: {ex.Message}");
            }
        }

        private void AddDoljnostiButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDoljnostiPage());
        }

        private void EditDoljnostiButton_Click(object sender, RoutedEventArgs e)
        {
            if (DoljnostiListView.SelectedItem is DoljnostiDto selectedDoljnosti)
            {
                NavigationService.Navigate(new AddDoljnostiPage(selectedDoljnosti));

            }
            else
            {
                MessageBox.Show("Выберите должность для редактирования.");
            }
        }

        private async void DeleteDoljnostiButton_Click(object sender, RoutedEventArgs e)
        {
            if (DoljnostiListView.SelectedItem is DoljnostiDto selectedDoljnosti)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить должность {selectedDoljnosti.Post}?", "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _apiService.DeleteDoljnostiAsync(selectedDoljnosti.id_doljnosti);
                        MessageBox.Show("Должность успешно удалена.");
                        await LoadDoljnosti();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении должности: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите должность для удаления.");
            }
        }

        private void BtnArrowLeft_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new PreviousPage());
        }

        private void BtnArrowRight_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new NextPage());
        }

        private void CompaniesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CompaniesPage());
        }

        private void TasksPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksPage());
        }
    }
}

