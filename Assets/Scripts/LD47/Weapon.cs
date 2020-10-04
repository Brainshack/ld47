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
        public float shotsPerSecond = 2f;
        public float autoFireShotsPerSecond = 0.2f;

        public bool isAutomatic = true;

        public UnityEvent<Vector3> OnWeaponHit;

        public float maxDeviation = 2f;

        public GameObject projectilePrefab;

        public Transform[] muzzleTransforms;
        
        private float earliestNextShot = 0;

        private float lastShotTime;

        private bool firing = false;

        private AudioManager _audio;

        public AudioClip shotSound;

        private void Awake()
        {
            _audio = FindObjectOfType<AudioManager>();
        }

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

            lastShotTime = Time.time;

            var index = Random.Range(0, muzzleTransforms.Length);
            
            var spawnTransform = muzzleTransforms[index];
            if (gameObject.CompareTag("Player"))
                _audio.PlaySound(shotSound, transform.position, true);
            else
                _audio.PlaySound(shotSound, transform.position);
            
            Instantiate(projectilePrefab, spawnTransform.position, spawnTransform.rotation);
            
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