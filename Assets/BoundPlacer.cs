using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundPlacer : MonoBehaviour
{
    [SerializeField]
    private Grid grid;
    [SerializeField] private GameObject boundRootPrefab;
    // Start is called before the first frame update

    private GameObject boundRoot;
    void Start()
    {
        var centerpos = GetPosOfCenter();
        
        boundRoot = Instantiate(boundRootPrefab);
        boundRoot.transform.parent = this.transform;
        boundRoot.transform.position = centerpos;
    }

    private Vector3 GetPosOfCenter()
    {
        var kvs = GameManager.Instance.SceneGOCacheKV;
        Vector3Int maxcoord = new Vector3Int(-100, -100, 0);
        Vector3Int mincoord = new Vector3Int(100, 100, 0);
        foreach (var kv in kvs)
        {
            var k = kv.Key;
            if ( k.x >= maxcoord.x && k.y >= maxcoord.y)
            {
                maxcoord = k;
            }
            if(k.x <= mincoord.x && k.y  <= mincoord.y)
            {
                mincoord = k;
            }
        }
        var maxpos = grid.CellToWorld(maxcoord);
        var minpos = grid.CellToWorld(mincoord);
        return (maxpos + minpos) / 2;
    }
}
