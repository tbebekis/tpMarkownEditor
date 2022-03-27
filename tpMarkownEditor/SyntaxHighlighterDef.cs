using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;
using System.IO;

using System.Text.RegularExpressions;

namespace MarkdownEditor
{

    /*

    <doc>
        <brackets left="&lt;" right="&gt;" />
        <style name="Maroon" color="Maroon" fontStyle="Bold,Italic" />
        <style name="Blue" color="Blue"/>
        <style name="Red" color="Red" backColor="#f5f5e5" />
        <rule style="Blue">&lt;|/&gt;|&lt;/|&gt;</rule>
        <rule style="Maroon">&lt;(?&lt;range&gt;[!\w\d]+)</rule>
        <rule style="Maroon">&lt;/(?&lt;range&gt;[\w\d]+)&gt;</rule>
        <rule style="Red" options="Multiline">(?&lt;range&gt;\S+?)='[^']*
            '|(?&lt;range&gt;\S+)="[^"]*"|(?&lt;range&gt;\S+)=\S+</rule>
        <folding start="&lt;div" finish="&lt;/div&gt;" options="IgnoreCase"/>
    </doc> 

     */
    public class SyntaxHighlighterDef
    {
 
        /* construction */
        public SyntaxHighlighterDef()
        {
        }


        /* static */
        static public SyntaxHighlighterDef CreateDefaultDef()
        {
           
            SyntaxHighlighterDef Result = new SyntaxHighlighterDef();

            // folding
            Result.Folding.Start = @"#";
            Result.Folding.Finish = @"#";
            Result.Folding.Options = RegexOptions.Multiline;
 
            // styles
            Result.Styles.Add(new Style("Heading", "SteelBlue", null, FontStyle.Regular));

            Result.Styles.Add(new Style("Bold", "Black", null, FontStyle.Bold));
            Result.Styles.Add(new Style("Italic", "Black", null, FontStyle.Regular));
            Result.Styles.Add(new Style("Monospace", "Black", "LightYellow", FontStyle.Regular));

            Result.Styles.Add(new Style("Link", "DarkMagenta", null, FontStyle.Bold));
            Result.Styles.Add(new Style("Image", "Orange", null, FontStyle.Bold));

            Result.Styles.Add(new Style("SourceCode", "Black", "LightYellow", FontStyle.Regular));
            Result.Styles.Add(new Style("BlockQuote", "Black", "LightGreen", FontStyle.Regular));
            Result.Styles.Add(new Style("SubOrSuperscript", "Black", "FloralWhite", FontStyle.Regular));

            // rules
            Result.Rules.Add(new Rule("Heading", App.SHeading1, RegexOptions.Multiline));
            Result.Rules.Add(new Rule("Heading", App.SHeading2, RegexOptions.Multiline));
            Result.Rules.Add(new Rule("Heading", App.SHeading3, RegexOptions.Multiline));

            Result.Rules.Add(new Rule("Bold", App.SBold, RegexOptions.Multiline));
            Result.Rules.Add(new Rule("Italic", App.SItalic, RegexOptions.Multiline));
            Result.Rules.Add(new Rule("Monospace", App.SMonospace, RegexOptions.Multiline));

            Result.Rules.Add(new Rule("Link", App.SLink, RegexOptions.Multiline));
            Result.Rules.Add(new Rule("Image", App.SImage, RegexOptions.Multiline));

            Result.Rules.Add(new Rule("SourceCode", App.SSourceCode, RegexOptions.Multiline));
            Result.Rules.Add(new Rule("BlockQuote", App.SBlockQuote, RegexOptions.Multiline));

            Result.Rules.Add(new Rule("SubOrSuperscript", App.SSubscript, RegexOptions.Multiline));
            Result.Rules.Add(new Rule("SubOrSuperscript", App.SSuperscript, RegexOptions.Multiline));

            return Result;
        }
 
        /* public */
        public XmlDocument CreateXmlDocument()
        {
            XmlDocument Doc = new XmlDocument();
            XmlElement Root = Doc.CreateElement("doc");
            Doc.AppendChild(Root);

            XmlDeclaration Declaration = Doc.CreateXmlDeclaration(encoding:"utf-8", version:"1.0", standalone: "yes");
            Doc.InsertBefore(Declaration, Root);

            // folding
            XmlElement FoldingEl = Root.AddElement("folding");
            FoldingEl.AddAttribute("start", Folding.Start);
            FoldingEl.AddAttribute("finish", Folding.Finish);
            FoldingEl.AddAttribute("options", Folding.Options.ToString());

            // styles
            XmlElement StyleEl;
            foreach (Style Style in Styles)
            {
                StyleEl = Root.AddElement("style");
                StyleEl.AddAttribute("name", Style.Name);
                if (!string.IsNullOrWhiteSpace(Style.Color))
                    StyleEl.AddAttribute("color", Style.Color);
                if (!string.IsNullOrWhiteSpace(Style.BackColor))
                    StyleEl.AddAttribute("backColor", Style.BackColor);
                StyleEl.AddAttribute("fontStyle", Style.FontStyle.ToString());
            }


            // rules
            XmlElement RuleEl;
            foreach (Rule Rule in Rules)
            {
                RuleEl = Root.AddElement("rule", Rule.Pattern);
                RuleEl.AddAttribute("style", Rule.Style);
                RuleEl.AddAttribute("options", Rule.Options.ToString());
            }

            return Doc;
        }
        public void Save()
        {
            XmlDocument Doc = CreateXmlDocument();
            //string XmlText = Doc.FormatXml();  // Doc.OuterXml;      
            Doc.Save(FilePath);
        }

        /* properties */
        public List<Style> Styles { get;  } = new List<Style>();
        public List<Rule> Rules { get; } = new List<Rule>();
        public Folding Folding { get; } = new Folding();
        static public string FilePath { get; } = Path.GetFullPath("htmlDesc.xml");
    }

    public class Style
    {
        public Style()
        {
        }
        public Style(string Name, string Color = "Black", string BackColor = null, FontStyle FontStyle = FontStyle.Regular)
        {
            this.Name = Name;
            this.Color = Color;
            this.BackColor = BackColor;
            this.FontStyle = FontStyle;
        }

        public string Name { get; set; }
        public string Color { get; set; }  
        public string BackColor { get; set; }  
        public FontStyle FontStyle { get; set; } = FontStyle.Regular;
    }

    public class Rule
    {
        public Rule()
        {
        }
        public Rule(string Style, string Pattern, RegexOptions Options)
        {
            this.Style = Style;
            this.Pattern = Pattern;
            this.Options = Options;
        }

        public string Style { get; set; }
        public string Pattern { get; set; }
        public RegexOptions Options { get; set; }        
    }

    public class Folding
    {
        public Folding()
        {
        }

        public string Start { get; set; } = "#";
        public string Finish { get; set; } = "#";
        public RegexOptions Options { get; set; } = RegexOptions.IgnoreCase;

    }
}
