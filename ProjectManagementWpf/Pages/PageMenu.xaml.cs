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
    /// Логика взаимодействия для PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            InitializeComponent();
        }

        private void DoljnostiPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DoljnostiPage());
        }

        private void CompaniesPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CompaniesPage());
        }

        private void EmployeesPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeesPage());
        }

        private void ProjectsPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectsPage());
        }

        private void TasksPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TasksPage());
        }
    }
}
