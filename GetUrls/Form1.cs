using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetUrls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            string sHost = txtHost.Text.Trim();
            if (string.IsNullOrEmpty(sHost))
            {
                MessageBox.Show("补全相对路径不能为空,请输入如:http://www.baidu.com");
                return;
            }
            List<string> lst = GetUrls(txtSoure.Text.Trim(), sHost);

            txtRz.Text = string.Join("\r\n", lst.ToArray());

        }

        private string GetUrlHost(string url)

        {

            string text = url;
            string pattern = @"(?<=http://)[\w\.]+[^/]"; //C#正则表达式提取匹配URL的模式，       

            string s = "";

            MatchCollection mc = Regex.Matches(text, pattern);//满足pattern的匹配集合        

            foreach (Match match in mc)

            {

                s = match.ToString();

            }

            return s;

            //textBox1.Text = s;     

        }

        public List<string> GetUrls(string sContent,string tHost)
        {
            Regex reg = new Regex(@"(?is)<a[^>]*?href=(['""]?)(?<url>[^'""\s>]+)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>");
            MatchCollection mc = reg.Matches(sContent);
            Uri baseUri = new Uri(tHost);
            List<string> urls = new List<string>();

            foreach (Match m in mc)
            {

                Uri uriPage = new Uri(baseUri, m.Groups["url"].Value);
                urls.Add(uriPage.ToString());
            }

            return (from i in urls select i).Distinct().ToList();
        }
    }
}
