using HarmonyLib;
using OpenVRWrapper;
using System;
using System.Reflection;
using VRageRender;

namespace ClientPlugin.Patches
{
    [HarmonyPatch]
    internal static class Patch_MyRender11
    {
        private static readonly Type RenderType = Type.GetType("VRageRender.MyRender11, VRage.Render11", true);

        private static readonly Type StereoStencilMaskType = Type.GetType("VRageRender.MyStereoStencilMask, VRage.Render11", true);

        private static readonly MethodBase InitUsingOpenVRMethod = AccessTools.Method(StereoStencilMaskType, "InitUsingOpenVR");

        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(RenderType, "InitSubsystems");
        }

        private static void Postfix(MyRenderDeviceSettings ___m_settings)
        {
            try
            {
                if (___m_settings.UseStereoRendering)
                {
                    new MyOpenVR();
                    InitUsingOpenVRMethod.Invoke(null, new object[] { });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
