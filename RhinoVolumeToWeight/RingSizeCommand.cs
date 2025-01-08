using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using System;
using System.CodeDom;
using static RingSizes;



namespace RhinoVolumeToWeight
{
    public class RingSizeCommand : Command
    {
        //if you want the value metalIndex to persist, you need to define it at the class level here, not inside the RunCommand method.
        private double ringSize = 6.0;
        public RingSizeCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static RingSizeCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "RingSize";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {

            using (var go = new GetOption())
            {
                RhinoApp.WriteLine("Select a ring size wl");


                //prompt for an option from the user.
                OptionDouble optDouble = new OptionDouble(ringSize, 3.0, 12.0);
                go.AcceptNothing(true);
                go.SetCommandPrompt("Select a ring size");
                //go.AddOptionList("List", RingSizes.ringSizeArray, ringIndex);
                go.AddOptionDouble(EnglishName, ref optDouble , "select a ring size in US half sizes");
                var res = go.Get();
                if (res == GetResult.Option)
                {
                    var opt = go.Option();
                    ringSize = optDouble.CurrentValue;
                    RhinoApp.WriteLine("The current ring size is {0}", ringSize);
                    ///do stuff with the ring size
                    double diameter = 0;
                    try
                    {
                        diameter = RingSizes.ringSizeDict[ringSize];
                    }
                    catch (Exception e)
                    {
                        RhinoApp.WriteLine("The ring size {0} is not in the dictionary. Only full and half sizes are available.", ringSize);
                        return Result.Failure;
                    }
                    RhinoApp.WriteLine("the diameter is {0}", diameter);
                    Circle circle = new Circle(Plane.WorldZX, diameter / 2.0);
                    doc.Objects.AddCircle(circle);
                    doc.Views.Redraw();
                    return Result.Success;
                }

                else if (res == GetResult.Nothing)
                {
                    RhinoApp.WriteLine("The current ring size is {0}", ringSize);
                    ///do stuff with the ring size
                    double diameter = RingSizes.ringSizeDict[ringSize];
                    RhinoApp.WriteLine("the diameter is {0}", diameter);
                    Circle circle = new Circle(Plane.WorldZX, diameter / 2.0);
                    doc.Objects.AddCircle(circle);
                    doc.Views.Redraw();
                    return Result.Success;
                }
                else
                {
                    RhinoApp.WriteLine("Cancel");
                    return Result.Cancel;
                }
            }
        }
    }
}