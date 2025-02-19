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
    /// Логика взаимодействия для AddDoljnostiPage.xaml
    /// </summary>
    public partial class AddDoljnostiPage : Page
    {
        private readonly ApiServiceDoljnosti _apiService;
        private readonly DoljnostiDto _doljnosti;

        public AddDoljnostiPage(DoljnostiDto doljnosti = null)
        {
            InitializeComponent();
            _apiService = new ApiServiceDoljnosti();
            _doljnosti = doljnosti ?? new DoljnostiDto();

            PostTextBox.Text = _doljnosti.Post;
            PasswordBox.Password = _doljnosti.Password;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _doljnosti.Post = PostTextBox.Text;
                _doljnosti.Password = PasswordBox.Password;

                if (_doljnosti.id_doljnosti == 0)
                {
                    await _apiService.AddDoljnostiAsync(_doljnosti);
                    MessageBox.Show("Должность успешно добавлена");
                }
                else
                {
                    await _apiService.UpdateDoljnostiAsync(_doljnosti.id_doljnosti, _doljnosti);

                    MessageBox.Show("Должность успешно обновлена");
                }

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении должности: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

