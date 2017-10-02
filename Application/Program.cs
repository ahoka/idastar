using System;
using System.Collections.Generic;
using System.IO;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("Hello World!");

            if (args.Length != 1)
            {
                Console.WriteLine("Not enough arguments");
                return;
            }

            Field[,] map;
            Position target = null;
            Position origin = null;
            
            using (var f = File.OpenText(args[0]))
            {
                int rows = 0;
                var l = f.ReadLine();
                if (l == null)
                {
                    Console.WriteLine("Empty imput file");
                    return;
                }

                var m = new List<Field[]>();
                var columns = l.Length;

                while (l != null)
                {
                    if (l.Length != columns)
                    {
                        Console.WriteLine("Inconsistent input file");
                    }
                    
                    var row = new Field[columns];

                    for (int col = 0; col < columns; col++)
                    {
                        switch (l[col])
                        {
                            case '#':
                                row[col] = Field.Obstacle;
                                break;
                            case ' ':
                                row[col] = Field.Empty;
                                break;
                            case 'T':
                                target = new Position(col, rows);
                                break;
                            case '@':
                                origin = new Position(col, rows);
                                break;
                            default:
                                Console.WriteLine($"Ignoring junk char: '{l[col]}'");
                                break;
                        }
                    }

                    m.Add(row);
                    rows++;
                    l = f.ReadLine();
                }
                
                map = new Field[columns, rows];

                for (int x = 0; x < columns; x++)
                {
                    for (int y = 0; y < rows; y++)
                    {
                        map[x, y] = m[y][x];
                    }
                }
            }

            if (origin != null && target != null)
            {
                Console.Clear();
                
                var solver = new PathFinder(map, origin, target);
                var (solution, cost) = solver.Search();
                
                Console.WriteLine($"Solution cost: {cost}");
                foreach (var position in solution)
                {
                    Console.WriteLine($"{position.X}, {position.Y}");
                }
            }
        }
    }
}
