using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    [SerializeField] AIAgent[] agents;
    [SerializeField] LayerMask layerMask;
    
    int index = 0;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,100,layerMask))
            {
                Instantiate(agents[index], hit.point, Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
            index = ++index % agents.Length;
        }
    }
    
    
}
