using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml.Serialization;

namespace TribalWars2TribeDotation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProgramData programData;

        public MainWindow()
        {
            InitializeComponent();

            programData = new ProgramData();

            usersLoad.DataContext = "Users: 0";

        }

        private async void loadTribeMembersButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text documents (.txt)|*.txt";

            var result = fileDialog.ShowDialog();
            if (result == true)
            {
                await FileOperations.LoadTribeMembers(programData, usersLoad, fileDialog.FileName);
            }
        }

        private void saveTribeMembersButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.FileName = "Tribe members list.xml";
            fileDialog.Filter = "Text documents (.xml)|*.xml";

            var result = fileDialog.ShowDialog();
            if (result == true)
            {
                FileOperations.SaveTribeMembers(programData, fileDialog.FileName);
            }
        }

        private async void loadDotationButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text documents (.txt)|*.txt";

            var result = fileDialog.ShowDialog();
            if (result == true)
            {
                await FileOperations.LoadDotation(programData, fileDialog.FileName);
            }
        }

        private async void loadTribeMembersNewButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text documents (.txt)|*.txt";

            var result = fileDialog.ShowDialog();
            if (result == true)
            {
                await FileOperations.LoadTribeMembersNew(programData, fileDialog.FileName);
            }
        }

        private void loadTribeMembersXmlButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "XML file (.xml)|*.xml";

            var result = fileDialog.ShowDialog();
            if (result == true)
            {
                FileOperations.LoadTribeMembersXml(programData, usersLoad, fileDialog.FileName);
            }
        }
    }
}
