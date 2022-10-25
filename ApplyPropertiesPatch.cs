using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better_Server_Banner
{
    
    [HarmonyPatch]
    static class ApplyPropertiesPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CharacterClassManager), nameof(CharacterClassManager.ApplyProperties))]

        public static void OnApplyProperties(CharacterClassManager __instance)
        {
            if (__instance != null && __instance.CurRole.team != Team.MTF)
            { 
                __instance.NetworkCurSpawnableTeamType = (byte)(__instance.CurRole.team + 1);
                __instance.NetworkCurUnitName = "";
            }
        }
    }
}
