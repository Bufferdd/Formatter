using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Formatter.Model;

namespace Formatter
{
    public interface IView 
    {
        string GetText();
        void SetText(string text);
        void AddText(string text);
        void ShowError(string error);
    }
    public partial class FormatterView : Form, IView
    {
        public FormatterView()
        {
            InitializeComponent();

            RedrawList();
        }
        // Отрисовывает richTextBox в зависимости от коэффициента different. Наибольший возможный - 5
        void RedrawList(int different = 5) 
        {
            int width = Convert.ToInt32(21 * CreateGraphics().DpiX / different);
            int height = Convert.ToInt32(29.7 * CreateGraphics().DpiY / different);

            // Подбор размеров листа А4
            listRichTextBox.Width = width;
            listRichTextBox.Height = height;

            Font font = new Font(listRichTextBox.Font.FontFamily, listRichTextBox.Font.Size / different, listRichTextBox.Font.Style);
            listRichTextBox.Font = font;
        }

        string IView.GetText() 
        {
            return listRichTextBox.Text;
        }
        void IView.SetText(string text) 
        {
            listRichTextBox.Text = text;
        }
        void IView.AddText(string text) 
        {
            listRichTextBox.Text += text;
        }
        void IView.ShowError(string error) 
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
