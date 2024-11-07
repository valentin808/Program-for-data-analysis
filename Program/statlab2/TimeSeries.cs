using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using TextBox = System.Windows.Forms.TextBox;

namespace statlab2
{
    internal class TimeSeries
    {
        private  List<double> Seriestime;
        private List<double> Time;
        private List<double> Prognoz_value;
        private List<double> Zmineni_dani;
        private Chart chart;
        private double _Alpha;
        private Form1 form1;
        private DataGridView data_grid;
        private DataGridView data_grid_eigen;
        private List<double> vlasniss_chisla;
        private int dovgina_guseni;
        public TimeSeries(List<double> Seriestime11)
        {
            Seriestime = Seriestime11;
            List<double> Time11 = new List<double>();
            for (int i = 0; i < Seriestime11.Count; i++)
            {
                Time11.Add(Convert.ToDouble(i));
            }
            Time = Time11;
        }
        public TimeSeries(List<double> Seriestime11,List<double> The_time)
        {
            Seriestime = Seriestime11;
            Time = The_time;
        }
        public Form1 MainForm
        {
            set { form1 = value; }
        }
        public int Show_protocol_in_dt()
        {
            double mean = Program.vect_seredne(Seriestime);
            double sigma=Program.vect_seredno_kvadratichnyh(Seriestime);

            return 0;
        }
        public Chart Get_Chart()
        {
            chart = new Chart();
            Size s = new Size(1100, 500);
            chart.Size = s;
            chart.ChartAreas.Add("0");
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MinorGrid.Enabled = false; ;
            chart.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
            chart.Series.Add("Ряд");
            
            chart.Series["Ряд"].IsVisibleInLegend = false;
            
            chart.Series["Ряд"].ChartType = SeriesChartType.Line;
            chart.Series["Ряд"].MarkerStyle = MarkerStyle.Circle;
            chart.Series["Ряд"].BorderWidth = 3;
            chart.Series["Ряд"].MarkerSize = 6;
            chart.Location = new Point(0, 0);


            for (int i = 0; i < Time.Count; i++)
            {
                chart.Series["Ряд"].Points.AddXY(Time[i], Seriestime[i]);
                
            }
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "0";
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "0.0";
            chart.ChartAreas[0].AxisX.Title = $"t";
            chart.ChartAreas[0].AxisY.Title = $"X(t)";
            /////створення контекстного меню
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem parentMenuItem = new ToolStripMenuItem("Згладжування");
            ToolStripMenuItem parentMenuItem11 = new ToolStripMenuItem("Прогнозування");

            ToolStripMenuItem menuItem1 = new ToolStripMenuItem("Ковзне середнє");
            ToolStripMenuItem menuItem2 = new ToolStripMenuItem("Нових даних");
            ToolStripMenuItem menuItem3 = new ToolStripMenuItem("По наявних даних");
            ToolStripMenuItem menuItem4 = new ToolStripMenuItem("Медіанне");
            ToolStripMenuItem menuItem5 = new ToolStripMenuItem("SMA");
            ToolStripMenuItem menuItem6 = new ToolStripMenuItem("WMA");
            ToolStripMenuItem menuItem7 = new ToolStripMenuItem("EMA");
            ToolStripMenuItem menuItem8 = new ToolStripMenuItem("DMA");
            ToolStripMenuItem menuItem9 = new ToolStripMenuItem("TMA");
            ToolStripMenuItem menuItem10 = new ToolStripMenuItem("Очистити графік");
            ToolStripMenuItem menuItem11 = new ToolStripMenuItem("Зберегти");
            ToolStripMenuItem menuItem12 = new ToolStripMenuItem("Вилучення аномалій");
            ToolStripMenuItem menuItem13 = new ToolStripMenuItem("Вилучення тренду");
            ToolStripMenuItem menuItem14 = new ToolStripMenuItem("Автокореляційна");
            ToolStripMenuItem menuItem15 = new ToolStripMenuItem("Автоковаріаційна");
            ToolStripMenuItem menuItem16 = new ToolStripMenuItem("Часовий ряд");
            ToolStripMenuItem menuItem17 = new ToolStripMenuItem("Декомпозиція");
            ToolStripMenuItem menuItem18 = new ToolStripMenuItem("Реконструкція");
            ToolStripMenuItem menuItem19 = new ToolStripMenuItem("Візуалізувати тренд");



            menuItem2.Click += MenuItem2_Click;
            menuItem1.Click += MenuItem1_Click; // Обробник події кліку
            menuItem3.Click += MenuItem3_Click;
            menuItem4.Click += MenuItem4_Click;
            menuItem5.Click += MenuItem5_Click;
            menuItem6.Click += MenuItem6_Click;
            menuItem7.Click += MenuItem7_Click;
            menuItem8.Click += MenuItem8_Click;
            menuItem9.Click += MenuItem9_Click;
            menuItem10.Click += MenuItem10_Click;
            menuItem11.Click += MenuItem11_Click;
            menuItem12.Click += MenuItem12_Click;
            menuItem13.Click += MenuItem13_Click;
            menuItem14.Click += MenuItem14_Click;
            menuItem15.Click += MenuItem15_Click;
            menuItem16.Click += MenuItem16_Click;
            menuItem17.Click += MenuItem17_Click;
            menuItem18.Click += MenuItem18_Click;
            menuItem19.Click += MenuItem19_Click;
            parentMenuItem.DropDownItems.Add(menuItem1);
            parentMenuItem.DropDownItems.Add(menuItem4);
            parentMenuItem.DropDownItems.Add(menuItem5);
            parentMenuItem.DropDownItems.Add(menuItem6);
            parentMenuItem.DropDownItems.Add(menuItem7);
            parentMenuItem.DropDownItems.Add(menuItem8);
            parentMenuItem.DropDownItems.Add(menuItem9);
            parentMenuItem11.DropDownItems.Add(menuItem2);
            parentMenuItem11.DropDownItems.Add(menuItem3);

            contextMenu.Items.AddRange(new[] {parentMenuItem, parentMenuItem11,menuItem12,menuItem13,menuItem14,menuItem15,menuItem19,menuItem16,menuItem17,menuItem18, menuItem10,menuItem11});
            // Додавання контекстного меню до Chart
            chart.ContextMenuStrip = contextMenu;
            return chart;
        }

        private void MenuItem18_Click(object sender, EventArgs e)
        {
            string[] promptValue = Prompt4.ShowDialog("Реконструція", "Реконструція");
            List<int> kompot = new List<int>();
            if (!string.IsNullOrEmpty(promptValue[0]))
            {
                for (int i = 0; i < dovgina_guseni; i++)
                {
                    kompot.Add(i);
                }
            }
            else if (!string.IsNullOrEmpty(promptValue[1]))
            {
                for (int i = 0; i < Convert.ToInt32(promptValue[1]); i++)
                {
                    kompot.Add(i);
                }
            }
            else if (!string.IsNullOrEmpty(promptValue[2]))
            {
                string[] str = promptValue[2].Split(' ');
                for (int i = 0; i < str.Length; i++)
                {
                    kompot.Add(Convert.ToInt16(str[i])-1);
                }
            }
            List<double> x_y_ser = Reconstruction(dovgina_guseni,kompot);
            Zmineni_dani = x_y_ser;
            MenuItem11_Click(null, EventArgs.Empty);
            
        }

        public DataGridView Get_table_eigen()
        {
            data_grid_eigen =new DataGridView();
            Size s = new Size(600, 500);
            return data_grid_eigen;
        }
        private void MenuItem19_Click(object sender, EventArgs e)
        {
            List<double> param_A = get_param_A_for_Deleting_Trend(Seriestime, 1);
           
            if (chart.Series.FindByName("Trend") != null)
            {
                chart.Series.Remove(chart.Series["Trend"]);
            }
           
            chart.Series.Add("Trend");
            chart.Series["Trend"].ChartType = SeriesChartType.Line;
            chart.Series["Trend"].BorderWidth = 3;
            for (int i = 0; i < Seriestime.Count; i++)
            {
                double novoutvoreniy_x = 0;
                for (int j = 0; j < param_A.Count; j++)
                {
                    novoutvoreniy_x += param_A[j] * Math.Pow(Time[i], j);
                }
                chart.Series["Trend"].Points.AddXY(Time[i], novoutvoreniy_x);
            }
            
            
        }

        private void MenuItem17_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Декомпозиція", "Довжина гусені");
            dovgina_guseni = Convert.ToInt16(promptValue);
            double[,] corelatu = Decomposition(Convert.ToInt16(promptValue));
            
            var box_list=form1.GetListBox1();
            for (int i = 0; i < corelatu.GetLength(0); i++)
            {
                List<double>komponent=new List<double>();
                for (int j = 0; j < corelatu.GetLength(1); j++)
                {
                    komponent.Add(corelatu[i,j]);
                }
                Universe.Data_Vectors.Add(komponent);
                box_list.Items.Add($"Компонента {i+1} ");
            }
            

        }

