namespace MBDChat.com.unice.mbds.mbdchat.model.clientServer
{
    public interface Receipter
    {
        void startListen(int port);
        void stopListen();
        void receiptMessage();
    }
}
