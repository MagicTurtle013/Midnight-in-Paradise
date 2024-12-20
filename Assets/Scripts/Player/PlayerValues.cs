using UnityEngine;

namespace MpPlayerValues
{
    public class PlayerValues : MonoBehaviour
    {
        [field: SerializeField]
        public Camera FirstPersonCamera {get; private set;}

        public static PlayerValues Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
