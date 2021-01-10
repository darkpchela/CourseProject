using System.IO;

namespace BusinessLayer.Models
{
    public class CreateResourceModel
    {
        public string Name { get; set; }

        public Stream FileStream { get; set; }
    }
}