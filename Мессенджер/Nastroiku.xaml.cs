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
using Microsoft.Win32;

namespace Мессенджер
{
    /// <summary>
    /// Логика взаимодействия для Nastroiku.xaml
    /// </summary>
    public partial class Nastroiku : Window
    {
        static private SQLiteConnection DB;
        static void Open_BD()
        {
            DB = new SQLiteConnection("DataSource =  C:\\Users\\maksi\\OneDrive\\Рабочий стол\\мессенджер\\Мессенджер\\Мессенджер\\bin\\Debug\\Messendjer.db; Version=3");
            //DB = new SQLiteConnection("Data Source=" + @"C:\\Users\\maksi\\source\repos\\Rest_api\\Rest_api\\bin\\baza sotrydnukov.db" + "; Version=3");
            DB.Open();
        }
        static void Close_BD()
        {
            DB.Close();
        }
        public static void Vuvod_dannuh()
        {
            /*Open_BD();
             SQLiteCommand CMD = DB.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
             CMD.CommandText = "SELECT * FROM Informacui_polzovatelei WHERE Id_polzovatela_akkaynta LIKE '" + Peredathik.id_rabot_int + "'";
             SQLiteDataReader SQL = CMD.ExecuteReader();//Возврощает данные, то есть берет одни данные выводит, берет вторые и т.д.
             if (SQL.HasRows)//Проверка вернулось ли что-то
             {
                 while (SQL.Read())//Когда указател закончит читать
                 {
                     object Name = SQL["Name"];
                     Name_txt.Text = Convert.ToString(Name);
                 }
             }
             Close_BD();*/
        }

