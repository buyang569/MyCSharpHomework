﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 2018年10月27日
 * 初步实现功能，情况2会造成大量重复，待解决
 * 使用暴力算法，暂未想出更好方案
 * 
 * 2018年10月28日
 * 使用了哈希去重，去除了字符重复，运算逻辑重复待解决
 * 
 */
namespace JF1
{
    class Program
    {
        private static float JiSuan(float x, float y, int mark)//计算
        {
            switch (mark)
            {
                case 0: return x + y;
                case 1: return x - y;
                case 2: return x * y;
                case 3: return x / y;
            }
            return 0;
        }
        //第一种整合方式((a+b)+c)+d
        static bool calculate_1(float a, float b, float c, float d, int mark1, int mark2, int mark3)
        {
            float r1, r2, r3;
            r1 = JiSuan(a, b, mark1);
            r2 = JiSuan(r1, c, mark2);
            r3 = JiSuan(r2, d, mark3);
            return r3 == 24;
        }
        //第二种整合方式(a+b)+(c+d)
        static bool calculate_2(float a, float b, float c, float d, int mark1, int mark2, int mark3)
        {
            float r1, r2, r3;
            r1 = JiSuan(a, b, mark1);
            r2 = JiSuan(c, d, mark3);
            r3 = JiSuan(r1, r2, mark2);
            return r3 == 24;
        }
        //第三种整合方式(a+(b+c))+d
        static bool calculate_3(float a, float b, float c, float d, int mark1, int mark2, int mark3)
        {
            float r1, r2, r3;
            r1 = JiSuan(b, c, mark2);
            r2 = JiSuan(a, r1, mark1);
            r3 = JiSuan(r2, d, mark3);
            return r3 == 24;
        }
        //第四种整合方式a+((b+c)+d)
        static bool calculate_4(float a, float b, float c, float d, int mark1, int mark2, int mark3)
        {
            float r1, r2, r3;
            r1 = JiSuan(b, c, mark2);
            r2 = JiSuan(r1, d, mark3);
            r3 = JiSuan(a, r2, mark1);
            return r3 == 24;
        }
        //第五种整合方式a+(b+(c+d))
        static bool calculate_5(float a, float b, float c, float d, int mark1, int mark2, int mark3)
        {
            float r1, r2, r3;
            r1 = JiSuan(c, d, mark3);
            r2 = JiSuan(b, r1, mark2);
            r3 = JiSuan(a, r2, mark1);
            return r3 == 24;
        }
        static bool Calculate(Int16 a, Int16 b, Int16 c, Int16 d, int mark1, int mark2, int mark3)
        //分五情况计算
        {

            bool i = false;
            if (calculate_1(a, b, c, d, mark1, mark2, mark3))
            {
                i = true; jishu++;
                String st = "((" + a + FuHao[mark1] + b + ")" + FuHao[mark2] + c + ")" + FuHao[mark3] + d + "=24";
                result.Add(st);
            }
            if (calculate_2(a, b, c, d, mark1, mark2, mark3))
            {
                i = true; jishu++;
                String st = "(" + a + FuHao[mark1] + b + ")" + FuHao[mark2] + "(" + c + FuHao[mark3] + d + ")" + "=24";
                result.Add(st);
            }
            if (calculate_3(a, b, c, d, mark1, mark2, mark3))
            {
                i = true; jishu++;
                String st = "(" + a + FuHao[mark1] + "(" + b + FuHao[mark2] + c + "))" + FuHao[mark3] + d + "=24";
                result.Add(st);
            }
            if (calculate_4(a, b, c, d, mark1, mark2, mark3))
            {
                i = true; jishu++;
                String st = a + FuHao[mark1] + "((" + b + FuHao[mark2] + c + ")" + FuHao[mark3] + d + ")" + "=24";
                result.Add(st);
            }
            if (calculate_5(a, b, c, d, mark1, mark2, mark3))
            {
                i = true; jishu++;
                String st = a + FuHao[mark1] + "(" + b + FuHao[mark2] + "(" + c + FuHao[mark3] + d + "))" + "=24";
                result.Add(st);
            }
            return i;
        }

