using System;

namespace UI
{
    public class UIAscii : IUIComponent
    {
        private string FilePath;
        
        public UIAscii(string filePath)
        {
            FilePath = filePath;
        }
        public void Print()
        {
            var lines = File.ReadAllLines(FilePath);
            foreach (string line in lines)
                Console. WriteLine(line);
        }
    }
}