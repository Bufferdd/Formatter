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
    }
}
