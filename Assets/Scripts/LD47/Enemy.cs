using UnityEngine;
using UnityEngine.Serialization;

namespace LD47
{
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        private Transform _player;

        private bool _chase = false;

        public float chaseDistance = 50f;
        public float combatDistance = 10f;

        public float chaseSpeed = 1f;

        private bool isFighting = false;

        private Weapon _weapon;

        private bool _isInCombatRange;

        [FormerlySerializedAs("whatIsPlayer")] public LayerMask scanMask;
        private bool _hasWeapon;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _weapon = GetComponent<Weapon>();
            _hasWeapon = _weapon != null;

            GetComponent<Health>().OnDeath.AddListener(() =>
            {
                // TODO: Add VFX and shit
                Destroy(gameObject);
            });
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _player.position) <= chaseDistance)
            {
                RaycastHit hit;
                var relativePos = _player.position - transform.position;
                var rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                Debug.DrawRay(transform.position, relativePos * 1000, Color.magenta);

                // When we are out of combat distance, stop fighting
                _isInCombatRange = Vector3.Distance(transform.position, _player.position) <= combatDistance;
                if (!_isInCombatRange && isFighting)
                {
                    StopFighting();
                }

                if (Physics.Raycast(transform.position, relativePos, out hit, 100f, scanMask))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        transform.rotation = rotation;

                        if (_hasWeapon && _isInCombatRange && !isFighting)
                        {
                            StartFighting();
                        }
                        else if (!isFighting)
                        {
                            transform.Translate(Vector3.forward * (chaseSpeed * Time.deltaTime));
                        }
                    }
                    else
                    {
                        if (_hasWeapon && isFighting)
                            StopFighting();
                    }
                }
            }
        }

        private void StartFighting()
        {
            if (_hasWeapon)
            {
                isFighting = true;
                _weapon.StartFire();
            }
        }

        private void StopFighting()
        {
            if (_hasWeapon)
            {
                _weapon.StopFire();
                isFighting = false;
            }
        }
    }
}