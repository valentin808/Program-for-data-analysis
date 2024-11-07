using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Accord.Statistics.Testing;
using Accord.Statistics.Distributions.Univariate;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using Accord.Math;

namespace statlab2
{
    internal  class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static public double ransom(List<double> dwew, double rt)
        {
            double num=0;
            int t = 1;
            for (int i = 0; i < dwew.Count; i++)
            {
                if (dwew[i]==rt)
                {
                    t++;
                }
            }
            num = t * 1.0;
            return num;
        }
        static public string[] strochka(string a)
        {
            string str = a;
            if (str == null)
            {
                return null;
            }
            string spliter = "";
            bool vflag = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]) || str[i] == '.' || str[i] == '-'|| str[i] == ','||str[i] == 'e'|| str[i] == '+' || str[i] == 'E')
                {
                    vflag = true;
                    spliter += str[i];
                }
                else
                {
                    if (vflag)
                    {
                        spliter += ' ';
                        vflag = false;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            string[] arr = spliter.Split(' ');
            
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = koma(arr[i]);
            }
            return arr;
        }
        static public string koma(string t )
        {
            string num = t;
            char[] crs=num.ToCharArray();
            for (int i = 0; i < t.Length; i++)
            {
                if (crs[i]=='.')
                {
                    crs[i] = ',';
                }
            }
            num=new string(crs);
            return num;
        }
        static public double kolmogorov_zgody(List<double> t)
        {
            KolmogorovSmirnovTest ksTest = new KolmogorovSmirnovTest(t.ToArray(), new NormalDistribution());
            // Проведення тесту
            double pValue = ksTest.PValue;
            return pValue;
        }
        static public string koma_inverse(string t)
        {
            string num = t;
            char[] crs = num.ToCharArray();
            for (int i = 0; i < t.Length; i++)
            {
                if (crs[i] == ',')
                {
                    crs[i] = '.';
                }
            }
            num = new string(crs);
            return num;
        }
        static public double LastFreeValue = double.NaN;
        static public double Norm_distribution(Random RND,double m=0, double sigma=1.0) 
        {
            if (double.IsNaN(LastFreeValue))
            {
                double x = 0;
                double y = 0;
                double R = 2;

                while (R > 1)
                {
                    x = RND.NextDouble() * 2 - 1;
                    y = RND.NextDouble() * 2 - 1;
                    R = x * x + y * y;
                }
                double t = Math.Sqrt(-2 * Math.Log(R) / R);
                Program.LastFreeValue = y * t;
                return x * t*sigma+m;
            }
            else
            {
                double Z1 = Program.LastFreeValue;
                Program.LastFreeValue = double.NaN;
                return Z1*sigma+m;
            }
            

            
        
        }
        static public double Exp_distribution(Random rnd,double lamda)
        {
            double l = lamda;
            double alpha=rnd.NextDouble();
            double vid= Math.Log(1.0 / (1.0 - alpha)) / l;
            return vid;

        }
        static public double Ravn_distribution(Random rnd,double a,double b) 
        {
            double a1 = a;
            double b1 = b;
            double alpha = rnd.NextDouble();
            double restik = (b1 - a1) * alpha + a1;
            return restik;
        }
        static public double Veib_distribution(Random rnd, double a, double b)
        {
            double alpha = a;
            double beta = b;
            double alp = rnd.NextDouble();
            double restik = Math.Pow((alpha * Math.Log(1.0 / (1.0 - alp))), 1.0 / beta);
            return restik;
        }

        static public double vect_seredne(List<double> vect_ser)
        {
            double seredne = 0;
            double chiselnyk = 0;
            for (int i = 0; i < vect_ser.Count; i++)
            {
                chiselnyk+=vect_ser[i];
            }
            seredne=chiselnyk/Convert.ToDouble(vect_ser.Count);
            return seredne;
        }//середнє
        static public double vect_seredno_kvadratichnyh(List<double> vect_ser)
        {
            double seredne = vect_seredne(vect_ser);
            double suma= 0;
            double dispersia = 0;
            for (int i = 0; i < vect_ser.Count; i++)
            {
                suma+= Math.Pow(vect_ser[i]-seredne,2.0);
            }
            dispersia = suma / Convert.ToDouble(vect_ser.Count-1);
            double sigma = Math.Sqrt(dispersia);
            return sigma;
        }//середньоквадратичне
        static public double vect_dispersia(List<double> vect_ser)
        {
            double seredne = vect_seredne(vect_ser);
            double suma = 0;
            double dispersia = 0;
            for (int i = 0; i < vect_ser.Count; i++)
            {
                suma += Math.Pow(vect_ser[i] - seredne, 2.0);
            }
            dispersia = suma / Convert.ToDouble(vect_ser.Count - 1);
            
            return dispersia;
        }//дисперсія
        static public double ocinka_parnoho_koef_korelacii(List<double> vect_ser1, List<double> vect_ser2)
        {
            double x_seredne = vect_seredne(vect_ser1);
            double y_seredne = vect_seredne(vect_ser2);
            double x_sigma = vect_seredno_kvadratichnyh(vect_ser1);
            double y_sigma = vect_seredno_kvadratichnyh(vect_ser2);
            double xy_seredne = 0;
            for (int i = 0; i < vect_ser1.Count; i++)
            {
                xy_seredne += vect_ser1[i] * vect_ser2[i];
            }
            xy_seredne = xy_seredne / Convert.ToDouble(vect_ser2.Count);
            double first_mn = (xy_seredne - (x_seredne * y_seredne)) / (x_sigma * y_sigma);
            double second_mn = Convert.ToDouble(vect_ser2.Count) / Convert.ToDouble(vect_ser2.Count-1);
            double koef_parn_korel = first_mn * second_mn;
            return koef_parn_korel;
        }
        static public double chastkoviy_ocinka(List<List<double>> vect_oznak,List<int> main_koef,List<int>res_koef)
        {
            ///якщо 3 вимірний
            
            double seredne = 0;
            double znamenyk = Math.Sqrt((1.0 - Math.Pow(ocinka_parnoho_koef_korelacii(vect_oznak[main_koef[0]], vect_oznak[res_koef[0]]),2))*(1.0 - Math.Pow(ocinka_parnoho_koef_korelacii(vect_oznak[main_koef[1]], vect_oznak[res_koef[0]]), 2)));
            double chiselnyk = ocinka_parnoho_koef_korelacii(vect_oznak[main_koef[0]], vect_oznak[main_koef[1]]) - ocinka_parnoho_koef_korelacii(vect_oznak[main_koef[0]], vect_oznak[res_koef[0]]) * ocinka_parnoho_koef_korelacii(vect_oznak[main_koef[1]], vect_oznak[res_koef[0]]);
            seredne = chiselnyk / znamenyk;
            return seredne;
        }//середнє

        static public double rekursia_chastkoviy_ocinka(List<List<double>> vect_oznak, List<int> main_koef, List<int> res_koef)
        {
            ///якщо 3 вимірний
            if (res_koef.Count==1)
            {
                return chastkoviy_ocinka(vect_oznak, main_koef, res_koef);
            }
            List<int> c_rest = new List<int>();
            List<int> i_d = new List<int>();
            i_d.Add(main_koef[0]);
            i_d.Add(res_koef.Last());
            List<int> j_d = new List<int>();
            j_d.Add(main_koef[1]);
            j_d.Add(res_koef.Last());
            for (int i = 0; i < res_koef.Count-1; i++)
            {
                c_rest.Add(res_koef[i]);
            }
            double seredne = 0;
            double chiselnyk = rekursia_chastkoviy_ocinka(vect_oznak, main_koef, c_rest) - rekursia_chastkoviy_ocinka(vect_oznak, i_d, c_rest) * rekursia_chastkoviy_ocinka(vect_oznak, j_d, c_rest);
            double znamenyk = Math.Sqrt((1.0 - Math.Pow(rekursia_chastkoviy_ocinka(vect_oznak, i_d, c_rest), 2)) * (1.0 - Math.Pow(rekursia_chastkoviy_ocinka(vect_oznak, j_d, c_rest), 2)));
            seredne = chiselnyk / znamenyk;
            return seredne;
        }//середнє
        static public double[,] DC_matrix(List<List<double>> vect_s)
        {
            double[,] matrix = new double[vect_s.Count,vect_s.Count];
            for (int i = 0; i < vect_s.Count; i++)
            {
                for (int j = 0; j < vect_s.Count; j++)
                {
                    if (i==j)
                    {
                        matrix[i, j] =Math.Pow(vect_seredno_kvadratichnyh(vect_s[i]),2.0);
                    }
                    else
                    {
                        matrix[i, j] = (vect_seredno_kvadratichnyh(vect_s[i]) * vect_seredno_kvadratichnyh(vect_s[j])) * ocinka_parnoho_koef_korelacii(vect_s[i], vect_s[j]);
                    }
                }
            }
            return matrix;
        }
        static public double[,] R_matrix(List<List<double>> vect_s)
        {
            double[,] matrix = new double[vect_s.Count, vect_s.Count];
            for (int i = 0; i < vect_s.Count; i++)
            {
                for (int j = 0; j < vect_s.Count; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 1.0;
                    }
                    else
                    {
                        matrix[i, j] =  ocinka_parnoho_koef_korelacii(vect_s[i], vect_s[j]);
                    }
                }
            }
            return matrix;
        }
        static public List<double> R_matrix_kk_determinate(List<List<double>> vect_s)
        {
            //визначник матриці, отриманої після викреслення із   Rˆ k - гостовпця та k - ої стороки.
            List<double> result = new List<double>();
            List<double> result111 = new List<double>();
            double[,] matr_R = R_matrix(vect_s);
            for (int i = 0; i < vect_s.Count; i++)
            {
                double[,] matr_in_cycle = new double[vect_s.Count - 1, vect_s.Count - 1];
                int index_row = 0;
                int index_col = 0;
                bool i_was_in_cycle = false;
                for (int t = 0; t < vect_s.Count; t++)
                {
                    for (int w = 0; w < vect_s.Count; w++)
                    {
                        if ((i!=w)&& (i!=t))
                        {
                            i_was_in_cycle = true;
                            matr_in_cycle[index_row, index_col] = matr_R[t, w];
                            index_col++;
                        }
                    }
                    index_col = 0;
                    if (i_was_in_cycle)
                    {
                        index_row++;
                    }
                    i_was_in_cycle = false;
                }
                result111.Add(Determinate(matr_in_cycle, vect_s.Count - 1));

            }
            for (int i = 0; i < vect_s.Count; i++)
            {
                List<List<double>> in_cycle=new List<List<double>>();
                for (int t = 0; t < vect_s.Count; t++)
                {

                    List<double> result_cyle = new List<double>();
                    if (t==i)
                    {
                        continue;
                    }
                    else
                    {
                        for (int re = 0; re < vect_s[t].Count; re++)
                        {
                            double rty = vect_s[t][re];
                            result_cyle.Add(rty);
                        }
                        in_cycle.Add(result_cyle);
                    }
                }
                result.Add(Determinate(R_matrix(in_cycle), vect_s.Count - 1));
            }
            return result111;
        }
        static public double Determinate(double[,] matr_x, int count)
        {
            double[,] matrix2 = new double[matr_x.GetUpperBound(0) + 1, matr_x.GetUpperBound(1) + 2];
            double[,] matrix3 = new double[matr_x.GetUpperBound(0) + 1, matr_x.GetUpperBound(1) + 1];
            
            for (int i = 0; i < matr_x.GetUpperBound(0) + 1; i++)
            {
                for (int t = 0; t < matr_x.GetUpperBound(1) + 1; t++)
                {
                    
                    matrix2[i, t] = matr_x[i, t];
                    matrix3[i, t] = matr_x[i, t];

                }
            }
            double kof = 1;          
            for (int i = 0; i < count; i++)
            {
                double tmp = matrix3[i, i];
                kof *= tmp;
                for (int j = 0; j < count; j++)
                {
                    matrix3[i, j] /= tmp;
                }
                for (int j = i + 1; j < count; j++)
                {
                    tmp = matrix3[j, i];
                    for (int k = 0; k < count; k++)
                    {
                        matrix3[j, k] -= tmp * matrix3[i, k];
                    }
                }

            }
            return kof;

        }

        static public List<rows> Variaciyniy_ryad(List<double> a5)
        {
            Dictionary<double, rows> variantDict = new Dictionary<double, rows>();
            double nBig = Convert.ToDouble(a5.Count);
            double chast;

            foreach (double comp in a5)
            {
                chast = 1.0 / nBig;

                if (variantDict.TryGetValue(comp, out rows existingRow))
                {
                    existingRow.chast += chast;
                    existingRow.count += 1;
                }
                else
                {
                    rows newRow = new rows
                    {
                        variant = comp,
                        chast = chast,
                        count = 1
                    };
                    variantDict.Add(comp, newRow);
                }
            }

            var variantList = variantDict.Values.ToList();
            variantList.Sort((x, y) => x.variant.CompareTo(y.variant));
            return variantList;
        }

        //static public List<rows> Variaciyniy_ryad(List<double> a5)
        //{
        //    List<rows> variant = new List<rows>();
        //    variant.Clear();
        //    double nBig = Convert.ToDouble(a5.Count);
        //    double chast;
        //    for (int i = 0; i < a5.Count; i++)
        //    {
        //        rows rows1 = new rows();
        //        double comp = 0;
        //        comp=a5[i];
        //        chast = 1.0 / nBig;
        //        rows1.chast = chast;
        //        int KKK = (int)variant.Count;
        //        rows1.variant = comp;
        //        rows1.count = 1;
        //        if (KKK == 0)
        //        {
        //            variant.Add(rows1);
        //        }
        //        else
        //        {
        //            for (int G = 0; G < KKK; G++)
        //            {
        //                if (variant[G].variant == comp)
        //                {
        //                    variant[G].chast += chast;
        //                    variant[G].count += 1;
        //                }
        //                else if ((G == KKK - 1) && variant[G].variant != comp)
        //                {
        //                    variant.Add(rows1);
        //                    break;
        //                }
        //            }
        //        }
        //
        //    }
        //    variant.Sort(delegate (rows x, rows y)
        //    {
        //        return x.variant.CompareTo(y.variant);
        //
        //    });
        //    return variant;
        //}
        static public bool unique_list(List<double> compbr, List<List<double>> origin_list)
        {
            bool res = true;
            foreach (List<double> vpr in origin_list)
            {
                if (compbr.SequenceEqual(vpr))
                {
                    res = false;
                    return res;
                }
            }
            return true;
        }
        static public List<double> Low_limit(List<List<double>> origin_list)
        {//мінімальні значення кожної ознаки
            List<double> result = new List<double>();

            for (int i = 0; i < origin_list.Count; i++)
            {
                double hight_value = origin_list[i].Min();
                result.Add(hight_value);
            }
            return result;
        }
        static public List<double> Hight_limit(List<List<double>> origin_list, int kil_class1)
        {
            //межі кожного кроку з права
            List<double> result = new List<double>();
            List<double> krochki =Program.krok_var_ryadu(origin_list,kil_class1);
            for (int i = 0; i < origin_list.Count; i++)
            {
                double hight_value = origin_list[i].Min();
                double val_resilt = hight_value + krochki[i];
                result.Add(val_resilt);
            }
            return result;
        }
        static public List<double> krok_var_ryadu(List<List<double>> origin_list,int kil_class)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < origin_list.Count; i++)
            {
                double hvalue = origin_list[i].Max() - origin_list[i].Min();
                double ksok=hvalue / Convert.ToDouble(kil_class);
                result.Add(ksok);
            }
            return result;
        }

        static public List<List<double>> varianta_each_vectors(List<List<double>> origin_list, int kil_class)
        {
            List<List<double>> result = new List<List<double>>();
            List<double> kroki_n_hist = Program.krok_var_ryadu(origin_list, kil_class);
            List<double> low_meshi = Program.Low_limit(origin_list);
            List<double> high_meshi = Program.Hight_limit(origin_list,  kil_class);
            for (int i = 0; i < origin_list.Count; i++)
            {
                List<double> resulat_each_cycle = new List<double>();
                for (int tr = 0; tr < origin_list[i].Count; tr++)
                {
                    if (resulat_each_cycle.Count==kil_class)
                    {
                        break;
                    }
                    double hvalue = (low_meshi[i] + high_meshi[i])/2.0;                  
                    low_meshi[i] += kroki_n_hist[i];
                    high_meshi[i] += kroki_n_hist[i];
                    resulat_each_cycle.Add(hvalue);
                }
                result.Add(resulat_each_cycle);                
            }
            return result;
        }
        static public List<double> Standartuzazia(List<double> compb)
        {
            List<double> variant = new List<double>();
            variant.Clear();
            double comp1;
            double ser_kwadra = Program.vect_seredno_kvadratichnyh(compb);
            double seredne = Program.vect_seredne(compb);
            double stand1;
            for (int i = 0; i < compb.Count; i++)
            {
                comp1 = compb[i];
                stand1 = (comp1 - seredne) / ser_kwadra;
                variant.Add(stand1);
            }
            return variant;
        }

        static public List<double> Centering(List<double> compb)
        {
            List<double> variant = new List<double>();
            variant.Clear();
            double comp1;
            
            double seredne = Program.vect_seredne(compb);
            double stand1;
            for (int i = 0; i < compb.Count; i++)
            {
                comp1 = compb[i];
                stand1 = (comp1 - seredne);
                variant.Add(stand1);
            }
            return variant;
        }
        static public List<double> Exponuvanya(List<double> compb)
        {
            List<double> variant = new List<double>();

          
            double comp1;


            for (int i = 0; i < compb.Count; i++)
            {
                comp1 = compb[i];
                double exponTransformed = Math.Exp(comp1);

                variant.Add(exponTransformed);
            }

            return variant;
        }

        static public List<double> Logariphmuvanya(List<double> compb)
        {
            List<double> variant = new List<double>();
            variant.Clear();
            bool minval = false;
            double comp1;
            double stand1;
            double min_value = compb.Min();
            for (int i = 0; i < compb.Count; i++)
            {
                comp1 = compb[i];
                if (min_value <= 0.1)
                {
                    minval = true;
                    comp1 += Math.Abs(min_value) + 0.01;
                    stand1 = Math.Log(Math.Abs(comp1));
                    variant.Add(stand1);
                }
                else if (minval)
                {
                    comp1 += Math.Abs(min_value) + 1;
                    stand1 = Math.Log(Math.Abs(comp1));
                    variant.Add(stand1);
                }
                else
                {
                    stand1 = Math.Log(Math.Abs(comp1));
                    variant.Add(stand1);
                }
            }           
            return variant;
        }
        static public double Kvantil_Norm_distibution(double probability)
        {

            //probability = probability;
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
        }//alpha=alpha
        static public double normKvantil_alphana2(double probability)
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
        }//alpha=1-alpha
        static public double Kvantil_Studenta(double probability, double stepin_vilnost)
        {

            double kvantil_start_rozpodil = Kvantil_Norm_distibution(probability);
            double g1_u = (Math.Pow(kvantil_start_rozpodil, 3.0) + kvantil_start_rozpodil) / 4.0;
            double g2_u = (5.0 * Math.Pow(kvantil_start_rozpodil, 5.0) + 16.0 * Math.Pow(kvantil_start_rozpodil, 3.0) + 3.0 * kvantil_start_rozpodil) / 96.0;
            double g3_u = (3.0 * Math.Pow(kvantil_start_rozpodil, 7.0) + 19.0 * Math.Pow(kvantil_start_rozpodil, 5.0) + 17.0 * Math.Pow(kvantil_start_rozpodil, 3.0) - (15.0 * kvantil_start_rozpodil)) / 384.0;
            double g4_u = (79.0 * Math.Pow(kvantil_start_rozpodil, 9.0) + 779.0 * Math.Pow(kvantil_start_rozpodil, 7.0) + 1482.0 * Math.Pow(kvantil_start_rozpodil, 5.0) - (1920.0 * Math.Pow(kvantil_start_rozpodil, 3.0)) - (945.0 * kvantil_start_rozpodil)) / 92160.0;
            double kvantil_Styudent = kvantil_start_rozpodil + (g1_u / stepin_vilnost) + (g2_u / Math.Pow(stepin_vilnost, 2.0)) + (g3_u / Math.Pow(stepin_vilnost, 3.0)) + (g4_u / Math.Pow(stepin_vilnost, 4.0));// квантиль розподілу Стьюдента
            return kvantil_Styudent;
        }
        static public double Kvantil_Fishera(double probability, double stepin_vilnost1, double stepin_vilnosty2)
        {
            double kvantil_start_rozpodil = Kvantil_Norm_distibution(probability);

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
       static public double Kvantil_Pirsona(double probability, double stepin_vilnost)
        {
            double kvantil_start_rozpodil = Kvantil_Norm_distibution(probability);
            double phi_kvadrat_pirsona = stepin_vilnost * Math.Pow((1.0 - (2 / (9 * stepin_vilnost)) + kvantil_start_rozpodil * Math.Sqrt(2 / (9 * stepin_vilnost))), 3.0);
            return phi_kvadrat_pirsona;
        }

        static public double[,] Inverse_matrix(double[,] matrix)
        {
            double[,] matr_inverse = new double[matrix.GetUpperBound(0) + 1, matrix.GetUpperBound(1) + 1];
            
            double[,] matrix2 = new double[matrix.GetUpperBound(0) + 1, matrix.GetUpperBound(1) + 2];
            double[,] matrix3 = new double[matrix.GetUpperBound(0) + 1, matrix.GetUpperBound(1) + 1];
            double[,] rezult = new double[matrix.GetUpperBound(0) + 1, matrix.GetUpperBound(1) + 1];
            for (int i = 0; i < matrix.GetUpperBound(0)+1; i++)
            {
                for (int t = 0; t < matrix.GetUpperBound(1) + 1; t++)
                {
                    matr_inverse[i, t] = matrix[i, t];
                    matrix2[i, t] = matrix[i, t];
                    matrix3[i, t] = matrix[i, t];

                }
            }
            double[] matr = new double[matrix.GetUpperBound(1) + 1];
            int count = matrix.GetUpperBound(1) + 1;
            for (int h = 0; h < count; h++)
            {
                for (int b = 0; b < count; b++)
                {
                    for (int i = 0; i < count; i++)
                    {
                        matrix2[b, i] = matrix3[b, i];
                    }
                }
                for (int q = 0; q < count; q++)
                {
                    if (q == h)
                    {
                        matrix2[q, count] = 1;
                    }
                    else
                    {
                        matrix2[q, count] = 0;
                    }

                }

                for (int i = 0; i < count; i++)
                {
                    double tmp = matrix2[i, i];

                    for (int j = 0; j < count + 1; j++)
                    {
                        matrix2[i, j] /= tmp;
                    }
                    for (int j = i + 1; j < count; j++)
                    {
                        tmp = matrix2[j, i];
                        for (int k = count; k >= i; k--)
                        {
                            matrix2[j, k] -= tmp * matrix2[i, k];
                        }
                    }
                }

                double[] xx1 = new double[count];
                xx1[count - 1] = matrix2[count - 1, count];
                for (int i = count - 2; i >= 0; i--)
                {
                    xx1[i] = matrix2[i, count];
                    for (int j = i + 1; j < count; j++)
                    {
                        xx1[i] -= matrix2[i, j] * xx1[j];
                    }
                }
                for (int t = 0; t < count; t++)
                {
                    rezult[t, h] = xx1[t];
                }


            }
            return rezult;
        }
        static public double[,] multiplication_column_on_rows(List<double> first_list, List<double> second_list)
        {
            double[,] rezult = new double[first_list.Count, first_list.Count];
            for (int i = 0; i < first_list.Count; i++)
            {
                for (int r = 0; r < first_list.Count; r++)
                {
                    rezult[i, r] = first_list[i] * second_list[r];
                }
            }
            return rezult;
        }
        static public double multiplication__rows_on_colums(List<double> first_list, List<double> second_list)
        {
            double rezult = 0; ;
            for (int i = 0; i < first_list.Count; i++)
            {
                rezult+=first_list[i] * second_list[i];
            }
            return rezult;
        }
        static public double[,] addition_matrix(double[,] first_list, double[,] second_list)
        {
            double[,] rezult = new double[first_list.GetLength(0), first_list.GetLength(1)];
            for (int i = 0; i < first_list.GetLength(0); i++)
            {
                for (int r = 0; r < first_list.GetLength(1); r++)
                {
                    rezult[i, r] = first_list[i, r] + second_list[i, r];
                }
            }
            return rezult;
        }
        static public double[,] subtraction_matrix(double[,] first_list, double[,] second_list)
        {
            double[,] rezult = new double[first_list.GetUpperBound(0) + 1, first_list.GetUpperBound(0) + 1];
            for (int i = 0; i < first_list.GetUpperBound(0) + 1; i++)
            {
                for (int r = 0; r < first_list.GetUpperBound(0) + 1; r++)
                {
                    rezult[i, r] = first_list[i, r] - second_list[i, r];
                }
            }
            return rezult;
        }
        static public double[,] multiplication_matrix_on_matrix(double[,] first_matr, double[,] second_matr)
        {
            double[,] rezult = new double[first_matr.GetLength(0), second_matr.GetLength(1)];
            if (first_matr.GetLength(1) != second_matr.GetLength(0))
            {
                return rezult;
            }
            for (int i = 0; i < first_matr.GetLength(0); i++)
            {
                for (int j = 0; j < second_matr.GetLength(1); j++)
                {
                    double sum = 0;
                    for (int t = 0; t < second_matr.GetLength(0); t++)
                    {
                        sum += first_matr[i, t] * second_matr[t, j];
                    }
                    rezult[i, j] = sum;
                }

            }
            return rezult;
        }
        static public double[,] List_to_matrix(List<List<double>> vectors)
        {
            double[,] rezult = new double[vectors.Count, vectors[0].Count];
            for (int i = 0; i < vectors.Count; i++)
            {
                for (int r = 0; r < vectors[i].Count; r++)
                {
                    rezult[i, r] = vectors[i][r];
                }
            }
            return rezult;
        }
        static public double[,] List_to_matrix(List<double> vectors)
        {
            double[,] rezult = new double[1, vectors.Count];
            for (int i = 0; i < vectors.Count; i++)
            {
                rezult[0, i] = vectors[i];
            }
            return rezult;
        }

        static public double[,] Transponovana_matrix(double[,] first_matr)
        {
            double[,] rezult = new double[first_matr.GetLength(1), first_matr.GetLength(0)];
            for (int i = 0; i < first_matr.GetLength(1); i++)
            {
                for (int t = 0; t < first_matr.GetLength(0); t++)
                {
                    rezult[i, t] = first_matr[t, i];
                }
            }
            return rezult;
        }
        static public double[,] multiplication_matrix_on_number(double[,] first_list, double number)
        {
            double[,] rezult = new double[first_list.GetLength(0), first_list.GetLength(1)];
            for (int i = 0; i < first_list.GetLength(0); i++)
            {
                for (int r = 0; r < first_list.GetLength(1); r++)
                {
                    rezult[i, r] = first_list[i, r] * number;
                }
            }
            return rezult;
        }

        static public List<double> multiplication_matrix_on_colums(double[,] first_list, List<double> number)
        {
            List<double> rezult = new List<double>();
            for (int i = 0; i < first_list.GetUpperBound(0) + 1; i++)
            {
                double suma = 0;
                for (int r = 0; r < number.Count; r++)
                {
                    suma+= first_list[i, r] * number[r];
                }
                rezult.Add(suma);
            }
            return rezult;
        }
        static public List<double> multiplication_row_on_matrix(List<double> number,double[,] first_list)
        {
            List<double> rezult = new List<double>();
            for (int i = 0; i < number.Count; i++)
            {
                double suma = 0;
                for (int r = 0; r < number.Count; r++)
                {
                    suma += number[r]*first_list[r, i];
                }
                rezult.Add(suma);
            }
            return rezult;
        }

        /////власні числа та власні вектори
        ///
        static public List<double> EigenValues(double[,] Matrix)
        {
            double[,] matrix_new = new double[Matrix.GetLength(0), Matrix.GetLength(1)];
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int t = 0; t < Matrix.GetLength(1); t++)
                {
                    matrix_new[i, t] = Matrix[i, t];
                }
            }
            var shmit = Grama_shmit(matrix_new);
            var matrixR = multiplication_matrix_on_matrix(Transponovana_matrix(shmit), matrix_new);
            var matrix_a = multiplication_matrix_on_matrix(matrixR, shmit);
            repeat:
            shmit = Grama_shmit(matrix_a);
            matrixR = multiplication_matrix_on_matrix(Transponovana_matrix(shmit), matrix_a);
            matrix_a = multiplication_matrix_on_matrix(matrixR, shmit);
            for (int i = 0; i < matrix_a.GetLength(0); i++)
            {
                for (int t = 0; t < matrix_a.GetLength(1); t++)
                {
                    double in_the_matrix = matrix_a[i, t];
                    if (Math.Abs(in_the_matrix) >= Math.Pow(10, -5) && i != t)
                    {
                        goto repeat;
                    }
                }
            }
            List<double> vlasnichisla = new List<double>();
            for (int i = 0; i < matrix_a.GetLength(0); i++)
            {
                for (int t = 0; t < matrix_a.GetLength(1); t++)
                {
                    if (i == t)
                    {
                        vlasnichisla.Add(matrix_a[i, t]);
                    }
                }
            }

            return vlasnichisla;

        }
        static public List<double[]> EigenVectors(double[,] Matrix, bool normovani)
        {
            List<double[]> crt = krilova(Matrix.GetLength(1), Matrix, EigenValues(Matrix));
            if (normovani)
            {
                for (int i = 0; i < crt.Count; i++)
                {
                    double len_massive = 0;
                    for (int tr = 0; tr < crt[i].Length; tr++)
                    {
                        len_massive += Math.Pow(crt[i][tr], 2.0);
                    }
                    len_massive = Math.Sqrt(len_massive);
                    for (int t = 0; t < crt[i].Length; t++)
                    {
                        crt[i][t] =crt[i][t] / len_massive;
                    }
                }

            }
            for (int i = 0; i < crt.Count; i++)
            {    
                for (int tr = 0; tr < crt[i].Length; tr++)
                {
                    crt[i][tr] = crt[i][tr]*(-1.0);
                }
            }

            return crt;
        }
        static public List<double[]> krilova(int n_for, double[,] matrix, List<double> vidpovid)
        {
            List<double[]> vidpovid12 = new List<double[]>();

            double[,] a_matrix = new double[n_for, n_for];
            double[,] ab_matrix = new double[n_for, n_for];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int t = 0; t < matrix.GetLength(1); t++)
                {
                    a_matrix[i, t] = matrix[i, t];
                    ab_matrix[i, t] = matrix[i, t];
                }
            }
            int count = n_for;
            double[] bn = new double[count];
            double[] bn1 = new double[count];
            double[,] itermatrix = new double[count, count + 1];
            itermatrix[0, 0] = 1.0;
            for (int i = 1; i < count; i++)
            {
                itermatrix[i, 0] = 0;

            }

            for (int i = 0; i < count; i++)
            {
                double sum = 0;
                for (int k = 0; k < count; k++)
                {
                    sum += a_matrix[i, k] * a_matrix[k, 0];
                }
                bn[i] = sum;
                itermatrix[i, 2] = sum;
                itermatrix[i, 1] = a_matrix[i, 0];

            }//створення СЛАР

            for (int y = 3; y < count + 1; y++)
            {

                for (int i = 0; i < count; i++)
                {
                    double sum = 0;
                    for (int k = 0; k < count; k++)
                    {
                        sum += ab_matrix[i, k] * bn[k];
                    }
                    bn1[i] = sum;
                    itermatrix[i, y] = sum;

                }
                for (int i = 0; i < count; i++)
                {
                    bn[i] = bn1[i];
                }
            }//формування СЛАР
            double[,] vektoo = new double[count, count];
            double[,] vektoo2 = new double[count, count];
            for (int i = 0; i < count; i++)
            {
                for (int k = 0; k < count; k++)
                {
                    vektoo[i, k] = itermatrix[i, k];
                    vektoo2[i, k] = itermatrix[i, k];
                }
            }


            ///////////////
            for (int i = 0; i < count; i++)
            {
                // Пошук максимального елемента в стовпці
                int maxRow = i;
                for (int k = i + 1; k < count; k++)
                {
                    if (Math.Abs(itermatrix[k, i]) > Math.Abs(itermatrix[maxRow, i]))
                    {
                        maxRow = k;
                    }
                }

                // Перестановка рядків
                for (int k = i; k <= count; k++)
                {
                    double tmp1 = itermatrix[maxRow, k];
                    itermatrix[maxRow, k] = itermatrix[i, k];
                    itermatrix[i, k] = tmp1;
                }
                double tmp = itermatrix[i, i];
                for (int j = 0; j < count + 1; j++)
                {
                    itermatrix[i, j] /= tmp;
                }
                for (int j = i + 1; j < count; j++)
                {
                    tmp = itermatrix[j, i];
                    for (int k = count; k >= i; k--)
                    {
                        itermatrix[j, k] -= tmp * itermatrix[i, k];
                    }
                }
                // Ваша існуюча логіка методу Гауса
                // ...
            }
            ///////////

            //for (int i = 0; i < count; i++)//метод гаусса
            //{
            //    double tmp = itermatrix[i, i];
            //    for (int j = 0; j < count + 1; j++)
            //    {
            //        itermatrix[i, j] /= tmp;
            //    }
            //    for (int j = i + 1; j < count; j++)
            //    {
            //        tmp = itermatrix[j, i];
            //        for (int k = count; k >= i; k--)
            //        {
            //            itermatrix[j, k] -= tmp * itermatrix[i, k];
            //        }
            //    }
            //
            //}
            double[] xx = new double[count];
            xx[count - 1] = itermatrix[count - 1, count];
            for (int i = count - 2; i >= 0; i--)//пошук коефіцієнтів
            {
                xx[i] = itermatrix[i, count];
                for (int j = i + 1; j < count; j++)
                {
                    xx[i] -= itermatrix[i, j] * xx[j];
                }
            }
            for (int i = 0; i < count; i++)
            {
                xx[i] = (-1.0) * xx[i];
            }
            double[] xx12 = new double[count];
            for (int i = 0; i < count; i++)
            {
                xx12[i] = xx[count - 1 - i];
            }
            double[] qqq = new double[count];
            double[] qqq123 = new double[count];
            for (int i = 0; i < count; i++)
            {
                int tet = 0;
                double lam = vidpovid[i];
                qqq[0] = 1.0;
                for (int h = 1; h < count; h++)
                {
                    qqq[h] = lam * qqq[h - 1] + xx12[h - 1];
                }
                for (int y = 0; y < count; y++)
                {
                    tet = 0;
                    qqq123[y] = 0;
                    double one = 0;
                    for (int z = count - 1; z >= 0; z--)
                    {
                        one += vektoo[y, z] * qqq[tet];
                        tet++;
                    }
                    qqq123[y] += one;
                }
                double[] qqq321 = new double[count];
                for (int y32 = 0; y32 < count; y32++)
                {
                    qqq321[y32] = qqq123[y32];
                }
                vidpovid12.Add(qqq321);

            }//пошук власних векторів


            return vidpovid12;

        }
        static public double[,] Grama_shmit(double[,] matrix)
        {
            List<double[]> result = new List<double[]>();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double[] in_cycle = new double[matrix.GetLength(1)];
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    in_cycle[j] = matrix[i, j];
                }
                if (i == 0)
                {
                    result.Add(Ortogonalization(in_cycle));
                    continue;
                }
                double[] suma = new double[matrix.GetLength(0)];
                for (int t = 0; t < i; t++)
                {
                    double[] proj = Projection(result[t], in_cycle);
                    suma = addition_vectors(suma, proj);
                }
                result.Add(Ortogonalization(subtraction_vectors(in_cycle, suma)));

            }

            double[,] result_matrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < result.Count; i++)
            {
                for (int t = 0; t < matrix.GetLength(0); t++)
                {
                    result_matrix[t, i] = result[i][t];
                }
            }

            return result_matrix;

        }
        static public double[] Projection(double[] matrixU, double[] matrixV)
        {
            //double numerator = Dot_product(matrixV, matrixU);
            //double denominator = Dot_product(matrixU, matrixU);
            double numerator = Dot_product(matrixV, matrixU);

            double denominator = Dot_product(matrixU, matrixU);
            var result = multiplication_vector_on_number(matrixU, (numerator / denominator));
            return result;
        }
        static public double[] Ortogonalization(double[] vector)
        {
            double modul_vect = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                modul_vect += Math.Pow(vector[i], 2.0);
            }
            double[] new_vetor = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                new_vetor[i] = vector[i] / Math.Sqrt(modul_vect);
            }
            return new_vetor;
        }
        static public double Dot_product(double[] matrix1, double[] matrix2)
        {
            double result = 0;
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                result += (matrix1[i] * matrix2[i]);
            }
            return result;
        }
        static public double[] subtraction_vectors(double[] first_list, double[] second_list)
        {
            double[] rezult = new double[first_list.Length];
            for (int i = 0; i < first_list.Length; i++)
            {
                rezult[i] = first_list[i] - second_list[i];

            }
            return rezult;
        }
        static public double[] addition_vectors(double[] first_list, double[] second_list)
        {
            double[] rezult = new double[first_list.Length];
            for (int i = 0; i < first_list.Length; i++)
            {
                rezult[i] = first_list[i] + second_list[i];

            }
            return rezult;
        }
        static public double[] multiplication_vector_on_number(double[] first_list, double number)
        {
            double[] rezult = new double[first_list.Length];
            for (int i = 0; i < first_list.Length; i++)
            {
                rezult[i] = number * first_list[i];
            }
            return rezult;
        }
        static public double[] Gaussa_metod(int n_for, double[,] itermatrix)
        {
            int count = n_for;

            double[] xx = new double[count];
            for (int i = 0; i < count; i++)//метод гаусса
            {
                double tmp = itermatrix[i, i];
                for (int j = 0; j < count + 1; j++)
                {
                    itermatrix[i, j] /= tmp;
                }
                for (int j = i + 1; j < count; j++)
                {
                    tmp = itermatrix[j, i];
                    for (int k = count; k >= i; k--)
                    {
                        itermatrix[j, k] -= tmp * itermatrix[i, k];
                    }

                }
            }
            xx[count - 1] = itermatrix[count - 1, count];
            for (int i = count - 2; i >= 0; i--)//пошук коефіцієнтів
            {
                xx[i] = itermatrix[i, count];
                for (int j = i + 1; j < count; j++)
                {
                    xx[i] -= itermatrix[i, j] * xx[j];
                }
            }

            return xx;
        }
        //коефіцієнти 3д площини
        static public List<double> koef_3D_area(List<List<double>> vectors)
        {
            List<double> xx = new List<double>();
            //create matrix
            double[,] matrix_gen = new double[vectors.Count, vectors.Count];
            for (int i = 0; i < vectors.Count; i++)
            {
                for (int j = 0; j < vectors.Count; j++)
                {
                    if (j==0)
                    {
                        matrix_gen[i, j] = (-1.0)*vectors[i][0];
                    }
                    else
                    {
                        matrix_gen[i, j] = vectors[i][0]-vectors[i][j];
                    }
                    
                }
            }

            double determinate = 0;
            for (int i = 0; i < matrix_gen.GetLength(0); i++)
            {
                //new matrix;
                double[,] Minor = new double[matrix_gen.GetLength(0) - 1, matrix_gen.GetLength(0) - 1];
                int index_row = 0;
                int index_col = 0;
                for (int j = 0; j < matrix_gen.GetLength(1); j++)
                {
                    index_col = 0;
                    for (int t = 0; t < matrix_gen.GetLength(1); t++)
                    {
                        if (j != i && t != 0)
                        {
                            Minor[index_row, index_col] = matrix_gen[j, t];
                            index_col++;
                        }
                    }
                    if (j!=i)
                    {
                        index_row++;
                    }
                    
                }
                double rezult_of_deter = Math.Pow(-1.0, 0 + 1 + i + 1) * determiante_rekursia(Minor);
                determinate += matrix_gen[i, 0] * (rezult_of_deter);
                xx.Add(rezult_of_deter);
            }
            xx.Add(determinate);
            
            //List<double> matrix_B=new List<double>();
            //for (int i = 0; i < vectors.Count; i++)
            //{
            //    matrix_B.Add(1.0);
            //}
            // double [,] matrix_A_trans=Transponovana_matrix(matrix_gen);
            // double[,] matrixA_trans_multA = multiplication_matrix_on_matrix(matrix_A_trans, matrix_gen);
            // double[,] matrixA_reverse = Inverse_matrix(matrixA_trans_multA);
            // double[,] matrix_second = multiplication_matrix_on_matrix(matrixA_reverse, matrix_A_trans);
            // List<double> matrix_finish = multiplication_matrix_on_colums(matrix_second, matrix_B);
            return xx;
        }

        static public double determiante_rekursia(double[,] matrix)
        {
            double determinate = 0;
            if (matrix.GetLength(0)==2)
            {
                determinate = (matrix[0, 0] * matrix[1, 1]) - (matrix[1, 0] * matrix[0,1]);
                return determinate;
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                //new matrix;
                double[,] Minor = new double[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
                int index_row = 0;
                int index_col = 0;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    index_col = 0;
                    for (int t = 0; t < matrix.GetLength(1); t++)
                    {
                        if (j!=0 && t!=i)
                        {
                            Minor[index_row, index_col] = matrix[j, t];
                            index_col++;
                        }
                    }
                    if (j != 0)
                    {
                        index_row++;
                    }
                   
                }
                determinate += matrix[0, i] * (Math.Pow(-1.0, 0 + 1 + i + 1)*determiante_rekursia(Minor));
            }

            return determinate;
        }
    }
}
