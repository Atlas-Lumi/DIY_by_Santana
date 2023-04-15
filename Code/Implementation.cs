using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace DiyBySantana;
internal class Implementation : MelonMod
    {
	    public override void OnInitializeMelon()
	    {
            Settings.OnLoad();
        }

        public override void OnSceneWasInitialized(int level, string name)
        {
            /*
            DiyChangeColor.ChangeWood("Board");
            DiyChangeColor.ChangeWood("Furniture");
            DiyChangeColor.ChangeWood("Tableware");
            */
            ChangeLayerOfGear.DiyChangeLayerNum();
        }

    }


// Not working

//---------------------------------------------------------------------------------------
// Break Down
// Add nail box if Yield has Reclaimed Wood.
//---------------------------------------------------------------------------------------

/*
[HarmonyPatch(typeof(BreakDown), "DoBreakDown")]
internal class BreakDownGetNail
{
    private static void Prefix(BreakDown __instance)
    {
        GameObject DiyBreakDownObj = __instance.gameObject;
        string DiyBreakDownObjName = DiyBreakDownObj.name;

        int DiyArrayLenght = __instance.m_YieldObject.Length;

        string DiyAddedYield = "GEAR_DiyANailBoxC";
        GameObject DiyAddedYieldObj = Resources.Load(DiyAddedYield).TryCast<GameObject>();
        GearItem DiyAddedYieldGearItem = DiyAddedYieldObj.GetComponent<GearItem>();
        int DiyAddedYieldObjUnit = 1;

        bool doAddNail = false;

        string DiyRWood = "GEAR_ReclaimedWoodB";
        var NoNailLst = new List<string>() { "Plank" };

        for (int i = 0; i < DiyArrayLenght; i++)
        {
            if (DiyBreakDownObj.GetComponent<BreakDown>().m_YieldObject[i].name == DiyRWood)
            {
                doAddNail = true;
                if (DiyBreakDownObj.GetComponent<BreakDown>().m_YieldObjectUnits[i] > 6) DiyAddedYieldObjUnit = 2;
            }
        }
        if (doAddNail == false) return;

        for (int i = 0; i < NoNailLst.Count; i++)
        {
            if (DiyBreakDownObjName.Contains(NoNailLst[i])) return;
        }

        GameObject[] tempArrayYo = new GameObject[DiyArrayLenght];
        int[] tempArrayYoU = new int[DiyArrayLenght];
        DiyBreakDownObj.GetComponent<BreakDown>().m_YieldObject.CopyTo(tempArrayYo, 0);
        DiyBreakDownObj.GetComponent<BreakDown>().m_YieldObjectUnits.CopyTo(tempArrayYoU, 0);
        DiyBreakDownObj.GetComponent<BreakDown>().m_YieldObject = new GameObject[DiyArrayLenght + 1];
        DiyBreakDownObj.GetComponent<BreakDown>().m_YieldObjectUnits = new int[DiyArrayLenght + 1];

        for (int i = 0; i < DiyArrayLenght; i++)
        {
            DiyBreakDownObj.GetComponent<BreakDown>().m_YieldObject[i] = tempArrayYo[i];
            DiyBreakDownObj.GetComponent<BreakDown>().m_YieldObjectUnits[i] = tempArrayYoU[i];
        }
        DiyBreakDownObj.GetComponent<BreakDown>().m_YieldObject[DiyArrayLenght] = DiyAddedYieldObj;
        DiyBreakDownObj.GetComponent<BreakDown>().m_YieldObjectUnits[DiyArrayLenght] = DiyAddedYieldObjUnit;

    }
}
*/
