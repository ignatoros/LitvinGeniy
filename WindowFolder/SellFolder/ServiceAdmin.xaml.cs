using ProjectLitvin.ClassFolder;
using ProjectLitvin.WindowFolder;
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

namespace ProjectLitvin.WindowFolder.SellFolder
{
    /// <summary>
    /// Логика взаимодействия для ServiceAdminPage.xaml
    /// </summary>
    public partial class ServiceAdmin : Window
    {
        DGClass dGClass;
        public ServiceAdmin()
        {
            InitializeComponent();
            dGClass = new DGClass(ListUserDG);
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            new AddService().ShowDialog();
        }

        private void BackIm_Click(object sender, RoutedEventArgs e)
        {
            new Authorization().ShowDialog();
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.SaleView " +
                $"Where ProductName Like '%{SearchTb.Text}%'");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.SaleView");
        }

        private void AddTypeIm_Click(object sender, RoutedEventArgs e)
        {
            new EditService().ShowDialog();
        }
    }
}
