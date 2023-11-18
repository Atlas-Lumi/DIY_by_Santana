using MelonLoader;
using HarmonyLib;
using UnityEngine;
using Il2Cpp;

namespace DiyBySantana
{

    public class ChangeLayerOfGear : MelonMod
    {
        public static GameObject? ChangeLayerGO;

        public static int layerNotInteraction = 28;
        public static int layerGear = 17;
        public static int changedLayerNum = layerGear;
        public static int changedLayerChild0Num = 0;

        static string[] targetGearNameArray = {
            "GEAR_DiyABoard2x1",
            "GEAR_DiyABoard6x1",
            "GEAR_DiyABoard4x2"
        };

        // ------------------------------------------------------------------------------------------------------------
        // Change Diy boards layer, Used by Implementations_OnSceneWasInitialized and Settings_OnConfirm
        //------------------------------------------------------------------------------------------------------------
        public static void DiyChangeLayerNum()
        {
            GameObject findTargetGO = new GameObject();

            for (int i = 0; i < targetGearNameArray.Length; i++)
            {
                findTargetGO = GameObject.Find(targetGearNameArray[i]);
                if (findTargetGO != null)
                {
                    break;
                }
            }
            if (findTargetGO == null)
            {
                return;
            }

            Transform gearsTrf = findTargetGO.transform.parent;

            if (gearsTrf == null)
            {
                MelonLogger.Msg(" ---------- DIY gearsTrf null");
                return;
            }

            int gearChildCount = gearsTrf.childCount;

            for (int i = 0; i < gearChildCount; i++)
            {
                for (int targetGearNameArrayNum = 0; targetGearNameArrayNum < targetGearNameArray.Length; targetGearNameArrayNum++)
                {
                    if (gearsTrf.GetChild(i).name == targetGearNameArray[targetGearNameArrayNum])
                    {

                        if (Settings.options.diyBoardsBoolVal == true)
                        { changedLayerNum = layerGear; }
                        else
                        { changedLayerNum = layerNotInteraction; }

                        gearsTrf.GetChild(i).gameObject.layer = changedLayerNum;

                        int childcount = gearsTrf.GetChild(i).gameObject.GetComponentInChildren<Transform>().childCount;
                        if (childcount > 0)
                        {
                            for (int j = 0; j < childcount; j++)
                            {
                                gearsTrf.GetChild(i).gameObject.transform.GetChild(j).gameObject.layer = changedLayerChild0Num;
                            }
                        }
                    }
                }
            }
        }

        // ------------------------------------------------------------------------------------------------------------
        // Drop
        //------------------------------------------------------------------------------------------------------------

        [HarmonyPatch(typeof(GearItem), "Drop")]
        internal class ChangeLayerOfGearDrop
        {
            private static void Postfix(GearItem __instance)
            {

                if (targetGearNameArray.Contains(__instance.gameObject.name))
                {
                }
                else
                {
                    return;
                }


                if (Settings.options.diyBoardsBoolVal == true)
                {
                    changedLayerNum = layerGear;
                }
                else
                {
                    changedLayerNum = layerNotInteraction;
                }

                __instance.gameObject.layer = changedLayerNum;

                int childcount = __instance.gameObject.GetComponentInChildren<Transform>().childCount;
                if (childcount == 0) return;

                for (int i = 0; i < childcount; i++)
                {
                    __instance.gameObject.transform.GetChild(i).gameObject.layer = changedLayerChild0Num;
                }

            }
        }

        // ------------------------------------------------------------------------------------------------------------
        // Inspect
        // ------------------------------------------------------------------------------------------------------------

        [HarmonyPatch(typeof(PlayerManager), "ExitInspectGearMode")]
        internal class ChangeLayerOfGearInspect
        {

