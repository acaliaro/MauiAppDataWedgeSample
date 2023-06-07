using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MauiAppDataWedgeSample.Messages
{
    public class DataWedgeMessage : ValueChangedMessage<string>
    {
        public DataWedgeMessage(string message) : base(message)
        {
        }
    }
}