        static string[] FuHao = new string[] { "+", "-", "*", "/" };
        static int jishu = 0;
        static List<string> result = new List<string>();
        static void Main(string[] args)
        {
            int fh1, fh2, fh3;

            KaiShi:
            Console.Write("请输入第一个数（0-10）：");
            Int16 n1 = Int16.Parse(Console.ReadLine());
            Console.Write("请输入第二个数（0-10）：");
            Int16 n2 = Int16.Parse(Console.ReadLine());
            Console.Write("请输入第三个数（0-10）：");
            Int16 n3 = Int16.Parse(Console.ReadLine());
            Console.Write("请输入第四个数（0-10）：");
            Int16 n4 = Int16.Parse(Console.ReadLine());
            if (n1 >= 0 && n1 <= 10 && n2 >= 0 && n2 <= 10 && n3 >= 0 && n3 <= 10 && n4 >= 0 && n4 <= 10)
            {
                //判断是否属于0-10
            }
            else
            {
                Console.WriteLine("请输入0-10的数字");
                goto KaiShi;
            }
            Int16 a = 0, b = 0, c = 0, d = 0;
            
            Console.WriteLine("Solving....");
            for (int i = 0; i <= 24; i++)
            {
                for (fh1 = 0; fh1 < 4; fh1++)
                {
                    for (fh2 = 0; fh2 < 4; fh2++)
                    {
                        for (fh3 = 0; fh3 < 4; fh3++)
                        {
                            HuanShunXu(i);
                            Calculate(a, b, c, d, fh1, fh2, fh3);
                        }
                    }
                }
            }
            if (jishu == 0)
            {
                Console.WriteLine("无解");
            }
            else
            {
                HashSet<string> hs = new HashSet<string> (result);
                Int16 jishu2 = 0;
                foreach (String res in hs)
                {
                    Console.WriteLine(res);
                    jishu2++;

                }
                Console.WriteLine("共有{0}个结果，去重输出{1}个，输出完毕", jishu,jishu2);
            }
            void HuanShunXu(int i)
            {//情况转换
                switch (i)
                {
                    case 1:
                        a = n1; b = n2; c = n3; d = n4;
                        break;
                    case 2:
                        a = n1; b = n2; d = n3; c = n4;
                        break;
                    case 3:
                        a = n1; c = n2; b = n3; d = n4;
                        break;
                    case 4:
                        a = n1; c = n2; d = n3; b = n4;
                        break;
                    case 5:
                        a = n1; d = n2; b = n3; c = n4;
                        break;
                    case 6:
                        a = n1; d = n2; c = n3; b = n4;
                        break;
                    case 7:
                        b = n1; a = n2; c = n3; d = n4;
                        break;
                    case 8:
                        b = n1; a = n2; d = n3; c = n4;
                        break;
                    case 9:
                        b = n1; c = n2; a = n3; d = n4;
                        break;
                    case 10:
                        b = n1; c = n2; d = n3; a = n4;
                        break;
                    case 11:
                        b = n1; d = n2; a = n3; c = n4;
                        break;
                    case 12:
                        b = n1; d = n2; c = n3; a = n4;
                        break;
                    case 13:
                        c = n1; a = n2; b = n3; d = n4;
                        break;
                    case 14:
                        c = n1; a = n2; d = n3; b = n4;
                        break;
                    case 15:
                        c = n1; b = n2; a = n3; d = n4;
                        break;
                    case 16:
                        c = n1; b = n2; d = n3; a = n4;
                        break;
                    case 17:
                        c = n1; d = n2; a = n3; b = n4;
                        break;
                    case 18:
                        c = n1; d = n2; b = n3; a = n4;
                        break;
                    case 19:
                        d = n1; a = n2; b = n3; c = n4;
                        break;
                    case 20:
                        d = n1; a = n2; c = n3; b = n4;
                        break;
                    case 21:
                        d = n1; b = n2; a = n3; c = n4;
                        break;
                    case 22:
                        d = n1; b = n2; c = n3; a = n4;
                        break;
                    case 23:
                        d = n1; c = n2; a = n3; b = n4;
                        break;
                    case 24:
                        d = n1; c = n2; b = n3; a = n4;
                        break;
                }
            }
        }

    }
}
