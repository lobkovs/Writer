using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatcherWriter
{
    public partial class Form1 : Form
    {
        string[] tag = { "ERROR", "DEBUG", "INFO", "WARNING" };
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
                {
                    var rnd = new Random();
                    var text = new StringBuilder();
                    var index = rnd.Next(0, tag.Length);

                    if (checkBox_timeInfo.Checked == true)
                        text.Append(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " [" + tag[index] + "] ");

                    if (checkBox1.Checked == true)
                        text.Append(textBox2.Text + "\n");
                    else
                        text.Append(textBox2.Text);

                    File.AppendAllText(textBox1.Text, text.ToString());

                    richTextBox1.AppendText(String.Format("В файл {0}, записано {1}", textBox1.Text, text.ToString()));
                }
            } catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string scanDir = "c:\\WORK\\WatcherWriter\\Test\\";
                string pattern = @"ret-value=1";
                string forText = "";
                RegexOptions options = RegexOptions.IgnoreCase;
                // Discover fileName in directory
                foreach (string fileName in Directory.GetFiles(scanDir))
                {
                    forText = fileName;
                    string[] lines = File.ReadAllLines(fileName);
                    if (lines.Length > 0)
                    {
                        // Discover lines in file
                        foreach(string line in lines)
                        {
                            if (Regex.IsMatch(line, pattern, options) == true)
                            {
                                forText += " - OK;\n";
                                break;
                            }
                        }
                        forText += " - No;\n";
                    }
                    richTextBox1.AppendText(forText);
                }

            } catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }
        }

        private void timerButton_Click(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(timerTextBox.Text);

            if (timer1.Enabled == true)
            {
                timerButton.Text = "Писать";
                timer1.Stop();
            } else
            {
                timerButton.Text = "Остановить";
                timer1.Start();
            }
            
        }

        private void timerWrite(object sender, EventArgs e)
        {
            button1.PerformClick();
        }
    }
}
