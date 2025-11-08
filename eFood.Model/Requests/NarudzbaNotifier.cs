using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model.Requests
{
    public class NarudzbaNotifier
    {
        public NarudzbaNotifier()
        {
        }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
