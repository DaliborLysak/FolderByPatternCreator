using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FolderByPatternCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            try
            {
                if (args.Length == 2)
                {
                    var path = args[0];
                    var folderCreatePattern = args[1];

                    Console.WriteLine($"In Folder {path} creating sub folders with pattern {folderCreatePattern}.");

                    if (!String.IsNullOrEmpty(path) && Directory.Exists(path) && !String.IsNullOrEmpty(folderCreatePattern))
                    {
                        var processor = new PatternProcessor();
                        var folders = processor?.Get(folderCreatePattern) ?? new List<string>();
                        folders.ForEach(f =>
                        {
                            Task task = Task.Factory.StartNew(() => { CreateFolder(path, f); });
                            task.Wait();
                        });
                    }
                }
                else
                {
                    throw new CreatorInputDataException("Wrong number of parameters, missing input folder and pattern for creation");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Console.WriteLine("Finished.");
            }
        }

        private static void CreateFolder(string path, string folderName)
        {
            var dirName = $"{path}{folderName}";
            Directory.CreateDirectory(dirName);
            Console.WriteLine($"Folder created: {dirName}");
        }
    }
}
