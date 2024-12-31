# Needrun unity game utils

## Dependency

- "com.unity.nuget.newtonsoft-json": "3.2.1"

## Locale

- `/Assets/StreamingAssets/Locale.json`을 사용함
  ```json
  [
    {
      "key": "",
      "kr": "",
      "en": "",
      "jp": ""
    }
  ]
  ```

## Utils

- MathUtils
- RandomUtils
- ReflectionUtils
- FunctionCall
- TaskUtils
- ThrottleUtils
- NumberUtils
- NonceUtils
- JsonUtils
- HashUtils
- GameObjectUtils
- 리소스를 불러올 때
  - PrefabsLoader
  - SpritesLoader
  - FileLoader
- 모바일 플랫폼
  - AndroidHelper
  - AppleHelper
- 버전 관리
  - VersionManager
  - SemanticVersioning
- API 연결
  - UnityWebRequestTask

## MonoBehaviour

- - ActiveSyncWith
  - ButtonDelegator
  - PositionXYFollower
- 네트워크 환경을 주기적으로 체크
  - NetworkChecker
- 모바일 기기 대응
  - MobileKeyboardRepositionPanel
  - MobileKeyboardSimulator
  - MobileOnly
- 어떤 게임 오브젝트를 날짜에 따라 ON/OFF하고 싶을 때
  - `[Required: DisplayOrHideManager]`
  - DisplayWhenMonthDayMatched
  - DisplayWhenYearMonthDayMatched
  - ExcludeWhenMonthDayUnmatched
  - ExcludeWhenYearMonthDayUnmatched
- 사용자 언어에 따라 이미지나 텍스트를 달리 보여주고 싶을 때
  - LocaleImage
  - LocaleText
- ScreenCaptureManager
  - `[Required]`
  - ScreenCaptureAtAndroid
  - ScreenCaptureAtIos
  - ScreenCaptureAtEditor

## SingletonMonoBehaviour

GameObject에 다음과 같은 컴포넌트를 넣어 기능을 활성화 할 수 있습니다.

- EscapeManager
- UnityMainThreadDispatcher
- DisplayOrHideManager
- DateChangeMonitoring
- MobileNotification
  - `[Required]`
  - AndroidMobileNotification : MonoBehaviour
  - IosMobileNotification : MonoBehaviour
- StoreReview
  - [Required]
  - AndroidStoreReview : MonoBehaviour
  - IosMobileNotification : MonoBehaviour
