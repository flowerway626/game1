using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int[] n = new int[9]; //宣告n[0]~n[8]整數陣列，用來表示8個圖片方塊所表示的值
                              //n[0]省略不用
        PictureBox[] pic = new PictureBox[9];//宣告p[0]~p[9]圖片方塊控制項陣列，
                                             //p[0]省略不用，pic[1]~pic[8]用來代表pictureBox1~pictureBox8
        PictureBox hit1, hit2;//宣告hit表示第一次翻牌的圖片方塊
                              //hit2表示第二次翻牌的圖片方塊
        string t1, t2;//t1字串存放第一次翻牌圖片所取得的值
                      //t2字串存放第二次翻牌圖片所取得的值
        bool frist = true;//First表示第一次按下圖片的旗標
        int tot;//答對的組數，若tot為4表示過關
        int timer1Tot;//表示timer1計時器執行的次數

        //亂數方法，用來將n陣列重新洗牌
        void SetRnd()
        {
            int[] a = new int[] { 0, 1, 1, 2, 2, 3, 3, 4, 4 };
            int max = n.GetUpperBound(0);
            Random rd = new Random();
            int num;
            string str = "";
            for (int i = 0; i <= n.GetUpperBound(0); i++)
            {
                num = rd.Next(1, max + 1);
                n[1] = a[num];
                a[num] = a[max];
                max--;
                str = a[num].ToString();
                textBox1.AppendText(str + "\n");//將亂數顯示在textBox1
            }
        }

        //Form1表單載入時，即觸發Form1_Load事件處理函式
        private void Form1_Load(object sender, EventArgs e)
        {
            //分別將pictureBox1~pictureBox8指定給pic[1]~pic[8]
            //表示pic[1]~pic[8]可以操作pictureBox1~pictureBox8控制項
            pic[1] = pictureBox1;
            pic[2] = pictureBox2;
            pic[3] = pictureBox3;
            pic[4] = pictureBox4;
            pic[5] = pictureBox5;
            pic[6] = pictureBox6;
            pic[7] = pictureBox7;
            pic[8] = pictureBox8;

            timer1.Enabled = false; //時間停止

            for (int i = 1; i <= n.GetUpperBound(0); i++)
            {
                pic[i].Image = new Bitmap("0.png");//使pictureBox1~pictureBox8顯示0.png
                pic[i].Tag = n[i];//pictureBox1~pictureBox8的Tag屬性皆設為n[1]~n[8]
                pic[i].Enabled = false;//pictureBox1~pictureBox8失效
                pic[i].Click += new EventHandler(PictureBox_Click);
                //使pictureBox1~pictureBox8的Click事件被觸發時皆會執行PictureBox_Click事件處理函式 
            }
        }

        //定義PictureBox_Click事件處理函式，以提供給pictureBox1~pictureBox8的Click事件使用
        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (frist)//第一次翻牌
            {
                hit1 = (PictureBox)sender; //將第一次翻牌的圖片方塊指定給hit1
                t1 = Convert.ToString(hit1.Tag);//將目前翻牌圖片的值指定給t1
                hit1.Image = new Bitmap(t1 + ".png");//顯示目前翻牌的圖示
                frist = false;//將First設為false表示目前己結束第二次翻牌
            }
            else
            {   
                hit2 = (PictureBox)sender;//將第二次翻牌的圖片方塊指定給hit2
                t2 = Convert.ToString(hit2.Tag);//將目前翻牌圖片的值指定給t2
                hit2.Image = new Bitmap(t2 + ".png");//顯示目前翻牌的圖示
                frist = true;// 將First設為true表示目前已結束第二次翻牌

                if (t1 == t2)//若t1等於t2，表示所翻牌兩個圖片的Tag屬性相同，即兩者的圖示相同
                {
                    //使目前翻牌兩個圖片失效
                    hit1.Enabled = false;
                    hit2.Enabled = false;
                    tot += 1;//答對組數加1
                }
                if (t1 != t2)//若t1不等於t2，表示所翻牌兩個圖片的Tag屬性不同，即兩者的圖示不相同
                {
                    //將第一次和第二次翻牌的圖示以0.png顯示
                    hit1.Image = new Bitmap("0.png");
                    hit2.Image = new Bitmap("0.png");
                }
                if (tot == 4) //若答對組數為4，即表示過關
                {
                    timer1.Enabled = false;//計時器停止
                }
            }
        }
        private void GameStart()//進行遊戲的GameStart()事件處理函式
        {
            tot = 0;//將答對的組數預設為0
            t1 = "";//將t1第一次翻牌圖片所取得的值設為空白
            t2 = "";//將t2第一次翻牌圖片所取得的值設為空白
            timer1Tot = 0;
            hit1 = null;//將hit1第一次翻牌的圖片方塊設為null
            hit2 = null;//將hit2第一次翻牌的圖片方塊設為null
            SetRnd();//呼叫亂數程序重新洗牌

            //使pictureBox1~pictureBox8顯示1~4.png四個圖示
            for (int i = 1; i <= n.GetUpperBound(0); i++)
            {
                pic[i].Tag = n[i];
                pic[i].Image = new Bitmap(Convert.ToString(n[i]) + ".png");
            }
        }
        //開始進行遊戲
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;//timer1計時器啟動
            GameStart();
        }

        //timer1計時器啟動時會觸發timer1_Tick事件
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1Tot += 1;//timer1Tot加1即遊戲時間加1
            label1.Text = Convert.ToString(timer1Tot) + "秒";

            for (int i = 1; i <= n.GetUpperBound(0); i++)
            {
                pic[i].Image = new Bitmap("0.png");  //pictureBox1~pictureBox8顯示0.png
                pic[i].Enabled = true;//pictureBox1~pictureBox8圖片啟用

            }
            if (timer1Tot == 10) //timer1Tot遊戲時間到30時，即執行下面敘述馬上停止遊戲
                {
                    timer1.Enabled = false;//timer1計時器停止

                for (int i = 1; i <= n.GetUpperBound(0); i++)
                    {
                        pic[i].Image = new Bitmap("0.png");  //pictureBox1~pictureBox8顯示0.png
                        pic[i].Enabled = false;//pictureBox1~pictureBox8圖片失效
                    }
            }
            
        }
        //遊戲暫停
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;//timer1計時器停止
        }

    }
}
