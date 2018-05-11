using System.Linq;
using UnityEngine;

namespace Boids
{
    public static class TransformUtils
    {
        /// <summary>
        /// Calc boids rule1. steering position by separation.
        /// </summary>
        /// <param name="target">target object</param>
        /// <param name="flockmates">local flockmates without the target object</param>
        /// <returns>stearing vector by separation rule</returns>
        public static Vector3 CalcSeparationRule(Transform target, Transform[] flockmates)
        {
            if (flockmates.Length < 1) return Vector3.zero;
            var v = flockmates.Select(it => target.position - it.position).Aggregate((sum, it) => it);
            return v / (0.00001f + v.sqrMagnitude);
        }

        /// <summary>
        /// Calc boids rule2. steering position by alignment.
        /// </summary>
        /// <param name="target">target object</param>
        /// <param name="flockmates">local flockmates without the target object</param>
        /// <returns></returns>
        /// <returns>stearing vector by alignment rule</returns>
        public static Vector3 CalcAlignmentRule(Transform target, Transform[] flockmates)
        {
            if (flockmates.Length < 1) return Vector3.zero;
            return flockmates.Select(it => it.forward).Aggregate((sum, it) => sum + it) / flockmates.Length;
        }

        /// <summary>
        /// Calc boids rule3. steering position by cohesion.
        /// </summary>
        /// <param name="target">target object</param>
        /// <param name="flockmates">local flockmates without the target object</param>
        /// <returns>stearing vector by cohesion rule</returns>
        public static Vector3 CalcCohesionRule(Transform target, Transform[] flockmates)
        {
            if (flockmates.Length < 1) return Vector3.zero;
            var average = flockmates.Select(it => it.position).Aggregate((sum, it) => sum + it) / flockmates.Length;
            return average - target.position;
        }

        /// <summary>
        /// Select local flockmates within the circle radius
        /// </summary>
        /// <param name="target">target object</param>
        /// <param name="mates">flockmates object without me</param>
        /// <param name="radius">raidus of target flockmates</param>
        /// <returns></returns>
        public static Transform[] SelectLocalFlockmates(Transform target, Transform[] mates, float radius)
        {
            if (mates.Length < 1) return new Transform[0];
            var radius2 = radius * radius;
            return mates.Where(it => (it.position - target.position).sqrMagnitude <= radius2).ToArray();
        }
    }
}