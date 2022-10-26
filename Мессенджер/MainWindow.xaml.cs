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
using System.Data.SQLite;

namespace Мессенджер
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static private SQLiteConnection DB;

        static void Open_BD()
        {
            DB = new SQLiteConnection("DataSource=  C:\\Users\\maksi\\OneDrive\\Рабочий стол\\мессенджер\\Мессенджер\\Мессенджер\\bin\\Debug\\Messendjer.db; Version=3");
            //DB = new SQLiteConnection("Data Source=" + @"C:\\Users\\maksi\\source\repos\\Rest_api\\Rest_api\\bin\\baza sotrydnukov.db" + "; Version=3");
            DB.Open();
        }
        static void Close_BD()
        {
            DB.Close();
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Vhod_Click(object sender, RoutedEventArgs e)
        {
            Vhod window_vhod = new Vhod();
            window_vhod.Show();
            Hide();
        }

        private void btn_zaregestrir_akkayn_Click(object sender, RoutedEventArgs e)
        {
            string Pasw_Zaeg = Pass_txt.Password.ToString();
            string Pasw_Zaeg2 = Pass2_txt.Password.ToString();
            Open_BD();

            if (Login_txt.Text != "" && Pasw_Zaeg != "" && Pasw_Zaeg.Length > 9 && Pasw_Zaeg2 != "" && Pasw_Zaeg2.Length > 9 && Email_txt.Text != "")//Вход в аккаунт
            {

                SQLiteCommand CMD = DB.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
                CMD.CommandText = "SELECT * FROM Registracui WHERE Login LIKE '" + Login_txt.Text + "'";
                SQLiteDataReader SQL = CMD.ExecuteReader();//Возврощает данные, то есть берет одни данные выводит, берет вторые и т.д.
                if (SQL.HasRows)//Проверка вернулось ли что-то, если да то аккаунт уже существует
                {
                    MessageBox.Show("Данный аккаунт уже зарегистрирован!!!");
                    Close_BD();
                }
                else
                {
                    //Создание аккаунта
                    SQLiteCommand CMD1 = DB.CreateCommand();//Команда для добавления
                                                            //Правильный вид записи, программа запомнит что данное обозначение @phone будет tbPhone.Text, и @fio будет tbFIO.Text
                    CMD1.CommandText = "insert into Registracui(Login, Password, Email) values (@Login, @Password, @Email)";//Запрос создания
                    CMD1.Parameters.Add("@Login", System.Data.DbType.String).Value = Login_txt.Text.ToUpper();//ToUpper() - в верхнем регистре будеем хранить записи, для того чтобы организовать регистро независимую выборку - для того чтобы было без разницы как ты записал данноы хоть в маленьм регистре хоть в большом, они всерано выведутся(потомучто SQLite не переводит кирилицу в верхний регистр)
                    CMD1.Parameters.Add("@Password", System.Data.DbType.String).Value = Pasw_Zaeg.ToUpper();//Объявление для передачи ФИО
                    CMD1.Parameters.Add("@Email", System.Data.DbType.String).Value = Email_txt.Text.ToUpper();//Объявление для передачи ФИО
                    CMD1.ExecuteNonQuery();//Выполняет запрос не подрозумевая возвращения никаких значений, это для insert, delete, update - так ка квозврощать они ничего не должны

                    //Для получения id пользователя от нового аккаута, так как мы создали аккаунт и если мы хотим передать стандартную фотографии в базу, а так как база не знает id в нового пользователя мы не знаем, поэтому после регистрации и перед созданием настроек нам нужно запросить id в новь созданного пользователя так как мы незнам его id
                    SQLiteCommand CMD2 = DB.CreateCommand();//Команда для вывода
                    CMD2.CommandText = "SELECT * FROM Registracui WHERE Login LIKE '" + Login_txt.Text + "' AND Password LIKE '" + Pasw_Zaeg + "'";
                    object id_rabotnuk_obj = CMD2.ExecuteScalar();
                    int id_rabotnuk_int = Convert.ToInt32(id_rabotnuk_obj);


                    //Созднание ячейки настроек для акакаунта, для того чтобы передать стандартную фотографию, что бы небыло ошибок при заходе в аккант(профиль), так как картинка берется из класса "Peredathic", а так как аккаунт еще небыл настрон и нету данных на счет фотографий. 
                    SQLiteCommand CMD3 = DB.CreateCommand();
                    CMD3.CommandText = "insert into Informacui_polzovatelei(Id_polzovatela_akkaynta, Name_Photo) values ('" + id_rabotnuk_int + "', '" + "15.jpg" + "')";//Запрос создания
                    CMD3.ExecuteNonQuery();//Выполняет запрос не подрозумевая возвращения никаких значений, это для insert, delete, update - так ка квозврощать они ничего не должны

                    Vhod window_vhod = new Vhod();
                    window_vhod.Show();
                    Hide();
                    Close_BD();
                }

            }
            else if (Pasw_Zaeg != Pasw_Zaeg2)//Если пароли не одинаковые
            {
                MessageBox.Show("Пароль не одинаковые!!!");
            }
            else if (Login_txt.Text.Length == 0 && Email_txt.Text.Length == 0)//Если оба поля свободны
            {
                MessageBox.Show("Поля были не заполнены!!!");
            }
            else if (Login_txt.Text.Length == 0)//Если не заполнено поле для почты
            {
                MessageBox.Show("Вы забыли ввести почту!");
            }
            else if (Pasw_Zaeg.Length < 10)//Обязательная длина пароля
            {
                MessageBox.Show("Некорректно введенный пароль, минимальный размер пароля должен составлять 10 символов!");
            }
            else//Общая ошибка
            {
                MessageBox.Show("Ошибка!!!");
            }
        }

    }
}


