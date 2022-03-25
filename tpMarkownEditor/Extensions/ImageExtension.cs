using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

using Markdig;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace MarkdownEditor
{


    public class ImageExtension : IMarkdownExtension
    {
        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            pipeline.DocumentProcessed += Pipeline_DocumentProcessed;
        }


        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        { 
        }


        void ProcessImageNode(LinkInline Img)
        {
            string Url = Img.Url;
            if (!string.IsNullOrWhiteSpace(Url) && !Url.StartsWith("http", StringComparison.OrdinalIgnoreCase) && !Url.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
            {
                string FilePath = Url.Replace('/', '\\');

                FilePath = Path.GetFullPath(FilePath);

                if (File.Exists(FilePath))
                {
                    string Ext = Path.GetExtension(FilePath).Replace(".", "").ToLower();
                    if (!string.IsNullOrWhiteSpace(Ext))
                    {
                        byte[] Buffer = File.ReadAllBytes(FilePath);

                        string ImgText = Convert.ToBase64String(Buffer);
                        string DataUri = $"data:image/{Ext};base64,{ImgText}";
                        Img.Url = DataUri;
                    }
                }
            }
        }

        void Pipeline_DocumentProcessed(MarkdownDocument document)
        {            
            IEnumerable<MarkdownObject> Nodes = document.Descendants();
            foreach (var node in Nodes.OfType<LinkInline>()
                                      .Where(l => l.IsImage))
            {
                ProcessImageNode(node);
            }
        }

    }
}
