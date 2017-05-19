using System;
using System.Text;
using System.IO;
using System.Linq;
namespace Sched
{
    public class Program
    {
        static Char Number2String(int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c;
        }
        static void wProf(string UserProfName, string path, string storagePath)
        {
            string profInfo = String.Format("{0}, {1}", UserProfName, path);
            File.AppendAllText(storagePath, profInfo + Environment.NewLine);
        }
		static string CreateProf(string userprof, string userpath)
        {
            int i = 0;
            string[] Periods = new string[7];
            while (i < 7)
            {
                int j = i + 1;
                char per = Number2String(j, true);
                Console.WriteLine("Please enter your {0} period class.", per);
                Periods[i] = Console.ReadLine();
                i++;
            }
            if (userpath == "D" || userpath == "d")
            {
                string path = String.Format("C:\\Sched\\{0}.txt", userprof);
                File.WriteAllLines(path, Periods);
                return path;
            }
            else
            {
                string path = String.Format(@userpath, userprof, ".txt");
                File.WriteAllLines(path, Periods);
                return path;
            }
        }
		//this is the main function
        public static void Main(string[] args)
        {
            //Writes the introduction and asks the user what they want to do.
            Console.WriteLine("[redacted for github]. \nThis program is not done \n{0}, {1}\nPress N to create a new schedule.  Press U to use an existing schedule", DateTime.Today.DayOfWeek, DateTime.Now.TimeOfDay);
            string inp = Console.ReadLine();
            TimeSpan time = DateTime.Now.TimeOfDay;
            string today = DateTime.Today.DayOfWeek.ToString();
            if (inp == "N" || inp == "n")
            {
                bool space = true;
                string UserProfName = null;
                while (space == true)
                {
                    Console.WriteLine("What do you want your schedule to be called?");
                    UserProfName = Console.ReadLine();
                    if(!UserProfName.Contains(' '))
                    {
                        space = false;
                    }
                    if(UserProfName.Contains(' '))
                    {
                        Console.WriteLine("Spaces are not allowed in profile names, all other characters are allowed.");
                    }
                }
                Console.WriteLine("Where do you want your sched to be stored (if you want to store it in the default location [C:\\sched\\YourProfileName] press D)");
                string UserPath = Console.ReadLine();
                string path = CreateProf(UserProfName, UserPath);
                wProf(UserProfName, path, @"C:\Code\Profs.txt");
                //path is directory of periods
                //Userpath is what the user defined as their name
            }
            //Start of Period Finder/Reader
            if (inp == "U" || inp == "u")
            {
                Console.WriteLine("Press P to find your schedule by path, press U to find it based on a user profile entered earlier.");
                string choice = Console.ReadLine();
                if (choice == "u" || choice == "U")
                {
                    Console.Write("Type the name of the profile you wish to use: ");
                    var keyword = Console.ReadLine();
                    using (var sr = new StreamReader(@"C:\Code\Profs.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            if (String.IsNullOrEmpty(line)) continue;
                            if (line.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) >= 0)
                            {
                                string[] splitString = line.Split(' ');
                                string[] userPeriods = (ReadPath(splitString[1]));
                                //POINT OF ENTIRE PROGRAM
								Console.WriteLine("You should be in {0}", PeriodTest(today, time, userPeriods));
                            }
                        }
                    }
                }
                if (choice == "p" || choice == "P")
                    {
                        Console.WriteLine("What is the path of the schedule you would like to use?");
                        string currentSched = Console.ReadLine();
                        string[] UserPeriods = (ReadPath(currentSched));
                        Console.WriteLine("You should be in {0}", PeriodTest(today, time, UserPeriods));
                        foreach (string period in UserPeriods)
                        {
                            Console.WriteLine(period);
                        }
                    }
            }
            Console.ReadKey();
        }
        static string[] ReadPath(string Ppath)
        {
            string[] lines = new string[7];
            int i = 0;
            foreach (string line in File.ReadLines(Ppath))
            {
                lines[i] = line;
                i++;
            }
            return lines;
        }
        public static string PeriodTest(string day, TimeSpan time, string[] Pers)
        {
            //TimeSpan Test1 = new TimeSpan(0, 8, 0, 0);
            //TimeSpan Test2 = new TimeSpan(0, 8, 0, 0);
            string CurrentPeriod = "";
            if (day == "Monday" || day == "Friday")
            {
                CurrentPeriod = monTests(Pers, time);
            }
            if (day == "Tuesday")
            {
                CurrentPeriod = TueTests(Pers, time);
            }
            if (day == "Wendsday")
            {
                CurrentPeriod = WedTests(Pers, time);
            }
            if (day == "Thursday")
            {
                CurrentPeriod = ThurTests(Pers, time);
            }
            if (day == "Saturday" || day == "Sunday")
            {
                CurrentPeriod = "It is a weekend.  If it isn't a weekend and you see this messsage, something is wrong.";
            }
            return CurrentPeriod;
        }
        #region daytests
        static string monTests(string[] Pers, TimeSpan time)
        {
            string cp = null;
            TimeSpan MonAs = new TimeSpan(0, 8, 0, 0);
            TimeSpan MonAe = new TimeSpan(0, 8, 50, 0);
            TimeSpan MonBs = new TimeSpan(0, 8, 55, 0);
            TimeSpan MonBe = new TimeSpan(0, 9, 45, 0);
            TimeSpan MonCs = new TimeSpan(0, 9, 55, 0);
            TimeSpan MonCe = new TimeSpan(0, 10, 45, 0);
            TimeSpan MonDs = new TimeSpan(0, 10, 50, 0);
            TimeSpan MonDe = new TimeSpan(0, 11, 40, 0);
            TimeSpan MonLs = new TimeSpan(0, 11, 40, 0);
            TimeSpan MonLe = new TimeSpan(0, 12, 55, 0);
            TimeSpan MonEs = new TimeSpan(0, 12, 55, 0);
            TimeSpan MonEe = new TimeSpan(0, 13, 45, 0);
            TimeSpan MonFs = new TimeSpan(0, 13, 50, 0);
            TimeSpan MonFe = new TimeSpan(0, 14, 40, 0);
            TimeSpan MonGs = new TimeSpan(0, 14, 40, 0);
            TimeSpan MonGe = new TimeSpan(0, 15, 35, 0);
              if (time > MonAs && time < MonAe)
            {
                cp = Pers[0];
            }
            if (time > MonBs && time < MonBe)
            {
                cp = Pers[1];
            }
            if (time > MonCs && time < MonCe)
            {
                cp = Pers[2];
            }
            if (time > MonDs && time < MonDe)
            {
                cp = Pers[3];
            }
            if (time > MonEs && time < MonEe)
            {
                cp = Pers[4];
            }
            if (time > MonFs && time < MonFe)
            {
                cp = Pers[5];
            }
            if (time > MonGs && time < MonGe)
            {
                cp = Pers[6];
            }
            if (time > MonLs && time < MonLe)
            {
                cp = "Lunch";
            }
            return cp;
        }

