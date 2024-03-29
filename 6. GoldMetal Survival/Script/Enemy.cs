using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _2Scripts
{
    public class Enemy : MonoBehaviour
    {
        public enum Type {A,B,C,D};
        public Type enemyType;
        public int maxHealth;
        public int curHealth;
        public int score;
        public GameManager manager;
        public Transform target;
        public BoxCollider meleeArea;
        public GameObject bullet;
        public GameObject[] coins;
        public bool isChase;
        public bool isAttack;
        public bool isDead;

        public Rigidbody rigid;
        public BoxCollider boxCollider;
        public MeshRenderer[] meshs;
        public NavMeshAgent nav;
        public Animator anim;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
            meshs = GetComponentsInChildren<MeshRenderer>();
            nav = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();

            if(enemyType != Type.D)
                Invoke("ChaseStart", 2f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Melee"))
            {
                Vector3 reactVec = transform.position - other.transform.position;
                StartCoroutine(OnDamage(reactVec,false));
                Weapon weapon = other.GetComponent<Weapon>();
                curHealth -= weapon.damage;
                //Debug.Log(transform.name + "Melee : " + curHealth);
            }

            else if(other.CompareTag("Bullet"))
            {
                Bullet bullet = other.GetComponent<Bullet>();
                curHealth -= bullet.damage;

                //Debug.Log("Bullet : " + curHealth);
                Vector3 reactVec = transform.position - other.transform.position;
                StartCoroutine(OnDamage(reactVec,false));
            }
        }

        public void HitByGrenade(Vector3 explosionPos)
        {
            curHealth -= 100;
            Vector3 reactVec = transform.position - explosionPos;
            StartCoroutine(OnDamage(reactVec,true));
        }

        IEnumerator OnDamage(Vector3 reactVec, bool isGrenade)
        {
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            //Debug.Log("코루틴스타트");

            if(curHealth > 0)
            {
                foreach (MeshRenderer mesh in meshs)
                    mesh.material.color = Color.white;
            }

            else
            {
                switch(enemyType)
                {
                    case Type.A:
                        manager.enemyCntA--;
                        Debug.Log("A");
                        break;

                    case Type.B:
                        manager.enemyCntB--;
                        Debug.Log("B");
                        break;

                    case Type.C:
                        manager.enemyCntC--;
                        Debug.Log("C");
                        break;

                    case Type.D:
                        manager.enemyCntD--;
                        Debug.Log("D");
                        break;
                }

                if (manager.enemyCntA < 0)
                    manager.enemyCntA = 0;
                
                if (manager.enemyCntB < 0)
                    manager.enemyCntB = 0;
                
                if (manager.enemyCntC < 0)
                    manager.enemyCntC = 0;
                
                if (manager.enemyCntD < 0)
                    manager.enemyCntD = 0;
                
                curHealth = 0;
                foreach (MeshRenderer mesh in meshs)
                    mesh.material.color = Color.gray;
                gameObject.layer = 12;
                isDead = true;
                isChase = false;
                nav.enabled = false;
                anim.SetTrigger("doDie");
                Player player = target.GetComponent<Player>();
                player.score += score;
                int ranCoin = Random.Range(0, 3);
                Instantiate(coins[ranCoin], transform.position, Quaternion.identity);

                

                if (isGrenade)
                {
                    reactVec = reactVec.normalized;
                    reactVec += Vector3.up * 3;
                    rigid.freezeRotation = false;
                    rigid.AddForce(reactVec * 5, ForceMode.Impulse);
                    rigid.AddTorque(reactVec * 15, ForceMode.Impulse);
                }

                else
                {
                    reactVec = reactVec.normalized;
                    reactVec += Vector3.up;
                    rigid.AddForce(reactVec * 5, ForceMode.Impulse);
                }
                yield return new WaitForSeconds(3f);
                Destroy(gameObject);
            }
        }

        void FreezeVelocity()
        {
            if(isChase)
            {
                rigid.angularVelocity = Vector3.zero;
                rigid.velocity = Vector3.zero;
            }
                
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void FixedUpdate()
        {
            Targetting();
            FreezeVelocity();
        }

        // Update is called once per frame
        void Update()
        {
            if (nav.enabled && enemyType != Type.D)
            {
                nav.SetDestination(target.position);
                nav.isStopped = !isChase;
            }
        }

        void ChaseStart()
        {
            isChase = true;
            anim.SetBool("isWalk", true);
        }

        void Targetting()
        {
            if(!isDead && enemyType != Type.D)
            {
                float targetRadius = 0f;
                float targetRange = 0f;

                switch (enemyType)
                {
                    case Type.A:
                        targetRadius = 1.5f;
                        targetRange = 3f;
                        break;

                    case Type.B:
                        targetRadius = 1f;
                        targetRange = 12f;
                        break;

                    case Type.C:
                        targetRadius = 0.5f;
                        targetRange = 25f;
                        break;
                }

                RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius,
                    transform.forward, targetRange,
                    LayerMask.GetMask("Player"));

                if (rayHits.Length > 0 && !isAttack)
                {
                    StartCoroutine(Attack());
                }

            
            }
        }
        

        IEnumerator Attack()
        {
        
            isChase = false;
            isAttack = true;
            anim.SetBool("isAttack", true);

            switch (enemyType)
            {
                case Type.A:
                    yield return new WaitForSeconds(0.2f);
                    meleeArea.enabled = true;

                    yield return new WaitForSeconds(1f);
                    meleeArea.enabled = false;

                    yield return new WaitForSeconds(1f);
                    break;

                case Type.B:
                    yield return new WaitForSeconds(0.1f);
                    rigid.AddForce(transform.forward * 20, ForceMode.Impulse);
                    meleeArea.enabled = true;

                    yield return new WaitForSeconds(0.5f);
                    rigid.velocity = Vector3.zero;
                    meleeArea.enabled = false;

                    yield return new WaitForSeconds(2f);
                    break;

                case Type.C:
                    yield return new WaitForSeconds(0.5f);
                    GameObject instantBullet = Instantiate(bullet,
                        new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z),
                        Quaternion.Euler(0, transform.rotation.eulerAngles.y - 90f, 0));
                    Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                    rigidBullet.velocity = transform.forward * 20;

                    yield return new WaitForSeconds(2f);
                    break;
            }

            isChase = true;
            isAttack = false;
            anim.SetBool("isAttack", false);

        }
    }
}
