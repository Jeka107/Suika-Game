using Unity.VisualScripting;
using UnityEngine;

public class AimLineController : MonoBehaviour
{ 
    [SerializeField] private Transform arrow;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Botton")
            {
                arrow.position = new Vector3(transform.position.x, hit.collider.transform.position.y + 0.35f, 0f);
            }
            if(hit.collider.tag =="Ball")
            {
                arrow.position = new Vector3(transform.position.x, hit.collider.bounds.extents.y + hit.collider.transform.position.y+0.25f, 0f);
            }
        }
    }
}

