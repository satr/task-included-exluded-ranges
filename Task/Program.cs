using System;
using System.Collections.Generic;

namespace Task
{
    class Program
    {
        private static AvlTree<int, Pair> tree;

        static void Main(string[] args)
        {
            tree = new AvlTree<int, Pair>();
            int num = 1;
//            Add(9, 100);
//            Add(10, 100);
//            Add(20, 200);
//            Pair a;
//            var key = 40;
//            var less = tree.SearchOrLess(key);
//            var greater = tree.SearchOrGreater(key);
//            
//            less = less;
            
//            Test("" + num++, new []{10, 100});
//            Test("" + num++, new[] {50, 5000,   10, 100});
//            Test("" + num++, new[] {10,100,   200,300});
//            Test("" + num++, new[] {10,100,   200,300,  400,500});
            Test("" + num++, new[] {10,100,   20,30,  70,200});
            Console.ReadKey();
        }

        private static void Test(string num, int[] includes)
        {
            Console.Out.WriteLine("-- " + num);
            for (int i = 0; i < includes.Length; i += 2)
                Add(includes[i], includes[i+1]);
            tree.DisplayTree();
//            Delete(25);
//            tree.DisplayTree();
            tree.Clear();
        }

        private static void Delete(int min)
        {
            tree.Delete(min);
        }

        private static void Add(int min, int max)
        {
            var nodesToDelete = new List<AvlTree<int, Pair>.AvlNode>();
            AvlTree<int, Pair>.AvlNode targedNode = null;
            var minNode = tree.SearchOrLess(min);
            if (minNode == null)
            {
                var greater = tree.SearchOrGreater(min);
                targedNode = tree.Insert(min, new Pair(min, max));
                if (greater == null //no root found
                    || greater.Value.Min > max)
                {
                    return;
                }
                JoinNodesByMax(max, targedNode, nodesToDelete, greater);
            } 
            else if (minNode.Value.Min == min)
            {
                targedNode = minNode;
                if (targedNode.Value.Max >= max)
                    return;
                var greater = tree.SearchOrGreater(targedNode.Value.Max);
                JoinNodesByMax(max, targedNode, nodesToDelete, greater);
            }
            else //minNode.Value.Min < min
            {
                targedNode = minNode.Value.Max < min 
                            ? tree.Insert(min, new Pair(min, max)) 
                            : minNode;
                var greater = tree.SearchOrGreater(targedNode.Value.Max);
                if (greater == null)
                {
                    //TODO
                }
                JoinNodesByMax(max, targedNode, nodesToDelete, greater);
            }

            foreach (var node in nodesToDelete)
            {
                tree.Delete(node.Key);
            }
        }

        private static void JoinNodesByMax(int max, AvlTree<int, Pair>.AvlNode targedNode, List<AvlTree<int, Pair>.AvlNode> nodesToDelete, AvlTree<int, Pair>.AvlNode greater)
        {
            while (greater != null)
            {
                if (greater.Value.Min > max)
                    return;
                if (greater.Value.Max <= max)
                {
                    nodesToDelete.Add(greater);
                    greater = tree.SearchOrGreater(greater.Value.Max);
                }
                else //greater.Value.Max > max
                {
                    targedNode.Value.Max = greater.Value.Max;
                    nodesToDelete.Add(greater);
                    return;
                }
            }
        }
    }
}
