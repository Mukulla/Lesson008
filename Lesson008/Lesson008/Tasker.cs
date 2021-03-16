using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyLib;

namespace Lesson008
{
    class Tasker
    {
        struct Str_Proc
        {
            public int ID;
            public string Name;
        }
        ConsoleKey SomeKey;

        string Spacer = "   ";
        string Marker = "->";
        int IndexMarker = 0;

        string[] Names = { "№", "ID", "Name" };
        List<Process> ProceList = new List<Process>();

        List<Str_Proc> SortList = new List<Str_Proc>();
        bool WasSorted = false;

        int Min = 0, Max = 0, Limit = 0, Step = 19, Pages = 0, CurrentPage = 0;

        bool SomeThingWrong = false;
        public Tasker()
        {
            Process[] PArray001 = Process.GetProcesses();
            Limit = PArray001.Length;
            Max = Step;
            Pages = Limit / Step;
           
            foreach (var Pro in PArray001)
            {
                ProceList.Add(Pro);
            }
        }
        public void HandleKeys()
        {
            SomeKey = Console.ReadKey().Key;

            if (SomeKey == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            
            if (SomeKey == ConsoleKey.PageUp)
            {
                if(Min == 0)
                {
                    return;
                }                
                
                Max = Min;
                Min -= Step;

                --CurrentPage;
                IndexMarker -= Step; 
                MyFunc.CheckLimitataAream(ref IndexMarker, 0, Limit);
                MyFunc.CheckLimitataAream(ref Min, 0, Limit);
                return;
            }
            if (SomeKey == ConsoleKey.PageDown)
            {
                if(Max == Limit)
                {
                    return;
                }
                
                Min = Max;
                Max += Step;
                
                ++CurrentPage;
                IndexMarker += Step;

                MyFunc.CheckLimitataAream(ref IndexMarker, 0, Limit);
                MyFunc.CheckLimitataAream(ref Max, 0, Limit);
                return;
            }

            if (SomeKey == ConsoleKey.DownArrow)
            {
                ++IndexMarker;
                MyFunc.CheckLimitataAream(ref IndexMarker, Min, Max - 1);
                return;
            }
            if (SomeKey == ConsoleKey.UpArrow)
            {
                --IndexMarker;
                MyFunc.CheckLimitataAream(ref IndexMarker, Min, Max - 1);
                return;
            }

            if (SomeKey == ConsoleKey.Home)
            {
                Min = 0;
                Max = Step;
                
                IndexMarker = IndexMarker - Step * CurrentPage;
                CurrentPage = 0;
                return;
            }
            if (SomeKey == ConsoleKey.End)
            {
                Min = Step * Pages;
                Max = Limit;

                IndexMarker = IndexMarker - Step * CurrentPage;
                CurrentPage = Pages;
                return;
            }

            //Delete Process
            if (SomeKey == ConsoleKey.Delete)
            {
                try
                {
                    ProceList[IndexMarker].Kill();
                    ProceList.RemoveAt(IndexMarker);
                }
                catch (Exception)
                {
                    SomeThingWrong = true;
                }
                return;
            }

            if (SomeKey == ConsoleKey.F1)
            {
                SortList.Clear();

                var SortedID = from Proc in ProceList
                               orderby Proc.Id
                               select Proc;
                Str_Proc Tempo;
                foreach (var item in SortedID)
                {
                    Tempo.ID = item.Id;
                    Tempo.Name = item.ProcessName;
                    SortList.Add(Tempo);
                }

                WasSorted = true;
                return;
            }
            if (SomeKey == ConsoleKey.F2)
            {
                SortList.Clear();

                var SortedID = from Proc in ProceList
                               orderby Proc.ProcessName
                               select Proc;
                Str_Proc Tempo;
                foreach (var item in SortedID)
                {
                    Tempo.ID = item.Id;
                    Tempo.Name = item.ProcessName;
                    SortList.Add(Tempo);
                }
                WasSorted = true;
                return;
            }

            if (SomeKey == ConsoleKey.F3)
            {
                WasSorted = false;
            }

        }
        public void Show()
        {
            Console.WriteLine($"{Spacer, 2}{Names[0],4} {Names[1],7} {Names[2]}");

            for (int i = Min; i < Max; ++i)
            {
                if(i == IndexMarker)
                {   
                    if(WasSorted)
                    {
                        Console.WriteLine($"{Marker,2}{i,4} {SortList[i].ID,7} {SortList[i].Name}");
                    }
                    else
                    {
                        Console.WriteLine($"{Marker,2}{i,4} {ProceList[i].Id,7} {ProceList[i].ProcessName}");
                    }                    
                }
                else
                {
                    if (WasSorted)
                    {
                        Console.WriteLine($"{Spacer,2}{i,4} {SortList[i].ID,7} {SortList[i].Name}");
                    }
                    else
                    {
                        Console.WriteLine($"{Spacer,2}{i,4} {ProceList[i].Id,7} {ProceList[i].ProcessName}");
                    }
                    //Console.WriteLine($"{Spacer,2}{i, 4} {ProceList[i].Id,7} {ProceList[i].ProcessName}");
                }                
            }

            Console.WriteLine();
            Console.WriteLine($"Total Pocesses Found: {Limit} | Page: {CurrentPage} Of {Pages}");
            Console.WriteLine("Press Escape To Exit Porgram");
            Console.WriteLine("Press PageUp And PageDown To Scroll The List");
            Console.WriteLine("Press Up And Down Arrow To Select Process");
            Console.WriteLine("Press Del To Delete Marked Process");
            Console.WriteLine("Press F1 To Sort By ID");
            Console.WriteLine("Press F2 To Sort By Name");
            Console.WriteLine("Press F3 To Reset Sorting");

            if(SomeThingWrong)
            {
                Console.WriteLine("Failed To Delete Process ");
                SomeThingWrong = false;
            }            
        }
    }
}
