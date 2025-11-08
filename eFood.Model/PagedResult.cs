using System;
using System.Collections.Generic;
using System.Text;

namespace eFood.Model
{
    public class PagedResult<T>
    {
        public List<T> Result { get; set; }
        public int? Count { get; set; }

    }
}
