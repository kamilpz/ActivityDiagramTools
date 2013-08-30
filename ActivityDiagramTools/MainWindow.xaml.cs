namespace ActivityDiagramTools
{
    using System;
    using System.Windows;
    using System.Linq;
    using ActivityDiagramTools.Classes;
    using ActivityDiagramTools.Classes.NodeClasses;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Win32;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string FilePath;
        private SequenceMaker sequenceMaker;

        public MainWindow()
        {
            InitializeComponent();

            GetLTLBtn.IsEnabled = false;
            SaveBtn.IsEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "xml files (*.xml)|*.xml";
            fileDialog.Title = "Select title";
            fileDialog.ShowDialog();
            FilePath = fileDialog.FileName;
            if(!String.IsNullOrEmpty(FilePath))
            {
                FileNameLabel.Text = FilePath;
                GetLTLBtn.IsEnabled = true;
            }
        }

        private void GetLTLBtn_Click(object sender, RoutedEventArgs e)
        {
            sequenceMaker = new SequenceMaker(FilePath);
            SequenceText.Text = sequenceMaker.BasedSequence;

            LTLSequenceText.Text = sequenceMaker.GetLTLSequence();
            SaveBtn.IsEnabled = true;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Choose file";
            saveFileDialog.Filter = "ltl files (*.ltl)|*.ltl";
            saveFileDialog.ShowDialog();
            string filePath = saveFileDialog.FileName;

            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.Write(sequenceMaker.GetLTLSequence());
            }
            MessageBox.Show("LTL successfully saved");
        }
    }
}