        private void MenuItem16_Click(object sender, EventArgs e)
        {
            MenuItem10_Click(null, EventArgs.Empty);
            if (chart.Series.FindByName("Ряд") != null)
            {
                chart.Series.Remove(chart.Series["Ряд"]);
            }
            if (chart.Series.FindByName("COR") != null)
            {
                chart.Series.Remove(chart.Series["COR"]);
            }
            if (chart.Series.FindByName("COV") != null)
            {
                chart.Series.Remove(chart.Series["COV"]);
            }
            chart.Series.Add("Ряд");

            chart.Series["Ряд"].ChartType = SeriesChartType.Line;
            chart.Series["Ряд"].MarkerStyle = MarkerStyle.Circle;
            chart.Series["Ряд"].BorderWidth = 3;
            chart.Series["Ряд"].MarkerSize = 6;
            
            for (int i = 0; i < Time.Count; i++)
            {
                chart.Series["Ряд"].Points.AddXY(Time[i], Seriestime[i]);

            }
        }

        private void MenuItem15_Click(object sender, EventArgs e)
        {
            MenuItem10_Click(null, EventArgs.Empty);
            if (chart.Series.FindByName("Ряд") != null)
            {
                chart.Series.Remove(chart.Series["Ряд"]);
            }
            if (chart.Series.FindByName("COR") != null)
            {
                chart.Series.Remove(chart.Series["COR"]);
            }
            if (chart.Series.FindByName("COV") != null)
            {
                chart.Series.Remove(chart.Series["COV"]);
            }
            chart.Series.Add("COV");

            chart.Series["COV"].ChartType = SeriesChartType.Line;
            chart.Series["COV"].MarkerStyle = MarkerStyle.Circle;
            chart.Series["COV"].BorderWidth = 3;
            chart.Series["COV"].MarkerSize = 6;
            List<double> corelatu = Auto_Kovariation();
            for (int i = 0; i < corelatu.Count; i++)
            {
                chart.Series["COV"].Points.AddXY(i, corelatu[i]);

            }
        }

        private void MenuItem14_Click(object sender, EventArgs e)
        {
            MenuItem10_Click(null, EventArgs.Empty);
            if (chart.Series.FindByName("Ряд") != null)
            {
                chart.Series.Remove(chart.Series["Ряд"]);
            }
            if (chart.Series.FindByName("COR") != null)
            {
                chart.Series.Remove(chart.Series["COR"]);
            }
            if (chart.Series.FindByName("COV") != null)
            {
                chart.Series.Remove(chart.Series["COV"]);
            }
            chart.Series.Add("COR");

            
            chart.Series["COR"].ChartType = SeriesChartType.Line;
            chart.Series["COR"].MarkerStyle = MarkerStyle.Circle;
            chart.Series["COR"].BorderWidth = 3;
            chart.Series["COR"].MarkerSize = 6;
            List<double> corelatu = Auto_Korelation();
            for (int i = 0; i < corelatu.Count; i++)
            {
                chart.Series["COR"].Points.AddXY(i, corelatu[i]);

            }


        }

        private void MenuItem13_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Вилучення тренду", "kє[1;2]");

