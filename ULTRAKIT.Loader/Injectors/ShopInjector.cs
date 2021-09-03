using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using UnityEngine;
using UnityEngine.UI;

namespace ULTRAKIT.Loader.Injectors
{
    //TODO: single create button function that accepts an index and creates a correctly positioned button
    [HarmonyPatch(typeof(ShopGearChecker))]
    public static class ShopInjector
    {
        static GameObject buttonTemplate;
        static int height = 30;

        static List<List<GameObject>> pages;
        static int page;

        static GameObject pageButton;
        

        [HarmonyPatch("OnEnable")]
        [HarmonyPostfix]
        public static void TurnOnPostfix(ShopGearChecker __instance)
        {
            if (!pageButton)
            {
                page = 0;

                buttonTemplate = __instance.GetComponentInChildren<ShopButton>().gameObject;

                pages = CreatePages(__instance.gameObject);
                pageButton = CreatePageButton();
            }

            UpdatePage();
        }

        static List<List<GameObject>> CreatePages(GameObject parent)
        {
            var res = new List<List<GameObject>>();

            // Vanilla page
            res.Add(new List<GameObject>());
            foreach(var vanillaButton in parent.GetComponentsInChildren<ShopButton>())
            {
                res[0].Add(vanillaButton.gameObject);
            }
            
            // Custom weapon pages
            var allWeaps = AddonLoader.GetAll<UKContentWeapon>();
            int curPage = 1;
            int curWeap = 0;
            res.Add(new List<GameObject>());
            foreach(var weap in allWeaps)
            {
                res[curPage].Add(CreateWeaponButton(parent, weap, curWeap));

                curWeap++;
                if(curWeap > 5)
                {
                    res.Add(new List<GameObject>());
                    curPage++;
                    curWeap = 0;
                }
            }

            return res;
        }

        static GameObject CreateWeaponButton(GameObject parent, UKContentWeapon weap, int i)
        {
            var go = GameObject.Instantiate(buttonTemplate, parent.transform);


            var top = (80) - (height * i);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMax = new Vector3(-100, top);
            rect.offsetMin = new Vector3(-260, top - height);

            go.GetComponentInChildren<Text>().text = weap.Name.ToUpper();

            go.GetComponent<ShopButton>().deactivated = true;
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                //TODO: make weapon panel appear
            });

            go.AddComponent<HudOpenEffect>();
            return go;
        }

        static GameObject CreatePageButton()
        {
            var pageGo = GameObject.Instantiate(buttonTemplate, buttonTemplate.transform.parent);

            var pageTop = (-10) - (height * 4);
            var pageRect = pageGo.GetComponent<RectTransform>();
            pageRect.offsetMax = new Vector3(-100, -130);
            pageRect.offsetMin = new Vector3(-260, -160);

            pageGo.GetComponentInChildren<Text>().text = $"PAGE {page+1}";

            pageGo.GetComponent<ShopButton>().deactivated = true;
            pageGo.GetComponent<Button>().onClick.AddListener(() =>
            {
                page++;
                UpdatePage();
            });

            pageGo.AddComponent<HudOpenEffect>();
            return pageGo;
        }

        static void UpdatePage()
        {
            page = (int)Mathf.Repeat(page, pages.Count);

            pageButton.GetComponentInChildren<Text>().text = $"PAGE {page + 1}";
            var index = 0;
            foreach (var pageList in pages)
            {
                foreach (var pageObj in pageList)
                {
                    pageObj.SetActive(page == index);

                }
                index++;
            }
        }
    }

}
