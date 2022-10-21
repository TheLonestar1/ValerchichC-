using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ConsoleApp1
{
    internal class Program
    {
        static Players player;
        static List<string> lines = new List<string>();
        static void Main(string[] args)
        {
            player = new Players();
            try
            {
                Load();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Нет сохранения");
                player = new Players();
            }
            try
            {
                using (StreamReader stream = new StreamReader("config.txt"))
                {

                    while (!stream.EndOfStream)
                    {
                         lines.Add(stream.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Нет конфига");
                if (lines == null)
                    return;
            }
            while (true)
            {
                Console.WriteLine("Твоя статистика: ");
                Console.WriteLine($"Здоровье:{player.health}");
                Console.WriteLine($"Алкоголь:{player.mana}");
                Console.WriteLine($"Жизнерадостность:{player.Happnies}");
                Console.WriteLine($"Усталость:{player.fatigue}");
                Console.WriteLine($"Деньги:{player.money}");
                int j = 0;
                foreach (string line in lines)
                {
                    Console.WriteLine( j + ") " + line.Split(':').First());
                    j++;
                }

                Console.Write("Введите номер дейтсвия: ");
                string chouse =  Console.ReadLine();
                if(chouse == "S")
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter("save.txt"))
                        {
                            writer.WriteLine($"Здоровье:{player.health}");
                            writer.WriteLine($"Алкоголь:{player.mana}");
                            writer.WriteLine($"Жизнерадостность:{player.Happnies}");
                            writer.WriteLine($"Усталость:{player.fatigue}");
                            writer.WriteLine($"Деньги:{player.money}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Не удолось сохранить");
                    }
                    continue;
                }
                try
                {
                    string[] parametrs = lines[Convert.ToInt32(chouse)].Split(':').ToList()[1].Split(',');
                    if (lines[Convert.ToInt32(chouse)].Split(':').First() == "Пойти на работу"  && (player.mana > 50 || player.fatigue > 10))
                    {
                        Console.WriteLine("Вы слишком устали");
                        continue;
                    }
                    if (lines[Convert.ToInt32(chouse)].Split(':').First() == "Петь в метро" && player.mana > 50 && player.mana < 70)
                    {
                        string[] parametrs1 = lines[Convert.ToInt32(chouse)].Split(':').ToList()[1].Split(',');
                        player.Happnies += Convert.ToInt32(parametrs1[0]);
                        player.mana += Convert.ToInt32(parametrs1[1]);
                        player.fatigue += Convert.ToInt32(parametrs1[2]);
                        player.money += Convert.ToInt32(parametrs1[3]);
                        player.health += Convert.ToInt32(parametrs1[4]);

                        continue;
                    }
                    else if (lines[Convert.ToInt32(chouse)].Split(':').First() == "Петь в метро")
                    {
                        string[] parametrs2 = lines[Convert.ToInt32(chouse)].Split(':').ToList()[1].Split(',');
                        player.Happnies += Convert.ToInt32(parametrs2[0]);
                        player.mana += Convert.ToInt32(parametrs2[1]);
                        player.fatigue += Convert.ToInt32(parametrs2[2]);
                        player.money += 0;
                        player.health += Convert.ToInt32(parametrs2[4]);
                        continue;
                    }
                    if(lines[Convert.ToInt32(chouse)].Split(':').First() == "Спать")
                    {
                        if(player.mana > 30)
                            player.health -= Convert.ToInt32(parametrs[4]);
                        if(player.mana < 70) player.Happnies -= Convert.ToInt32(parametrs[0]);
                    }  
                    player.Happnies += Convert.ToInt32(parametrs[0]);
                    player.mana += Convert.ToInt32(parametrs[1]);
                    player.fatigue += Convert.ToInt32(parametrs[2]);
                    player.money += Convert.ToInt32(parametrs[3]);
                    player.health += Convert.ToInt32(parametrs[4]);
                    if (player.health > 100)
                        player.health = 100;
                    if(player.health <= 0)
                    {
                        GameOver();
                        continue;
                    }
                    if(player.mana < 0)
                    {
                        player.mana = 0;
                    }
                    if (player.mana > 100)
                        player.mana = 100;
                    if(player.fatigue < 0)
                    {
                        player.fatigue = 0;
                    }
                    if (player.fatigue > 100)
                    {
                        GameOver();
                        continue;
                    }
                    if(player.money < -100)
                    {
                        GameOver();
                        continue;
                    }
                    if (player.Happnies > 10)
                    {
                        player.Happnies = 10;
                    }
                    if(player.Happnies < -10)
                    {
                        GameOver();
                        continue;
                    }


                    System.Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка в конфигурации события ");
                    return;
                }
            }

            void GameOver()
            {
                Console.WriteLine("Вы проиграли если хотите продолжить заново нажмите R или начать с последнего сохрянанеия L");
                string chousen = Console.ReadLine();
                if (chousen == "R")
                {
                    player = new Players();
                }
                else if (chousen == "L")
                {
                    Load();
                }
                else
                {
                    GameOver();
                }
            }
            void Load()
            {
                using (StreamReader stream = new StreamReader("save.txt"))
                {
                    player.health = Convert.ToInt32(stream.ReadLine().Split(':').ToList()[1]);
                    player.mana = Convert.ToInt32(stream.ReadLine().Split(':').ToList()[1]);
                    player.Happnies = Convert.ToInt32(stream.ReadLine().Split(':').ToList()[1]);
                    player.fatigue = Convert.ToInt32(stream.ReadLine().Split(':').ToList()[1]);
                    player.money = Convert.ToInt32(stream.ReadLine().Split(':').ToList()[1]);
                }
            }
        }



    }
}
