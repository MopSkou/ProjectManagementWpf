using ProjectManagementWpf.Pages.Employee;
using ProjectManagementWpf.Pages.ProjectManager;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly ApiServiceLogin _apiService;

        public LoginPage()
        {
            InitializeComponent();
            _apiService = new ApiServiceLogin();
        }


            private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            // Проверяем, что поля не пустые
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            // Выполняем аутентификацию
            var userRole = await _apiService.AuthenticateAsync(login, password);

            // Проверяем результат
            if (userRole != null)
            {
                // Проверяем роль пользователя
                var mainWindow = (MainWindow)Application.Current.MainWindow;

                switch (userRole.Post)
                {
                    case "Руководитель":
                        NavigationService.Navigate(new PageMenu());
                        break;
                    case "Менеджер проекта":
                        NavigationService.Navigate(new ProjectsPageManager());
                        break;
                    case "Сотрудник":
                        NavigationService.Navigate(new ProjectsPageEmployee());
                        break;
                    default:
                        MessageBox.Show("Неизвестная роль пользователя");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
    }
}
