using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeedrunGameUtils
{
    public class DemoActivator : MonoBehaviour
    {
        private void Start()
        {
            ActivateOrNotDemoComponentAll();
            SceneManager.sceneLoaded += (scene, loadScene) => ActivateOrNotDemoComponentAll();
        }

        private void ActivateOrNotDemoComponentAll()
        {
            ActivateWhenDemo[] activateWhenDemos = Resources.FindObjectsOfTypeAll<ActivateWhenDemo>();
            foreach (ActivateWhenDemo activateWhenDemo in activateWhenDemos)
            {
                activateWhenDemo.ActivateOrNot();
            }

            InactivateWhenDemo[] inactivateWhenDemos = Resources.FindObjectsOfTypeAll<InactivateWhenDemo>();
            foreach (InactivateWhenDemo inactivateWhenDemo in inactivateWhenDemos)
            {
                inactivateWhenDemo.ActivateOrNot();
            }

        }
    }
}