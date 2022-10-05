using UnityEngine;

namespace _2Scripts
{
    public class BGMManager : MonoBehaviour
    {
        AudioSource bgm;
        public enum ClipName { intro, robby, weaponShop, itemShop, battle, win, winner}
        public AudioClip[] bgmClip;
        public GameManager gameManager;


        // Start is called before the first frame update

        private void Awake()
        {
            bgm = GetComponent<AudioSource>();
            PlayBgm(ClipName.intro);
        }

        public void PlayBgm(ClipName situation)
        {
            bgm.Stop();
            bgm.clip = bgmClip[(int)situation];
            bgm.Play();
        }

        // Update is called once per frame
        void Update()
        {
      

        }
    }
}
