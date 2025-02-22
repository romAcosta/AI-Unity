using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    [SerializeField] private AIAgent[] agents;
    [SerializeField] private LayerMask layerMask;

    private int index;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift)))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, layerMask))
                Instantiate(agents[index], hit.point, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Tab)) index = ++index % agents.Length;
    }
}