namespace DreamServer
{
    public static class ConsoleUtils
    {
        public static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            int width = Console.WindowWidth;

            string border = new string('=', 44);
            string title = "DreamCraft MMORPG Server v1.0";

            WriteCentered(border, width);
            WriteCentered(title, width);
            WriteCentered(border, width);

            Console.ResetColor();
            AnimateDots("[INFO] Lancement du serveur", ConsoleColor.DarkYellow, 3);
        }

        private static void WriteCentered(string text, int width)
        {
            if (text.Length >= width)
            {
                Console.WriteLine(text);
                return;
            }
            int leftPadding = (width - text.Length) / 2;
            Console.WriteLine(new string(' ', leftPadding) + text);
        }

        private static void AnimateDots(string message, ConsoleColor color, int count)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            for (int i = 0; i < count; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
            Console.WriteLine();
            Console.ResetColor();
        }


        public static void Log(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}