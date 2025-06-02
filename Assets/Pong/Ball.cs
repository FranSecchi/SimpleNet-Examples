using SimpleNet.Network;
using UnityEngine;

namespace NetPackage.Runtime
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float initialSpeed = 5f;
        [SerializeField] private float speedIncrease = 0.5f;
        [SerializeField] private float maxSpeed = 20f;
        [SerializeField] private float collisionCooldown = 0.1f;
        private float currentSpeed;
        private Vector3 direction;
        private Rigidbody rb;
        private float lastCollisionTime;

        private void Start()
        {
            if (!NetManager.IsHost)
            {
                GetComponent<Collider>().enabled = false;
                enabled = false;
                return;
            }
            rb = GetComponent<Rigidbody>();

            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            
            currentSpeed = initialSpeed;
            SetRandomDirection();
            lastCollisionTime = -collisionCooldown; 
            
            rb.velocity = direction * currentSpeed;
        }

        private void FixedUpdate()
        {
            rb.velocity = direction * currentSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Vector3 normal = collision.contacts[0].normal;
            normal.z = 0;
            normal = normal.normalized;
            
            direction = Vector3.Reflect(direction, normal);
            direction.z = 0;
            direction = direction.normalized;

            if (Time.time - lastCollisionTime >= collisionCooldown)
            {
                currentSpeed = Mathf.Min(currentSpeed + speedIncrease, maxSpeed);
                lastCollisionTime = Time.time;
            }
        }

        private void SetRandomDirection()
        {
            float randomAngle = UnityEngine.Random.Range(0f, 360f);
            direction = new Vector3(
                Mathf.Cos(randomAngle * Mathf.Deg2Rad),
                Mathf.Sin(randomAngle * Mathf.Deg2Rad),
                0f
            ).normalized;
        }
    }
}
