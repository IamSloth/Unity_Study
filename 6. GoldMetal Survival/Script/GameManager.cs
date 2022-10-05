using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _2Scripts
{
    public class GameManager : MonoBehaviour
    {
        public GameObject menuCam;
        public GameObject gameCam;
        public Player player;
        public Boss boss;
        public GameObject itemShop;
        public GameObject weaponShop;
        public GameObject startZone;
        public int stage;
        public float playTime;
        public bool isBattle;
        public GameObject battleButton;
        public int enemyCntA;
        public int enemyCntB;
        public int enemyCntC;
        public int enemyCntD;

        public Transform[] enemyZones;
        public GameObject[] enemise;
        public List<int> enemyList;

        public GameObject menuPanel;
        public GameObject gamePanel;
        public GameObject overPanel;
        public Text maxScoreTxt;

        public Text scoreTxt;
        public Text stageTxt;
        public Text playTimeTxt;
        public Text playerHealthTxt;
        public Text playerAmmoTxt;
        public Text playerCoinTxt;
        public Text gameOverTxt;
        public Text youWinTxt;
        [FormerlySerializedAs("StatusTxt")] public TextMesh statusTxt;
        [FormerlySerializedAs("StatusOutTxt")] public TextMesh statusOutTxt;

        public Image weapon1Img;
        public Image weapon2Img;
        public Image weapon3Img;
        public Image weaponRImg;

        public Text enemyATxt;
        public Text enemyBTxt;
        public Text enemyCTxt;

        public RectTransform bossHealthGroup;
        public RectTransform bossHealthBar;
        public Text curScoreText;
        public Text bestText;

        public BGMManager bgm;


        // Start is called before the first frame update

        private void Awake()
        {
            Application.targetFrameRate = 60;
            enemyList = new List<int>();
            maxScoreTxt.text = "HIGH SCORE : " + string.Format("{0:n0}", PlayerPrefs.GetInt("MaxScore"));

            if (PlayerPrefs.HasKey("MaxScore"))
                PlayerPrefs.SetInt("MaxScore", 0);

        }

        public void GameStart()
        {
            menuCam.SetActive(false);
            gameCam.SetActive(true);

            menuPanel.SetActive(false);
            gamePanel.SetActive(true);

            player.gameObject.SetActive(true);
            bgm.PlayBgm(BGMManager.ClipName.robby);

        }

        public void GameOver()
        {
            gamePanel.SetActive(false);
            overPanel.SetActive(true);
            curScoreText.text = "SCORE : " + player.score.ToString();

            int maxScore = PlayerPrefs.GetInt("MaxScore");
            if(player.score > maxScore)
            {
                bestText.gameObject.SetActive(true);
                PlayerPrefs.SetInt("MaxScore", player.score);
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }

        public void StageStart()
        {
            itemShop.SetActive(false);
            weaponShop.SetActive(false);
            startZone.SetActive(false);

            foreach (Transform zone in enemyZones)
                zone.gameObject.SetActive(true);
        
            isBattle = true;
            battleButton.transform.GetChild(0).gameObject.SetActive(false);
            battleButton.transform.GetChild(1).gameObject.SetActive(true);
            battleButton.transform.GetChild(2).gameObject.SetActive(true);
            battleButton.transform.GetChild(3).gameObject.SetActive(true);
            battleButton.transform.GetChild(4).gameObject.SetActive(true);
            
            bgm.PlayBgm(BGMManager.ClipName.battle);
            StartCoroutine(InBattle());
        }

        public void StageEnd()
        {

            if (stage == 5 && player.health >= 0)
            {
                stage = 1;
                GameOver();
                gameOverTxt.text = "Congratulation!";
                bgm.PlayBgm(BGMManager.ClipName.winner);
                gameOverTxt.color = new Color(1f, 0.84f, 0.49f);
                youWinTxt.gameObject.SetActive(true);
            }

            else
            {
                player.transform.position = Vector3.up * 0.8f;
                foreach (Transform zone in enemyZones)
                    zone.gameObject.SetActive(false);

                isBattle = false;
                battleButton.transform.GetChild(0).gameObject.SetActive(true);
                battleButton.transform.GetChild(1).gameObject.SetActive(false);
                battleButton.transform.GetChild(2).gameObject.SetActive(false);
                battleButton.transform.GetChild(3).gameObject.SetActive(false);
                battleButton.transform.GetChild(4).gameObject.SetActive(false);

                bgm.PlayBgm(BGMManager.ClipName.robby);
                itemShop.SetActive(true);
                weaponShop.SetActive(true);
                startZone.SetActive(true);
                stage++;
            }
            
            
        }

        IEnumerator InBattle()
        {
            if(stage % 5 == 0)
            {
                enemyCntD++;
                GameObject instantEnemy = Instantiate(enemise[3], enemyZones[0].position, enemyZones[0].rotation);
                Enemy enemy = instantEnemy.GetComponent<Enemy>();
                enemy.target = player.transform;
                enemy.manager = this;
                boss = instantEnemy.GetComponent<Boss>();
            }

            else
            {
                for (int index = 0; index < stage*2; index++)
                {
                    int ran = Random.Range(0, 3);
                    enemyList.Add(ran);

                    switch (ran)
                    {
                        case 0:
                            enemyCntA++;
                            break;

                        case 1:
                            enemyCntB++;
                            break;

                        case 2:
                            enemyCntC++;
                            break;
                    }
                }

                while (enemyList.Count > 0)
                {
                    int ranZone = Random.Range(0, 4);
                    GameObject instantEnemy = Instantiate(enemise[enemyList[0]], enemyZones[ranZone].position, enemyZones[ranZone].rotation);
                    Enemy enemy = instantEnemy.GetComponent<Enemy>();
                    enemy.target = player.transform;
                    enemy.manager = this;
                    enemyList.RemoveAt(0);
                    yield return new WaitForSeconds(2);
                }

            }

            while(enemyCntA + enemyCntB + enemyCntC + enemyCntD > 0)
            {
                yield return null;
            }

            bgm.PlayBgm(BGMManager.ClipName.win);
            yield return new WaitForSeconds(3f);
            boss = null;
            StageEnd();
        }

        private void LateUpdate()
        {
            scoreTxt.text = string.Format("{0:n0}", player.score);
            stageTxt.text = "STAGE " + stage;

            int hour = (int)(playTime / 3600);
            int min = (int)((playTime - hour * 3600) / 60);
            int second = (int)(playTime % 60);

            playTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);

            playerHealthTxt.text = player.health + " / " + player.maxHealth;
            playerCoinTxt.text = string.Format("{0:n0}", player.coin);
            if (player.equipWeapon == null)
                playerAmmoTxt.text = "- / " + player.ammo;
            else if (player.equipWeapon.type == Weapon.Type.Melee)
                playerAmmoTxt.text = "- / " + player.ammo;
            else
                playerAmmoTxt.text = player.equipWeapon.curAmmo + " / " + player.ammo;

        
            weapon1Img.color = new Color(1, 1, 1, player.hasWeapons[0] ? 1 : 0);
            weapon2Img.color = new Color(1, 1, 1, player.hasWeapons[1] ? 1 : 0);
            weapon3Img.color = new Color(1, 1, 1, player.hasWeapons[2] ? 1 : 0);
            weaponRImg.color = new Color(1, 1, 1, player.hasGrenades > 0 ? 1 : 0);

            enemyATxt.text = enemyCntA.ToString();
            enemyBTxt.text = enemyCntB.ToString();
            enemyCTxt.text = enemyCntC.ToString();

            if(boss != null)
            {
                bossHealthGroup.anchoredPosition = Vector3.down * 30;
                bossHealthBar.localScale = new Vector3((float)boss.curHealth / boss.maxHealth, 1, 1);
            }

            else
            {
                bossHealthGroup.anchoredPosition = Vector3.up * 200;
            }
        }

        

        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (isBattle)
                playTime += Time.deltaTime;
        }
    }
}
