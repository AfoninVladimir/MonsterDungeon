using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.InGame
{
    public class BoxColliderObject : BaseBoxColliderObject
    {
		public BoxColliderObject() : base() 
        {
            ID = "HitBox";
            BackgroundImage = "none";
            Position = "absolute";
            Class = "BoxCollider";
            ObjectType = "BoxCollider";
		}
    }
}
