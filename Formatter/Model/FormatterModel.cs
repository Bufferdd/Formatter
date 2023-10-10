using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Formatter.Model
{
    public interface IModel 
    {
        // Получить имя файла
        string GetFilename();
        // Поменять имя файла
        void SetFilename(string filename);
        // Получить текст
        string GetText();
        // Поменять текст
        void SetText(string text);
        // Добавить текст
        void AddText(string text);
        Dictionary<int, Color> GetColors();
        Dictionary<int, Font> GetFonts();
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
        private Dictionary<int, Color> _colors;
        private Dictionary<int, Font> _fonts;

        // Весь текст
        public string Text { get { return _text; } set { _text = value; } }
        // Файл, в котором хранится текст
        public string Filename { get { return _filename; } set { _filename = value; } }
        // Ключ - индекс текста, с которого начинается выбранный цвет, значение - этот самый цвет
        public Dictionary<int, Color> Colors { get { return _colors; } set { _colors = value; } }
        // Ключ - индекс текста, с которого начинается выбранный шрифт, значение - этот самый шрифт
        public Dictionary<int, Font> Fonts { get { return _fonts; } set { _fonts = value; } }
        // Конструктор
        public FormatterModel(string text, string filename)
        {
            Text = text;
            Filename = filename;
            Colors = new Dictionary<int, Color>();
            Fonts = new Dictionary<int, Font>();
        }
        string IModel.GetFilename() => Filename;
        string IModel.GetText() => Text;
        void IModel.SetText(string text) => Text = text;
        void IModel.AddText(string text) => Text += text;
        Dictionary<int, Color> IModel.GetColors() => Colors;
        Dictionary<int, Font> IModel.GetFonts() => Fonts;
        void IModel.SetFilename(string filename) => Filename = filename;
        void IModel.SaveInFile(string filename) 
        {
            if (_filename == null)
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    for (int i = 0; i < Text.Length; ++i)
                    {
                        if (Colors.ContainsKey(i))
                        {
                            writer.Write($"[color: {Colors[i].R};{Colors[i].G};{Colors[i].B}]");
                        }
                        if (Fonts.ContainsKey(i))
                        {
                            Font font = Fonts[i];
                            writer.Write($"[font: {font.Size};{font.Name};{font.Style}]");
                        }
                        writer.Write(Text[i]);
                    }
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(_filename))
                {
                    for (int i = 0; i < Text.Length; ++i)
                    {
                        if (Colors.ContainsKey(i))
                        {
                            writer.Write($"[color: {Colors[i].R};{Colors[i].G};{Colors[i].B}]");
                        }
                        if (Fonts.ContainsKey(i))
                        {
                            Font font = Fonts[i];
                            writer.Write($"[font: {font.Size};{font.Name};{font.Style}]");
                        }
                        writer.Write(Text[i]);
                    }
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

                string patternAll = @"\[font:\s(\d+\W?\d+);([\w*\s*]*);(\w+)\]|\[color:\s*(\d+);(\d+);(\d+)\]";

                // Создание экземпляра класса Regex
                Regex regexAll = new Regex(patternAll);

                int lengthPatterns = 0;
                // Замена всех вхождений шаблона на пустую строку и сохранение значений в список
                _text = regexAll.Replace(_text, match =>
                {
                    if (match.Groups[0].Value.Substring(1, 4) == "font")
                    {
                        string[] font = match.Groups[0].Value.Split(';', ']', '[');

                        string fontSize = font[1].Substring(6, font[1].Length - 6);
                        string fontFamily = font[2];
                        string fontStyleStr = font[3];

                        float size;
                        float.TryParse(fontSize, out size);
                        FontStyle fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), fontStyleStr);

                        int key = match.Index - lengthPatterns;

                        if (Fonts.ContainsKey(key))
                            Fonts[key] = new Font(fontFamily, size, fontStyle);
                        else
                            Fonts.Add(key, new Font(fontFamily, size, fontStyle));
                    }
                    else if (match.Groups[0].Value.Substring(1, 5) == "color")
                    {
                        string[] color = match.Groups[0].Value.Split(';', ']', '[');

                        string red = color[1].Substring(7, color[1].Length - 7);
                        string green = color[2];
                        string blue = color[3];

                        int key = match.Index - lengthPatterns;

                        if (Colors.ContainsKey(key))
                            Colors[key] = Color.FromArgb(Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue));
                        else
                            Colors.Add(key, Color.FromArgb(Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue)));
                    }
                    lengthPatterns += match.Length;
                    return "";
                });
            }
            else
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    _text = reader.ReadToEnd();
                }

                string patternAll = @"\[font:\s(\d+\W?\d+);([\w*\s*]*);(\w+)\]|\[color:\s*(\d+);(\d+);(\d+)\]";

                // Создание экземпляра класса Regex
                Regex regexAll = new Regex(patternAll);

                int lengthPatterns = 0;
                // Замена всех вхождений шаблона на пустую строку и сохранение значений в список
                _text = regexAll.Replace(_text, match =>
                {
                    if (match.Groups[0].Value.Substring(1, 4) == "font")
                    {
                        string[] font = match.Groups[0].Value.Split(';', ']', '[');

                        string fontSize = font[1].Substring(6, font[1].Length - 6);
                        string fontFamily = font[2];
                        string fontStyleStr = font[3];

                        float size;
                        float.TryParse(fontSize, out size);
                        FontStyle fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), fontStyleStr);

                        int key = match.Index - lengthPatterns;

                        if (Fonts.ContainsKey(key))
                            Fonts[key] = new Font(fontFamily, size, fontStyle);
                        else
                            Fonts.Add(key, new Font(fontFamily, size, fontStyle));
                    }
                    else if (match.Groups[0].Value.Substring(1, 5) == "color")
                    {
                        string[] color = match.Groups[0].Value.Split(';', ']', '[');

                        string red = color[1].Substring(7, color[1].Length - 7);
                        string green = color[2];
                        string blue = color[3];

                        int key = match.Index - lengthPatterns;

                        if (Colors.ContainsKey(key))
                            Colors[key] = Color.FromArgb(Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue));
                        else
                            Colors.Add(key, Color.FromArgb(Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue)));
                    }

                    lengthPatterns += match.Length;
                    return "";
                });
            }
        }
    }
}
