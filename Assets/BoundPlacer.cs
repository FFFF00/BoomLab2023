using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class BoundPlacer : MonoBehaviour
{
    private Grid grid;
    [SerializeField] private GameObject boundRootPrefab;
    // Start is called before the first frame update

    private GameObject boundRoot;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
    void Start()
    {
        var centerpos = GetCenterPos();
        
        boundRoot = Instantiate(boundRootPrefab);
        boundRoot.transform.parent = this.transform;
        boundRoot.transform.localPosition = centerpos;
        boundRoot.transform.localScale = transform.localScale;
    }

    private Vector3 GetCenterPos()
    {
        var kvs = GameManager.Instance.SceneGOCacheKV;
        Vector3Int maxcoord = new Vector3Int(-100, -100, 0);
        foreach (var kv in kvs)
        {
            var k = kv.Key;
            if ( k.x >= maxcoord.x && k.y >= maxcoord.y)
            {
                maxcoord = k;
            }
        }
        return grid.CellToLocal(maxcoord);
    }
}
