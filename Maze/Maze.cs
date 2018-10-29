using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
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
        enum BlockType
        {
            MASK = 0,
            ROAD = 1,
            BACK = 2,
            INWALL = 3,
            OUTWALL = 4,
            EMPTYWALL = 5
        };
        enum Direction
        {
            EAST = 6,
            SOUTH = 7,
            WEST = 8,
            NORTH = 9
        };
        
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
            DuQu(out saveMaze);
            //寻找起止点
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < H; j++)
                {
                    if(saveMaze[i,j]=="S")
                    {
                        KaiShi.x = j;
                        KaiShi.y = i;
                    }
                    if (saveMaze[i, j] == "E")
                    {
                        JieShu.x = j;
                        JieShu.y = i;
                    }
                }
            }
            Console.WriteLine("Kaishi:X {0} Y:{1}", KaiShi.x, KaiShi.y);
            Console.WriteLine("Jie:X {0} Y:{1}", JieShu.x, JieShu.y);
            //


        }
    }
}
