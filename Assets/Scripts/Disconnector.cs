using RootMotion.Dynamics;
using UnityEngine;

public class Disconnector : MonoBehaviour
{
    [SerializeField] private MuscleDisconnectMode _disconnectMuscleMode;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _velocityDelta;
    [SerializeField] private float _unpin = 10f;
    [SerializeField] private float _force = 10f;
    [SerializeField] private ParticleSystem _particles;

    private Camera _camera;
    private bool _isHit;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        CheckingVelocity();
    }

    private void CheckingVelocity()
    {
        _isHit = _rigidbody.velocity.magnitude >= _velocityDelta;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isHit)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            /*if (hit.collider == null)
                return;*/

            if (collision.collider.attachedRigidbody.TryGetComponent<MuscleCollisionBroadcaster>(out var broadcaster))
            {
                broadcaster.Hit(_unpin, ray.direction * _force, hit.point);

                broadcaster.puppetMaster.DisconnectMuscleRecursive(broadcaster.muscleIndex, _disconnectMuscleMode);

                _particles.transform.position = hit.point;
                _particles.transform.rotation = Quaternion.LookRotation(-ray.direction);
                _particles.Emit(5);
            }
        }
    }
}