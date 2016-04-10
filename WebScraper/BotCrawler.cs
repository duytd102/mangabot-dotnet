using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;

namespace WebScraper
{
    class BotCrawler<T>
    {
        private string url;
        private List<string> referencedAssemblies = new List<string>();

        public BotCrawler(string url)
        {
            this.url = url;
        }

        public BotCrawler(string url, List<string> assemblyNames)
        {
            this.url = url;
            this.referencedAssemblies = assemblyNames;
        }

        /// <summary>
        /// Execute a method and return value as specific type.
        /// </summary>
        /// <example>
        /// An example of bot script
        /// <code>
        /// namespace WebScraper.Scrapers.Implement { 
        /// 
        ///     public class TruyenTranh8Scraper {
        ///     
        ///         public int GetTotalPages() {
        ///             return 1;
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <param name="fullClassName">WebScraper.Scrapers.Implement.TruyenTranh8Scraper</param>
        /// <param name="methodName">GetTotalPages</param>
        /// <returns></returns>
        public T Invoke(string fullClassName, string methodName, object[] parameters = null)
        {
            string script = ScriptManager.GetInstance().GetScript(url);

            CompilerParameters compilerParams = new CompilerParameters(new[] {
                "Common.dll",
                "WebScraper.dll",
                "HtmlAgilityPack.dll",
                "System.dll",
                "System.Core.dll",
                "System.Data.dll",
                "System.Xml.dll",
                "System.Xml.Linq.dll",
            });
            compilerParams.GenerateInMemory = true;
            compilerParams.GenerateExecutable = false;
            compilerParams.TreatWarningsAsErrors = false;

            foreach (string assemblyName in referencedAssemblies)
            {
                compilerParams.ReferencedAssemblies.Add(assemblyName);
            }

            CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, script);
            if (results.Errors.HasErrors)
            {
                string text = "Compile error: ";
                foreach (CompilerError ce in results.Errors)
                {
                    text += "\r\n" + ce.ToString();
                }
                throw new Exception(text);
            }

            object o = results.CompiledAssembly.CreateInstance(fullClassName);
            MethodInfo mi = o.GetType().GetMethod(methodName);
            object returnValue = mi.Invoke(o, parameters);
            return (T)Convert.ChangeType(returnValue, typeof(T));
        }
    }
}
