using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Threading;
using System.Xml;
using System.Windows.Threading;

namespace Мессенджер
{

    public partial class Perepuska : Window
    {
        static private SQLiteConnection DB;
        static private SQLiteConnection DB2;
        public static Perepuska MW = new Perepuska();
        //public static XmlReader XL;
        public static int Kol_Soobhen_1 = 0;
        public static int Kol_Soobhen_2 = 0;
        public static int shethuk_in = 0;
        //public static Label label = new Label();
        DispatcherTimer timer = new DispatcherTimer();
        public static int Artukel_Perepusku_int = Peredathik.Artukyl_peredaha;//Данная переменная для хранения артикула

        public static void Open_BD()
        {
            DB = new SQLiteConnection("DataSource = C:\\Users\\maksi\\OneDrive\\Рабочий стол\\мессенджер\\Мессенджер\\Мессенджер\\bin\\Debug\\Messendjer.db; Version=3");

            //DB = new SQLiteConnection("DataSource = Messendjer.db; Version=3");
            //DB = new SQLiteConnection("Data Source=" + @"C:\\Users\\maksi\\source\repos\\Rest_api\\Rest_api\\bin\\baza sotrydnukov.db" + "; Version=3");
            DB.Open();
        }
        public static void Close_BD()
        {
            DB.Close();
        }


        //Подключение ко второй базе
        public static void Open_BD_Soobhen()
        {
            DB2 = new SQLiteConnection("DataSource= soohenui.db; Version=3");
            //DB = new SQLiteConnection("Data Source=" + @"C:\\Users\\maksi\\source\repos\\Rest_api\\Rest_api\\bin\\baza sotrydnukov.db" + "; Version=3");
            DB2.Open();
        }
        public static void Close_BD_Soobhen()
        {
            DB2.Close();
        }


        //Пеердача артикуля
        public static void Artukel_Soobhen()
        {
            Open_BD();
            SQLiteCommand CMD1 = DB.CreateCommand();//Команда для добавления
            CMD1.CommandText = "SELECT Artukyl FROM friends WHERE ID_1 = '" + Peredathik.id_1_soobhen + "' and ID_2 = '" + Peredathik.id_2_soobhen + "' or ID_1 = '" + Peredathik.id_2_soobhen + "' and ID_2 = '" + Peredathik.id_1_soobhen + "'";
            object Artukel_Perepusku_obj = CMD1.ExecuteScalar();
            Artukel_Perepusku_int = Convert.ToInt32(Artukel_Perepusku_obj);
            SQLiteDataReader SQL1 = CMD1.ExecuteReader();
            Close_BD();
        }

        //Пеердача артикуля
        public static void Artukel_Soobhen2()
        {
            Open_BD();
            SQLiteCommand CMD5 = DB.CreateCommand();//Команда для проверки зарегистрирован ли такой пользователь
            CMD5.CommandText = "SELECT Artukyl FROM friends where ID_1 = '" + Peredathik.id_1_soobhen + "' and ID_2 = '" + Peredathik.id_2_soobhen + "'";
            object Artukyl_Max = CMD5.ExecuteScalar();
            Artukel_Perepusku_int = Convert.ToInt32(Artukyl_Max);
            CMD5.ExecuteNonQuery();
            Close_BD();
        }




