using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace Fluent_Note.Modelos
{
    public class FontAndMoreModel
    {
        public ObservableCollection<FontFamily> fonts { get; set; }
        public ObservableCollection<string> pixeltam { get; set; }
        public ObservableCollection<string> fontstyle { get; set; }
    }
}
