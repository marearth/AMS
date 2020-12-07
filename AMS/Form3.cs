using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Common;
using System.Xml.Linq;
using System.Web;
namespace AMS
{
    public partial class Form3 : Form
    {
       string tbn = "accinfo";
       List<string> info1 = new List<string>();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
        }
        private void SetupDataGridView()
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "acc_id";
            dataGridView1.Columns[1].Name = "name";
            dataGridView1.Columns[2].Name = "code";
            dataGridView1.Columns[3].Name = "mailbox";
            dataGridView1.Columns[0].ReadOnly = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string s1 = Convert.ToString(textBox1.Text).Trim().ToUpper();
            XElement xel = XElement.Load("data.xml");
            IEnumerable<XElement> tests =
           from el in xel.Elements("ROW")
           where el.Element("acc_id").Value.ToString().Contains(s1)
           select el;
            if (tests.Count() != 0)
            {
                info1.Clear();
                foreach (XElement v1 in tests)
                {
                    List<string> t1 = new List<string>();
                    t1.Add(v1.Element("acc_id").Value.ToString());
                    t1.Add(v1.Element("name").Value.ToString());
                    t1.Add(v1.Element("code").Value.ToString());
                    t1.Add(v1.Element("mailbox").Value.ToString());
                    info1.Add(v1.Element("info").Value.ToString());
                    dataGridView1.Rows.Add(t1.ToArray());
                }
                info.Text = info1[0];
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
       /*     MySqlDataAdapter daacc = new MySqlDataAdapter();
            DataSet dsacc = new DataSet();
            string constr = "server=localhost;user=root;database=ams;port=3306;password=123mysql;";
            MySqlConnection conn = new MySqlConnection(constr);
            string s1 = Convert.ToString(textBox1.Text);
            try
            {             
                conn.Open();
                label2.Text = "Connected to database!";                             
                dataGridView1.DataSource = dsacc;
                dataGridView1.DataMember = "accinfo";              
                daacc.Update(dsacc, "accinfo");
                label2.Text = "Database Updated!";
            }
            catch (Exception ex)
            {
                label12.Text = "Fail to connect to database!";
            }                     
            conn.Close();
            daacc.Update(dsacc, "accinfo");
            label12.Text = "Database Updated!";*/
          /*  daacc = new MySqlDataAdapter();
            dsacc = new DataSet();
            string constr = "server=localhost;user=root;database=ams;port=3306;password=123mysql;";
            MySqlConnection conn = new MySqlConnection(constr);
            string selectSQL = "SELECT * FROM accinfo";
            conn.Open();
            daacc = new MySqlDataAdapter(selectSQL, conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(daacc);
            //BindingSource bstp = (BindingSource)dataGridView1.DataSource;
           // var bindingSource = (BindingSource)dataGridView1.DataSource;
           // var dt = (DataTable)bindingSource.DataSource;
            System.Data.DataTable mytable = (dataGridView1.DataSource as DataSet).Tables[0];
            try
            {
                label12.Text = "Connected to database!";
                DialogResult result = MessageBox.Show("These changes will be written back to database!", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {                   
                    daacc.Update(mytable);
                }              
            }
            catch(Exception)
            {
                label12.Text = "Fail to connect to database!";
            }
            conn.Close();
           * */
            DialogResult result = MessageBox.Show("These changes will be written back to database!", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    XElement xel = XElement.Load("data.xml");
                    foreach (DataGridViewRow v1 in dataGridView1.Rows)
                    {
                        if (v1.Index == dataGridView1.Rows.Count-1)
                            break;
                        string indexword = v1.Cells[0].Value.ToString();
                        IEnumerable<XElement> tests =
                        from el in xel.Elements("ROW")
                        where el.Element("acc_id").Value.ToString() == indexword
                        select el;
                        foreach (XElement v2 in tests)
                        {
                            XElement xel1 = new XElement("ROW");
                            xel1.Add(new XElement("acc_id", v1.Cells[0].Value.ToString()));
                            xel1.Add(new XElement("name", v1.Cells[1].Value.ToString()));
                            xel1.Add(new XElement("code", v1.Cells[2].Value.ToString()));
                            xel1.Add(new XElement("mailbox", v1.Cells[3].Value.ToString()));
                            xel1.Add(new XElement("info", info.Text.ToString()));
                            v2.ReplaceWith(xel1);
                        }
                    }
                    xel.Save("data.xml");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
       {
            if(dataGridView1.SelectedRows.Count>0 && dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count-1)
            {
                string indexword=dataGridView1.SelectedRows[0].Cells[0].Value.ToString();               
                XElement xel = XElement.Load("data.xml");
                IEnumerable<XElement> tests =
               from el in xel.Elements("ROW")
               where el.Element("acc_id").Value.ToString() == indexword
               select el;
                DialogResult result = MessageBox.Show("This deletion will be written back to data xml!", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                    foreach(XElement v1 in tests)
                    {
                        v1.Remove();
                    }
                    xel.Save("data.xml");                  
                }

            }
          }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string s1 = Convert.ToString(textBox1.Text).Trim().ToUpper();
            XElement xel = XElement.Load("data.xml");
            IEnumerable<XElement> tests =
           from el in xel.Elements("ROW")
           where el.Element("acc_id").Value.ToString().Contains(s1)
           select el;
            if (tests.Count() != 0)
            {
                info1.Clear();
                foreach (XElement v1 in tests)
                {
                    List<string> t1 = new List<string>();
                    t1.Add(v1.Element("acc_id").Value.ToString());
                    t1.Add(v1.Element("name").Value.ToString());
                    t1.Add(v1.Element("code").Value.ToString());
                    t1.Add(v1.Element("mailbox").Value.ToString());
                    info1.Add(v1.Element("info").Value.ToString());
                    dataGridView1.Rows.Add(t1.ToArray());
                }
                info.Text = info1[0];
            }
        }

        private void create_Click(object sender, EventArgs e)
        {
            string s1 = accid.Text.ToString();
            string s2 = name.Text.ToString();
            string s3 = code.Text.ToString();
            string s4 = mailbox.Text.ToString();
            string s5 = info.Text.ToString();
            string t1 = s1.Trim().ToUpper();
            XElement xel = XElement.Load("data.xml");
            IEnumerable<XElement> tests =
           from el in xel.Elements("ROW")
           where el.Element("acc_id").Value.ToString()==t1
           select el;
           if(tests.Count()==0)
           {
               XElement xel1 = new XElement("ROW");
               xel1.Add(new XElement("acc_id", s1));
               xel1.Add(new XElement("name", s2));
               xel1.Add(new XElement("code", s3));
               xel1.Add(new XElement("mailbox", s4));
               xel1.Add(new XElement("info", s5));
               xel.Add(xel1);
               xel.Save("data.xml");    
               MessageBox.Show("The record has been saved!","Tip");
           }
           else
           {
               MessageBox.Show("The index word has already existed!","Warning");
           }
        }
        private void textBox1_KeyDown(Object sender, KeyEventArgs e)
        {
          if (e.KeyCode == Keys.Enter)
            {
                dataGridView1.Rows.Clear();
                string s1 = Convert.ToString(textBox1.Text).Trim().ToUpper();
                XElement xel = XElement.Load("data.xml");
                IEnumerable<XElement> tests =
               from el in xel.Elements("ROW")
               where el.Element("acc_id").Value.ToString().Contains(s1)
               select el;
                if (tests.Count() != 0)
                {
                    info1.Clear();
                    foreach (XElement v1 in tests)
                    {
                        List<string> t1 = new List<string>();
                        t1.Add(v1.Element("acc_id").Value.ToString());
                        t1.Add(v1.Element("name").Value.ToString());
                        t1.Add(v1.Element("code").Value.ToString());
                        t1.Add(v1.Element("mailbox").Value.ToString());
                        info1.Add(v1.Element("info").Value.ToString());
                        dataGridView1.Rows.Add(t1.ToArray());
                    }
                    info.Text = info1[0];
                }
            }
        
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count - 1)
            {
                info.Text = info1[dataGridView1.SelectedRows[0].Index];
            }
            else
            {
                info.Text = "";
            }         
        }
    }
 }

