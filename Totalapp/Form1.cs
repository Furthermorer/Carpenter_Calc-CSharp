using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Totalapp
{
    public partial class MainForm : Form
    {
        private About abt = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private static decimal Xiebianxs(decimal w)
        {
            return w / (decimal)Math.Sin(Math.PI / 2 - Math.Asin((double)w / 100) / 2);
        }
        private static void Plus(ref decimal a)
        {
            if ((double)(1 - (a - (int)a)) < 1e-6)
                a = (decimal)((int)a + 1);
        }
        private static void Bujiao(ref decimal d)
        {
            d= Convert.ToDecimal(180 - d);
        }
        private static decimal Fen(decimal d)
        {
            decimal dfen;
            dfen = (d - (int)d) * 60;
            MainForm.Plus(ref dfen);
            return dfen;
        }
        private static decimal Miao(decimal dfen)
        {
            decimal dmiao;
            dmiao = (dfen - (int)dfen) * 60;
            MainForm.Plus(ref dmiao);
            return dmiao;
        }
        private static void Miaocarry(ref decimal dmiao, ref decimal dfen)
        {
            if ((int)dmiao == 60)
            {
                dmiao -= 60;
                dfen += 1;
            }
        }
        private static void Fencarry(ref decimal d, ref decimal dfen)
        {
            if ((int)dfen == 60)
            {
                dfen -= 60;
                d += 1;
            }
        }
        private static decimal GetR(decimal a, decimal b)
        {
            decimal r;
            r = b * (b / a + a / b) / 2;
            return r;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal r, b, x, d = 0, x1 = 0, x2 = 0,r2,result,baseb;
            string str = "";
            try
            {
                r = Convert.ToDecimal(textBox1.Text);
                b = Convert.ToDecimal(textBox2.Text);
                x = Convert.ToDecimal(textBox3.Text);
                if (r <= 0 || b <= 0)
                {
                    MessageBox.Show("r和b都应大于0。");
                    textBox1.Focus();
                }
                else if (r < b)
                {
                    MessageBox.Show("半径应大于等于半弦长。");
                    textBox1.Focus();
                }
                else
                {
                    r2 = r * r;
                    x1 = r2 - x * x;
                    x2 = r2 - b * b;
                    baseb = b*b / 100;
                    d = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(x1)) - Math.Sqrt(Convert.ToDouble(x2)));
                    textBox4.Text = d.ToString("f6");
                    for (int i = 0; i <= 9; i++)
                    {
                        
                        result = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(r2-baseb*i*i)) - Math.Sqrt(Convert.ToDouble(x2)));
                        str += String.Format("相对坐标：{0} 拱高为:{1:N6}", i, result) + "\n";
                        label39.Text = str;
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入数字。");
                textBox1.Focus();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal r = 100, re = 0, b=0, tep=0, x, fenmu = 10, result = 0, r2=0;
            string str = "";
            try
            {
                b = Convert.ToDecimal(textBox5.Text);
                r2 = r * r;
                tep = r2 - b * b;
                if (b < Convert.ToDecimal(0.01))
                {
                    MessageBox.Show("弦径比应大于0.01%。");
                    textBox5.Focus();
                }
                else
                {
                    re = r - Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(tep)));
                    textBox6.Text = re.ToString("f4") + "%";

                    for (int i = 0; i <= 9; i++)
                    {
                        x = b * i / fenmu;
                        result = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(r2 - x * x)) - Math.Sqrt(Convert.ToDouble(tep)));
                        str += String.Format("坐标：{0} 拱高坐标系数为:{1:N6}%", i, result) + "\n";
                        label12.Text = str;
                    }
                }
            }
            catch (OverflowException)
            {
                MessageBox.Show("弦径比应小于100%。");
                textBox5.Focus();
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入数字。");
                textBox5.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            decimal w, x = 0, gonggaoxs, xiebianxs, w2, t, k, dek;
            double dw, dx;
            int idek;
            string str = "";
            try
            {
                w = Convert.ToDecimal(textBox7.Text);
                k = w;
                dw = Convert.ToDouble(w);
                w2 = w * w;
                x = Convert.ToDecimal(textBox8.Text);
                dx = Convert.ToDouble(x);
                if (dx % 2 != 0 )
                {
                    MessageBox.Show("弦长级应为2的幂指数");
                    textBox8.Focus();
                }
                else if (dx <= 0)
                {
                    MessageBox.Show("弦长级应大于0");
                    textBox8.Focus();
                }
                else
                {
                    t = Convert.ToDecimal(1) / x;
                    dek = Convert.ToDecimal(Math.Log(dx, 2) - 1);
                    idek = Convert.ToInt32(dek);
                    gonggaoxs = Convert.ToDecimal(100.0 - Math.Sqrt(10000.0 - Convert.ToDouble(w2)));
                    xiebianxs = MainForm.Xiebianxs(w);
                    if (t == Convert.ToDecimal(0.5))
                    {
                        str += String.Format("弦径比:{0:0.0000}%\n", w);
                        str += String.Format("圆弧拱高系数:{0:0.0000%}\n", gonggaoxs/100);
                        str += String.Format("斜边系数:{0:0.0000%}", xiebianxs/100);
                    }
                    else
                    {
                        while (idek-- > 0)
                        {
                            w = MainForm.Xiebianxs(w) / 2;
                        }
                        w2 = w * w;
                        gonggaoxs = Convert.ToDecimal(100.0 - Math.Sqrt(10000.0 - Convert.ToDouble(w2)));
                        xiebianxs = MainForm.Xiebianxs(w);                     
                        str += String.Format("1/2弦径比:{0:0.0000}%\n", k);
                        str += String.Format("弦径比:{0:0.0000}%\n", w);
                        str += String.Format("圆弧拱高系数:{0:0.0000%}\n", gonggaoxs/100);
                        str += String.Format("斜边系数:{0:0.0000%}", xiebianxs/100);
                    }
                    label15.Text = str;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入数字。");
                textBox7.Focus();
            }
            catch (OverflowException)
            {
                MessageBox.Show("弦径比应小于100%。");
                textBox7.Focus();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            decimal b, poxi, A = 100, A2, b2;
            string str = "";
            try
            {
                b = Convert.ToDecimal(textBox9.Text);
                if (b < 0)
                {
                    MessageBox.Show("坡度应大于0。");
                    textBox9.Focus();
                }
                else
                {
                    A2 = A * A;
                    b2 = b * b;
                    poxi = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(A2) + Convert.ToDouble(b2))) / A;
                    str += String.Format("{0:0.00000}", poxi);
                    label19.Text = str;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入数字。");
                textBox9.Focus();
            }
                
        }

        private void button5_Click(object sender, EventArgs e)
        {
            decimal a, b, c, resulta, resultb, resultc, p, s, max, s1, p2, resultaa, resultbb, resultcc, a2, b2, c2;
            string str = "";
            try
            {
                a = Convert.ToDecimal(textBox10.Text);
                b = Convert.ToDecimal(textBox11.Text);
                c = Convert.ToDecimal(textBox12.Text);
                a2 = a * a;
                b2 = b * b;
                c2 = c * c;
                if (a >= b)
                    max = a;
                else
                    max = b;
                if (c > max)
                    max = c;
                p = (a + b + c) / 2;
                if (a < 0 || b < 0 || c < 0)
                {
                    MessageBox.Show("三边长应都大于0。");
                    textBox10.Focus();
                }
                else if ((2 * p - max) <= max)
                {
                    MessageBox.Show("两小边长之和应大于第三边。");
                    textBox10.Focus();
                }
                else
                {
                    s1 = p * (p - a) * (p - b) * (p - c);
                    s = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(s1)));
                    p2 = 2 * p;
                    resulta = (p2 * (p - a) - b * c) / (2 * s) * 100;
                    resultb = (p2 * (p - b) - a * c) / (2 * s) * 100;
                    resultc = (p2 * (p - c) - a * b) / (2 * s) * 100;
                    decimal d1, d2, d3, d1fen, d2fen, d3fen, d1miao, d2miao, d3miao;
                    resultaa = b * c / (2 * s);
                    d1 = Convert.ToDecimal(Math.Asin(Convert.ToDouble(1) / Convert.ToDouble(resultaa)) * 180 / Math.PI);
                    decimal _x, _y, _z;
                    _x = b2 + c2 - a2;
                    _y = a2 + c2 - b2;
                    _z = a2 + b2 - c2;
                    if (_x < 0)
                        MainForm.Bujiao(ref d1);
                    d1fen = MainForm.Fen(d1);
                    d1miao = MainForm.Miao(d1fen);
                    resultbb = a * c / (2 * s);
                    d2 = Convert.ToDecimal(Math.Asin(Convert.ToDouble(1) / Convert.ToDouble(resultbb)) * 180 / Math.PI);
                    if (_y < 0)
                        MainForm.Bujiao(ref d2);
                    d2fen = MainForm.Fen(d2);
                    d2miao = MainForm.Miao(d2fen);
                    resultcc = a * b / (2 * s);
                    d3 = Convert.ToDecimal(Math.Asin(Convert.ToDouble(1) / Convert.ToDouble(resultcc)) * 180 / Math.PI);
                    if (_z < 0)
                        MainForm.Bujiao(ref d3);
                    d3fen = MainForm.Fen(d3);
                    d3miao = MainForm.Miao(d3fen);

                    MainForm.Miaocarry(ref d1miao, ref d1fen);
                    MainForm.Miaocarry(ref d2miao, ref d2fen);
                    MainForm.Miaocarry(ref d3miao, ref d3fen);
                    MainForm.Plus(ref d1miao);
                    MainForm.Plus(ref d2miao);
                    MainForm.Plus(ref d3miao);
                    MainForm.Fencarry(ref d1, ref d1fen);
                    MainForm.Fencarry(ref d2, ref d2fen);
                    MainForm.Fencarry(ref d3, ref d3fen);
                    MainForm.Plus(ref d1fen);
                    MainForm.Plus(ref d2fen);
                    MainForm.Plus(ref d3fen);
                    if (resulta < 0)
                        resulta = -resulta;
                    if (resultb < 0)
                        resultb = -resultb;
                    if (resultc < 0)
                        resultc = -resultc;
                    str += String.Format("∠A:\n");
                    str += String.Format("∠A = {0:0}°{1:0}′{2:0.00}″\n∠A坡度：{3:0.00}%\n∠A坡度系数：{4:0.0000}\n\n", (int)d1, (int)d1fen, d1miao, resulta, resultaa);
                    str += String.Format("∠B:\n");
                    str += String.Format("∠B = {0:0}°{1:0}′{2:0.00}″\n∠B坡度：{3:0.00}%\n∠B坡度系数：{4:0.0000}\n\n", (int)d2, (int)d2fen, d2miao, resultb, resultbb);
                    str += String.Format("∠C:\n");
                    str += String.Format("∠C = {0:0}°{1:0}′{2:0.00}″\n∠C坡度：{3:0.00}%\n∠C坡度系数：{4:0.0000}\n\n", (int)d3, (int)d3fen, d3miao, resultc, resultcc);
                    label26.Text = str;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入数字。");
                textBox10.Focus();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            decimal d,f,m,pd,pdxs;
            string str="";
            try
            {
                d = Convert.ToDecimal(textBox13.Text);
                f = Convert.ToDecimal(textBox14.Text);
                m = Convert.ToDecimal(textBox15.Text);
                if (d < 0 || f < 0 || m < 0)
                {
                    MessageBox.Show("度角分应都大于0。");
                    textBox13.Focus();
                }
                else if (d > 180)
                {
                    MessageBox.Show("度数应小于180°。");
                    textBox13.Focus();
                }
                else
                {
                    d = d + f / 60 + m / 3600;
                    pd = Convert.ToDecimal(1.0 / Math.Tan(Convert.ToDouble(d) * Math.PI / 180) * 100);
                    pdxs = Convert.ToDecimal(Math.Sqrt(1.0 + Convert.ToDouble(pd * pd) / 10000));
                    str += String.Format("坡度={0:0.0000}%\n坡度系数={1:0.0000}", pd, pdxs);
                    label32.Text = str;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入数字。");
                textBox13.Focus();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确认退出吗?", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void CloseProgramCToolStrip_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimizedMToolStrip_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            decimal a, b, r;
            string str = "";
            try
            {
                a = Convert.ToDecimal(textBox18.Text);
                b = Convert.ToDecimal(textBox17.Text);
                if (a <= 0||b<=0)
                {
                    MessageBox.Show("拱高和半弦都应大于0");
                    textBox18.Focus();
                }
                else
                {
                    r = MainForm.GetR(a, b);
                    str += String.Format("圆弧半径长为：{0:0.00}", r);
                    label35.Text = str;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入数字。");
                textBox18.Focus();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            decimal n;
            decimal a, b, c, d, ew, f, g, h,saved=0;
            string str = "";
            try
            {
                n = Convert.ToDecimal(textBox16.Text);
                if (n < 3)
                {
                    MessageBox.Show("边数应大于3。");
                    textBox16.Focus();
                }
                else if (Convert.ToDecimal(n - (int)n) > 0)
                {
                    MessageBox.Show("边数应为整数。");
                    textBox16.Focus();
                }
                else
                {
                    a = Convert.ToDecimal(Math.Sin(Convert.ToDouble(1) / Convert.ToDouble(n) * Math.PI));
                    b = Convert.ToDecimal((Convert.ToDouble(1) - Math.Cos(Convert.ToDouble(1) / Convert.ToDouble(n) * Math.PI)) / 2);
                    c = Convert.ToDecimal(Convert.ToDouble(90) - Convert.ToDouble(180) / (double)n);
                    ew = (c - (int)c) * 60;
                    MainForm.Plus(ref ew);
                    g = (ew - (int)ew) * 60;
                    d = (Convert.ToDecimal(360) / n);
                    if (d > 90)
                        d = 180 - d;
                    f = (d - (int)d) * 60;
                    MainForm.Plus(ref f);
                    h = (f - (int)f) * 60;
                    if (n == 3)
                        saved = 120;
                    saved = d;
                    str += String.Format("分块系数：{0:0.0000}\n", a);
                    str += String.Format("圆弧拱高系数：{0:0.0000}\n", b);
                    str += String.Format("交角度数：{0:0}°{1:00}′{2:0.00}″\n", (int)c, ew, g);
                    str += String.Format("交角坡度：：{0:0.00}%\n", Convert.ToDecimal(100 / Math.Tan(Convert.ToDouble(c) * Math.PI / 180)));
                    str += String.Format("互角度数：{0:0}°{1:00}′{2:0.00}″\n", (int)saved, f, h);
                    str += String.Format("互角坡度：：{0:0.00}%\n", Convert.ToDecimal(100 / Math.Tan(Convert.ToDouble(d) * Math.PI / 180)));
                    label36.Text = str;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入数字。");
                textBox18.Focus();
            }
        }

        private void AboutToolStrip_Click(object sender, EventArgs e)
        {
            abt = new About();
            abt.Show();
        }

        private void gg_Click(object sender, EventArgs e)
        {

        }
    }
}
