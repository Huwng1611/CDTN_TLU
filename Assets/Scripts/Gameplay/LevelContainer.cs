using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cros.FuzzlePuzzle
{
    [ExecuteInEditMode]
    public class LevelContainer : MonoBehaviour
    {
        [SerializeField] RectTransform checkTransform;
        [SerializeField] float bottomMargin;
        [SerializeField] float topMargin;

        private void Start()
        {
            CalculateScale();
        }

        private void CalculateScale()
        {
            if (checkTransform == null) return;
            float scale = 1.0f;
            {
                float width = 5.6f;
                // height
                float height;
                {
                    height = width * Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height) - bottomMargin - topMargin - checkTransform.offsetMin.y - checkTransform.offsetMax.y;
                    this.transform.localPosition = new Vector3(0, (bottomMargin - topMargin) / 2.0f, 0);
                }
                // set scale 9-16
                if (width / height > 9.0f / 16)// width larger than required
                {
                    scale = (height * 9.0f / 16) / width;
                }
                else if (width / height < 9.0 / 16)// height larger than required
                {
                    //scale = 1.0f;// (16 / 9.0f * width)/height;
                    scale = (16 / 9.0f * width) / height;
                }
                //Debug.Log("LevelContainerTransformControl need set transform: " + width + ", " + height + ", " + Screen.width + ", " + Screen.height);
            }
            this.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}