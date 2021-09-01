using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace Fluent_Note.Bindings
{
    class coleccionesFonts
    {
        public ObservableCollection<string> cargarFontFamily()
        {
            var s = Fonts.SystemFontFamilies;
            ObservableCollection<string> fontfamilys = new ObservableCollection<string>();
            foreach (var i in s)
            {
                fontfamilys.Add(i.ToString());
            }
            fontfamilys = ordenar(fontfamilys);
            return fontfamilys;
        }
        ObservableCollection<string> ordenar(ObservableCollection<string> vs)
        {
            var d = vs.OrderBy(o => o).ToList();
            vs.Clear();
            foreach (var a in d)
            {
                vs.Add(a);
            }
            return vs;
        }
        public ObservableCollection<string> cargarFontSize()
        {
            ObservableCollection<string> FontSizes = new ObservableCollection<string>();
            for (int x = 6; x < 13; x++)
            {
                FontSizes.Add(x + " px");
            }

            for (int x = 14; x < 26; x += 2)
            {
                FontSizes.Add(x + " px");
            }

            FontSizes.Add(36 + " px");
            FontSizes.Add(48 + " px");
            FontSizes.Add(72 + " px");
            return FontSizes;
        }

        public ObservableCollection<string> cargarFontStyles()
        {
            ObservableCollection<string> FontStyleses = new ObservableCollection<string>();
            FontStyleses.Add("Italic");
            FontStyleses.Add("Normal");
            FontStyleses.Add("Oblique");
            return FontStyleses;
        }
    }
}
