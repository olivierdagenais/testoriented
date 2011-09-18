using System.IO;

namespace SoftwareNinjas.PublicInterfaceComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            var baselineFile = new FileInfo(args[0]);
            var challengerFile = new FileInfo(args[1]);
            var reportFile = new FileInfo(args[2]);
            // TODO: scan both files, then produce a report based on their differences
        }
    }
}
