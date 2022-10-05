using UnityEngine;
using UnityEngine.AI;

namespace _2Scripts
{
    public class BoseMissile : Bullet
    {

        public Transform target;
        NavMeshAgent nav;

        // Start is called before the first frame update
        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            nav.SetDestination(target.position);
        }
    }
}
