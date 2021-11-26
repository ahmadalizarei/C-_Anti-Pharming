using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using System.Drawing;

namespace WindowsFormsApp5
{
 
    public partial class Form1 : Form
    {
         
        public Form1()
        {
            
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.Opacity = 0;
            this.Location = new Point(-10000, -10000);

        }


        string path = "C:\\Windows\\System32\\drivers\\etc\\hosts";
        string default_chksum = "eeea48303c423557de1f85d661c93e27";
   
        static void InvokeMethod()
        {
            int flag = 0;
            while (true)
            {
                if (check() != "eeea48303c423557de1f85d661c93e27") { 
                    MessageBox.Show("فایل هاست تغییر کرده است. ممکن است یک حمله فارمینگ باشد");
                    flag = 1;

                System.Diagnostics.Process.Start("C:\\Windows\\System32\\drivers\\etc\\hosts");
                 }

                Thread.Sleep(5000);
                if (flag == 1) { 

                    Thread.Sleep(30000);

                    flag = 0;
                   }

            }
        }
        public static string BytesToString(byte[] bytes)
        {
            string result = "";
            foreach (byte b in bytes) result += b.ToString("x2");
            return result;
        }
        public static Byte[] ComputeHash(String path)
        {
            
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                  return md5.ComputeHash(stream);
                }
            }
        }

        static String check()
        {
            string path = "C:\\Windows\\System32\\drivers\\etc\\hosts";
            using (StreamWriter writer = File.AppendText("D:\\c.txt"))
            {
                writer.WriteLine(BytesToString(ComputeHash(path)));
              

            }
            return BytesToString(ComputeHash(path));
        

        }
        private void Form1_Load(object sender,EventArgs e)

        {
          

            Thread t = new Thread(new ThreadStart(InvokeMethod));
            t.Start();
        }
    }
}
