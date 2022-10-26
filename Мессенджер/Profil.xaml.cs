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
    /// Логика взаимодействия для Profil.xaml
    /// </summary>
    public partial class Profil : Window
    {

        static private SQLiteConnection DB;
        static private SQLiteConnection DB3;
        public static int New_post_MAx;
        static void Open_BD()
        {
            DB = new SQLiteConnection("DataSource = C:\\Users\\maksi\\OneDrive\\Рабочий стол\\мессенджер\\Мессенджер\\Мессенджер\\bin\\Debug\\Messendjer.db; Version=3");
            //DB = new SQLiteConnection("DataSource = Messendjer.db; Version=3");
            //DB = new SQLiteConnection("Data Source=" + @"C:\\Users\\maksi\\source\repos\\Rest_api\\Rest_api\\bin\\baza sotrydnukov.db" + "; Version=3");
            DB.Open();
        }
        static void Close_BD()
        {
            DB.Close();
        }

        
        static void Open_BD_post()
        {
            DB3 = new SQLiteConnection("DataSource =  C:\\Users\\maksi\\OneDrive\\Рабочий стол\\мессенджер\\Мессенджер\\Мессенджер\\bin\\Debug\\Post_na_stranuce.db; Version=3");
            //DB = new SQLiteConnection("Data Source=" + @"C:\\Users\\maksi\\source\repos\\Rest_api\\Rest_api\\bin\\baza sotrydnukov.db" + "; Version=3");
            DB3.Open();
        }
        static void Close_BD_post()
        {
            DB3.Close();
        }

        static void Photo_polzovat_id()
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
        }



        //Количество постов
        public static void New_nom_post()//При загрузке страницы я получаю последний номер сообщения, для того чтобы в дальнейшем увеличивать его на еденицу создавая новый пост
        {
            Open_BD_post();
            SQLiteCommand CMD1 = DB3.CreateCommand();//Команда для добавления
            CMD1.CommandText = "SELECT Max(N_posta) FROM '" + Peredathik.id_rabot_int + "'";
            object Kol_postov_obj = CMD1.ExecuteScalar();
            string Kol_postov_str = Convert.ToString(Kol_postov_obj);

            //object Kol_Soobhen_obj_prover;//Данная переменнуя нужна для того чтобы указать проверку, то есть если наша переменная равна пустате такде как и пустая переменная object Kol_Soobhen_obj_prover, то присваиваем ноль
            if (Kol_postov_str == "")//Тaк как если люди еще не общались, то у них нету сообющений, для этого нужно проверить если сообщений нету, нужно передать в место null - ноль, для того чтобы небыло ошибки, так как null нельзя конвертировать в int
            {
                New_post_MAx = 0;
            }
            else
            {
                New_post_MAx = Convert.ToInt32(Kol_postov_obj);
            }
            CMD1.ExecuteNonQuery();
            Close_BD_post();
        }


        public Profil()
        {
            InitializeComponent();

            New_nom_post();//При загрузке страницы я получаю последний номер сообщения, для того чтобы в дальнейшем увеличивать его на еденицу создавая новый пост

            Photo_polzovat_id();//Вызов функции которая зугружает фото ползователя
            //Name_Polzovat.Text = "Трифонов Максим";
            ___Photo_Proful_.Source = new BitmapImage(new Uri(Peredathik.Name_id_photo_polzovatela, UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };//ЗАгрузка фото
            Open_BD();
            SQLiteCommand CMD = DB.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
            CMD.CommandText = "SELECT * FROM Informacui_polzovatelei WHERE Id_polzovatela_akkaynta LIKE '" + Peredathik.id_rabot_int + "'";
            SQLiteDataReader SQL = CMD.ExecuteReader();//Возврощает данные, то есть берет одни данные выводит, берет вторые и т.д.
            if (SQL.HasRows)//Проверка вернулось ли что-то
            {
                while (SQL.Read())//Когда указател закончит читать
                {
                    //object Name = SQL["Name"];
                    Peredathik.Name_id_1 = Convert.ToString(SQL["Name"] + " " + SQL["Familia"]);//Передаем имя для дальнейшего использования
                    Name_Polzovat.Text = Convert.ToString(SQL["Name"] + " " + SQL["Familia"]);
                    Year_txt.Text = Convert.ToString(SQL["Age"]);
                    Rabot_yheb_txt.Text = Convert.ToString(SQL["Work_and_study"]);
                    Interesu_txt.Text = Convert.ToString(SQL["Text_enteresu"]);
                }
            }
            Close_BD();

            Open_BD_post();
            string txt_Post = "";
            SQLiteCommand CMD2 = DB3.CreateCommand();//Команда для добавления
            CMD2.CommandText = "SELECT  text_post, data_and_time From '" + Peredathik.id_rabot_int + "'";//Запрос на вывод новых сообщений если подошел id, а также если количество сообщений изменилось, то начиная от того сообщения на котором последний раз выводили, выводим все остальные после него, для того чтобы сделаь какбы дописывание сообщений, а не перезаписывание всех данных, и при условие если сообщений много это будет нагрудать систему, так как нужно будет постоянно перезаписывать и выводить все сообщения

            SQLiteDataReader SQL2 = CMD2.ExecuteReader();

            if (SQL2.HasRows)//Проверка вернулось ли что-то
            {
                while (SQL2.Read())//Когда указател закончит читать
                {
                    for (int i = 0; i < 1; i++)
                    {
                        txt_Post ="" + SQL2["text_post"]+"\n"+ SQL2["data_and_time"];//В таком виде будут выводится задания

                        //Заполняем данные в ячейки для сообщений
                        Label label = new Label();
                        label.Content = "" + txt_Post;
                        Post.Children.Add(label);//все остальные элементы добавляются по аналогии

                    }
                }
            }
            Close_BD_post();

        }

        private void btn_Vhod_Click(object sender, RoutedEventArgs e)
        {
            Nastroiku window_vhod = new Nastroiku();
            window_vhod.Show();
            Hide();
        }

        private void Year_txt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_Friends_Click(object sender, RoutedEventArgs e)
        {
            Friends friends = new Friends();
            friends.Show();
            Hide();
        }

        private void btn_Perepuska_Click(object sender, RoutedEventArgs e)
        {
            Perepuska friends = new Perepuska();
            friends.Show();
            Hide();
        }



       
        private void btn_otpravut_post_Click(object sender, RoutedEventArgs e)
        {
            if (Post_txt.Text.Length == 0)
            {
                MessageBox.Show("Нельзя публиковать пустое пост!");
            }
            else
            {
                Open_BD_post();
                SQLiteCommand CMD3 = DB3.CreateCommand();//Команда для добавления
                CMD3.CommandText = "SELECT Max(N_posta) FROM '" + Peredathik.id_rabot_int + "'";
                SQLiteDataReader SQL1 = CMD3.ExecuteReader();//Возврощает данные, то есть берет одни данные выводит, берет вторые и т.д.
                if (SQL1.HasRows)//Проверка вернулось ли что-то
                {
                    New_post_MAx++;//Новый номер сообщения
                    SQLiteCommand CMD6 = DB3.CreateCommand();
                    CMD6.CommandText = "insert into '" + Peredathik.id_rabot_int + "'(N_posta, text_post,data_and_time) values ('" + New_post_MAx + "', '" + Post_txt.Text + "','" + DateTime.Now + "')"; //Запрос создания
                    CMD6.ExecuteNonQuery();//Выполняет запрос не подрозумевая возвращения никаких значений, это для insert, delete, update - так ка квозврощать они ничего не должны

                    //Заполняем данные в ячейки для поста, так как посты мы выводим заходя на страницу, то мы можем при выкладке сообщения добобовлять его визуально на страницу при отправке на базу, а уже после если мы и перезайдем, то будет тоже самое. То есть что бы каждый раз не обновлять посты, что бы их видеть нужно их какбы выгладывать визуально для себя, а уже остальны еопльзователи их увидять если зайдут на вашу страницу и не нужно придумывать велосипед с выводом сообщений после их отправки в базу.
                    Label label = new Label();
                    label.Content = "" + Post_txt.Text + "\n '" + DateTime.Now + "'";
                    Post.Children.Add(label);//все остальные элементы добавляются по аналогии
                }

                else
                {
                   
                    //string ID_soobhen = "'\'ID_soobhen'\'";
                    SQLiteCommand CMD5 = DB3.CreateCommand();
                    CMD5.CommandText = "CREATE TABLE '" + Peredathik.id_rabot_int + "' ('N_posta' INTEGER NOT NULL,'text_post' TEXT NOT NULL, 'data_and_time' TEXT NOT NULL)";
                    CMD5.ExecuteNonQuery();
                    
                }
                Close_BD_post();
            }
        }
    }
}

