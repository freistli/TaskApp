

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Author:  freistli
/// Purpose: Used to demo Task behavior in C# 7.0
/// Date:    2019-03-06
/// More explaination is on https://docs.microsoft.com/en-us/dotnet/standard/async-in-depth
/// </summary>
namespace TaskApp
{

    class Program
    {
        /// <summary>
        /// Task decorated by async
        /// </summary>
        /// <returns></returns>
        static async Task<int> test1()
        {
            return await Task.Run(async () => {
                Console.WriteLine("Run 1.1");
                await Task.Delay(2000);
                Console.WriteLine("Run 1.2");                
                return 1;
            });
        }
        /// <summary>
        /// Task without async
        /// </summary>
        /// <returns></returns>
        static  Task<int> test2()
        {
            return  Task.Run(() => {
                Console.WriteLine("Run 2.1");
                Task.Delay(2000).Wait();
                Console.WriteLine("Run 2.2");                
                return 2;
            });
        }

        /// <summary>
        /// Task without async and no explicit return value
        /// </summary>
        /// <returns></returns>
        static Task test3()
        {
            return Task.Run(() => {
                Console.WriteLine("Run 3.1");
                Task.Delay(2000).Wait();
                Console.WriteLine("Run 3.2");
                return 3;
            });
        }

        static async Task AllTestsAsync()
        {
            var t =  test1(); //  test1() <-async and no promising 
            
            Console.WriteLine("Continue "+t);
            await test2(); // test2() <-async
            Console.WriteLine("Continue 2");
            await Task.Delay(1000);
            Console.WriteLine("Continue 3");
            await test3();
            await Task.Delay(5000);
        }

        static void AllTests()
        {
            var t = test1(); // test1().Result or .Wait()<-sync; test1() <-async and no promising
            Console.WriteLine("Continue "+t.Result);
            test2(); //test2()).Result or .Wait()<-sync;   test2() <-async and no promising
            Console.WriteLine("Continue 2");
            Task.Delay(1000).Wait();
            Console.WriteLine("Continue 3");
            test3();
            Task.Delay(50000).Wait();
            
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Async output \r\n");
            AllTestsAsync().Wait(); //Sync call. This Task has no return value, so has no .Result
            Console.WriteLine("Sync output \r\n");
            AllTests();
        }
    }
}
