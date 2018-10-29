using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Maze
{
    class Maze
    {
        static void Main(string[] args)
        {
            string strLine;
            try
            {
                FileStream aFile = new FileStream("maze.txt", FileMode.Open);
                StreamReader sr = new StreamReader(aFile);
                strLine = sr.ReadLine();
                //Read data in line by line 这个兄台看的懂吧~一行一行的读取
                while (strLine != null)
                {
                    Console.WriteLine(strLine);
                    strLine = sr.ReadLine();
                }
                sr.Close();
            }
            catch(IOException ex)
            {
                Console.WriteLine("An IOException has been thrown!");
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
                return;
            }
        }
    }
}
