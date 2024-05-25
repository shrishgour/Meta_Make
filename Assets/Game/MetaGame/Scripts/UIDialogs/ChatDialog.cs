using Core.Config;
using Core.Sequencer.Commands;
using Core.Services;
using Core.UI;
using DG.Tweening;
using Game.Character;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatDialog : BaseDialog, IPointerDownHandler
{
    public const string DialogID = nameof(ChatDialog);
    private const float CHAT_VIEW_OFFSET = 80f;


    [SerializeField] private RectTransform chatView;
    [SerializeField] private RectTransform chatViewBackground;
    [SerializeField] private RectTransform charView;

    [SerializeField] private RawImage charImg;
    [SerializeField] private TextMeshProUGUI chatText;

    private ChatData chatData;
    private Action onClose;
    private ChatAnimationConfig chatAnimationConfig;
    private ChatAnimationConfigData animConfigData;
    private float chatLineHeight = 0;
    private bool isTypingComplete = false;

    public override void OnDialogOpen()
    {
        if (animConfigData == null)
        {
            chatAnimationConfig = ConfigRegistry.GetConfig<ChatAnimationConfig>();
        }

        RegisterListener();
    }

    private void RegisterListener()
    {

    }

    private void UnregisterListener()
    {

    }

    public void Init(ChatData chatData, Action onClose)
    {
        this.chatData = chatData;
        this.onClose = onClose;

        SetupValue();
        SetupCharTexture();
        DoAnimation();
    }

    private void SetupValue()
    {
        animConfigData = chatAnimationConfig.Data[chatData.screenOrient];
        charView.anchoredPosition = animConfigData.charStartPos;
        chatView.anchoredPosition = animConfigData.chatStartPos;
        chatText.SetText(chatData.chatString);
        chatText.alpha = 0f;
        chatViewBackground.sizeDelta = new Vector2(chatViewBackground.sizeDelta.x, 100);
        isTypingComplete = false;
    }

    private void DoAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        Tween moveTween = charView.DOLocalMove(animConfigData.charEndPos, animConfigData.animDuration);
        sequence.Join(moveTween);
        sequence.AppendCallback(() => DoChatViewAnimation()).AppendInterval(animConfigData.animDuration * 0.5f);
        sequence.SetEase(Ease.Linear);
        sequence.Play();
    }

    private void SetupCharTexture()
    {
        charImg.texture = ServiceRegistry.Get<CharacterService>().GetCharacterRenderTexture(chatData.charID);
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
        sequence.AppendCallback(() => chatView.transform.localPosition = animConfigData.chatEndPos);
        sequence.Append(chatViewBackground.DOSizeDelta(new Vector3(chatViewBackground.sizeDelta.x, targetY), animConfigData.animDuration * 1.5f));
        sequence.AppendCallback(() =>
        {
            ServiceRegistry.Get<TypingService>().TypeText(ref chatText, chatData.chatString, () =>
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
