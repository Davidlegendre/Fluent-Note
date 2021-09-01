using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using Fluent_Note.Modelos;
using System.Windows.Controls;

namespace Fluent_Note.Bindings
{
    public class Bindos : INotifyPropertyChanged
    {
        string _texto;
        Brush _bordersystem;
        TextAlignment _textalignment;
        double _fontSize;
        double _ColorTop;
        FontStyle _fontStyle;
        FontFamily _fontfamily;
        string _TextWrapping;
        Brush _foreground;
        Brush _background;
        ObservableCollection<string> _coleccionFontFamily;
        ObservableCollection<string> _coleccionFontSize;
        ObservableCollection<string> _coleccionFontStyle;

        public Bindos()
        {
            _texto = "";
            _ColorTop = Fluent_Note.Properties.Settings.Default.colortop;
            _bordersystem = Brushes.Black;
            _textalignment = ConvertStringTextAling(Fluent_Note.Properties.Settings.Default.TextAling);
            _fontSize = Fluent_Note.Properties.Settings.Default.FontSize;
            if (Fluent_Note.Properties.Settings.Default.FontStyle == "Normal")
                _fontStyle = FontStyles.Normal;
            else if (Fluent_Note.Properties.Settings.Default.FontStyle == "Italic")
                _fontStyle = FontStyles.Italic;
            else
                _fontStyle = FontStyles.Oblique;
            _fontfamily = new FontFamily(Fluent_Note.Properties.Settings.Default.FontFamily);
         
            _coleccionFontFamily = null;
            _coleccionFontSize = null;
            _coleccionFontStyle = null;
            _TextWrapping = Fluent_Note.Properties.Settings.Default.TextWrap;
            _foreground = (Fluent_Note.Properties.Settings.Default.Theme == "BaseDark") ? Brushes.White : Brushes.Black;
            _background = (_foreground == Brushes.Black) ? new SolidColorBrush(System.Windows.Media.Color.FromRgb(44, 44, 44)): Brushes.White;
        }

        TextAlignment ConvertStringTextAling(string opcion)
        {
            if (opcion == "Left")
                return TextAlignment.Left;
            if (opcion == "Right")
                return TextAlignment.Right;
            if (opcion == "Justify")
                return TextAlignment.Justify;
            if (opcion == "Center")
                return TextAlignment.Center;
            return 0;
        }

        public string textWrapping
        {
            get { return _TextWrapping; }
            set
            {
                if (value != _TextWrapping)
                {
                    _TextWrapping = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("textWrapping"));
                }
            }
        }

        public Brush Foreground
        {
            get { return _foreground; }
            set {
                if (value != _foreground)
                {
                    _foreground = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Foreground"));
                }
            }
        }


        public Brush Background
        {
            get { return _background; }
            set
            {
                if (value != _background)
                {
                    _background = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Background"));
                }
            }
        }

        public double ColorTop
        {
            get { return _ColorTop; }
            set
            {
                if (value != _ColorTop)
                {
                    _ColorTop = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ColorTop"));
                }
            }
        }

        public string Texto
        {
            get { return _texto; }
            set
            {
                if (value != _texto)
                {
                    _texto = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Texto"));
                }
            }
        }

        public ObservableCollection<string> ColeccionFontFamily
        {
            get
            {
                return _coleccionFontFamily;
            }
            set
            {
                if (value != _coleccionFontFamily)
                {
                    _coleccionFontFamily = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ColeccionFontFamily"));
                }
            }
        }
        public ObservableCollection<string> ColeccionFontSize
        {
            get
            {
                return _coleccionFontSize;
            }
            set
            {
                if (value != _coleccionFontSize)
                {
                    _coleccionFontSize = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ColeccionFontSize"));
                }
            }
        }
        public ObservableCollection<string> ColeccionFontStyle
        {
            get
            {
                return _coleccionFontStyle;
            }
            set
            {
                if (value != _coleccionFontStyle)
                {
                    _coleccionFontStyle = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ColeccionFontStyle"));
                }
            }
        }

        public FontFamily FontFamily
        {
            get
            {
                return _fontfamily;
            }
            set
            {
                if (value != _fontfamily)
                {
                    _fontfamily = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FontFamily"));
                }
            }
        }

        public double FontSize
        {
            get {
                return _fontSize;
            }
            set {
                if (value != _fontSize)
                {
                    _fontSize = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FontSize"));
                }
            }
        }

        public FontStyle FontStyle
        {
            get
            {
                return _fontStyle;
            }
            set
            {
                if (value != _fontStyle)
                {
                    _fontStyle = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FontStyle"));
                }
            }
        }

        public TextAlignment TextAlig
        {
            get {
                return _textalignment;
            }
            set
            {
                if (value != _textalignment)
                {
                    _textalignment = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TextAlig"));
                }
            }
        }

        public Brush BorderSystem
        {
            get {
                return _bordersystem;
            }
            set {
                if (value != _bordersystem)
                {
                    _bordersystem = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("BorderSystem"));
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
