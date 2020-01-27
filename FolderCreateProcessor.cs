using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FolderByPatternCreator
{
    public static class FolderCreateProcessor
    {
        public static void RunTests()
        {
            Console.WriteLine("Test mode, running tests...");

            new List<string> () {"", "_row_[0..5]", "_set_[b,cd,efg]", "/*/", "_seq0_[a..d]", "_seq1_[B..z]", "_seq2_[b..Z]", "_seq3_[Z..a]"}
                .ForEach(t => 
                {
                    Task task = Task.Factory.StartNew(() => { CreateFolders(@"c:\temp\desarrollo - tests\FolderByPatternCreator", t); });
                    task.Wait();
                });
        }

        public static void CreateFolders(string path, string folderCreatePattern)
        {
            Console.WriteLine($"In Folder {path} creating sub folders with pattern {folderCreatePattern}.");

            if (!String.IsNullOrEmpty(path) && !String.IsNullOrEmpty(folderCreatePattern))
            {
                var processor = new PatternProcessor();
                var folders = processor?.Get(folderCreatePattern) ?? new List<string>();
                folders.ForEach(f =>
                {
                    Task task = Task.Factory.StartNew(() => { CreateFolder(path, f); });
                    Console.WriteLine($"In Folder {path} creating sub folders with pattern {folderCreatePattern} was Sucessful.");
                    task.Wait();
                });
            }
            else
            {
                Console.WriteLine($"In Folder {path} creating sub folders with pattern {folderCreatePattern} failed.");
            }
        }

        public static void CreateFolder(string path, string folderName)
        {
            var dirName = $"{path}{folderName}";

            try
            {
                Directory.CreateDirectory(dirName);
            }
            catch (Exception e)
            {
                 Console.WriteLine($"Creation of folder {dirName} faild with exception: {e.Message}.");
            }

            Console.WriteLine($"Folder created: {dirName}");
        }
    }
}