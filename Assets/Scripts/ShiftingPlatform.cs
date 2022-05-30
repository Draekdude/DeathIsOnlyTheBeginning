using UnityEngine;

public class ShiftingPlatform : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] Transform startingLocation;
    [SerializeField] Transform endingLocation;
    Vector3 _newPosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingLocation.position;
        _newPosition = endingLocation.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetNewLocation();
        transform.position = Vector3.MoveTowards(transform.position, _newPosition, Time.deltaTime * moveSpeed);
    }

    private void GetNewLocation()
    {
        if (Vector3.Distance(transform.position, endingLocation.position) == 0) 
        {
            _newPosition = startingLocation.position;
        } 
        if (Vector3.Distance(transform.position, startingLocation.position) == 0) {
            _newPosition = endingLocation.position;
        }
    }
}
