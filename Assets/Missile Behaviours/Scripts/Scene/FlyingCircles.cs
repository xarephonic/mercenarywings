using UnityEngine;
using System.Collections;
namespace MissileBehaviours.Scene
{
    /// <summary>
    /// Simple script that makes the object turn over time.
    /// </summary>
    public class FlyingCircles : MonoBehaviour
    {
        public float turnSpeed = 0.5f;
        void Update()
        {
            transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0));
        }
    }
}