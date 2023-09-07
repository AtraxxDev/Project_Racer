using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutofBounds : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> checkpoints;
    [SerializeField] Vector3 vectorPoint;
    [SerializeField] GameObject checkOutofBounds;

    private int checkpointLayer;
    // Start is called before the first frame update
    void Awake()
    {
      
        checkpointLayer = LayerMask.NameToLayer("Checkpoints");
    }

    void OutofBound()
    {
        player.transform.position = vectorPoint;
    }

    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.name=="OOB")
        {
            OutofBound();
        }
        if (collider.gameObject.layer == checkpointLayer)
        {
            vectorPoint=player.transform.position;
        }

    }
}
