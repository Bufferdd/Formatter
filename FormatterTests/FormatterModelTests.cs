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
    }
}
