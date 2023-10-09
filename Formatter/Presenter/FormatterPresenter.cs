using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
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
                _model.LoadFromFile(fileDialog.FileName);
                _model.SetFilename(fileDialog.FileName);
            }
        }
        public void Clear(object sender, EventArgs e) 
        {
            _view.SetText("");
            _model.SetText("");
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
    }
}
