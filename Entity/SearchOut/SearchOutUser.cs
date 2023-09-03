using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.SearchOut
{
    public class SearchOutUser
    {
        public int Count { get; set; }
        public int Total { get; set; }
        public List<Entity.User> Users { get; set; }
    }
}
