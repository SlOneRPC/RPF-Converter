using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceCreatorv2
{
    public class EngineAudio
    {
        public static ResourceCreator convertForm;

        public static void Start(object form)
        {
            convertForm = (ResourceCreator)form;

            // Check directories
            if (!Directory.Exists("./input"))
            {
                Directory.CreateDirectory("./input");
            }

                
            //TODO Implement
        }
    }
}
