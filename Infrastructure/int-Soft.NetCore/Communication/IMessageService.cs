using System.Threading.Tasks;

namespace int_Soft.NetCore.Communication
{
    public interface IMessageService
    {
        Task<MessageServiceResult> SendAsync(Message message);
    }
}