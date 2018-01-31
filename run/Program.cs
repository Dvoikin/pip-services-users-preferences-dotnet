using PipServices.UsersPreferences.Container;

using System;

namespace PipServices.UsersPreferences
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var task = (new UsersPreferencesProcess()).RunAsync(args);
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}
