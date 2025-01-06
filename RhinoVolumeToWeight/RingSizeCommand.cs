using Rhino;
using Rhino.Commands;
using System;



namespace RhinoVolumeToWeight
{
    public class RingSize : Command
    {
        //if you want the value metalIndex to persist, you need to define it at the class level here, not inside the RunCommand method.
        private double ringSize = 2;
        public RingSize()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static RingSize Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "RingSize";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            return Rhino.Commands.Result.Failure;
        }
    }
}