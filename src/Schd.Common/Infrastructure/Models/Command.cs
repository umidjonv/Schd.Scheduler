

using Newtonsoft.Json;

namespace Schd.Common.Infrastructure.Models
{
    public class Command: ICommand
    {
        public string Content { get; set; }
        
    }
}