            private static void Postfix(PlayerManager __instance)
            {
                if (__instance.m_Gear == null)
                {
                    return;
                }

                if (targetGearNameArray.Contains(__instance.m_Gear.gameObject.name))
                {

                }
                else
                {
                    return;
                }

                if (Settings.options.diyBoardsBoolVal == true)
                {
                    changedLayerNum = layerGear;
                }
                else
                {
                    changedLayerNum = layerNotInteraction;
                }

                __instance.m_Gear.gameObject.layer = changedLayerNum;


                int childcount = __instance.m_Gear.gameObject.GetComponentInChildren<Transform>().childCount;
                if (childcount == 0) return;

                for (int i = 0; i < childcount; i++)
                {
                    __instance.m_Gear.gameObject.transform.GetChild(i).gameObject.layer = changedLayerChild0Num;
                }

                __instance.m_Gear = null; //========== if it is not null, ExitMeshPlacement error occurs.

            }

        }

        // ------------------------------------------------------------------------------------------------------------
        // Placement
        // ------------------------------------------------------------------------------------------------------------

        [HarmonyPatch(typeof(PlayerManager), "ExitMeshPlacement")]
        internal class ChangeLayerOfGearPlacement
        {
            private static void Prefix(PlayerManager __instance)
            {

                ChangeLayerGO = null;

                if (__instance.m_ObjectToPlace == null)
                {
                    return;
                }

                if (targetGearNameArray.Contains(__instance.m_ObjectToPlace.name))
                {

                }
                else
                {
                    return;
                }

                ChangeLayerGO = __instance.m_ObjectToPlace.TryCast<GameObject>();

            }


            private static void Postfix(PlayerManager __instance)
            {

                if (ChangeLayerGO == null)
                {
                    return;
                }

                if (Settings.options.diyBoardsBoolVal == true)
                {
                    changedLayerNum = layerGear;
                }
                else
                {
                    changedLayerNum = layerNotInteraction;
                }

                ChangeLayerGO.layer = changedLayerNum;

                int childcount = ChangeLayerGO.GetComponentInChildren<Transform>().childCount;
                if (childcount == 0) return;

                for (int i = 0; i < childcount; i++)
                {
                    ChangeLayerGO.transform.GetChild(i).gameObject.layer = changedLayerChild0Num;
                }

            }

        }

        // ------------------------------------------------------------------------------------------------------------
        // Working gun racks
        //------------------------------------------------------------------------------------------------------------

        [HarmonyPatch(typeof(GearItem), "Awake")]
        internal class AddingPlacePointsToGunRack
        {
            private static void Postfix(GearItem __instance)
            {
                if (!__instance.gameObject.name.Contains("GEAR_DiyGunRackA")) return;

                if (__instance.gameObject.GetComponentInChildren<GearPlacePoint>()) return;

                GameObject gearPlacePoint = new GameObject();
                gearPlacePoint.name = "GearPlacePoint";
                gearPlacePoint.transform.parent = __instance.gameObject.transform;
                gearPlacePoint.transform.localPosition = new Vector3(0f, 0.04f, -0.085f);
                gearPlacePoint.transform.Rotate(new Vector3(354.8378f, 269.559f, 359.3606f));
                gearPlacePoint.transform.localScale = new Vector3(1f, 1f, 1f);

                GameObject collider = new GameObject();
                collider.name = "Collider";

                BoxCollider box = collider.AddComponent<BoxCollider>();
                collider.transform.parent = gearPlacePoint.transform;
                box.size = new Vector3(0.5f, 0.5f, 0.5f);
                collider.active = false;

                GearPlacePoint placePoint = gearPlacePoint.AddComponent<GearPlacePoint>();
                placePoint.m_AuthorizedGearPrefabs = new Il2CppSystem.Collections.Generic.List<GearItem>();
                placePoint.m_AuthorizedGearPrefabs.Add(GearItem.LoadGearItemPrefab("GEAR_Rifle"));
                placePoint.m_AuthorizedGearPrefabs.Add(GearItem.LoadGearItemPrefab("GEAR_Rifle_Barbs"));
                placePoint.m_AuthorizedGearPrefabs.Add(GearItem.LoadGearItemPrefab("GEAR_Rifle_Curators"));
                placePoint.m_AuthorizedGearPrefabs.Add(GearItem.LoadGearItemPrefab("GEAR_Rifle_Vaughns"));
                placePoint.m_ColliderObject = collider;

            }
        }
    }
}
