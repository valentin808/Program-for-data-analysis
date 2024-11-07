using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using TextBox = System.Windows.Forms.TextBox;

namespace statlab2
{
    public partial class Form2 : Form
    {
        private OpenFileDialog openFileDialog1;
        ContextMenu contextMenuStrip1 = new ContextMenu();
        public Form2()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            for (int i = 0; i < Universe.Data_files.Count; i++)
            {
                comboBox1.Items.Add($"Файл # {i+1}");
            }
            for (int i = 0; i < Universe.Data_files.Count; i++)
            {
                ds.Tables.Add("Score" + index_every_data_set);
                for (int c = 0; c < Universe.Data_files[i].Count; c++)
                {
                    ds.Tables[index_every_data_set].Columns.Add($"{c + 1}");

                }
                for (int w = 0; w < 25000; w++)// 25000 пустих колонок
                {
                    ds.Tables[index_every_data_set].Rows.Add();
                }
                for (int t = 0; t < Universe.Data_files[i].Count; t++)
                {
                    for (int r = 0; r < Universe.Data_files[i][t].Count; r++)
                    {
                        ds.Tables[index_every_data_set].Rows[r][t]= Universe.Data_files[i][t][r];
                    } 
                }
                index_every_data_set++;
            }
            for (int i = 0; i < Universe.Data_Value.Count; i++)
            {
                comboBox2.Items.Add($"Сукупності {i + 1}");
            }
            openFileDialog1 = new OpenFileDialog();
            MenuItem menuItem1 = new MenuItem("Видалити стовпець обраної сукупності");
            contextMenuStrip1.MenuItems.Add(menuItem1);
            dataGridView1.ContextMenu = contextMenuStrip1;
            menuItem1.Click += MenuItem1_Click;          
        }
        int index_column = 0;
        private void MenuItem1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex>-1)
            {
                Universe.Data_Value[comboBox2.SelectedIndex].RemoveAt(index_column);
                //Universe.Data_Value[comboBox2.SelectedIndex].Remove(index_column);
                if (Universe.Data_Value[comboBox2.SelectedIndex].Count == 0)
                {
                    Universe.Data_Value.RemoveAt(comboBox2.SelectedIndex);
                    //DataSet new_ds = NewData_set(Universe.Data_Value[comboBox2.SelectedIndex]);
                    //dataGridView1.DataSource = new_ds.Tables[0]; ;
                    comboBox2.Items.Clear();
                    for (int i = 0; i < Universe.Data_Value.Count; i++)
                    {
                        comboBox2.Items.Add($"Сукупності {i + 1}");
                    }
                    comboBox1.SelectedIndex = 0;
                    comboBox2.Text = "";
                }
                else
                {
                    DataSet new_ds = NewData_set(Universe.Data_Value[comboBox2.SelectedIndex]);
                    dataGridView1.DataSource = new_ds.Tables[0];
                    comboBox2.Items.Clear();
                    for (int i = 0; i < Universe.Data_Value.Count; i++)
                    {
                        comboBox2.Items.Add($"Сукупності {i + 1}");
                    }
                }
               
                

            }
            
        }
        
        public int index_every_data_set = 0;
        public DataSet ds = new DataSet();
        
        public DataSet NewData_set(List<List<double>> papir)
        {
            DataSet set = new DataSet();
            set.Tables.Add("Score" + papir.Count);
            int the_max_rows = 0;
            for (int c = 0; c < papir.Count; c++)
            {
                if (papir[c].Count>the_max_rows)
                {
                    the_max_rows = papir[c].Count;
                }
                set.Tables[0].Columns.Add($"{c + 1}");
            }

            for (int i = 0; i < the_max_rows; i++)
            {
                set.Tables[0].Rows.Add();
            }
           
            for (int i = 0; i < the_max_rows; i++)
            {
                for (int t = 0; t < papir.Count; t++)
                {
                    set.Tables[0].Rows[i][t] = papir[t][i];
                }
                
            }
            return set;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            var fileContent = string.Empty;
            string filename = openFileDialog1.FileName;
            var fileStream = openFileDialog1.OpenFile();
            ds.Tables.Add("Score"+ index_every_data_set);
            string header = "";
            using (StreamReader reade1 = new StreamReader(fileStream))
            {
                fileContent = reade1.ReadLine();
                header = fileContent;

            }

            string[] col = Program.strochka(header);
            
            for (int c = 0; c < col.Length; c++)
            {
                ds.Tables[index_every_data_set].Columns.Add($"{c + 1}");
                
            }
            fileStream.Close();
            fileStream = openFileDialog1.OpenFile();

            int rows_for_loop = 0;

            
           using (StreamReader reader = new StreamReader(fileStream))
           {
               fileContent = reader.ReadLine();
               while (reader != null)
               {
                   //string[] rvalue = System.Text.RegularExpressions.Regex.Split(fileContent, " ");
                   string[] rvalue = Program.strochka(fileContent);
                   if (rvalue == null)
                   {
                       break;
                   }
                   ds.Tables[index_every_data_set].Rows.Add(rvalue);
                   rows_for_loop++;
                   fileContent = reader.ReadLine();
               }

           }
           for (int i = 0; i < 25000 - rows_for_loop; i++)
           {
               ds.Tables[index_every_data_set].Rows.Add();
               
           }
            

            dataGridView1.AllowUserToAddRows = false;
            
            dataGridView1.DataSource = ds.Tables[index_every_data_set];
                        
            List<List<double>> data_every_file = new List<List<double>>();
            for (int t = 0; t < dataGridView1.Columns.Count; t++)
            {                
                List<double> data_column = new List<double>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    double comp = 0;
                    if (Double.TryParse(dataGridView1.Rows[i].Cells[t].Value.ToString(), out double restik))
                    {
                        comp = restik;
                        data_column.Add(comp);
                    }
                    else
                    {
                        continue;
                    }
                }
                data_every_file.Add(data_column);

            }//зчитуємо всю таблицю DataGridView

            Universe.Data_files.Add(data_every_file);
            comboBox1.Items.Add("Файл #" + Universe.Data_files.Count);
            comboBox1.SelectedIndex = Universe.Data_files.Count - 1;
            index_every_data_set++;
            

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox2.SelectedIndex = -1;
            comboBox2.Text = " ";
            int index = comboBox1.SelectedIndex;
            dataGridView1.DataSource = ds.Tables[index];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] str = textBox1.Text.Split(' ');           
            int[] el22 = new int[str.Length];           
            for (int i = 0; i < str.Length; i++)
            {
                int por = int.Parse(str[i]);
                el22[i] = por - 1;
            } // вибираємо яку вибірку порівняти
            List<List<double>> list = new List<List<double>>();
            for (int i = 0; i < el22.Length; i++)
            {
                List<double> list_in_cycle = new List<double>();
                for (int t = 0; t < dataGridView1.Rows.Count-1; t++)
                {
                    double comp = 0;
                    if (Double.TryParse(dataGridView1.Rows[t].Cells[el22[i]].Value.ToString(), out double restik))
                    {
                        comp = restik;
                        list_in_cycle.Add(comp);
                    }
                    else
                    {
                        continue;
                    }
                }
                list.Add(list_in_cycle);
            }
            Universe.Data_Value.Add(list);
            comboBox2.Items.Add("Сукупність #" + Universe.Data_Value.Count);
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Universe.Data_Value.RemoveAt(comboBox2.SelectedIndex);
            comboBox2.Items.Clear();
            for (int i = 0; i < Universe.Data_Value.Count; i++)
            {
                comboBox2.Items.Add("Сукупність №"+(i+1));
            }
            dataGridView1.Columns.Clear();
            //dataGridView1.Rows.Clear();
            comboBox2.Text = " ";
            comboBox2.SelectedIndex = -1;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index_column = e.ColumnIndex;
            Rectangle r = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point p = new Point(r.X, r.Y + r.Height);
            contextMenuStrip1.Show(dataGridView1,p);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dataSet1 = NewData_set(Universe.Data_Value[comboBox2.SelectedIndex]);
            comboBox1.Text="";
            dataGridView1.DataSource = dataSet1.Tables[0];
            dataGridView1.AllowUserToAddRows = false;

        }
        public static class Prompt
        {
            public static TextBox textBox_c1;
            public static TextBox textBox_c2;

            public static string[] ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {

                    Width = 500,
                    Height = 230,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                string[] text_list_box = new string[2];
                Label textLabel = new Label() { Left = 50, Top = 30, Text = "Номер стовпців" };
                Label textLabel1 = new Label() { Left = 50, Top = 80, Text = "Номер сукупності" };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                TextBox textBox1 = new TextBox() { Left = 50, Top = 100, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 110, Top = 150, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(textBox1);
                prompt.Controls.Add(textLabel1);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;
                text_list_box[0] = "";
                text_list_box[1] = "";
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    text_list_box[0] = textBox.Text;
                    text_list_box[1] = textBox1.Text;
                    return text_list_box;
                }
                return  text_list_box;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (Universe.Data_Value.Any())
            {
                string[] promptValue = Prompt.ShowDialog("Номер стовпців", "Виберіть в яку сукупінсть додати вектор");
                if (promptValue[0].Length <= 0)
                {
                    return;
                }
                string[] str = promptValue[0].Split(' ');
                int[] stovpec = new int[str.Length];
                for (int i = 0; i < str.Length; i++)
                {
                    int por = int.Parse(str[i]);
                    stovpec[i] = por - 1;
                } //вибираємо стовпці
                if (promptValue[1].Length > 1)
                {
                    MessageBox.Show("Добавляємо тільки в одну сукупність");
                    return;
                }
                str = promptValue[1].Split(' ');
                int[] sukupnist = new int[str.Length];
                for (int i = 0; i < str.Length; i++)
                {
                    int por = int.Parse(str[i]);
                    sukupnist[i] = por - 1;
                } //обираєм сукупність
                int index_of_file = comboBox1.SelectedIndex;
                for (int i = 0; i < stovpec.Length; i++)
                {
                    int nomer_stovpec = stovpec[i];
                    Universe.Data_Value[sukupnist[0]].Add(Universe.Data_files[index_of_file][nomer_stovpec]);
                }

            }
            else
            {
                MessageBox.Show("Сукупностей не виявлено");
            }
            
        }

    }
}
