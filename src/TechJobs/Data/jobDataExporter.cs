using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;


namespace TechJobs.Data
{
    public class jobDataExporter
    {
     

        public static void writeJobDataRow(String jobDataRow)
        {
            using (StreamWriter sw = File.AppendText("Data/job_data.csv"))
            {
                sw.WriteLine(jobDataRow);
                sw.Flush();
                sw.Close();
            }

          
        }
    }
}
