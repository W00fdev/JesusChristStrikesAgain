using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Christ.Core
{
    public class MeshCreator : MonoBehaviour
    {
        public Material MeshFriendMaterial;
        public Material MeshEnemyMaterial;
        public Transform FriendlyZoneParent;
        public Transform EnemyZoneParent;

        [SerializeField] private Vector3 _meshPosition = new Vector3(6, -3, 0);
        [SerializeField] private Vector3 _meshRotation = new Vector3(0, 0, 0);
        [SerializeField] private Vector3 _meshScale = new Vector3(2, 2, 1);

        [SerializeField] private Vector3 _meshPosition2 = new Vector3(-5, 3, 0);
        [SerializeField] private Vector3 _meshRotation2 = new Vector3(0, 0, 45);
        [SerializeField] private Vector3 _meshScale2 = new Vector3(1, 1, 1);

        GenericMesh _mesh;
        GenericMesh _mesh2;

        private void Start()
        {
            _mesh = new GenericMeshQuad(_meshPosition, _meshRotation, _meshScale, FriendlyZoneParent, MeshFriendMaterial);
            _mesh2 = new GenericMeshQuad(_meshPosition2, _meshRotation2, _meshScale2, EnemyZoneParent, MeshEnemyMaterial);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                _meshPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _meshPosition.z = 0f;
            }

            if (Input.GetMouseButton(1))
            {
                _meshPosition2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _meshPosition2.z = 0f;
            }

            _mesh.ApplyTransform(_meshPosition, _meshRotation, _meshScale);
            _mesh2.ApplyTransform(_meshPosition2, _meshRotation2, _meshScale2);
        }
    }

}