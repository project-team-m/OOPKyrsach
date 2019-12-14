using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace КурсоваяООП
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

        }

        //открыть файл
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            textBox2.Text = "";
        }

        //закодировать
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Text = "Закодированная последовательность";
            LZ77 LZ = new LZ77();
            textBox2.Text = LZ.Kod(textBox1.Text);
        }

        //декодировать
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Text = "Декодированная последовательность";
            textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            LZ77 LZ = new LZ77();
            textBox2.Text = LZ.Dekod(textBox1.Text);
        }

        //сохранить в файл
        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            File.WriteAllText(saveFileDialog1.FileName, textBox2.Text);
            MessageBox.Show("Сохранено");
        }
    }
}
