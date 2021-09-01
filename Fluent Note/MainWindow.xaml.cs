using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Diagnostics;
using System.Windows.Threading;
using Fluent_Note.Bindings;

namespace Fluent_Note
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [STAThread]
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public Theme.ThemeDL themeDl;
        public Bindings.Bindos binds = new Bindings.Bindos();
        public int opc = Fluent_Note.Properties.Settings.Default.Opc;
        string pathactual = null;
        DispatcherTimer timer = new DispatcherTimer();
        PrintDialog printDlg = new PrintDialog(); coleccionesFonts cFont = new coleccionesFonts();
        string[] ArchivosAdmitidos = { ".txt",".sql", ".cs", ".vb", ".xaml", ".bat", ".cmd", ".html", ".css", ".php", ".js", ".java", ".xml", ".json", ".ts", ".htaccess" };
      

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
            this.Deactivated += MainWindow_Deactivated;
            this.Activated += MainWindow_Activated;
            this.StateChanged += MainWindow_StateChanged;
            this.closebtn.Click += Closebtn_Click;
            this.minibtn.Click += Minibtn_Click;
            this.maxibtn.Click += Maxibtn_Click;
            this.morebtn.Click += Morebtn_Click;
            this.popmORE.Closed += PopmORE_Closed;
            this.fontbtn.Click += Fontbtn_Click;
            this.SizeChanged += MainWindow_SizeChanged;
            this.FontStybtn.Click += Fontbtn_Click;
            this.Styletextbtn.Click += styletextbtn1_Click;
            this.Printbtn2.Click += printbtn_Click;
            this.CreatNewFileBTn.Click += CreatNewFileBTn_Click;
            this.NewFilebtn1.Click += CreatNewFileBTn_Click;
            this.Saveasbtn.Click += saveAllbtn_Click;
            this.DataContext = binds;
            popmORE.IsOpen = false;
            themeDl = new Theme.ThemeDL(ThemeDL, bordertitleup, tile, savebtn, minibtn);
            timer.Interval = new TimeSpan(0, 0, 20, 0, 0);
            timer.Tick += Timer_Tick;
            themeDl.ChangeTheme(Fluent_Note.Properties.Settings.Default.Theme);
            changecolor(Fluent_Note.Properties.Settings.Default.Color);
            

        }
        void Archiveros()
        {
            /*string mu = "";
            foreach (System.Diagnostics.Process s in System.Diagnostics.Process.GetProcessesByName("Fluent Note"))
            {
                
                mu += s.ProcessName + " ";
            }
            
            MessageBox.Show(mu);*/
            //Environment.GetCommandLineArgs() obtiene todos los archivos que se estan abriendo

            if (Environment.CommandLine.Contains("\" \""))
            {
                var ruta = Environment.CommandLine.Split(new char[] { '"' })[3];
                if (IsArchivoAdmitido(ruta) == true)
                {
                    cargar(ruta);
                    textotext.Focus();
                }
                else
                {
                    App.Current.Shutdown();
                }
            }
            else
            {
                tile.Text = "Fluent Note - Sin Titulo";
            }
        }

        void crearnuevo()
        {
            tile.Text = "Fluent Note - Sin Titulo";
            pathactual = null;
            nowstring = "";
            textotext.Clear();
            textotext.UndoLimit = 0;
            textotext.UndoLimit = int.MaxValue;
            textotext.Focus();
        }

        private void CreatNewFileBTn_Click(object sender, RoutedEventArgs e)
        {
            
            if (tile.Text.Contains('*') && textotext.Text != "" && pathactual == null)
            {
                optionswindow optacer = new Fluent_Note.optionswindow();
                var result = optacer.Mensaje("Creando Archivo Nuevo", "¿Desea Guardar?", 3);
                if (result == 1)
                {
                    guardar(false);
                    crearnuevo();
                }
                else if (result == 2)
                {
                    crearnuevo();
                }

            }
            else if(tile.Text.Contains('*') && pathactual != null)
            {
                optionswindow optacer = new Fluent_Note.optionswindow();
                var resutl = optacer.Mensaje("Mensaje", "¿Desea Sobreescribir el archivo?", 3);
                optacer.Owner = this;
                if (resutl == 1)
                {
                    guardar(false);
                    crearnuevo();
                }
                else if (resutl == 2)
                    crearnuevo();
                
            }else
            {
                crearnuevo();
            }
          
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            alzheimer();
        }

        public static void alzheimer()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //tile.Text = "ActualWidth = " + this.ActualWidth;
            if(this.ActualWidth < 713)
                searchcontainer.Width = 245;
            else
                searchcontainer.Width = 340;

            if (this.ActualWidth < 610)
            {
                
                printbtn.Visibility = Visibility.Collapsed;
                Printbtn2.Visibility = Visibility.Visible;
            }
            else
            {
               
                printbtn.Visibility = Visibility.Visible;
                Printbtn2.Visibility = Visibility.Collapsed;
            }

            if (this.ActualWidth < 560)
            {
                styletextbtn1.Visibility = Visibility.Collapsed;
                Styletextbtn.Visibility = Visibility.Visible;
            }
            else
            {
                styletextbtn1.Visibility = Visibility.Visible;
                Styletextbtn.Visibility = Visibility.Collapsed;
            }

            if (this.ActualWidth < 515)
            {
                fontbtn.Visibility = Visibility.Collapsed;
                FontStybtn.Visibility = Visibility.Visible;
            }
            else
            {
                fontbtn.Visibility = Visibility.Visible;
                FontStybtn.Visibility = Visibility.Collapsed;
            }

            if (this.ActualWidth < 470)
            {
                NewFilebtn1.Visibility = Visibility.Collapsed;
                CreatNewFileBTn.Visibility = Visibility.Visible;
            }
            else
            {
                NewFilebtn1.Visibility = Visibility.Visible;
                CreatNewFileBTn.Visibility = Visibility.Collapsed;
            }
            if (this.ActualWidth < 425)
            {
                saveAllbtn.Visibility = Visibility.Collapsed;
                Saveasbtn.Visibility = Visibility.Visible;
            }
            else
            {
               
                saveAllbtn.Visibility = Visibility.Visible;
                Saveasbtn.Visibility = Visibility.Collapsed;
            }
        }

        private void Fontbtn_Click(object sender, RoutedEventArgs e)
        {
            optionswindow optw = new Fluent_Note.optionswindow();
            optw.Owner = this;
            optw.title.Content = "Formato del texto";
            optw.controlselect.SelectedIndex = 0;
            optw.Height = 376;
            optw.ShowDialog();
        }

        private void PopmORE_Closed(object sender, EventArgs e)
        {
            morebtn.IsEnabled = true;
            protector.Visibility = Visibility.Collapsed;
        }

        private void Morebtn_Click(object sender, RoutedEventArgs e)
        {
            if (tile.Foreground == Brushes.Black)
                menucard.Background = ThemeDL.Fill;
            else
                menucard.Background = bordertitleup.Background;
            popmORE.IsOpen = true;
            morebtn.IsEnabled = false;
            protector.Visibility = Visibility.Visible;
        }

        private void Maxibtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                bor.Margin = th1;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                bor.Margin = th2;
            }
        }

        private void Minibtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        Thickness th1 = new Thickness(0);
        Thickness th2 = new Thickness(7);

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                bor.Margin = th2;
                System.Windows.Shapes.Path p = (System.Windows.Shapes.Path)maxibtn.Content;
                p.Data = Geometry.Parse("M14.390625,28.78125L12.5390625,29.15625 11.015625,30.2109375 9.984375,31.7578125 9.609375,33.609375 9.609375,81.609375 9.984375,83.4609375 11.015625,84.984375 12.5390625,86.015625 14.390625,86.390625 62.390625,86.390625 64.2421875,86.015625 65.7890625,84.984375 66.84375,83.4609375 67.21875,81.609375 67.21875,33.609375 66.84375,31.734375 65.8125,30.1875 64.265625,29.15625 62.390625,28.78125 14.390625,28.78125z M14.15625,19.21875L62.625,19.21875 65.4140625,19.505859375 68.0625,20.3671875 70.4765625,21.708984375 72.5625,23.4375 74.291015625,25.5234375 75.6328125,27.9375 76.494140625,30.5859375 76.78125,33.375 76.78125,81.84375 76.494140625,84.626953125 75.6328125,87.2578125 74.28515625,89.66015625 72.5390625,91.7578125 70.44140625,93.50390625 68.0390625,94.8515625 65.408203125,95.712890625 62.625,96 14.15625,96 11.40234375,95.712890625 8.765625,94.8515625 6.34570264816284,93.50390625 4.2421875,91.7578125 2.49609375,89.654296875 1.1484375,87.234375 0.287109375,84.59765625 0,81.84375 0,33.375 0.287109375,30.591796875 1.1484375,27.9609375 2.49609375,25.55859375 4.2421875,23.4609375 6.33984327316284,21.71484375 8.7421875,20.3671875 11.373046875,19.505859375 14.15625,19.21875z M33.609375,0L67.171875,0 72.94921875,0.568359375 78.375,2.2734375 83.291015625,4.951171875 87.5390625,8.4375 91.04296875,12.66796875 93.7265625,17.578125 95.431640625,23.00390625 96,28.78125 96,62.390625 95.3203125,66.8203125 93.375,70.734375 90.328125,73.875 86.390625,75.984375 86.390625,28.453125 86.00390625,24.673828125 84.84375,21.1171875 83.021484375,17.89453125 80.6484375,15.1171875 77.7890625,12.83203125 74.5078125,11.0859375 70.927734375,9.978515625 67.171875,9.609375 20.015625,9.609375 22.125,5.671875 25.265625,2.625 29.1796875,0.6796875 33.609375,0z");
            }
            else
            {
                bor.Margin = th1;
                System.Windows.Shapes.Path p = (System.Windows.Shapes.Path)maxibtn.Content;
                p.Data = Geometry.Parse("M14.390625,9.609375L12.5390625,9.984375 11.015625,11.015625 9.984375,12.5390625 9.609375,14.390625 9.609375,81.609375 9.984375,83.4609375 11.015625,84.984375 12.5390625,86.015625 14.390625,86.390625 81.609375,86.390625 83.4609375,86.015625 84.984375,84.984375 86.015625,83.4609375 86.390625,81.609375 86.390625,14.390625 86.015625,12.5390625 84.984375,11.015625 83.4609375,9.984375 81.609375,9.609375 14.390625,9.609375z M14.15625,0L81.84375,0 84.59765625,0.287109375 87.234375,1.1484375 89.654296875,2.49609375 91.7578125,4.2421875 93.50390625,6.345703125 94.8515625,8.765625 95.712890625,11.40234375 96,14.15625 96,81.84375 95.712890625,84.59765625 94.8515625,87.234375 93.50390625,89.654296875 91.7578125,91.7578125 89.654296875,93.50390625 87.234375,94.8515625 84.59765625,95.712890625 81.84375,96 14.15625,96 11.40234375,95.712890625 8.765625,94.8515625 6.34570264816284,93.50390625 4.2421875,91.7578125 2.49609375,89.654296875 1.1484375,87.234375 0.287109375,84.59765625 0,81.84375 0,14.15625 0.287109375,11.40234375 1.1484375,8.765625 2.49609375,6.345703125 4.2421875,4.2421875 6.34570264816284,2.49609375 8.765625,1.1484375 11.40234375,0.287109375 14.15625,0z");
            }
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            bordertitleup.Background.Opacity = 1;
        }

        private void MainWindow_Deactivated(object sender, EventArgs e)
        {
            binds.BorderSystem = desact;
            bordertitleup.Background.Opacity = 0.84;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tile.Text[tile.Text.Length - 1].Equals('*') && textotext.Text != "" && pathactual == null)
            {
                optionswindow optacer = new Fluent_Note.optionswindow();                
                var result = optacer.Mensaje("Saliendo", "¿Desea Guardar?", 3);
                if (result == 1)
                {
                    guardar(false);
                }
                else if(result == 0)
                {
                    e.Cancel = true;
                }
            }
            else if(pathactual != null && tile.Text[tile.Text.Length - 1].Equals('*'))
            {
                optionswindow optacer = new Fluent_Note.optionswindow();
                var resutl = optacer.Mensaje("Mensaje", "¿Desea Sobreescribir el archivo?", 3);
                optacer.Owner = this;
                if (resutl == 1)
                {
                    guardar(false);
                }
                else if (resutl == 0)
                {
                    e.Cancel = true;
                }
                
            }
        }
        
        Brush desact = new SolidColorBrush(Color.FromRgb(102, 102, 102));

        bool IsArchivoAdmitido(string ruta)
        {
            bool r = false;
            foreach (string ar in ArchivosAdmitidos)
            {
                if (ruta.Contains(ar))
                {
                    r = true;
                    break;
                }
            }
            return r;
        }
        
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Archiveros();
            tile.TextTrimming = TextTrimming.CharacterEllipsis;
            //EnableBlur(this);
            textotext.Focus();
            binds.ColeccionFontFamily = cFont.cargarFontFamily();
            binds.ColeccionFontSize = cFont.cargarFontSize();
            binds.ColeccionFontStyle = cFont.cargarFontStyles();
            timer.Start();
            HandyControl.Themes.Theme.SetSkin(textotext, (Fluent_Note.Properties.Settings.Default.Theme == "BaseLight") ? HandyControl.Data.SkinType.Violet : HandyControl.Data.SkinType.Dark);
            HandyControl.Themes.Theme.SetSkin(searchtext, (Fluent_Note.Properties.Settings.Default.Theme == "BaseLight") ? HandyControl.Data.SkinType.Violet : HandyControl.Data.SkinType.Dark);
            HandyControl.Themes.Theme.SetSkin(newtexttxt, (Fluent_Note.Properties.Settings.Default.Theme == "BaseLight") ? HandyControl.Data.SkinType.Violet : HandyControl.Data.SkinType.Dark);
            HandyControl.Themes.Theme.SetSkin(oldtexttxt, (Fluent_Note.Properties.Settings.Default.Theme == "BaseLight") ? HandyControl.Data.SkinType.Violet : HandyControl.Data.SkinType.Dark);

        }

        private void searchtext_GotFocus(object sender, RoutedEventArgs e)
        {
            searchborder.Opacity = 1;
        }

        private void searchtext_LostFocus(object sender, RoutedEventArgs e)
        {
            searchborder.Opacity = 0.5;
        }

        private void protector_MouseDown(object sender, MouseButtonEventArgs e)
        {
            protector.Visibility = Visibility.Collapsed;
            morebtn.IsEnabled = true;
        }

        private void configbtn_Click(object sender, RoutedEventArgs e)
        {
            optionswindow otpe = new Fluent_Note.optionswindow();
            otpe.Owner = this;
            otpe.title.Content = "Configuraciones";
            otpe.controlselect.SelectedIndex = 2;
            otpe.Height = 479;
            otpe.ShowDialog();
        }

        private void styletextbtn1_Click(object sender, RoutedEventArgs e)
        {
            optionswindow otpe = new Fluent_Note.optionswindow();
            otpe.Owner = this;
            otpe.title.Content = "Alineacion del Texto";
            otpe.controlselect.SelectedIndex = 1;
            otpe.Height = 370;
            otpe.ShowDialog();
        }
        private void printbtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textotext.Text))
            {
                popmORE.IsOpen = false;
                if (printDlg.ShowDialog() == true)
                {
                    optionswindow optacer = new Fluent_Note.optionswindow();                    
                    RichTextBox t = new RichTextBox();
                    t.AppendText(textotext.Text);
                    FlowDocument doc = t.Document;
                    doc.LineHeight = 1;
                    double pageHeight = doc.PageHeight;
                    double pageWidth = doc.PageWidth;
                    double columnGap = doc.ColumnGap;
                    double columnWidth = doc.ColumnWidth;
                    doc.PageHeight = printDlg.PrintableAreaHeight;
                    doc.PageWidth = printDlg.PrintableAreaWidth;
                    doc.ColumnGap = 5;
                    doc.ColumnWidth = doc.PageWidth - doc.ColumnGap - doc.PagePadding.Left - doc.PagePadding.Right;
                    doc.PageHeight = pageHeight;
                    doc.PageWidth = pageWidth;
                    doc.ColumnGap = columnGap;
                    doc.ColumnWidth = columnWidth;
                    doc.PagePadding = new Thickness(50);
                    doc.FontSize = textotext.FontSize;
                    doc.FontFamily = textotext.FontFamily;
                    doc.FontStyle = textotext.FontStyle;
                    doc.TextAlignment = textotext.TextAlignment;                    
                    doc.Foreground = Brushes.Black;
                    doc.Name = "FlowDoc";
                    printDlg.PrintDocument(((IDocumentPaginatorSource)doc).DocumentPaginator, "Impresora");
                    t = null; doc = null;                    
                    optacer.Mensaje("Mensaje", "Proceso terminado", 1);
                }
                else
                {
                    optionswindow optacer = new Fluent_Note.optionswindow();
                    optacer.Mensaje("Mensaje", "No se imprimira nada", 1);
                }
            }
            else
            {
                optionswindow optacer = new Fluent_Note.optionswindow();
                optacer.Mensaje("Mensaje", "No hay nada que imprimir", 1);
            }
        }

        string nowstring = "";
        private void textotext_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (pathactual != "")
            {
                if (textotext.Text != nowstring)
                {
                    savebtn.IsEnabled = true;
                    savebtn.Opacity = 1;
                    if (!tile.Text.ToString().Contains("*"))
                        this.tile.Text += "*";
                }
                else
                {
                    savebtn.IsEnabled = false;
                    savebtn.Opacity = 0.25;
                    if (tile.Text.ToString().Contains('*') == true)
                        tile.Text = tile.Text.ToString().Replace("*","");
                }
            }
            else
            {
                if (textotext.Text != "")
                {
                    savebtn.IsEnabled = true;
                    savebtn.Opacity = 1;
                    if (!tile.Text.ToString().Contains("*"))
                        this.tile.Text += "*";
                }
                else
                {
                    savebtn.IsEnabled = false;
                    savebtn.Opacity = 0.25;
                    if (tile.Text.ToString().Contains('*') == true)
                        tile.Text = tile.Text.ToString().Replace("*", "");
                }
            }
        }

        async void cargar(string ruta)
        {
            savebtn.IsEnabled = false;
            savebtn.Opacity = 0.25;
            using (StreamReader sr = new StreamReader(ruta, Encoding.Default, true))
            {
               
                nowstring = await sr.ReadToEndAsync();
            }
            pathactual = ruta;
            var utf8_ = System.Text.Encoding.Default.GetBytes(nowstring);
            nowstring = System.Text.Encoding.Default.GetString(utf8_);
            binds.Texto = nowstring;
            this.tile.Text = "Fluent Note - " + System.IO.Path.GetFileName(ruta);
        }
        string extensiones()
        {
            string filter = "";
            foreach (string s in ArchivosAdmitidos)
            {
                filter += "Archivos de " + s.ToUpperInvariant() + "|*" + s + "|";
            }
            filter = filter.Remove(filter.Length - 1);
            return filter;
        }
        void guardar(bool creararchivo)
        {
            if (tile.Text[tile.Text.Length - 1].Equals('*'))
            {
                if (pathactual == null)
                {
                    using (System.Windows.Forms.SaveFileDialog sv = new System.Windows.Forms.SaveFileDialog())
                    {
                        sv.Title = "Donde lo Guardará";                        
                        sv.Filter = extensiones();
                        sv.FileName = "Sin Titulo";
                        sv.OverwritePrompt = true;
                        if (sv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            System.IO.File.WriteAllText(sv.FileName, textotext.Text, Encoding.Default);
                            optionswindow optacer = new Fluent_Note.optionswindow();
                            optacer.Mensaje("Mensaje", "Guardado",1);
                            if (creararchivo == true)
                                cargar(sv.FileName);
                            
                        }
                    }
                }
                else
                {
                    System.IO.File.WriteAllText(pathactual, textotext.Text, Encoding.Default);
                    if (creararchivo == true)
                        cargar(pathactual);
                   
                }

            }
        }

        private void savebtn_Click(object sender, RoutedEventArgs e)
        {
            guardar(true);
        }


        private void openFilebtn_Click(object sender, RoutedEventArgs e)
        {
            if (tile.Text.Contains('*') && textotext.Text != "" && pathactual == null)
            {
                optionswindow optacer = new Fluent_Note.optionswindow();
                var result = optacer.Mensaje("Abriendo Archivo", "¿Desea Guardar?", 3);
                if (result == 1)
                {
                    guardar(false);
                    openfile();
                }
                else if (result == 2)
                {
                    openfile();
                }

            }
            else if (tile.Text.Contains('*') && pathactual != null)
            {
                optionswindow optacer = new Fluent_Note.optionswindow();
                var resutl = optacer.Mensaje("Mensaje", "¿Desea Sobreescribir el archivo?", 3);
                optacer.Owner = this;
                if (resutl == 1)
                {
                    guardar(false);
                    openfile();
                }
                else if (resutl == 2)
                    openfile();

            }
            else
            {
                openfile();
            }
           
        }
        void openfile()
        {
            using (System.Windows.Forms.OpenFileDialog op = new System.Windows.Forms.OpenFileDialog())
            {
                op.Title = "Abrir Texto";
                op.Filter = extensiones();
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    cargar(op.FileName);
                }
            }
        }

        List<object> buscar(string t)
        {
            List<object> resultadofinal = new List<object>();
            List<int[]> posiciones = new List<int[]>();
            string Textoeva = binds.Texto.ToLower(); var countext = 0;
            for (int x = 0; x < Textoeva.Length; x++)
            {                
                if (Textoeva[x] == t.ToLower()[0])
                {
                    String finals = "";
                    int y = x;
                    try
                    {
                        for (int h = 0; h < t.Length; h++)
                        {
                            finals += Textoeva[y];
                            y++;
                        }
                    }catch{ }
                    if (finals.Equals(t.ToLower()))
                    {
                        countext += 1;
                        posiciones.Add(new int[] { x, t.Length });
                    }
                }
            }
            resultadofinal.Add(countext);
            resultadofinal.Add(posiciones);
            return resultadofinal;
        }
        
        private void searchtext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(binds.Texto) && !string.IsNullOrWhiteSpace(searchtext.Text))
                {
                    optionswindow optacer = new Fluent_Note.optionswindow();
                    optacer.Mensaje("Mensaje", "No hay nada para Buscar", 1);
                }else if (!string.IsNullOrWhiteSpace(searchtext.Text))
                {
                    var results = buscar(searchtext.Text);
                    if ((int)results[0] == 1)
                    {
                        var listain = (List<int[]>)results[1];
                        var inicio = ((int[])listain[0])[0];
                        var fin = ((int[])listain[0])[1];
                        textotext.Focus();
                        textotext.Select(inicio, fin);
                        textotext.ScrollToLine(textotext.GetLineIndexFromCharacterIndex(inicio));
                    }
                    else
                    {
                        optionswindow optacer = new Fluent_Note.optionswindow();
                        optacer.Mensaje("Resultados", "Se encontraron " + results[0].ToString() + " " + '"' + searchtext.Text + '"' + " en el texto", 4, results);
                    }
                }
            }            
        }

        private void Acercabtn_Click(object sender, RoutedEventArgs e)
        {
           
            optionswindow optacer = new Fluent_Note.optionswindow();
            optacer.Owner = this;
            optacer.controlselect.SelectedIndex = 3;
            optacer.title.Content = "Acerca de";
            optacer.Height = 170;
            optacer.ShowDialog();
        }

        private void saveAllbtn_Click(object sender, RoutedEventArgs e)
        {
            if (textotext.Text != "")
            {
                using (System.Windows.Forms.SaveFileDialog sv = new System.Windows.Forms.SaveFileDialog())
                {
                    sv.Title = "Guardar como";
                    sv.Filter = extensiones();
                    sv.FileName = System.IO.Path.GetFileNameWithoutExtension(pathactual) + ".txt";
                    if (sv.FileName == ".txt")
                    {
                        sv.FileName = "Sin titulo.txt";
                    }
                    sv.OverwritePrompt = true;
                    if (sv.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(sv.FileName, textotext.Text, Encoding.Default);
                        optionswindow optacer = new Fluent_Note.optionswindow();
                        optacer.Mensaje("Resultados", "Guardado", 1);
                        cargar(sv.FileName);
                    }
                }
            }
        }
        MaterialDesignThemes.Wpf.PaletteHelper p = new MaterialDesignThemes.Wpf.PaletteHelper();
        public void changecolor(string color)
        {
            if (tile.Foreground != Brushes.Black)
            {
               
                MahApps.Metro.ThemeManager.ChangeAppStyle(App.Current, MahApps.Metro.ThemeManager.GetAccent(color), MahApps.Metro.ThemeManager.GetAppTheme("BaseDark"));

            }
            else
            {
                MahApps.Metro.ThemeManager.ChangeAppStyle(App.Current, MahApps.Metro.ThemeManager.GetAccent(color), MahApps.Metro.ThemeManager.GetAppTheme("BaseLight"));
            }
            Fluent_Note.Properties.Settings.Default.Color = color;
            Fluent_Note.Properties.Settings.Default.Save();
            Fluent_Note.Properties.Settings.Default.Upgrade();
        }

        private void searchbtn_Click(object sender, RoutedEventArgs e)
        {
            searchtext.Text = "";
            newtexttxt.Text = "";
            oldtexttxt.Text = "";
            if (controloptionsearch.SelectedIndex == 0)
            {
                controloptionsearch.SelectedIndex = 1;                
            }
            else
            {
                controloptionsearch.SelectedIndex = 0;
            }
        }

        private void oldtexttxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (oldtexttxt.Text != "")
            {
               
                if (controlchangetexts.IsOpen == false)
                {
                    controlchangetexts.IsOpen = true;
                }
            }
            else
            {
                if (controlchangetexts.IsOpen == true)
                {
                    controlchangetexts.IsOpen = false;
                }
            }
        }

      
        private void changetextbtn_Click(object sender, RoutedEventArgs e)
        {
            if (textotext.Text != "")
            {                
                if (textotext.Text.Contains(oldtexttxt.Text))
                {
                    binds.Texto = textotext.Text.Replace(oldtexttxt.Text, newtexttxt.Text);      
                }
                else
                {
                    optionswindow optacer = new Fluent_Note.optionswindow();
                    optacer.Mensaje("Resultados", "No existe el texto: " + '"' + oldtexttxt.Text + '"',1);
                }
            }
            else
            {
                optionswindow optacer = new Fluent_Note.optionswindow();
                optacer.Mensaje("Resultados", "El editor esta vacio", 1);
            }
        }

        private void textotext_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.S))
            {
                guardar(true);
            }
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.E))
            {
                textotext.SelectAll();
            }
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.T))
            {
                binds.TextAlig = TextAlignment.Center;   
            }
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.J))
            {
                binds.TextAlig = TextAlignment.Justify;
            }
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.D))
            {
                binds.TextAlig = TextAlignment.Right;
            }
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.A))
            {
                binds.TextAlig = TextAlignment.Left;
            }
        }

        private void window_GotFocus(object sender, RoutedEventArgs e)
        {
            if (pathactual != null)
            {
                if (!File.Exists(pathactual))
                {
                    string[] auxs = { tile.Text, textotext.Text };
                    crearnuevo();
                    optionswindow op = new optionswindow();
                    op.Mensaje("Mensaje", "El archivo se ha movido, eliminado o cambiado de nombre", 1);
                    binds.Texto = auxs[1];
                    textotext.Text = binds.Texto;
                    tile.Text = auxs[0] + "*";
                }
            }
        }
    }
}