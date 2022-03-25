using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
