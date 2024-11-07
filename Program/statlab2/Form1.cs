using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using statlab2;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Runtime.CompilerServices;
using TextBox = System.Windows.Forms.TextBox;
using Button = System.Windows.Forms.Button;
using ScottPlot.Drawing.Colormaps;
using System.Security.Cryptography;
using ScottPlot.Plottable;

namespace statlab2
{
    public partial class Form1 : Form
    {

        private OpenFileDialog openFileDialog1;
        private int the_select_index = 0;
        public static double kofAsimeNoMove;
        public static double matspod;
        public static bool Vflag;
        public static double kwadratvid;
        public static double kofekses2;
        public static int kilkclass;
        ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();
        ContextMenuStrip contextMenuStrip2 = new ContextMenuStrip();
        ContextMenuStrip contextMenuStri_for_list_box1 = new ContextMenuStrip();

        List<tochki_2d> tochki_2Ds=new List<tochki_2d>();
        List<table_of_chastot> Table_Of_Chastots=new List<table_of_chastot>();
        List<List<rows>> vibirku_var = new List<List<rows>>();
        List<List<double>> not_sortData = new List<List<double>>();
       // List<List<rang>> rangova_vibirka = new List<List<rang>>();
        List<normclass> norm_or_notnorm = new List<normclass>();

        ToolStripMenuItem LogFunc;
        ///ToolStripMenuItemExpoRoz
        ToolStripMenuItem StandFunc;
        ToolStripMenuItem Anomalfunc;
        ToolStripMenuItem dovirInterval;
        ToolStripMenuItem NOTdovirInterval;
        ToolStripMenuItem standartuzuvany_list_box1;
        ToolStripMenuItem exponenta_list_box1;

        ToolStripMenuItem logariphmuvanya_list_box1;
        ToolStripMenuItem centruvanya_list_box1;
        ToolStripMenuItem deleting_list_box1;
        ToolStripMenuItem liniyna_regresia;
        ToolStripMenuItem dovirchi_intevalu_every_regresii;
        ToolStripMenuItem tolerantrni_meshi;
        ToolStripMenuItem parabolichna_regresia;
        ToolStripMenuItem kvaziliniyna_regresia;
        ToolStripMenuItem dovirchi_intervaly_for_prognozu;
        ToolStripMenuItem method_Teyla;
        ToolStripMenuItem kvazi_bez_waga;
        ToolStripMenuItem Function_shilnosty;


        public Form1()
        {
            InitializeComponent();
            openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            standartuzuvany_list_box1 = new ToolStripMenuItem("Стандартизування векторів");
            standartuzuvany_list_box1.Click += Standartuzuvany_list_box1_Click;
            exponenta_list_box1 = new ToolStripMenuItem("Експоненування векторів");
            exponenta_list_box1.Click += Exponenta_list_box1_Click;
            centruvanya_list_box1 = new ToolStripMenuItem("Центрування векторів");
            centruvanya_list_box1.Click += Centruvanya_list_box1_Click;
            logariphmuvanya_list_box1 =new ToolStripMenuItem("Логарифмування векторів");
            logariphmuvanya_list_box1.Click += Logariphmuvanya_list_box1_Click;
            deleting_list_box1 = new ToolStripMenuItem("Вилучення векторів");
            deleting_list_box1.Click += Deleting_list_box1_Click;



            contextMenuStri_for_list_box1.Items.AddRange(new[] { standartuzuvany_list_box1, exponenta_list_box1, logariphmuvanya_list_box1, centruvanya_list_box1, deleting_list_box1 });
            listBox1.ContextMenuStrip = contextMenuStri_for_list_box1;
            LogFunc = new ToolStripMenuItem("Логарифмування");
            StandFunc = new ToolStripMenuItem("Стандартизація");
            Function_shilnosty = new ToolStripMenuItem("Функція щільності");
            Anomalfunc = new ToolStripMenuItem("Вилучення аномальних значень");
            dovirInterval = new ToolStripMenuItem("Довірчі інтервали");
            NOTdovirInterval = new ToolStripMenuItem("Вилучити довірчі інтервали");
            contextMenuStrip1.Items.AddRange(new[] { LogFunc, StandFunc, Anomalfunc, dovirInterval, NOTdovirInterval });
            chart1.ContextMenuStrip = contextMenuStrip1;
            LogFunc.Click += LogFunc_Click;
            Anomalfunc.Click += Anomalfunc_Click;
            StandFunc.Click += StandFunc_Click;
            // ExpoRozpodil.Click += ExpoRozpodil_Click;
            button1.Click += Calculater;
            dovirInterval.Click += DovirInterval_Click;
            dovirInterval.DoubleClick += DovirInterval_DoubleClick;
            NOTdovirInterval.Click += NOTdovirInterval_Click;
            ////////двовимірний аналіз///////
            liniyna_regresia = new ToolStripMenuItem("Лінійна регресія");
            dovirchi_intevalu_every_regresii = new ToolStripMenuItem("Довірчі інтервали");
            tolerantrni_meshi = new ToolStripMenuItem("Толерантні межі");
            dovirchi_intervaly_for_prognozu = new ToolStripMenuItem("Довірчі інтервали для прогнозу спостереження");
            parabolichna_regresia = new ToolStripMenuItem("Параболічна регресія");
            kvaziliniyna_regresia = new ToolStripMenuItem("Квазілінійна регресія");
            kvazi_bez_waga = new ToolStripMenuItem("Квазілінійна без вагових функцій");
            method_Teyla = new ToolStripMenuItem("Метод Тейла");

            contextMenuStrip2.Items.AddRange(new[] { liniyna_regresia, method_Teyla, dovirchi_intevalu_every_regresii, tolerantrni_meshi, dovirchi_intervaly_for_prognozu,parabolichna_regresia, kvaziliniyna_regresia, kvazi_bez_waga,Function_shilnosty });
            chart3.ContextMenuStrip = contextMenuStrip2;
            liniyna_regresia.CheckedChanged += Liniyna_regresia_CheckedChanged;
            Function_shilnosty.Click += Function_shilnosty_Click;
            Function_shilnosty.CheckedChanged += Function_shilnosty_CheckedChanged;
            liniyna_regresia.Click += Liniyna_regresia_Click;
            method_Teyla.Click += Method_Teyla_Click;
            method_Teyla.CheckedChanged += Method_Teyla_CheckedChanged;
            tolerantrni_meshi.CheckedChanged += Tolerantrni_meshi_CheckedChanged;
            tolerantrni_meshi.Click += Tolerantrni_meshi_Click;
            dovirchi_intervaly_for_prognozu.Click += Dovirchi_intervaly_for_prognozu_Click;
            dovirchi_intervaly_for_prognozu.CheckedChanged += Dovirchi_intervaly_for_prognozu_CheckedChanged;
            dovirchi_intevalu_every_regresii.Click += Dovirchi_intevalu_every_regresii_Click;
            dovirchi_intevalu_every_regresii.CheckedChanged += Dovirchi_intevalu_every_regresii_CheckedChanged;
            parabolichna_regresia.Click += Parabolichna_regresia_Click;
            parabolichna_regresia.CheckedChanged += Parabolichna_regresia_CheckedChanged;
            //ntextMenuStrip2.MouseDown += ContextMenuStrip2_MouseDown;
            kvaziliniyna_regresia.Click += Kvaziliniyna_regresia_Click;
            kvaziliniyna_regresia.CheckedChanged += Kvaziliniyna_regresia_CheckedChanged;
            chart3.ChartAreas[0].AxisX.LabelStyle.Format = "0.000";
            kvazi_bez_waga.Click += Kvazi_bez_waga_Click;
            kvazi_bez_waga.CheckedChanged += Kvazi_bez_waga_CheckedChanged;
            
        }

        private void Exponenta_list_box1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    if (listBox1.Items.Contains($"{listBox1.Items[i].ToString()} Center"))
                    {
                        continue;
                    }
                    else
                    {
                        List<double> New_the_data = Program.Exponuvanya(Universe.Data_Vectors[i]);
                        Universe.Data_Vectors.Add(New_the_data);
                        listBox1.Items.Add($"{listBox1.Items[i].ToString()} Exp");
                    }


                }
            }
        }

        private void Centruvanya_list_box1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    if (listBox1.Items.Contains($"{listBox1.Items[i].ToString()} Center"))
                    {
                        continue;
                    }
                    else
                    {
                        List<double> New_the_data = Program.Centering(Universe.Data_Vectors[i]);
                        Universe.Data_Vectors.Add(New_the_data);
                        listBox1.Items.Add($"{listBox1.Items[i].ToString()} Center");
                    }


                }
            }
        }

        private void Deleting_list_box1_Click(object sender, EventArgs e)
        {
            
            repit:
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    listBox1.Items.RemoveAt(i);
                    Universe.Data_Vectors.RemoveAt(i);
                    goto repit;

                }
                
                
            }
        }

        private void Function_shilnosty_CheckedChanged(object sender, EventArgs e)
        {
            if (!Function_shilnosty.Checked)
            {

                //if (chart3.Series.IsUniqueName("Функція щільності"))
                //{
                //    chart3.Series.RemoveAt(8);
                //}
                if (8< chart3.Series.Count)
                {
                    chart3.Series.RemoveAt(8);
                }
            }
            else
            {
                List<List<double>> vector_oznak = new List<List<double>>();
                List<double> x_list_ = new List<double>();
                List<double> y_list_ = new List<double>();
                List<tochki_2d> d2_coord = new List<tochki_2d>();
                for (int i = 0; i < tochki_2Ds.Count; i++)
                {
                    tochki_2d dr = new tochki_2d();
                    dr.x = tochki_2Ds[i].x;
                    dr.y = tochki_2Ds[i].y;
                    x_list_.Add(tochki_2Ds[i].x);
                    y_list_.Add(tochki_2Ds[i].y);
                    d2_coord.Add(dr);
                }
                vector_oznak.Add(x_list_);
                vector_oznak.Add(y_list_);
                List<double> kroki_n_hist = Program.krok_var_ryadu(vector_oznak, 15);
                int optima_krok = vector_oznak[0].Count;
                if (optima_krok>100)
                {
                    optima_krok = 110;
                }
                var result = Program.varianta_each_vectors(vector_oznak,optima_krok).CartesianProduct();
                List<double> vidnosna_chast = new List<double>();
                List<List<double>> varianty_each_cube = new List<List<double>>();
                foreach (var item in result)
                {
                    varianty_each_cube.Add(item.ToList());
                }
                List<N_dimension> varianty_dimension = new List<N_dimension>();
                for (int i = 0; i < varianty_each_cube.Count; i++)
                {
                    N_dimension before_cycle = new N_dimension();
                    for (int it = 0; it < varianty_each_cube[i].Count; it++)
                    {
                        before_cycle.spisok_coordinate.Add(varianty_each_cube[i][it]);
                    }
                    int chastotu_vhodgenya = 0;
                    for (int t = 0; t < vector_oznak[0].Count; t++)
                    {
                        List<bool> coor_in_cube = new List<bool>();
                        for (int t1 = 0; t1 < varianty_each_cube[i].Count; t1++)
                        {
                            if ((vector_oznak[t1][t] <= (varianty_each_cube[i][t1] + (kroki_n_hist[t1] / 2.0))) && (vector_oznak[t1][t] >= (varianty_each_cube[i][t1] - (kroki_n_hist[t1] / 2.0))))
                            {
                                coor_in_cube.Add(true);
                            }
                            else
                            {
                                coor_in_cube.Add(false);
                            }

                        }
                        if (!coor_in_cube.Contains(false))
                        {
                            chastotu_vhodgenya++;
                        }

                    }
                    before_cycle.chastota_p = chastotu_vhodgenya;
                    before_cycle.vidnosna_chastota = (Convert.ToDouble(chastotu_vhodgenya) / Convert.ToDouble(vector_oznak[0].Count));
                    vidnosna_chast.Add(before_cycle.vidnosna_chastota);
                    varianty_dimension.Add(before_cycle);
                }

                Series series_point = new Series("Функція щільності");
                series_point.ChartType = SeriesChartType.Point;
                series_point.IsVisibleInLegend = false;
                //for (int i = 0; i < varianty_dimension.Count; i++)
                //{
                //    double chasr_in_cycle = (varianty_dimension[i].vidnosna_chastota - vidnosna_chast.Min()) / (vidnosna_chast.Max() + vidnosna_chast.Min());
                //    //int red = Convert.ToByte((255.0*(1.0-()));
                //    //int green = Convert.ToByte(255.0);
                //    //int blue = Convert.ToByte((255.0 * (1.0 - ((varianty_dimension[i].vidnosna_chastota - vidnosna_chast.Min()) / (vidnosna_chast.Max() + vidnosna_chast.Min())))));
                //    //Color ccccc = Color.FromArgb(255, red, green, blue);
                //    //if (red>230)
                //    //{
                //    //    continue;
                //    //}
                //    series_point.Points.AddXY(varianty_dimension[i].spisok_coordinate[0], varianty_dimension[i].spisok_coordinate[1]);
                //    ///int green =Convert.ToInt32(255.0*((varianty_dimension[i].vidnosna_chastota - vidnosna_chast.Min()) / (vidnosna_chast.Max() + vidnosna_chast.Min())));           
                //    series_point.Points[i].Color = Color.FromArgb((int)(255 * chasr_in_cycle), Color.Green);

                //}
                varianty_dimension.Sort(delegate (N_dimension x, N_dimension y)
                {
                    return x.chastota_p.CompareTo(y.chastota_p);

                });
                d2_coord.Sort(delegate (tochki_2d x, tochki_2d y)//from small to hight
                {
                    return x.y.CompareTo(y.y);

                });
                double kut_cooeficient = (Program.ocinka_parnoho_koef_korelacii(vector_oznak[0], vector_oznak[1]) * Program.vect_seredno_kvadratichnyh(vector_oznak[1])) / Program.vect_seredno_kvadratichnyh(vector_oznak[0]);
                double a_coord = (Program.vect_seredne(vector_oznak[1])) - (kut_cooeficient * Program.vect_seredne(vector_oznak[0]));
                //double b_hight = d2_coord[d2_coord.Count - 1].y - (d2_coord[d2_coord.Count - 1].x * kut_cooeficient);
                //double b_low = d2_coord[0].y - (d2_coord[0].x * kut_cooeficient);
                int counter_in_data_point = 0;
                ///пошук b_hight and b_low
                List<tochki_2d> b_cooef = new List<tochki_2d>();
                for (int i = 0; i < vector_oznak[0].Count; i++)
                {
                    tochki_2d wd_newest = new tochki_2d();
                    wd_newest.x = vector_oznak[0][i];
                    wd_newest.y = vector_oznak[1][i];
                    double y_serd=a_coord+kut_cooeficient * vector_oznak[0][i];
                    wd_newest.rang_y = y_serd - vector_oznak[1][i];
                    b_cooef.Add(wd_newest);

                }
                b_cooef.Sort(delegate (tochki_2d x, tochki_2d y)
                {
                    return x.rang_y.CompareTo(y.rang_y);

                });
                double b_hight = b_cooef[0].y - (b_cooef[0].x * kut_cooeficient);
                double b_low = b_cooef[b_cooef.Count-1].y - (b_cooef[b_cooef.Count-1].x * kut_cooeficient);
                
                //for (int i = 0; i < varianty_dimension.Count; i++)
                //{
                //    double y_top = varianty_dimension[i].spisok_coordinate[0] * kut_cooeficient + b_hight;
                //    double y_bottom= varianty_dimension[i].spisok_coordinate[0] * kut_cooeficient + b_low;


                //    if (varianty_dimension[i].spisok_coordinate[1]<=y_top && varianty_dimension[i].spisok_coordinate[1]>=y_bottom)
                //    {
                //        double chasr_in_cycle = (varianty_dimension[i].vidnosna_chastota - vidnosna_chast.Min()) / (vidnosna_chast.Max() + vidnosna_chast.Min());

                //        int red = Convert.ToByte(255.0*(1.0-chasr_in_cycle));
                //        int green = Convert.ToByte(255.0);
                //        int blue = Convert.ToByte(255.0 * (1.0 - chasr_in_cycle));
                //        //Color ccccc = Color.FromArgb(255, red, green, blue);
                //        if (red>=250)
                //        {
                //            red = red-10;
                //            blue = blue-10;
                //        }
                //        series_point.Points.AddXY(varianty_dimension[i].spisok_coordinate[0], varianty_dimension[i].spisok_coordinate[1]);
                //        ///int green =Convert.ToInt32(255.0*((varianty_dimension[i].vidnosna_chastota - vidnosna_chast.Min()) / (vidnosna_chast.Max() + vidnosna_chast.Min())));           
                //        series_point.Points[counter_in_data_point].Color = Color.FromArgb(red,green,blue);
                //        counter_in_data_point++;
                //    }


                //}
                for (int i = 0; i < varianty_dimension.Count; i++)
                {
                    double y_top = varianty_dimension[i].spisok_coordinate[0] * kut_cooeficient + b_hight;
                    double y_bottom = varianty_dimension[i].spisok_coordinate[0] * kut_cooeficient + b_low;


                    if ((varianty_dimension[i].spisok_coordinate[1] <= y_top && varianty_dimension[i].spisok_coordinate[1] >= y_bottom)&&(varianty_dimension[i].spisok_coordinate[0] <= x_list_.Max() && varianty_dimension[i].spisok_coordinate[0] >= x_list_.Min()))
                    {
                        double chasr_in_cycle = (varianty_dimension[i].vidnosna_chastota - vidnosna_chast.Min()) / (vidnosna_chast.Max() + vidnosna_chast.Min());

                        int red = Convert.ToByte(255.0 * (1.0 - chasr_in_cycle));
                        int green = Convert.ToByte(255.0);
                        int blue = Convert.ToByte(255.0 * (1.0 - chasr_in_cycle));
                        //Color ccccc = Color.FromArgb(255, red, green, blue);
                        if (red >= 250)
                        {
                            red = red - 10;
                            blue = blue - 10;
                            series_point.Points.AddXY(varianty_dimension[i].spisok_coordinate[0], varianty_dimension[i].spisok_coordinate[1]);
                            series_point.Points[counter_in_data_point].Color = Color.FromArgb(red, green, blue);
                        }
                        else
                        {
                            series_point.Points.AddXY(varianty_dimension[i].spisok_coordinate[0], varianty_dimension[i].spisok_coordinate[1]);
                            series_point.Points[counter_in_data_point].Color = Color.FromArgb((int)(255 * chasr_in_cycle), Color.Green);
                        }
                        counter_in_data_point++;
                    }


                }
                chart3.Series.Add(series_point);
            }
        }

        private void Function_shilnosty_Click(object sender, EventArgs e)
        {
            if (!Function_shilnosty.Checked)
            {
                liniyna_regresia.Checked = false;
                dovirchi_intevalu_every_regresii.Checked = false;
                tolerantrni_meshi.Checked = false;
                parabolichna_regresia.Checked = false;
                kvaziliniyna_regresia.Checked = false;
                dovirchi_intervaly_for_prognozu.Checked = false;

                kvazi_bez_waga.Checked = false;
                method_Teyla.Checked = false;
                Function_shilnosty.Checked = true;

            }
            else
            {
                Function_shilnosty.Checked = false;
            }
        }

        private void Logariphmuvanya_list_box1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    if (listBox1.Items.Contains($"{listBox1.Items[i].ToString()} Log"))
                    {

                        continue;
                    }
                    else
                    {
                        List<double> New_the_data = Program.Logariphmuvanya(Universe.Data_Vectors[i]);
                        Universe.Data_Vectors.Add(New_the_data);
                        listBox1.Items.Add($"{listBox1.Items[i].ToString()} Log");
                    }


                }
            }
        }

        private void Standartuzuvany_list_box1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i)==true)
                {
                    if (listBox1.Items.Contains($"{listBox1.Items[i].ToString()} Standard"))
                    {                     
                        continue;
                    }
                    else
                    {
                        List<double> New_the_data = Program.Standartuzazia(Universe.Data_Vectors[i]);
                        Universe.Data_Vectors.Add(New_the_data);
                        listBox1.Items.Add($"{listBox1.Items[i].ToString()} Standard");
                    }
                    
                    
                }
            }
        }

        private void Kvazi_bez_waga_CheckedChanged(object sender, EventArgs e)
        {
            if (!kvazi_bez_waga.Checked)
            {
                chart3.Series[1].Points.Clear();
                //chart3.Series[5].Points.Clear();

            }
            else
            {
                chart3.Series[1].Points.Clear();
                List<tochki_2d> coor_2d = new List<tochki_2d>();
                List<tochki_2d> coor_2d_zminene = new List<tochki_2d>();
                for (int i = 0; i < tochki_2Ds.Count; i++)
                {
                    coor_2d.Add(tochki_2Ds[i]);
                }
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double novekiy_x = 1.0 / (coor_2d[i].x);
                    double novekiy_y = 1.0 / (coor_2d[i].y);

                    tochki_2d tochki_2 = new tochki_2d();
                    tochki_2.x = novekiy_x;
                    tochki_2.y = novekiy_y;

                    coor_2d_zminene.Add(tochki_2);
                }
                double z_seredne = 0;
                double t_seredne = 0;
                double t_dispersia = 0;
                double z_dispersia = 0;
                double zt_seredne = 0;
                for (int i = 0; i < coor_2d_zminene.Count; i++)
                {
                    z_seredne += coor_2d_zminene[i].y;
                    t_seredne += coor_2d_zminene[i].x;
                    zt_seredne += coor_2d_zminene[i].y * coor_2d_zminene[i].x;
                }
                zt_seredne/= Convert.ToDouble(coor_2d_zminene.Count);
                z_seredne /= Convert.ToDouble(coor_2d_zminene.Count);
                t_seredne /= Convert.ToDouble(coor_2d_zminene.Count);
                
                for (int i = 0; i < coor_2d_zminene.Count; i++)
                {
                    t_dispersia += Math.Pow(coor_2d_zminene[i].x - t_seredne, 2.0);
                    z_dispersia += Math.Pow(coor_2d_zminene[i].y - z_seredne, 2.0);
                }
                t_dispersia /= Convert.ToDouble(coor_2d_zminene.Count - 1);
                z_dispersia /= Convert.ToDouble(coor_2d_zminene.Count - 1);

                double koef_par_kor = (zt_seredne - (z_seredne * t_seredne)) / (Math.Sqrt(t_dispersia)*Math.Sqrt(z_dispersia));///оцінка парного коефіцієнта кореляції
                koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d_zminene.Count) / Convert.ToDouble(coor_2d_zminene.Count - 1));
                double ocinka_B = koef_par_kor * (Math.Sqrt(z_dispersia) / Math.Sqrt(t_dispersia));

                double ocinka_A = z_seredne - (ocinka_B * t_seredne);

                double N_for_N = Convert.ToDouble(coor_2d_zminene.Count);



                double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                double paramtr_a = ocinka_A / ocinka_B;
                double parametr_b = 1.0 / ocinka_B;

                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double d1_ce = (1.0 / coor_2d[i].x) + paramtr_a;
                    double y_f9x0 = parametr_b / d1_ce;

                    chart3.Series[1].Points.AddXY(coor_2d[i].x, y_f9x0);

                }

                double dispersia_zalishkova_z_ocinkamy = 0;
                for (int i = 0; i < coor_2d_zminene.Count; i++)
                {
                    double videmnyk = coor_2d_zminene[i].x * ocinka_B + ocinka_A;
                    dispersia_zalishkova_z_ocinkamy += Math.Pow(coor_2d_zminene[i].y - videmnyk, 2.0);
                }
                dispersia_zalishkova_z_ocinkamy /= (N_for_N - 2.0);
                double dispersia_zalishkova_z_parametramy = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double d1_ce = (1.0 / coor_2d[i].x) + paramtr_a;
                    double y_f9x0 = parametr_b / d1_ce;
                    dispersia_zalishkova_z_parametramy += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);

                }
                dispersia_zalishkova_z_parametramy /= (N_for_N - 2.0);
                
                dataGridView10.Rows.Clear();
                dataGridView10.Columns.Clear();
                dataGridView10.AllowUserToAddRows = false;
                dataGridView10.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView10.Columns.Add("1", "Характеристика");
                dataGridView10.Columns.Add("2", "INF");
                dataGridView10.Columns.Add("3", "Значення");
                dataGridView10.Columns.Add("4", "SUP");
                //double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                //double N_for_N = Convert.ToDouble(coor_2d.Count);
                double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                dataGridView10.Rows.Add("Оцінка парметра A", "", Math.Round(ocinka_A, 4), "");
                dataGridView10.Rows.Add("Оцінка парметра B", "", Math.Round(ocinka_B, 4), "");

                double S_a = Math.Sqrt(dispersia_zalishkova_z_parametramy) * Math.Sqrt((1.0 / N_for_N) + (Math.Pow(t_seredne, 2.0) / (t_dispersia * (N_for_N - 1.0))));
                double S_b = Math.Sqrt(dispersia_zalishkova_z_parametramy) / (Math.Sqrt(t_dispersia) * Math.Sqrt(N_for_N - 1.0));
                dataGridView10.Rows.Add("Інтервальне оцінювання параметрів", Math.Round(ocinka_A - (kvantil_studen * S_a),4), "A",  (Math.Round(ocinka_A + (kvantil_studen * S_a), 4)));
                dataGridView10.Rows.Add("Інтервальне оцінювання параметрів",  (Math.Round(ocinka_B - (kvantil_studen * S_b), 4)), "B", (Math.Round(ocinka_B + (kvantil_studen * S_b), 4)));
                dataGridView10.Rows.Add("Залишкова дисперсія S", "", Math.Round(dispersia_zalishkova_z_parametramy, 4), "");
                dataGridView10.Rows.Add("σ оцінка \n параметра A", "", Math.Round(S_a, 4), "");
                dataGridView10.Rows.Add("σ оцінка \n параметра B", "", Math.Round(S_b, 4), "");
                dataGridView10.Rows.Add("Оцінка парметра â", "", Math.Round(paramtr_a, 4), "");
                dataGridView10.Rows.Add("Оцінка парметра b", "", Math.Round(parametr_b, 4), "");
                dataGridView10.Rows.Add("Інтервальне оцінювання параметрів",Math.Round(1.0/(ocinka_A - (kvantil_studen * S_a)),4), "a", Math.Round(1.0 / (ocinka_A + (kvantil_studen * S_a)), 4));
                dataGridView10.Rows.Add("Інтервальне оцінювання параметрів",Math.Round(1.0/(ocinka_B - (kvantil_studen * S_b)),4), "b", Math.Round(1.0 / (ocinka_B + (kvantil_studen * S_b)), 4));
                richTextBox3.Text = "Квазілінійна регресія\n";
                richTextBox3.Text += $"y={Math.Round(parametr_b,4)}/((1/x)+({Math.Round(paramtr_a,4)})\n";
                double koef_determinaci = (1.0 - (dispersia_zalishkova_z_parametramy) / z_dispersia);
                dataGridView10.Rows.Add("Коефіцієнт детермінації R", "", $"{Math.Round(koef_determinaci*100.0, 4)}%", "");
                double fishera_roazpodil = kvantil_Fishera(kvantil_start_rozpodil, N_for_N - 1.0, N_for_N - 3.0);
                double f_statistika = dispersia_zalishkova_z_parametramy / z_dispersia;
                if (f_statistika > fishera_roazpodil)
                {
                    richTextBox3.Text += $"f={Math.Round(f_statistika, 4)} f>{Math.Round(fishera_roazpodil, 4)} відтворена квазілійна модель регресії не є адекватна\n";
                }
                else
                {
                    richTextBox3.Text += $"f={Math.Round(f_statistika, 4)} f<={Math.Round(fishera_roazpodil, 4)} відтворена квазілійна модель регресії є адекватна\n";
                }
            }
        }

        private void Kvazi_bez_waga_Click(object sender, EventArgs e)
        {
            if (!kvazi_bez_waga.Checked)
            {
                liniyna_regresia.Checked = false;
                dovirchi_intevalu_every_regresii.Checked = false;
                tolerantrni_meshi.Checked = false;
                parabolichna_regresia.Checked = false;
                kvaziliniyna_regresia.Checked = false;
                dovirchi_intervaly_for_prognozu.Checked = false;
                method_Teyla.Checked = false;
                Function_shilnosty.Checked = false;
                kvazi_bez_waga.Checked = true;

            }
            else
            {
                kvazi_bez_waga.Checked = false;
            }
        }

        private void Kvaziliniyna_regresia_CheckedChanged(object sender, EventArgs e)
        {
            if (!kvaziliniyna_regresia.Checked)
            {
                chart3.Series[1].Points.Clear();
                //chart3.Series[5].Points.Clear();

            }
            else
            {
                chart3.Series[1].Points.Clear();
                List<tochki_2d> coor_2d = new List<tochki_2d>();
                List<tochki_2d> coor_2d_zminene = new List<tochki_2d>();
                for (int i = 0; i < tochki_2Ds.Count; i++)
                {
                    coor_2d.Add(tochki_2Ds[i]);
                }
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double novekiy_x = 1.0 / (coor_2d[i].x);
                    double novekiy_y = 1.0 / (coor_2d[i].y);
                   
                    tochki_2d tochki_2 = new tochki_2d();
                    tochki_2.x = novekiy_x;
                    tochki_2.y = novekiy_y;

                    coor_2d_zminene.Add(tochki_2);
                }

                
                double sumaW_l = 0;
                double serednePSI_y = 0;
                double serednePHI_x = 0;
                double serednePHI_x_kvadrat = 0;
                double serednePHI_PSI = 0;
                for (int i = 0; i < coor_2d_zminene.Count; i++)
                {
                    sumaW_l += Math.Pow(coor_2d[i].y, 4.0) / Math.Pow(coor_2d[i].x,4.0);
                    serednePHI_x += coor_2d_zminene[i].x*(Math.Pow(coor_2d[i].y, 4.0) / Math.Pow(coor_2d[i].x, 4.0));
                    serednePSI_y+= coor_2d_zminene[i].y * (Math.Pow(coor_2d[i].y, 4.0) / Math.Pow(coor_2d[i].x, 4.0)); ;
                    serednePHI_PSI+= coor_2d_zminene[i].y*coor_2d_zminene[i].x * (Math.Pow(coor_2d[i].y, 4.0) / Math.Pow(coor_2d[i].x, 4.0));
                    serednePHI_x_kvadrat+= Math.Pow(coor_2d_zminene[i].x,2.0) * (Math.Pow(coor_2d[i].y, 4.0) / Math.Pow(coor_2d[i].x, 4.0)); ;
                }
                serednePHI_x /= sumaW_l;
                serednePHI_x_kvadrat /= sumaW_l;
                serednePSI_y /= sumaW_l;
                serednePHI_PSI /= sumaW_l;



                double N_for_N = Convert.ToDouble(coor_2d_zminene.Count);
                

                double ocinka_B = (serednePHI_PSI-(serednePHI_x*serednePSI_y))/(serednePHI_x_kvadrat-Math.Pow(serednePHI_x,2.0));
                double ocinka_A = serednePSI_y-ocinka_B*serednePHI_x;
                
                double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                double paramtr_a = ocinka_A / ocinka_B;
                double parametr_b = 1.0 / ocinka_B;

                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double d1_ce = (1.0 / coor_2d[i].x) + paramtr_a;
                    double y_f9x0 = parametr_b / d1_ce;
                    chart3.Series[1].Points.AddXY(coor_2d[i].x, y_f9x0);

                }

                double dispersia_zalishkova_z_ocinkamy = 0;
                for (int i = 0; i < coor_2d_zminene.Count; i++)
                {
                    double videmnyk=coor_2d_zminene[i].x*ocinka_B+ocinka_A;
                    dispersia_zalishkova_z_ocinkamy += Math.Pow(coor_2d_zminene[i].y - videmnyk, 2.0);
                }
                dispersia_zalishkova_z_ocinkamy /= (N_for_N - 2.0);
                double dispersia_zalishkova_z_parametramy = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double d1_ce = (1.0 / coor_2d[i].x) + paramtr_a;
                    double y_f9x0 = parametr_b / d1_ce;
                    dispersia_zalishkova_z_parametramy+=Math.Pow( coor_2d[i].y- y_f9x0,2.0);

                }
                dispersia_zalishkova_z_parametramy /= (N_for_N - 2.0);
                
                dataGridView10.Rows.Clear();
                dataGridView10.Columns.Clear();
                dataGridView10.AllowUserToAddRows = false;
                dataGridView10.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView10.Columns.Add("1", "Характеристика");
                dataGridView10.Columns.Add("2", "INF");
                dataGridView10.Columns.Add("3", "Значення");
                dataGridView10.Columns.Add("4", "SUP");
                //double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                //double N_for_N = Convert.ToDouble(coor_2d.Count);
                double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                dataGridView10.Rows.Add("Оцінка парметра A", "", Math.Round(ocinka_A, 4), "");
                dataGridView10.Rows.Add("Оцінка парметра B", "", Math.Round(ocinka_B, 4), "");

                //double S_a = Math.Sqrt(dispersia_zalishkova_z_parametramy) * Math.Sqrt((1.0 / N_for_N) + (Math.Pow(serednePHI_x, 2.0) / (t_dispersia * (N_for_N - 1.0))));
                //double S_b = Math.Sqrt(dispersia_zalishkova_z_parametramy) / (Math.Sqrt(t_dispersia) * Math.Sqrt(N_for_N - 1.0));
                //dataGridView10.Rows.Add("Інтервальне оцінювання параметрів", Math.Round(ocinka_A - (kvantil_studen * S_a), 4), "A", (Math.Round(ocinka_A + (kvantil_studen * S_a), 4)));
                //dataGridView10.Rows.Add("Інтервальне оцінювання параметрів", (Math.Round(ocinka_B - (kvantil_studen * S_b), 4)), "B", (Math.Round(ocinka_B + (kvantil_studen * S_b), 4)));
                dataGridView10.Rows.Add("Залишкова дисперсія S", "", Math.Round(dispersia_zalishkova_z_parametramy, 4), "");
                //dataGridView10.Rows.Add("σ оцінка \n параметра A", "", Math.Round(S_a, 4), "");
                //dataGridView10.Rows.Add("σ оцінка \n параметра B", "", Math.Round(S_b, 4), "");
                dataGridView10.Rows.Add("Оцінка парметра â", "", Math.Round(paramtr_a, 4), "");
                dataGridView10.Rows.Add("Оцінка парметра b", "", Math.Round(parametr_b, 4), "");
                //dataGridView10.Rows.Add("Інтервальне оцінювання параметрів", Math.Round(1.0 / (ocinka_A - (kvantil_studen * S_a)), 4), "a", Math.Round(1.0 / (ocinka_A + (kvantil_studen * S_a)), 4));
                //dataGridView10.Rows.Add("Інтервальне оцінювання параметрів", Math.Round(1.0 / (ocinka_B - (kvantil_studen * S_b)), 4), "b", Math.Round(1.0 / (ocinka_B + (kvantil_studen * S_b)), 4));
                richTextBox3.Text = "Квазілінійна регресія\n";
                richTextBox3.Text += $"y={Math.Round(parametr_b, 4)}/((1/x)+({Math.Round(paramtr_a, 4)}))";
            }
        }

        private void Kvaziliniyna_regresia_Click(object sender, EventArgs e)
        {
            if (!kvaziliniyna_regresia.Checked)
            {
                liniyna_regresia.Checked = false;
                dovirchi_intevalu_every_regresii.Checked = false;
                tolerantrni_meshi.Checked = false;
                parabolichna_regresia.Checked = false;
                Function_shilnosty.Checked = false;
                dovirchi_intervaly_for_prognozu.Checked = false;
                method_Teyla.Checked = false;
                kvazi_bez_waga.Checked = false;
                kvaziliniyna_regresia.Checked = true;

            }
            else
            {
                kvaziliniyna_regresia.Checked = false;
            }
        }

        private void Method_Teyla_CheckedChanged(object sender, EventArgs e)
        {
            if (!method_Teyla.Checked)
            {
                chart3.Series[1].Points.Clear();
                //chart3.Series[5].Points.Clear();

            }
            else
            {
                chart3.Series[1].Points.Clear();
                List<tochki_2d> coor_2d = new List<tochki_2d>();
                for (int i = 0; i < tochki_2Ds.Count; i++)
                {
                    coor_2d.Add(tochki_2Ds[i]);
                }
                
                double y_seredne = 0;
                double x_seredne = 0;
                double x_dispersia = 0;
                double y_dispersia = 0;
                double xy_seredne = 0;
                
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    
                    y_seredne += coor_2d[i].y;
                    x_seredne += coor_2d[i].x;
                    xy_seredne += (coor_2d[i].x * coor_2d[i].y);
                }

                
                y_seredne /= Convert.ToDouble(coor_2d.Count);
                x_seredne /= Convert.ToDouble(coor_2d.Count);
                xy_seredne /= Convert.ToDouble(coor_2d.Count);
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    x_dispersia += Math.Pow(coor_2d[i].x - x_seredne, 2.0);
                    y_dispersia += Math.Pow(coor_2d[i].y - y_seredne, 2.0);
                }
                x_dispersia /= Convert.ToDouble(coor_2d.Count - 1);
                y_dispersia /= Convert.ToDouble(coor_2d.Count - 1);


                double N_for_N = Convert.ToDouble(coor_2d.Count);
                double koef_par_kor = (xy_seredne - (x_seredne * y_seredne)) / (Math.Sqrt(x_dispersia * y_dispersia));///оцінка парного коефіцієнта кореляції
                koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d.Count) / Convert.ToDouble(coor_2d.Count - 1));

                double ocinka_B = 0;
                
                double ocinka_A = y_seredne;
                List<double> b_med = new List<double>();
                List<double> a_med = new List<double>();
                for (int j = 0; j < coor_2d.Count; j++)
                {
                    for (int i = 0; i < j-1; i++)
                    {
                        double u_list = (coor_2d[j].y - coor_2d[i].y) / (coor_2d[j].x - coor_2d[i].x);
                        b_med.Add(u_list);
                    }
                }
                b_med.Sort();
                if (b_med.Count % 2 == 0)
                {
                    ocinka_B = (b_med[(b_med.Count / 2) - 1] + b_med[(b_med.Count / 2)]) / 2.0;
                }
                else
                {
                    ocinka_B = b_med[(b_med.Count / 2)];
                }
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double u_a_list = coor_2d[i].y - (ocinka_B * coor_2d[i].x);
                    a_med.Add(u_a_list);
                }
                a_med.Sort();
                if (a_med.Count % 2 == 0)
                {
                    ocinka_A = (a_med[(a_med.Count / 2) - 1] + a_med[(a_med.Count / 2)]) / 2.0;
                }
                else
                {
                    ocinka_A = a_med[(a_med.Count / 2)];
                }
                double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                    chart3.Series[1].Points.AddXY(coor_2d[i].x, y_f9x0);

                }
                dataGridView10.Rows.Clear();
                dataGridView10.Columns.Clear();
                dataGridView10.AllowUserToAddRows = false;
                dataGridView10.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView10.Columns.Add("1", "Характеристика");
                dataGridView10.Columns.Add("2", "INF");
                dataGridView10.Columns.Add("3", "Значення");
                dataGridView10.Columns.Add("4", "SUP");
                //double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                //double N_for_N = Convert.ToDouble(coor_2d.Count);
                double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                dataGridView10.Rows.Add("Оцінка парметра а", "", Math.Round(ocinka_A, 4), "");
                dataGridView10.Rows.Add("Оцінка парметра b", "", Math.Round(ocinka_B, 4), "");
                double sigma_epsilon = Math.Sqrt(y_dispersia) * Math.Sqrt((1.0 - Math.Pow(koef_par_kor, 2.0)) * ((N_for_N - 1.0) / (N_for_N - 2.0)));
                double dispersia_zalishkova = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                    dispersia_zalishkova += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);
                }
                dispersia_zalishkova /= (N_for_N - 2.0);
                double S_a = Math.Sqrt(dispersia_zalishkova) * Math.Sqrt((1.0 / N_for_N) + (Math.Pow(x_seredne, 2.0) / (x_dispersia * (N_for_N - 1.0))));
                double S_b = Math.Sqrt(dispersia_zalishkova) / (Math.Sqrt(x_dispersia) * Math.Sqrt(N_for_N - 1.0));
                dataGridView10.Rows.Add("Залишкова дисперсія S", "", Math.Round(dispersia_zalishkova, 4), "");
                dataGridView10.Rows.Add("σ оцінка \n параметра а", "", Math.Round(S_a, 4), "");
                dataGridView10.Rows.Add("σ оцінка \n параметра b", "", Math.Round(S_b, 4), "");
                dataGridView10.Rows.Add("Інтервальне оцінювання параметрів", Math.Round(ocinka_A - (kvantil_studen * S_a), 4), "a", Math.Round(ocinka_A + (kvantil_studen * S_a), 4));
                dataGridView10.Rows.Add("Інтервальне оцінювання параметрів", Math.Round(ocinka_B - (kvantil_studen * S_b), 4), "b", Math.Round(ocinka_B + (kvantil_studen * S_b), 4));

                double koef_determinaz = Math.Pow(koef_par_kor, 2.0);
                dataGridView10.Rows.Add("Коефіцієнт детермінації", "", $"{Math.Round(koef_determinaz * 100.0, 4)}%", "");
                if (coor_2d.Count <= 100)
                {
                    kilkclass = (int)Math.Sqrt(coor_2d.Count);
                }
                else
                {
                    kilkclass = (int)Math.Pow(coor_2d.Count, 0.3333333);
                }
                double sered = coor_2d[coor_2d.Count - 1].x - coor_2d[0].x;
                double shah = sered / Convert.ToDouble(kilkclass);
                double shaht = shah;
                shah += coor_2d[0].x;
                double sum = coor_2d[0].x;

                int chastoty = 0;
                List<int> kil_tochek_v_promishku = new List<int>();
                List<double> dispersia_koshnoho_promishka = new List<double>();

                for (int i = 0; i < kilkclass; i++)
                {

                    chastoty = 0;
                    double y_v_vibirci = 0;
                    double diaper_v_promishlu = 0;
                    for (int p = 0; p < coor_2d.Count; p++)
                    {
                        if (coor_2d[p].x <= shah && coor_2d[p].x >= sum)
                        {
                            chastoty += 1;
                            y_v_vibirci += coor_2d[p].y;
                        }
                    }
                    kil_tochek_v_promishku.Add(chastoty);

                    double y_ser = y_v_vibirci / Convert.ToDouble(chastoty);

                    for (int p = 0; p < coor_2d.Count; p++)
                    {
                        if (coor_2d[p].x <= shah && coor_2d[p].x >= sum)
                        {
                            diaper_v_promishlu += Math.Pow(coor_2d[p].y - y_ser, 2.0);
                        }
                    }
                    diaper_v_promishlu /= Convert.ToDouble(chastoty - 1);
                    dispersia_koshnoho_promishka.Add(diaper_v_promishlu);
                    sum += shaht;
                    shah += shaht;


                }
                double c_statistika_Lambda = 0;
                for (int i = 0; i < kil_tochek_v_promishku.Count; i++)
                {
                    c_statistika_Lambda += Convert.ToDouble(1.0 / kil_tochek_v_promishku[i]);
                }
                c_statistika_Lambda -= (1.0 / N_for_N);
                c_statistika_Lambda = c_statistika_Lambda * (1.0 / (3.0 * Convert.ToDouble(kilkclass - 1))) + 1.0;
                double lambda_s_kvadrat = 0;
                for (int i = 0; i < dispersia_koshnoho_promishka.Count; i++)
                {
                    lambda_s_kvadrat += Convert.ToDouble(kil_tochek_v_promishku[i] - 1) * dispersia_koshnoho_promishka[i];

                }
                lambda_s_kvadrat /= (N_for_N - Convert.ToDouble(kilkclass));
                double Statistica_Lambda = 0;
                for (int i = 0; i < dispersia_koshnoho_promishka.Count; i++)
                {
                    Statistica_Lambda += Convert.ToDouble(kil_tochek_v_promishku[i]) * Math.Log(dispersia_koshnoho_promishka[i] / lambda_s_kvadrat);
                }
                Statistica_Lambda = Statistica_Lambda * (-1.0 / c_statistika_Lambda);
                richTextBox3.Text = "Лінійна регресія методом Тейла \n";
                richTextBox3.Text += $"y=({Math.Round(ocinka_A, 4)})+({Math.Round(ocinka_B, 4)})*x\n";
                double kvanta_pirsona = kvantil_Pirsona(kvantil_start_rozpodil, Convert.ToDouble(kilkclass - 1.0));
                // richTextBox3.Text += $"Λ={Math.Round(Statistica_Lambda,4)}";
                if (Statistica_Lambda > kvanta_pirsona)
                {
                    richTextBox3.Text += $"Λ={Math.Round(Statistica_Lambda, 4)} Λ>{Math.Round(kvanta_pirsona, 4)} дисперсія y не є стала\n";
                }
                else
                {
                    richTextBox3.Text += $"Λ={Math.Round(Statistica_Lambda, 4)} Λ<={Math.Round(kvanta_pirsona, 4)} дисперсія y стала\n";
                }
                double t_test_ocinki_A = ocinka_A / S_a;
                double t_test_ocinki_B = ocinka_B / S_b;
                if (Math.Abs(t_test_ocinki_A) > kvantil_studen)
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_A), 4)} t>{Math.Round(kvantil_studen, 4)} оцінка параметра а є значуща\n";
                }
                else
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_A), 4)} t<={Math.Round(kvantil_studen, 4)} оцінка параметра а не є значуща\n";
                }
                if (Math.Abs(t_test_ocinki_B) > kvantil_studen)
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_B), 4)} |t|>{Math.Round(kvantil_studen, 4)} оцінка параметра b є значуща\n";
                }
                else
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_B), 4)} |t|<={Math.Round(kvantil_studen, 4)} оцінка параметра b не є значуща\n";
                }
                double fishera_roazpodil = kvantil_Fishera(kvantil_start_rozpodil, N_for_N - 1.0, N_for_N - 3.0);
                double f_statistika = dispersia_zalishkova / y_dispersia;
                if (f_statistika > fishera_roazpodil)
                {
                    richTextBox3.Text += $"f={Math.Round(f_statistika, 4)} f>{Math.Round(fishera_roazpodil, 4)} відтворена лінійна модель регресії не є адекватна\n";
                }
                else
                {
                    richTextBox3.Text += $"f={Math.Round(f_statistika, 4)} f<={Math.Round(fishera_roazpodil, 4)} відтворена лінійна модель регресії є адекватна\n";
                }
            }
        }

        private void Method_Teyla_Click(object sender, EventArgs e)
        {
            if (!method_Teyla.Checked)
            {
                liniyna_regresia.Checked = false;
                dovirchi_intevalu_every_regresii.Checked = false;
                tolerantrni_meshi.Checked = false;
                parabolichna_regresia.Checked = false;
                kvaziliniyna_regresia.Checked = false;
                dovirchi_intervaly_for_prognozu.Checked = false;
                Function_shilnosty.Checked = false;
                kvazi_bez_waga.Checked = false;
                method_Teyla.Checked = true;

            }
            else
            {
                method_Teyla.Checked = false;
            }
        }

        private void Dovirchi_intervaly_for_prognozu_CheckedChanged(object sender, EventArgs e)
        {
            if (!dovirchi_intervaly_for_prognozu.Checked)
            {
                chart3.Series[6].Points.Clear();
                chart3.Series[7].Points.Clear();

            }
            else
            {
                List<tochki_2d> coor_2d = new List<tochki_2d>();
                for (int i = 0; i < tochki_2Ds.Count; i++)
                {
                    coor_2d.Add(tochki_2Ds[i]);
                }
                
                double y_seredne = 0;
                double x_seredne = 0;
                double x_dispersia = 0;
                double y_dispersia = 0;
                double xy_seredne = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    y_seredne += coor_2d[i].y;
                    x_seredne += coor_2d[i].x;
                    xy_seredne += (coor_2d[i].x * coor_2d[i].y);
                }
                y_seredne /= Convert.ToDouble(coor_2d.Count);
                x_seredne /= Convert.ToDouble(coor_2d.Count);
                xy_seredne /= Convert.ToDouble(coor_2d.Count);
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    x_dispersia += Math.Pow(coor_2d[i].x - x_seredne, 2.0);
                    y_dispersia += Math.Pow(coor_2d[i].y - y_seredne, 2.0);
                }
                x_dispersia /= Convert.ToDouble(coor_2d.Count - 1);
                y_dispersia /= Convert.ToDouble(coor_2d.Count - 1);
                if (liniyna_regresia.Checked)
                {
                    double koef_par_kor = (xy_seredne - (x_seredne * y_seredne)) / (Math.Sqrt(x_dispersia)* Math.Sqrt(y_dispersia));///оцінка парного коефіцієнта кореляції
                    koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d.Count) / Convert.ToDouble(coor_2d.Count - 1));
                    double ocinka_B = koef_par_kor * (Math.Sqrt(y_dispersia) / Math.Sqrt(x_dispersia));
                    double ocinka_A = y_seredne - (ocinka_B * x_seredne);
                    double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                    double N_for_N = Convert.ToDouble(coor_2d.Count);
                    double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                    double sigma_epsilon = Math.Sqrt(y_dispersia) * Math.Sqrt((1.0 - Math.Pow(koef_par_kor, 2.0)) * ((N_for_N - 1.0) / (N_for_N - 2.0)));
                    double dispersia_zalishkova = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                        dispersia_zalishkova += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);
                    }
                    dispersia_zalishkova /= (N_for_N - 2.0);
                    double S_a = Math.Sqrt(dispersia_zalishkova) * Math.Sqrt((1.0 / N_for_N) + (Math.Pow(x_seredne, 2.0) / (x_dispersia - 1.0)));
                    double S_b = Math.Sqrt(dispersia_zalishkova) / (Math.Sqrt(x_dispersia) * Math.Sqrt(N_for_N - 1.0));
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                        double S_y_ser_x = Math.Sqrt((Math.Pow(sigma_epsilon, 2.0) *( 1.0+(1.0/N_for_N))) + (Math.Pow(S_b, 2.0) * Math.Pow(coor_2d[i].x - x_seredne, 2.0)));
                        chart3.Series[6].Points.AddXY(coor_2d[i].x, y_f9x0 - (S_y_ser_x * kvantil_studen));
                        chart3.Series[7].Points.AddXY(coor_2d[i].x, y_f9x0 + (S_y_ser_x * kvantil_studen));
                    }
                }
                else if (parabolichna_regresia.Checked)
                {
                    double x_kvadrat_seredne = 0;
                    double x_cube_seredne = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        x_kvadrat_seredne += Math.Pow(coor_2d[i].x, 2.0);
                        x_cube_seredne += Math.Pow(coor_2d[i].x, 3.0);

                    }

                    x_cube_seredne /= Convert.ToDouble(coor_2d.Count);
                    x_kvadrat_seredne /= Convert.ToDouble(coor_2d.Count);




                    double N_for_N = Convert.ToDouble(coor_2d.Count);


                    double ocinka_B = 0;
                    double numerator_ocinki_b = 0;
                    double denaminator_icinki_b = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        numerator_ocinki_b += (coor_2d[i].x - x_seredne) * coor_2d[i].y;
                        denaminator_icinki_b += Math.Pow(coor_2d[i].x - x_seredne, 2.0);
                    }
                    ocinka_B = numerator_ocinki_b / denaminator_icinki_b;
                    double ocinka_A = y_seredne;
                    double ocinka_C = 0;
                    double numerator_ocinki_c = 0;
                    double denaminator_icinki_c = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        numerator_ocinki_c += fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne) * coor_2d[i].y;
                        denaminator_icinki_c += Math.Pow(fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne), 2.0);
                    }
                    ocinka_C = numerator_ocinki_c / denaminator_icinki_c;
                    double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                    double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 3.0);
                    double dispersia_zalishkova = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + ((coor_2d[i].x - x_seredne) * ocinka_B) + (ocinka_C * fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne));
                        dispersia_zalishkova += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);
                    }
                    dispersia_zalishkova /= (N_for_N - 3.0);
                    double fi2_kvadrat_seredne = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        fi2_kvadrat_seredne += Math.Pow(fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne), 2.0);
                    }
                    fi2_kvadrat_seredne /= N_for_N;
                    double S_b1 = Math.Sqrt(dispersia_zalishkova) / (Math.Sqrt(x_dispersia) * Math.Sqrt(N_for_N));
                    double S_c1 = Math.Sqrt(dispersia_zalishkova) / (Math.Sqrt(N_for_N * fi2_kvadrat_seredne));
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double fiii1 = Math.Pow(coor_2d[i].x - x_seredne,2.0);
                        double fiii2 = Math.Pow(fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne),2.0);

                        double d_for_d1 = Math.Sqrt(dispersia_zalishkova) / Math.Sqrt(N_for_N);
                        double s_y_ser_vid_x = Math.Sqrt(N_for_N + 1.0 + (fiii1 / x_dispersia) + (fiii2 / fi2_kvadrat_seredne)) * d_for_d1;
                        double y_f9x0 = ocinka_A + ((coor_2d[i].x - x_seredne) * ocinka_B) + (ocinka_C * fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne));
                        chart3.Series[6].Points.AddXY(coor_2d[i].x, y_f9x0 - (s_y_ser_vid_x * kvantil_studen));
                        chart3.Series[7].Points.AddXY(coor_2d[i].x, y_f9x0 + (s_y_ser_vid_x * kvantil_studen));

                    }
                }
                else if (kvazi_bez_waga.Checked)
                {
                    List<tochki_2d> coor_2d_zminene = new List<tochki_2d>();
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double novekiy_x = 1.0 / (coor_2d[i].x);
                        double novekiy_y = 1.0 / (coor_2d[i].y);
                        tochki_2d tochki_2 = new tochki_2d();
                        tochki_2.x = novekiy_x;
                        tochki_2.y = novekiy_y;
                        coor_2d_zminene.Add(tochki_2);
                    }
                    double z_seredne = 0;
                    double t_seredne = 0;
                    double t_dispersia = 0;
                    double z_dispersia = 0;
                    double zt_seredne = 0;
                    for (int i = 0; i < coor_2d_zminene.Count; i++)
                    {
                        z_seredne += coor_2d_zminene[i].y;
                        t_seredne += coor_2d_zminene[i].x;
                        zt_seredne += coor_2d_zminene[i].y * coor_2d_zminene[i].x;
                    }
                    zt_seredne /= Convert.ToDouble(coor_2d_zminene.Count);
                    z_seredne /= Convert.ToDouble(coor_2d_zminene.Count);
                    t_seredne /= Convert.ToDouble(coor_2d_zminene.Count);

                    for (int i = 0; i < coor_2d_zminene.Count; i++)
                    {
                        t_dispersia += Math.Pow(coor_2d_zminene[i].x - t_seredne, 2.0);
                        z_dispersia += Math.Pow(coor_2d_zminene[i].y - z_seredne, 2.0);
                    }
                    t_dispersia /= Convert.ToDouble(coor_2d_zminene.Count - 1);
                    z_dispersia /= Convert.ToDouble(coor_2d_zminene.Count - 1);

                    double koef_par_kor = (zt_seredne - (z_seredne * t_seredne)) / (Math.Sqrt(t_dispersia) * Math.Sqrt(z_dispersia));///оцінка парного коефіцієнта кореляції
                    koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d_zminene.Count) / Convert.ToDouble(coor_2d_zminene.Count - 1));
                    double ocinka_B = koef_par_kor * (Math.Sqrt(z_dispersia) / Math.Sqrt(t_dispersia));

                    double ocinka_A = z_seredne - (ocinka_B * t_seredne);

                    double N_for_N = Convert.ToDouble(coor_2d_zminene.Count);

                    double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                    double paramtr_a = ocinka_A / ocinka_B;
                    double parametr_b = 1.0 / ocinka_B;

                    double dispersia_zalishkova_z_ocinkamy = 0;
                    for (int i = 0; i < coor_2d_zminene.Count; i++)
                    {
                        double videmnyk = coor_2d_zminene[i].x * ocinka_B + ocinka_A;
                        dispersia_zalishkova_z_ocinkamy += Math.Pow(coor_2d_zminene[i].y - videmnyk, 2.0);
                    }
                    dispersia_zalishkova_z_ocinkamy /= (N_for_N - 2.0);
                    double dispersia_zalishkova_z_parametramy = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double d1_ce = (1.0 / coor_2d[i].x) + paramtr_a;
                        double y_f9x0 = parametr_b / d1_ce;
                        dispersia_zalishkova_z_parametramy += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);

                    }
                    dispersia_zalishkova_z_parametramy /= (N_for_N - 2.0);
                    double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                    double S_a = Math.Sqrt(dispersia_zalishkova_z_parametramy) * Math.Sqrt((1.0 / N_for_N) + (Math.Pow(t_seredne, 2.0) / (t_dispersia * (N_for_N - 1.0))));
                    double S_b = Math.Sqrt(dispersia_zalishkova_z_parametramy) / (Math.Sqrt(t_dispersia) * Math.Sqrt(N_for_N - 1.0));
                    for (int i = 0; i < coor_2d_zminene.Count; i++)
                    {
                        double d1_ce = (1.0 / coor_2d[i].x) + paramtr_a;
                        double y_f9x0 = parametr_b / d1_ce;
                        
                        double S_y_ser_x = Math.Sqrt((dispersia_zalishkova_z_parametramy *(1.0+(1.0/N_for_N))) + (Math.Pow(S_b, 2.0) * Math.Pow(coor_2d_zminene[i].x - t_seredne, 2.0)));
                        chart3.Series[6].Points.AddXY(coor_2d[i].x, (y_f9x0 - (S_y_ser_x * kvantil_studen)));
                        chart3.Series[7].Points.AddXY(coor_2d[i].x, (y_f9x0 + (S_y_ser_x * kvantil_studen)));
                    }

                }
                else if (method_Teyla.Checked)
                {
                    double koef_par_kor = (xy_seredne - (x_seredne * y_seredne)) / (Math.Sqrt(x_dispersia) * Math.Sqrt(y_dispersia));///оцінка парного коефіцієнта кореляції
                    koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d.Count) / Convert.ToDouble(coor_2d.Count - 1));
                    double ocinka_B = 0;

                    double ocinka_A = y_seredne;
                    List<double> b_med = new List<double>();
                    List<double> a_med = new List<double>();
                    for (int j = 0; j < coor_2d.Count; j++)
                    {
                        for (int i = 0; i < j - 1; i++)
                        {
                            double u_list = (coor_2d[j].y - coor_2d[i].y) / (coor_2d[j].x - coor_2d[i].x);
                            b_med.Add(u_list);
                        }
                    }
                    b_med.Sort();
                    if (b_med.Count % 2 == 0)
                    {
                        ocinka_B = (b_med[(b_med.Count / 2) - 1] + b_med[(b_med.Count / 2)]) / 2.0;
                    }
                    else
                    {
                        ocinka_B = b_med[(b_med.Count / 2)];
                    }
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double u_a_list = coor_2d[i].y - (ocinka_B * coor_2d[i].x);
                        a_med.Add(u_a_list);
                    }
                    a_med.Sort();
                    if (a_med.Count % 2 == 0)
                    {
                        ocinka_A = (a_med[(a_med.Count / 2) - 1] + a_med[(a_med.Count / 2)]) / 2.0;
                    }
                    else
                    {
                        ocinka_A = a_med[(a_med.Count / 2)];
                    }
                    double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                    double N_for_N = Convert.ToDouble(coor_2d.Count);
                    double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                    double sigma_epsilon = Math.Sqrt(y_dispersia) * Math.Sqrt((1.0 - Math.Pow(koef_par_kor, 2.0)) * ((N_for_N - 1.0) / (N_for_N - 2.0)));
                    double dispersia_zalishkova = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                        dispersia_zalishkova += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);
                    }
                    dispersia_zalishkova /= (N_for_N - 2.0);
                    double S_a = Math.Sqrt(dispersia_zalishkova) * Math.Sqrt((1.0 / N_for_N) + (Math.Pow(x_seredne, 2.0) / (x_dispersia - 1.0)));
                    double S_b = Math.Sqrt(dispersia_zalishkova) / (Math.Sqrt(x_dispersia) * Math.Sqrt(N_for_N - 1.0));
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                        double S_y_ser_x = Math.Sqrt((Math.Pow(sigma_epsilon, 2.0) * (1.0 + (1.0 / N_for_N))) + (Math.Pow(S_b, 2.0) * Math.Pow(coor_2d[i].x - x_seredne, 2.0)));
                        chart3.Series[6].Points.AddXY(coor_2d[i].x, y_f9x0 - (S_y_ser_x * kvantil_studen));
                        chart3.Series[7].Points.AddXY(coor_2d[i].x, y_f9x0 + (S_y_ser_x * kvantil_studen));
                    }
                }

            }
        }

        private void Dovirchi_intervaly_for_prognozu_Click(object sender, EventArgs e)
        {

            if (!dovirchi_intervaly_for_prognozu.Checked)
            {
                dovirchi_intervaly_for_prognozu.Checked = true;

            }
            else
            {
                dovirchi_intervaly_for_prognozu.Checked = false;
            }
        }

        double fi2_vid_x(double x, double x_cube_seredne, double x_kvadrat_seredne, double x_disperisa, double x_seredne)
        {
            double kof = (x_cube_seredne - (x_kvadrat_seredne * x_seredne)) / x_disperisa;
            double kof2 = kof * (x - x_seredne);
            double result = Math.Pow(x,2.0)-kof2-x_kvadrat_seredne;
            return result;
        }
        private void Parabolichna_regresia_CheckedChanged(object sender, EventArgs e)
        {
            if (!parabolichna_regresia.Checked)
            {
                chart3.Series[1].Points.Clear();
                

            }
            else
            {
                chart3.Series[1].Points.Clear();
                List<tochki_2d> coor_2d = new List<tochki_2d>();
                for (int i = 0; i < tochki_2Ds.Count; i++)
                {
                    coor_2d.Add(tochki_2Ds[i]);
                }
                
                double y_seredne = 0;
                double x_seredne = 0;
                double x_dispersia = 0;
                double y_dispersia = 0;
                double xy_seredne = 0;
                double x_kvadrat_seredne = 0;
                double x_cube_seredne = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    x_kvadrat_seredne += Math.Pow(coor_2d[i].x, 2.0);
                    x_cube_seredne += Math.Pow(coor_2d[i].x, 3.0);
                    y_seredne += coor_2d[i].y;
                    x_seredne += coor_2d[i].x;
                    xy_seredne += (coor_2d[i].x * coor_2d[i].y);
                }
                
                x_cube_seredne /= Convert.ToDouble(coor_2d.Count);
                x_kvadrat_seredne /= Convert.ToDouble(coor_2d.Count);
                y_seredne /= Convert.ToDouble(coor_2d.Count);
                x_seredne /= Convert.ToDouble(coor_2d.Count);
                xy_seredne /= Convert.ToDouble(coor_2d.Count);
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    x_dispersia += Math.Pow(coor_2d[i].x - x_seredne, 2.0);
                    y_dispersia += Math.Pow(coor_2d[i].y - y_seredne, 2.0);
                }
                x_dispersia /= Convert.ToDouble(coor_2d.Count - 1);
                y_dispersia /= Convert.ToDouble(coor_2d.Count - 1);


                double N_for_N = Convert.ToDouble(coor_2d.Count);
                double koef_par_kor = (xy_seredne - (x_seredne * y_seredne)) / (Math.Sqrt(x_dispersia * y_dispersia));///оцінка парного коефіцієнта кореляції
                koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d.Count) / Convert.ToDouble(coor_2d.Count - 1));

                double ocinka_B = 0;
                double numerator_ocinki_b = 0;
                double denaminator_icinki_b = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    numerator_ocinki_b += (coor_2d[i].x - x_seredne) * coor_2d[i].y;
                    denaminator_icinki_b += Math.Pow(coor_2d[i].x - x_seredne, 2.0);
                }
                ocinka_B = numerator_ocinki_b / denaminator_icinki_b;
                double ocinka_A = y_seredne;
                double ocinka_C = 0;
                double numerator_ocinki_c = 0;
                double denaminator_icinki_c = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    numerator_ocinki_c += fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne) * coor_2d[i].y;
                    denaminator_icinki_c += Math.Pow(fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne), 2.0);
                }
                ocinka_C = numerator_ocinki_c / denaminator_icinki_c;
                double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double y_f9x0 = ocinka_A + ((coor_2d[i].x - x_seredne) * ocinka_B) + (ocinka_C * fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne));
                    chart3.Series[1].Points.AddXY(coor_2d[i].x, y_f9x0);
      
                }
                dataGridView10.Rows.Clear();
                dataGridView10.Columns.Clear();
                dataGridView10.AllowUserToAddRows = false;
                dataGridView10.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView10.Columns.Add("1", "Характеристика");
                dataGridView10.Columns.Add("2", "INF");
                dataGridView10.Columns.Add("3", "Значення");
                dataGridView10.Columns.Add("4", "SUP");
                //double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                //double N_for_N = Convert.ToDouble(coor_2d.Count);
                double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 3.0);
                dataGridView10.Rows.Add("Оцінка парметра а", "", Math.Round(ocinka_A, 4), "");
                dataGridView10.Rows.Add("Оцінка парметра b", "", Math.Round(ocinka_B, 4), "");
                dataGridView10.Rows.Add("Оцінка парметра c", "", Math.Round(ocinka_C, 4), "");
                double sigma_epsilon = Math.Sqrt(y_dispersia) * Math.Sqrt((1.0 - Math.Pow(koef_par_kor, 2.0)) * ((N_for_N - 1.0) / (N_for_N - 2.0)));
                
               
                double dispersia_zalishkova = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double y_f9x0 = ocinka_A + ((coor_2d[i].x - x_seredne) * ocinka_B) + (ocinka_C * fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne));
                    dispersia_zalishkova += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);
                }
                dispersia_zalishkova /= (N_for_N - 3.0);
                double fi2_kvadrat_seredne = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    fi2_kvadrat_seredne += Math.Pow(fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne), 2.0);
                }
                fi2_kvadrat_seredne /= N_for_N;
                double S_b1 = Math.Sqrt(dispersia_zalishkova) / (Math.Sqrt(x_dispersia) * Math.Sqrt(N_for_N));
                double S_c1 = Math.Sqrt(dispersia_zalishkova) / (Math.Sqrt(N_for_N * fi2_kvadrat_seredne));
                double S_a1 = Math.Sqrt(dispersia_zalishkova) / Math.Sqrt(N_for_N);
                dataGridView10.Rows.Add("Залишкова дисперсія S", "", Math.Round(dispersia_zalishkova, 4), "");
                dataGridView10.Rows.Add("σ оцінка \n параметра а", "", Math.Round(S_a1, 4), "");
                dataGridView10.Rows.Add("σ оцінка \n параметра b", "", Math.Round(S_b1, 4), "");
                dataGridView10.Rows.Add("σ оцінка \n параметра c", "", Math.Round(S_c1, 4), "");
                dataGridView10.Rows.Add("Інтервальне оцінювання параметрів", Math.Round(ocinka_A - (kvantil_studen * S_a1), 4), "a", Math.Round(ocinka_A + (kvantil_studen * S_a1), 4));
                dataGridView10.Rows.Add("Інтервальне оцінювання параметрів", Math.Round(ocinka_B - (kvantil_studen * S_b1), 4), "b", Math.Round(ocinka_B + (kvantil_studen * S_b1), 4));
                dataGridView10.Rows.Add("Інтервальне оцінювання параметрів", Math.Round(ocinka_C - (kvantil_studen * S_c1), 4), "c", Math.Round(ocinka_C + (kvantil_studen * S_c1), 4));
                double koef_determinaz = (1.0 - (dispersia_zalishkova / y_dispersia));
                dataGridView10.Rows.Add("Коефіцієнт детермінації", "",$"{Math.Round(koef_determinaz*100.0,4)}%", "");
               
                richTextBox3.Text = "Параболічна регресія \n";
                richTextBox3.Text += $"y=({Math.Round(ocinka_A, 4)})+({Math.Round(ocinka_B, 4)})*x+({Math.Round(ocinka_B, 4)})*x^2\n";
                double kvanta_pirsona = kvantil_Pirsona(kvantil_start_rozpodil, Convert.ToDouble(kilkclass - 1.0));
                
                double t_test_ocinki_A = (ocinka_A / Math.Sqrt(dispersia_zalishkova))*Math.Sqrt(N_for_N);
                double t_test_ocinki_B = ((ocinka_B*Math.Sqrt(x_dispersia))/ Math.Sqrt(dispersia_zalishkova))*Math.Sqrt(N_for_N);
                double t_test_ocinki_C = (ocinka_C / Math.Sqrt(dispersia_zalishkova))*Math.Sqrt(N_for_N* fi2_kvadrat_seredne);
                if (Math.Abs(t_test_ocinki_A) > kvantil_studen)
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_A), 4)} t>{Math.Round(kvantil_studen, 4)} оцінка параметра а є значуща\n";
                }
                else
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_A), 4)} t<={Math.Round(kvantil_studen, 4)} оцінка параметра а не є значуща\n";
                }
                if (Math.Abs(t_test_ocinki_B) > kvantil_studen)
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_B), 4)} |t|>{Math.Round(kvantil_studen, 4)} оцінка параметра b є значуща\n";
                }
                else
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_B), 4)} |t|<={Math.Round(kvantil_studen, 4)} оцінка параметра b не є значуща\n";
                }
                if (Math.Abs(t_test_ocinki_C) > kvantil_studen)
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_C), 4)} |t|>{Math.Round(kvantil_studen, 4)} оцінка параметра с є значуща\n";
                }
                else
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_C), 4)} |t|<={Math.Round(kvantil_studen, 4)} оцінка параметра с не є значуща\n";
                }
                double fishera_roazpodil = kvantil_Fishera(kvantil_start_rozpodil, N_for_N - 1.0, N_for_N - 3.0);
                double f_statistika = dispersia_zalishkova / y_dispersia;
                if (f_statistika > fishera_roazpodil)
                {
                    richTextBox3.Text += $"f={Math.Round(f_statistika, 4)} f>{Math.Round(fishera_roazpodil, 4)} відтворена параболічна модель регресії не є адекватна\n";
                }
                else
                {
                    richTextBox3.Text += $"f={Math.Round(f_statistika, 4)} f<={Math.Round(fishera_roazpodil, 4)} відтворена параболічна модель регресії є адекватна\n";
                }
            }
        }

        private void Parabolichna_regresia_Click(object sender, EventArgs e)
        {
            if (!parabolichna_regresia.Checked)
            {
                liniyna_regresia.Checked = false;
                dovirchi_intevalu_every_regresii.Checked = false;
                tolerantrni_meshi.Checked = false;
                Function_shilnosty.Checked = false;
                kvaziliniyna_regresia.Checked = false;
                dovirchi_intervaly_for_prognozu.Checked = false;
                method_Teyla.Checked = false;
                kvazi_bez_waga.Checked = false;
                parabolichna_regresia.Checked = true;

            }
            else
            {
                parabolichna_regresia.Checked = false;
            }
        }

        private void Dovirchi_intevalu_every_regresii_CheckedChanged(object sender, EventArgs e)
        {
            if (!dovirchi_intevalu_every_regresii.Checked)
            {
                chart3.Series[4].Points.Clear();
                chart3.Series[5].Points.Clear();

            }
            else
            {
                List<tochki_2d> coor_2d = new List<tochki_2d>();
                for (int i = 0; i < tochki_2Ds.Count; i++)
                {
                    coor_2d.Add(tochki_2Ds[i]);
                }
                
                double y_seredne = 0;
                double x_seredne = 0;
                double x_dispersia = 0;
                double y_dispersia = 0;
                double xy_seredne = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    y_seredne += coor_2d[i].y;
                    x_seredne += coor_2d[i].x;
                    xy_seredne += (coor_2d[i].x * coor_2d[i].y);
                }
                y_seredne /= Convert.ToDouble(coor_2d.Count);
                x_seredne /= Convert.ToDouble(coor_2d.Count);
                xy_seredne /= Convert.ToDouble(coor_2d.Count);
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    x_dispersia += Math.Pow(coor_2d[i].x - x_seredne, 2.0);
                    y_dispersia += Math.Pow(coor_2d[i].y - y_seredne, 2.0);
                }
                x_dispersia /= Convert.ToDouble(coor_2d.Count - 1);
                y_dispersia /= Convert.ToDouble(coor_2d.Count - 1);
                if (liniyna_regresia.Checked)
                {
                    double koef_par_kor = (xy_seredne - (x_seredne * y_seredne)) / (Math.Sqrt(x_dispersia) * Math.Sqrt(y_dispersia));///оцінка парного коефіцієнта кореляції
                    koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d.Count) / Convert.ToDouble(coor_2d.Count - 1));
                    double ocinka_B = koef_par_kor * (Math.Sqrt(y_dispersia) / Math.Sqrt(x_dispersia));
                    double ocinka_A = y_seredne - (ocinka_B * x_seredne);
                    double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                    double N_for_N = Convert.ToDouble(coor_2d.Count);
                    double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                    double sigma_epsilon = Math.Sqrt(y_dispersia) * Math.Sqrt((1.0 - Math.Pow(koef_par_kor, 2.0)) * ((N_for_N - 1.0) / (N_for_N - 2.0)));
                    double dispersia_zalishkova = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                        dispersia_zalishkova += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);
                    }
                    dispersia_zalishkova /= (N_for_N - 2.0);
                    double S_a = Math.Sqrt(dispersia_zalishkova) * Math.Sqrt((1.0 / N_for_N) + (Math.Pow(x_seredne, 2.0) / (x_dispersia * (N_for_N - 1.0))));
                    double S_b = Math.Sqrt(dispersia_zalishkova) / (Math.Sqrt(x_dispersia) * Math.Sqrt(N_for_N - 1.0));
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                        double S_y_ser_x = Math.Sqrt((Math.Pow(sigma_epsilon, 2.0) / N_for_N) + (Math.Pow(S_b, 2.0) * Math.Pow(coor_2d[i].x - x_seredne, 2.0)));
                        chart3.Series[4].Points.AddXY(coor_2d[i].x, y_f9x0 - (S_y_ser_x * kvantil_studen));
                        chart3.Series[5].Points.AddXY(coor_2d[i].x, y_f9x0 + (S_y_ser_x * kvantil_studen));
                    }
                }
                else if (parabolichna_regresia.Checked)
                {
                    double x_kvadrat_seredne = 0;
                    double x_cube_seredne = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        x_kvadrat_seredne += Math.Pow(coor_2d[i].x, 2.0);
                        x_cube_seredne += Math.Pow(coor_2d[i].x, 3.0);

                    }

                    x_cube_seredne /= Convert.ToDouble(coor_2d.Count);
                    x_kvadrat_seredne /= Convert.ToDouble(coor_2d.Count);

                    


                    double N_for_N = Convert.ToDouble(coor_2d.Count);


                    double ocinka_B = 0;
                    double numerator_ocinki_b = 0;
                    double denaminator_icinki_b = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        numerator_ocinki_b += (coor_2d[i].x - x_seredne) * coor_2d[i].y;
                        denaminator_icinki_b += Math.Pow(coor_2d[i].x - x_seredne, 2.0);
                    }
                    ocinka_B = numerator_ocinki_b / denaminator_icinki_b;
                    double ocinka_A = y_seredne;
                    double ocinka_C = 0;
                    double numerator_ocinki_c = 0;
                    double denaminator_icinki_c = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        numerator_ocinki_c += fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne) * coor_2d[i].y;
                        denaminator_icinki_c += Math.Pow(fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne), 2.0);
                    }
                    ocinka_C = numerator_ocinki_c / denaminator_icinki_c;
                    double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                    double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                    double dispersia_zalishkova = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + ((coor_2d[i].x - x_seredne) * ocinka_B) + (ocinka_C * fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne));
                        dispersia_zalishkova += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);
                    }
                    dispersia_zalishkova /= (N_for_N - 3.0);
                    double fi2_kvadrat_seredne = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        fi2_kvadrat_seredne += Math.Pow(fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne), 2.0);
                    }
                    fi2_kvadrat_seredne /= N_for_N;
                    double S_b1 = Math.Sqrt(dispersia_zalishkova) / (Math.Sqrt(x_dispersia) * Math.Sqrt(N_for_N));
                    double S_c1 = Math.Sqrt(dispersia_zalishkova) / (Math.Sqrt(N_for_N * fi2_kvadrat_seredne));
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double s_y_ser_vid_x = Math.Sqrt((dispersia_zalishkova / N_for_N) + (Math.Pow(S_b1, 2.0) * Math.Pow(coor_2d[i].x-x_seredne,2.0))+(Math.Pow(S_c1, 2.0) * Math.Pow(fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne), 2.0)));
                        double y_f9x0 = ocinka_A + ((coor_2d[i].x - x_seredne) * ocinka_B) + (ocinka_C * fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne));
                        chart3.Series[4].Points.AddXY(coor_2d[i].x, y_f9x0 - (s_y_ser_vid_x * kvantil_studen));
                        chart3.Series[5].Points.AddXY(coor_2d[i].x, y_f9x0 + (s_y_ser_vid_x * kvantil_studen));

                    }
                }
                else if (kvazi_bez_waga.Checked)
                {                    
                    List<tochki_2d> coor_2d_zminene = new List<tochki_2d>();                   
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double novekiy_x = 1.0 / (coor_2d[i].x);
                        double novekiy_y = 1.0 / (coor_2d[i].y);
                        tochki_2d tochki_2 = new tochki_2d();
                        tochki_2.x = novekiy_x;
                        tochki_2.y = novekiy_y;
                        coor_2d_zminene.Add(tochki_2);
                    }
                    double z_seredne = 0;
                    double t_seredne = 0;
                    double t_dispersia = 0;
                    double z_dispersia = 0;
                    double zt_seredne = 0;
                    for (int i = 0; i < coor_2d_zminene.Count; i++)
                    {
                        z_seredne += coor_2d_zminene[i].y;
                        t_seredne += coor_2d_zminene[i].x;
                        zt_seredne += coor_2d_zminene[i].y * coor_2d_zminene[i].x;
                    }
                    zt_seredne /= Convert.ToDouble(coor_2d_zminene.Count);
                    z_seredne /= Convert.ToDouble(coor_2d_zminene.Count);
                    t_seredne /= Convert.ToDouble(coor_2d_zminene.Count);

                    for (int i = 0; i < coor_2d_zminene.Count; i++)
                    {
                        t_dispersia += Math.Pow(coor_2d_zminene[i].x - t_seredne, 2.0);
                        z_dispersia += Math.Pow(coor_2d_zminene[i].y - z_seredne, 2.0);
                    }
                    t_dispersia /= Convert.ToDouble(coor_2d_zminene.Count - 1);
                    z_dispersia /= Convert.ToDouble(coor_2d_zminene.Count - 1);

                    double koef_par_kor = (zt_seredne - (z_seredne * t_seredne)) / (Math.Sqrt(t_dispersia) * Math.Sqrt(z_dispersia));///оцінка парного коефіцієнта кореляції
                    koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d_zminene.Count) / Convert.ToDouble(coor_2d_zminene.Count - 1));
                    double ocinka_B = koef_par_kor * (Math.Sqrt(z_dispersia) / Math.Sqrt(t_dispersia));

                    double ocinka_A = z_seredne - (ocinka_B * t_seredne);

                    double N_for_N = Convert.ToDouble(coor_2d_zminene.Count);

                    double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                    double paramtr_a = ocinka_A / ocinka_B;
                    double parametr_b = 1.0 / ocinka_B;
                   
                    double dispersia_zalishkova_z_ocinkamy = 0;
                    for (int i = 0; i < coor_2d_zminene.Count; i++)
                    {
                        double videmnyk = coor_2d_zminene[i].x * ocinka_B + ocinka_A;
                        dispersia_zalishkova_z_ocinkamy += Math.Pow(coor_2d_zminene[i].y - videmnyk, 2.0);
                    }
                    dispersia_zalishkova_z_ocinkamy /= (N_for_N - 2.0);
                    double dispersia_zalishkova_z_parametramy = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double d1_ce = (1.0 / coor_2d[i].x) + paramtr_a;
                        double y_f9x0 = parametr_b / d1_ce;
                        dispersia_zalishkova_z_parametramy += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);

                    }
                    dispersia_zalishkova_z_parametramy /= (N_for_N - 2.0);  
                    double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                    double S_a = Math.Sqrt(dispersia_zalishkova_z_parametramy) * Math.Sqrt((1.0 / N_for_N) + (Math.Pow(t_seredne, 2.0) / (t_dispersia * (N_for_N - 1.0))));
                    double S_b = Math.Sqrt(dispersia_zalishkova_z_parametramy) / (Math.Sqrt(t_dispersia) * Math.Sqrt(N_for_N - 1.0));
                   for (int i = 0; i < coor_2d_zminene.Count; i++)
                   {
                        double y_f9x0 = ocinka_A + (coor_2d_zminene[i].x * ocinka_B);
                        double S_y_ser_x = Math.Sqrt((dispersia_zalishkova_z_parametramy / N_for_N) + (Math.Pow(S_b, 2.0) * Math.Pow(coor_2d_zminene[i].x - t_seredne, 2.0)));
                        chart3.Series[4].Points.AddXY(coor_2d[i].x,1.0/( y_f9x0 - (S_y_ser_x * kvantil_studen)));
                        chart3.Series[5].Points.AddXY(coor_2d[i].x,1.0/( y_f9x0 + (S_y_ser_x * kvantil_studen)));
                   }
                   
                }
                else if (method_Teyla.Checked)
                {
                    double koef_par_kor = (xy_seredne - (x_seredne * y_seredne)) / (Math.Sqrt(x_dispersia) * Math.Sqrt(y_dispersia));///оцінка парного коефіцієнта кореляції
                    koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d.Count) / Convert.ToDouble(coor_2d.Count - 1));
                    double ocinka_B = 0;

                    double ocinka_A = y_seredne;
                    List<double> b_med = new List<double>();
                    List<double> a_med = new List<double>();
                    for (int j = 0; j < coor_2d.Count; j++)
                    {
                        for (int i = 0; i < j - 1; i++)
                        {
                            double u_list = (coor_2d[j].y - coor_2d[i].y) / (coor_2d[j].x - coor_2d[i].x);
                            b_med.Add(u_list);
                        }
                    }
                    b_med.Sort();
                    if (b_med.Count % 2 == 0)
                    {
                        ocinka_B = (b_med[(b_med.Count / 2) - 1] + b_med[(b_med.Count / 2)]) / 2.0;
                    }
                    else
                    {
                        ocinka_B = b_med[(b_med.Count / 2)];
                    }
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double u_a_list = coor_2d[i].y - (ocinka_B * coor_2d[i].x);
                        a_med.Add(u_a_list);
                    }
                    a_med.Sort();
                    if (a_med.Count % 2 == 0)
                    {
                        ocinka_A = (a_med[(a_med.Count / 2) - 1] + a_med[(a_med.Count / 2)]) / 2.0;
                    }
                    else
                    {
                        ocinka_A = a_med[(a_med.Count / 2)];
                    }
                    double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                    double N_for_N = Convert.ToDouble(coor_2d.Count);
                    double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                    double sigma_epsilon = Math.Sqrt(y_dispersia) * Math.Sqrt((1.0 - Math.Pow(koef_par_kor, 2.0)) * ((N_for_N - 1.0) / (N_for_N - 2.0)));
                    double dispersia_zalishkova = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                        dispersia_zalishkova += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);
                    }
                    dispersia_zalishkova /= (N_for_N - 2.0);
                    double S_a = Math.Sqrt(dispersia_zalishkova) * Math.Sqrt((1.0 / N_for_N) + (Math.Pow(x_seredne, 2.0) / (x_dispersia * (N_for_N - 1.0))));
                    double S_b = Math.Sqrt(dispersia_zalishkova) / (Math.Sqrt(x_dispersia) * Math.Sqrt(N_for_N - 1.0));
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                        double S_y_ser_x = Math.Sqrt((Math.Pow(sigma_epsilon, 2.0) / N_for_N) + (Math.Pow(S_b, 2.0) * Math.Pow(coor_2d[i].x - x_seredne, 2.0)));
                        chart3.Series[4].Points.AddXY(coor_2d[i].x, y_f9x0 - (S_y_ser_x * kvantil_studen));
                        chart3.Series[5].Points.AddXY(coor_2d[i].x, y_f9x0 + (S_y_ser_x * kvantil_studen));
                    }
                }
                
            }
        }

        private void Dovirchi_intevalu_every_regresii_Click(object sender, EventArgs e)
        {
            if (!dovirchi_intevalu_every_regresii.Checked)
            {
                dovirchi_intevalu_every_regresii.Checked = true;

            }
            else
            {
                dovirchi_intevalu_every_regresii.Checked = false;
            }
        }

        private void Tolerantrni_meshi_Click(object sender, EventArgs e)
        {
            if (!tolerantrni_meshi.Checked)
            {
                tolerantrni_meshi.Checked = true;

            }
            else
            {
                tolerantrni_meshi.Checked = false;
            }
        }

        private void Tolerantrni_meshi_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!tolerantrni_meshi.Checked)
            {
                chart3.Series[2].Points.Clear();
                chart3.Series[3].Points.Clear();
            }
            else
            {
                List<tochki_2d> coor_2d = new List<tochki_2d>();
                for (int i = 0; i < tochki_2Ds.Count; i++)
                {
                    coor_2d.Add(tochki_2Ds[i]);
                }
                
                double y_seredne = 0;
                double x_seredne = 0;
                double x_dispersia = 0;
                double y_dispersia = 0;
                double xy_seredne = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    y_seredne += coor_2d[i].y;
                    x_seredne += coor_2d[i].x;
                    xy_seredne += (coor_2d[i].x * coor_2d[i].y);
                }
                y_seredne /= Convert.ToDouble(coor_2d.Count);
                x_seredne /= Convert.ToDouble(coor_2d.Count);
                xy_seredne /= Convert.ToDouble(coor_2d.Count);
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    x_dispersia += Math.Pow(coor_2d[i].x - x_seredne, 2.0);
                    y_dispersia += Math.Pow(coor_2d[i].y - y_seredne, 2.0);
                }
                x_dispersia /= Convert.ToDouble(coor_2d.Count - 1);
                y_dispersia /= Convert.ToDouble(coor_2d.Count - 1);


                if (liniyna_regresia.Checked)
                {
                    double koef_par_kor = (xy_seredne - (x_seredne * y_seredne)) / (Math.Sqrt(x_dispersia * y_dispersia));///оцінка парного коефіцієнта кореляції
                    koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d.Count) / Convert.ToDouble(coor_2d.Count - 1));
                    double ocinka_B = koef_par_kor * (Math.Sqrt(y_dispersia) / Math.Sqrt(x_dispersia));
                    double ocinka_A = y_seredne - (ocinka_B * x_seredne);
                    double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                    double N_for_N = Convert.ToDouble(coor_2d.Count);
                    double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                    double sigma_epsilon = Math.Sqrt(y_dispersia) * Math.Sqrt((1.0 - Math.Pow(koef_par_kor, 2.0)) * ((N_for_N - 1.0) / (N_for_N - 2.0)));
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                        chart3.Series[2].Points.AddXY(coor_2d[i].x, y_f9x0 - (sigma_epsilon * kvantil_studen));
                        chart3.Series[3].Points.AddXY(coor_2d[i].x, y_f9x0 + (sigma_epsilon * kvantil_studen));
                    }
                }
                else if (parabolichna_regresia.Checked)
                {
                    double x_kvadrat_seredne = 0;
                    double x_cube_seredne = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        x_kvadrat_seredne += Math.Pow(coor_2d[i].x, 2.0);
                        x_cube_seredne += Math.Pow(coor_2d[i].x, 3.0);
                       
                    }

                    x_cube_seredne /= Convert.ToDouble(coor_2d.Count);
                    x_kvadrat_seredne /= Convert.ToDouble(coor_2d.Count);
                    
                    


                    double N_for_N = Convert.ToDouble(coor_2d.Count);
                  

                    double ocinka_B = 0;
                    double numerator_ocinki_b = 0;
                    double denaminator_icinki_b = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        numerator_ocinki_b += (coor_2d[i].x - x_seredne) * coor_2d[i].y;
                        denaminator_icinki_b += Math.Pow(coor_2d[i].x - x_seredne, 2.0);
                    }
                    ocinka_B = numerator_ocinki_b / denaminator_icinki_b;
                    double ocinka_A = y_seredne;
                    double ocinka_C = 0;
                    double numerator_ocinki_c = 0;
                    double denaminator_icinki_c = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        numerator_ocinki_c += fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne) * coor_2d[i].y;
                        denaminator_icinki_c += Math.Pow(fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne), 2.0);
                    }
                    ocinka_C = numerator_ocinki_c / denaminator_icinki_c;
                    double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                    double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                    double dispersia_zalishkova = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + ((coor_2d[i].x - x_seredne) * ocinka_B) + (ocinka_C * fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne));
                        dispersia_zalishkova += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);
                    }
                    dispersia_zalishkova /= (N_for_N - 3.0);
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + ((coor_2d[i].x - x_seredne) * ocinka_B) + (ocinka_C * fi2_vid_x(coor_2d[i].x, x_cube_seredne, x_kvadrat_seredne, x_dispersia, x_seredne));
                        chart3.Series[2].Points.AddXY(coor_2d[i].x, y_f9x0 - (Math.Sqrt(dispersia_zalishkova) * kvantil_studen));
                        chart3.Series[3].Points.AddXY(coor_2d[i].x, y_f9x0 + (Math.Sqrt(dispersia_zalishkova) * kvantil_studen));

                    }
                }
                else if (kvazi_bez_waga.Checked)
                {
                    List<tochki_2d> coor_2d_zminene = new List<tochki_2d>();
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double novekiy_x = 1.0 / (coor_2d[i].x);
                        double novekiy_y = 1.0 / (coor_2d[i].y);
                        tochki_2d tochki_2 = new tochki_2d();
                        tochki_2.x = novekiy_x;
                        tochki_2.y = novekiy_y;
                        coor_2d_zminene.Add(tochki_2);
                    }
                    double z_seredne = 0;
                    double t_seredne = 0;
                    double t_dispersia = 0;
                    double z_dispersia = 0;
                    double zt_seredne = 0;
                    for (int i = 0; i < coor_2d_zminene.Count; i++)
                    {
                        z_seredne += coor_2d_zminene[i].y;
                        t_seredne += coor_2d_zminene[i].x;
                        zt_seredne += coor_2d_zminene[i].y * coor_2d_zminene[i].x;
                    }
                    zt_seredne /= Convert.ToDouble(coor_2d_zminene.Count);
                    z_seredne /= Convert.ToDouble(coor_2d_zminene.Count);
                    t_seredne /= Convert.ToDouble(coor_2d_zminene.Count);

                    for (int i = 0; i < coor_2d_zminene.Count; i++)
                    {
                        t_dispersia += Math.Pow(coor_2d_zminene[i].x - t_seredne, 2.0);
                        z_dispersia += Math.Pow(coor_2d_zminene[i].y - z_seredne, 2.0);
                    }
                    t_dispersia /= Convert.ToDouble(coor_2d_zminene.Count - 1);
                    z_dispersia /= Convert.ToDouble(coor_2d_zminene.Count - 1);

                    double koef_par_kor = (zt_seredne - (z_seredne * t_seredne)) / (Math.Sqrt(t_dispersia) * Math.Sqrt(z_dispersia));///оцінка парного коефіцієнта кореляції
                    koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d_zminene.Count) / Convert.ToDouble(coor_2d_zminene.Count - 1));
                    double ocinka_B = koef_par_kor * (Math.Sqrt(z_dispersia) / Math.Sqrt(t_dispersia));

                    double ocinka_A = z_seredne - (ocinka_B * t_seredne);

                    double N_for_N = Convert.ToDouble(coor_2d_zminene.Count);

                    double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                    double paramtr_a = ocinka_A / ocinka_B;
                    double parametr_b = 1.0 / ocinka_B;

                    double dispersia_zalishkova_z_ocinkamy = 0;
                    for (int i = 0; i < coor_2d_zminene.Count; i++)
                    {
                        double videmnyk = coor_2d_zminene[i].x * ocinka_B + ocinka_A;
                        dispersia_zalishkova_z_ocinkamy += Math.Pow(coor_2d_zminene[i].y - videmnyk, 2.0);
                    }
                    dispersia_zalishkova_z_ocinkamy /= (N_for_N - 2.0);
                    double dispersia_zalishkova_z_parametramy = 0;
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double d1_ce = (1.0 / coor_2d[i].x) + paramtr_a;
                        double y_f9x0 = parametr_b / d1_ce;
                        dispersia_zalishkova_z_parametramy += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);

                    }
                    dispersia_zalishkova_z_parametramy /= (N_for_N - 2.0);
                    double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                    double S_a = Math.Sqrt(dispersia_zalishkova_z_parametramy) * Math.Sqrt((1.0 / N_for_N) + (Math.Pow(t_seredne, 2.0) / (t_dispersia * (N_for_N - 1.0))));
                    double S_b = Math.Sqrt(dispersia_zalishkova_z_parametramy) / (Math.Sqrt(t_dispersia) * Math.Sqrt(N_for_N - 1.0));
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double d1_ce = (1.0 / coor_2d[i].x) + paramtr_a;
                        double y_f9x0 = parametr_b / d1_ce;
                        double S_y_ser_x = Math.Sqrt(dispersia_zalishkova_z_parametramy);
                        chart3.Series[2].Points.AddXY(coor_2d[i].x,  (y_f9x0 - (S_y_ser_x * kvantil_studen)));
                        chart3.Series[3].Points.AddXY(coor_2d[i].x,  (y_f9x0 + (S_y_ser_x * kvantil_studen)));
                    }

                }
                else if (method_Teyla.Checked)
                {
                    double koef_par_kor = (xy_seredne - (x_seredne * y_seredne)) / (Math.Sqrt(x_dispersia * y_dispersia));///оцінка парного коефіцієнта кореляції
                    koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d.Count) / Convert.ToDouble(coor_2d.Count - 1));
                    double ocinka_B = 0;

                    double ocinka_A = y_seredne;
                    List<double> b_med = new List<double>();
                    List<double> a_med = new List<double>();
                    for (int j = 0; j < coor_2d.Count; j++)
                    {
                        for (int i = 0; i < j - 1; i++)
                        {
                            double u_list = (coor_2d[j].y - coor_2d[i].y) / (coor_2d[j].x - coor_2d[i].x);
                            b_med.Add(u_list);
                        }
                    }
                    b_med.Sort();
                    if (b_med.Count % 2 == 0)
                    {
                        ocinka_B = (b_med[(b_med.Count / 2) - 1] + b_med[(b_med.Count / 2)]) / 2.0;
                    }
                    else
                    {
                        ocinka_B = b_med[(b_med.Count / 2)];
                    }
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double u_a_list = coor_2d[i].y - (ocinka_B * coor_2d[i].x);
                        a_med.Add(u_a_list);
                    }
                    a_med.Sort();
                    if (a_med.Count % 2 == 0)
                    {
                        ocinka_A = (a_med[(a_med.Count / 2) - 1] + a_med[(a_med.Count / 2)]) / 2.0;
                    }
                    else
                    {
                        ocinka_A = a_med[(a_med.Count / 2)];
                    }
                    double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                    double N_for_N = Convert.ToDouble(coor_2d.Count);
                    double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                    double sigma_epsilon = Math.Sqrt(y_dispersia) * Math.Sqrt((1.0 - Math.Pow(koef_par_kor, 2.0)) * ((N_for_N - 1.0) / (N_for_N - 2.0)));
                    for (int i = 0; i < coor_2d.Count; i++)
                    {
                        double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                        chart3.Series[2].Points.AddXY(coor_2d[i].x, y_f9x0 - (sigma_epsilon * kvantil_studen));
                        chart3.Series[3].Points.AddXY(coor_2d[i].x, y_f9x0 + (sigma_epsilon * kvantil_studen));
                    }
                }

            }
        }

        private void Liniyna_regresia_Click(object sender, EventArgs e)
        {
            if (!liniyna_regresia.Checked)
            {
                dovirchi_intevalu_every_regresii.Checked = false;
                tolerantrni_meshi.Checked = false;
                parabolichna_regresia.Checked = false;
                kvaziliniyna_regresia.Checked = false;
                dovirchi_intervaly_for_prognozu.Checked = false;
                method_Teyla.Checked = false;
                kvazi_bez_waga.Checked = false;
                Function_shilnosty.Checked = false;
                liniyna_regresia.Checked = true;
                
            }
            else
            {
                liniyna_regresia.Checked = false;
            }
            
        }

        private void Liniyna_regresia_CheckedChanged(object sender, EventArgs e)
        {
            if (!liniyna_regresia.Checked)//знімаєм значок
            {
                
                chart3.Series[1].Points.Clear();
            }
            else//прикріпили значок
            {
                List<tochki_2d> coor_2d = new List<tochki_2d>();
                for (int i = 0; i < tochki_2Ds.Count; i++)
                {
                    coor_2d.Add(tochki_2Ds[i]);
                }
                
                double y_seredne = 0;
                double x_seredne = 0;
                double x_dispersia = 0;
                double y_dispersia = 0;
                double xy_seredne = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    y_seredne += coor_2d[i].y;
                    x_seredne += coor_2d[i].x;
                    xy_seredne += (coor_2d[i].x * coor_2d[i].y);
                }
                y_seredne /= Convert.ToDouble(coor_2d.Count);
                x_seredne /= Convert.ToDouble(coor_2d.Count);
                xy_seredne /= Convert.ToDouble(coor_2d.Count);
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    x_dispersia += Math.Pow(coor_2d[i].x - x_seredne, 2.0);
                    y_dispersia += Math.Pow(coor_2d[i].y - y_seredne, 2.0);
                }
                x_dispersia /= Convert.ToDouble(coor_2d.Count - 1);
                y_dispersia /= Convert.ToDouble(coor_2d.Count - 1);

                double koef_par_kor = (xy_seredne - (x_seredne * y_seredne)) / (Math.Sqrt(x_dispersia)* Math.Sqrt(y_dispersia));///оцінка парного коефіцієнта кореляції
                koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d.Count) / Convert.ToDouble(coor_2d.Count - 1));
                double ocinka_B = koef_par_kor * (Math.Sqrt(y_dispersia) / Math.Sqrt(x_dispersia));
                double ocinka_A = y_seredne - (ocinka_B * x_seredne);
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                    chart3.Series[1].Points.AddXY(coor_2d[i].x,y_f9x0);
                }
                dataGridView10.Rows.Clear();
                dataGridView10.Columns.Clear();
                dataGridView10.AllowUserToAddRows = false;
                dataGridView10.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView10.Columns.Add("1", "Характеристика");
                dataGridView10.Columns.Add("2", "INF");
                dataGridView10.Columns.Add("3", "Значення");
                dataGridView10.Columns.Add("4", "SUP");
                double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
                double N_for_N = Convert.ToDouble(coor_2d.Count);
                double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_for_N - 2.0);
                dataGridView10.Rows.Add("Оцінка парметра а","",Math.Round(ocinka_A,4),"");
                dataGridView10.Rows.Add("Оцінка парметра b", "",Math.Round(ocinka_B,4), "");
                double sigma_epsilon = Math.Sqrt(y_dispersia) * Math.Sqrt((1.0 - Math.Pow(koef_par_kor, 2.0)) * ((N_for_N - 1.0) / (N_for_N - 2.0)));
                double dispersia_zalishkova = 0;
                for (int i = 0; i < coor_2d.Count; i++)
                {
                    double y_f9x0 = ocinka_A + (coor_2d[i].x * ocinka_B);
                    dispersia_zalishkova += Math.Pow(coor_2d[i].y - y_f9x0, 2.0);
                }
                dispersia_zalishkova /= (N_for_N - 2.0);
                double S_a = Math.Sqrt(dispersia_zalishkova) * Math.Sqrt((1.0 / N_for_N) + (Math.Pow(x_seredne, 2.0) / (x_dispersia *(N_for_N- 1.0))));
                double S_b = Math.Sqrt(dispersia_zalishkova) / (Math.Sqrt(x_dispersia) * Math.Sqrt(N_for_N - 1.0));
                dataGridView10.Rows.Add("Залишкова дисперсія S","",Math.Round(dispersia_zalishkova,4),"");
                dataGridView10.Rows.Add("σ оцінка \n параметра а", "", Math.Round(S_a, 4), "");
                dataGridView10.Rows.Add("σ оцінка \n параметра b", "", Math.Round(S_b, 4), "");
                dataGridView10.Rows.Add("Інтервальне оцінювання параметрів", Math.Round(ocinka_A - (kvantil_studen * S_a), 4), "a", Math.Round(ocinka_A + (kvantil_studen * S_a), 4));
                dataGridView10.Rows.Add("Інтервальне оцінювання параметрів", Math.Round(ocinka_B - (kvantil_studen * S_b), 4), "b", Math.Round(ocinka_B + (kvantil_studen * S_b), 4));

                double koef_determinaz = Math.Pow(koef_par_kor,2.0);
                dataGridView10.Rows.Add("Коефіцієнт детермінації", "", $"{Math.Round(koef_determinaz * 100.0, 4)}%", "");
                
                if (coor_2d.Count <= 100)
                {
                    kilkclass = (int)Math.Sqrt(coor_2d.Count);
                }
                else
                {
                    kilkclass = (int)Math.Pow(coor_2d.Count, 0.3333333);
                }
                double sered = coor_2d[coor_2d.Count - 1].x - coor_2d[0].x;
                double shah = sered / Convert.ToDouble(kilkclass);                
                double shaht = shah;            
                shah += coor_2d[0].x;                
                double sum = coor_2d[0].x;
                
                int chastoty = 0;
                List<int> kil_tochek_v_promishku = new List<int>();
                List<double> dispersia_koshnoho_promishka = new List<double>();
                
                for (int i = 0; i < kilkclass; i++)
                {

                    chastoty = 0;
                    double y_v_vibirci = 0;
                    double diaper_v_promishlu = 0;
                    for (int p = 0; p < coor_2d.Count; p++)
                    {
                        if (coor_2d[p].x <= shah && coor_2d[p].x >= sum)
                        {
                            chastoty += 1;
                            y_v_vibirci += coor_2d[p].y;
                        }
                    }
                    kil_tochek_v_promishku.Add(chastoty);

                    double y_ser = y_v_vibirci / Convert.ToDouble(chastoty);

                    for (int p = 0; p < coor_2d.Count; p++)
                    {
                        if (coor_2d[p].x <= shah && coor_2d[p].x >= sum)
                        {
                            diaper_v_promishlu += Math.Pow(coor_2d[p].y - y_ser, 2.0);
                        }
                    }
                    diaper_v_promishlu/=Convert.ToDouble(chastoty-1);
                    dispersia_koshnoho_promishka.Add(diaper_v_promishlu);
                    sum += shaht;
                    shah += shaht;


                }
                double c_statistika_Lambda = 0;
                for (int i = 0; i < kil_tochek_v_promishku.Count; i++)
                {
                    c_statistika_Lambda += Convert.ToDouble(1.0 / kil_tochek_v_promishku[i]);
                }
                c_statistika_Lambda -= (1.0 / N_for_N);
                c_statistika_Lambda = c_statistika_Lambda * (1.0 / (3.0 * Convert.ToDouble(kilkclass - 1)))+1.0;
                double lambda_s_kvadrat = 0;
                for (int i = 0; i < dispersia_koshnoho_promishka.Count; i++)
                {
                    lambda_s_kvadrat += Convert.ToDouble(kil_tochek_v_promishku[i] - 1) * dispersia_koshnoho_promishka[i];

                }
                lambda_s_kvadrat /= (N_for_N - Convert.ToDouble(kilkclass));
                double Statistica_Lambda = 0;
                for (int i = 0; i < dispersia_koshnoho_promishka.Count; i++)
                {
                    Statistica_Lambda += Convert.ToDouble(kil_tochek_v_promishku[i]) * Math.Log(dispersia_koshnoho_promishka[i] / lambda_s_kvadrat);
                }
                Statistica_Lambda = Statistica_Lambda * (-1.0 / c_statistika_Lambda);
                richTextBox3.Text = "Лінійна регресія \n";
                richTextBox3.Text += $"y=({Math.Round(ocinka_A,4)})+({Math.Round(ocinka_B,4)})*x\n";
                double kvanta_pirsona = kvantil_Pirsona(kvantil_start_rozpodil, Convert.ToDouble(kilkclass - 1.0));
               // richTextBox3.Text += $"Λ={Math.Round(Statistica_Lambda,4)}";
                if (Statistica_Lambda > kvanta_pirsona)
                {
                    richTextBox3.Text += $"Λ={Math.Round(Statistica_Lambda, 4)} Λ>{Math.Round(kvanta_pirsona, 4)} дисперсія y не є стала";
                }
                else
                {
                    richTextBox3.Text += $"Λ={Math.Round(Statistica_Lambda, 4)} Λ<={Math.Round(kvanta_pirsona, 4)} дисперсія y стала\n";
                }
                double t_test_ocinki_A = ocinka_A / S_a;
                double t_test_ocinki_B= ocinka_B / S_b;
                if (Math.Abs(t_test_ocinki_A) > kvantil_studen)
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_A), 4)} t>{Math.Round(kvantil_studen, 4)} оцінка параметра а є значуща\n";
                }
                else
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_A), 4)} t<={Math.Round(kvantil_studen, 4)} оцінка параметра а не є значуща\n";
                }
                if (Math.Abs(t_test_ocinki_B) > kvantil_studen)
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_B), 4)} |t|>{Math.Round(kvantil_studen, 4)} оцінка параметра b є значуща\n";
                }
                else
                {
                    richTextBox3.Text += $"|t|={Math.Round(Math.Abs(t_test_ocinki_B), 4)} |t|<={Math.Round(kvantil_studen, 4)} оцінка параметра b не є значуща\n";
                }
                double fishera_roazpodil = kvantil_Fishera(kvantil_start_rozpodil, N_for_N - 1.0, N_for_N - 3.0);
                double f_statistika=dispersia_zalishkova/y_dispersia;
                if (f_statistika > fishera_roazpodil)
                {
                    richTextBox3.Text += $"f={Math.Round(f_statistika, 4)} f>{Math.Round(fishera_roazpodil, 4)} відтворена лінійна модель регресії не є адекватна\n";
                }
                else
                {
                    richTextBox3.Text += $"f={Math.Round(f_statistika, 4)} f<={Math.Round(fishera_roazpodil, 4)} відтворена лінійна модель регресії є адекватна\n";
                }

            }
           
            
        } 

        void buhalter(List<double> origin)
        {
            List<rows> a4 = Program.Variaciyniy_ryad(origin);
            panel1.BackColor = Color.Red;

            chart1.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            chart1.Series[0].IsVisibleInLegend = false;
            chart1.Series[1].IsVisibleInLegend = false;
            chart1.Series[2].IsVisibleInLegend = false;
            chart1.Series[3].IsVisibleInLegend = false;

            chart2.Series[0].IsVisibleInLegend = false;
            chart2.Series[1].IsVisibleInLegend = false;
            chart2.Series[2].IsVisibleInLegend = false;
            chart2.Series[3].IsVisibleInLegend = false;
            chart2.Series[4].IsVisibleInLegend = false;



            double cifr;

            dataBANK.not_sort_log_list.Clear();
            dataBANK.not_sort_standart_list.Clear();
            dataBANK.corTX.Clear();
            dataBANK.Chast.Clear();
            dataBANK.AnomalChast.Clear();
            dataBANK.AnolomalLOGfunc.Clear();
            dataBANK.AnomalcorTX.Clear();
            dataBANK.AnomalSTandfunc.Clear();
            dataBANK.standfunc.Clear();
            dataBANK.uolsh.Clear();
            dataBANK.MAD.Clear();
            dataBANK.logfunc.Clear();

            for (int i = 0; i < origin.Count; i++)
            {

                cifr = origin[i];
                dataBANK.corTX.Add(cifr);
                dataBANK.AnomalcorTX.Add(cifr);
                dataBANK.OriginalData.Add(cifr);

            }

            double disper = 0;
            richTextBox1.Text = "";
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataBANK.corTX.Sort();
            dataBANK.AnomalcorTX.Sort();
            int nBig = origin.Count;

            double nsmal = 0;
            double chast = 0;

            dataGridView1.Columns.Add("Xval", "Варіанти");
            dataGridView1.Columns.Add("funVAL", "Частота");
            dataGridView1.Columns.Add("f123", "Відносначастота");

            richTextBox1.Visible = false;
            dataGridView2.Columns.Add("1", "Характеристика");
            dataGridView2.Columns.Add("2", "INF");
            dataGridView2.Columns.Add("3", "Значення");
            dataGridView2.Columns.Add("4", "SUP");
            dataGridView2.Columns.Add("5", "SKV");

            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {

                double comp = dataBANK.corTX[i];
                nsmal = Program.ransom(dataBANK.corTX, comp);

                chast = nsmal / nBig;
                dataBANK.AnomalChast.Add(chast);
                dataBANK.Chast.Add(chast);

            }

            for (int i = 0; i < a4.Count; i++)//варіаційний ряд
            {

                dataGridView1.Rows.Add(a4[i].variant, a4[i].count, a4[i].chast);//варіаційний ряд
            }//варіаційний ряд
            nsmal = 0;
            double nsmal1 = 0;
            double xi = 0;
            double xj = 0;
            for (int i = 0; i < a4.Count; i++)
            {
                nsmal += dataBANK.corTX[i];
                nsmal1 += Math.Pow(dataBANK.corTX[i], 2);

                xi = dataBANK.corTX[i];
                for (int j = 0; j < dataBANK.corTX.Count; j++)
                {
                    xj = dataBANK.corTX[j];
                    double res = (xi + xj) / 2;
                    dataBANK.uolsh.Add(res);
                }

            }
            dataBANK.uolsh.Sort();
            double medUolsh = 0;
            if (dataBANK.uolsh.Count % 2 == 0)
            {
                medUolsh = (dataBANK.uolsh[(dataBANK.uolsh.Count / 2) - 1] + dataBANK.uolsh[dataBANK.uolsh.Count / 2]) / 2;
            }
            else
            {
                medUolsh = dataBANK.uolsh[(dataBANK.uolsh.Count / 2)];
            }
            double mediana = 0;
            if (dataBANK.corTX.Count % 2 == 0)
            {
                mediana = (dataBANK.corTX[(dataBANK.corTX.Count / 2) - 1] + dataBANK.corTX[(dataBANK.corTX.Count / 2)]) / 2;
            }
            else
            {
                mediana = dataBANK.corTX[(dataBANK.corTX.Count / 2)];
            }

            double kkk;
            if (Double.TryParse(textBox2.Text, out double kkk2))
            {
                if (kkk2 <= 0.5 && kkk2 >= 0)
                {
                    kkk = kkk2;
                }
                else
                {
                    kkk = 0.2;
                }

            }
            else
            {
                kkk = 0.2;
            }
            int Kusichmed = (int)Math.Truncate(dataBANK.corTX.Count * kkk);
            double kofusichmed = dataBANK.corTX.Count - (2 * Kusichmed);
            double usichsered = 0;
            for (int i = Kusichmed + 1; i < dataBANK.corTX.Count - Kusichmed; i++)
            {
                usichsered += (1 / kofusichmed) * dataBANK.corTX[i];
            }
            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                double x1l = dataBANK.corTX[i];
                double resar = Math.Abs(x1l - mediana);
                dataBANK.MAD.Add(resar);
            }



            double MAD = 0;
            if (dataBANK.MAD.Count % 2 == 0)
            {
                MAD = (dataBANK.MAD[(dataBANK.MAD.Count / 2) - 1] + dataBANK.MAD[dataBANK.MAD.Count / 2]) / 2;
            }
            else
            {
                MAD = dataBANK.MAD[dataBANK.MAD.Count / 2];
            }
            
            MAD = MAD * 1.483;
            matspod = 0;
            for (int i = 0; i < a4.Count; i++)
            {
                matspod += a4[i].chast * a4[i].variant;
            }//середнє
            for (int i = 0; i < a4.Count; i++)
            {
                disper += Math.Pow(a4[i].variant, 2) * a4[i].chast;
            }//дисперсія зсунена
            disper = disper - Math.Pow(matspod, 2);
            double disper2 = 0;
            for (int i = 0; i < a4.Count; i++)
            {
                disper2 += (Math.Pow(a4[i].variant - matspod, 2) * a4[i].count) / (nBig - 1);
            }//дисперсія незсунена

            kwadratvid = Math.Sqrt(disper2);

            double kofasimetry = 0;
            kofAsimeNoMove = 0;
            for (int i = 0; i < a4.Count; i++)
            {
                kofasimetry += (1 / Math.Pow(kwadratvid, 3)) * Math.Pow(a4[i].variant - matspod, 3) * a4[i].chast;
            }
            kofAsimeNoMove = kofasimetry * Math.Sqrt(nBig * (nBig - 1)) / (nBig - 2);

            double kofekses = 0;
            for (int i = 0; i < a4.Count; i++)
            {
                kofekses += (1 / Math.Pow(disper, 2)) * Math.Pow(a4[i].variant - matspod, 4) * a4[i].chast;
            }

            kofekses2 = ((Math.Pow(nBig, 2) - 1) / ((nBig - 2) * (nBig - 3))) * ((kofekses - 3) + (6 / (nBig + 1)));

            double kofkontrekses = (1 / Math.Sqrt(Math.Abs(kofekses2)));
            double kofPirsona = Math.Sqrt(disper2) / matspod;
            double noparKofVariac = MAD / mediana;


            bool minval = false;

            double comp1;
            double stand1;
            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                comp1 = dataBANK.corTX[i];
                stand1 = (comp1 - matspod) / kwadratvid;
                dataBANK.standfunc.Add(stand1);
                dataBANK.AnomalSTandfunc.Add(stand1);
            }
            for (int i = 0; i < origin.Count; i++)
            {
                comp1 = origin[i];
                stand1 = (comp1 - matspod) / kwadratvid;
                dataBANK.not_sort_standart_list.Add(stand1);
            }

            /// стандартизація та логарифмування
            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                comp1 = dataBANK.corTX[i];
                if (dataBANK.corTX[0] <= 0.1)
                {
                    minval = true;
                    comp1 += Math.Abs(dataBANK.corTX[0]) + 0.01;
                    stand1 = Math.Log(Math.Abs(comp1));
                    dataBANK.logfunc.Add(stand1);
                    dataBANK.AnolomalLOGfunc.Add(stand1);
                }
                else if (minval)
                {
                    comp1 += Math.Abs(dataBANK.corTX[0]) + 1;
                    stand1 = Math.Log(Math.Abs(comp1));
                    dataBANK.logfunc.Add(stand1);
                    dataBANK.AnolomalLOGfunc.Add(stand1);
                }
                else
                {
                    stand1 = Math.Log(Math.Abs(comp1));
                    dataBANK.logfunc.Add(Math.Round(stand1, 5));
                    dataBANK.AnolomalLOGfunc.Add(stand1);
                }
            }
            for (int i = 0; i < origin.Count; i++)
            {
                comp1 = origin[i];
                if (dataBANK.corTX[0] <= 0.1)
                {
                    minval = true;
                    comp1 += Math.Abs(dataBANK.corTX[0]) + 0.01;
                    stand1 = Math.Log(Math.Abs(comp1));
                    dataBANK.not_sort_log_list.Add(stand1);
                }
                else if (minval)
                {
                    comp1 += Math.Abs(dataBANK.corTX[0]) + 1;
                    stand1 = Math.Log(Math.Abs(comp1));
                    dataBANK.not_sort_log_list.Add(stand1);
                }
                else
                {
                    stand1 = Math.Log(Math.Abs(comp1));
                    dataBANK.not_sort_log_list.Add(stand1);
                }
            }

            // ДОВІРЧІІНТЕРВАЛИ

            double Qmatspod = kwadratvid / Math.Sqrt(nBig);
            double Qkwadvid = kwadratvid / Math.Sqrt(2 * nBig);
            double Qpirsona = kofPirsona * Math.Sqrt((1 + 2 * Math.Pow(kofPirsona, 2)) / (2 * nBig));
            double Qasimetry = Math.Sqrt(((double)6 * (nBig - 2)) / ((nBig + 1) * (nBig + 3)));

            double t24 = 24;
            double t1 = 1;
            double t225 = 225;
            double t15 = 15;
            double t124 = 124;
            double Qekses = (double)Math.Sqrt((t24 / nBig) * (t1 - (t225 / (t15 * nBig + t124))));


            double KVANTIL;
            if (Double.TryParse(textBox3.Text, out double KW2))
            {
                KVANTIL = KW2;
            }
            else
            {
                KVANTIL = 1.96;
            }
            double Qdisper = (2 * Math.Pow(disper2, 2)) / (nBig - 1);
            double Qkontrekses = Math.Sqrt(Math.Abs(kofekses) / ((double)29 * nBig)) * Math.Pow(Math.Abs(Math.Pow(kofekses, 2) - (double)1), 0.25);



            dataGridView2.Rows.Add("Середнє", Math.Round(matspod - (KVANTIL * Qmatspod), 5), Math.Round(matspod, 5), Math.Round(matspod + (KVANTIL * Qmatspod), 5), Math.Round(Qmatspod, 5));
            dataGridView2.Rows.Add("Дисперсія ", Math.Round(disper2 - (KVANTIL * Qdisper), 5), Math.Round(disper2, 5), Math.Round(disper2 + (KVANTIL * Qdisper), 5), Math.Round(Qdisper, 5));

            dataGridView2.Rows.Add("MED", 0, Math.Round(mediana, 5), 0);
            dataGridView2.Rows.Add("MAD", 0, Math.Round(MAD, 5), 0);
            dataGridView2.Rows.Add("Усіченесереднє", 0, Math.Round(usichsered, 5), 0);
            dataGridView2.Rows.Add("MED Уолша", 0, Math.Round(medUolsh, 5), 0);
            dataGridView2.Rows.Add("Cер. квадратичне", Math.Round(kwadratvid - (Qkwadvid * KVANTIL), 5), Math.Round(kwadratvid, 5), Math.Round(kwadratvid + (Qkwadvid * KVANTIL), 5), Math.Round(Qkwadvid, 5));
            dataGridView2.Rows.Add("Aсиметрія", Math.Round(kofAsimeNoMove - (Qasimetry * KVANTIL), 5), Math.Round(kofAsimeNoMove, 5), Math.Round(kofAsimeNoMove + (Qasimetry * KVANTIL), 5), Math.Round(Qasimetry, 5));
            dataGridView2.Rows.Add("Контрексцес", Math.Round(kofkontrekses - (Qkontrekses * KVANTIL), 5), Math.Round(kofkontrekses, 5), Math.Round(kofkontrekses + (Qkontrekses * KVANTIL), 5), Math.Round(Qkontrekses, 5));
            dataGridView2.Rows.Add("Ексцес", Math.Round(kofekses2 - (Qekses * KVANTIL), 5), Math.Round(kofekses2, 5), Math.Round(kofekses2 + (Qekses * KVANTIL), 5), Math.Round(Qekses, 5));
            dataGridView2.Rows.Add("Коф. Пірсона", Math.Round(kofPirsona - (Qpirsona * KVANTIL), 5), Math.Round(kofPirsona, 5), Math.Round(kofPirsona + (Qpirsona * KVANTIL), 5), Math.Round(Qpirsona, 5));


            dataGridView2.Rows.Add("Непараметричнийкоф. варіації", 0, Math.Round(noparKofVariac, 5), 0);
            double peredbachenya = kwadratvid * Math.Sqrt((double)1 + ((double)1 / nBig));
            dataGridView2.Rows.Add("Інт. передбачення", Math.Round(matspod - (KVANTIL * peredbachenya), 5), "X нове", Math.Round(matspod + (KVANTIL * peredbachenya), 5));


            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart1.Series[0].Points.Clear();
            chart1.ChartAreas[0].AxisX.Title = "Значення";
            chart1.ChartAreas[0].AxisY.Title = "Відносначастота";
            kilkclass = (int)numericUpDown1.Value;

            if (numericUpDown1.Value == 0)
            {
                if (a4.Count <= 100)
                {
                    kilkclass = (int)Math.Sqrt(a4.Count);
                }
                else
                {
                    kilkclass = (int)Math.Pow(a4.Count, 0.3333333);
                }
            }
            else
            {
                kilkclass = (int)numericUpDown1.Value;
            }
            double sered = a4[a4.Count - 1].variant - a4[0].variant;

            double shah = sered / kilkclass;

            double shaht = shah;
            shah += a4[0].variant;

            double sum = 0;
            double t = 0;

            int chislo = 0;
            int otchet = 0;
            int end = 0;
            double sum1 = 0;
            for (int i = 0; i < a4.Count; i++)
            {
                //MessageBox.Show($"{i}");

                if (a4[i].variant <= shah + t)
                {
                    sum += a4[i].chast;
                    sum1 += a4[i].chast;
                    end = i;
                }
                else
                {
                    otchet = i;
                    sum = a4[i].chast;
                    sum1 += a4[i].chast;
                    chart2.Series[1].Points.AddXY(a4[chislo].variant, sum1);
                    chart2.Series[1].Points.AddXY(a4[otchet].variant, sum1);
                    chislo = i;
                    t += shaht;
                }
                if (a4.Count - 1 == i)
                {
                    otchet = i;
                    chart2.Series[1].Points.AddXY(a4[chislo].variant, sum1);
                    chart2.Series[1].Points.AddXY(a4[otchet].variant, sum1);

                }

            }
            sum = 0;
            sum1 = 0;
            for (int i = 0; i < a4.Count; i++)
            {

                if (a4[i].variant <= shah)
                {
                    sum += a4[i].chast;
                    sum1 += a4[i].chast;
                    end = i;
                    if (a4.Count - 1 == i)
                    {
                        otchet = i;

                        chart1.Series[0].Points.AddXY(shah - shaht, 0);
                        chart1.Series[0].Points.AddXY(shah - shaht, sum);
                        chart1.Series[0].Points.AddXY(shah, sum);
                        chart1.Series[0].Points.AddXY(shah, 0);

                    }
                }
                else
                {

                    otchet = i;

                    chart1.Series[0].Points.AddXY(shah - shaht, 0);
                    chart1.Series[0].Points.AddXY(shah - shaht, sum);
                    chart1.Series[0].Points.AddXY(shah, sum);
                    chart1.Series[0].Points.AddXY(shah, 0);
                    shah += shaht;
                    sum = a4[i].chast;
                    sum1 += a4[i].chast;
                    chislo = i;
                    t += shaht;
                }

            }
          
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0.000";
            chart1.ChartAreas[0].AxisX.Maximum = a4[a4.Count - 1].variant;
            chart1.ChartAreas[0].AxisX.Minimum = a4[0].variant;
            //chart1.Series[0].Points.AddXY(dataBANK.corTX[dataBANK.corTX.Count - 1], sum);
            chart1.Series[0].ChartType = SeriesChartType.Range;

            sum = 0;
            for (int i = 0; i < a4.Count; i++)
            {
                sum += a4[i].chast;
                chart2.Series[0].Points.AddXY(a4[i].variant, sum);
            }
            sum = 0;



        }
        private void Anomalfunc_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            StandFunc.Checked = false;
            LogFunc.Checked = false;


            if (StandFunc.Checked == true)
            {
                double a;
                double b;
                double t1 = 2 + 0.2 * Math.Log10(0.04 + dataBANK.corTX.Count);
                double t2 = Math.Pow(19 * Math.Pow(kofekses2 + 2, 0.5) + 1, 0.5);
                if (kofAsimeNoMove < -0.2)
                {
                    a = matspod - (t2 * kwadratvid);
                    b = matspod + (t1 * kwadratvid);
                }
                else if (kofAsimeNoMove > 0.2)
                {
                    a = matspod - (t1 * kwadratvid);
                    b = matspod + (t2 * kwadratvid);
                }
                else if (Math.Abs(kofAsimeNoMove) <= 0.2)
                {
                    a = matspod - (t1 * kwadratvid);
                    b = matspod + (t1 * kwadratvid);
                }
            }
            else if (LogFunc.Checked == true)
            {
                double a;
                double b;
                double t1 = 2 + 0.2 * Math.Log10(0.04 + dataBANK.corTX.Count);
                double t2 = Math.Pow(19 * Math.Pow(kofekses2 + 2, 0.5) + 1, 0.5);
                if (kofAsimeNoMove < -0.2)
                {
                    a = matspod - (t2 * kwadratvid);
                    b = matspod + (t1 * kwadratvid);
                }
                else if (kofAsimeNoMove > 0.2)
                {
                    a = matspod - (t1 * kwadratvid);
                    b = matspod + (t2 * kwadratvid);
                }
                else if (Math.Abs(kofAsimeNoMove) <= 0.2)
                {
                    a = matspod - (t1 * kwadratvid);
                    b = matspod + (t1 * kwadratvid);
                }
            }
            else
            {
            startStandart:
                richTextBox1.Text = "";
                double disper = 0;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
                dataBANK.AnomalChast.Clear();

                dataBANK.AnolomalLOGfunc.Clear();
                //dataBANK.AnomalcorTX.Clear();
                dataBANK.AnomalSTandfunc.Clear();
                dataBANK.standfunc.Clear();
                dataBANK.uolsh.Clear();
                dataBANK.MAD.Clear();
                dataBANK.logfunc.Clear();
                //dataBANK.AnomalcorTX.Sort();

                dataBANK.AnomalcorTX.Sort();
                int nBig = dataBANK.AnomalcorTX.Count;

                double nsmal = 0;
                double chast = 0;

                dataGridView1.Columns.Add("Xval", "Варіанти");
                dataGridView1.Columns.Add("funVAL", "Частота");
                dataGridView1.Columns.Add("f123", "Відносначастота");

                dataGridView2.Columns.Add("1", "Характеристика");
                dataGridView2.Columns.Add("2", "INF");
                dataGridView2.Columns.Add("3", "Значення");
                dataGridView2.Columns.Add("4", "SUP");
                dataGridView2.Columns.Add("5", "SKV");


                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {

                    double comp = dataBANK.AnomalcorTX[i];
                    nsmal = Program.ransom(dataBANK.AnomalcorTX, comp);

                    chast = nsmal / nBig;
                    //dataBANK.Chast.Add(chast);

                    dataBANK.AnomalChast.Add(chast);


                }

                List<rows> variant = new List<rows>();
                variant.Clear();
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    rows rows1 = new rows();
                    double comp = dataBANK.AnomalcorTX[i];
                    chast = 1.0 / nBig;
                    rows1.chast = chast;
                    int KKK = (int)variant.Count;
                    rows1.variant = comp;
                    rows1.count = 1;
                    if (KKK == 0)
                    {
                        variant.Add(rows1);
                    }
                    else
                    {
                        for (int G = 0; G < KKK; G++)
                        {
                            if (variant[G].variant == comp)
                            {
                                variant[G].chast += chast;
                                variant[G].count += 1;
                            }
                            else if ((G == KKK - 1) && variant[G].variant != nsmal)
                            {
                                variant.Add(rows1);
                                break;
                            }
                        }
                    }
                }

                for (int i = 0; i < variant.Count; i++)
                {

                    dataGridView1.Rows.Add(variant[i].variant, variant[i].count, variant[i].chast);
                }
                nsmal = 0;
                double nsmal1 = 0;
                double xi = 0;
                double xj = 0;
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    nsmal += dataBANK.AnomalcorTX[i];
                    nsmal1 += Math.Pow(dataBANK.AnomalcorTX[i], 2);

                    xi = dataBANK.AnomalcorTX[i];
                    for (int j = 0; j < dataBANK.AnomalcorTX.Count; j++)
                    {
                        xj = dataBANK.AnomalcorTX[j];
                        double res = (xi + xj) / 2;
                        dataBANK.uolsh.Add(res);
                    }

                }
                dataBANK.uolsh.Sort();
                double medUolsh = 0;
                if (dataBANK.uolsh.Count % 2 == 0)
                {
                    medUolsh = (dataBANK.uolsh[(dataBANK.uolsh.Count / 2) - 1] + dataBANK.uolsh[dataBANK.uolsh.Count / 2]) / 2;
                }
                else
                {
                    medUolsh = dataBANK.uolsh[(dataBANK.uolsh.Count / 2)];
                }
                double mediana = 0;
                if (dataBANK.AnomalcorTX.Count % 2 == 0)
                {
                    mediana = (dataBANK.AnomalcorTX[(dataBANK.AnomalcorTX.Count / 2) - 1] + dataBANK.AnomalcorTX[(dataBANK.AnomalcorTX.Count / 2)]) / 2;
                }
                else
                {
                    mediana = dataBANK.AnomalcorTX[(dataBANK.AnomalcorTX.Count / 2)];
                }
                double kkk;
                if (Double.TryParse(textBox2.Text, out double kkk2))
                {
                    if (kkk2 <= 0.5 && kkk2 >= 0)
                    {
                        kkk = kkk2;
                    }
                    else
                    {
                        kkk = 0.2;
                    }
                }
                else
                {
                    kkk = 0.2;
                }
                int Kusichmed = (int)Math.Truncate(dataBANK.AnomalcorTX.Count * kkk);
                double kofusichmed = dataBANK.AnomalcorTX.Count - (2 * Kusichmed);
                double usichsered = 0;
                for (int i = Kusichmed + 1; i < dataBANK.AnomalcorTX.Count - Kusichmed; i++)
                {
                    usichsered += (1 / kofusichmed) * dataBANK.AnomalcorTX[i];
                }
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    double x1l = dataBANK.AnomalcorTX[i];
                    double resar = Math.Abs(x1l - mediana);
                    dataBANK.MAD.Add(resar);
                }



                double MAD = 0;
                if (dataBANK.MAD.Count % 2 == 0)
                {
                    MAD = (dataBANK.MAD[(dataBANK.MAD.Count / 2) - 1] + dataBANK.MAD[dataBANK.MAD.Count / 2]) / 2;
                }
                else
                {
                    MAD = dataBANK.MAD[dataBANK.MAD.Count / 2];
                }

                MAD = MAD * 1.483;
                matspod = nsmal / nBig;

                for (int i = 0; i < dataBANK.corTX.Count; i++)
                {
                    disper += Math.Pow(dataBANK.corTX[i], 2) / (nBig);
                }
                disper = disper - Math.Pow(matspod, 2);
                double disper2 = 0;
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    disper2 += Math.Pow(dataBANK.AnomalcorTX[i] - matspod, 2) / (nBig - 1);
                }

                kwadratvid = Math.Sqrt(disper2);

                double kofasimetry = 0;
                kofAsimeNoMove = 0;
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    kofasimetry += (1 / Math.Pow(kwadratvid, 3)) * Math.Pow(dataBANK.AnomalcorTX[i] - matspod, 3) * dataBANK.AnomalChast[i];
                }
                kofAsimeNoMove = kofasimetry * Math.Sqrt(nBig * (nBig - 1)) / (nBig - 2);

                double kofekses = 0;
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    kofekses += (1 / Math.Pow(kwadratvid, 4)) * Math.Pow(dataBANK.AnomalcorTX[i] - matspod, 4) * dataBANK.AnomalChast[i];
                }

                kofekses2 = ((Math.Pow(nBig, 2) - 1) / ((nBig - 2) * (nBig - 3))) * ((kofekses - 3) + (6 / (nBig + 1)));
                double kofkontrekses = (1 / Math.Sqrt(Math.Abs(kofekses2)));
                double noparKofVariac = MAD / mediana;
                double kofPirsona = Math.Sqrt(disper2) / matspod;


                bool minval = false;

                double comp1;
                double stand1;
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    comp1 = dataBANK.AnomalcorTX[i];
                    stand1 = (comp1 - matspod) / kwadratvid;
                    dataBANK.standfunc.Add(Math.Round(stand1, 5));
                    dataBANK.AnomalSTandfunc.Add(Math.Round(stand1, 5));
                }

                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {

                    comp1 = dataBANK.AnomalcorTX[i];


                    if (dataBANK.AnomalcorTX[0] <= 0.1)
                    {
                        minval = true;
                        comp1 += Math.Abs(dataBANK.AnomalcorTX[0]) + 0.01;
                        stand1 = Math.Log(Math.Abs(comp1));
                        dataBANK.logfunc.Add(Math.Round(stand1, 5));
                        dataBANK.AnolomalLOGfunc.Add(Math.Round(stand1, 5));
                    }
                    else if (minval)
                    {
                        comp1 += Math.Abs(dataBANK.AnomalcorTX[0]) + 0.01;
                        stand1 = Math.Log(Math.Abs(comp1));
                        dataBANK.logfunc.Add(Math.Round(stand1, 5));
                        dataBANK.AnolomalLOGfunc.Add(Math.Round(stand1, 5));
                    }
                    else
                    {
                        stand1 = Math.Log(Math.Abs(comp1));
                        dataBANK.logfunc.Add(Math.Round(stand1, 5));
                        dataBANK.AnolomalLOGfunc.Add(Math.Round(stand1, 5));
                    }



                }

                // ДОВІРЧІІНТЕРВАЛИ

                double Qmatspod = kwadratvid / Math.Sqrt(nBig);
                double Qkwadvid = kwadratvid / Math.Sqrt(2 * nBig);
                double Qpirsona = kofPirsona * Math.Sqrt((1 + 2 * Math.Pow(kofPirsona, 2)) / (2 * nBig));
                double Qasimetry = Math.Sqrt(((double)6 * (nBig - 2)) / ((nBig + 1) * (nBig + 3)));


                double t24 = 24;
                double t11 = 1;
                double t225 = 225;
                double t15 = 15;
                double t124 = 124;
                double Qekses = (double)Math.Sqrt((t24 / nBig) * (t11 - (t225 / (t15 * nBig + t124))));
                double KVANTIL;
                if (Double.TryParse(textBox3.Text, out double KW2))
                {
                    KVANTIL = KW2;
                }
                else
                {
                    KVANTIL = 1.96;
                }
                double Qdisper = (2 * Math.Pow(disper2, 2)) / (nBig - 1);
                double Qkontrekses = Math.Sqrt(Math.Abs(kofekses) / ((double)29 * nBig)) * Math.Pow(Math.Abs(Math.Pow(kofekses, 2) - (double)1), 0.25);

                dataGridView2.Rows.Add("Середнє", Math.Round(matspod - (KVANTIL * Qmatspod), 5), Math.Round(matspod, 5), Math.Round(matspod + (KVANTIL * Qmatspod), 5), Math.Round(Qmatspod, 5));
                dataGridView2.Rows.Add("Дисперсія ", Math.Round(disper2 - (KVANTIL * Qdisper), 5), Math.Round(disper2, 5), Math.Round(disper2 + (KVANTIL * Qdisper), 5), Math.Round(Qdisper, 5));


                dataGridView2.Rows.Add("MED", 0, Math.Round(mediana, 5), 0);
                dataGridView2.Rows.Add("MAD", 0, Math.Round(MAD, 5), 0);
                dataGridView2.Rows.Add("Усіченесереднє", 0, Math.Round(usichsered, 5), 0);
                dataGridView2.Rows.Add("MED Уолша", 0, Math.Round(medUolsh, 5), 0);

                dataGridView2.Rows.Add("Cер. квадратичне", Math.Round(kwadratvid - (Qkwadvid * KVANTIL), 5), Math.Round(kwadratvid, 5), Math.Round(kwadratvid + (Qkwadvid * KVANTIL), 5), Math.Round(Qkwadvid, 5));
                dataGridView2.Rows.Add("Aсиметрія", Math.Round(kofAsimeNoMove - (Qasimetry * KVANTIL), 5), Math.Round(kofAsimeNoMove, 5), Math.Round(kofAsimeNoMove + (Qasimetry * KVANTIL), 5), Math.Round(Qasimetry, 5));
                dataGridView2.Rows.Add("Контрексцес", Math.Round(kofkontrekses - (Qkontrekses * KVANTIL), 5), Math.Round(kofkontrekses, 5), Math.Round(kofkontrekses + (Qkontrekses * KVANTIL), 5), Math.Round(Qkontrekses, 5));
                dataGridView2.Rows.Add("Ексцес", Math.Round(kofekses2 - (Qekses * KVANTIL), 5), Math.Round(kofekses2, 5), Math.Round(kofekses2 + (Qekses * KVANTIL), 5), Math.Round(Qekses, 5));
                dataGridView2.Rows.Add("Коф. Пірсона", Math.Round(kofPirsona - (Qpirsona * KVANTIL), 5), Math.Round(kofPirsona, 5), Math.Round(kofPirsona + (Qpirsona * KVANTIL), 5), Math.Round(Qpirsona, 5));


                dataGridView2.Rows.Add("Непараметричнийкоф. варіації", 0, Math.Round(noparKofVariac, 5), 0);
                double peredbachenya = kwadratvid * Math.Sqrt((double)1 + ((double)1 / nBig));
                dataGridView2.Rows.Add("Інт. передбачення", Math.Round(matspod - (KVANTIL * peredbachenya), 5), "X нове", Math.Round(matspod + (KVANTIL * peredbachenya), 5));

                double a;
                double b;
                double t1 = 2 + 0.2 * Math.Log10(0.04 + dataBANK.AnomalcorTX.Count);
                double t2 = Math.Pow(19 * Math.Pow(kofekses2 + 2, 0.5) + 1, 0.5);
                if (kofAsimeNoMove < -0.2)
                {
                    a = matspod - (t2 * kwadratvid);
                    b = matspod + (t1 * kwadratvid);
                    for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                    {
                        if (dataBANK.AnomalcorTX[i] < a)
                        {
                            dataBANK.AnomalcorTX.RemoveAt(i);
                            goto startStandart;
                        }
                        else if (dataBANK.AnomalcorTX[i] > b)
                        {
                            dataBANK.AnomalcorTX.RemoveAt(i);
                            goto startStandart;
                        }
                    }
                }
                else if (kofAsimeNoMove > 0.2)
                {
                    a = matspod - (t1 * kwadratvid);
                    b = matspod + (t2 * kwadratvid);
                    for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                    {
                        if (dataBANK.AnomalcorTX[i] < a)
                        {
                            dataBANK.AnomalcorTX.RemoveAt(i);
                            goto startStandart;
                        }
                        else if (dataBANK.AnomalcorTX[i] > b)
                        {
                            dataBANK.AnomalcorTX.RemoveAt(i);
                            goto startStandart;
                        }
                    }
                }
                else if (Math.Abs(kofAsimeNoMove) <= 0.2)
                {
                    a = matspod - (t1 * kwadratvid);
                    b = matspod + (t1 * kwadratvid);
                    for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                    {
                        if (dataBANK.AnomalcorTX[i] < a)
                        {
                            dataBANK.AnomalcorTX.RemoveAt(i);
                            goto startStandart;
                        }
                        else if (dataBANK.AnomalcorTX[i] > b)
                        {
                            dataBANK.AnomalcorTX.RemoveAt(i);
                            goto startStandart;
                        }
                    }

                }

                /// graphiks
                chart1.Series[0].Points.Clear();
                chart2.Series[0].Points.Clear();
                chart2.Series[1].Points.Clear();
                chart1.ChartAreas[0].AxisX.Title = "Значення";
                chart1.ChartAreas[0].AxisY.Title = "Відносначастота";
                chart2.ChartAreas[0].AxisX.Title = "Значення";
                chart2.ChartAreas[0].AxisY.Title = "Відносначастота";
                kilkclass = (int)numericUpDown1.Value;

                if (numericUpDown1.Value == 0)
                {
                    if (variant.Count <= 100)
                    {
                        kilkclass = (int)Math.Sqrt(variant.Count);
                    }
                    else
                    {
                        kilkclass = (int)Math.Pow(variant.Count, 0.3333333);
                    }
                }
                else
                {
                    kilkclass = (int)numericUpDown1.Value;
                }
                double sered = variant[variant.Count - 1].variant - variant[0].variant;

                double shah = sered / kilkclass;

                double shaht = shah;
                shah += variant[0].variant;

                double sum = 0;
                double t = 0;

                int chislo = 0;
                int otchet = 0;
                int end = 0;
                double sum1 = 0;
                for (int i = 0; i < variant.Count; i++)
                {
                    //MessageBox.Show($"{i}");

                    if (variant[i].variant <= shah + t)
                    {
                        sum += variant[i].chast;
                        sum1 += variant[i].chast;
                        end = i;
                    }
                    else
                    {

                        otchet = i;
                        chart1.Series[0].Points.AddXY(variant[chislo].variant, sum);
                        chart1.Series[0].Points.AddXY(variant[end].variant, sum);
                        chart1.Series[0].Points.AddXY(variant[otchet].variant, sum);
                        sum = variant[i].chast;
                        sum1 += variant[i].chast;
                        chart2.Series[1].Points.AddXY(variant[chislo].variant, sum1);
                        chart2.Series[1].Points.AddXY(variant[otchet].variant, sum1);
                        chislo = i;
                        t += shaht;
                    }
                    if (variant.Count - 1 == i)
                    {
                        //MessageBox.Show($"2 di");
                        otchet = i;
                        chart1.Series[0].Points.AddXY(variant[chislo].variant, sum);
                        chart1.Series[0].Points.AddXY(variant[end].variant, sum);
                        chart1.Series[0].Points.AddXY(variant[otchet].variant, sum);
                        chart2.Series[1].Points.AddXY(variant[chislo].variant, sum1);
                        chart2.Series[1].Points.AddXY(variant[otchet].variant, sum1);

                    }

                }
                //chart1.Series[0].Points.AddXY(dataBANK.corTX[dataBANK.corTX.Count - 1], sum);
                chart1.Series[0].ChartType = SeriesChartType.Range;

                sum = 0;
                for (int i = 0; i < variant.Count; i++)
                {
                    sum += variant[i].chast;
                    chart2.Series[0].Points.AddXY(variant[i].variant, sum);
                }
                sum = 0;


            }

        }

        private void StandFunc_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            StandFunc.Checked = true;
            LogFunc.Checked = false;
            if (dataBANK.standfunc == null)
            {
                Close();
            }
            dataBANK.uolsh.Clear();
            dataBANK.MAD.Clear();
            double disper = 0;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            dataGridView1.Columns.Add("Xval", "Варіанти");
            dataGridView1.Columns.Add("funVAL", "Частота");
            dataGridView1.Columns.Add("f123", "Відносначастота");

            dataGridView2.Columns.Add("1", "Характеристика");
            dataGridView2.Columns.Add("2", "INF");
            dataGridView2.Columns.Add("3", "Значення");
            dataGridView2.Columns.Add("4", "SUP");
            dataGridView2.Columns.Add("5", "SKV");
            int nBig = dataBANK.AnomalSTandfunc.Count;
            dataBANK.AnomalChast.Clear();
            double nsmal = 0;
            double chast = 0;
            for (int i = 0; i < dataBANK.AnomalSTandfunc.Count; i++)
            {

                double comp = dataBANK.AnomalSTandfunc[i];
                nsmal = Program.ransom(dataBANK.AnomalSTandfunc, comp);

                chast = nsmal / nBig;

                dataBANK.AnomalChast.Add(chast);

            }

            List<rows> variant = new List<rows>();
            variant.Clear();
            for (int i = 0; i < dataBANK.AnomalSTandfunc.Count; i++)
            {
                rows rows1 = new rows();
                double comp = dataBANK.AnomalSTandfunc[i];
                chast = 1.0 / nBig;
                rows1.chast = chast;
                int KKK = (int)variant.Count;
                rows1.variant = comp;
                rows1.count = 1;
                if (KKK == 0)
                {
                    variant.Add(rows1);
                }
                else
                {
                    for (int G = 0; G < KKK; G++)
                    {
                        if (variant[G].variant == comp)
                        {
                            variant[G].chast += chast;
                            variant[G].count += 1;
                        }
                        else if ((G == KKK - 1) && variant[G].variant != nsmal)
                        {
                            variant.Add(rows1);
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < variant.Count; i++)
            {

                dataGridView1.Rows.Add(variant[i].variant, variant[i].count, variant[i].chast);
            }
            nsmal = 0;
            double nsmal1 = 0;
            double xi = 0;
            double xj = 0;
            for (int i = 0; i < dataBANK.AnomalSTandfunc.Count; i++)
            {
                nsmal += dataBANK.AnomalSTandfunc[i];
                nsmal1 += Math.Pow(dataBANK.AnomalSTandfunc[i], 2);

                xi = dataBANK.AnomalSTandfunc[i];
                for (int j = 0; j < dataBANK.AnomalSTandfunc.Count; j++)
                {
                    xj = dataBANK.AnomalSTandfunc[j];
                    double res = (xi + xj) / 2;
                    dataBANK.uolsh.Add(res);
                }

            }
            dataBANK.uolsh.Sort();
            double medUolsh = 0;
            if (dataBANK.uolsh.Count % 2 == 0)
            {
                medUolsh = (dataBANK.uolsh[(dataBANK.uolsh.Count / 2) - 1] + dataBANK.uolsh[dataBANK.uolsh.Count / 2]) / 2;
            }
            else
            {
                medUolsh = dataBANK.uolsh[(dataBANK.uolsh.Count / 2)];
            }
            double mediana = 0;
            if (dataBANK.standfunc.Count % 2 == 0)
            {
                mediana = (dataBANK.AnomalSTandfunc[(dataBANK.AnomalSTandfunc.Count / 2) - 1] + dataBANK.AnomalSTandfunc[(dataBANK.AnomalSTandfunc.Count / 2)]) / 2;
            }
            else
            {
                mediana = dataBANK.AnomalSTandfunc[(dataBANK.AnomalSTandfunc.Count / 2)];
            }
            double kkk;
            if (Double.TryParse(textBox2.Text, out double kkk2))
            {
                if (kkk2 <= 0.5 && kkk2 >= 0)
                {
                    kkk = kkk2;
                }
                else
                {
                    kkk = 0.2;
                }
            }
            else
            {
                kkk = 0.2;
            }
            int Kusichmed = (int)Math.Truncate(dataBANK.AnomalSTandfunc.Count * kkk);
            double kofusichmed = dataBANK.AnomalSTandfunc.Count - (2 * Kusichmed);
            double usichsered = 0;
            for (int i = Kusichmed + 1; i < dataBANK.AnomalSTandfunc.Count - Kusichmed; i++)
            {
                usichsered += (1 / kofusichmed) * dataBANK.AnomalSTandfunc[i];
            }
            for (int i = 0; i < dataBANK.AnomalSTandfunc.Count; i++)
            {
                double x1l = dataBANK.AnomalSTandfunc[i];
                double resar = Math.Abs(x1l - mediana);
                dataBANK.MAD.Add(resar);
            }



            double MAD = 0;
            if (dataBANK.MAD.Count % 2 == 0)
            {
                MAD = (dataBANK.MAD[(dataBANK.MAD.Count / 2) - 1] + dataBANK.MAD[dataBANK.MAD.Count / 2]) / 2;
            }
            else
            {
                MAD = dataBANK.MAD[dataBANK.MAD.Count / 2];
            }

            MAD = MAD * 1.483;
            matspod = nsmal / nBig;

            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                disper += Math.Pow(dataBANK.corTX[i], 2) / (nBig);
            }
            disper = disper - Math.Pow(matspod, 2);
            double disper2 = 0;
            for (int i = 0; i < dataBANK.AnomalSTandfunc.Count; i++)
            {
                disper2 += Math.Pow(dataBANK.AnomalSTandfunc[i] - matspod, 2) / (nBig - 1);
            }

            kwadratvid = Math.Sqrt(disper2);

            double kofasimetry = 0;
            kofAsimeNoMove = 0;
            for (int i = 0; i < dataBANK.AnomalSTandfunc.Count; i++)
            {
                kofasimetry += (1 / Math.Pow(kwadratvid, 3)) * Math.Pow(dataBANK.AnomalSTandfunc[i] - matspod, 3) * dataBANK.AnomalChast[i];
            }
            kofAsimeNoMove = kofasimetry * Math.Sqrt(nBig * (nBig - 1)) / (nBig - 2);

            double kofekses = 0;
            for (int i = 0; i < dataBANK.AnomalSTandfunc.Count; i++)
            {
                kofekses += (1 / Math.Pow(kwadratvid, 4)) * Math.Pow(dataBANK.AnomalSTandfunc[i] - matspod, 4) * dataBANK.AnomalChast[i];
            }

            kofekses2 = ((Math.Pow(nBig, 2) - 1) / ((nBig - 2) * (nBig - 3))) * ((kofekses - 3) + (6 / (nBig + 1)));
            double kofkontrekses = (1 / Math.Sqrt(Math.Abs(kofekses2)));
            double noparKofVariac = MAD / mediana;
            double kofPirsona = Math.Sqrt(disper2);



            double Qmatspod = kwadratvid / Math.Sqrt(nBig);
            double Qkwadvid = kwadratvid / Math.Sqrt(2 * nBig);
            double Qpirsona = kofPirsona * Math.Sqrt(((double)1 + 2 * Math.Pow(kofPirsona, 2)) / (2 * nBig));
            double Qasimetry = Math.Sqrt(((double)6 * (nBig - 2)) / ((nBig + 1) * (nBig + 3)));


            double t24 = 24;
            double t1 = 1;
            double t225 = 225;
            double t15 = 15;
            double t124 = 124;
            double Qekses = (double)Math.Sqrt((t24 / nBig) * (t1 - (t225 / (t15 * nBig + t124))));
            double KVANTIL;
            if (Double.TryParse(textBox3.Text, out double KW2))
            {
                KVANTIL = KW2;
            }
            else
            {
                KVANTIL = 1.96;
            }
            double Qdisper = (2 * Math.Pow(disper2, 2)) / (nBig - 1);
            double Qkontrekses = Math.Sqrt(Math.Abs(kofekses) / ((double)29 * nBig)) * Math.Pow(Math.Abs(Math.Pow(kofekses, 2) - (double)1), 0.25);

            dataGridView2.Rows.Add("Середнє", Math.Round(matspod - (KVANTIL * Qmatspod), 5), Math.Round(matspod, 5), Math.Round(matspod + (KVANTIL * Qmatspod), 5), Math.Round(Qmatspod, 5));
            dataGridView2.Rows.Add("Дисперсія ", Math.Round(disper2 - (KVANTIL * Qdisper), 5), Math.Round(disper2, 5), Math.Round(disper2 + (KVANTIL * Qdisper), 5), Math.Round(Qdisper, 5));

            dataGridView2.Rows.Add("MED", 0, Math.Round(mediana, 5), 0);
            dataGridView2.Rows.Add("MAD", 0, Math.Round(MAD, 5), 0);
            dataGridView2.Rows.Add("Усіченесереднє", 0, Math.Round(usichsered, 5), 0);
            dataGridView2.Rows.Add("MED Уолша", 0, Math.Round(medUolsh, 5), 0);

            dataGridView2.Rows.Add("Cер. квадратичне", Math.Round(kwadratvid - (Qkwadvid * KVANTIL), 5), Math.Round(kwadratvid, 5), Math.Round(kwadratvid + (Qkwadvid * KVANTIL), 5), Math.Round(Qkwadvid, 5));
            dataGridView2.Rows.Add("Aсиметрія", Math.Round(kofAsimeNoMove - (Qasimetry * KVANTIL), 5), Math.Round(kofAsimeNoMove, 5), Math.Round(kofAsimeNoMove + (Qasimetry * KVANTIL), 5), Math.Round(Qasimetry, 5));
            dataGridView2.Rows.Add("Контрексцес", Math.Round(kofkontrekses - (Qkontrekses * KVANTIL), 5), Math.Round(kofkontrekses, 5), Math.Round(kofkontrekses + (Qkontrekses * KVANTIL), 5), Math.Round(Qkontrekses, 5));
            dataGridView2.Rows.Add("Ексцес", Math.Round(kofekses2 - (Qekses * KVANTIL), 5), Math.Round(kofekses2, 5), Math.Round(kofekses2 + (Qekses * KVANTIL), 5), Math.Round(Qekses, 5));

            dataGridView2.Rows.Add("Непараметричнийкоф. варіації", 0, Math.Round(noparKofVariac, 5), 0);

            double peredbachenya = kwadratvid * Math.Sqrt((double)1 + ((double)1 / nBig));
            dataGridView2.Rows.Add("Інт. передбачення", Math.Round(matspod - (KVANTIL * peredbachenya), 5), "X нове", Math.Round(matspod + (KVANTIL * peredbachenya), 5));

            /// drawing graphics;
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart1.Series[0].Points.Clear();

            chart1.ChartAreas[0].AxisX.Title = "Значення";
            chart1.ChartAreas[0].AxisY.Title = "Відносначастота";
            kilkclass = (int)numericUpDown1.Value;

            if (numericUpDown1.Value == 0)
            {
                if (dataBANK.AnomalSTandfunc.Count <= 100)
                {
                    kilkclass = (int)Math.Sqrt(dataBANK.AnomalSTandfunc.Count);
                }
                else
                {
                    kilkclass = (int)Math.Pow(dataBANK.AnomalSTandfunc.Count, 0.3333333);
                }
            }
            else
            {
                kilkclass = (int)numericUpDown1.Value;
            }
            double sered = dataBANK.AnomalSTandfunc[dataBANK.AnomalSTandfunc.Count - 1] - dataBANK.AnomalSTandfunc[0];

            double shah = sered / kilkclass;

            double shaht = shah;
            shah += dataBANK.AnomalSTandfunc[0];
            double sum = 0;
            double t = 0;

            int chislo = 0;
            int otchet = 0;
            int end = 0;
            double sum1 = 0;
            for (int i = 0; i < dataBANK.AnomalSTandfunc.Count; i++)
            {

                if (dataBANK.AnomalSTandfunc[i] <= shah + t)
                {
                    sum += dataBANK.AnomalChast[i];
                    sum1 += dataBANK.AnomalChast[i];

                    end = i;
                }
                else
                {

                    otchet = i;
                    chart1.Series[0].Points.AddXY(dataBANK.AnomalSTandfunc[chislo], sum);
                    chart1.Series[0].Points.AddXY(dataBANK.AnomalSTandfunc[end], sum);
                    chart1.Series[0].Points.AddXY(dataBANK.AnomalSTandfunc[otchet], sum);
                    sum = dataBANK.AnomalChast[i];
                    sum1 += dataBANK.AnomalChast[i];

                    chart2.Series[1].Points.AddXY(dataBANK.AnomalSTandfunc[chislo], sum1);
                    chart2.Series[1].Points.AddXY(dataBANK.AnomalSTandfunc[otchet], sum1);

                    chislo = i;
                    t += shaht;

                }
                if (dataBANK.AnomalSTandfunc.Count - 1 == i)
                {
                    //MessageBox.Show($"2 di");
                    otchet = i;
                    chart1.Series[0].Points.AddXY(dataBANK.AnomalSTandfunc[chislo], sum);
                    chart1.Series[0].Points.AddXY(dataBANK.AnomalSTandfunc[end], sum);
                    chart1.Series[0].Points.AddXY(dataBANK.AnomalSTandfunc[otchet], sum);

                    chart2.Series[1].Points.AddXY(dataBANK.AnomalSTandfunc[chislo], sum1);
                    chart2.Series[1].Points.AddXY(dataBANK.AnomalSTandfunc[otchet], sum1);


                }

            }
            //chart1.Series[0].Points.AddXY(dataBANK.corTX[dataBANK.corTX.Count - 1], sum);
            chart1.Series[0].ChartType = SeriesChartType.Range;

            sum = 0;
            for (int i = 0; i < dataBANK.AnomalSTandfunc.Count; i++)
            {
                sum += dataBANK.AnomalChast[i];
                chart2.Series[0].Points.AddXY(dataBANK.AnomalSTandfunc[i], sum);
            }
            sum = 0;

        }

        private void LogFunc_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            StandFunc.Checked = false;
            LogFunc.Checked = true;
            if (dataBANK.logfunc == null)
            {
                Close();
            }


            dataBANK.uolsh.Clear();
            dataBANK.MAD.Clear();
            double disper = 0;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();


            dataGridView1.Columns.Add("Xval", "Варіанти");
            dataGridView1.Columns.Add("funVAL", "Частота");
            dataGridView1.Columns.Add("f123", "Відносначастота");

            dataGridView2.Columns.Add("1", "Характеристика");
            dataGridView2.Columns.Add("2", "INF");
            dataGridView2.Columns.Add("3", "Значення");
            dataGridView2.Columns.Add("4", "SUP");
            dataGridView2.Columns.Add("5", "SKV");
            int nBig = dataBANK.logfunc.Count;
            dataBANK.AnomalChast.Clear();
            double nsmal = 0;
            double chast = 0;
            for (int i = 0; i < dataBANK.logfunc.Count; i++)
            {

                double comp = dataBANK.logfunc[i];
                nsmal = Program.ransom(dataBANK.logfunc, comp);

                chast = nsmal / nBig;
                dataBANK.AnomalChast.Add(chast);

            }

            List<rows> variant = new List<rows>();
            variant.Clear();
            for (int i = 0; i < dataBANK.logfunc.Count; i++)
            {
                rows rows1 = new rows();
                double comp = dataBANK.logfunc[i];
                chast = 1.0 / nBig;
                rows1.chast = chast;
                int KKK = (int)variant.Count;
                rows1.variant = comp;
                rows1.count = 1;
                if (KKK == 0)
                {
                    variant.Add(rows1);
                }
                else
                {
                    for (int G = 0; G < KKK; G++)
                    {
                        if (variant[G].variant == comp)
                        {
                            variant[G].chast += chast;
                            variant[G].count += 1;
                        }
                        else if ((G == KKK - 1) && variant[G].variant != nsmal)
                        {
                            variant.Add(rows1);
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < variant.Count; i++)
            {

                dataGridView1.Rows.Add(variant[i].variant, variant[i].count, variant[i].chast);
            }
            nsmal = 0;
            double nsmal1 = 0;
            double xi = 0;
            double xj = 0;
            for (int i = 0; i < dataBANK.logfunc.Count; i++)
            {
                nsmal += dataBANK.logfunc[i];
                nsmal1 += Math.Pow(dataBANK.logfunc[i], 2);

                xi = dataBANK.logfunc[i];
                for (int j = 0; j < dataBANK.logfunc.Count; j++)
                {
                    xj = dataBANK.logfunc[j];
                    double res = (xi + xj) / 2;
                    dataBANK.uolsh.Add(res);
                }

            }
            dataBANK.uolsh.Sort();
            double medUolsh = 0;
            if (dataBANK.uolsh.Count % 2 == 0)
            {
                medUolsh = (dataBANK.uolsh[(dataBANK.uolsh.Count / 2) - 1] + dataBANK.uolsh[dataBANK.uolsh.Count / 2]) / 2;
            }
            else
            {
                medUolsh = dataBANK.uolsh[(dataBANK.uolsh.Count / 2)];
            }
            double mediana = 0;
            if (dataBANK.standfunc.Count % 2 == 0)
            {
                mediana = (dataBANK.logfunc[(dataBANK.logfunc.Count / 2) - 1] + dataBANK.logfunc[(dataBANK.logfunc.Count / 2)]) / 2;
            }
            else
            {
                mediana = dataBANK.logfunc[(dataBANK.logfunc.Count / 2)];
            }
            double kkk;
            if (Double.TryParse(textBox2.Text, out double kkk2))
            {
                if (kkk2 <= 0.5 && kkk2 >= 0)
                {
                    kkk = kkk2;
                }
                else
                {
                    kkk = 0.2;
                }
            }
            else
            {
                kkk = 0.2;
            }
            int Kusichmed = (int)Math.Truncate(dataBANK.logfunc.Count * kkk);
            double kofusichmed = dataBANK.logfunc.Count - (2 * Kusichmed);
            double usichsered = 0;
            for (int i = Kusichmed + 1; i < dataBANK.logfunc.Count - Kusichmed; i++)
            {
                usichsered += (1 / kofusichmed) * dataBANK.logfunc[i];
            }
            for (int i = 0; i < dataBANK.logfunc.Count; i++)
            {
                double x1l = dataBANK.logfunc[i];
                double resar = Math.Abs(x1l - mediana);
                dataBANK.MAD.Add(resar);
            }



            double MAD = 0;
            if (dataBANK.MAD.Count % 2 == 0)
            {
                MAD = (dataBANK.MAD[(dataBANK.MAD.Count / 2) - 1] + dataBANK.MAD[dataBANK.MAD.Count / 2]) / 2;
            }
            else
            {
                MAD = dataBANK.MAD[dataBANK.MAD.Count / 2];
            }

            MAD = MAD * 1.483;
            matspod = nsmal / nBig;

            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                disper += Math.Pow(dataBANK.corTX[i], 2) / (nBig);
            }
            disper = disper - Math.Pow(matspod, 2);
            double disper2 = 0;
            for (int i = 0; i < dataBANK.logfunc.Count; i++)
            {
                disper2 += Math.Pow(dataBANK.logfunc[i] - matspod, 2) / (nBig - 1);
            }

            kwadratvid = Math.Sqrt(disper2);

            double kofasimetry = 0;
            kofAsimeNoMove = 0;
            for (int i = 0; i < dataBANK.logfunc.Count; i++)
            {
                kofasimetry += (1 / Math.Pow(kwadratvid, 3)) * Math.Pow(dataBANK.logfunc[i] - matspod, 3) * dataBANK.AnomalChast[i];
            }
            kofAsimeNoMove = kofasimetry * Math.Sqrt(nBig * (nBig - 1)) / (nBig - 2);

            double kofekses = 0;
            for (int i = 0; i < dataBANK.logfunc.Count; i++)
            {
                kofekses += (1 / Math.Pow(kwadratvid, 4)) * Math.Pow(dataBANK.logfunc[i] - matspod, 4) * dataBANK.AnomalChast[i];
            }

            kofekses2 = ((Math.Pow(nBig, 2) - 1) / ((nBig - 2) * (nBig - 3))) * ((kofekses - 3) + (6 / (nBig + 1)));
            double kofkontrekses = (1 / Math.Sqrt(Math.Abs(kofekses2)));
            double noparKofVariac = MAD / mediana;
            double kofPirsona = Math.Sqrt(disper2) / matspod;



            double Qmatspod = kwadratvid / Math.Sqrt(nBig);
            double Qkwadvid = kwadratvid / Math.Sqrt(2 * nBig);
            double Qpirsona = kofPirsona * Math.Sqrt((1 + 2 * Math.Pow(kofPirsona, 2)) / (2 * nBig));
            double Qasimetry = Math.Sqrt(((double)6 * (nBig - 2)) / ((nBig + 1) * (nBig + 3)));


            double t24 = 24;
            double t11 = 1;
            double t225 = 225;
            double t15 = 15;
            double t124 = 124;
            double Qekses = (double)Math.Sqrt((t24 / nBig) * (t11 - (t225 / (t15 * nBig + t124))));

            double KVANTIL;
            if (Double.TryParse(textBox3.Text, out double KW2))
            {
                KVANTIL = KW2;
            }
            else
            {
                KVANTIL = 1.96;
            }
            double Qdisper = (2 * Math.Pow(disper2, 2)) / (nBig - 1);
            double Qkontrekses = Math.Sqrt(Math.Abs(kofekses) / ((double)29 * nBig)) * Math.Pow(Math.Abs(Math.Pow(kofekses, 2) - (double)1), 0.25);

            dataGridView2.Rows.Add("Середнє", Math.Round(matspod - (KVANTIL * Qmatspod), 5), Math.Round(matspod, 5), Math.Round(matspod + (KVANTIL * Qmatspod), 5), Math.Round(Qmatspod, 5));
            dataGridView2.Rows.Add("Дисперсія ", Math.Round(disper2 - (KVANTIL * Qdisper), 5), Math.Round(disper2, 5), Math.Round(disper2 + (KVANTIL * Qdisper), 5), Math.Round(Qdisper, 5));

            dataGridView2.Rows.Add("MED", 0, Math.Round(mediana, 5), 0);
            dataGridView2.Rows.Add("MAD", 0, Math.Round(MAD, 5), 0);
            dataGridView2.Rows.Add("Усіченесереднє", 0, Math.Round(usichsered, 5), 0);
            dataGridView2.Rows.Add("MED Уолша", 0, Math.Round(medUolsh, 5), 0);
            dataGridView2.Rows.Add("Cер. квадратичне", Math.Round(kwadratvid - (Qkwadvid * KVANTIL), 5), Math.Round(kwadratvid, 5), Math.Round(kwadratvid + (Qkwadvid * KVANTIL), 5), Math.Round(Qkwadvid, 5));
            dataGridView2.Rows.Add("Aсиметрія", Math.Round(kofAsimeNoMove - (Qasimetry * KVANTIL), 5), Math.Round(kofAsimeNoMove, 5), Math.Round(kofAsimeNoMove + (Qasimetry * KVANTIL), 5), Math.Round(Qasimetry, 5));
            dataGridView2.Rows.Add("Контрексцес", Math.Round(kofkontrekses - (Qkontrekses * KVANTIL), 5), Math.Round(kofkontrekses, 5), Math.Round(kofkontrekses + (Qkontrekses * KVANTIL), 5), Math.Round(Qkontrekses, 5));
            dataGridView2.Rows.Add("Ексцес", Math.Round(kofekses2 - (Qekses * KVANTIL), 5), Math.Round(kofekses2, 5), Math.Round(kofekses2 + (Qekses * KVANTIL), 5), Math.Round(Qekses, 5));
            dataGridView2.Rows.Add("Коф. Пірсона", Math.Round(kofPirsona - (Qpirsona * KVANTIL), 5), Math.Round(kofPirsona, 5), Math.Round(kofPirsona + (Qpirsona * KVANTIL), 5), Math.Round(Qpirsona, 5));
            dataGridView2.Rows.Add("Непараметричнийкоф. варіації", 0, Math.Round(noparKofVariac, 5), 0);

            double peredbachenya = kwadratvid * Math.Sqrt((double)1 + ((double)1 / nBig));
            dataGridView2.Rows.Add("Інт. передбачення", Math.Round(matspod - (KVANTIL * peredbachenya), 5), "X нове", Math.Round(matspod + (KVANTIL * peredbachenya), 5));

            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart1.Series[0].Points.Clear();
            chart1.ChartAreas[0].AxisX.Title = "Значення";
            chart1.ChartAreas[0].AxisY.Title = "Відносначастота";
            kilkclass = (int)numericUpDown1.Value;

            if (numericUpDown1.Value == 0)
            {
                if (variant.Count <= 100)
                {
                    kilkclass = (int)Math.Sqrt(variant.Count);
                }
                else
                {
                    kilkclass = (int)Math.Pow(variant.Count, 0.3333333);
                }
            }
            else
            {
                kilkclass = (int)numericUpDown1.Value;
            }
            double sered = variant[variant.Count - 1].variant - variant[0].variant;

            double shah = sered / kilkclass;

            double shaht = shah;
            shah += variant[0].variant;

            double sum = 0;
            double t = 0;

            int chislo = 0;
            int otchet = 0;
            int end = 0;
            double sum1 = 0;
            for (int i = 0; i < variant.Count; i++)
            {
                //MessageBox.Show($"{i}");

                if (variant[i].variant <= shah + t)
                {
                    sum += variant[i].chast;
                    sum1 += variant[i].chast;
                    end = i;
                }
                else
                {

                    otchet = i;
                    chart1.Series[0].Points.AddXY(variant[chislo].variant, sum);
                    chart1.Series[0].Points.AddXY(variant[end].variant, sum);
                    chart1.Series[0].Points.AddXY(variant[otchet].variant, sum);
                    sum = variant[i].chast;
                    sum1 += variant[i].chast;
                    chart2.Series[1].Points.AddXY(variant[chislo].variant, sum1);
                    chart2.Series[1].Points.AddXY(variant[otchet].variant, sum1);
                    chislo = i;
                    t += shaht;
                }
                if (variant.Count - 1 == i)
                {
                    //MessageBox.Show($"2 di");
                    otchet = i;
                    chart1.Series[0].Points.AddXY(variant[chislo].variant, sum);
                    chart1.Series[0].Points.AddXY(variant[end].variant, sum);
                    chart1.Series[0].Points.AddXY(variant[otchet].variant, sum);
                    chart2.Series[1].Points.AddXY(variant[chislo].variant, sum1);
                    chart2.Series[1].Points.AddXY(variant[otchet].variant, sum1);

                }

            }
            //chart1.Series[0].Points.AddXY(dataBANK.corTX[dataBANK.corTX.Count - 1], sum);
            chart1.Series[0].ChartType = SeriesChartType.Range;

            sum = 0;
            for (int i = 0; i < variant.Count; i++)
            {
                sum += variant[i].chast;
                chart2.Series[0].Points.AddXY(variant[i].variant, sum);
            }
            sum = 0;

        }

        private void DovirInterval_DoubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Calculater(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void DovirInterval_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            dovirInterval.Checked = true;
            Vflag = true;
        }

        private void NOTdovirInterval_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            dovirInterval.Checked = false;
            chart2.Series[3].Points.Clear();
            chart2.Series[4].Points.Clear();
            Vflag = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            chart1.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            string filename = openFileDialog1.FileName;

            string fileText = System.IO.File.ReadAllText(filename);
            textBox1.Text = fileText;

            string[] text = textBox1.Lines;
            double cifr;

            string fro;
            dataBANK.corTX.Clear();
            dataBANK.Chast.Clear();
            dataBANK.AnomalChast.Clear();
            dataBANK.AnolomalLOGfunc.Clear();
            dataBANK.AnomalcorTX.Clear();
            dataBANK.AnomalSTandfunc.Clear();
            dataBANK.standfunc.Clear();
            dataBANK.uolsh.Clear();
            dataBANK.MAD.Clear();
            dataBANK.logfunc.Clear();
            string[] del;
            for (int i = 0; i < textBox1.Lines.Length; i++)
            {
                del = text[i].Split(' ');
                fro = Program.koma(del[0]);

                cifr = double.Parse(fro);
                // cifr = Convert.ToDouble(fro);
                dataBANK.corTX.Add(cifr);
                dataBANK.AnomalcorTX.Add(cifr);
                dataBANK.OriginalData.Add(cifr);

            }

            double disper = 0;
            richTextBox1.Text = "";
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataBANK.corTX.Sort();
            dataBANK.AnomalcorTX.Sort();
            int nBig = dataBANK.corTX.Count;

            double nsmal = 0;
            double chast = 0;

            dataGridView1.Columns.Add("Xval", "Варіанти");
            dataGridView1.Columns.Add("funVAL", "Частота");
            dataGridView1.Columns.Add("f123", "Відносначастота");

            richTextBox1.Visible = false;
            dataGridView2.Columns.Add("1", "Характеристика");
            dataGridView2.Columns.Add("2", "INF");
            dataGridView2.Columns.Add("3", "Значення");
            dataGridView2.Columns.Add("4", "SUP");
            dataGridView2.Columns.Add("5", "SKV");

            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {

                double comp = dataBANK.corTX[i];
                nsmal = Program.ransom(dataBANK.corTX, comp);

                chast = nsmal / nBig;
                dataBANK.AnomalChast.Add(chast);
                dataBANK.Chast.Add(chast);

            }
            List<rows> variant = new List<rows>();
            variant.Clear();
            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                rows rows1 = new rows();
                double comp = dataBANK.corTX[i];
                chast = 1.0 / nBig;
                rows1.chast = chast;
                int KKK = (int)variant.Count;
                rows1.variant = comp;
                rows1.count = 1;
                if (KKK == 0)
                {
                    variant.Add(rows1);
                }
                else
                {
                    for (int G = 0; G < KKK; G++)
                    {
                        if (variant[G].variant == comp)
                        {
                            variant[G].chast += chast;
                            variant[G].count += 1;
                        }
                        else if ((G == KKK - 1) && variant[G].variant != comp)
                        {
                            variant.Add(rows1);
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < variant.Count; i++)
            {

                dataGridView1.Rows.Add(variant[i].variant, variant[i].count, variant[i].chast);
            }
            nsmal = 0;
            double nsmal1 = 0;
            double xi = 0;
            double xj = 0;
            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                nsmal += dataBANK.corTX[i];
                nsmal1 += Math.Pow(dataBANK.corTX[i], 2);

                xi = dataBANK.corTX[i];
                for (int j = 0; j < dataBANK.corTX.Count; j++)
                {
                    xj = dataBANK.corTX[j];
                    double res = (xi + xj) / 2;
                    dataBANK.uolsh.Add(res);
                }

            }
            dataBANK.uolsh.Sort();
            double medUolsh = 0;
            if (dataBANK.uolsh.Count % 2 == 0)
            {
                medUolsh = (dataBANK.uolsh[(dataBANK.uolsh.Count / 2) - 1] + dataBANK.uolsh[dataBANK.uolsh.Count / 2]) / 2;
            }
            else
            {
                medUolsh = dataBANK.uolsh[(dataBANK.uolsh.Count / 2)];
            }
            double mediana = 0;
            if (dataBANK.corTX.Count % 2 == 0)
            {
                mediana = (dataBANK.corTX[(dataBANK.corTX.Count / 2) - 1] + dataBANK.corTX[(dataBANK.corTX.Count / 2)]) / 2;
            }
            else
            {
                mediana = dataBANK.corTX[(dataBANK.corTX.Count / 2)];
            }

            double kkk;
            if (Double.TryParse(textBox2.Text, out double kkk2))
            {
                if (kkk2 <= 0.5 && kkk2 >= 0)
                {
                    kkk = kkk2;
                }
                else
                {
                    kkk = 0.2;
                }

            }
            else
            {
                kkk = 0.2;
            }
            int Kusichmed = (int)Math.Truncate(dataBANK.corTX.Count * kkk);
            double kofusichmed = dataBANK.corTX.Count - (2 * Kusichmed);
            double usichsered = 0;
            for (int i = Kusichmed + 1; i < dataBANK.corTX.Count - Kusichmed; i++)
            {
                usichsered += (1 / kofusichmed) * dataBANK.corTX[i];
            }
            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                double x1l = dataBANK.corTX[i];
                double resar = Math.Abs(x1l - mediana);
                dataBANK.MAD.Add(resar);
            }



            double MAD = 0;
            if (dataBANK.MAD.Count % 2 == 0)
            {
                MAD = (dataBANK.MAD[(dataBANK.MAD.Count / 2) - 1] + dataBANK.MAD[dataBANK.MAD.Count / 2]) / 2;
            }
            else
            {
                MAD = dataBANK.MAD[dataBANK.MAD.Count / 2];
            }

            MAD = MAD * 1.483;
            matspod = nsmal / nBig;

            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                disper += Math.Pow(dataBANK.corTX[i], 2) / (nBig);
            }
            disper = disper - Math.Pow(matspod, 2);
            double disper2 = 0;
            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                disper2 += Math.Pow(dataBANK.corTX[i] - matspod, 2) / (nBig - 1);
            }

            kwadratvid = Math.Sqrt(disper2);

            double kofasimetry = 0;
            kofAsimeNoMove = 0;
            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                kofasimetry += (1 / Math.Pow(kwadratvid, 3)) * Math.Pow(dataBANK.corTX[i] - matspod, 3) * dataBANK.Chast[i];
            }
            kofAsimeNoMove = kofasimetry * Math.Sqrt(nBig * (nBig - 1)) / (nBig - 2);

            double kofekses = 0;
            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                kofekses += (1 / Math.Pow(disper, 2)) * Math.Pow(dataBANK.corTX[i] - matspod, 4) * dataBANK.Chast[i];
            }

            kofekses2 = ((Math.Pow(nBig, 2) - 1) / ((nBig - 2) * (nBig - 3))) * ((kofekses - 3) + (6 / (nBig + 1)));

            double kofkontrekses = (1 / Math.Sqrt(Math.Abs(kofekses2)));
            double kofPirsona = Math.Sqrt(disper2) / matspod;
            double noparKofVariac = MAD / mediana;


            bool minval = false;

            double comp1;
            double stand1;
            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {
                comp1 = dataBANK.corTX[i];
                stand1 = (comp1 - matspod) / kwadratvid;
                dataBANK.standfunc.Add(Math.Round(stand1, 5));
                dataBANK.AnomalSTandfunc.Add(Math.Round(stand1, 5));
            }


            /// стандартизаціяталогарифмування
            for (int i = 0; i < dataBANK.corTX.Count; i++)
            {

                comp1 = dataBANK.corTX[i];


                if (dataBANK.corTX[0] <= 0.1)
                {
                    minval = true;
                    comp1 += Math.Abs(dataBANK.corTX[0]) + 0.01;
                    stand1 = Math.Log(Math.Abs(comp1));
                    dataBANK.logfunc.Add(Math.Round(stand1, 5));
                    dataBANK.AnolomalLOGfunc.Add(Math.Round(stand1, 5));
                }
                else if (minval)
                {
                    comp1 += Math.Abs(dataBANK.corTX[0]) + 1;
                    stand1 = Math.Log(Math.Abs(comp1));
                    dataBANK.logfunc.Add(Math.Round(stand1, 5));
                    dataBANK.AnolomalLOGfunc.Add(Math.Round(stand1, 5));
                }
                else
                {
                    stand1 = Math.Log(Math.Abs(comp1));
                    dataBANK.logfunc.Add(Math.Round(stand1, 5));
                    dataBANK.AnolomalLOGfunc.Add(Math.Round(stand1, 5));
                }



            }

            // ДОВІРЧІІНТЕРВАЛИ

            double Qmatspod = kwadratvid / Math.Sqrt(nBig);
            double Qkwadvid = kwadratvid / Math.Sqrt(2 * nBig);
            double Qpirsona = kofPirsona * Math.Sqrt((1 + 2 * Math.Pow(kofPirsona, 2)) / (2 * nBig));
            double Qasimetry = Math.Sqrt(((double)6 * (nBig - 2)) / ((nBig + 1) * (nBig + 3)));

            double t24 = 24;
            double t1 = 1;
            double t225 = 225;
            double t15 = 15;
            double t124 = 124;
            double Qekses = (double)Math.Sqrt((t24 / nBig) * (t1 - (t225 / (t15 * nBig + t124))));


            double KVANTIL;
            if (Double.TryParse(textBox3.Text, out double KW2))
            {
                KVANTIL = KW2;
            }
            else
            {
                KVANTIL = 1.96;
            }
            double Qdisper = (2 * Math.Pow(disper2, 2)) / (nBig - 1);
            double Qkontrekses = Math.Sqrt(Math.Abs(kofekses) / ((double)29 * nBig)) * Math.Pow(Math.Abs(Math.Pow(kofekses, 2) - (double)1), 0.25);



            dataGridView2.Rows.Add("Середнє", Math.Round(matspod - (KVANTIL * Qmatspod), 5), Math.Round(matspod, 5), Math.Round(matspod + (KVANTIL * Qmatspod), 5), Math.Round(Qmatspod, 5));
            dataGridView2.Rows.Add("Дисперсія ", Math.Round(disper2 - (KVANTIL * Qdisper), 5), Math.Round(disper2, 5), Math.Round(disper2 + (KVANTIL * Qdisper), 5), Math.Round(Qdisper, 5));

            dataGridView2.Rows.Add("MED", 0, Math.Round(mediana, 5), 0);
            dataGridView2.Rows.Add("MAD", 0, Math.Round(MAD, 5), 0);
            dataGridView2.Rows.Add("Усіченесереднє", 0, Math.Round(usichsered, 5), 0);
            dataGridView2.Rows.Add("MED Уолша", 0, Math.Round(medUolsh, 5), 0);
            dataGridView2.Rows.Add("Cер. квадратичне", Math.Round(kwadratvid - (Qkwadvid * KVANTIL), 5), Math.Round(kwadratvid, 5), Math.Round(kwadratvid + (Qkwadvid * KVANTIL), 5), Math.Round(Qkwadvid, 5));
            dataGridView2.Rows.Add("Aсиметрія", Math.Round(kofAsimeNoMove - (Qasimetry * KVANTIL), 5), Math.Round(kofAsimeNoMove, 5), Math.Round(kofAsimeNoMove + (Qasimetry * KVANTIL), 5), Math.Round(Qasimetry, 5));
            dataGridView2.Rows.Add("Контрексцес", Math.Round(kofkontrekses - (Qkontrekses * KVANTIL), 5), Math.Round(kofkontrekses, 5), Math.Round(kofkontrekses + (Qkontrekses * KVANTIL), 5), Math.Round(Qkontrekses, 5));
            dataGridView2.Rows.Add("Ексцес", Math.Round(kofekses2 - (Qekses * KVANTIL), 5), Math.Round(kofekses2, 5), Math.Round(kofekses2 + (Qekses * KVANTIL), 5), Math.Round(Qekses, 5));
            dataGridView2.Rows.Add("Коф. Пірсона", Math.Round(kofPirsona - (Qpirsona * KVANTIL), 5), Math.Round(kofPirsona, 5), Math.Round(kofPirsona + (Qpirsona * KVANTIL), 5), Math.Round(Qpirsona, 5));


            dataGridView2.Rows.Add("Непараметричнийкоф. варіації", 0, Math.Round(noparKofVariac, 5), 0);
            double peredbachenya = kwadratvid * Math.Sqrt((double)1 + ((double)1 / nBig));
            dataGridView2.Rows.Add("Інт. передбачення", Math.Round(matspod - (KVANTIL * peredbachenya), 5), "X нове", Math.Round(matspod + (KVANTIL * peredbachenya), 5));



        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    buhalter(Universe.Data_Vectors[i]);
                    break;

                }
            }
            //chart1.Series[0].Points.Clear();
            //chart2.Series[0].Points.Clear();
            //chart2.Series[1].Points.Clear();
            //chart1.ChartAreas[0].AxisX.Title = "Значення";
            //chart1.ChartAreas[0].AxisY.Title = "Відносначастота";
            //chart2.ChartAreas[0].AxisX.Title = "Значення";
            //chart2.ChartAreas[0].AxisY.Title = "Відносначастота";
            //kilkclass = (int)numericUpDown1.Value;
            //List<rows> variant = new List<rows>();
            //variant.Clear();
            //double chast = 0;
            //double nBig = dataBANK.AnomalcorTX.Count;
            //for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
            //{
            //    rows rows1 = new rows();
            //    double comp = dataBANK.AnomalcorTX[i];
            //    chast = 1.0 / nBig;
            //    rows1.chast = chast;
            //    int KKK = (int)variant.Count;
            //    rows1.variant = comp;
            //    rows1.count = 1;
            //    if (KKK == 0)
            //    {
            //        variant.Add(rows1);
            //    }
            //    else
            //    {
            //        for (int G = 0; G < KKK; G++)
            //        {
            //            if (variant[G].variant == comp)
            //            {
            //                variant[G].chast += chast;
            //                variant[G].count += 1;
            //            }
            //            else if ((G == KKK - 1) && variant[G].variant != comp)
            //            {
            //                variant.Add(rows1);
            //                break;
            //            }
            //        }
            //    }
            //}
            //if (numericUpDown1.Value == 0)
            //{
            //    if (variant.Count <= 100)
            //    {
            //        kilkclass = (int)Math.Sqrt(variant.Count);
            //    }
            //    else
            //    {
            //        kilkclass = (int)Math.Pow(variant.Count, 0.3333333);
            //    }
            //}
            //else
            //{
            //    kilkclass = (int)numericUpDown1.Value;
            //}
            //double sered = variant[variant.Count - 1].variant - variant[0].variant;

            //double shah = sered / kilkclass;

            //double shaht = shah;
            //shah += variant[0].variant;

            //double sum = 0;
            //double t = 0;

            //int chislo = 0;
            //int otchet = 0;
            //int end = 0;
            //double sum1 = 0;
            //for (int i = 0; i < variant.Count; i++)
            //{
            //    //MessageBox.Show($"{i}");

            //    if (variant[i].variant <= shah + t)
            //    {
            //        sum += variant[i].chast;
            //        sum1 += variant[i].chast;
            //        end = i;
            //    }
            //    else
            //    {

            //        otchet = i;
            //        chart1.Series[0].Points.AddXY(variant[chislo].variant, sum);
            //        chart1.Series[0].Points.AddXY(variant[end].variant, sum);
            //        chart1.Series[0].Points.AddXY(variant[otchet].variant, sum);
            //        sum = variant[i].chast;
            //        sum1 += variant[i].chast;
            //        chart2.Series[1].Points.AddXY(variant[chislo].variant, sum1);
            //        chart2.Series[1].Points.AddXY(variant[otchet].variant, sum1);
            //        chislo = i;
            //        t += shaht;
            //    }
            //    if (variant.Count - 1 == i)
            //    {
            //        //MessageBox.Show($"2 di");
            //        otchet = i;
            //        chart1.Series[0].Points.AddXY(variant[chislo].variant, sum);
            //        chart1.Series[0].Points.AddXY(variant[end].variant, sum);
            //        chart1.Series[0].Points.AddXY(variant[otchet].variant, sum);
            //        chart2.Series[1].Points.AddXY(variant[chislo].variant, sum1);
            //        chart2.Series[1].Points.AddXY(variant[otchet].variant, sum1);

            //    }

            //}
            ////chart1.Series[0].Points.AddXY(dataBANK.corTX[dataBANK.corTX.Count - 1], sum);
            //chart1.Series[0].ChartType = SeriesChartType.Range;

            //sum = 0;
            //for (int i = 0; i < variant.Count; i++)
            //{
            //    sum += variant[i].chast;
            //    chart2.Series[0].Points.AddXY(variant[i].variant, sum);
            //}
            //sum = 0;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart1.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            List<rows> variant = new List<rows>();
            variant.Clear();
            double chast = 0;
            double nBig = dataBANK.AnomalcorTX.Count;
            for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
            {
                rows rows1 = new rows();
                double comp = dataBANK.AnomalcorTX[i];
                chast = 1.0 / nBig;
                rows1.chast = chast;
                int KKK = (int)variant.Count;
                rows1.variant = comp;
                rows1.count = 1;
                if (KKK == 0)
                {
                    variant.Add(rows1);
                }
                else
                {
                    for (int G = 0; G < KKK; G++)
                    {
                        if (variant[G].variant == comp)
                        {
                            variant[G].chast += chast;
                            variant[G].count += 1;
                        }
                        else if ((G == KKK - 1) && variant[G].variant != comp)
                        {
                            variant.Add(rows1);
                            break;
                        }
                    }
                }
            }

            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Clear();
            dataGridView3.AutoSizeColumnsMode =
DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView4.AutoSizeColumnsMode =
DataGridViewAutoSizeColumnsMode.AllCells;
            if (comboBox1.SelectedIndex == 0)
            {
                //нормальнийрозподіл
                kilkclass = (int)numericUpDown1.Value;

                if (numericUpDown1.Value == 0)
                {
                    if (dataBANK.AnomalcorTX.Count <= 100)
                    {
                        kilkclass = (int)Math.Sqrt(dataBANK.AnomalcorTX.Count);
                    }
                    else
                    {
                        kilkclass = (int)Math.Pow(dataBANK.AnomalcorTX.Count, 0.3333333);
                    }
                }
                else
                {
                    kilkclass = (int)numericUpDown1.Value;
                }
                double sered = dataBANK.AnomalcorTX[dataBANK.AnomalcorTX.Count - 1] - dataBANK.AnomalcorTX[0];

                double shah = sered / kilkclass;
                double m = dataBANK.AnomalcorTX[dataBANK.AnomalcorTX.Count - 1];
                if (dataBANK.AnomalcorTX.Count % 2 == 0)
                {
                    m = (dataBANK.AnomalcorTX[(dataBANK.AnomalcorTX.Count / 2) - 1] + dataBANK.AnomalcorTX[dataBANK.AnomalcorTX.Count / 2]) / 2;
                }
                else
                {
                    m = dataBANK.AnomalcorTX[dataBANK.AnomalcorTX.Count / 2];
                }
                double f = 0;
                double x = 0;
                double b1 = 0.31938153;
                double b2 = -0.356563782;
                double b4 = -1.821255978;
                double b3 = 1.781477937;
                double b5 = 1.330274429;
                double p = 0.2316419;
                double epsU = 7.8 * Math.Pow(10.0, -8.0);
                double T = 0;
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    x = dataBANK.AnomalcorTX[i];
                    f = (Math.Exp(-1.0 * ((Math.Pow(x - m, 2) / (2.0 * Math.Pow(kwadratvid, 2)))))) / (kwadratvid * Math.Sqrt(2.0 * Math.PI));
                    f = f * shah;
                    chart1.Series[1].Points.AddXY(x, f);

                }

                List<double> kolmogorov = new List<double>();
                List<double> teorchasto = new List<double>();
                List<double> kolmogorov2 = new List<double>();
                double u = 0;
                double chase = 0;
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    x = dataBANK.AnomalcorTX[i];
                    u = (x - matspod) / kwadratvid;
                    chase += dataBANK.AnomalChast[i];
                    double dod = 0;
                    if (u >= 0)
                    {
                        T = 1.0 / (1.0 + p * u);
                        f = 1.0 - (1.0 / (Math.Sqrt(2.0 * Math.PI))) * Math.Exp(-1.0 * (Math.Pow(u, 2) / 2.0)) * (b1 * T + b2 * Math.Pow(T, 2.0) + b3 * Math.Pow(T, 3.0) + b4 * Math.Pow(T, 4.0) + b5 * Math.Pow(T, 5.0)) + epsU;
                        chart2.Series[2].Points.AddXY(x, f);
                    }
                    else
                    {
                        T = 1.0 / (1.0 + p * Math.Abs(u));
                        f = 1.0 - (1.0 / (Math.Sqrt(2.0 * Math.PI))) * Math.Exp(-1.0 * (Math.Pow(u, 2) / 2.0)) * (b1 * T + b2 * Math.Pow(T, 2.0) + b3 * Math.Pow(T, 3.0) + b4 * Math.Pow(T, 4.0) + b5 * Math.Pow(T, 5.0)) + epsU;
                        f = 1.0 - f;
                        chart2.Series[2].Points.AddXY(x, f);
                    }
                    dod = Math.Abs(chase - f);
                    kolmogorov.Add(dod);

                }
                dataGridView4.Columns.Add("345", "Параметр");
                dataGridView4.Columns.Add("345", "Значення");
                dataGridView4.Rows.Add("сігма", Math.Round(kwadratvid, 3));
                dataGridView4.Rows.Add("середнє", Math.Round(matspod, 3));
                //колмогоров
                double Dplus = kolmogorov.Max();
                double Dminus = 0;
                double z = 0;
                if (Dplus >= Dminus)
                {
                    z = Math.Sqrt(dataBANK.AnomalcorTX.Count) * Dplus;
                }
                else
                {
                    z = Math.Sqrt(dataBANK.AnomalcorTX.Count) * Dminus;
                }

                double Kz = 0;
                for (double k = 1.0; k <= 4.0; k++)
                {
                    double f2 = 5.0 * Math.Pow(k, 2) + 22.0 - 7.5 * (1.0 - Math.Pow(-1.0, k));
                    double f1 = Math.Pow(k, 2) - 0.5 * (1.0 - Math.Pow(-1.0, k));
                    Kz += Math.Pow(-1.0, k) * Math.Exp(-2.0 * (Math.Pow(k, 2.0) * Math.Pow(z, 2.0))) * (1.0 - ((2.0 * Math.Pow(k, 2) * z) / (3.0 * Math.Sqrt(nBig))) - (1.0 / (18.0 * nBig)) *
                                            ((f1 - 4.0 * (f1 + 3.0)) * Math.Pow(k, 2.0) * Math.Pow(z, 2.0) + (8.0 * Math.Pow(k, 4.0) * Math.Pow(z, 4.0))) + ((Math.Pow(k, 2.0) * z) / (27.0 * Math.Sqrt(Math.Pow(nBig, 3.0))))
                                            * ((Math.Pow(f2, 2) / 5.0) - ((4.0 * (f2 + 45.0) * Math.Pow(k, 2.0) * Math.Pow(z, 2.0)) / (15.0)) + (8.0 * Math.Pow(k, 4.0) * Math.Pow(z, 4.0))));
                }


                Kz = 1.0 + (2.0 * Kz);
                // MessageBox.Show($"{Kz}");
                /// критерійпірсона
                sered = variant[variant.Count - 1].variant - variant[0].variant;

                shah = sered / kilkclass;

                double shaht = shah;
                shah += variant[0].variant;
                double sum = 0;
                double t = 0;
                double kofPirsona = 0;
                //double sum1 = 0;
                double f0 = 0;
                x = dataBANK.AnomalcorTX[0];
                u = (x - matspod) / kwadratvid;
                if (u >= 0)
                {
                    T = 1.0 / (1.0 + p * u);
                    f = 1.0 - (1.0 / (Math.Sqrt(2.0 * Math.PI))) * Math.Exp(-1.0 * (Math.Pow(u, 2) / 2.0)) * (b1 * T + b2 * Math.Pow(T, 2.0) + b3 * Math.Pow(T, 3.0) + b4 * Math.Pow(T, 4.0) + b5 * Math.Pow(T, 5.0)) + epsU;
                }
                else
                {
                    T = 1.0 / (1.0 + p * Math.Abs(u));
                    f = 1.0 - (1.0 / (Math.Sqrt(2.0 * Math.PI))) * Math.Exp(-1.0 * (Math.Pow(u, 2) / 2.0)) * (b1 * T + b2 * Math.Pow(T, 2.0) + b3 * Math.Pow(T, 3.0) + b4 * Math.Pow(T, 4.0) + b5 * Math.Pow(T, 5.0)) + epsU;
                    f = 1.0 - f;
                }
                f0 = f;
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {

                    x = dataBANK.AnomalcorTX[i];
                    u = (x - matspod) / kwadratvid;

                    if (u >= 0)
                    {
                        T = 1.0 / (1.0 + p * u);
                        f = 1.0 - (1.0 / (Math.Sqrt(2.0 * Math.PI))) * Math.Exp(-1.0 * (Math.Pow(u, 2) / 2.0)) * (b1 * T + b2 * Math.Pow(T, 2.0) + b3 * Math.Pow(T, 3.0) + b4 * Math.Pow(T, 4.0) + b5 * Math.Pow(T, 5.0)) + epsU;
                    }
                    else
                    {
                        T = 1.0 / (1.0 + p * Math.Abs(u));
                        f = 1.0 - (1.0 / (Math.Sqrt(2.0 * Math.PI))) * Math.Exp(-1.0 * (Math.Pow(u, 2) / 2.0)) * (b1 * T + b2 * Math.Pow(T, 2.0) + b3 * Math.Pow(T, 3.0) + b4 * Math.Pow(T, 4.0) + b5 * Math.Pow(T, 5.0)) + epsU;
                        f = 1.0 - f;
                    }
                    if (variant[i].variant <= shah + t)
                    {
                        sum += variant[i].chast;
                        //sum1 += variant[i].count;
                    }
                    else
                    {
                        f0 = f - f0;
                        f0 = f0 * nBig;
                        kofPirsona += Math.Pow(sum - f0, 2) / f0;
                        sum = variant[i].chast;
                        //sum1 = 0;
                        f0 = f;
                        t += shaht;
                    }
                    if (variant.Count - 1 == i)
                    {
                        break;
                    }

                }
                // MessageBox.Show($"{kofPirsona}");
                dataGridView3.Columns.Add("11", "Критеріїзгоди");
                dataGridView3.Columns.Add("11", "Значення");
                dataGridView3.Columns.Add("11", "Критичнезначення");
                dataGridView3.Columns.Add("11", "Висновок");
                double ALPHAZHODA = 0;
                double kritical = 0;
                double Pzet = 1.0 - Program.kolmogorov_zgody(dataBANK.AnomalcorTX);
                double expirs;
                if (nBig > 100)
                {
                    ALPHAZHODA = 0.05;
                    kritical = 1.36;
                    expirs = 113.3;

                }
                else if (nBig < 30)
                {
                    ALPHAZHODA = 0.3;
                    kritical = 0.97;
                    expirs = 29.3;
                }
                else
                {
                    ALPHAZHODA = 0.15;
                    kritical = 1.14;
                    expirs = 69.3;
                }

                if (Pzet >= ALPHAZHODA)
                {
                    dataGridView3.Rows.Add("Колмогорова", Math.Round(Program.kolmogorov_zgody(dataBANK.AnomalcorTX), 4), Math.Round(kritical, 3), "+");
                }
                else
                {
                    dataGridView3.Rows.Add("Колмогорова", Math.Round(Program.kolmogorov_zgody(dataBANK.AnomalcorTX), 4), Math.Round(kritical, 3), "-");
                }

                if (kofPirsona < Math.Pow(expirs, 2))
                {
                    dataGridView3.Rows.Add("Пірсона", Math.Round(kofPirsona, 3), Math.Round(Math.Pow(expirs, 2), 3), "+");
                }
                else
                {
                    dataGridView3.Rows.Add("Пірсона", Math.Round(kofPirsona, 3), Math.Round(Math.Pow(expirs, 2), 3), "-");
                }

                double KVANTIL = 0;
                ///довірчийінтервал
                if (Double.TryParse(textBox3.Text, out double KW2))
                {
                    KVANTIL = KW2;
                }
                else
                {
                    KVANTIL = 1.96;
                }
                double Dm = Math.Pow(kwadratvid, 2) / (double)dataBANK.AnomalcorTX.Count;
                double Dq = Math.Pow(kwadratvid, 2) / (2.0 * (double)dataBANK.AnomalcorTX.Count);
                double DFDm = 0;
                double dfdq = 0;
                double FFF = 0;
                if (Vflag)
                {
                    double F11 = 0;
                    double F22 = 0;
                    for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                    {
                        x = dataBANK.AnomalcorTX[i];
                        u = (x - matspod) / kwadratvid;
                        if (u >= 0)
                        {
                            T = 1.0 / (1.0 + p * u);
                            f = 1.0 - (1.0 / (Math.Sqrt(2.0 * Math.PI))) * Math.Exp(-1.0 * (Math.Pow(u, 2) / 2.0)) * (b1 * T + b2 * Math.Pow(T, 2.0) + b3 * Math.Pow(T, 3.0) + b4 * Math.Pow(T, 4.0) + b5 * Math.Pow(T, 5.0)) + epsU;

                        }
                        else
                        {
                            T = 1.0 / (1.0 + p * Math.Abs(u));
                            f = 1.0 - (1.0 / (Math.Sqrt(2.0 * Math.PI))) * Math.Exp(-1.0 * (Math.Pow(u, 2) / 2.0)) * (b1 * T + b2 * Math.Pow(T, 2.0) + b3 * Math.Pow(T, 3.0) + b4 * Math.Pow(T, 4.0) + b5 * Math.Pow(T, 5.0)) + epsU;
                            f = 1.0 - f;

                        }
                        DFDm = (-1.0) * (1.0 / (kwadratvid * Math.Sqrt(2.0 * Math.PI))) * Math.Exp(-1.0 * (Math.Pow(x - matspod, 2.0) / (2.0 * Math.Pow(kwadratvid, 2))));
                        dfdq = (-1.0) * ((x - matspod) / (Math.Pow(kwadratvid, 2) * Math.Sqrt(2.0 * Math.PI))) * Math.Exp(-1.0 * (Math.Pow(x - matspod, 2.0) / (2.0 * Math.Pow(kwadratvid, 2))));
                        FFF = Math.Pow(DFDm, 2) * Dm + Math.Pow(dfdq, 2) * Dq;
                        FFF = Math.Sqrt(FFF);
                        F11 = f + KVANTIL * FFF;
                        F22 = f - KVANTIL * FFF;
                        chart2.Series[3].Points.AddXY(x, F11);
                        chart2.Series[4].Points.AddXY(x, F22);
                    }
                }

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                //експоненціальнийрозподіл
                kilkclass = (int)numericUpDown1.Value;

                if (numericUpDown1.Value == 0)
                {
                    if (dataBANK.AnomalcorTX.Count <= 100)
                    {
                        kilkclass = (int)Math.Sqrt(dataBANK.AnomalcorTX.Count);
                    }
                    else
                    {
                        kilkclass = (int)Math.Pow(dataBANK.AnomalcorTX.Count, 0.3333333);
                    }
                }
                else
                {
                    kilkclass = (int)numericUpDown1.Value;
                }
                double sered = dataBANK.AnomalcorTX[dataBANK.AnomalcorTX.Count - 1] - dataBANK.AnomalcorTX[0];

                double shah = sered / kilkclass;
                double lamda = 1.0 / matspod;
                double f = 0;
                double x = 0;
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    x = dataBANK.AnomalcorTX[i];
                    f = lamda * Math.Exp(-(x * lamda));
                    f = f * shah;
                    chart1.Series[1].Points.AddXY(x, f);
                    //MessageBox.Show($"{kwadratvid}\n {m}");
                }
                List<double> kolmogorov = new List<double>();
                //List<double>teorchasto = new List<double>();
                //List<double> kolmogorov2 = new List<double>();
                double u = 0;
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    x = dataBANK.AnomalcorTX[i];
                    u += dataBANK.AnomalChast[i];

                    f = 1.0 - Math.Exp(-1.0 * (x * lamda));

                    chart2.Series[2].Points.AddXY(x, f);
                    double add = Math.Abs(u - f);
                    kolmogorov.Add(add);
                }
                //колмогоров
                double Dplus = kolmogorov.Max();
                double Dminus = 0;
                double z = 0;
                if (Dplus >= Dminus)
                {
                    z = Math.Sqrt(dataBANK.AnomalcorTX.Count) * Dplus;
                }
                else
                {
                    z = Math.Sqrt(dataBANK.AnomalcorTX.Count) * Dminus;
                }

                double Kz = 0;
                for (double k = 1.0; k <= 4.0; k++)
                {
                    double f2 = 5.0 * Math.Pow(k, 2) + 22.0 - 7.5 * (1.0 - Math.Pow(-1.0, k));
                    double f1 = Math.Pow(k, 2) - 0.5 * (1.0 - Math.Pow(-1.0, k));
                    Kz += (Math.Exp(-2.0 * (Math.Pow(k, 2.0) * Math.Pow(z, 2.0))) * (1.0 - ((2.0 * Math.Pow(k, 2) * z) / (3.0 * Math.Sqrt(nBig))) - (1.0 / (18.0 * nBig)) *
                                            ((f1 - 4.0 * (f1 + 3.0)) * Math.Pow(k, 2.0) * Math.Pow(z, 2.0) + (8.0 * Math.Pow(k, 4.0) * Math.Pow(z, 4.0))) + ((Math.Pow(k, 2.0) * z) / (27.0 * Math.Sqrt(Math.Pow(nBig, 3.0))))
                                            * ((Math.Pow(f2, 2) / 5.0) - ((4.0 * (f2 + 45.0) * Math.Pow(k, 2.0) * Math.Pow(z, 2.0)) / (15.0)) + (8.0 * Math.Pow(k, 4.0) * Math.Pow(z, 4.0)))));
                    Kz = Math.Pow(-1.0, k) * Kz;
                }


                Kz = 1.0 + (2.0 * Kz);
                /// критерійпірсона
                sered = variant[variant.Count - 1].variant - variant[0].variant;

                shah = sered / kilkclass;

                double shaht = shah;
                shah += variant[0].variant;
                double sum = 0;
                double t = 0;
                double kofPirsona = 0;
                //double sum1 = 0;
                double f0 = 0;
                x = dataBANK.AnomalcorTX[0];

                f = 1.0 - Math.Exp(-1.0 * (x * lamda));
                f0 = f;
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {

                    x = dataBANK.AnomalcorTX[i];
                    f = 1.0 - Math.Exp(-1.0 * (x * lamda));
                    if (variant[i].variant <= shah + t)
                    {
                        sum += variant[i].chast;
                        //sum1 += variant[i].count;
                    }
                    else
                    {
                        f0 = f - f0;
                        f0 = f0 * nBig;
                        kofPirsona += Math.Pow(sum - f0, 2) / f0;
                        sum = variant[i].chast;
                        //sum1 = 0;
                        f0 = f;
                        t += shaht;
                    }
                    if (variant.Count - 1 == i)
                    {
                        break;
                    }
                }
                dataGridView3.Columns.Add("11", "Критеріїзгоди");
                dataGridView3.Columns.Add("11", "Значення");
                dataGridView3.Columns.Add("11", "Критичнезначення");
                dataGridView3.Columns.Add("11", "Висновок");
                double ALPHAZHODA = 0;
                double kritical = 0;
                double Pzet = 1.0 - Kz;
                double expirs;
                if (nBig > 100)
                {
                    ALPHAZHODA = 0.05;
                    kritical = 1.36;
                    expirs = 113.3;

                }
                else if (nBig < 30)
                {
                    ALPHAZHODA = 0.3;
                    kritical = 0.97;
                    expirs = 29.3;
                }
                else
                {
                    ALPHAZHODA = 0.15;
                    kritical = 1.14;
                    expirs = 69.3;
                }

                if (Pzet >= ALPHAZHODA)
                {
                    dataGridView3.Rows.Add("Колмогорова", Math.Round(Kz, 3), Math.Round(kritical, 3), "+");
                }
                else
                {
                    dataGridView3.Rows.Add("Колмогорова", Math.Round(Kz, 3), Math.Round(kritical, 3), "-");
                }

                if (kofPirsona < Math.Pow(expirs, 2))
                {
                    dataGridView3.Rows.Add("Пірсона", Math.Round(kofPirsona, 3), Math.Round(Math.Pow(expirs, 2), 3), "+");
                }
                else
                {
                    dataGridView3.Rows.Add("Пірсона", Math.Round(kofPirsona, 3), Math.Round(Math.Pow(expirs, 2), 3), "-");
                }
                dataGridView4.Columns.Add("345", "Параметр");
                dataGridView4.Columns.Add("345", "Значення");
                dataGridView4.Rows.Add("лямбда", Math.Round(lamda, 3));

                if (Vflag)
                {
                    //експоненціальний
                    double KVANTIL;
                    if (Double.TryParse(textBox3.Text, out double KW2))
                    {
                        KVANTIL = KW2;
                    }
                    else
                    {
                        KVANTIL = 1.96;
                    }

                    lamda = 1.0 / matspod;
                    f = 0;
                    double FUNC = 0;
                    double f2 = 0;
                    x = 0;
                    double d = 0;
                    double bigN = (double)dataBANK.AnomalChast.Count;

                    for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                    {
                        //MessageBox.Show("FGHJK");
                        x = dataBANK.AnomalcorTX[i];
                        d = Math.Pow(dataBANK.AnomalcorTX[i], 2) * Math.Exp(-2.0 * lamda * dataBANK.AnomalcorTX[i]) * (Math.Pow(lamda, 2) / bigN);
                        FUNC = 1.0 - Math.Exp(-1.0 * (x * lamda));
                        f = FUNC - (KVANTIL * Math.Sqrt(d));
                        f2 = FUNC + (KVANTIL * Math.Sqrt(d));
                        //MessageBox.Show($"{dataBANK.AnomalChast[i]} {dataBANK.AnomalcorTX[i]}");
                        chart2.Series[3].Points.AddXY(x, f);
                        chart2.Series[4].Points.AddXY(x, f2);
                        //MessageBox.Show($"{kwadratvid}\n {m}");
                    }
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {

                //розподілвейбулла
                kilkclass = (int)numericUpDown1.Value;

                if (numericUpDown1.Value == 0)
                {
                    if (variant.Count <= 100)
                    {
                        kilkclass = (int)Math.Sqrt(variant.Count);
                    }
                    else
                    {
                        kilkclass = (int)Math.Pow(variant.Count, 0.3333333);
                    }
                }
                else
                {
                    kilkclass = (int)numericUpDown1.Value;
                }
                double sered = variant[variant.Count - 1].variant - variant[0].variant;

                double shah = sered / kilkclass;
                double x = 0;


                double a21 = 0;
                double a22 = 0;
                double b1 = 0;
                double b2 = 0;
                double chas = 0;
                for (int i = 0; i < variant.Count - 1; i++)
                {
                    x = variant[i].variant;
                    a21 += Math.Log(x);
                    a22 += Math.Pow(Math.Log(x), 2);
                }
                for (int i = 0; i < variant.Count - 1; i++)
                {
                    x = variant[i].variant;
                    chas += variant[i].chast;
                    b1 += Math.Log(Math.Log((1.0) / (1.0 - chas)));
                    b2 += Math.Log(x) * (Math.Log(Math.Log(1.0 / (1.0 - chas))));
                }
                double a11 = (double)(variant.Count - 1.0);

                double kof = 1;
                double kof1 = 1;
                double kof2 = 1;

                kof = (a11 * a22) - (a21 * a21);

                kof1 = (b1 * a22) - (b2 * a21);
                kof2 = (a11 * b2) - (a21 * b1);
                double A = kof1 / kof;
                double beta = kof2 / kof;
                double alpha = Math.Exp(-1.0 * A);
                double Szal = 0;
                chas = 0;
                dataGridView4.Columns.Add("345", "Параметр");
                dataGridView4.Columns.Add("345", "Значення");
                dataGridView4.Rows.Add("Alpha", Math.Round(alpha, 3));
                dataGridView4.Rows.Add("Beta", Math.Round(beta, 3));
                for (int i = 0; i < variant.Count - 1; i++)
                {
                    x = variant[i].variant;
                    chas += variant[i].chast;
                    Szal += (1.0 / (double)(variant.Count - 3.0)) * Math.Pow(Math.Log(Math.Log((1.0) / (1.0 - chas))) - A - (beta * Math.Log(x)), 2);

                }

                double FUNC = 0;

                for (int i = 0; i < variant.Count - 1; i++)
                {
                    x = variant[i].variant;
                    FUNC = (beta / alpha) * Math.Pow(x, beta - 1.0) * Math.Exp(-1.0 * (Math.Pow(x, beta) / alpha));
                    FUNC = (FUNC * shah);
                    chart1.Series[1].Points.AddXY(x, FUNC);
                }
                List<double> kolmogorov = new List<double>();
                double add = 0;
                for (int i = 0; i < variant.Count - 1; i++)
                {
                    x = variant[i].variant;
                    add += variant[i].chast;
                    FUNC = 1.0 - Math.Exp(-1.0 * (Math.Pow(x, beta) / alpha));
                    double aad = Math.Abs(add - FUNC);
                    kolmogorov.Add(aad);
                    chart2.Series[2].Points.AddXY(x, FUNC);
                }
                //kolmogorov
                double Dplus = kolmogorov.Max();
                double Dminus = 0;
                double z = 0;
                if (Dplus >= Dminus)
                {
                    z = Math.Sqrt(nBig) * Dplus;
                }
                else
                {
                    z = Math.Sqrt(nBig) * Dminus;
                }

                double Kz = 0;
                for (double k = 1.0; k <= 4.0; k++)
                {
                    double f2 = 5.0 * Math.Pow(k, 2) + 22.0 - 7.5 * (1.0 - Math.Pow(-1.0, k));
                    double f1 = Math.Pow(k, 2) - 0.5 * (1.0 - Math.Pow(-1.0, k));
                    Kz += Math.Pow(-1.0, k) * Math.Exp(-2.0 * (Math.Pow(k, 2.0) * Math.Pow(z, 2.0))) * (1.0 - ((2.0 * Math.Pow(k, 2) * z) / (3.0 * Math.Sqrt(nBig))) - (1.0 / (18.0 * nBig)) *
                                            ((f1 - 4.0 * (f1 + 3.0)) * Math.Pow(k, 2.0) * Math.Pow(z, 2.0) + (8.0 * Math.Pow(k, 4.0) * Math.Pow(z, 4.0))) + ((Math.Pow(k, 2.0) * z) / (27.0 * Math.Sqrt(Math.Pow(nBig, 3.0))))
                                            * ((Math.Pow(f2, 2) / 5.0) - ((4.0 * (f2 + 45.0) * Math.Pow(k, 2.0) * Math.Pow(z, 2.0)) / (15.0)) + (8.0 * Math.Pow(k, 4.0) * Math.Pow(z, 4.0))));
                }


                Kz = 1.0 + (2.0 * Kz);
                /// критерійпірсона
                sered = variant[variant.Count - 1].variant - variant[0].variant;

                shah = sered / kilkclass;

                double shaht = shah;
                shah += variant[0].variant;
                double sum = 0;
                double t = 0;
                double kofPirsona = 0;
                //double sum1 = 0;
                double f0 = 0;
                x = variant[0].variant;
                FUNC = 1.0 - Math.Exp(-1.0 * (Math.Pow(x, beta) / alpha));
                f0 = FUNC;
                for (int i = 0; i < variant.Count; i++)
                {

                    x = variant[i].variant;
                    FUNC = 1.0 - Math.Exp(-1.0 * (Math.Pow(x, beta) / alpha));
                    if (variant[i].variant <= shah + t)
                    {
                        sum += variant[i].chast;
                        //sum1 += variant[i].count;
                    }
                    else
                    {
                        f0 = FUNC - f0;
                        f0 = f0 * nBig;
                        kofPirsona += Math.Pow(sum - f0, 2) / f0;
                        sum = variant[i].chast;
                        //sum1 = 0;
                        f0 = FUNC;
                        t += shaht;
                    }
                    if (variant.Count - 1 == i)
                    {
                        break;
                    }
                }
                dataGridView3.Columns.Add("11", "Критеріїзгоди");
                dataGridView3.Columns.Add("11", "Значення");
                dataGridView3.Columns.Add("11", "Критичнезначення");
                dataGridView3.Columns.Add("11", "Висновок");
                double ALPHAZHODA = 0;
                double kritical = 0;
                double Pzet = 1.0 - Kz;
                double expirs;
                if (nBig > 100)
                {
                    ALPHAZHODA = 0.05;
                    kritical = 1.36;
                    expirs = 113.3;

                }
                else if (nBig < 30)
                {
                    ALPHAZHODA = 0.3;
                    kritical = 0.97;
                    expirs = 29.3;
                }
                else
                {
                    ALPHAZHODA = 0.15;
                    kritical = 1.14;
                    expirs = 69.3;
                }

                if (Pzet >= ALPHAZHODA)
                {
                    dataGridView3.Rows.Add("Колмогорова", Math.Round(Kz, 3), Math.Round(kritical, 3), "+");
                }
                else
                {
                    dataGridView3.Rows.Add("Колмогорова", Math.Round(Kz, 3), Math.Round(kritical, 3), "-");
                }

                if (kofPirsona < Math.Pow(expirs, 2))
                {
                    dataGridView3.Rows.Add("Пірсона", Math.Round(kofPirsona, 3), Math.Round(Math.Pow(expirs, 2), 3), "+");
                }
                else
                {
                    dataGridView3.Rows.Add("Пірсона", Math.Round(kofPirsona, 3), Math.Round(Math.Pow(expirs, 2), 3), "-");
                }

                double dFda = 0;
                double dFdb = 0;
                double DA = (a22 * Szal) / ((a11 * a22) - (a21 * a21));
                double COVAB = (-1.0) * ((a21 * Szal) / ((a11 * a22) - (a21 * a21)));
                double COVab = (-1.0) * Math.Exp(A) * COVAB;
                double Dalpha = Math.Exp(-2.0 * A) * DA;
                double Dbeta = (a11 * Szal) / ((a11 * a22) - (a21 * a21));
                double KVANTIL;
                if (Double.TryParse(textBox3.Text, out double KW2))
                {
                    KVANTIL = KW2;
                }
                else
                {
                    KVANTIL = 1.96;
                }
                if (Vflag)
                {
                    double Dff = 0;
                    double f11;
                    double f22;
                    for (int i = 0; i < variant.Count; i++)
                    {
                        x = variant[i].variant;
                        FUNC = 1.0 - Math.Exp(-1.0 * (Math.Pow(x, beta) / alpha));
                        dFda = -1.0 * (Math.Pow(x, beta) / Math.Pow(alpha, 2)) * Math.Exp(-1.0 * (Math.Pow(x, beta) / alpha));
                        dFdb = (Math.Pow(x, beta) / alpha) * Math.Log(x) * Math.Exp(-1.0 * (Math.Pow(x, beta) / alpha));
                        Dff = Math.Pow(dFda, 2) * Dalpha + Math.Pow(dFdb, 2) * Dbeta + (2.0 * dFda * dFdb * COVab);
                        Dff = Math.Sqrt(Dff);
                        f11 = FUNC + (KVANTIL * Dff);
                        f22 = FUNC - (KVANTIL * Dff);
                        chart2.Series[3].Points.AddXY(x, f11);
                        chart2.Series[4].Points.AddXY(x, f22);
                    }


                }

            }
            else if (comboBox1.SelectedIndex == 3)
            {
                //розподілрівномірний
                kilkclass = (int)numericUpDown1.Value;

                if (numericUpDown1.Value == 0)
                {
                    if (dataBANK.AnomalcorTX.Count <= 100)
                    {
                        kilkclass = (int)Math.Sqrt(dataBANK.AnomalcorTX.Count);
                    }
                    else
                    {
                        kilkclass = (int)Math.Pow(dataBANK.AnomalcorTX.Count, 0.3333333);
                    }
                }
                else
                {
                    kilkclass = (int)numericUpDown1.Value;
                }
                double sered = dataBANK.AnomalcorTX[dataBANK.AnomalcorTX.Count - 1] - dataBANK.AnomalcorTX[0];

                double shah = sered / kilkclass;
                double lamda = 1.0 / matspod;
                double f = 0;
                double x = 0;
                double a = dataBANK.AnomalcorTX[0];
                double b = dataBANK.AnomalcorTX[dataBANK.AnomalcorTX.Count - 1];
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    x = dataBANK.AnomalcorTX[i];
                    f = 1.0 / (b - a);
                    f = f * shah;
                    chart1.Series[1].Points.AddXY(x, f);
                    //MessageBox.Show($"{kwadratvid}\n {m}");
                }
                double newmaspod = 0;
                dataGridView4.Columns.Add("345", "Параметр");
                dataGridView4.Columns.Add("345", "Значення");
                dataGridView4.Rows.Add("a", Math.Round(a, 3));
                dataGridView4.Rows.Add("b", Math.Round(b, 3));
                List<double> kolmogorov = new List<double>();
                double rfd = 0;
                for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                {
                    x = dataBANK.AnomalcorTX[i];
                    rfd += dataBANK.AnomalChast[i];

                    f = (x - a) / (b - a);
                    newmaspod += Math.Pow(x, 2);
                    chart2.Series[2].Points.AddXY(x, f);
                    double addd = Math.Abs(f - rfd);
                    kolmogorov.Add(addd);

                }

                //kolmogorov
                double Dplus = kolmogorov.Max();
                double Dminus = 0;
                double z = 0;
                if (Dplus >= Dminus)
                {
                    z = Math.Sqrt(dataBANK.AnomalcorTX.Count) * Dplus;
                }
                else
                {
                    z = Math.Sqrt(dataBANK.AnomalcorTX.Count) * Dminus;
                }

                double Kz = 0;
                for (double k = 1.0; k <= 4.0; k++)
                {
                    double f2 = 5.0 * Math.Pow(k, 2) + 22.0 - 7.5 * (1.0 - Math.Pow(-1.0, k));
                    double f1 = Math.Pow(k, 2) - 0.5 * (1.0 - Math.Pow(-1.0, k));
                    Kz += Math.Pow(-1.0, k) * Math.Exp(-2.0 * (Math.Pow(k, 2.0) * Math.Pow(z, 2.0))) * (1.0 - ((2.0 * Math.Pow(k, 2) * z) / (3.0 * Math.Sqrt(nBig))) - (1.0 / (18.0 * nBig)) *
                                            ((f1 - 4.0 * (f1 + 3.0)) * Math.Pow(k, 2.0) * Math.Pow(z, 2.0) + (8.0 * Math.Pow(k, 4.0) * Math.Pow(z, 4.0))) + ((Math.Pow(k, 2.0) * z) / (27.0 * Math.Sqrt(Math.Pow(nBig, 3.0))))
                                            * ((Math.Pow(f2, 2) / 5.0) - ((4.0 * (f2 + 45.0) * Math.Pow(k, 2.0) * Math.Pow(z, 2.0)) / (15.0)) + (8.0 * Math.Pow(k, 4.0) * Math.Pow(z, 4.0))));
                }


                Kz = 1.0 + (2.0 * Kz);
                /// критерійпірсона
                sered = variant[variant.Count - 1].variant - variant[0].variant;

                shah = sered / kilkclass;

                double shaht = shah;
                shah += variant[0].variant;
                double sum = 0;
                double t = 0;
                double kofPirsona = 0;
                //double sum1 = 0;
                double f0 = 0;
                x = variant[0].variant;
                f = (x - a) / (b - a);
                f0 = f;
                for (int i = 0; i < variant.Count; i++)
                {

                    x = variant[i].variant;
                    f = (x - a) / (b - a);
                    if (variant[i].variant <= shah + t)
                    {
                        sum += variant[i].chast;
                        //sum1 += variant[i].count;
                    }
                    else
                    {
                        f0 = f - f0;
                        f0 = f0 * nBig;
                        kofPirsona += Math.Pow(sum - f0, 2) / f0;
                        sum = variant[i].chast;
                        //sum1 = 0;
                        f0 = f;
                        t += shaht;
                    }
                    if (variant.Count - 1 == i)
                    {
                        break;
                    }
                }
                double nnn = (double)dataBANK.AnomalcorTX.Count;
                newmaspod = newmaspod / dataBANK.AnomalcorTX.Count;
                double Alabmd = matspod - Math.Sqrt(3.0 * (newmaspod - Math.Pow(matspod, 2.0)));
                double Blamda = matspod + Math.Sqrt(3.0 * (newmaspod - Math.Pow(matspod, 2.0)));
                double dH1x1 = 1.0 + 3.0 * ((Alabmd + Blamda) / (Blamda - Alabmd));
                double dH2x1 = 1.0 - 3.0 * ((Alabmd + Blamda) / (Blamda - Alabmd));
                double dh1x2 = (-1.0) * (3.0 / (Blamda - Alabmd));
                double dh2x2 = (3.0 / (Blamda - Alabmd));
                double dispX1 = Math.Pow(Blamda - Alabmd, 2) / (12.0 * nnn);
                double COVx1x2 = ((Alabmd + Blamda) * Math.Pow(Blamda - Alabmd, 2)) / (12.0 * nnn);
                double disX2 = (1.0 / (180.0 * nnn)) * (Math.Pow(Blamda - Alabmd, 4.0) + 15.0 * Math.Pow(Alabmd + Blamda, 2.0) * Math.Pow(Blamda - Alabmd, 2.0));
                double KVANTIL = 0;
                ///довірчийінтервал
                if (Double.TryParse(textBox3.Text, out double KW2))
                {
                    KVANTIL = KW2;
                }
                else
                {
                    KVANTIL = 1.96;
                }
                double covAB = (dH1x1 * dH2x1 * dispX1) + (dh1x2 * dh2x2 * disX2) + (dH1x1 * dh2x2 + dh1x2 * dH2x1) * COVx1x2;
                double disA = Math.Pow(dH1x1, 2) * dispX1 + Math.Pow(dh1x2, 2) * disX2 + 2.0 * (dH1x1 * dh1x2) * COVx1x2;
                double disB = Math.Pow(dH2x1, 2) * dispX1 + Math.Pow(dh2x2, 2) * disX2 + 2.0 * (dH2x1 * dh2x2) * COVx1x2;
                double Fdisp = 0;
                if (Vflag)
                {
                    double f11 = 0;
                    double f22 = 0;
                    for (int i = 0; i < dataBANK.AnomalcorTX.Count; i++)
                    {
                        x = dataBANK.AnomalcorTX[i];
                        f = (x - a) / (b - a);
                        Fdisp = (Math.Pow(x - Blamda, 2) / Math.Pow(Blamda - Alabmd, 4)) * disA + (Math.Pow(x - Alabmd, 2) / Math.Pow(Blamda - Alabmd, 4)) * disB - 2.0 * (((x - Alabmd) * (x - Blamda)) / Math.Pow(Blamda - Alabmd, 4)) * covAB;
                        Fdisp = Math.Sqrt(Fdisp);
                        f11 = f + KVANTIL * Fdisp;
                        f22 = f - KVANTIL * Fdisp;

                        chart2.Series[3].Points.AddXY(x, f11);
                        chart2.Series[4].Points.AddXY(x, f22);

                    }
                }
                dataGridView3.Columns.Add("11", "Критеріїзгоди");
                dataGridView3.Columns.Add("11", "Значення");
                dataGridView3.Columns.Add("11", "Критичнезначення");
                dataGridView3.Columns.Add("11", "Висновок");
                double ALPHAZHODA = 0;
                double kritical = 0;
                double Pzet = 1.0 - Kz;
                double expirs;
                if (nBig > 100)
                {
                    ALPHAZHODA = 0.05;
                    kritical = 1.36;
                    expirs = 113.3;

                }
                else if (nBig < 30)
                {
                    ALPHAZHODA = 0.3;
                    kritical = 0.97;
                    expirs = 29.3;
                }
                else
                {
                    ALPHAZHODA = 0.15;
                    kritical = 1.14;
                    expirs = 69.3;
                }

                if (Pzet >= ALPHAZHODA)
                {
                    dataGridView3.Rows.Add("Колмогорова", Math.Round(Kz, 3), Math.Round(kritical, 3), "+");
                }
                else
                {
                    dataGridView3.Rows.Add("Колмогорова", Math.Round(Kz, 3), Math.Round(kritical, 3), "-");
                }

                if (kofPirsona < Math.Pow(expirs, 2))
                {
                    dataGridView3.Rows.Add("Пірсона", Math.Round(kofPirsona, 3), Math.Round(Math.Pow(expirs, 2), 3), "+");
                }
                else
                {
                    dataGridView3.Rows.Add("Пірсона", Math.Round(kofPirsona, 3), Math.Round(Math.Pow(expirs, 2), 3), "-");
                }
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                /// розподілзкласуекстремальних
                kilkclass = (int)numericUpDown1.Value;

                if (numericUpDown1.Value == 0)
                {
                    if (variant.Count <= 100)
                    {
                        kilkclass = (int)Math.Sqrt(variant.Count);
                    }
                    else
                    {
                        kilkclass = (int)Math.Pow(variant.Count, 0.3333333);
                    }
                }
                else
                {
                    kilkclass = (int)numericUpDown1.Value;
                }
                double sered = variant[variant.Count - 1].variant - variant[0].variant;

                double shah = sered / kilkclass;
                double x = 0;

                double a21 = 0;
                double a22 = 0;
                double b1 = 0;
                double b2 = 0;
                double chas = 0;
                for (int i = 0; i < variant.Count - 1; i++)
                {
                    x = variant[i].variant;
                    a21 += Math.Log(x);
                    chas += variant[i].chast;
                    a22 += Math.Pow(Math.Log(x), 2);
                    b1 += Math.Log(Math.Log(1.0 + (Math.Log(1.0 / (1.0 - chas)))));
                    b2 += Math.Log(x) * Math.Log(Math.Log(1.0 + (Math.Log(1.0 / (1.0 - chas)))));
                }

                double a11 = (double)(variant.Count - 1.0);


                double kof = 1;
                double kof1 = 1;
                double kof2 = 1;

                kof = (a11 * a22) - (a21 * a21);

                kof1 = (b1 * a22) - (b2 * a21);
                kof2 = (a11 * b2) - (a21 * b1);

                double A = kof1 / kof;
                double beta = kof2 / kof;
                double B = Math.Exp(A);
                double FUNC = 0;
                dataGridView4.Columns.Add("345", "Параметр");
                dataGridView4.Columns.Add("345", "Значення");
                dataGridView4.Rows.Add("B", Math.Round(B, 3));
                dataGridView4.Rows.Add("Beta", Math.Round(beta, 3));
                for (int i = 0; i < variant.Count; i++)
                {
                    x = variant[i].variant;
                    FUNC = B * beta * Math.Pow(x, beta - 1) * Math.Exp(-1.0 * (Math.Exp(B * Math.Pow(x, beta)) - 1.0)) * Math.Exp(B * Math.Pow(x, beta));
                    FUNC = FUNC * shah;
                    chart1.Series[1].Points.AddXY(x, FUNC);
                    //MessageBox.Show($"{kwadratvid}\n {m}");
                }
                double ards = 0;
                List<double> kolmogorov = new List<double>();
                for (int i = 0; i < variant.Count; i++)
                {
                    x = variant[i].variant;
                    FUNC = 1.0 - Math.Exp(-1.0 * (Math.Exp(B * Math.Pow(x, beta)) - 1.0));
                    ards += variant[i].chast;
                    double rff = Math.Abs(FUNC - ards);
                    chart2.Series[2].Points.AddXY(x, FUNC);
                    kolmogorov.Add(rff);
                }
                //kolmogorov
                double Dplus = kolmogorov.Max();
                double Dminus = 0;
                double z = 0;
                if (Dplus >= Dminus)
                {
                    z = Math.Sqrt(dataBANK.AnomalcorTX.Count) * Dplus;
                }
                else
                {
                    z = Math.Sqrt(dataBANK.AnomalcorTX.Count) * Dminus;
                }

                double Kz = 0;
                for (double k = 1.0; k <= 4.0; k++)
                {
                    double f2 = 5.0 * Math.Pow(k, 2) + 22.0 - 7.5 * (1.0 - Math.Pow(-1.0, k));
                    double f1 = Math.Pow(k, 2) - 0.5 * (1.0 - Math.Pow(-1.0, k));
                    Kz += Math.Pow(-1.0, k) * Math.Exp(-2.0 * (Math.Pow(k, 2.0) * Math.Pow(z, 2.0))) * (1.0 - ((2.0 * Math.Pow(k, 2) * z) / (3.0 * Math.Sqrt(nBig))) - (1.0 / (18.0 * nBig)) *
                                            ((f1 - 4.0 * (f1 + 3.0)) * Math.Pow(k, 2.0) * Math.Pow(z, 2.0) + (8.0 * Math.Pow(k, 4.0) * Math.Pow(z, 4.0))) + ((Math.Pow(k, 2.0) * z) / (27.0 * Math.Sqrt(Math.Pow(nBig, 3.0))))
                                            * ((Math.Pow(f2, 2) / 5.0) - ((4.0 * (f2 + 45.0) * Math.Pow(k, 2.0) * Math.Pow(z, 2.0)) / (15.0)) + (8.0 * Math.Pow(k, 4.0) * Math.Pow(z, 4.0))));
                }
                Kz = 1.0 + (2.0 * Kz);
                /// критерійпірсона
                sered = variant[variant.Count - 1].variant - variant[0].variant;

                shah = sered / kilkclass;

                double shaht = shah;
                shah += variant[0].variant;
                double sum = 0;
                double t = 0;
                double kofPirsona = 0;
                //double sum1 = 0;
                double f0 = 0;
                x = variant[0].variant;
                FUNC = 1.0 - Math.Exp(-1.0 * (Math.Exp(B * Math.Pow(x, beta)) - 1.0));
                f0 = FUNC;
                for (int i = 0; i < variant.Count; i++)
                {

                    x = variant[i].variant;
                    FUNC = 1.0 - Math.Exp(-1.0 * (Math.Exp(B * Math.Pow(x, beta)) - 1.0));
                    if (variant[i].variant <= shah + t)
                    {
                        sum += variant[i].chast;
                    }
                    else
                    {
                        f0 = FUNC - f0;
                        f0 = f0 * nBig;
                        kofPirsona += Math.Pow(sum - f0, 2) / f0;
                        sum = variant[i].chast;
                        //sum1 = 0;
                        f0 = FUNC;
                        t += shaht;
                    }
                    if (variant.Count - 1 == i)
                    {
                        break;
                    }
                }
                dataGridView3.Columns.Add("11", "Критеріїзгоди");
                dataGridView3.Columns.Add("11", "Значення");
                dataGridView3.Columns.Add("11", "Критичнезначення");
                dataGridView3.Columns.Add("11", "Висновок");
                double ALPHAZHODA = 0;
                double kritical = 0;
                double Pzet = 1.0 - Program.kolmogorov_zgody(dataBANK.AnomalcorTX);
                double expirs;
                if (nBig > 100)
                {
                    ALPHAZHODA = 0.05;
                    kritical = 1.36;
                    expirs = 113.3;

                }
                else if (nBig < 30)
                {
                    ALPHAZHODA = 0.3;
                    kritical = 0.97;
                    expirs = 29.3;
                }
                else
                {
                    ALPHAZHODA = 0.15;
                    kritical = 1.14;
                    expirs = 69.3;
                }

                if (Pzet >= ALPHAZHODA)
                {
                    dataGridView3.Rows.Add("Колмогорова", Math.Round(Program.kolmogorov_zgody(dataBANK.AnomalcorTX), 3), Math.Round(kritical, 3), "+");
                }
                else
                {
                    dataGridView3.Rows.Add("Колмогорова", Math.Round(Program.kolmogorov_zgody(dataBANK.AnomalcorTX), 3), Math.Round(kritical, 3), "-");
                }

                if (kofPirsona < Math.Pow(expirs, 2))
                {
                    dataGridView3.Rows.Add("Пірсона", Math.Round(kofPirsona, 3), Math.Round(Math.Pow(expirs, 2), 3), "+");
                }
                else
                {
                    dataGridView3.Rows.Add("Пірсона", Math.Round(kofPirsona, 3), Math.Round(Math.Pow(expirs, 2), 3), "-");
                }
                double KVANTIL;
                if (Double.TryParse(textBox3.Text, out double KW2))
                {
                    KVANTIL = KW2;
                }
                else
                {
                    KVANTIL = 1.96;
                }
                if (Vflag)
                {
                    double Dfbeta = 0;
                    double DfB = 0;
                    double f11;
                    double f22;
                    double diszal = 0;
                    double zll = 0;

                    for (int i = 0; i < variant.Count - 1; i++)
                    {
                        x = variant[i].variant;
                        FUNC = 1.0 - Math.Exp(-1.0 * (Math.Exp(B * Math.Pow(x, beta)) - 1.0));
                        zll = Math.Log(Math.Log(1.0 + Math.Log((1.0) / (1.0 - FUNC))));
                        diszal += ((1.0) / (nBig - 3.0)) * Math.Pow((zll - Math.Log(B) - beta * Math.Log(x)), 2);
                    }
                    double DA = (a22 * diszal) / ((a11 * a22) - (a21 * a21));
                    double COVAB = (-1.0) * ((a21 * diszal) / ((a11 * a22) - (a21 * a21)));

                    double Dbeta = (a11 * diszal) / ((a11 * a22) - (a21 * a21));
                    double covBbeta = COVAB * B;
                    double DISoc = 0;
                    double dB = Math.Pow(B, 2) * DA;
                    for (int i = 0; i < variant.Count; i++)
                    {
                        x = variant[i].variant;
                        FUNC = 1.0 - Math.Exp(-1.0 * (Math.Exp(B * Math.Pow(x, beta)) - 1.0));
                        DfB = Math.Pow(x, beta) * Math.Exp(-1.0 * (Math.Exp(B * Math.Pow(x, beta)) - 1.0)) * Math.Exp(B * Math.Pow(x, beta));
                        Dfbeta = B * Math.Pow(x, beta) * Math.Log(x) * Math.Exp(-1.0 * (Math.Exp(B * Math.Pow(x, beta)) - 1.0)) * Math.Exp(B * Math.Pow(x, beta));
                        DISoc = Math.Pow(DfB, 2) * dB + Math.Pow(Dfbeta, 2) * Dbeta + (2.0 * DfB * Dfbeta) * covBbeta;
                        f11 = FUNC + (KVANTIL * DISoc);
                        f22 = FUNC - (KVANTIL * DISoc);
                        chart2.Series[3].Points.AddXY(x, f11);
                        chart2.Series[4].Points.AddXY(x, f22);
                    }
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        public int count_of_columns = 0;
        public DataSet ds = new DataSet();
        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            var fileContent = string.Empty;

            string filename = openFileDialog1.FileName;


            if (count_of_columns == 0)
            {
                ds.Tables.Add("Score");

            }
            vibirku_var.Clear();
            not_sortData.Clear();
            var fileStream = openFileDialog1.OpenFile();

            string header = "";
            using (StreamReader reade1 = new StreamReader(fileStream))
            {
                fileContent = reade1.ReadLine();
                header = fileContent;

            }

            string[] col = Program.strochka(header);
            int c1_of_cycle = count_of_columns;
            for (int c = c1_of_cycle; c < col.Length + c1_of_cycle; c++)
            {
                ds.Tables[0].Columns.Add($"{c + 1}");
                count_of_columns++;
            }
            fileStream.Close();
            fileStream = openFileDialog1.OpenFile();


            int rows_for_loop = 0;

            if (c1_of_cycle == 0)
            {
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
                        ds.Tables[0].Rows.Add(rvalue);
                        rows_for_loop++;
                        fileContent = reader.ReadLine();
                    }

                }
                for (int i = 0; i < 25000 - rows_for_loop; i++)
                {
                    ds.Tables[0].Rows.Add();
                }
            }
            else
            {
                int counter_of = 0;
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
                        //ds.Tables[0].Rows.Add(rvalue);
                        int intovi_strochi = 0;
                        for (int t = c1_of_cycle; t < dataGridView5.Columns.Count; t++)
                        {

                            dataGridView5.Rows[counter_of].Cells[t].Value = rvalue[intovi_strochi];
                            intovi_strochi++;
                        }
                        counter_of++;
                        fileContent = reader.ReadLine();
                    }

                }
            }

            dataGridView5.AllowUserToAddRows = false;
            comboBox2.Items.Clear();
            dataGridView5.DataSource = ds.Tables[0];
            double nBig = dataGridView5.Rows.Count;
            double chast;
            //////списки
            ///
            not_sortData.Clear();
            vibirku_var.Clear();
            comboBox2.Items.Clear();
            //norm_or_notnorm.Clear();
            for (int t = 0; t < dataGridView5.Columns.Count; t++)
            {
                normclass g12 = new normclass();
                g12.norm = false;
                g12.nomer_vibirk =t+1;
                norm_or_notnorm.Add(g12);
                List<double> numberrs = new List<double>();
                List<rows> variant = new List<rows>();
                comboBox2.Items.Add($"{t + 1}");
                variant.Clear();
                nBig = 0;
                for (int i = 0; i < dataGridView5.Rows.Count; i++)
                {
                    if (Double.TryParse(dataGridView5.Rows[i].Cells[t].Value.ToString(), out double restik))
                    {
                        nBig++;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = 0; i < dataGridView5.Rows.Count; i++)
                {
                    rows rows1 = new rows();
                    double comp = 0;
                    if (Double.TryParse(dataGridView5.Rows[i].Cells[t].Value.ToString(), out double restik))
                    {
                        comp = restik;
                    }
                    else
                    {
                        continue;
                    }
                    //Double.TryParse(dataGridView5.Rows[i].Cells[t].Value.ToString());
                    numberrs.Add(comp);
                    chast = 1.0 / nBig;
                    rows1.chast = chast;
                    int KKK = (int)variant.Count;
                    rows1.variant = comp;
                    rows1.count = 1;
                    if (KKK == 0)
                    {
                        variant.Add(rows1);
                    }
                    else
                    {
                        for (int G = 0; G < KKK; G++)
                        {
                            if (variant[G].variant == comp)
                            {
                                variant[G].chast += chast;
                                variant[G].count += 1;
                            }
                            else if ((G == KKK - 1) && variant[G].variant != comp)
                            {
                                variant.Add(rows1);
                                break;
                            }
                        }
                    }

                }

                not_sortData.Add(numberrs);
                variant.Sort(delegate (rows x, rows y)
                {
                        return x.variant.CompareTo(y.variant);

                });

                vibirku_var.Add(variant);
            }
            //MessageBox.Show("whoe");



        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index_sukup = comboBox4.SelectedIndex;
            int index_oznaki = comboBox2.SelectedIndex;
            buhalter(Universe.Data_Value[index_sukup][index_oznaki]);
            //for (int i = 0; i < dataGridView5.Columns.Count; i++)
            //{
            //    if (comboBox2.SelectedIndex == i)
            //    {
            //        buhalter(not_sortData[i]);
            //    }
            //}
        }
        double normKvantil(double probability)
        {
            probability = 1- probability ;
            if (probability == 0.5)
            {
                return 0.0;
            }
            else if (probability < 0.5 && probability >= 0.31)
            {
                return -0.29;
            }
            else if (probability < 0.31 && probability >= 0.16)
            {
                return -0.75;
            }
            else if (probability < 0.16 && probability >= 0.07)
            {
                return -1.3;
            }
            else if (probability < 0.07 && probability >= 0.023)
            {
                return -1.75;
            }
            else if (probability < 0.023 && probability >= 0.006)
            {
                return -2.27;
            }
            else if (probability < 0.006 && probability >= 0.001)
            {
                return -2.79;
            }
            else if (probability < 0.001 && probability >= 0.0002)
            {
                return -3.25;
            }
            else if (probability < 0.0002 && probability > 0.0001)
            {
                return -3.59;
            }
            else if (probability > 0.5 && probability <= 0.68)
            {
                return 0.27;
            }
            else if (probability > 0.68 && probability <= 0.84)
            {
                return 0.77;
            }
            else if (probability > 0.84 && probability <= 0.93)
            {
                return 1.29;
            }
            else if (probability > 0.93 && probability <= 0.97)
            {
                return 1.77;
            }
            else if (probability > 0.97 && probability <= 0.99)
            {
                return 2.25;
            }
            else if (probability > 0.99 && probability <= 0.9986)
            {
                return 2.75;
            }
            else if (probability > 0.9986 && probability <= 0.9997)
            {
                return 3.25;
            }
            else
            {
                return 3.5;
            }
        }
        double normKvantil_alphana2(double probability)
        {
            probability = 1.0 - probability;
            if (probability == 0.5)
            {
                return 0.0;
            }
            else if (probability < 0.5 && probability >= 0.31)
            {
                return -0.29;
            }
            else if (probability < 0.31 && probability >= 0.16)
            {
                return -0.75;
            }
            else if (probability < 0.16 && probability >= 0.07)
            {
                return -1.3;
            }
            else if (probability < 0.07 && probability >= 0.023)
            {
                return -1.75;
            }
            else if (probability < 0.023 && probability >= 0.006)
            {
                return -2.27;
            }
            else if (probability < 0.006 && probability >= 0.001)
            {
                return -2.79;
            }
            else if (probability < 0.001 && probability >= 0.0002)
            {
                return -3.25;
            }
            else if (probability < 0.0002 && probability > 0.0001)
            {
                return -3.59;
            }
            else if (probability > 0.5 && probability <= 0.68)
            {
                return 0.27;
            }
            else if (probability > 0.68 && probability <= 0.84)
            {
                return 0.77;
            }
            else if (probability > 0.84 && probability <= 0.93)
            {
                return 1.29;
            }
            else if (probability > 0.93 && probability <= 0.97)
            {
                return 1.77;
            }
            else if (probability > 0.97 && probability <= 0.99)
            {
                return 2.25;
            }
            else if (probability > 0.99 && probability <= 0.9986)
            {
                return 2.75;
            }
            else if (probability > 0.9986 && probability <= 0.9997)
            {
                return 3.25;
            }
            else
            {
                return 3.5;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            
            // int[] first_or_secont_vibirka=new int[2];
            List<int> nomer_oznak = new List<int>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (nomer_oznak.Count == 2)
                {
                    break;
                }
                if (listBox1.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            }
            List<List<double>> vector_oznak12 = new List<List<double>>();
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                vector_oznak12.Add(Universe.Data_Vectors[nomer_oznak[i]]);
            }
           
            List<rang> data = new List<rang>();//місце де  зберігаємо дані котрі вибрали
            List<rang> first_vibirka = new List<rang>();
            List<rang> second_vibirka = new List<rang>();
            
            int perevirka_norma = 0;
            for (int t = 0; t < vector_oznak12[0].Count; t++)
            {
                rang cheb = new rang();
                cheb.which_elem = 0;
                cheb.element = vector_oznak12[0][t];
                first_vibirka.Add(cheb);
            }
            for (int t = 0; t < vector_oznak12[1].Count; t++)
            {
                rang cheb = new rang();
                cheb.which_elem = 1;
                cheb.element = vector_oznak12[1][t];
                second_vibirka.Add(cheb);
            }
 
            for (int i = 0; i < nomer_oznak.Count; i++)
            {

                for (int t = 0; t < vector_oznak12[i].Count; t++)
                {
                    rang cheb = new rang();
                    cheb.which_elem = i;
                    cheb.element = vector_oznak12[i][t];
                    data.Add(cheb);
                }
  
            }

            data.Sort(delegate (rang x, rang y) { return x.element.CompareTo(y.element); });//помістили дві вибірки і посортували щоб знайти ранги
            List<List<rang>> our_rang = new List<List<rang>>();

            double elemnt_oflist = 0;
            List<rang> firstrang = new List<rang>();
            for (int t = 0; t < data.Count; t++)
            {
                elemnt_oflist = data[t].element;
                rang squatr = new rang();
                squatr.element = elemnt_oflist;
                squatr.rangg = 1 + t;
                squatr.which_elem = data[t].which_elem;

                int KKK = (int)firstrang.Count;

                if (KKK == 0)
                {
                    firstrang.Add(squatr);
                }
                else
                {
                    double enumer = 1;
                    double qt = 1 + t;
                    for (int G = 0; G < KKK; G++)
                    {
                        if (firstrang[G].element == elemnt_oflist)
                        {
                            enumer++;
                            qt += (G + 1);
                        }
                    }
                    for (int G = 0; G < KKK; G++)
                    {
                        if (firstrang[G].element == elemnt_oflist)
                        {
                            firstrang[G].rangg = qt / enumer;
                        }
                    }
                    squatr.rangg = qt / enumer;
                    firstrang.Add(squatr);
                }
            }//пошук рангів
            our_rang.Add(firstrang);//лист де зберігається віраційний ряд з рангами двох вибірок

            //MessageBox.Show("work");
            List<double> zet_vibirka = new List<double>();
            double zet_seredne = 0;
            for (int i = 0; i < first_vibirka.Count; i++)
            {
                double riznica = 0;
                riznica = first_vibirka[i].element - second_vibirka[i].element;
                zet_seredne += riznica;
                zet_vibirka.Add(riznica);

            }// записуємо вибірку z=x-y
            zet_seredne /= zet_vibirka.Count; //середнє вибірки z = x - y
            double zet_dispersia = 0;
            for (int i = 0; i < zet_vibirka.Count; i++)
            {
                zet_dispersia += Math.Pow((zet_vibirka[i] - zet_seredne), 2.0);
            }
            zet_dispersia /= (zet_vibirka.Count - 1.0);//дисперсія вибірки z=x-y

            double t_character = (zet_seredne * Math.Sqrt(zet_vibirka.Count)) / (Math.Sqrt(zet_dispersia));//статистична характеристика t
            double first_vibirka_disper = 0;//дисперсія першої вибірки
            double second_vibirka_disper = 0;//дисперсія другої вибірки
            double first_vibirka_seredne = 0;//середнє першої вибірки
            double second_vibirka_seredne = 0;//середнє другої вибірки
            /// обчислюємо середнє та дисперсію двох вибірок
            for (int i = 0; i < first_vibirka.Count; i++)
            {
                first_vibirka_seredne += first_vibirka[i].element;
            }
            for (int i = 0; i < second_vibirka.Count; i++)
            {
                second_vibirka_seredne += second_vibirka[i].element;
            }
            first_vibirka_seredne /= (first_vibirka.Count);
            second_vibirka_seredne /= second_vibirka.Count;
            for (int i = 0; i < first_vibirka.Count; i++)
            {
                first_vibirka_disper += Math.Pow((first_vibirka[i].element - first_vibirka_seredne), 2.0);
            }
            first_vibirka_disper /= (first_vibirka.Count - 1.0);
            for (int i = 0; i < second_vibirka.Count; i++)
            {
                second_vibirka_disper += Math.Pow((second_vibirka[i].element - second_vibirka_seredne), 2.0);
            }
            second_vibirka_disper /= (second_vibirka.Count - 1.0);
            /// закінчили обчислювати середнє та дисперсію двох вибірок
            double zet_dispersia_seredna = (first_vibirka_disper/first_vibirka.Count) + (second_vibirka_disper/second_vibirka.Count);
            double t_character122 = zet_seredne / Math.Sqrt(zet_dispersia_seredna);// t-тест по другій формулі

            double Fisher = 0;//F-тест розподіл фішера
            if (first_vibirka_disper > second_vibirka_disper)
            {
                Fisher = first_vibirka_disper / second_vibirka_disper;
            }
            else
            {
                Fisher = second_vibirka_disper / first_vibirka_disper;
            }
            //критерій Бартлета
            double B_bartlet = 0;
            double C_bartlet = 0;
            double S_kwadrat_bartleta = 0;
            double chisel_bartlet = ((first_vibirka.Count - 1.0) * first_vibirka_disper) + ((second_vibirka.Count - 1.0) * second_vibirka_disper);
            double znamenyk_bartleta = (first_vibirka.Count - 1.0) + (second_vibirka.Count - 1.0);
            S_kwadrat_bartleta = chisel_bartlet / znamenyk_bartleta;
            B_bartlet = -1.0 * (((first_vibirka.Count - 1.0) * Math.Log(first_vibirka_disper / S_kwadrat_bartleta)) + ((second_vibirka.Count - 1.0) * Math.Log(second_vibirka_disper / S_kwadrat_bartleta)));
            chisel_bartlet = (1.0 / (first_vibirka.Count - 1.0)) + (1.0 / (second_vibirka.Count - 1.0));
            znamenyk_bartleta = (first_vibirka.Count - 1.0) + (second_vibirka.Count - 1.0);
            C_bartlet = 1.0 + (1.0 / 3.0) * ((chisel_bartlet) - (1.0 / (znamenyk_bartleta)));
            double phi_bartlet = B_bartlet / C_bartlet;//критерій Бартлета

            ///критерій однорідності смирнова-колмогорова
            //double supremum_each_vibirkb = 0;
            List<rows> first_vibirka_var_ryad = new List<rows>();//варіаційний ряд першої вибірки
            List<rows> second_vibirka_var_ryad = new List<rows>();//варіаційний ряд другої вибірки
            List<rows> vibirka_var11 = Program.Variaciyniy_ryad(vector_oznak12[0]);
            List<rows> vibirka_var22 = Program.Variaciyniy_ryad(vector_oznak12[1]);

            double suma_chastot = 0;
            for (int i = 0; i < vibirka_var11.Count; i++)
            {
                suma_chastot += vibirka_var11[i].chast;
                rows rwt = new rows();
                rwt.variant = vibirka_var11[i].variant;
                rwt.count = vibirka_var11[i].count;
                rwt.chast = Math.Round(suma_chastot, 7);
                first_vibirka_var_ryad.Add(rwt);
            }//склали варіаційний ряд першої вибірки
            suma_chastot = 0;
            for (int i = 0; i < vibirka_var22.Count; i++)
            {
                suma_chastot += vibirka_var22[i].chast;
                rows rwt = new rows();
                rwt.variant = vibirka_var22[i].variant;
                rwt.count = vibirka_var22[i].count;
                rwt.chast = Math.Round(suma_chastot, 7);
                second_vibirka_var_ryad.Add(rwt);
            }//склали варіаційний ряд другої вибірки
            ///пошук критерія однорідності Смирнова-Колмогорова 
            List<double> smirnov_kolmogorov_list = new List<double>();
            double krok_rozbutya = (our_rang[0][our_rang[0].Count - 1].element - our_rang[0][0].element) / (first_vibirka_var_ryad.Count + second_vibirka_var_ryad.Count);
            for (double i = our_rang[0][0].element; i < our_rang[0][our_rang[0].Count - 1].element; i += krok_rozbutya)
            {
                double first_chislo = 0;
                double second_chislo = 0;
                for (int t = 1; t < first_vibirka_var_ryad.Count; t++)
                {

                    if (i < first_vibirka_var_ryad[t].variant && i > first_vibirka_var_ryad[t - 1].variant)
                    {
                        first_chislo = first_vibirka_var_ryad[t].chast;
                    }
                    else if (i >= first_vibirka_var_ryad[first_vibirka_var_ryad.Count - 1].variant)
                    {
                        first_chislo = 1.0;
                    }
                }
                for (int t = 1; t < second_vibirka_var_ryad.Count; t++)
                {

                    if (i < second_vibirka_var_ryad[t].variant && i > second_vibirka_var_ryad[t - 1].variant)
                    {
                        second_chislo = second_vibirka_var_ryad[t].chast;
                    }
                    else if (i >= second_vibirka_var_ryad[second_vibirka_var_ryad.Count - 1].variant)
                    {
                        second_chislo = 1.0;
                    }
                }
                smirnov_kolmogorov_list.Add(Math.Abs(first_chislo - second_chislo));
            }
            double smirnov_kolmogorov = smirnov_kolmogorov_list.Max();// z=sup|F(x)-G(xz)|
            double N_for_smirnov_kolmogorof = 0;
            if (first_vibirka.Count > second_vibirka.Count)
            {
                N_for_smirnov_kolmogorof = second_vibirka.Count;
            }
            else
            {
                N_for_smirnov_kolmogorof = first_vibirka.Count;
            }

            double kryteriy_smirnova_kolmogorava = 1.0 - Math.Exp(-2.0 * Math.Pow(smirnov_kolmogorov, 2.0)) * (1.0 - ((2.0 * smirnov_kolmogorov) / (3.0 * Math.Sqrt(N_for_smirnov_kolmogorof)))
               + ((2.0 * Math.Pow(smirnov_kolmogorov, 2.0) / (3.0 * N_for_smirnov_kolmogorof))) * (1.0 - ((2.0 * Math.Pow(smirnov_kolmogorov, 2.0)) / (3.0)))
               + ((4.0 * smirnov_kolmogorov) / (9.0 * Math.Sqrt(Math.Pow(smirnov_kolmogorov, 3.0))))
               * ((1.0 / 5.0) - ((19.0 * Math.Pow(smirnov_kolmogorov, 2.0)) / 15.0) + ((2.0 * Math.Pow(smirnov_kolmogorov, 4.0)) / 3.0))
               );///функція розподілу Смирнова L(z)

            //////////повертаємо дві вибірки з рангами
            List<rang> first_vibirka_po_rangam = new List<rang>();
            List<rang> second_vibirka_po_rangam = new List<rang>();
            for (int i = 0; i < our_rang[0].Count; i++)
            {
                if (our_rang[0][i].which_elem == 0)
                {
                    first_vibirka_po_rangam.Add(our_rang[0][i]);
                }
                else
                {
                    second_vibirka_po_rangam.Add(our_rang[0][i]);
                }
            }
            ///////// вернули дві вибірки з рангами
            double first_vibirka_po_rangam_seredne = 0;
            double secon_vibirka_po_rangam_seredne = 0;
            //вілкоксон
            double wilkokson_r1 = 0;
            double wilkokson_r2 = 0;
            for (int i = 0; i < first_vibirka_po_rangam.Count; i++)
            {
                wilkokson_r1 += first_vibirka_po_rangam[i].rangg;
            }
            first_vibirka_po_rangam_seredne = wilkokson_r1 / first_vibirka_po_rangam.Count;
            for (int i = 0; i < second_vibirka_po_rangam.Count; i++)
            {
                wilkokson_r2 += second_vibirka_po_rangam[i].rangg;
            }
            secon_vibirka_po_rangam_seredne = wilkokson_r2 / second_vibirka_po_rangam.Count;
            double e_wilk_r1 = ((first_vibirka_po_rangam.Count * (first_vibirka_po_rangam.Count + second_vibirka_po_rangam.Count + 1))) / 2.0;
            double e_wilk_r2 = ((second_vibirka_po_rangam.Count * (first_vibirka_po_rangam.Count + second_vibirka_po_rangam.Count + 1))) / 2.0;
            double d_wilk_r1_and_r2 = ((first_vibirka_po_rangam.Count * second_vibirka_po_rangam.Count) * (first_vibirka_po_rangam.Count + second_vibirka_po_rangam.Count + 1)) / 12.0;
            double wilkokson_porivnyana_value_r1 = (wilkokson_r1 - e_wilk_r1) / Math.Sqrt(d_wilk_r1_and_r2);
            double wilkokson_porivnyana_value_r2 = (wilkokson_r2 - e_wilk_r2) / Math.Sqrt(d_wilk_r1_and_r2);
            //критерій манна уїтні
            double mana_uinty = (first_vibirka_po_rangam.Count * second_vibirka_po_rangam.Count) + ((first_vibirka_po_rangam.Count * (first_vibirka_po_rangam.Count - 1.0)) / 2.0) - wilkokson_r1;
            double e_mana_uinty = (first_vibirka_po_rangam.Count * second_vibirka_po_rangam.Count) / 2.0;
            double d_mana_uinty = ((first_vibirka_po_rangam.Count * second_vibirka_po_rangam.Count) * (first_vibirka_po_rangam.Count + second_vibirka_po_rangam.Count + 1.0)) / 12.0;
            double mana_uinty_porivnyanya = (mana_uinty - e_mana_uinty) / Math.Sqrt(d_mana_uinty);
            //н-критерій Крускала-Уоліса
            double n_for_kruscala = first_vibirka_po_rangam.Count + second_vibirka_po_rangam.Count;
            double h_kriteriy = (Math.Pow(first_vibirka_po_rangam_seredne - ((n_for_kruscala + 1.0) / 2.0), 2.0) / ((n_for_kruscala + 1.0) * (n_for_kruscala - first_vibirka_po_rangam.Count) / (12.0 * first_vibirka_po_rangam.Count))) * (1.0 - (first_vibirka_po_rangam.Count / n_for_kruscala));
            h_kriteriy += (Math.Pow(secon_vibirka_po_rangam_seredne - ((n_for_kruscala + 1.0) / 2.0), 2.0) / ((n_for_kruscala + 1.0) * (n_for_kruscala - second_vibirka_po_rangam.Count) / (12.0 * second_vibirka_po_rangam.Count))) * (1.0 - (second_vibirka_po_rangam.Count / n_for_kruscala));//H-kriteriy
            ///критерій різниці середніх рангів
            double riznica_serednih = (first_vibirka_po_rangam_seredne - secon_vibirka_po_rangam_seredne) / ((first_vibirka_po_rangam.Count + second_vibirka_po_rangam.Count) * Math.Sqrt((first_vibirka_po_rangam.Count + second_vibirka_po_rangam.Count + 1.0) / (12.0 * first_vibirka_po_rangam.Count * second_vibirka_po_rangam.Count)));
            ///критерій знаків
            double S_for_znakiv = 0;
            for (int i = 0; i < zet_vibirka.Count; i++)
            {
                if (zet_vibirka[i] > 0)
                {
                    S_for_znakiv++;
                }
            }
            double s_zvezda_for_znakiv = (2.0 * S_for_znakiv - 1.0 - zet_vibirka.Count) / Math.Sqrt(zet_vibirka.Count);
            /// q-критерій Кохрена
            double porivn_q = (first_vibirka_seredne + second_vibirka_seredne) / 2.0;
            List<List<double>> kohren = new List<List<double>>();
            List<double> t_kohrena = new List<double>();
            List<double> u_kohrena = new List<double>();
            List<double> u_kvadra_kohrena = new List<double>();

            for (int i = 0; i < 2; i++)
            {
                double tr = 0;
                List<double> doit = new List<double>();
                for (int t = 0; t < vector_oznak12[i].Count; t++)
                {
                    if (vector_oznak12[i][t] > porivn_q)
                    {
                        doit.Add(1.0);
                        tr++;
                    }
                    else
                    {
                        doit.Add(0.0);
                    }
                }
                t_kohrena.Add(tr);
                kohren.Add(doit);
            }
            double t_kohrena_seredne = 0;
            for (int i = 0; i < kohren[0].Count; i++)
            {

                double u = 0;
                for (int t = 0; t < kohren.Count; t++)
                {
                    u += kohren[t][i];
                }
                u_kohrena.Add(u);
                u_kvadra_kohrena.Add(Math.Pow(u, 2.0));
            }
            for (int i = 0; i < kohren.Count; i++)
            {
                t_kohrena_seredne = t_kohrena_seredne + t_kohrena[i];

            }
            t_kohrena_seredne = t_kohrena_seredne / kohren.Count;
            double suma_u = 0;
            double rizniza_t = 0;
            double sume_u_seredne = 0;
            for (int i = 0; i < kohren[0].Count; i++)
            {
                suma_u = suma_u + u_kohrena[i];
                sume_u_seredne = sume_u_seredne + u_kvadra_kohrena[i];
            }
            for (int i = 0; i < kohren.Count; i++)
            {
                rizniza_t += Math.Pow(t_kohrena[i] - t_kohrena_seredne, 2.0);
            }
            double q_kohrena = (2.0 * rizniza_t) / (2.0 * suma_u - sume_u_seredne);

            /// критерій Аббе
            double d_kvadrat = 0;
            double suma_d_kvadrata = 0;
            for (int i = 0; i < zet_vibirka.Count - 1; i++)
            {
                suma_d_kvadrata += Math.Pow(zet_vibirka[i + 1] - zet_vibirka[i], 2.0);
            }
            d_kvadrat = suma_d_kvadrata / (zet_vibirka.Count - 1.0);
            double q_kvadrat_abbe = d_kvadrat / (2.0 * zet_dispersia);
            double statistik_Abbe = (q_kvadrat_abbe - 1.0) * Math.Sqrt((Math.Pow(zet_vibirka.Count, 2.0) - 1) / (zet_vibirka.Count - 2.0));
            //////////next step квантилі Стьюдент Пірсон Фішера 
            double stepin_vilnost = first_vibirka.Count+second_vibirka.Count - 2;
            double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));//функція стандартного нормального розподілу ймовірностей
            kvantil_start_rozpodil = normKvantil_alphana2(Double.Parse(textBox5.Text));
            double g1_u = (Math.Pow(kvantil_start_rozpodil, 3.0) + kvantil_start_rozpodil) / 4.0;
            double g2_u = (5.0 * Math.Pow(kvantil_start_rozpodil, 5.0) + 16.0 * Math.Pow(kvantil_start_rozpodil, 3.0) + 3.0 * kvantil_start_rozpodil) / 96.0;
            double g3_u = (3.0 * Math.Pow(kvantil_start_rozpodil, 7.0) + 19.0 * Math.Pow(kvantil_start_rozpodil, 5.0) + 17.0 * Math.Pow(kvantil_start_rozpodil, 3.0) - (15.0 * kvantil_start_rozpodil)) / 384.0;
            double g4_u = (79.0 * Math.Pow(kvantil_start_rozpodil, 9.0) + 779.0 * Math.Pow(kvantil_start_rozpodil, 7.0) + 1482.0 * Math.Pow(kvantil_start_rozpodil, 5.0) - (1920.0 * Math.Pow(kvantil_start_rozpodil, 3.0)) - (945.0 * kvantil_start_rozpodil)) / 92160.0;
            double kvantil_Styudent = kvantil_start_rozpodil + (g1_u / stepin_vilnost) + (g2_u / Math.Pow(stepin_vilnost, 2.0)) + (g3_u / Math.Pow(stepin_vilnost, 3.0)) + (g4_u / Math.Pow(stepin_vilnost, 4.0));// квантиль розподілу Стьюдента
            double riven_znachuchosti = Double.Parse(textBox5.Text);
            kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
            double sigma_Fishera = (1.0 / ((data.Count / 2.0) - 1.0)) + (1.0 / ((data.Count / 2.0) - 1.0));
            double delts_Fishera = (1.0 / ((data.Count / 2.0) - 1.0)) - (1.0 / ((data.Count / 2.0) - 1.0));
            double kvantil_Fishera = kvantil_start_rozpodil * Math.Sqrt(sigma_Fishera / 2.0) - delts_Fishera * (Math.Pow(kvantil_start_rozpodil, 2.0) + 2.0)//квантиль Фішера
                + Math.Sqrt(sigma_Fishera / 2.0) * (((sigma_Fishera / 24.0) * (Math.Pow(kvantil_start_rozpodil, 2.0) + 3.0 * kvantil_start_rozpodil)) +
                (1.0 / 72.0) * (Math.Pow(delts_Fishera, 2.0) / sigma_Fishera) * (Math.Pow(kvantil_start_rozpodil, 3.0) + 11.0 * kvantil_start_rozpodil)) -
                ((sigma_Fishera * delts_Fishera) / 120.0) * (Math.Pow(kvantil_start_rozpodil, 4.0) + 9.0 * Math.Pow(kvantil_start_rozpodil, 2.0) + 8.0) + (Math.Pow(delts_Fishera, 3.0) / (3240.0 * sigma_Fishera)) * (3.0 * Math.Pow(kvantil_start_rozpodil, 4.0) + 7.0 * Math.Pow(kvantil_start_rozpodil, 2.0) - 16.0) +
                (Math.Sqrt(sigma_Fishera / 2.0)) * ((Math.Pow(sigma_Fishera, 2.0) / 1920.0) * (Math.Pow(kvantil_start_rozpodil, 5.0) + 20.0 * Math.Pow(kvantil_start_rozpodil, 3.0) + 15.0 * kvantil_start_rozpodil) + (Math.Pow(delts_Fishera, 4.0) / 2880.0) * (Math.Pow(kvantil_start_rozpodil, 5.0) + 44.0 * (Math.Pow(kvantil_start_rozpodil, 3.0)) + 183.0 * kvantil_start_rozpodil)
                + ((Math.Pow(delts_Fishera, 4.0)) / (155520.0 * Math.Pow(sigma_Fishera, 2.0))) * (9.0 * Math.Pow(kvantil_start_rozpodil, 5.0) - 284.0 * Math.Pow(kvantil_start_rozpodil, 3.0) - 1513.0 * kvantil_start_rozpodil));
            double fisher_exp = Math.Exp(2.0 * kvantil_Fishera);
            stepin_vilnost = 1.0;
            double phi_kvadrat_pirsona = stepin_vilnost * Math.Pow((1.0 - (2 / (9 * stepin_vilnost)) + kvantil_start_rozpodil * Math.Sqrt(2 / (9 * stepin_vilnost))), 3.0);
            dataGridView6.Columns.Clear();
            dataGridView6.Rows.Clear();
            if (radioButton1.Checked)//незалежні
            {
                dataGridView6.Columns.Add("1", "Назва");
                dataGridView6.Columns.Add("2", "Значення");
                dataGridView6.Columns.Add("3", "Критичне значення");
                dataGridView6.Columns.Add("4", "Висновок");

                if (perevirka_norma==2)
                {
                    if (Fisher <= fisher_exp)
                    {
                        dataGridView6.Rows.Add("F-тест", Math.Round(Fisher,4), fisher_exp, "+");
                    }
                    else
                    {
                        dataGridView6.Rows.Add("F-тест", Math.Round(Fisher,4), fisher_exp, "-");
                    }
                }
                if (perevirka_norma == 2)
                {
                    if (Math.Abs(t_character) > kvantil_Styudent)
                    {
                        dataGridView6.Rows.Add("Збіг середніх", Math.Round(Math.Abs(t_character), 4), kvantil_Styudent, "-");
                    }
                    else
                    {
                        dataGridView6.Rows.Add("Збіг середніх", Math.Round(Math.Abs(t_character), 4), kvantil_Styudent, "+");
                    }
                }


                if (Math.Abs(wilkokson_porivnyana_value_r1) <= kvantil_start_rozpodil)
                {
                    dataGridView6.Rows.Add("Критерій Вілкоксона", Math.Round(Math.Abs(wilkokson_porivnyana_value_r1), 4), kvantil_start_rozpodil, "+");
                }
                else
                {
                    dataGridView6.Rows.Add("Критерій Вілкоксона", Math.Round(Math.Abs(wilkokson_porivnyana_value_r1), 4), kvantil_start_rozpodil, "-");
                }

                if (Math.Abs(mana_uinty_porivnyanya) <= kvantil_start_rozpodil)
                {
                    dataGridView6.Rows.Add("Критерій Манна-Уїтні", Math.Round(Math.Abs(mana_uinty_porivnyanya), 4), kvantil_start_rozpodil, "+");
                }
                else
                {
                    dataGridView6.Rows.Add("Критерій Манна-Уїтні", Math.Round(Math.Abs(mana_uinty_porivnyanya), 4), kvantil_start_rozpodil, "-");
                }
                if (Math.Abs(riznica_serednih) <= kvantil_start_rozpodil)
                {
                    dataGridView6.Rows.Add("Критерій різниці середніх рангів", Math.Round(Math.Abs(riznica_serednih), 4), kvantil_start_rozpodil, "+");
                }
                else
                {
                    dataGridView6.Rows.Add("Критерій різниці середніх рангів", Math.Round(Math.Abs(riznica_serednih), 4), kvantil_start_rozpodil, "-");
                }


                if ((1.0 - kryteriy_smirnova_kolmogorava) > riven_znachuchosti)
                {
                    dataGridView6.Rows.Add("Критерій Смирнова-Колмогорова",Math.Round(1.0-kryteriy_smirnova_kolmogorava,4), riven_znachuchosti, "+");
                }
                else
                {
                    dataGridView6.Rows.Add("Критерій Смирнова-Колмогорова", Math.Round(1.0-kryteriy_smirnova_kolmogorava,4), riven_znachuchosti, "-");
                }
            }
            else if (radioButton2.Checked)//залежні
            {
                dataGridView6.Columns.Add("1", "Назва");
                dataGridView6.Columns.Add("2", "Значення");
                dataGridView6.Columns.Add("3", "Критичне значення");
                dataGridView6.Columns.Add("4", "Висновок");

                if (perevirka_norma==2 &&(Fisher <= fisher_exp))
                {
                    if (Math.Abs(t_character) > kvantil_Styudent)
                    {
                        dataGridView6.Rows.Add("Збіг середніх", Math.Round(Math.Abs(t_character122),4), kvantil_Styudent, "-");
                    }
                    else
                    {
                        dataGridView6.Rows.Add("Збіг середніх", Math.Round(Math.Abs(t_character122), 4), kvantil_Styudent, "+");
                    }
                }
                
                if (s_zvezda_for_znakiv <= kvantil_start_rozpodil)
                {
                    dataGridView6.Rows.Add("Критерій знаків", Math.Round(s_zvezda_for_znakiv,4), kvantil_start_rozpodil, "+");
                }
                else
                {
                    dataGridView6.Rows.Add("Критерій знаків", Math.Round(s_zvezda_for_znakiv,4), kvantil_start_rozpodil, "-");
                }
                if (statistik_Abbe > riven_znachuchosti)
                {
                    dataGridView6.Rows.Add("Критерій Аббе", Math.Round(statistik_Abbe, 4), riven_znachuchosti, "+");
                }
                else
                {
                    dataGridView6.Rows.Add("Критерій Аббе", Math.Round(statistik_Abbe,4), riven_znachuchosti, "-");
                }
                

            }
        }
        double kvantil_studenta(double probability, double stepin_vilnost)
        {

            double kvantil_start_rozpodil = probability;
            double g1_u = (Math.Pow(kvantil_start_rozpodil, 3.0) + kvantil_start_rozpodil) / 4.0;
            double g2_u = (5.0 * Math.Pow(kvantil_start_rozpodil, 5.0) + 16.0 * Math.Pow(kvantil_start_rozpodil, 3.0) + 3.0 * kvantil_start_rozpodil) / 96.0;
            double g3_u = (3.0 * Math.Pow(kvantil_start_rozpodil, 7.0) + 19.0 * Math.Pow(kvantil_start_rozpodil, 5.0) + 17.0 * Math.Pow(kvantil_start_rozpodil, 3.0) - (15.0 * kvantil_start_rozpodil)) / 384.0;
            double g4_u = (79.0 * Math.Pow(kvantil_start_rozpodil, 9.0) + 779.0 * Math.Pow(kvantil_start_rozpodil, 7.0) + 1482.0 * Math.Pow(kvantil_start_rozpodil, 5.0) - (1920.0 * Math.Pow(kvantil_start_rozpodil, 3.0)) - (945.0 * kvantil_start_rozpodil)) / 92160.0;
            double kvantil_Styudent = kvantil_start_rozpodil + (g1_u / stepin_vilnost) + (g2_u / Math.Pow(stepin_vilnost, 2.0)) + (g3_u / Math.Pow(stepin_vilnost, 3.0)) + (g4_u / Math.Pow(stepin_vilnost, 4.0));// квантиль розподілу Стьюдента
            return kvantil_Styudent;
        }
        double kvantil_Fishera(double probability, double stepin_vilnost1,double stepin_vilnosty2)
        {
            double kvantil_start_rozpodil = probability;

            double sigma_Fishera = (1.0 / stepin_vilnost1) + (1.0 / stepin_vilnosty2);
            double delts_Fishera = (1.0 / stepin_vilnost1) - (1.0 / stepin_vilnosty2);
            double kvantil_Fishera = kvantil_start_rozpodil * Math.Sqrt(sigma_Fishera / 2.0) - delts_Fishera * (Math.Pow(kvantil_start_rozpodil, 2.0) + 2.0)//квантиль Фішера
                + Math.Sqrt(sigma_Fishera / 2.0) * (((sigma_Fishera / 24.0) * (Math.Pow(kvantil_start_rozpodil, 2.0) + 3.0 * kvantil_start_rozpodil)) +
                (1.0 / 72.0) * (Math.Pow(delts_Fishera, 2.0) / sigma_Fishera) * (Math.Pow(kvantil_start_rozpodil, 3.0) + 11.0 * kvantil_start_rozpodil)) -
                ((sigma_Fishera * delts_Fishera) / 120.0) * (Math.Pow(kvantil_start_rozpodil, 4.0) + 9.0 * Math.Pow(kvantil_start_rozpodil, 2.0) + 8.0) + (Math.Pow(delts_Fishera, 3.0) / (3240.0 * sigma_Fishera)) * (3.0 * Math.Pow(kvantil_start_rozpodil, 4.0) + 7.0 * Math.Pow(kvantil_start_rozpodil, 2.0) - 16.0) +
                (Math.Sqrt(sigma_Fishera / 2.0)) * ((Math.Pow(sigma_Fishera, 2.0) / 1920.0) * (Math.Pow(kvantil_start_rozpodil, 5.0) + 20.0 * Math.Pow(kvantil_start_rozpodil, 3.0) + 15.0 * kvantil_start_rozpodil) + (Math.Pow(delts_Fishera, 4.0) / 2880.0) * (Math.Pow(kvantil_start_rozpodil, 5.0) + 44.0 * (Math.Pow(kvantil_start_rozpodil, 3.0)) + 183.0 * kvantil_start_rozpodil)
                + ((Math.Pow(delts_Fishera, 4.0)) / (155520.0 * Math.Pow(sigma_Fishera, 2.0))) * (9.0 * Math.Pow(kvantil_start_rozpodil, 5.0) - 284.0 * Math.Pow(kvantil_start_rozpodil, 3.0) - 1513.0 * kvantil_start_rozpodil));
            double fisher_exp = Math.Exp(2.0 * kvantil_Fishera);
            return fisher_exp;
        }
        double kvantil_Pirsona(double probability, double stepin_vilnost)
        {
            double kvantil_start_rozpodil = probability;
            double phi_kvadrat_pirsona = stepin_vilnost * Math.Pow((1.0 - (2 / (9 * stepin_vilnost)) + kvantil_start_rozpodil * Math.Sqrt(2 / (9 * stepin_vilnost))), 3.0);
            return phi_kvadrat_pirsona;
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int N_vibirka = Convert.ToInt32(textBox6.Text);
            textBox7.Visible = false;
            textBox8.Visible = false;
            textBox10.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label9.Visible = false;

            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
            textBox13.Visible = false;

            DateTime dt = DateTime.Now;
            if (comboBox3.SelectedIndex == 0)
            {
                //нормальний розподіл
                textBox7.Visible = true;
                label6.Visible = true;
                textBox8.Visible = true;
                label5.Visible = true;
                label6.Text = "m";
                label5.Text = "sigma";
                the_select_index = 0;


            }
            else if (comboBox3.SelectedIndex == 1)
            {
                the_select_index = 1;
                label6.Visible = true;
                label6.Text = "lambda";
                textBox8.Visible = true;
                //експоненціальний розподіл

            }
            else if (comboBox3.SelectedIndex == 2)
            {
                the_select_index = 2;
                //рівномірний розподіл
                label6.Visible = true;
                textBox7.Visible = true;
                label5.Visible = true;
                textBox8.Visible = true;
                label6.Text = "a";
                label5.Text = "b";
            }
            else if (comboBox3.SelectedIndex == 3)
            {
                //розподіл ВЕЙБУЛЛА 
                the_select_index = 3;

                label6.Visible = true;
                textBox7.Visible = true;
                label5.Visible = true;
                textBox8.Visible = true;
                label6.Text = "alpha";
                label5.Text = "beta";
            }
            else if (comboBox3.SelectedIndex == 4)//параболіяна
            {
                the_select_index = 4;
                
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                textBox11.Visible = true;
                textBox12.Visible = true;
                textBox13.Visible = true;

                textBox10.Visible = true;
                label9.Visible = true;
                label6.Visible = true;
                textBox7.Visible = true;
                label5.Visible = true;
                textBox8.Visible = true;
                label6.Text = "a";
                label5.Text = "b";
                label9.Text = "ε";
                label11.Text = "Параметр а";
                label12.Text = "Параметр b";
                label10.Text = "Параметр с";
            }
            else if (comboBox3.SelectedIndex == 5)//квазілійна
            {
                the_select_index = 5;
                
                label11.Visible = true;
                label12.Visible = true;
                
                textBox12.Visible = true;
                textBox13.Visible = true;

                textBox10.Visible = true;
               
                label9.Visible = true;
                label6.Visible = true;
                textBox7.Visible = true;
                label5.Visible = true;
                textBox8.Visible = true;
                label6.Text = "a";
                label5.Text = "b";
                label9.Text = "ε";
                label11.Text = "Параметр а";
                label12.Text = "Параметр b";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //DateTime dt = new DateTime();
            int k_vect =(int)numericUpDown2.Value;
            string name_of_fiel = textBox17.Text;
            if (the_select_index == 0)
            {
                int N_vibirka = Convert.ToInt32(textBox6.Text);
                double m = Convert.ToDouble(textBox8.Text);
                double sigma = Convert.ToDouble(textBox7.Text);
                string path = $@"D:\projekt visual\model_data\NORM({name_of_fiel}.txt";
                Random rand = new Random();
                using (FileStream fl = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamWriter rd = new StreamWriter(fl))
                    {
                        for (int i = 0; i < N_vibirka; i++)
                        {
                            string data_in_str = "";
                            for (int rt = 0; rt < k_vect; rt++)
                            {
                                data_in_str += $" {Program.Norm_distribution(rand, m, sigma).ToString()}";
                            }
                            rd.WriteLine($"{Program.koma_inverse(data_in_str)}");
                        }
                    }
                }
            }//нормальний розподіл
            else if (the_select_index == 1)
            {
                int N_vibirka = Convert.ToInt32(textBox6.Text);
                double lada = Convert.ToDouble(textBox8.Text);
                string path = $@"D:\projekt visual\model_data\exp{N_vibirka} lmd{lada} {name_of_fiel}.txt";
                Random rand = new Random();
                using (FileStream fl = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamWriter rd = new StreamWriter(fl))
                    {
                        for (int i = 0; i < N_vibirka; i++)
                        {
                            string data_in_str = "";
                            for (int rt = 0; rt < k_vect; rt++)
                            {
                                data_in_str += $" {Program.Exp_distribution(rand, lada).ToString("0.###########################################")}";
                            }
                            rd.WriteLine($"{Program.koma_inverse(data_in_str)}");
                           // rd.WriteLine($"{Program.koma_inverse(Program.Exp_distribution(rand, lada).ToString("0.###########################################"))}");
                        }
                    }
                }
            }//експоненціальний розподіл
            else if (the_select_index == 2)
            {
                int N_vibirka = Convert.ToInt32(textBox6.Text);
                double a = Convert.ToDouble(textBox8.Text);
                double b = Convert.ToDouble(textBox7.Text);
                string path = $@"D:\projekt visual\model_data\ravn{N_vibirka} a{a}b{b}.txt";
                Random rand = new Random();
                using (FileStream fl = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamWriter rd = new StreamWriter(fl))
                    {
                        for (int i = 0; i < N_vibirka; i++)
                        {
                            rd.WriteLine($"{Program.koma_inverse(Program.Ravn_distribution(rand, a, b).ToString())}");
                        }
                    }
                }
            }//рівномірний розподіл
            else if (the_select_index == 3)
            {
                int N_vibirka = Convert.ToInt32(textBox6.Text);
                double a = Convert.ToDouble(textBox8.Text);
                double b = Convert.ToDouble(textBox7.Text);
                string path = $@"D:\projekt visual\model_data\veib{N_vibirka} apha{a}beta{b}.txt";
                Random rand = new Random();
                using (FileStream fl = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamWriter rd = new StreamWriter(fl))
                    {
                        for (int i = 0; i < N_vibirka; i++)
                        {
                            rd.WriteLine($"{Program.koma_inverse(Program.Veib_distribution(rand, a, b).ToString())}");
                        }
                    }
                }
            }//розподіл ВЕЙБУЛЛА
            else if (the_select_index == 4)//параболічня регресія
            {
                int N_vibirka = Convert.ToInt32(textBox6.Text);
                double a = Convert.ToDouble(textBox8.Text);
                double b = Convert.ToDouble(textBox7.Text);
                double pohibka=Convert.ToDouble(textBox10.Text);
                double parametr_a = Convert.ToDouble(textBox12.Text);
                double parametr_b = Convert.ToDouble(textBox13.Text);
                double parametr_c = Convert.ToDouble(textBox11.Text);

                string path = $@"D:\projekt visual\model_data\parabula{N_vibirka} a{parametr_a} b{parametr_b} c{parametr_c} e{pohibka}.txt";
                Random rand = new Random();
                List<double> List_eps_const=new List<double>();
                for (int i = 0; i < N_vibirka; i++)
                {
                    double vsdds = Program.Norm_distribution(rand, 0.0, pohibka);
                    List_eps_const.Add(vsdds);
                }
                List<double> masic_ixiv = new List<double>();
                List<double> masic_yyyyy = new List<double>();
                for (int i = 0; i < N_vibirka; i++)
                {
                    double vsdds = rand.Next(Convert.ToInt32(a), Convert.ToInt32(b));
                    vsdds += rand.NextDouble();
                    masic_ixiv.Add(vsdds);
                    double zanesty_do_y=parametr_a+(parametr_b*vsdds)+parametr_c*Math.Pow(vsdds,2.0);
                    masic_yyyyy.Add(zanesty_do_y+ List_eps_const[i]);
                }

                using (FileStream fl = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamWriter rd = new StreamWriter(fl))
                    {
                        for (int i = 0; i < N_vibirka; i++)
                        {
                            rd.WriteLine($"{masic_ixiv[i].ToString()}      {masic_yyyyy[i].ToString()}");
                        }
                    }
                }
            }
            else if (the_select_index == 5)//квазілінійна
            {
                int N_vibirka = Convert.ToInt32(textBox6.Text);
                double a = Convert.ToDouble(textBox8.Text);
                double b = Convert.ToDouble(textBox7.Text);
                double pohibka = Convert.ToDouble(textBox10.Text);
                double parametr_a = Convert.ToDouble(textBox12.Text);
                double parametr_b = Convert.ToDouble(textBox13.Text);
                

                string path = $@"D:\projekt visual\model_data\kvaziliniyna{N_vibirka} a{parametr_a} b{parametr_b} e{pohibka}.txt";
                Random rand = new Random();
                List<double> List_eps_const = new List<double>();
                for (int i = 0; i < N_vibirka; i++)
                {
                    double vsdds = Program.Norm_distribution(rand, 0.0, pohibka);
                    List_eps_const.Add(vsdds);
                }
                List<double> masic_ixiv = new List<double>();
                List<double> masic_yyyyy = new List<double>();
                for (int i = 0; i < N_vibirka; i++)
                {
                    double promishok = b - a;
                    double p_adob = rand.NextDouble();
                    double vsdds = promishok*p_adob+a;
                    masic_ixiv.Add(vsdds);
                    double zanesty_do_y = parametr_b/((1.0/vsdds)+parametr_a);
                    //double zanesty_do_y = Math.Sqrt(parametr_b* vsdds + parametr_a);
                    masic_yyyyy.Add(zanesty_do_y + List_eps_const[i]);
                }

                using (FileStream fl = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (StreamWriter rd = new StreamWriter(fl))
                    {
                        for (int i = 0; i < N_vibirka; i++)
                        {
                            rd.WriteLine($"{masic_ixiv[i].ToString()}      {masic_yyyyy[i].ToString()}");
                        }
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView8.Columns.Clear();
            dataGridView8.Rows.Clear();

            List<int> nomer_oznak = new List<int>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            }
            List<List<double>> vector_oznak12 = new List<List<double>>();
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                vector_oznak12.Add(Universe.Data_Vectors[nomer_oznak[i]]);
            }

            //вибираємо яку вибірку порівняти
            List<rang> data = new List<rang>();//місце де  зберігаємо дані котрі вибрали
            List<List<rang>> vibirka = new List<List<rang>>();
            //List<rang> second_vibirka = new List<rang>();
            int k = nomer_oznak.Count;
            int perevirka_norm_vibir = 0;
            //for (int i = 0; i < dataGridView5.Columns.Count; i++)
            //{
            //    if (i == el[vibir])
            //    {
            //        if (norm_or_notnorm[i].norm)
            //        {
            //            perevirka_norm_vibir += 1;
            //        }
            //        List<rang> first_vibirka = new List<rang>();
            //        for (int t = 0; t < not_sortData[i].Count; t++)
            //        {
            //            rang cheb = new rang();
            //            cheb.which_elem = el[vibir];
            //            cheb.element = not_sortData[i][t];
            //            first_vibirka.Add(cheb);
            //        }
            //        vibirka.Add(first_vibirka);
            //
            //        if (vibir==(k-1))
            //        {
            //            break;
            //        }
            //        vibir++;
            //
            //    }
            //
            //
            //}
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                
                if (i>-1)
                {
                    perevirka_norm_vibir += 1;
                }
                List<rang> first_vibirka = new List<rang>();
                for (int t = 0; t < vector_oznak12[i].Count; t++)
                {
                    rang cheb = new rang();
                    cheb.which_elem = i;
                    cheb.element = vector_oznak12[i][t];
                    first_vibirka.Add(cheb);
                }
                vibirka.Add(first_vibirka);


            }
            
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                

                for (int t = 0; t < vector_oznak12[i].Count; t++)
                {
                    rang cheb = new rang();
                    cheb.which_elem = i;
                    cheb.element = vector_oznak12[i][t];
                    data.Add(cheb);
                }

               

                

            }
            data.Sort(delegate (rang x, rang y) { return x.element.CompareTo(y.element); });
            List<List<rang>> our_rang = new List<List<rang>>();

            double elemnt_oflist = 0;
            List<rang> firstrang = new List<rang>();
            for (int t = 0; t < data.Count; t++)
            {
                elemnt_oflist = data[t].element;
                rang squatr = new rang();
                squatr.element = elemnt_oflist;
                squatr.rangg = 1 + t;
                squatr.which_elem = data[t].which_elem;

                int KKK = (int)firstrang.Count;

                if (KKK == 0)
                {
                    firstrang.Add(squatr);
                }
                else
                {
                    double enumer = 1;
                    double qt = 1 + t;
                    for (int G = 0; G < KKK; G++)
                    {
                        if (firstrang[G].element == elemnt_oflist)
                        {
                            enumer++;
                            qt += (G + 1);
                        }
                    }
                    for (int G = 0; G < KKK; G++)
                    {
                        if (firstrang[G].element == elemnt_oflist)
                        {
                            firstrang[G].rangg = qt / enumer;
                        }
                    }
                    squatr.rangg = qt / enumer;
                    firstrang.Add(squatr);
                }
            }//пошук рангів
            our_rang.Add(firstrang);//лист з рангами
            List<double> seredni_vibirok = new List<double>();//середнж вибірок
            List<double> dispersia_vibirok = new List<double>();//дисперсія вибірок
            for (int i = 0; i < k; i++)
            {
                double suma = 0;
                for (int t = 0; t < vibirka[i].Count; t++)
                {
                    suma += vibirka[i][t].element;
                }
                suma /= vibirka[i].Count;
                seredni_vibirok.Add(suma);
            }//seredne
            for (int i = 0; i < k; i++)
            {
                double suma = 0;
                for (int t = 0; t < vibirka[i].Count; t++)
                {
                    suma += Math.Pow(vibirka[i][t].element - seredni_vibirok[i], 2.0);
                }
                suma /= (vibirka[i].Count - 1.0);
                dispersia_vibirok.Add(suma);
            }//dispersia
            double disp_bartlet = 0;
            double chisel_bartlet2 = 0;
            double znamenyk_bartleta = 0;
            for (int i = 0; i < k; i++)
            {
                chisel_bartlet2 += (vibirka[i].Count - 1.0) * dispersia_vibirok[i];
                znamenyk_bartleta += (vibirka[i].Count - 1.0);
            }

            disp_bartlet = chisel_bartlet2 / znamenyk_bartleta;
            double b_bartleta = 0;
            for (int i = 0; i < k; i++)
            {
                b_bartleta += (vibirka[i].Count - 1.0) * Math.Log(dispersia_vibirok[i] / disp_bartlet);
            }
            b_bartleta = -1.0 * b_bartleta;
            double c_bartleta = 0;
            chisel_bartlet2 = 0;
            znamenyk_bartleta = 0;
            for (int i = 0; i < k; i++)
            {
                chisel_bartlet2 += 1.0 / (vibirka[i].Count - 1.0);
                znamenyk_bartleta += (vibirka[i].Count - 1.0);
            }
            c_bartleta = 1.0 + (1.0 / (3.0 * (k - 1.0))) * (chisel_bartlet2 - (1.0 / znamenyk_bartleta));

            double kriyeriy_bartleat = b_bartleta / c_bartleta;//критерій бартлета


            List<List<rang>> SECONDvibirka = new List<List<rang>>();
            List<double> wilkokson_list = new List<double>();
            for (int i = 0; i < k; i++)
            {
                List<rang> first_vibirka22 = new List<rang>();
                for (int t = 0; t < our_rang[0].Count; t++)
                {
                    if (our_rang[0][t].which_elem == i)
                    {
                        first_vibirka22.Add(our_rang[0][t]);
                    }

                }
                SECONDvibirka.Add(first_vibirka22);
            }//вибірки з рангами
            
            for (int i = 0; i < k; i++)
            {
                double suma_rangiv = 0;
                for (int t = 0; t < SECONDvibirka[i].Count; t++)
                {
                    suma_rangiv += SECONDvibirka[i][t].rangg;
                }
                suma_rangiv /= SECONDvibirka[i].Count;
                wilkokson_list.Add(suma_rangiv);
            }
            double n_all_vibirok = our_rang[0].Count;
            double H_kriteriy = 0;
            for (int i = 0; i < k; i++)
            {
                H_kriteriy += (Math.Pow(wilkokson_list[i] - ((n_all_vibirok) / 2.0), 2.0) / ((n_all_vibirok + 1.0) * (n_all_vibirok - SECONDvibirka[i].Count) / (12.0 * SECONDvibirka[i].Count))) * (1.0 - (SECONDvibirka[i].Count / n_all_vibirok));
            }//h-критерій
            /////кохрен

            double porivn_q = 0;
            for (int i = 0; i < k; i++)
            {
                porivn_q += seredni_vibirok[i];
            }
            porivn_q /= k;
            List<List<double>> kohren = new List<List<double>>();
            List<double> t_kohrena = new List<double>();
            List<double> u_kohrena = new List<double>();
            List<double> u_kvadra_kohrena = new List<double>();

            for (int i = 0; i < k; i++)
            {
                double tr = 0;
                List<double> doit = new List<double>();
                for (int t = 0; t < vector_oznak12[i].Count; t++)
                {
                    if (vector_oznak12[i][t] > porivn_q)
                    {
                        doit.Add(1.0);
                        tr++;
                    }
                    else
                    {
                        doit.Add(0.0);
                    }
                }
                t_kohrena.Add(tr);
                kohren.Add(doit);
            }
            double t_kohrena_seredne = 0;
            for (int i = 0; i < kohren[0].Count; i++)
            {

                double u = 0;
                for (int t = 0; t < kohren.Count; t++)
                {
                    u += kohren[t][i];
                }
                u_kohrena.Add(u);
                u_kvadra_kohrena.Add(Math.Pow(u, 2.0));
            }
            for (int i = 0; i < kohren.Count; i++)
            {
                t_kohrena_seredne = t_kohrena_seredne + t_kohrena[i];

            }
            t_kohrena_seredne = t_kohrena_seredne / kohren.Count;
            double suma_u = 0;
            double rizniza_t = 0;
            double sume_u_seredne = 0;
            for (int i = 0; i < kohren[0].Count; i++)
            {
                suma_u = suma_u + u_kohrena[i];
                sume_u_seredne = sume_u_seredne + u_kvadra_kohrena[i];
            }
            for (int i = 0; i < kohren.Count; i++)
            {
                rizniza_t += Math.Pow(t_kohrena[i] - t_kohrena_seredne, 2.0);
            }
            double q_kohrena = (k * (k - 1.0) * rizniza_t) / (k * suma_u - sume_u_seredne);//кохрена
            double stepin_vilnost = Convert.ToDouble(k - 1.0);
            double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));
            double phi_kvadrat_pirsona = stepin_vilnost * Math.Pow((1.0 - (2.0 / (9.0 * stepin_vilnost)) + kvantil_start_rozpodil * Math.Sqrt(2.0 / (9.0 * stepin_vilnost))), 3.0);


            ///6. Однофакторний дисперсійний аналіз
            double Sm_kvadrat = 0;
            double n_big_all_data = 0;
            double zagalne_seredne = 0;
            for (int i = 0; i < k; i++)
            {
                n_big_all_data += vibirka[i].Count;
                zagalne_seredne += vibirka[i].Count * seredni_vibirok[i];
            }
            zagalne_seredne /= n_big_all_data;
            for (int i = 0; i < k; i++)
            {
                Sm_kvadrat += vibirka[i].Count * Math.Pow((seredni_vibirok[i] - zagalne_seredne), 2.0);
            }
            Sm_kvadrat/= Convert.ToDouble(k-1);
            double Sv_kvadrat = 0;
            for (int i = 0; i < k; i++)
            {
                Sv_kvadrat += (vibirka[i].Count - 1.0) * dispersia_vibirok[i];
            }
            Sv_kvadrat /= (n_big_all_data - k);

            //double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));//функція стандартного нормального розподілу ймовірностей
            double g1_u = (Math.Pow(kvantil_start_rozpodil, 3.0) + kvantil_start_rozpodil) / 4.0;
            double g2_u = (5.0 * Math.Pow(kvantil_start_rozpodil, 5.0) + 16.0 * Math.Pow(kvantil_start_rozpodil, 3.0) + 3.0 * kvantil_start_rozpodil) / 96.0;
            double g3_u = (3.0 * Math.Pow(kvantil_start_rozpodil, 7.0) + 19.0 * Math.Pow(kvantil_start_rozpodil, 5.0) + 17.0 * Math.Pow(kvantil_start_rozpodil, 3.0) - 15.0 * kvantil_start_rozpodil) / 384.0;
            double g4_u = (79.0 * Math.Pow(kvantil_start_rozpodil, 9.0) + 779.0 * Math.Pow(kvantil_start_rozpodil, 7.0) + 1482.0 * Math.Pow(kvantil_start_rozpodil, 5.0) - 1920.0 * Math.Pow(kvantil_start_rozpodil, 3.0) - 945.0 * kvantil_start_rozpodil) / 92160.0;
            double kvantil_Styudent = kvantil_start_rozpodil + (g1_u / stepin_vilnost) + (g2_u / Math.Pow(stepin_vilnost, 2.0)) + (g3_u / Math.Pow(stepin_vilnost, 3.0)) + (g4_u / Math.Pow(stepin_vilnost, 4.0));// квантиль розподілу Стьюдента
            double riven_znachuchosti = Double.Parse(textBox5.Text);
            double sigma_Fishera = (1.0 / ((data.Count / 2.0) - 1.0)) + (1.0 / ((data.Count / 2.0) - 1.0));
            double delts_Fishera = (1.0 / ((data.Count / 2.0) - 1.0)) - (1.0 / ((data.Count / 2.0) - 1.0));
            double kvantil_Fishera = kvantil_start_rozpodil * Math.Sqrt(sigma_Fishera / 2.0) - delts_Fishera * (Math.Pow(kvantil_start_rozpodil, 2.0) + 2.0)//квантиль Фішера
                + Math.Sqrt(sigma_Fishera / 2.0) * (((sigma_Fishera / 24.0) * (Math.Pow(kvantil_start_rozpodil, 2.0) + 3.0 * kvantil_start_rozpodil)) +
                (1.0 / 72.0) * (Math.Pow(delts_Fishera, 2.0) / sigma_Fishera) * (Math.Pow(kvantil_start_rozpodil, 3.0) + 11.0 * kvantil_start_rozpodil)) -
                ((sigma_Fishera * delts_Fishera) / 120.0) * (Math.Pow(kvantil_start_rozpodil, 4.0) + 9.0 * Math.Pow(kvantil_start_rozpodil, 2.0) + 8.0) + (Math.Pow(delts_Fishera, 3.0) / (3240.0 * sigma_Fishera)) * (3.0 * Math.Pow(kvantil_start_rozpodil, 4.0) + 7.0 * Math.Pow(kvantil_start_rozpodil, 2.0) - 16.0) +
                (Math.Sqrt(sigma_Fishera / 2.0)) * ((Math.Pow(sigma_Fishera, 2.0) / 1920.0) * (Math.Pow(kvantil_start_rozpodil, 5.0) + 20.0 * Math.Pow(kvantil_start_rozpodil, 3.0) + 15.0 * kvantil_start_rozpodil) + (Math.Pow(delts_Fishera, 4.0) / 2880.0) * (Math.Pow(kvantil_start_rozpodil, 5.0) + 44.0 * (Math.Pow(kvantil_start_rozpodil, 3.0)) + 183.0 * kvantil_start_rozpodil)
                + ((Math.Pow(delts_Fishera, 4.0)) / (155520.0 * Math.Pow(sigma_Fishera, 2.0))) * (9.0 * Math.Pow(kvantil_start_rozpodil, 5.0) - 284.0 * Math.Pow(kvantil_start_rozpodil, 3.0) - 1513.0 * kvantil_start_rozpodil));
            double fisher_exp = Math.Exp(2.0 * kvantil_Fishera);

            double F_disper_analis = Sm_kvadrat / Sv_kvadrat;
            dataGridView8.Columns.Add("1", "Назва");
            dataGridView8.Columns.Add("2", "Значення");
            dataGridView8.Columns.Add("3", "Критичне значення");
            dataGridView8.Columns.Add("4", "Висновок");


            if (radioButton1.Checked)
            {
                if (kriyeriy_bartleat <= phi_kvadrat_pirsona)
                {
                    dataGridView8.Rows.Add("Критерій Бартлета", Math.Round(kriyeriy_bartleat, 4), phi_kvadrat_pirsona, "+");
                }
                else
                {
                    dataGridView8.Rows.Add("Критерій Бартлета", Math.Round(kriyeriy_bartleat, 4), phi_kvadrat_pirsona, "-");
                }
                if (perevirka_norm_vibir == k)
                {
                    if (F_disper_analis <= fisher_exp)
                    {
                        dataGridView8.Rows.Add("Однофакторний дисперсійний аналіз", Math.Round(F_disper_analis, 4), fisher_exp, "+");
                    }
                    else
                    {
                        dataGridView8.Rows.Add("Однофакторний дисперсійний аналіз", Math.Round(F_disper_analis, 4), fisher_exp, "-");
                    }
                }

                if (perevirka_norm_vibir == 0 && k > 2)
                {
                    if (H_kriteriy > phi_kvadrat_pirsona)
                    {
                        dataGridView8.Rows.Add("H-критерій", Math.Round(H_kriteriy, 4), phi_kvadrat_pirsona, "+");
                    }
                    else
                    {
                        dataGridView8.Rows.Add("H-критерій", Math.Round(H_kriteriy, 4), phi_kvadrat_pirsona, "-");
                    }
                }
            }
            
           
            if (q_kohrena <= phi_kvadrat_pirsona)
            {
                dataGridView8.Rows.Add("Q-критерій", Math.Round(q_kohrena,4), phi_kvadrat_pirsona, "+");
            }
            else
            {
                dataGridView8.Rows.Add("Q-критерій", Math.Round(q_kohrena,4), phi_kvadrat_pirsona, "-");
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            liniyna_regresia.Checked = false;
            dovirchi_intevalu_every_regresii.Checked = false;
            tolerantrni_meshi.Checked = false;
            parabolichna_regresia.Checked = false;
            kvaziliniyna_regresia.Checked = false;
            dovirchi_intervaly_for_prognozu.Checked = false;
            method_Teyla.Checked = false;
            kvazi_bez_waga.Checked = false;
            Function_shilnosty.Checked = false;
            dataGridView10.Rows.Clear();
            dataGridView10.Columns.Clear();
            richTextBox3.Text = "";
            string[] str = textBox4.Text.Split(' ');
            int[] el = new int[2];
            int[] el22 = new int[2];
            List<int> nomer_oznak = new List<int>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            }
            List<List<double>> vector_oznak12 = new List<List<double>>();
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                List<double> vseredini_cycle = new List<double>();
                for (int j = 0; j < Universe.Data_Vectors[nomer_oznak[i]].Count; j++)
                {
                    vseredini_cycle.Add(Universe.Data_Vectors[nomer_oznak[i]][j]);
                }
                vector_oznak12.Add(vseredini_cycle);
            }
            for (int i = 0; i < 2; i++)
            {
                el[i] = nomer_oznak[i];
                el22[i] = nomer_oznak[i];
            } // вибираємо яку вибірку порівняти
            chart3.Series[0].IsVisibleInLegend = false;
            chart3.Series[1].IsVisibleInLegend = false;
            chart3.Series[2].IsVisibleInLegend = false;
            chart3.Series[3].IsVisibleInLegend = false;
            chart3.Series[4].IsVisibleInLegend = false;
            chart3.Series[5].IsVisibleInLegend = false;
            chart3.Series[6].IsVisibleInLegend = false;
            chart3.Series[7].IsVisibleInLegend = false;
            chart3.ChartAreas[0].AxisX.Title = "Х";
            chart3.ChartAreas[0].AxisY.Title = "Y";
            List<rang> data_x = new List<rang>();//місце де  зберігаємо дані котрі вибрали
            List<rang> data_y = new List<rang>();
            List<rang> first_vibirka = new List<rang>();
            List<rang> second_vibirka = new List<rang>();
            List<tochki_2d> coor_2d = new List<tochki_2d>();
            int vibir = 0;
            for (int i = 0; i < Universe.Data_Vectors.Count; i++)
            {
                if (i == el22[vibir])
                {
                    if (vibir == 0)
                    {
                        for (int t1 = 0; t1 < Universe.Data_Vectors[i].Count; t1++)
                        {
                            rang cheb = new rang();
                            cheb.which_elem = el22[vibir];
                            cheb.element = Universe.Data_Vectors[i][t1];
                            first_vibirka.Add(cheb);
                        }
                    }
                    else
                    {
                        for (int t1 = 0; t1 < Universe.Data_Vectors[i].Count; t1++)
                        {
                            rang cheb = new rang();
                            cheb.which_elem = el22[vibir];
                            cheb.element = Universe.Data_Vectors[i][t1];
                            second_vibirka.Add(cheb);
                        }
                    }

                    vibir++;

                }
                if (vibir == 2)
                {
                    break;
                }

            }
            vibir = 0;
            for (int i = 0; i < second_vibirka.Count; i++)
            {
                tochki_2d rep = new tochki_2d();
                rep.x = first_vibirka[i].element;
                rep.y = second_vibirka[i].element;
                coor_2d.Add(rep);

            }//добавили пари точок
            for (int i = 0; i < Universe.Data_Vectors.Count; i++)
            {
                if (i == el[vibir])
                {

                    //for (int t1 = 0; t1 < not_sortData[i].Count; t1++)
                    //{
                    //    rang cheb = new rang();
                    //    cheb.which_elem = el[vibir];
                    //    cheb.element = not_sortData[i][t1];
                    //    data.Add(cheb);
                    //}
                    if (vibir == 0)
                    {
                        for (int t1 = 0; t1 < Universe.Data_Vectors[i].Count; t1++)
                        {
                            rang cheb = new rang();
                            cheb.which_elem = el22[vibir];
                            cheb.element = Universe.Data_Vectors[i][t1];
                            cheb.index = t1;
                            data_x.Add(cheb);
                        }
                    }
                    else
                    {
                        for (int t1 = 0; t1 < Universe.Data_Vectors[i].Count; t1++)
                        {
                            rang cheb = new rang();
                            cheb.which_elem = el22[vibir];
                            cheb.element = Universe.Data_Vectors[i][t1];
                            cheb.index = t1;
                            data_y.Add(cheb);
                        }
                    }
                    vibir++;
                    if (vibir == 2)
                    {
                        break;
                    }
                }

            }
            //помістили дві вибірки і посортували щоб знайти ранги
            data_x.Sort(delegate (rang x, rang y) { return x.element.CompareTo(y.element); });
            data_y.Sort(delegate (rang x, rang y) { return x.element.CompareTo(y.element); });
            coor_2d.Sort(delegate (tochki_2d x, tochki_2d y) { return x.x.CompareTo(y.x); });
            List<List<rang>> our_rang = new List<List<rang>>();
            
            double elemnt_oflist = 0;
           
            List<rang> firstrang_x = new List<rang>();
            List<rang> firstrang_y = new List<rang>();
            
            for (int t1 = 0; t1 < data_x.Count; t1++)
            {
                elemnt_oflist = data_x[t1].element;
                rang squatr = new rang();
                squatr.element = elemnt_oflist;
                squatr.rangg = 1 + t1;
                squatr.which_elem = data_x[t1].which_elem;
                squatr.index = data_x[t1].index;

                int KKK = (int)firstrang_x.Count;

                if (KKK == 0)
                {
                    firstrang_x.Add(squatr);
                }
                else
                {
                    double enumer = 1;
                    double qt = 1 + t1;
                    for (int G = 0; G < KKK; G++)
                    {
                        if (firstrang_x[G].element == elemnt_oflist)
                        {
                            enumer++;
                            qt += (G + 1);
                        }
                    }
                    for (int G = 0; G < KKK; G++)
                    {
                        if (firstrang_x[G].element == elemnt_oflist)
                        {
                            firstrang_x[G].rangg = qt / enumer;
                        }
                    }
                    squatr.rangg = qt / enumer;
                    firstrang_x.Add(squatr);
                }
            }//пошук рангів
            for (int t1 = 0; t1 < data_y.Count; t1++)
            {
                elemnt_oflist = data_y[t1].element;
                rang squatr = new rang();
                squatr.element = elemnt_oflist;
                squatr.rangg = 1 + t1;
                squatr.which_elem = data_y[t1].which_elem;
                squatr.index = data_y[t1].index;
                int KKK = (int)firstrang_y.Count;

                if (KKK == 0)
                {
                    firstrang_y.Add(squatr);
                }
                else
                {
                    double enumer = 1;
                    double qt = 1 + t1;
                    for (int G = 0; G < KKK; G++)
                    {
                        if (firstrang_y[G].element == elemnt_oflist)
                        {
                            enumer++;
                            qt += (G + 1);
                        }
                    }
                    for (int G = 0; G < KKK; G++)
                    {
                        if (firstrang_y[G].element == elemnt_oflist)
                        {
                            firstrang_y[G].rangg = qt / enumer;
                        }
                    }
                    squatr.rangg = qt / enumer;
                    firstrang_y.Add(squatr);
                }
            }
            //лист де зберігається віраційний ряд з рангами двох вибірок
            List<rows> first_vibirka_var_ryad = Program.Variaciyniy_ryad(Universe.Data_Vectors[el22[0]]);//варіаційний ряд першої вибірки
            List<rows> second_vibirka_var_ryad = Program.Variaciyniy_ryad(Universe.Data_Vectors[el22[1]]);//варіаційний ряд другої вибірки
           // double suma_chastot = 0;
            //for (int i = 0; i < vibirku_var[el22[0]].Count; i++)
            //{
            //    suma_chastot = vibirku_var[el22[0]][i].chast;
            //    rows rwt = new rows();
            //    rwt.variant = vibirku_var[el22[0]][i].variant;
            //    rwt.count = vibirku_var[el22[0]][i].count;
            //    rwt.chast = Math.Round(suma_chastot, 7);
            //    first_vibirka_var_ryad.Add(rwt);
            //}//склали варіаційний ряд першої вибірки
            //suma_chastot = 0;
            //for (int i = 0; i < vibirku_var[el22[1]].Count; i++)
            //{
            //    suma_chastot = vibirku_var[el22[1]][i].chast;
            //    rows rwt = new rows();
            //    rwt.variant = vibirku_var[el22[1]][i].variant;
            //    rwt.count = vibirku_var[el22[1]][i].count;
            //    rwt.chast = Math.Round(suma_chastot, 7);
            //    second_vibirka_var_ryad.Add(rwt);
            //}//склали варіаційний ряд другої вибірки

            //////////////////////////////////////////////////////////////
            dataGridView7.Columns.Clear();
            dataGridView7.Rows.Clear();
            dataGridView7.AllowUserToAddRows = false;
            dataGridView7.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;
            
            if (first_vibirka_var_ryad.Count <= 100)
            {
                kilkclass = (int)Math.Sqrt(first_vibirka_var_ryad.Count);
            }
            else
            {
                kilkclass = (int)Math.Pow(first_vibirka_var_ryad.Count, 0.3333333);
            }
            if (!string.IsNullOrEmpty(textBox19.Text))
            {
                kilkclass = Convert.ToInt16(textBox19.Text);
            }
            if (kilkclass > 1)
            {
                for (int i = 0; i <kilkclass+1; i++)
                {
                    dataGridView7.Columns.Add("32"+i,"f"+i);
                }
                for (int i = 0; i < kilkclass+1; i++)
                {
                    dataGridView7.Rows.Add();
                }
            }
            //MessageBox.Show($"rows{dataGridView7.Rows.Count} colo{dataGridView7.Columns.Count}  kilkas{kilkclass}");
            // побудова 2д гістограми
            double sered = first_vibirka_var_ryad[first_vibirka_var_ryad.Count - 1].variant - first_vibirka_var_ryad[0].variant;
            double sered_second = second_vibirka_var_ryad[second_vibirka_var_ryad.Count - 1].variant - second_vibirka_var_ryad[0].variant;
            double shah = sered /Convert.ToDouble(kilkclass);
            double shah2 = sered_second / Convert.ToDouble(kilkclass);
            double shaht = shah;
            double shaht2 = shah2;
            shah += first_vibirka_var_ryad[0].variant;
            shah2 += second_vibirka_var_ryad[0].variant;
            double sum = first_vibirka_var_ryad[0].variant;
            double sum2 = second_vibirka_var_ryad[0].variant;           
            int column_2d_hist = 1;
            int chastoty = 0;
            List<table_of_chastot> table_chastot = new List<table_of_chastot>();
            for (int i = 0; i < coor_2d.Count; i++)
            {
                int rows_2d_hist = kilkclass - 1;
                for (int o = 0; o < coor_2d.Count; o++)
                {
                    for (int p = 0; p < coor_2d.Count; p++)
                    {
                        if ((coor_2d[p].x <= shah && coor_2d[p].x >= sum) && (coor_2d[p].y <= shah2 && coor_2d[p].y >= sum2))
                        {
                            chastoty+= 1;
                        }
                    }
                    dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Value =Math.Round(Convert.ToDouble(chastoty) / Convert.ToDouble(coor_2d.Count),4);
                    double sumtwosum= Convert.ToDouble(chastoty) / Convert.ToDouble(coor_2d.Count);
                    if ( sumtwosum >= 0.1)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(0, 102, 0);
                    }
                    else if (sumtwosum < 0.1 && sumtwosum >= 0.04)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(0, 153, 0);
                    }
                    else if (sumtwosum < 0.04 && sumtwosum >= 0.03)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(0, 204, 0);
                    }
                    else if (sumtwosum < 0.03 && sumtwosum >= 0.02)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(0, 255, 0);
                    }
                    else if (sumtwosum < 0.02 && sumtwosum >= 0.01)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(51, 255, 51);
                    }
                    else if (sumtwosum < 0.01 && sumtwosum >= 0.007)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(102, 255, 102);
                    }
                    else if (sumtwosum < 0.007 && sumtwosum >= 0.003)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(153, 255, 153);
                    }
                    else if (sumtwosum < 0.003 && sumtwosum >= 0)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(204, 255, 204);
                    }
                    dataGridView7.Rows[rows_2d_hist].Cells[0].Value = Math.Round(shah2, 4);
                    table_of_chastot tbl = new table_of_chastot();
                    tbl.kil_chastot = Convert.ToDouble(chastoty);
                    tbl.chastota = sumtwosum;
                    tbl.variant_po_x = (sum+shah) / 2.0;
                    tbl.variant_po_y = (sum2+shah2) / 2.0;
                    tbl.index_x = i;
                    tbl.index_y = o;
                    tbl.kvadrat_x_end = shah;
                    tbl.kvadrat_y_end = shah2;
                    tbl.kvadrat_y_zero = sum2;
                    tbl.kvadrat_x_zero = sum;
                    table_chastot.Add(tbl);
                    rows_2d_hist--;
                    chastoty = 0;
                    sum2 += shaht2;
                    shah2 += shaht2;
                    if (rows_2d_hist<0)
                    {
                        break;
                    }
                }
                dataGridView7.Rows[kilkclass].Cells[column_2d_hist].Value = Math.Round(shah, 4);
                sum += shaht;
                shah += shaht;
                shah2 = sered_second / Convert.ToDouble(kilkclass);
                shah2 += second_vibirka_var_ryad[0].variant;
                sum2 = second_vibirka_var_ryad[0].variant;
                column_2d_hist++;
                
                if (column_2d_hist>kilkclass)
                {
                    break;
                }


            }//2d hostograma
            //double suma_chasto1t = 0;
            //for (int i = 0; i < dataGridView7.ColumnCount - 1; i++)
            //{
            //    for (int k = 1; k < dataGridView7.RowCount; k++)
            //    {
            //        if (double.TryParse(dataGridView7.Rows[i].Cells[k].Value.ToString(), out double res))
            //        {
            //            suma_chasto1t += res;
            //        }
            //    }
            //    MessageBox.Show($"{suma_chasto1t}");
            //}
            Table_Of_Chastots.Clear();
            for (int i = 0; i < table_chastot.Count; i++)
            {
                Table_Of_Chastots.Add(table_chastot[i]);
            }
            double y_seredne = 0;
            double x_seredne = 0;
            double x_dispersia = 0;
            double y_dispersia = 0;
            double xy_seredne = 0;
            for (int i = 0; i < coor_2d.Count; i++)
            {
                y_seredne += coor_2d[i].y;
                x_seredne += coor_2d[i].x;
                xy_seredne += (coor_2d[i].x * coor_2d[i].y);
            }
            y_seredne /= Convert.ToDouble(coor_2d.Count);
            x_seredne /= Convert.ToDouble(coor_2d.Count);
            xy_seredne /= Convert.ToDouble(coor_2d.Count);
            for (int i = 0; i < coor_2d.Count; i++)
            {
                x_dispersia += Math.Pow(coor_2d[i].x - x_seredne, 2.0);
                y_dispersia += Math.Pow(coor_2d[i].y - y_seredne, 2.0);
            }
            x_dispersia /= Convert.ToDouble(coor_2d.Count-1);
            y_dispersia /= Convert.ToDouble(coor_2d.Count-1);
            List<double> pxaxsaxa = new List<double>();
            double phi_kvadrat_pirsona = 0; ////оцінки адекватності відтворення
            for (int i = 0; i < kilkclass; i++)
            {
                double fun2d = 0;
                double p_zirochka = 0;
                for (int j = 0; j < kilkclass; j++)
                {
                    for (int t = 0; t < table_chastot.Count; t++)
                    {
                        if (table_chastot[t].index_x==i && table_chastot[t].index_y==j)
                        {
                            fun2d = func_2d_norm(Math.Sqrt(x_dispersia), Math.Sqrt(y_dispersia), x_seredne, y_seredne, table_chastot[t].variant_po_x, table_chastot[t].variant_po_y);
                            p_zirochka = fun2d * shaht * shaht2;
                            pxaxsaxa.Add(p_zirochka);
                            phi_kvadrat_pirsona+=(Math.Pow(table_chastot[t].chastota- p_zirochka, 2)/(p_zirochka));


                        }
                    }
                    
                }
            }///оцінки адекватності відтворення
            double sss = func_2d_norm(Math.Sqrt(x_dispersia), Math.Sqrt(y_dispersia), x_seredne, y_seredne, 5.0, 5.0);
            double koef_par_kor = (xy_seredne-(x_seredne*y_seredne))/(Math.Sqrt(x_dispersia*y_dispersia));///оцінка парного коефіцієнта кореляції
            koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d.Count) / Convert.ToDouble(coor_2d.Count - 1));


            chart3.Series[0].Points.Clear();
            for (int i = 0; i < first_vibirka.Count; i++)
            {
                chart3.Series[0].Points.AddXY(first_vibirka[i].element, second_vibirka[i].element);
            }///кореляційні поля
             ///кореляційні відношення///
            double koef_kor_vidnoshenya = 0;///коефіцієнт кореляційного відношення


            sered = first_vibirka_var_ryad[first_vibirka_var_ryad.Count - 1].variant - first_vibirka_var_ryad[0].variant;
            sered_second = second_vibirka_var_ryad[second_vibirka_var_ryad.Count - 1].variant - second_vibirka_var_ryad[0].variant;
            shah = sered / Convert.ToDouble(kilkclass);
            shah2 = sered_second / Convert.ToDouble(kilkclass);
            shaht = shah;
            shaht2 = shah2;
            shah += first_vibirka_var_ryad[0].variant;
            shah2 += second_vibirka_var_ryad[0].variant;
            sum = first_vibirka_var_ryad[0].variant;
            sum2 = second_vibirka_var_ryad[0].variant;
            column_2d_hist = 1;
            chastoty = 0;
            // List<table_of_chastot> table_chastot = new List<table_of_chastot>();
            double y_diper_kor_vidnohenya = 0;
            for (int i = 0; i < kilkclass; i++)
            {
               
                chastoty = 0;
                double y_v_vibirci = 0;
                for (int p = 0; p < coor_2d.Count; p++)
                {
                    if (coor_2d[p].x <= shah && coor_2d[p].x >= sum)
                    {
                        chastoty += 1;
                        y_v_vibirci += coor_2d[p].y;
                        y_diper_kor_vidnohenya += Math.Pow(coor_2d[p].y - y_seredne, 2.0);
                    }
                }
                double y_ser = y_v_vibirci / Convert.ToDouble(chastoty);
                
                koef_kor_vidnoshenya += chastoty * Math.Pow(y_ser-y_seredne,2.0);
                sum += shaht;
                shah += shaht;
                
                                 
            }
            //koef_kor_vidnoshenya = (koef_kor_vidnoshenya * Convert.ToDouble(coor_2d.Count - 1)) / y_dispersia;
            koef_kor_vidnoshenya = (koef_kor_vidnoshenya ) / y_diper_kor_vidnohenya;
            koef_kor_vidnoshenya = Math.Sqrt(koef_kor_vidnoshenya);
            /////кінець кореляційного відношення///////
            for (int o = 0; o < coor_2d.Count; o++)
            {
                double x1111 = coor_2d[o].x;
                
                int indexacia = firstrang_x[o].index;
                for (int t = 0; t < firstrang_y.Count; t++)
                {
                    if (indexacia == firstrang_y[t].index)
                    {
                        coor_2d[o].rang_x = firstrang_x[o].rangg;
                        coor_2d[o].rang_y = firstrang_y[t].rangg;
                        
                        break;
                    }
                }
            }///знайшли пари рангів
            tochki_2Ds.Clear();
            for (int i = 0; i < coor_2d.Count; i++)
            {
                tochki_2Ds.Add(coor_2d[i]);
            }
            double kor_Spirmena = 0;//оцінка рангового коефіцієнта кореляції Спірмена
            double d_l = 0;
            for (int i = 0; i < coor_2d.Count; i++)
            {
                d_l += Math.Pow(coor_2d[i].rang_x - coor_2d[i].rang_y, 2.0);
            }
            kor_Spirmena = 1.0 - ((6.0*d_l) / (Convert.ToDouble(coor_2d.Count) * (Math.Pow(Convert.ToDouble(coor_2d.Count), 2.0) - 1.0)));
            //ранговий коефіцієнт кендала////
            double s_rangu_kendala = 0;
            for (int l = 0; l < coor_2d.Count-1; l++)
            {
                for (int j = l+1; j < coor_2d.Count; j++)
                {
                    if (coor_2d[l].rang_y < coor_2d[j].rang_y)
                    {
                        s_rangu_kendala += 1.0;

                    }
                    else
                    {
                        s_rangu_kendala += (-1.0);
                    }
                }
            }
            ///////////////таблиця сполучень/////////////////
            kilkclass = 2;
            sered = first_vibirka_var_ryad[first_vibirka_var_ryad.Count - 1].variant - first_vibirka_var_ryad[0].variant;
            sered_second = second_vibirka_var_ryad[second_vibirka_var_ryad.Count - 1].variant - second_vibirka_var_ryad[0].variant;
            shah = sered / Convert.ToDouble(kilkclass);
            shah2 = sered_second / Convert.ToDouble(kilkclass);
            shaht = shah;
            shaht2 = shah2;
            shah += first_vibirka_var_ryad[0].variant;
            shah2 += second_vibirka_var_ryad[0].variant;
            sum = first_vibirka_var_ryad[0].variant;
            sum2 = second_vibirka_var_ryad[0].variant;
            column_2d_hist = 1;
            chastoty = 0;
            //List<table_of_chastot> table_chastot = new List<table_of_chastot>();
            List<table_of_spoluchen> table_spoluchen = new List<table_of_spoluchen>();
            for (int i = 0; i < coor_2d.Count; i++)
            {
                int rows_2d_hist = kilkclass - 1;
                for (int o = 0; o < coor_2d.Count; o++)
                {
                    for (int p = 0; p < coor_2d.Count; p++)
                    {
                        if ((coor_2d[p].x <= shah && coor_2d[p].x >= sum) && (coor_2d[p].y <= shah2 && coor_2d[p].y >= sum2))
                        {
                            chastoty += 1;
                        }
                    }

                    double sumtwosum = Convert.ToDouble(chastoty) ;
                    table_of_spoluchen tbl_s = new table_of_spoluchen();
                    tbl_s.chastota = sumtwosum;
                    tbl_s.index_x = i;
                    tbl_s.index_y = o;
                    table_spoluchen.Add(tbl_s);

                    rows_2d_hist--;
                    chastoty = 0;
                    sum2 += shaht2;
                    shah2 += shaht2;
                    if (rows_2d_hist < 0)
                    {
                        break;
                    }
                }
                sum += shaht;
                shah += shaht;
                shah2 = sered_second / Convert.ToDouble(kilkclass);
                shah2 += second_vibirka_var_ryad[0].variant;
                sum2 = second_vibirka_var_ryad[0].variant;
                column_2d_hist++;

                if (column_2d_hist > kilkclass)
                {
                    break;
                }


            }
            double M_zero = table_spoluchen[0].chastota + table_spoluchen[1].chastota;
            double M_one = table_spoluchen[2].chastota + table_spoluchen[3].chastota;
            double N_zero = table_spoluchen[0].chastota + table_spoluchen[2].chastota;
            double N_one = table_spoluchen[1].chastota + table_spoluchen[3].chastota;
            double N_of_n0_n1= N_zero + N_one;
            double index_Fechner = 0;//індекс Фехнера
            index_Fechner = (table_spoluchen[0].chastota+ table_spoluchen[3].chastota- table_spoluchen[1].chastota- table_spoluchen[2].chastota) / (table_spoluchen[0].chastota + table_spoluchen[3].chastota + table_spoluchen[1].chastota + table_spoluchen[2].chastota);
            double koef_spoluchen_FI = (table_spoluchen[0].chastota * table_spoluchen[3].chastota - table_spoluchen[1].chastota * table_spoluchen[2].chastota) / Math.Sqrt(N_zero*N_one*M_zero*M_one);//Коефіцієнт сполучень Фі
            double koef_zvazku_Yulla_Y = (Math.Sqrt(table_spoluchen[0].chastota * table_spoluchen[3].chastota) - Math.Sqrt(table_spoluchen[1].chastota * table_spoluchen[2].chastota)) / (Math.Sqrt(table_spoluchen[0].chastota* table_spoluchen[3].chastota) +Math.Sqrt(table_spoluchen[1].chastota* table_spoluchen[2].chastota));//Коефіцієнти зв’язку Юла Y
            double koef_zvazku_Yulla_Q = (table_spoluchen[0].chastota * table_spoluchen[3].chastota- table_spoluchen[1].chastota * table_spoluchen[2].chastota) / (table_spoluchen[0].chastota * table_spoluchen[3].chastota + table_spoluchen[1].chastota * table_spoluchen[2].chastota);
            double s_Yulla_q = (1.0 - Math.Pow(koef_zvazku_Yulla_Q, 2.0)) * Math.Sqrt((1.0/ table_spoluchen[0].chastota) +(1.0 / table_spoluchen[1].chastota) +(1.0 / table_spoluchen[2].chastota) +(1.0 / table_spoluchen[3].chastota));
            s_Yulla_q /= 2.0;
            double s_Yulla_y = (1.0 - Math.Pow(koef_zvazku_Yulla_Y, 2.0)) * Math.Sqrt((1.0 / table_spoluchen[0].chastota) + (1.0 / table_spoluchen[1].chastota) + (1.0 / table_spoluchen[2].chastota) + (1.0 / table_spoluchen[3].chastota));
            s_Yulla_y /= 4.0;
            double kvantil_yulla_q = koef_zvazku_Yulla_Q / s_Yulla_q;
            double kvantil_yulla_y=koef_zvazku_Yulla_Y/s_Yulla_y;
            ///таблиця перехресного табулювання ////
            if (first_vibirka_var_ryad.Count <= 100)
            {
                kilkclass = (int)Math.Sqrt(first_vibirka_var_ryad.Count);
            }
            else
            {
                kilkclass = (int)Math.Pow(first_vibirka_var_ryad.Count, 0.3333333);
            }
            double phi_TABLE_kvadra_pirsona = 0;
            double N_ij = 0;
            for (int m = 0; m <table_chastot.Count ; m++)
            {
                N_ij += table_chastot[m].kil_chastot;
            }
            for (int i = 0; i < kilkclass; i++)
            {
                for (int j = 0; j < kilkclass; j++)
                {
                    for (int t = 0; t < table_chastot.Count; t++)
                    {
                        if (table_chastot[t].index_x == j && table_chastot[t].index_y==i)
                        {
                            double n_i = 0;
                            double m_j = 0;
                            for (int m = 0; m < kilkclass; m++)
                            {
                                for (int w = 0; w < table_chastot.Count; w++)
                                {
                                    if (table_chastot[w].index_y == i && table_chastot[w].index_x==m)
                                    {
                                        n_i += table_chastot[w].kil_chastot;
                                    }
                                }
                            }//обраховуєм ni
                            for (int n = 0; n < kilkclass; n++)
                            {
                                for (int w = 0; w < table_chastot.Count; w++)
                                {
                                    if (table_chastot[w].index_y == n && table_chastot[w].index_x == j)
                                    {
                                        m_j += table_chastot[w].kil_chastot;
                                    }
                                }
                            }//обраховуєм mj
                            double n_ijji = 0;
                            n_ijji = (n_i * m_j) / N_ij;
                            phi_TABLE_kvadra_pirsona += Math.Pow(table_chastot[t].kil_chastot - n_ijji, 2.0) / n_ijji;

                            break;
                            

                        }
                    }
                }
            }//знайшли критерій х^2


            double kof_spoluchen_Pirsona = Math.Sqrt(phi_TABLE_kvadra_pirsona/(N_ij+phi_TABLE_kvadra_pirsona));//Коефіцієнт сполучень Пірсона
            /////міра звязку кендалла///
            double P_kendela = 0;
            for (int i = 0; i < kilkclass; i++)
            {
                for (int j = 0; j < kilkclass; j++)
                {
                    for (int t = 0; t < table_chastot.Count; t++)
                    {
                        if (table_chastot[t].index_x == j && table_chastot[t].index_y == i)
                        {
                            double n_kll = 0;
                            for (int k = i+1; k < kilkclass; k++)
                            {
                                for (int l = j+1; l < kilkclass; l++)
                                {
                                    for (int t1 = 0; t1 < table_chastot.Count; t1++)
                                    {
                                        if (table_chastot[t1].index_x == l && table_chastot[t1].index_y == k)
                                        {
                                            n_kll += table_chastot[t1].kil_chastot;
                                        }
                                    }
                                }
                            }
                            P_kendela += table_chastot[t].kil_chastot*n_kll;
                        }
                    }
                }
            }

            double Q_kendela = 0;
            for (int i = 0; i < kilkclass; i++)
            {
                for (int j = 0; j < kilkclass; j++)
                {
                    for (int t = 0; t < table_chastot.Count; t++)
                    {
                        if (table_chastot[t].index_x == j && table_chastot[t].index_y == i)
                        {
                            double n_kll = 0;
                            for (int k = i + 1; k < kilkclass; k++)
                            {
                                for (int l = 0; l < j-1; l++)
                                {
                                    for (int t1 = 0; t1 < table_chastot.Count; t1++)
                                    {
                                        if (table_chastot[t1].index_x == l && table_chastot[t1].index_y == k)
                                        {
                                            n_kll += table_chastot[t1].kil_chastot;
                                        }
                                    }
                                }
                            }
                            Q_kendela += table_chastot[t].kil_chastot * n_kll;
                        }
                    }
                }
            }
            double T1_kendela = 0;
            for (int i = 0; i < kilkclass; i++)
            {
                double n_i = 0;
                
                for (int m = 0; m < kilkclass; m++)
                {
                    for (int w = 0; w < table_chastot.Count; w++)
                    {
                        if (table_chastot[w].index_y == i && table_chastot[w].index_x == m)
                        {
                            n_i += table_chastot[w].kil_chastot;
                        }
                    }
                }//обраховуєм ni
                T1_kendela += n_i * (n_i - 1.0);
            }
            T1_kendela /= 2.0;

            double T2_kendela = 0;
            for (int j = 0; j < kilkclass; j++)
            {
                double m_j = 0;
                for (int n = 0; n < kilkclass; n++)
                {
                    for (int w = 0; w < table_chastot.Count; w++)
                    {
                        if (table_chastot[w].index_y == n && table_chastot[w].index_x == j)
                        {
                            m_j += table_chastot[w].kil_chastot;
                        }
                    }
                }//обраховуєм mj
                T2_kendela+=m_j * (m_j - 1.0);
            }
            T2_kendela /= 2.0;

            double mira_zvazku_Kendalla = (P_kendela-Q_kendela) /Math.Sqrt(((N_ij*(N_ij-1.0)/2.0)-T1_kendela)*((N_ij * (N_ij - 1.0) / 2.0) - T2_kendela));
            ////статистика Стюарда////

            List<table_of_spoluchen> table_spoluchen_stuard = new List<table_of_spoluchen>();
            double statistika_stuarda = 0;
            double zn_stuard = 0;
            if (!string.IsNullOrEmpty(textBox9.Text))
            {
                
                str = textBox9.Text.Split(' ');
                int[] mxn = new int[2];
                for (int i = 0; i < 2; i++)
                {
                    mxn[i] = int.Parse(str[i]);

                }
                //kilkclass = 2;
                sered = first_vibirka_var_ryad[first_vibirka_var_ryad.Count - 1].variant - first_vibirka_var_ryad[0].variant;
                sered_second = second_vibirka_var_ryad[second_vibirka_var_ryad.Count - 1].variant - second_vibirka_var_ryad[0].variant;
                shah = sered / Convert.ToDouble(mxn[0]);
                shah2 = sered_second / Convert.ToDouble(mxn[1]);
                shaht = shah;
                shaht2 = shah2;
                shah += first_vibirka_var_ryad[0].variant;
                shah2 += second_vibirka_var_ryad[0].variant;
                sum = first_vibirka_var_ryad[0].variant;
                sum2 = second_vibirka_var_ryad[0].variant;
                column_2d_hist = 1;
                chastoty = 0;
                for (int i = 0; i < mxn[0]; i++)
                {
                    
                    for (int o = 0; o < mxn[1]; o++)
                    {
                        for (int p = 0; p < coor_2d.Count; p++)
                        {
                            if ((coor_2d[p].x <= shah && coor_2d[p].x >= sum) && (coor_2d[p].y <= shah2 && coor_2d[p].y >= sum2))
                            {
                                chastoty += 1;
                            }
                        }

                        double sumtwosum = Convert.ToDouble(chastoty);
                        table_of_spoluchen tbl_s = new table_of_spoluchen();
                        tbl_s.chastota = sumtwosum;
                        tbl_s.index_x = i;
                        tbl_s.index_y = o;
                        table_spoluchen_stuard.Add(tbl_s);

                        
                        chastoty = 0;
                        sum2 += shaht2;
                        shah2 += shaht2;
                        
                    }
                    sum += shaht;
                    shah += shaht;
                    shah2 = sered_second / Convert.ToDouble(mxn[1]);
                    shah2 += second_vibirka_var_ryad[0].variant;
                    sum2 = second_vibirka_var_ryad[0].variant;
                    column_2d_hist++;



                }///стоврили нову таблицю сполучень
                N_ij = 0;

                for (int m = 0; m < table_spoluchen_stuard.Count; m++)
                {
                    N_ij += table_spoluchen_stuard[m].chastota;
                }

                P_kendela = 0;
                for (int i = 0; i < mxn[1]; i++)
                {
                    for (int j = 0; j < mxn[0]; j++)
                    {
                        for (int t = 0; t < table_spoluchen_stuard.Count; t++)
                        {
                            if (table_spoluchen_stuard[t].index_x == j && table_spoluchen_stuard[t].index_y == i)
                            {
                                double n_kll = 0;
                                for (int k = i + 1; k < mxn[1]; k++)
                                {
                                    for (int l = j + 1; l < mxn[0]; l++)
                                    {
                                        for (int t1 = 0; t1 < table_spoluchen_stuard.Count; t1++)
                                        {
                                            if (table_spoluchen_stuard[t1].index_x == l && table_spoluchen_stuard[t1].index_y == k)
                                            {
                                                n_kll += table_spoluchen_stuard[t1].chastota;
                                            }
                                        }
                                    }
                                }
                                P_kendela += table_spoluchen_stuard[t].chastota * n_kll;
                            }
                        }
                    }
                }//порахували P

                 Q_kendela = 0;
                for (int i = 0; i < mxn[1]; i++)
                {
                    for (int j = 0; j < mxn[0]; j++)
                    {
                        for (int t = 0; t < table_spoluchen_stuard.Count; t++)
                        {
                            if (table_spoluchen_stuard[t].index_x == j && table_spoluchen_stuard[t].index_y == i)
                            {
                                double n_kll = 0;
                                for (int k = i + 1; k < mxn[1]; k++)
                                {
                                    for (int l = 0; l < j - 1; l++)
                                    {
                                        for (int t1 = 0; t1 < table_spoluchen_stuard.Count; t1++)
                                        {
                                            if (table_spoluchen_stuard[t1].index_x == l && table_spoluchen_stuard[t1].index_y == k)
                                            {
                                                n_kll += table_spoluchen_stuard[t1].chastota;
                                            }
                                        }
                                    }
                                }
                                Q_kendela += table_spoluchen_stuard[t].chastota * n_kll;
                            }
                        }
                    }
                }///порахували Q
                double min_z_rozmiru_tabl = 0;
                if (mxn[0] < mxn[1])
                {
                    min_z_rozmiru_tabl = Convert.ToDouble(mxn[0]);
                }
                else
                {
                    min_z_rozmiru_tabl = Convert.ToDouble(mxn[1]);
                }

                statistika_stuarda = (2.0*(P_kendela-Q_kendela)*min_z_rozmiru_tabl) / (Math.Pow(N_ij,2.0)*(min_z_rozmiru_tabl-1.0));
                ///значущість Стюарда
                double suma_pid_znakom_korena = 0;
                for (int i = 0; i < mxn[1]; i++)
                {
                    for (int j = 0; j < mxn[0]; j++)
                    {
                        for (int t = 0; t < table_spoluchen_stuard.Count; t++)
                        {
                            if (table_spoluchen_stuard[t].index_x == j && table_spoluchen_stuard[t].index_y == i)
                            {
                               
                                double A_stuarda = 0;
                                for (int k = i + 1; k < mxn[1]; k++)
                                {
                                    for (int l = j+1; l < mxn[0]; l++)
                                    {
                                        for (int t1 = 0; t1 < table_spoluchen_stuard.Count; t1++)
                                        {
                                            if (table_spoluchen_stuard[t1].index_x == l && table_spoluchen_stuard[t1].index_y == k)
                                            {
                                                A_stuarda += table_spoluchen_stuard[t1].chastota;
                                            }
                                        }
                                    }
                                }
                                for (int k = 0; k < i-1; k++)
                                {
                                    for (int l = 0; l < j-1; l++)
                                    {
                                        for (int t1 = 0; t1 < table_spoluchen_stuard.Count; t1++)
                                        {
                                            if (table_spoluchen_stuard[t1].index_x == l && table_spoluchen_stuard[t1].index_y == k)
                                            {
                                                A_stuarda += table_spoluchen_stuard[t1].chastota;
                                            }
                                        }
                                    }
                                }
                                double B_stuarda = 0;
                                for (int k = i+1; k < mxn[1] ; k++)
                                {
                                    for (int l = 0; l < j-1; l++)
                                    {
                                        for (int t1 = 0; t1 < table_spoluchen_stuard.Count; t1++)
                                        {
                                            if (table_spoluchen_stuard[t1].index_x == l && table_spoluchen_stuard[t1].index_y == k)
                                            {
                                                B_stuarda += table_spoluchen_stuard[t1].chastota;
                                            }
                                        }
                                    }
                                }
                                for (int k = 0; k < i-1; k++)
                                {
                                    for (int l = j+1; l < mxn[0]; l++)
                                    {
                                        for (int t1 = 0; t1 < table_spoluchen_stuard.Count; t1++)
                                        {
                                            if (table_spoluchen_stuard[t1].index_x == l && table_spoluchen_stuard[t1].index_y == k)
                                            {
                                                B_stuarda += table_spoluchen_stuard[t1].chastota;
                                            }
                                        }
                                    }
                                }
                               

                                suma_pid_znakom_korena += table_spoluchen_stuard[t].chastota * Math.Pow(A_stuarda-B_stuarda,2.0);
                            }
                        }
                    }
                }
                double znachusist_stuarda = Math.Sqrt(Math.Pow(N_ij, 2.0) * suma_pid_znakom_korena - (4.0 * N_ij * (P_kendela - Q_kendela)));
                znachusist_stuarda = znachusist_stuarda * ((2.0*min_z_rozmiru_tabl)/(Math.Pow(N_ij,3.0)*(min_z_rozmiru_tabl-1.0)));
                zn_stuard = znachusist_stuarda;
                


            }
            double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));

            dataGridView9.Rows.Clear();
            dataGridView9.Columns.Clear();
            dataGridView9.AllowUserToAddRows = false;
            dataGridView9.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView9.Columns.Add("1", "Характеристика");
            dataGridView9.Columns.Add("2", "INF");
            dataGridView9.Columns.Add("3", "Значення");
            dataGridView9.Columns.Add("4", "SUP");
            //dataGridView9.Columns.Add("5", "SKV");

            double znachustits_spirmena = Math.Sqrt((1.0-Math.Pow(kor_Spirmena,2.0))/(N_ij-2.0));
            double znachustits_kendala = Math.Sqrt(((4.0*N_ij)+10.0) / (9.0*(Math.Pow(N_ij,2.0)-N_ij)));
            dataGridView9.Rows.Add("Середнє Х", "", Math.Round(x_seredne, 4), "");
            dataGridView9.Rows.Add("Середнє Y", "", Math.Round(y_seredne, 4), "");
            dataGridView9.Rows.Add("Дисперіся Х", "", Math.Round(x_dispersia, 4), "");
            dataGridView9.Rows.Add("Дисперсія Y", "", Math.Round(y_dispersia, 4), "");
            dataGridView9.Rows.Add("Оцінка адекватності відтворення ХY", "", Math.Round(phi_kvadrat_pirsona, 4), "");
            dataGridView9.Rows.Add("Коефіцієнт кореляції", Math.Round((koef_par_kor + ((koef_par_kor * (1.0 - Math.Pow(koef_par_kor, 2.0))) / (2.0 * Convert.ToDouble(coor_2d.Count))) - kvantil_start_rozpodil * ((1.0 - Math.Pow(koef_par_kor, 2.0)) / Math.Sqrt(Convert.ToDouble(coor_2d.Count) - 1.0))), 4), Math.Round(koef_par_kor, 4), Math.Round((koef_par_kor + ((koef_par_kor * (1.0 - Math.Pow(koef_par_kor, 2.0))) / (2.0 * Convert.ToDouble(coor_2d.Count))) + kvantil_start_rozpodil * ((1.0 - Math.Pow(koef_par_kor, 2.0)) / Math.Sqrt(Convert.ToDouble(coor_2d.Count) - 1.0))), 4));
            dataGridView9.Rows.Add("Кореляційне відношення", "", Math.Round(koef_kor_vidnoshenya, 4), "");
            dataGridView9.Rows.Add("Індекс Фехнера", "", Math.Round(index_Fechner, 4), "");
            dataGridView9.Rows.Add("Коефіцієнт сполучень «Фі»", "", Math.Round(koef_spoluchen_FI, 4), "");
            dataGridView9.Rows.Add("Коефіцієнти зв’язку Юла Q", "", Math.Round(koef_zvazku_Yulla_Q, 4), "");
            dataGridView9.Rows.Add("Коефіцієнти зв’язку Юла Y", "", Math.Round(koef_zvazku_Yulla_Y, 4), "");
            dataGridView9.Rows.Add("Коефіцієнт сполучень Пірсона", "", Math.Round(kof_spoluchen_Pirsona, 4), "");
            richTextBox2.Text = "";
            double t_test_koef_korelacii = (koef_par_kor*Math.Sqrt(N_ij-2.0)) / (Math.Sqrt(1.0-Math.Pow(koef_par_kor,2.0)));
            double t_test_kor_vidnoshenya=(Math.Sqrt(koef_kor_vidnoshenya) *Math.Sqrt(N_ij-2.0)) / (Math.Sqrt(1.0-koef_kor_vidnoshenya));
            double f_test_kor_vidnoshenya = ((koef_kor_vidnoshenya) / (1.0 - koef_kor_vidnoshenya)) * ((Convert.ToDouble(N_ij - kilkclass)) / (Convert.ToDouble(kilkclass - 1)));
            double t_test_rang_koef_spirmena= (kor_Spirmena * Math.Sqrt(N_ij - 2.0)) / (Math.Sqrt(1.0 - Math.Pow(kor_Spirmena, 2.0)));
            double koef_rang_kendal = ((2.0*s_rangu_kendala) / (N_ij*(N_ij-1.0)));
            double u_test_rang_koef_kendalla = ((3.0 * koef_rang_kendal) / Math.Sqrt(2.0 * (2.0 * N_ij + 5.0))) * Math.Sqrt(N_ij * (N_ij - 1.0));
            double phi_koef_spoluchen_fi = N_ij * Math.Pow(koef_spoluchen_FI, 2.0);

            double kvantil_pirsona = kvantil_Pirsona(kvantil_start_rozpodil,Convert.ToDouble(kilkclass*kilkclass-2.0));
            if (phi_kvadrat_pirsona>=kvantil_pirsona)
            {
                richTextBox2.Text += $"χ={Math.Round(phi_kvadrat_pirsona, 4)} χ>={Math.Round(kvantil_pirsona,4)} двовимірний нормальний розподіл не є адекватним\n";
            }
            else
            {
                richTextBox2.Text += $"χ={Math.Round(phi_kvadrat_pirsona, 4)} χ<{Math.Round(kvantil_pirsona, 4)} двовимірний нормальний розподіл є адекватним\n";
            }

            double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_ij - 2.0);
            if (Math.Abs(t_test_kor_vidnoshenya)<=kvantil_studen)
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_kor_vidnoshenya),4)} |t|<={Math.Round(kvantil_studen,4)} кореляційний зв’язок поміж η,ξ відсутній\n";
            }
            else
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_kor_vidnoshenya), 4)} |t|<={Math.Round(kvantil_studen, 4)}  кореляційний зв’язок поміж η,ξ присутній\n";
            }
            kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_ij - 1.0);
            if (Math.Abs(t_test_koef_korelacii) <= kvantil_studen)
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_koef_korelacii), 4)} |t|<={Math.Round(kvantil_studen, 4)} оцінкa коефіцієнта кореляції  не є значуща\n";
            }
            else
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_koef_korelacii), 4)} |t|>{Math.Round(kvantil_studen, 4)}  оцінкa коефіцієнта кореляції  є значуща\n";
            }
            kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_ij - 2.0);
            double kvanta_fisher = kvantil_Fishera(kvantil_start_rozpodil,Convert.ToDouble(kilkclass-1),Convert.ToDouble(N_ij-kilkclass));
            if (f_test_kor_vidnoshenya>kvanta_fisher)
            {
                richTextBox2.Text += $"f = {Math.Round(f_test_kor_vidnoshenya,4)} f>{Math.Round(kvanta_fisher,4)} коефіцієнт кореляційного відношення не є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"f = {Math.Round(f_test_kor_vidnoshenya,4)} f<={Math.Round(kvanta_fisher, 4)} коефіцієнт кореляційного є значущим\n";
            }

            if (Math.Abs(t_test_rang_koef_spirmena) <= kvantil_studen)
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_rang_koef_spirmena), 4)} |t|>{Math.Round(kvantil_studen, 4)} ранговий коефіцієнт кореляції Спірмена не є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_rang_koef_spirmena),4)} |t|<{Math.Round(kvantil_studen,4)} ранговий коефіцієнт кореляції Спірмена  є значущим\n";
            }

            dataGridView9.Rows.Add("Коефіцієнт Спірмена", Math.Round(kor_Spirmena-(kvantil_studen*znachustits_spirmena),4), Math.Round(kor_Spirmena, 4), Math.Round(kor_Spirmena + (kvantil_studen * znachustits_spirmena), 4));
            if (Math.Abs(u_test_rang_koef_kendalla) <= kvantil_start_rozpodil)
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(u_test_rang_koef_kendalla),4)} |u| <={Math.Round(kvantil_start_rozpodil,4)} ранговий коефіцієнт Кендалла  не є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(u_test_rang_koef_kendalla), 4)} |u|>{Math.Round(kvantil_start_rozpodil, 4)} ранговий коефіцієнт Кендалла  є значущим\n";
            }

            double kendal = (2.0 * s_rangu_kendala) / (Convert.ToDouble(coor_2d.Count * (coor_2d.Count - 1)));
            dataGridView9.Rows.Add("Коефіцієнт Кендалла", Math.Round(kendal-(kvantil_start_rozpodil*znachustits_kendala),4), Math.Round(kendal, 4), Math.Round(kendal + (kvantil_start_rozpodil * znachustits_kendala), 4));
            
            dataGridView9.Rows.Add("Міра зв’язку Кендалла", Math.Round(mira_zvazku_Kendalla - (kvantil_start_rozpodil * zn_stuard), 4), Math.Round(mira_zvazku_Kendalla, 4), Math.Round(mira_zvazku_Kendalla + (kvantil_start_rozpodil * zn_stuard), 4));
            dataGridView9.Rows.Add("Статистика Стюарда", Math.Round(statistika_stuarda - (kvantil_start_rozpodil * zn_stuard), 4), Math.Round(statistika_stuarda, 4), Math.Round(statistika_stuarda + (kvantil_start_rozpodil * zn_stuard), 4));
            kvantil_pirsona= kvantil_Pirsona(kvantil_start_rozpodil, 1.0);
            if (phi_koef_spoluchen_fi>= kvantil_pirsona)
            {
                richTextBox2.Text += $"χ={Math.Round(phi_koef_spoluchen_fi,4)} χ>={Math.Round(kvantil_pirsona,4)} коефіцієнт сполучень Ф є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"χ={Math.Round(phi_koef_spoluchen_fi, 4)} <{Math.Round(kvantil_pirsona, 4)} коефіцієнт сполучень Ф не є значущим\n";
            }


            kvantil_pirsona=kvantil_Pirsona(kvantil_start_rozpodil,Convert.ToDouble((kilkclass-1) * (kilkclass-1)));
            if (phi_TABLE_kvadra_pirsona>kvantil_pirsona)
            {
                richTextBox2.Text += $"χ={Math.Round(phi_TABLE_kvadra_pirsona, 4)} χ>{Math.Round(kvantil_pirsona, 4)} наявний зв`язок між ознаками X та Y \n";
            }
            else
            {
                richTextBox2.Text += $"χ={Math.Round(phi_TABLE_kvadra_pirsona, 4)} χ<={Math.Round(kvantil_pirsona, 4)} відсутній зв`язок між ознаками X та Y \n";
            }
            kvantil_pirsona = kvantil_Pirsona(kvantil_start_rozpodil, Convert.ToDouble((kilkclass - 1)));
            if (kof_spoluchen_Pirsona > kvantil_pirsona)
            {
                richTextBox2.Text += $"C={Math.Round(kof_spoluchen_Pirsona, 4)} C>{Math.Round(kvantil_pirsona, 4)} Коефіцієнт сполучень Пірсона є значущим \n";
            }
            else
            {
                richTextBox2.Text += $"C={Math.Round(kof_spoluchen_Pirsona, 4)} C<={Math.Round(kvantil_pirsona, 4)} Коефіцієнт сполучень Пірсона не є значущим \n";
            }
            if (Math.Abs(kvantil_yulla_q) <= kvantil_start_rozpodil)
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(kvantil_yulla_q),4)} |u|<={Math.Round(kvantil_start_rozpodil,4)} Коефіцієнти зв’язку Юла Q не є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(kvantil_yulla_q), 4)} |u|>{Math.Round(kvantil_start_rozpodil, 4)} Коефіцієнти зв’язку Юла Q є значущим\n";
            }

            if (Math.Abs(kvantil_yulla_y) <= kvantil_start_rozpodil)
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(kvantil_yulla_y),4)} |u|<={Math.Round(kvantil_start_rozpodil,4)} Коефіцієнти зв’язку Юла Y не є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(kvantil_yulla_y), 4)} |u|>{Math.Round(kvantil_start_rozpodil, 4)} Коефіцієнти зв’язку Юла Y є значущим\n";
            }

            double u_test_mira_zvazku_kendala = ((3.0 * mira_zvazku_Kendalla) / Math.Sqrt(2.0 * (2.0 * N_ij + 5.0))) * Math.Sqrt(N_ij * (N_ij - 1.0)); ;
            if (Math.Abs(u_test_mira_zvazku_kendala) <= kvantil_start_rozpodil)
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(u_test_mira_zvazku_kendala), 4)} |u| <={Math.Round(kvantil_start_rozpodil, 4)} Міра зв’язку Кендалла не є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(u_test_mira_zvazku_kendala), 4)} |u|>{Math.Round(kvantil_start_rozpodil, 4)} Міра зв’язку Кендалла  є значущим\n";
            }


            double t_test_studenad =(-statistika_stuarda)/zn_stuard ;
            kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_ij - 1.0);
            if (Math.Abs(t_test_studenad) <= kvantil_studen)
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_studenad), 4)} |t|<={Math.Round(kvantil_studen, 4)} статистика Стюарда не є значуща\n";
            }
            else
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_studenad), 4)} |t|>{Math.Round(kvantil_studen, 4)}  статистика Стюарда є значуща\n";
            }
        }

        double func_2d_norm(double sigma_x,double sigma_y,double x_seredne, double y_seredne, double x_x, double y_y)
        {

            double result = 0;
            double dod1 = Math.Pow((x_x-x_seredne)/sigma_x,2.0);
            double dod2 = Math.Pow((y_y-y_seredne)/sigma_y,2.0);
            double exp = Math.Exp((dod1 + dod2) / (-2.0));
            result = exp / (2.0 * Math.PI * sigma_x * sigma_y);
            return result;

        }

        private void button8_Click(object sender, EventArgs e)//визначаємо чи вважається вибірка нормальною
        {
            int index = comboBox2.SelectedIndex;
            for (int i = 0; i < norm_or_notnorm.Count; i++)
            {
                if (norm_or_notnorm[i].nomer_vibirk-1==index)
                {
                    norm_or_notnorm[i].norm = true;
                }
            }
            
        }

        private void button9_Click(object sender, EventArgs e)
        {

            
            vibirku_var.Clear();
            not_sortData.Clear();
            

            
            int c1_of_cycle = count_of_columns;
            for (int c = c1_of_cycle; c < 1 + c1_of_cycle; c++)
            {
                ds.Tables[0].Columns.Add($"{c + 1}");
                count_of_columns++;
            }
            


            int rows_for_loop = 0;

            if (c1_of_cycle == 0)
            {
                

                if (LogFunc.Checked==true)
                {
                    for (int i = 0; i < dataBANK.not_sort_log_list.Count; i++)
                    {
                        ds.Tables[0].Rows.Add(dataBANK.not_sort_log_list[i]);
                        rows_for_loop++;
                    }
                }
                else if (StandFunc.Checked==true)
                {
                    for (int i = 0; i < dataBANK.not_sort_standart_list.Count; i++)
                    {
                        ds.Tables[0].Rows.Add(dataBANK.not_sort_standart_list[i]);
                        rows_for_loop++;
                    }
                }

                for (int i = 0; i < 25000 - rows_for_loop; i++)
                {
                    ds.Tables[0].Rows.Add();
                }
            }
            else
            {
                int counter_of = 0;
                
                if (LogFunc.Checked == true)
                {
                    for (int i = 0; i < dataBANK.not_sort_log_list.Count; i++)
                    {
                        
                        int intovi_strochi = 0;
                        for (int t = c1_of_cycle; t < dataGridView5.Columns.Count; t++)
                        {

                            dataGridView5.Rows[counter_of].Cells[t].Value = dataBANK.not_sort_log_list[i];
                            intovi_strochi++;
                        }
                        counter_of++;
                    }
                }
                else if (StandFunc.Checked == true)
                {
                    for (int i = 0; i < dataBANK.not_sort_standart_list.Count; i++)
                    {

                        int intovi_strochi = 0;
                        for (int t = c1_of_cycle; t < dataGridView5.Columns.Count; t++)
                        {

                            dataGridView5.Rows[counter_of].Cells[t].Value = dataBANK.not_sort_standart_list[i];
                            intovi_strochi++;
                        }
                        counter_of++;
                    }
                }
            }

            dataGridView5.AllowUserToAddRows = false;
            comboBox2.Items.Clear();
            dataGridView5.DataSource = ds.Tables[0];
            double nBig = dataGridView5.Rows.Count;
            double chast;
            //////списки
            ///
            not_sortData.Clear();
            vibirku_var.Clear();
            comboBox2.Items.Clear();
            //norm_or_notnorm.Clear();
            for (int t = 0; t < dataGridView5.Columns.Count; t++)
            {
                normclass g12 = new normclass();
                g12.norm = false;
                g12.nomer_vibirk = t + 1;
                norm_or_notnorm.Add(g12);
                List<double> numberrs = new List<double>();
                List<rows> variant = new List<rows>();
                comboBox2.Items.Add($"{t + 1}");
                variant.Clear();
                nBig = 0;
                for (int i = 0; i < dataGridView5.Rows.Count; i++)
                {
                    if (Double.TryParse(dataGridView5.Rows[i].Cells[t].Value.ToString(), out double restik))
                    {
                        nBig++;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = 0; i < dataGridView5.Rows.Count; i++)
                {
                    rows rows1 = new rows();
                    double comp = 0;
                    if (Double.TryParse(dataGridView5.Rows[i].Cells[t].Value.ToString(), out double restik))
                    {
                        comp = restik;
                    }
                    else
                    {
                        continue;
                    }
                    //Double.TryParse(dataGridView5.Rows[i].Cells[t].Value.ToString());
                    numberrs.Add(comp);
                    chast = 1.0 / nBig;
                    rows1.chast = chast;
                    int KKK = (int)variant.Count;
                    rows1.variant = comp;
                    rows1.count = 1;
                    if (KKK == 0)
                    {
                        variant.Add(rows1);
                    }
                    else
                    {
                        for (int G = 0; G < KKK; G++)
                        {
                            if (variant[G].variant == comp)
                            {
                                variant[G].chast += chast;
                                variant[G].count += 1;
                            }
                            else if ((G == KKK - 1) && variant[G].variant != comp)
                            {
                                variant.Add(rows1);
                                break;
                            }
                        }
                    }

                }

                not_sortData.Add(numberrs);
                variant.Sort(delegate (rows x, rows y)
                {
                    return x.variant.CompareTo(y.variant);

                });

                vibirku_var.Add(variant);
            }
            ;
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
        private void button10_Click(object sender, EventArgs e)
        {
            ////////видаляємо аномальні значення////////////////


            liniyna_regresia.Checked = false;
            dovirchi_intevalu_every_regresii.Checked = false;
            tolerantrni_meshi.Checked = false;
            parabolichna_regresia.Checked = false;
            kvaziliniyna_regresia.Checked = false;
            dovirchi_intervaly_for_prognozu.Checked = false;
            method_Teyla.Checked = false;
            kvazi_bez_waga.Checked = false;
            dataGridView10.Rows.Clear();
            dataGridView10.Columns.Clear();
            richTextBox3.Text = "";
            string promptValue = Prompt.ShowDialog("Значення", "Вилучення аномальних значень");
            if (string.IsNullOrEmpty(promptValue))
            {
                return;
            }
            double alpha_anomal = Convert.ToDouble(promptValue);
            for (int i = 0; i < Table_Of_Chastots.Count; i++)
            {
                if (Table_Of_Chastots[i].chastota <= alpha_anomal)
                {
                    startfuncagain:
                    for (int g = 0; g < tochki_2Ds.Count; g++)
                    {
                        if ((Table_Of_Chastots[i].kvadrat_x_end >= tochki_2Ds[g].x && Table_Of_Chastots[i].kvadrat_x_zero <= tochki_2Ds[g].x) && (Table_Of_Chastots[i].kvadrat_y_end >= tochki_2Ds[g].y && Table_Of_Chastots[i].kvadrat_y_zero<= tochki_2Ds[g].y))
                        {
                            tochki_2Ds.RemoveAt(g);
                            goto startfuncagain;
                            
                        }
                    }
                }
                
            }
            List<tochki_2d> coor_2d = new List<tochki_2d>();
            for (int i = 0; i < tochki_2Ds.Count; i++)
            {
                coor_2d.Add(tochki_2Ds[i]);
            }





            /////////////////////////////////////
            string[] str = textBox4.Text.Split(' ');
            int[] el = new int[2];
            int[] el22 = new int[2];

            for (int i = 0; i < 2; i++)
            {
                el[i] = int.Parse(str[i]);
                el22[i] = el[i] - 1;
            } // вибираємо яку вибірку порівняти

            List<rang> data_x = new List<rang>();//місце де  зберігаємо дані котрі вибрали
            List<rang> data_y = new List<rang>();
            List<rang> first_vibirka = new List<rang>();
            List<rang> second_vibirka = new List<rang>();
       
            for (int i = 0; i < coor_2d.Count; i++)
            {
                rang cheb = new rang();
                cheb.which_elem = el22[0];
                cheb.element = coor_2d[i].x;
                first_vibirka.Add(cheb);
            }
            for (int i = 0; i < coor_2d.Count; i++)
            {
                rang cheb = new rang();
                cheb.which_elem = el22[1];
                cheb.element = coor_2d[i].y;
                second_vibirka.Add(cheb);
            }
            for (int i = 0; i < coor_2d.Count; i++)
            {
                rang cheb = new rang();
                cheb.which_elem = el22[0];
                cheb.element = coor_2d[i].x;
                cheb.index = i;
                data_x.Add(cheb);
            }
            for (int i = 0; i < coor_2d.Count; i++)
            {
                rang cheb = new rang();
                cheb.which_elem = el22[1];
                cheb.element = coor_2d[i].y;
                cheb.index = i;
                data_y.Add(cheb);
            }

            
           
            
            data_x.Sort(delegate (rang x, rang y) { return x.element.CompareTo(y.element); });
            data_y.Sort(delegate (rang x, rang y) { return x.element.CompareTo(y.element); });
            coor_2d.Sort(delegate (tochki_2d x, tochki_2d y) { return x.x.CompareTo(y.x); });
            List<List<rang>> our_rang = new List<List<rang>>();

            double elemnt_oflist = 0;

            List<rang> firstrang_x = new List<rang>();
            List<rang> firstrang_y = new List<rang>();

            for (int t1 = 0; t1 < data_x.Count; t1++)
            {
                elemnt_oflist = data_x[t1].element;
                rang squatr = new rang();
                squatr.element = elemnt_oflist;
                squatr.rangg = 1 + t1;
                squatr.which_elem = data_x[t1].which_elem;
                squatr.index = data_x[t1].index;

                int KKK = (int)firstrang_x.Count;

                if (KKK == 0)
                {
                    firstrang_x.Add(squatr);
                }
                else
                {
                    double enumer = 1;
                    double qt = 1 + t1;
                    for (int G = 0; G < KKK; G++)
                    {
                        if (firstrang_x[G].element == elemnt_oflist)
                        {
                            enumer++;
                            qt += (G + 1);
                        }
                    }
                    for (int G = 0; G < KKK; G++)
                    {
                        if (firstrang_x[G].element == elemnt_oflist)
                        {
                            firstrang_x[G].rangg = qt / enumer;
                        }
                    }
                    squatr.rangg = qt / enumer;
                    firstrang_x.Add(squatr);
                }
            }//пошук рангів
            for (int t1 = 0; t1 < data_y.Count; t1++)
            {
                elemnt_oflist = data_y[t1].element;
                rang squatr = new rang();
                squatr.element = elemnt_oflist;
                squatr.rangg = 1 + t1;
                squatr.which_elem = data_y[t1].which_elem;
                squatr.index = data_y[t1].index;
                int KKK = (int)firstrang_y.Count;

                if (KKK == 0)
                {
                    firstrang_y.Add(squatr);
                }
                else
                {
                    double enumer = 1;
                    double qt = 1 + t1;
                    for (int G = 0; G < KKK; G++)
                    {
                        if (firstrang_y[G].element == elemnt_oflist)
                        {
                            enumer++;
                            qt += (G + 1);
                        }
                    }
                    for (int G = 0; G < KKK; G++)
                    {
                        if (firstrang_y[G].element == elemnt_oflist)
                        {
                            firstrang_y[G].rangg = qt / enumer;
                        }
                    }
                    squatr.rangg = qt / enumer;
                    firstrang_y.Add(squatr);
                }
            }
            //лист де зберігається віраційний ряд з рангами двох вибірок
            List<double> first_vibirka_var_ryad = new List<double>();//варіаційний ряд першої вибірки
            List<double> second_vibirka_var_ryad = new List<double>();//варіаційний ряд другої вибірки
            for (int i = 0; i < coor_2d.Count; i++)
            {
                first_vibirka_var_ryad.Add(coor_2d[i].x);
                second_vibirka_var_ryad.Add(coor_2d[i].y);
            }
            first_vibirka_var_ryad.Sort();
            second_vibirka_var_ryad.Sort();

            

            //////////////////////////////////////////////////////////////
            dataGridView7.Columns.Clear();
            dataGridView7.Rows.Clear();
            dataGridView7.AllowUserToAddRows = false;
            dataGridView7.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;

            if (first_vibirka_var_ryad.Count <= 100)
            {
                kilkclass = (int)Math.Sqrt(first_vibirka_var_ryad.Count);
            }
            else
            {
                kilkclass = (int)Math.Pow(first_vibirka_var_ryad.Count, 0.3333333);
            }

            if (kilkclass > 1)
            {
                for (int i = 0; i < kilkclass + 1; i++)
                {
                    dataGridView7.Columns.Add("32" + i, "f" + i);
                }
                for (int i = 0; i < kilkclass + 1; i++)
                {
                    dataGridView7.Rows.Add();
                }
            }
            //MessageBox.Show($"rows{dataGridView7.Rows.Count} colo{dataGridView7.Columns.Count}  kilkas{kilkclass}");
            // побудова 2д гістограми
            double sered = first_vibirka_var_ryad[first_vibirka_var_ryad.Count - 1] - first_vibirka_var_ryad[0];
            double sered_second = second_vibirka_var_ryad[second_vibirka_var_ryad.Count - 1] - second_vibirka_var_ryad[0];
            double shah = sered / Convert.ToDouble(kilkclass);
            double shah2 = sered_second / Convert.ToDouble(kilkclass);
            double shaht = shah;
            double shaht2 = shah2;
            shah += first_vibirka_var_ryad[0];
            shah2 += second_vibirka_var_ryad[0];
            double sum = first_vibirka_var_ryad[0];
            double sum2 = second_vibirka_var_ryad[0];
            int column_2d_hist = 1;
            int chastoty = 0;
            List<table_of_chastot> table_chastot = new List<table_of_chastot>();
            for (int i = 0; i < coor_2d.Count; i++)
            {
                int rows_2d_hist = kilkclass - 1;
                for (int o = 0; o < coor_2d.Count; o++)
                {
                    for (int p = 0; p < coor_2d.Count; p++)
                    {
                        if ((coor_2d[p].x <= shah && coor_2d[p].x >= sum) && (coor_2d[p].y <= shah2 && coor_2d[p].y >= sum2))
                        {
                            chastoty += 1;
                        }
                    }
                    dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Value = Math.Round(Convert.ToDouble(chastoty) / Convert.ToDouble(coor_2d.Count), 4);
                    double sumtwosum = Convert.ToDouble(chastoty) / Convert.ToDouble(coor_2d.Count);
                    if (sumtwosum >= 0.1)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(0, 102, 0);
                    }
                    else if (sumtwosum < 0.1 && sumtwosum >= 0.04)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(0, 153, 0);
                    }
                    else if (sumtwosum < 0.04 && sumtwosum >= 0.03)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(0, 204, 0);
                    }
                    else if (sumtwosum < 0.03 && sumtwosum >= 0.02)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(0, 255, 0);
                    }
                    else if (sumtwosum < 0.02 && sumtwosum >= 0.01)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(51, 255, 51);
                    }
                    else if (sumtwosum < 0.01 && sumtwosum >= 0.007)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(102, 255, 102);
                    }
                    else if (sumtwosum < 0.007 && sumtwosum >= 0.003)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(153, 255, 153);
                    }
                    else if (sumtwosum < 0.003 && sumtwosum >= 0)
                    {
                        dataGridView7.Rows[rows_2d_hist].Cells[column_2d_hist].Style.BackColor = Color.FromArgb(204, 255, 204);
                    }
                    dataGridView7.Rows[rows_2d_hist].Cells[0].Value = Math.Round(shah2, 4);
                    table_of_chastot tbl = new table_of_chastot();
                    tbl.kil_chastot = Convert.ToDouble(chastoty);
                    tbl.chastota = sumtwosum;
                    tbl.variant_po_x = (sum+shah) / 2.0;
                    tbl.variant_po_y = (sum2+shah2) / 2.0;
                    tbl.index_x = i;
                    tbl.index_y = o;
                    tbl.kvadrat_x_end = shah;
                    tbl.kvadrat_y_end = shah2;
                    tbl.kvadrat_y_zero = sum2;
                    tbl.kvadrat_x_zero = sum;
                    table_chastot.Add(tbl);
                    rows_2d_hist--;
                    chastoty = 0;
                    sum2 += shaht2;
                    shah2 += shaht2;
                    if (rows_2d_hist < 0)
                    {
                        break;
                    }
                }
                dataGridView7.Rows[kilkclass].Cells[column_2d_hist].Value = Math.Round(shah, 4);
                sum += shaht;
                shah += shaht;
                shah2 = sered_second / Convert.ToDouble(kilkclass);
                shah2 += second_vibirka_var_ryad[0];
                sum2 = second_vibirka_var_ryad[0];
                column_2d_hist++;

                if (column_2d_hist > kilkclass)
                {
                    break;
                }


            }//2d hostograma
            //double suma_chasto1t = 0;
            //for (int i = 0; i < dataGridView7.ColumnCount - 1; i++)
            //{
            //    for (int k = 1; k < dataGridView7.RowCount; k++)
            //    {
            //        if (double.TryParse(dataGridView7.Rows[i].Cells[k].Value.ToString(), out double res))
            //        {
            //            suma_chasto1t += res;
            //        }
            //    }
            //    MessageBox.Show($"{suma_chasto1t}");
            //}
            Table_Of_Chastots.Clear();
            for (int i = 0; i < table_chastot.Count; i++)
            {
                Table_Of_Chastots.Add(table_chastot[i]);
            }
            double y_seredne = 0;
            double x_seredne = 0;
            double x_dispersia = 0;
            double y_dispersia = 0;
            double xy_seredne = 0;
            for (int i = 0; i < coor_2d.Count; i++)
            {
                y_seredne += coor_2d[i].y;
                x_seredne += coor_2d[i].x;
                xy_seredne += (coor_2d[i].x * coor_2d[i].y);
            }
            y_seredne /= Convert.ToDouble(coor_2d.Count);
            x_seredne /= Convert.ToDouble(coor_2d.Count);
            xy_seredne /= Convert.ToDouble(coor_2d.Count);
            for (int i = 0; i < coor_2d.Count; i++)
            {
                x_dispersia += Math.Pow(coor_2d[i].x - x_seredne, 2.0);
                y_dispersia += Math.Pow(coor_2d[i].y - y_seredne, 2.0);
            }
            x_dispersia /= Convert.ToDouble(coor_2d.Count - 1);
            y_dispersia /= Convert.ToDouble(coor_2d.Count - 1);

            double phi_kvadrat_pirsona = 0;
            for (int i = 0; i < kilkclass; i++)
            {
                double fun2d = 0;
                double p_zirochka = 0;
                for (int j = 0; j < kilkclass; j++)
                {
                    for (int t = 0; t < table_chastot.Count; t++)
                    {
                        if (table_chastot[t].index_x == i && table_chastot[t].index_y == j)
                        {
                            fun2d = func_2d_norm(Math.Sqrt(x_dispersia), Math.Sqrt(y_dispersia), x_seredne, y_seredne, table_chastot[t].variant_po_x, table_chastot[t].variant_po_y);
                            p_zirochka = fun2d * shaht * shaht2;
                            phi_kvadrat_pirsona += (Math.Pow(table_chastot[t].chastota - p_zirochka, 2) / (p_zirochka));


                        }
                    }

                }
            }///оцінки адекватності відтворення

            double koef_par_kor = (xy_seredne - (x_seredne * y_seredne)) / (Math.Sqrt(x_dispersia * y_dispersia));///оцінка парного коефіцієнта кореляції
            koef_par_kor = koef_par_kor * (Convert.ToDouble(coor_2d.Count) / Convert.ToDouble(coor_2d.Count - 1));


            chart3.Series[0].Points.Clear();
            for (int i = 0; i < first_vibirka.Count; i++)
            {
                chart3.Series[0].Points.AddXY(first_vibirka[i].element, second_vibirka[i].element);
            }///кореляційні поля
             ///кореляційні відношення///
             ///кореляційні відношення///
            double koef_kor_vidnoshenya = 0;///коефіцієнт кореляційного відношення


            sered = first_vibirka_var_ryad[first_vibirka_var_ryad.Count - 1] - first_vibirka_var_ryad[0];
            sered_second = second_vibirka_var_ryad[second_vibirka_var_ryad.Count - 1] - second_vibirka_var_ryad[0];
            shah = sered / Convert.ToDouble(kilkclass);
            shah2 = sered_second / Convert.ToDouble(kilkclass);
            shaht = shah;
            shaht2 = shah2;
            shah += first_vibirka_var_ryad[0];
            shah2 += second_vibirka_var_ryad[0];
            sum = first_vibirka_var_ryad[0];
            sum2 = second_vibirka_var_ryad[0];
            column_2d_hist = 1;
            chastoty = 0;
            // List<table_of_chastot> table_chastot = new List<table_of_chastot>();
            double y_diper_kor_vidnohenya = 0;
            for (int i = 0; i < kilkclass; i++)
            {

                chastoty = 0;
                double y_v_vibirci = 0;
                for (int p = 0; p < coor_2d.Count; p++)
                {
                    if (coor_2d[p].x <= shah && coor_2d[p].x >= sum)
                    {
                        chastoty += 1;
                        y_v_vibirci += coor_2d[p].y;
                        y_diper_kor_vidnohenya += Math.Pow(coor_2d[p].y - y_seredne, 2.0);
                    }
                }
                double y_ser = y_v_vibirci / Convert.ToDouble(chastoty);

                koef_kor_vidnoshenya += chastoty * Math.Pow(y_ser - y_seredne, 2.0);
                sum += shaht;
                shah += shaht;


            }
            //koef_kor_vidnoshenya = (koef_kor_vidnoshenya * Convert.ToDouble(coor_2d.Count - 1)) / y_dispersia;
            koef_kor_vidnoshenya = (koef_kor_vidnoshenya) / y_diper_kor_vidnohenya;
            koef_kor_vidnoshenya = Math.Sqrt(koef_kor_vidnoshenya);
            /////кінець кореляційного відношення///////


            for (int o = 0; o < coor_2d.Count; o++)
            {
                double x1111 = coor_2d[o].x;

                int indexacia = firstrang_x[o].index;
                for (int t = 0; t < firstrang_y.Count; t++)
                {
                    if (indexacia == firstrang_y[t].index)
                    {
                        coor_2d[o].rang_x = firstrang_x[o].rangg;
                        coor_2d[o].rang_y = firstrang_y[t].rangg;

                        break;
                    }
                }
            }///знайшли пари рангів
            tochki_2Ds.Clear();
            for (int i = 0; i < coor_2d.Count; i++)
            {
                tochki_2Ds.Add(coor_2d[i]);
            }
            double kor_Spirmena = 0;//оцінка рангового коефіцієнта кореляції Спірмена
            double d_l = 0;
            for (int i = 0; i < coor_2d.Count; i++)
            {
                d_l += Math.Pow(coor_2d[i].rang_x - coor_2d[i].rang_y, 2.0);
            }
            kor_Spirmena = 1.0 - ((6.0 * d_l) / (Convert.ToDouble(coor_2d.Count) * (Math.Pow(Convert.ToDouble(coor_2d.Count), 2.0) - 1.0)));
            //ранговий коефіцієнт кендала////
            double s_rangu_kendala = 0;
            for (int l = 0; l < coor_2d.Count - 1; l++)
            {
                for (int j = l + 1; j < coor_2d.Count; j++)
                {
                    if (coor_2d[l].rang_y < coor_2d[j].rang_y)
                    {
                        s_rangu_kendala += 1.0;

                    }
                    else
                    {
                        s_rangu_kendala += (-1.0);
                    }
                }
            }
            ///////////////таблиця сполучень/////////////////
            kilkclass = 2;
            sered = first_vibirka_var_ryad[first_vibirka_var_ryad.Count - 1] - first_vibirka_var_ryad[0];
            sered_second = second_vibirka_var_ryad[second_vibirka_var_ryad.Count - 1] - second_vibirka_var_ryad[0];
            shah = sered / Convert.ToDouble(kilkclass);
            shah2 = sered_second / Convert.ToDouble(kilkclass);
            shaht = shah;
            shaht2 = shah2;
            shah += first_vibirka_var_ryad[0];
            shah2 += second_vibirka_var_ryad[0];
            sum = first_vibirka_var_ryad[0];
            sum2 = second_vibirka_var_ryad[0];
            column_2d_hist = 1;
            chastoty = 0;
            //List<table_of_chastot> table_chastot = new List<table_of_chastot>();
            List<table_of_spoluchen> table_spoluchen = new List<table_of_spoluchen>();
            for (int i = 0; i < coor_2d.Count; i++)
            {
                int rows_2d_hist = kilkclass - 1;
                for (int o = 0; o < coor_2d.Count; o++)
                {
                    for (int p = 0; p < coor_2d.Count; p++)
                    {
                        if ((coor_2d[p].x <= shah && coor_2d[p].x >= sum) && (coor_2d[p].y <= shah2 && coor_2d[p].y >= sum2))
                        {
                            chastoty += 1;
                        }
                    }

                    double sumtwosum = Convert.ToDouble(chastoty);
                    table_of_spoluchen tbl_s = new table_of_spoluchen();
                    tbl_s.chastota = sumtwosum;
                    tbl_s.index_x = i;
                    tbl_s.index_y = o;
                    table_spoluchen.Add(tbl_s);

                    rows_2d_hist--;
                    chastoty = 0;
                    sum2 += shaht2;
                    shah2 += shaht2;
                    if (rows_2d_hist < 0)
                    {
                        break;
                    }
                }
                sum += shaht;
                shah += shaht;
                shah2 = sered_second / Convert.ToDouble(kilkclass);
                shah2 += second_vibirka_var_ryad[0];
                sum2 = second_vibirka_var_ryad[0];
                column_2d_hist++;

                if (column_2d_hist > kilkclass)
                {
                    break;
                }


            }
            double M_zero = table_spoluchen[0].chastota + table_spoluchen[1].chastota;
            double M_one = table_spoluchen[2].chastota + table_spoluchen[3].chastota;
            double N_zero = table_spoluchen[0].chastota + table_spoluchen[2].chastota;
            double N_one = table_spoluchen[1].chastota + table_spoluchen[3].chastota;
            double N_of_n0_n1 = N_zero + N_one;
            double index_Fechner = 0;//індекс Фехнера
            index_Fechner = (table_spoluchen[0].chastota + table_spoluchen[3].chastota - table_spoluchen[1].chastota - table_spoluchen[2].chastota) / (table_spoluchen[0].chastota + table_spoluchen[3].chastota + table_spoluchen[1].chastota + table_spoluchen[2].chastota);
            double koef_spoluchen_FI = (table_spoluchen[0].chastota * table_spoluchen[3].chastota - table_spoluchen[1].chastota * table_spoluchen[2].chastota) / Math.Sqrt(N_zero * N_one * M_zero * M_one);//Коефіцієнт сполучень Фі
            double koef_zvazku_Yulla_Y = (Math.Sqrt(table_spoluchen[0].chastota * table_spoluchen[3].chastota) - Math.Sqrt(table_spoluchen[1].chastota * table_spoluchen[2].chastota)) / (Math.Sqrt(table_spoluchen[0].chastota * table_spoluchen[3].chastota) + Math.Sqrt(table_spoluchen[1].chastota * table_spoluchen[2].chastota));//Коефіцієнти зв’язку Юла Y
            double koef_zvazku_Yulla_Q = (table_spoluchen[0].chastota * table_spoluchen[3].chastota - table_spoluchen[1].chastota * table_spoluchen[2].chastota) / (table_spoluchen[0].chastota * table_spoluchen[3].chastota + table_spoluchen[1].chastota * table_spoluchen[2].chastota);
            double s_Yulla_q = (1.0 - Math.Pow(koef_zvazku_Yulla_Q, 2.0)) * Math.Sqrt((1.0 / table_spoluchen[0].chastota) + (1.0 / table_spoluchen[1].chastota) + (1.0 / table_spoluchen[2].chastota) + (1.0 / table_spoluchen[3].chastota));
            s_Yulla_q /= 2.0;
            double s_Yulla_y = (1.0 - Math.Pow(koef_zvazku_Yulla_Y, 2.0)) * Math.Sqrt((1.0 / table_spoluchen[0].chastota) + (1.0 / table_spoluchen[1].chastota) + (1.0 / table_spoluchen[2].chastota) + (1.0 / table_spoluchen[3].chastota));
            s_Yulla_y /= 4.0;
            double kvantil_yulla_q = koef_zvazku_Yulla_Q / s_Yulla_q;
            double kvantil_yulla_y = koef_zvazku_Yulla_Y / s_Yulla_y;
            ///таблиця перехресного табулювання ////
            if (first_vibirka_var_ryad.Count <= 100)
            {
                kilkclass = (int)Math.Sqrt(first_vibirka_var_ryad.Count);
            }
            else
            {
                kilkclass = (int)Math.Pow(first_vibirka_var_ryad.Count, 0.3333333);
            }
            double phi_TABLE_kvadra_pirsona = 0;
            double N_ij = 0;
            for (int m = 0; m < table_chastot.Count; m++)
            {
                N_ij += table_chastot[m].kil_chastot;
            }
            for (int i = 0; i < kilkclass; i++)
            {
                for (int j = 0; j < kilkclass; j++)
                {
                    for (int t = 0; t < table_chastot.Count; t++)
                    {
                        if (table_chastot[t].index_x == j && table_chastot[t].index_y == i)
                        {
                            double n_i = 0;
                            double m_j = 0;
                            for (int m = 0; m < kilkclass; m++)
                            {
                                for (int w = 0; w < table_chastot.Count; w++)
                                {
                                    if (table_chastot[w].index_y == i && table_chastot[w].index_x == m)
                                    {
                                        n_i += table_chastot[w].kil_chastot;
                                    }
                                }
                            }//обраховуєм ni
                            for (int n = 0; n < kilkclass; n++)
                            {
                                for (int w = 0; w < table_chastot.Count; w++)
                                {
                                    if (table_chastot[w].index_y == n && table_chastot[w].index_x == j)
                                    {
                                        m_j += table_chastot[w].kil_chastot;
                                    }
                                }
                            }//обраховуєм mj
                            double n_ijji = 0;
                            n_ijji = (n_i * m_j) / N_ij;
                            phi_TABLE_kvadra_pirsona += Math.Pow(table_chastot[t].kil_chastot - n_ijji, 2.0) / n_ijji;

                            break;


                        }
                    }
                }
            }//знайшли критерій х^2


            double kof_spoluchen_Pirsona = Math.Sqrt(phi_TABLE_kvadra_pirsona / (N_ij + phi_TABLE_kvadra_pirsona));//Коефіцієнт сполучень Пірсона
            /////міра звязку кендалла///
            double P_kendela = 0;
            for (int i = 0; i < kilkclass; i++)
            {
                for (int j = 0; j < kilkclass; j++)
                {
                    for (int t = 0; t < table_chastot.Count; t++)
                    {
                        if (table_chastot[t].index_x == j && table_chastot[t].index_y == i)
                        {
                            double n_kll = 0;
                            for (int k = i + 1; k < kilkclass; k++)
                            {
                                for (int l = j + 1; l < kilkclass; l++)
                                {
                                    for (int t1 = 0; t1 < table_chastot.Count; t1++)
                                    {
                                        if (table_chastot[t1].index_x == l && table_chastot[t1].index_y == k)
                                        {
                                            n_kll += table_chastot[t1].kil_chastot;
                                        }
                                    }
                                }
                            }
                            P_kendela += table_chastot[t].kil_chastot * n_kll;
                        }
                    }
                }
            }

            double Q_kendela = 0;
            for (int i = 0; i < kilkclass; i++)
            {
                for (int j = 0; j < kilkclass; j++)
                {
                    for (int t = 0; t < table_chastot.Count; t++)
                    {
                        if (table_chastot[t].index_x == j && table_chastot[t].index_y == i)
                        {
                            double n_kll = 0;
                            for (int k = i + 1; k < kilkclass; k++)
                            {
                                for (int l = 0; l < j - 1; l++)
                                {
                                    for (int t1 = 0; t1 < table_chastot.Count; t1++)
                                    {
                                        if (table_chastot[t1].index_x == l && table_chastot[t1].index_y == k)
                                        {
                                            n_kll += table_chastot[t1].kil_chastot;
                                        }
                                    }
                                }
                            }
                            Q_kendela += table_chastot[t].kil_chastot * n_kll;
                        }
                    }
                }
            }
            double T1_kendela = 0;
            for (int i = 0; i < kilkclass; i++)
            {
                double n_i = 0;

                for (int m = 0; m < kilkclass; m++)
                {
                    for (int w = 0; w < table_chastot.Count; w++)
                    {
                        if (table_chastot[w].index_y == i && table_chastot[w].index_x == m)
                        {
                            n_i += table_chastot[w].kil_chastot;
                        }
                    }
                }//обраховуєм ni
                T1_kendela += n_i * (n_i - 1.0);
            }
            T1_kendela /= 2.0;

            double T2_kendela = 0;
            for (int j = 0; j < kilkclass; j++)
            {
                double m_j = 0;
                for (int n = 0; n < kilkclass; n++)
                {
                    for (int w = 0; w < table_chastot.Count; w++)
                    {
                        if (table_chastot[w].index_y == n && table_chastot[w].index_x == j)
                        {
                            m_j += table_chastot[w].kil_chastot;
                        }
                    }
                }//обраховуєм mj
                T2_kendela += m_j * (m_j - 1.0);
            }
            T2_kendela /= 2.0;

            double mira_zvazku_Kendalla = (P_kendela - Q_kendela) / Math.Sqrt(((N_ij * (N_ij - 1.0) / 2.0) - T1_kendela) * ((N_ij * (N_ij - 1.0) / 2.0) - T2_kendela));
            ////статистика Стюарда////

            List<table_of_spoluchen> table_spoluchen_stuard = new List<table_of_spoluchen>();
            double statistika_stuarda = 0;
            double zn_stuard = 0;
            if (!string.IsNullOrEmpty(textBox9.Text))
            {
                
                str = textBox9.Text.Split(' ');
                int[] mxn = new int[2];
                for (int i = 0; i < 2; i++)
                {
                    mxn[i] = int.Parse(str[i]);

                }
                //kilkclass = 2;
                sered = first_vibirka_var_ryad[first_vibirka_var_ryad.Count - 1] - first_vibirka_var_ryad[0];
                sered_second = second_vibirka_var_ryad[second_vibirka_var_ryad.Count - 1] - second_vibirka_var_ryad[0];
                shah = sered / Convert.ToDouble(mxn[0]);
                shah2 = sered_second / Convert.ToDouble(mxn[1]);
                shaht = shah;
                shaht2 = shah2;
                shah += first_vibirka_var_ryad[0];
                shah2 += second_vibirka_var_ryad[0];
                sum = first_vibirka_var_ryad[0];
                sum2 = second_vibirka_var_ryad[0];
                column_2d_hist = 1;
                chastoty = 0;
                for (int i = 0; i < mxn[0]; i++)
                {

                    for (int o = 0; o < mxn[1]; o++)
                    {
                        for (int p = 0; p < coor_2d.Count; p++)
                        {
                            if ((coor_2d[p].x <= shah && coor_2d[p].x >= sum) && (coor_2d[p].y <= shah2 && coor_2d[p].y >= sum2))
                            {
                                chastoty += 1;
                            }
                        }

                        double sumtwosum = Convert.ToDouble(chastoty);
                        table_of_spoluchen tbl_s = new table_of_spoluchen();
                        tbl_s.chastota = sumtwosum;
                        tbl_s.index_x = i;
                        tbl_s.index_y = o;
                        table_spoluchen_stuard.Add(tbl_s);


                        chastoty = 0;
                        sum2 += shaht2;
                        shah2 += shaht2;

                    }
                    sum += shaht;
                    shah += shaht;
                    shah2 = sered_second / Convert.ToDouble(mxn[1]);
                    shah2 += second_vibirka_var_ryad[0];
                    sum2 = second_vibirka_var_ryad[0];
                    column_2d_hist++;



                }///стоврили нову таблицю сполучень
                N_ij = 0;

                for (int m = 0; m < table_spoluchen_stuard.Count; m++)
                {
                    N_ij += table_spoluchen_stuard[m].chastota;
                }

                P_kendela = 0;
                for (int i = 0; i < mxn[1]; i++)
                {
                    for (int j = 0; j < mxn[0]; j++)
                    {
                        for (int t = 0; t < table_spoluchen_stuard.Count; t++)
                        {
                            if (table_spoluchen_stuard[t].index_x == j && table_spoluchen_stuard[t].index_y == i)
                            {
                                double n_kll = 0;
                                for (int k = i + 1; k < mxn[1]; k++)
                                {
                                    for (int l = j + 1; l < mxn[0]; l++)
                                    {
                                        for (int t1 = 0; t1 < table_spoluchen_stuard.Count; t1++)
                                        {
                                            if (table_spoluchen_stuard[t1].index_x == l && table_spoluchen_stuard[t1].index_y == k)
                                            {
                                                n_kll += table_spoluchen_stuard[t1].chastota;
                                            }
                                        }
                                    }
                                }
                                P_kendela += table_spoluchen_stuard[t].chastota * n_kll;
                            }
                        }
                    }
                }//порахували P

                Q_kendela = 0;
                for (int i = 0; i < mxn[1]; i++)
                {
                    for (int j = 0; j < mxn[0]; j++)
                    {
                        for (int t = 0; t < table_spoluchen_stuard.Count; t++)
                        {
                            if (table_spoluchen_stuard[t].index_x == j && table_spoluchen_stuard[t].index_y == i)
                            {
                                double n_kll = 0;
                                for (int k = i + 1; k < mxn[1]; k++)
                                {
                                    for (int l = 0; l < j - 1; l++)
                                    {
                                        for (int t1 = 0; t1 < table_spoluchen_stuard.Count; t1++)
                                        {
                                            if (table_spoluchen_stuard[t1].index_x == l && table_spoluchen_stuard[t1].index_y == k)
                                            {
                                                n_kll += table_spoluchen_stuard[t1].chastota;
                                            }
                                        }
                                    }
                                }
                                Q_kendela += table_spoluchen_stuard[t].chastota * n_kll;
                            }
                        }
                    }
                }///порахували Q
                double min_z_rozmiru_tabl = 0;
                if (mxn[0] < mxn[1])
                {
                    min_z_rozmiru_tabl = Convert.ToDouble(mxn[0]);
                }
                else
                {
                    min_z_rozmiru_tabl = Convert.ToDouble(mxn[1]);
                }

                statistika_stuarda = (2.0 * (P_kendela - Q_kendela) * min_z_rozmiru_tabl) / (Math.Pow(N_ij, 2.0) * (min_z_rozmiru_tabl - 1.0));
                ///значущість Стюарда
                double suma_pid_znakom_korena = 0;
                for (int i = 0; i < mxn[1]; i++)
                {
                    for (int j = 0; j < mxn[0]; j++)
                    {
                        for (int t = 0; t < table_spoluchen_stuard.Count; t++)
                        {
                            if (table_spoluchen_stuard[t].index_x == j && table_spoluchen_stuard[t].index_y == i)
                            {

                                double A_stuarda = 0;
                                for (int k = i + 1; k < mxn[1]; k++)
                                {
                                    for (int l = j + 1; l < mxn[0]; l++)
                                    {
                                        for (int t1 = 0; t1 < table_spoluchen_stuard.Count; t1++)
                                        {
                                            if (table_spoluchen_stuard[t1].index_x == l && table_spoluchen_stuard[t1].index_y == k)
                                            {
                                                A_stuarda += table_spoluchen_stuard[t1].chastota;
                                            }
                                        }
                                    }
                                }
                                for (int k = 0; k < i - 1; k++)
                                {
                                    for (int l = 0; l < j - 1; l++)
                                    {
                                        for (int t1 = 0; t1 < table_spoluchen_stuard.Count; t1++)
                                        {
                                            if (table_spoluchen_stuard[t1].index_x == l && table_spoluchen_stuard[t1].index_y == k)
                                            {
                                                A_stuarda += table_spoluchen_stuard[t1].chastota;
                                            }
                                        }
                                    }
                                }
                                double B_stuarda = 0;
                                for (int k = i + 1; k < mxn[1]; k++)
                                {
                                    for (int l = 0; l < j - 1; l++)
                                    {
                                        for (int t1 = 0; t1 < table_spoluchen_stuard.Count; t1++)
                                        {
                                            if (table_spoluchen_stuard[t1].index_x == l && table_spoluchen_stuard[t1].index_y == k)
                                            {
                                                B_stuarda += table_spoluchen_stuard[t1].chastota;
                                            }
                                        }
                                    }
                                }
                                for (int k = 0; k < i - 1; k++)
                                {
                                    for (int l = j + 1; l < mxn[0]; l++)
                                    {
                                        for (int t1 = 0; t1 < table_spoluchen_stuard.Count; t1++)
                                        {
                                            if (table_spoluchen_stuard[t1].index_x == l && table_spoluchen_stuard[t1].index_y == k)
                                            {
                                                B_stuarda += table_spoluchen_stuard[t1].chastota;
                                            }
                                        }
                                    }
                                }


                                suma_pid_znakom_korena += table_spoluchen_stuard[t].chastota * Math.Pow(A_stuarda - B_stuarda, 2.0);
                            }
                        }
                    }
                }
                double znachusist_stuarda = Math.Sqrt(Math.Pow(N_ij, 2.0) * suma_pid_znakom_korena - (4.0 * N_ij * (P_kendela - Q_kendela)));
                znachusist_stuarda = znachusist_stuarda * ((2.0 * min_z_rozmiru_tabl) / (Math.Pow(N_ij, 3.0) * (min_z_rozmiru_tabl - 1.0)));
                zn_stuard = znachusist_stuarda;
                


            }
            double kvantil_start_rozpodil = normKvantil(Double.Parse(textBox5.Text));

            dataGridView9.Rows.Clear();
            dataGridView9.Columns.Clear();
            dataGridView9.AllowUserToAddRows = false;
            dataGridView9.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView9.Columns.Add("1", "Характеристика");
            dataGridView9.Columns.Add("2", "INF");
            dataGridView9.Columns.Add("3", "Значення");
            dataGridView9.Columns.Add("4", "SUP");
            //dataGridView9.Columns.Add("5", "SKV");

            double znachustits_spirmena = Math.Sqrt((1.0 - Math.Pow(kor_Spirmena, 2.0)) / (N_ij - 2.0));
            double znachustits_kendala = Math.Sqrt(((4.0 * N_ij) + 10.0) / (9.0 * (Math.Pow(N_ij, 2.0) - N_ij)));
            dataGridView9.Rows.Add("Середнє Х", "", Math.Round(x_seredne, 4), "");
            dataGridView9.Rows.Add("Середнє Y", "", Math.Round(y_seredne, 4), "");
            dataGridView9.Rows.Add("Дисперіся Х", "", Math.Round(x_dispersia, 4), "");
            dataGridView9.Rows.Add("Дисперсія Y", "", Math.Round(y_dispersia, 4), "");
            dataGridView9.Rows.Add("Оцінка адекватності відтворення ХY", "", Math.Round(phi_kvadrat_pirsona, 4), "");
            dataGridView9.Rows.Add("Коефіцієнт кореляції", Math.Round((koef_par_kor + ((koef_par_kor * (1.0 - Math.Pow(koef_par_kor, 2.0))) / (2.0 * Convert.ToDouble(coor_2d.Count))) - kvantil_start_rozpodil * ((1.0 - Math.Pow(koef_par_kor, 2.0)) / Math.Sqrt(Convert.ToDouble(coor_2d.Count) - 1.0))), 4), Math.Round(koef_par_kor, 4), Math.Round((koef_par_kor + ((koef_par_kor * (1.0 - Math.Pow(koef_par_kor, 2.0))) / (2.0 * Convert.ToDouble(coor_2d.Count))) + kvantil_start_rozpodil * ((1.0 - Math.Pow(koef_par_kor, 2.0)) / Math.Sqrt(Convert.ToDouble(coor_2d.Count) - 1.0))), 4));
            dataGridView9.Rows.Add("Кореляційне відношення", "", Math.Round(koef_kor_vidnoshenya, 4), "");
            dataGridView9.Rows.Add("Індекс Фехнера", "", Math.Round(index_Fechner, 4), "");
            dataGridView9.Rows.Add("Коефіцієнт сполучень «Фі»", "", Math.Round(koef_spoluchen_FI, 4), "");
            dataGridView9.Rows.Add("Коефіцієнти зв’язку Юла Q", "", Math.Round(koef_zvazku_Yulla_Q, 4), "");
            dataGridView9.Rows.Add("Коефіцієнти зв’язку Юла Y", "", Math.Round(koef_zvazku_Yulla_Y, 4), "");
            dataGridView9.Rows.Add("Коефіцієнт сполучень Пірсона", "", Math.Round(kof_spoluchen_Pirsona, 4), "");
            richTextBox2.Text = "";
            double t_test_koef_korelacii = (koef_par_kor * Math.Sqrt(N_ij - 2.0)) / (Math.Sqrt(1.0 - Math.Pow(koef_par_kor, 2.0)));
            double t_test_kor_vidnoshenya = (Math.Sqrt(koef_kor_vidnoshenya) * Math.Sqrt(N_ij - 2.0)) / (Math.Sqrt(1.0 - koef_kor_vidnoshenya));
            double f_test_kor_vidnoshenya = ((koef_kor_vidnoshenya) / (1.0 - koef_kor_vidnoshenya)) * ((Convert.ToDouble(N_ij - kilkclass)) / (Convert.ToDouble(kilkclass - 1)));
            double t_test_rang_koef_spirmena = (kor_Spirmena * Math.Sqrt(N_ij - 2.0)) / (Math.Sqrt(1.0 - Math.Pow(kor_Spirmena, 2.0)));
            double koef_rang_kendal = ((2.0 * s_rangu_kendala) / (N_ij * (N_ij - 1.0)));
            double u_test_rang_koef_kendalla = ((3.0 * koef_rang_kendal) / Math.Sqrt(2.0 * (2.0 * N_ij + 5.0))) * Math.Sqrt(N_ij * (N_ij - 1.0));
            double phi_koef_spoluchen_fi = N_ij * Math.Pow(koef_spoluchen_FI, 2.0);

            double kvantil_pirsona = kvantil_Pirsona(kvantil_start_rozpodil, Convert.ToDouble(kilkclass * kilkclass - 2.0));
            if (phi_kvadrat_pirsona >= kvantil_pirsona)
            {
                richTextBox2.Text += $"χ={Math.Round(phi_kvadrat_pirsona, 4)} χ>={Math.Round(kvantil_pirsona, 4)} двовимірний нормальний розподіл не є адекватним\n";
            }
            else
            {
                richTextBox2.Text += $"χ={Math.Round(phi_kvadrat_pirsona, 4)} χ<{Math.Round(kvantil_pirsona, 4)} двовимірний нормальний розподіл є адекватним\n";
            }

            double kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_ij - 2.0);
            if (Math.Abs(t_test_kor_vidnoshenya) <= kvantil_studen)
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_kor_vidnoshenya), 4)} |t|<={Math.Round(kvantil_studen, 4)} кореляційний зв’язок поміж η,ξ відсутній\n";
            }
            else
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_kor_vidnoshenya), 4)} |t|<={Math.Round(kvantil_studen, 4)}  кореляційний зв’язок поміж η,ξ присутній\n";
            }
            kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_ij - 1.0);
            if (Math.Abs(t_test_koef_korelacii) <= kvantil_studen)
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_koef_korelacii), 4)} |t|<={Math.Round(kvantil_studen, 4)} оцінкa коефіцієнта кореляції не є значуща\n";
            }
            else
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_koef_korelacii), 4)} |t|>{Math.Round(kvantil_studen, 4)}  оцінкa коефіцієнта кореляції є значуща\n";
            }
            kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_ij - 2.0);
            double kvanta_fisher = kvantil_Fishera(kvantil_start_rozpodil, Convert.ToDouble(kilkclass - 1), Convert.ToDouble(N_ij - kilkclass));
            if (f_test_kor_vidnoshenya > kvanta_fisher)
            {
                richTextBox2.Text += $"f = {Math.Round(f_test_kor_vidnoshenya, 4)} f>{Math.Round(kvanta_fisher, 4)} коефіцієнт кореляційного відношення не є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"f = {Math.Round(f_test_kor_vidnoshenya, 4)} f<={Math.Round(kvanta_fisher, 4)} коефіцієнт кореляційного є значущим\n";
            }

            if (Math.Abs(t_test_rang_koef_spirmena) <= kvantil_studen)
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_rang_koef_spirmena), 4)} |t|>{Math.Round(kvantil_studen, 4)} ранговий коефіцієнт кореляції Спірмена є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_rang_koef_spirmena), 4)} |t|<{Math.Round(kvantil_studen, 4)} ранговий коефіцієнт кореляції Спірмена не є значущим\n";
            }

            dataGridView9.Rows.Add("Коефіцієнт Спірмена", Math.Round(kor_Spirmena - (kvantil_studen * znachustits_spirmena), 4), Math.Round(kor_Spirmena, 4), Math.Round(kor_Spirmena + (kvantil_studen * znachustits_spirmena), 4));
            if (Math.Abs(u_test_rang_koef_kendalla) <= kvantil_start_rozpodil)
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(u_test_rang_koef_kendalla), 4)} |u| <={Math.Round(kvantil_start_rozpodil, 4)} ранговий коефіцієнт Кендалла  не є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(u_test_rang_koef_kendalla), 4)} |u|>{Math.Round(kvantil_start_rozpodil, 4)} ранговий коефіцієнт Кендалла  є значущим\n";
            }

            double kendal = (2.0 * s_rangu_kendala) / (Convert.ToDouble(coor_2d.Count * (coor_2d.Count - 1)));
            dataGridView9.Rows.Add("Коефіцієнт Кендалла", Math.Round(kendal - (kvantil_start_rozpodil * znachustits_kendala), 4), Math.Round(kendal, 4), Math.Round(kendal + (kvantil_start_rozpodil * znachustits_kendala), 4));

            dataGridView9.Rows.Add("Міра зв’язку Кендалла", Math.Round(mira_zvazku_Kendalla - (kvantil_start_rozpodil * zn_stuard), 4), Math.Round(mira_zvazku_Kendalla, 4), Math.Round(mira_zvazku_Kendalla + (kvantil_start_rozpodil * zn_stuard), 4));
            dataGridView9.Rows.Add("Статистика Стюарда", Math.Round(statistika_stuarda - (kvantil_start_rozpodil * zn_stuard), 4), Math.Round(statistika_stuarda, 4), Math.Round(statistika_stuarda + (kvantil_start_rozpodil * zn_stuard), 4));
            kvantil_pirsona = kvantil_Pirsona(kvantil_start_rozpodil, 1.0);
            if (phi_koef_spoluchen_fi >= kvantil_pirsona)
            {
                richTextBox2.Text += $"χ={Math.Round(phi_koef_spoluchen_fi, 4)} χ>={Math.Round(kvantil_pirsona, 4)} коефіцієнт сполучень Ф є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"χ={Math.Round(phi_koef_spoluchen_fi, 4)} <{Math.Round(kvantil_pirsona, 4)} коефіцієнт сполучень Ф не є значущим\n";
            }


            kvantil_pirsona = kvantil_Pirsona(kvantil_start_rozpodil, Convert.ToDouble((kilkclass - 1) * (kilkclass - 1)));
            if (phi_TABLE_kvadra_pirsona > kvantil_pirsona)
            {
                richTextBox2.Text += $"χ={Math.Round(phi_TABLE_kvadra_pirsona, 4)} χ>{Math.Round(kvantil_pirsona, 4)} наявний зв`язок між ознаками X та Y \n";
            }
            else
            {
                richTextBox2.Text += $"χ={Math.Round(phi_TABLE_kvadra_pirsona, 4)} χ<={Math.Round(kvantil_pirsona, 4)} відсутній зв`язок між ознаками X та Y \n";
            }
            kvantil_pirsona = kvantil_Pirsona(kvantil_start_rozpodil, Convert.ToDouble((kilkclass - 1)));
            if (kof_spoluchen_Pirsona > kvantil_pirsona)
            {
                richTextBox2.Text += $"C={Math.Round(kof_spoluchen_Pirsona, 4)} C>{Math.Round(kvantil_pirsona, 4)} Коефіцієнт сполучень Пірсона є значущим \n";
            }
            else
            {
                richTextBox2.Text += $"C={Math.Round(kof_spoluchen_Pirsona, 4)} C<={Math.Round(kvantil_pirsona, 4)} Коефіцієнт сполучень Пірсона не є значущим \n";
            }
            if (Math.Abs(kvantil_yulla_q) <= kvantil_start_rozpodil)
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(kvantil_yulla_q), 4)} |u|<={Math.Round(kvantil_start_rozpodil, 4)} Коефіцієнти зв’язку Юла Q не є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(kvantil_yulla_q), 4)} |u|>{Math.Round(kvantil_start_rozpodil, 4)} Коефіцієнти зв’язку Юла Q  є значущим\n";
            }

            if (Math.Abs(kvantil_yulla_y) <= kvantil_start_rozpodil)
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(kvantil_yulla_y), 4)} |u|<={Math.Round(kvantil_start_rozpodil, 4)} Коефіцієнти зв’язку Юла Y не є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(kvantil_yulla_y), 4)} |u|>{Math.Round(kvantil_start_rozpodil, 4)} Коефіцієнти зв’язку Юла Y  є значущим\n";
            }

            double u_test_mira_zvazku_kendala = ((3.0 * mira_zvazku_Kendalla) / Math.Sqrt(2.0 * (2.0 * N_ij + 5.0))) * Math.Sqrt(N_ij * (N_ij - 1.0)); ;
            if (Math.Abs(u_test_mira_zvazku_kendala) <= kvantil_start_rozpodil)
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(u_test_mira_zvazku_kendala), 4)} |u| <={Math.Round(kvantil_start_rozpodil, 4)} Міра зв’язку Кендалла не є значущим\n";
            }
            else
            {
                richTextBox2.Text += $"|u|={Math.Round(Math.Abs(u_test_mira_zvazku_kendala), 4)} |u|>{Math.Round(kvantil_start_rozpodil, 4)} Міра зв’язку Кендалла  є значущим\n";
            }


            double t_test_studenad = (-statistika_stuarda) / zn_stuard;
            kvantil_studen = kvantil_studenta(kvantil_start_rozpodil, N_ij - 1.0);
            if (Math.Abs(t_test_studenad) <= kvantil_studen)
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_studenad), 4)} |t|<={Math.Round(kvantil_studen, 4)} статистика Стюарда не є значуща\n";
            }
            else
            {
                richTextBox2.Text += $"|t|={Math.Round(Math.Abs(t_test_studenad), 4)} |t|>{Math.Round(kvantil_studen, 4)}  статистика Стюарда  є значуща\n";
            }


        }

        private void button11_Click(object sender, EventArgs e)
        {
            string[] vector_data = textBox14.Text.Split(' ');
            int[] nomer_oznak = new int[vector_data.Length];
            for (int i = 0; i < nomer_oznak.Length; i++)
            {
                nomer_oznak[i] = int.Parse(vector_data[i])-1;
            }
            List<double> vector_serednih = new List<double>();
            List<double> vector_sigma = new List<double>();
            List<List<double>> vector_oznak = new List<List<double>>();
            for (int i = 0; i < nomer_oznak.Length; i++)
            {
                vector_oznak.Add(not_sortData[nomer_oznak[i]]);
                vector_serednih.Add(Program.vect_seredne(not_sortData[nomer_oznak[i]]));
                vector_sigma.Add(Program.vect_seredno_kvadratichnyh(not_sortData[nomer_oznak[i]]));
            }




        }

        private void button12_Click(object sender, EventArgs e)
        {
            string[] first_omega = textBox15.Text.Split(' ');
            string[] second_omega = textBox16.Text.Split(' ');
            int[] nomer_oznak_1_omega = new int[first_omega.Length];
            int[] nomer_oznak_2_omega = new int[second_omega.Length];
            for (int i = 0; i < first_omega.Length; i++)
            {
                nomer_oznak_1_omega[i] = int.Parse(first_omega[i]) - 1;
            }
            for (int i = 0; i < second_omega.Length; i++)
            {
                nomer_oznak_2_omega[i] = int.Parse(second_omega[i]) - 1;
            }
            int N1 = 0;
            int N2 = 0;

            List<double> vector_serednih_1_omega = new List<double>();
            List<double> vector_serednih_2_omega = new List<double>();
            List<double> vector_sigma_1_omega = new List<double>();
            List<double> vector_sigma_2_omega = new List<double>();
            List<List<double>> vector_oznak_1_omega = new List<List<double>>();
            List<List<double>> vector_oznak_2_omega = new List<List<double>>();
            for (int i = 0; i < first_omega.Length; i++)
            {
                N1 = not_sortData[nomer_oznak_1_omega[i]].Count;
                vector_oznak_1_omega.Add(not_sortData[nomer_oznak_1_omega[i]]);
                vector_serednih_1_omega.Add(Program.vect_seredne(not_sortData[nomer_oznak_1_omega[i]]));
                vector_sigma_1_omega.Add(Program.vect_seredno_kvadratichnyh(not_sortData[nomer_oznak_1_omega[i]]));
            }
            for (int i = 0; i < second_omega.Length; i++)
            {
                N2 = not_sortData[nomer_oznak_2_omega[i]].Count;
                vector_oznak_2_omega.Add(not_sortData[nomer_oznak_2_omega[i]]);
                vector_serednih_2_omega.Add(Program.vect_seredne(not_sortData[nomer_oznak_2_omega[i]]));
                vector_sigma_2_omega.Add(Program.vect_seredno_kvadratichnyh(not_sortData[nomer_oznak_2_omega[i]]));
            }

            double[,] dc_1 = Program.DC_matrix(vector_oznak_1_omega);
            double[,] dc_2 = Program.DC_matrix(vector_oznak_2_omega);
            double[,] S_0 = new double[vector_oznak_1_omega.Count, vector_oznak_1_omega.Count];
            double[,] S_1 = new double[vector_oznak_2_omega.Count, vector_oznak_2_omega.Count];
            for (int i = 0; i < vector_oznak_1_omega.Count; i++)
            {
                for (int j = 0; j < vector_oznak_1_omega.Count; j++)
                {
                    double x_i_l_x_j_l = 0;
                    double y_i_l_y_j_l = 0;
                    double x_i_l = 0;
                    double x_j_l = 0;
                    double y_i_l = 0;
                    double y_j_l = 0;
                    for (int l = 0; l < N1; l++)
                    {
                        x_i_l_x_j_l += (vector_oznak_1_omega[i][l]* vector_oznak_1_omega[j][l]);
                        x_i_l += vector_oznak_1_omega[i][l];
                        x_j_l += vector_oznak_1_omega[j][l];
                    }
                    for (int l = 0; l < N2; l++)
                    {
                        y_i_l_y_j_l += (vector_oznak_2_omega[i][l] * vector_oznak_2_omega[j][l]);
                        y_i_l += vector_oznak_2_omega[i][l];
                        y_j_l += vector_oznak_2_omega[j][l];
                    }
                    double s_i_j = x_i_l_x_j_l + y_i_l_y_j_l - (((x_i_l + y_i_l) * (x_j_l + y_j_l)) / (Convert.ToDouble(N1 + N2)));
                    s_i_j = s_i_j / Convert.ToDouble(N1+N2-2);
                    S_0[i, j] = s_i_j;


                }
            } ///матриця S_0
            for (int i = 0; i < vector_oznak_2_omega.Count; i++)
            {
                for (int j = 0; j < vector_oznak_2_omega.Count; j++)
                {
                    double x_i_l_x_j_l = 0;
                    double y_i_l_y_j_l = 0;
                    double x_i_l = 0;
                    double x_j_l = 0;
                    double y_i_l = 0;
                    double y_j_l = 0;
                    for (int l = 0; l < N1; l++)
                    {
                        x_i_l_x_j_l += (vector_oznak_1_omega[i][l] * vector_oznak_1_omega[j][l]);
                        x_i_l += vector_oznak_1_omega[i][l];
                        x_j_l += vector_oznak_1_omega[j][l];
                    }
                    for (int l = 0; l < N2; l++)
                    {
                        y_i_l_y_j_l += (vector_oznak_2_omega[i][l] * vector_oznak_2_omega[j][l]);
                        y_i_l += vector_oznak_2_omega[i][l];
                        y_j_l += vector_oznak_2_omega[j][l];
                    }
                    double s_i_j = x_i_l_x_j_l + y_i_l_y_j_l - ((x_i_l*x_j_l)/Convert.ToDouble(N1)) - ((y_i_l * y_j_l) / Convert.ToDouble(N2));
                    s_i_j = s_i_j / Convert.ToDouble(N1 + N2 - 2);
                    S_1[i, j] = s_i_j;


                }
            } ///матриця S_1

            double S0_determ = Program.Determinate(S_0, vector_oznak_1_omega.Count);
            double S1_determ = Program.Determinate(S_1, vector_oznak_2_omega.Count);
            double Statistic_V = -1.0*Convert.ToDouble(N1+N2-2-(vector_oznak_1_omega.Count/2.0))*Math.Log(S0_determ/S1_determ);//статистика V





        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
           // form2.ShowDialog();
            if (form2.ShowDialog()==DialogResult.Cancel)
            {
                MessageBox.Show("forma is closed");
                comboBox4.Items.Clear();
                for (int i = 0; i < Universe.Data_Value.Count; i++)
                {
                    comboBox4.Items.Add($"Сукупність {i+1}") ;
                }
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            int nomer_oznaki = comboBox2.SelectedIndex;
            int index_sukupnosti = comboBox4.SelectedIndex;
            comboBox2.Items.Clear();
            
            if (LogFunc.Checked == true)
            {
                List<double> chisla = new List<double>();
                for (int i = 0; i < dataBANK.not_sort_log_list.Count; i++)
                {
                    chisla.Add(dataBANK.not_sort_log_list[i]);
                }
                Universe.Data_Value[index_sukupnosti].Add(chisla);
                Universe.Data_Value[index_sukupnosti].RemoveAt(nomer_oznaki);
            }
            else if (StandFunc.Checked == true)
            {
                List<double> chisla = new List<double>();
                for (int i = 0; i < dataBANK.not_sort_standart_list.Count; i++)
                {
                    chisla.Add(dataBANK.not_sort_standart_list[i]);
                }
                Universe.Data_Value[index_sukupnosti].Add(chisla);
                Universe.Data_Value[index_sukupnosti].RemoveAt(nomer_oznaki);
            }  
            for (int i = 0; i < Universe.Data_Value[comboBox4.SelectedIndex].Count; i++)
            {
                comboBox2.Items.Add($"Ознака {i + 1}");
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            for (int i = 0; i < Universe.Data_Value[comboBox4.SelectedIndex].Count; i++)
            {
                comboBox2.Items.Add($"Ознака {i + 1}");
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        public int nomer_vectora=1;
        private void button13_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            // form2.ShowDialog();
            if (form2.ShowDialog() == DialogResult.Cancel)
            {
              
                comboBox4.Items.Clear();
                
                for (int i = 0; i < Universe.Data_Value.Count; i++)
                {
                    comboBox4.Items.Add($"Сукупність {i + 1}");
                }
                listBox2.Items.Clear();
                for (int i = 0; i < Universe.Data_Value.Count; i++)
                {
                    listBox2.Items.Add($"Сукупність {i + 1}");
                }
                
            
                for (int t = 0; t < Universe.Data_Value[Universe.Data_Value.Count-1].Count; t++)
                {
                    if (Program.unique_list(Universe.Data_Value[Universe.Data_Value.Count-1][t], Universe.Data_Vectors))
                    {
                        listBox1.Items.Add($"Вектор {nomer_vectora}");
                        nomer_vectora++;
                        Universe.Data_Vectors.Add(Universe.Data_Value[Universe.Data_Value.Count-1][t]);
                    }
                }
            
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            List<List<double>>the_new_sukupnist = new List<List<double>>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    the_new_sukupnist.Add(Universe.Data_Vectors[i]);
                }
            }
            Universe.Data_Value.Add(the_new_sukupnist);
            listBox2.Items.Clear();
            comboBox4.Items.Clear();
            for (int i = 0; i < Universe.Data_Value.Count; i++)
            {
                comboBox4.Items.Add($"Сукупність {i + 1}");
                listBox2.Items.Add($"Сукупність {i + 1}");
            }
        }
      
        private void button11_Click_1(object sender, EventArgs e)
        {
            
            List<int> nomer_oznak = new List<int>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            }
            List<List<double>> vector_oznak12 = new List<List<double>>();
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                vector_oznak12.Add(Universe.Data_Vectors[nomer_oznak[i]]);
            }
            multy_dimension(vector_oznak12);
            
    

        }

        void multy_dimension(List<List<double>> ozanky)
        {
            List<List<double>> vector_oznak = new List<List<double>>();
            for (int i = 0; i < ozanky.Count; i++)
            {
                vector_oznak.Add(ozanky[i]);
            }
           

            List<double> vector_serednih = new List<double>();
            List<double> vector_sigma = new List<double>();
            for (int i = 0; i < vector_oznak.Count; i++)
            {
                vector_serednih.Add(Program.vect_seredne(vector_oznak[i]));
                vector_sigma.Add(Program.vect_seredno_kvadratichnyh(vector_oznak[i]));
            }
            dataGridView11.Rows.Clear();
            dataGridView11.Columns.Clear();
            dataGridView11.AllowUserToAddRows = false;
            dataGridView11.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView11.Columns.Add($"вектор", $"Оцінки");
            for (int i = 0; i < vector_oznak.Count; i++)
            {
                dataGridView11.Columns.Add($"{i}x", $"{i}");
            }
            dataGridView11.Rows.Add();
            dataGridView11.Rows[0].Cells[0].Value = "Вектор середніх";
            for (int i = 1; i < dataGridView11.Columns.Count; i++)
            {
                dataGridView11.Rows[0].Cells[i].Value = $"{Math.Round(vector_serednih[i - 1], 4)}";
            }
            dataGridView11.Rows.Add();
            dataGridView11.Rows[dataGridView11.Rows.Count - 1].Cells[0].Value = "Вектор середньо-квадратичних";
            for (int i = 1; i < dataGridView11.Columns.Count; i++)
            {
                dataGridView11.Rows[dataGridView11.Rows.Count - 1].Cells[i].Value = $"{Math.Round(vector_sigma[i - 1], 4)}";
            }

            dataGridView11.Rows.Add();
            double[,] dc_1 = Program.DC_matrix(vector_oznak);
            dataGridView11.Rows[dataGridView11.Rows.Count - 1].Cells[0].Value = "DC-матриця";
            for (int i = 1; i < dataGridView11.Columns.Count; i++)
            {
                for (int t = 0; t < vector_oznak.Count; t++)
                {
                    dataGridView11.Rows[dataGridView11.Rows.Count - 1].Cells[t + 1].Value = $"{Math.Round(dc_1[i - 1, t], 4)}";
                }
                dataGridView11.Rows.Add();

            }
            dataGridView11.Rows.Add();
            double[,] R_matrix = Program.R_matrix(vector_oznak);
            dataGridView11.Rows[dataGridView11.Rows.Count - 1].Cells[0].Value = "R-матриця";
            for (int i = 1; i < dataGridView11.Columns.Count; i++)
            {
                for (int t = 0; t < vector_oznak.Count; t++)
                {
                    dataGridView11.Rows[dataGridView11.Rows.Count - 1].Cells[t + 1].Value = $"{Math.Round(R_matrix[i - 1, t], 4)}";
                }
                dataGridView11.Rows.Add();

            }

            /////побудова варіаційного ряду
            int kilkisty_classiv = 1;
            if (vector_oznak[0].Count <= 100)
            {
                kilkisty_classiv = (int)Math.Sqrt(vector_oznak[0].Count);
            }
            else
            {
                kilkisty_classiv = (int)Math.Pow(vector_oznak[0].Count, 0.3333333);
            }
            List<double> kroki_n_hist = Program.krok_var_ryadu(vector_oznak, kilkisty_classiv);
            //List<double> low_meshi = Program.Low_limit(vector_oznak);
            //List<double> high_meshi = Program.Hight_limit(vector_oznak, kilkisty_classiv);
            //List<double> init_low_meshi = Program.Low_limit(vector_oznak);
            //List<double> init_high_meshi = Program.Hight_limit(vector_oznak, kilkisty_classiv);

            var result = Program.varianta_each_vectors(vector_oznak, kilkisty_classiv).CartesianProduct();
            List<List<double>> varianty_each_cube = new List<List<double>>();
            foreach (var item in result)
            {
                varianty_each_cube.Add(item.ToList());
            }
            List<N_dimension> varianty_dimension = new List<N_dimension>();
            for (int i = 0; i < varianty_each_cube.Count; i++)
            {
                N_dimension before_cycle = new N_dimension();
                for (int it = 0; it < varianty_each_cube[i].Count; it++)
                {
                    before_cycle.spisok_coordinate.Add(varianty_each_cube[i][it]);
                }
                int chastotu_vhodgenya = 0;
                for (int t = 0; t < vector_oznak[0].Count; t++)
                {
                    List<bool> coor_in_cube = new List<bool>();
                    for (int t1 = 0; t1 < varianty_each_cube[i].Count; t1++)
                    {
                        if ((vector_oznak[t1][t] <= (varianty_each_cube[i][t1] + (kroki_n_hist[t1] / 2.0))) && (vector_oznak[t1][t] >= (varianty_each_cube[i][t1] - (kroki_n_hist[t1] / 2.0))))
                        {
                            coor_in_cube.Add(true);
                        }
                        else
                        {
                            coor_in_cube.Add(false);
                        }

                    }
                    if (!coor_in_cube.Contains(false))
                    {
                        chastotu_vhodgenya++;
                    }

                }
                before_cycle.chastota_p = chastotu_vhodgenya;
                before_cycle.vidnosna_chastota = (Convert.ToDouble(chastotu_vhodgenya) / Convert.ToDouble(vector_oznak[0].Count));
                varianty_dimension.Add(before_cycle);
            }
            dataGridView12.Rows.Clear();
            dataGridView12.Columns.Clear();
            dataGridView12.AllowUserToAddRows = false;
            dataGridView12.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView12.Columns.Add($"варіанта", $"Варіанти");
            dataGridView12.Columns.Add($"відносна частота", $"Відносна частота");
            dataGridView12.Columns.Add($"відносна частота", $"Частота");
            for (int i = 0; i < varianty_dimension.Count; i++)
            {
                string str_coore = "";
                for (int r = 0; r < varianty_dimension[i].spisok_coordinate.Count; r++)
                {
                    str_coore += $"{Math.Round(varianty_dimension[i].spisok_coordinate[r], 4)};";
                    //str_coore += $"{varianty_dimension[i].spisok_coordinate[r]};";
                }
                dataGridView12.Rows.Add(str_coore, varianty_dimension[i].vidnosna_chastota, varianty_dimension[i].chastota_p);
            }
            //MessageBox.Show($"{varianty_dimension.Count}");
            //множині коеф кореляції
            double deter_ocinky_korel_matrix = Program.Determinate(R_matrix, vector_oznak.Count);
            List<double> deter_kk_r_matrix = Program.R_matrix_kk_determinate(vector_oznak);
            dataGridView14.Rows.Clear();
            dataGridView14.Columns.Clear();
            dataGridView14.AllowUserToAddRows = false;
            dataGridView14.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView14.Columns.Add($"варіанта", $"Ознака");
            dataGridView14.Columns.Add($"відносна частота", $"Значення");
            dataGridView14.Columns.Add($"F", $"f статистика");
            dataGridView14.Columns.Add($"H", $"Ho ");
            double kvantil_Fishera = Program.Kvantil_Fishera(1.0-Double.Parse(textBox14.Text),vector_oznak.Count, vector_oznak[0].Count - vector_oznak.Count - 1);
            for (int i = 0; i < deter_kk_r_matrix.Count; i++)
            {
                double r_x_k= Math.Sqrt(1.0 - (deter_ocinky_korel_matrix/deter_kk_r_matrix[i]));
                double f_n_i = (Convert.ToDouble(vector_oznak[0].Count - vector_oznak.Count - 1) / Convert.ToDouble(vector_oznak.Count)) * ((Math.Pow(r_x_k,2.0)/(1.0- Math.Pow(r_x_k, 2.0))));
                if (f_n_i<=kvantil_Fishera)
                {
                    dataGridView14.Rows.Add(i, Math.Round(r_x_k,4), Math.Round(f_n_i,4),"Коефіцієнт не є значущим");
                }
                else
                {
                    dataGridView14.Rows.Add(i, Math.Round(r_x_k, 4), Math.Round(f_n_i, 4), "Коефіцієнт є значущим");
                }


            }
            //часткові коеф кореляції////////////////

            List<double> value_of_chast_koef_korelacia = new List<double>();//коефіцієнти кореляції часткові
            List<int> list_with_index = new List<int>();
            for (int i = 0; i < vector_oznak.Count; i++)
            {
                list_with_index.Add(i);
            }
            List<List<int>> List_generate_main_index = new List<List<int>>();//пари головних коефіцієнтів
            var res_main_index = Extansions.GetKCombs(list_with_index, 2);
            foreach (var item in res_main_index)
            {
                List_generate_main_index.Add(item.ToList());
            }
            for (int i = 0; i < List_generate_main_index.Count; i++)
            {
                List<int> rest_index = new List<int>();
                for (int ty = 0; ty < vector_oznak.Count; ty++)
                {
                    if (!List_generate_main_index[i].Contains(ty))
                    {
                        rest_index.Add(ty);
                    }
                }
                double koef_liniyn_in_cycle = Program.rekursia_chastkoviy_ocinka(vector_oznak, List_generate_main_index[i],rest_index);
                value_of_chast_koef_korelacia.Add(koef_liniyn_in_cycle);

            }

            dataGridView13.Rows.Clear();
            dataGridView13.Columns.Clear();
            dataGridView13.AllowUserToAddRows = false;
            dataGridView13.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView13.Columns.Add($"варіанта", $"Індекс");
            dataGridView13.Columns.Add($"відносна частота", $"Нижня межа");
            dataGridView13.Columns.Add($"F", $"Значення");
            dataGridView13.Columns.Add($"H", $"Верхня межа");
            dataGridView13.Columns.Add($"H", $"t-статистика");
            dataGridView13.Columns.Add($"H", $"Значущість");
            double kvantil_normalnoho_rozpodilu = Program.Kvantil_Norm_distibution(Double.Parse(textBox14.Text)/2.0);
            double kvantil_student = Program.Kvantil_Studenta(Double.Parse(textBox14.Text), Convert.ToDouble(vector_oznak[0].Count - 4 - vector_oznak.Count));
            for (int i = 0; i < List_generate_main_index.Count; i++)
            {
                string main_indes = "";
                for (int t = 0; t < List_generate_main_index[i].Count; t++)
                {
                    if (t== List_generate_main_index[i].Count-1)
                    {
                        main_indes += $"{List_generate_main_index[i][t]}";
                    }
                    else
                    {
                        main_indes += $"{List_generate_main_index[i][t]},";
                    }
                   
                }
                string rest_indes=" {";
                for (int ty = 0; ty < vector_oznak.Count; ty++)
                {
                    if (!List_generate_main_index[i].Contains(ty))
                    {
                        rest_indes+=$"{ty},";
                    }
                }
                rest_indes += "}";
                double t_statiscit = (value_of_chast_koef_korelacia[i] * Math.Sqrt(Convert.ToDouble(vector_oznak[0].Count-2-vector_oznak.Count-2)))/Math.Sqrt(1.0-Math.Pow(value_of_chast_koef_korelacia[i], 2));
                double hight_bound = (0.5*Math.Log((1.0 + value_of_chast_koef_korelacia[i]) / (1.0 - value_of_chast_koef_korelacia[i])))+((kvantil_normalnoho_rozpodilu) /(Convert.ToDouble(vector_oznak[0].Count - 3 - vector_oznak.Count-2)));
                double low_bound = (0.5*Math.Log((1.0 + value_of_chast_koef_korelacia[i]) / (1.0 - value_of_chast_koef_korelacia[i]))) - ((kvantil_normalnoho_rozpodilu) / (Convert.ToDouble(vector_oznak[0].Count - 3 - vector_oznak.Count-2)));
                double left_bound = (Math.Exp(2.0 * low_bound) - 1.0) / (Math.Exp(2.0*low_bound)+1.0);
                double right_bound= (Math.Exp(2.0 * hight_bound) - 1.0) / (Math.Exp(2.0 * hight_bound) + 1.0);
                //if (t_statiscit<=kvantil_student)
                //{
                //    dataGridView13.Rows.Add(main_indes + rest_indes, Math.Round(left_bound, 4), Math.Round(value_of_chast_koef_korelacia[i], 4), Math.Round(right_bound, 4), Math.Round(t_statiscit, 4), "+");
                //}
                //else
                //{
                //    dataGridView13.Rows.Add(main_indes + rest_indes, Math.Round(left_bound, 4), Math.Round(value_of_chast_koef_korelacia[i], 4), Math.Round(right_bound, 4), Math.Round(t_statiscit, 4), "-");
                //}
                if (Math.Abs(t_statiscit) > kvantil_student)
                {
                    dataGridView13.Rows.Add(main_indes + rest_indes, Math.Round(right_bound, 4), Math.Round(value_of_chast_koef_korelacia[i], 4), Math.Round(left_bound, 4), Math.Round(t_statiscit, 4), "+");
                }
                else
                {
                    dataGridView13.Rows.Add(main_indes + rest_indes, Math.Round(right_bound, 4), Math.Round(value_of_chast_koef_korelacia[i], 4), Math.Round(left_bound, 4), Math.Round(t_statiscit, 4), "-");
                }
                //value_of_chast_koef_korelacia.;

            }

            
        }
        private void button16_Click(object sender, EventArgs e)
        {
            List<int> nomer_oznak = new List<int>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            }
            List<List<double>> vector_oznak = new List<List<double>>();
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                List<double> resnoa = new List<double>();
                for (int t = 0; t < Universe.Data_Vectors[nomer_oznak[i]].Count; t++)
                {
                    resnoa.Add(Universe.Data_Vectors[nomer_oznak[i]][t]);
                }
                vector_oznak.Add(resnoa);
                
            }
            string promptValue = Prompt.ShowDialog("Значення", "Вилучення аномальних значень");
            if (string.IsNullOrEmpty(promptValue))
            {
                return;
            }
            double alpha_anomal = Convert.ToDouble(promptValue);

            /////побудова варіаційного ряду
            int kilkisty_classiv = 1;
            if (vector_oznak[0].Count <= 100)
            {
                kilkisty_classiv = (int)Math.Sqrt(vector_oznak[0].Count);
            }
            else
            {
                kilkisty_classiv = (int)Math.Pow(vector_oznak[0].Count, 0.3333333);
            }
            List<double> kroki_n_hist = Program.krok_var_ryadu(vector_oznak, kilkisty_classiv);


            var result = Program.varianta_each_vectors(vector_oznak, kilkisty_classiv).CartesianProduct();
            List<List<double>> varianty_each_cube = new List<List<double>>();
            foreach (var item in result)
            {
                varianty_each_cube.Add(item.ToList());
            }
            List<N_dimension> varianty_dimension = new List<N_dimension>();
            for (int i = 0; i < varianty_each_cube.Count; i++)
            {
                N_dimension before_cycle = new N_dimension();
                for (int it = 0; it < varianty_each_cube[i].Count; it++)
                {
                    before_cycle.spisok_coordinate.Add(varianty_each_cube[i][it]);
                }
                int chastotu_vhodgenya = 0;
                for (int t = 0; t < vector_oznak[0].Count; t++)
                {
                    List<bool> coor_in_cube = new List<bool>();
                    for (int t1 = 0; t1 < varianty_each_cube[i].Count; t1++)
                    {
                        if ((vector_oznak[t1][t] <= (varianty_each_cube[i][t1] + (kroki_n_hist[t1] / 2.0))) && (vector_oznak[t1][t] >= (varianty_each_cube[i][t1] - (kroki_n_hist[t1] / 2.0))))
                        {
                            coor_in_cube.Add(true);
                        }
                        else
                        {
                            coor_in_cube.Add(false);
                        }

                    }
                    if (!coor_in_cube.Contains(false))
                    {
                        chastotu_vhodgenya++;
                    }

                }
                before_cycle.chastota_p = chastotu_vhodgenya;
                before_cycle.vidnosna_chastota = (Convert.ToDouble(chastotu_vhodgenya) / Convert.ToDouble(vector_oznak[0].Count));
                varianty_dimension.Add(before_cycle);
            }

            for (int i = 0; i < varianty_dimension.Count; i++)
            {
                if (varianty_dimension[i].vidnosna_chastota!=0)
                {
                    if (varianty_dimension[i].vidnosna_chastota <= alpha_anomal)
                    {
                        reverse_cycle:
                        for (int t = 0; t < vector_oznak[0].Count; t++)
                        {
                            List<bool> coor_in_cube = new List<bool>();
                            for (int t1 = 0; t1 < varianty_dimension[i].spisok_coordinate.Count; t1++)
                            {
                                if ((vector_oznak[t1][t] <= (varianty_each_cube[i][t1] + (kroki_n_hist[t1] / 2.0))) && (vector_oznak[t1][t] >= (varianty_each_cube[i][t1] - (kroki_n_hist[t1] / 2.0))))
                                {
                                    coor_in_cube.Add(true);
                                }
                                else
                                {
                                    coor_in_cube.Add(false);
                                }

                            }
                            if (!coor_in_cube.Contains(false))
                            {
                                for (int q = 0; q < vector_oznak.Count; q++)
                                {
                                    vector_oznak[q].RemoveAt(t);
                                }
                                goto reverse_cycle;
                            }

                        }
                    }
                }
            }

            multy_dimension(vector_oznak);
            
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                List<double> resnoa = vector_oznak[i];
    
                Universe.Data_Vectors.Add(resnoa);
                listBox1.Items.Add($"{listBox1.Items[nomer_oznak[i]].ToString()} NoA");
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
            
        }

        private void button12_Click_1(object sender, EventArgs e)
        {

        }

        private void button12_Click_2(object sender, EventArgs e)
        {
            List<int> nomer_oznak = new List<int>();
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                if (listBox2.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            }
            List<List<List<double>>> vector_oznak = new List<List<List<double>>>();
            richTextBox4.Text = "";
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                List<List<double>> resnoa = new List<List<double>>();
                for (int t = 0; t < Universe.Data_Value[nomer_oznak[i]].Count; t++)
                {
                    List<double> insert_2_cycle = new List<double>();
                    for (int r = 0; r < Universe.Data_Value[nomer_oznak[i]][t].Count; r++)
                    {
                        insert_2_cycle.Add(Universe.Data_Value[nomer_oznak[i]][t][r]);
                    }
                    resnoa.Add(insert_2_cycle);
                }
                vector_oznak.Add(resnoa);

            }
            for (int i = 0; i < vector_oznak.Count-1; i++)
            {
                if (vector_oznak[i].Count != vector_oznak[i+1].Count)
                {
                    MessageBox.Show("виміри сукупностей різні");
                    return;
                }
            }
            int n_small = vector_oznak[0].Count;//к-ксть характеристик
            if (vector_oznak.Count == 2 && (vector_oznak[0].Count == vector_oznak[1].Count))
            {
                int N1 = vector_oznak[0][0].Count;
                int N2 = vector_oznak[1][0].Count;

                List<double> vector_serednih_1_omega = new List<double>();
                List<double> vector_serednih_2_omega = new List<double>();
                List<double> vector_sigma_1_omega = new List<double>();
                List<double> vector_sigma_2_omega = new List<double>();
                List<List<double>> vector_oznak_1_omega = vector_oznak[0];
                List<List<double>> vector_oznak_2_omega = vector_oznak[1];
                

                double[,] dc_1 = Program.DC_matrix(vector_oznak_1_omega);
                double[,] dc_2 = Program.DC_matrix(vector_oznak_2_omega);
                double[,] S_0 = new double[vector_oznak_1_omega.Count, vector_oznak_1_omega.Count];
                double[,] S_1 = new double[vector_oznak_2_omega.Count, vector_oznak_2_omega.Count];
                for (int i = 0; i < vector_oznak_1_omega.Count; i++)
                {
                    for (int j = 0; j < vector_oznak_1_omega.Count; j++)
                    {
                        double x_i_l_x_j_l = 0;
                        double y_i_l_y_j_l = 0;
                        double x_i_l = 0;
                        double x_j_l = 0;
                        double y_i_l = 0;
                        double y_j_l = 0;
                        for (int l = 0; l < N1; l++)
                        {
                            x_i_l_x_j_l += (vector_oznak_1_omega[i][l] * vector_oznak_1_omega[j][l]);
                            x_i_l += vector_oznak_1_omega[i][l];
                            x_j_l += vector_oznak_1_omega[j][l];
                        }
                        for (int l = 0; l < N2; l++)
                        {
                            y_i_l_y_j_l += (vector_oznak_2_omega[i][l] * vector_oznak_2_omega[j][l]);
                            y_i_l += vector_oznak_2_omega[i][l];
                            y_j_l += vector_oznak_2_omega[j][l];
                        }
                        double s_i_j = x_i_l_x_j_l + y_i_l_y_j_l - (((x_i_l + y_i_l) * (x_j_l + y_j_l)) / (Convert.ToDouble(N1 + N2)));
                        s_i_j = s_i_j / Convert.ToDouble(N1 + N2 - 2);
                        S_0[i, j] = s_i_j;


                    }
                } ///матриця S_0
                for (int i = 0; i < vector_oznak_2_omega.Count; i++)
                {
                    for (int j = 0; j < vector_oznak_2_omega.Count; j++)
                    {
                        double x_i_l_x_j_l = 0;
                        double y_i_l_y_j_l = 0;
                        double x_i_l = 0;
                        double x_j_l = 0;
                        double y_i_l = 0;
                        double y_j_l = 0;
                        for (int l = 0; l < N1; l++)
                        {
                            x_i_l_x_j_l += (vector_oznak_1_omega[i][l] * vector_oznak_1_omega[j][l]);
                            x_i_l += vector_oznak_1_omega[i][l];
                            x_j_l += vector_oznak_1_omega[j][l];
                        }
                        for (int l = 0; l < N2; l++)
                        {
                            y_i_l_y_j_l += (vector_oznak_2_omega[i][l] * vector_oznak_2_omega[j][l]);
                            y_i_l += vector_oznak_2_omega[i][l];
                            y_j_l += vector_oznak_2_omega[j][l];
                        }
                        double s_i_j = x_i_l_x_j_l + y_i_l_y_j_l - ((x_i_l * x_j_l) / Convert.ToDouble(N1)) - ((y_i_l * y_j_l) / Convert.ToDouble(N2));
                        s_i_j = s_i_j / Convert.ToDouble(N1 + N2 - 2);
                        S_1[i, j] = s_i_j;


                    }
                } ///матриця S_1

                double S0_determ = Program.Determinate(S_0, vector_oznak[0].Count);
                double S1_determ = Program.Determinate(S_1, vector_oznak[0].Count);
                double Statistic_V = -1.0 * Convert.ToDouble(N1 + N2 - 2 - (n_small / 2.0)) * Math.Log(S1_determ / S0_determ);//статистика V
               
                double phi_kvadrat_1 = Program.Kvantil_Pirsona(1.0-Double.Parse(textBox14.Text), Convert.ToDouble(n_small));
                if (Statistic_V<=phi_kvadrat_1)
                {
                    richTextBox4.Text += $"Рівність двох багатовимірних середніх у разі рівних ДК матриць \n";
                    richTextBox4.Text += $"Квантиль {Math.Round(phi_kvadrat_1, 4)} \n";
                    richTextBox4.Text += $"V={Math.Round(Statistic_V,4)}. Головна гіпотеза підтверджена\n";
                    
                }
                else
                {
                    richTextBox4.Text += $"Рівність двох багатовимірних середніх у разі рівних ДК матриць \n";
                    richTextBox4.Text += $"Квантиль {Math.Round(phi_kvadrat_1, 4)} \n";
                    richTextBox4.Text += $"V={Math.Round(Statistic_V, 4)}. Головна гіпотеза відхилена\n";
                }


            }//правильно робить
            ////збіг k n-вимірних середніх при розбіжності діисперсійно-коваріаційної матриці
            List<double[,]> S_d = new List<double[,]>();
            List<List<double>> x_d_small_sereden_list=new List<List<double>>();
            for (int i = 0; i < vector_oznak.Count; i++)
            {
                List<double> X_l_d = new List<double>();
                List<double> x_smal_seredne = new List<double>();
                for (int t = 0; t < n_small; t++)
                {
                    x_smal_seredne.Add(0.0);
                    X_l_d.Add(0.0);
                }
                for (int t = 0; t < vector_oznak[i][0].Count; t++)
                {
                    for (int re = 0; re < n_small; re++)
                    {
                        X_l_d[re] = X_l_d[re] + vector_oznak[i][re][t];
                    }
                }
                for (int t = 0; t < x_smal_seredne.Count; t++)
                {
                    x_smal_seredne[t] = X_l_d[t] / Convert.ToDouble(vector_oznak[i][0].Count);
                }
                x_d_small_sereden_list.Add(x_smal_seredne);
                X_l_d.Clear();
                for (int t = 0; t < n_small; t++)
                {
                   
                    X_l_d.Add(0.0);
                }
                List<double[,]> S_d_in_cicle = new List<double[,]>();
                for (int t = 0; t < vector_oznak[i][0].Count; t++)
                {
                    for (int re = 0; re < n_small; re++)
                    {
                        X_l_d[re] = vector_oznak[i][re][t] - x_smal_seredne[re];
                    }
                    S_d_in_cicle.Add(Program.multiplication_column_on_rows(X_l_d, X_l_d));
                }
                double[,] empty_matrix = new double[n_small, n_small];
                for (int t = 0; t < n_small; t++)
                {
                    for (int w = 0; w < n_small; w++)
                    {
                        empty_matrix[t, w] = 0.0;
                    }
                }
                
                for (int t = 0; t < S_d_in_cicle.Count; t++)
                {
                    empty_matrix = Program.addition_matrix(S_d_in_cicle[t],empty_matrix);
                }
                double[,] final = Program.multiplication_matrix_on_number(empty_matrix,(1.0/Convert.ToDouble(vector_oznak[i][0].Count-1)));
                S_d.Add(final);

            }
            List<double[,]> S_d_after_cicle = new List<double[,]>();
            List<List<double>> x_d_after_cycle = new List<List<double>>();
            for (int i = 0; i < vector_oznak.Count; i++)
            {
                S_d_after_cicle.Add(Program.multiplication_matrix_on_number(Program.Inverse_matrix(S_d[i]),Convert.ToDouble(vector_oznak[i][0].Count)));
                x_d_after_cycle.Add(Program.multiplication_matrix_on_colums(Program.multiplication_matrix_on_number(Program.Inverse_matrix(S_d[i]), Convert.ToDouble(vector_oznak[i][0].Count)), x_d_small_sereden_list[i]));
            }
            double[,] S_d_after_empty = new double[n_small, n_small];//перший множник
            for (int t = 0; t < n_small; t++)
            {
                for (int w = 0; w < n_small; w++)
                {
                    S_d_after_empty[t, w] = 0.0;
                }
            }
            for (int t = 0; t < S_d_after_cicle.Count; t++)
            {
                S_d_after_empty = Program.addition_matrix(S_d_after_cicle[t], S_d_after_empty);
            }
          //  S_d_after_empty = Program.Inverse_matrix(S_d_after_empty);
            List<double> empty_list = new List<double>();
            for (int t = 0; t < n_small; t++)
            {
                empty_list.Add(0.0); 
            }
            //for (int i = 0; i < x_d_small_sereden_list.Count; i++)
            //{
            //    for (int r = 0; r < x_d_small_sereden_list[i].Count; r++)
            //    {
            //        empty_list[r] = empty_list[r] + x_d_small_sereden_list[i][r];
            //    }
            //}
            for (int i = 0; i < x_d_after_cycle.Count; i++)
            {
                for (int r = 0; r < x_d_after_cycle[i].Count; r++)
                {
                    empty_list[r] = empty_list[r] + x_d_after_cycle[i][r];
                }
            }
            List<double> x_zagalne_seredne = Program.multiplication_matrix_on_colums(Program.Inverse_matrix(S_d_after_empty), empty_list);//x-zagalne seredne
            
            double V_stat_k = 0;
            for (int i = 0; i < vector_oznak.Count; i++)
            {
                List<double> cycle_v_sta = new List<double>();
                List<double> cycle_v_sta12 = new List<double>();
                for (int t = 0; t < n_small; t++)
                {
                    cycle_v_sta.Add(0.0);
                    cycle_v_sta12.Add(0.0);
                }
                for (int re = 0; re < n_small; re++)
                {
                    cycle_v_sta[re] = (x_d_small_sereden_list[i][re] - x_zagalne_seredne[re]);
                    cycle_v_sta12[re] = (x_d_small_sereden_list[i][re] - x_zagalne_seredne[re]);
                }
                List<double> cycle_v_sta_1 = Program.multiplication_row_on_matrix(cycle_v_sta,Program.Inverse_matrix(S_d[i]));
                V_stat_k += Program.multiplication__rows_on_colums(cycle_v_sta_1, cycle_v_sta12)* Convert.ToDouble(vector_oznak[i][0].Count);
            }
            double phi_kvadrat_2 = Program.Kvantil_Pirsona(1.0 - Double.Parse(textBox14.Text), Convert.ToDouble(n_small*(vector_oznak.Count-1)));
            if (V_stat_k<=phi_kvadrat_2)
            {
                richTextBox4.Text += $"Збіг k n-вимірних середніх при розбіжності ДК матриць \n";
                richTextBox4.Text += $"Квантиль {Math.Round(phi_kvadrat_2, 4)} \n";
                richTextBox4.Text += $"V={Math.Round(V_stat_k, 4)}. Головна гіпотеза підтверджена\n";

            }
            else
            {
                richTextBox4.Text += "Збіг k n-вимірних середніх при розбіжності ДК матриць \n";
                richTextBox4.Text += $"Квантиль {Math.Round(phi_kvadrat_2, 4)} \n";
                richTextBox4.Text += $"V={Math.Round(V_stat_k, 4)}. Головна гіпотеза відхилена\n";
            }
            //збіг дисперсійно коваріаційних матриць
            double[,] empty_matrix_after_cycle = new double[n_small, n_small];//S-matrix
            for (int t = 0; t < n_small; t++)
            {
                for (int w = 0; w < n_small; w++)
                {
                    empty_matrix_after_cycle[t, w] = 0.0;
                }
            }
            List<double[,]> S_d_DC = new List<double[,]>();
            for (int i = 0; i < vector_oznak.Count; i++)
            {
                S_d_DC.Add(Program.DC_matrix(vector_oznak[i]));
            }
            //for (int t = 0; t < S_d.Count; t++)
            //{
            //    empty_matrix_after_cycle = Program.addition_matrix(Program.multiplication_matrix_on_number(S_d[t], Convert.ToDouble(vector_oznak[t][0].Count - 1)), empty_matrix_after_cycle);
            //}
            for (int t = 0; t < S_d.Count; t++)
            {
                empty_matrix_after_cycle = Program.addition_matrix(Program.multiplication_matrix_on_number(S_d_DC[t], Convert.ToDouble(vector_oznak[t][0].Count - 1)), empty_matrix_after_cycle);
            }
            
            double N_big = 0;
            for (int i = 0; i < vector_oznak.Count; i++)
            {
                N_big += Convert.ToDouble(vector_oznak[i][0].Count);
            }
            double[,] S_big_matrix = Program.multiplication_matrix_on_number(empty_matrix_after_cycle, 1.0 / (N_big - Convert.ToDouble(vector_oznak.Count)));
            double v_stat_kov_matrix_end = 0;
            //for (int i = 0; i < vector_oznak.Count; i++)
            //{
            //    double ln_in_cycle = Math.Log(Program.Determinate(S_big_matrix, n_small) / Program.Determinate(S_d[i],n_small));
            //    v_stat_kov_matrix_end += ((Convert.ToDouble(vector_oznak[i][0].Count - 1) / 2.0) * ln_in_cycle);
            //}
            for (int i = 0; i < vector_oznak.Count; i++)
            {
                double ln_in_cycle = Math.Log(Program.Determinate(S_big_matrix, n_small) / Program.Determinate(S_d_DC[i], n_small));
                v_stat_kov_matrix_end += ((Convert.ToDouble(vector_oznak[i][0].Count - 1) / 2.0) * ln_in_cycle);
            }
            double phi_kvadrat_3 = Program.Kvantil_Pirsona(1.0 - Double.Parse(textBox14.Text), Convert.ToDouble(n_small *(n_small+1)*(vector_oznak.Count - 1)/2));
            if (v_stat_kov_matrix_end<=phi_kvadrat_3)
            {
                richTextBox4.Text += $"Збіг ДК матриць \n";
                richTextBox4.Text += $"Квантиль {Math.Round(phi_kvadrat_3, 4)} \n";
                richTextBox4.Text += $"V={Math.Round(v_stat_kov_matrix_end, 4)}. Головна гіпотеза підтверджена\n";

            }
            else
            {
                richTextBox4.Text += "Збіг ДК матриць \n";
                richTextBox4.Text += $"Квантиль {Math.Round(phi_kvadrat_3, 4)} \n";
                richTextBox4.Text += $"V={Math.Round(v_stat_kov_matrix_end, 4)}. Головна гіпотеза відхилена\n";
            }
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            List<int> nomer_oznak = new List<int>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            }
            List<List<double>> vector_oznak = new List<List<double>>();
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                List<double> resnoa = new List<double>();
                for (int t = 0; t < Universe.Data_Vectors[nomer_oznak[i]].Count; t++)
                {
                    resnoa.Add(Universe.Data_Vectors[nomer_oznak[i]][t]);
                }
                vector_oznak.Add(resnoa);

            }
            List<int> list_with_index = new List<int>();
            
            if (comboBox5.SelectedIndex == 0)//матриця розпаду
            {
                for (int i = 0; i < vector_oznak.Count; i++)
                {
                    list_with_index.Add(i);
                }
                List<List<int>> List_generate_main_index = new List<List<int>>();//пари головних коефіцієнтів
                var res_main_index = Extansions.GetPermutationsWithRept(list_with_index, 2);
                foreach (var item in res_main_index)
                {
                    List_generate_main_index.Add(item.ToList());
                }
                Size s = new Size(300, 300);
                int x_location = 1;
                int y_location = 10;
                for (int j = 0; j < List_generate_main_index.Count; j++)
                {
                    Chart chart_in_cycle = new Chart();

                    chart_in_cycle.Size = s;
                    chart_in_cycle.Series.Add("1q");
                    chart_in_cycle.Series.Add("2q");


                    chart_in_cycle.Series[0].IsVisibleInLegend = false;
                    chart_in_cycle.Series[1].IsVisibleInLegend = false;
                    chart_in_cycle.ChartAreas.Add("0");
                    

                    chart_in_cycle.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chart_in_cycle.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                    chart_in_cycle.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                    chart_in_cycle.ChartAreas[0].AxisY.MinorGrid.Enabled = false; ;
                    chart_in_cycle.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
                    chart_in_cycle.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
                   
                    chart_in_cycle.Location = new Point(x_location,y_location);
                    if (List_generate_main_index[j][1]== List_generate_main_index[j][0])
                    {//будуємо гістограму
                        chart_in_cycle.Series[0].ChartType = SeriesChartType.Column;
                        chart_in_cycle.Series[1].ChartType = SeriesChartType.Line;
                        chart_in_cycle.Series[1].Color = Color.Black;

                        List<rows> a4 = Program.Variaciyniy_ryad(vector_oznak[List_generate_main_index[j][0]]);
                        chart1.ChartAreas[0].AxisX.Title = "Значення";
                        chart1.ChartAreas[0].AxisY.Title = "Відносна частота";
                        int kil_classiv=1;
                        if (a4.Count <= 100)
                        {
                            kil_classiv = (int)Math.Sqrt(a4.Count);
                        }
                        else
                        {
                            kil_classiv = (int)Math.Pow(a4.Count, 0.3333333);
                        }
                        double sered = a4[a4.Count - 1].variant - a4[0].variant;
                        double shah = sered / kil_classiv;
                        double shaht = shah;
                        shah += a4[0].variant;
                        double sum = 0;
                        double t = 0;
                        int chislo = 0;
                        int otchet = 0;
                        int end = 0;
                        double sum1 = 0;
                        for (int i = 0; i < a4.Count; i++)
                        {
                            
                            if (a4[i].variant <= shah)
                            {
                                sum += a4[i].chast;
                                sum1 += a4[i].chast;
                                end = i;
                                if (a4.Count - 1 == i)
                                {
                                    otchet = i;

                                    chart_in_cycle.Series[0].Points.AddXY(shah - shaht, 0);
                                    chart_in_cycle.Series[0].Points.AddXY(shah - shaht, sum);
                                    chart_in_cycle.Series[0].Points.AddXY(shah, sum);
                                    chart_in_cycle.Series[0].Points.AddXY(shah, 0);

                                    chart_in_cycle.Series[1].Points.AddXY(shah - shaht, 0);
                                    chart_in_cycle.Series[1].Points.AddXY(shah - shaht, sum);
                                    chart_in_cycle.Series[1].Points.AddXY(shah, sum);
                                    chart_in_cycle.Series[1].Points.AddXY(shah, 0);

                                }
                            }
                            else
                            {

                                otchet = i;

                                chart_in_cycle.Series[0].Points.AddXY(shah-shaht, 0);
                                chart_in_cycle.Series[0].Points.AddXY(shah-shaht, sum);
                                chart_in_cycle.Series[0].Points.AddXY(shah, sum);
                                chart_in_cycle.Series[0].Points.AddXY(shah, 0);
                                chart_in_cycle.Series[1].Points.AddXY(shah - shaht, 0);
                                chart_in_cycle.Series[1].Points.AddXY(shah - shaht, sum);
                                chart_in_cycle.Series[1].Points.AddXY(shah, sum);
                                chart_in_cycle.Series[1].Points.AddXY(shah, 0);
                                shah += shaht;
                                sum = a4[i].chast;
                                sum1 += a4[i].chast;
                                chislo = i;
                                t += shaht;
                            }
                            
                        }
                        chart_in_cycle.ChartAreas[0].AxisX.Title = $"Значення {List_generate_main_index[j][0]+1}";
                        chart_in_cycle.ChartAreas[0].AxisY.Title = "Відносна частота";
                        chart_in_cycle.Series[0].ChartType = SeriesChartType.Range;
                        chart_in_cycle.ChartAreas[0].AxisX.LabelStyle.Format = "0.000";
                        chart_in_cycle.ChartAreas[0].AxisX.Maximum = a4[a4.Count-1].variant;
                        chart_in_cycle.ChartAreas[0].AxisX.Minimum = a4[0].variant;
                        panel2.Controls.Add(chart_in_cycle);
                        y_location += 310;
                    }
                    else
                    {// будуємо кореляційні поля
                        chart_in_cycle.Series[0].ChartType = SeriesChartType.Point;
                       
                        if (vector_oznak[List_generate_main_index[j][0]].Count== vector_oznak[List_generate_main_index[j][1]].Count)
                        {
                            for (int i = 0; i < vector_oznak[List_generate_main_index[j][0]].Count; i++)
                            {
                                chart_in_cycle.Series[0].Points.AddXY(vector_oznak[List_generate_main_index[j][0]][i], vector_oznak[List_generate_main_index[j][1]][i]);
                            }
                            chart_in_cycle.ChartAreas[0].AxisX.LabelStyle.Format = "0.000";
                            chart_in_cycle.ChartAreas[0].AxisY.LabelStyle.Format = "0.00";
                            chart_in_cycle.ChartAreas[0].AxisX.Title = $"Ознака{List_generate_main_index[j][0] + 1}";
                            chart_in_cycle.ChartAreas[0].AxisY.Title = $"Ознака{List_generate_main_index[j][1] + 1}";
                            chart_in_cycle.ChartAreas[0].AxisX.Maximum = vector_oznak[List_generate_main_index[j][0]].Max();
                            chart_in_cycle.ChartAreas[0].AxisX.Minimum = vector_oznak[List_generate_main_index[j][0]].Min();
                            chart_in_cycle.ChartAreas[0].AxisY.Maximum = vector_oznak[List_generate_main_index[j][1]].Max();
                            chart_in_cycle.ChartAreas[0].AxisY.Minimum = vector_oznak[List_generate_main_index[j][1]].Min();
                            panel2.Controls.Add(chart_in_cycle);
                            y_location += 310;
                        }
                        else
                        {
                            Panel p1_cucle = new Panel();
                            p1_cucle.Size = new Size(290, 290);
                            p1_cucle.BackColor = Color.Snow;
                            p1_cucle.Location = new Point(x_location, y_location);
                            panel2.Controls.Add(p1_cucle);
                            y_location += 310;
                            //continue;
                        }

                        
                        
                    }
                    if (List_generate_main_index[j][1] == (vector_oznak.Count - 1))
                    {
                        x_location += 310;
                        y_location = 10;
                    }
                }

            }
            else if (comboBox5.SelectedIndex == 1)//бульбашка
            {
                if (vector_oznak.Count==3)
                {
                    Size s = new Size(1000, 800);
                    int x_location = 1;
                    int y_location = 10;
                    Chart chart_in_cycle = new Chart();
                    chart_in_cycle.Size = s;
                    chart_in_cycle.ChartAreas.Add("0");
                    chart_in_cycle.Series.Add("1q");
                    chart_in_cycle.Series[0].IsVisibleInLegend = false;
                    chart_in_cycle.Series[0].ChartType = SeriesChartType.Bubble;
                    chart_in_cycle.Series[0].MarkerStyle = MarkerStyle.Circle;
                    chart_in_cycle.Series[0].Color = Color.FromArgb(50,Color.Blue);
                    chart_in_cycle.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chart_in_cycle.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                    chart_in_cycle.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                    chart_in_cycle.ChartAreas[0].AxisY.MinorGrid.Enabled = false; ;
                    chart_in_cycle.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
                    chart_in_cycle.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;

                    chart_in_cycle.Location = new Point(x_location, y_location);
                    if ((vector_oznak[0].Count == vector_oznak[1].Count)&&(vector_oznak[2].Count == vector_oznak[1].Count))
                    {
                        for (int rt = 0; rt < vector_oznak[0].Count; rt++)
                        {
                            chart_in_cycle.Series[0].Points.AddXY(vector_oznak[0][rt], vector_oznak[1][rt], Math.Sqrt(Math.Abs(vector_oznak[2][rt])/Math.PI) );
                        }
                        panel2.Controls.Add(chart_in_cycle);
                    }
                }
                else
                {
                    return;
                }
                
            }
            else if (comboBox5.SelectedIndex == 2)
            {// паралельні координати
                Size s = new Size(1300, 800);
                int x_location = 1;
                int y_location = 10;
                Chart chart_in_cycle = new Chart();
                chart_in_cycle.ChartAreas.Add("0");
                chart_in_cycle.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                chart_in_cycle.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
                chart_in_cycle.ChartAreas[0].AxisX.Maximum = vector_oznak.Count;
                chart_in_cycle.ChartAreas[0].AxisX.Minimum = 1;
                chart_in_cycle.ChartAreas[0].AxisY.Minimum = 0;
                chart_in_cycle.ChartAreas[0].AxisY.Maximum=1;

                chart_in_cycle.Size = s;
               
                for (int i = 0; i < vector_oznak[0].Count; i++)
                {
                    chart_in_cycle.Series.Add("i"+i);
                    chart_in_cycle.Series[i].IsVisibleInLegend = false;
                    chart_in_cycle.Series[i].ChartType = SeriesChartType.Line;
                    chart_in_cycle.Series[i].Color = Color.Red;
                    for (int re = 0; re < vector_oznak.Count; re++)
                    {
                        double per_cent_in_cycle = (vector_oznak[re][i] - vector_oznak[re].Min()) / (vector_oznak[re].Max() - vector_oznak[re].Min());
                        chart_in_cycle.Series[i].Points.AddXY(re+1, per_cent_in_cycle);
                    }
                }
                chart_in_cycle.Location = new Point(x_location, y_location);
                panel2.Controls.Add(chart_in_cycle);
            }
            else if (comboBox5.SelectedIndex==3)
            {

                DataGridView dt_grd = new DataGridView();

                dt_grd.Height = 700;
                dt_grd.Width = 1100;
                dt_grd.BackgroundColor = Color.AliceBlue;

                dt_grd.RowTemplate.Height = Convert.ToInt16(Convert.ToDouble(dt_grd.Size.Height) / Convert.ToDouble(vector_oznak[0].Count));
               // dt_grd.RowTemplate.Height = 1;
                dt_grd.AllowUserToAddRows = false;
                for (int i = 0; i < vector_oznak.Count; i++)
                {
                    dt_grd.Columns.Add("t", "" + i);
                    dt_grd.Columns[i].Width = Convert.ToInt16(dt_grd.Size.Width / vector_oznak.Count) - 1;

                }
                for (int i = 0; i < vector_oznak[0].Count; i++)
                {
                    dt_grd.Rows.Add();
                }
                dt_grd.ColumnHeadersVisible = false;
                dt_grd.RowHeadersVisible = false;
                //dataGridView1.RowTemplate.Height = Convert.ToInt16(dataGridView1.Size.Height/ints.Count);
                for (int i = 0; i < vector_oznak.Count; i++)
                {
                    int k_cyle = 0;
                    for (int tr = vector_oznak[0].Count-1; tr >= 0; tr--)
                    {
                        double val = Convert.ToDouble(vector_oznak[i][tr] - vector_oznak[i].Min()) / Convert.ToDouble(vector_oznak[i].Max() - vector_oznak[i].Min());
                        int r = Convert.ToByte(255.0 * (1.0 - val));
                        int g = Convert.ToByte(255.0 * (1.0 - val));
                        int b = Convert.ToByte(255);
                        //int b = Convert.ToByte(255 * val);

                        Color s12 = Color.FromArgb(255, r, g, b);
                        dt_grd.Rows[k_cyle].Cells[i].Style.BackColor = s12;
                        
                        k_cyle++;
                    }
                    

                }
                dt_grd.ClearSelection();
                dt_grd.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
                dt_grd.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
                dt_grd.AdvancedCellBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                dt_grd.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                dt_grd.RowTemplate.Height = 20;
                dt_grd.Rows.Add();



                for (int i = 0; i < vector_oznak.Count; i++)
                {
                   
                    dt_grd.Rows[vector_oznak[0].Count].Cells[i].Value= i+1;
                    //dt_grd.Rows[vector_oznak[0].Count].Cells[i].Size.Width = (int)(Convert.ToInt16(dt_grd.Size.Width / vector_oznak.Count) - 1);
                }
                dt_grd.Location = new Point(1, 10);
                panel2.Controls.Add(dt_grd);
            } 
            else if (comboBox5.SelectedIndex == 4)
            {
                //радарна діаграмам
                Size s = new Size(1300, 800);
                int x_location = 1;
                int y_location = 10;
                Chart chart_in_cycle = new Chart();
                chart_in_cycle.ChartAreas.Add("0");

                string promptValue = Prompt.ShowDialog("Значення", "Номер спостереження");
                int nomer_spos = Convert.ToInt16(promptValue);
                chart_in_cycle.Size = s;

               
                    chart_in_cycle.Series.Add("i");
                    //chart_in_cycle.Series[0].IsVisibleInLegend = false;
                    chart_in_cycle.Series[0].ChartType = SeriesChartType.Radar;
                    chart_in_cycle.Series[0].Color = Color.Red;
                 for (int re = 0; re < vector_oznak.Count; re++)
                 {
                     double per_cent_in_cycle = (vector_oznak[re][nomer_spos] - vector_oznak[re].Min()) / (vector_oznak[re].Max() - vector_oznak[re].Min());
                     chart_in_cycle.Series[0].Points.AddXY($"{re+1}", per_cent_in_cycle);
                 }
                
                chart_in_cycle.Location = new Point(x_location, y_location);
                panel2.Controls.Add(chart_in_cycle);

            }


        }

        private void button17_Click(object sender, EventArgs e)
        {
            dataGridView15.Rows.Clear();
            dataGridView15.Columns.Clear();
            dataGridView15.RowHeadersVisible = false;
            dataGridView15.AllowUserToAddRows = false;
            dataGridView15.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView15.Columns.Add("Параметр","  ");
            dataGridView15.Columns.Add("Нижня межа", "Нижня межа");
            dataGridView15.Columns.Add("Значення", "Значення");
            dataGridView15.Columns.Add("Верхня межа", "Верхня межа");
            dataGridView15.Columns.Add("значущість", "Значущість");
            dataGridView15.Columns.Add("стандартизація", "Стандартизція");
            List<int> nomer_oznak = new List<int>();
            string promptValue = Prompt.ShowDialog("Значення", "Залежний вектор");
            int dependet_vector_number = Convert.ToInt16(promptValue)-1;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            }
            if (dependet_vector_number > nomer_oznak.Count)
            {
                MessageBox.Show("Невірний формат");
                return;
            }
            List<List<double>> vector_oznak = new List<List<double>>();//X
            List<List<double>> vector_oznak_all = new List<List<double>>();
            List<double> dependet_vector = new List<double>();
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                List<double> resnoa = new List<double>();
                for (int t = 0; t < Universe.Data_Vectors[nomer_oznak[i]].Count; t++)
                {
                    resnoa.Add(Universe.Data_Vectors[nomer_oznak[i]][t]);
                }
                if (i==dependet_vector_number)
                {
                    for (int d = 0; d < resnoa.Count; d++)
                    {
                        dependet_vector.Add(resnoa[d]);
                    }
                }
                else
                {
                    vector_oznak.Add(resnoa);
                }
                vector_oznak_all.Add(resnoa);

            }
            List<List<double>> X_minus_x = new List<List<double>>();
            List<double> vector_serednih = new List<double>();
            for (int i = 0; i < vector_oznak.Count; i++)
            {
                vector_serednih.Add(Program.vect_seredne(vector_oznak[i]));
                List<double> list_in_cycl = new List<double>();
                for (int t = 0; t < vector_oznak[i].Count; t++)
                {
                    list_in_cycl.Add(vector_oznak[i][t]- vector_serednih[i]);
                }
                X_minus_x.Add(list_in_cycl);
            }
            List<double> Y_minus_y = new List<double>();
            double Y_seredn = Program.vect_seredne(dependet_vector);
            for (int i = 0; i < dependet_vector.Count; i++)
            {
                Y_minus_y.Add(dependet_vector[i]-Y_seredn);
            }
            double[,] koef_A_1 = Program.Inverse_matrix(Program.multiplication_matrix_on_matrix(Program.List_to_matrix(X_minus_x),Program.Transponovana_matrix(Program.List_to_matrix(X_minus_x))));
            double[,] koef_A_2 = Program.multiplication_matrix_on_matrix(Program.List_to_matrix(X_minus_x), Program.Transponovana_matrix(Program.List_to_matrix(Y_minus_y)));
            double[,] koef_A_result = Program.multiplication_matrix_on_matrix(koef_A_1, koef_A_2);
            List<double> a_koef = new List<double>();
            for (int i = 0; i < koef_A_result.GetLength(0); i++)
            {
                a_koef.Add(koef_A_result[i, 0]);
            }
            double a_koef_zero = 0;
            for (int i = 0; i < a_koef.Count; i++)
            {
                a_koef_zero += a_koef[i] * vector_serednih[i];
            }
            a_koef.Insert(0, Y_seredn - a_koef_zero); ;//знайшли всі коефіцієнти
            List<double> list_kk_matr = Program.R_matrix_kk_determinate(vector_oznak_all);
            double determinater_R_matrix = Program.Determinate(Program.R_matrix(vector_oznak_all), vector_oznak_all.Count);
            double koef_R_determinacii = 1.0 - (determinater_R_matrix / list_kk_matr[dependet_vector_number]);///R^2
            ////дагностична діаграма
            double[,] matr_A_for_diagn_diagram = Program.multiplication_matrix_on_matrix(
                Program.Inverse_matrix(Program.multiplication_matrix_on_matrix(Program.List_to_matrix(vector_oznak), Program.Transponovana_matrix(Program.List_to_matrix(vector_oznak)))),
                Program.multiplication_matrix_on_matrix(Program.List_to_matrix(vector_oznak), Program.Transponovana_matrix(Program.List_to_matrix(dependet_vector))));
            List<double> a_koef_dign = new List<double>();
            for (int i = 0; i < matr_A_for_diagn_diagram.GetLength(0); i++)
            {
                a_koef_dign.Add(matr_A_for_diagn_diagram[i, 0]);
            }
            double a_koef_zero_dign = 0;
            for (int i = 0; i < a_koef_dign.Count; i++)
            {
                a_koef_zero_dign += a_koef_dign[i] * vector_serednih[i];
            }
            a_koef_dign.Insert(0,Y_seredn - a_koef_zero_dign);//знайшли всі коефіцієнти діагностичної матриці
            chart4.Series[0].Points.Clear();
            chart4.Series[0].IsVisibleInLegend = false;
            chart4.ChartAreas[0].AxisY.LabelStyle.Format = "0.0000";
            chart4.ChartAreas[0].AxisX.LabelStyle.Format = "0.0000";
            chart4.ChartAreas[0].AxisX.Title = "y";
            chart4.ChartAreas[0].AxisY.Title = "E";
            double S_rest_eps = 0;
            for (int i = 0; i < dependet_vector.Count; i++)
            {
                double Eps_l = 0;
                double suma_in_cycle = 0;
                for (int t = 1; t < a_koef_dign.Count; t++)
                {
                    suma_in_cycle += a_koef_dign[t] * vector_oznak[t - 1][i];
                }
                double suma_in_cycle2 = 0;
                for (int t = 1; t < a_koef_dign.Count; t++)
                {
                    suma_in_cycle2 += a_koef[t] * vector_oznak[t - 1][i];
                }
                Eps_l = dependet_vector[i] -suma_in_cycle;
                S_rest_eps += Math.Pow((dependet_vector[i] - a_koef[0]-suma_in_cycle2), 2);
                chart4.Series[0].Points.AddXY(dependet_vector[i],Eps_l);
            }//діагностична матриця
            ////
            S_rest_eps = S_rest_eps / (Convert.ToDouble(dependet_vector.Count - vector_oznak_all.Count));
            ///матриця C
            double[,] C_koef_matrix = Program.Inverse_matrix(Program.multiplication_matrix_on_matrix(Program.List_to_matrix(vector_oznak), Program.Transponovana_matrix(Program.List_to_matrix(vector_oznak))));

            double T_statist = Program.Kvantil_Studenta(1.0 - Double.Parse(textBox14.Text)/2.0, Convert.ToDouble(dependet_vector.Count - vector_oznak.Count));
            ///довірчі інтеравали для коефіцієнтів регерсії
            for (int i = 0; i < a_koef.Count; i++)
            {
                if (i==0)
                {
                    dataGridView15.Rows.Add($"a{i}","" , Math.Round(a_koef[i], 4),"");
                }
                else
                {
                    double T_sta_for_a= Program.Kvantil_Studenta(Double.Parse(textBox14.Text) / 2.0, Convert.ToDouble(dependet_vector.Count - vector_oznak.Count));
                    double t_a = a_koef[i] / (Math.Sqrt(S_rest_eps)* Math.Sqrt(C_koef_matrix[i - 1, i - 1]));
                   
                    double Left_bound = a_koef[i] - T_statist * Math.Sqrt(S_rest_eps) * Math.Sqrt(C_koef_matrix[i - 1, i - 1]);
                    double Right_bound = a_koef[i] + T_statist * Math.Sqrt(S_rest_eps) * Math.Sqrt(C_koef_matrix[i - 1, i - 1]);
                    double stand_a = (a_koef[i] * Program.vect_dispersia(vector_oznak[i - 1]))/ Program.vect_dispersia(dependet_vector);
                    if (Math.Abs(t_a) <= T_sta_for_a)
                    {
                        dataGridView15.Rows.Add($"a{i}", Math.Round(Left_bound, 4), Math.Round(a_koef[i], 4), Math.Round(Right_bound, 4),"-", Math.Round(stand_a, 4));
                    }
                    else
                    {
                        dataGridView15.Rows.Add($"a{i}", Math.Round(Left_bound, 4), Math.Round(a_koef[i], 4), Math.Round(Right_bound, 4), "+", Math.Round(stand_a, 4));
                    }
                    //dataGridView15.Rows.Add($"a{i}", Math.Round(Left_bound, 4), Math.Round(a_koef[i], 4), Math.Round(Right_bound, 4));
                }
                
            }
            // MessageBox.Show(""+T_statist);
            double phi_pirs1 = Program.Kvantil_Pirsona((1.0-(1.0-Double.Parse(textBox14.Text)))/2.0,Convert.ToDouble(dependet_vector.Count - vector_oznak.Count));
            double phi_pirs2 = Program.Kvantil_Pirsona((1.0 + (1.0 - Double.Parse(textBox14.Text))) / 2.0, Convert.ToDouble(dependet_vector.Count - vector_oznak.Count));

            dataGridView15.Rows.Add($"Дисперія залишкова", Math.Round((S_rest_eps* Convert.ToDouble(dependet_vector.Count - vector_oznak.Count))/ phi_pirs2, 4), Math.Round(S_rest_eps, 4), Math.Round((S_rest_eps * Convert.ToDouble(dependet_vector.Count - vector_oznak.Count)) / phi_pirs1, 4), "", "");
            dataGridView15.Rows.Add($"Коеф. детермінації", "", Math.Round(koef_R_determinacii, 4), "", "", "");
            // F-test
            double f_stat = ((1.0 - koef_R_determinacii) / koef_R_determinacii) * ((Convert.ToDouble(dependet_vector.Count - vector_oznak.Count - 1)) / Convert.ToDouble(vector_oznak.Count));
            double f_test_stat = Program.Kvantil_Fishera(1.0 - Double.Parse(textBox14.Text), Convert.ToDouble(vector_oznak.Count), Convert.ToDouble(dependet_vector.Count - vector_oznak.Count - 1));
            if (f_stat>f_test_stat)
            {
                dataGridView15.Rows.Add($"Значущість регресійної моделі", "", Math.Round(f_stat, 4), "", "+", "");
            }
            else
            {
                dataGridView15.Rows.Add($"Значущість регресійної моделі", "", Math.Round(f_stat, 4), "", "-", "");
            }

            ///довірчі інтеравали для значення регресіїї
            dataGridView15.Rows.Add($"Довірчі інтервали для регресії", "", "", "", "", "");
            for (int i = 0; i < dependet_vector.Count; i++)
            {
                double[,] X_in_cycle = new double[vector_oznak.Count,1];
                double[,] A_in_cycle = new double[vector_oznak.Count, 1];
                double y_value = a_koef[0];
                string par_in_cycle = "(";
                for (int t = 0; t < vector_oznak.Count; t++)
                {
                    y_value += a_koef[t + 1] * vector_oznak[t][i];
                    A_in_cycle[t, 0] = a_koef[t + 1];
                    X_in_cycle[t, 0] = vector_oznak[t][i];
                    par_in_cycle += $"{Math.Round(vector_oznak[t][i], 4)};";

                }
                par_in_cycle += ")";
                double[,] AtX = Program.multiplication_matrix_on_matrix(Program.Transponovana_matrix(A_in_cycle), X_in_cycle);
                double[,] XtC = Program.multiplication_matrix_on_matrix(Program.Transponovana_matrix(X_in_cycle), C_koef_matrix);
                double[,] XtCX = Program.multiplication_matrix_on_matrix(XtC, X_in_cycle);
                double Left_bound = AtX[0, 0] - T_statist * Math.Sqrt(S_rest_eps) * Math.Sqrt(1.0 + XtCX[0, 0]);
                double Right_bound = AtX[0, 0] + T_statist * Math.Sqrt(S_rest_eps) * Math.Sqrt(1.0 + XtCX[0, 0]);
                dataGridView15.Rows.Add(par_in_cycle, Math.Round(Left_bound + a_koef[0],4),Math.Round(y_value,4), Math.Round(Right_bound+ a_koef[0], 4));
                //MessageBox.Show($"{koef_A_result[0, 0]}/n {koef_A_result[1, 0]}");
            }
            
        }

        private void button18_Click(object sender, EventArgs e)
        {
            List<int> nomer_oznak = new List<int>();
            string promptValue = Prompt.ShowDialog("Значення", "Залежний вектор");
            int dependet_vector_number = Convert.ToInt16(promptValue) - 1;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            }
            if (dependet_vector_number > nomer_oznak.Count)
            {
                MessageBox.Show("Невірний формат");
                return;
            }
            List<List<double>> vector_oznak = new List<List<double>>();//X
            List<List<double>> vector_oznak_all = new List<List<double>>();
            List<double> dependet_vector = new List<double>();
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                List<double> resnoa = new List<double>();
                for (int t = 0; t < Universe.Data_Vectors[nomer_oznak[i]].Count; t++)
                {
                    resnoa.Add(Universe.Data_Vectors[nomer_oznak[i]][t]);
                }
                if (i == dependet_vector_number)
                {
                    for (int d = 0; d < resnoa.Count; d++)
                    {
                        dependet_vector.Add(resnoa[d]);
                    }
                }
                else
                {
                    vector_oznak.Add(resnoa);
                }
                vector_oznak_all.Add(resnoa);

            }
            if (vector_oznak_all.Count!=3)
            {
                MessageBox.Show("Вибірка повинна бути тривимірна");
                return;
            }
            double y_ser = Program.vect_seredne(dependet_vector);
            double x1_ser = Program.vect_seredne(vector_oznak[0]);
            double x2_ser = Program.vect_seredne(vector_oznak[1]);
            double x1_kvad = 0;
            double x2_kvad = 0;
            double x1_x2 = 0;
            double x1_y = 0;
            double x2_y = 0;
            for (int i = 0; i < dependet_vector.Count; i++)
            {
                x1_kvad += Math.Pow(vector_oznak[0][i], 2);
                x2_kvad += Math.Pow(vector_oznak[1][i], 2);
                x1_x2 += (vector_oznak[1][i] * vector_oznak[0][i]);
                x1_y += vector_oznak[0][i] * dependet_vector[i];
                x2_y += vector_oznak[1][i] * dependet_vector[i];
            }
            x1_kvad /= Convert.ToDouble(dependet_vector.Count);
            x2_kvad /=Convert.ToDouble(dependet_vector.Count);
            x1_x2 /= Convert.ToDouble(dependet_vector.Count);
            x1_y /= Convert.ToDouble(dependet_vector.Count);
            x2_y /= Convert.ToDouble(dependet_vector.Count);
            double[,] det_zag = new double[3, 3]
            {
                {1.0,x1_ser,x2_ser },
                {x1_ser,x1_kvad,x1_x2 },
                { x2_ser,x1_x2,x2_kvad}
            };
            double[,] det1_zag = new double[3, 3]
            {
                {y_ser,x1_ser,x2_ser },
                {x1_y,x1_kvad,x1_x2 },
                { x2_y,x1_x2,x2_kvad}
            };

            double[,] det2_zag = new double[3, 3]
            {
                {1.0,y_ser,x2_ser },
                {x1_ser,x1_y,x1_x2 },
                { x2_ser,x2_y,x2_kvad}
            };

            double[,] det3_zag = new double[3, 3]
            {
                {1.0,x1_ser,y_ser },
                {x1_ser,x1_kvad,x1_y },
                { x2_ser,x1_x2,x2_y}
            };

            double deter_za = Program.Determinate(det_zag, 3);
            double deter_za1 = Program.Determinate(det1_zag, 3);
            double deter_za2 = Program.Determinate(det2_zag, 3);
            double deter_za3 = Program.Determinate(det3_zag, 3);
            double a_0 = deter_za1 / deter_za;
            double a_1 = deter_za2 / deter_za;
            double a_2 = deter_za3 / deter_za;
            MessageBox.Show($"a0 = {Math.Round(a_0,5)}\na1 = {Math.Round(a_1, 5)}\na2 = {Math.Round(a_2, 5)}");

            

        }

        private void button19_Click(object sender, EventArgs e)
        {
            List<int> nomer_oznak = new List<int>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            } 
            List<List<double>> vector_oznak = new List<List<double>>();//X
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                List<double> resnoa = new List<double>();
                for (int t = 0; t < Universe.Data_Vectors[nomer_oznak[i]].Count; t++)
                {
                    resnoa.Add(Universe.Data_Vectors[nomer_oznak[i]][t]);
                }
                vector_oznak.Add(resnoa);

            }
            ///центрування даних
            ///
            List<List<double>> vector_oznak_centr_mean = new List<List<double>>();
            for (int i = 0; i < vector_oznak.Count; i++)
            {
                List<double> resnoa = new List<double>();
                double seredn = Program.vect_seredne(vector_oznak[i]);
                for (int t = 0; t < vector_oznak[i].Count; t++)
                {
                    resnoa.Add(vector_oznak[i][t]-seredn);
                }
                vector_oznak_centr_mean.Add(resnoa);
            }
            ///ДК-матриця
            double[,] DC_matirx = Program.multiplication_matrix_on_number(Program.multiplication_matrix_on_matrix(Program.List_to_matrix(vector_oznak_centr_mean), Program.Transponovana_matrix(Program.List_to_matrix(vector_oznak_centr_mean))), 1.0/Convert.ToDouble(vector_oznak[0].Count));
            List<double> vlasni_chisla = Program.EigenValues(DC_matirx);
            List<double[]> vlasni_vectory = Program.EigenVectors(DC_matirx, true);

            ///побудова таблиці
            dataGridView16.Rows.Clear();
            dataGridView16.Columns.Clear();
            dataGridView16.AllowUserToAddRows = false;
            dataGridView16.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.AllCells;
            for (int i = 0; i < vlasni_chisla.Count+1; i++)
            {
                if (i==0)
                {
                    dataGridView16.Columns.Add($"x{i}", $"  ");
                }
                else
                {
                    dataGridView16.Columns.Add($"x{i}", $"x{i}");
                }
               
            }
            for (int i = 0; i < vlasni_chisla.Count; i++)
            {
                dataGridView16.Rows.Add($"x{i+1}");
                for (int t = 0; t < vlasni_vectory.Count; t++)
                {
                    dataGridView16.Rows[dataGridView16.Rows.Count - 1].Cells[t + 1].Value = Math.Round(vlasni_vectory[t][i],5);
                }
            }
            dataGridView16.Rows.Add("Власні числа");
            for (int i = 0; i < vlasni_chisla.Count; i++)
            {
                dataGridView16.Rows[dataGridView16.Rows.Count - 1].Cells[i + 1].Value = Math.Round(vlasni_chisla[i], 5);
            }

            dataGridView16.Rows.Add("% на напрям");
            for (int i = 0; i < vlasni_chisla.Count; i++)
            {
                dataGridView16.Rows[dataGridView16.Rows.Count - 1].Cells[i + 1].Value = Math.Round((vlasni_chisla[i]/Convert.ToDouble(vlasni_chisla.Sum()))*100.0, 5);
            }
            dataGridView16.Rows.Add("%Накопичений");
            for (int i = 0; i < vlasni_chisla.Count; i++)
            {
                double suma_nakopich = 0;
                int t_plus_i = i + 1;
                for (int t = 0; t < t_plus_i; t++)
                {
                    suma_nakopich+= (vlasni_chisla[t] / Convert.ToDouble(vlasni_chisla.Sum()))*100.0;
                }
                dataGridView16.Rows[dataGridView16.Rows.Count - 1].Cells[i + 1].Value = Math.Round(suma_nakopich, 5);
            }
            ///кінець побудови таблиці
            ///метод тростини
            ///
            chart5.Series[0].Points.Clear();
            //chart5.Titles[0].Text="Метод зламаної тростини";
            chart5.ChartAreas[0].AxisX.LabelStyle.Format = "0.000";
            chart5.ChartAreas[0].AxisX.Maximum = vlasni_chisla.Count-1;
            chart5.ChartAreas[0].AxisX.Minimum = 0;
           // chart5.ChartAreas[0].AxisY.Maximum = vlasni_chisla[vlasni_chisla.Count-1];
            chart5.ChartAreas[0].AxisX.Title = "Компоненти";
            chart5.ChartAreas[0].AxisY.Title = "Власні числа";
            for (int i = 0; i < vlasni_chisla.Count; i++)
            {
                chart5.Series[0].Points.AddXY(i, vlasni_chisla[i]);
            }

            /// кінець побудови тростини


            double[,] Matrix_A = new double[DC_matirx.GetLength(0), DC_matirx.GetLength(1)];
            for (int i = 0; i < DC_matirx.GetLength(0); i++)
            {
                for (int t = 0; t < DC_matirx.GetLength(1); t++)
                {
                    Matrix_A[t, i] = vlasni_vectory[i][t];
                }
            }
            List<List<double>> vector_oznak_ortogonalni = new List<List<double>>();
            for (int i = 0; i < vector_oznak.Count; i++)
            {
                List<double> resnoa = new List<double>();
                for (int t = 0; t < vector_oznak[0].Count; t++)
                {
                    double suma_in_cycle = 0;
                    for (int tn = 0; tn < vector_oznak.Count; tn++)
                    {
                        suma_in_cycle += Matrix_A[tn, i] * vector_oznak_centr_mean[tn][t];
                    }
                    resnoa.Add(suma_in_cycle);
                }
                vector_oznak_ortogonalni.Add(resnoa);
            }

            for (int i = 0; i < vector_oznak_ortogonalni.Count; i++)
            {
                Universe.Data_Vectors.Add(vector_oznak_ortogonalni[i]);
                listBox1.Items.Add($"{listBox1.Items[nomer_oznak[i]].ToString()} МГК");
            }


            ////повернення даних
            int pershi = int.Parse(textBox18.Text);
            
            if (radioButton3.Checked == true)
            {
                pershi = nomer_oznak.Count;
            }
            if (radioButton4.Checked == true)
            {
                pershi = int.Parse(textBox18.Text);
            }
            List<List<double>> vector_oznak_ortogonalni_reverse = new List<List<double>>();
            
            for (int i = 0; i < vector_oznak_ortogonalni.Count; i++)
            {
                List<double> resnoa = new List<double>();
                for (int t = 0; t < vector_oznak_ortogonalni[0].Count; t++)
                {
                    double suma_in_cycle = 0;
                    for (int tn = 0; tn < pershi; tn++)
                    {
                        suma_in_cycle += Matrix_A[i, tn] * vector_oznak_ortogonalni[tn][t];
                    }
                    resnoa.Add(suma_in_cycle);
                }
                vector_oznak_ortogonalni_reverse.Add(resnoa);
            }

            for (int i = 0; i < vector_oznak_ortogonalni.Count; i++)
            {
                Universe.Data_Vectors.Add(vector_oznak_ortogonalni_reverse[i]);
                listBox1.Items.Add($"{listBox1.Items[nomer_oznak[i]].ToString()} МГК reverse");
            }


            //List<double> gen_matrix = Program.koef_3D_area(vector_oznak_ortogonalni_reverse);//рівняння площини
            //string equation = "";
            //for (int i = 0; i < gen_matrix.Count; i++)
            //{
             //   equation += "" + Math.Round(gen_matrix[i], 4)+"x"+(i+1)+" ";
            //}
            //MessageBox.Show(equation);

        }

        private void button20_Click(object sender, EventArgs e)
        {
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Значення", "Залежний вектор");
            int dependet_vector_number = Convert.ToInt16(promptValue) - 1;
            List<int> nomer_oznak = new List<int>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            }
            List<List<double>> vector_oznak = new List<List<double>>();//X
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                List<double> resnoa = new List<double>();
                for (int t = 0; t < Universe.Data_Vectors[nomer_oznak[i]].Count; t++)
                {
                    resnoa.Add(Universe.Data_Vectors[nomer_oznak[i]][t]);
                }
                vector_oznak.Add(resnoa);

            }
            List<double> gen_matrix = Program.koef_3D_area(vector_oznak);//рівняння площини
            string equation = "y = ";
            double dependet_zminaa = gen_matrix[dependet_vector_number];
            int numerix_of_zmin = 1;
            for (int i = 0; i < gen_matrix.Count; i++)
            {   if (i == dependet_vector_number)
                {
                    continue;
                }
                else if (i== gen_matrix.Count - 1)
                {
                    equation += "(" + Math.Round(gen_matrix[i] / dependet_zminaa, 4) + ") ";
                }
                else
                {
                    equation += "(" + Math.Round(gen_matrix[i] / dependet_zminaa, 4) + ")x" + (numerix_of_zmin) + "+ ";
                    numerix_of_zmin++;
                }
               
            }
            CustomMessageBoxForm customMessageBox = new CustomMessageBoxForm();
            customMessageBox.Title = "Лінійне різноманіття";
            customMessageBox.Message = equation;
            customMessageBox.ShowDialog();
            
        }

        private void button21_Click(object sender, EventArgs e)
        {
            List<int> nomer_oznak = new List<int>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            }
            List<List<double>> vector_oznak12 = new List<List<double>>();
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                vector_oznak12.Add(Universe.Data_Vectors[nomer_oznak[i]]);
            }
            double[,] R_matrix = Program.R_matrix(vector_oznak12);
            List<double[]> vlasni_vectory = Program.EigenVectors(R_matrix, true);
            List<double> vlasni_chisla = Program.EigenValues(R_matrix);
            int w_count = 0;
            for (int i = 0; i <vlasni_chisla.Count; i++)
            {
                if (vlasni_chisla[i]>=1)
                {
                    w_count++;
                }
            }
            double[,] Matrix_A = new double[R_matrix.GetLength(0), R_matrix.GetLength(1)];
             
            for (int i = 0; i < R_matrix.GetLength(0); i++)
            {
                for (int t = 0; t < R_matrix.GetLength(1); t++)
                {
                    Matrix_A[t, i] = vlasni_vectory[i][t];
                }
            }
            
            //метод максимальних коерляцій
            List<double> h_2 = new List<double>();
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                List<double> h = new List<double>();
                for (int t = 0; t < nomer_oznak.Count; t++)
                {
                    if (i!=t)
                    {
                        double promin = Program.ocinka_parnoho_koef_korelacii(vector_oznak12[i], vector_oznak12[t]);
                        h.Add(Math.Abs(promin));
                    }
                    
                }
                h_2.Add(h.Max());
                
            }
            //метод тріад
            List<double> h_2_triad = new List<double>();
            for (int h1 = 0; h1 < nomer_oznak.Count; h1++)
            {
                int[] index_i_j = new int[nomer_oznak.Count-1];
                List<double> h = new List<double>();
                for (int i = 0; i < nomer_oznak.Count; i++)
                {
                    if (i != h1)
                    {
                        double promin = R_matrix[h1,i];
                        h.Add(promin);                     
                    }
                }
                double rki = h.Max();
                int indexi=h.IndexOf(rki);
                h.RemoveAt(indexi);
                int indexj = 0;
                double rkj = h.Max();
                for (int i = 0; i < nomer_oznak.Count; i++)
                {
                    if (R_matrix[h1, i] == rki)
                    {
                        indexi = i;
                        break;
                    }
                }
                for (int i = 0; i < nomer_oznak.Count; i++)
                {
                    if (R_matrix[h1, i] == rkj)
                    {
                        indexj = i;
                        break;
                    }
                }
                double result = Math.Abs((rki * rkj) / R_matrix[indexi,indexj]);
                h_2_triad.Add(result);
            }
            //метод усереднення
            List<double> h_2_userednenya = new List<double>();
            for (int h1 = 0; h1 < nomer_oznak.Count; h1++)
            {   
                List<double> h = new List<double>();
                double suma_h2 = 0;
                for (int t = 0; t < nomer_oznak.Count; t++)
                {
                    if (h1 != t)
                    {
                        double promin = Program.ocinka_parnoho_koef_korelacii(vector_oznak12[h1], vector_oznak12[t]);
                        suma_h2 +=Math.Abs(promin);
                    }
                }
                suma_h2 = suma_h2 / Convert.ToDouble(nomer_oznak.Count - 1);
                h_2_userednenya.Add(suma_h2);
                
            }
            //центроїдний метод
            List<double> h_2_centroid = new List<double>();
            for (int h1 = 0; h1 < nomer_oznak.Count; h1++)
            {
                List<double> h = new List<double>();
                double suma_h2 = 0;
                for (int t = 0; t < nomer_oznak.Count; t++)
                {
                    double promin = Program.ocinka_parnoho_koef_korelacii(vector_oznak12[h1], vector_oznak12[t]);
                    suma_h2 += Math.Abs(promin);
                
                }
                suma_h2 = Math.Pow(suma_h2,2.0);
                double denominator = 0;
                for (int i = 0; i < nomer_oznak.Count; i++)
                {
                    for (int j = 0; j < nomer_oznak.Count; j++)
                    {                  
                       double promin = Program.ocinka_parnoho_koef_korelacii(vector_oznak12[i], vector_oznak12[j]);
                       denominator += Math.Abs(promin);
                    }
                }
                h_2_centroid.Add(suma_h2/denominator);
            }
            //авероїдний метод
            List<double> h_2_averoid = new List<double>();
            for (int h1 = 0; h1 < nomer_oznak.Count; h1++)
            {
                List<double> h = new List<double>();
                double suma_h2 = 0;
                for (int t = 0; t < nomer_oznak.Count; t++)
                {
                    if (h1 != t)
                    {
                        double promin = Program.ocinka_parnoho_koef_korelacii(vector_oznak12[h1], vector_oznak12[t]);
                        suma_h2 += Math.Abs(promin);
                    }
                }
                suma_h2 = Math.Pow(suma_h2, 2.0);
                double denominator = 0;
                for (int i = 0; i < nomer_oznak.Count; i++)
                {
                    for (int j = 0; j < nomer_oznak.Count; j++)
                    {
                        if (i!=j)
                        {
                            double promin = Program.ocinka_parnoho_koef_korelacii(vector_oznak12[h1], vector_oznak12[j]);
                            denominator += Math.Abs(promin);

                        }
                    }
                }
                double in_list = (Convert.ToDouble(nomer_oznak.Count) / Convert.ToDouble(nomer_oznak.Count - 1)) * (suma_h2 / denominator);
                h_2_averoid.Add(in_list);

            }
            //метод що грунтується по мгк
            List<double> h_2_MGK = new List<double>();
            for (int h1 = 0; h1 < nomer_oznak.Count; h1++)
            {
                List<double> h = new List<double>();
                double suma_h2 = 0;
                for (int t = 0; t < w_count; t++)
                {
                        suma_h2 += Math.Pow(Matrix_A[h1,t],2.0);
                }
                
                h_2_MGK.Add(suma_h2);

            }
            List<List<double>> all_h = new List<List<double>>();
            all_h.Add(h_2);
            all_h.Add(h_2_triad);
            all_h.Add(h_2_userednenya);
            all_h.Add(h_2_centroid);
            all_h.Add(h_2_averoid);
            all_h.Add(h_2_MGK);
            //шукаєм мінімальну ефку
            List<double> r_zal_list = new List<double>();

        H_greater_1:

            for (int i = 0; i < all_h.Count; i++)
            {
                for (int r = 0; r < nomer_oznak.Count; r++)
                {
                    if (all_h[i][r] > 1)
                    {
                        all_h.RemoveAt(i);
                        r_zal_list.Clear();
                        goto H_greater_1;
                    }
                }
                double[,] Rh_matrix1 = new double[R_matrix.GetLength(0), R_matrix.GetLength(1)];
                for (int t = 0; t < R_matrix.GetLength(0); t++)
                {
                    for (int j = 0; j < R_matrix.GetLength(1); j++)
                    {
                        Rh_matrix1[t, j] = R_matrix[t, j];
                    }
                }
                for (int r = 0; r < nomer_oznak.Count; r++)
                {
                    Rh_matrix1[r, r] = all_h[i][r];
                }
                double[,] Matrix_A1 = new double[R_matrix.GetLength(0), R_matrix.GetLength(1)];
                List<double[]> vlasni_vectory1 = Program.EigenVectors(Rh_matrix1, true);
                for (int r = 0; r < R_matrix.GetLength(0); r++)
                {
                    for (int t = 0; t < R_matrix.GetLength(1); t++)
                    {
                        Matrix_A1[t, r] = vlasni_vectory1[r][t];
                    }
                }
                double[,] R_zal1 = Program.subtraction_matrix(Rh_matrix1, Program.multiplication_matrix_on_matrix(Matrix_A1, Program.Transponovana_matrix(Matrix_A1)));
                double f1 = 0;
                for (int v = 0; v < nomer_oznak.Count; v++)
                {
                    for (int q = 0; q < nomer_oznak.Count; q++)
                    {
                        if (v != q)
                        {
                            f1 += Math.Pow(R_zal1[v, q], 2.0);
                        }
                    }
                }
                r_zal_list.Add(f1);
                
                
            }
             int index_min_r=r_zal_list.IndexOf(r_zal_list.Min());
            //перший опорний план
            double[,] Rh_matrix = new double[R_matrix.GetLength(0), R_matrix.GetLength(1)];
            for (int t = 0; t < R_matrix.GetLength(0); t++)
            {
                for (int j = 0; j < R_matrix.GetLength(1); j++)
                {
                    Rh_matrix[t, j] = R_matrix[t, j];
                }
            }
            for (int r = 0; r < nomer_oznak.Count; r++)
            {
                Rh_matrix[r, r] = all_h[index_min_r][r];
            }

            double[,] Matrix_A_Rh = new double[R_matrix.GetLength(0), R_matrix.GetLength(1)];
            List<double[]> vlasni_vectory_Rh = Program.EigenVectors(Rh_matrix, true);
            for (int r = 0; r < R_matrix.GetLength(0); r++)
            {
                for (int t = 0; t < R_matrix.GetLength(1); t++)
                {
                    Matrix_A_Rh[t, r] = vlasni_vectory_Rh[r][t];
                }
            }
            double[,] R_zal = Program.subtraction_matrix(Rh_matrix, Program.multiplication_matrix_on_matrix(Matrix_A_Rh, Program.Transponovana_matrix(Matrix_A_Rh)));
            double f_zal = 0;
            for (int v = 0; v < nomer_oznak.Count; v++)
            {
                for (int q = 0; q < nomer_oznak.Count; q++)
                {
                    if (v != q)
                    {
                        f_zal += Math.Pow(R_zal[v, q], 2.0);
                    }
                }
            }
            //iteration
            List<double> vlasni_shisla_Rh = Program.EigenValues(Rh_matrix);
            double suma_vlasnih_chisel = vlasni_shisla_Rh.Sum()/Convert.ToDouble(vlasni_shisla_Rh.Count);
            int w_count_rh = 0;
            for (int i = 0; i < vlasni_shisla_Rh.Count; i++)
            {
                if (vlasni_shisla_Rh[i]>=suma_vlasnih_chisel)
                {
                    w_count_rh++;
                }             
            }
            if (w_count_rh>w_count)
            {
                w_count = w_count_rh;
            }
            if (!string.IsNullOrEmpty(textBox20.Text))
            {
                w_count = Convert.ToInt16(textBox20.Text);
            }
            bool h1_ch = true;
            bool a_ae = false;
            bool f_chek = false;
            do
            {
                double[,] Rh_matrix1 = new double[R_matrix.GetLength(0), R_matrix.GetLength(1)];
                for (int t = 0; t < R_matrix.GetLength(0); t++)
                {
                    for (int j = 0; j < R_matrix.GetLength(1); j++)
                    {
                        Rh_matrix1[t, j] = R_matrix[t, j];
                    }
                }
                List<double> list_of_new_h = new List<double>();
                for (int r = 0; r < nomer_oznak.Count; r++)
                {
                    double new_h_k = 0;
                    for (int w = 0; w < w_count; w++)
                    {
                        new_h_k += Math.Pow(Matrix_A_Rh[r, w], 2.0);
                    }
                    list_of_new_h.Add(new_h_k);
                }
                for (int r = 0; r < nomer_oznak.Count; r++)
                {
                    Rh_matrix1[r, r] = list_of_new_h[r];
                }
                double[,] Matrix_A1 = new double[R_matrix.GetLength(0), R_matrix.GetLength(1)];
                List<double[]> vlasni_vectory1 = Program.EigenVectors(Rh_matrix1, true);
                for (int r = 0; r < R_matrix.GetLength(0); r++)
                {
                    for (int t = 0; t < R_matrix.GetLength(1); t++)
                    {
                        Matrix_A1[t, r] = vlasni_vectory1[r][t];
                    }
                }
                double[,] R_zal1 = Program.subtraction_matrix(Rh_matrix1, Program.multiplication_matrix_on_matrix(Matrix_A1, Program.Transponovana_matrix(Matrix_A1)));
                double f1 = 0;
                for (int v = 0; v < nomer_oznak.Count; v++)
                {
                    for (int q = 0; q < nomer_oznak.Count; q++)
                    {
                        if (v != q)
                        {
                            f1 += Math.Pow(R_zal1[v, q], 2.0);
                        }
                    }
                }

                //f(i)>f(i+1)
                f_chek = false;

                if (f1 < f_zal)
                {
                    f_chek = true;
                }
                //a-a>e
                a_ae = false;
                double suma_a_ai = 0;
                for (int v = 0; v < nomer_oznak.Count; v++)
                {
                    for (int q = 0; q < nomer_oznak.Count; q++)
                    {
                        suma_a_ai += Math.Pow(Matrix_A_Rh[v, q] - Matrix_A1[v, q], 2.0);
                    }
                }
                if (suma_a_ai > 0.1)
                {
                    a_ae = true;
                }
                //h<=1
                h1_ch = true;
                for (int r = 0; r < nomer_oznak.Count; r++)
                {
                    if (list_of_new_h[r] > 1)
                    {
                        h1_ch = false;
                        break;
                    }
                }
                if (h1_ch && a_ae && f_chek)
                {
                    for (int r = 0; r < R_matrix.GetLength(0); r++)
                    {
                        for (int t = 0; t < R_matrix.GetLength(1); t++)
                        {
                            f_zal = f1;
                            Rh_matrix[t, r] = Rh_matrix1[t, r];
                            Matrix_A_Rh[t, r] = Matrix_A1[t, r];
                        }
                    }
                }
                else
                {
                    break;
                }
            } while (h1_ch && a_ae && f_chek);


            double[,] R_zal12 = Program.multiplication_matrix_on_matrix(Matrix_A_Rh, Program.Transponovana_matrix(Matrix_A_Rh));
            panel3.Controls.Clear();
            DataGridView dt_grd = new DataGridView();

            dt_grd.Height = 500;
            dt_grd.Width = 700;
            dt_grd.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dt_grd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dt_grd.BackgroundColor = Color.AliceBlue;
            dt_grd.AllowUserToAddRows = false;
            for (int i = 0; i < w_count+1+2; i++)
            {
                dt_grd.Columns.Add("t", "" + i);
            }
            dt_grd.Rows.Add();
            for (int q = 0; q < w_count; q++)
            {
                dt_grd.Rows[dt_grd.Rows.Count - 1].Cells[q + 1].Value = "F"+(q+1);
            }
            dt_grd.Rows.Add();
            dt_grd.Rows[dt_grd.Rows.Count - 1].Cells[0].Value = "Матриця навантажень";
            dt_grd.ColumnHeadersVisible = false;
            dt_grd.RowHeadersVisible = false;
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                for (int q = 0; q < w_count; q++)
                {
                    dt_grd.Rows[dt_grd.Rows.Count - 1].Cells[q + 1].Value = Math.Round(Matrix_A_Rh[i,q],3);
                }
                dt_grd.Rows.Add();
            }
            //dt_grd.Rows[dt_grd.Rows.Count - 1].Cells[0].Value = "Загальності";
            dt_grd.Rows[0].Cells[w_count + 1].Value = "Загальності";
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                // dt_grd.Rows[dt_grd.Rows.Count - 1].Cells[i + 1].Value = Math.Round(Rh_matrix[i, i], 3);
                double sun_h = 0;
                for (int q = 0; q < w_count; q++)
                {
                    sun_h += Math.Pow(Matrix_A_Rh[i, q], 2);
                }
                ///dt_grd.Rows[dt_grd.Rows.Count - 1].Cells[i + 1].Value = Math.Round(sun_h, 3);
                dt_grd.Rows[i+1].Cells[w_count + 1].Value = Math.Round(sun_h, 3);
            }
            dt_grd.Rows.Add();
            //dt_grd.Rows[dt_grd.Rows.Count - 1].Cells[nomer_oznak.Count].Value = "Характерності";
            
            dt_grd.Rows[0].Cells[w_count+2].Value = "Характерності";
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                double sun_h = 0;
                for (int q = 0; q < w_count; q++)
                {
                    sun_h += Math.Pow(Matrix_A_Rh[i, q], 2);
                }
                //dt_grd.Rows[dt_grd.Rows.Count - 1].Cells[i + 1].Value = Math.Round(1.0-sun_h, 3);
                dt_grd.Rows[i+1].Cells[w_count + 2].Value = Math.Round(1.0 - sun_h, 3);
            }
           
            List<double> vlasni_shisla_Rh1 = Program.EigenValues(Rh_matrix);
            dt_grd.Rows[dt_grd.Rows.Count - 1].Cells[0].Value = "% на напрям";
            for (int i = 0; i < w_count; i++)
            {
                double sumaV = 0;
                for (int t = 0; t < nomer_oznak.Count; t++)
                {
                    sumaV +=Math.Abs(vlasni_shisla_Rh1[t]);
                }

                dt_grd.Rows[dt_grd.Rows.Count - 1].Cells[i + 1].Value = Math.Round(Math.Abs(vlasni_shisla_Rh1[i]) / sumaV, 3);
            }
            dt_grd.Rows.Add();
            dt_grd.Rows[dt_grd.Rows.Count - 1].Cells[0].Value = "% накопичений";
            double nakopicheniy =0;
            for (int i = 0; i < w_count; i++)
            {
                double sumaV = 0;
                for (int t = 0; t < nomer_oznak.Count; t++)
                {
                    sumaV += Math.Abs(vlasni_shisla_Rh1[t]);
                }
                if (i==0)
                {
                    nakopicheniy += Math.Abs(vlasni_shisla_Rh1[i]) / sumaV;
                }
                else
                {
                    nakopicheniy += Math.Abs(vlasni_shisla_Rh1[i]) / sumaV;
                }
                dt_grd.Rows[dt_grd.Rows.Count - 1].Cells[i + 1].Value = Math.Round(nakopicheniy, 3);
            }
            dt_grd.Location = new Point(0, 0);
            panel3.Controls.Add(dt_grd);
        }

        private void label19_Click(object sender, EventArgs e)
        {
            
        }

        private void button22_Click(object sender, EventArgs e)
        {
            panel4.Controls.Clear();
            
            //panel4.Controls.Add(chart_in_cycle);
            List<int> nomer_oznak = new List<int>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i) == true)
                {
                    nomer_oznak.Add(i);
                }
            }
            List<List<double>> vector_oznak = new List<List<double>>();//X
            for (int i = 0; i < nomer_oznak.Count; i++)
            {
                List<double> resnoa = new List<double>();
                for (int t = 0; t < Universe.Data_Vectors[nomer_oznak[i]].Count; t++)
                {
                    resnoa.Add(Universe.Data_Vectors[nomer_oznak[i]][t]);
                }
                vector_oznak.Add(resnoa);

            }
            TimeSeries TS;
            if (vector_oznak.Count == 2)
            {
                TS = new TimeSeries(vector_oznak[0], vector_oznak[1]);
            }
            else
            {
                TS = new TimeSeries(vector_oznak[0]);
            }
            double kw_alp = Convert.ToDouble(textBox21.Text);
            TS.MainForm = this;

            
            Chart chart_ser=TS.Get_Chart();
            panel4.Controls.Add(chart_ser);
            DataGridView dt_grd = TS.Get_table(kw_alp);
            DataGridView dt_grd_2 = TS.Get_table_eigen();
            //dt_grd.Dock = DockStyle.Fill;
            dt_grd.Width = panel5.Width;
            dt_grd.Height = panel5.Height;
            dt_grd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dt_grd_2.Width = panel6.Width;
            dt_grd_2.Height = panel6.Height;
            dt_grd_2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel5.Controls.Add(dt_grd);
            panel6.Controls.Add(dt_grd_2);
        }

        public ListBox GetListBox1()
        {
            return listBox1;
        }
    }
    public class CustomMessageBoxForm : Form
    {
        private Label messageLabel;
        private Button okButton;

        public string Message
        {
            get { return messageLabel.Text; }
            set
            {
                messageLabel.Text = value;
               
                messageLabel.Font = new Font("Arial", 12, FontStyle.Regular); 
            }
        }

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public CustomMessageBoxForm()
        {
            this.Text = "Custom Message Box";
            this.Size = new Size(500, 170);
            this.StartPosition = FormStartPosition.CenterScreen;

            messageLabel = new Label
            {
                Location = new Point(20, 20),
                Size = new Size(350, 60),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };

            okButton = new Button
            {
                Text = "OK",
                Size = new Size(150, 30),
                Location = new Point(110, 90),
                DialogResult = DialogResult.OK
            };

            okButton.Click += (sender, e) => this.Close();

            this.Controls.Add(messageLabel);
            this.Controls.Add(okButton);
        }
    }
    public static class Extansions
    {
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences) =>
         sequences.Aggregate(
        Enumerable.Empty<T>().AsSingleton(),
        (accumulator, sequence) => accumulator.SelectMany(
            accseq => sequence,
            (accseq, item) => accseq.Append(item)));
        public static IEnumerable<T> AsSingleton<T>(this T item) => new[] { item };
        public static IEnumerable<IEnumerable<T>>  GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        //{1,2} {1,3} {1,4} {2,1} {2,3} {2,4} {3,1} {3,2} {3,4} {4,1} {4,2} {4,3}
        public static IEnumerable<IEnumerable<T>> GetKCombs<T>(IEnumerable<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetKCombs(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        //{1,2} {1,3} {1,4} {2,3} {2,4} {3,4}
        public static IEnumerable<IEnumerable<T>>GetPermutationsWithRept<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutationsWithRept(list, length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        //{1,1} {1,2} {1,3} {1,4} {2,1} {2,2} {2,3} {2,4} {3,1} {3,2} {3,3} {3,4} {4,1} {4,2} {4,3} {4,4}
    }
}

