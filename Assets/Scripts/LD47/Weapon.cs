using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace LD47
{
    public class Weapon : MonoBehaviour
    {
        public bool isHitScan = true;

        public float shotsPerSecond = 2f;
        public float autoFireShotsPerSecond = 0.2f;

        public bool isAutomatic = true;

        public LayerMask targetMask;

        public int hitScanDamage = 1;

        public UnityEvent<Vector3> OnWeaponHit;

        [FormerlySerializedAs("enforceFireRate")] public bool enforceAutoFireRate;
        
        private bool firing = false;

        public float maxDeviation = 2f;

        private float earliestNextShot = 0;

        private float lastShotTime;
        
        public void StartFire()
        {
            firing = true;
        }

        public void StopFire()
        {
            firing = false;
            earliestNextShot = lastShotTime + 1f / shotsPerSecond;
        }

        void Update()
        {
            if (!firing) return;
            if (Time.time < earliestNextShot) return;

            Debug.Log($"Time since we last shot {Time.time - lastShotTime}");
            
            lastShotTime = Time.time;
            
            if (isHitScan)
            {
                RaycastHit hit;
                
                Vector3 forwardVector = Vector3.forward;
                float deviation = Random.Range(0f, maxDeviation);
                float angle = Random.Range(0f, 360f);
                forwardVector = Quaternion.AngleAxis(deviation, Vector3.up) * forwardVector;
                forwardVector = Quaternion.AngleAxis(angle, Vector3.forward) * forwardVector;
                forwardVector = transform.rotation * forwardVector;
                
                Debug.DrawRay(transform.position, forwardVector * 1000, Color.red, 0.2f);
                if (Physics.Raycast(transform.position, forwardVector, out hit, 100f, targetMask))
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

            if (!isAutomatic)
            {
                firing = false;
            }
            else
            {
                earliestNextShot = lastShotTime + 1f / autoFireShotsPerSecond;
            }
        }
    }
}