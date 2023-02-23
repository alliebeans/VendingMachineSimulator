using System;
using UI;
using System.Globalization;

namespace UIExtensions
{
    public class UIExtension
    {
        public static int Width = 86;
        public static int VendingMachineWidth = Width - 8;
        public static int Height = 28;
        public static void LeftScreenPad() => Console.Write("".PadLeft(4));
        public static void ProductPad(int offset = 0) => Console.Write("".PadLeft(16 - offset));
        public static void HeightPad(int heightPad) { for(int i = 0; i < heightPad; i++) Console.WriteLine(); }
        public static ConsoleColor BG = Console.BackgroundColor;
        public static ConsoleColor FG = Console.ForegroundColor;
        public static (ConsoleColor, ConsoleColor) InvertColor() => (Console.BackgroundColor = ConsoleColor.White, Console.ForegroundColor = ConsoleColor.Black);
        public static (ConsoleColor, ConsoleColor) GrayColor() => (Console.BackgroundColor = ConsoleColor.DarkGray, Console.ForegroundColor = ConsoleColor.Gray);
        public static void ClearLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, currentLineCursor);
        }
        public static void ClearStatusBar()
        {
            int currentColumnCursor = Console.GetCursorPosition().Left;
            int currentRowCursor = Console.GetCursorPosition().Top;
            Console.SetCursorPosition(0, Console.CursorTop-1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(currentColumnCursor, currentRowCursor);
        }
        private static CultureInfo SECulture => new CultureInfo("sv-SE"); 
        public static CultureInfo SE() => SECulture;
    }
}