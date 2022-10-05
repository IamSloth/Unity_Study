using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace _2Scripts
{
    public class Player : MonoBehaviour
    {

        public GameObject[] weapons;
        public bool[] hasWeapons;

        public GameObject[] grenades;
        public int hasGrenades;

        public GameObject grenadeObj;

        public AudioSource[] actionSound;
        public enum Clips { jump, reload, dead, getItem, getCoin, myThrow, equipment, buy}


        public Camera followCamera;

        public GameManager manager;

        public Joystick joystick;

        float hAxis;
        float vAxis;

        bool wDown;
        bool jDown;
        bool fDown;
        bool gDown;
        bool rDown;
        bool iDown;
        bool sDown1;
        bool sDown2;
        bool sDown3;
        bool isBoder;
        bool isDamage;
        bool isDead;

        public void FButtonOn()
        {
            fDown = true;
            Attack();
        }

        public void FButtonOff()
        {
            fDown = false;
        }
        
        public void IButtonOn()
        {
            iDown = true;
            Interaction();
        }

        public void IButtonOff()
        {
            iDown = false;
        }
        
        public void JButtonOn()
        {
            jDown = true;
            Dodge();
            //Debug.Log(jDown);
        }

        public void JButtonOff()
        {
            jDown = false;
            //Debug.Log(jDown);
        }

        public void Sdown1On()
        {
            sDown1 = true;
            Swap();
        }
        
        public void Sdown1Off()
        {
            sDown1 = false;
        }
        
        public void Sdown2On()
        {
            sDown2 = true;
            Swap();
        }
        
        public void Sdown2Off()
        {
            sDown2 = false;
        }
        
        public void Sdown3On()
        {
            sDown3 = true;
            Swap();
        }
        
        public void Sdown3Off()
        {
            sDown3 = false;
        }

        public void RdownOn()
        {
            rDown = true;
            Reload();
        }

        public void RdownOff()
        {
            rDown = false;
        }
        
        public void GdownOn()
        {
            gDown = true;
            Grenade();
        }

        public void GdownOff()
        {
            gDown = false;
        }

        public int ammo;
        public int coin;
        public int health;
        public int score;

        public int maxAmmo;
        public int maxCoin;
        public int maxHealth;
        public int maxHasGrenades;

        public bool isGrenade;
        public bool isDodge;
        public bool isSwap;
        public bool isFireReady = true;
        public bool isReload;
        public bool isShop;
        public bool isWeaponShop;
        public bool isItemShop;

        public float speed;
        Vector3 moveVec;
        Vector3 dodgeVec;

        Animator anim;
        Rigidbody rigid;
        MeshRenderer[] meshs;

        GameObject nearObject;
        public Weapon equipWeapon;
        int equipWeaponIndex = -1;

        float fireDelay;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            rigid = GetComponent<Rigidbody>();
            meshs = GetComponentsInChildren<MeshRenderer>();

            Debug.Log(PlayerPrefs.GetInt("MaxScore"));
            //PlayerPrefs.SetInt("MaxScore", 112500);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetInput();
            Move();
            Turn();
            Dodge();
            Interaction();
            Swap();
            Attack();
            Grenade();
            Reload();
        }

        private void FixedUpdate()
        {
            FreezeRotation();
            StopToWall();
        }

        void GetInput()
        {
            hAxis = Input.GetAxisRaw("Horizontal") + joystick.Horizontal;
            vAxis = Input.GetAxisRaw("Vertical") + joystick.Vertical;
            wDown = Input.GetButton("Walk");
            jDown = Input.GetButtonDown("Jump");
            //fDown = Input.GetButton("Fire1");
            //gDown = Input.GetButtonDown("Fire2");
            rDown = Input.GetButtonDown("Reload");
            iDown = Input.GetButtonDown("Interaction");
            sDown1 = Input.GetButtonDown("Swap1");
            sDown2 = Input.GetButtonDown("Swap2");
            sDown3 = Input.GetButtonDown("Swap3");

        }

        void Move()
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            if (isDodge)
            {
                moveVec = dodgeVec;
            }

            if (isSwap || !isFireReady || isReload || isDead)
                moveVec = Vector3.zero;

            if (wDown && !isBoder)
                transform.position += moveVec * speed * 0.3f * Time.deltaTime;
            else if(!wDown && !isBoder)
                transform.position += moveVec * speed * Time.deltaTime;

            anim.SetBool("isRun", moveVec != Vector3.zero);
            anim.SetBool("isWalk", wDown);
        }

        void Turn()
        {
            transform.LookAt(transform.position + moveVec);

            /*
             Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if(fDown && !isDead)
            {
                if (Physics.Raycast(ray, out rayHit, 100))
                {
                    Vector3 nextVec = rayHit.point - transform.position;
                    nextVec.y = 0;
                    transform.LookAt(transform.position + nextVec);
                }
            }
            */
        
        }

        void Attack()
        {
            if (equipWeapon == null)
                return;

            fireDelay += Time.deltaTime;
            isFireReady = equipWeapon.rate < fireDelay;

            if(fDown && isFireReady && !isDodge && !isSwap && !isShop && !isDead &&!isReload)
            {
                equipWeapon.Use();
                anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
                fireDelay = 0;
            }
        }

        void Grenade()
        {
            if (hasGrenades == 0)
                return;

            if(gDown && !isGrenade && !isReload && !isSwap && !isShop && !isDead)
            {
                isGrenade = true;
                actionSound[(int)Clips.myThrow].Play();
                GameObject instantGrenade = Instantiate(grenadeObj, transform.position, transform.rotation);
                Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
                rigidGrenade.AddForce(transform.forward, ForceMode.Impulse);
                rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

                hasGrenades--;
                grenades[hasGrenades].SetActive(false);
                Invoke("GrenadeOut", 0.5f);
                /*
                 Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit, 100))
                {
                    Vector3 nextVec = rayHit.point - transform.position;
                    nextVec.y += 10;

                    GameObject instantGrenade = Instantiate(grenadeObj, transform.position, transform.rotation);
                    Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
                    rigidGrenade.AddForce(nextVec/2, ForceMode.Impulse);
                    rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

                    hasGrenades--;
                    grenades[hasGrenades].SetActive(false);
                    
                }
                */
            }
        }

        void GrenadeOut()
        {
            isGrenade = false;
        }

        void Dodge()
        {
            if (jDown && moveVec != Vector3.zero && !isDodge && !isSwap && !isShop && !isDead)
            {
                dodgeVec = moveVec;
                speed *= 2;
                anim.SetTrigger("doDodge");
                isDodge = true;
                actionSound[(int)Clips.jump].Play();

                Invoke("DodgeOut", 0.4f);

            }
        }

        void DodgeOut()
        {
            speed *= 0.5f;
            isDodge = false;
        }

        void Interaction()
        {
            if(iDown && nearObject != null && !isDodge && !isDead)
            {
                if(nearObject.tag == "Weapon")
                {
                    Item item = nearObject.GetComponent<Item>();
                    int weaponIndex = item.value;
                    hasWeapons[weaponIndex] = true;
                    switch (weaponIndex)
                    {
                        case 0:
                            manager.statusTxt.gameObject.SetActive(true);
                            manager.statusOutTxt.gameObject.SetActive(true);
                            manager.statusTxt.text = "+ 망치";
                            manager.statusOutTxt.text = "+ 망치";
                            Invoke("StatusTextOff",2);
                            break;
                        
                        case 1:
                            manager.statusTxt.gameObject.SetActive(true);
                            manager.statusOutTxt.gameObject.SetActive(true);
                            manager.statusTxt.text = "+ 피스톨";
                            manager.statusOutTxt.text = "+ 피스톨";
                            Weapon weapon1 = weapons[1].GetComponent<Weapon>();
                            ammo += weapon1.maxAmmo;
                            Invoke("StatusTextOff",2);
                            break;
                        
                        case 2:
                            manager.statusTxt.gameObject.SetActive(true);
                            manager.statusOutTxt.gameObject.SetActive(true);
                            manager.statusTxt.text = "+ 머신건";
                            manager.statusOutTxt.text = "+ 머신건";
                            Weapon weapon2 = weapons[2].GetComponent<Weapon>();
                            ammo += weapon2.maxAmmo;
                            Invoke("StatusTextOff",2);
                            break;
                        
                    }
                    actionSound[(int)Clips.equipment].Play();

                    Destroy(nearObject);
                }

                else if(nearObject.tag == "Shop")
                {
                    Shop shop = nearObject.GetComponent<Shop>();
                    isShop = true;
                    shop.Enter(this);
               
                }
            }
        }
        
        void StatusTextOff()
        {
            manager.statusTxt.gameObject.SetActive(false);
            manager.statusOutTxt.gameObject.SetActive(false);
        }

        void Swap()
        {
            if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
                return;
            if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
                return;
            if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
                return;

            int weaponIndex = -1;
            if (sDown1) weaponIndex = 0;
            if (sDown2) weaponIndex = 1;
            if (sDown3) weaponIndex = 2;

            if((sDown1 || sDown2 || sDown3) && !isDodge && !isShop && !isDead)
            {
                actionSound[(int)Clips.equipment].Play();
                if (equipWeapon != null)
                    equipWeapon.gameObject.SetActive(false);
                equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
                equipWeapon.gameObject.SetActive(true);
                anim.SetTrigger("doSwap");
                isSwap = true;
                Invoke("SwapOut", 0.4f);
                equipWeaponIndex = weaponIndex;
            }

       
        }

        void SwapOut()
        {
            isSwap = false;
        }

        void Reload()
        {
            if (equipWeapon == null)
                return;

            if (equipWeapon.type == Weapon.Type.Melee)
                return;

            if (ammo == 0)
                return;

            if(rDown && !isDodge && !isSwap && isFireReady && !isShop && !isDead && !isReload)
            {
                actionSound[(int)Clips.reload].Play();
                manager.statusTxt.gameObject.SetActive(true);
                manager.statusOutTxt.gameObject.SetActive(true);
                manager.statusTxt.text = "장전중...";
                manager.statusOutTxt.text = "장전중...";
                anim.SetTrigger("doReload");
                isReload = true;
                Invoke("ReloadOut", 3f);

            }
        }

        void ReloadOut()
        {
            manager.statusTxt.gameObject.SetActive(false);
            manager.statusOutTxt.gameObject.SetActive(false);
            int reAmmo = ammo < equipWeapon.maxAmmo ? ammo : equipWeapon.maxAmmo;
            equipWeapon.curAmmo = equipWeapon.maxAmmo;
            ammo -= reAmmo;
            isReload = false;
        }

        void FreezeRotation()
        {
            rigid.angularVelocity = Vector3.zero;
            rigid.velocity = Vector3.zero;
        }

        void StopToWall()
        {
            Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
            isBoder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Floor")
            {

            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Item")
            {
            
                Item item = other.GetComponent<Item>();
                switch(item.type)
                {
                
                    case Item.Type.Ammo:
                        ammo += item.value;
                        actionSound[(int)Clips.getItem].Play();
                        if (ammo > maxAmmo)
                            ammo = maxAmmo;
                        break;

                    case Item.Type.Heart:
                        health += item.value;
                        actionSound[(int)Clips.getItem].Play();
                        if (health > maxHealth)
                            health = maxHealth;
                        break;

                    case Item.Type.Coin:
                        coin += item.value;
                        actionSound[(int)Clips.getCoin].Play();
                        if (coin > maxCoin)
                            coin = maxCoin;
                        break;

                    case Item.Type.Grenade:
                        if (hasGrenades == maxHasGrenades)
                            return;
                        actionSound[(int)Clips.getItem].Play();
                        grenades[hasGrenades].SetActive(true);
                        hasGrenades += item.value;
                        if(hasGrenades>maxHasGrenades)
                            hasGrenades = maxHasGrenades;
                        break;
                }
                Destroy(other.gameObject);
            }

            else if(other.tag == "EnemyBullet")
            {
                if(!isDamage)
                {
                    Bullet enemyBullet = other.GetComponent<Bullet>();
                    health -= enemyBullet.damage;

                    bool isBossAtk = other.name == "Boss Melee Area";
                    StartCoroutine(OnDamage(isBossAtk));
                }
                if (other.GetComponent<Rigidbody>() != null)
                    Destroy(other.gameObject);

            }
        }
    
        IEnumerator OnDamage(bool isBossAtk)
        {
            isDamage = true;
            foreach(MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.yellow;
            }

            if(isBossAtk)
            {
                rigid.AddForce(transform.forward * -100f, ForceMode.Impulse);
            }

            if (health <= 0 && !isDead)
            {
                OnDie();
            }

            yield return new WaitForSeconds(1f);
        
            isDamage = false;
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }

            if (isBossAtk)
                rigid.velocity = Vector3.zero;

        
        }

        void OnDie()
        {
            actionSound[(int)Clips.dead].Play();
            anim.SetTrigger("doDie");
            isDead = true;
            manager.GameOver();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Weapon" || other.tag == "Shop")
                nearObject = other.gameObject;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Weapon"))
            {
                if(nearObject != null)
                {
                    nearObject = null;
                }
            }

            else if (other.CompareTag("Shop"))
            {
                if(nearObject != null)
                {
                    Shop shop = nearObject.GetComponent<Shop>();
                    shop.Exit(this);
                    nearObject = null;
                }
            }
        }
    }
}
