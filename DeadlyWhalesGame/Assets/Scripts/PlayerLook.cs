using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        // Rzut promienia od kamery do płaszczyzny
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float rayLength))
        {
            // Oblicz punkt, na który gracz ma patrzeć
            Vector3 pointToLook = ray.GetPoint(rayLength);
            Vector3 direction = (pointToLook - transform.position).normalized;

            // Zablokuj obrót w osi Y
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
