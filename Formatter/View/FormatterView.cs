using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formatter.View
{
    public interface IView
    {
        // Получить текст
        string GetText();
        // Задать новый текст
        void SetText(string text);
        // Добавить текст
        void AddText(string text);
        // Вывести ошибку
        void ShowError(string error);

        void 
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
            // Подбор размеров листа А4
            double width = 21 * CreateGraphics().DpiX / different;
            double height = 29.7 * CreateGraphics().DpiY / different;

            listRichTextBox.Width = Convert.ToInt32(width);
            listRichTextBox.Height = Convert.ToInt32(height);

            Font font = new Font(listRichTextBox.Font.FontFamily, listRichTextBox.Font.SizeInPoints, listRichTextBox.Font.Style);
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

