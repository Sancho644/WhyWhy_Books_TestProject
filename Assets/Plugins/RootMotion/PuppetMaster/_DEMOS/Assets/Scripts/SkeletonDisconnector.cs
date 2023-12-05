using UnityEngine;
using RootMotion.Dynamics;

namespace RootMotion.Demos
{
    public class SkeletonDisconnector : MonoBehaviour
    {
        public MuscleDisconnectMode disconnectMuscleMode;
        public LayerMask layers;
        public float unpin = 10f;
        public float force = 10f;
        public ParticleSystem particles;

        // Update is called once per frame
        void Update()
        {
            // Shooting
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Raycast to find a ragdoll collider
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, 100f, layers))
                {
                    var rigidbody = hit.collider.attachedRigidbody;
                    if (rigidbody == null)
                        return;

                    // If is a muscle...
                    if (hit.collider.attachedRigidbody.TryGetComponent<MuscleCollisionBroadcaster>(out var broadcaster))
                    {
                        broadcaster.Hit(unpin, ray.direction * force, hit.point);

                        // Remove the muscle and its children
                        broadcaster.puppetMaster.DisconnectMuscleRecursive(broadcaster.muscleIndex,
                            disconnectMuscleMode);
                    }
                    else
                    {
                        // Add force
                        hit.collider.attachedRigidbody.AddForceAtPosition(ray.direction * force, hit.point);
                    }

                    // Particle FX
                    particles.transform.position = hit.point;
                    particles.transform.rotation = Quaternion.LookRotation(-ray.direction);
                    particles.Emit(5);
                }
            }
        }
    }
}