using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Formatter.Model;
using Formatter.View;

namespace Formatter.Presenter
{
    public interface IPresenter 
    {
        void Run();
    }
    public class FormatterPresenter : IPresenter
    {
        private IModel _model;
        private IView _view;

        public FormatterPresenter(IView view, IModel model)
        {
            _view = view;
            _model = model;

            if (_view != null)
            {
                _view.MenuFile_CreateClick += CreateFile;
                _view.MenuFile_OpenClick += OpenFile;
                _view.MenuFile_ClearClick += Clear;
                _view.MenuFile_SaveClick += SaveFile;
                _view.MenuFile_SaveHowClick += SaveHowFile;
                _view.MenuFile_PrintClick += PrintFile;

                _view.MenuFormat_ColorClick += ChooseColor;
                _view.MenuFormat_FontClick += ChooseFormat;

                _view.MenuReference_AboutProgramClick += AboutProgramShow;
                _view.MenuReference_AuthorsClick += AuthorsShow;

                _view.Text_TextChanged += Text_TextChanged;
                _view.Text_KeyPress += Text_KeyPress;
                _view.Text_MouseUp += Text_MouseUp;

                _view.SetSelectionTextColor(Color.Black);
                _view.SetSelectionTextFont(new Font("Times New Roman", 14, FontStyle.Regular));
            }

            if (_model != null)
            {
                _model.GetColors().Add(0, Color.Black);
                _model.GetFonts().Add(0, new Font("Times New Roman", 14, FontStyle.Regular));
            }
        }

        void IPresenter.Run() 
        {
            _view.Show();
        }
        public void CreateFile(object sender, EventArgs e) 
        {
            string filename = @"Новый formatter.frmt";

            using (StreamWriter stream = new StreamWriter(filename)) { }

            if(_model != null)
                _model.SetFilename(filename);
        }
        public void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "frmt files (*.frmt)|*.frmt|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                _model.GetColors().Clear();
                _model.SetText("");
                _model.GetFonts().Clear();

                _model.LoadFromFile(fileDialog.FileName);
                _model.SetFilename(fileDialog.FileName);
                _view.SetText(_model.GetText());

                int length = _model.GetText().Length;
                int borderColor = length;
                int borderFont = length;
                for (int i = length - 1; i >= 0; --i)
                {
                    if (_model.GetColors().ContainsKey(i))
                    {
                        _view.SetSelectionLength(borderColor - i);
                        _view.SetSelectionStart(i);

                        _view.SetSelectionTextColor(_model.GetColors()[i]);
                        borderColor = i;
                    }

                    if (_model.GetFonts().ContainsKey(i))
                    {
                        _view.SetSelectionLength(borderFont - i);
                        _view.SetSelectionStart(i);

                        _view.SetSelectionTextFont(_model.GetFonts()[i]);
                        borderFont = i;
                    }
                }
            }
        }
        public void Clear(object sender, EventArgs e) 
        {
            _view.SetText("");
            _model.SetText("");
            _model.GetColors().Clear();
            _model.GetFonts().Clear();
        }
        public void SaveFile(object sender, EventArgs e)
        {
            if (_model.GetFilename() == null)
                SaveHowFile(sender, e);
            else
                _model.SaveInFile(null);
        }
        public void SaveHowFile(object sender, EventArgs e) 
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.Filter = "frmt files (*.frmt)|*.frmt|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                _model.SaveInFile(fileDialog.FileName);
                _model.SetFilename(fileDialog.FileName);
            }
        }
        public void PrintFile(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDialog.Document.PrintPage += PrintDocument_PrintPage;

                printDialog.Document.Print();
            }
        }
        public void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 12);

            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            e.Graphics.DrawString(_model.GetText(), font, Brushes.Black, x, y);
        }
        public void ChooseColor(object sender, EventArgs e) 
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _view.SetSelectionTextColor(colorDialog.Color);

                if (_model.GetColors().ContainsKey(_view.GetSelectionStart()))
                    _model.GetColors().Remove(_view.GetSelectionStart());

                _model.GetColors().Add(_view.GetSelectionStart(), colorDialog.Color);

                int border = _view.GetSelectionStart() + _view.GetSelectionLength();
                if (border < _view.GetText().Length)
                {
                    if (_model.GetColors().ContainsKey(border))
                        _model.GetColors().Remove(border);

                    _model.GetColors().Add(border, Color.Black);
                }
            }
        }
        public void ChooseFormat(object sender, EventArgs e)
        {
            try
            {
                FontDialog fontDialog = new FontDialog();

                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    _view.SetSelectionTextFont(fontDialog.Font);

                    if (_model.GetFonts().ContainsKey(_view.GetSelectionStart()))
                        _model.GetFonts().Remove(_view.GetSelectionStart());

                    _model.GetFonts().Add(_view.GetSelectionStart(), fontDialog.Font);

                    int border = _view.GetSelectionStart() + _view.GetSelectionLength();
                    if (border < _view.GetText().Length)
                    {
                        if(_model.GetFonts().ContainsKey(border))
                            _model.GetFonts().Remove(border);

                        _model.GetFonts().Add(border, new Font("Times New Roman", 14f, FontStyle.Regular));
                    }
                }
            }
            catch (Exception ex) 
            {
                _view.ShowError(ex.Message);
            }
        }
        public void AboutProgramShow(object sender, EventArgs e) 
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = assembly.GetName();

            string programInfo = $"Название программы: {assemblyName.Name}\n" +
                                    $"Версия: {assemblyName.Version}\n" +
                                    $"Описание программы: Программа Formatter является простым текстовым редактором, который предоставляет пользователям возможность редактировать и форматировать текстовые файлы. \n" + 
                                    $"Программа позволяет сохранять файлы в формате .frmt, а также печатать текст в принтере.";

            // Создание и отображение окна с информацией о программе
            MessageBox.Show(programInfo, "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void AuthorsShow(object sender, EventArgs e)
        {
            MessageBox.Show("Авторы программы:\n\n" +
                            "Гусев Павел Александрович - разработчик модели (Model)\n" +
                            "Доможиров Владислав Вячеславович - разработчик представления (View)\n" +
                            "Ковацкий Алексей Александрович - разработчик презентера (Presenter)\n",
                            "О программе",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void Text_TextChanged(object sender, EventArgs e)
        {
            _model.SetText(_view.GetText());
        }
        public void Text_KeyPress(object sender, EventArgs e)
        {
            // Тут возможен контроль над вводом
        }
        public void Text_MouseUp(object sender, EventArgs e)
        {
            try
            {
                string text = _view.GetSelectionText();

                if (text != null)
                {
                    Font font = _view.GetSelectionTextFont();
                    Color color = _view.GetSelectionTextColor();

                    if (font != null)
                    {
                        _view.SetFontSize(font.Size.ToString());
                        _view.SetFontStyle(font.Style.ToString());
                        _view.SetFont(font.Name.ToString());
                    }

                    if(color != null)
                        _view.SetFontColor(color.ToString());
                }
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }
    }
}
