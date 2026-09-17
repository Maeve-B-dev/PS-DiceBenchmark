/* Made with <3 by Maeve Bollmann - Dice Benchmark for Project Steam */
/* Performance Version of PS Dice Benchmark. */
using System.CommandLine;
using System.CommandLine.Parsing;

// Struct for all Parameters this Programm can use.
public struct diceParameter
{
    public int tw;
    public int bw;
    public int mw;
    public int sw;
    public int roll;
    public diceParameter(int Tw, int Bw, int Mw, int Sw, int Roll) {
        tw = Tw;
        bw = Bw;
        mw = Mw;
        sw = Sw;
        roll = Roll;
    }
}

//Struct for results, always populated by this Programm.
struct diceResult
{
    public int critSuccesses;
    public int successes;
    public int advantages;
    public int disadvantages;
    public int failures;
    public int critFailures;
    public int successfulRolls;
    public int criticalSuccessfullRolls;
    public int criticalFailureRolls;
    public diceResult(int CritSuccesses, int Successes, int Advantages, int Disadvantages, int Failures, int CritFailures, int SuccessfulRolls, int CriticalSuccessfullRolls, int CriticalFailureRolls)
    {
        critSuccesses = CritSuccesses;
        successes = Successes;
        advantages = Advantages;
        disadvantages = Disadvantages;
        failures = Failures;
        critFailures = CritFailures;
        successfulRolls = SuccessfulRolls;
        criticalSuccessfullRolls = CriticalSuccessfullRolls;
        criticalFailureRolls = CriticalFailureRolls;
    }
}

// Struct for command line arguments, if used.
struct argumentsValues
{
    public int tw;
    public int bw;
    public int mw;
    public int sw;
    public int rolls;
    public argumentsValues(int Tw, int Bw, int Mw, int Sw, int Rolls)
    {
        tw = Tw;
        bw = Bw;
        mw = Mw;
        sw = Sw;
        rolls = Rolls;
    }
}
public class Application
{
    // Gives extra output if enabled
    static bool verbose = false;

    //Defines if User wants to change Variables manually
    static bool defineDice = false;

    // Exits the Programm if set to true, used for main loop.
    static bool exitProgramm = false;

