using UnityEngine;

namespace LD47
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {

        public int damage;

        public float speed;

        public float knockBackForce = 500f;
        
        void Update()
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        private void OnCollisionEnter(Collision other)
        {
            var health = other.gameObject.GetComponent<Health>();
            var rb = other.gameObject.GetComponent<Rigidbody>();
            if (health != null)
            {
                health.TakeDamage(damage, transform);
            }

            if (rb != null)
            {
                if (!other.transform.CompareTag("Player"))
                {
                    Debug.Log(transform.forward * knockBackForce);
                }
                rb.AddForce(transform.forward * knockBackForce);
            }
            
            Destroy(gameObject);
        }
    }
}