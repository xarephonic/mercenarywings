using UnityEngine;
using System.Collections;
using MissileBehaviours.Controller;

namespace MissileBehaviours.Actions
{
    /// <summary>
    /// This script uses the new active target of a missile controller to keep the missile on a specific altitude until close to the target. 
    /// This is particularly useful for ground to ground missiles, which can use this to avoid obstacles, by first flying high, moving towards the target and then dropping straight down.
    /// As you may expect, this script doesn't work well with the fuel feature, since a missile might not be able to even reach the desired altitude. Keep that in mind if you use that.
    /// Note that this script is a lot more complicated than it might sound or seem and is still a bit of a work in progress. It works perfectly fine in some use cases but might still show odd behaviour in others.
    /// If there is demand I'll look into improving this in the future. Feel free to sent me a mail or post in the forum thread if you wish to see that. A link to both can be found in the readme file.
    /// </summary>
    [RequireComponent(typeof(MissileController))]
    public class AltitudeControl : MonoBehaviour
    {
        [SerializeField, Tooltip("The cruise height.")]
        float height;
        [SerializeField, Tooltip("A multiplier to approximate the real turn time of the missile. Please open the script for more information on why this unfortunate magic number is needed.")]
        float turnTimeCorrection = 0.375f; // This is an unfortunate magic number that is necessary because, to my knowledge, it's impossible to predict how long the missile will actually take to turn by 90° and how much speed it will lose or gain in this time. I tried some approximations to elimate the magic number, but they quickly became very expensive to compute, so I decided to leave this in. If you know of a better way, please let me know!
                                           // Furthermore, I also intentionally left out any consideration of drag. For two reasons: 1. It didn't play well with the magic number. 2. I think a specific game will likely use one, or very few, different drag values for the missiles, depending on their games scale. So calculating it every frame seemed like a bad idea.
        [SerializeField, Tooltip("Whether or not this script should be destroyed when the missiles target is changed.")]
        bool destroyOnTargetChange = true;
        [SerializeField, Tooltip("Whether or not the height of the target should be used as the cruising altitude. If true, the height value will offset this height. Furthermore, the missile will start chasing the target as soon as the altitude is reached. If this is false, it will first go to the defined altitude, then move towards the target while staying on that altitude and only once the missile is below/above the target it will start to chase.")]
        bool useTargetHeight = false;
        MissileController controller;
        GameObject imaginaryTarget; // This is a temporary target used to control the path of the missile. 

        bool gainingAltitude = true;

        void Start()
        {
            controller = GetComponent<MissileController>();

            // Create the imaginary transform.
            imaginaryTarget = new GameObject();
            imaginaryTarget.name = "Imaginary Target";
            imaginaryTarget.transform.parent = transform;
            
            // Listen to the new OnTargetChange event of the controller. This allows the script to shut down if the intended target is destroyed or otherwise changed.
            if (destroyOnTargetChange)
            {
                controller.OnTargetChange += Controller_OnTargetChange;
            }
        }

        private void Controller_OnTargetChange(object sender, System.EventArgs e)
        {
            if (destroyOnTargetChange)
                Destroy(this);
        }

        void OnDestroy ()
        {
            // As with all event listeners, we need to make sure to unsubscribe when the object is destroyed to avoid leaving weird memory leaks or keeping objects alive that shouldn't be.
            controller.OnTargetChange -= Controller_OnTargetChange;
        }

        void Update()
        {
            // Since this script can't function without a set target, we do nothing if there is no target.
            if (!controller.Target)
                return;

            float turnDistance = 0;

            // If the missile is still trying to reach the desired altitude.
            if (gainingAltitude)
            {
                controller.ActiveTarget = imaginaryTarget.transform; // Doing this in update ensures that this script basically overrides any other attempts to change the active target. If this is not a desired behaviour, move this line into the Start() method.
                turnDistance = controller.RotationRate * turnTimeCorrection; // Figure out the approximate time the missile will take for a 90° turn. Again, I'll try to get rid of this magic number in the future but for now I've run out of ideas.
                turnDistance *= controller.Velocity.y + Physics.gravity.y; // Multiply the turn time by the approximate distance the missile will travel within that time to get an offset we can add to the altitude at which the missile will begin the turn.

                // If we use the altitude of the target, we need to consider its position. Otherwise we just use the settings and the turn distance.
                if (useTargetHeight)
                    imaginaryTarget.transform.position = new Vector3(transform.position.x, controller.Target.transform.position.y + height - turnDistance, transform.position.z);
                else
                    imaginaryTarget.transform.position = new Vector3(transform.position.x, height - turnDistance, transform.position.z);

                // In a way, this whole thing is a waypoint system. So once we are close enough to the waypoint we can stop gaining altitude and move towards the target while maintaining that altitude.
                if (Vector3.Distance(imaginaryTarget.transform.position, transform.position) <= controller.Velocity.magnitude * Time.fixedDeltaTime)
                {
                    if (useTargetHeight)
                    {
                        controller.ActiveTarget = null;
                        Destroy(this);
                    }
                    else
                        gainingAltitude = false;
                }
            }
            // Moving towards the target at the specified altitude.
            else
            {
                imaginaryTarget.transform.position = new Vector3(controller.Target.transform.position.x, height, controller.Target.transform.position.z);
                turnDistance = controller.RotationRate * turnTimeCorrection;
                turnDistance *= controller.Velocity.magnitude; // This time we use the magnitude, since we are moving an every axis, not just y.

                // Once the missile is above/below the target, this script has done its job and we can let the normal guidance take over again.
                if (Vector3.Distance(imaginaryTarget.transform.position, transform.position) - turnDistance <= controller.Velocity.magnitude * Time.fixedDeltaTime)
                {
                    controller.ActiveTarget = null; // This makes the controller and guidance script use the normal target again.
                    Destroy(this);
                }
                else
                    controller.ActiveTarget = imaginaryTarget.transform;
            }
        }
    }
}