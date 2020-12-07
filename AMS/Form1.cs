using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Xml;
using System.Xml.Linq;
namespace AMS
{
    public partial class Form1 : Form
    {
        public bool s1 = false;
        List<string> info=new List<string>();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("data.xml"))
            {
                XElement xel = new XElement("DATA");
                xel.Save("data.xml");
            }
            SetupDataGridView();
        }
        private void  SetupDataGridView()
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "acc_id";
            dataGridView1.Columns[1].Name = "name";
            dataGridView1.Columns[2].Name = "code";
            dataGridView1.Columns[3].Name = "mailbox";
            dataGridView1.ReadOnly = true;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 secondform = new Form2();
            secondform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
  /*       MySqlDataAdapter daacc;
            DataSet dsacc;
            string constr = "server=localhost;user=root;database=ams;port=3306;password=123mysql;";
            MySqlConnection conn = new MySqlConnection(constr);
            string s1 =Convert.ToString( textBox1.Text);
            try
            {   
                conn.Open();
                label2.Text = "Connected to database!";
                string sql = "SELECT * FROM accinfo WHERE acc_id like @acc_id";
                daacc=new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand(sql, conn);                
                string pro_input = s1.ToUpper() + "%";
                cmd.Parameters.AddWithValue("@acc_id", pro_input);
                daacc.SelectCommand=cmd;
                dsacc=new DataSet();
                daacc.Fill(dsacc,"accinfo");
                dataGridView1.DataSource=dsacc;
                dataGridView1.DataMember="accinfo";
            }
            catch(Exception ex)
            {
                label2.Text = "Fail to connect to database!";
                Console.WriteLine(ex.ToString());
            }
            conn.Close();*/
            dataGridView1.Rows.Clear();
            string s1 = Convert.ToString(textBox1.Text).Trim().ToUpper();
            XElement xel = XElement.Load("data.xml");
            IEnumerable<XElement> tests =
           from el in xel.Elements("ROW")
           where  el.Element("acc_id").Value.ToString().Contains(s1)
           select el;
           if (tests.Count()!=0)
           {
               info.Clear();
               foreach(XElement v1 in tests)
               {
                   List<string> t1=new List<string>();
                   t1.Add(v1.Element("acc_id").Value.ToString());
                   t1.Add(v1.Element("name").Value.ToString());
                   t1.Add(v1.Element("code").Value.ToString());
                   t1.Add(v1.Element("mailbox").Value.ToString());
                   info.Add(v1.Element("info").Value.ToString());
                   dataGridView1.Rows.Add(t1.ToArray());
               }
               richTextBox1.Text = info[0];
           }
           
        }
       private void  textBox1_KeyDown(Object sender,KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
             /*   e.SuppressKeyPress = true;
                MySqlDataAdapter daacc;
                DataSet dsacc;
                string constr = "server=localhost;user=root;database=ams;port=3306;password=123mysql;";
                MySqlConnection conn = new MySqlConnection(constr);
                string s1 = Convert.ToString(textBox1.Text);
                try
                {
                    conn.Open();
                    label2.Text = "Connected to database!";
                    string sql = "SELECT * FROM accinfo WHERE acc_id like @acc_id";
                    daacc = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    string pro_input = s1.ToUpper() + "%";
                    cmd.Parameters.AddWithValue("@acc_id", pro_input);
                    daacc.SelectCommand = cmd;
                    dsacc = new DataSet();
                    daacc.Fill(dsacc, "accinfo");
                    dataGridView1.DataSource = dsacc;
                    dataGridView1.DataMember = "accinfo";
                }
                catch (Exception ex)
                {
                    label2.Text = "Fail to connect to database!";
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
              */
                dataGridView1.Rows.Clear();
                string s1 = Convert.ToString(textBox1.Text).Trim().ToUpper();
                XElement xel = XElement.Load("data.xml");
                IEnumerable<XElement> tests =
               from el in xel.Elements("ROW")
               where el.Element("acc_id").Value.ToString().Contains(s1)
               select el;
                if (tests.Count() != 0)
                {
                    info.Clear();
                    foreach (XElement v1 in tests)
                    {
                        List<string> t1 = new List<string>();
                        t1.Add(v1.Element("acc_id").Value.ToString());
                        t1.Add(v1.Element("name").Value.ToString());
                        t1.Add(v1.Element("code").Value.ToString());
                        t1.Add(v1.Element("mailbox").Value.ToString());
                        info.Add(v1.Element("info").Value.ToString());
                        dataGridView1.Rows.Add(t1.ToArray());
                    }
                    /*     if(dataGridView1.SelectedRows.Count>0 && dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count-1)
                         {
                             richTextBox1.Text = info[dataGridView1.SelectedRows[0].Index];
                         }
                         else
                         {
                             richTextBox1.Text = "";
                         }
                     */
                    richTextBox1.Text = info[0];
                }
            }
        }

       private void button3_Click(object sender, EventArgs e)
       {
           s1 = !s1;
           if (s1)
           {
               button3.ForeColor = Color.Red;
               this.TopMost = s1;
           }
           else
           {
               button3.ForeColor = Color.Black;
               this.TopMost = s1;
           }
           
       }

       private void button4_Click(object sender, EventArgs e)
       {
           openFileDialog1.InitialDirectory = "c:\\";
           openFileDialog1.Filter = "XML文件|*.xml|所有文件|*.*";
           openFileDialog1.RestoreDirectory = true;
           openFileDialog1.FilterIndex = 1;
           if (openFileDialog1.ShowDialog() == DialogResult.OK)
           {
               DialogResult result = MessageBox.Show("The import file will cover data xml!", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
               if (result == DialogResult.Yes)
               {
                   string fName = openFileDialog1.FileName;
                   XElement xel = XElement.Load(fName);
                   xel.Save("data.xml");
               }              
           }
       }

       private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
       {
           if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count - 1)
           {
               richTextBox1.Text = info[dataGridView1.SelectedRows[0].Index];
           }
           else
           {
               richTextBox1.Text = "";
           }         
       }

       private void button5_Click(object sender, EventArgs e)
       {
           Stream myStream;
           saveFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
           saveFileDialog1.FilterIndex = 2;
           saveFileDialog1.RestoreDirectory = true;
           if (saveFileDialog1.ShowDialog() == DialogResult.OK)
           {
               if ((myStream = saveFileDialog1.OpenFile()) != null)
               {
                   XElement xel = XElement.Load("data.xml");
                   XDocument xd= new XDocument(xel);
                   xd.Save(myStream);
                   myStream.Close();
               }
           }
       }

    }
}
