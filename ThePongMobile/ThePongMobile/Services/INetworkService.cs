namespace ThePongMobile.Services
{
    public interface INetworkService
    {
        void SendMessage(int direction);
        int ReceiveScore();
    }
}