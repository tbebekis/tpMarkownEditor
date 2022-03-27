using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownEditor
{
    static public partial class App
    {
        /// <summary>
        /// # ## ###
        /// </summary>
        public const string SHeading1 = @"(#+)(.*)";
        /// <summary>
        /// =======
        /// </summary>
        public const string SHeading2 = @"^=+";
        /// <summary>
        /// -----------
        /// </summary>
        public const string SHeading3 = @"^-+";
        /// <summary>
        /// **bold**
        /// </summary>
        public const string SBold = @"[\*\*].*[\*\*]";
        /// <summary>
        /// _italic_
        /// </summary>
        public const string SItalic = @"[_][^_].*[^_][_]";
        /// <summary>
        /// `monospace`
        /// </summary>
        public const string SMonospace = @"[`][^`].*[^`][`]";
        /// <summary>
        /// []()
        /// </summary>
        public const string SLink = @"^\[(.*?)\]\((.*?)\)";
        /// <summary>
        /// ![]()
        /// </summary>
        public const string SImage = @"^!\[(.*?)\]\((.*?)\)";
        /// <summary>
        /// ``` code here ```
        /// </summary>
        public const string SSourceCode = @"^```(.|\n|\r)*```";
        /// <summary>
        /// &gt; 
        /// <para>&gt;</para>
        /// </summary>
        public const string SBlockQuote = @"^\>(.*)";
        /// <summary>
        /// ~subscript~
        /// </summary>
        public const string SSubscript = @"~(.*?)~";
        /// <summary>
        /// ^superscript^
        /// </summary>
        public const string SSuperscript = @"\^(.*?)\^";
    }
}
