using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.Windows.Forms;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace DNDApp
{
    public class DNDDatabaseHandler
    {
        public static ObservableCollection<DNDCharacter> characters {  get; private set; } = new ObservableCollection<DNDCharacter>();
        public static ObservableCollection<DNDClassInfo> classesInfo { get; private set; } = new ObservableCollection<DNDClassInfo>();

        //public static void CreateTestJSON()
        //{
        //    string basePath = AppDomain.CurrentDomain.BaseDirectory;
        //    string jsonPath = Path.Combine(basePath, "characters.json");

        //    DNDCharacter character1 = new DNDCharacter();
        //    character1.Biography.Age = "25";
        //    DNDCharacter character2 = new DNDCharacter();
        //    character2.Biography.Age = "100";

        //    ObservableCollection<DNDCharacter> cs = new ObservableCollection<DNDCharacter>();
        //    cs.Add(character1);
        //    cs.Add(character2);

        //    //JObject o = JObject.FromObject(cs);
        //    JArray o = JArray.FromObject(cs);
        //    File.WriteAllText(jsonPath, o.ToString());
        //}

        public static ObservableCollection<DNDCharacter> LoadCharactersJSON()
        {
            characters = new ObservableCollection<DNDCharacter>();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                //if (!File.Exists(openFileDialog.FileName))
                //{
                //    Console.WriteLine("JSON does not exist");
                //    return;
                //}

                string jsonContent = File.ReadAllText(openFileDialog.FileName);
                characters = JsonConvert.DeserializeObject<ObservableCollection<DNDCharacter>>(jsonContent);


                //DNDCharacter character = ProcessJSON(jsonPath);
                
            }

            if (ReferenceEquals(characters, null))
            {
                Console.WriteLine("JSON deserialization failed");
                return new ObservableCollection<DNDCharacter>();
            }

            return characters;
        }

        public static void SaveCharacters(ObservableCollection<DNDCharacter> characters)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
            {
                JArray o = JArray.FromObject(characters);
                File.WriteAllText(saveFileDialog.FileName, o.ToString());

            }
        }

        public static void LoadClassesJSON()
        {
            classesInfo = new ObservableCollection<DNDClassInfo>();

            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "JSON files (*.json)|*.json";
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    //if (!File.Exists(openFileDialog.FileName))
            //    //{
            //    //    Console.WriteLine("JSON does not exist");
            //    //    return;
            //    //}

            //    string jsonContent = File.ReadAllText(openFileDialog.FileName);
            //    characters = JsonConvert.DeserializeObject<ObservableCollection<DNDCharacter>>(jsonContent);


            //    //DNDCharacter character = ProcessJSON(jsonPath);

            //}

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string jsonPath = Path.Combine(basePath, "classes.json");

            if (!File.Exists(jsonPath))
            {
                Console.WriteLine("JSON does not exist");
                //return new ObservableCollection<DNDClassInfo>();
                return;
            }

            string jsonContent = File.ReadAllText(jsonPath);
            classesInfo = JsonConvert.DeserializeObject<ObservableCollection<DNDClassInfo>>(jsonContent);

            if (ReferenceEquals(classesInfo, null))
            {
                Console.WriteLine("JSON deserialization failed");
                //return new ObservableCollection<DNDClassInfo>();
                return;
            }

            //return classesInfo;
        }
    }
}
