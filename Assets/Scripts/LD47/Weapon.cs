using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace LD47
{
    public class Weapon : MonoBehaviour
    {
        public bool isHitScan = true;

        public int shotsPerSecond = 1;

        public bool isAutomatic = true;

        public LayerMask targetMask;

        public int hitScanDamage = 1;

        public UnityEvent<Vector3> OnWeaponHit;
        
        private bool firing = false;
        
        

        public void StartFire()
        {
            if (!firing && isAutomatic)
            {
                firing = true;
                StartCoroutine(nameof(AutoFire));
            }
        }

        public void StopFire()
        {
            firing = false;
        }

        IEnumerator AutoFire()
        {
            while (firing)
            {
                Fire();
                yield return new WaitForSeconds(1f / shotsPerSecond);
            }
        }

        void Fire()
        {
            if (isHitScan)
            {
                RaycastHit hit;
                Debug.DrawRay(transform.position, transform.forward * 1000, Color.red, 0.2f);
                if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, targetMask))
                {
                    if (hit.collider)
                    {
                        var targetGo = hit.transform.gameObject;
                        var health = targetGo.gameObject.GetComponent<Health>();
                        if (health != null)
                        {
                            OnWeaponHit.Invoke(hit.point);
                            health.TakeDamage(hitScanDamage, transform);
                        }
                    }
                }
            }
        }
    }
}