using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] movePoint;
    [SerializeField] private float speed;
    [SerializeField] private float cooldown;

    private int movePointIndex;
    private float cooldownTime;

    private void Start()
    {
        transform.position = movePoint[0].transform.position;
    }

    private void Update()
    {
        cooldownTime -= Time.deltaTime;

        bool isWorking = cooldownTime < 0;

        if (isWorking)
            transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoint[movePointIndex].position) < 0.1f)
        {
            movePointIndex++;
            cooldownTime = cooldown;

            if (movePointIndex >= movePoint.Length)
            {
                movePointIndex = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
