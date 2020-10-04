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

        public GameObject deathParticles;
        private MeshRenderer _meshRenderer;

        private Texture _mainTexture;
        private Texture _emissiveTexture;
        private Color _emissiveColor;
        private static readonly int EmissionMap = Shader.PropertyToID("_EmissionMap");
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private GameFeel _gameFeel;
        
        private void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _weapon = GetComponent<Weapon>();
            _hasWeapon = _weapon != null;
            _meshRenderer = GetComponentInChildren<MeshRenderer>();

            _gameFeel = FindObjectOfType<GameFeel>();
            
            _mainTexture = _meshRenderer.material.mainTexture;
            _emissiveTexture = _meshRenderer.material.GetTexture (EmissionMap);
            _emissiveColor = _meshRenderer.material.GetColor(EmissionColor);
            
            GetComponent<Health>().OnDeath.AddListener(() =>
            {
                if (deathParticles != null)
                {
                    _gameFeel.FreezeFrame(GameFeel.FreezeFrameType.Long);
                    Instantiate(deathParticles, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            });
            
            GetComponent<Health>().OnDamageTaken.AddListener((int dmg, Vector3 pos) =>
            {
                _gameFeel.FreezeFrame(GameFeel.FreezeFrameType.Short);
                _meshRenderer.material.mainTexture = null;
                _meshRenderer.material.SetTexture (EmissionMap, null);
                _meshRenderer.material.SetColor(EmissionColor, Color.white);
                Invoke(nameof(ResetMaterial), 0.1f);
            });
        }

        void ResetMaterial()
        {
            _meshRenderer.material.mainTexture = _mainTexture;
            _meshRenderer.material.SetTexture (EmissionMap, _emissiveTexture);
            _meshRenderer.material.SetColor(EmissionColor, _emissiveColor);
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
                else
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