using Unity.VisualScripting;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    private Transform tr;
    private void Start()
    {
        tr = transform;
    }

    private void Update()
    {
        if(StatusVoln.statVoln)
            MouseRoteit();
    }

    private void MouseRoteit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targ = hit.point - tr.position;
            Quaternion tar = Quaternion.LookRotation(targ);
            tr.rotation = Quaternion.Lerp(tr.rotation, tar, 100 * Time.deltaTime);
        }
    }
}
