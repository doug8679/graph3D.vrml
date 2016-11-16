using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graph3D.Vrml.Fields;

namespace Graph3D.Vrml.Nodes
{
    public class WorldInfoNode : BaseNode, IChildNode
    {
        public WorldInfoNode() {
            addExposedField("info", new MFString());
            addExposedField("title", new SFString());
        }
        #region Overrides of BaseNode

        protected override BaseNode createInstance() {
            return new WorldInfoNode();
        }

        public override void AcceptVisitor(INodeVisitor visitor) {
            visitor.Visit(this);
        }

        #endregion
    }
}
