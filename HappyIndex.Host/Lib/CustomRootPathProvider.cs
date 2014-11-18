using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyIndex.Host.Lib
{
    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            try
            {
                return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\"));
            }
            catch
            {
                return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\"));
            }
        }
    }
}
