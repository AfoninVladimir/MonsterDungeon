using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.InGame
{
    public class ItemObject : BaseObject
    {
        public ItemObject() : base()
        {
            Width = 32;
            Height = 32;
            Position = "absolute";
            Class = "itemForInventory";
            ObjectType = "Item";

		}
    }
}
