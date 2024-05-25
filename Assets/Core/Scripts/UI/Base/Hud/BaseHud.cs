using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public interface IHud
    {
        void OnHUDOpen();
        void OnHUDShown();
        void OnHide();
        void OnHUDClosed();
        void OnBackPressed();
        void OnClickBackground();
        void SwitchState(string stateName);
        void SetVisible(bool value);
    }

    public abstract class BaseHud<T> : MonoBehaviour, IHud where T : IHudState
    {
        public List<T> hudStates;
        private HudElement[] allElements;
        protected T currentState;

        private void Awake()
        {
            allElements = transform.GetComponentsInChildren<HudElement>();
        }

        public virtual void OnHUDOpen()
        {
        }

        public virtual void OnHUDShown()
        {
        }

        public virtual void OnHide()
        {
        }

        public virtual void OnHUDClosed()
        {
        }

        public virtual void OnBackPressed()
        {
        }

        public virtual void OnClickBackground()
        {
        }

        public void SwitchState(string stateName)
        {
            foreach (var element in allElements)
            {
                element.gameObject.SetActive(false);
            }

            currentState = hudStates.Find(x => x.StateName == stateName);
            var elements = currentState.HudElements;

            foreach (var element in elements)
            {
                element.gameObject.SetActive(true);
            }
        }

        public virtual void SetVisible(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
