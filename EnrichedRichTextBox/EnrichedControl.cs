using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnrichedRichTextBox
{
    public partial class EnrichedControl : UserControl
    {
        private List<TextArea> textAreaList = new List<TextArea>();

        public EnrichedControl()
        {
            InitializeComponent();
        }

        public new event MouseEventHandler MouseDown;
        public new event MouseEventHandler MouseMove;

        public void Add(TextArea textArea)
        {
            textAreaList.Add(textArea);
        }

        public void Clear()
        {
            textAreaList.Clear();
        }

        public void Render()
        {
            foreach(TextArea textArea in textAreaList)
            {
                textArea.Render(richTextBox);
            }
        }

        public List<TextArea> GetTextAreasAtPosititon(Point position)
        {
            List<TextArea> res = new List<TextArea>();
            TextArea currentTextArea = GetTextAreaAtPosition(position);
            while (currentTextArea != null)
            {
                res.Add(currentTextArea);
                currentTextArea = currentTextArea.Parent;
            }
            return res;
        }

        public TextArea GetTextAreaAtIndex(int index)
        {
            int currentIndex = index;
            foreach(TextArea textArea in textAreaList)
            {
                TextArea foundTextArea = textArea.GetTextAreaAtIndex(currentIndex);
                if(foundTextArea != null)
                {
                    return foundTextArea;
                }
                else
                {
                    currentIndex -= textArea.Length;
                }
            }
            return null;
        }

        public TextArea GetTextAreaAtPosition(Point position)
        {
            return GetTextAreaAtIndex(richTextBox.GetCharIndexFromPosition(position));
        }

        private void richTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown?.Invoke(sender, e);
        }

        private void richTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove?.Invoke(sender, e);
        }

        public override Cursor Cursor { get => base.Cursor; set { base.Cursor = value; richTextBox.Cursor = value; } }
    }
}
