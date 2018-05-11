using UnityEngine;

namespace Boids
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveForward : MonoBehaviour
    {
        public float Speed = 10f;
        
        private void FixedUpdate()
        {
            this.transform.GetComponent<Rigidbody>().velocity = this.transform.forward * this.Speed;
        }
    }
}