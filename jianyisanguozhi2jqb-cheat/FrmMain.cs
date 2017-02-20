using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace jianyisanguozhi2jqb_cheat
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //人物信息
            PersonInfo();
            //游戏帮助
            GameReadme();
            //查看地图
            ShowMap();
            //查看插图
            ShowChatu();
        }

        /// <summary>
        /// 人物信息
        /// </summary>
        private void PersonInfo()
        {
            FileStream fs = new FileStream("data/psn.zil", FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            txtBangzhu.Text = sr.ReadToEnd();
            sr.Close();
            fs.Close();
        }

        /// <summary>
        /// 游戏帮助
        /// </summary>
        private void GameReadme()
        {
            FileStream fs = new FileStream("data/readme.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            txtBangzhu.Text = sr.ReadToEnd();
            sr.Close();
            fs.Close();
        }

        /// <summary>
        /// 查看地图
        /// </summary>
        private void ShowMap()
        {
            FileStream fs = new FileStream("data/map.zil", FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            txtMap.Text = sr.ReadToEnd();
            sr.Close();
            fs.Close();
        }

        /// <summary>
        /// 查看插图
        /// </summary>
        private void ShowChatu()
        {
            FileStream fs = new FileStream("data/image.zil", FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            txtChatu.Text = sr.ReadToEnd();
            sr.Close();
            fs.Close();
        }
    }
}
