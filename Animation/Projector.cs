using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;


namespace Animation
{
    internal class Projector
    {
        string[] frames;

        Pixel[] pixelReferences = new Pixel[]
        {
            //skin
            new Pixel(Color.FromArgb(255, 221, 189), ConsoleColor.Yellow, ConsoleColor.Yellow, true),
            new Pixel(Color.FromArgb(228, 197, 168), ConsoleColor.Yellow, ConsoleColor.DarkYellow, false),
            //jacket
            new Pixel(Color.FromArgb(75, 91, 171), ConsoleColor.Blue, ConsoleColor.Blue, true),
            new Pixel(Color.FromArgb(67, 80, 149), ConsoleColor.Blue, ConsoleColor.DarkBlue, false),
            //pants
            new Pixel(Color.FromArgb(52, 52, 54), ConsoleColor.Green, ConsoleColor.Green, true),
            new Pixel(Color.FromArgb(43, 43, 44), ConsoleColor.Green, ConsoleColor.DarkGreen, false),
            //boots
            new Pixel(Color.FromArgb(0, 0, 0), ConsoleColor.DarkGray, ConsoleColor.DarkGray, true),
            //hat
            new Pixel(Color.FromArgb(208, 212, 213), ConsoleColor.Gray, ConsoleColor.Gray, true),
            new Pixel(Color.FromArgb(201, 205, 206), ConsoleColor.DarkGray, ConsoleColor.Gray, true),
            new Pixel(Color.FromArgb(193, 196, 197), ConsoleColor.Gray, ConsoleColor.DarkGray, false),
            //hair
            new Pixel(Color.FromArgb(165, 123, 83), ConsoleColor.Red, ConsoleColor.Red, true),
            new Pixel(Color.FromArgb(152, 113, 76), ConsoleColor.Red, ConsoleColor.DarkRed, false),
        };

        public Projector(string path)
        {
            frames = Directory.GetFiles(path, "*.png");

            foreach (string imgPath in frames)
            {
                Console.WriteLine(Path.GetFileName(imgPath));
            }
        }
    }
}
