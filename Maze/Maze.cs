using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

/*
 * 2018年10月29日
 * 实现了读取功能
 * 计划使用链表保存可走路径进行遍历
 * 
 * 2018年11月1日
 * 换用了BFS/DFS算法，实现了最短路径寻址，BFS/DFS源码来自CSDN论坛：
 * 作者：D_T 
 * 来源：CSDN 
 * 原文：https://blog.csdn.net/dream_dt/article/details/79275777 
 * 
 * 最后为废弃的回溯法，暂时没能力解决，四面可寻路的情况下会无限循环
 */
namespace Maze
{

    class Maze
    {
        class WeiZhi
        {
            public int x;
            public int y;
            public int di;
        }
        static int H = 9;//定义行数
        static int L = 16;//定义列
        static List<WeiZhi> JiLu = new List<WeiZhi>();

        static bool DuQu(out String[,] output)
        #region//读取文件中的迷宫
        {
            string strLine;
            String[,] duru = new String[H, L];
            try
            {
                FileStream aFile = new FileStream("maze.txt", FileMode.Open);
                StreamReader sr = new StreamReader(aFile);

                for (int i = 0; i < H; i++)
                {
                    int j = 0;
                    strLine = sr.ReadLine();
                    string[] word = strLine.Split(new char[] { '\t' });
                    foreach (string temp in word)
                    {
                        duru[i, j] = temp;
                        j++;
                    }
                }
                output = duru;
                sr.Close();
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine("An IOException has been thrown!");
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
                output = null;
                return false;
            }
        }
        #endregion
        public class MyPoint
        {
            public MyPoint parent { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public MyPoint(int a, int b)
            {
                this.X = a;
                this.Y = b;
            }
        }
        static void WritePath(MyPoint p)
        {
            if (p.parent != null)
            {
                WritePath(p.parent);
                WeiZhi buffer = new WeiZhi();
                buffer.x = p.X;
                buffer.y = p.Y;
                JiLu.Add(buffer);
                Console.Write("(" + p.X + "," + p.Y + ")-->");
            }
        }
        private static WeiZhi jieShu;
        private static WeiZhi kaiShi;

        private static WeiZhi KaiShi { get => kaiShi; set => kaiShi = value; }
        private static WeiZhi JieShu { get => jieShu; set => jieShu = value; }

        static void Main(string[] args)
        {
            
            KaiShi = new WeiZhi();
            JieShu = new WeiZhi();
            String[,] saveMaze = new String[H, L];
            int[,] arr = new int[H, L];

            DuQu(out saveMaze);
            //寻找起止点
            for (int i = 0; i < L; i++)
            {
                for (int j = 0; j < H; j++)
                {
                    if (saveMaze[j, i] == "S")
                    {
                        saveMaze[j, i] = "0";
                        KaiShi.x = j;
                        KaiShi.y = i;
                    }
                    if (saveMaze[j, i] == "E")
                    {
                        saveMaze[j, i] = "0";
                        JieShu.x = j;
                        JieShu.y = i;
                    }
                }
            }
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < L; j++)
                {
                    arr[i, j] = int.Parse(saveMaze[i, j]);
                }
            }


