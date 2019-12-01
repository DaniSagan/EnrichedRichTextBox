using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnrichedRichTextBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Font titleFont = new Font("", 16f, FontStyle.Bold);
            Font subtitleFont = new Font("", 14f, FontStyle.Italic);
            Font textFont = new Font("", 12f, FontStyle.Regular);
            List<Article> articles = new List<Article>();
            for(int k = 0; k < 10; k++)
            {
                articles.Add(new Article { Title = $"This is title {k}", SubTitle = $"This is the subtitle {k}", Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum." });
            }

            foreach(Article article in articles)
            {
                TextArea articleArea = new TextArea { Text = "{0}\n\n{1}\n\n{2}\n\n", Color = Color.Black, Font = titleFont, Tag = article };
                TextArea title = articleArea.AddChild(article.Title, titleFont, Color.Black);
                title.Tag = article;
                articleArea.AddChild(article.SubTitle, subtitleFont, Color.Black);
                articleArea.AddChild(article.Text, textFont, Color.Black);
                enrichedControl1.Add(articleArea);
            }

            //TextArea parentArea = new TextArea
            //{
            //    Text = "Hello {0}!!",
            //    Font = new Font("Consola Sans", 12f, FontStyle.Bold),
            //    Color = Color.Black
            //};

            //TextArea childArea = parentArea.AddChild("{0}\nworld", new Font("Comic Sans", 18f, FontStyle.Italic), Color.Red);
            //childArea.AddChild("wonderful", new Font("", 16f, FontStyle.Bold), Color.Green);

            //enrichedControl1.Add(parentArea);

            //TextArea t = enrichedControl1.GetTextAreaAtIndex(8);

            enrichedControl1.Render();
        }

        private void enrichedControl1_MouseDown(object sender, MouseEventArgs e)
        {
            TextArea t = enrichedControl1.GetTextAreaAtPosition(e.Location);
            if(t.Tag != null)
            {
                MessageBox.Show(((Article)t.Tag).Title ?? "Nothing");
            }
        }

        private void enrichedControl1_MouseMove(object sender, MouseEventArgs e)
        {
            List<TextArea> t = enrichedControl1.GetTextAreasAtPosititon(e.Location);
            List<Article> articles = t.Select(x => x.Tag).Where(x => x != null).Cast<Article>().ToList();
            if (articles.Count > 0)
            {
                textBox1.Text = articles.First().Title;
                if (enrichedControl1.Cursor != Cursors.Hand)
                {
                    enrichedControl1.Cursor = Cursors.Hand;
                }
            }
            else
            {
                textBox1.Text = string.Empty;
                if (enrichedControl1.Cursor != Cursors.IBeam)
                {
                    enrichedControl1.Cursor = Cursors.IBeam;
                }
            }
        }
    }
}
