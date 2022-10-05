using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _2Scripts
{
    public class StartZone : MonoBehaviour
    {
        public GameManager manager;
        public Player player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!player.hasWeapons[0] && !player.hasWeapons[1] && !player.hasWeapons[2])
                {
                    manager.statusTxt.gameObject.SetActive(true);
                    manager.statusOutTxt.gameObject.SetActive(true);
                    manager.statusTxt.text = "무기가 없습니다!";
                    manager.statusOutTxt.text = "무기가 없습니다!";
                }
                else
                {
                    manager.StageStart();    
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                manager.statusTxt.gameObject.SetActive(false);
                manager.statusOutTxt.gameObject.SetActive(false);
            }
            
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
