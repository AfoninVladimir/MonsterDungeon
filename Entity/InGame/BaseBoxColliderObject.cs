using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.InGame
{
    public class BaseBoxColliderObject
    {
		public string ID { get; set; }
		public double? Width { get; set; }
		public double? Height { get; set; }
		public double? Left { get; set; }
		public double? Right { get; set; }
		public double? Top { get; set; }
		public double? Bottom { get; set; }
		public string? Position { get; set; }
		public string? ObjectType { get; set; }
		public string? BackgroundSize { get; set; }
		public string? BackgroundColor { get; set; }
		public string? BackgroundImage { get; set; }
		public string? BackgroundRepeat { get; set; }
		public string? Class { get; set; }
		public string? z_index { get; set; }
		public int? BackgroundPositionX { get; set; }
		public int? BackgroundPositionY { get; set; }
		public double? Rotation { get; set; }
		public string? id { get; set; }
		public string? Display { get; set; }
	}
}
