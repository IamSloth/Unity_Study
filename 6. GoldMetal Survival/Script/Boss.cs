using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _2Scripts
{
    public class Boss : Enemy
    {
        public GameObject missile;
        public Transform missilePortA;
        public Transform missilePortB;

        Vector3 lookVec;
        Vector3 tauntVec;
        public bool isLook;

        // Start is called before the first frame update
        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
            meshs = GetComponentsInChildren<MeshRenderer>();
            nav = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();

            nav.isStopped = true;
            StartCoroutine(Think());
        }

        // Update is called once per frame
        void Update()
        {
            if (isDead)
            {
                StopAllCoroutines();
                return;
            }
            

            if (isLook)
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                lookVec = new Vector3(h, 0, v) * 5f;
                transform.LookAt(target.position + lookVec);
            }

            else
                nav.SetDestination(tauntVec);
        }

        IEnumerator Think()
        {
            yield return new WaitForSeconds(0.1f);

            int ranAction = Random.Range(0, 5);
            switch (ranAction)
            {
                case 0:
                case 1:
                    StartCoroutine(MissileShot());
                    break;
                case 2:
                case 3:
                    StartCoroutine(RockShot());
                    break;
                case 4:
                    StartCoroutine(Taunt());
                    break;

            }
        }

        IEnumerator MissileShot()
        {
            anim.SetTrigger("doShot");
            yield return new WaitForSeconds(0.2f);
            GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
            BoseMissile boseMissileA = instantMissileA.GetComponent<BoseMissile>();
            boseMissileA.target = target;

            yield return new WaitForSeconds(0.3f);
            GameObject instantMissileB = Instantiate(missile, missilePortB.position, missilePortB.rotation);
            BoseMissile boseMissileB = instantMissileB.GetComponent<BoseMissile>();
            boseMissileB.target = target;

            yield return new WaitForSeconds(2f);
            StartCoroutine(Think());
        }
    
        IEnumerator RockShot()
        {
            isLook = false;
            anim.SetTrigger("doBigShot");
            Vector3 rockPosition = transform.position + transform.forward * 10;
            Instantiate(bullet,rockPosition, transform.rotation);
            yield return new WaitForSeconds(3f);
            isLook = true;
            StartCoroutine(Think());
        }
    
        IEnumerator Taunt()
        {
            tauntVec = target.position + lookVec;
            isLook = false;
            nav.isStopped = false;
            boxCollider.enabled = false;
            anim.SetTrigger("doTaunt");
            yield return new WaitForSeconds(1.5f);
            meleeArea.enabled = true;
            yield return new WaitForSeconds(0.5f);
            meleeArea.enabled = false;

            yield return new WaitForSeconds(1f);
            isLook = true;
            nav.isStopped = true;
            boxCollider.enabled = true;
            StartCoroutine(Think());
        }
    }
}
