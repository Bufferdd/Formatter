using System;
using System.IO;
using System.Text;

using Formatter.Model;
using Formatter.View;

namespace Formatter.Presenter
{
    public class FormatterPresenter
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
            }
        }

        public void CreateFile(object sender, EventArgs e) 
        {
            using (StreamWriter writer = new StreamWriter(@"Новый formatter.frmt")) 
            {
                
            }
        }
    }
}
