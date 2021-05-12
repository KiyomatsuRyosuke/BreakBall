using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // ターゲットマネージャー.
    GameObject manager;
    TargetManager script;

    // 回転
    [SerializeField] float angle = 90f;
    [SerializeField] Vector3 axis = Vector3.up;
    [SerializeField] float step = 1;
    Quaternion targetRot;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("TargetManager");
        script = manager.GetComponent<TargetManager>();

        targetRot = Quaternion.AngleAxis(angle, axis) * transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, step);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Debug.Log("当たり");
            script.Hit();
        }
    }
}
