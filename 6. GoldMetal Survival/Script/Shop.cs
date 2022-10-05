using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _2Scripts
{
    public class Shop : MonoBehaviour
    {
        public bool isItemShop;

        public RectTransform uiGroup;
        public Animator anim;

        public GameObject[] itemObj;
        public int[] itemPrice;
        public Transform[] itemPos;
        public string[] talkData;
        public Text talkText;

        Player enterPlayer;
        public BGMManager bgm;
        AudioSource buySound;

        private void Awake()
        {
            buySound = GetComponent<AudioSource>();
        }

        public void Enter(Player player)
        {
            enterPlayer = player;
            enterPlayer.isShop = true;
            uiGroup.anchoredPosition = Vector3.zero;
            if (isItemShop)
            {
                bgm.PlayBgm(BGMManager.ClipName.itemShop);
            }

            else
            {
                bgm.PlayBgm(BGMManager.ClipName.weaponShop);
            }

        }

        // Update is called once per frame
        public void Exit(Player player)
        {
            enterPlayer = player;
            anim.SetTrigger("doHello");
            uiGroup.anchoredPosition = Vector3.down * 1000;
            if(enterPlayer.isShop == true)
            {
                bgm.PlayBgm(BGMManager.ClipName.robby);
                enterPlayer.isShop = false;
            }
        
        }

        public void Buy(int index)
        {
            int price = itemPrice[index];
            if(price > enterPlayer.coin)
            {
                StopCoroutine(Talk());
                StartCoroutine(Talk());
                return;
            }
            buySound.PlayOneShot(buySound.clip);
            enterPlayer.coin -= price;
            Vector3 ranVec = Vector3.right * Random.Range(-3, 3) + Vector3.forward * Random.Range(-3, 3);
            Instantiate(itemObj[index], itemPos[index].position + ranVec, itemPos[index].rotation);
        }

        IEnumerator Talk()
        {
            talkText.text = talkData[1];
            yield return new WaitForSeconds(2f);
            talkText.text = talkData[0];
        }
    }
}
