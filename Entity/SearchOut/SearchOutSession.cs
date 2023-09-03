using System;
using System.Collections.Generic;


namespace Entity.SearchOut
{
    public class SearchOutSession
    {
        public int Count { get; set; }
        public int Total { get; set; }
        public List<Entity.Session> Sessions { get; set; }
    }
}
