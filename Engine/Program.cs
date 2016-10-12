using DotNetCoreKoans.Koans;
using Microsoft.DotNet.Cli.Utils;

namespace DotNetCoreKoans.Engine
{
    public class Program
    {
        //can accept an array w/o length specified because it is happening in a method wherein the arguments are serving as defacto length???
        public static int Main(string[] args)
        
        {
            var reporter = Reporter.Output;
            var sensei = new Sensei(reporter);
            var path = new PathToEnlightenment();
            
            return path.Walk(sensei);
        }
    }
}
