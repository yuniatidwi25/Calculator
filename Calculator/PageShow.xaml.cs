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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for PageShow.xaml
    /// </summary>
    public partial class PageShow : Window
    {
        public PageShow()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            //membuat koneksi mysql
            MySqlConnection koneksi = new MySqlConnection("server=localhost;port=3306;database=kalkulatorutami;uid=root;password=Nyuna;SslMode=none");
            koneksi.Open();
            String sql_find = "Select * from kalkulator;";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql_find, koneksi);
                MySqlDataReader myData = cmd.ExecuteReader();
                if (!myData.HasRows)
                {
                    myData.Close();
                    //ga ada data 
                    Console.WriteLine("There are no data.");
                }
                else
                {
                    while (myData.Read())
                    {
                        String d_id = myData.GetString(0);
                        String d_ekspresi = myData.GetString(1);
                        String d_preorder = myData.GetString(2);
                        String d_postorder = myData.GetString(3);
                        String d_dec = myData.GetString(4);
                        String d_bina = myData.GetString(5);

                        result_table.Items.Add(new { id = d_id, Ekspresi = d_ekspresi, Preorder = d_preorder, Postorder = d_postorder, Desimal = d_dec, Binari = d_bina });
                    }
                }
                myData.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " : " + ex.Message);
            }
            koneksi.Close();
        }

        private void balik_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void hapus_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = result_table.SelectedItem;
            string id = (result_table.SelectedCells[0].Column.GetCellContent(selectedItem) as TextBlock).Text;
            if (selectedItem != null)
            {
                MySqlConnection koneksi = new MySqlConnection("server=localhost;port=3306;database=kalkulatorutami;uid=root;password=;SslMode=none");
                koneksi.Open();
                String sql_del = "Delete from kalkulator where id=" + id;
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sql_del, koneksi);
                    MySqlDataReader myData = cmd.ExecuteReader();
                    myData.Close();
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    Console.WriteLine("Error " + ex.Number + " : " + ex.Message);
                }

                koneksi.Close();
                result_table.Items.Remove(selectedItem);
            }
        }
    }
}
