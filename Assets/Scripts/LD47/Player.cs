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
        
        private void Awake()
        {
            Health = GetComponent<Health>();
            _cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
            Health.OnDeath.AddListener(() =>
            {
                //Debug.Log("Oh noes, player dead!!!!!!!!!!!!!!!!!!!!!");
            });
            
            Health.OnDamageTaken.AddListener((int damage, Vector3 source) =>
            {
               // _cinemachineImpulse.GenerateImpulse();
            });

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
        }
    }
}