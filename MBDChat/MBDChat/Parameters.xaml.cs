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

            //Add ip client
            IpStarter.Items.Add(getIpLocal());

            //Set checkbox to true
            ContainNode.IsChecked = true;
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
                controller.port = Int32.Parse(PortStarter.Text);
            } catch (FormatException e) {
                PortStarter.Text = "";
                return;
            }

            controller.nickname = NameStarter.Text;
            controller.ipAddress = (string)IpStarter.SelectedValue;

            //New Address ip
            if (ContainNode.IsChecked.Value)
            {
                int portNode = -1;
                try
                {
                    portNode = Int32.Parse(PortNode.Text);
                }
                catch (FormatException e)
                {
                    PortNode.Text = "";
                    return;
                }
                controller.addParticipant(NameNode.Text);
                controller.addPair(new Pair(AddressIpNode.Text, portNode));
            }
            
            Hide();
            new MainWindow(controller).Show();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (ContainNode.IsChecked.Value)
            {
                AddressIpNode.IsEnabled = true;
                PortNode.IsEnabled = true;
                NameNode.IsEnabled = true;
            }
            else
            {
                AddressIpNode.IsEnabled = false;
                PortNode.IsEnabled = false;
                NameNode.IsEnabled = false;
            }
        }
    }
}
