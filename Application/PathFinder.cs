using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using IdaStar;

namespace Application
{
    [DebuggerDisplay("{X}, {Y}")]
    public class Position
    {
        private bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }

    public enum Field
    {
        Empty,
        Obstacle
    }
    
    public class PathFinder : IdaStar.Solver<Position>
    {
        private readonly Field[,] m;
        private readonly Position origin;
        private readonly Position destination;

        public PathFinder(Field[,] m, Position origin, Position destination)
            : base(origin)
        {
            this.m = m;
            this.origin = origin;
            this.destination = destination;
        }

        protected override bool IsGoal(Position node)
        {
            return Equals(node, destination);
        }

        protected override double Heuristic(Position node)
        {            
            var dx = Math.Abs(node.X - destination.X);
            var dy = Math.Abs(node.Y - destination.Y);

            return Math.Max(dx, dy);// + (Math.Sqrt(2) - 1 * Math.Min(dx, dy));
        }

        protected override IEnumerable<Position> Successors(Position node)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }
                    
                    if (m[node.X + dx, node.Y + dy] != Field.Obstacle)
                    {
                        yield return new Position(node.X + dx, node.Y + dy);
                    }
                }
            }
        }

        protected override double Cost(Position node, Position next)
        {
            var dx = Math.Abs(node.X - next.X);
            var dy = Math.Abs(node.Y - next.Y);

            //return Math.Sqrt(dx + dy);

            return dx + dy;
        }

        protected override void Dump(IEnumerable<Position> path)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            
            Console.WriteLine($"({origin.X}, {origin.Y}) => ({destination.X}, {destination.Y})");
            Console.WriteLine($"{m.GetLength(0)}x{m.GetLength(1)}");
            Console.WriteLine();
            
            for (int y = 0; y < m.GetLength(1); y++)
            {
                for (int x = 0; x < m.GetLength(0); x++)
                {
                    if (new Position(x, y).Equals(destination))
                    {
                        Console.Write('T');
                        continue;
                    }
                    
                    if (path.Contains(new Position(x, y)))
                    {
                        Console.Write('.');
                        continue;
                    }

                    switch (m[x, y])
                    {
                        case Field.Empty:
                            Console.Write(' ');
                            break;
                        case Field.Obstacle:
                            Console.Write('#');
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                
                Console.WriteLine();
            }
            
            Console.WriteLine();
            Console.CursorVisible = true;
        }
    }
}