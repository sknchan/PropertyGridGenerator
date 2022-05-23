using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PropertyGridGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool chk = false;
            foreach(ListViewItem v in itemlist.Items)
            {
                if(v.Text == name.Text)
                {
                    chk = true;
                    break;
                }
            }
            if(chk)
            {
                return;
            }
            if(name.Text.Length ==0 || des.Text.Length ==0 || category.Text.Length ==0)
            {
                return;
            }
            ListViewItem item = new ListViewItem();
            item.Text = name.Text;
            item.SubItems.Add(des.Text);
            item.SubItems.Add((radioButton1.Checked) ? "1":"0");
            item.SubItems.Add(category.Text);
            string aa = tp_string.Checked ? "string" : tp_bool.Checked ? "bool" : "int";
            item.SubItems.Add(aa);
            itemlist.Items.Add(item);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(classte.Text.Length==0 || space.Text.Length ==0)
            {
                return;
            }
            string result = "";
            //base
            result += "using System.ComponentModel;\n namespace " + space.Text +"\n";
            result += "{\n public class "+classte.Text+"\n{\n";
            //base end

            foreach(ListViewItem item in itemlist.Items)
            {
                if (item.SubItems[4].Text == "string")
                {
                    
                    result += "public string " + item.Text + ";\n";
                }
                else if (item.SubItems[4].Text == "bool")
                {
                    result += "public bool " + item.Text + ";\n";
                }
                else if (item.SubItems[4].Text == "int")
                {
                    result += "public int " + item.Text + ";\n";
                }
            }
            foreach (ListViewItem item in itemlist.Items)
            {

                result += "\n\n[DisplayName(\"" + item.Text + "\")]\n[Description(\"" + item.SubItems[1].Text + "\")]\n[Category(\"" + item.SubItems[3].Text + "\")]\n";
                if (item.SubItems[2].Text == "1")
                {
                    result += "[ReadOnly(true)]\n";
                }
                else
                {
                    result += "[ReadOnly(false)]\n";
                }
                if (item.SubItems[4].Text == "string")
                {
                    result += "public string _" + item.Text;
                    result += "\n{\n get { return this."+item.Text +"; }\n";
                    result += "\n set { this." + item.Text + " = value; }\n}\n";
                }
                else if (item.SubItems[4].Text == "bool")
                {
                    result += "public bool _" + item.Text;
                    result += "\n{\n get { return this." + item.Text + "; }\n";
                    result += "\n set { this." + item.Text + " = value; }\n}\n";
                }
                else if (item.SubItems[4].Text == "int")
                {
                    result += "public int _" + item.Text;
                    result += "\n{\n get { return this." + item.Text + "; }\n";
                    result += "\n set { this." + item.Text + " = value; }\n}\n";
                }
            }
            result += "\n}\n}";
            try
            {
                Clipboard.SetText(result);
                MessageBox.Show("복사되었습니다");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(itemlist.SelectedItems.Count ==0)
            { return; }
            itemlist.Items.Remove(itemlist.SelectedItems[0]);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> Namelist = new List<string>();
            List<string> Deslist = new List<string>();
            string read = "0";
            string inttext = "1";
            string category = "MainSetting";
            Namelist.Add("MinRunningTime");
            Namelist.Add("MaxRunningTime");
            Namelist.Add("MinRestTime");
            Namelist.Add("MaxRestTime");
            Namelist.Add("MinDelayTime");
            Namelist.Add("MaxDelayTime");
            Deslist.Add("최소 작동 시간");
            Deslist.Add("최대 작동 시간");
            Deslist.Add("최소 휴식 시간");
            Deslist.Add("최대 휴식 시간");
            Deslist.Add("최소 친추 시간");
            Deslist.Add("최대 친추 시간");

            space.Text = "FBBOT.Data";
            classte.Text = "Setting";
            int temp = 0;
            foreach (var la in Namelist)
            {
               
                ListViewItem item = new ListViewItem();
                item.Text = Namelist[temp];
                item.SubItems.Add(Deslist[temp]);
                item.SubItems.Add(read);
                item.SubItems.Add(category);
                item.SubItems.Add(inttext);
                itemlist.Items.Add(item);
                temp++;
            }
        }
    }
    }

