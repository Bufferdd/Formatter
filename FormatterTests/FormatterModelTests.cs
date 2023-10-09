using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

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
            string filename = "Тест";
            string format = ".frmt";

            FormatterModel model = new FormatterModel(text);
            IModel iModel = model;

            iModel.SaveInFile(filename, format);

            Assert.AreEqual(true, File.Exists(filename + format));

            using (StreamReader reader = new StreamReader(filename + format)) 
            {
                Assert.AreEqual(text, reader.ReadToEnd());
            }

            File.Delete(filename + format);
        }

        [TestMethod]
        public void Test_LoadFromFile() 
        {
            string text = "Тестовый текст для проверки модели";
            string filename = "Тест";
            string format = ".frmt";

            FormatterModel model = new FormatterModel(null);
            Assert.IsNull(model.Text);
            IModel iModel = model;

            if (File.Exists(filename + format))
                File.Delete(filename + format);
            
            using (StreamWriter writer = new StreamWriter(filename + format)) 
            {
                writer.Write(text);
            }

            iModel.LoadFromFile(filename, format);

            Assert.AreEqual(text, model.Text);

            File.Delete(filename + format);
        }
    }
}
