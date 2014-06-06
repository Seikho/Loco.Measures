using System;
using System.IO;

namespace LocoDataExtractor
{
    public class LocoAnalyser
    {
        public String Filename;
        protected String Folder;

        public LocoAnalyser(string filename, int sampleFreq)
        {
            Filename = filename;
            Folder = Path.GetDirectoryName(Filename);

        }

        public void Extract()
        {

        }

        protected string NewFile(string extension, bool asCsv = false)
        {
            var path = Path.GetDirectoryName(Filename) + "\\";
            var noext = Path.GetFileNameWithoutExtension(Filename);
            var count = 1;
            while (File.Exists(path + noext + "_" + count + "_" + extension + (asCsv ? ".csv" : ".txt"))) count++;
            return path + noext + "_" + count + "_" + extension + (asCsv ? ".csv" : ".txt");
        }
    }
}
