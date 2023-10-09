using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Formatter.Model
{
    public interface IModel 
    {
        // Получить имя файла
        string GetFilename();
        // Поменять имя файла
        void SetFilename(string filename);
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
        string IModel.GetFilename() => Filename;
        void IModel.SetFilename(string filename) => Filename = filename;
        void IModel.SaveInFile(string filename) 
        {
            if (_filename == null)
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.Write(_text);
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(_filename))
                {
                    writer.Write(_text);
                }
            }
        }
        void IModel.LoadFromFile(string filename)
        {
            if (_filename == null)
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    _text = reader.ReadToEnd();
                }
            }
            else
            {
                using (StreamReader reader = new StreamReader(_filename))
                {
                    _text = reader.ReadToEnd();
                }
            }
        }
    }
}
