using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float speed;
    [SerializeField] float knockBackForce = 10f;

    private void Update()
    {
        if (target == null) return;

        transform.LookAt(target.transform.position);
        Vector3 towardTarget = (target.transform.position - transform.position).normalized;
        transform.Translate(towardTarget * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null) return;
        if (other.gameObject == target)
        {
            if (other.TryGetComponent(out Rigidbody targetRb))
            {
                Vector3 awayFromRocket = (target.transform.position - transform.position).normalized;
                targetRb.AddForce(awayFromRocket * knockBackForce, ForceMode.Impulse);
            }
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        if (target == null) return;
        this.target = target;
    }

}
