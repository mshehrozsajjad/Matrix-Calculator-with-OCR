using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Forms.Integration;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;
using MessageBox = System.Windows.Forms.MessageBox;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using Rectangle = System.Drawing.Rectangle;

namespace SemesterProject
{
    /// <summary>
    /// Interaction logic for Manual.xaml
    /// </summary>
    /// 
    using tessnet2;

    public partial class Manual : Window
    {
        StringBuilder mystring = new StringBuilder();
        int[] Table;
        int[,] OcrTable;
        int[] Table1;
        int[,] OcrTable1;
        int column = 1;
        int row = 1;
        int CurrentRow;
        int CurrentColumn;

        int columnB = 1;
        int rowB = 1;
        int CurrentRowB;
        int CurrentColumnB;


        int leafsize = 18;

        public Manual()
        {

            InitializeComponent();
            
        }
        private void move(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        int[] Add(int[] array, int newValue)
        {
            int newLength = array.Length + 1;

            int[] result = new int[newLength];

            result[newLength - 1] = newValue;

            return result;
        }
        /***************************************************************
        *                        OCR IMPLEMENTATION
       ****************************************************************/
        private void Input_Image(object sender, RoutedEventArgs e)
        {
            int size = -1;
            row = Convert.ToInt32(Row11.Text);
            column = Convert.ToInt32(Column11.Text);
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                var result = openFileDialog1.ShowDialog(); // Show the dialog.
                string file = openFileDialog1.FileName;
                // Bitmap loadedBitmap = Bitmap.FromFile(file);
                if (file != null)
                {
                    try
                    {
                        string text = File.ReadAllText(file);
                        size = text.Length;
                        Console.WriteLine(size); // <-- Shows file size in debugging mode.
                        Console.WriteLine(result); // <-- For debugging use.
                    }
                    catch (IOException)
                    {
                    }
                    try
                    {
                        Bitmap myBmp = new Bitmap(file);
                        var image = myBmp;

                        var ocr = new Tesseract();

                        ocr.SetVariable("preserve_interword_spaces", "1");
                        ocr.SetVariable("tessedit_char_whitelist", "0123456789-");
                        ocr.Init("..\tessdata", "eng", false);
                        var results = ocr.DoOCR(image, Rectangle.Empty);
                        string my = "";

                        foreach (tessnet2.Word word in results)
                        {
                            //Console.WriteLine("{0} : {1}", word.Confidence, word.Text);
                            my = string.Format("{0}", word.Text);
                            mystring.Append(my);


                        }
                        for (int i = 0; i < mystring.Length; i++)
                        {
                            Console.WriteLine("{0} ", mystring[i]);
                        }
                        Table = new int[row * column];
                        for (int i = 0; i < mystring.Length; i++)
                        {
                            Table[i] = Convert.ToInt32(mystring[i] - 48);
                        }
                        Console.WriteLine("\n Table :");
                        for (int i = 0; i < Table.Length; i++)
                        {
                            Console.WriteLine("{0} ", Table[i]);
                        }
                        mystring.Clear();
                    }
                    catch (Exception)
                    {
                    }
                }


                CurrentRow = DataGrid.RowCount;
                CurrentColumn = DataGrid.ColumnCount;
                if (row * column == Table.Length)
                {
                    OcrTable = new int[row, column];
                    OcrTable = Make2DArray<int>(Table, row, column);

                    DataGrid.RowCount = row;
                    DataGrid.AllowUserToResizeRows = true;
                    CurrentRow = DataGrid.RowCount;

                    DataGrid.AllowUserToAddRows = false;

                    DataGrid.ColumnCount = column;
                    DataGrid.AllowUserToResizeColumns = true;
                    CurrentColumn = DataGrid.ColumnCount;

                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < column; j++)
                        {
                            DataGrid.Rows[i].Cells[j].Value = OcrTable[i, j];
                        }
                    }

                    DataGrid.AutoResizeColumns();
                    DataGrid.AutoResizeRows();


                }
                else
                    MessageBox.Show("Invalid Matrix!");
            }
            catch (Exception)
            {
                MessageBox.Show("Input Image");
            }
        }
        private void Input_Image2(object sender, RoutedEventArgs e)
        {
            int size = -1;
            rowB = Convert.ToInt32(Row11_Copy.Text);
            columnB = Convert.ToInt32(Column11_Copy.Text);
           try
          {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                var result = openFileDialog1.ShowDialog(); // Show the dialog.
                string file = openFileDialog1.FileName;
                // Bitmap loadedBitmap = Bitmap.FromFile(file);
                if (file != null)
                {
                    try
                    {
                        string text = File.ReadAllText(file);
                        size = text.Length;
                        Console.WriteLine(size); // <-- Shows file size in debugging mode.
                        Console.WriteLine(result); // <-- For debugging use.
                    }
                    catch (IOException)
                    {
                    }
                    try
                    {
                        Bitmap myBmp = new Bitmap(file);
                        var image = myBmp;

                        var ocr = new Tesseract();

                        //     ocr.SetVariable("preserve_interword_spaces", "1");
                        //ocr.SetVariable("tessedit_char_whitelist", "0123456789-");
                        ocr.Init("..\tessdata", "eng", false);
                        var results = ocr.DoOCR(image, Rectangle.Empty);
                        string my = "";

                        foreach (tessnet2.Word word in results)
                        {
                            //Console.WriteLine("{0} : {1}", word.Confidence, word.Text);
                            my = string.Format("{0}", word.Text);
                            mystring.Append(my);


                        }
                        for (int i = 0; i < mystring.Length; i++)
                        {
                            Console.WriteLine("{0} ", mystring[i]);
                        }
                        Table1 = new int[rowB * columnB];
                        for (int i = 0; i < mystring.Length; i++)
                        {
                            Table1[i] = Convert.ToInt32(mystring[i] - 48);
                        }
                        Console.WriteLine("\n Table :");
                        for (int i = 0; i < Table1.Length; i++)
                        {
                            Console.WriteLine("{0} ", Table1[i]);
                        }
                        mystring.Clear();
                    }
                    catch (Exception)
                    {
                    }
                }
            
            CurrentRowB = DataGrid1.RowCount;
            CurrentColumnB = DataGrid1.ColumnCount;
            if (rowB * columnB == Table1.Length)
            {
                OcrTable1 = new int[rowB, columnB];
                OcrTable1 = Make2DArray<int>(Table1, rowB, columnB);

                DataGrid1.RowCount = rowB;
                DataGrid1.AllowUserToResizeRows = true;
                CurrentRowB = DataGrid1.RowCount;

                DataGrid1.AllowUserToAddRows = false;

                DataGrid1.ColumnCount = columnB;
                DataGrid1.AllowUserToResizeColumns = true;
                CurrentColumnB = DataGrid1.ColumnCount;

                for (int i = 0; i < rowB; i++)
                {
                    for (int j = 0; j < columnB; j++)
                    {
                        DataGrid1.Rows[i].Cells[j].Value = OcrTable1[i, j];
                    }
                }

                DataGrid1.AutoResizeColumns();
                DataGrid1.AutoResizeRows();


            }
            else
                MessageBox.Show("Invalid Matrix!");
          }
          catch (Exception)
            {
                MessageBox.Show("Select Image");
            }
        }
        /***************************************************************
         *                        FIRST MATRIX
        ****************************************************************/
        private void Show_Matrix_A(object sender, RoutedEventArgs e)
        {
            // Generating Matrices
            row = Convert.ToInt32(Row11.Text);
            column = Convert.ToInt32(Column11.Text);
            CurrentRow = DataGrid.RowCount;
            CurrentColumn = DataGrid.ColumnCount;
            if (row > 0 && column > 0)
            {
               
                DataGrid.RowCount = row;
                DataGrid.AllowUserToResizeRows = true;
                CurrentRow = DataGrid.RowCount;
                
                DataGrid.AllowUserToAddRows = false;

                DataGrid.ColumnCount = column;
                DataGrid.AllowUserToResizeColumns = true;
                CurrentColumn = DataGrid.ColumnCount;
                DataGrid.AutoResizeColumns();
                DataGrid.AutoResizeRows();
            }
            else
            {
                MessageBox.Show("Please Give A Valid Input in Rows And Columns.");
            }
            
           
            Console.WriteLine("{0} : {1} ", column, row);
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            DataGrid.Columns.Add(col);
            Column11.Text = Convert.ToString(CurrentColumn + 1);
            CurrentColumn = DataGrid.ColumnCount;
            DataGrid.AutoResizeColumns();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            DataGrid.Rows.Add();
            Row11.Text = Convert.ToString(CurrentRow+1);
            CurrentRow = DataGrid.RowCount;
            DataGrid.AutoResizeRows();

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (CurrentRow > 1)
            {
                DataGrid.Rows.Remove(DataGrid.Rows[0]);
                Row11.Text = Convert.ToString(CurrentRow - 1);
                CurrentRow = DataGrid.RowCount;
                DataGrid.AutoResizeRows();
            }
            else
                MessageBox.Show("Invalid Selection.");
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (CurrentColumn > 1)
            {
                DataGrid.Columns.Remove(DataGrid.Columns[0]);
                Column11.Text = Convert.ToString(CurrentColumn - 1);
                CurrentColumn = DataGrid.ColumnCount;
                DataGrid.AutoResizeColumns();
            }
            else
                MessageBox.Show("Invalid Selection.");
        }

        /***************************************************************
         *                        SECOND MATRIX
        ****************************************************************/
        private void Show_Matrix_B(object sender, RoutedEventArgs e)
        {
            // Generating Matrices
            rowB = Convert.ToInt32(Row11_Copy.Text);
            columnB = Convert.ToInt32(Column11_Copy.Text);
            CurrentRowB = DataGrid1.RowCount;
            CurrentColumnB = DataGrid1.ColumnCount;
            if (rowB > 0 && columnB > 0)
            {

                DataGrid1.RowCount = rowB;
                DataGrid1.AllowUserToResizeRows = true;
                CurrentRowB = DataGrid1.RowCount;
                DataGrid1.AutoResizeRows();
                DataGrid1.AllowUserToAddRows = false;

                DataGrid1.ColumnCount = columnB;
                DataGrid1.AllowUserToResizeColumns = true;
                CurrentColumnB = DataGrid1.ColumnCount;
                DataGrid1.AutoResizeColumns();
            }
            else
            {
                MessageBox.Show("Please Give A Valid Input in Rows And Columns.");
            }
                    
        }
        private void Button_ClickB(object sender, RoutedEventArgs e)
        {
            DataGrid1.ColumnCount = CurrentColumnB + 1;
            Column11_Copy.Text = Convert.ToString(CurrentColumnB + 1);
            CurrentColumnB = DataGrid1.ColumnCount;
            DataGrid1.AutoResizeColumns();
        }
        private void Button_Click_1B(object sender, RoutedEventArgs e)
        {
            DataGrid1.RowCount = CurrentRowB + 1;
            Row11_Copy.Text = Convert.ToString(CurrentRowB + 1);
            CurrentRowB = DataGrid1.RowCount;
            DataGrid1.AutoResizeRows();
        }
        private void Button_Click_2B(object sender, RoutedEventArgs e)
        {
            if (CurrentRowB > 1)
            {
                DataGrid1.RowCount = CurrentRowB - 1;
                Row11_Copy.Text = Convert.ToString(CurrentRowB - 1);
                CurrentRowB = DataGrid1.RowCount;
                DataGrid1.AutoResizeRows();
            }
            else
                MessageBox.Show("Invalid Selection.");
        }
        private void Button_Click_3B(object sender, RoutedEventArgs e)
        {
            if (CurrentColumnB > 1)
            {
                DataGrid1.ColumnCount = CurrentColumnB - 1;
                Column11_Copy.Text = Convert.ToString(CurrentColumnB - 1);
                CurrentColumnB = DataGrid1.ColumnCount;
                DataGrid1.AutoResizeColumns();
            }
            else
                MessageBox.Show("Invalid Selection.");
        }

        /*************************************************************
         *                     MATRIX OPERATIONS
         *************************************************************/

        public void Sum(DataGridView A, DataGridView B, DataGridView Ans)
        {
            if (Convert.ToInt16(Column11.Text) == Convert.ToInt16(Column11_Copy.Text) && Convert.ToInt16(Row11.Text) == Convert.ToInt16(Row11_Copy.Text))
            {
                Ans.RowCount = A.RowCount;
                Ans.ColumnCount = B.RowCount;
                Ans.AutoResizeColumns();
                Ans.AutoResizeRows();
                for (int i = 0; i < A.RowCount; i++)
                {
                    for (int j = 0; j < B.ColumnCount; j++)
                    {
                        decimal temp;
                        temp = (Convert.ToDecimal(A.Rows[i].Cells[j].Value.ToString()) + Convert.ToDecimal(B.Rows[i].Cells[j].Value.ToString()));

                        Ans.Rows[i].Cells[j].Value = temp;
                    }
                }
            }

            else
            {
                MessageBox.Show("Matrix Order Must Be Same For Both Matrices.");
            }
        }                    //Function used to add Matrices.
        public void Minus(DataGridView A, DataGridView B, DataGridView Ans)
        {
            if (Convert.ToInt16(Column11.Text) == Convert.ToInt16(Column11_Copy.Text) && Convert.ToInt16(Row11.Text) == Convert.ToInt16(Row11_Copy.Text))
            {
                Ans.RowCount = A.RowCount;
                Ans.ColumnCount = B.RowCount;
                Ans.AutoResizeColumns();
                Ans.AutoResizeRows();
                for (int i = 0; i < A.RowCount; i++)
                {
                    for (int j = 0; j < B.ColumnCount; j++)
                    {
                        decimal temp;
                        temp = (Convert.ToDecimal(A.Rows[i].Cells[j].Value.ToString()) - Convert.ToDecimal(B.Rows[i].Cells[j].Value.ToString()));

                        Ans.Rows[i].Cells[j].Value = temp;
                    }
                }
            }

            else
            {
                MessageBox.Show("Matrix Order Must Be Same For Both Matrices.");
            }
        }                  //Function used to subtract Matrices.
        public void SimpleMultiply(DataGridView A, DataGridView B, DataGridView Ans)
        {
            if (Convert.ToInt16(Column11.Text) == Convert.ToInt16(Row11_Copy.Text) )
            {
                Ans.RowCount = A.RowCount;
                Ans.ColumnCount = B.ColumnCount;
                Ans.AutoResizeColumns();
                Ans.AutoResizeRows();
                for (int i = 0; i < A.RowCount; i++)
                {
                    for (int j = 0; j < B.ColumnCount; j++)
                    {
                        decimal temp = 0;
                        for (int k = 0; k < A.ColumnCount; k++)
                        {
                            temp += (Convert.ToDecimal(A.Rows[i].Cells[k].Value) * Convert.ToDecimal(B.Rows[k].Cells[j].Value));

                        }
                        Ans.Rows[i].Cells[j].Value = temp;
                    }
                }
            }

            else
            {
                MessageBox.Show("Column Of Matrix A != Row Of Matrix B");
            }
        }       //Function used o Multiply Marices.
        public  uint nextPowerOfTwo(int n) {
            uint log2 = Convert.ToUInt16(Math.Ceiling(Math.Log(n)/Math.Log(2)));
        return Convert.ToUInt16(Math.Pow(2,log2));
        }                                               //Function used to find next Power of 2 used with Strassen Algorithm
        private static T[,] Make2DArray<T>(T[] input, int height, int width)
        {
            T[,] output = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            }
            return output;
        }                // Function Used to onvert 1d array to 2d array.
        private void strassenR(DataGridView A , DataGridView B, DataGridView C) {

            int TAM = A.RowCount;    
            //Set LEAF_SIZE to 1 if you want to the pure strassen algorithm
            // otherwise, the ikj-algorithm will be applied when the split
            // matrices are as small as LEAF_SIZE x LEAF_SIZE
           
             if(TAM <= leafsize){
                SimpleMultiply(A,B,C);
                 return;
             }
             else
             {
                 int NewTam = TAM / 2;

                 DataGridView a11 = new DataGridView();
                 a11.RowCount = NewTam;
                 a11.ColumnCount = NewTam;
                 DataGridView a12 = new DataGridView();
                 a12.RowCount = NewTam;
                 a12.ColumnCount = NewTam;
                 DataGridView a21 = new DataGridView();
                 a21.RowCount = NewTam;
                 a21.ColumnCount = NewTam;
                 DataGridView a22 = new DataGridView();
                 a22.RowCount = NewTam;
                 a22.ColumnCount = NewTam;

                 DataGridView b11 = new DataGridView();
                 b11.RowCount = NewTam;
                 b11.ColumnCount = NewTam;
                 DataGridView b12 = new DataGridView();
                 b12.RowCount = NewTam;
                 b12.ColumnCount = NewTam;
                 DataGridView b21 = new DataGridView();
                 b21.RowCount = NewTam;
                 b21.ColumnCount = NewTam;
                 DataGridView b22 = new DataGridView();
                 b22.RowCount = NewTam;
                 b22.ColumnCount = NewTam;

                 DataGridView c11 = new DataGridView();
                 c11.RowCount = NewTam;
                 c11.ColumnCount = NewTam;
                 DataGridView c12 = new DataGridView();
                 c12.RowCount = NewTam;
                 c12.ColumnCount = NewTam;
                 DataGridView c21 = new DataGridView();
                 c21.RowCount = NewTam;
                 c21.ColumnCount = NewTam;
                 DataGridView c22 = new DataGridView();
                 c22.RowCount = NewTam;
                 c22.ColumnCount = NewTam;


                 DataGridView p1 = new DataGridView();
                 p1.RowCount = NewTam;
                 p1.ColumnCount = NewTam;
                 DataGridView p2 = new DataGridView();
                 p2.RowCount = NewTam;
                 p2.ColumnCount = NewTam;
                 DataGridView p3 = new DataGridView();
                 p3.RowCount = NewTam;
                 p3.ColumnCount = NewTam;
                 DataGridView p4 = new DataGridView();
                 p4.RowCount = NewTam;
                 p4.ColumnCount = NewTam;
                 DataGridView p5 = new DataGridView();
                 p5.RowCount = NewTam;
                 p5.ColumnCount = NewTam;
                 DataGridView p6 = new DataGridView();
                 p6.RowCount = NewTam;
                 p6.ColumnCount = NewTam;
                 DataGridView p7 = new DataGridView();
                 p7.RowCount = NewTam;
                 p7.ColumnCount = NewTam;

                 DataGridView aResult = new DataGridView();
                 aResult.RowCount = NewTam;
                 aResult.ColumnCount = NewTam;
                 DataGridView bResult = new DataGridView();
                 bResult.RowCount = NewTam;
                 bResult.ColumnCount = NewTam;

                 //dividing the matrices in 4 sub-matrices:
                 for (int i = 0; i < NewTam; i++)
                 {
                     for (int j = 0; j < NewTam; j++)
                     {
                         a11.Rows[i].Cells[j].Value = Convert.ToInt16(A.Rows[i].Cells[j].Value);
                         a12.Rows[i].Cells[j].Value = Convert.ToInt16(A.Rows[i].Cells[j+NewTam].Value);
                         a21.Rows[i].Cells[j].Value = Convert.ToInt16(A.Rows[i+NewTam].Cells[j].Value);
                         a22.Rows[i].Cells[j].Value = Convert.ToInt16(A.Rows[i+NewTam].Cells[j+NewTam].Value);

                         b11.Rows[i].Cells[j].Value = Convert.ToInt16(B.Rows[i].Cells[j].Value);
                         b12.Rows[i].Cells[j].Value = Convert.ToInt16(B.Rows[i].Cells[j+NewTam].Value);
                         b21.Rows[i].Cells[j].Value = Convert.ToInt16(B.Rows[i+NewTam].Cells[j].Value);
                         b22.Rows[i].Cells[j].Value = Convert.ToInt16(B.Rows[i+ NewTam].Cells[j + NewTam].Value);                   
                     }
                 }
                 ////////////////////////////////////////////

                 // Calculating p1 to p7:

                 Sum(a11, a22, aResult); // a11 + a22
                 Sum(b11, b22, bResult); // b11 + b22
                 strassenR(aResult, bResult, p1); // p1 = (a11+a22) * (b11+b22)

                 Sum(a21, a22, aResult); // a21 + a22
                 strassenR(aResult, b11, p2); // p2 = (a21+a22) * (b11)

                 Minus(b12, b22, bResult); // b12 - b22
                 strassenR(a11, bResult, p3); // p3 = (a11) * (b12 - b22)

                 Minus(b21, b11, bResult); // b21 - b11
                 strassenR(a22, bResult, p4); // p4 = (a22) * (b21 - b11)

                 Sum(a11, a12, aResult); // a11 + a12
                 strassenR(aResult, b22, p5); // p5 = (a11+a12) * (b22)   

                 Minus(a21, a11, aResult); // a21 - a11
                 Sum(b11, b12, bResult); // b11 + b12
                 strassenR(aResult, bResult, p6); // p6 = (a21-a11) * (b11+b12)

                 Minus(a12, a22, aResult); // a12 - a22
                 Sum(b21, b22, bResult); // b21 + b22
                 strassenR(aResult, bResult, p7); // p7 = (a12-a22) * (b21+b22)

                 // calculating c21, c21, c11 e c22:

                 Sum(p3, p5, c12); // c12 = p3 + p5
                 Sum(p2, p4, c21); // c21 = p2 + p4

                 Sum(p1, p4, aResult); // p1 + p4
                 Sum(aResult, p7, bResult); // p1 + p4 + p7
                 Minus(bResult, p5, c11); // c11 = p1 + p4 - p5 + p7

                 Sum(p1, p3, aResult); // p1 + p3
                 Sum(aResult, p6, bResult); // p1 + p3 + p6
                 Minus(bResult, p2, c22); // c22 = p1 + p3 - p2 + p6

                 // Grouping the results obtained in a single matrix:
                 for (int i = 0; i < NewTam; i++)
                 {
                     for (int j = 0; j < NewTam; j++)
                     {
                         C.Rows[i].Cells[j].Value = Convert.ToInt16(c11.Rows[i].Cells[j].Value);
                         C.Rows[i].Cells[j+ NewTam].Value = Convert.ToInt16(c12.Rows[i].Cells[j].Value);
                         C.Rows[i+NewTam].Cells[j].Value = Convert.ToInt16(c21.Rows[i].Cells[j].Value);
                         C.Rows[i+NewTam].Cells[j+NewTam].Value = Convert.ToInt16(c22.Rows[i].Cells[j].Value);
                     }
                 }
 
             }
        }              //Strassen Algorithm Recursive Function
        private void strassen(DataGridView A, DataGridView B, DataGridView C) {

            uint m = nextPowerOfTwo(A.RowCount*A.RowCount);

            C.RowCount = A.RowCount;
            C.ColumnCount = B.RowCount;
            C.AutoResizeColumns();
            C.AutoResizeRows();



            DataGridView APrep = new DataGridView();
            APrep.RowCount = Convert.ToInt16(m);
            APrep.ColumnCount = Convert.ToInt16(m);
            DataGridView BPrep = new DataGridView();
            BPrep.RowCount = Convert.ToInt16(m);
            BPrep.ColumnCount = Convert.ToInt16(m);
            DataGridView CPrep = new DataGridView();
            CPrep.RowCount = Convert.ToInt16(m);
            CPrep.ColumnCount = Convert.ToInt16(m);

            for (int i = 0; i < A.RowCount; i++)
            {
                for (int j = 0; j < A.RowCount; j++)
                {
                    APrep.Rows[i].Cells[j].Value = Convert.ToInt16(A.Rows[i].Cells[j].Value);
                    BPrep.Rows[i].Cells[j].Value = Convert.ToInt16(B.Rows[i].Cells[j].Value);
                   
                }
            }

            strassenR(APrep,BPrep,CPrep);

            for (int i = 0; i < A.RowCount; i++)
            {
                for (int j = 0; j < A.RowCount; j++)
                {
                    C.Rows[i].Cells[j].Value = Convert.ToInt16(CPrep.Rows[i].Cells[j].Value);
                }
            }

        
        }               //Srassen Algorithm to Multiply Larger Matrices
        public void Bareiss_Algorithm(DataGridView A, DataGridView Ans)                      //Bareiss Algorithm to find Determinant 
        {
            if (A.RowCount == A.ColumnCount)
            {
                Ans.RowCount =1;
                Ans.ColumnCount =1;
                Ans.AutoResizeColumns();
                Ans.AutoResizeRows();
                DataGridView Data = new DataGridView();
                Data.RowCount = A.RowCount;
                Data.ColumnCount = A.RowCount;
                for (int i = 0; i < A.RowCount; i++)
                {
                    for (int j = 0; j < A.RowCount; j++)
                    {
                        Data.Rows[i].Cells[j].Value = Convert.ToDouble(A.Rows[i].Cells[j].Value);
                    }
                }

                for (int i = 0; i < CurrentRow - 1; i++)
                {
                    for (int j = i + 1; j < CurrentRow; j++)
                    {
                        for (int k = i + 1; k < CurrentRow; k++)
                        {

                            double temp1 = Convert.ToDouble(Data.Rows[j].Cells[k].Value);
                            double temp2 = Convert.ToDouble(Data.Rows[i].Cells[i].Value);
                            double temp3 = Convert.ToDouble(Data.Rows[j].Cells[i].Value);
                            double temp4 = Convert.ToDouble(Data.Rows[i].Cells[k].Value);
                            Data.Rows[j].Cells[k].Value = ((temp1 * temp2) - (temp3 * temp4));
                            if (i != 0) { Data.Rows[j].Cells[k].Value = Convert.ToDouble(Data.Rows[j].Cells[k].Value) / Convert.ToDouble(DataGrid.Rows[i - 1].Cells[i - 1].Value); }
                        }
                    }
                }
                Console.WriteLine(Data.Rows[CurrentRow - 1].Cells[CurrentRow - 1].Value);
                Ans.Rows[0].Cells[0].Value = Convert.ToDouble(Data.Rows[A.RowCount - 1].Cells[A.RowCount - 1].Value);
            }
            else
            {
                MessageBox.Show("Only Applicable For Square Matrix!");
            }
        }
        public void Transpose_Matrix(DataGridView A, DataGridView Ans)                      //Algorithm to find transpose of a matrix.
        {
            if (A.RowCount >= 1 && A.ColumnCount >= 1)
            {
                Ans.RowCount = A.ColumnCount;
                Ans.ColumnCount = A.RowCount;
                Ans.AutoResizeColumns();
                Ans.AutoResizeRows();
                for (int i = 0; i < A.RowCount; i++)
                {
                    for (int j = 0; j < A.ColumnCount; j++)
                    {
                        Ans.Rows[j].Cells[i].Value = A.Rows[i].Cells[j].Value;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Input A Valid Matrix!");
            }

        }
        public int get_Minor(DataGridView A, DataGridView Ans,int row1,int col1)             //Function to find Minor of a matrix
        {
            int colCount = 0;
            int rowCount = 0;
            Ans.RowCount = A.ColumnCount;
            Ans.ColumnCount = A.RowCount;
            Ans.AutoResizeColumns();
            Ans.AutoResizeRows();
            int order = A.RowCount;
            for (int i = 0; i < order; i++)
            {
                if (i != row1)
                {
                    colCount = 0;
                    for (int j = 0; j < order; j++)
                    {
                        // when j is not the element
                        if (j != col1)
                        {
                            Ans.Rows[rowCount].Cells[colCount].Value = Convert.ToDecimal(A.Rows[i].Cells[j].Value);
                            colCount++;
                        }
                    }
                    rowCount++;
                }
            }
            return 1;

        }
        public void Inverse_Matrix(DataGridView A, DataGridView Ans)                        //Function to find determinant of a matrix
        {
            if (A.RowCount == A.ColumnCount)
            {
                Ans.RowCount = A.ColumnCount;
                Ans.ColumnCount = A.RowCount;
                Ans.AutoResizeColumns();
                Ans.AutoResizeRows();
               double det = 1 / Determinant(A, A.RowCount);
                for (int i = 0; i < A.RowCount; i++)
                {
                    for (int j = 0; j < A.ColumnCount; j++)
                    {
                        Ans.Rows[i].Cells[j].Value = Convert.ToDouble(A.Rows[i].Cells[j].Value) * det;
                    }
                }
              //  Cofactor(A,Ans);
            }
            else
                MessageBox.Show("Only Valid For Square Matrix");
        }
        public double Determinant(DataGridView A, int Count)
        {


            double s = 1, det = 0;
            DataGridView b = new DataGridView();
            b.RowCount = Count;
            b.ColumnCount = Count;
            int i, j, m, n, c;
            if (Count == 1)
            { return (Convert.ToDouble(A.Rows[0].Cells[0].Value)); }
            else
            {
                det = 0;
                for (c = 0; c < Count; c++)
                {
                    m = 0;
                    n = 0;
                    for (i = 0; i < Count; i++)
                    {
                        for (j = 0; j < Count; j++)
                        {
                            b.Rows[i].Cells[j].Value = 0;
                            if (i != 0 && j != c)
                            {
                                b.Rows[m].Cells[n].Value = A.Rows[i].Cells[j].Value;
                                if (n < (Count - 2))
                                    n++;
                                else
                                {
                                    n = 0;
                                    m++;
                                }
                            }
                        }
                    }
                    det = det + s * (Convert.ToDouble(A.Rows[0].Cells[c].Value) * Determinant(b, Count - 1));
                    s = -1 * s;
                }
            }
            return (det);

        }
        public void Cofactor(DataGridView A, DataGridView Ans) {

            Ans.RowCount = A.RowCount;
            Ans.ColumnCount = A.ColumnCount;

            DataGridView b = new DataGridView();
            b.RowCount = A.RowCount;
            b.ColumnCount = A.RowCount;
            DataGridView fac = new DataGridView();
            fac.RowCount = A.RowCount;
            fac.ColumnCount = A.RowCount;

    int p, q, m, n, i, j;
    for (q = 0; q < A.RowCount; q++)
    {
        for (p = 0; p < A.RowCount; p++)
        {
            m = 0;
            n = 0;
            for (i = 0; i < A.RowCount; i++)
            {
                for (j = 0; j < A.RowCount; j++)
                {
                    if (i != q && j != p)
                    {
                        b.Rows[m].Cells[n].Value = A.Rows[i].Cells[j].Value;

                        if (n < (A.RowCount - 2))
                            n++;
                        else
                        {
                            n = 0;
                            m++;
                        }
                    }
                }
            }
            DataGridView temp = new DataGridView();
            temp.RowCount = A.RowCount-1;
            temp.ColumnCount = A.RowCount-1;
            for (int i1 = 0; i1 < A.RowCount -1; i1++)
            {
                for (int j1 = 0; j1 < A.RowCount-1; j1++)
                {
                    b.Rows[i1].Cells[j1].Value = temp.Rows[i1].Cells[j1].Value;
                }
            }

            fac.Rows[q].Cells[p].Value = Math.Pow(-1, q + p) * Determinant(temp, temp.RowCount-1);

        }

    }
    DataGridView temp3 = new DataGridView();
    Transpose_Matrix(fac, temp3);


    Double Det = 1 / Determinant(A, A.RowCount);
               for (int i2 = 0; i2 < A.RowCount; i2++)
               {
                   for (int i3 = 0; i3 < A.RowCount; i3++)
                   {
                       Ans.Rows[i2].Cells[i3].Value = Convert.ToDouble(temp3.Rows[i2].Cells[i3].Value)*Det;
                   }
               }
}
       
        
        /********************************************************************
        *               Implementation Of Operations
        **********************************************************************/
        private void Multiplication(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt16(Column11.Text) == Convert.ToInt16(Column11_Copy.Text) && Convert.ToInt16(Row11.Text) == Convert.ToInt16(Row11_Copy.Text))
                strassen(DataGrid, DataGrid1, DataGrid2);
            else
            SimpleMultiply(DataGrid, DataGrid1, DataGrid2);

            DataGrid2.AutoResizeRows();
            DataGrid2.AutoResizeColumns();
        }
        private void Subtraction(object sender, RoutedEventArgs e)
        {
            Minus(DataGrid, DataGrid1, DataGrid2);
            DataGrid2.AutoResizeRows();
            DataGrid2.AutoResizeColumns();
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            DataGrid.Columns.Clear();
            DataGrid1.Columns.Clear();
            DataGrid2.Columns.Clear();

        }
        private void Addition(object sender, RoutedEventArgs e)
        {
            Sum(DataGrid,DataGrid1,DataGrid2);
            DataGrid2.AutoResizeRows();
            DataGrid2.AutoResizeColumns();
        }
        private void Determinant_A(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGrid.RowCount <= 2)
                    Bareiss_Algorithm(DataGrid, DataGrid2);
                else
                {
                    DataGrid2.RowCount = 1;
                    DataGrid2.ColumnCount = 1;
                   DataGrid2.Rows[0].Cells[0].Value= Convert.ToDouble(Determinant(DataGrid,DataGrid.RowCount));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sorry! I can't.");
            }
            DataGrid2.AutoResizeRows();
            DataGrid2.AutoResizeColumns();
        }
        private void Determinant_B(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGrid1.RowCount <= 2)
                    Bareiss_Algorithm(DataGrid1, DataGrid2);
                else
                    Determinant(DataGrid1,DataGrid1.RowCount);
            }
            catch(Exception){
                MessageBox.Show("Sorry I can't.");
            }
            DataGrid2.AutoResizeRows();
            DataGrid2.AutoResizeColumns();
        }
        private void Transpose_A(object sender, RoutedEventArgs e)
        {
            Transpose_Matrix(DataGrid,DataGrid2);
            DataGrid2.AutoResizeRows();
            DataGrid2.AutoResizeColumns();
        }
        private void Transpose_B(object sender, RoutedEventArgs e)
        {
            Transpose_Matrix(DataGrid1,DataGrid2);
            DataGrid2.AutoResizeRows();
            DataGrid2.AutoResizeColumns();
        }
        private void Inverse_A(object sender, RoutedEventArgs e)
        {
            Inverse_Matrix(DataGrid,DataGrid2);
            DataGrid2.AutoResizeRows();
            DataGrid2.AutoResizeColumns();
        }  
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Multiplication_B(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt16(Column11_Copy.Text) == Convert.ToInt16(Column11_Copy.Text) && Convert.ToInt16(Row11.Text) == Convert.ToInt16(Row11_Copy.Text))
                strassen(DataGrid1, DataGrid, DataGrid2);
            else
                SimpleMultiply(DataGrid1, DataGrid, DataGrid2);
            DataGrid2.AutoResizeRows();
            DataGrid2.AutoResizeColumns();
        }
        private void Addition_B(object sender, RoutedEventArgs e)
        {
            Sum(DataGrid1, DataGrid, DataGrid2);
            DataGrid2.AutoResizeRows();
            DataGrid2.AutoResizeColumns();
        }
        private void Subtraction_B(object sender, RoutedEventArgs e)
        {
            Minus(DataGrid1, DataGrid, DataGrid2);
            DataGrid2.AutoResizeRows();
            DataGrid2.AutoResizeColumns();
        }
        private void Inverse_B(object sender, RoutedEventArgs e)
        {
            Inverse_Matrix(DataGrid1, DataGrid2);
            DataGrid2.AutoResizeRows();
            DataGrid2.AutoResizeColumns();
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
           
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();


        }
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }


        

       
    }
}
