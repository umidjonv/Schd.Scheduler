namespace Schd.Common.Infrastructure.Models
{
    public interface IMessage<T>
    {
        T Message { get; set; }

    }

}
