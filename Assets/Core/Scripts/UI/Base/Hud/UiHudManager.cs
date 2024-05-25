using Core.Config;
using Core.Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class UiHudManager : SerializedSingleton<UiHudManager>
    {
        public Transform hudRoot;

        private Dictionary<string, IHud> hudMap = new Dictionary<string, IHud>();
        private IHud currentHud;

        private void Start()
        {
            CreateOrLoadHuds();
        }

        private void CreateOrLoadHuds()
        {
            var hudDatas = ConfigRegistry.GetConfig<UIHudConfig>().data;

            foreach (var hudData in hudDatas)
            {
                GameObject hud = Instantiate(hudData.hudPrefab, hudRoot);
                hud.SetActive(false);
                hudMap.Add(hudData.hudName, hud.GetComponent<IHud>());
            }
        }

        public void OpenHud(string hudName, string hudState)
        {
            if (currentHud != null)
            {
                currentHud.OnHUDClosed();
                currentHud.SetVisible(false);
            }

            var hud = hudMap[hudName];
            hud.SetVisible(true);
            hud.OnHUDOpen();
            hud.SwitchState(hudState);
            currentHud = hud;
        }
    }
}
