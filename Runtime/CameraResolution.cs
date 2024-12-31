using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedrunGameUtils
{
    public class CameraResolution : MonoBehaviour
    {
        private float scaleWidthRatio = 16f;
        private float scaleHeightRatio = 9f;

        private void Awake()
        {
            Camera camera = GetComponent<Camera>();
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float scaleHeight = (screenWidth / screenHeight) / (scaleWidthRatio / scaleHeightRatio);
            float scaleWidth = 1f / scaleHeight;
            if (scaleHeight < 1)
            {
                fitSparseHeigth(camera, scaleHeight);
            }
            else
            {
                fitSparseWidth(camera, scaleWidth);
            }
        }

        private void fitSparseHeigth(Camera camera, float scaleHeight)
        {
            Rect rect = camera.rect;
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
            camera.rect = rect;
        }

        private void fitSparseWidth(Camera camera, float scaleWidth)
        {
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
            camera.rect = rect;
        }

        private void OnPreCull()
        {
            // OnPreCull은 카메라가 culling(렌더링 단계에서 어떤 오브젝트를 표시하는 때) 단계에 호출됩니다.
            // 이 메서드는 GetComponent로 카메라가 붙어있는 경우만 활성화 됩니다.
            // https://docs.unity3d.com/kr/530/ScriptReference/MonoBehaviour.OnPreCull.html
            // 
            // 필러박스 반짝임 현상을 제거하기 위해 GL.Clear를 사용합니다. 
            // 이전에 렌더링했던 화면을 지우는 역할인데, 원래는 잔상을 지우기위한 용도로 보임.
            // 필러박스 반짝임 문제가 잔상에 의한 것이라 추정되서 사용했지만, 정확한 이유는 모름. 
            // ref 1. https://www.google.com/search?q=유니티+레터박스+반짝임
            // ref 2. https://www.google.com/search?q=OnPreCull+GL.Clear
            // ref 3. https://dhshin94.tistory.com/m/183
            // ref 4. https://goraniunity2d.blogspot.com/2019/07/cameraresolution.html
            GL.Clear(true, true, Color.black);
        }
    }
}