using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private void OnMouseDown() {
        Destroy(gameObject);
    }
}
