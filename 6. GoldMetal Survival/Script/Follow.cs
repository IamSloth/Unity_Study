using UnityEngine;

namespace _2Scripts
{
    public class Follow : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = target.position + offset;
        }
    }
}