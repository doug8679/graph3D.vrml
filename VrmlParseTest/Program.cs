using Graph3D.Vrml.Tokenizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph3D.Vrml;
using Graph3D.Vrml.Nodes;
using Graph3D.Vrml.Nodes.Appearance;
using Graph3D.Vrml.Nodes.Appearance.Texture;
using Graph3D.Vrml.Nodes.Bindable;
using Graph3D.Vrml.Nodes.Geometry;
using Graph3D.Vrml.Nodes.Grouping;
using Graph3D.Vrml.Nodes.Interpolation;
using Graph3D.Vrml.Nodes.LightSources;
using Graph3D.Vrml.Nodes.Sensors;
using Graph3D.Vrml.Parser;

namespace VrmlParseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                VrmlScene scene;
                using (var stream = System.IO.File.OpenRead(args[0])) {
                //using (var stream = typeof(Program).Assembly.GetManifestResourceStream(typeof(Program), "D2.wrl")) {
                    var tokenizer = new Vrml97Tokenizer(stream);
                    var parser = new VrmlParser(tokenizer);
                    scene = new VrmlScene();
                    parser.Parse(scene);
                    var visitor = new TestNodeVisitor();
                    visitor.Visit(scene.root);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }

    public class TestNodeVisitor : INodeVisitor {
        #region Implementation of INodeVisitor

        public void Visit(AnchorNode node) {
            Console.WriteLine(node);
        }
        public void Visit(AppearanceNode node) { Console.WriteLine(node); }
        public void Visit(BackgroundNode node) { Console.WriteLine(node); }
        public void Visit(BoxNode node) { Console.WriteLine(node); }
        public void Visit(ColorNode node) { Console.WriteLine(node); }
        public void Visit(CoordinateInterpolatorNode node) { Console.WriteLine(node); }
        public void Visit(CoordinateNode node) { Console.WriteLine(node); }
        public void Visit(CylinderNode node) { Console.WriteLine(node); }
        public void Visit(ProtoNode node) { Console.WriteLine(node); }
        public void Visit(DirectionalLightNode node) { Console.WriteLine(node); }
        public void Visit(ExtrusionNode node) { Console.WriteLine(node); }
        public void Visit(GroupNode node) { Console.WriteLine(node); }
        public void Visit(IndexedFaceSetNode node) { Console.WriteLine(node); }
        public void Visit(MaterialNode node) { Console.WriteLine(node); }
        public void Visit(NavigationInfoNode node) { Console.WriteLine(node); }
        public void Visit(NormalNode node) { Console.WriteLine(node); }
        public void Visit(OrientationInterpolatorNode node) { Console.WriteLine(node); }
        public void Visit(PixelTextureNode node) { Console.WriteLine(node); }
        public void Visit(PointLightNode node) { Console.WriteLine(node); }
        public void Visit(PositionInterpolatorNode node) { Console.WriteLine(node); }
        public void Visit(ScalarInterpolationNode node) { Console.WriteLine(node); }
        public void Visit(SceneGraphNode node) { Console.WriteLine(node); }
        public void Visit(ScriptNode node) { Console.WriteLine(node); }
        public void Visit(ShapeNode node) { Console.WriteLine(node); }
        public void Visit(SphereNode node) { Console.WriteLine(node); }
        public void Visit(TextureCoordinateNode node) { Console.WriteLine(node); }
        public void Visit(TimeSensorNode node) { Console.WriteLine(node); }
        public void Visit(TransformNode node) { Console.WriteLine(node); }
        public void Visit(ViewpointNode node) { Console.WriteLine(node); }
        public void Visit(WorldInfoNode node) { Console.WriteLine(node); }

        #endregion
    }
}
