using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.Diagnostics;

namespace XmlMerge
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            ICommandLineParser parser = new CommandLineParser(new CommandLineParserSettings(Console.Error));
            if (!parser.ParseArguments(args, options))
            {
                Console.ReadKey();
                Environment.Exit(1);
            }

			//while (!Debugger.IsAttached);
            bool success = Merge(options);
			if (success)
				Console.WriteLine("Merge successfull");
			Environment.Exit(success ? 0 : 1);
        }

        private static bool Merge(Options options)
        {
            bool succes = false;
            XmlDocument doc1 = new XmlDocument();
            XmlDocument doc2 = new XmlDocument();
            try
            {
                doc1.Load(options.SourceFile);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error loading source file.\r\n" + ex.Message);
                return succes;
            }
            try
            {
                doc2.Load(options.TargetFile);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error loading target file.\r\n" + ex.Message);
                return succes;
            }            

            XmlNodeList sourceNodes = null;
            try
            {
                sourceNodes = doc1.SelectNodes(options.SourceXPath);
            }
            catch(XPathException xpex)
            {
                Console.WriteLine("Error selecting source nodes.\r\n" + xpex.Message);
                return succes;
            }

            if (sourceNodes== null || sourceNodes.Count == 0)
            {
                Console.WriteLine("Source query returned nothing.");
                return succes;
            }

            XmlNode targetNode = null;
            try
            {
                targetNode = doc2.SelectSingleNode(options.TargetXPath);
            }
            catch(XPathException xpex)
            {
                Console.WriteLine("Error selecting target node.\r\n" + xpex.Message);
                return succes;
            }

            if (targetNode == null)
            {
                Console.WriteLine("Cannot find target node.");
                return succes;
            }

			var targetBrothers = targetNode.ParentNode.ChildNodes.OfType<XmlNode>();

            foreach(XmlNode node in sourceNodes) 
            {
                try
                {
					if (options.SkipExistingNodes)
					{
						if (targetBrothers.Any(b => b.OuterXml.ToLower() == node.OuterXml.ToLower()))
							Console.WriteLine(string.Format("\tSkipping node '{0}'", node.OuterXml));
					}
                    XmlNode importedNode = doc2.ImportNode(node, true);
                    targetNode.ParentNode.AppendChild(importedNode);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error inserting nodes at target.\r\n" + ex.Message);
                    return succes;
                }
            }

            try
            {
                doc2.Save(options.OutputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving output file.\r\n" + ex.Message);
                return succes;
            }

            succes = true;
            return succes;
        }
    }
}
