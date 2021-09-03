using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULTRAKIT.Data.ScriptableObjects.Registry;
using ULTRAKIT.Loader.Registries;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ULTRAKIT.Loader.Injectors
{
    [HarmonyPatch(typeof(VariationInfo))]
    public static class VariationInfoInjections
    {
        [HarmonyPatch("ChangeEquipment")]
        [HarmonyPrefix]
        public static bool ChangeEquipmentPrefix(VariationInfo __instance)
        {
            return __instance.enabled;
        }
    }

    //TODO: single create button function that accepts an index and creates a correctly positioned button
    [HarmonyPatch(typeof(ShopGearChecker))]
    public static class ShopInjector
    {
        static GameObject buttonTemplate, panelTemplate, variantTemplate;
        static int buttonHeight = 30, variantHeight = 80;

        static Dictionary<UKContentWeapon, GameObject> panels;
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

                buttonTemplate = __instance.GetComponentInChildren<ShopButton>(true).gameObject;
                variantTemplate = __instance.GetComponentInChildren<VariationInfo>(true).gameObject;
                panelTemplate = variantTemplate.transform.parent.gameObject;

                panels = CreatePanels(__instance.gameObject);
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
                foreach (var panel in panels.Values)
                {
                    vanillaButton.toDeactivate = vanillaButton.toDeactivate.AddItem(panel).ToArray();
                }
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


            var top = (80) - (buttonHeight * i);
            var rect = go.GetComponent<RectTransform>();
            rect.offsetMax = new Vector3(-100, top);
            rect.offsetMin = new Vector3(-260, top - buttonHeight);

            go.GetComponentInChildren<Text>().text = weap.Name.ToUpper();

            go.GetComponent<ShopButton>().deactivated = true;
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                foreach(var pair in panels)
                {
                    pair.Value.SetActive(false);
                }

                foreach(Transform obj in parent.transform)
                {
                    if(obj == parent.transform)
                    {
                        continue;
                    }

                    if (obj.GetComponentInChildren<VariationInfo>(true) != null)
                    {
                        obj.gameObject.SetActive(panels[weap] == obj.gameObject);
                    }
                }
            });

            go.AddComponent<HudOpenEffect>();
            return go;
        }

        static GameObject CreatePageButton()
        {
            var pageGo = GameObject.Instantiate(buttonTemplate, buttonTemplate.transform.parent);

            var pageTop = (-10) - (buttonHeight * 4);
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

        static Dictionary<UKContentWeapon, GameObject> CreatePanels(GameObject parent)
        {
            var res = new Dictionary<UKContentWeapon, GameObject>();
            var allWeaps = AddonLoader.GetAll<UKContentWeapon>();
            foreach(var weap in allWeaps)
            {
                var go = GameObject.Instantiate(panelTemplate, parent.transform);
                foreach (Transform child in go.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                int i = 0;
                foreach(var variant in weap.Variants)
                {
                    CreateVariantOption(go, weap, i);
                    i++;
                }

                res.Add(weap, go);
            }

            return res;
        }

        static GameObject CreateVariantOption(GameObject panel, UKContentWeapon weapon, int i)
        {
            var go = GameObject.Instantiate(variantTemplate, panel.transform);
            var info = go.GetComponent<VariationInfo>();
            info.enabled = false;

            var cbget = info.GetComponentInChildren<ColorBlindGet>();
            cbget.GetComponent<Image>().sprite = weapon.Icon;
            cbget.variationNumber = i;
            info.transform.Find("Text").GetComponent<Text>().text = weapon.Variants[i].name;
            info.transform.Find("Text (1)").gameObject.SetActive(false);

            // very hacky
            var equipmentStuffs = info.transform.Find("EquipmentStuffs");
            Button lb = equipmentStuffs.Find("PreviousButton").GetComponent<Button>();
            Button rb = equipmentStuffs.Find("NextButton").GetComponent<Button>();

            Image img = info.equipButton.transform.GetChild(0).GetComponent<Image>();

            var isEquipped = GunSetterPatch.equippedDict[weapon.Variants[i]];
            img.sprite = info.equipSprites[isEquipped ? 1 : 0];

            
            UnityAction del = () =>
            {
                GunSetterPatch.equippedDict[weapon.Variants[i]] = !GunSetterPatch.equippedDict[weapon.Variants[i]];
                img.sprite = info.equipSprites[GunSetterPatch.equippedDict[weapon.Variants[i]] ? 1 : 0];
                MonoSingleton<GunSetter>.Instance.ResetWeapons();
            };

            lb.onClick.AddListener(del);
            rb.onClick.AddListener(del);

            go.GetComponent<RectTransform>().offsetMax = new Vector2(0, 160 - (i * variantHeight));
            go.GetComponent<RectTransform>().offsetMin = new Vector2(-360, 80 - (i * variantHeight));

            //todo: equip buttons

            return go;
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
