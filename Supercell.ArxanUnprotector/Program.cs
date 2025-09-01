using Supercell.ArxanUnprotector;
using Supercell.ArxanUnprotector.Actions;

Dictionary<string, string> arguments = ParseArguments(args);

string original = arguments.GetValueOrDefault("-i");
string modified = arguments.GetValueOrDefault("-m");
string output = arguments.GetValueOrDefault("-o", modified);
bool isKillStringEncryption = false;

if (arguments.ContainsKey("-e"))
{
    // why did i needed so much code just to do that..
    if (arguments["-e"] == null)
    {
        isKillStringEncryption = true;
    }
    else if (bool.TryParse(arguments["-e"], out var parsed))
    {
        isKillStringEncryption = parsed;
    }
}

IAction action = arguments.GetValueOrDefault("-a") switch
{
    "verify-crc" => new VerifyChecksumsAction(),
    "update-crc" => new UpdateChecksumsAction(),
    "decrypt" => new DecryptStringsAction(),
    "encrypt" => new EncryptStringsAction(),
    _ => null
};

if (action == null)
{
    Console.WriteLine("""
        Usage: Supercell.ArxanUnprotector -a <action> -i <original> -m <modified> [-o <output> -k <kill string encryption>]
        Actions:
            verify-crc - Verify checksums
            update-crc - Update checksums
            decrypt - Decrypt strings
            encrypt - Encrypt strings
    """);
    return -1;
}

Library originalLibrary = File.Exists(original) ? LibraryLoader.Load(original) : null;
Library modifiedLibrary = File.Exists(modified) ? LibraryLoader.Load(modified) : null;

if (output != null)
{
    string outputDirectory = Path.GetDirectoryName(output);

    if (!string.IsNullOrEmpty(outputDirectory) && !Directory.Exists(outputDirectory))
    {
        Directory.CreateDirectory(outputDirectory);
    }
}

string result = action.Execute(originalLibrary, modifiedLibrary, output, isKillStringEncryption);

if (result == null)
{
    Console.WriteLine("Success!");
    return 0;
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result);
    Console.ResetColor();

    return -1;
}

static Dictionary<string, string?> ParseArguments(string[] args)
{
    var arguments = new Dictionary<string, string?>();

    for (int i = 0; i < args.Length; i++)
    {
        string key = args[i];

        // had to edit it
        if (i + 1 < args.Length && !args[i + 1].StartsWith("-"))
        {
            arguments[key] = args[i + 1];
            i++; 
        }
        else
        {
            arguments[key] = null;
        }
    }

    return arguments;
}