            MyPoint p = new MyPoint(KaiShi.x, KaiShi.y);
            p.parent = null;
            bool a = BFS(p, arr);
            foreach (WeiZhi res in JiLu)
            {
                arr[res.x, res.y] = 2;
                //Console.Write(res.x+""+res.y+"\t");
            }
            arr[JieShu.x, JieShu.y] = 2;
            arr[KaiShi.x, KaiShi.y] = 2;
            Console.WriteLine();
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < L; j++)
                {
                    if (arr[i, j] == 1)
                    {
                        Console.Write("■");
                    }
                    else if (arr[i, j] == 2)
                    {
                        Console.Write("◇");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
            Console.ReadLine();
            //////////////////////END////////////////
        }

        static void DFS(MyPoint qp, int[,] data)
        {
            for (int i = -1; i < 2; i++) //遍历可以到达的节点
            {
                for (int j = -1; j < 2; j++)
                {
                    if ((qp.X + i >= 0) && (qp.X + i < L) && (qp.Y + j >= 0) && (qp.Y + j < H) && (qp.X + i == qp.X || qp.Y == qp.Y + j)) //是否越界 只遍历上下左右
                    {
                        if (data[qp.X + i, qp.Y + j] == 0)
                        {
                            if (qp.X + i == JieShu.x && qp.Y + j == JieShu.y)  //是否为终点
                            {
                                Console.WriteLine("");
                                Console.Write("DFS:");
                                WritePath(qp); //递归输出路径
                                Console.Write("({0},{1})",JieShu.x,JieShu.y);
                                Console.WriteLine();
                                return;
                                
                            }
                            else
                            {
                                MyPoint temp = new MyPoint(qp.X + i, qp.Y + j);   //加入队列
                                temp.parent = qp;
                                data[qp.X + i, qp.Y + j] = -1;
                                DFS(temp, data);
                            }
                        }
                    }
                }
            }
        }


        static bool BFS(MyPoint p, int[,] data)
        {
            Queue<MyPoint> q = new Queue<MyPoint>();
            data[0, 0] = -1;
            q.Enqueue(p);
            while (q.Count > 0)
            {
                MyPoint qp = (MyPoint)q.Dequeue();
                for (int i = -1; i < 2; i++) //遍历可以到达的节点
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if ((qp.X + i >= 0) && (qp.X + i < H) && (qp.Y + j >= 0) && (qp.Y + j < L) && (qp.X + i == qp.X || qp.Y == qp.Y + j)) //是否越界 只遍历上下左右
                        {
                            if (data[qp.X + i, qp.Y + j] == 0)
                            {
                                if (qp.X + i == JieShu.x && qp.Y + j == JieShu.y)  //是否为终点
                                {
                                    WritePath(qp); //递归输出路径
                                    Console.Write("({0},{1})", JieShu.x, JieShu.y);
                                    Console.WriteLine();
                                    return true;
                                }
                                else
                                {
                                    MyPoint temp = new MyPoint(qp.X + i, qp.Y + j);   //加入队列
                                    data[qp.X + i, qp.Y + j] = -1;
                                    temp.parent = qp;
                                    q.Enqueue(temp);
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }



        ///////////废弃的回溯法
        //            for (int i = 0; i < 12; i++)
        //            {
        //                for (int j = 0; j < 12; j++)
        //                {
        //                    if (arr[i, j] == 0)
        //                    {
        //                        Console.Write("  ");
        //                    }
        //                    else if (arr[i, j] == 1)
        //                    {
        //                        Console.Write("■");
        //                    }
        //                    else
        //                    {
        //                        Console.Write("◇");
        //                    }
        //                }
        //                Console.WriteLine();
        //            }
        //            //
        //            Stack stack = new Stack();
        //            stack.push(1, 1);
        //            bool temp = true;
        //            while (temp)
        //            {
        //                int x, y;
        //                Direction fx;
        //                //上
        //                if (GetXY(arr, stack, Direction.Left, stack.Top.X, stack.Top.Y, out x, out y))
        //                {
        //                    fx = Direction.Right;
        //                    stack.Top.fxList.Add(Direction.Left);
        //                }
        //                //右
        //                else if (GetXY(arr, stack, Direction.Down, stack.Top.X, stack.Top.Y, out x, out y))
        //                {
        //                    fx = Direction.Up;
        //                    stack.Top.fxList.Add(Direction.Down);
        //                }
        //                //下
        //                else if (GetXY(arr, stack, Direction.Right, stack.Top.X, stack.Top.Y, out x, out y))
        //                {
        //                    fx = Direction.Left;
        //                    stack.Top.fxList.Add(Direction.Right);
        //                }
        //                //左
        //                else if (GetXY(arr, stack, Direction.Up, stack.Top.X, stack.Top.Y, out x, out y))
        //                {
        //                    fx = Direction.Down;
        //                    stack.Top.fxList.Add(Direction.Up);
        //                }
        //                else//没有路 出栈
        //                {
        //                    stack.pop();
        //                    continue;
        //                }
        //                stack.push(x, y);
        //                stack.Top.fxList.Add(fx);
        //                if (x == 10 && y == 10)
        //                {
        //                    temp = false;
        //                }
        //                arr[x, y] = 2;
        //                Console.Clear();
        //                for (int i = 0; i < 12; i++)
        //                {
        //                    for (int j = 0; j < 12; j++)
        //                    {
        //                        if (arr[i, j] == 0)
        //                        {
        //                            Console.Write("  ");
        //                        }
        //                        else if (arr[i, j] == 1)
        //                        {
        //                            Console.Write("■");
        //                        }
        //                        else
        //                        {
        //                            Console.Write("◇");
        //                        }
        //                    }
        //                    Console.WriteLine();
        //                }
        //                //
        //                Console.WriteLine(x + "," + y);
        //                Thread.Sleep(10);
        //            }

        //            while (!stack.IsEmpty())
        //            {
        //                arr[stack.Top.X, stack.Top.Y] = 2;
        //                stack.pop();
        //            }
        //            Console.Clear();
        //            for (int i = 0; i < 12; i++)
        //            {
        //                for (int j = 0; j < 12; j++)
        //                {
        //                    if (arr[i, j] == 0)
        //                    {
        //                        Console.Write("  ");
        //                    }
        //                    else if (arr[i, j] == 1)
        //                    {
        //                        Console.Write("■");
        //                    }
        //                    else
        //                    {
        //                        Console.Write("◇");
        //                    }
        //                }
        //                Console.WriteLine();
        //            }
        //            Console.ReadLine();



        //        /// <summary>
        //        /// 得到这个点 所指向的下一个点
        //        /// </summary>
        //        /// <param name="stack"></param>
        //        /// <param name="fx"></param>
        //        /// <param name="x"></param>
        //        /// <param name="y"></param>
        //        /// <param name="_x"></param>
        //        /// <param name="_y"></param>
        //        /// <returns></returns>
        //        static bool GetXY(int[,] arr, Stack stack, Direction fx, int x, int y, out int _x, out int _y)
        //        {
        //            _x = x;
        //            _y = y;
        //            switch (fx)
        //            {
        //                case Direction.Up:
        //                    _x = _x - 1;
        //                    break;
        //                case Direction.Right:
        //                    _y = _y + 1;
        //                    break;
        //                case Direction.Down:
        //                    _x = _x + 1;
        //                    break;
        //                case Direction.Left:
        //                    _y = _y - 1;
        //                    break;
        //            }


        //            if (stack.Top.fxList.Contains(fx) || arr[_x, _y] == 1)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //    }

        //    public enum Direction
        //    {
        //        Up, Down, Left, Right
        //    }

        //    /// <summary>
        //    /// 栈
        //    /// </summary>
        //    public class Stack
        //    {
        //        /// <summary>
        //        /// 栈顶 指向栈中最上面的元素
        //        /// </summary>
        //        public StackNode Top;
        //        /// <summary>
        //        /// 栈底 指向栈中最下面元素的下一个元素
        //        /// </summary>
        //        public StackNode Bottom;

        //        /// <summary>
        //        /// 构造方法
        //        /// </summary>
        //        public Stack()
        //        {
        //            Top = new StackNode();
        //            Bottom = Top;
        //        }

        //        /// <summary>
        //        /// 入栈
        //        /// </summary>
        //        public void push(int x, int y)
        //        {
        //            StackNode node = new StackNode(x, y);
        //            node.NextNode = Top;
        //            Top = node;
        //        }
        //        /// <summary>
        //        /// 出栈
        //        /// </summary>
        //        public void pop()
        //        {
        //            if (IsEmpty())
        //                return;
        //            Top = Top.NextNode;
        //        }

        //        public bool IsEmpty()
        //        {
        //            return Top == Bottom;
        //        }


        //    }
        //    /// <summary>
        //    /// 栈的节点
        //    /// </summary>
        //    public class StackNode
        //    {
        //        public int X;
        //        public int Y;
        //        public List<Direction> fxList = new List<Direction>();
        //        public StackNode NextNode;
        //        public StackNode(int x, int y)
        //        {
        //            X = x;
        //            Y = y;
        //        }
        //        public StackNode() { }
        //    }

    }
}
