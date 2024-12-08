using UnityEngine;
using UnityEngine.Playables;

public class path : MonoBehaviour
{
    public GameObject ToPath;
    public float moveSpeed;
    [SerializeField] private bool isNotFollowPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ToPath == null || isNotFollowPath)
        {
            ToPath = BuildingSystem.current.humanKingdom;
        }
        transform.position = Vector3.MoveTowards(transform.position, ToPath.transform.position, moveSpeed * Time.deltaTime);

        //rotate
        transform.LookAt(ToPath.transform.position);


        //how enemy know where to move next
        if (Vector3.Distance(transform.position, ToPath.transform.position) <= 5 && ToPath != BuildingSystem.current.humanKingdom)
        {
            ToPath = ToPath.GetComponent<path_linkedlist>().nextPath;
            if (ToPath != BuildingSystem.current.humanKingdom)
            {
                transform.parent = ToPath.GetComponent<path_linkedlist>().enemys.transform;

            }
        }

    }
}
