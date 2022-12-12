using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (end - start).normalized;
        Vector3 pos = transform.position;
        pos += speed * dir * Time.smoothDeltaTime;
        transform.position = pos;
        if (Vector3.Magnitude(transform.position - start) > Vector3.Magnitude(end - start)) {
            Destroy(this.gameObject);
        }
    }

    public void SetEndPts(Vector3 start, Vector3 end)
    {
        this.start = start;
        this.end = end;
        transform.position = start;
    }
}
