using System;
using System.Drawing;
using System.Windows.Forms;

namespace Formatter.View
{
    public interface IView
    {
        // Показать форму
        void Show();
        // Закрыть форму
        void Close();
        // Получить текст
        string GetText();
        // Получить выделенный текст
        string GetSelectionText();
        // Получить цвет выделенного текста
        Color GetSelectionTextColor();
        // Получить шрифт выделенного текста
        Font GetSelectionTextFont();
        // Задать новый текст
        void SetText(string text);
        // Добавить текст
        void AddText(string text);
        // Задать цвет выделенного текста
        void SetSelectionTextColor(Color color);
        // Задать шрифт выделенного текста
        void SetSelectionTextFont(Font font);
        // Вывести ошибку
        void ShowError(string error);

        // События с текстом
        event EventHandler Text_TextChanged;
        event EventHandler Text_KeyPress;
        event EventHandler Text_MouseUp;

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

        // Параметры шрифта в правом окне
        void SetFontSize(string fontSize);
        void SetFont(string font);
        void SetFontStyle(string font);
        void SetFontColor(string color);
    }
    public partial class FormatterView : Form, IView
    {
        // События с текстом
        public event EventHandler Text_TextChanged;
        public event EventHandler Text_KeyPress;
        public event EventHandler Text_MouseUp;

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
        }
        void IView.Show() => Application.Run(this);
        void IView.Close() => Application.Exit();
        string IView.GetText() => listRichTextBox.Text;
        string IView.GetSelectionText() => listRichTextBox.SelectedText;
        Color IView.GetSelectionTextColor() => listRichTextBox.SelectionColor;
        Font IView.GetSelectionTextFont() => listRichTextBox.SelectionFont;
        void IView.SetText(string text) => listRichTextBox.Text = text;
       
        void IView.AddText(string text) => listRichTextBox.Text += text;
        void IView.SetSelectionTextColor(Color color) => listRichTextBox.SelectionColor = color;
        void IView.SetSelectionTextFont(Font font) => listRichTextBox.SelectionFont = font;
        void IView.ShowError(string error) => MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        private void listRichTextBox_TextChanged(object sender, EventArgs e) => Text_TextChanged?.Invoke(this, e);

        private void listRichTextBox_KeyPress(object sender, KeyPressEventArgs e) => Text_KeyPress?.Invoke(this, e);
        private void listRichTextBox_MouseUp(object sender, MouseEventArgs e) => Text_MouseUp?.Invoke(this, e);
        void IView.SetFontSize(string fontSize) => fontSizeTextBox.Text = fontSize;
        void IView.SetFont(string font) => fontTextBox.Text = font;
        void IView.SetFontStyle(string fontStyle) => fontStyleTextBox.Text = fontStyle;
        void IView.SetFontColor(string fontColor) => fontColorTextBox.Text = fontColor;
    }
}

