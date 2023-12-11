namespace TicTacToe.Library.Data
{
	public struct Vector2
	{
		public static readonly Vector2 Zero = new Vector2(0, 0);

		public int X { get; private set; }
		public int Y { get; private set; }

		public Vector2(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return $"({X},{Y})";
		}

		public static Vector2 FromString(string representation)
		{
			representation = representation.Split('(')[1].Split(')')[0];
			return new Vector2(int.Parse(representation.Split(',')[0]), int.Parse(representation.Split(',')[1]));
		}

		public static Vector2 IndexToVector2(int index)
		{
			int x = index % 3;
			int y = index / 3;
			return new Vector2(x, y);
		}

		public static int Vector2ToIndex(Vector2 vector)
		{
			int x = vector.X;
			int y = vector.Y;
			return y * 3 + x;
		}
	}
}
