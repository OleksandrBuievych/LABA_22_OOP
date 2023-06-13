using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LABA_22_OOP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "RTF Files (*.rtf)|*.rtf";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        TextRange range = new TextRange(txtEditor.Document.ContentStart, txtEditor.Document.ContentEnd);
                        range.Load(fs, DataFormats.Rtf);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "RTF Files (*.rtf)|*.rtf";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        TextRange range = new TextRange(txtEditor.Document.ContentStart, txtEditor.Document.ContentEnd);
                        range.Save(fs, DataFormats.Rtf);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtEditor.Selection.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold))
            {
                txtEditor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                txtEditor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtEditor.Selection.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic))
            {
                txtEditor.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                txtEditor.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty) != null)
            {
                txtEditor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
            else
            {
                txtEditor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void fontComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!txtEditor.Selection.IsEmpty && txtEditor.Selection.Start.Paragraph != null)
            {
                ComboBox comboBox = (ComboBox)sender;
                ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
                string fontName = selectedItem.Content.ToString();

                TextRange selectedText = new TextRange(txtEditor.Selection.Start, txtEditor.Selection.End);
                selectedText.ApplyPropertyValue(TextElement.FontFamilyProperty, new FontFamily(fontName));
            }
        }

        private void fontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!txtEditor.Selection.IsEmpty && txtEditor.Selection.Start.Paragraph != null)
            {
                ComboBox comboBox = (ComboBox)sender;
                ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
                int fontSize = int.Parse(selectedItem.Content.ToString());

                TextRange selectedText = new TextRange(txtEditor.Selection.Start, txtEditor.Selection.End);
                selectedText.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
            }
        }

        private void AlignLeftButton_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Left);
        }

        private void AlignCenterButton_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Center);
        }

        private void AlignRightButton_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Right);
        }

        private void AlignJustifyButton_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Justify);
        }

        



    }
}
