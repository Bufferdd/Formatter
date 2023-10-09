using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Formatter.Model
{
    public interface IModel 
    {
        // Сохранение данных в файл определенного формата
        void SaveInFile(string filename);
        // Получение данных из файла определенного формата
        void LoadFromFile(string filename);
    }

    // Модель класса Formatter
    public class FormatterModel : IModel
    {
        private string _text;

        // Весь текст
        public string Text { get { return _text; } set { _text = value; } }
        // Конструктор
        public FormatterModel(string text)
        {
            Text = text;
        }

        void IModel.SaveInFile(string filename) 
        {
            using (StreamWriter writer = new StreamWriter(filename)) 
            {
                writer.Write(_text);
            }
        }
        void IModel.LoadFromFile(string filename) 
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                _text = reader.ReadToEnd();
            }
        }
    }
}
