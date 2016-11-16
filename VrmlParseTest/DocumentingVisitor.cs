using System;
using Graph3D.Vrml.Fields;
using Graph3D.Vrml.Nodes;
using Graph3D.Vrml.Nodes.Appearance;
using Graph3D.Vrml.Nodes.Appearance.Texture;
using Graph3D.Vrml.Nodes.Bindable;
using Graph3D.Vrml.Nodes.Geometry;
using Graph3D.Vrml.Nodes.Grouping;
using Graph3D.Vrml.Nodes.Interpolation;
using Graph3D.Vrml.Nodes.LightSources;
using Graph3D.Vrml.Nodes.Sensors;

namespace VrmlParseTest {
    public class DocumentingVisitor : INodeVisitor, IFieldVisitor {
        private int indent = 0;

        private void Document(string msg) {
            for ( int i = 0; i < indent; i++ ) {
                Console.Write("   ");
            }
            Console.WriteLine(msg);
        }

        #region Implementation of INodeVisitor
        public void Visit(AnchorNode node) {
            Document($"Visiting AnchorNode: {node.name}");
            indent++;
            foreach ( var child in node.children ) {
                child.AcceptVisitor(this);
            }
            indent--;
        }

        public void Visit(AppearanceNode node) {
            Document($"Visiting AppearanceNode: {node.name}");
            indent++;
            node.material?.AcceptVisitor(this);
            node.texture?.AcceptVisitor(this);
            node.textureTransform?.AcceptVisitor(this);
            indent--;
        }

        public void Visit(BackgroundNode node) {
            Document($"Visiting BackgroundNode: {node.name}");
        }

        public void Visit(BoxNode node) {
            Document($"Visiting BoxNode: {node.name}");
            indent++;
            Document($"size: {node.size}");
            indent--;
        }
        public void Visit(ColorNode node) {
            Document($"Visiting ColorNode: {node.name}");
            indent++;
            Document($"color: {node.color}");
            indent--;
        }

        public void Visit(CoordinateInterpolatorNode node) {
            Document($"Visiting CoordinateInterpolationNode: {node.name}");
        }

        public void Visit(CoordinateNode node) {
            Document($"Visiting CoordinateNode: {node.name}");
            indent++;
            Document($"point: {node.point}");
            indent--;
        }
        public void Visit(CylinderNode node) { Console.WriteLine(node); }
        public void Visit(ProtoNode node) { Console.WriteLine(node); }
        public void Visit(DirectionalLightNode node) {
            Document($"Visiting DirectionalLightNode: {node.name}");
            indent++;
            Document($"direction: {node.direction}");
            indent--;
        }
        public void Visit(ExtrusionNode node) { Console.WriteLine(node); }

        public void Visit(GroupNode node) {
            Document($"Visiting GroupNode: {node.name}");
            indent++;
            node.children.AcceptVisitor(this);
            indent--;
        }

        public void Visit(IndexedFaceSetNode node) {
            Document($"Visiting IndexedFaceSetNode: {node.name}");
            indent++;
            Document($"color-per-vertex: {node.colorPerVertex}");
            Document($"normal-per-vertex: {node.normalPerVertex}");
            node.coord?.AcceptVisitor(this);
            node.normal?.AcceptVisitor(this);
            node.color?.AcceptVisitor(this);
            node.texCoord?.AcceptVisitor(this);
            indent--;
        }

        public void Visit(MaterialNode node) {
            Document($"Visiting MaterialNode: {node.name}");
            indent++;
            Document($"ambient intensity: {node.ambientIntensity}");
            Document($"diffuse color: {node.diffuseColor}");
            Document($"emissive color: {node.emissiveColor}");
            Document($"shininess: {node.shininess}");
            Document($"specular color: {node.specularColor}");
            Document($"transparency: {node.transparency}");
            indent--;
        }
        public void Visit(NavigationInfoNode node) {
            Document($"Visiting NavigationInfoNode: {node.name}");
            indent++;
            Document($"headlight: {node.headlight.Value}");
            indent--;
        }

        public void Visit(NormalNode node) {
            Document($"Visiting NormalNode: {node.name}");
            indent++;
            Document($"normal vector: {node.vector}");
            indent--;
        }
        public void Visit(OrientationInterpolatorNode node) { Console.WriteLine(node); }
        public void Visit(PixelTextureNode node) { Console.WriteLine(node); }
        public void Visit(PointLightNode node) { Console.WriteLine(node); }
        public void Visit(PositionInterpolatorNode node) { Console.WriteLine(node); }
        public void Visit(ScalarInterpolationNode node) { Console.WriteLine(node); }

        public void Visit(SceneGraphNode node) {
            Document($"Visiting SceneGraphNode: {node.name}");
            indent++;
            foreach ( var child in node.children ) {
                child.AcceptVisitor(this);
            }
            indent--;
        }
        public void Visit(ScriptNode node) { Console.WriteLine(node); }
        public void Visit(ShapeNode node) {
            Document($"Visiting ShapeNode: {node.name}");
            indent++;
            node.appearance.AcceptVisitor(this);
            node.geometry.AcceptVisitor(this);
            indent--;
        }

        public void Visit(SphereNode node) {
            Document($"Visiting SphereNode: {node.name}");
            indent++;
            Document($"radius: {node.radius}");
            indent--;
        }
        public void Visit(TextureCoordinateNode node) { Console.WriteLine(node); }
        public void Visit(TimeSensorNode node) { Console.WriteLine(node); }

