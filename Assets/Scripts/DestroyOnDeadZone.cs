using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeadZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("DeadZone"))) {
            Destroy(gameObject);
        }
    }
}