        static string TueTests(string[] Pers, TimeSpan time)
        {

            string cp = null;
            TimeSpan MonAs = new TimeSpan(0, 8, 0, 0);
            TimeSpan MonAe = new TimeSpan(0, 8, 45, 0);
            TimeSpan MonBs = new TimeSpan(0, 8, 50, 0);
            TimeSpan MonBe = new TimeSpan(0, 9, 35, 0);
            TimeSpan MonCs = new TimeSpan(0, 9, 45, 0);
            TimeSpan MonCe = new TimeSpan(0, 10, 30, 0);
            TimeSpan MonDs = new TimeSpan(0, 10, 35, 0);
            TimeSpan MonDe = new TimeSpan(0, 11, 20, 0);
            TimeSpan MonEs = new TimeSpan(0, 13, 10, 0);
            TimeSpan MonEe = new TimeSpan(0, 13, 55, 0);
            TimeSpan MonFs = new TimeSpan(0, 14, 0, 0);
            TimeSpan MonFe = new TimeSpan(0, 14, 45, 0);
            TimeSpan MonGs = new TimeSpan(0, 14, 50, 0);
            TimeSpan MonGe = new TimeSpan(0, 15, 35, 0);
            //Lunch
            TimeSpan MonLs = new TimeSpan(0, 12, 10, 0);
            TimeSpan MonLe = new TimeSpan(0, 13, 10, 0);
            //Seminar
            TimeSpan SemS = new TimeSpan(0, 11, 25, 0);
            TimeSpan SemE = new TimeSpan(0, 12, 10, 0);
            if (time > SemS && time < SemE)
            {
                cp = "Seminar";
            }
            if (time > MonAs && time < MonAe)
            {
                cp = Pers[0];
            }
            if (time > MonBs && time < MonBe)
            {
                cp = Pers[1];
            }
            if (time > MonCs && time < MonCe)
            {
                cp = Pers[2];
            }
            if (time > MonDs && time < MonDe)
            {
                cp = Pers[3];
            }
            if (time > MonEs && time < MonEe)
            {
                cp = Pers[4];
            }
            if (time > MonFs && time < MonFe)
            {
                cp = Pers[5];
            }
            if (time > MonGs && time < MonGe)
            {
                cp = Pers[6];
            }
            if (time > MonLs && time < MonLe)
            {
                cp = "Lunch";
            }
            return cp;
        }

