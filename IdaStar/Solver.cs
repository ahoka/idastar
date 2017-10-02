using System;
using System.Collections.Generic;
using System.Linq;

namespace IdaStar
{
    public abstract class Solver<T>
    {
        private readonly T root;

        protected Solver(T root)
        {
            this.root = root;
        }

        protected abstract bool IsGoal(T node);

        protected abstract double Heuristic(T node);

        protected abstract IEnumerable<T> Successors(T node);

        protected abstract double Cost(T node, T next);

        protected abstract void Dump(IEnumerable<T> path);

        public (IList<T>, double) Search()
        {
            var bound = Heuristic(root);
            var path = new HashStack<T>();
            path.Push(root);

            var i = 1;
            
            while (true)
            {
                Console.WriteLine($"Iteration #{i++}, Bound: {bound}");
                
                var (found, t) = Search(path, 0, bound);
                if (found)
                {
                    var solutionSteps = path.ToList();
                    
                    return (solutionSteps, bound);
                }

                if (double.IsInfinity(t))
                {
                    throw new SolutionNotFoundException("Solution not found");
                }
                
                bound = t;
            }
        }

        private (bool, double) Search(HashStack<T> path, double cost, double bound)
        {
            var node = path.Peek();

            var estimatedCost = cost + Heuristic(node);
            
            if (estimatedCost > bound)
            {
                return (false, estimatedCost);
            }
            
            Dump(path);
            
            if (IsGoal(node))
            {
                Dump(path);
                return (true, cost);
            }

            var min = double.PositiveInfinity;

            foreach (var successor in Successors(node).OrderBy(Heuristic))
            {
                if (!path.Contains(successor))
                {
                    path.Push(successor);
                    
                    var (found, t) = Search(path, cost + Cost(node, successor), bound);

                    if (found)
                    {
                        return (true, t);
                    }

                    if (t < min)
                    {
                        min = t;
                    }

                    path.Pop();
                }
            }

            return (false, min);
        }
    }
}
