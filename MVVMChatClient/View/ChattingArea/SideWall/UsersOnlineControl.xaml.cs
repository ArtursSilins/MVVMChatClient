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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVMChatClient
{
    /// <summary>
    /// Interaction logic for UsersOnlineControl.xaml
    /// </summary>
    public partial class UsersOnlineControl : UserControl
    {
        public static int counter { get; set; }

        public UsersOnlineControl()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GetTopLevelControl(this);
        }

        /// <summary>
        /// Get parent control where is storyboard.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private void GetTopLevelControl(DependencyObject control)
        {
            DependencyObject tmp = control;
            DependencyObject parent = null;
            while ((tmp = VisualTreeHelper.GetParent(tmp)) != null)
            {

                if(tmp == tmp as PublicChat)
                {
                    PublicChat publicChat = tmp as PublicChat;
                    ((BeginStoryboard)publicChat.Resources["UnloadAnimation"]).Storyboard.Begin();
                    break;
                }

                parent = tmp;

            }
            
        }
    }
}
