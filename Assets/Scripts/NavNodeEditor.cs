using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class NavNodeEditor : MonoBehaviour
{
    [SerializeField] private GameObject navNodePrefab;
    [SerializeField] private LayerMask layerMask;

    private bool active;
    private NavNode activeNavNode;
    private readonly Color green = new(0, 1, 0, 0.5f);
    private NavNode navNode;

    private Vector3 position = Vector3.zero;
    private readonly Color red = new(1, 0, 0, 0.5f);
    private bool spawnable;

    private readonly Color white = new(1, 1, 1, 0.5f);

    private void OnEnable()
    {
        if (!Application.isEditor) Destroy(this);
        SceneView.duringSceneGui += OnScene;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnScene;
    }

    private void OnDrawGizmos()
    {
        if (!active) return;

        // draw cursor sphere
        if (spawnable && navNode == null)
        {
            Gizmos.color = white;
            Gizmos.DrawSphere(position, 1);
        }

        // draw sphere on nav node
        if (navNode != null && navNode != activeNavNode)
        {
            Gizmos.color = green;
            Gizmos.DrawSphere(navNode.gameObject.transform.position, 1);
        }

        // draw connection sphere and line
        if (activeNavNode != null)
        {
            var connected = false;
            foreach (var neighbor in activeNavNode.neighbors)
                if (neighbor == navNode)
                {
                    connected = true;
                    break;
                }

            Gizmos.color = navNode != null && activeNavNode != navNode && !connected ? green : red;
            Gizmos.DrawSphere(activeNavNode.gameObject.transform.position, 1.25f);
            Gizmos.DrawLine(activeNavNode.gameObject.transform.position, position);
        }

        // draw connections
        var nodes = NavNode.GetNavNodes();
        foreach (var node in nodes)
        foreach (var neighbors in node.neighbors)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(node.transform.position, neighbors.transform.position);
        }
    }


    private void OnScene(SceneView scene)
    {
        var e = Event.current;

        // set editor active when space is held down
        if (e.isKey && e.keyCode == KeyCode.Space)
        {
            if (e.type == EventType.KeyDown) active = true;
            if (e.type == EventType.KeyUp) active = false;
        }

        // return if not active, reset nodes
        if (!active)
        {
            navNode = null;
            activeNavNode = null;
            return;
        }

        // scene does not pass mouse up event, work around to get mouse up event type
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

        var controlID = GUIUtility.GetControlID(FocusType.Passive);
        var eventType = e.GetTypeForControl(controlID);

        // check for node or spawn ray hit
        if (e.isMouse && (e.type == EventType.MouseMove || e.type == EventType.MouseDrag))
        {
            // get scene mouse position
            Vector3 mousePosition = e.mousePosition;
            mousePosition.y = scene.camera.pixelHeight - mousePosition.y * EditorGUIUtility.pixelsPerPoint;
            mousePosition.x *= EditorGUIUtility.pixelsPerPoint;

            // compute ray from mouse position
            var ray = scene.camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var hitInfo, 100, layerMask))
            {
                position = hitInfo.point;
                // if not over node in scene, set spawnable to true
                spawnable = !hitInfo.collider.gameObject.TryGetComponent(out navNode);
                e.Use();
            }
            else
            {
                // if not over spawn or node layer then reset navNode
                navNode = null;
                spawnable = false;
            }
        }

        // check mouse down
        if (eventType == EventType.MouseDown)
        {
            // if spawnable, create nav node
            if (spawnable && navNode == null && activeNavNode == null)
                Instantiate(navNodePrefab, position, Quaternion.identity, transform);
            // if nav node is selected then set active nav node to nav node (used for connections)
            if (navNode != null && activeNavNode == null)
            {
                activeNavNode = navNode;
                navNode = null;
            }

            e.Use();
        }

        // check mouse up
        if (eventType == EventType.MouseUp)
        {
            // if there's an active node and over a different node, create connection
            if (activeNavNode != null && navNode != null && activeNavNode != navNode)
            {
                // connect from active nav node to nav node, if not already connected
                if (!activeNavNode.neighbors.Contains(navNode)) activeNavNode.neighbors.Add(navNode);

                // connect from nav node to active nav node, if not already connected
                if (!navNode.neighbors.Contains(activeNavNode)) navNode.neighbors.Add(activeNavNode);
            }

            activeNavNode = null;
            e.Use();
        }

        // remove nav node
        if (e.isKey && e.keyCode == KeyCode.D)
        {
            if (navNode != null)
            {
                // remove node from neighbors
                foreach (var neighbor in navNode.neighbors)
                    if (neighbor.neighbors.Contains(navNode))
                        neighbor.neighbors.Remove(navNode);

                // remove nav node
                DestroyImmediate(navNode.gameObject);
            }

            e.Use();
        }
    }
}