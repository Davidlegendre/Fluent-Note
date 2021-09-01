using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Fluent_Note.Bindings;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using System.Drawing;
using Media = System.Windows.Media;
using Shapes = System.Windows.Shapes;

namespace Fluent_Note.Theme
{
    public class ThemeDL
    {
        Media.Brush BrD = new SolidColorBrush(Media.Color.FromRgb(25, 25, 25));
        Media.Brush BrDTop = new SolidColorBrush(Media.Color.FromRgb(44, 44, 44));
        Media.Brush BrL = new SolidColorBrush(Media.Color.FromRgb(236,236,236));
        Shapes.Rectangle PrincipalBackgroundSolid; Border PrincipalBackgroundFluent; TextBlock PrincipalForeground;
        Tile PrincipalStyleFluent, PrincipalStyleFluent2; MaterialDesignThemes.Wpf.PaletteHelper p = new MaterialDesignThemes.Wpf.PaletteHelper();
        public ThemeDL(Shapes.Rectangle PrincipalBackgroundSolid, Border PrincipalBackgroundFluent, TextBlock PrincipalForeground, Tile PrincipalStyleFluent, Tile PrincipalStyleFluent2)
        {
            this.PrincipalBackgroundSolid = PrincipalBackgroundSolid;
            this.PrincipalBackgroundFluent = PrincipalBackgroundFluent;
            this.PrincipalForeground = PrincipalForeground;
            this.PrincipalStyleFluent = PrincipalStyleFluent;
            this.PrincipalStyleFluent2 = PrincipalStyleFluent2;
        }

        public void ChangeTheme(string theme)
        {
            switch (theme)
            {
                case "BaseDark":
                    ThemeDark();
                    break;
                case "BaseLight":
                    ThemeLigth();
                    break;
            }
        }

        void ThemeDark()
        {
            PrincipalBackgroundSolid.Fill = BrD;
            PrincipalBackgroundFluent.Background = BrDTop;
            PrincipalForeground.Foreground = Media.Brushes.White;
            PrincipalStyleFluent.Style = App.Current.FindResource("FluentStyle") as Style;
            PrincipalStyleFluent2.Style = App.Current.FindResource("FluentStyle2") as Style;
            MahApps.Metro.ThemeManager.ChangeAppTheme(App.Current, "BaseDark");
            p.SetLightDark(true);
            Fluent_Note.Properties.Settings.Default.Theme = "BaseDark";
            Fluent_Note.Properties.Settings.Default.Save();
            Fluent_Note.Properties.Settings.Default.Upgrade();
        }

        void ThemeLigth()
        {
            PrincipalBackgroundFluent.Background = BrL;
            PrincipalBackgroundSolid.Fill = Media.Brushes.White;
            PrincipalForeground.Foreground = Media.Brushes.Black;
            PrincipalStyleFluent.Style = App.Current.FindResource("FluentStyleLigth") as Style;
            PrincipalStyleFluent2.Style = App.Current.FindResource("FluentStyle2Light") as Style;
            MahApps.Metro.ThemeManager.ChangeAppTheme(App.Current, "BaseLight");
            p.SetLightDark(false);
            Fluent_Note.Properties.Settings.Default.Theme = "BaseLight";
            Fluent_Note.Properties.Settings.Default.Save();
            Fluent_Note.Properties.Settings.Default.Upgrade();
        }
    }
}
