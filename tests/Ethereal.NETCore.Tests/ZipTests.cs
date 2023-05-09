using System.IO;
using System.IO.Compression;
using Xunit;

namespace Ethereal.NETCore.Tests;
public class ZipTests
{
    [Fact]
    public void Tests()
    {
        using (FileStream zipToOpen = File.OpenRead("Microsoft.TestPlatform.CommunicationUtilities.zip"))
        {
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
            {
                foreach (var item in archive.Entries)
                {

                }
            }
        }
    }

}
