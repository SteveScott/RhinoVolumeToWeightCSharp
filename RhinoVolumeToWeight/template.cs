/*
protected override Result RunCommand(RhinoDoc doc, RunMode mode)
{
  const string materialKey = "Material";
  var materialList = new string[] { "Brass", "Bronze", "Gold_14k", "Gold_18K", "Gold_24K", "Platinum", "Paladium", "SterlingSilver" };

  // Get persistent settings
  var materialIndex = Settings.GetInteger(materialKey, 0);

  var getOption = new GetOption();
  getOption.SetCommandPrompt("Choose command option");
  getOption.AcceptNothing(true);
  while (true)
  {
    getOption.ClearCommandOptions();
    var materialOption = getOption.AddOptionList(materialKey, materialList, materialIndex);
    var res = getOption.Get();
    if (res == GetResult.Option)
    {
      var option = getOption.Option();
      if (option.Index == materialOption)
        materialIndex = option.CurrentListOptionIndex;
      continue;
    }
    else if (res == GetResult.Nothing)
    {
      break;
    }
    else
    {
      return Result.Cancel;
    }
  }

  // Set persistent settings
  Settings.SetInteger(materialKey, materialIndex);

  return Result.Success;
}
*/