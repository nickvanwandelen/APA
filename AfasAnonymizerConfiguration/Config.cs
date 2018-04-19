using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfasAnonymizerConfiguration
{
    public static class Config
    {
        public static string SERVERNAME { get; set; }
        public static string TARGETDATABASE { get; set; } 
        public static string COMPAREDATABASE { get; set; }
        public static string COPYNAME { get; set; } = "Sauzijcenbroodje";
        public static int PERCENTAGEMATCH { get; set; }
        public static bool SHOULDCOMPAREDATABASE { get; set; }
        public static bool SHOULDCOPYDATABASE { get; set; }

        public static void Start()
        {
            if (SHOULDCOPYDATABASE)
            {
                DatabaseCopier.CopyProfitDatabase();
            }
        }
    }
}
