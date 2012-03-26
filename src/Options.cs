using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace XmlMerge
{
    public sealed class Options : CommandLineOptionsBase
    {
        #region Standard Option Attribute
        
        [Option("s", "source",
                Required = true,
                HelpText = "Input XML file.")]
        public string SourceFile = String.Empty;

        [Option("m", "sourceXpath",
               Required = true,
               HelpText = "XPath used to select source nodes.")]
        public string SourceXPath = String.Empty;

        [Option("t", "target",
                Required = true,
                HelpText = "Target XML file.")]
        public string TargetFile = String.Empty;

        [Option("n", "targetXpath",
               Required = true,
               HelpText = "XPath used to specify location to insert nodes.")]
        public string TargetXPath = String.Empty;

        [Option("o", "output",
               Required = true,
               HelpText = "Output XML file.")]
        public string OutputFile = String.Empty;

		[Option("k", "sKip existing nodes",
			   Required = false,
			   HelpText = "Skip nodes that already exist in target document.")]
		public bool SkipExistingNodes = true;

        #endregion

        #region Specialized Option Attribute

        [HelpOption(HelpText = "Dispaly this help screen.")]
        public string GetUsage()
        {
            var help = new HelpText(Constants.AppName);
            help.AdditionalNewLineAfterOption = true;
            help.Copyright = new CopyrightInfo("Chitza", 2012, 2012);
            this.HandleParsingErrorsInHelp(help);

			help.AddPreOptionsLine(@"Usage: " + Constants.AppName);
			help.AddPreOptionsLine(@"        -s Source.xml -m /xpath/to/nodes/to/select");
			help.AddPreOptionsLine(@"        -t Target.xml -n /xpath/to/insert/location");
			help.AddPreOptionsLine(@"        -o Output.xml");
			help.AddOptions(this);

            return help;
        }

        private void HandleParsingErrorsInHelp(HelpText help)
        {
            string errors = help.RenderParsingErrorsText(this);
            if (!string.IsNullOrEmpty(errors))
            {
                help.AddPreOptionsLine(string.Concat(Environment.NewLine, "ERROR: ", errors, Environment.NewLine));
            }
        }
        #endregion
    }
}
