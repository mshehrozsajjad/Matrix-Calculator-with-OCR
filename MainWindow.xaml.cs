using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace SemesterProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    using System.Drawing;
    using tessnet2;

    public partial class MainWindow : Window
    {
        Manual M = new Manual();
      
        StringBuilder mystring = new StringBuilder();
        int coloumn = 1;
        int row = 1;
        public MainWindow()
        {
            InitializeComponent();
            FlowDocument result = new FlowDocument();

            StackPanel myPanel = new StackPanel();
            myPanel.Margin = new Thickness(10);
            
            
          
          //  MyRectangle.Name = "myRectangle";
            
        }

     
        private void Input_Text(object sender, RoutedEventArgs e)
        {

            this.Close();
            M.Show();
        }



        public void Input_Image(object sender, RoutedEventArgs e)
        {
            //int size = -1;
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //var result = openFileDialog1.ShowDialog(); // Show the dialog.
            //string file = openFileDialog1.FileName;
            //// Bitmap loadedBitmap = Bitmap.FromFile(file);
            //if (file != null)
            //{
            //    try
            //    {
            //        string text = File.ReadAllText(file);
            //        size = text.Length;
            //        Console.WriteLine(size); // <-- Shows file size in debugging mode.
            //        Console.WriteLine(result); // <-- For debugging use.
            //    }
            //    catch (IOException)
            //    {
            //    }
            //    try
            //    {
            //        Bitmap myBmp = new Bitmap(file);
            //        var image = myBmp;
                                                   
            //        var ocr = new Tesseract();
                    
            //        ocr.SetVariable("preserve_interword_spaces","1");
                   
            //      // ocr.SetVariable("tessedit_char_whitelist", "0123456789"); // If digit only 
            //        //@"C:\OCRTest\tessdata" contains the language package, without this the method crash and app breaks 
            //        ocr.Init(@"C:\Users\MuhammadShahroz\Documents\Visual Studio 2013\Projects\ConsoleApplication3\tessdata", "eng", false);
            //        var results = ocr.DoOCR( image, Rectangle.Empty);
            //     string my = "";
            //        foreach (tessnet2.Word word in results)
            //        {
            //            Console.WriteLine("{0} : {1}", word.Confidence, word.Text);
            //           my = string.Format("{0}",word.Text);
            //           mystring.AppendFormat("{0} ", my);
                                               
            //        }
                    
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // FlowDocument objf = new FlowDocument();
           // Paragraph objp = new Paragraph();
           
           // objp.Inlines.Add(new Run(mystring.ToString()));
           // objf.Blocks.Add(objp);
           // //RichTextBox myrich = new RichTextBox();
           // RichBox.Document = objf;
           // TextRange textRange = new TextRange( RichBox_Copy.Document.ContentStart,RichBox_Copy.Document.ContentEnd );
           // if (coloumn != 0)
           //     int.TryParse(textRange.Text, out coloumn);
           // else
           // {
           //     coloumn = 1;
           //     int.TryParse(textRange.Text, out coloumn);
           // }
           //     TextRange textRange1 = new TextRange(RichBox_Copy1.Document.ContentStart, RichBox_Copy1.Document.ContentEnd);
           //if(row != 0)
           //    int.TryParse(textRange1.Text,out row);
           //else
           //{
           //    row = 1;
           //    int.TryParse(textRange1.Text, out row);
           //}
           // Console.WriteLine("{0} : {1} ", coloumn, row);
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void move(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void myRectangleLoaded(object sender, RoutedEventArgs e)
        {
            //myStoryboard.Begin(this);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //RichBox.Document.Blocks.Clear();
            //RichBox_Copy.Document.Blocks.Clear();
            //RichBox_Copy1.Document.Blocks.Clear();
            //mystring.Clear();
            //row = 1;
            //coloumn = 1;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
