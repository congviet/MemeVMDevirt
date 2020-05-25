using dnlib.DotNet;
using dnlib.IO;
using System.Linq;
using static Context;


namespace MemeVMDevirt.Protections
{
    public static class AnalyseResources
    {
        public static void InitialiseResources()
        {
            EmbeddedResource resource = (from x in module.Resources where x.IsPrivate && x.ResourceType == ResourceType.Embedded select x).First() as EmbeddedResource;
            Stores.Resources_Virt = resource.Data.CreateStream();
        }
    }
}
