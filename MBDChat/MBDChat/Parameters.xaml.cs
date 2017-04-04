using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Windows;

namespace MBDChat
{
    /// <summary>
    /// Logique d'interaction pour Parameters.xaml
    /// </summary>
    public partial class Parameters : Window
    {
        private ChatRoomController controller = ChatRoomController.Instance;

        public Parameters()
        {
            InitializeComponent();
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs rea)
        {
            //Port
            try {
                controller.port = Int32.Parse(this.portStarter.Text);
            } catch (FormatException e) {
                this.portStarter.Text = "";
                return;
            }

            //New Address ip
            int portNode = -1;
            try {
                portNode = Int32.Parse(this.PortNode.Text);
            } catch (FormatException e) {
                this.PortNode.Text = "";
                return;
            }

            controller.addPair(new Pair(this.AddressIpNode.Text, portNode, this.NameNode.Text));

            this.Hide();
            new MainWindow(controller).Show();
        }
    }
}
