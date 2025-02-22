using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavController : MonoBehaviour
{
    private NavMeshAgent agent;
    
    
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.destination = hit.point;
            }
            
        }

        if (agent.velocity.magnitude != 0f)
            
        {
            
        }
        
    }
}































