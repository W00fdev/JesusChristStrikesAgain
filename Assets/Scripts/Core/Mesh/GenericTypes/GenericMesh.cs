using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Christ.Core
{
    // Mesh can be shared (no need to create each time)
    public abstract class GenericMesh
    {
        protected Mesh CurrentMesh;
        protected Transform Parent;
        protected Material CurrentMaterial;
        protected BoxCollider2D Collider;

        protected MeshFilter FilterCached;
        protected MeshRenderer RendererCached;

        protected Transform transform;
        protected Vector3 Position;
        protected Vector3 Rotation;
        protected Vector3 Scale;

        [SerializeField]
        protected Vector3[] Vertices;
        protected Vector2[] UV;
        protected int[] Triangles;

        public enum PivotType { LEFT = 0, CENTER = 1 };

        // Constraints left pivot base
        internal static readonly Vector3 vec2_up = new Vector3(0, 1, 0);
        internal static readonly Vector3 vec2_one = new Vector3(1, 1, 0);
        internal static readonly Vector3 vec2_zero = new Vector3(0, 0, 0);
        internal static readonly Vector3 vec2_right = new Vector3(1, 0, 0);

        internal static readonly Vector3 vec2_pivot_middle = new Vector3(-0.5f, -0.5f);

        protected Vector3 mesh_up = new Vector3(0, 1, 0);
        protected Vector3 mesh_one = new Vector3(1, 1, 0);
        protected Vector3 mesh_zero = new Vector3(0, 0, 0);
        protected Vector3 mesh_right = new Vector3(1, 0, 0);


        public GenericMesh(Vector3 position, Vector3 rotation, Vector3 scale, Transform parent, Material material)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;

            Parent = parent;
            CurrentMaterial = material;
        }

        public abstract void CreateMesh();

        // Can be properties-based.
        public abstract void ApplyPosition(Vector3 newPosition);
        public abstract void ApplyRotation(Vector3 newRotation);
        public abstract void ApplyScale(Vector3 newScale);

        // Change to specific data-structure
        public abstract void ApplyTransform(Vector3 newPosition, Vector3 newScale);
        public abstract void ApplyTransform(Vector3 newPosition, Vector3 newRotation, Vector3 newScale);

        public virtual void SetPivot(PivotType type)
        {
            switch(type)
            {
                case PivotType.LEFT:
                    mesh_up = vec2_up;
                    mesh_one = vec2_one;
                    mesh_zero = vec2_zero;
                    mesh_right = vec2_right;
                    break;

                case PivotType.CENTER:
                    mesh_up = vec2_up + vec2_pivot_middle;
                    mesh_one = vec2_one + vec2_pivot_middle;
                    mesh_zero = vec2_zero + vec2_pivot_middle;
                    mesh_right = vec2_right + vec2_pivot_middle;
                    break;
            }    
        }

        internal static Vector3 VecMultiply(Vector3 left, Vector3 right)
        {
            return new Vector3(left.x * right.x, left.y * right.y, left.z * right.z);
        }
    }
}
