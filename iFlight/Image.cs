using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using iFlight;
using iFlight.Models;

namespace iFlight
{
    public class Image
    {
        IFlightContext DB = new IFlightContext();
        public string CreateNewNameOrMakeItUniqe(string name, string id)
        {
            try
            {

                return name + id;

            }
            catch (Exception)
            {
                return null;
            }


        }
        public string ImageFileExist(string Name, string rootPath)
        {

            string[] names = Directory.GetFiles(rootPath);

            foreach (var fileName in names)
            {
                if (Path.GetFileNameWithoutExtension(fileName).IndexOf(Path.GetFileNameWithoutExtension(Name)) != -1)
                {

                    return fileName;
                }
            }

            return null;
        }

    }
}
