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

        chooseDB cDB = null;

        #endregion

        #region Memberfunction
        public Login()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = string.Empty;
                int count = 0;

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
            catch (Exception ie)
            {
                MessageBox.Show(ie.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cDB = new chooseDB();
            cDB.MouseDoubleClick += CDB_MouseDoubleClick;
            cDB.Show();
        }
        private void CDB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (cDB is null)
                return;
            var dt = cDB.Tag;
            tbDBIP.Text = ((chooseDB.DS)dt).ServerIP;
            tbDBName.Text = ((chooseDB.DS)dt).DBName;
            tbID.Text = ((chooseDB.DS)dt).User;
            tbPW.Text = ((chooseDB.DS)dt).Password;
        }
        #endregion
    }
}
