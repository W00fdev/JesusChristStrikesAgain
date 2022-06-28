using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Christ.Core
{
    public class GenericMeshQuad : GenericMesh
    {
        public GenericMeshQuad(Vector3 position, Vector3 rotation, Vector3 scale, Transform parent, Material material) 
            : base(position, rotation, scale, parent, material)
        {
            SetPivot(PivotType.CENTER);
            CreateMesh();
        }

        public override void CreateMesh()
        {
            // Left-down pivot.

            Vertices = new Vector3[4];
            UV = new Vector2[4];
            Triangles = new int[6];

            UpdateVertices(false);

            //  *--*    (0,1) -- (1, 1)
            //  | /|      |         |
            //  |/ |      |         |
            //  *--*    (0,0) -- (1, 0)

            UV[0] = new Vector2(0, 1);
            UV[1] = new Vector2(1, 1);
            UV[2] = new Vector2(0, 0);
            UV[3] = new Vector2(1, 0);

            Triangles[0] = 0;
            Triangles[1] = 1;
            Triangles[2] = 2;   // first clock-wise triangle

            Triangles[3] = 2;
            Triangles[4] = 1;
            Triangles[5] = 3;   // second clock-wise triangle

            CurrentMesh = new Mesh
            {
                vertices = Vertices,
                uv = UV,
                triangles = Triangles
            }; 

            GameObject gameObject = new GameObject("Mesh", typeof(MeshRenderer));
            transform = gameObject.transform;
            transform.localScale = new Vector3(1, 1, 1);
            transform.position = Position;
            transform.parent = Parent;

            FilterCached = gameObject.AddComponent<MeshFilter>();
            FilterCached.mesh = CurrentMesh;

            RendererCached = gameObject.GetComponent<MeshRenderer>();
            RendererCached.material = CurrentMaterial;

            Collider = gameObject.AddComponent<BoxCollider2D>();
            Collider.usedByComposite = true;
        }

        public override void ApplyPosition(Vector3 newPosition)
        {
            // No update of position
            if (newPosition == Position)
            {
                return;
            }

            transform.position = newPosition;
            Position = newPosition;
        }

        public override void ApplyRotation(Vector3 newRotation)
        {
            // No update of rotation
            if (Rotation == newRotation)
                return;

            transform.eulerAngles = newRotation;
        }

        public override void ApplyScale(Vector3 newScale)
        {
            // No update of scale
            if (Scale == newScale)
                return;

            Scale = newScale;
            UpdateVertices();
            Collider.size = Scale;
        }

        public override void ApplyTransform(Vector3 newPosition, Vector3 newScale)
        {
            ApplyPosition(newPosition);
            ApplyScale(newScale);
        }

        public override void ApplyTransform(Vector3 newPosition, Vector3 newRotation, Vector3 newScale)
        {
            ApplyPosition(newPosition);
            ApplyRotation(newRotation);
            ApplyScale(newScale);
        }

        private void UpdateVertices(bool set = true)
        {
            Vertices[0] = VecMultiply(mesh_up, Scale);
            Vertices[1] = VecMultiply(mesh_one, Scale);
            Vertices[2] = VecMultiply(mesh_zero, Scale);
            Vertices[3] = VecMultiply(mesh_right, Scale);

            if (set == true)
                CurrentMesh.vertices = Vertices;
        }

    }
}
