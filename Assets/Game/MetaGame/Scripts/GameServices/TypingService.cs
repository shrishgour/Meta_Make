using Core.Services;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypingService : BaseService
{
    private const float TYPING_SPEED = 0.01f;
    private Coroutine typeRoutine = null;
    private bool finishTyping = false;
    private Color32 oldColor = Color.white;
    private Color32 newColor = Color.white;

    public void TypeText(ref TextMeshProUGUI textElement, string textString, Action onComplete)
    {
        if (typeRoutine != null)
        {
            Coroutiner.StopAll();
        }
        typeRoutine = Coroutiner.Start(TypeLine(textElement, textString, onComplete));
    }

    public void FinishTyping()
    {
        if (typeRoutine != null)
        {
            finishTyping = true;
        }
    }

    private IEnumerator TypeLine(TextMeshProUGUI textElement, string textString, Action onComplete)
    {
        textElement.text = textString;
        int totalCharacters = textElement.text.Length;

        for (int i = 0; i < totalCharacters; i++)
        {
            if (finishTyping)
            {
                finishTyping = false;
                textElement.alpha = 1;
                break;
            }
            oldColor = textElement.textInfo.characterInfo[i].color;
            newColor = new Color32(oldColor.r, oldColor.g, oldColor.b, 255);
            int materialIndex = textElement.textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = textElement.textInfo.characterInfo[i].vertexIndex;

            Color32[] newVertexColors = textElement.textInfo.meshInfo[materialIndex].colors32;
            newVertexColors[vertexIndex + 0] = newColor;
            newVertexColors[vertexIndex + 1] = newColor;
            newVertexColors[vertexIndex + 2] = newColor;
            newVertexColors[vertexIndex + 3] = newColor;

            textElement.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            yield return new WaitForSeconds(TYPING_SPEED);
        }

        onComplete.Invoke();
    }
}
