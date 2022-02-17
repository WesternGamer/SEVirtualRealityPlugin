using HarmonyLib;
using OpenVRWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VRageMath;

namespace ClientPlugin.Patches
{
    [HarmonyPatch]
    internal class Patch_MyStereoStencilMask
    {
        private static readonly Type StereoStencilMaskType = Type.GetType("VRageRender.MyStereoStencilMask, VRage.Render11", true);

        private static readonly MethodBase InitInternalMethod = AccessTools.Method(StereoStencilMaskType, "InitInternal");

        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(StereoStencilMaskType, "InitUsingOpenVR");
        }

        private static void Postfix(Vector2[] ___m_VBdata)
        {
            ___m_VBdata = MyOpenVR.GetStencilMask();
            InitInternalMethod.Invoke(null, new object[] { ___m_VBdata });
        }
    }
}
