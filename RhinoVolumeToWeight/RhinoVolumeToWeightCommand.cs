using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace RhinoVolumeToWeight
{
    public class RhinoVolumeToWeightCommand : Command
    {
        //if you want the value metalIndex to persist, you need to define it at the class level here, not inside the RunCommand method.
        private int metalIndex = 2;
        public RhinoVolumeToWeightCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static RhinoVolumeToWeightCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "MetalWeight";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: start here modifying the behaviour of your command.
            // ---
            RhinoApp.WriteLine("Select a volume");
            Rhino.Geometry.Mesh thisMesh;
            Rhino.Geometry.Brep thisBrep;

            double thisVolume; //in cubic mm
            using (GetObject getObject = new GetObject())
            {
                //select an object


                getObject.SetCommandPrompt("Select an object.");
                getObject.EnablePressEnterWhenDonePrompt(true);

                var result = getObject.Get();
                ObjRef objref = getObject.Object(0);
                thisBrep = objref.Brep();
                thisMesh = objref.Mesh();
                if (thisBrep == null && thisMesh == null)
                {
                    RhinoApp.WriteLine("Not an Object. It is a {0}", result);
                    return Result.Failure;
                }

                else
                {
                    if (thisMesh != null)
                    {
                        if (thisMesh.IsSolid)
                        {
                            var massProperties = VolumeMassProperties.Compute(thisMesh);
                            thisVolume = massProperties.Volume;
                            RhinoApp.WriteLine("the volume of the MESH is {0} cm^3", thisVolume / 1000);
                        }
                        else
                        {
                            RhinoApp.WriteLine("this mesh is not solid.");
                            return Result.Failure;
                        }
                    }
                    else
                    {
                        if (thisBrep.IsSolid)
                        {
                            var massProperties = VolumeMassProperties.Compute(thisBrep);
                            thisVolume = massProperties.Volume;
                            RhinoApp.WriteLine("The volume of this BREP is {0} cm^3", thisVolume / 1000);

                        }
                        else
                        {
                            RhinoApp.WriteLine("this mesh is not solid");
                            return Result.Failure;
                        }
                    }
                }
             
                var go = new GetOption();
                //get stored value
                int opList = go.AddOptionList("Metals", Densities.metals, metalIndex);
                go.AcceptNothing(true);
                go.SetCommandPrompt("Please select a material.");
                GetResult res = go.Get();

                if (res == GetResult.Option)
                { 
                    var option = go.Option();
                    metalIndex = go.Option().CurrentListOptionIndex;
                }
                
                //caluclate density
                Rhino.RhinoApp.WriteLine(" selection = {0}", Densities.metals[metalIndex]);
                double density = Densities.metalDensitites[Densities.metals[metalIndex]];
                Rhino.RhinoApp.WriteLine("density of {0} is {1} grams per cubic cm", Densities.metals[metalIndex], density * 1000);
                double mass = density * thisVolume;
                RhinoApp.WriteLine("The volume is {0}", thisVolume);
                RhinoApp.WriteLine("weight is {0} {1} of {2}.", mass, "grams", Densities.metals[metalIndex]);
                return Result.Success;
            }

        }
    }
}
