using ProjectLitvin.ClassFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Xml;

namespace ProjectLitvin.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=Litvin;" +
            "Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;

        public Authorization()
        {
            InitializeComponent();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(
                    "SELECT * From dbo.[User] " +
                    $"Where UserName='{LoginTb.Text}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                if (dataReader[2].ToString() != PasswordTb.Text)
                {
                    MBClass.ErrorMB("Неверный пароль");
                    PasswordTb.Focus();
                }
                else
                {
                    switch (dataReader[3].ToString())
                    {
                        case "1":
                            new AdminFolder.Admin().ShowDialog();
                            break;
                        case "2":
                            new GuestWindow.EmployeeGuest().ShowDialog();
                            break;
                        case "3":
                            new SellFolder.ServiceAdmin().ShowDialog();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            new Registration().ShowDialog();
        }        
    }
}
