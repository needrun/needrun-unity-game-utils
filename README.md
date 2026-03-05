# Needrun unity game utils

## 사용법

일부 static 클래스의 경우 다음과 같이 static 생성자를 사용해서 초기화할 수 있습니다.

```csharp
using System;
using NeedrunGameUtils;
using UnityEngine.Events;

public class RootStore : Singleton<RootStore>
{
    public readonly UnityEvent playerDataChangedEvent = new UnityEvent();

    static RootStore()
    {
        // Needrun Game Utils의 설정부
        TimeUtils.Config(new TimeUtilsConfig
        {
            ntpConfidenceLevel = TimeSpan.FromDays(3.0),
            ntpFailCallback = () => ServiceLocator.instance.networkTimeFromServer.GetOrLocal()
        });
        NetworkTimer.RegisterNtpServer(new string[] {
            "time.windows.com",
            "time.google.com"
        });
        I18n.Load(new string[]{
            "Locales/IAPLocales.json",
            "Locales/SystemLocales.json",
            "Locales/TitleLocales.json",
            "Locales/IngredientLocales.json",
            "Locales/RecipeLocales.json",
            "Locales/BakingLocales.json",
            "Locales/FamiliarLocales.json",
            "Locales/SettingsLocales.json",
            "Locales/RemoteResources.json",
        });
    }

    private RootStore()
    {
    }
}
```

또는

```csharp
public class NeedrunGameUtilConfig : SingletonMonoBehaviour<NeedrunGameUtilConfig>
{
    // Script Execution Order을 최소값(-10000)으로 설정하여, 기본 스크립트 이전에 실행되도록 함
    protected override void Awake()
    {
        base.Awake();
        // 이하 설정 부분은 동문
    }
}
```

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
  - PrefabLoader
  - SpriteLoader
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

- ActiveSyncWith
- ButtonDelegator
- PositionXYFollower
- ViewModelMonoBehaviour
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

## SingletonMonoBehaviour

GameObject에 다음과 같은 컴포넌트를 넣어 기능을 활성화 할 수 있습니다.

- EscapeManager
- UnityMainThreadDispatcher
- DisplayOrHideManager
- DateChangeMonitoring
