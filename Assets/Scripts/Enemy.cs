using UnityEngine;

public class Enemy : MonoBehaviour
{
    const string SPAWN_AT_RUNTIME = "SpawnAtRuntime";
    [Header("Enemy Stats")]
    [Tooltip("How many points each hit earns")]
    [SerializeField] int _scorePerHit = 25;

    [Tooltip("Each player laser shot removes one hitpoint")]
    [SerializeField] int _hitPoints = 4;
    
    
    [Header("Particle effects")]
    [Tooltip("Particle Effect for laser strikes")]
    [SerializeField] GameObject _hitVFX;
    [Tooltip("Explosion effect on death")]
    [SerializeField] GameObject _deathVFX;
    Transform _spawnAtRuntime;
    ScoreBoard _scoreBoard;

    void Awake ()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
        _spawnAtRuntime = GameObject.FindWithTag(SPAWN_AT_RUNTIME).transform;
    }
        
    void Start ()
    {
        AddRigidBody();
    } 

    void AddRigidBody()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;
        }
    }
    void OnParticleCollision(GameObject other) => ProcessHit();

    void ProcessHit ()
    {
        InstantiateParticleEffect(_hitVFX);
        _hitPoints--;
        _scoreBoard.IncreaseScore(_scorePerHit);
        if (_hitPoints < 1) KillEnemy();
    }
    
    void KillEnemy ()
    {
        InstantiateParticleEffect(_deathVFX);
        Destroy(gameObject);
    }

    void InstantiateParticleEffect(GameObject effect) // sets particle effects to spawn in a parent container
    {
        GameObject vfx = Instantiate(effect, transform.position, Quaternion.identity);
        vfx.transform.parent = _spawnAtRuntime;
    }
}