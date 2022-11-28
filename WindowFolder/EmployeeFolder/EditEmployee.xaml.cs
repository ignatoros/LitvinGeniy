using ProjectLitvin.ClassFolder;
using ProjectLitvin.WindowFolder;
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

namespace ProjectLitvin.WindowFolder.EmployeeFolder
{
    /// <summary>
    /// Логика взаимодействия для EditEmployeePage.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=Litvin;" +
            "Integrated Security=True");
        SqlCommand sqlCommand;
        CBClass cB;
        SqlDataReader dataReader;
        string login;
        public EditEmployee()
        {
            InitializeComponent();
            cB = new CBClass();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            cB.LoadCB(RoleCb, CBClass.CBType.Role);
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * From dbo.Employee " +
                    $"Where IdEmployee='{VarialbleClass.UserId}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                NameTb.Text = dataReader[2].ToString();
                SecondNameTb.Text = dataReader[3].ToString();
                LastNameTb.Text = dataReader[4].ToString();
                NumberTb.Text = dataReader[5].ToString();
                EmailTb.Text = dataReader[6].ToString();
                sqlCommand = new SqlCommand("Select * From dbo.[User] " +
                    $"Where IdUser='{dataReader[1]}'", sqlConnection);
                dataReader.Close();
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                LoginTb.Text = dataReader[1].ToString();
                PasswordTb.Text = dataReader[2].ToString();
                RoleCb.SelectedValue = dataReader[3].ToString();
                login = LoginTb.Text;
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

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            string znak = "!@#$%^&";
            string cif = "1234567890";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string bol = "QWERTYUIOPASDFGHJKLZXCVBNM";

            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Введите логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordTb.Text))
            {
                MBClass.ErrorMB("Введите пароль");
                PasswordTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(NameTb.Text))
            {
                MBClass.ErrorMB("Введите имя");
                NameTb.Focus();
            }
            else if (PasswordTb.Text.IndexOfAny(znak.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать" +
                    " !@#$%^&");
                PasswordTb.Focus();
            }
            else if (PasswordTb.Text.IndexOfAny(cif.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать" +
                    " цифру");
                PasswordTb.Focus();
            }
            else if (PasswordTb.Text.IndexOfAny(mal.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать" +
                    " строчную букву");
                PasswordTb.Focus();
            }
            else if (PasswordTb.Text.IndexOfAny(bol.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать" +
                    " заглавную букву");
                PasswordTb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    int id;
                    sqlCommand = new SqlCommand(
                   "SELECT * From dbo.[User] " +
                   $"Where UserName='{login}'",
                   sqlConnection);
                    dataReader = sqlCommand.ExecuteReader();
                    dataReader.Read();
                    id = int.Parse(dataReader[0].ToString());
                    dataReader.Close();
                    
                    sqlCommand =
                        new SqlCommand("Update " +
                        "dbo.[User] " +
                        $"Set UserName='{LoginTb.Text}'," +
                        $"UserPassword='{PasswordTb.Text}'," +
                        $"IdRole='{RoleCb.SelectedValue.ToString()}' " +
                        $"Where IdUser='{id.ToString()}'",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    
                    sqlCommand =
                        new SqlCommand("Update " +
                        "dbo.Employee " +
                        $"Set IdUser='{id.ToString()}'," +
                        $"EmployeeName='{NameTb.Text}'," +
                        $"EmployeeSecondName='{SecondNameTb.Text}'," +
                        $"EmployeeLastName='{LastNameTb.Text}'," +
                        $"EmployeePhoneNumber='{NumberTb.Text}'," +
                        $"EmployeeEmail='{EmailTb.Text}' " +
                        $"Where IdEmployee='{VarialbleClass.UserId}'",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Данные работника " +
                        $"успешно отредактированы");
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
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new EmployeeAdmin().ShowDialog();
        }
    }
}
