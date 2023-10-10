using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Drawing;

using Formatter.Model;

namespace FormatterTests
{
    [TestClass]
    public class FormatterModelTests
    {
        [TestMethod]
        public void Test_SaveInFile()
        {
            string text = "Тестовый текст для проверки модели";
            string filename = "Тест.frmt";

            FormatterModel model = new FormatterModel(text, filename);
            IModel iModel = model;

            iModel.SaveInFile(filename);

            Assert.AreEqual(true, File.Exists(filename));

            using (StreamReader reader = new StreamReader(filename)) 
            {
                Assert.AreEqual(text, reader.ReadToEnd());
            }

            File.Delete(filename);
        }
        [TestMethod]
        public void Test_SaveInFileColors() 
        {
            string text = "Тестовый текст для проверки модели";
            string textWithColor = "Тес[color: 255;0;0]товый т[color: 0;128;0]ек[color: 0;0;255]ст для проверки модели";
            string filename = "Тест.frmt";

            FormatterModel model = new FormatterModel(text, filename);
            IModel iModel = model;

            model.Colors.Add(3, Color.Red);
            model.Colors.Add(10, Color.Green);
            model.Colors.Add(12, Color.Blue);

            iModel.SaveInFile(filename);

            Assert.AreEqual(true, File.Exists(filename));

            using (StreamReader reader = new StreamReader(filename))
            {
                string textNew = reader.ReadToEnd();
                Assert.AreEqual(textWithColor, textNew);
            }

            File.Delete(filename);
        }
        [TestMethod]
        public void Test_SaveInFileFonts()
        {
            string text = "Тестовый текст для проверки модели";
            string textWithFonts = "Тес[font: 10;Times New Roman;Italic]товый т[font: 20;Courier New;Regular]ек[font: 15,5;Microsoft Sans Serif;Underline]ст для проверки модели";
            string filename = "Тест.frmt";

            FormatterModel model = new FormatterModel(text, filename);
            IModel iModel = model;

            Font font1 = new Font(FontFamily.GenericSerif, 10, FontStyle.Italic);
            Font font2 = new Font(FontFamily.GenericMonospace, 20, FontStyle.Regular);
            Font font3 = new Font(FontFamily.GenericSansSerif, 15.5f, FontStyle.Underline);
            model.Fonts.Add(3, font1);
            model.Fonts.Add(10, font2);
            model.Fonts.Add(12, font3);

            iModel.SaveInFile(filename);

            Assert.AreEqual(true, File.Exists(filename));

            using (StreamReader reader = new StreamReader(filename))
            {
                string textNew = reader.ReadToEnd();
                Assert.AreEqual(textWithFonts, textNew);
            }

            File.Delete(filename);
        }
        public void Test_SaveInFileFontsColors() 
        {
            string filename = "Тест.frmt";
            string text = "Тестовый текст для проверки модели";
            string textWithColorsAndFonts = "Тес[color: 255;0;0][font: 10;Times New Roman;Italic]товый т[color: 0;128;0][font: 20;Courier New;Regular]" +
                        "ек[color: 0;0;255][font: 15,5;Microsoft Sans Serif;Underline]ст для проверки модели";

            FormatterModel model = new FormatterModel(text, filename);
            IModel iModel = model;

            Font font1 = new Font(FontFamily.GenericSerif, 10, FontStyle.Italic);
            Font font2 = new Font(FontFamily.GenericMonospace, 20, FontStyle.Regular);
            Font font3 = new Font(FontFamily.GenericSansSerif, 15.5f, FontStyle.Underline);
            model.Fonts.Add(3, font1);
            model.Fonts.Add(10, font2);
            model.Fonts.Add(12, font3);
            model.Colors.Add(3, Color.Red);
            model.Colors.Add(10, Color.Green);
            model.Colors.Add(12, Color.Blue);

            iModel.SaveInFile(filename);

            Assert.AreEqual(true, File.Exists(filename));

            using (StreamReader reader = new StreamReader(filename))
            {
                string textNew = reader.ReadToEnd();
                Assert.AreEqual(textWithColorsAndFonts, textNew);
            }

            File.Delete(filename);
        }
        [TestMethod]
        public void Test_LoadFromFile() 
        {
            string text = "Тестовый текст для проверки модели";
            string filename = "Тест.frmt";

            FormatterModel model = new FormatterModel(null, null);
            Assert.IsNull(model.Text);
            IModel iModel = model;

            if (File.Exists(filename))
                File.Delete(filename);
            
            using (StreamWriter writer = new StreamWriter(filename)) 
            {
                writer.Write(text);
            }

            iModel.LoadFromFile(filename);

            Assert.AreEqual(text, model.Text);

            File.Delete(filename);
        }
        [TestMethod]
        public void Test_LoadFromFileColors()
        {
            string textFile = "Тес[color: 255;0;0]товый т[color: 0;128;0]ек[color: 0;0;255]ст для проверки модели";
            string text = "Тестовый текст для проверки модели";
            string filename = "Тест.frmt";

            FormatterModel model = new FormatterModel(null, null);
            Assert.IsNull(model.Text);
            IModel iModel = model;

            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(textFile);
            }