        private void obnovlenue_soobhen(/*object state*/object sender, EventArgs e)
        {
            //Достапем количество сообщений и счраниваем с последним с прошлым результатом, и если количество больше то выводим недостающие сообщения
            Open_BD_Soobhen();
            SQLiteCommand CMD1 = DB2.CreateCommand();//Команда для добавления
            CMD1.CommandText = "SELECT max(ID_soobhen) From '" + Artukel_Perepusku_int + "'";//Запрос создания 
            object Kol_Soobhen_obj = CMD1.ExecuteScalar();
            string Kol_Soobhen_str = Convert.ToString(Kol_Soobhen_obj);//данная переменная нужна, чтобы обойти ошибку, так как если в чате нету сообщений, то при проверке количества сообщений мы не можем присвоть пустоту в переменную int, пооэтому если переменная object будет равна пустоте, то присвоива ее в переменнкю string, мы сможем проверить и задать условие если оно равно пустоте, то присваиваем к переменной хранящей количество 0, а если сы получили какоето количество сообщений, то передаем количество в переменную int

            //object Kol_Soobhen_obj_prover;//Данная переменнуя нужна для того чтобы указать проверку, то есть если наша переменная равна пустате такде как и пустая переменная object Kol_Soobhen_obj_prover, то присваиваем ноль
            if (Kol_Soobhen_str == "")//Тaк как если люди еще не общались, то у них нету сообющений, для этого нужно проверить если сообщений нету, нужно передать в место null - ноль, для того чтобы небыло ошибки, так как null нельзя конвертировать в int
            {
                Kol_Soobhen_2 = 0;
            }
            else
            {
                Kol_Soobhen_2 = Convert.ToInt32(Kol_Soobhen_obj);
            }
            CMD1.ExecuteNonQuery();
            Close_BD_Soobhen();

            if (Kol_Soobhen_1 != Kol_Soobhen_2)//Проверка если количество не совподает значит количество сообщений увеличилось
            {

                Open_BD_Soobhen();
                string txt_Zadan = "";

                string shethuk_st = "";
                SQLiteCommand CMD = DB2.CreateCommand();//Команда для добавления
                CMD.CommandText = "SELECT Name, soobhen From '" + Artukel_Perepusku_int + "' where ID_soobhen > '" + Kol_Soobhen_1 + "'";//Запрос на вывод новых сообщений если подошел id, а также если количество сообщений изменилось, то начиная от того сообщения на котором последний раз выводили, выводим все остальные после него, для того чтобы сделаь какбы дописывание сообщений, а не перезаписывание всех данных, и при условие если сообщений много это будет нагрудать систему, так как нужно будет постоянно перезаписывать и выводить все сообщения

                SQLiteDataReader SQL = CMD.ExecuteReader();

                if (SQL.HasRows)//Проверка вернулось ли что-то
                {
                    while (SQL.Read())//Когда указател закончит читать
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            shethuk_in++;//Счетчик для количества заданий
                            shethuk_st = Convert.ToString(shethuk_in);
                            txt_Zadan = shethuk_st + ":   " + SQL["Name"] + " -- " + SQL["soobhen"];//В таком виде будут выводится задания

                            //Заполняем данные в ячейки для сообщений
                            Label label = new Label();
                            label.Content = "* " + txt_Zadan;
                            MyPanel.Children.Add(label);//все остальные элементы добавляются по аналогии

                        }
                    }
                }
                Close_BD_Soobhen();
                //Kol_Soobhen_1 = Kol_Soobhen_2;//Перезаписываем количество сообщений, так как оно стало больше
            }
        }

        public void timerStart()
        {
            timer.Tick += new EventHandler(obnovlenue_soobhen);
            timer.Interval = TimeSpan.FromMilliseconds(3000);
            timer.Start();
        }



        /*//Количество сообщений
        public static void Kol_Soobhenui_funk()
        {
            Open_BD();
            SQLiteCommand CMD1 = DB.CreateCommand();//Команда для добавления
            CMD1.CommandText = "SELECT max(ID_Chata) From Soobhenui";//Запрос создания 
            object Kol_Soobhen_obj = CMD1.ExecuteScalar();
            Kol_Soobhen_1 = Convert.ToInt32(Kol_Soobhen_obj);
            CMD1.ExecuteNonQuery();
            Close_BD();
        }*/

        //Количество сообщений
        public static void Kol_Soobhenui_funk()
        {
            Open_BD_Soobhen();
            SQLiteCommand CMD1 = DB2.CreateCommand();//Команда для добавления
            CMD1.CommandText = "SELECT max(ID_soobhen) From '" + Artukel_Perepusku_int + "'";//Запрос создания 
            object Kol_Soobhen_obj = CMD1.ExecuteScalar();
            string Kol_Soobhen_str = Convert.ToString(Kol_Soobhen_obj);

            //object Kol_Soobhen_obj_prover;//Данная переменнуя нужна для того чтобы указать проверку, то есть если наша переменная равна пустате такде как и пустая переменная object Kol_Soobhen_obj_prover, то присваиваем ноль
            if (Kol_Soobhen_str == "")//Тaк как если люди еще не общались, то у них нету сообющений, для этого нужно проверить если сообщений нету, нужно передать в место null - ноль, для того чтобы небыло ошибки, так как null нельзя конвертировать в int
            {
                Kol_Soobhen_1 = 0;
            }
            else
            {
                Kol_Soobhen_1 = Convert.ToInt32(Kol_Soobhen_obj);
            }
            CMD1.ExecuteNonQuery();
            Close_BD_Soobhen();
        }


        public Perepuska()
        {
            InitializeComponent();
            //Artukel_Soobhen2();
            Kol_Soobhenui_funk();//Получение количества сообщений
            Open_BD_Soobhen();
            string txt_Zadan = "";

            string shethuk_st = "";
            SQLiteCommand CMD = DB2.CreateCommand();//Команда для добавления
            CMD.CommandText = "SELECT Name, soobhen From '" + Artukel_Perepusku_int + "'";//Запрос на вывод новых сообщений если подошел id, а также если количество сообщений изменилось, то начиная от того сообщения на котором последний раз выводили, выводим все остальные после него, для того чтобы сделаь какбы дописывание сообщений, а не перезаписывание всех данных, и при условие если сообщений много это будет нагрудать систему, так как нужно будет постоянно перезаписывать и выводить все сообщения

            SQLiteDataReader SQL = CMD.ExecuteReader();

            if (SQL.HasRows)//Проверка вернулось ли что-то
            {
                while (SQL.Read())//Когда указател закончит читать
                {
                    for (int i = 0; i < 1; i++)
                    {
                        shethuk_in++;//Счетчик для количества заданий
                        shethuk_st = Convert.ToString(shethuk_in);
                        txt_Zadan = shethuk_st + ":   " + SQL["Name"] + " -- " + SQL["soobhen"];//В таком виде будут выводится задания

                        //Заполняем данные в ячейки для сообщений
                        Label label = new Label();
                        label.Content = "* " + txt_Zadan;
                        MyPanel.Children.Add(label);//все остальные элементы добавляются по аналогии

                    }
                }
            }
            Close_BD_Soobhen();

            //timerStart();
        }

        private void btn_vuhod_new_polzova_Click(object sender, RoutedEventArgs e)
        {
            //Достапем количество сообщений и счраниваем с последним с прошлым результатом, и если количество больше то выводим недостающие сообщения
            Open_BD_Soobhen();
            SQLiteCommand CMD1 = DB2.CreateCommand();//Команда для добавления
            CMD1.CommandText = "SELECT max(ID_soobhen) From '" + Artukel_Perepusku_int + "'";//Запрос создания 
            object Kol_Soobhen_obj = CMD1.ExecuteScalar();
            string Kol_Soobhen_str = Convert.ToString(Kol_Soobhen_obj);//данная переменная нужна, чтобы обойти ошибку, так как если в чате нету сообщений, то при проверке количества сообщений мы не можем присвоть пустоту в переменную int, пооэтому если переменная object будет равна пустоте, то присвоива ее в переменнкю string, мы сможем проверить и задать условие если оно равно пустоте, то присваиваем к переменной хранящей количество 0, а если сы получили какоето количество сообщений, то передаем количество в переменную int

            //object Kol_Soobhen_obj_prover;//Данная переменнуя нужна для того чтобы указать проверку, то есть если наша переменная равна пустате такде как и пустая переменная object Kol_Soobhen_obj_prover, то присваиваем ноль
            if (Kol_Soobhen_str == "")//Тaк как если люди еще не общались, то у них нету сообющений, для этого нужно проверить если сообщений нету, нужно передать в место null - ноль, для того чтобы небыло ошибки, так как null нельзя конвертировать в int
            {
                Kol_Soobhen_2 = 0;
            }
            else
            {
                Kol_Soobhen_2 = Convert.ToInt32(Kol_Soobhen_obj);
            }
            CMD1.ExecuteNonQuery();
            Close_BD_Soobhen();

            if (Kol_Soobhen_1 != Kol_Soobhen_2)//Проверка если количество не совподает значит количество сообщений увеличилось
            {

                Open_BD_Soobhen();
                string txt_Zadan = "";

                string shethuk_st = "";
                SQLiteCommand CMD = DB2.CreateCommand();//Команда для добавления
                CMD.CommandText = "SELECT Name, soobhen From '" + Artukel_Perepusku_int + "' where ID_soobhen > '" + Kol_Soobhen_1 + "'";//Запрос на вывод новых сообщений если подошел id, а также если количество сообщений изменилось, то начиная от того сообщения на котором последний раз выводили, выводим все остальные после него, для того чтобы сделаь какбы дописывание сообщений, а не перезаписывание всех данных, и при условие если сообщений много это будет нагрудать систему, так как нужно будет постоянно перезаписывать и выводить все сообщения

                SQLiteDataReader SQL = CMD.ExecuteReader();

                if (SQL.HasRows)//Проверка вернулось ли что-то
                {
                    while (SQL.Read())//Когда указател закончит читать
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            shethuk_in++;//Счетчик для количества заданий
                            shethuk_st = Convert.ToString(shethuk_in);
                            txt_Zadan = shethuk_st + ":   " + SQL["Name"] + " -- " + SQL["soobhen"];//В таком виде будут выводится задания

                            //Заполняем данные в ячейки для сообщений
                            Label label = new Label();
                            label.Content = "* " + txt_Zadan;
                            MyPanel.Children.Add(label);//все остальные элементы добавляются по аналогии

                        }
                    }
                }
                Close_BD_Soobhen();
                Kol_Soobhen_1 = Kol_Soobhen_2;//Перезаписываем количество сообщений, так как оно стало больше
            }
        }

        private void btn_vuhod_Vhod_Click(object sender, RoutedEventArgs e)
        {
            Profil Profil_vhod = new Profil();
            Profil_vhod.Show();
            Hide();
        }


        private void btn_otpravut_soobhenue_Click(object sender, RoutedEventArgs e)
        {
            if(Perepuska_txt.Text.Length == 0)
            {
                MessageBox.Show("Нельзя отправлять пустое сообщение!");
            }
            else
            { 
            Open_BD_Soobhen();
            SQLiteCommand CMD1 = DB2.CreateCommand();
            CMD1.CommandText = "insert into '" + Artukel_Perepusku_int + "'(ID_1, Name, soobhen) values ('" + Peredathik.id_rabot_int + "', '" + Peredathik.Name_id_1 + "', '" + "\r" + Perepuska_txt.Text + "')"; //Запрос создания
            CMD1.ExecuteNonQuery();//Выполняет запрос не подрозумевая возвращения никаких значений, это для insert, delete, update - так ка квозврощать они ничего не должны
            Close_BD_Soobhen();
            }
        }

        private void btn_vuhod_new_polzova_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
