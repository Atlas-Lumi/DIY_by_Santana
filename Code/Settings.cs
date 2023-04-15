using ModSettings;
using UnityEngine;

namespace DiyBySantana
{
    internal class DiySettings : JsonModSettings
    {
        /*
        //[Section("Wood Color: Boards")]
        [Section("Wood Color")]

        [Name("DIY Board : R")]
        [Description("Material color multiplied by boards texture, Red.The game needs to be reloaded for the settings to be fully reflected.")]
        [Slider(0, 255)]
        public int colorBR = 255;

        [Name("DIY Board : G")]
        [Description("Material color multiplied by boards texture, Green.The game needs to be reloaded for the settings to be fully reflected.")]
        [Slider(0, 255)]
        public int colorBG = 255;

        [Name("DIY Board : B")]
        [Description("Material color multiplied by boards texture, Blue.The game needs to be reloaded for the settings to be fully reflected.")]
        [Slider(0, 255)]
        public int colorBB = 255;


        //[Section("Wood Color: Furniture")]

        [Name("DIY Furniture : R")]
        [Description("Material color multiplied by furniture texture, Red.The game needs to be reloaded for the settings to be fully reflected.")]
        [Slider(0, 255)]
        public int colorFR = 255;

        [Name("DIY Furniture : G")]
        [Description("Material color multiplied by furniture texture, Green.The game needs to be reloaded for the settings to be fully reflected.")]
        [Slider(0, 255)]
        public int colorFG = 255;

        [Name("DIY Furniture : B")]
        [Description("Material color multiplied by furniture texture, Blue.The game needs to be reloaded for the settings to be fully reflected.")]
        [Slider(0, 255)]
        public int colorFB = 255;


        //[Section("Wood Color: Tableware")]

        [Name("DIY Tableware : R")]
        [Description("Material color multiplied by tableware texture, Red.The game needs to be reloaded for the settings to be fully reflected.")]
        [Slider(0, 255)]
        public int colorTR = 255;

        [Name("DIY Tableware : G")]
        [Description("Material color multiplied by tableware texture, Green.The game needs to be reloaded for the settings to be fully reflected.")]
        [Slider(0, 255)]
        public int colorTG = 255;

        [Name("DIY Tableware : B")]
        [Description("Material color multiplied by tableware texture, Blue.The game needs to be reloaded for the settings to be fully reflected.")]
        [Slider(0, 255)]
        public int colorTB = 255;
        */

        [Section("DIY board : Interaction")]

        [Name("Enable Interaction mode ")]
        [Description("Do you want to be able to pick up DIY boards that are placed? If NO is chosen you cannot interact with DIY boards.")]
        public bool diyBoardsBoolVal = true;

        protected override void OnConfirm()
        {
            base.OnConfirm();

            /*
            DiyChangeColor.ChangeWood("Board");
            DiyChangeColor.ChangeWood("Furniture");
            DiyChangeColor.ChangeWood("Tableware");
            */

            ChangeLayerOfGear.DiyChangeLayerNum();
        }
    }


    internal static class Settings
    {
        public static DiySettings options;

        public static void OnLoad()
        {
            options = new DiySettings();
            options.AddToModSettings("DIY Settings", MenuType.Both);
        }
    }


}