using System.Collections;
using UnityEngine;

namespace _2Scripts
{
    public class Grenade : MonoBehaviour
    {
        public GameObject meshObj;
        public GameObject effectObj;
        public Rigidbody rigid;

        public AudioSource grenadeSound;

        // Start is called before the first frame update
        private void Awake()
        {
            grenadeSound = GetComponent<AudioSource>();
        }

        void Start()
        {
            StartCoroutine(Expolsion());
        }

        IEnumerator Expolsion()
        {
            yield return new WaitForSeconds(3f);
            grenadeSound.Play();
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
            meshObj.SetActive(false);
            effectObj.SetActive(true);

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 15,
                Vector3.up, 0f,
                LayerMask.GetMask("Enemy"));

            foreach(RaycastHit hitObj in rayHits)
            {
                hitObj.transform.GetComponent<Enemy>().HitByGrenade(transform.position);
            }

            Destroy(gameObject, 5);

        }
    }
}
