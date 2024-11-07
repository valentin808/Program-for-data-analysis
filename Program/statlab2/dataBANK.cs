using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace statlab2
{
    public class dataBANK
    {

        public static List<double> corTX = new List<double>();
        public static List<double> AnomalChast = new List<double>();
        public static List<double> AnolomalLOGfunc = new List<double>();
        public static List<double> AnomalSTandfunc = new List<double>();
        public static List<double> standfunc = new List<double>();
        public static List<double> uolsh = new List<double>();
        public static List<double> MAD = new List<double>();
        public static List<double> logfunc = new List<double>();
        public static List<double> AnomalcorTX = new List<double>();
        public static List<double> Chast = new List<double>();
        public static List<double> OriginalData = new List<double>();

        public static List<double> not_sort_log_list = new List<double>();//несорторований логарифмічний лість
        public static List<double> not_sort_standart_list = new List<double>();//несорторований стандартизований лість

        //public static List<List<rows>> vibirka_list = new List<List<rows>>();
    }
       class rows
       {
          public int count;
          public double variant;
          public double chast;
        
        
       }
    class normclass
    {
        public bool norm;
        public int nomer_vibirk;

    }
    class rang 
    {
        public double element ;
        public double rangg;
        public int index;
        public double which_elem;

    }
    class tochki_2d
    {
        public double x;
        public double y;
        public double rang_x;
        public double rang_y;
    }
    class N_dimension
    {
        public List<double> spisok_coordinate = new List<double>();
        public List<int> spisok_indexiv = new List<int>();
        public int chastota_p=0;
        public double vidnosna_chastota = 0;
    }
    class table_of_chastot
    {
        public int index_x;
        public int index_y;
        public double chastota;
        public double kil_chastot;
        public double variant_po_x;
        public double variant_po_y;
        public double kvadrat_x_zero;
        public double kvadrat_y_zero;
        public double kvadrat_x_end;
        public double kvadrat_y_end;
    }
    class table_of_spoluchen
    {
        public int index_x;
        public int index_y;
        public double chastota;


    }
    //class rang_korel
    //{
    //    public double element;
    //    public double rangg;
        

    //}

}
