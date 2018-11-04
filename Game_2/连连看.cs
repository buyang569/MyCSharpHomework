using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_2
{
    public partial class 连连看 : Form
    {
        public 连连看()
        {
            InitializeComponent();
        }

        private void 连连看_Load(object sender, EventArgs e)
        {

        }
        static int[] arr1 = new int[80];
        private void InitControl()
        {
            int iSeed = 10;
            Random ro = new Random(iSeed);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));

            for (int i = 0; i < 80; i += 2)
            {
                arr1[i] = ran.Next(1, 20);
                arr1[i + 1] = arr1[i];
            }

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    int m = ran.Next(0, 19);
                    Label lb = new Label();
                    lb.Click += new System.EventHandler(Label_Click);
                    lb.Location = new System.Drawing.Point(65 + 50 * j, 80 + 50 * i);
                    lb.Size = new Size(40, 30);
                    lb.Text = arr1[m].ToString();
                    lb.BackColor = Color.White;
                    lb.TextAlign = ContentAlignment.MiddleCenter;
                    this.Controls.Add(lb);
                }
            }
        }
        static private int i = 0;
        static private Label One, Two;
        static int[,] arr = new int[16, 9];
        private void Label_Click(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            if (i == 0)//第一次点击
            {
                One = lb;
                lb.BackColor = Color.Yellow;
                i = 1;
            }
            else if (i == 1)
            {
                Two = lb;
                lb.BackColor = Color.Yellow;
                if (One.Location!=Two.Location)
                {
                    if (One.Text.Equals(Two.Text))
                    {
                        MyPoint KaiShi = new MyPoint((One.Location.X - 65) / 50, (One.Location.Y - 80) / 50);
                        MyPoint JieShu = new MyPoint((Two.Location.X - 65) / 50, (Two.Location.Y - 80) / 50);
                        MyPoint p = new MyPoint(KaiShi.X, KaiShi.Y);
                        p.parent = null;
                        for (int i = 0; i < 9; i++)
                        {
                            for (int j = 0; j < 16; j++)
                            {
                                if (arr[j, i] == -1)
                                {
                                    arr[j, i] = 0;
                                }

                            }
                        }
                        arr[KaiShi.X, KaiShi.Y] = 0;
                        arr[JieShu.X, JieShu.Y] = 0;
                        if (BFS(p, arr, JieShu))
                        {
                            One.Visible = false;
                            Two.Visible = false;
                        }
                        else if ((KaiShi.X == 13 && JieShu.X == 13 && (Math.Abs(KaiShi.Y - JieShu.Y) < 100)) || (KaiShi.Y == 6 && JieShu.Y == 6 && (Math.Abs(KaiShi.X - JieShu.X) < 100)))//补丁
                        {
                            One.Visible = false;
                            Two.Visible = false;
                        }
                        else
                        {
                            arr[KaiShi.X, KaiShi.Y] = 1;
                            arr[JieShu.X, JieShu.Y] = 1;
                            One.BackColor = Color.White;
                            Two.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        One.BackColor = Color.White;
                        Two.BackColor = Color.White;
                    }
                }
                else
                {
                    i = 0;
                    One.BackColor = Color.White;
                    Two.BackColor = Color.White;
                }
                
                i = 0;
            }
            //lb.Visible = false;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            InitControl();
            One = new Label();
            Two = new Label();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    arr[j, i] = 0;
                }
            }
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    arr[j + 1, i + 1] = 1;
                }
            }
        }
        private void Restart_Click(object sender, EventArgs e)
        {
            foreach (Label lab in this.Controls.OfType<Label>())
            {
                lab.Visible = false;
            }
            label1.Visible = true;
        }

        #region 寻找通路
        /// <summary>
        /// 迷宫程序移植，运用BFS，计算两点是否存在通路
        /// </summary>
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

        

        static bool BFS(MyPoint p, int[,] data, MyPoint js)
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
                        if ((qp.X + i >= 0) && (qp.X + i < 14) && (qp.Y + j >= 0) && (qp.Y + j < 7) && (qp.X + i == qp.X || qp.Y == qp.Y + j)) //是否越界 只遍历上下左右
                        {
                            if (data[qp.X + i, qp.Y + j] == 0)
                            {
                                if (qp.X + i == js.X && qp.Y + j == js.Y)  //是否为终点
                                {
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
        #endregion
    }
}
