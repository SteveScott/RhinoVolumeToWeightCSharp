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
        public RhinoVolumeToWeightCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static RhinoVolumeToWeightCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "RhinoVolumeToWeightCommand";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: start here modifying the behaviour of your command.
            // ---
            RhinoApp.WriteLine("Select a volume");
            Rhino.Geometry.Mesh thisMesh;
            Rhino.Geometry.Brep thisBrep;
  
            double thisVolume;
            using (GetObject getObject = new GetObject())
            {

                getObject.SetCommandPrompt("Please select a volume.");
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
                        if (thisMesh.IsSolid) {
                            var massProperties = VolumeMassProperties.Compute(thisMesh);
                            RhinoApp.WriteLine("the volume of the MESH is {0}", massProperties.Volume);
                        }
                        else
                        {
                            RhinoApp.WriteLine("this mesh is not solid.");
                            return Result.Failure;
                        }


                    }
                    else
                    {
                        if (thisBrep.IsSolid){
                            var massProperties = VolumeMassProperties.Compute(thisBrep);
                            RhinoApp.WriteLine("The volume of this BREP is {0}", massProperties.Volume);
                        }
                        else
                        {
                            RhinoApp.WriteLine("this mesh is not solid");
                            return Result.Failure;
                        }
                    }
                    return Result.Success;

                }    
                
            }
            
            /*
            Point3d pt0;
            using (GetPoint getPointAction = new GetPoint())
            {
                getPointAction.SetCommandPrompt("Please select the start point");
                if (getPointAction.Get() != GetResult.Point)
                {
                    RhinoApp.WriteLine("No start point was selected.");
                    return getPointAction.CommandResult();
                }
                pt0 = getPointAction.Point();
            }

            Point3d pt1;
            using (GetPoint getPointAction = new GetPoint())
            {
                getPointAction.SetCommandPrompt("Please select the end point");
                getPointAction.SetBasePoint(pt0, true);
                getPointAction.DynamicDraw +=
                  (sender, e) => e.Display.DrawLine(pt0, e.CurrentPoint, System.Drawing.Color.DarkRed);
                if (getPointAction.Get() != GetResult.Point)
                {
                    RhinoApp.WriteLine("No end point was selected.");
                    return getPointAction.CommandResult();
                }
                pt1 = getPointAction.Point();
            }

            doc.Objects.AddLine(pt0, pt1);
            doc.Views.Redraw();
            RhinoApp.WriteLine("The {0} command added one line to the document.", EnglishName);
            */
            // ---

        }
    }
}
