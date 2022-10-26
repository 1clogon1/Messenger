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
    /// Логика взаимодействия для Friends.xaml
    /// </summary>
    public partial class Friends : Window
    {
        static private SQLiteConnection DB;
        static private SQLiteConnection DB2;
        Random rnd = new Random();
        public static int[] Random_polzovat_MAS = new int[5];//Массив для хранения рандомных id
        public static string[] name_MAS = new string[5];//Для хранения имен пользователей
        public static string[] photo_MAS = new string[5];//Для хранения названийфотографий пользователей
        public static int Nomer_Heloveka = 0;//Через данную переменную будет передаватся номер человека из списка в зависимости от кнопки
        public static int New_Artukyl_MAx = 0;//Новый артикуль передаем в даннцую переменную, для того чтобы создать переписку при добавление пользователя в друзья

        public static int Proverka_Artukyl = 0;//Переменная для хранения номера артикуля для проверки есть ли у данных друзей переписка в таблице для сообщений

        //Подключение к первой базе
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

        //Подключение ко второй базе
        static void Open_BD_Soobhen()
        {
            DB2 = new SQLiteConnection("DataSource=  C:\\Users\\maksi\\OneDrive\\Рабочий стол\\мессенджер\\Мессенджер\\Мессенджер\\bin\\Debug\\soohenui.db; Version=3");
            //DB = new SQLiteConnection("Data Source=" + @"C:\\Users\\maksi\\source\repos\\Rest_api\\Rest_api\\bin\\baza sotrydnukov.db" + "; Version=3");
            DB2.Open();
        }
        static void Close_BD_Soobhen()
        {
            DB2.Close();
        }
        /*static void Photo_polzovat_id()
        {
            Open_BD();
            SQLiteCommand CMD1 = DB.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
            CMD1.CommandText = "SELECT Name_Photo FROM Informacui_polzovatelei WHERE Id_polzovatela_akkaynta LIKE '" + Peredathik.id_rabot_int + "'";
            SQLiteDataReader SQL1 = CMD1.ExecuteReader();//Возврощает данные, то есть берет одни данные выводит, берет вторые и т.д.

            while (SQL1.Read())//Когда указател закончит читать
            {
                Peredathik.Name_id_photo_polzovatela = Convert.ToString(SQL1["Name_Photo"]);
            }
            Close_BD();
        }*/

        public Friends()
        {
            InitializeComponent();
            random_polzovatelu();
            Name1_txt.Text = name_MAS[0];
            Photo_Proful_1.Source = new BitmapImage(new Uri(photo_MAS[0], UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото

            Name2_txt.Text = name_MAS[1];
            Photo_Proful_2.Source = new BitmapImage(new Uri(photo_MAS[1], UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото

            Name3_txt.Text = name_MAS[2];
            Photo_Proful_3.Source = new BitmapImage(new Uri(photo_MAS[2], UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото

            Name4_txt.Text = name_MAS[3];
            Photo_Proful_4.Source = new BitmapImage(new Uri(photo_MAS[3], UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото

            Name5_txt.Text = name_MAS[4];
            Photo_Proful_5.Source = new BitmapImage(new Uri(photo_MAS[4], UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото

        }

        //Получение артикуля для перехода в чат для даннх пользователей
        public static void Artukel_Soobhen()
        {
            Open_BD();
            SQLiteCommand CMD1 = DB.CreateCommand();//Команда для добавления
            CMD1.CommandText = "SELECT Artukyl FROM friends WHERE ID_1 = '" + Peredathik.id_1_soobhen + "' and ID_2 = '" + Peredathik.id_2_soobhen + "' or ID_1 = '" + Peredathik.id_2_soobhen + "' and ID_2 = '" + Peredathik.id_1_soobhen + "'";
            object Artukel_Perepusku_obj = CMD1.ExecuteScalar();
            Peredathik.Artukyl_peredaha = Convert.ToInt32(Artukel_Perepusku_obj);
            SQLiteDataReader SQL1 = CMD1.ExecuteReader();
            Close_BD();
        }


        static void random_polzovatelu()
        {

            Random rnd = new Random();//Рандом
            int switchh = 0;//Для хранения числа для case в switch
            Open_BD();
            int shethik_rand = 0;
            int random_polzovatel = 0;
            int povtoru = 0;
            SQLiteCommand CMD = DB.CreateCommand();//Команда для добавления
            CMD.CommandText = "SELECT max(ID_Polzovatela) from Registracui";//Запрос для получение максимального id
            object result = CMD.ExecuteScalar();
            int ID_polzovatela_max = Convert.ToInt32(result);//Максимальное id
            SQLiteDataReader SQL = CMD.ExecuteReader();
            Close_BD();



            for (; shethik_rand < 5;)//Пока не будет 5 записанных чисел работает цикл
            {
                povtoru = 0;//Количество повторов, для того чтобы осчитать сколько чисел не совпало, и если из 5 мест их будет 5 товсе хорошо и записываем число
                random_polzovatel = rnd.Next(4, ID_polzovatela_max);//Рандомные пользователи
                for (int j = 0; j < 5; j++)
                {
                    if (Random_polzovat_MAS[j] != random_polzovatel && Random_polzovat_MAS[j] != Peredathik.id_rabot_int)//Проверка сколько чисел не совпало, а также условие чтобы сам человек не попадал в список рандомных людей
                    {
                        povtoru++;
                    }
                }

                if (povtoru == 5)//Если 5 чисел не совпали, то записываем передаем число в массив
                {
                    Random_polzovat_MAS[shethik_rand] = random_polzovatel;//Передача числа в массив
                    shethik_rand++; //Счетчик для мест в массиве
                }
            }

            //Передпача в массив имени и фамилии, а также названия фотографии
            for (int i = 0; i < 5; i++)
            {
                Open_BD();
                SQLiteCommand CMD1 = DB.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
                CMD1.CommandText = "SELECT * FROM Informacui_polzovatelei WHERE Id_polzovatela_akkaynta LIKE '" + Random_polzovat_MAS[i] + "'";
                SQLiteDataReader SQL1 = CMD1.ExecuteReader();//Возврощает данные, то есть берет одни данные выводит, берет вторые и т.д.
                if (SQL1.HasRows)//Проверка вернулось ли что-то
                {
                    while (SQL1.Read())//Когда указател закончит читать
                    {
                        //object Name = SQL["Name"];
                        name_MAS[i] = Convert.ToString(SQL1["Name"] + " " + SQL1["Familia"]);
                    }
                }
                Close_BD();


                Open_BD();
                SQLiteCommand CMD2 = DB.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
                CMD2.CommandText = "SELECT Name_Photo FROM Informacui_polzovatelei WHERE Id_polzovatela_akkaynta LIKE '" + Random_polzovat_MAS[i] + "'";
                SQLiteDataReader SQL2 = CMD2.ExecuteReader();//Возврощает данные, то есть берет одни данные выводит, берет вторые и т.д.

                while (SQL2.Read())//Когда указател закончит читать
                {
                    photo_MAS[i] = Convert.ToString(SQL2["Name_Photo"]);
                }
                Close_BD();
            }
        }


        private void btn_vuhod_Vhod_Click(object sender, RoutedEventArgs e)
        {
            Profil Profil_vhod = new Profil();
            Profil_vhod.Show();
            Hide();
        }

        private void btn_vuhod_new_polzova_Click(object sender, RoutedEventArgs e)
        {

            random_polzovatelu();
            //Вывод данных
            Name1_txt.Text = name_MAS[0];
            Photo_Proful_1.Source = new BitmapImage(new Uri(photo_MAS[0], UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото

            Name2_txt.Text = name_MAS[1];
            Photo_Proful_2.Source = new BitmapImage(new Uri(photo_MAS[1], UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото

            Name3_txt.Text = name_MAS[2];
            Photo_Proful_3.Source = new BitmapImage(new Uri(photo_MAS[2], UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото

            Name4_txt.Text = name_MAS[3];
            Photo_Proful_4.Source = new BitmapImage(new Uri(photo_MAS[3], UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото

            Name5_txt.Text = name_MAS[4];
            Photo_Proful_5.Source = new BitmapImage(new Uri(photo_MAS[4], UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото


        }

        //Получение последнего артикуля для того чтобы сделать ++, и полукчить новый артикуля для создания переписки при добавлении в друзья(то есть уникальный артикуль)
        public void New_Artukyl()
        {
            Open_BD();
            SQLiteCommand CMD1 = DB.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
            CMD1.CommandText = "SELECT max(Artukyl) FROM friends";
            object Artukyl_Max = CMD1.ExecuteScalar();
            New_Artukyl_MAx = Convert.ToInt32(Artukyl_Max);
            New_Artukyl_MAx++;//Новый артикуль
            CMD1.ExecuteNonQuery();
            Close_BD();
        }

        //Передача в базу Sobhenui артикуль и id ползователей, для создания между ними раздела для переписок
        public void Sozdanue_tablicu_soobhenui_new_polzovateley()
        {
            Open_BD_Soobhen();
            //string ID_soobhen = "'\'ID_soobhen'\'";
            SQLiteCommand CMD4 = DB2.CreateCommand();
            CMD4.CommandText = "CREATE TABLE '" + New_Artukyl_MAx + "' ('ID_soobhen'    INTEGER, 'ID_1'  INTEGER NOT NULL, 'Name'  TEXT NOT NULL, 'soobhen'   TEXT NOT NULL,'data_time'  TEXT NOT NULL)";//, PRIMARY KEY('" + ID_soobhen+"' AUTOINCREMENT)
            CMD4.ExecuteNonQuery();
            Close_BD_Soobhen();
        }

        public void Sozdanue_tablicu_soobhenui_new_polzovateley2(int Proverka_Artukyl)
        {

            Open_BD_Soobhen();
            SQLiteCommand CMD1 = DB2.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
            CMD1.CommandText = "SELECT * FROM friends WHERE ID_1 = '" + Peredathik.id_rabot_int + "' and ID_2 = '" + Random_polzovat_MAS[Nomer_Heloveka] + "' or ID_1 = '" + Random_polzovat_MAS[Nomer_Heloveka] + "' and ID_2 = '" + Peredathik.id_rabot_int + "'";
            SQLiteDataReader SQL1 = CMD1.ExecuteReader();//Возврощает данные, то есть берет одни данные выводит, берет вторые и т.д.
            if (SQL1.HasRows)//Проверка вернулось ли что-то
            {
                MessageBox.Show("Данный пользователь у вас в друзьях");
            }
            else //Создание друзей, то есть передача в таблицу друзей их ID
            {
                SQLiteCommand CMD2 = DB2.CreateCommand();
                CMD2.CommandText = "insert into friends(ID_1, ID_2, Artukyl) values ('" + Peredathik.id_rabot_int + "','" + Random_polzovat_MAS[Nomer_Heloveka] + "','" + New_Artukyl_MAx + "')"; //Запрос создания
                //CMD2.CommandText = "insert into friends(ID_1, ID_2) values ('" + Peredathik.id_rabot_int + "','" + Random_polzovat_MAS[Nomer_Heloveka] + "')"; //Запрос создания
                CMD2.ExecuteNonQuery();//Выполняет запрос не подрозумевая возвращения никаких значений, это для insert, delete, update - так ка квозврощать они ничего не должны
                Sozdanue_tablicu_soobhenui_new_polzovateley();//Создание таблицы в базе данных для хренения сообщений, для создания переписки меджду двумя пользователями при добавлении в друзья
            }
            //string ID_soobhen = "'\'ID_soobhen'\'";
            SQLiteCommand CMD4 = DB2.CreateCommand();
            CMD4.CommandText = "CREATE TABLE '" + Proverka_Artukyl + "' ('ID_soobhen'    INTEGER, 'ID_1'  INTEGER NOT NULL, 'Name'  TEXT NOT NULL, 'soobhen'   TEXT NOT NULL)";//, PRIMARY KEY('" + ID_soobhen+"' AUTOINCREMENT)
            //CMD1.CommandText = "insert into '" + Proverka_Artukyl + "'(ID_1, Name, soobhen) values ('" + 0+ "', '" + "Администрация" + "', '" + "Добро пожаловать в вашу личную переписку" + "')"; //Запрос создания первого сообщения для каждой переписки, для того чтобы обойти вопрос с тем что так как при создани переписки там нету сообщений,  то при заходи в сообщения функция вычисляющая количество сообщений дл явывода ничего не сможет получить, а так как присвоить пустоту в переменную int нельзя, то создаем специальное первое сообеие от администрации, котолрое и даст нам возможность уйти от пустоты в чате и поприветствоавть новых пользователей
            CMD4.ExecuteNonQuery();
            Close_BD_Soobhen();
        }


        //Функция для добавления в друзья
        public void Dobavlenue_V_dryzi(int Nomer_Heloveka)
        {
            New_Artukyl();//Получение новогоартикуля
            //Проверка нет ли таких пользователей уже в таблице друзей
            Open_BD();
            SQLiteCommand CMD1 = DB.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
            CMD1.CommandText = "SELECT * FROM friends WHERE ID_1 = '" + Peredathik.id_rabot_int + "' and ID_2 = '" + Random_polzovat_MAS[Nomer_Heloveka] + "' or ID_1 = '" + Random_polzovat_MAS[Nomer_Heloveka] + "' and ID_2 = '" + Peredathik.id_rabot_int + "'";
            SQLiteDataReader SQL1 = CMD1.ExecuteReader();//Возврощает данные, то есть берет одни данные выводит, берет вторые и т.д.
            if (SQL1.HasRows)//Проверка вернулось ли что-то
            {
                MessageBox.Show("Данный пользователь у вас в друзьях");
            }
            else //Создание друзей, то есть передача в таблицу друзей их ID
            {
                SQLiteCommand CMD2 = DB.CreateCommand();
                CMD2.CommandText = "insert into friends(ID_1, ID_2, Artukyl) values ('" + Peredathik.id_rabot_int + "','" + Random_polzovat_MAS[Nomer_Heloveka] + "','" + New_Artukyl_MAx + "')"; //Запрос создания
                //CMD2.CommandText = "insert into friends(ID_1, ID_2) values ('" + Peredathik.id_rabot_int + "','" + Random_polzovat_MAS[Nomer_Heloveka] + "')"; //Запрос создания
                CMD2.ExecuteNonQuery();//Выполняет запрос не подрозумевая возвращения никаких значений, это для insert, delete, update - так ка квозврощать они ничего не должны
                Sozdanue_tablicu_soobhenui_new_polzovateley();//Создание таблицы в базе данных для хренения сообщений, для создания переписки меджду двумя пользователями при добавлении в друзья
            }
            Close_BD();

        }

        //Каждая кнопка имеет счвой номер, за счет которгго и будет оппределтся какого человека мы выбираем
        private void btn_friend_1_Click(object sender, RoutedEventArgs e)
        {
            Nomer_Heloveka = 0;
            Dobavlenue_V_dryzi(Nomer_Heloveka);
        }

        private void btn_friend_2_Click(object sender, RoutedEventArgs e)
        {
            Nomer_Heloveka = 1;
            Dobavlenue_V_dryzi(Nomer_Heloveka);
        }

        private void btn_friend_3_Click(object sender, RoutedEventArgs e)
        {
            Nomer_Heloveka = 2;
            Dobavlenue_V_dryzi(Nomer_Heloveka);
        }

        private void btn_friend_4_Click(object sender, RoutedEventArgs e)
        {
            Nomer_Heloveka = 3;
            Dobavlenue_V_dryzi(Nomer_Heloveka);
        }

        private void btn_friend_5_Click(object sender, RoutedEventArgs e)
        {
            Nomer_Heloveka = 4;
            Dobavlenue_V_dryzi(Nomer_Heloveka);
        }





        //Функция для переход в сообщения с данным пользователем
        private void Perehod()
        {
            /* Open_BD();
             SQLiteCommand CMD1 = DB.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
             CMD1.CommandText = "SELECT Artukyl FROM friends WHERE ID_1 = '" + Peredathik.id_rabot_int + "' and ID_2 = '" + Random_polzovat_MAS[Nomer_Heloveka] + "' or ID_1 = '" + Random_polzovat_MAS[Nomer_Heloveka] + "' and ID_2 = '" + Peredathik.id_rabot_int + "'";
             SQLiteDataReader SQL1 = CMD1.ExecuteReader();//Возврощает данные, то есть берет одни данные выводит, берет вторые и т.д.
             if (SQL1.HasRows)//Проверка вернулось ли что-то
             {
                 object Artukyl = CMD1.ExecuteScalar();
                 Proverka_Artukyl = Convert.ToInt32(Artukyl);
                 CMD1.ExecuteNonQuery();
                 Sozdanue_tablicu_soobhenui_new_polzovateley2(Proverka_Artukyl);//Создание таблицы в базе данных для хренения сообщений, для создания переписки меджду двумя пользователями при добавлении в друзья


             }
             else //Создание друзей, то есть передача в таблицу друзей их ID
             {
                 MessageBox.Show("У вса нету данного пользователя в друзьях");
             }
             Close_BD();*/

            Perepuska friends = new Perepuska();
            friends.Show();
            Hide();
        }
        //Переход в сообщения
        private void btn_Soobhen_1_Click(object sender, RoutedEventArgs e)
        {
            Nomer_Heloveka = 0;
            Peredathik.id_1_soobhen = Peredathik.id_rabot_int;
            Peredathik.id_2_soobhen = Random_polzovat_MAS[Nomer_Heloveka];
            Artukel_Soobhen();//Переждача артикула для сообщений
            Perehod();
        }

        private void btn_Soobhen_2_Click(object sender, RoutedEventArgs e)
        {
            Nomer_Heloveka = 1;
            Peredathik.id_1_soobhen = Peredathik.id_rabot_int;
            Peredathik.id_2_soobhen = Random_polzovat_MAS[Nomer_Heloveka];
            Artukel_Soobhen();//Переждача артикула для сообщений
            Perehod();
        }

        private void btn_Soobhen_3_Click(object sender, RoutedEventArgs e)
        {
            Nomer_Heloveka = 2;
            Peredathik.id_1_soobhen = Peredathik.id_rabot_int;
            Peredathik.id_2_soobhen = Random_polzovat_MAS[Nomer_Heloveka];
            Artukel_Soobhen();//Переждача артикула для сообщений
            Perehod();
        }

        private void btn_Soobhen_4_Click(object sender, RoutedEventArgs e)
        {
            Nomer_Heloveka = 3;
            Peredathik.id_1_soobhen = Peredathik.id_rabot_int;
            Peredathik.id_2_soobhen = Random_polzovat_MAS[Nomer_Heloveka];
            Artukel_Soobhen();//Переждача артикула для сообщений
            Perehod();
        }

        private void btn_Soobhen_5_Click(object sender, RoutedEventArgs e)
        {
            Nomer_Heloveka = 4;
            Peredathik.id_1_soobhen = Peredathik.id_rabot_int;
            Peredathik.id_2_soobhen = 10;
            Artukel_Soobhen();//Переждача артикула для сообщений
            Perehod();
        }
    }
}
