using System;
using UnityEngine;

namespace Components
{
    public class DamageOrRegen : MonoBehaviour
    {
        [SerializeField] private int value;

        public void Receiving(GameObject target)
        {
            var healty = target.GetComponent<HealtyComponent>();
            if (healty != null)
            {
                healty.ChangeHealth(value);
            }
        }
    }
}