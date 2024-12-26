﻿using Rhino;
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

            double thisVolume; //in cubic mm
            using (GetObject getObject = new GetObject())
            {
                //select an object

                int listIndex = 0;
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

                Rhino.Input.Custom.OptionInteger optInteger = new Rhino.Input.Custom.OptionInteger(3, 0, Densities.metals.Length);
                var go = new GetOption();
                //int optInteger2 = go.AddOptionInteger("MetalOption", ref optInteger);
                int opList = go.AddOptionList("Metals", Densities.metals, listIndex);
                go.SetCommandPrompt("Please select a material.");
                GetResult res = go.Get();
                if (res != GetResult.Option)
                {
                    RhinoApp.WriteLine("invalid input. please select a material");
                    return Result.Failure;
                }
                var option = go.Option();
                int metalIndex = option.CurrentListOptionIndex;
                //caluclate density
                Rhino.RhinoApp.WriteLine(" selection = {0}", Densities.metals[metalIndex]);
                double density = Densities.metalDensitites[Densities.metals[metalIndex]];
                Rhino.RhinoApp.WriteLine("density of {0} is {1} grams per cubic cm", Densities.metals[metalIndex], density * 1000);
                double mass = density * thisVolume;
                RhinoApp.WriteLine("The volume is {0}", thisVolume);
                RhinoApp.WriteLine("weight is {0} {1} of {2}.", mass, "grams", Densities.metals[metalIndex]);
                return Result.Success;
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
