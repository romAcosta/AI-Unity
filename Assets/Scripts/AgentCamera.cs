using UnityEngine;

public class AgentCamera : MonoBehaviour
{
    [SerializeField] private Transform[] transforms;

    private Camera followCamera;
    private int transformIndex;

    private void Start()
    {
        followCamera = Camera.main;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var agents = FindObjectsByType<AIAgent>(FindObjectsSortMode.None);
            if (agents.Length == 0) return;

            followCamera.transform.parent = agents[Random.Range(0, agents.Length)].transform;
            followCamera.transform.localPosition = new Vector3(0, 5, -10);
            followCamera.transform.localRotation = Quaternion.AngleAxis(20, Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.C) && transforms.Length > 0)
        {
            followCamera.transform.parent = transforms[++transformIndex % transforms.Length].transform;
            followCamera.transform.localPosition = Vector3.zero;
            followCamera.transform.localRotation = Quaternion.identity;
        }
    }
}