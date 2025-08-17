using mahak.Hacks;
using mahak.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace mahak.GUI.Windows
{
    public class WindowManager : MonoBehaviour
    {
        public static WindowManager Instance { get; private set; }
        public bool mainWindowOpen = false;

        #region CATAGORIES

        public FourPairValue<bool, Rect, Action<int>, string>[] windows =
            [
                new(false, CreateCenteredRect(), GetWindowContents, "Movement"), // Movement
                new(false, CreateCenteredRect(), GetWindowContents, "Casting"), // Casting
                new(false, CreateCenteredRect(), GetWindowContents, "Item"), // Item
                new(false, CreateCenteredRect(), GetWindowContents, "Troll"), // Troll
                new(false, CreateCenteredRect(), GetWindowContents, "Misc") // Misc
            ];

        

        public List<TwoPairValue<Catagory, BaseHack[]>> catagoryKeys = new();

        public static void GetWindowContents(int id)
        {
            id -= 1;

            BaseHack[] catagoryHacks = GetCatagoryHacks(Instance.catagoryKeys[id].First);

            if (catagoryHacks.Length == 0)
            {
                GUILayout.Label("No hacks available in this category.");
            }
            else
            {

                foreach (BaseHack hack in catagoryHacks)
                {
                    if (GUILayout.Button($"{hack.Name} ({(hack.IsEnabled ? "On" : "Off")})"))
                    {
                        hack.IsEnabled = !hack.IsEnabled;
                        if (hack.IsEnabled)
                        {
                            hack.OnEnable();
                        }
                        else
                        {
                            hack.OnDisable();
                        }
                    }
                }
            }


            UnityEngine.GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }

        public static BaseHack[] GetCatagoryHacks(Catagory catagory)
        {
            return Instance.catagoryKeys.FirstOrDefault(c => c.First == catagory)?.Second ?? Array.Empty<BaseHack>();
        }
        #endregion

        private void Awake()
        {
            Instance = this;

            catagoryKeys.Add(new TwoPairValue<Catagory, BaseHack[]>(Catagory.Movement, Plugin.hacks.Where(h => h.Catagory == Catagory.Movement).ToArray()));
            catagoryKeys.Add(new TwoPairValue<Catagory, BaseHack[]>(Catagory.Casting, Plugin.hacks.Where(h => h.Catagory == Catagory.Casting).ToArray()));
            catagoryKeys.Add(new TwoPairValue<Catagory, BaseHack[]>(Catagory.Item, Plugin.hacks.Where(h => h.Catagory == Catagory.Item).ToArray()));
            catagoryKeys.Add(new TwoPairValue<Catagory, BaseHack[]>(Catagory.Troll, Plugin.hacks.Where(h => h.Catagory == Catagory.Troll).ToArray()));
            catagoryKeys.Add(new TwoPairValue<Catagory, BaseHack[]>(Catagory.Misc, Plugin.hacks.Where(h => h.Catagory == Catagory.Misc).ToArray()));
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                Cursor.visible = !mainWindowOpen;
                Cursor.lockState = !mainWindowOpen ? CursorLockMode.None : CursorLockMode.Locked;
                mainWindowOpen = !mainWindowOpen;
            }
        }

        Rect mainWindowRect = CreateCenteredRect();

        private void OnGUI()
        {
            if (mainWindowOpen)
            {
                mainWindowRect = GUILayout.Window(0, mainWindowRect, MainWindow, "Generic Mage Arena hack");

                foreach (FourPairValue<bool, Rect, Action<int>, string> window in windows)
                {
                    if (window.first)
                    {
                        int windowIndex = windows.ToList().IndexOf(window) + 1;
                        window.second = GUILayout.Window(windowIndex, window.second, id => window.third(id), window.fourth);
                    }
                }
            }
        }

        void MainWindow(int id)
        {
            foreach (FourPairValue<bool, Rect, Action<int>, string> window in windows)
            {
                if (GUILayout.Button($"{(window.first ? "Close" : "Open")} {window.fourth} Window"))
                {
                    window.first = !window.first;
                }
            }
        }

        public static Rect CreateCenteredRect()
        {
            return new Rect(Screen.width / 4, Screen.height / 4, Mathf.RoundToInt((Screen.width / 2) - Screen.width / 4), Mathf.RoundToInt((Screen.height / 2) - Screen.height / 4));
        }
    }
}
