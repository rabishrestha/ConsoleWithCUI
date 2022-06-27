namespace ConsoleWithCUI
{
    public static class Program
    {
        public static List<Family> family_list = new List<Family>();
        static FileStream fs;
        static StreamWriter sw;
        static string file_name = "D:\\message.txt";

        public static void Main()
        {
            ReadFromFile();
            DisplayOptions();
        }
        public static void DisplayOptions()
        {
            Console.WriteLine("\n\nEnter your option\n 1 : Add Data\n " +
                "2 : List Data\n 3 : List Tabular Data \n 4 : Save Data \n 0 : Quit");
            int c = 2;
            try
            {
                c = Convert.ToInt32(Console.ReadLine());
                switch (c){
                    case 1:
                        AddData();
                        break;
                    case 2:
                        DisplayData();
                        break;
                    case 3:
                        DisplayTabularData();
                        break;
                    case 4:
                        SaveData();
                        break;
                    default:
                        break;
                }                
            }
            catch
            {
                Console.WriteLine("Please enter numbers between 1 to 3 only");               
            }
        }

        public static void AddData()
        {
            Console.Clear();
            try
            {
                int id = family_list.Count + 1;
                Console.Write("Enter full name (string) :\t");
                string full_name = Console.ReadLine();
                Console.Write("Enter gender (string => M or F) :\t");
                string gender = Console.ReadLine();
                if (gender == "M" || gender == "m") gender = "Male";
                else if (gender == "F" || gender == "f") gender = "Female";
                else gender = "Male";
                Console.Write("Enter age (int) :\t");
                int age = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Relation (int => 0: Father, 1: Mother, 2: Brother, " +
                    "3: Sister, 4: None) :\t");
                Relation relation = (Relation)Convert.ToInt32(Console.ReadLine());
                family_list.Add(new Family(id, full_name, gender, age, relation));
                Console.Clear();
                Console.WriteLine($"\nData entered successfully\nTotal no of records : " +
                    $"{family_list.Count}");
            }
            catch
            {
                Console.WriteLine("Please enter appropriate data");
            }
            DisplayOptions();
        }
        public static void DisplayData()
        {
            Console.Clear();
            if (family_list.Count > 0)
            {
                Console.WriteLine("Family Data");
                foreach (var xx in family_list)
                {
                    Console.WriteLine($"\t{xx}");
                }
            }
            else
            {
                Console.WriteLine("No data to display.\nPlease enter some data first.");
            }
            DisplayOptions();
        }
        public static void DisplayTabularData()
        {
            Console.Clear();
            if (family_list.Count > 0)
            {
                Console.WriteLine("Family Data");
                Console.WriteLine("\tID\tFullName\t\tAge\tGender\tRelation");
                foreach (var xx in family_list)
                {
                    Console.WriteLine($"\t{xx.id}\t{xx.full_name}\t\t\t{xx.age}" +
                        $"\t{xx.gender}\t{xx.relation}");
                }
                Console.WriteLine(("").PadRight(56, '-'));
            }
            else
            {
                Console.WriteLine("No data to display.\nPlease enter some data first.");
            }
            DisplayOptions();
        }
        public static void SaveData()
        {
            File.WriteAllText(file_name, string.Empty); //Clears file
            if (family_list.Count > 0)
            {
                Console.WriteLine("Family Data");
                foreach (var xx in family_list)
                {
                    var s = xx.id + ", " + xx.full_name + ", " + 
                        xx.gender + ", " + xx.age + ", " + 
                        (int)xx.relation;
                    WriteToFile(s);
                }
            }
            else
            {
                Console.WriteLine("No data to display.\nPlease enter some data first.");
            }
            DisplayOptions();
        }
        public static void WriteToFile(string s)
        {
            fs = new FileStream(file_name,
            FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fs);
            sw.WriteLine(s); sw.Flush(); sw.Close();
            fs.Close();
        }
        public static void ReadFromFile()
        {
            if (File.Exists(file_name))
            {
                string[] lines = File.ReadAllLines(file_name);
                foreach (string line in lines)
                {
                    var ss = line.Split(',');
                    if (ss.Length == 5)
                    {
                        var f = new Family(Convert.ToInt32(ss[0]), ss[1], ss[2], 
                            Convert.ToInt32(ss[3]), (Relation)Convert.ToInt32(ss[4]));
                        family_list.Add(f);
                    }                    
                }   
            }
        }
    }
}
