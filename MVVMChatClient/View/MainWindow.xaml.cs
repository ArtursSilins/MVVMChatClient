using MVVMChatClient.Core;
using MVVMChatClient.Core.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace MVVMChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool Maximized { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Core.Interfaces.IFilePath filePath = new FilePath();
            Core.Model.FilePath.filePath = filePath;

            Core.Interfaces.IAlertMessages alertMessages = new AlertMessages();
            Core.Model.AlertMessages.alertMessages = alertMessages;

            MVVMChatClient.Core.Interfaces.IImageConverter imageConverter = new ImageConverter();
            MVVMChatClient.Core.Model.ConvertImage.imageConverter = imageConverter;

            //DataContext = new WindowsViewModel();

            Maximized = false;
        }

        private void Cloase_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (!Maximized)
            {
                this.WindowState = WindowState.Maximized;
                Maximized = true;
            }
            else if (Maximized)
            {
                this.WindowState = WindowState.Normal;
                Maximized = false;
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
