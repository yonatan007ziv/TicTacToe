namespace TicTacToe.Library
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            new Systems.Server().ServerLoop().GetAwaiter().GetResult();
        }
    }
}