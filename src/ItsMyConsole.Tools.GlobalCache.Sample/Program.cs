using ItsMyConsole;
using ItsMyConsole.Tools.GlobalCache;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace MyExampleConsole
{
    class Program
    {
        static async Task Main() {
            ConsoleCommandLineInterpreter ccli = new ConsoleCommandLineInterpreter();

            // Console configuration
            ccli.Configure(options => {
                options.Prompt = ">> ";
                options.LineBreakBetweenCommands = true;
                options.HeaderText = "##################\n#  Global Cache  #\n##################\n";
                options.TrimCommand = true;
            });

            // Update global cache
            // Example : set MyNewValue
            ccli.AddCommand("^set (.+)$", RegexOptions.IgnoreCase, tools => {
                string value = tools.CommandMatch.Groups[1].Value;
                if (tools.GlobalCache().TryGetValue("<KEY>", out string oldValue))
                    Console.WriteLine($"Old value: {oldValue}");
                tools.GlobalCache().Set("<KEY>", value);
                Console.WriteLine("Global cache updated");
            });

            // Get the value in the global cache
            // Example : get
            ccli.AddCommand("^get$", RegexOptions.IgnoreCase, tools => {
                string value = tools.GlobalCache().Get<string>("<KEY>");
                Console.WriteLine($"Value: {value}");
            });

            await ccli.RunAsync();
        }
    }
}
