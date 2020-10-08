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
        public MainWindow()
        {
            InitializeComponent();

            Core.Interfaces.IFilePath filePath = new FilePath();
            Core.Model.FilePath.filePath = filePath;

            Core.Interfaces.IAlertMessages alertMessages = new AlertMessages();
            Core.Model.AlertMessages.alertMessages = alertMessages;

            MVVMChatClient.Core.Interfaces.IImageConverter imageConverter = new ImageConverter();
            MVVMChatClient.Core.Model.ConvertImage.imageConverter = imageConverter;

            DataContext = new WindowsViewModel();
        }
        
    }
}
