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
        
        public Transform muzzleTransform;
        
        private void Awake()
        {
            _health = GetComponent<Health>();
            _cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
            _health.OnDeath.AddListener(() =>
            {
                //Debug.Log("Oh noes, player dead!!!!!!!!!!!!!!!!!!!!!");
            });
            
            _health.OnDamageTaken.AddListener((int damage, Vector3 source) =>
            {
               // _cinemachineImpulse.GenerateImpulse();
            });

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