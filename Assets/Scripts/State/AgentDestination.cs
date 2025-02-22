using UnityEditor.Search;
using UnityEngine;

public class AgentDestination : MonoBehaviour
{
    
    [SerializeField] private LayerMask layerMask;

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                var agents = FindObjectsByType<Movement>(FindObjectsSortMode.None);
                foreach (var agent in agents)
                {
                    agent.Destination = hit.point;
                }
            }
                
        }

        
    }

    
}
