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

        // События с текстом
        event EventHandler Text_TextChanged;
        event EventHandler Text_KeyPress;

        // События с меню
        event EventHandler MenuFile_CreateClick;
        event EventHandler MenuFile_OpenClick;
        event EventHandler MenuFile_SaveClick;
        event EventHandler MenuFile_SaveHowClick;
        event EventHandler MenuFile_ClearClick;
        event EventHandler MenuFile_PrintClick;

        // События с форматом
        event EventHandler MenuFormat_ColorClick;
        event EventHandler MenuFormat_FontClick;

        // События с меню-справкой
        event EventHandler MenuReference_AboutProgramClick;
        event EventHandler MenuReference_AuthorsClick;
    }
    public partial class FormatterView : Form, IView
    {
        // События с текстом
        public event EventHandler Text_TextChanged;
        public event EventHandler Text_KeyPress;

        // События с меню
        public event EventHandler MenuFile_CreateClick;
        public event EventHandler MenuFile_OpenClick;
        public event EventHandler MenuFile_SaveClick;
        public event EventHandler MenuFile_SaveHowClick;
        public event EventHandler MenuFile_ClearClick;
        public event EventHandler MenuFile_PrintClick;

        // События с форматом
        public event EventHandler MenuFormat_ColorClick;
        public event EventHandler MenuFormat_FontClick;

        // События с меню-справкой
        public event EventHandler MenuReference_AboutProgramClick;
        public event EventHandler MenuReference_AuthorsClick;
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

        private void createToolStripMenuItem_Click(object sender, EventArgs e) => MenuFile_CreateClick?.Invoke(this, e);

        private void openToolStripMenuItem_Click(object sender, EventArgs e) => MenuFile_OpenClick?.Invoke(this, e);

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) => MenuFile_SaveClick?.Invoke(this, e);

        private void saveHowToolStripMenuItem_Click(object sender, EventArgs e) => MenuFile_SaveHowClick?.Invoke(this, e);

        private void clearToolStripMenuItem_Click(object sender, EventArgs e) => MenuFile_ClearClick?.Invoke(this, e);

        private void printToolStripMenuItem_Click(object sender, EventArgs e) => MenuFile_PrintClick?.Invoke(this, e);

        private void colorToolStripMenuItem_Click(object sender, EventArgs e) => MenuFormat_ColorClick?.Invoke(this, e);

        private void fontToolStripMenuItem_Click(object sender, EventArgs e) => MenuFormat_FontClick?.Invoke(this, e);

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e) => MenuReference_AboutProgramClick?.Invoke(this, e);

        private void authorsToolStripMenuItem1_Click(object sender, EventArgs e) => MenuReference_AuthorsClick?.Invoke(this, e);
    }
}