    // Fullautomatic mode for Web/integrated use, supresses most output and needs all parameters to be set via command line arguments.
    static bool fullAutomatic = false;
    public static void Main(string[] args)
    {
        // Set all command line Options with System.CommandLine.
        Option<bool> verboseOption = new("-v")
        {
            Description = "Run the benchmark with verbose output."
        };
        Option<int> twOption = new("--tw")
        {
            Description = "Number of Tw to roll.",
            DefaultValueFactory = parseResult => -1
        };
        Option<int> bwOption = new("--bw")
        {
            Description = "Number of Bw to roll.",
            DefaultValueFactory = parseResult => -1
        };
        Option<int> mwOption = new("--mw")
        {
            Description = "Number of Mw to roll.",
            DefaultValueFactory = parseResult => -1
        };
        Option<int> swOption = new("--sw")
        {
            Description = "Number of Sw to roll.",
            DefaultValueFactory = parseResult => -1
        };
        Option<int> rollsOption = new("--rolls")
        {
            Description = "Number of rolls to perform.",
            DefaultValueFactory = parseResult => -1
        };

        RootCommand rootCommand = new("PS Dice Benchmark - Benchmark dice rolls for Project Steam.");

        rootCommand.Options.Add(verboseOption);
        rootCommand.Options.Add(twOption);
        rootCommand.Options.Add(bwOption);
        rootCommand.Options.Add(mwOption);
        rootCommand.Options.Add(swOption);
        rootCommand.Options.Add(rollsOption);

        // Declares new argumentsValues with defaults of -1, overwrites if args were given.
        argumentsValues arguments = new(-1, -1, -1, -1, -1);
        ParseResult parseResult = rootCommand.Parse(args);
        if (parseResult.Errors.Count == 0)
        {
            verbose = parseResult.GetValue(verboseOption);
            arguments.tw = parseResult.GetValue(twOption);
            arguments.bw = parseResult.GetValue(bwOption);
            arguments.mw = parseResult.GetValue(mwOption);
            arguments.sw = parseResult.GetValue(swOption);
            arguments.rolls = parseResult.GetValue(rollsOption);
        }
        foreach (ParseError parseError in parseResult.Errors)
        {
            Console.Error.WriteLine(parseError.Message);
        }

        // Set default values
        diceParameter diceParametersMain = new diceParameter(4, 1, 1, 2, 2000);

        // Outputs title
        Console.WriteLine(" ______  _________ _______  _______    ______   _______  _        _______           _______  _______  _______  _       \r\n(  __  \\ \\__   __/(  ____ \\(  ____ \\  (  ___ \\ (  ____ \\( (    /|(  ____ \\|\\     /|(       )(  ___  )(  ____ )| \\    /\\\r\n| (  \\  )   ) (   | (    \\/| (    \\/  | (   ) )| (    \\/|  \\  ( || (    \\/| )   ( || () () || (   ) || (    )||  \\  / /\r\n| |   ) |   | |   | |      | (__      | (__/ / | (__    |   \\ | || |      | (___) || || || || (___) || (____)||  (_/ / \r\n| |   | |   | |   | |      |  __)     |  __ (  |  __)   | (\\ \\) || |      |  ___  || |(_)| ||  ___  ||     __)|   _ (  \r\n| |   ) |   | |   | |      | (        | (  \\ \\ | (      | | \\   || |      | (   ) || |   | || (   ) || (\\ (   |  ( \\ \\ \r\n| (__/  )___) (___| (____/\\| (____/\\  | )___) )| (____/\\| )  \\  || (____/\\| )   ( || )   ( || )   ( || ) \\ \\__|  /  \\ \\\r\n(______/ \\_______/(_______/(_______/  |/ \\___/ (_______/|/    )_)(_______/|/     \\||/     \\||/     \\||/   \\__/|_/    \\/\r\n                                                                                                                       ");

        while (!exitProgramm)
        {
            // Outputs info if args were used.
            if (arguments.tw != -1 || arguments.bw != -1 || arguments.mw != -1 || arguments.sw != -1 || arguments.rolls != -1)
            {
                Console.WriteLine("Using command line arguments for dice parameters.");
            }
            // If all args were used, enable full automatic mode for integration.
            if (arguments.tw != -1 && arguments.bw != -1 && arguments.mw != -1 && arguments.sw != -1 && arguments.rolls != -1)
            {
                fullAutomatic = true;
                Console.Clear();
                diceParametersMain = new diceParameter(arguments.tw, arguments.bw, arguments.mw, arguments.sw, arguments.rolls);
            }
            // Ask to define Parameters
            if (!fullAutomatic)
            {
                Console.Write("Define dice parameters?(y/N) ");
                defineDice = Console.ReadKey().KeyChar.CompareTo('y') == 0;

                if (defineDice)
                {
                    GetUserDiceParameters(arguments.tw, arguments.bw, arguments.mw, arguments.sw, arguments.rolls);
                }
            }

            // Run Benchmark and output results
            diceResult result = RunBenchmark(diceParametersMain, verbose);
            Console.WriteLine("\n\nBenchmark Results: \n" + diceParametersMain.roll + " Rolls.\nSuccess%: " + (double)result.successfulRolls / diceParametersMain.roll * 100 + "%; \nCritical Successes: " + result.criticalSuccessfullRolls + "(" + (double)result.criticalSuccessfullRolls / diceParametersMain.roll * 100 + "%)\nCritical Failures: " + result.criticalFailureRolls + "(" + (double)result.criticalFailureRolls / diceParametersMain.roll * 100 + "%)");
            // Output all results if verbose is enabled.
            if (verbose)
            {
                Console.WriteLine(ProcessTextResults(result));
            }
            
            if(fullAutomatic)
            {
                Console.WriteLine("\nFull automatic mode enabled, exiting after benchmark.");
                Environment.Exit(0);
            }
            Console.WriteLine("\nDo you want to run another benchmark? (y/N) ");
            exitProgramm = Console.ReadKey().KeyChar.CompareTo('y') != 0;
        }
        Environment.Exit(0);
    }
    // Combines 2 diceResult structs and returns the diceResult.
    static diceResult AddDiceResults(diceResult result1, diceResult result2)
    {
        return new diceResult(
            result1.critSuccesses + result2.critSuccesses,
            result1.successes + result2.successes,
            result1.advantages + result2.advantages,
            result1.disadvantages + result2.disadvantages,
            result1.failures + result2.failures,
            result1.critFailures + result2.critFailures,
            result1.successfulRolls + result2.successfulRolls,
            result1.criticalSuccessfullRolls + result2.criticalSuccessfullRolls,
            result1.criticalFailureRolls + result2.criticalFailureRolls
        );
    }
    // Ask user for each dice Parameter not set when calling function, returns a diceParameter struct with all values set.
    static diceParameter GetUserDiceParameters(int tw = -1, int bw = -1, int mw = -1, int sw = -1, int roll = -1)
    {
        string? input = "";
        diceParameter diceParameters = new diceParameter();
        Console.WriteLine("\nDefine dice (tw, bw, mw, sw) and the number of rolls:");

        input = "";
        while (!int.TryParse(input, out diceParameters.tw) && tw == -1)
        {
            Console.Write("Tw: ");
            input = Console.ReadLine();
        }

        input = "";
        while (!int.TryParse(input, out diceParameters.bw) && bw == -1)
        {
            Console.Write("Bw: ");
            input = Console.ReadLine();
        }

        input = "";
        while (!int.TryParse(input, out diceParameters.mw) && mw == -1)
        {
            Console.Write("Mw: ");
            input = Console.ReadLine();
        }

        input = "";
        while (!int.TryParse(input, out diceParameters.sw) && sw == -1)
        {
            Console.Write("Sw: ");
            input = Console.ReadLine();
        }

        Console.Write("Roll: ");
        input = "";
        while (!int.TryParse(input, out diceParameters.roll) && roll == -1)
        {
            Console.Write("Roll: ");
            input = Console.ReadLine();
        }
        Console.WriteLine("Do you want the program to run verbosely? (y/N)");
        verbose = Console.ReadKey().KeyChar.CompareTo('y') == 0;

        return diceParameters;
    }
    // Runs main Benchmark with given diceParameters and returns a diceResult struct with all results combined.
    static diceResult RunBenchmark(diceParameter diceParameters, bool writeRolls)
    {
        diceResult resultsCombined = new diceResult(0, 0, 0, 0, 0, 0, 0, 0, 0);
        for (int i = 0; i < diceParameters.roll; i++)
        {
            // Run rolls for each dice type and combine results
            diceResult rollResult = new diceResult(0, 0, 0, 0, 0, 0, 0, 0, 0);
            diceResult rollResultTW = RunRoll("tw", diceParameters.tw);
            rollResult = AddDiceResults(rollResult, rollResultTW);
            diceResult rollResultBW = RunRoll("bw", diceParameters.bw);
            rollResult = AddDiceResults(rollResult, rollResultBW);
            diceResult rollResultMW = RunRoll("mw", diceParameters.mw);
            rollResult = AddDiceResults(rollResult, rollResultMW);
            diceResult rollResultSW = RunRoll("sw", diceParameters.sw);
            rollResult = AddDiceResults(rollResult, rollResultSW);

            // Outputs each roll individually, if the user wishes.
            if (writeRolls)
            {
                Console.WriteLine("\n\nRoll " + (i + 1) + ":\n" + ProcessTextResults(rollResult));
            }

            int succ = rollResult.successes - rollResult.failures;

            if (succ > 0)
            {
                resultsCombined.successfulRolls++;
            }

            //Process the Result and add it to combined results
            if (rollResult.critSuccesses >= Math.Round((double)diceParameters.tw/2) && succ > 0 && diceParameters.tw > 0)
            {
                resultsCombined.criticalSuccessfullRolls++;
            }else if(rollResult.critFailures >= Math.Round((double)diceParameters.sw / 2) && succ < 0 && diceParameters.sw > 0)
            {
                resultsCombined.criticalFailureRolls++;
            }
            resultsCombined = AddDiceResults(resultsCombined, rollResult);
        }
        return resultsCombined;
    }
    //Runs a defined amount of rolls for a given dice type and returns a diceResult struct with the results.
    static diceResult RunRoll(string dice, int count)
    {
        diceResult result = new diceResult(0,0,0,0,0,0,0,0,0);

        for (int i = 0; i < count; i++)
        {
            int roll = new Random().Next(1, 7);
            switch ((roll, dice))
            {
                case (1, "tw"):
                    result.failures++;
                    break;
                case (2, "tw"):
                    result.successes++;
                    break;
                case (3, "tw"):
                    result.successes++;
                    break;
                case (4, "tw"):
                    result.successes++;
                    break;
                case (5, "tw"):
                    result.successes++;
                    break;
                case (6, "tw"):
                    result.critSuccesses++;
                    result.successes += 2;
                    break;

                case (1, "bw"): break;
                case (2, "bw"): break;
                case (3, "bw"):
                    result.successes++;
                    break;
                case (4, "bw"):
                    result.successes++;
                    break;
                case (5, "bw"):
                    result.successes++;
                    break;
                case (6, "bw"):
                    result.successes++;
                    break;

                case (1, "mw"):
                    result.failures++;
                    break;
                case (2, "mw"):
                    result.failures++;
                    break;
                case (3, "mw"):
                    result.failures++;
                    break;
                case (4, "mw"):
                    result.failures++;
                    break;
                case (5, "mw"): break;
                case (6, "mw"): break;

                case (1, "sw"):
                    result.failures += 2;
                    result.critFailures++;
                    break;
                case (2, "sw"):
                    result.failures++;
                    break;
                case (3, "sw"):
                    result.failures++;
                    break;
                case (4, "sw"):
                    result.failures++;
                    break;
                case (5, "sw"):
                    result.failures++;
                    break;
                case (6, "sw"):
                    AddDiceResults(result, RunRoll("bw", 1));
                    break;

                default:
                    Console.WriteLine("Dice Result out of Range or Dice not recognized: " + roll + " for dice type: " + dice);
                    Environment.Exit(1);
                    break;
            }
        }

        return result;
    }
    // Returns a string with all results from a diceResult struct, formatted for output.
    static string ProcessTextResults(diceResult result)
    {
        string textResult = "Crit Successes: " + result.critSuccesses + "\n" +
                            "Successes: " + result.successes + "\n" +
                            "Advantages: " + result.advantages + "\n" +
                            "Disadvantages: " + result.disadvantages + "\n" +
                            "Failures: " + result.failures + "\n" +
                            "Crit Failures: " + result.critFailures + "\n" +
                            "Successful Rolls: " + result.successfulRolls + "\n" +
                            "Critical Successful Rolls: " + result.criticalSuccessfullRolls + "\n" +
                            "Critical Failure Rolls: " + result.criticalFailureRolls;
        return textResult;
    }
}