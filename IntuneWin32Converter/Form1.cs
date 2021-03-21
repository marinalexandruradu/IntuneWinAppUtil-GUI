using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace IntuneWin32Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            groupBox1.Show();
            groupBox2.Hide();
            groupBox3.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            string locatie = textBox1.Text;
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                toolStripStatusLabel1.Text = "Please select a source folder";
                statusStrip1.BackColor = Color.Red;
                statusStrip1.ForeColor = Color.White;
            }
            else { 
            groupBox1.Hide();
         
            string[] filePaths = Directory.GetFiles(@locatie, "*.*",
                                         SearchOption.TopDirectoryOnly);
            listBox1.Items.Clear();
            string str = "";
            foreach (string file in filePaths)
            {
                str = str + ", " + file;
                    string letstry = file.Replace(textBox1.Text, "");
                    string letstry2 = letstry.Replace(@"\", "");
                    listBox1.Items.Add(letstry2);

            }
            groupBox2.Show();
            groupBox3.Hide();
                toolStripStatusLabel1.Text = "Ready";
                statusStrip1.BackColor = Color.Transparent;
                statusStrip1.ForeColor = Color.Black;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string textfromlist = listBox1.GetItemText(listBox1.SelectedItem);
            if (String.IsNullOrEmpty(textfromlist))
            {
                toolStripStatusLabel1.Text = "Please select a source setup file";
                statusStrip1.BackColor = Color.Red;
                statusStrip1.ForeColor = Color.White;
            }
            else
            {
                groupBox3.Show();
                button8.Hide();
                groupBox1.Hide();
                groupBox2.Hide();
                toolStripStatusLabel1.Text = "Ready";
                statusStrip1.BackColor = Color.Transparent;
                statusStrip1.ForeColor = Color.Black;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox1.Show();
            groupBox2.Hide();
            groupBox3.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string locatie = textBox1.Text;
            string[] filePaths = Directory.GetFiles(@locatie, "*.*",
                                         SearchOption.TopDirectoryOnly);
            listBox1.Items.Clear();
            string str = "";
            foreach (string file in filePaths)
            {
                str = str + ", " + file;
                string letstry = file.Replace(textBox1.Text, "");
                string letstry2 = letstry.Replace(@"\", "");
                listBox1.Items.Add(letstry2);

            }
            groupBox1.Hide();
            groupBox2.Show();
            groupBox3.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedPath ="";
            var t = new Thread((ThreadStart)(() => {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() == DialogResult.Cancel)
                    return;

                selectedPath = fbd.SelectedPath;
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            Console.WriteLine(selectedPath);
            // string folderPath = "";
            //FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            //if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            //{
            //  folderPath = folderBrowserDialog1.SelectedPath;
            textBox1.Text = selectedPath;
            //}
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string selectedPath = "";
            var t = new Thread((ThreadStart)(() => {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() == DialogResult.Cancel)
                    return;

                selectedPath = fbd.SelectedPath;
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            Console.WriteLine(selectedPath);
            // string folderPath = "";
            //FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            //if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            //{
            //  folderPath = folderBrowserDialog1.SelectedPath;
            textBox3.Text = selectedPath;
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                toolStripStatusLabel1.Text = "Please select an output folder";
                statusStrip1.BackColor = Color.Red;
                statusStrip1.ForeColor = Color.White;
            }
            else {
                string textfromlist = listBox1.GetItemText(listBox1.SelectedItem);
                string letstry = textfromlist.Replace(textBox1.Text,"");
                string letstry2 = letstry.Replace(@"\", "");
               
                string appPath = Application.StartupPath;
                

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = appPath + @"\IntuneWinAppUtil.exe";
                char c = (char)34;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = " -c " + c + textBox1.Text + c + " -s " +  c + letstry2 + c + " -o " + c + textBox3.Text + c + " -q";
                Console.WriteLine(startInfo.Arguments);
                try
                {
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                        toolStripStatusLabel1.Text = "Conversion succesfull";
                        statusStrip1.BackColor = Color.Green;
                        statusStrip1.ForeColor = Color.White;
                        button8.Show();
                    }
                }
                catch
                {
                    toolStripStatusLabel1.Text = "Conversion error";
                    statusStrip1.BackColor = Color.Red;
                    statusStrip1.ForeColor = Color.White;
                }
             
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @textBox3.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string appPath = Application.StartupPath;
            string IntuneFileLocation = appPath + @"\IntuneWinAppUtil.exe";
            if (!File.Exists(IntuneFileLocation))
            {
                bool Download = Program.DownloadFromGitHub();

                if (Download)
                {
                    toolStripStatusLabel1.Text = "IntuneWinAppUtil.exe succesfully downloaded from GitHub. Ready to convert!";
                    statusStrip1.BackColor = Color.Green;
                    statusStrip1.ForeColor = Color.White;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Could not download IntuneWinAppUtil.exe from GitHub. Please download it manually and place it near the exe";
                    statusStrip1.BackColor = Color.Red;
                    statusStrip1.ForeColor = Color.White;
                }
            }
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Download = Program.DownloadFromGitHub();

            if (Download)
            {
                toolStripStatusLabel1.Text = "IntuneWinAppUtil.exe succesfully downloaded from GitHub. Ready to convert!";
                statusStrip1.BackColor = Color.Green;
                statusStrip1.ForeColor = Color.White;
            }
            else {
                toolStripStatusLabel1.Text = "Could not download IntuneWinAppUtil.exe from GitHub. Please download it manually and place it near the exe";
                statusStrip1.BackColor = Color.Red;
                statusStrip1.ForeColor = Color.White;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.alexandrumarin.com/");
        }
    }
}
