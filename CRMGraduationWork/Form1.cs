﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Office.Interop.Word;

namespace CRMGraduationWork
{
    public partial class Form1 : Form
    {
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();
        }

        public void loadRequestsByStatusInDGV1()
        {
            List<RequestDGV> requests = new List<RequestDGV>();
            requests.Clear();
            int id;
            DateTime createDate;
            System.Windows.Forms.Button AcceptBtn = new System.Windows.Forms.Button();            
            string connStr = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT * FROM requests WHERE status = @status";
            command.Parameters.AddWithValue("@status", 0);

            using (MySqlDataReader result = command.ExecuteReader())
            {
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        id = Convert.ToInt32(result[0]);
                        Console.WriteLine("Date:" + result[4].ToString());
                        createDate = DateTime.ParseExact(result[4].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        requests.Add(new RequestDGV { ID = id, CreateDate = createDate });

                    }
                }
            }

            conn.Close();
            conn.Dispose();

            try
            {
                dataGridView1.Columns.Clear();
            }
            catch { }

            dataGridView1.DataSource = requests;

            DataGridViewButtonColumn acceptButton = new DataGridViewButtonColumn();
            acceptButton.Name = "Accept";
            acceptButton.HeaderText = "Accept";
            acceptButton.Text = "Accept";
            acceptButton.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Insert(2, acceptButton);

            DataGridViewButtonColumn rejectButton = new DataGridViewButtonColumn();
            rejectButton.Name = "Reject";
            rejectButton.HeaderText = "Reject";
            rejectButton.Text = "Reject";
            rejectButton.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Insert(3, rejectButton);