        static string WedTests(string[] Pers, TimeSpan time)
        {
            string cp = null;
            TimeSpan MonAs = new TimeSpan(0, 8, 0, 0);
            TimeSpan MonAe = new TimeSpan(0, 9, 40, 0);
            TimeSpan MonCs = new TimeSpan(0, 9, 55, 0);
            TimeSpan MonCe = new TimeSpan(0, 10, 45, 0);
            TimeSpan MonEs = new TimeSpan(0, 12, 55, 0);
            TimeSpan MonEe = new TimeSpan(0, 13, 45, 0);
            //Lunch
            TimeSpan MonLs = new TimeSpan(0, 11, 40, 0);
            TimeSpan MonLe = new TimeSpan(0, 12, 55, 0);
            //Assembly
            TimeSpan AssS = new TimeSpan(0, 9, 45, 0);
            TimeSpan AssE = new TimeSpan(0, 10, 25, 0);
            if (time > MonAs && time < MonAe)
            {
                cp = Pers[0];
            }
            if (time > MonCs && time < MonCe)
            {
                cp = Pers[2];
            }
            if (time > MonEs && time < MonEe)
            {
                cp = Pers[4];
            }
            if (time > MonLs && time < MonLe)
            {
                cp = "Lunch";
            }
            return cp;

        }

        static string ThurTests(string[] Pers, TimeSpan time)
        {
            string cp = null;
            TimeSpan MonBs = new TimeSpan(0, 8, 0, 0);
            TimeSpan MonBe = new TimeSpan(0, 9, 40, 0);
            TimeSpan MonDs = new TimeSpan(0, 10, 50, 0);
            TimeSpan MonDe = new TimeSpan(0, 11, 40, 0);
            TimeSpan MonFs = new TimeSpan(0, 12, 30, 0);
            TimeSpan MonFe = new TimeSpan(0, 14, 15, 0);
            TimeSpan MonGs = new TimeSpan(0, 14, 25, 0);
            TimeSpan MonGe = new TimeSpan(0, 16, 05, 0);
            //Lunch
            TimeSpan MonLs = new TimeSpan(0, 11, 35, 0);
            TimeSpan MonLe = new TimeSpan(0, 12, 35, 0);
            if (time > MonBs && time < MonBe)
            {
                cp = Pers[1];
            }
            if (time > MonDs && time < MonDe)
            {
                cp = Pers[3];
            }
            if (time > MonFs && time < MonFe)
            {
                cp = Pers[5];
            }
            if (time > MonGs && time < MonGe)
            {
                cp = Pers[6];
            }
            if (time > MonLs && time < MonLe)
            {
                cp = "Lunch";
            }
            return cp;

        }
        #endregion
    }
}