            iModel.LoadFromFile(filename);

            Assert.AreEqual(text, model.Text);
            Assert.IsTrue(model.Colors.ContainsKey(3));
            Assert.IsTrue(model.Colors.ContainsKey(10));
            Assert.IsTrue(model.Colors.ContainsKey(12));
            Assert.AreEqual(Color.FromArgb(255, 0, 0), model.Colors[3]);
            Assert.AreEqual(Color.FromArgb(0, 128, 0), model.Colors[10]);
            Assert.AreEqual(Color.FromArgb(0, 0, 255), model.Colors[12]);

            File.Delete(filename);
        }
        [TestMethod]
        public void Test_LoadFromFileFonts()
        {
            string textFile = "Тес[font: 10;Times New Roman;Italic]товый т[font: 20;Courier New;Regular]ек[font: 15,5;Microsoft Sans Serif;Underline]ст для проверки модели";
            string text = "Тестовый текст для проверки модели";
            string filename = "Тест.frmt";

            FormatterModel model = new FormatterModel(null, null);
            Assert.IsNull(model.Text);
            IModel iModel = model;

            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(textFile);
            }

            iModel.LoadFromFile(filename);

            Assert.AreEqual(text, model.Text);
            Assert.IsTrue(model.Fonts.ContainsKey(3));
            Assert.IsTrue(model.Fonts.ContainsKey(10));
            Assert.IsTrue(model.Fonts.ContainsKey(12));
            Assert.AreEqual(model.Fonts[3], new Font("Times New Roman", 10, FontStyle.Italic));
            Assert.AreEqual(model.Fonts[10], new Font("Courier New", 20, FontStyle.Regular));
            Assert.AreEqual(model.Fonts[12], new Font("Microsoft Sans Serif", 15.5f, FontStyle.Underline));

            File.Delete(filename);
        }
        [TestMethod]
        public void Test_LoadFromFileFontsColors()
        {
            string textWithColorsAndFonts = "Тес[color: 255;0;0][font: 10;Times New Roman;Italic]товый т[font: 20;Courier New;Regular]" +
                        "ек[color: 0;0;255]ст для проверки модели";
            string text = "Тестовый текст для проверки модели";
            string filename = "Тест.frmt";

            FormatterModel model = new FormatterModel(null, null);
            Assert.IsNull(model.Text);
            IModel iModel = model;

            if (File.Exists(filename))
                File.Delete(filename);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(textWithColorsAndFonts);
            }

            iModel.LoadFromFile(filename);

            Assert.AreEqual(text, model.Text);

            Assert.IsTrue(model.Colors.ContainsKey(3));
            Assert.IsTrue(model.Colors.ContainsKey(12));
            Assert.AreEqual(Color.FromArgb(255, 0, 0), model.Colors[3]);
            Assert.AreEqual(Color.FromArgb(0, 0, 255), model.Colors[12]);

            Assert.IsTrue(model.Fonts.ContainsKey(3));
            Assert.IsTrue(model.Fonts.ContainsKey(10));
            Assert.AreEqual(model.Fonts[3], new Font("Times New Roman", 10, FontStyle.Italic));
            Assert.AreEqual(model.Fonts[10], new Font("Courier New", 20, FontStyle.Regular));

            File.Delete(filename);
        }
    }
}