            this.dataGridView1.ClearSelection();
        }

        public void loadRequestsByStatusInDGV2()
        {
            List<AcceptedRequestDGB> acceptedRequests = new List<AcceptedRequestDGB>();
            int id;
            DateTime createDate;
            DateTime acceptDate;
            System.Windows.Forms.Button AcceptBtn = new System.Windows.Forms.Button();
            string connStr = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT * FROM requests WHERE status = @status";
            command.Parameters.AddWithValue("@status", 1);

            using (MySqlDataReader result = command.ExecuteReader())
            {
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        id = Convert.ToInt32(result[0]);
                        createDate = DateTime.ParseExact(result[4].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        acceptDate = DateTime.ParseExact(result[5].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        acceptedRequests.Add(new AcceptedRequestDGB { ID = id, CreateDate = createDate, AcceptDate = acceptDate });
                    }
                }
            }

            conn.Close();
            conn.Dispose();

            try
            {
                dataGridView2.Columns.Clear();
            }
            catch { }
            dataGridView2.DataSource = acceptedRequests;

            DataGridViewButtonColumn acceptButton = new DataGridViewButtonColumn();
            acceptButton.Name = "CreateDoc";
            acceptButton.HeaderText = "Create Doc";
            acceptButton.Text = "Create Doc";
            acceptButton.UseColumnTextForButtonValue = true;
            this.dataGridView2.Columns.Insert(3, acceptButton);

            DataGridViewButtonColumn rejectButton = new DataGridViewButtonColumn();
            rejectButton.Name = "Complete";
            rejectButton.HeaderText = "Complete";
            rejectButton.Text = "Complete";
            rejectButton.UseColumnTextForButtonValue = true;
            this.dataGridView2.Columns.Insert(4, rejectButton);

            this.dataGridView2.ClearSelection();
        }

        public void loadRequestsByStatusInDGV3()
        {
            List<CompletedRequestDGV> acceptedRequests = new List<CompletedRequestDGV>();
            int id;
            DateTime createDate;
            DateTime acceptDate;
            DateTime completeDate;
            System.Windows.Forms.Button AcceptBtn = new System.Windows.Forms.Button();
            string connStr = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT * FROM requests WHERE status = @status";
            command.Parameters.AddWithValue("@status", 2);

            using (MySqlDataReader result = command.ExecuteReader())
            {
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        id = Convert.ToInt32(result[0]);
                        createDate = DateTime.ParseExact(result[4].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        acceptDate = DateTime.ParseExact(result[5].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        completeDate = DateTime.ParseExact(result[6].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        acceptedRequests.Add(new CompletedRequestDGV { ID = id, CreateDate = createDate, AcceptDate = acceptDate, CompleteDate = completeDate });
                    }
                }
            }

            conn.Close();
            conn.Dispose();

            try
            {
                dataGridView3.Columns.Clear();
            }
            catch { }
            dataGridView3.DataSource = acceptedRequests;

            DataGridViewButtonColumn acceptButton = new DataGridViewButtonColumn();
            acceptButton.Name = "CreateDoc";
            acceptButton.HeaderText = "Create Doc";
            acceptButton.Text = "Create Doc";
            acceptButton.UseColumnTextForButtonValue = true;
            this.dataGridView3.Columns.Insert(4, acceptButton);

            DataGridViewButtonColumn AddButton = new DataGridViewButtonColumn();
            AddButton.Name = "Add";
            AddButton.HeaderText = "Add";
            AddButton.Text = "Add";
            AddButton.UseColumnTextForButtonValue = true;
            this.dataGridView3.Columns.Insert(5, AddButton);

            this.dataGridView3.ClearSelection();
        }

        public void loadRequestsByStatusInDGV4()
        {
            List<MastersDGV> masters = new List<MastersDGV>();
            int id;
            string name = "";
            string login = "";
            string password = "";
            string connStr = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT * FROM masters;";

            using (MySqlDataReader result = command.ExecuteReader())
            {
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        id = Convert.ToInt32(result[0]);
                        name = result[1].ToString();
                        login = result[2].ToString();
                        password = result[3].ToString();
                        masters.Add(new MastersDGV { ID = id, Name = name, Login = login, Password = password });
                    }
                }
            }

            conn.Close();
            conn.Dispose();

            try
            {
                dataGridView4.Columns.Clear();
            }
            catch { }
            dataGridView4.DataSource = masters;            

            this.dataGridView4.ClearSelection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myTimer.Tick += new EventHandler(TimerEventProcessor);

            myTimer.Interval = 500;
            myTimer.Start();            
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            loadRequestsByStatusInDGV1();
            loadRequestsByStatusInDGV2();
            loadRequestsByStatusInDGV3();
            loadRequestsByStatusInDGV4();
        }  

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            switch (e.ColumnIndex)
            {
                // click accept
                case 2:
                    Form2 form = new Form2(ID);
                    form.Show();
                                        
                    break;

                // click reject
                case 3:
                    string connStr2 = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
                    MySqlConnection conn2 = new MySqlConnection(connStr2);

                    try
                    {
                        conn2.Open();
                    }
                    catch
                    {
                        MessageBox.Show("Error", "Unable to connect to the server");
                        return;
                    }

                    MySqlCommand command2 = new MySqlCommand();
                    command2.Connection = conn2;
                    command2.CommandText = "UPDATE requests SET status = @status WHERE id = @id";
                    command2.Parameters.AddWithValue("@id", ID);
                    command2.Parameters.AddWithValue("@status", -1);
                    command2.ExecuteNonQuery();

                    conn2.Close();
                    conn2.Dispose();
                    break;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ID = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);

            switch (e.ColumnIndex)
            {
                // click create doc
                case 3:

                    string name = "";
                    string email = "";
                    string phone = "";
                    string createDate = "";
                    string acceptDate = "";
                    string typeOfEquipment = "";
                    string issue = "";
                    int masterID = -1;
                    string masterName = "";


                    string connStr = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
                    MySqlConnection conn = new MySqlConnection(connStr);

                    try
                    {
                        conn.Open();
                    }
                    catch
                    {
                        MessageBox.Show("Error", "Unable to connect to the server");
                        return;
                    }

                    MySqlCommand command = new MySqlCommand();
                    command.Connection = conn;
                    command.CommandText = "SELECT * FROM requests WHERE status = @status";
                    command.Parameters.AddWithValue("@status", 1);

                    using (MySqlDataReader result = command.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                name = result[1].ToString();
                                email = result[2].ToString();
                                phone = result[3].ToString();
                                createDate = result[4].ToString();
                                acceptDate = result[5].ToString();
                                typeOfEquipment = result[7].ToString();
                                issue = result[8].ToString();
                                masterID = Convert.ToInt32(result[9]);
                            }
                        }
                    }

                    conn.Close();
                    conn.Dispose();

                    
                    string connStr3 = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
                    MySqlConnection conn3 = new MySqlConnection(connStr3);

                    try
                    {
                        conn3.Open();
                    }
                    catch
                    {
                        MessageBox.Show("Error", "Unable to connect to the server");
                        return;
                    }

                    MySqlCommand command3 = new MySqlCommand();
                    command3.Connection = conn3;
                    command3.CommandText = "SELECT * FROM masters WHERE id = @id";
                    command3.Parameters.AddWithValue("@id", masterID);

                    using (MySqlDataReader result3 = command3.ExecuteReader())
                    {
                        if (result3.HasRows)
                        {
                            while (result3.Read())
                            {
                                masterName = result3[1].ToString();
                            }
                        }
                    }

                    conn3.Close();
                    conn3.Dispose();

                    Console.WriteLine(ID);
                    Console.WriteLine(name);
                    Console.WriteLine(email);
                    Console.WriteLine(phone);
                    Console.WriteLine(createDate);
                    Console.WriteLine(acceptDate);
                    Console.WriteLine(typeOfEquipment);
                    Console.WriteLine(issue);
                    Console.WriteLine(masterName);

                    Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                    winword.ShowAnimation = false;
                    winword.Visible = false;
                    object missing = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                    document.Content.SetRange(0, 0);
                    document.Content.Text = "Акт о начале работ" + Environment.NewLine +
                                            "Номер заказа: " + ID.ToString() + Environment.NewLine +
                                            "ФИО: " + name + Environment.NewLine +
                                            "Email: " + email + Environment.NewLine +
                                            "Номер тел.: " + phone + Environment.NewLine +
                                            "Дата создания: " + createDate + Environment.NewLine +
                                            "Дата принятия: " + acceptDate + Environment.NewLine +
                                            "Тип: " + typeOfEquipment + Environment.NewLine +
                                            "Проблема: " + issue + Environment.NewLine +
                                            "ФИО мастера: " + masterName + Environment.NewLine;

                    object filename = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Act_o_nachale_rabot_" + ID.ToString() + ".docx";
                    document.SaveAs2(ref filename);
                    document.Close(ref missing, ref missing, ref missing);
                    document = null;
                    winword.Quit(ref missing, ref missing, ref missing);
                    winword = null;
                    break;

                // click complete
                case 4:
                    string completeDate = DateTime.UtcNow.Date.ToString("yyyy-MM-dd HH:mm:ss");
                    string connStr2 = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
                    MySqlConnection conn2 = new MySqlConnection(connStr2);

                    try
                    {
                        conn2.Open();
                    }
                    catch
                    {
                        MessageBox.Show("Error", "Unable to connect to the server");
                        return;
                    }

                    MySqlCommand command2 = new MySqlCommand();
                    command2.Connection = conn2;
                    command2.CommandText = "UPDATE requests SET status = @status, complete_date = @complete_date WHERE id = @id";
                    command2.Parameters.AddWithValue("@status", 2);
                    command2.Parameters.AddWithValue("@complete_date", completeDate);
                    command2.Parameters.AddWithValue("@id", ID);
                    command2.ExecuteNonQuery();

                    conn2.Close();
                    conn2.Dispose();
                    break;
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ID = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[0].Value);

            switch (e.ColumnIndex)
            {
                // click create doc
                case 4:
                    string name = "";
                    string email = "";
                    string phone = "";
                    string createDate = "";
                    string acceptDate = "";
                    string completeDate = "";
                    string typeOfEquipment = "";
                    string price = "";
                    string refinements = "";
                    string issue = "";
                    int masterID = -1;
                    string masterName = "";


                    string connStr = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
                    MySqlConnection conn = new MySqlConnection(connStr);

                    try
                    {
                        conn.Open();
                    }
                    catch
                    {
                        MessageBox.Show("Error", "Unable to connect to the server");
                        return;
                    }

                    MySqlCommand command = new MySqlCommand();
                    command.Connection = conn;
                    command.CommandText = "SELECT * FROM requests WHERE id = @id";
                    command.Parameters.AddWithValue("@id", ID);

                    using (MySqlDataReader result = command.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                name = result[1].ToString();
                                email = result[2].ToString();
                                phone = result[3].ToString();
                                createDate = result[4].ToString();
                                acceptDate = result[5].ToString();
                                completeDate = result[6].ToString();
                                typeOfEquipment = result[7].ToString();
                                issue = result[8].ToString();
                                masterID = Convert.ToInt32(result[9]);
                                price = result[12].ToString();
                                refinements = result[13].ToString();
                            }
                        }
                    }

                    conn.Close();
                    conn.Dispose();

                    string connStr3 = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
                    MySqlConnection conn3 = new MySqlConnection(connStr3);

                    try
                    {
                        conn3.Open();
                    }
                    catch
                    {
                        MessageBox.Show("Error", "Unable to connect to the server");
                        return;
                    }

                    MySqlCommand command3 = new MySqlCommand();
                    command3.Connection = conn3;
                    command3.CommandText = "SELECT * FROM masters WHERE id = @id";
                    command3.Parameters.AddWithValue("@id", masterID);

                    using (MySqlDataReader result3 = command3.ExecuteReader())
                    {
                        if (result3.HasRows)
                        {
                            while (result3.Read())
                            {
                                masterName = result3[1].ToString();
                            }
                        }
                    }

                    conn3.Close();
                    conn3.Dispose();

                    Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                    winword.ShowAnimation = false;
                    winword.Visible = false;
                    object missing = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                    document.Content.SetRange(0, 0);
                    document.Content.Text = "Акт о завершении работ" + Environment.NewLine +
                                            "Номер заказа: " + ID.ToString() + Environment.NewLine +
                                            "ФИО: " + name + Environment.NewLine +
                                            "Email: " + email + Environment.NewLine +
                                            "Номер тел.: " + phone + Environment.NewLine +
                                            "Дата создания: " + createDate + Environment.NewLine +
                                            "Дата принятия: " + acceptDate + Environment.NewLine +
                                            "Дата завершения: " + completeDate + Environment.NewLine +
                                            "Тип: " + typeOfEquipment + Environment.NewLine +
                                            "Проблема: " + issue + Environment.NewLine +
                                            "ФИО мастера: " + masterName + Environment.NewLine +
                                            "Что сделано: " + refinements + Environment.NewLine +
                                            "Цена: " + price + Environment.NewLine;

                    if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Act_o_zavershenii_rabot_" + ID.ToString() + ".docx"))
                    {
                        System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Act_o_zavershenii_rabot_" + ID.ToString() + ".docx");
                    }

                    object filename = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Act_o_zavershenii_rabot_" + ID.ToString() + ".docx";
                    document.SaveAs2(ref filename);
                    document.Close(ref missing, ref missing, ref missing);
                    document = null;
                    winword.Quit(ref missing, ref missing, ref missing);
                    winword = null;
                    break;

                case 5:
                    Form3 form3 = new Form3(ID);
                    form3.Show();

                    break;
            }
        }
    }
}
