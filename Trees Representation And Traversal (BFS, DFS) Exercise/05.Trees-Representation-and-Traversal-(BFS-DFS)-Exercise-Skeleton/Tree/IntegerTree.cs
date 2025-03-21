namespace Tree
{
    using System.Collections.Generic;

    public class IntegerTree : Tree<int>, IIntegerTree
    {
        public IntegerTree(int key, params Tree<int>[] children)
            : base(key, children)
        {
        }

        public List<List<int>> PathsWithGivenSum(int sum)
        {
            var result = new List<List<int>>();

            var currPath = new LinkedList<int>();
            currPath.AddFirst(this.Key);
            int currSum = this.Key;
            this.Dfs(this, result, currPath, ref currSum, sum);

            return result;
        }

        private void Dfs(Tree<int> subtree, List<List<int>> result, LinkedList<int> currPath, ref int currSum, int wantedSum)
        {
            foreach (var child in subtree.Children)
            {
                currSum += child.Key;
                currPath.AddLast(child.Key);
                this.Dfs(child, result, currPath, ref currSum, wantedSum);
            }

            if (currSum == wantedSum)
            {
                result.Add(new List<int>(currPath));
            }

            currSum -= subtree.Key;
            currPath.RemoveLast();
        }

        public List<Tree<int>> SubTreesWithGivenSum(int sum)
        {
            var result = new List<Tree<int>>();

            var tree = this;
            int currSum = this.Key;
            this.Bfs(this, result, ref currSum, sum);

            return result;
        }

        private void Bfs(Tree<int> tree, List<Tree<int>> result, ref int currSum, int wantedSum)
        {
            var queue = new Queue<Tree<int>>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var currSubtree = queue.Dequeue();

                if (currSubtree.Children.Count == 0)
                {
                    currSum += currSubtree.Key;
                    if (currSum == wantedSum)
                    {
                        result.Add(currSubtree);
                        break;
                    }

                    currSum = 0;
                }

                foreach (var child in currSubtree.Children)
                {
                    currSum += child.Key;
                    queue.Enqueue(child);
                }

                currSum += currSubtree.Key;
                if (currSum == wantedSum)
                {
                    result.Add(currSubtree);
                    break;
                }

                currSum = 0;
            }
        }
    }
}
