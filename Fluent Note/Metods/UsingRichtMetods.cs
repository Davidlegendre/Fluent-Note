using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Fluent_Note.Metods
{
    class UsingRichtMetods
    {
        public void LoadTextDocument(string fileName, RichTextBox rich)
        {
            TextRange range;
            System.IO.FileStream fStream;
            if (System.IO.File.Exists(fileName))
            {
                range = new TextRange(rich.Document.ContentStart, rich.Document.ContentEnd);
                fStream = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate);
                range.Load(fStream, System.Windows.DataFormats.Text);
                fStream.Close();
            }
        }
        public string ConvertRichTextBoxContentsToString(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }
    }
}
