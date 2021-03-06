﻿using Graph3D.Vrml.Nodes;

namespace Graph3D.Vrml.Fields {
    public class MFNode : MField<BaseNode> {

        public override void AcceptVisitor(IFieldVisitor visitor) {
            visitor.visit(this);
        }

        public override Field Clone() {
            var clone = new MFNode();
            foreach (var child in Values) {
                clone.AppendValue(child.clone());
            }
            return clone;
        }

        public override FieldType Type {
            get { return FieldType.MFNode; }
        }

    }
}
