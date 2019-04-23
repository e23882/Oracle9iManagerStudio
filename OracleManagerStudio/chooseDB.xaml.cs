using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OracleManagerStudio
{
    /// <summary>
    /// chooseDB.xaml 的互動邏輯
    /// </summary>
    public partial class chooseDB : Window
    {
        #region Property
        public class DS
        {
            public string DBName { get; set; }
            public string ServerIP { get; set; }
            public string Password { get; set; }
            public string User { get; set; }
        }
        #endregion

        #region Memberfunction
        public chooseDB()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            List<DS> list = new List<DS>();
            list.Add(new DS { DBName = "DB1", ServerIP = "192.168.1.1", User = "user", Password = "pw123" });
            list.Add(new DS { DBName = "DB2", ServerIP = "192.168.1.2", User = "user", Password = "pw123" });
            list.Add(new DS { DBName = "DB3", ServerIP = "192.168.1.3", User = "user", Password = "pw123" });
            foreach (var item in list)
            {
                ListBoxItem lbi = new ListBoxItem();
                lbi.MouseDoubleClick += ListBoxItem_MouseDoubleClick;
                lbi.Tag = item;
                lbi.Content = item.DBName + ".world";
                lbDB.Items.Add(lbi);
            }
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Tag = (sender as ListBoxItem).Tag;
            this.Close();
        }
        #endregion
    }
}
