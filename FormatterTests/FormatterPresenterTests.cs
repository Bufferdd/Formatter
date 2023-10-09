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
            FormatterPresenter formatterPresenter = new FormatterPresenter(null, null);

            if (File.Exists("Новый formatter.frmt")) 
                File.Delete("Новый formatter.frmt");

            Assert.AreEqual(false, File.Exists("Новый formatter.frmt"));
            
            formatterPresenter.CreateFile(null, null);
            Assert.AreEqual(true, File.Exists("Новый formatter.frmt"));

            File.Delete("Новый formatter.frmt");
            Assert.AreEqual(false, File.Exists("Новый formatter.frmt"));
        }
    }
}
