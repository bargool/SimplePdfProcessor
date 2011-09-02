using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;

namespace SimplePdfProcessor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //lstActivities.SelectedIndex=0;
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            ActivitySelected();
        }

        private void ActivitySelected()
        {
            if (true == rdMerge.IsChecked)
            {
                MergeWindow mergewin = new MergeWindow();
                mergewin.Owner = this;
                mergewin.ShowDialog();
            }
            if (true == rdDelete.IsChecked)
            {
                DeletePages delpagewin = new DeletePages();
                delpagewin.Owner = this;
                delpagewin.txtFromPage.Text = "1";
                delpagewin.txtToPage.Text = "1";
                delpagewin.ShowDialog();
            }
            if (true == rdRotate.IsChecked)
            {
                RotatePagesWindow rotatewin = new RotatePagesWindow();
                rotatewin.Owner = this;
                rotatewin.ShowDialog();
            }
            if (true == rdRectangle.IsChecked)
            {
                RectangleWindow rotatewin = new RectangleWindow();
                rotatewin.Owner = this;
                rotatewin.ShowDialog();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lstActivities_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ActivitySelected();
        }
    }
}
