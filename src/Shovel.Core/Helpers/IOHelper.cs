namespace Shovel.Core.Helpers;

public static class IOHelper
{
    public static void EnsureDirectoryExists(string path)
    {
        if (Path.IsPathRooted(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        }
    }

    public static void DeleteDirectory(string dirPath)
    {
        foreach (var folder in Directory.GetDirectories(dirPath))
        {
            DeleteDirectory(folder);
        }

        foreach (string file in Directory.GetFiles(dirPath))
        {
            var pPath = Path.Combine(dirPath, file);
            File.SetAttributes(pPath, FileAttributes.Normal);
            File.Delete(file);
        }

        Directory.Delete(dirPath);
    }

    public static string SanitizeFileName(string fileName, char replacementChar = '_')
    {
        var blackList = new HashSet<char>(Path.GetInvalidFileNameChars()) { '"' }; // '"' not invalid in Linux, but causes problems
        var output = fileName.ToCharArray();
        for (int i = 0, ln = output.Length; i < ln; i++)
        {
            if (blackList.Contains(output[i]))
            {
                output[i] = replacementChar;
            }
        }
        return new string(output);
    }
}
