using UnityEngine;

namespace LD47
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {

        public int damage;

        public float speed;

        public float knockBackForce = 500f;

        private void Start()
        {
            Invoke("Kill", 20f);
        }

        void Kill()
        {
            Destroy(gameObject);
        }
        
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
                rb.AddForce(transform.forward * knockBackForce);
            }
            
            Destroy(gameObject);
        }
    }
}