using System.Linq;
using UnityEngine;

namespace Boids
{
    public class Boid : MonoBehaviour
    {
        public bool ApplySeparation = true;
        public bool ApplyAlignment = true;
        public bool ApplyCohesion = true;
        public bool ApplyRandom = true;
        public float Radius = 10f;

        private Transform[] allmates;

        private void OnEnable()
        {
            this.allmates = this.transform.parent.GetComponentsInChildren<Boid>()
                .Where(it => it.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
                .Select(it => it.transform)
                .ToArray();
        }

        private void FixedUpdate()
        {
            var randomStrength = 0.0001f;
            var flockmates = TransformUtils.SelectLocalFlockmates(this.transform, this.allmates, this.Radius);

            var v1 = this.ApplySeparation
                ? TransformUtils.CalcSeparationRule(this.transform, flockmates) * 10f
                : Vector3.zero;
            var v2 = this.ApplyAlignment
                ? TransformUtils.CalcAlignmentRule(this.transform, flockmates)
                : Vector3.zero;
            var v3 = this.ApplyCohesion
                ? TransformUtils.CalcCohesionRule(this.transform, flockmates)
                : Vector3.zero;
            var vr = this.ApplyRandom
                ? new Vector3(
                    Random.Range(-randomStrength, randomStrength),
                    Random.Range(-randomStrength, randomStrength),
                    Random.Range(-randomStrength, randomStrength))
                : Vector3.zero;
            var diff = v1 + v2 + v3 + vr;

            this.transform.LookAt(this.transform.position + diff);
        }
    }
}