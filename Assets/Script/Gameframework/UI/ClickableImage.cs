using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameFramework.UI {
    [RequireComponent(typeof(Image))]
    public class ClickableImage : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            UnityEngine.Debug.Log("Image Clicked");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UnityEngine.Debug.Log("Image Enter");
        }
    }
}