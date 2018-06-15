using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Domain;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using WeatherDb;

namespace Weather
{
    public partial class Form1 : Form
    {
        public string []r=new string [23];
        public Form1()
        {
            InitializeComponent();

            cmb_province.ValueMember = "ProvinceId";
            cmb_province.DisplayMember = "ProName";

            cmb_city.ValueMember = "CityId";
            cmb_city.DisplayMember = "CityName";

            cmb_distinct.ValueMember = "DistrictId";
            cmb_distinct.DisplayMember = "DisName";

            //设置数据下拉列表高度
            cmb_province.DropDownHeight = 120;
            cmb_city.DropDownHeight = 120;
            cmb_distinct.DropDownHeight = 120;

            
        }

        public void initform(string str)
        {
             if (str.Contains("雨"))
                this.BackgroundImage = Image.FromFile("F:/人机交互/PS/图/yu.jpg");
            else if (str.Contains("晴"))
                this.BackgroundImage = Image.FromFile("F:/人机交互/PS/图/taiyang.jpg");
            else if (str.Contains("云") || str.Contains("阴"))
                this.BackgroundImage = Image.FromFile("F:/人机交互/PS/图/yun.jpg");
            else if (str.Contains("雪"))
                this.BackgroundImage = Image.FromFile("F:/人机交互/PS/图/xue.jpg");
        }

        Weather.cn.com.webxml.www.WeatherWebService w = new Weather.cn.com.webxml.www.WeatherWebService();
        
        
        private void button1_Click(object sender, EventArgs e)
        {

            string[] r_1 = w.getWeatherbyCityName(cmb_city.Text.Trim());
            for(int i = 0; i < r.Length; i++)
             {
                r[i] = r_1[i];
             }
            // Console.WriteLine(r[6]);
             initform(r[6]);
            //var ls = new jiekou.jiekou();
            //string str = ls.getHtml("http://wthrcdn.etouch.cn/weather_mini?city=%E7%A6%8F%E5%B7%9E");
            //string str= ls.GetFunction("http://wthrcdn.etouch.cn/weather_mini?city=%E7%A6%8F%E5%B7%9E", "");
            //Console.WriteLine(str + "**********************************");
            if (cmb_city.Text.Trim() != "")
             {
                 richTextBox1.Text = String.Empty;
                 richTextBox2.Text = String.Empty;
                 richTextBox3.Text = String.Empty;
                 richTextBox4.Text = String.Empty;

               for (int i = 0; i < 12; i++)
                 {
                     if (r[i].EndsWith(".gif") || r[i].EndsWith(".jpg"))
                     {
                         /* for(int j = 0; j <= 31; j++)
                          {
                              if (r[i].Equals(j + ".gif"))
                              {

                                  richTextBox1.Text +=Image.FromFile(".../.../weather/"+ j+".gif") + "\r\n";
                              }
                          }*/
                     }
                     else
                     {
                         richTextBox1.Text += r[i] + "\r\n";
                     }
                 }

                 for (int i = 12; i < 17; i++)
                 {
                     if (r[i].EndsWith(".gif") || r[i].EndsWith(".jpg")) { }
                     else richTextBox2.Text += r[i] + "\r\n";
                 }

                 for (int i = 17; i < 22; i++)
                 {
                     if (r[i].EndsWith(".gif") || r[i].EndsWith(".jpg")) { }
                     else richTextBox3.Text += r[i] + "\r\n";
                 }

                var contents = string.Format("{0}=>{1}:今天气温{2}{3}",
                       DateTime.Now,
                       cmb_city.Text.Trim(),
                       r[5],
                      /* r[12],
                       r[17],*/
                        Environment.NewLine
                        );
                var log = new Weatherdb();
                log.Write(contents);
             }else
             {
                 richTextBox1.Clear();
                 richTextBox2.Clear();
                 richTextBox3.Clear();
                 richTextBox4.Clear();
             }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmb_province.DataSource = Data.Provinces.OrderBy(t => t.ProSort).ToList();
        }

       private void cmb_province_SelectedIndexChanged(object sender, EventArgs e)
        {
            var proId = (int)cmb_province.SelectedValue;
            
            
            var city =Domain.Data.Cities.Where(t => t.ProvinceId == proId).OrderBy(t => t.CitySort);
            cmb_city.DataSource = city.ToList();
        }

       private void cmb_city_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cityId = (int)cmb_city.SelectedValue;
            var distinct = Domain.Data.Districts.Where(t => t.CityId == cityId).OrderBy(t => t.DisSort);
            cmb_distinct.DataSource = distinct.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox4.Visible = true;
            richTextBox4.Clear();
            chart1.Visible = false;
            //string[] r = w.getWeatherbyCityName(cmb_city.Text.Trim());
            for (int i = 22; i < 23; i++)
            {
                
                if (r[i].EndsWith(".gif") || r[i].EndsWith(".jpg")) { }
                else richTextBox4.Text += r[i] + "\r\n";
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            //string[] r = w.getWeatherbyCityName(cmb_city.Text.Trim());
            chart1.Visible = true;
            richTextBox4.Visible = false;
            string str_1 = r[5];
            string str_2= r[12];
            string str_3 =r[17];
            /*string str_1 = "17℃/30℃";
            string str_2 = "17℃/30℃";
            string str_3 = "17℃/30℃";*/
            int[] a = new int[6] {
                int.Parse(str_1.Substring(0, 2)), int.Parse(str_1.Substring(4, 2)),
                int.Parse(str_2.Substring(0, 2)) , int.Parse(str_2.Substring(4, 2)) ,
                int.Parse(str_3.Substring(0, 2)),int.Parse(str_3.Substring(4, 2))
            };
            List<string> xData = new List<string>() { "今天", "今天", "明天", "明天", "后天", "后天" };
            List<int> yData = new List<int>() { a[0], a[1], a[2], a[3], a[4], a[5] };
            //线条颜色
            chart1.Series[0].Color = Color.Green;
            //线条粗细
            chart1.Series[0].BorderWidth = 2;
            //标记点边框颜色      
            chart1.Series[0].MarkerBorderColor = Color.Blue;
            //标记点边框大小
            chart1.Series[0].MarkerBorderWidth = 3; //chart1.;// Xaxis 
            //标记点中心颜色
            chart1.Series[0].MarkerColor = Color.White;//AxisColor
            //标记点大小
            chart1.Series[0].MarkerSize = 8;
            //标记点类型     
            chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
            //将文字移到外侧
            chart1.Series[0]["PieLabelStyle"] = "Outside";
            //绘制黑色的连线
            chart1.Series[0]["PieLineColor"] = "Black";
            chart1.Series[0].Points.DataBindXY(xData, yData);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox4.Visible = true;
            richTextBox4.Clear();
            chart1.Visible = false;
            var log = new Weatherdb();
            var contents = log.Read();
            richTextBox4.Text = contents;
        }
    }
}
