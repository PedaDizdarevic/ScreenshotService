using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Screenshot
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public DateTime Cerated { get; set; }
        public byte[] Image { get; set; }
    }
}
