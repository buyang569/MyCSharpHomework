using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Game_1
{
    public partial class 考眼力 : Form
    {
        public Random ram = new Random();
        public char[] Zifu = {
            '1', 'I', 'I','1',
            '0', 'o', '0', 'O', 'o', 'O' , 'o', '0', 'O', '0', 'O', 'o',
            '5', 's', 's', '5', '5', 'S' , 'S', '5', 'S', 's', 's', 'S',
        };
        public 考眼力()
        {
            InitializeComponent();
        }

        
        private void MuBiao_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            label34.Text = "time:"+(time/10.0).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Label> labels = new List<Label>();
            int a= ram.Next(0, 14)*2;
            for (int i = ram.Next(20, 30); i >0 ; i--)
            {
                Label lbl = new Label();//声明一个label
                lbl.Location = new System.Drawing.Point(ram.Next(20,430), ram.Next(100, 430));//设置位置
                lbl.Size = new Size(20, 30);//设置大小
                lbl.Text = Zifu[a].ToString();//设置Text值
                this.Controls.Add(lbl);//在当前窗体上添加这个label控件
                labels.Add(lbl);
            }
            MuBiao.Location = new System.Drawing.Point(ram.Next(20, 430), ram.Next(100, 430));
            MuBiao.Text = Zifu[a + 1].ToString();
            timer1.Enabled = true;
        }
        public Int32 time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            label34.Text = "time:" + (time / 10.0).ToString();
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
            Application.Restart();
            Process.GetCurrentProcess().Kill();
        }
    }
}
