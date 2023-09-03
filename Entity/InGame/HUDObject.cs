using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.InGame
{
    public class HUDObject : BaseObject
    {
        public new string BackgroundSize { get; set; }
        public string BackgroundRepeat { get; set; }
        public int Top { get; set; }
        public HUDObject() : base()
        {
            Width = 340;
            Height = 100;
            BackgroundImage = "url(https://localhost:44319/images/HUD.png)";
            BackgroundSize = "contain";
            BackgroundRepeat = "no-repeat";
            Position = "absolute";
            Left = 0;
            Top = 0;
            Class = "HUD";
        }
    }
}
