using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] private GameObject hitVFX;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float kunaiSpeed;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }
    private void OnInit()
    {
        rb.velocity = transform.right * kunaiSpeed;
        Invoke(nameof(OnDespawn), 4f);
    }

    private void OnDespawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Enemy"))
        {
            collision.GetComponent<Character>().OnHit(30f);
            Instantiate(hitVFX, transform.position, transform.rotation);
            OnDespawn();
        }
    }

}
