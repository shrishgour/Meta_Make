using Core.Config;
using Core.Services;
using DG.Tweening;
using System;
using UnityEngine;

namespace Game.Services
{
    public class BackgroundService : BaseService
    {
        private SpriteRenderer background;
        private BackgroundConfig backgroundConfig;

        public override void Initialize()
        {
            base.Initialize();

            backgroundConfig = ConfigRegistry.GetConfig<BackgroundConfig>();
            var backgroundObject = new GameObject("Background");
            background = backgroundObject.AddComponent<SpriteRenderer>();
            backgroundObject.transform.position = Vector3.zero;
        }

        public void SetBackground(string bgID, Action onComplete = null)
        {
            background.sprite = backgroundConfig.Data[bgID].backgroundSprite;
            background.color = Color.black;
            background.DOColor(Color.white, 0.5f).OnComplete(() => onComplete?.Invoke());
        }

        public string GetCurrentBackgroundName()
        {
            if (background.sprite == null)
            {
                return string.Empty;
            }

            return background.sprite.name;
        }
    }
}
