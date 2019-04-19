using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CommonFunction;

namespace OracleManagerStudio
{
    /// <summary>
    /// Login.xaml 的互動邏輯
    /// </summary>
    public partial class Login : Window
    {
        #region Declarations
        public string dbName { get; set; }
        public string Status { get; set; }

        public string IP { get; set; }
        public List<string> allTable { get; set; }
        #endregion


        public Login()
        {
            InitializeComponent();
        }

        private void BtConnect_Click(object sender, RoutedEventArgs e)
        {
            string query = string.Empty;
            int count = 0;
            //original connection string "Provider=MSDAORA.1;Data Source=OFD.world;User ID=sw;Password=ofdadm1116;"
            OraConnection ofd = new OraConnection(tbDBIP.Text, tbDBName.Text, tbID.Text, tbPW.Text);
            var dtofd = ofd.ExecQuery("select count(*) from v$session;");
            foreach (DataRow dr in dtofd.Rows)
            {
                count++;
            }
            if (count > 0)
            {
                dbName = tbDBName.Text;
                Status = "connect";
                IP = tbDBIP.Text;
                var dtTable = ofd.ExecQuery("select table_name from user_tables;");
                List<string> temp = new List<string>();
                foreach (DataRow dr in dtTable.Rows)
                {
                    temp.Add(dr[0].ToString());
                }
                allTable = temp;
                this.Close();
            }
            else
                Status = "connect fail";
        }
        
        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
