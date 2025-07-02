using UnityEngine;

namespace Cavrnus.SpatialConnector.Samples.CollaborationShowcase
{
    public class CavrnusUIRotate : MonoBehaviour
    {
        [Header("Rotation Settings")]
        public Vector3 rotationAxis = new Vector3(0f, 0f, 1f);
        public float rotationSpeed = 90f;

        private void Update()
        {
            transform.Rotate(rotationAxis.normalized * (rotationSpeed * Time.deltaTime));
        }
    }
}