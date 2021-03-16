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
        ConsoleKey SomeKey;

        string Spacer = "   ";
        string Marker = "->";
        int IndexMarker = 0;

        string[] Names = { "№", "ID", "Name" };
        List<Process> ProceList = new List<Process>();

        int Min = 0, Max = 0, Limit = 0, Step = 19, Pages = 0, CurrentPage = 0;
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
                MyFunc.CheckLimitataAream(ref IndexMarker, 0, Limit - 1);
                return;
            }
            if (SomeKey == ConsoleKey.UpArrow)
            {
                --IndexMarker;
                MyFunc.CheckLimitataAream(ref IndexMarker, 0, Limit);
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
                ProceList[IndexMarker].Kill();
            }
        }
        public void Show()
        {
            Console.WriteLine($"{Spacer, 2}{Names[0],4} {Names[1],7} {Names[2]}");

            for (int i = Min; i < Max; ++i)
            {
                if(i == IndexMarker)
                {
                    Console.WriteLine($"{Marker,2}{i,4} {ProceList[i].Id,7} {ProceList[i].ProcessName}");
                }
                else
                {
                    Console.WriteLine($"{Spacer,2}{i, 4} {ProceList[i].Id,7} {ProceList[i].ProcessName}");
                }                
            }

            Console.WriteLine();
            Console.WriteLine($"Total Pocesses Found: {Limit} | Page: {CurrentPage} Of {Pages}");
            Console.WriteLine();
            Console.WriteLine("To Exit Porgram Press Escape");
            Console.WriteLine("Press PageUp And PageDown To Scroll The List ");
        }
    }
}
