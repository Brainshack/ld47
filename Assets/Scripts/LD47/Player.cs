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
        public Health Health { get; private set; }

        private CinemachineImpulseSource _cinemachineImpulse;

        public Weapon weapon;
        
        public Transform muzzleTransform;

        public Material pathHighlightMaterial;

        public LayerMask highlightMask;
        
        private void Awake()
        {
            Health = GetComponent<Health>();
            _cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
        }

        public void resetPlayer()
        {
            Health.ResetHealth();
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

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down * 50, out hit, highlightMask))
            {
                hit.collider.gameObject.GetComponentInChildren<MeshRenderer>().material = pathHighlightMaterial;
            }
        }
    }
}