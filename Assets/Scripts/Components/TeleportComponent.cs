using UnityEngine;

namespace Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _teleportPos;

        public void Teleport(GameObject gameObject)
        {
            gameObject.transform.position = _teleportPos.transform.position;
        }
    }
}