        public void Visit(TransformNode node) {
            Document($"Visiting TransformNode: {node.name}");
            indent++;
            Document($"center: {node.center}");
            Document($"rotation: {node.rotation}");
            Document($"scale: {node.scale}");
            Document($"scale orientation: {node.scaleOrientation}");
            Document($"translation: {node.translation}");
            Console.Read();
            foreach (var child in node.children)
            {
                child.AcceptVisitor(this);
            }
            indent--;
        }
        public void Visit(ViewpointNode node) {
            Document($"Visiting ViewpointNode: {node.name}");
            indent++;
            Document(node.description.Value);
            indent--;
        }
        public void Visit(WorldInfoNode node) {
            Document($"Visiting WorldInfoNode: {node.name}");
        }

        #endregion

        #region Implementation of IFieldVisitor

        public void visit(SFBool field) {}
        public void visit(SFImage field) {}
        public void visit(SFFloat field) {}
        public void visit(MFFloat field) {}
        public void visit(SFString field) {}
        public void visit(MFString field) {}
        public void visit(SFInt32 field) {}
        public void visit(MFInt32 field) {}
        public void visit(SFVec2f field) {}
        public void visit(MFVec2f field) {}
        public void visit(SFVec3f field) {}
        public void visit(MFVec3f field) {}
        public void visit(SFColor field) {}
        public void visit(MFColor field) {}
        public void visit(SFNode field) { field.Node?.AcceptVisitor(this); }

        public void visit(MFNode field) {
            foreach ( var node in field.Values ) {
                node.AcceptVisitor(this);
            }
        }
        public void visit(SFRotation field) {}
        public void visit(MFRotation field) {}
        public void visit(SFTime field) {}
        #endregion
    }

    public class ConversionVisitor : INodeVisitor, IFieldVisitor {

        #region Implementation of INodeVisitor

        public void Visit(AnchorNode node) { throw new NotImplementedException(); }
        public void Visit(AppearanceNode node) { throw new NotImplementedException(); }
        public void Visit(BackgroundNode node) { throw new NotImplementedException(); }
        public void Visit(BoxNode node) { throw new NotImplementedException(); }
        public void Visit(ColorNode node) { throw new NotImplementedException(); }
        public void Visit(CoordinateInterpolatorNode node) { throw new NotImplementedException(); }
        public void Visit(CoordinateNode node) { throw new NotImplementedException(); }
        public void Visit(CylinderNode node) { throw new NotImplementedException(); }
        public void Visit(ProtoNode node) { throw new NotImplementedException(); }
        public void Visit(DirectionalLightNode node) { throw new NotImplementedException(); }
        public void Visit(ExtrusionNode node) { throw new NotImplementedException(); }
        public void Visit(GroupNode node) { throw new NotImplementedException(); }
        public void Visit(IndexedFaceSetNode node) { throw new NotImplementedException(); }
        public void Visit(MaterialNode node) { throw new NotImplementedException(); }
        public void Visit(NavigationInfoNode node) { throw new NotImplementedException(); }
        public void Visit(NormalNode node) { throw new NotImplementedException(); }
        public void Visit(OrientationInterpolatorNode node) { throw new NotImplementedException(); }
        public void Visit(PixelTextureNode node) { throw new NotImplementedException(); }
        public void Visit(PointLightNode node) { throw new NotImplementedException(); }
        public void Visit(PositionInterpolatorNode node) { throw new NotImplementedException(); }
        public void Visit(ScalarInterpolationNode node) { throw new NotImplementedException(); }
        public void Visit(SceneGraphNode node) { throw new NotImplementedException(); }
        public void Visit(ScriptNode node) { throw new NotImplementedException(); }
        public void Visit(ShapeNode node) { throw new NotImplementedException(); }
        public void Visit(SphereNode node) { throw new NotImplementedException(); }
        public void Visit(TextureCoordinateNode node) { throw new NotImplementedException(); }
        public void Visit(TimeSensorNode node) { throw new NotImplementedException(); }
        public void Visit(TransformNode node) { throw new NotImplementedException(); }
        public void Visit(ViewpointNode node) { throw new NotImplementedException(); }
        public void Visit(WorldInfoNode node) { throw new NotImplementedException(); }

        #endregion

        #region Implementation of IFieldVisitor

        public void visit(SFBool field) { throw new NotImplementedException(); }
        public void visit(SFImage field) { throw new NotImplementedException(); }
        public void visit(SFFloat field) { throw new NotImplementedException(); }
        public void visit(MFFloat field) { throw new NotImplementedException(); }
        public void visit(SFString field) { throw new NotImplementedException(); }
        public void visit(MFString field) { throw new NotImplementedException(); }
        public void visit(SFInt32 field) { throw new NotImplementedException(); }
        public void visit(MFInt32 field) { throw new NotImplementedException(); }
        public void visit(SFVec2f field) { throw new NotImplementedException(); }
        public void visit(MFVec2f field) { throw new NotImplementedException(); }
        public void visit(SFVec3f field) { throw new NotImplementedException(); }
        public void visit(MFVec3f field) { throw new NotImplementedException(); }
        public void visit(SFColor field) { throw new NotImplementedException(); }
        public void visit(MFColor field) { throw new NotImplementedException(); }
        public void visit(SFNode field) { throw new NotImplementedException(); }
        public void visit(MFNode field) { throw new NotImplementedException(); }
        public void visit(SFRotation field) { throw new NotImplementedException(); }
        public void visit(MFRotation field) { throw new NotImplementedException(); }
        public void visit(SFTime field) { throw new NotImplementedException(); }

        #endregion
    }
}