using UnityEngine;

namespace LD47
{
    public class Bob : MonoBehaviour
    {
        Vector3 startPos;
        public float amplitude = 10f;
        public float period = 5f;

        public Rigidbody useRigidbody;
        protected void Start() {
            startPos = transform.localPosition;
        }
    
        protected void Update() {
            float theta = Time.time / period;
            float distance = amplitude * Mathf.Sin(theta);

            if (useRigidbody != null)
            {
                useRigidbody.MovePosition(transform.position + Vector3.up * distance);
            }
            else
            {
                transform.localPosition = startPos + Vector3.up * distance;    
            }
            
        }
    }
}