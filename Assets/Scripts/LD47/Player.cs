using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

namespace LD47
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CinemachineImpulseSource))]
    [RequireComponent(typeof(Weapon))]
    public class Player : MonoBehaviour
    {
        private Health _health;

        private CinemachineImpulseSource _cinemachineImpulse;

        public Weapon weapon;
        
        public LineRenderer weaponTraceRenderer;

        public Transform muzzleTransform;
        
        private void Awake()
        {
            
            weaponTraceRenderer.gameObject.SetActive(false);
            _health = GetComponent<Health>();
            _cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
            _health.OnDeath.AddListener(() =>
            {
                //Debug.Log("Oh noes, player dead!!!!!!!!!!!!!!!!!!!!!");
            });
            
            _health.OnDamageTaken.AddListener((int damage, Vector3 source) =>
            {
                _cinemachineImpulse.GenerateImpulse(source - transform.position * ((float) damage / _health.maxHealth));
            });
            
            weapon.OnWeaponHit.AddListener((Vector3 hitPos) =>
            {
                StartCoroutine(ShowLineCast(hitPos));
            });
        }

        private IEnumerator ShowLineCast(Vector3 hitPos)
        {
            weaponTraceRenderer.gameObject.SetActive(true);
            var positions = new[] {muzzleTransform.position, hitPos};
            weaponTraceRenderer.SetPositions(positions);
            yield return new WaitForSeconds(0.1f);
            weaponTraceRenderer.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                weapon.StartFire();
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                weapon.StopFire();
            }
        }
    }
}