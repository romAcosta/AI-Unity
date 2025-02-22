using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NavNode : MonoBehaviour
{
    public List<NavNode> neighbors = new();
    

    private void Start()
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.TryGetComponent<NavPath>(out NavPath path))
        {
            
            if (path.TargetNode == this)
            {
                path.TargetNode = path.GetNextNavNode(this);
            }
            
        }
    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<NavPath>(out NavPath path))
        {
            
            if (path.TargetNode == this)
            {
                path.TargetNode = path.GetNextNavNode(this);
            }
        }
    }
    public NavNode GetRandomNeighbour()
    {
        return neighbors.Count > 0 ? neighbors[Random.Range(0, neighbors.Count)] : null;
    }

    

    public static NavNode[] GetNavNodes()
    {
        return FindObjectsByType<NavNode>(FindObjectsSortMode.None);
    }

    public static NavNode[] GetNavNodesByTag(string tag)
    {
        var nodes = new List<NavNode>();

        var gameObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (var gameObject in gameObjects)
            if (gameObject.TryGetComponent(out NavNode navNode))
                nodes.Add(navNode);

        return nodes.ToArray();
    }

    public static NavNode GetRandomNavNode()
    {
        var nodes = GetNavNodes();
        return nodes?[Random.Range(0, nodes.Length)];
    }


    /// <summary>
    ///     Finds the nearest NavNode to a given position based on squared distance.
    /// </summary>
    public static NavNode GetNearestNavNode(Vector3 position)
    {
        NavNode nearestNode = null;
        var nearestDistance = float.MaxValue;

        var nodes = GetNavNodes();
        foreach (var node in nodes)
        {
            var distance = (position - node.transform.position).sqrMagnitude; // Use sqrMagnitude for efficiency
            if (distance < nearestDistance)
            {
                nearestNode = node;
                nearestDistance = distance;
            }
        }

        return nearestNode;
    }

    /// <summary>
    ///     Reconstructs the path from the given node back to the start node using the Previous references.
    /// </summary>
    public static void CreatePath(NavNode node, ref List<NavNode> path)
    {
        // Traverse backward through the previous nodes to reconstruct the shortest path
        while (node != null)
        {
            path.Add(node); // Add current node to the path
            node = node.Previous; // Move to the previous node in the path
        }

        // Reverse the path to ensure it follows the correct order (start to destination)
        path.Reverse();
    }

    /// <summary>
    ///     Resets all NavNodes, clearing pathfinding data (Cost and Previous references).
    /// </summary>
    public static void ResetNodes()
    {
        var nodes = GetNavNodes();
        foreach (var node in nodes)
        {
            node.Previous = null;
            node.Cost = float.MaxValue; // Reset cost to a high value (infinity equivalent)
        }
    }

    public float Cost { get; set; }

    public NavNode Previous { get; set; }
}