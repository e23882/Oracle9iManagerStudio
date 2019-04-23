using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommonFunction;

namespace OracleManagerStudio
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Declarations
        TreeViewItem currentTreeNodeParent = null;
        Login lg = null;
        #endregion

        #region Property
        public class dbInfo
        {
            public string dbName { get; set; }
            public List<string> TableName { get; set; }
            public string Status { get; set; }
            public string IP { get; set; }
            public string PW { get; set; }
            public string ID { get; set; }
        }

        #endregion

        #region MemberFunction
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtConnect_Click(object sender, RoutedEventArgs e)
        {
            lg = new Login();
            lg.Show();
            lg.Closed += Window_Closed;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            dbInfo info = new dbInfo();
            info.TableName = lg.allTable;
            info.dbName = lg.dbName;
            info.Status = lg.Status;
            info.IP = lg.IP;
            info.ID = lg.tbID.Text;
            info.PW = lg.tbPW.Text;
            TreeViewItem topNode = new TreeViewItem();
            topNode.Header = info.dbName;
            topNode.Tag = info;
            foreach (var item in info.TableName)
            {
                TreeViewItem ti = new TreeViewItem();
                ti.Header = item;
                ti.MouseDoubleClick += TreeViewItem_MouseDoubleClick;
                topNode.Items.Add(ti);
                ti.Selected += Ti_Selected;
            }
            tree.Items.Add(topNode);
        }
        private void Ti_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item != null)
            {
                ItemsControl parent = GetSelectedTreeViewItemParent(item);
                currentTreeNodeParent = parent as TreeViewItem;
            }
        }
        public ItemsControl GetSelectedTreeViewItemParent(TreeViewItem item)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as ItemsControl;
        }
        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((sender as TreeViewItem) is null)
                return;
            var dt = (sender as TreeViewItem).Header;
            tbSQL.Text = "select * from " + dt;
        }
        private void TbSQL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                ExecSQLQuery();
            }
        }
        private void BtDisconnect_Click(object sender, RoutedEventArgs e)
        {
            tree.Items.Remove(currentTreeNodeParent);
        }
        #endregion

        private void BtExecute_Click(object sender, RoutedEventArgs e)
        {
            ExecSQLQuery();
        }

        public void ExecSQLQuery()
        {
            if (currentTreeNodeParent is null)
                return;
            try
            {
                var dt = (dbInfo)currentTreeNodeParent.Tag;
                OraConnection ofd = new OraConnection(dt.IP, dt.dbName, dt.ID, dt.PW);
                var dtofd = ofd.ExecQuery(tbSQL.Text);
                dgResult.ItemsSource = dtofd.AsDataView();
            }
            catch (Exception ie)
            {
                dgResult.ItemsSource = null;
                MessageBox.Show(ie.Message);
            }
            
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
