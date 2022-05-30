using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform endLocation;
    [SerializeField] float dropSpeed;
    bool isFalling = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling) 
        {
            transform.position = UnityEngine.Vector3.MoveTowards(transform.position, endLocation.position, Time.deltaTime * dropSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerController>().IsFalling();
            isFalling = true;
        }
        Destroy(gameObject, 10f);
    }
}
