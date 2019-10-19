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
using SpecLookUp.DAL;
using SpecLookUp.Model;

namespace SpecLookUp
{
    /// <summary>
    /// Logika interakcji dla klasy EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window 
    {
        public Device SelectedLog { get; set; }
        public EditWindow(Device selected)
        {
            InitializeComponent();
            SelectedLog = selected;

            DeviceTBox.Text = SelectedLog.DeviceModel;
            SerialTBox.Text = SelectedLog.DeviceSerial;
            DescriptionTBox.Text = SelectedLog.Description;

            RpTBox.Text = SelectedLog.Rp;
            SoTBox.Text = SelectedLog.So;
            LabelTBox.Text = SelectedLog.OsLabel;
            CmarTBox.Text = SelectedLog.OsCmar;
            CommentsTBox.Text = SelectedLog.Comments;
        }

        private void UpdateSelectedDevice()
        {
            SelectedLog.Rp = RpTBox.Text.Trim();
            SelectedLog.So = SoTBox.Text.Trim();
            SelectedLog.OsLabel = LabelTBox.Text.Trim();
            SelectedLog.OsCmar = CmarTBox.Text.Trim();
            SelectedLog.Comments = CommentsTBox.Text.Trim();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedDevice();

            var updateSucceed = MysqlWorker.UpdateDevice(SelectedLog);

            if (updateSucceed)
            {
                MessageBox.Show("Log has been updated.");
                this.Close();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var hideSucceed = MysqlWorker.HideDevice(SelectedLog.Id.ToString());

            if (hideSucceed)
            {
                MessageBox.Show("Log has been removed.");
                this.Close();

            }
        }
    }
}
