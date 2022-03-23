using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float _destroyDelay = 3f;
    void Start() => Destroy(gameObject, _destroyDelay);
}