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
    public partial class EnrichedRichTextBoxControl : RichTextBox
    {
        private List<TextArea> textAreas = new List<TextArea>();

        public EnrichedRichTextBoxControl()
        {
            InitializeComponent();

            MouseDown += EnrichedRichTextBoxControl_MouseClick;
        }

        private void EnrichedRichTextBoxControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {   //click event
                ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
                MenuItem menuItem = new MenuItem("Cut");
                menuItem.Click += new EventHandler(Action);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Copy");
                menuItem.Click += new EventHandler(Action);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Paste");
                menuItem.Click += new EventHandler(Action);
                contextMenu.MenuItems.Add(menuItem);

                ContextMenu = contextMenu;
            }
        }

        protected void Action(object sender, EventArgs e)
        {

        }

        public void AddText(string text)
        {

        }

        public class TextArea: IDisposable
        {
            int BeginCharIndex { get; set; }
            int EndCharIndex { get; set; }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
    }
}
