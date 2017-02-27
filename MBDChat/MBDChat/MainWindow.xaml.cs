using MBDChat.com.unice.mbds.mbdchat.model.clientServer;
using MBDChat.com.unice.mbds.mbdchat.model.message;
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

namespace MBDChat
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ChatRoomController controller =  ChatRoomController.Instance;
            controller.addPair(new Pair("127.0.0.1", 2323));
            controller.startUp();

            /*List<Pair> pairs = new List<Pair>();
            pairs.Add(new Pair("192.168.0.5", "2323"));
            pairs.Add(new Pair("192.168.0.45", "2323"));
            Message msg = new Message("HELLO", new HelloData("127.0.0.1", "2323", pairs));

            string json = ChatRoomController.toJson(msg);
            Console.WriteLine("json : " + json);*/
        }
    }
}
