using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public Camera cam;
    public OutlineOnLook outline;

    public LayerMask layerMask;

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2500, layerMask))
        {
            if(hit.transform.TryGetComponent(out OutlineOnLook outline))
            {
                if(this.outline != outline)
                {
                    this.outline?.Outline(false);
                }

                this.outline = outline;
                outline.Outline(true);

                Debug.Log("Change Outline");
            }
            else
            {
                this.outline?.Outline(false);
                this.outline = default;
            }

        }
        else
        {
            outline?.Outline(false);
            outline = default;
        }
    }
}
