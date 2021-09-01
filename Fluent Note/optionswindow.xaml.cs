using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Fluent_Note
{

    /// <summary>
    /// Lógica de interacción para optionswindow.xaml
    /// </summary>
    public partial class optionswindow : Window
    {
        MainWindow mw = (MainWindow)App.Current.Windows[0];
        Brush selectnow;
        int AnteriorAlineacion = 1;

        public optionswindow()
        {
            InitializeComponent();
            this.Loaded += Optionswindow_Loaded;
            this.textLeftbtn.Click += TextLeftbtn_Click;
            this.Rightbtn.Click += Rightbtn_Click;
            this.Centerbtn.Click += Centerbtn_Click;
            this.Justifybtn.Click += Justifybtn_Click;
            this.Closed += Optionswindow_Closed;
            this.DataContext = mw.binds;
        }
        private void Optionswindow_Closed(object sender, EventArgs e)
        {
            if(mw.searchcontainer.IsEnabled == false)
                mw.searchcontainer.IsEnabled = true;
            Fluent_Note.Properties.Settings.Default.colortop = mw.binds.ColorTop;
            Fluent_Note.Properties.Settings.Default.TextWrap = mw.binds.textWrapping;
            Fluent_Note.Properties.Settings.Default.TextAling = TextAlingConvertString(mw.binds.TextAlig);
            Fluent_Note.Properties.Settings.Default.Save();
            Fluent_Note.Properties.Settings.Default.Upgrade();
        }

        string TextAlingConvertString(TextAlignment textoalineado)
        {
            if (textoalineado == TextAlignment.Left)
                return "Left";
            if (textoalineado == TextAlignment.Center)
                return "Center";
            if (textoalineado == TextAlignment.Right)
                return "Right";
            if (textoalineado == TextAlignment.Justify)
                return "Justify";
            return "";
        }

        void saveOciondeltextoestilo(int opc)
        {
            Fluent_Note.Properties.Settings.Default.Opc = opc;
            switch(opc)
            {
                case 1: mw.binds.TextAlig = TextAlignment.Left; AnteriorAlineacion = opc; break;
                case 2: mw.binds.TextAlig = TextAlignment.Right; AnteriorAlineacion = opc; break;
                case 3: mw.binds.TextAlig = TextAlignment.Center; AnteriorAlineacion = opc; break;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            mw.textotext.Focus();
        }
        
        private void Justifybtn_Click(object sender, RoutedEventArgs e)
        {
            mw.binds.TextAlig = TextAlignment.Justify;            
            saveOciondeltextoestilo(4);
            SelectTEf(4);
        }

        private void Centerbtn_Click(object sender, RoutedEventArgs e)
        {
            saveOciondeltextoestilo(3);
            SelectTEf(3);
        }

        private void Rightbtn_Click(object sender, RoutedEventArgs e)
        {
            saveOciondeltextoestilo(2);
            SelectTEf(2);
        }

        void SelectTEf(int i)
        {
            foreach (MahApps.Metro.Controls.Tile t in listate.Children)
            {
                t.IsEnabled = true;
                t.Background = null;
            }
            mw.opc = i;
            switch (i)
            {
                case 1:
                    textLeftbtn.Background = selectnow;
                    textLeftbtn.IsEnabled = false;
                    break;
                case 2:
                    Rightbtn.Background = selectnow;
                    Rightbtn.IsEnabled = false;
                    break;
                case 3:
                    Centerbtn.Background = selectnow;
                    Centerbtn.IsEnabled = false;
                    break;
                case 4:
                    Justifybtn.Background = selectnow;
                    Justifybtn.IsEnabled = false;
                    break;
            }
            if (Fluent_Note.Properties.Settings.Default.AjusteTexto == 0)
            { Justifybtn.IsEnabled = false; Justifybtn.Opacity = 0.5; }
        }

        private void TextLeftbtn_Click(object sender, RoutedEventArgs e)
        {           
            saveOciondeltextoestilo(1);
            SelectTEf(1);
        }

        void change()
        {            
            selectnow = cancelbtn.Background;
            mw.binds.Background = (mw.binds.Foreground == Brushes.Black) ? Brushes.White : new SolidColorBrush(System.Windows.Media.Color.FromRgb(44, 44, 44));
            HandyControl.Themes.Theme.SetSkin(mw.textotext, (Fluent_Note.Properties.Settings.Default.Theme == "BaseLight") ? HandyControl.Data.SkinType.Default : HandyControl.Data.SkinType.Dark);
            HandyControl.Themes.Theme.SetSkin(cambofont, (Fluent_Note.Properties.Settings.Default.Theme == "BaseLight") ? HandyControl.Data.SkinType.Violet : HandyControl.Data.SkinType.Dark);
            HandyControl.Themes.Theme.SetSkin(tamcombo, (Fluent_Note.Properties.Settings.Default.Theme == "BaseLight") ? HandyControl.Data.SkinType.Violet : HandyControl.Data.SkinType.Dark);
            HandyControl.Themes.Theme.SetSkin(estilocombo, (Fluent_Note.Properties.Settings.Default.Theme == "BaseLight") ? HandyControl.Data.SkinType.Violet : HandyControl.Data.SkinType.Dark);
            HandyControl.Themes.Theme.SetSkin(mw.searchtext, (Fluent_Note.Properties.Settings.Default.Theme == "BaseLight") ? HandyControl.Data.SkinType.Violet : HandyControl.Data.SkinType.Dark);
            HandyControl.Themes.Theme.SetSkin(mw.newtexttxt, (Fluent_Note.Properties.Settings.Default.Theme == "BaseLight") ? HandyControl.Data.SkinType.Violet : HandyControl.Data.SkinType.Dark);
            HandyControl.Themes.Theme.SetSkin(mw.oldtexttxt, (Fluent_Note.Properties.Settings.Default.Theme == "BaseLight") ? HandyControl.Data.SkinType.Violet : HandyControl.Data.SkinType.Dark);

            if (mw.binds.ColorTop == 0.4)
                Colortoptgg.IsChecked = true;
            else
                Colortoptgg.IsChecked = false;
            if (Fluent_Note.Properties.Settings.Default.AjusteTexto == 1)
                AjusteToggle.IsChecked = true;
            else
                AjusteToggle.IsChecked = false;
            this.closebtn.Style = mw.minibtn.Style;
            this.textLeftbtn.Style = mw.savebtn.Style;

            if (mw.binds.Foreground == Brushes.Black)
            {
                ligthbtn.Background = selectnow;
                darkbtn.Background = null;
                ligthbtn.IsEnabled = false;
                darkbtn.IsEnabled = true;
            }
            else
            {
                darkbtn.Background = selectnow;
                ligthbtn.Background = null;
                ligthbtn.IsEnabled = true;
                darkbtn.IsEnabled = false;
            }
        }


        private void Optionswindow_Loaded(object sender, RoutedEventArgs e)
        {
            change();
            SelectTEf(mw.opc);
            if (this.controlselect.SelectedIndex == 0)
            {
                this.cambofont.SelectedItem = mw.binds.FontFamily.ToString();
                this.tamcombo.SelectedItem = mw.binds.FontSize + " px";
                this.estilocombo.SelectedItem = mw.binds.FontStyle.ToString();
                savebtn.IsEnabled = false;
                savebtn.Opacity = 0.5;            
            }
            if (controlselect.SelectedIndex == 4)
            {
                    mw.textotext.Focus();
            }
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ligthbtn_Click(object sender, RoutedEventArgs e)
        {
            mw.themeDl.ChangeTheme("BaseLight");
            mw.binds.Foreground = Brushes.Black;
            change();

        }

        private void darkbtn_Click(object sender, RoutedEventArgs e)
        {
            mw.themeDl.ChangeTheme("BaseDark");
            mw.binds.Foreground = Brushes.White;
            change();
        }

        private void savebtn_Click(object sender, RoutedEventArgs e)
        {
            if (tamcombo.SelectedIndex != -1)
            {
                mw.binds.FontSize = Convert.ToDouble(tamcombo.SelectedItem.ToString().Replace(" px", ""));
                Fluent_Note.Properties.Settings.Default.FontSize = mw.binds.FontSize;
            }
            if (string.IsNullOrWhiteSpace(cambofont.Text))
            {
                optionswindow optad = new optionswindow();
                optad.Mensaje("Mensaje", "Seleccione alguna fuente", 1);
                cambofont.Focus();
            }else if (cambofont.SelectedIndex != -1)
            {
                mw.binds.FontFamily = new FontFamily(cambofont.SelectedItem.ToString());
                Fluent_Note.Properties.Settings.Default.FontFamily = cambofont.SelectedItem.ToString();
            }
            
            if (estilocombo.SelectedIndex != -1)
            {
                mw.binds.FontStyle = ConvertToFontStyle(estilocombo.SelectedItem.ToString());
                Fluent_Note.Properties.Settings.Default.FontStyle = estilocombo.SelectedItem.ToString();
            }
            Fluent_Note.Properties.Settings.Default.Save();
            Fluent_Note.Properties.Settings.Default.Upgrade();
            savebtn.IsEnabled = false;
        }

        FontStyle ConvertToFontStyle(string fontstyle)
        {
            FontStyle f = new FontStyle();
            switch (fontstyle)
            {
                case "Normal":
                    f= FontStyles.Normal;
                    break;
                case "Italic":
                    f= FontStyles.Italic;
                    break;
                default:
                    f= FontStyles.Oblique;
                    break;
            }
            return f;
        }

        private void twitbtn_Click(object sender, RoutedEventArgs e)
        {
           Process.Start("https://t.me/Davidlegendre");
        }

        private void facebtn_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.facebook.com/david.legendre.182");
        }

        private void colorbtn_Click(object sender, RoutedEventArgs e)
        {
            mw.changecolor(e.Source.ToString().Remove(0, e.Source.ToString().IndexOf(' ') + 1));
            change();
        }

        private void cambofont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            savebtn.IsEnabled = true;
            savebtn.Opacity = 1;
        }

        private void Colortoptgg_Click_1(object sender, RoutedEventArgs e)
        {
            if (Colortoptgg.IsChecked == true)
            {
                mw.binds.ColorTop = 0.4;
            }
            else
            {
                mw.binds.ColorTop = 0;
            }
        }

        private void AjusteToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (mw.binds.textWrapping == "NoWrap")
                wrapoption(1);
            else
                wrapoption(2);
        }
        /// <summary>
        ///opcion 1 --> wrap ; opcion 2 --> noWrap
        /// </summary>
        /// <param name="opcion"></param>
        void wrapoption(int opcion)
        {
            if (opcion == 1)
            {
                mw.binds.textWrapping = "Wrap";
                Justifybtn.IsEnabled = true;
                Fluent_Note.Properties.Settings.Default.AjusteTexto = 1;
                Justifybtn.Opacity = 1;
            }
            else if(opcion == 2)
            {
                mw.binds.textWrapping = "NoWrap";
                Justifybtn.IsEnabled = false;
                Fluent_Note.Properties.Settings.Default.AjusteTexto = 0;
                Justifybtn.Opacity = 0.5;
                SelectTEf(AnteriorAlineacion);
                saveOciondeltextoestilo(AnteriorAlineacion);
            }
        }
        int DialogoResultado;
        public int Mensaje(string titulo, string contenido, int opc, List<object> lista = null)
        {
            this.Owner = mw;
            this.controlselect.SelectedIndex = 4;
            this.title.Content = "";
            this.SizeToContent = SizeToContent.Height;   
            this.TituloMensaje.Content = titulo;
            this.ContentidoMensaje.Text = contenido;
            this.closebtn.Visibility = Visibility.Collapsed;
           
            this.MensajesOpcion(opc);
            chromewin.CaptionHeight = 0;
            this.Focusable = false;
            if (opc == 4)
            {
                if (lista != null)
                {
                    if ((int)lista[0] == 0)
                    {
                        this.MensajesOpcion(1);
                        this.ShowDialog();
                    }
                    else if ((int)lista[0] == 1)
                    {
                        navigationbutton.IsEnabled = false;
                        navigationbutton.Children[0].Opacity = 0.5;
                        navigationbutton.Children[1].Opacity = 0.5;
                    }
                    else
                    {
                        navigationbutton.Children[0].IsEnabled = false;
                        navigationbutton.Children[0].Opacity = 0.5;
                    }
                    if ((int)lista[0] >= 1)
                    {
                        mw.textotext.Focus();
                        var listaind = (List<int[]>)lista[1];
                        mw.textotext.SelectionStart = ((int[])listaind[0])[0];
                        mw.textotext.SelectionLength = ((int[])listaind[0])[1];
                        mw.textotext.ScrollToLine(mw.textotext.GetLineIndexFromCharacterIndex(mw.textotext.SelectionStart));
                        chromewin.CaptionHeight = 30;
                        listaindices = (List<int[]>)lista[1];
                        mw.searchcontainer.IsEnabled = false;
                        this.Show();
                    }

                }               
            }
            else
            {
                this.ShowDialog();
            }
            
            
            return DialogoResultado;
        }


        public void MensajesOpcion(int opc)
        {
            topbar.Visibility = Visibility.Collapsed;
            switch (opc)
            {
                case 1:
                    NormalOpcion.Visibility = Visibility.Visible;
                    YesCancelOpcion.Visibility = Visibility.Collapsed;
                    YesNoOpcion.Visibility = Visibility.Collapsed;
                    cancelbutton.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    NormalOpcion.Visibility = Visibility.Collapsed;
                    YesCancelOpcion.Visibility = Visibility.Visible;
                    YesNoOpcion.Visibility = Visibility.Collapsed;
                    cancelbutton.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    NormalOpcion.Visibility = Visibility.Collapsed;
                    YesCancelOpcion.Visibility = Visibility.Collapsed;
                    YesNoOpcion.Visibility = Visibility.Visible;
                    cancelbutton.Visibility = Visibility.Collapsed;
                    break;
                case 4:
                    cancelbutton.Visibility = Visibility.Visible;
                    NormalOpcion.Visibility = Visibility.Collapsed;
                    YesCancelOpcion.Visibility = Visibility.Collapsed;
                    YesNoOpcion.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            DialogoResultado = 0;
            this.Close();
        }

        private void yesbtn_Click(object sender, RoutedEventArgs e)
        {
            DialogoResultado = 1;
            this.Close();
        }

        private void Nobtn_Click(object sender, RoutedEventArgs e)
        {
            DialogoResultado = 2;
            this.Close();
        }
        List<int[]> listaindices; int contador = 0;
        private void atras_click(object sender, RoutedEventArgs e)
        {
            contador--;
            seleccionar(((int[])listaindices[contador])[0], ((int[])listaindices[contador])[1], backbtn, nextbtn);
            
        }

        void seleccionar(int inicio, int termino, MahApps.Metro.Controls.Tile back, MahApps.Metro.Controls.Tile next)
        {
            if (contador <= 0)
            {
                back.IsEnabled = false;
                back.Opacity = 0.5;
            }
            else
            {
                back.IsEnabled = true;
                back.Opacity = 1;
            }

            if (contador == listaindices.Count - 1)
            {
                next.IsEnabled = false;
                next.Opacity = 0.5;
            }
            else
            {
                next.IsEnabled = true;
                next.Opacity = 1;
            }
            mw.textotext.Focus();
            mw.textotext.Select(inicio, termino);
            mw.textotext.ScrollToLine(mw.textotext.GetLineIndexFromCharacterIndex(inicio));
            
        }

        private void adelante_click(object sender, RoutedEventArgs e)
        {
            contador++;
            seleccionar(((int[])listaindices[contador])[0], ((int[])listaindices[contador])[1], backbtn, nextbtn);
            
        }

        private void cambofont_GotFocus(object sender, RoutedEventArgs e)
        {
            cambofont.IsDropDownOpen = true;
        }

        private void cambofont_LostFocus(object sender, RoutedEventArgs e)
        {
            cambofont.IsDropDownOpen = false;
        }
    }
}
