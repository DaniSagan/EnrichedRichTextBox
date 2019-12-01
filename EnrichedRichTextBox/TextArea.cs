using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnrichedRichTextBox
{
    public class TextArea
    {
        public TextArea Parent { get; private set; }
        public List<TextArea> Children { get; private set; } = new List<TextArea>();
        public string Text { get; set; }
        public Font Font { get; set; }
        public Color Color { get; set; }
        public object Tag { get; set; }

        public override string ToString()
        {
            return string.Format(Text, Children.Select(x => x.ToString()).ToArray());
        }

        public TextArea AddChild(string text, Font font, Color? color)
        {
            TextArea childTextArea = new TextArea
            {
                Parent = this,
                Text = text,
                Font = font ?? this.Font,
                Color = color ?? this.Color
            };
            Children.Add(childTextArea);
            return childTextArea;
        }

        public void Render(RichTextBox richTextBox)
        {
            string currentString = string.Empty;
            bool readingToken = false;
            foreach (char c in Text)
            {
                if (!readingToken)
                {
                    if (c == '{')
                    {
                        RenderText(richTextBox, currentString);
                        currentString = string.Empty;
                        readingToken = true;
                    }
                    else
                    {
                        currentString += c;
                    }
                }
                else
                {
                    if (c == '}')
                    {
                        int index = int.Parse(currentString);
                        Children[index].Render(richTextBox);
                        currentString = string.Empty;
                        readingToken = false;
                    }
                    else
                    {
                        currentString += c;
                    }
                }
            }
            if (!readingToken && currentString != string.Empty)
            {
                RenderText(richTextBox, currentString);
            }
        }

        public int Length => this.ToString().Length;

        public TextArea GetTextAreaAtIndex(int anIndex)
        {
            string currentString = string.Empty;
            int currentIndex = 0;
            bool readingToken = false;
            foreach (char c in Text)
            {
                if (!readingToken)
                {
                    if (c == '{')
                    {
                        //RenderText(richTextBox, currentString);
                        currentString = string.Empty;
                        readingToken = true;
                    }
                    else
                    {
                        currentString += c;
                        if(currentIndex == anIndex)
                        {
                            return this;
                        }
                        else
                        {
                            currentIndex++;
                        }
                    }
                }
                else
                {
                    if (c == '}')
                    {
                        int index = int.Parse(currentString);
                        TextArea childTextArea = Children[index].GetTextAreaAtIndex(anIndex - currentIndex);
                        if (childTextArea != null)
                        {
                            return childTextArea;
                        }
                        else
                        {
                            currentIndex += Children[index].Length;
                            currentString = string.Empty;
                            readingToken = false;
                        }
                    }
                    else
                    {
                        currentString += c;
                    }
                }
            }
            return null;
        }

        private void RenderText(RichTextBox richTextBox, string text)
        {
            richTextBox.SelectionFont = Font ?? Parent?.Font ?? richTextBox.Font;
            //richTextBox.SelectionColor = (Color ?? Parent?.Color) ?? richTextBox.ForeColor;
            richTextBox.SelectionColor = Color;
            richTextBox.AppendText(text);
        }
    }
}
