
using System;
using System.Drawing;

namespace Animation
{
    internal class Pixel
    {
        public Color pixelColor;
        public ConsoleColor charColor;
        public ConsoleColor backColor;
        public char character;

        public Pixel(Color pixelColor, ConsoleColor charColor, ConsoleColor backColor, bool c)
        {
            this.pixelColor = pixelColor;
            this.charColor = charColor;
            this.backColor = backColor;
            this.character = (c) ? '■' : '▧';
        }
    }
}
