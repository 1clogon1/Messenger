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
using System.Data.SQLite;

namespace Мессенджер
{
    /// <summary>
    /// Логика взаимодействия для Vhod.xaml
    /// </summary>
    public partial class Vhod : Window
    {
        static private SQLiteConnection DB;

        static void Open_BD()
        {
            DB = new SQLiteConnection("DataSource = C:\\Users\\maksi\\OneDrive\\Рабочий стол\\мессенджер\\Мессенджер\\Мессенджер\\bin\\Debug\\Messendjer.db; Version=3");
            //DB = new SQLiteConnection("Data Source=" + @"C:\\Users\\maksi\\source\repos\\Rest_api\\Rest_api\\bin\\baza sotrydnukov.db" + "; Version=3");
            DB.Open();
        }
        static void Close_BD()
        {
            DB.Close();
        }
        public Vhod()
        {
            InitializeComponent();
        }

        private void btn_per_zareg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window_vhod = new MainWindow();
            window_vhod.Show();
            Hide();
        }

        private void btn_vhod_v_akkaynt_Click(object sender, RoutedEventArgs e)
        {
            string Pasw_vhod = Pasw_txt.Password.ToString();
            Open_BD();
            int id_rabotnuk_int = 0;
            if (Name_txt.Text != "" && Pasw_vhod != "")//Вход в аккаунт
            {
                SQLiteCommand CMD = DB.CreateCommand();//Команда для вывода
                CMD.CommandText = "SELECT * FROM Registracui WHERE Login LIKE '" + Name_txt.Text + "' AND Password LIKE '" + Pasw_vhod + "'";
                object id_rabotnuk_obj = CMD.ExecuteScalar();
                id_rabotnuk_int = Convert.ToInt32(id_rabotnuk_obj);

                if (id_rabotnuk_int != 0)
                {
                    Peredathik.id_rabot_int = id_rabotnuk_int;
                    Profil window2 = new Profil();
                    window2.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Неверно введенные данные");
                }
                Close_BD();
            }
            else if (Name_txt.Text.Length == 0 /*&& Pasw_vhod.Text.Length == 0*/)
            {
                MessageBox.Show("Оба поля были не заполнены!!!");
            }
            else if (Name_txt.Text.Length == 0)
            {
                MessageBox.Show("Поле для почты было не заполненно!");
            }
            else if (Pasw_vhod.Length == 0)
            {
                MessageBox.Show("Поле для пароля было не заполненно!");
            }
            else
            {
                MessageBox.Show("Ошибка!!!");
            }
        }

        /*private void btn_vostonav_parol_Click(object sender, RoutedEventArgs e)
        {

            Nastroika_Profula Nastroika_Profula = new Nastroika_Profula();
            Nastroika_Profula.Show();
            Hide();

            /* SmtpClient Smtp = new SmtpClient("mail.ru", 25);
             Smtp.Credentials = new NetworkCredential("ivanov@mail.ru", "password");
             MailMessage Message = new MailMessage();
             Message.From = new MailAddress("t.t.maxi05092003@gmail.com");
             Message.To.Add(new MailAddress("azazaza08@yandex.ru"));
             Message.Subject = "Сброс пароля";
             Message.Body = "Востонавление пароля 123";
             Smtp.Send(Message);*/


        /*// отправитель - устанавливаем адрес и отображаемое в письме имя
        MailAddress from = new MailAddress("azazaza08@yandex.ru", "СПБ-мессенджер");
        // кому отправляем
        string myAdress = "t.t.maxi05092003@gmail.com";
        MailAddress to = new MailAddress(myAdress);
        // создаем объект сообщения
        MailMessage m = new MailMessage(from, to);
        // тема письма
        m.Subject = "откуда-куда";
        // текст письма
        m.Body = "Привет";
        // письмо представляет код html
        // m.IsBodyHtml = true;
        // адрес smtp-сервера и порт, с которого будем отправлять письмо
        SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
        // логин и пароль
        smtp.Credentials = new NetworkCredential("azazaza08@yandex.ru", "DoterMaxI20032019");
        smtp.EnableSsl = true;
        smtp.Send(m);*/
        //}

        private void btn_Nastroiku_Click(object sender, RoutedEventArgs e)
        {
            Nastroiku window_vhod = new Nastroiku();
            window_vhod.Show();
            Hide();
        }


    }
}