using UnityEngine;

namespace _2Scripts
{
    public class Item : MonoBehaviour
    {
        public enum Type { Ammo, Coin, Grenade, Heart, Weapon };
        public Type type;
        public int value;

        Rigidbody rb;
        SphereCollider sphereCollider;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            sphereCollider = GetComponent<SphereCollider>();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up * (20 * Time.deltaTime));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Floor")
            {
                rb.isKinematic = true;
                sphereCollider.enabled = false;
            }
        }
    }
}
