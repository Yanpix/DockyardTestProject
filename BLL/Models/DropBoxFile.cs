using System;

namespace BLL.Models
{
    public class DropBoxFile
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Size { get; set; }
        public string Icon { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}