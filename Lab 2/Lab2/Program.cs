using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Lab2
{
    class Program
    {
        //Assuming processArrival is sorted in an ascending order
        //TurnaroundTime = Finish Time - Arrival time
        public static double FCFSgetAverageTurnAroundTime(int [] processArrival, int [] processBurst, int size)
        {
            int [] turnAround = new int[size];
            int [] finishtime = new int[size];
            int [] starttime = new int[size];
            finishtime[0] = processBurst[0]; // the first process finish time should equal to its burst time
            starttime[0] = processArrival[0]; // the first process start time should equal to its arrival time
            for(int i = 1; i < size; i++)
            {
                starttime[i] = finishtime[i - 1]; //When the first process finished, second one goes on
                finishtime[i] = processBurst[i] + starttime[i];  // burst time + start time will give us the finish time
            }
            int turnAroundSum = 0;
            for(int i = 0; i < size; i++)
            {
                turnAround[i] = finishtime[i] - processArrival[i];
                turnAroundSum += turnAround[i];
            }
            return turnAroundSum * 1.0000 / size;
        }

        //throughput = number of process / total CPU burst
        public static double FCFSgetThroughput(int[] processBurst, int size)
        {
            int totalburst = 0;
            for(int i = 0; i < size; i++)
            {
                totalburst += processBurst[i];
            }
            return size * 1.00000 / totalburst;
        }

        //Assume the dispatch latency (for context switch) D is 10 ms. The total dispatch latency T = the number of context swithes * D. CPU utilization is (total CPU burst)/(total CPU burst + T).
        public static double FCFSgetCPUutilization(int [] processArrival, int[] processBurst, int latency, int size)
        {
            int[] NumContextSwitches = new int[size];
            int totalContextSwitches = 0;
            int totalburst = 0;
            for (int i = 0; i < size; i++)
            {
                NumContextSwitches[i] = 2; // In FCFS schedule, all process will have two context switch, start and finish
                totalContextSwitches += NumContextSwitches[i]; //add up all the context switches
                totalburst += processBurst[i]; // calculate the total burst
            }
            return totalburst * 1.0000 / (totalburst + (latency * totalContextSwitches));
            
        }

        //Assume it is non-preemptive and processArrival and processBurst is Sorted
        public static double SJFgetAverageTurnAroundTime(int[] processArrival, int[] processBurst, int size)
        {
            int[] turnAround = new int[size];
            int[] finishtime = new int[size];
            int[] starttime = new int[size];
            finishtime[0] = processBurst[0]; // the first process finish time should equal to its burst time
            starttime[0] = processArrival[0]; // the first process start time should equal to its arrival time
            for (int i = 1; i < size; i++)
            {
                starttime[i] = finishtime[i - 1]; //When the first process finished, second one goes on
                finishtime[i] = processBurst[i] + starttime[i];  // burst time + start time will give us the finish time
            }
            int turnAroundSum = 0;
            for (int i = 0; i < size; i++)
            {
                turnAround[i] = finishtime[i] - processArrival[i];
                turnAroundSum += turnAround[i];
            }
            return turnAroundSum * 1.0000 / size;
        }

        public static double SJFgetThroughput(int[] processBurst, int size)
        {
            int totalburst = 0;
            for (int i = 0; i < size; i++)
            {
                totalburst += processBurst[i];
            }
            return size * 1.00000 / totalburst;
        }

        //Assume the dispatch latency (for context switch) D is 10 ms. The total dispatch latency T = the number of context swithes * D. CPU utilization is (total CPU burst)/(total CPU burst + T).
        public static double SJFgetCPUutilization(int[] processArrival, int[] processBurst, int latency, int size)
        {
            int[] NumContextSwitches = new int[size];
            int totalContextSwitches = 0;
            int totalburst = 0;
            for (int i = 0; i < size; i++)
            {
                NumContextSwitches[i] = 2; // In SJF non preemptive schedule, all process will have two context switch, start and finish
                totalContextSwitches += NumContextSwitches[i]; //add up all the context switches
                totalburst += processBurst[i]; // calculate the total burst
            }
            return totalburst * 1.0000 / (totalburst + (latency * totalContextSwitches));

        }


        static void Main(string[] args)
        {
            int processSize = 10;
            int latency = 10;
            int[] processArrival = {0, 2, 2, 5, 5, 8, 10, 20, 26, 35}; //sorted
            int[] processBurst = {11, 6, 40, 20, 22, 30, 16, 5, 24, 40}; // unsorted
            int[] processBurst2 = { 5, 6, 11, 16, 20, 22, 24, 30, 40, 40 }; //sorted 
            double FCFSavgTurnAround = FCFSgetAverageTurnAroundTime(processArrival, processBurst, processSize);
            double FCFSavgThroughput = FCFSgetThroughput(processBurst, processSize);
            double FCFSCPUutilization = FCFSgetCPUutilization(processArrival, processBurst, latency, processSize);
            double SJFavgTurnAround = SJFgetAverageTurnAroundTime(processArrival, processBurst2, processSize);
            double SJFavgThroughput = SJFgetThroughput(processBurst2, processSize);
            double SJFCPUutilization = SJFgetCPUutilization(processArrival, processBurst2, latency, processSize);
            Console.WriteLine("With FCFS schedule, Average TurnAround time for size of " + processSize + " is: " + FCFSavgTurnAround + " ms");
            Console.WriteLine("With FCFS schedule, Average Throughput for size of " + processSize + " is: " + FCFSavgThroughput + " /ms");
            Console.WriteLine("With FCFS schedule, CPU utilization for size of " + processSize + " is: " + FCFSCPUutilization);
            Console.WriteLine();
            Console.WriteLine("With SJF schedule, Average TurnAround time for size of " + processSize + " is: " + SJFavgTurnAround + " ms");
            Console.WriteLine("With SJF schedule, Average Throughput for size of " + processSize + " is: " + SJFavgThroughput + " /ms");
            Console.WriteLine("With SJF schedule, CPU utilization for size of " + processSize + " is: " + SJFCPUutilization);
        }
    }
    
}
