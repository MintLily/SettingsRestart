using MelonLoader;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using System.Diagnostics;

namespace SettingsRestart
{
    public static class BuildInfo
    {
        public const string Name = "SettingsRestart";
        public const string Author = "Lily";
        public const string Company = null;
        public const string Version = "1.0.1";
        public const string DownloadLink = "https://github.com/MintLily/SettingsRestart";
        public const string Description = "Simply adds a restart button on Settings, next to the Exit button.";
    }

    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(GetAssembly());
            MelonLogger.Msg("Initialized!");
        }

        private void OnUiManagerInit() => MelonCoroutines.Start(CreateButton());

        private GameObject RealSettingsExit, SettingsExit, SettingsRestart;

        private IEnumerator CreateButton()
        {
            yield return new WaitForSeconds(8f);
            try {
                // Hey skids, if you're gonna take this and add to your mod, at least give me some credit. Much appreciated!
                #region Exit Button
                RealSettingsExit = GameObject.Find("UserInterface/MenuContent/Screens/Settings/Footer/Exit").gameObject;

                SettingsExit = UnityEngine.Object.Instantiate<GameObject>(GameObject.Find("UserInterface/MenuContent/Screens/Settings/Footer/Exit"), GameObject.Find("UserInterface/MenuContent/Screens/Settings/Footer").transform);
                SettingsExit.GetComponentInChildren<Text>().text = "EXIT";
                SettingsExit.name = "LilyMod_SettingsExitGame";
                SettingsExit.GetComponent<RectTransform>().localPosition = new Vector2(-90f, -456f);
                SettingsExit.GetComponent<RectTransform>().sizeDelta -= new Vector2(150.0f, 0.0f);
                SettingsExit.SetActive(true);
                SettingsExit.GetComponentInChildren<Button>().onClick.AddListener(new System.Action(() =>
                RealSettingsExit.GetComponent<Button>().onClick.Invoke()));
                #endregion

                #region Restart Button
                SettingsRestart = UnityEngine.Object.Instantiate<GameObject>(GameObject.Find("UserInterface/MenuContent/Screens/Settings/Footer/Exit"), GameObject.Find("UserInterface/MenuContent/Screens/Settings/Footer").transform);
                SettingsRestart.transform.SetParent(GameObject.Find("UserInterface/MenuContent/Screens/Settings/Footer/").transform);
                SettingsRestart.name = "LilyMod_SettingsRestartGame";
                SettingsRestart.GetComponentInChildren<Text>().text = "RESTART";
                SettingsRestart.GetComponent<RectTransform>().localPosition = new Vector2(90f, -456f);
                SettingsRestart.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 109f);
                SettingsRestart.SetActive(true);
                SettingsRestart.GetComponentInChildren<Button>().onClick.AddListener(new System.Action(() => {
                    try { Process.Start(System.Environment.CurrentDirectory + "\\VRChat.exe", System.Environment.CommandLine.ToString()); }
                    catch (System.Exception) { new System.Exception(); }
                    RealSettingsExit.GetComponent<Button>().onClick.Invoke();
                }));
                #endregion
            }
            catch (Exception e) { MelonLogger.Error("Settings Restart Button failure:\n" + e.ToString()); }

            #region Logic
            RealSettingsExit.SetActive(false);
            if (RealSettingsExit.activeInHierarchy)
            {
                yield return new WaitForSeconds(2f);
                RealSettingsExit.SetActive(false);
            }
            #endregion
            yield break;
        }

        private IEnumerator GetAssembly()
        {
            Assembly assemblyCSharp = null;
            while (true) {
                assemblyCSharp = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.GetName().Name == "Assembly-CSharp");
                if (assemblyCSharp == null)
                    yield return null;
                else
                    break;
            }

            MelonCoroutines.Start(WaitForUiManagerInit(assemblyCSharp));
        }

        private IEnumerator WaitForUiManagerInit(Assembly assemblyCSharp)
        {
            Type vrcUiManager = assemblyCSharp.GetType("VRCUiManager");
            PropertyInfo uiManagerSingleton = vrcUiManager.GetProperties().First(pi => pi.PropertyType == vrcUiManager);
            while (uiManagerSingleton.GetValue(null) == null)
                yield return null;
            OnUiManagerInit();
        }
    }
}