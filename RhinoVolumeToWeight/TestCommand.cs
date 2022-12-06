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
            //the default option integer is always 0 because we are not storing it with Settings.SetInteger. The range of possible integers is -1 to 100.
            Rhino.Input.Custom.OptionInteger optInteger = new Rhino.Input.Custom.OptionInteger(0, -1, 100);
            GetOption getIntOption = new GetOption();
            //add OptionInteger to the GetOption class instance. 
            getIntOption.AddOptionInteger("optionInteger", ref optInteger);
            getIntOption.SetCommandPrompt("Please select an integer.");
            //Get awaits user input.
            result = getIntOption.Get();
            Rhino.RhinoApp.WriteLine(" Integer = {0}", optInteger.CurrentValue);

            //get an option
            const string optionsKey = "Options";
            //No whitespace in options. 
            string[] optionsList = new string[] { "foo", "bar", "baz" };
            //setting is long-term storage. It persists even between sessions. Zero is the default
            var optionIndex = Settings.GetInteger(optionsKey, 0);
            var getOption = new GetOption();
            //there is a loop below. To exit the loop in needs the user to hit the spacebar when all options are complete. 
            // AcceptNothing allows a Reult.Nothing to be passed when you hit spacebard, which we set as a break in the loop. Otherewise it will send a Result.Cancel, which returns.
            // Hitting Escape also results in Resutl.Cancel.
            getOption.AcceptNothing(true);
            getOption.SetCommandPrompt("select option.");
            //After getting the option, it will display the option agian getOption.Get() until you hit escape (cancel) or spacebar (nothing) to break the loop.
            while (true)
            {
                //clear out existing options. Don't think we need it.
                //getOption.ClearCommandOptions();

                //add the option list to the GetOption object.
                // Rhino will automatically assign letters.For example[foo, bar, baz] will underline f, b, a.
                var materialOption = getOption.AddOptionList(optionsKey, optionsList, optionIndex);
                //Accept user input. It will display all the options from materialList. 
                var res = getOption.Get();
                if (res == GetResult.Option)
                {
                    Rhino.RhinoApp.WriteLine("result was an option. Continuing.");
                    var option = getOption.Option();
                    if (option.Index == materialOption)
                    {
                        optionIndex = option.CurrentListOptionIndex;
                    }
                    continue; //after selecting an option from the optionList, returns to the prompt from getOption.Get() and asks if you want to set the option again.
                }
                else if (res == GetResult.Nothing)
                {
                    Rhino.RhinoApp.WriteLine("result was nothing. We break the infinite loop and continue to the final section.");
                    break;
                }
                else if (res == GetResult.Cancel)
                {
                    Rhino.RhinoApp.WriteLine("result was cancel");
                    return Result.Cancel; //end command prematurely by returning a Cancel result.
                }
                else
                {
                    Rhino.RhinoApp.Write("the result fell through and was a {0}.", res);
                    return Result.Cancel;
                }

            }
            //settings is long-term storage. Pushing the selected materialIndex to Settings for storage.
            Settings.SetInteger(optionsKey, optionIndex);
            Rhino.RhinoApp.WriteLine("option selected is {0}", optionsList[optionIndex]);
            return Result.Success;
            

        }
    }
}
