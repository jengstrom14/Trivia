using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trivia
{
    public partial class Form1 : Form
    {
        private Form1Model MODEL;

        public Form1()
        {
            InitializeComponent();
            MODEL = new Form1Model();
            UpdateFields();
            button1.Enabled = false;
        }

        private void UpdateFields()
        {
            label1.Text = "Category: " + WebUtility.HtmlDecode(MODEL.CategoryText);
            label2.Text = "Difficulty: " + WebUtility.HtmlDecode(MODEL.Difficulty_Text);
            label3.Text = "Question #: " + MODEL.QuestionNumber;
            textBox1.Text = WebUtility.HtmlDecode(MODEL.QuestionText);

            if (MODEL.CurQuestion.Type == "boolean")
            {
                radioButton1.Text = "True";
                radioButton2.Text = "False";
                radioButton3.Enabled = false;
                radioButton3.Visible = false;
                radioButton3.Text = "";
                radioButton4.Text = "";
                radioButton4.Enabled = false;
                radioButton4.Visible = false;
            } else if( MODEL.CurQuestion.Type == "multiple")
            {
                radioButton1.Text = WebUtility.HtmlDecode(MODEL.Responses[0]);
                radioButton2.Text = WebUtility.HtmlDecode(MODEL.Responses[1]);
                radioButton3.Text = WebUtility.HtmlDecode(MODEL.Responses[2]);
                radioButton4.Text = WebUtility.HtmlDecode(MODEL.Responses[3]);
            }
            this.Refresh();
            this.button2.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;

            radioButton3.Visible = true;
            radioButton4.Visible = true;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;

            radioButton1.Font = new Font(radioButton1.Font, FontStyle.Regular);
            radioButton2.Font = new Font(radioButton2.Font, FontStyle.Regular);
            radioButton3.Font = new Font(radioButton3.Font, FontStyle.Regular);
            radioButton4.Font = new Font(radioButton4.Font, FontStyle.Regular);

            MODEL.NextQuestion();
            UpdateFields();
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
            if (MODEL.CurQuestion.Type == "boolean")
            {
                if (MODEL.CurQuestion.Correct_answer == "True") radioButton1.Font = new Font(radioButton1.Font, FontStyle.Bold);
                else radioButton2.Font = new Font(radioButton2.Font, FontStyle.Bold);

            }
            else if (MODEL.CurQuestion.Type == "multiple")
            {
                var correctAnswerFormatted = WebUtility.HtmlDecode(MODEL.CurQuestion.Correct_answer);
                if (radioButton1.Text == correctAnswerFormatted) radioButton1.Font = new Font(radioButton1.Font, FontStyle.Bold);
                else if (radioButton2.Text == correctAnswerFormatted) radioButton2.Font = new Font(radioButton2.Font, FontStyle.Bold);
                else if (radioButton3.Text == correctAnswerFormatted) radioButton3.Font = new Font(radioButton3.Font, FontStyle.Bold);
                else if (radioButton4.Text == correctAnswerFormatted) radioButton4.Font = new Font(radioButton4.Font, FontStyle.Bold);
            }
            button1.Enabled = true;
        }
    }
}
