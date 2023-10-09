using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject chainPrefab;
    public Rigidbody2D firstChain;
    public HingeJoint2D connectedBody;

    public int chainCount = 5;

    private LineRenderer line;
    private List<Transform> chains = new List<Transform>();

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        line.positionCount = chainCount + 2;

        chains.Add(firstChain.transform);

        Rigidbody2D lastChain = firstChain;

        for (int i = 0; i < chainCount; i++)
        {
            GameObject newChain = Instantiate(chainPrefab, transform.position, Quaternion.identity);

            chains.Add(newChain.transform);

            newChain.GetComponent<HingeJoint2D>().connectedBody = lastChain;

            lastChain = newChain.GetComponent<Rigidbody2D>();
        }

        connectedBody.connectedBody = lastChain;

        chains.Add(connectedBody.transform);
    }

    private void Update()
    {
        for (int i = 0; i < chainCount + 2; i++)
        {
            line.SetPosition(i, chains[i].position);
        }
    }
}
