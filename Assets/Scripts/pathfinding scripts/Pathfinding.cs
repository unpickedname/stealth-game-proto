using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;



//Largely done following Sebastian Lague YouTube tutuorial for A* algorithm 
public class Pathfinding : MonoBehaviour {
    NodeGrid grid;
    PathRequestManager requestManger;


    private void Awake()
    {
        requestManger = GetComponent<PathRequestManager>();
        grid = this.GetComponent<NodeGrid>();
    }

 

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(findPath(startPos, targetPos));
    }

    IEnumerator findPath(Vector3 start, Vector3 targetPos)
    {

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        Node startNode = grid.NodeFromWorldPoint(start);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);


        if (startNode.walkable && targetNode.walkable)
        {
            Heap<Node> openList = new Heap<Node>(grid.maxSize());
            HashSet<Node> closedList = new HashSet<Node>();

            openList.Add(startNode);

            while (openList.Count() > 0)
            {
                Node currentNode = openList.RemoveFirst();
                closedList.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (Node n in grid.getNeighbors(currentNode))
                {
                    if (!n.walkable || closedList.Contains(n))
                    {
                        continue;
                    }
                    int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, n);

                    if (newMovementCostToNeighbor < n.gCost || !openList.Contains(n))
                    {
                        n.gCost = newMovementCostToNeighbor;
                        n.hCost = GetDistance(n, targetNode);
                        n.parent = currentNode;

                        if (!openList.Contains(n))
                        {
                            openList.Add(n);
                        }
                        else
                            openList.Update(n);
                    }
                }
            }
        }

        yield return null;

        if(pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        requestManger.FinishedProcessingPath(waypoints, pathSuccess); 
    }


    Vector3[] RetracePath(Node startNode,  Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

         Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);

        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
           Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
           if (directionNew != directionOld)
           {
                waypoints.Add(path[i].worldPosition);
            }
           directionOld = directionNew;
        }
        return waypoints.ToArray();
    }


    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        else
            return 14 * distanceX + 10 * (distanceY - distanceX);

    }


}
