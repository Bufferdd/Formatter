﻿using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

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
                _view.MenuFile_OpenClick += OpenFile;
                _view.MenuFile_ClearClick += Clear;
            }
        }

        public void CreateFile(object sender, EventArgs e) 
        {
            using (StreamWriter writer = new StreamWriter(@"Новый formatter.frmt")) 
            {
                
            }
        }
        public void OpenFile(object sender, EventArgs e) 
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
                _model.LoadFromFile(fileDialog.FileName);
        }
        public void Clear(object sender, EventArgs e) 
        {
            _view.SetText("");
        }
    }
}
