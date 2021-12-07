using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    private LayerMask AtkableLayer => LayerMask.GetMask("Atkable");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("²âÊÔ")]
    public void Test()
    {
        Debug.Log(GetAtkTarget(transform.position, 10).name);
    }

    public GameObject GetAtkTarget(Vector3 pos,float range)
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(pos, range, AtkableLayer);
        if(col.Length>0)
        {
            GameObject target = col[0].gameObject;
            float MinDis = (col[0].transform.position-pos).sqrMagnitude;
            for (int i = 1; i < col.Length; i++)
            {
                float dis = (col[i].transform.position - pos).sqrMagnitude;
                if (dis<MinDis)
                {
                    MinDis = dis;
                    target = col[i].gameObject;
                }
            }
            return target;
        }
        else
        {
            return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 10);
    }
}
