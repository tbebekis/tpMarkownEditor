using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

using Markdig;

namespace MarkdownEditor
{
    static public partial class App
    {
        // MarkdownExtensions
        // https://github.com/xoofx/markdig/blob/master/src/Markdig/MarkdownExtensions.cs
        // MarkdownDocument

        static readonly MarkdownPipeline DefaultPipeline;
        static MarkdownPipeline Pipeline;

        static App()
        {
            if (!File.Exists(SyntaxHighlighterDef.FilePath))
            {
                var Syntax = SyntaxHighlighterDef.CreateDefaultDef();
                Syntax.Save();
            }

            DefaultPipeline = new MarkdownPipelineBuilder()
                    .UseAdvancedExtensions()
                    .Build();

            Pipeline = new MarkdownPipelineBuilder()
                    .UseAdvancedExtensions()
                    .Use<ImageExtension>()
                    .Build();
        }


        static public string ToHtml(string Text)
        {
            return !string.IsNullOrWhiteSpace(Text) ? Markdown.ToHtml(Text, Pipeline) : string.Empty;
        }
        static public string ToHtmlDefault(string Text)
        {
            return !string.IsNullOrWhiteSpace(Text) ? Markdown.ToHtml(Text, DefaultPipeline) : string.Empty;
        }


        static public bool QuestionBox(string Text)
        {
            return MessageBox.Show( Text, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        static public void InfoBox(string Text)
        {
            MessageBox.Show(Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /* xml */
        static public XmlDocument CreateDoc(string RootName = "root", string Encoding = "utf-8", string Version = "1.0", string StandAlone = "yes")
        {
            XmlDocument Doc = new XmlDocument();

            XmlElement Root = Doc.CreateElement(RootName);
            Doc.AppendChild(Root);

            XmlDeclaration Declaration = Doc.CreateXmlDeclaration(Version, Encoding, StandAlone);
            Doc.InsertBefore(Declaration, Root);

            return Doc;
        }
        static public XmlElement AddElement(this XmlNode Node, string ElementName, string Value = null)
        {
            XmlElement Result = Node.OwnerDocument.CreateElement(ElementName);
            Node.AppendChild(Result);
            if (!string.IsNullOrWhiteSpace(Value))
                Result.InnerText = Value;
            return Result;
        }
        static public XmlAttribute AddAttribute(this XmlNode Node, string AttributeName, string Value)
        {
            XmlAttribute Result = Node.OwnerDocument.CreateAttribute(AttributeName);
            Node.Attributes.Append(Result);
            Result.Value = Value;
            return Result;
        }
        /// <summary>
        /// Returns formatted xml string (indent and newlines) from unformatted Xml
        /// string for display in eg textboxes.
        /// <para>SOURCE: http://www.codeproject.com/KB/cpp/FormattingXML.aspx</para>
        /// </summary>    
        static public string FormatXml(string Text)
        {
            //load unformatted xml into a dom
            XmlDocument Doc = new XmlDocument();
            Doc.LoadXml(Text);

            return FormatXml(Doc);
        }
        /// <summary>
        /// Returns formatted xml string (indent and newlines) from unformatted Xml
        /// string for display in eg textboxes.
        /// <para>SOURCE: http://www.codeproject.com/KB/cpp/FormattingXML.aspx</para>
        /// </summary>    
        static public string FormatXml(this XmlDocument Doc)
        {
            //will hold formatted xml
            StringBuilder SB = new StringBuilder();

            //pumps the formatted xml into the StringBuilder above
            StringWriter SW = new StringWriter(SB);

            //does the formatting
            XmlTextWriter XTW = null;

            try
            {
                //point the XTW at the StringWriter
                XTW = new XmlTextWriter(SW);

                //we want the output formatted
                XTW.Formatting = Formatting.Indented;

                //get the dom to dump its contents into the XTW 
                Doc.WriteTo(XTW);
            }
            finally
            {
                //clean up even if error
                if (XTW != null)
                    XTW.Close();
            }

            //return the formatted xml
            return SB.ToString();
        }
    }
}
