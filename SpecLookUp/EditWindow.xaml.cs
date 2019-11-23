using System.Windows;
using SpecLookUp.DAL;
using SpecLookUp.Model;

namespace SpecLookUp
{
    /// <summary>
    ///     Logika interakcji dla klasy EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow(Device selected)
        {
            InitializeComponent();
            SelectedLog = selected;
            TypeTBox.Text=SelectedLog.LogType;
            DeviceTBox.Text = SelectedLog.DeviceModel;
            SerialTBox.Text = SelectedLog.DeviceSerial;
            DescriptionTBox.Text = SelectedLog.Description;

            RpTBox.Text = SelectedLog.Rp;
            SoTBox.Text = SelectedLog.So;
            LabelTBox.Text = SelectedLog.OsLabel;
            CmarTBox.Text = SelectedLog.OsCmar;
            CommentsTBox.Text = SelectedLog.Comments;
        }

        public Device SelectedLog { get; set; }

        private void UpdateSelectedDevice()
        {
            SelectedLog.Rp = RpTBox.Text.Trim();
            SelectedLog.So = SoTBox.Text.Trim();
            SelectedLog.OsLabel = LabelTBox.Text.Trim();
            SelectedLog.OsCmar = CmarTBox.Text.Trim();
            SelectedLog.Comments = CommentsTBox.Text.Trim();
            SelectedLog.LogType=TypeTBox.Text.Trim();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedDevice();

            var updateSucceed = MysqlWorker.UpdateDevice(SelectedLog);

            if (updateSucceed)
            {
                MessageBox.Show("Log has been updated.");
                Close();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var hideSucceed = MysqlWorker.HideDevice(SelectedLog.Id.ToString());

            if (hideSucceed)
            {
                MessageBox.Show("Log has been removed.");
                Close();
            }
        }
    }
}