using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BIS4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string text;
        private void start_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            HashCalculator();
        }

        void HashCalculator()
        {
            const int k = 64, n=32;
            ASCIIEncoding e = new ASCIIEncoding();
            byte[] mass;
            BitArray hi = new BitArray(e.GetBytes(h.Text));
            BitArray m;
            string str="";

            for (int i = 0; i < text.Length/(k/8-n/8); i ++){
                str = "";
                for (int j = 0; j < n / 8; j++) {
                    str += text[i*4 + j].ToString();
                }
                m = new BitArray(e.GetBytes(str));
                hi.Xor(m);
                hi = Shift(hi, 2);
                hi.Xor(m);
                mass = new byte[hi.Length/8];
                hi.CopyTo(mass, 0);
                str = "";
                foreach (byte a in mass)
                    str += (a.ToString()+" ");
                listBox1.Items.Add(str);
            }
            listBox1.Items.Add("--");
            listBox1.Items.Add(str);
            }

        static BitArray Shift(BitArray bitArray, int count)
        {
            for (int i = 0; i < count; i++) {
                BitArray bit = new BitArray(1);
                bit[0] = bitArray[bitArray.Count - 1];
                for (int j = bitArray.Count - 1; j >= 1; j--)
                    bitArray[j] = bitArray[j - 1];
                bitArray[0] = bit[0];
            }
            return bitArray;
        }
        private void load_Click(object sender, EventArgs e)
        {
            string filePath = "";
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                //ofd.InitialDirectory = "c:\\";
                ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.FilterIndex = 2;
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //File f = new File(filePath);
                    filePath = ofd.FileName;

                    var fileStream = ofd.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        text = reader.ReadToEnd();
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