        public Nastroiku()
        {
            InitializeComponent();
            //Name_Polzovat.Text = "Трифонов Максим";
            ___Photo_Proful_.Source = new BitmapImage(new Uri("13.jpg", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото

            //Заполнение данных пользователя в настройках
            Open_BD();
            SQLiteCommand CMD = DB.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
            CMD.CommandText = "SELECT * FROM Informacui_polzovatelei WHERE Id_polzovatela_akkaynta LIKE '" + Peredathik.id_rabot_int + "'";
            SQLiteDataReader SQL = CMD.ExecuteReader();//Возврощает данные, то есть берет одни данные выводит, берет вторые и т.д.
            if (SQL.HasRows)//Проверка вернулось ли что-то
            {
                while (SQL.Read())//Когда указател закончит читать
                {
                    //object Name = SQL["Name"];
                    Name_txt.Text = Convert.ToString(SQL["Name"]);
                    Familia_txt.Text = Convert.ToString(SQL["Familia"]);
                    Othestvo_txt.Text = Convert.ToString(SQL["Othestvo"]);
                    Pol_txt.Text = Convert.ToString(SQL["Pol"]);
                    Years_txt.Text = Convert.ToString(SQL["Age"]);
                    Opusanue_txt.Text = Convert.ToString(SQL["Text_opusanue"]);
                    Semeinoe_polojen_txt.Text = Convert.ToString(SQL["Marital_status"]);
                    Unteresu_txt.Text = Convert.ToString(SQL["Text_enteresu"]);
                    Rabota_yheba_txt.Text = Convert.ToString(SQL["Work_and_study"]);
                }
            }
            Close_BD();
        }

        private void btn_zagyzut_foto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f1 = new OpenFileDialog();//Открываем файловый диалог
            f1.Filter = "Image Files(*.png;*.jpeg;*.bmp;*.jpg;)|*.png;*.jpeg;*.bmp;*.jpg;|AllFiles(*.*)|*.*";//Передаем типы файлов с которыми мы хотим работать//AllFiles(*.*) - для всех файлов, Files(*.png;*.jpeg;*.bmp;*.jpg;) - определнный тип фалов
            //Image Files(*.png; *.jpeg; *.bmp; *.jpg;) - то что мы видим, |*.png;*.jpeg;*.bmp;*.jpg;| - то что передается в программу для опредеоения
            if (f1.ShowDialog() != DialogResult)
            {
                ___Photo_Proful_.Source = new BitmapImage(new Uri(f1.SafeFileName, UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото из внутренних файлов приложения
            }
            Open_BD();
            SQLiteCommand CMD1 = DB.CreateCommand();
            CMD1.CommandText = "update Informacui_polzovatelei set  Name_Photo = '" + f1.SafeFileName + "'  where Id_polzovatela_akkaynta = '" + Peredathik.id_rabot_int + "'";//Запрос создания
            SQLiteDataReader SQL1 = CMD1.ExecuteReader();
            Close_BD();
        }


        private void btn_save_Click(object sender, RoutedEventArgs e)
        {

            Open_BD();

            if (Name_txt.Text != "" && Familia_txt.Text != "" && Othestvo_txt.Text != "")//Вход в аккаунт
            {

                SQLiteCommand CMD = DB.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
                CMD.CommandText = "SELECT * FROM Informacui_polzovatelei WHERE Id_polzovatela_akkaynta LIKE '" + Peredathik.id_rabot_int + "'";
                SQLiteDataReader SQL = CMD.ExecuteReader();//Возврощает данные, то есть берет одни данные выводит, берет вторые и т.д.
                if (SQL.HasRows)//Проверка вернулось ли что-то, если да то аккаунт уже существует
                {
                    SQLiteCommand CMD1 = DB.CreateCommand();
                    CMD1.CommandText = "update Informacui_polzovatelei set Name = '" + Name_txt.Text + "', Familia = '" + Familia_txt.Text + "', Othestvo = '" + Othestvo_txt.Text + "', Pol = '" + Pol_txt.Text + "', Age = '" + Years_txt.Text + "', Text_opusanue = '" + Opusanue_txt.Text + "', Marital_status = '" + Semeinoe_polojen_txt.Text + "', Text_enteresu = '" + Unteresu_txt.Text + "', Work_and_study = '" + Rabota_yheba_txt.Text + "'  where Id_polzovatela_akkaynta = '" + Peredathik.id_rabot_int + "'";//Запрос создания
                    SQLiteDataReader SQL1 = CMD1.ExecuteReader();
                    Close_BD();
                }
                else
                {
                    SQLiteCommand CMD1 = DB.CreateCommand();
                    CMD1.CommandText = "insert into Informacui_polzovatelei(Id_polzovatela_akkaynta, Name, Familia, Othestvo, Pol, Age,Text_opusanue, Marital_status, Text_enteresu, Work_and_study) values ('" + Peredathik.id_rabot_int + "','" + Name_txt.Text + "', '" + Familia_txt.Text + "', '" + Othestvo_txt.Text + "', '" + Pol_txt.Text + "', '" + Years_txt.Text + "', '" + Opusanue_txt.Text + "', '" + Semeinoe_polojen_txt.Text + "', '" + Unteresu_txt.Text + "', '" + Rabota_yheba_txt.Text + "')";//Запрос создания
                    CMD1.ExecuteNonQuery();//Выполняет запрос не подрозумевая возвращения никаких значений, это для insert, delete, update - так ка квозврощать они ничего не должны
                    Close_BD();
                }
            }
            else if (Name_txt.Text.Length != 0)//Если не ввели имя
            {
                MessageBox.Show("Не ввели имя");
            }
            else if (Familia_txt.Text.Length != 0)//Если не ввели имя
            {
                MessageBox.Show("Не ввели Фамилию");
            }
            else if (Othestvo_txt.Text.Length != 0)//Если не ввели имя
            {
                MessageBox.Show("Не ввели отчество");
            }
            else//Общая ошибка
            {
                MessageBox.Show("Ошибка!!!");
            }
        }

        private void btn_vuhod_Vhod_Click(object sender, RoutedEventArgs e)
        {
            Profil window_vhod = new Profil();
            window_vhod.Show();
            Hide();
        }
    }
}


