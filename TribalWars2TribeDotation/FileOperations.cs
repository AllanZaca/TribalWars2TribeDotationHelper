using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace TribalWars2TribeDotation
{
    public static class FileOperations
    {
        public async static Task LoadTribeMembers(ProgramData programData, TextBlock usersLoad, string path)
        {
            
            using (StreamReader stream = new StreamReader(path))
            {
                TribeMember member;
                string temp = await stream.ReadLineAsync();
                if (temp == "Dotation list")
                {
                    while ((temp = await stream.ReadLineAsync()) != null)
                    {
                        member = new TribeMember();

                        member.PlayerName = await stream.ReadLineAsync();
                        member.PlayerStatOld.PlayerName = member.PlayerName;

                        temp = await stream.ReadLineAsync();

                        temp = await stream.ReadLineAsync();
                        string[] numbers = temp.Split('\t');

                        int tempI = 0;
                        string[] pointsPart = numbers[0].Split(null);

                        Int32.TryParse((pointsPart[0] + pointsPart[1]), out tempI);
                        member.PlayerStatOld.Points = tempI;

                        Int32.TryParse(numbers[1], out tempI);
                        member.PlayerStatOld.VillagesNumber = tempI;

                        Int32.TryParse(numbers[3], out tempI);
                        member.PlayerStatOld.HonorPoints = tempI;

                        programData.TribeMembers.Add(member);
                    }
                }
                else
                {
                    MessageBox.Show("Invelid file");
                }
            }
            

            usersLoad.Text = "Users: " + programData.TribeMembers.Count();
        }

        public static void SaveTribeMembers(ProgramData programData, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TribeMember>));
            using (TextWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, programData.TribeMembers);

            }
        }

        public static async Task LoadDotation(ProgramData programData, string path)
        {
            programData.Dotations = new List<Dotation>();
            
            using (StreamReader stream = new StreamReader(path))
            {
                string[] line;

                string temp = await stream.ReadLineAsync();
                if (temp == "Dotation list")
                {
                    while ((temp = await stream.ReadLineAsync()) != null)
                    {

                        line = temp.Split('-');

                        string playerName = line[0];
                        playerName = playerName.Remove(playerName.Length - 1, 1);
                        int index = 0;
                        Console.WriteLine(playerName);
                        TribeMember tribeMember = new TribeMember();
                        bool founded = false;
                        for (int i = 0; i < programData.TribeMembers.Count; i++)
                        {
                            if (programData.TribeMembers[i].PlayerName == playerName)
                            {
                                index = i;
                                tribeMember = programData.TribeMembers[i];
                                founded = true;
                                break;
                            }
                        }


                        temp = await stream.ReadLineAsync();
                        line = temp.Split('+');
                        int dotationRate = 0;
                        for (int i = 0; i < line.Length; i++)
                        {
                            string newLine = Regex.Replace(line[i], "k", "000");
                            int dot;
                            Int32.TryParse(newLine, out dot);
                            dotationRate += dot;
                        }
                        tribeMember.Dotation = dotationRate;

                        while ((temp = await stream.ReadLineAsync()) != null)
                        {
                            if (temp == "")
                            {
                                break;
                            }
                        }

                        if (!founded)
                        {
                            continue;
                        }

                        programData.TribeMembers[index] = tribeMember;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid file");
                }
            }
            
        }

        public static async Task LoadTribeMembersNew(ProgramData programData, string path)
        {
            
            using (StreamReader stream = new StreamReader(path))
            {
                int index = 0;
                bool founded;
                TribeMember tribeMember = new TribeMember();
                string temp;

                while ((temp = await stream.ReadLineAsync()) != null)
                {
                    founded = false;
                    tribeMember = new TribeMember();
                    temp = await stream.ReadLineAsync();
                    for (int i = 0; i < programData.TribeMembers.Count; i++)
                    {
                        if (programData.TribeMembers[i].PlayerName == temp)
                        {
                            index = i;
                            tribeMember = programData.TribeMembers[i];
                            founded = true;
                            break;
                        }
                    }

                    tribeMember.PlayerStatNew.PlayerName = temp;

                    temp = await stream.ReadLineAsync();

                    temp = await stream.ReadLineAsync();
                    string[] numbers = temp.Split('\t');

                    int tempI = 0;
                    string[] pointsPart = numbers[0].Split(null);

                    Int32.TryParse((pointsPart[0] + pointsPart[1]), out tempI);
                    tribeMember.PlayerStatNew.Points = tempI;

                    Int32.TryParse(numbers[1], out tempI);
                    tribeMember.PlayerStatNew.VillagesNumber = tempI;

                    Int32.TryParse(numbers[3], out tempI);
                    tribeMember.PlayerStatNew.HonorPoints = tempI;

                    if (founded)
                    {
                        programData.TribeMembers[index] = tribeMember;
                    }
                    else
                    {
                        tribeMember.PlayerName = tribeMember.PlayerStatNew.PlayerName;
                        programData.AddTribeMember(tribeMember);
                    }
                }
            }
            
        }

        public static void LoadTribeMembersXml(ProgramData programData, TextBlock usersLoad, string path)
        {
            
            XmlSerializer serializer = new XmlSerializer(typeof(List<TribeMember>));

            using (StreamReader reader = new StreamReader(path))
            {
                programData.TribeMembers = (List<TribeMember>)serializer.Deserialize(reader);
            }
            
            usersLoad.Text = "Users: " + programData.TribeMembers.Count();
        }

    }
}
