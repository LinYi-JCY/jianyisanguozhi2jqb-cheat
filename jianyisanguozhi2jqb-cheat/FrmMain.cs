using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            List<string> lines = File.ReadLines("data/psn.zil", Encoding.Default).ToList();
            string reg = "^.+#$";
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            string lineItem = "";
            List<string> lineList = null;
            foreach (var line in lines)
            {
                if (Regex.Match(line, reg).Success)
                {
                    if (lineList != null && lineList.Count > 0 && lineList[lineList.Count - 1] == lineItem)
                    {
                        string psnName = lineList[0].Split('#')[0];
                        if (!dic.ContainsKey(psnName))
                        {
                            dic.Add(psnName, lineList);
                        }
                        else
                        {
                            dic.Add(psnName + "x", lineList);
                        }
                    }
                    lineList = new List<string> { line };
                }
                else
                {
                    lineList.Add(line);
                }
                lineItem = line;
            }

            foreach (var psnName in dic.Keys)
            {
                lbPerson.Items.Add(psnName);
            }
            lbPerson.Tag = dic;
        }

        /// <summary>
        /// 获取人物信息
        /// </summary>
        /// <param name="psnName">人物名称</param>
        private void GetPersonInfo(string psnName)
        {
            foreach (Control ctl in gbInfo.Controls)
            {
                if (ctl is TextBox)
                {
                    ctl.Text = "";
                }
            }

            Dictionary<string, List<string>> dic = (Dictionary<string, List<string>>)lbPerson.Tag;
            List<string> psn = dic[psnName];
            string[] psnInfo = psn[0].Split('#');
            lblPsnName.Text = psnInfo[0];
            txt名称.Text = psnInfo[0];
            txt编号.Text = psnInfo[1];
            txt性别.Text = psnInfo[2] == "0" ? "女" : "男";
            switch (int.Parse(psnInfo[3]))
            {
                case 1:
                    txt性格.Text = @"卤莽";
                    break;
                case 2:
                    txt性格.Text = @"冷静";
                    break;
                case 3:
                    txt性格.Text = @"怯懦";
                    break;
                case 4:
                    txt性格.Text = @"高傲";
                    break;
                case 5:
                    txt性格.Text = @"稳重";
                    break;
                case 6:
                    txt性格.Text = @"勇猛";
                    break;
            }
            txt忠诚.Text = psnInfo[4];
            txt武力.Text = psnInfo[5];
            txt统率.Text = psnInfo[6];
            txt智力.Text = psnInfo[7];
            txt政治.Text = psnInfo[8];
            txt魅力.Text = psnInfo[9];
            txt单挑.Text = psnInfo[10];
            switch (psnInfo[10])
            {
                case "0":
                case "1":
                    txt单挑.Text = @"无";
                    break;
                case "2":
                    txt单挑.Text = @"低";
                    break;
                case "3":
                    txt单挑.Text = @"中";
                    break;
                case "4":
                    txt单挑.Text = @"高";
                    break;
                case "5":
                    txt单挑.Text = @"极";
                    break;
            }
            txt军师.Text = psnInfo[11];
            switch (psnInfo[11])
            {
                case "0":
                    txt军师.Text = @"无";
                    break;
                case "1":
                case "2":
                    txt军师.Text = @"低";
                    break;
                case "3":
                    txt军师.Text = @"中";
                    break;
                case "4":
                    txt军师.Text = @"高";
                    break;
                case "5":
                    txt军师.Text = @"极";
                    break;
            }
            txt生年.Text = psnInfo[12];
            txt状态.Text = psnInfo[13] == "0" ? "生" : "死";

            if (psn.Count > 1)
            {
                txt内政语录.Text = psn[1];
            }
            if (psn.Count > 2)
            {
                txt单挑语录1.Text = psn[2];
            }
            if (psn.Count > 3)
            {
                txt单挑语录2.Text = psn[3];
            }
            if (psn.Count > 4)
            {
                txt单挑语录3.Text = psn[4];
            }
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

        //搜索人物
        private void txtPsnName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string psnName = txtPsnName.Text.Trim();
                if (lbPerson.Items.Contains(psnName))
                {
                    lbPerson.SelectedItem = psnName;
                    GetPersonInfo(psnName);
                }
            }
        }

        //查看人物
        private void lbPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPersonInfo(lbPerson.SelectedItem.ToString());
        }
    }
}
