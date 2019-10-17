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

            DeviceTBox.Text = selected.DeviceModel;
            SerialTBox.Text = selected.DeviceSerial;
            DescriptionTBox.Text = selected.Description;

            RpTBox.Text = selected.Rp;
            SoTBox.Text = selected.So;
            DescriptionTBox.Text = selected.Description;
        }
    }
}
