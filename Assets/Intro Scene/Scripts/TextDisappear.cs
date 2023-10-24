using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisappear : MonoBehaviour
{
    public TMP_Text textToDisappear;
    public float duration = 5f;

    private void Start()
    {
        StartCoroutine(DisappearAfterDelay());
    }

    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(duration);

        // the text disappears after the duration i set
        textToDisappear.gameObject.SetActive(false);
    }
}
