using UnityEngine;

namespace Core.UI
{
    public class HudElement : MonoBehaviour
    {
        [SerializeField] string elementID;
        public string ElementID => elementID;
    }
}