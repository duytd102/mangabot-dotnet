using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Converter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var binPath = Path.GetFullPath(Application.StartupPath + "\\bin\\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");

            if (File.Exists(binPath))
                return Assembly.LoadFrom(binPath);

            return null;
        }
    }
}
