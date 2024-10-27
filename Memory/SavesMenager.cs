using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Memory
{
    internal static class SavesMenager
    {
        static string fileName;
        static string SavePath = "..\\..\\saves\\";
        static string[] Files = Directory.GetFiles(SavePath, "*.txt");

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
                        char shape = card.shape;
                        string state = "";
                        string color = "";

                        foreach (KeyValuePair<string, ConsoleColor> colorDict in Colors)
                        {
                            if (card.color == colorDict.Value)
                            {
                                color = colorDict.Key;
                            }
                        }

                        foreach (KeyValuePair<string, State> stateDict in States)
                        {
                            if (card.state == stateDict.Value)
                            {
                                state = stateDict.Key;
                            }
                        }
                        

                        saveWriter.Write("{0},{1},{2},{3};",id, shape, color, state);
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
            string[] filesName = new string[Files.Length];

            for (int i = 0; i < Files.Length; i++)
            {
                filesName[i] = Path.GetFileName(Files[i]);
            }
            return filesName;
        }

        public static void Load(int fileNumber, ref Board board, ref Player player1, ref Player player2, ref bool active)
        {
            string file = Files[fileNumber];

            try
            {
                using (StreamReader saveLoader = new StreamReader(file))
                {
                    string cardsLine = saveLoader.ReadLine();
                    cardsLine = cardsLine.Substring(0, cardsLine.Length - 1);
                    string[] rawCards = cardsLine.Split(';');
                    string[][] dataCards = new string[rawCards.Length][];

                    int lenght = (int)Math.Sqrt(rawCards.Length);

                    Card[,] cards = new Card[lenght, lenght];

                    string p1Line = saveLoader.ReadLine();
                    string p2Line = saveLoader.ReadLine();
                    string activeLine = saveLoader.ReadLine();

                    for (int i = 0; i < rawCards.Length; i++)
                    {
                        dataCards[i] = rawCards[i].Split(',');
                    }

                    int counter = 0;

                    foreach (string[] data in dataCards)
                    {
                        //Console.WriteLine("{6} {4}:{5} id:{0}, shape:{1}, color:{2}, state:{3}",
                        //    data[0], data[1], data[2], data[3], counter / lenght, counter % lenght, counter);

                        int id = int.Parse(data[0]);
                        char shape = Char.Parse(data[1]);
                        ConsoleColor color = Colors[data[2]];
                        State state = States[data[3]];

                        cards[(counter / lenght), (counter % lenght)] = new Card(id, shape, color, state);

                        counter++;
                    }

                    board = new Board(cards, lenght);

                    string[] p1 = p1Line.Split(',');
                    player1 = new Player(p1[0], int.Parse(p1[1]));

                    string[] p2 = p2Line.Split(',');
                    player2 = new Player(p2[0], int.Parse(p2[1]));

                    active = int.Parse(activeLine) != 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
        }
    }
}
