﻿using Graph3D.Vrml.Fields;

namespace Graph3D.Vrml.Nodes.Grouping {
    /// <summary>
    /// Transform { 
    ///   eventIn      MFNode      addChildren
    ///   eventIn      MFNode      removeChildren
    ///   exposedField SFVec3f     center           0 0 0    # (-,)
    ///   exposedField MFNode      children         []
    ///   exposedField SFRotation  rotation         0 0 1 0  # [-1,1],(-,)
    ///   exposedField SFVec3f     scale            1 1 1    # (0,)
    ///   exposedField SFRotation  scaleOrientation 0 0 1 0  # [-1,1],(-,)
    ///   exposedField SFVec3f     translation      0 0 0    # (-,)
    ///   field        SFVec3f     bboxCenter       0 0 0    # (-,)
    ///   field        SFVec3f     bboxSize         -1 -1 -1 # (0,) or -1,-1,-1
    /// }
    /// </summary>
    public class TransformNode : GroupingNode, IChildNode {

        public TransformNode() {
            addExposedField("center", new SFVec3f(0, 0, 0));
            addExposedField("rotation", new SFRotation(0, 0, 1, 0));
            addExposedField("scale", new SFVec3f(1, 1, 1));
            addExposedField("scaleOrientation", new SFRotation(0, 0, 1, 0));
            addExposedField("translation", new SFVec3f(0, 0, 0));
        }

        public SFVec3f center {
            get { return GetExposedField("center") as SFVec3f; }
        }

        public SFRotation rotation {
            get { return GetExposedField("rotation") as SFRotation; }
        }

        public SFVec3f scale {
            get { return GetExposedField("scale") as SFVec3f; }
        }

        public SFRotation scaleOrientation {
            get { return GetExposedField("scaleOrientation") as SFRotation; }
        }

        public SFVec3f translation {
            get { return GetExposedField("translation") as SFVec3f; }
        }

        /// <summary>
        /// Generates matrix for tranformation from local coordinate system to origin one.
        /// </summary>
        /// <returns>3x4 matrix.</returns>
        public float[,] GenerateTransformMatrix() {
            //P' = T × C × R × SR × S × -SR × -C × P
            float[,] matrix = VrmlMath.GetUnitMatrix();

            float[,] temp = null;

            temp = VrmlMath.GetUnitMatrix();
            temp[0, 3] = -center.x;
            temp[1, 3] = -center.y;
            temp[2, 3] = -center.z;
            VrmlMath.ConcatenateMatrixes(temp, matrix, matrix);

            temp = VrmlMath.GenerateRotationMatrix(scaleOrientation.X, scaleOrientation.Y, scaleOrientation.Z, -scaleOrientation.Angle);
            VrmlMath.ConcatenateMatrixes(temp, matrix, matrix);

            temp = VrmlMath.GetUnitMatrix();
            temp[0, 0] = 1 / scale.x;
            temp[1, 1] = 1 / scale.y;
            temp[2, 2] = 1 / scale.z;
            VrmlMath.ConcatenateMatrixes(temp, matrix, matrix);

            temp = VrmlMath.GenerateRotationMatrix(scaleOrientation.X, scaleOrientation.Y, scaleOrientation.Z, scaleOrientation.Angle);
            VrmlMath.ConcatenateMatrixes(temp, matrix, matrix);

            temp = VrmlMath.GenerateRotationMatrix(rotation.X, rotation.Y, rotation.Z, rotation.Angle);
            VrmlMath.ConcatenateMatrixes(temp, matrix, matrix);

            temp = VrmlMath.GetUnitMatrix();
            temp[0, 3] = center.x;
            temp[1, 3] = center.y;
            temp[2, 3] = center.z;
            VrmlMath.ConcatenateMatrixes(temp, matrix, matrix);

            temp = VrmlMath.GetUnitMatrix();
            temp[0, 3] = translation.x;
            temp[1, 3] = translation.y;
            temp[2, 3] = translation.z;
            VrmlMath.ConcatenateMatrixes(temp, matrix, matrix);

            return matrix;
        }

        protected override BaseNode createInstance() {
            return new TransformNode();
        }

        public override void AcceptVisitor(INodeVisitor visitor) {
            visitor.Visit(this);
        }

    }
}
