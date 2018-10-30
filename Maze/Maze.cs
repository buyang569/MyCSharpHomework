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
 * 
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
        static int H = 10;//定义行数
        
        static bool DuQu(out String[,] output)
        #region//读取文件中的迷宫
        {
            string strLine;
            String[,] duru = new String[H, H];
            try
            {
                FileStream aFile = new FileStream("maze.txt", FileMode.Open);
                StreamReader sr = new StreamReader(aFile);

                for(int i=0;i<H;i++)
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
        static void Main(string[] args)
        {
            WeiZhi KaiShi, JieShu;
            KaiShi = new WeiZhi();
            JieShu = new WeiZhi();
            String[,] saveMaze = new String[H, H];
            int[,] arr = new int[H+2, H+2];
            for (int i = 0; i < H+2; i++)
            {
                for (int j = 0; j < H+2; j++)
                {
                    arr[i, j] = 1;
                }
            }
            DuQu(out saveMaze);
            //寻找起止点
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < H; j++)
                {
                    if (saveMaze[i, j] == "S")
                    {
                        saveMaze[i, j] = "0";
                        KaiShi.x = j;
                        KaiShi.y = i;
                    }
                    if (saveMaze[i, j] == "E")
                    {
                        saveMaze[i, j] = "0";
                        JieShu.x = j;
                        JieShu.y = i;
                    }
                }
            }
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < H; j++)
                {
                    arr[i+1, j+1] = int.Parse(saveMaze[i, j]);
                }
            }
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (arr[i, j] == 0)
                    {
                        Console.Write("  ");
                    }
                    else if (arr[i, j] == 1)
                    {
                        Console.Write("■");
                    }
                    else
                    {
                        Console.Write("◇");
                    }
                }
                Console.WriteLine();
            }
            //
            Stack stack = new Stack();
            stack.push(1, 1);
            bool temp = true;
            while (temp)
            {
                int x, y;
                Direction fx;
                //上
                if (GetXY(arr, stack, Direction.Down, stack.Top.X, stack.Top.Y, out x, out y))
                {
                    fx = Direction.Up;
                    stack.Top.fxList.Add(Direction.Down);
                }
                //右
                else if (GetXY(arr, stack, Direction.Left, stack.Top.X, stack.Top.Y, out x, out y))
                {
                    fx = Direction.Right;
                    stack.Top.fxList.Add(Direction.Left);
                }
                //下
                else if (GetXY(arr, stack, Direction.Up, stack.Top.X, stack.Top.Y, out x, out y))
                {
                    fx = Direction.Down;
                    stack.Top.fxList.Add(Direction.Up);
                }
                //左
                else if (GetXY(arr, stack, Direction.Right, stack.Top.X, stack.Top.Y, out x, out y))
                {
                    fx = Direction.Left;
                    stack.Top.fxList.Add(Direction.Right);
                }
                else//没有路 出栈
                {
                    stack.pop();
                    continue;
                }
                stack.push(x, y);
                stack.Top.fxList.Add(fx);
                if (x == 10 && y == 10)
                {
                    temp = false;
                }
                arr[x, y] = 2;
                Console.Clear();
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (arr[i, j] == 0)
                        {
                            Console.Write("  ");
                        }
                        else if (arr[i, j] == 1)
                        {
                            Console.Write("■");
                        }
                        else
                        {
                            Console.Write("◇");
                        }
                    }
                    Console.WriteLine();
                }
                //
                Console.WriteLine(x + "," + y);
                Thread.Sleep(10);
            }

            while (!stack.IsEmpty())
            {
                arr[stack.Top.X, stack.Top.Y] = 2;
                stack.pop();
            }
            Console.Clear();
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (arr[i, j] == 0)
                    {
                        Console.Write("  ");
                    }
                    else if (arr[i, j] == 1)
                    {
                        Console.Write("■");
                    }
                    else
                    {
                        Console.Write("◇");
                    }
                }
                Console.WriteLine();
            }
            Console.ReadLine();

        }


        /// <summary>
        /// 得到这个点 所指向的下一个点
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="fx"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <returns></returns>
        static bool GetXY(int[,] arr, Stack stack, Direction fx, int x, int y, out int _x, out int _y)
        {
            _x = x;
            _y = y;
            switch (fx)
            {
                case Direction.Up:
                    _x = _x - 1;
                    break;
                case Direction.Right:
                    _y = _y + 1;
                    break;
                case Direction.Down:
                    _x = _x + 1;
                    break;
                case Direction.Left:
                    _y = _y - 1;
                    break;
            }


            if (stack.Top.fxList.Contains(fx) || arr[_x, _y] == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public enum Direction
    {
        Up, Down, Left, Right
    }

    /// <summary>
    /// 栈
    /// </summary>
    public class Stack
    {
        /// <summary>
        /// 栈顶 指向栈中最上面的元素
        /// </summary>
        public StackNode Top;
        /// <summary>
        /// 栈底 指向栈中最下面元素的下一个元素
        /// </summary>
        public StackNode Bottom;

        /// <summary>
        /// 构造方法
        /// </summary>
        public Stack()
        {
            Top = new StackNode();
            Bottom = Top;
        }

        /// <summary>
        /// 入栈
        /// </summary>
        public void push(int x, int y)
        {
            StackNode node = new StackNode(x, y);
            node.NextNode = Top;
            Top = node;
        }
        /// <summary>
        /// 出栈
        /// </summary>
        public void pop()
        {
            if (IsEmpty())
                return;
            Top = Top.NextNode;
        }

        public bool IsEmpty()
        {
            return Top == Bottom;
        }


    }
    /// <summary>
    /// 栈的节点
    /// </summary>
    public class StackNode
    {
        public int X;
        public int Y;
        public List<Direction> fxList = new List<Direction>();
        public StackNode NextNode;
        public StackNode(int x, int y)
        {
            X = x;
            Y = y;
        }
        public StackNode() { }
    }

}
