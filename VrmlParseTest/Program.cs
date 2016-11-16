using Graph3D.Vrml.Tokenizer;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph3D.Vrml;
using Graph3D.Vrml.Parser;

namespace VrmlParseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*try {
                Browser browser = new Browser();
                var sw = Stopwatch.StartNew();
                var nodes = browser.createVrmlFromString(File.ReadAllText(args[0]));
                sw.Stop();
                Console.WriteLine($"Processing of VRML with browser took {sw.ElapsedMilliseconds}ms");
                foreach ( var node in nodes ) {
                    Console.WriteLine($"node name: {node.name}, node type: {node.GetType().Name}");
                }
            } catch ( Exception ex ) {
                Console.Error.WriteLine(ex);
            }*/
            try {
                VrmlScene scene;
                using (var stream = System.IO.File.OpenRead(args[0])) {
                //using (var stream = typeof(Program).Assembly.GetManifestResourceStream(typeof(Program), "D2.wrl")) {
                    var tokenizer = new Vrml97Tokenizer(stream);
                    var parser = new VrmlParser(tokenizer);
                    scene = new VrmlScene();
                    parser.Parse(scene);
                    var visitor = new DocumentingVisitor();
                    visitor.Visit(scene.root);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }
}
