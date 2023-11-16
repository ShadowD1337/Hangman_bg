using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection.Emit;
using Label = System.Windows.Forms.Label;

namespace Бесеница
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog folderDlg = new FolderBrowserDialog();
        Random random = new Random();
        string text = "";
        int wordnum = 1;
        string word = "";
        int lifes = 6;
        bool wrong = true;
        int right = 0;
        int hints = 3;
        int maxhints = 3;
        int rnd = 1;

        public Form1()
        {
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                text = File.ReadAllText(folderDlg.SelectedPath + @"\words.txt");
            }
            InitializeComponent();

            pbox_life.Image = Image.FromFile(folderDlg.SelectedPath + @"\" + lifes + " From Dead.png");
            New();

            foreach (var label in this.Controls.OfType<Label>().Where(name => name.Name.Contains("lbl_word_") && !(name.Name.Equals("lbl_word_1"))))
            {
                int x = Convert.ToInt32(label.Name.Skip(9).First().ToString() + (label.Name.Contains("1") ? label.Name.Skip(10).First().ToString() : null));
                label.Location = new Point(lbl_word_1.Location.X + (29 * (x - 1)), lbl_word_1.Location.Y);

            }
        }

        private void Contains(char letter)
        {
            foreach (var label in this.Controls.OfType<Label>().Where(name => name.Name.Contains("lbl_word_")))
            {
                if (label.Text == Convert.ToString(letter) && label.Visible == false)
                {
                    label.Visible = true;
                    wrong = false;
                    right++;
                }
            }

            if (wrong) lifes--;

            if (right == word.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).Select(c => c.ToString()).Aggregate((a, b) => a + b).Length)
            {
                MessageBox.Show("You Guessed the Word!", "Congratulations!");
                New();
            }

            wrong = true;
            pbox_life.Image = Image.FromFile(folderDlg.SelectedPath + @"\" + lifes + " From Dead.png");

            if (lifes == 0)
            {
                MessageBox.Show("You are DEAD!", "DEAD!");
                New();
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            New();
        }

        private void btn_hint_Click(object sender, EventArgs e)
        {
            if (hints > 0)
            {
                for (double i = 0; i < word.Length / 5 || i < 1; i++)
                {
                    rnd = random.Next(1, word.Length);
                    //textBox1.Text = rnd.ToString();
                    if(this.Controls.OfType<Label>().Where(name => name.Name == "lbl_word_" + Convert.ToString(rnd)).All(c => c.Text != "" && c.Text != " " && !c.Visible))
                    {
                        this.Controls.OfType<Label>().Where(name => name.Name == "lbl_word_" + Convert.ToString(rnd)).All(c => c.Visible = true);
                        right++;
                    }
                    else
                    {
                        rnd = random.Next(1, word.Length);
                        while(!this.Controls.OfType<Label>().Where(name => name.Name == "lbl_word_" + Convert.ToString(rnd)).All(c => c.Text != "" && c.Text != " " && !c.Visible))
                        {
                            rnd = random.Next(1, word.Length);
                        }
                        this.Controls.OfType<Label>().Where(name => name.Name == "lbl_word_" + Convert.ToString(rnd)).All(c => c.Visible = true);
                        right++;
                    }

                }
                hints--;
                btn_hint.Text = "Hint (" + hints + "/" + maxhints + ")";
                if (right == word.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).Select(c => c.ToString()).Aggregate((a, b) => a + b).Length)
                {
                    MessageBox.Show("You Guessed the Word!", "Congratulations!");
                    New();
                }
            }
        }

        private void New()
        {
            textBox1.Visible = false;

            right = 0;
            lifes = 6;
            pbox_life.Image = Image.FromFile(folderDlg.SelectedPath + @"\" + lifes + " From Dead.png");
            wordnum = text.Split(',').Count();
            word = text.Split(',').Skip(random.Next(0, wordnum)).First();
            if (!(word == text.Split(',').First())) word = word.Remove(0, 1).ToString();
            if (word.Length > 3) hints = 3;
            else if (word.Length <= 3) hints = 1;
            maxhints = hints;
            btn_hint.Text = "Hint (" + hints + "/" + hints + ")";

            foreach (var label in this.Controls.OfType<Label>().Where(name => name.Name.Contains("lbl_word_")))
            {
                label.Text = "";
                label.Visible = false;
            }
            for (int i = 1; i <= word.Length; i++)
            {
                foreach (var label in this.Controls.OfType<Label>().Where(name => name.Name == ("lbl_word_" + i)))
                {
                    label.Text = word.Skip(i - 1).First().ToString();
                }
            }
            uscores.Text = "";
            for (int i = 0; i < word.Length; i++)
            {
                if (word.Skip(i).First() != ' ')
                {
                    uscores.Text += "_  ";
                }
                else uscores.Text += "    ";
            }
        }

        private void btn_A_Click(object sender, EventArgs e)
        {
            Contains('a');
        }
        private void btn_B_Click(object sender, EventArgs e)
        {
            Contains('b');
        }
        private void btn_C_Click(object sender, EventArgs e)
        {
            Contains('c');
        }
        private void btn_D_Click(object sender, EventArgs e)
        {
            Contains('d');
        }
        private void btn_E_Click(object sender, EventArgs e)
        {
            Contains('e');
        }
        private void btn_F_Click(object sender, EventArgs e)
        {
            Contains('f');
        }
        private void btn_G_Click(object sender, EventArgs e)
        {
            Contains('g');
        }
        private void btn_H_Click(object sender, EventArgs e)
        {
            Contains('h');
        }
        private void btn_I_Click(object sender, EventArgs e)
        {
            Contains('i');
        }
        private void btn_J_Click(object sender, EventArgs e)
        {
            Contains('j');
        }
        private void btn_K_Click(object sender, EventArgs e)
        {
            Contains('k');
        }
        private void btn_L_Click(object sender, EventArgs e)
        {
            Contains('l');
        }
        private void btn_M_Click(object sender, EventArgs e)
        {
            Contains('m');
        }
        private void btn_N_Click(object sender, EventArgs e)
        {
            Contains('n');
        }
        private void btn_O_Click(object sender, EventArgs e)
        {
            Contains('o');
        }
        private void btn_P_Click(object sender, EventArgs e)
        {
            Contains('p');
        }
        private void btn_Q_Click(object sender, EventArgs e)
        {
            Contains('q');
        }
        private void btn_R_Click(object sender, EventArgs e)
        {
            Contains('r');
        }
        private void btn_S_Click(object sender, EventArgs e)
        {
            Contains('s');
        }
        private void btn_T_Click(object sender, EventArgs e)
        {
            Contains('t');
        }
        private void btn_U_Click(object sender, EventArgs e)
        {
            Contains('u');
        }
        private void btn_V_Click(object sender, EventArgs e)
        {
            Contains('v');
        }
        private void btn_W_Click(object sender, EventArgs e)
        {
            Contains('w');
        }
        private void btn_X_Click(object sender, EventArgs e)
        {
            Contains('x');
        }
        private void btn_Y_Click(object sender, EventArgs e)
        {
            Contains('y');
        }
        private void btn_Z_Click(object sender, EventArgs e)
        {
            Contains('z');
        }

    }
}
