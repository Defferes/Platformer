using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class DestroyComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private UnityEvent _AfterDestroy;

        public void OnDestroy()
        {
            Destroy(_target);
            _AfterDestroy?.Invoke();
        }
    }
}