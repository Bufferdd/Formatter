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
        private string _filename;

        // Весь текст
        public string Text { get { return _text; } set { _text = value; } }
        // Файл, в котором хранится текст
        public string Filename { get { return _filename; } set { _filename = value; } }
        // Конструктор
        public FormatterModel(string text, string filename)
        {
            Text = text;
            Filename = filename;
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
