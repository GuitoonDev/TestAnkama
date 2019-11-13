using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CutsceneText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    [SerializeField] private string[] countdownTextToDisplayArray = null;

    private int countdownTextIndex;

    private void Awake() {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        textMesh.text = countdownTextToDisplayArray[countdownTextIndex];
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine() {
        while (countdownTextIndex < countdownTextToDisplayArray.Length - 1) {
            yield return new WaitForSeconds(0.95f);

            countdownTextIndex++;
            textMesh.text = countdownTextToDisplayArray[countdownTextIndex];
        }

        Destroy(transform.parent.gameObject, 0.95f);
    }
}
