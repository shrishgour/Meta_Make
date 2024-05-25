using Core.Config;
using Core.Services;
using Core.UI;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NarrativeDialog : BaseDialog, IPointerDownHandler
{
    public const string DialogID = nameof(NarrativeDialog);
    private const float CHAT_VIEW_OFFSET = 80f;
    private const float TYPING_SPEED = 0.01f;

    [SerializeField] private RectTransform chatView;
    [SerializeField] private RectTransform chatViewBackground;
    [SerializeField] private TextMeshProUGUI chatText;

    private string chatString;
    private Action onClose;
    private ChatAnimationConfig chatAnimationConfig;
    private ChatAnimationConfigData animConfigData;
    private float chatLineHeight = 0;
    private bool isTypingComplete = false;

    public override void OnDialogOpen()
    {
        if (chatAnimationConfig == null)
        {
            chatAnimationConfig = ConfigRegistry.GetConfig<ChatAnimationConfig>();
            animConfigData = chatAnimationConfig.data[0];
        }

        RegisterListener();
    }

    private void RegisterListener()
    {

    }

    private void UnregisterListener()
    {

    }

    public void Init(string chatString, Action onClose)
    {
        this.chatString = chatString;
        this.onClose = onClose;

        SetupValue();
        Invoke(nameof(DoChatViewAnimation), 0.1f);
    }

    private void SetupValue()
    {
        chatText.SetText(chatString);
        chatText.alpha = 0f;
        chatViewBackground.sizeDelta = new Vector2(chatViewBackground.sizeDelta.x, 100);
        isTypingComplete = false;
    }

    private void DoChatViewAnimation()
    {
        if (chatLineHeight == 0)
        {
            chatLineHeight = chatText.textInfo.lineInfo[0].lineHeight + chatText.lineSpacing;
        }

        var targetY = (chatText.textInfo.lineCount * chatLineHeight) + CHAT_VIEW_OFFSET;
        targetY = (targetY < 150f) ? 150 : targetY;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(chatViewBackground.DOSizeDelta(new Vector3(chatViewBackground.sizeDelta.x, targetY), animConfigData.animDuration * 1.5f));
        sequence.AppendCallback(() =>
        {
            ServiceRegistry.Get<TypingService>().TypeText(ref chatText, chatString, () =>
            {
                isTypingComplete = true;
            });
        });

        sequence.SetEase(Ease.OutBounce);
        sequence.Play();
    }

    public override void OnDialogClosed()
    {
        UnregisterListener();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (!isTypingComplete)
        {
            ServiceRegistry.Get<TypingService>().FinishTyping();
            return;
        }

        if (isTypingComplete)
        {
            onClose?.Invoke();
        }
    }
}
