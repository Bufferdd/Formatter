using System;
using System.IO;
using System.Text;

using Formatter.Model;
using Formatter.View;

namespace Formatter.Presenter
{
    class FormatterPresenter
    {
        private IModel _model;
        private IView _view;

        public FormatterPresenter(IView view, IModel model)
        {
            _view = view;
            _model = new FormatterModel(null);

            _view.MenuFile_CreateClick += CreateFile;
            _view.MenuFile_OpenClick += OpenFile;
        }

        public void CreateFile(object sender, EventArgs e) 
        {
            using (StreamWriter writer = new StreamWriter(@"Новый formatter.frmt")) 
            {
                
            }
        }

    }
}
