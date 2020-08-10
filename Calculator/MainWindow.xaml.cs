using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
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
using MySql.Data.MySqlClient;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool press = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        //fungsi tombol off
        public void disable()
        {
            layar1.IsEnabled = false;
            mati.Visibility = Visibility.Hidden;
            nyala.Visibility = Visibility.Visible;
            nol.IsEnabled = false;
            satu.IsEnabled = false;
            dua.IsEnabled = false;
            tiga.IsEnabled = false;
            empat.IsEnabled = false;
            lima.IsEnabled = false;
            enam.IsEnabled = false;
            tujuh.IsEnabled = false;
            delapan.IsEnabled = false;
            sembilan.IsEnabled = false;
            tambah.IsEnabled = false;
            kurang.IsEnabled = false;
            kali.IsEnabled = false;
            bagi.IsEnabled = false;
            del.IsEnabled = false;
            alldel.IsEnabled = false;
            masukdb.IsEnabled = false;
            liatdb.IsEnabled = false;
            preor.IsEnabled = false;
            postor.IsEnabled = false;
            dec.IsEnabled = false;
            bina.IsEnabled = false;
            samadengan.IsEnabled = false;
            layar1.Text = "";
            preor.Text = "";
            postor.Text = "";
            dec.Text = "";
            bina.Text = "";
        }

        //fungsi tombol on
        public void enable()
        {
            samadengan.IsEnabled = true;
            layar1.IsEnabled = true;
            mati.Visibility = Visibility.Visible;
            nyala.Visibility = Visibility.Hidden;
            nol.IsEnabled = true;
            satu.IsEnabled = true;
            dua.IsEnabled = true;
            tiga.IsEnabled = true;
            empat.IsEnabled = true;
            lima.IsEnabled = true;
            enam.IsEnabled = true;
            tujuh.IsEnabled = true;
            delapan.IsEnabled = true;
            sembilan.IsEnabled = true;
            tambah.IsEnabled = true;
            kurang.IsEnabled = true;
            kali.IsEnabled = true;
            bagi.IsEnabled = true;
            del.IsEnabled = true;
            alldel.IsEnabled = true;
            masukdb.IsEnabled = true;
            liatdb.IsEnabled = true;
            preor.IsEnabled = true;
            postor.IsEnabled = true;
            dec.IsEnabled = true;
            bina.IsEnabled = true;
        }

        private void mati_Click(object sender, RoutedEventArgs e)
        {
            disable();
        }

        private void nyala_Click(object sender, RoutedEventArgs e)
        {
            enable();
        }

        //angka clicked
        private void angka_Click (object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (layar1.Text == "0")
                layar1.Clear();
            layar1.Text = layar1.Text + button.Content;
            
        }

        //operator clicked
        private void operator_Click(object sender, RoutedEventArgs e)
        {
            if (layar1.Text.Length > 0)
            {
                Button button = (Button)sender;
                layar1.Text = layar1.Text + button.Content;
                
            }
        }

        //allclear button
        private void alldel_Click(object sender, RoutedEventArgs e)
        {
            layar1.Text = "";
            preor.Text = "";
            postor.Text = "";
            dec.Text = "";
            bina.Text = "";

        }

        //backspace button
        private void del_Click(object sender, RoutedEventArgs e)
        {
            preor.Text = "";
            postor.Text = "";
            dec.Text = "";
            bina.Text = "";
            if (layar1.Text.Length > 0)
            {
                if (layar1.Text != "0")
                {
                    layar1.Undo();
                    if (layar1.Text.Length == 0)
                        layar1.Text = "0";
                }
            }
        }
            
        private void masukdb_Click(object sender, RoutedEventArgs e)
        {
            //membuat koneksi mysql
            MySqlConnection koneksi = new MySqlConnection("server=localhost;port=3306;database=kalkulatorutami;uid=root;password=Nyuna;SslMode=none");
            koneksi.Open();
            string sql_find = "Select * from kalkulator where postorder ='" + postor.Text.ToString() + "'";
            //String sql_find = "Select * from kalkulator";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql_find, koneksi);
                MySqlDataReader myData = cmd.ExecuteReader();
                if (!myData.HasRows)
                {
                    myData.Close();
                    //ga ada data 
                    Console.WriteLine("There are no data.");
                    String sql_insert = "Insert into kalkulator(Ekspresi, Preorder, Postorder, Desimal, Binari) values" +
                    "('" + layar1.Text.ToString() + "','" + postor.Text.ToString() +
                    "','" + preor.Text.ToString() + "','" + dec.Text.ToString() +
                    "','" + bina.Text.ToString() + "');";

                    cmd = new MySqlCommand(sql_insert, koneksi);
                    myData = cmd.ExecuteReader();
                    MessageBox.Show("Insert Expression Success!");
                }
                else 
                {
                    MessageBox.Show("Upppss!! There is another expression!");
                    myData.Close();
                }
                myData.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " : " + ex.Message);
            }
            koneksi.Close();
        }

        private void liatdb_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
           PageShow w_data = new PageShow();
            w_data.Show();
        }

        private void samadengan_Click(object sender, RoutedEventArgs e)
        {
            press = true;

            string[] words = layar1.Text.Split(' ');
            Stack<string> check = new Stack<string>();

            int len = words.Length - 1;

            while (len >= 0)
            {
                if (words[len] != "+" && words[len] != "-" && words[len] != "*" && words[len] != "/")
                {
                    preor.Text = " " + words[len] + preor.Text;
                }
                else
                {
                    if (check.Count != 0)
                    {
                        if (words[len] == "*" || words[len] == "/")
                        {
                            check.Push(words[len]);
                        }

                        if (words[len] == "+" || words[len] == "-")
                        {
                            if (check.Peek() == "*" | check.Peek() == "/")
                            {
                                while (check.Count > 0)
                                {
                                    preor.Text = " " + check.Peek() + preor.Text;
                                    check.Pop();
                                }

                                check.Push(words[len]);
                            }
                            else
                            {
                                check.Push(words[len]);
                            }
                        }
                    }
                    else
                    {
                        check.Push(words[len]);
                    }
                }
                len--;
            }
            while (check.Count > 0)
            {
                preor.Text = " " + check.Peek() + preor.Text;
                check.Pop();
            }

            len = 0;

            while (len <= words.Length - 1)
            {
                if (words[len] != "+" && words[len] != "-" && words[len] != "*" && words[len] != "/")
                {
                    postor.Text += words[len] + " ";
                }
                else
                {
                    if (check.Count != 0)
                    {
                        if (words[len] == "*" || words[len] == "/")
                        {
                            if (check.Peek() == "*" | check.Peek() == "/")
                            {
                                postor.Text += check.Peek() + " ";
                                check.Pop();
                                check.Push(words[len]);
                            }
                            else
                            {
                                check.Push(words[len]);
                            }
                        }

                        if (words[len] == "+" || words[len] == "-")
                        {

                            while (check.Count > 0)
                            {
                                postor.Text += check.Peek() + " ";
                                check.Pop();
                            }
                            check.Push(words[len]);
                        }
                    }
                    else
                    {
                        check.Push(words[len]);
                    }
                }
                len++;
            }
            while (check.Count > 0)
            {
                postor.Text += check.Peek() + " ";
                check.Pop();
            }

            DataTable dt = new DataTable();
            string ten_result = dt.Compute(layar1.Text, "false").ToString();

            dec.Text = ten_result;

            bina.Text = Convert.ToString(int.Parse(dec.Text), 2);
        }
    }    
}
