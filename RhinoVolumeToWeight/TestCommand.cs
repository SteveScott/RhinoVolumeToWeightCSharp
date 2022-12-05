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
            Rhino.Input.Custom.OptionInteger optInteger = new Rhino.Input.Custom.OptionInteger(1, -1, 100);
            //selects an object
                
            //get an integer
            GetInteger getInteger = new GetInteger();
            getInteger.SetCommandPrompt("Please select an integer.");
            result = getInteger.Get();
            Rhino.RhinoApp.WriteLine(" Integer = {0}", optInteger.CurrentValue);
            //always prints "Integer = 1"

            //get an option
            GetOption getOption = new GetOption();
            string[] listValues = new string[] { "Sterling Silver", "Brass", "Bronze", "14K Gold", "18K Gold", "24K Gold", "Platinum", "Paladium" };
            int listIndex = 0;
            getOption.AddOptionList("Material", listValues, listIndex);
            getOption.SetCommandPrompt("select option");
            getOption.Get();
            Rhino.RhinoApp.WriteLine("material selected is {0}", listValues[getOption.OptionIndex()]);
            //always says invalid selection for any number or string. Does not display options.
            //Throws an exception when I hit escape.
            return Result.Success;
            

        }
    }
}
