namespace TicTacToe.Server
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            new Systems.Server().ServerLoop().GetAwaiter().GetResult();
        }
    }
}