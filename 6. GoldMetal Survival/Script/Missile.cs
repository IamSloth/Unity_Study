using UnityEngine;

namespace _2Scripts
{
    public class Missile : MonoBehaviour
    {
    

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.right * (90 * Time.deltaTime));
        }
    }
}
