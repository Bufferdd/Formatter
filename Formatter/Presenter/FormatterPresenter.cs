using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
