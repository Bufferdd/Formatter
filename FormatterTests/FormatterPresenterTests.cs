using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

using Formatter.Presenter;

namespace FormatterTests
{
    [TestClass]
    public class FormatterPresenterTests
    {
        [TestMethod]
        public void Test_CreateFile()
        {
            string filename = "Новый formatter.frmt";
            FormatterPresenter formatterPresenter = new FormatterPresenter(null, null);

            if (File.Exists(filename)) 
                File.Delete(filename);

            Assert.AreEqual(false, File.Exists(filename));
            
            formatterPresenter.CreateFile(null, null);
            Assert.AreEqual(true, File.Exists(filename));

            File.Delete(filename);
            Assert.AreEqual(false, File.Exists(filename));
        }
    }
}
