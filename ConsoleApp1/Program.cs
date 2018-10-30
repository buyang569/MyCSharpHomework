using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZouMiGong
{
    class Program
    {
        static void Main(string[] args)
        {
            //地图开始
            int[,] arr = {
                                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                                {1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                                {1,1,0,0,0,0,1,0,0,0,1,1,1,1,0,1,0,0,0,1},
                                {1,1,1,1,0,1,0,0,0,0,1,1,1,0,0,1,0,1,0,1},
                                {1,1,0,0,0,0,1,0,0,0,1,1,1,0,1,1,0,1,0,1},
                                {1,1,0,1,0,0,1,0,1,1,1,1,0,0,1,0,0,1,0,1},
                                {1,1,0,1,1,1,0,1,0,0,1,1,0,1,1,0,1,0,0,1},
                                {1,1,0,0,0,0,0,1,0,0,1,1,0,1,0,0,1,0,1,1},
                                {1,1,0,0,0,1,0,0,0,0,1,1,1,1,0,1,1,0,1,1},
                                {1,1,0,0,0,1,0,0,0,0,0,1,1,1,0,0,1,0,1,1},
                                {1,1,1,1,1,1,1,1,1,1,0,1,1,0,1,0,1,0,0,1},
                                {1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1,1,0,1},
                                {1,0,0,1,0,0,1,0,1,0,0,1,0,0,0,0,1,0,0,1},
                                {1,1,0,1,1,0,1,0,0,1,0,1,1,1,0,1,1,0,1,1},
                                {1,0,0,0,1,0,1,1,0,1,0,1,0,0,0,1,0,0,0,1},
                                {1,0,1,0,1,0,0,0,0,1,0,0,0,1,1,0,0,1,0,1},
                                {1,0,1,0,1,0,1,1,0,0,1,0,1,1,0,0,1,1,0,1},
                                {1,0,1,1,0,0,0,0,1,0,1,1,0,0,1,0,0,0,1,1},
                                {1,0,0,0,0,1,0,1,1,0,0,0,0,1,1,0,1,0,0,1},
                                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
                            };
            //int[,] arr = {
            //            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            //           {1,0,1,0,1,0,0,0,0,0,1,0,0,0,0,0,1,1,1,1},
            //           {1,0,0,0,1,1,0,0,1,0,1,0,1,1,0,1,0,0,0,1},
            //           {1,0,1,0,0,0,0,1,1,0,1,0,1,0,0,1,0,1,0,1},
            //           {1,0,1,0,1,1,0,0,1,0,0,0,1,0,1,1,0,1,0,1},
            //           {1,0,1,0,0,0,1,0,1,1,1,1,0,0,1,0,0,1,0,1},
            //           {1,0,1,0,1,1,0,0,1,0,0,1,0,1,1,0,1,0,0,1},
            //           {1,0,1,0,1,0,0,1,0,0,1,0,0,1,0,0,1,0,1,1},
            //           {1,0,1,0,1,0,1,1,0,1,1,0,1,1,0,1,1,0,1,1},
            //           {1,0,1,0,1,1,0,0,0,1,0,0,1,1,0,0,1,0,1,1},
            //           {1,0,1,0,1,1,0,1,0,0,0,1,1,0,1,0,1,0,0,1},
            //           {1,0,1,0,0,0,0,1,1,1,1,1,1,0,1,0,1,1,0,1},
            //           {1,0,1,0,1,0,1,0,1,0,0,1,0,0,0,0,1,0,0,1},
            //           {1,0,1,0,1,0,1,0,0,1,0,1,1,1,0,1,1,0,1,1},
            //           {1,0,1,0,1,0,1,1,0,1,0,1,0,0,0,1,0,0,0,1},
            //           {1,0,0,0,1,0,0,0,0,1,0,0,0,1,0,0,1,1,0,1},
            //           {1,0,1,0,1,0,1,1,0,0,1,0,1,1,0,0,1,1,0,1},
            //           {1,0,1,0,1,0,0,0,1,0,1,1,0,0,0,0,0,0,1,1},
            //           {1,0,1,0,1,1,0,1,1,0,0,0,0,1,1,0,1,0,0,1},
            //           {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            //                };
            //地图结束

            Stack stack = new Stack();
            stack.push(1, 1);
            bool temp = true;
            while (temp)
            {
                int x, y;
                Direction fx;
                //上
                if (GetXY(arr, stack, Direction.Up, stack.Top.X, stack.Top.Y, out x, out y))
                {
                    fx = Direction.Down;
                    stack.Top.fxList.Add(Direction.Up);
                }
                //右
                else if (GetXY(arr, stack, Direction.Right, stack.Top.X, stack.Top.Y, out x, out y))
                {
                    fx = Direction.Left;
                    stack.Top.fxList.Add(Direction.Right);
                }
                //下
                else if (GetXY(arr, stack, Direction.Down, stack.Top.X, stack.Top.Y, out x, out y))
                {
                    fx = Direction.Up;
                    stack.Top.fxList.Add(Direction.Down);
                }
                //左
                else if (GetXY(arr, stack, Direction.Left, stack.Top.X, stack.Top.Y, out x, out y))
                {
                    fx = Direction.Right;
                    stack.Top.fxList.Add(Direction.Left);
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
                Console.WriteLine(x + "," + y);
            }

            while (!stack.IsEmpty())
            {
                arr[stack.Top.X, stack.Top.Y] = 2;
                stack.pop();
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
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