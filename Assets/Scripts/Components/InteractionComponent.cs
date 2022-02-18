using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class InteractionComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _event;

        public void Interact()
        {
            _event?.Invoke();
        }
    }
}