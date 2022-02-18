using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class HealtyComponent : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private UnityEvent OnDie;
        [SerializeField] private UnityEvent OnDamage;

        public void ChangeHealth(int value)
        {
            health += value;
            if (value < 0)
            {
                OnDamage?.Invoke();
            }
            if (health <= 0)
            {
                OnDie?.Invoke();
            }
        }
    }
}