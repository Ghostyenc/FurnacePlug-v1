/* 
           ________               __
          / ____/ /_  ____  _____/ /___  __
         / / __/ __ \/ __ \/ ___/ __/ / / /
        / /_/ / / / / /_/ (__  ) /_/ /_/ /
        \____/_/ /_/\____/____/\__/\__, /
                                  /____/

This plugin is exclusively licensed to Enchanted.gg and may not be edited or sold without explicit permission.

© 2024 Ghosty & Enchanted.gg
*/

using System;
using System.Reflection;
using HarmonyLib;
using Oxide.Core;
using Oxide.Core.Plugins;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("Furnace Plug", "Ghosty", "1.0.0")]
    [Description("Modifies the rates and speed of furnaces")]
    public class FurnacePlug : RustPlugin
    {
        private Harmony _harmony;

        // Configuration variables
        private static int _fuelRateMultiplier = 1;
        private static int _charcoalRateMultiplier = 2;
        private static float _smeltingSpeedMultiplier = 2f;

        #region Oxide Hooks

        private void Init()
        {
            LoadConfig();

            try
            {
                ApplyPatches();
            }
            catch (Exception ex)
            {
                PrintError($"Failed to apply patches: {ex}");
            }
        }

        private void Unload()
        {
            if (_harmony != null)
            {
                try
                {
                    _harmony.UnpatchAll(_harmony.Id);
                }
                catch (Exception ex)
                {
                    PrintError($"Failed to unpatch: {ex}");
                }
                finally
                {
                    _harmony = null;
                }
            }
        }

        #endregion

        #region Harmony Patching

        private void ApplyPatches()
        {
            _harmony = new Harmony("FurnacePlug_" + Guid.NewGuid().ToString("N").Substring(0, 8));

            var baseOvenType = typeof(BaseOven);

            PatchMethod(baseOvenType, "GetFuelRate", nameof(GetFuelRatePatch));
            PatchMethod(baseOvenType, "GetCharcoalRate", nameof(GetCharcoalRatePatch));
            PatchMethod(baseOvenType, "GetSmeltingSpeed", nameof(GetSmeltingSpeedPatch));
        }

        private void PatchMethod(Type type, string methodName, string patchMethodName)
        {
            try
            {
                var originalMethod = AccessTools.Method(type, methodName);
                if (originalMethod == null)
                {
                    PrintError($"Could not find method {methodName} in {type.Name}");
                    return;
                }

                var patchMethod = AccessTools.Method(typeof(FurnacePlug), patchMethodName);
                if (patchMethod == null)
                {
                    PrintError($"Could not find patch method {patchMethodName} in FurnacePlug");
                    return;
                }

                _harmony.Patch(originalMethod, postfix: new HarmonyMethod(patchMethod));
                Puts($"Successfully patched {methodName}");
            }
            catch (Exception ex)
            {
                PrintError($"Failed to patch {methodName}: {ex}");
            }
        }

        // Harmony patches
        private static void GetFuelRatePatch(ref int __result)
        {
            __result *= _fuelRateMultiplier;
        }

        private static void GetCharcoalRatePatch(ref int __result)
        {
            __result *= _charcoalRateMultiplier;
        }

        private static void GetSmeltingSpeedPatch(ref float __result)
        {
            __result *= _smeltingSpeedMultiplier;
        }

        #endregion

        #region Configuration

        protected override void LoadConfig()
        {
            base.LoadConfig();
            try
            {
                _fuelRateMultiplier = Config.Get<int>("FuelRateMultiplier");
                _charcoalRateMultiplier = Config.Get<int>("CharcoalRateMultiplier");
                _smeltingSpeedMultiplier = Config.Get<float>("SmeltingSpeedMultiplier");
            }
            catch (Exception ex)
            {
                PrintError($"Error loading config: {ex.Message}");
                LoadDefaultConfig();
            }
        }

        protected override void LoadDefaultConfig()
        {
            Config.Clear();
            Config.Set("FuelRateMultiplier", 1);
            Config.Set("CharcoalRateMultiplier", 2);
            Config.Set("SmeltingSpeedMultiplier", 2f);
        }

        protected override void SaveConfig() => Config.Save();

        #endregion
    }
}
