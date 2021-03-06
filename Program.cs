﻿using System;
using ConsoleCommandHelper;

namespace FolderByPatternCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            try
            {
                if ((args.Length == 1))
                {
                    CommandService.TestMethod = FolderCreateProcessor.RunTests;
                    var argument = CommandService.Execute(args[0]);
                }
                else if (args.Length == 2)
                {
                    FolderCreateProcessor.CreateFolders(args[0], args[1]);
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

    }
}