            List<double> x_y_ser = Deleting_trend(Convert.ToInt16(promptValue));
            Zmineni_dani = x_y_ser;
            MenuItem11_Click(null, EventArgs.Empty);
            
        }

        private void MenuItem12_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Вилучення аномалій", "kє[3;9]");

            List<double> x_y_ser = Deleting_anomalia(Convert.ToInt16(promptValue));
            Zmineni_dani = x_y_ser;
            MenuItem11_Click(null, EventArgs.Empty);
        }

        private void Update_Chart()
        {
            if (chart.Series.FindByName("Ряд") != null)
            {
                chart.Series.Remove(chart.Series["Ряд"]);
            }
            chart.Series.Add("Ряд");

            chart.Series["Ряд"].IsVisibleInLegend = false;

            chart.Series["Ряд"].ChartType = SeriesChartType.Line;
            chart.Series["Ряд"].MarkerStyle = MarkerStyle.Circle;
            chart.Series["Ряд"].BorderWidth = 3;
            chart.Series["Ряд"].MarkerSize = 6;
            for (int i = 0; i < Time.Count; i++)
            {
                chart.Series["Ряд"].Points.AddXY(Time[i], Seriestime[i]);

            }
        }
        private void Update_table()
        {
            data_grid.Columns.Clear();
            data_grid.Rows.Clear();
            double kvantill = Program.Kvantil_Norm_distibution(1.0 - (_Alpha / 2.0));
            data_grid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            data_grid.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);
            data_grid.AllowUserToAddRows = false;
            data_grid.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;
            data_grid.RowHeadersVisible = false;
            data_grid.Columns.Add("1", "Характеристика");
            data_grid.Columns.Add("3", "Значення");
            data_grid.Columns.Add("4", "Висновок");
            double mean = Program.vect_seredne(Seriestime);
            double sigma = Program.vect_seredno_kvadratichnyh(Seriestime);
            data_grid.Rows.Add("Мат. сподівання", Math.Round(mean, 4), "  ");
            data_grid.Rows.Add("Дисперсія", Math.Round(Math.Pow(sigma, 2), 4), "  ");
            data_grid.Rows.Add("Сер. квадратичне", Math.Round(sigma, 4), "  ");
            double kriteriy = Kriteriy_znakik();
            if (Math.Abs(kriteriy) < kvantill)
            {
                data_grid.Rows.Add("Критерій знаків", Math.Round(kriteriy, 4), "Процес є стаціонарний");
            }
            else
            {
                if (kriteriy > kvantill)
                {
                    data_grid.Rows.Add("Критерій знаків", Math.Round(kriteriy, 4), "Тенденція до зростання");
                }
                else
                {
                    data_grid.Rows.Add("Критерій знаків", Math.Round(kriteriy, 4), "Тенденція до спадання");
                }
            }
            kriteriy = Kriteriy_Manna();
            if (Math.Abs(kriteriy) < kvantill)
            {
                data_grid.Rows.Add("Критерій Манна", Math.Round(kriteriy, 4), "Процес є стаціонарний");
            }
            else
            {
                if (kriteriy > kvantill)
                {
                    data_grid.Rows.Add("Критерій Манна", Math.Round(kriteriy, 4), "Тенденція до зростання");
                }
                else
                {
                    data_grid.Rows.Add("Критерій Манна", Math.Round(kriteriy, 4), "Тенденція до спадання");
                }
            }
            if (_Alpha == 0.05)
            {
                bool kr_ser = Kriteriy_Seriy();
                if (kr_ser)
                {
                    data_grid.Rows.Add("Критерій серій", " ", "Процес є стаціонарний");
                }
                else
                {
                    data_grid.Rows.Add("Критерій серій", " ", "Процес є нестаціонарний");
                }
                kr_ser = Kriteriy_Zrostauchih_Seriy();
                if (kr_ser)
                {
                    data_grid.Rows.Add("Критерій «зростаючих» і «спадаючих» серій", " ", "Процес є стаціонарний");
                }
                else
                {
                    data_grid.Rows.Add("Критерій «зростаючих» і «спадаючих» серій", " ", "Процес є нестаціонарний");
                }
            }

            kriteriy = Kriteriy_Abbe();
            if (Math.Abs(kriteriy) < kvantill)
            {
                data_grid.Rows.Add("Критерій Аббе", Math.Round(kriteriy, 4), "Процес є стаціонарний");
            }
            else
            {
                if (kriteriy > kvantill)
                {
                    data_grid.Rows.Add("Критерій Аббе", Math.Round(kriteriy, 4), "Тенденція до зростання");
                }
                else
                {
                    data_grid.Rows.Add("Критерій Аббе", Math.Round(kriteriy, 4), "Тенденція до спадання");
                }
            }
        }
        private void Update_table_2(double[,] matrix_x)
        {
            data_grid_eigen.Columns.Clear();
            data_grid_eigen.Rows.Clear();
            
            data_grid_eigen.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            data_grid_eigen.DefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Regular);
            data_grid_eigen.AllowUserToAddRows = false;
            data_grid_eigen.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;
            data_grid_eigen.RowHeadersVisible = false;

            for (int i = 0; i < matrix_x.GetLength(1) + 1; i++)
            {
                if (i == 0)
                {
                    data_grid_eigen.Columns.Add($"x{i}", $"  ");
                }
                else
                {
                    data_grid_eigen.Columns.Add($"x{i}", $"x{i}");
                }

            }
            for (int i = 0; i < matrix_x.GetLength(1); i++)
            {
                data_grid_eigen.Rows.Add($"x{i + 1}");
                for (int t = 0; t < matrix_x.GetLength(1); t++)
                {
                    data_grid_eigen.Rows[data_grid_eigen.Rows.Count - 1].Cells[t + 1].Value = Math.Round(matrix_x[t,i], 5);
                }
            }
            data_grid_eigen.Rows.Add("Власні числа");
            for (int i = 0; i < matrix_x.GetLength(1); i++)
            {
                data_grid_eigen.Rows[data_grid_eigen.Rows.Count - 1].Cells[i + 1].Value = Math.Round(vlasniss_chisla[i], 5);
            }

            data_grid_eigen.Rows.Add("% на напрям");
            for (int i = 0; i < matrix_x.GetLength(1); i++)
            {
                data_grid_eigen.Rows[data_grid_eigen.Rows.Count - 1].Cells[i + 1].Value = Math.Round((vlasniss_chisla[i] / Convert.ToDouble(vlasniss_chisla.Sum())) * 100.0, 5);
            }
            data_grid_eigen.Rows.Add("%Накопичений");
            for (int i = 0; i < matrix_x.GetLength(1); i++)
            {
                double suma_nakopich = 0;
                int t_plus_i = i + 1;
                for (int t = 0; t < t_plus_i; t++)
                {
                    suma_nakopich += (vlasniss_chisla[t] / Convert.ToDouble(vlasniss_chisla.Sum())) * 100.0;
                }
                data_grid_eigen.Rows[data_grid_eigen.Rows.Count - 1].Cells[i + 1].Value = Math.Round(suma_nakopich, 5);
            }
        }
        private void MenuItem11_Click(object sender, EventArgs e)
        {
            Seriestime = Zmineni_dani;
            List<double> Time11 = new List<double>();
            for (int i = 0; i < Zmineni_dani.Count; i++)
            {
                Time11.Add(Convert.ToDouble(i));
            }
            Time = Time11;
            MenuItem10_Click(null, EventArgs.Empty);
            Update_Chart();
            Update_table();
        }

        public DataGridView Get_table(double param_alpha)
        {
            double kvantill=Program.Kvantil_Norm_distibution(1.0-(param_alpha/2.0));
            _Alpha = param_alpha;
            data_grid =new DataGridView();
            Size s = new Size(600, 500);
            data_grid.Size = s;
            data_grid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            data_grid.DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);
            data_grid.Location = new Point(0, 0);
            data_grid.Columns.Clear();
            data_grid.Rows.Clear();
            data_grid.AllowUserToAddRows = false;
            data_grid.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;
            data_grid.RowHeadersVisible = false;
            data_grid.Columns.Add("1", "Характеристика");
            data_grid.Columns.Add("3", "Значення");
            data_grid.Columns.Add("4", "Висновок");
            double mean = Program.vect_seredne(Seriestime);
            double sigma = Program.vect_seredno_kvadratichnyh(Seriestime);
            data_grid.Rows.Add("Мат. сподівання", Math.Round(mean, 4), "  ");
            data_grid.Rows.Add("Дисперсія", Math.Round(Math.Pow(sigma,2), 4), "  ");
            data_grid.Rows.Add("Сер. квадратичне", Math.Round(sigma, 4), "  ");
            double kriteriy = Kriteriy_znakik();
            if (Math.Abs(kriteriy)<kvantill)
            {
                data_grid.Rows.Add("Критерій знаків", Math.Round(kriteriy, 4), "Процес є стаціонарний");
            }
            else
            {
                if (kriteriy>kvantill)
                {
                    data_grid.Rows.Add("Критерій знаків", Math.Round(kriteriy, 4), "Тенденція до зростання");
                }
                else
                {
                    data_grid.Rows.Add("Критерій знаків", Math.Round(kriteriy, 4), "Тенденція до спадання");
                }
            }
            kriteriy = Kriteriy_Manna();
            if (Math.Abs(kriteriy) < kvantill)
            {
                data_grid.Rows.Add("Критерій Манна", Math.Round(kriteriy, 4), "Процес є стаціонарний");
            }
            else
            {
                if (kriteriy > kvantill)
                {
                    data_grid.Rows.Add("Критерій Манна", Math.Round(kriteriy, 4), "Тенденція до зростання");
                }
                else
                {
                    data_grid.Rows.Add("Критерій Манна", Math.Round(kriteriy, 4), "Тенденція до спадання");
                }
            }
            if (param_alpha==0.05)
            {
                bool kr_ser = Kriteriy_Seriy();
                if (kr_ser)
                {
                    data_grid.Rows.Add("Критерій серій", " ", "Процес є стаціонарний");
                }
                else
                {
                    data_grid.Rows.Add("Критерій серій", " ", "Процес є нестаціонарний");
                }
                kr_ser = Kriteriy_Zrostauchih_Seriy();
                if (kr_ser)
                {
                    data_grid.Rows.Add("Критерій «зростаючих» і «спадаючих» серій", " ", "Процес є стаціонарний");
                }
                else
                {
                    data_grid.Rows.Add("Критерій «зростаючих» і «спадаючих» серій", " ", "Процес є нестаціонарний");
                }
            }
           
            kriteriy = Kriteriy_Abbe();
            if (Math.Abs(kriteriy) < kvantill)
            {
                data_grid.Rows.Add("Критерій Аббе", Math.Round(kriteriy, 4), "Процес є стаціонарний");
            }
            else
            {
                if (kriteriy > kvantill)
                {
                    data_grid.Rows.Add("Критерій Аббе", Math.Round(kriteriy, 4), "Тенденція до зростання");
                }
                else
                {
                    data_grid.Rows.Add("Критерій Аббе", Math.Round(kriteriy, 4), "Тенденція до спадання");
                }
            }
            return data_grid;
        }
        private void MenuItem9_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Інтервал ковзання", "Інтервал ковзання");
            List<double> x_t_new = TMA(Convert.ToInt16(promptValue));
            if (chart.Series.FindByName("TMA") != null)
            {
                chart.Series.Remove(chart.Series["TMA"]);
            }
            Zmineni_dani = x_t_new;
            chart.Series.Add("TMA");
            chart.Series["TMA"].ChartType = SeriesChartType.Line;
            chart.Series["TMA"].BorderWidth = 3;
            for (int i = Convert.ToInt16(promptValue); i < Seriestime.Count; i++)
            {
                chart.Series["TMA"].Points.AddXY(Time[i], x_t_new[i - Convert.ToInt16(promptValue)]);
            }
        }

        private void MenuItem8_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Інтервал ковзання", "Інтервал ковзання");
            List<double> x_t_new = DMA(Convert.ToInt16(promptValue));
            if (chart.Series.FindByName("DMA") != null)
            {
                chart.Series.Remove(chart.Series["DMA"]);
            }
            Zmineni_dani = x_t_new;
            chart.Series.Add("DMA");
            chart.Series["DMA"].ChartType = SeriesChartType.Line;
            chart.Series["DMA"].BorderWidth = 3;
            for (int i = Convert.ToInt16(promptValue); i < Seriestime.Count; i++)
            {
                chart.Series["DMA"].Points.AddXY(Time[i], x_t_new[i - Convert.ToInt16(promptValue)]);
            }
        }

        private void MenuItem7_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Інтервал ковзання", "Інтервал ковзання");
            List<double> x_t_new = EMA(Convert.ToInt16(promptValue));
            if (chart.Series.FindByName("EMA") != null)
            {
                chart.Series.Remove(chart.Series["EMA"]);
            }
            chart.Series.Add("EMA");
            Zmineni_dani = x_t_new;
            chart.Series["EMA"].ChartType = SeriesChartType.Line;
            chart.Series["EMA"].BorderWidth= 3;
            for (int i = Convert.ToInt16(promptValue); i < Seriestime.Count; i++)
            {
                chart.Series["EMA"].Points.AddXY(Time[i], x_t_new[i - Convert.ToInt16(promptValue)]);
            }
        }

        private void MenuItem6_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Інтервал ковзання", "Інтервал ковзання");
            List<double> x_t_new = WMA(Convert.ToInt16(promptValue));
            if (chart.Series.FindByName("WMA") != null)
            {
                chart.Series.Remove(chart.Series["WMA"]);
            }
            Zmineni_dani = x_t_new;
            chart.Series.Add("WMA");
            chart.Series["WMA"].ChartType = SeriesChartType.Line;
            chart.Series["WMA"].BorderWidth = 3;
            for (int i = Convert.ToInt16(promptValue); i < Seriestime.Count; i++)
            {
                chart.Series["WMA"].Points.AddXY(Time[i], x_t_new[i - Convert.ToInt16(promptValue)]);
            }
        }

        private void MenuItem10_Click(object sender, EventArgs e)
        {
            if (chart.Series.FindByName("ковзне середнє") != null)
            {
                chart.Series.Remove(chart.Series["ковзне середнє"]);
            }
            if (chart.Series.FindByName("медіанне") != null)
            {
                chart.Series.Remove(chart.Series["медіанне"]);
            }
            if (chart.Series.FindByName("SMA") != null)
            {
                chart.Series.Remove(chart.Series["SMA"]);
            }
            if (chart.Series.FindByName("WMA") != null)
            {
                chart.Series.Remove(chart.Series["WMA"]);
            }
            if (chart.Series.FindByName("EMA") != null)
            {
                chart.Series.Remove(chart.Series["EMA"]);
            }
            if (chart.Series.FindByName("DMA") != null)
            {
                chart.Series.Remove(chart.Series["DMA"]);
            }
            if (chart.Series.FindByName("TMA") != null)
            {
                chart.Series.Remove(chart.Series["TMA"]);
            }
            if (chart.Series.FindByName("Метод Гусені") != null)
            {
                chart.Series.Remove(chart.Series["Метод Гусені"]);
            }
            if (chart.Series.FindByName("Метод Гусені test") != null)
            {
                chart.Series.Remove(chart.Series["Метод Гусені test"]);
            }
            if (chart.Series.FindByName("Trend") != null)
            {
                chart.Series.Remove(chart.Series["Trend"]);
            }
            //"Метод Гусені"
        }

        private void MenuItem5_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Інтервал ковзання", "Інтервал ковзання");
            List<double> x_t_new = SMA(Convert.ToInt16(promptValue));
            if (chart.Series.FindByName("SMA") != null)
            {
                chart.Series.Remove(chart.Series["SMA"]);
            }
            Zmineni_dani = x_t_new;
            chart.Series.Add("SMA");
            chart.Series["SMA"].ChartType = SeriesChartType.Line;
            chart.Series["SMA"].BorderWidth = 3;
            for (int i = Convert.ToInt16(promptValue); i < Seriestime.Count; i++)
            {
                chart.Series["SMA"].Points.AddXY(Time[i], x_t_new[i- Convert.ToInt16(promptValue)]);
            }
        }
        
        private void MenuItem4_Click(object sender, EventArgs e)
        {

            List<double> x_t_new = Median_zlahodguvanya();
            if (chart.Series.FindByName("медіанне") != null)
            {
                chart.Series.Remove(chart.Series["медіанне"]);
            }
            chart.Series.Add("медіанне");
            Zmineni_dani = x_t_new;
            chart.Series["медіанне"].ChartType = SeriesChartType.Line;
            chart.Series["медіанне"].BorderWidth = 3;
            for (int i = 0; i < Seriestime.Count; i++)
            {
                chart.Series["медіанне"].Points.AddXY(Time[i], x_t_new[i]);
            }
        }

        private void MenuItem3_Click(object sender, EventArgs e)
        {
            //метод Гусенні
            Prognoz_value = new List<double>();
            //int lenght_of_gusenni = Convert.ToInt32(Time.Count / 5);
            string[] promptValue = Prompt2.ShowDialog("Довжина гусені", "к-сть точок для прогнозу");
            if (promptValue[0].Length <= 0)
            {
                return;
            }
            int lenght_of_gusenni = Convert.ToInt32(promptValue[0]);
            int count_of_point_to_prognoz = Convert.ToInt32(promptValue[1]);
            
            double[,] matrix_XX = new double[lenght_of_gusenni, Time.Count - lenght_of_gusenni+1- count_of_point_to_prognoz];

            for (int i = 0; i < lenght_of_gusenni; i++)
            {
                for (int t = 0; t < Time.Count - lenght_of_gusenni- count_of_point_to_prognoz+1; t++)
                {
                    matrix_XX[i, t] = Seriestime[i + t];
                }
            }


            for (int ttr = 0; ttr < count_of_point_to_prognoz; ttr++)
            {
                double[,] S_matrix = Program.multiplication_matrix_on_matrix(matrix_XX, Program.Transponovana_matrix(matrix_XX));
                double[,] vlasni_vectory = Eigen_vectors(S_matrix);
                double[,] A_matrix = matrix_A_without_end_row_col(vlasni_vectory);
                List<double> X_col_end = Last_col_of_X(matrix_XX);
                List<double> Y_col = Program.multiplication_matrix_on_colums(Program.Inverse_matrix(A_matrix), X_col_end);
                List<double> A_col = Last_row_of_A(vlasni_vectory);
                double the_new_prognoz = Program.multiplication__rows_on_colums(A_col, Y_col);
                matrix_XX = matrix_plus_prognoz(matrix_XX, the_new_prognoz);
                Prognoz_value.Add(the_new_prognoz);
            }
            



            // матркалід з лекціїї
            if (chart.Series.FindByName("Метод Гусені test") != null)
            {
                chart.Series.Remove(chart.Series["Метод Гусені test"]);
            }
            chart.Series.Add("Метод Гусені test");
            chart.Series["Метод Гусені test"].ChartType = SeriesChartType.Line;
            chart.Series["Метод Гусені test"].BorderWidth = 3;
            for (int i = 0; i < count_of_point_to_prognoz; i++)
            {
                
                chart.Series["Метод Гусені test"].Points.AddXY(Time[Time.Count - Prognoz_value.Count+i], Prognoz_value[i]);

            }
        }

        private void MenuItem2_Click(object sender, EventArgs e)
        {
            //метод Гусенні
            Prognoz_value=new List<double>();
            //int lenght_of_gusenni = Convert.ToInt32(Time.Count / 5);
            string[] promptValue = Prompt2.ShowDialog("Довжина гусені", "к-сть точок для прогнозу");
            if (promptValue[0].Length <= 0)
            {
                return;
            }
            int lenght_of_gusenni = Convert.ToInt32(promptValue[0]);
            int count_of_point_to_prognoz = Convert.ToInt32(promptValue[1]);
            //сформолюємо матрицю Х
            double[,] matrix_XX = new double[lenght_of_gusenni, Time.Count-lenght_of_gusenni+1];
            
            for (int i = 0; i < lenght_of_gusenni; i++)
            {
                for (int t = 0; t < Time.Count - lenght_of_gusenni+1; t++)
                {
                    matrix_XX[i, t] = Seriestime[i + t];
                }
            }

            //////////////SVD forecasting/////

            for (int ttr = 0; ttr < count_of_point_to_prognoz; ttr++)
            {
                double[,] S_matrix = Program.multiplication_matrix_on_matrix(matrix_XX, Program.Transponovana_matrix(matrix_XX));
                double[,] vlasni_vectory = Eigen_vectors(S_matrix);
                double[,] A_matrix = matrix_A_without_end_row_col(vlasni_vectory);
                List<double> X_col_end = Last_col_of_X(matrix_XX);
                List<double> Y_col = Program.multiplication_matrix_on_colums(Program.Inverse_matrix(A_matrix), X_col_end);
                List<double> A_col = Last_row_of_A(vlasni_vectory);
                double the_new_prognoz = Program.multiplication__rows_on_colums(A_col, Y_col);
                matrix_XX = matrix_plus_prognoz(matrix_XX, the_new_prognoz);
                Prognoz_value.Add(the_new_prognoz);
            }


            if (chart.Series.FindByName("Метод Гусені") != null)
            {
                chart.Series.Remove(chart.Series["Метод Гусені"]);
            }
            chart.Series.Add("Метод Гусені");
            chart.Series["Метод Гусені"].ChartType = SeriesChartType.Line;
            chart.Series["Метод Гусені"].BorderWidth = 3;
            for (int i = 0; i < count_of_point_to_prognoz; i++)
            {
                //chart.Series["Метод Гусені"].Points.AddXY(Time[Time.Count-1]+(i+1)*(Time[Time.Count - 1]- Time[Time.Count - 2]), Prognoz_value[i]);
                chart.Series["Метод Гусені"].Points.AddXY(Time[Time.Count - 1] + (i + 1), Prognoz_value[i]);

            }



        }

        private void MenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ковзне середнє");
            //string promptValue = Prompt.ShowDialog("Значення K", "Вибір кроку");
            string[] promptValue = Prompt2.ShowDialog("Степінь полінома", "к-сть точок");
            if (promptValue[0].Length <= 0)
            {
                return;
            }
            
            int krok = Convert.ToInt16(promptValue[1]);
            if (krok % 2 == 0)
            {
                krok = krok + 1;
            }
            List<int> spisok_T = new List<int>();
            int The_M = (krok - 1) / 2;
            for (int i = -1 * The_M; i <= The_M; i++)
            {
                spisok_T.Add(i);
            }
            List<double> x_t_new = new List<double>();

            for (int i = The_M; i < Seriestime.Count-The_M; i++)
            {
               
                List<double> t_new = new List<double>();
                for (int t = i-The_M; t <= i+The_M; t++)
                {
                    t_new.Add(Seriestime[t]);
                }
                List<double> param_A = get_param_A_for_mean(krok, t_new, Convert.ToInt16(promptValue[0]));
                
                x_t_new.Add(param_A[0]);

            }

            for (int i = 0; i < 2; i++)
            {
                if (i==0)
                {
                    List<double> t_new = new List<double>();
                    for (int t = 0; t < krok; t++)
                    {
                        t_new.Add(Seriestime[t]);
                    }
                    List<double> param_A1 = get_param_A_for_mean(krok, t_new, Convert.ToInt16(promptValue[0]));
                    for (int t = 0; t < The_M; t++)
                    {
                        double novoutvoreniy_x = 0;
                        for (int j = 0; j < param_A1.Count; j++)
                        {
                            novoutvoreniy_x += param_A1[j] * Math.Pow(spisok_T[t], j);
                        }
                        x_t_new.Insert(t,novoutvoreniy_x);
                    }                  
                }
                else if (i==1)
                {
                    List<double> t_new = new List<double>();
                    for (int t = Seriestime.Count-krok; t < Seriestime.Count; t++)
                    {
                        t_new.Add(Seriestime[t]);
                    }
                    List<double> param_A1 = get_param_A_for_mean(krok, t_new, Convert.ToInt16(promptValue[0]));
                    for (int t = The_M+1; t < krok; t++)
                    {
                        double novoutvoreniy_x = 0;
                        for (int j = 0; j < param_A1.Count; j++)
                        {
                            novoutvoreniy_x += param_A1[j] * Math.Pow(spisok_T[t], j);
                        }
                        x_t_new.Insert(x_t_new.Count, novoutvoreniy_x);
                    }
                }
            }
            
            if (chart.Series.FindByName("ковзне середнє") != null)
            {
                chart.Series.Remove(chart.Series["ковзне середнє"]);
            }
            chart.Series.Add("ковзне середнє");
            chart.Series["ковзне середнє"].ChartType = SeriesChartType.Line;
            chart.Series["ковзне середнє"].BorderWidth = 3;
            Zmineni_dani = x_t_new;
            for (int i = 0; i < Time.Count; i++)
            {
                chart.Series["ковзне середнє"].Points.AddXY(Time[i], x_t_new[i]);
                
            }

        }

        private List<double> get_param_A_for_mean(int K_krok,List<double> the_x,int stepin_polinoma)
        {
            //ковзне середнє
            int K_for_shag = 3;
            if (K_krok % 2 == 0)
            {
                K_for_shag = K_krok + 1;
            }
            else
            {
                K_for_shag = K_krok;
            }
            //список для Т
            List < double > spisok_T= new List<double>();
            int The_M=(K_for_shag-1)/ 2;
            for (int i = -1*The_M; i <= The_M; i++)
            {
                spisok_T.Add(Convert.ToDouble(i));
            }
            double[,] matrix_par = BuildMatrixT(spisok_T, K_for_shag - 1,stepin_polinoma);
            double[,] invs = Program.Inverse_matrix(matrix_par);
            List<double> y_col = Y_column_param(spisok_T, the_x,stepin_polinoma);
            List<double> result = Program.multiplication_matrix_on_colums(Program.Inverse_matrix(matrix_par), y_col);
            return result;

        }
        static double[,] BuildMatrixT(List<double> t_list, int n, int stepin_polinoma)
        {
            double[,] matrix = new double[stepin_polinoma+1, stepin_polinoma+1];

            for (int i = 0; i <= stepin_polinoma; i++)
            {
                for (int j = 0; j <= stepin_polinoma; j++)
                {
                    matrix[i, j] = Suma_in_pow(t_list, i + j);
                }
            }

            return matrix;
        }
        static List<double> Y_column_param(List<double> t_list, List<double> the_x, int stepin_polinoma)
        {
            List<double> matrix = new List<double>();

            for (int i = 0; i < stepin_polinoma+1; i++)
            {
                matrix.Add(Suma_in_pow_Y(t_list,the_x, i ));
            }

            return matrix;
        }
        private List<double> Y_column_param_deleting_trend(List<double> t_list, List<double> the_x, int stepin_polinoma)
        {
            List<double> matrix = new List<double>();

            for (int i = 0; i < stepin_polinoma + 1; i++)
            {
                double xxw = Suma_in_pow_Y(t_list, the_x, i);
                xxw = xxw / Convert.ToDouble(Seriestime.Count);
                matrix.Add(xxw);
            }

            return matrix;
        }
        private List<double> Last_col_1_rows_of_X(double[,] mtrx)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < mtrx.GetLength(0)-1; i++)
            {
                result.Add(mtrx[i, mtrx.GetLength(1)-1]);
            }
            return result;
        }
        static double Suma_in_pow(List<double> t_list, int power)
        {
            double sum_of_T = 0;

            for (int i = 0; i < t_list.Count; i++)
            {
                sum_of_T +=Convert.ToDouble(Math.Pow(t_list[i], power));
            }

            return sum_of_T;
        }
        static double Suma_in_pow_Y(List<double> t_list, List<double> the_x, int power)
        {
            double sum_of_T = 0;

            for (int i = 0; i < t_list.Count; i++)
            {
                sum_of_T += Convert.ToDouble(Math.Pow(t_list[i], power) ) *the_x[i];
            }

            return sum_of_T;
        }
        public double[,] Averege_matrix(double[,] matrix)
        {
            double[,] result = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                
                for (int t = 0; t < matrix.GetLength(1); t++)
                {
                    double the_sum_diagonal = 0;
                    double count_of_diagonal_element = 0;
                    int the_index_row = i;
                    int the_index_column = t;
                    List<int[]> LIST_INDEX_DIAGONAL = Index_of_giagona(matrix.GetLength(0), matrix.GetLength(1), the_index_row, the_index_column);
                    for (int k = 0; k < LIST_INDEX_DIAGONAL.Count; k++)
                    {
                        the_sum_diagonal += matrix[LIST_INDEX_DIAGONAL[k][0], LIST_INDEX_DIAGONAL[k][1]];
                        count_of_diagonal_element += 1.0;
                    }
                    result[i,t] = (the_sum_diagonal + matrix[i,t])/(count_of_diagonal_element+1.0);
                    
                }
            }
            return result;
        }

        public double[,] AverageMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double sum = 0;
                    int count = 0;

                    // Рух ліворуч вгору
                    int row = i, col = j;
                    while (row >= 0 && col >= 0)
                    {
                        sum += matrix[row, col];
                        count++;
                        row--;
                        col--;
                    }

                    // Рух праворуч вниз, починаючи з наступного елемента
                    row = i + 1;
                    col = j + 1;
                    while (row < rows && col < cols)
                    {
                        sum += matrix[row, col];
                        count++;
                        row++;
                        col++;
                    }

                    result[i, j] = sum / count;
                }
            }

            return result;
        }

        public List<int[]> Index_of_giagona(int row, int col, int index_i, int index_j)
        {
            List<int[]> result = new List<int[]>();
            int index_the_col = index_j-1;
            int index_the_row = index_i+1;
            while (index_the_col >= 0 && index_the_row < row)
            {
                int[] indexxx = new int[2];
                indexxx[0] = index_the_row;
                indexxx[1] = index_the_col;
                index_the_row++;
                index_the_col--;

                result.Add(indexxx);
            }

            index_the_col = index_j+1;
            index_the_row = index_i-1;
            while (index_the_col < col && index_the_row >= 0)
            {
                int[] indexxx = new int[2];
                indexxx[0] = index_the_row;
                indexxx[1] = index_the_col;
                index_the_row--;
                index_the_col++;

                result.Add(indexxx);
            }
            return result;


            //List<int[]> result = new List<int[]>();
            //int index_the_col = index_j - 1;
            //int index_the_row = index_i - 1;
            //while (index_the_col >=0 && index_the_row >= 0)
            //{
            //    int[] indexxx = new int[2];
            //    indexxx[0] = index_the_row;
            //    indexxx[1] = index_the_col;
            //    index_the_row--;
            //    index_the_col--;
            //
            //    result.Add(indexxx);
            //}
            //
            //index_the_col = index_j + 1;
            //index_the_row = index_i + 1;
            //while (index_the_col < col && index_the_row < row)
            //{
            //    int[] indexxx = new int[2];
            //    indexxx[0] = index_the_row;
            //    indexxx[1] = index_the_col;
            //    index_the_row++;
            //    index_the_col++;
            //
            //    result.Add(indexxx);
            //}
            //return result;
        }
        private List<double> Get_Row(double[,] mtrx,int k)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < mtrx.GetLength(1); i++)
            {
                result.Add(mtrx[k,i]);
            }
            return result;
        }
        private List<double> Get_Column(double[,] mtrx, int k)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < mtrx.GetLength(0); i++)
            {
                result.Add(mtrx[i, k]);
            }
            return result;
        }
        private double[,] matrix_A_for_first_K(double[,] mtrx, int stepin_polinoma)
        {
            //ковзне середнє
            //double[,] result = new double[mtrx.GetLength(0), mtrx.GetLength(1)];
            //for (int i = 0; i < mtrx.GetLength(0); i++)
            //{
            //
            //    for (int t = 0; t < mtrx.GetLength(1); t++)
            //    {
            //
            //        if (t < stepin_polinoma)
            //        {
            //            result[i, t] = mtrx[i, t];
            //        }
            //        else
            //        {
            //            result[i, t] = 0.0;
            //        }
            //        
            //    }
            //}
            double[,] result = new double[stepin_polinoma, mtrx.GetLength(1)];
            for (int i = 0; i < stepin_polinoma; i++)
            {

                for (int t = 0; t < mtrx.GetLength(1); t++)
                {
                        result[i, t] = mtrx[i, t];
                    
                    
                }
            }
            return result;


        }
        private double[,] matrix_A_for_first_K_2(double[,] mtrx, int stepin_polinoma)
        {
            
            double[,] result = new double[mtrx.GetLength(0), stepin_polinoma];
            for (int i = 0; i < mtrx.GetLength(0); i++)
            {

                for (int t = 0; t < stepin_polinoma; t++)
                {
                    result[i, t] = mtrx[i, t];


                }
            }
            return result;


        }
        private double[,] Diagonal_matrix_A_for_first_K(List<double> mtrx, int stepin_polinoma)
        {
            //ковзне середнє
            double[,] result = new double[stepin_polinoma, stepin_polinoma];
            for (int i = 0; i < stepin_polinoma; i++)
            {

                for (int t = 0; t < stepin_polinoma; t++)
                {

                    if (t ==i)
                    {
                        result[i, t] = mtrx[i];
                    }
                    else
                    {
                        result[i, t] = 0.0;
                    }

                }
            }
            return result;


        }
        private double[,] Diagonal_matrix_A_with_count_of_column(List<double> mtrx, int stepin_polinoma)
        {
            //ковзне середнє
            double[,] result = new double[mtrx.Count, stepin_polinoma];
            for (int i = 0; i < mtrx.Count; i++)
            {

                for (int t = 0; t < stepin_polinoma; t++)
                {

                    if (t == i)
                    {
                        result[i, t] = mtrx[i];
                    }
                    else
                    {
                        result[i, t] = 0.0;
                    }

                }
            }
            return result;


        }
        private double[,] matrix_plus_prognoz(double[,] mtrx, double value)
        {
            //ковзне середнє
            double[,] result = new double[mtrx.GetLength(0), mtrx.GetLength(1)+1];
            result[mtrx.GetLength(0)-1, mtrx.GetLength(1)]=value;
            for (int i = 0; i < mtrx.GetLength(0); i++)
            {

                for (int t = 0; t < mtrx.GetLength(1); t++)
                {
                    result[i, t] = mtrx[i, t];

                }
            }
            //остнні стовпець
            for (int i = 0; i < mtrx.GetLength(0)-1; i++)
            {

               result[i, mtrx.GetLength(1)] = mtrx[i+1, mtrx.GetLength(1)-1];

            }
            return result;


        }

        private double[,] matrix_A_without_end_row_col(double[,] mtrx)
        {
            //ковзне середнє
            double[,] result = new double[mtrx.GetLength(0)-1, mtrx.GetLength(1) - 1];
            for (int i = 0; i < mtrx.GetLength(0)-1; i++)
            {

                for (int t = 0; t < mtrx.GetLength(1)-1; t++)
                {  
                    result[i, t] = mtrx[i, t];

                }
            }
            return result;


        }

        private double[,] matrix_A_without_end_col(double[,] mtrx)
        {
            //ковзне середнє
            double[,] result = new double[mtrx.GetLength(0), mtrx.GetLength(1)-1];
            for (int i = 0; i < mtrx.GetLength(0); i++)
            {
                for (int t = 0; t < mtrx.GetLength(1) - 1; t++)
                {
                    result[i, t] = mtrx[i, t];
                }
            }
            return result;
        }

        private double[,] rest_lust_columns(double[,] mtrx,int count)
        {
            
            double[,] result = new double[mtrx.GetLength(0), count];
            for (int i = 0; i < mtrx.GetLength(0); i++)
            {
                int index = 0;
                for (int t = mtrx.GetLength(1)-count; t < mtrx.GetLength(1); t++)
                {
                    result[i, index] = mtrx[i, t];
                    index++;
                }
            }
            return result;
        }

        private List<double> Last_col_of_X(double[,] mtrx)
        {
            //ковзне середнє
            List<double> result = new List<double>();
            for (int i = 1; i < mtrx.GetLength(0); i++)
            {
                result.Add(mtrx[i, mtrx.GetLength(1)-1]);
            }
            return result;


        }
        private List<double> Last_col_of_A(double[,] mtrx)
        {
            //ковзне середнє
            List<double> result = new List<double>();
            for (int i = 0; i < mtrx.GetLength(0)-1; i++)
            {
                result.Add(mtrx[i, mtrx.GetLength(1) - 1]);
            }
            return result;


        }
        private List<double> Last_row_of_A(double[,] mtrx)
        {
            //ковзне середнє
            List<double> result = new List<double>();
            for (int i = 0; i < mtrx.GetLength(1)-1; i++)
            {
                result.Add(mtrx[mtrx.GetLength(0)-1, i]);
            }
            return result;


        }
        public static class Prompt
        {
            //public static TextBox textBox_c1;
            //public static TextBox textBox_c2;

            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {

                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };

                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }


        public static class Prompt2
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
                Label textLabel = new Label() { Left = 50, Top = 30, Text = text };
                Label textLabel1 = new Label() { Left = 50, Top = 80, Text = caption };
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
                return text_list_box;
            }
        }

        private double[,] Eigen_vectors(double[,] mtrx)
        {
            //ковзне середнє
            var matrix = DenseMatrix.OfArray(mtrx);

            var evd = matrix.Evd();
            var eigenValues = evd.EigenValues;
            var eigenVectors = evd.EigenVectors;

            // Співвіднесення власних значень з власними векторами
            var eigenPairs = eigenValues
                .Select((value, index) => new { Value = value, Vector = eigenVectors.Column(index) })
                .ToList();

            eigenPairs.Sort((pair1, pair2) => pair2.Value.Magnitude.CompareTo(pair1.Value.Magnitude));

            var sortedEigenVectorsMatrix = DenseMatrix.Create(eigenVectors.RowCount, eigenVectors.ColumnCount, 0);
            for (int i = 0; i < eigenPairs.Count; i++)
            {
                sortedEigenVectorsMatrix.SetColumn(i, eigenPairs[i].Vector);
            }
            double[,] sortedEigenVectors = sortedEigenVectorsMatrix.ToArray();

            return sortedEigenVectors;
        }
        private List<double> Eigen_value(double[,] mtrx)
        {

            var matrix = DenseMatrix.OfArray(mtrx);
            var evd = matrix.Evd();

            // Власні числа
            var eigenValues = evd.EigenValues;

            List<double> eigenValuesReal = new List<double>();
            foreach (var value in evd.EigenValues)
            {
                eigenValuesReal.Add(value.Real);
            }

            eigenValuesReal.Sort((a, b) => Math.Abs(b).CompareTo(Math.Abs(a)));
            return eigenValuesReal;
        }

        static double[,] OuterProduct(List<double> a, List<double> b)
        {
            double[,] result = new double[a.Count, b.Count];

            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < b.Count; j++)
                {
                    result[i, j] = a[i] * b[j];
                }
            }

            return result;
        }

        public static class Prompt3
        {
            public static TextBox textBox_c1;
            public static TextBox textBox_c2;

            public static string[] ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 280, // Збільшення висоти форми для додаткового рядка
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                string[] text_list_box = new string[3];
                Label textLabel = new Label() { Left = 50, Top = 20, Width = 400, Text = "Розмір гусениці" };
                Label textLabel1 = new Label() { Left = 50, Top = 70, Width = 400,Text = "К-сть компонент" };
                Label textLabel2 = new Label() { Left = 50, Top = 120, Width = 400, Text = "К-сть точок для прогнозування" }; // Новий рядок
                TextBox textBox = new TextBox() { Left = 50, Top = 40, Width = 400 };
                TextBox textBox1 = new TextBox() { Left = 50, Top = 90, Width = 400 };
                TextBox textBox2 = new TextBox() { Left = 50, Top = 140, Width = 400 }; // Нове текстове поле
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 110, Top = 190, DialogResult = DialogResult.OK }; // Зміщення кнопки вниз
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(textBox1);
                prompt.Controls.Add(textBox2); // Додавання нового текстового поля
                prompt.Controls.Add(textLabel1);
                prompt.Controls.Add(textLabel2); // Додавання нової мітки
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                // Збільшення шрифту для всіх міток та текстових полів
                var font = new Font(prompt.Font.FontFamily, prompt.Font.Size + 2, FontStyle.Regular);
                textLabel.Font = font;
                textLabel1.Font = font;
                textLabel2.Font = font;
                textBox.Font = font;
                textBox1.Font = font;
                textBox2.Font = font;
                confirmation.Font = font;

                text_list_box[0] = "";
                text_list_box[1] = "";
                text_list_box[2] = ""; // Ініціалізація для нового рядка
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    text_list_box[0] = textBox.Text;
                    text_list_box[1] = textBox1.Text;
                    text_list_box[2] = textBox2.Text; // Зберігання значення з нового текстового поля
                    return text_list_box;
                }
                return text_list_box;
            }
        }

        public static class Prompt4
        {
            public static TextBox textBox_c1;
            public static TextBox textBox_c2;

            public static string[] ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 280, // Збільшення висоти форми для додаткового рядка
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                string[] text_list_box = new string[3];
                Label textLabel = new Label() { Left = 50, Top = 20, Width = 500, Text = "Повернутись по всім" };
                Label textLabel1 = new Label() { Left = 50, Top = 70, Width = 500, Text = "Повернутись по першим" };
                Label textLabel2 = new Label() { Left = 50, Top = 120, Width = 500, Text = "Декілька компонент" }; // Новий рядок
                TextBox textBox = new TextBox() { Left = 50, Top = 40, Width = 400 };
                TextBox textBox1 = new TextBox() { Left = 50, Top = 90, Width = 400 };
                TextBox textBox2 = new TextBox() { Left = 50, Top = 140, Width = 400 }; // Нове текстове поле
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 110, Top = 190, DialogResult = DialogResult.OK }; // Зміщення кнопки вниз
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(textBox1);
                prompt.Controls.Add(textBox2); // Додавання нового текстового поля
                prompt.Controls.Add(textLabel1);
                prompt.Controls.Add(textLabel2); // Додавання нової мітки
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                // Збільшення шрифту для всіх міток та текстових полів
                var font = new Font(prompt.Font.FontFamily, prompt.Font.Size + 2, FontStyle.Regular);
                textLabel.Font = font;
                textLabel1.Font = font;
                textLabel2.Font = font;
                textBox.Font = font;
                textBox1.Font = font;
                textBox2.Font = font;
                confirmation.Font = font;

                text_list_box[0] = "";
                text_list_box[1] = "";
                text_list_box[2] = ""; // Ініціалізація для нового рядка
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    text_list_box[0] = textBox.Text;
                    text_list_box[1] = textBox1.Text;
                    text_list_box[2] = textBox2.Text; // Зберігання значення з нового текстового поля
                    return text_list_box;
                }
                return text_list_box;
            }
        }

        private List<double> Auto_Korelation()
        {
            double sigma_2 = 0;
            double mean = Program.vect_seredne(Seriestime);
            for (int i = 0; i <Seriestime.Count; i++)
            {
                sigma_2 += Math.Pow(Seriestime[i] - mean, 2.0);
            }
            sigma_2 = sigma_2 / Convert.ToDouble(Seriestime.Count);
            List<double> result = new List<double>();
            for (int i = 1; i < Seriestime.Count - 1; i++)
            {
                double the_new_phi_teta = 0;
                for (int t = 0; t < Seriestime.Count - i; t++)
                {
                    the_new_phi_teta += (Seriestime[t] - mean) * (Seriestime[t + i] - mean);
                }
                the_new_phi_teta = the_new_phi_teta / (Convert.ToDouble(Seriestime.Count - 1));
                the_new_phi_teta = the_new_phi_teta / sigma_2;
                result.Add(the_new_phi_teta);
            }

            return result;
        }
        private List<double> Auto_Kovariation()
        {
            List<double> result=new List<double>();
            double mean = Program.vect_seredne(Seriestime);
            for (int i = 1; i < Seriestime.Count-1; i++)
            {
                double the_new_phi_teta = 0;
                for (int t = 0; t < Seriestime.Count-i; t++)
                {
                    the_new_phi_teta += (Seriestime[t] - mean) * (Seriestime[t + i] - mean);
                }
                the_new_phi_teta = the_new_phi_teta / (Convert.ToDouble(Seriestime.Count - 1));
                result.Add(the_new_phi_teta);
            }
           
            return result;
        }

        private List<double> Deleting_anomalia(int K_k)
        {
            List<double> result = new List<double>();
            result.Add(Seriestime[0]);
            result.Add(Seriestime[1]);
            double k_dbl=Convert.ToDouble(K_k);

            for (int i = 1; i < Seriestime.Count-1; i++)
            {
                double x_i_mean = 0;
                for (int t = 0; t < i; t++)
                {
                    x_i_mean += Seriestime[t];
                }
                x_i_mean = x_i_mean / Convert.ToDouble(i+1);

                double x_i_S = 0;
                for (int t = 0; t < i; t++)
                {
                    x_i_S += Math.Pow(Seriestime[t] - x_i_mean,2.0);
                }
                x_i_S=x_i_S / Convert.ToDouble(i);
                if (Seriestime[i + 1] < x_i_mean + k_dbl * Math.Sqrt(x_i_S) && (Seriestime[i + 1] > x_i_mean - k_dbl * Math.Sqrt(x_i_S)))
                {
                    result.Add(Seriestime[i + 1]);
                }
                else
                {
                    double alternative = 2.0 * Seriestime[i] - Seriestime[i - 1];
                    result.Add(alternative);
                };
               
            }

            return result;
        }

        private double Kriteriy_znakik()
        {
            double C_i = 0;
            for (int i = 1; i < Seriestime.Count-1; i++)
            {
                if (Seriestime[i] > Seriestime[i-1])
                {
                    C_i += 1.0;
                }
            }
            double D_c = Convert.ToDouble(Seriestime.Count + 1) / 12.0;
            double E_c = Convert.ToDouble(Seriestime.Count - 1) / 2.0;
            double C_static =( C_i - E_c)/Math.Sqrt(D_c);
            //C_static = Math.Abs(C_static);
            return C_static;
        }
        private double Kriteriy_Manna()
        {
            List<double> result = new List<double>();
            double T_ij = 0;
            for (int i = 1; i < Seriestime.Count; i++)
            {
                for (int t = i+1; t < Seriestime.Count-1; t++)
                {
                    if (Seriestime[i] < Seriestime[t])
                    {
                        T_ij += 1.0;
                    }
                    else if (Seriestime[i] == Seriestime[t])
                    {
                        T_ij += 0.5;
                    }
                }
                
            }
            double D_c = (Convert.ToDouble(2*Seriestime.Count + 5)* Convert.ToDouble(Seriestime.Count - 1)* Convert.ToDouble(Seriestime.Count)) / 75.0;
            double E_c = (Convert.ToDouble(Seriestime.Count)*Convert.ToDouble(Seriestime.Count - 1)) / 4.0;
            double C_static = (T_ij+0.5 - E_c) / Math.Sqrt(D_c);
            //C_static = Math.Abs(C_static);
            return C_static;
        }

        private double Mediana_ryadu()
        {
            List<double> result = new List<double>();
            double Mediana = 0;
            for (int i = 0; i < Seriestime.Count; i++)
            {
                result.Add(Seriestime[i]);
            }
            result.Sort();
            if (result.Count%2==0)
            {
                Mediana = (result[result.Count / 2]+ result[(result.Count/ 2)+1])/2.0;
            }
            else
            {
                Mediana = result[(result.Count + 1) / 2];

            }
            return Mediana;
        }

        private bool Kriteriy_Seriy()
        {
            double result = Mediana_ryadu();
            List<int> Y_ser=new List<int>();
            double T_ij = 0;
            for (int i = 0; i < Seriestime.Count; i++)
            {
                if (Seriestime[i] >= result)
                {
                    Y_ser.Add(1);
                }
                else
                {
                    Y_ser.Add(-1);
                }

            }
            var resul1 = FindSeries(Y_ser);


            double V_n = (Convert.ToDouble(Seriestime.Count) + 1.0 - 1.96 * Math.Sqrt(Convert.ToDouble(Seriestime.Count - 1))) / 2.0;
            double dc = 3.3 * Math.Log(Convert.ToDouble(Seriestime.Count + 1));
            if (resul1.Item2>Convert.ToInt32(Math.Truncate(V_n))&& resul1.Item1 < Convert.ToInt32(Math.Truncate(dc)))
            {
                return true;
            }
            else
            {
                return false;
            }
            
            
        }

        private bool Kriteriy_Zrostauchih_Seriy()
        {
           
            List<int> Y_ser = new List<int>();
            
            for (int i = 1; i < Seriestime.Count; i++)
            {
                if (Seriestime[i] - Seriestime[i-1] >= 0)
                {
                    Y_ser.Add(1);
                }
                else
                {
                    Y_ser.Add(-1);
                }

            }
            var resul1 = FindSeries(Y_ser);

            int d_0_N = 0;
            if (Seriestime.Count<=26)
            {
                d_0_N = 5;
            }
            else if (Seriestime.Count > 26 && Seriestime.Count <= 153)
            {
                d_0_N = 6;
            }
            else
            {
                d_0_N = 7;
            }

            double V_n = ((Convert.ToDouble(2*Seriestime.Count-1)/3.0) - 1.96 * Math.Sqrt(Convert.ToDouble(16*Seriestime.Count - 29)/90.0));
            double dc = 3.3 * Math.Log(Convert.ToDouble(Seriestime.Count + 1));
            if (resul1.Item2 > Convert.ToInt32(Math.Truncate(V_n)) && resul1.Item1 > d_0_N)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        private double Kriteriy_Abbe()
        {
            double mean = Program.vect_seredne(Seriestime);
            double q_kw = 0;
            double s_kw = 0;
            for (int i = 0; i < Seriestime.Count-1; i++)
            {
                q_kw += Math.Pow(Seriestime[i] - Seriestime[i + 1],2.0);
            }
            for (int i = 0; i < Seriestime.Count; i++)
            {
                q_kw += Math.Pow(Seriestime[i] - mean, 2.0);
            }
            q_kw =q_kw/Convert.ToDouble(Seriestime.Count);
            s_kw = s_kw / Convert.ToDouble(Seriestime.Count-1);
            double y_stat = q_kw / (2.0 * s_kw);
            double count_list = Convert.ToDouble(Seriestime.Count);
            double U_static = (y_stat-1.0) / Math.Sqrt((Math.Pow(count_list,2)-1.0)/(count_list-2.0));
            U_static = Math.Abs(U_static);
            return U_static;
        }

        private List<double> Median_zlahodguvanya()
        {
            List<double> result = new List<double>();
            double first_point = (4.0*Seriestime[0] + Seriestime[1] - 2.0*Seriestime[2]) / 3.0;
            result.Add(first_point);
            first_point = (4.0 * Seriestime[Seriestime.Count-1] + Seriestime[Seriestime.Count - 2] - 2.0 * Seriestime[Seriestime.Count - 3]) / 3.0;
           
           

            for (int i = 1; i < Seriestime.Count - 1; i++)
            {
                double x_i_mean = Seriestime[i - 1]+Seriestime[i]+ Seriestime[i+1];
                x_i_mean = x_i_mean/3.0;
                result.Add(x_i_mean);
                
            }
            result.Add(first_point);

            return result;
        }

        private List<double> SMA(int interva)
        {
            double interval_kovznayna = Convert.ToDouble(interva);
            List<double> result = new List<double>();
            
            for (int i = interva-1; i < Seriestime.Count; i++)
            {

                double x_i_mean = 0;
                for (int t = 0; t < interva-1; t++)
                {
                    x_i_mean += Seriestime[i - t];
                }
                x_i_mean = x_i_mean / interval_kovznayna;
                result.Add(x_i_mean);

            }
            return result;
        }

        private List<double> WMA(int interva)
        {
            double interval_kovznayna = Convert.ToDouble(interva);
            List<double> result = new List<double>();

            for (int i = interva - 1; i < Seriestime.Count; i++)
            {

                double x_i_mean = 0;
                for (int t = 0; t < interva - 1; t++)
                {
                    x_i_mean += Seriestime[i - t]*Convert.ToDouble(interva-t);
                }
                x_i_mean = (2.0*x_i_mean) / Convert.ToDouble(interva*(interva+1));
                result.Add(x_i_mean);

            }
            return result;
        }

        private List<double> EMA(int interva)
        {
            double interval_kovznayna = Convert.ToDouble(interva);
            List<double> result = new List<double>();
            double alpha_ema = 1.0 / (interval_kovznayna + 1.0);
            int index_of_list = 0;
            for (int i = interva - 1; i < Seriestime.Count; i++)
            {
                double x_i_mean = 0;
                if (i==(interva-1))
                {
                    for (int t = 0; t < interva - 1; t++)
                    {
                        x_i_mean += Seriestime[i - t];
                    }
                    x_i_mean = x_i_mean / interval_kovznayna;
                    result.Add(x_i_mean);
                    index_of_list++;
                }
                else
                {
                    for (int t = 0; t < interva - 1; t++)
                    {
                        x_i_mean += Seriestime[i - t]* Math.Pow(1.0-alpha_ema,Convert.ToDouble(t));
                    }
                    x_i_mean = alpha_ema * x_i_mean;
                    x_i_mean = x_i_mean+Math.Pow(1.0 - alpha_ema, interval_kovznayna) * result[index_of_list-1];
                    result.Add(x_i_mean);
                    index_of_list++;
                }
                
            }
            return result;
        }

        private List<double> DMA(int interva)
        {
            List<double> EMaa = EMA(interva);
            double interval_kovznayna = Convert.ToDouble(interva);
            List<double> result = new List<double>();
            double alpha_ema = 1.0 / (interval_kovznayna + 1.0);
            int index_of_list = 0;
            for (int i = 0; i < EMaa.Count; i++)
            {
                double x_i_mean = 0;
                if (i == 0)
                {
                    index_of_list++;
                    x_i_mean = EMaa[i];
                    result.Add(x_i_mean);
                }
                else
                {
                    x_i_mean = alpha_ema * EMaa[i];
                    x_i_mean = x_i_mean +   (1.0 - alpha_ema) * result[index_of_list  - 1];
                    result.Add(x_i_mean);
                    index_of_list++;
                }

            }
            return result;
        }

        private List<double> TMA(int interva)
        {
            List<double> EMaa = DMA(interva);
            double interval_kovznayna = Convert.ToDouble(interva);
            List<double> result = new List<double>();
            double alpha_ema = 1.0 / (interval_kovznayna + 1.0);
            int index_of_list = 0;
            for (int i = 0; i < EMaa.Count; i++)
            {
                double x_i_mean = 0;
                if (i == 0)
                {
                    x_i_mean = EMaa[i];
                    result.Add(x_i_mean);
                    index_of_list++;
                }
                else
                {
                    x_i_mean = alpha_ema * EMaa[i];
                    x_i_mean = x_i_mean + (1.0 - alpha_ema) * result[index_of_list - 1];
                    result.Add(x_i_mean);
                    index_of_list++;
                }
            }
            return result;
        }

        private List<double> get_param_A_for_Deleting_Trend(List<double> the_x, int stepin_polinoma)
        {
            //ковзне середнє
            
            List<double> spisok_T = new List<double>();
            for (int i = 0; i < Time.Count; i++)
            {
                spisok_T.Add(Time[i]);
            }
            double[,] matrix_par = BuildMatrixT(spisok_T, 1, stepin_polinoma);
            matrix_par = Program.multiplication_matrix_on_number(matrix_par, 1.0 / Convert.ToDouble(spisok_T.Count));
            List<double> y_col = Y_column_param_deleting_trend(spisok_T, the_x, stepin_polinoma);
            List<double> result = Program.multiplication_matrix_on_colums(Program.Inverse_matrix(matrix_par), y_col);
            return result;

        }
        private List<double> Deleting_trend(int interva)
        {
            MenuItem10_Click(null, EventArgs.Empty);
            List<double> param_A = get_param_A_for_Deleting_Trend(Seriestime, interva);
            List<double> result = new List<double>();
            for (int i = 0; i < Seriestime.Count; i++)
            {

                double novoutvoreniy_x = 0;
                for (int j = 0; j < param_A.Count; j++)
                {
                    novoutvoreniy_x += param_A[j] * Math.Pow(Time[i], j);
                }
                result.Add(Seriestime[i]-novoutvoreniy_x);
                
            }
            return result;
        }

        private double[,] Decomposition(int lenght_of_gusenni)
        {
            double[,] matrix_XX = new double[lenght_of_gusenni, Time.Count - lenght_of_gusenni + 1 ];
            for (int i = 0; i < lenght_of_gusenni; i++)
            {
                for (int t = 0; t < Seriestime.Count - lenght_of_gusenni + 1; t++)
                {
                    matrix_XX[i, t] = Seriestime[i + t];
                }
            }
            double[,] S_matrix = Program.multiplication_matrix_on_matrix(matrix_XX, Program.Transponovana_matrix(matrix_XX));
            double[,] vlasni_vectory = Eigen_vectors(S_matrix);
            vlasniss_chisla = Eigen_value(S_matrix);
            Update_table_2(vlasni_vectory);
            double[,] Y_col = Program.multiplication_matrix_on_matrix(Program.Transponovana_matrix(vlasni_vectory), matrix_XX);
            
            return Y_col;
        }

        private List<double> Reconstruction(int lenght_of_gusenni, List<int> component)
        {
            double[,] matrix_XX = new double[lenght_of_gusenni, Time.Count - lenght_of_gusenni + 1];
            for (int i = 0; i < lenght_of_gusenni; i++)
            {
                for (int t = 0; t < Seriestime.Count - lenght_of_gusenni + 1; t++)
                {
                    matrix_XX[i, t] = Seriestime[i + t];
                }
            }
            double[,] S_matrix = Program.multiplication_matrix_on_matrix(matrix_XX, Program.Transponovana_matrix(matrix_XX));
            double[,] vlasni_vectory = Eigen_vectors(S_matrix);
            double[,] Y_col = Program.multiplication_matrix_on_matrix(Program.Transponovana_matrix(vlasni_vectory), matrix_XX);

            double[,] matrix_with_componentam=matrix_with_componentami(vlasni_vectory,component);
            double[,] matrix_ended = Program.multiplication_matrix_on_matrix(matrix_with_componentam, Y_col);
            double[,] average_matrix=Averege_matrix(matrix_ended);
            List<double> sum=ryad_from_matrix(average_matrix);
            return sum;
        }

        private double[,] matrix_with_componentami(double[,] matrix, List<int> component)
        {
            double[,] matrix_XX = new double[matrix.GetLength(0), matrix.GetLength(1)];
            int index_kom = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (i == component[index_kom])
                {
                    index_kom++;
                    for (int t = 0; t < matrix.GetLength(0); t++)
                    {
                        matrix_XX[t, i] = matrix[t, i];
                    }
                    if (index_kom==component.Count)
                    {
                        index_kom = 0;
                    }
                }
                else
                {
                    for (int t = 0; t < matrix.GetLength(0); t++)
                    {
                        matrix_XX[t, i] =0.0;
                    }

                }
                
                
            }

            return matrix_XX;
        }


        private List<double> ryad_from_matrix(double[,] matrix)
        {
            List<double> result=new List<double>();
            
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                result.Add(matrix[0, i]);
            }
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                result.Add(matrix[i, matrix.GetLength(1)-1]);
            }
            return result;
        }
        private Tuple<int, int> FindSeries(List<int> array)
        {
            if (array == null || array.Count == 0)
                return Tuple.Create(0, 0);

            int maxSeriesLength = 1;
            int currentSeriesLength = 1;
            int seriesCount = 1;

            for (int i = 1; i < array.Count; i++)
            {
                if (array[i] == array[i - 1])
                {
                    currentSeriesLength++;
                    maxSeriesLength = Math.Max(maxSeriesLength, currentSeriesLength);
                }
                else
                {
                    currentSeriesLength = 1;
                    seriesCount++;
                }
            }

            return Tuple.Create(maxSeriesLength, seriesCount);
        }
    }
}
