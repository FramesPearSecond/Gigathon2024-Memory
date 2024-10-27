using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Memory
{
    internal static class SavesMenager
    {
        static string fileName;
        static string SavePath = "..\\..\\saves\\";

        static Dictionary<string, State> States = new Dictionary<string, State>
        {
            { "Covered", State.Covered },
            { "CoveredSelected", State.CoveredSelected },
            { "Uncovered", State.Uncovered },
            { "UncoveredSelected", State.UncoveredSelected },
            { "Choosed", State.Choosed },
            { "ChoosedSelected", State.ChoosedSelected }
        };

        static Dictionary<string, ConsoleColor> Colors = new Dictionary<string, ConsoleColor>
        {
            { "White", ConsoleColor.White },
            { "Green", ConsoleColor.Green },
            { "DarkCyan", ConsoleColor.DarkCyan },
            { "Magenta", ConsoleColor.Magenta },
            { "DarkYellow", ConsoleColor.DarkYellow },
            { "DarkRed", ConsoleColor.DarkRed },
            { "Yellow", ConsoleColor.Yellow }
        };

        static char[] loadFrames = new char[]
        {
            '\u25CB', '\u25D4', '\u25D1', '\u25D5', '\u25CF'
        };

        public static void Save(Card[,] cards, Player p1, Player p2, bool active)
        {
            fileName = string.Format("{0}_vs_{1}.txt", p1.name, p2.name);
            string savePath = SavePath + fileName;

            try
            {
                using (StreamWriter saveWriter = new StreamWriter(savePath, false, Encoding.UTF8))
                {
                    foreach (Card card in cards)
                    {
                        int id = card.id;
                        string state = "";
                        string color = "";
                        char shape = card.shape;

                        foreach (KeyValuePair<string, State> stateDict in States)
                        {
                            if (card.state == stateDict.Value)
                            {
                                color = stateDict.Key;
                            }
                        }

                        foreach (KeyValuePair<string, ConsoleColor> colorDict in Colors)
                        {
                            if(card.color == colorDict.Value)
                            {
                                state = colorDict.Key;
                            }
                        }

                        saveWriter.Write("{0},{1},{2},{3};",id, state, color, shape);
                    }
                    saveWriter.Write('\n');

                    saveWriter.WriteLine("{0},{2}\n{1},{3}",p1.name,p2.name,p1.points,p2.points);

                    saveWriter.WriteLine((active)? 1 : 0);

                    saveWriter.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Zapisywanie nie powiodło się: " + ex.ToString());
                Console.ReadKey();
            }
            finally
            {
                
                foreach(char frame in loadFrames)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0} zapisywanie".PadLeft(5), frame);
                    Thread.Sleep(300);
                }
                Console.ResetColor();
            }
        }

        public static string[] MenuLoad()
        {
            string[] files = Directory.GetFiles(SavePath, "*.txt");
            string[] filesName = new string[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                filesName[i] = Path.GetFileName(files[i]);
            }
            return filesName;
        }
    }
}
