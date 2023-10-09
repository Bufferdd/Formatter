using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Formatter.Model;
using Formatter.View;
using Formatter.Presenter;

namespace Formatter
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormatterPresenter presenter = new FormatterPresenter(new FormatterView(), new FormatterModel(null));
            IPresenter iPresenter = presenter;
            iPresenter.Run();
        }
    }
}
