using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using MBDChat.com.unice.mbds.mbdchat.model.message;
using System;
using System.Net;
using System.Net.Sockets;
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
            IpStarter.Items.Add(getIpLocal());
        }

        public string getIpLocal()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new System.Exception("Local IP Address Not Found!");
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs rea)
        {
            //Port
            try {
                controller.port = Int32.Parse(this.PortStarter.Text);
            } catch (FormatException e) {
                this.PortStarter.Text = "";
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

            controller.nickname = this.NameStarter.Text;
            controller.ipAddress = (string)this.IpStarter.SelectedValue;
            controller.addPair(new Pair(this.AddressIpNode.Text, portNode, this.NameNode.Text));

            this.Hide();
            new MainWindow(controller).Show();
        }
    }
}
