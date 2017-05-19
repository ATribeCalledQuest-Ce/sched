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
                string path = String.Format(@"Schedules\{0}.txt", userprof);
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
                Console.WriteLine("Where do you want your sched to be stored? \nIf you want to store it in the default location (sched\\Profiles\\YourProfileName.txt) press D. \nDefault is recommended, as it will work without further configuration of files etc");
                string UserPath = Console.ReadLine();
                string path = CreateProf(UserProfName, UserPath);
                wProf(UserProfName, path, @"Profiles\Profs.txt");
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
                    using (var sr = new StreamReader(@"Profiles\Profs.txt"))
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
            string CurrentPeriod = null;
			if (day == "Monday" || day == "Friday")
            {	
				TimeSpan As = new TimeSpan(0, 8, 0, 0);
				TimeSpan Ae = new TimeSpan(0, 8, 50, 0);
				TimeSpan Bs = new TimeSpan(0, 8, 55, 0);
				TimeSpan Be = new TimeSpan(0, 9, 45, 0);
				TimeSpan Cs = new TimeSpan(0, 9, 55, 0);
				TimeSpan Ce = new TimeSpan(0, 10, 45, 0);
				TimeSpan Ds = new TimeSpan(0, 10, 50, 0);
				TimeSpan De = new TimeSpan(0, 11, 40, 0);
				TimeSpan Ls = new TimeSpan(0, 11, 40, 0);
				TimeSpan Le = new TimeSpan(0, 12, 55, 0);
				TimeSpan Es = new TimeSpan(0, 12, 55, 0);
				TimeSpan Ee = new TimeSpan(0, 13, 45, 0);
				TimeSpan Fs = new TimeSpan(0, 13, 50, 0);
				TimeSpan Fe = new TimeSpan(0, 14, 40, 0);
				TimeSpan Gs = new TimeSpan(0, 14, 40, 0);
				TimeSpan Ge = new TimeSpan(0, 15, 35, 0);
				if (time > As && time < Ae)
				{
					CurrentPeriod = Pers[0];
				}
				if (time > Bs && time < Be)
				{
					CurrentPeriod = Pers[1];
				}
				if (time > Cs && time < Ce)
				{
					CurrentPeriod = Pers[2];
				}
				if (time > Ds && time < De)
				{
					CurrentPeriod = Pers[3];
				}
				if (time > Es && time < Ee)
				{
					CurrentPeriod = Pers[4];
				}
				if (time > Fs && time < Fe)
				{
					CurrentPeriod = Pers[5];
				}
				if (time > Gs && time < Ge)
				{
					CurrentPeriod = Pers[6];
				}
				if (time > Ls && time < Le)
				{
					CurrentPeriod = "Lunch";
				}
            }
            if (day == "Tuesday")
            {
				TimeSpan As = new TimeSpan(0, 8, 0, 0);
				TimeSpan Ae = new TimeSpan(0, 8, 45, 0);
				TimeSpan Bs = new TimeSpan(0, 8, 50, 0);
				TimeSpan Be = new TimeSpan(0, 9, 35, 0);
				TimeSpan Cs = new TimeSpan(0, 9, 45, 0);
				TimeSpan Ce = new TimeSpan(0, 10, 30, 0);
				TimeSpan Ds = new TimeSpan(0, 10, 35, 0);
				TimeSpan De = new TimeSpan(0, 11, 20, 0);
				TimeSpan Es = new TimeSpan(0, 13, 10, 0);
				TimeSpan Ee = new TimeSpan(0, 13, 55, 0);
				TimeSpan Fs = new TimeSpan(0, 14, 0, 0);
				TimeSpan Fe = new TimeSpan(0, 14, 45, 0);
				TimeSpan Gs = new TimeSpan(0, 14, 50, 0);
				TimeSpan Ge = new TimeSpan(0, 15, 35, 0);
				//Lunch
				TimeSpan Ls = new TimeSpan(0, 12, 10, 0);
				TimeSpan Le = new TimeSpan(0, 13, 10, 0);
				//Seminar
				TimeSpan SemS = new TimeSpan(0, 11, 25, 0);
				TimeSpan SemE = new TimeSpan(0, 12, 10, 0);
				if (time > SemS && time < SemE)
				{
					CurrentPeriod = "Seminar";
				}
				if (time > As && time < Ae)
				{
					CurrentPeriod = Pers[0];
				}
				if (time > Bs && time < Be)
				{
					CurrentPeriod = Pers[1];
				}
				if (time > Cs && time < Ce)
				{
					CurrentPeriod = Pers[2];
				}
				if (time > Ds && time < De)
				{
					CurrentPeriod = Pers[3];
				}
				if (time > Es && time < Ee)
				{
					CurrentPeriod = Pers[4];
				}
				if (time > Fs && time < Fe)
				{
					CurrentPeriod = Pers[5];
				}
				if (time > Gs && time < Ge)
				{
					CurrentPeriod = Pers[6];
				}
				if (time > Ls && time < Le)
				{
					CurrentPeriod = "Lunch";
				}
            }
            if (day == "Wendsday")
			{
				TimeSpan As = new TimeSpan(0, 8, 0, 0);
				TimeSpan Ae = new TimeSpan(0, 9, 40, 0);
				TimeSpan Cs = new TimeSpan(0, 9, 55, 0);
				TimeSpan Ce = new TimeSpan(0, 10, 45, 0);
				TimeSpan Es = new TimeSpan(0, 12, 55, 0);
				TimeSpan Ee = new TimeSpan(0, 13, 45, 0);
				//Lunch
				TimeSpan Ls = new TimeSpan(0, 11, 40, 0);
				TimeSpan Le = new TimeSpan(0, 12, 55, 0);
				//Assembly
				TimeSpan AssS = new TimeSpan(0, 9, 45, 0);
				TimeSpan AssE = new TimeSpan(0, 10, 25, 0);
				if (time > As && time < Ae)
				{
					CurrentPeriod = Pers[0];
				}
				if (time > Cs && time < Ce)
				{
					CurrentPeriod = Pers[2];
				}
				if (time > Es && time < Ee)
				{
					CurrentPeriod = Pers[4];
				}
				if (time > Ls && time < Le)
				{
					CurrentPeriod = "Lunch";
				}
            }
            if (day == "Thursday")
            {
				TimeSpan Bs = new TimeSpan(0, 8, 0, 0);
				TimeSpan Be = new TimeSpan(0, 9, 40, 0);
				TimeSpan Ds = new TimeSpan(0, 10, 50, 0);
				TimeSpan De = new TimeSpan(0, 11, 40, 0);
				TimeSpan Fs = new TimeSpan(0, 12, 30, 0);
				TimeSpan Fe = new TimeSpan(0, 14, 15, 0);
				TimeSpan Gs = new TimeSpan(0, 14, 25, 0);
				TimeSpan Ge = new TimeSpan(0, 16, 05, 0);
				//Lunch
				TimeSpan Ls = new TimeSpan(0, 11, 35, 0);
				TimeSpan Le = new TimeSpan(0, 12, 35, 0);
				if (time > Bs && time < Be)
				{
					CurrentPeriod = Pers[1];
				}
				if (time > Ds && time < De)
				{
					CurrentPeriod = Pers[3];
				}
				if (time > Fs && time < Fe)
				{
					CurrentPeriod = Pers[5];
				}
				if (time > Gs && time < Ge)
				{
					CurrentPeriod = Pers[6];
				}
				if (time > Ls && time < Le)
				{
					CurrentPeriod = "Lunch";
				}
			}
            if (day == "Saturday" || day == "Sunday")
            {
                CurrentPeriod = "It is a weekend.  If it isn't a weekend and you see this messsage, something is wrong.";
            }
			//Final Return
            return CurrentPeriod;
        }
		/*
        #region daytests
        static string monTests(string[] Pers, TimeSpan time)
        {
            string CurrentPeriod = null;
            TimeSpan As = new TimeSpan(0, 8, 0, 0);
            TimeSpan Ae = new TimeSpan(0, 8, 50, 0);
            TimeSpan Bs = new TimeSpan(0, 8, 55, 0);
            TimeSpan Be = new TimeSpan(0, 9, 45, 0);
            TimeSpan Cs = new TimeSpan(0, 9, 55, 0);
            TimeSpan Ce = new TimeSpan(0, 10, 45, 0);
            TimeSpan Ds = new TimeSpan(0, 10, 50, 0);
            TimeSpan De = new TimeSpan(0, 11, 40, 0);
            TimeSpan Ls = new TimeSpan(0, 11, 40, 0);
            TimeSpan Le = new TimeSpan(0, 12, 55, 0);
            TimeSpan Es = new TimeSpan(0, 12, 55, 0);
            TimeSpan Ee = new TimeSpan(0, 13, 45, 0);
            TimeSpan Fs = new TimeSpan(0, 13, 50, 0);
            TimeSpan Fe = new TimeSpan(0, 14, 40, 0);
            TimeSpan Gs = new TimeSpan(0, 14, 40, 0);
            TimeSpan Ge = new TimeSpan(0, 15, 35, 0);
              if (time > As && time < Ae)
            {
                CurrentPeriod = Pers[0];
            }
            if (time > Bs && time < Be)
            {
                CurrentPeriod = Pers[1];
            }
            if (time > Cs && time < Ce)
            {
                CurrentPeriod = Pers[2];
            }
            if (time > Ds && time < De)
            {
                CurrentPeriod = Pers[3];
            }
            if (time > Es && time < Ee)
            {
                CurrentPeriod = Pers[4];
            }
            if (time > Fs && time < Fe)
            {
                CurrentPeriod = Pers[5];
            }
            if (time > Gs && time < Ge)
            {
                CurrentPeriod = Pers[6];
            }
            if (time > Ls && time < Le)
            {
                CurrentPeriod = "Lunch";
            }
            return CurrentPeriod;
        }
		
        static string TueTests(string[] Pers, TimeSpan time)
        {

            string CurrentPeriod = null;
            TimeSpan As = new TimeSpan(0, 8, 0, 0);
            TimeSpan Ae = new TimeSpan(0, 8, 45, 0);
            TimeSpan Bs = new TimeSpan(0, 8, 50, 0);
            TimeSpan Be = new TimeSpan(0, 9, 35, 0);
            TimeSpan Cs = new TimeSpan(0, 9, 45, 0);
            TimeSpan Ce = new TimeSpan(0, 10, 30, 0);
            TimeSpan Ds = new TimeSpan(0, 10, 35, 0);
            TimeSpan De = new TimeSpan(0, 11, 20, 0);
            TimeSpan Es = new TimeSpan(0, 13, 10, 0);
            TimeSpan Ee = new TimeSpan(0, 13, 55, 0);
            TimeSpan Fs = new TimeSpan(0, 14, 0, 0);
            TimeSpan Fe = new TimeSpan(0, 14, 45, 0);
            TimeSpan Gs = new TimeSpan(0, 14, 50, 0);
            TimeSpan Ge = new TimeSpan(0, 15, 35, 0);
            //Lunch
            TimeSpan Ls = new TimeSpan(0, 12, 10, 0);
            TimeSpan Le = new TimeSpan(0, 13, 10, 0);
            //Seminar
            TimeSpan SemS = new TimeSpan(0, 11, 25, 0);
            TimeSpan SemE = new TimeSpan(0, 12, 10, 0);
            if (time > SemS && time < SemE)
            {
                CurrentPeriod = "Seminar";
            }
            if (time > As && time < Ae)
            {
                CurrentPeriod = Pers[0];
            }
            if (time > Bs && time < Be)
            {
                CurrentPeriod = Pers[1];
            }
            if (time > Cs && time < Ce)
            {
                CurrentPeriod = Pers[2];
            }
            if (time > Ds && time < De)
            {
                CurrentPeriod = Pers[3];
            }
            if (time > Es && time < Ee)
            {
                CurrentPeriod = Pers[4];
            }
            if (time > Fs && time < Fe)
            {
                CurrentPeriod = Pers[5];
            }
            if (time > Gs && time < Ge)
            {
                CurrentPeriod = Pers[6];
            }
            if (time > Ls && time < Le)
            {
                CurrentPeriod = "Lunch";
            }
            return CurrentPeriod;
        }

        static string WedTests(string[] Pers, TimeSpan time)
        {
            string CurrentPeriod = null;
            TimeSpan As = new TimeSpan(0, 8, 0, 0);
            TimeSpan Ae = new TimeSpan(0, 9, 40, 0);
            TimeSpan Cs = new TimeSpan(0, 9, 55, 0);
            TimeSpan Ce = new TimeSpan(0, 10, 45, 0);
            TimeSpan Es = new TimeSpan(0, 12, 55, 0);
            TimeSpan Ee = new TimeSpan(0, 13, 45, 0);
            //Lunch
            TimeSpan Ls = new TimeSpan(0, 11, 40, 0);
            TimeSpan Le = new TimeSpan(0, 12, 55, 0);
            //Assembly
            TimeSpan AssS = new TimeSpan(0, 9, 45, 0);
            TimeSpan AssE = new TimeSpan(0, 10, 25, 0);
            if (time > As && time < Ae)
            {
                CurrentPeriod = Pers[0];
            }
            if (time > Cs && time < Ce)
            {
                CurrentPeriod = Pers[2];
            }
            if (time > Es && time < Ee)
            {
                CurrentPeriod = Pers[4];
            }
            if (time > Ls && time < Le)
            {
                CurrentPeriod = "Lunch";
            }
            return CurrentPeriod;

        }

        static string ThurTests(string[] Pers, TimeSpan time)
        {
            string CurrentPeriod = null;
            TimeSpan Bs = new TimeSpan(0, 8, 0, 0);
            TimeSpan Be = new TimeSpan(0, 9, 40, 0);
            TimeSpan Ds = new TimeSpan(0, 10, 50, 0);
            TimeSpan De = new TimeSpan(0, 11, 40, 0);
            TimeSpan Fs = new TimeSpan(0, 12, 30, 0);
            TimeSpan Fe = new TimeSpan(0, 14, 15, 0);
            TimeSpan Gs = new TimeSpan(0, 14, 25, 0);
            TimeSpan Ge = new TimeSpan(0, 16, 05, 0);
            //Lunch
            TimeSpan Ls = new TimeSpan(0, 11, 35, 0);
            TimeSpan Le = new TimeSpan(0, 12, 35, 0);
            if (time > Bs && time < Be)
            {
                CurrentPeriod = Pers[1];
            }
            if (time > Ds && time < De)
            {
                CurrentPeriod = Pers[3];
            }
            if (time > Fs && time < Fe)
            {
                CurrentPeriod = Pers[5];
            }
            if (time > Gs && time < Ge)
            {
                CurrentPeriod = Pers[6];
            }
            if (time > Ls && time < Le)
            {
                CurrentPeriod = "Lunch";
            }
            return CurrentPeriod;

        }
        #endregion
		*/
    }
}
