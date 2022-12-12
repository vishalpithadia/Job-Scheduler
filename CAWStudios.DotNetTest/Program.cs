using System.Collections.Concurrent;
using System.Diagnostics;
using System.Dynamic;
using CAWStudios.DotNetTest;


var listOfJobs = JobManager.GetJobList();
var sw = new Stopwatch();
sw.Start();

// you can change the code in this file only below the 1st line and above the next 2nd line.
// you are also allowed to change the code in the ResourceManager.cs file as described within the file.
// you are NOT allowed to change any code in any other class\file.

// you goal is to make the total running time as fast as possible (it doesnt have to be the fastest solution,
// but fast enough)) while all the jobs provided in the list MUST run and be competed (you have to await them) !
// the order of the jobs is NOT important in any way and you can (but not have to) change
// it in any way you would like if you think it will improve your solution in any way.

// you can use any external nuget you would like but you are not required to do so.
// any technique or software architecture you would like is ok.
// any code you will add/modify should be clean, readable and "good looking" just like real production code,
// you can and should add comments to your code to describe and explain anything you added or modified.

// for any question or issue with the test, @@  please email me at kartik.bansal@cawstudios.com && ishika.jain@cawstudios.com @@

//░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
//░░░░░░░░░░░░░░░░░░░░░░████████░░░░░░░░░
//░░███████░░░░░░░░░░███▒▒▒▒▒▒▒▒███░░░░░░
//░░█▒▒▒▒▒▒█░░░░░░░███▒▒▒▒▒▒▒▒▒▒▒▒███░░░░
//░░░█▒▒▒▒▒▒█░░░░██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██░░
//░░░░█▒▒▒▒▒█░░░██▒▒▒▒▄██▄▒▒▒▒▄██▄▒▒▒███░
//░░░░░█▒▒▒█░░░█▒▒▒▒▒▒████▒▒▒▒████▒▒▒▒▒██
//░░░█████████████▒▒▒▒▀██▀▒▒▒▒▀██▀▒▒▒▒▒██
//░░░█▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒▒▒██
//░██▒▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒██▒▒▒▒▒▒▒▒▒██▒▒▒▒██
//██▒▒▒███████████▒▒▒▒▒██▒▒▒▒▒▒▒██▒▒▒▒▒██
//█▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒███████▒▒▒▒▒▒▒██
//██▒▒▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██░
//░█▒▒▒███████████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██░░░
//░██▒▒▒▒▒▒▒▒▒▒▒███▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█░░░░░
//░░████████████░░░████████████████░░░░░░
//░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
//░░▄█████▄░▄███████▄░▄███████▄░██████▄░░
//░░██▒▒▒▒█░███▒▒▒███░███▒▒▒███░██▒▒▒██░░
//░░██▒▒▒▒▒░██▒▒▒▒▒██░██▒▒▒▒▒██░██▒▒▒██░░
//░░██▒▒▒▀█░███▒▒▒███░███▒▒▒███░██▒▒▒██░░
//░░▀█████▀░▀███████▀░▀███████▀░██████▀░░
//░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
//░░░░██▒▒▒▒░██▒▒▒██░▄█████░██▒▒▒▒██▀░░░░
//░░░░██▒▒▒▒░██▒▒▒██░██▀▒▒▒░██▒▒▒██░░░░░░
//░░░░██▒▒▒▒░██▒▒▒██░██▒▒▒▒░█████▀░░░░░░░
//░░░░██▒▒▒▒░██▄▒▄██░██▄▒▒▒░██▒▒▒██░░░░░░
//░░░░▀█████░▀█████▀░▀█████░██▒▒▒▒██▄░░░░
//░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
// ----------------------------------------------------------- 1st line

// we must run all jobs in the list!
List<Task> tasks = new List<Task>(listOfJobs.Count);

int listOfJobsCounter = listOfJobs.Count;

int i = 0;
for (; i < 3; i++)
{
    tasks.Add(listOfJobs[i].Start());
    listOfJobsCounter--;
}
while (listOfJobsCounter > 0)
{
    Task.WaitAny(tasks.ToArray());
    for (int j = 0; j < 3;j++)
    {
        if (tasks[j].IsCompleted && listOfJobsCounter > 0)
        {
            tasks[j] = listOfJobs[i].Start();
            i++;
            listOfJobsCounter--;
        }
    }
}

Task.WaitAll(tasks.ToArray());

// ----------------------------------------------------------- 2nd line

// here all jobs must be finished (awaited) !
sw.Stop();
Console.WriteLine($"Total jobs: {listOfJobs.Count}");
Console.WriteLine($"Shortest job: {listOfJobs.MinBy(x=>x.TimeSeconds)!.TimeSeconds} seconds");
Console.WriteLine($"Longest job: {listOfJobs.MaxBy(x => x.TimeSeconds)!.TimeSeconds} seconds");
Console.WriteLine("---------------------------------------------------------");
Console.WriteLine($"You running time: {sw.ElapsedMilliseconds/1000} seconds");

   