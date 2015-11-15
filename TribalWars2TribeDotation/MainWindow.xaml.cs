using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            dotationsLoad.DataContext = "Dotations: 0";

        }

        private async void loadTribeMembersButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text documents (.txt)|*.txt";

            var result = fileDialog.ShowDialog();
            if (result != null)
            {
                using (StreamReader stream = new StreamReader(fileDialog.FileName))
                {
                    TribeMember member;
                    string temp;
                    while ((temp = await stream.ReadLineAsync()) != null)
                    {
                        member = new TribeMember();

                        member.Name = await stream.ReadLineAsync();

                        temp = await stream.ReadLineAsync();

                        temp = await stream.ReadLineAsync();
                        string[] numbers = temp.Split('\t');

                        int tempI = 0;
                        string[] pointsPart = numbers[0].Split(null);
                        Console.WriteLine(pointsPart.Length);
                        Int32.TryParse((pointsPart[0]+pointsPart[1]), out tempI);
                        member.Points = tempI;

                        Int32.TryParse(numbers[1], out tempI);
                        member.VillagesNumber = tempI;

                        Int32.TryParse(numbers[3], out tempI);
                        member.HonorPoints = tempI;

                        programData.TribeMembers.Add(member);
                    }
                }
            }

            usersLoad.Text = "Users: " + programData.TribeMembers.Count();
        }

        private void saveTribeMembersButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.FileName = "Tribe members list.xml";
            fileDialog.Filter = "Text documents (.xml)|*.xml";

            if (fileDialog.ShowDialog() != false)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<TribeMember>));
                using (TextWriter writer = new StreamWriter(fileDialog.FileName))
                {
                    serializer.Serialize(writer, programData.TribeMembers);
                    
                }
            }
        }
    }
}
