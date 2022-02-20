using UnityEngine;

namespace Components
{
    public class CreateDustParticles : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefabs;
        public void SpawnDust()
        {
            Instantiate(_prefabs, _target);
        }
    }
}