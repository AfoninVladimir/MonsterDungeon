namespace Entity.InGame
{
	public class BaseObject
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
        public string? z_index { get; set; } = "0";
        public int? BackgroundPositionX { get; set; } = 0;
        public int? BackgroundPositionY { get; set; }
        public double? Rotation { get; set; }
        public string? id { get; set; }
        public string? Transform { get; set; }
        public BoxColliderObject BoxCollider { get; set; } = new BoxColliderObject();

		public int MaxHP { get; set; }
		public int HP { get; set; }
		public int Damage { get; set; }
		public string? Status { get; set; }


		public static int currentId = 0;
        public BaseObject()
        {

            currentId++;
            ID = currentId.ToString();

        }

    }
}
