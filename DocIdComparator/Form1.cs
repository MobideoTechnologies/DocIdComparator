using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DocIdComparator
{
    public partial class Form1 : Form
    {
        string firstXmlStr, seconedXmlStr;
        string[] docIdsInFirstFile, docIdsInSecondFile;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var doc = XDocument.Load(openFileDialog1.FileName);
            firstXmlStr = doc.ToString();
            button1.Text = "File Loaded";

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var doc = XDocument.Load(openFileDialog1.FileName);
            seconedXmlStr = doc.ToString();
            button2.Text = "File Loaded";
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            docIdsInFirstFile = firstXmlStr.Split(new[] {"docid=\""},StringSplitOptions.RemoveEmptyEntries);
            for(int i=0;i< docIdsInFirstFile.Length;i++)
            {
                docIdsInFirstFile[i] = docIdsInFirstFile[i].Substring(0,docIdsInFirstFile[i].IndexOf('"'));
            }
            docIdsInSecondFile = seconedXmlStr.Split(new[] { "docid=\"" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < docIdsInSecondFile.Length; i++)
            {
                docIdsInSecondFile[i] = docIdsInSecondFile[i].Substring(0, docIdsInSecondFile[i].IndexOf('"'));
            }
            bool sameSize = docIdsInFirstFile.Length == docIdsInSecondFile.Length;
            if(sameSize)
                label1.Text = "The two files have the same number of docids";
            else
                label1.Text = "The two files doesn't have the same number of docids.\nfirst with "+ docIdsInFirstFile.Length+" and second with "+ docIdsInSecondFile.Length;

            if (sameSize)
            {
                progressBar1.Maximum = docIdsInFirstFile.Length;
                for (int i = 1; i < docIdsInFirstFile.Length; i++)//first isn't a docid
                {
                        if (docIdsInFirstFile.Contains(docIdsInSecondFile[i]))
                            listBox1.Items.Add(docIdsInFirstFile[i] + " was found in both files");
                        progressBar1.Value = i;
                }
            }

            if(listBox1.Items.Count==0)
                label1.Text = label1.Text + "\n all docids are different";
            else
                label1.Text = label1.Text + "\n there are duplicates of docids";
        }
    }
}
