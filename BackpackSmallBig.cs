using ModShardLauncher;
using ModShardLauncher.Mods;
using System.Collections.Generic;

// using UndertaleModLib.Models;

namespace Speedshard_Backpack;

public class SpeedshardBackpack : Mod
{
    public override string Author => "uuiid";
    public override string Name => "BackpackSmallBig";
    public override string Description => "将小背包扩大哦";
    public override string Version => "1.0.0.0";
    public override string TargetVersion => "0.1.1.0";
#pragma warning disable CA1416
    public override void PatchMod()
    {
        // // create a new texture page item
        // string newTexturePageItemName = Msl.AddNewTexturePageItem(
        //     "Texture 30", // hardcoded; the embedded texture with the backpack has to be found by hand
        //     new RectTexture(592, 1446, 48, 75),
        //     new RectTexture(4, 2, 48, 75), // resizing the backpack
        //     new BoundingData<ushort>(54, 81)); // resizing the backpack
        //
        // // create a new sprite with only the previous texture page item
        // string biggerBackpackSpriteName = Msl.AddNewSprite(
        //     "s_inv_travellersbackpack_big",
        //     new List<string>() { newTexturePageItemName },
        //     new MarginData(1, 53, 80, 1),
        //     new OriginData(0, 0),
        //     new BoundingData<uint>(54, 81)
        // );


        // inject some lines
        Msl.LoadGML("gml_GlobalScript_scr_sessionDataInit")
            .MatchFrom("{")
            .InsertBelow(@"object_set_sprite(o_container_backpack_small, s_container_360)")
            .Save();

        // 小背包调整
        Msl.LoadGML("gml_Object_o_container_backpack_small_Other_10")
            .MatchAll()
            .ReplaceBy(@"
event_inherited();
closeButton = scr_adaptiveCloseButtonCreate(id, depth - 1, 229, 3);

with (closeButton)
    drawHover = true;

getbutton = scr_adaptiveTakeAllButtonCreate(id, depth - 1, 230, 27);

with (getbutton)
    owner = other.id;

itemsContainer = scr_guiCreateContainer(id, 77, depth, adaptiveOffsetX, adaptiveOffsetY);
var _cellsContainerTop = scr_inventory_cells_container_create(itemsContainer, 7, 4479, 0, 0);
scr_inventory_cells_add(id, _cellsContainerTop, 5);
")
            .Save();
//         Msl.LoadGML("gml_Object_o_inv_backpack_small_Create_0")
//             .MatchAll()
//             .ReplaceBy(@"
// event_inherited();
// slot = ""Back"";
// can_equip = true;
// container_type = 2432;
// drop_gui_sound = 956;
// pickup_sound = 958;
// cells_x_size = 7;
// cells_y_size = 5;
// stack_limit = 1200;
// ");
    }
}