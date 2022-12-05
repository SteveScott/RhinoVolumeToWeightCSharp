using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

namespace RhinoVolumeToWeight
{
    public class TestCommand : Command
    {
        public TestCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static TestCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "TestCommand";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: start here modifying the behaviour of your command.
            // ---
            RhinoApp.WriteLine("Select a volume");
            
            //get an object
            GetObject getObject = new GetObject();
            getObject.SetCommandPrompt("Select an object.");
            var result = getObject.Get();

            //get an integer
            Rhino.Input.Custom.OptionInteger optInteger = new Rhino.Input.Custom.OptionInteger(1, -1, 100);
            GetOption getIntOption = new GetOption();
            getIntOption.AddOptionInteger("optionInteger", ref optInteger);
            getIntOption.SetCommandPrompt("Please select an integer.");
            result = getIntOption.Get();
            Rhino.RhinoApp.WriteLine(" Integer = {0}", optInteger.CurrentValue);

            //get a material
            const string materialKey = "Material";
            string[] materialList = new string[] { "SterlingSilver", "Brass", "Bronze", "Gold_14K", "Gold_18K", "Gold_24K", "Platinum", "Paladium" };
            var materialIndex = Settings.GetInteger(materialKey, 0);
            var getOption = new GetOption();
            getOption.SetCommandPrompt("select option");
            while (true)
            {
                getOption.ClearCommandOptions();
                var materialOption = getOption.AddOptionList(materialKey, materialList, materialIndex);
                var res = getOption.Get();
                if (res == GetResult.Option)
                {
                    Rhino.RhinoApp.WriteLine("result was an option. Continuing.");
                    var option = getOption.Option();
                    if (option.Index == materialOption)
                    {
                        materialIndex = option.CurrentListOptionIndex;
                    }
                    continue;
                }
                else if (res == GetResult.Nothing)
                {
                    Rhino.RhinoApp.WriteLine("result was nothing.");
                    break;
                }
                else if (res == GetResult.Cancel)
                {
                    Rhino.RhinoApp.WriteLine("result was cancel");
                    return Result.Cancel;
                }
                else
                {
                    Rhino.RhinoApp.Write("the result fell through and was a {0}.", res);
                    return Result.Cancel;
                }

            }
            //this code is unreachable.
            Settings.SetInteger(materialKey, materialIndex);
            Rhino.RhinoApp.WriteLine("material selected is {0}", materialList[materialIndex]);
            //always says invalid selection for any number or string. Does not display options.
            //Throws an exception when I hit escape.
            return Result.Success;
            

        }
    }
}
