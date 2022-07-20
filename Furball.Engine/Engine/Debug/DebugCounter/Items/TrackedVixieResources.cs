using Furball.Vixie.Backends.Shared;

namespace Furball.Engine.Engine.Debug.DebugCounter.Items; 

public class TrackedVixieResources : DebugCounterItem {

    public override string GetAsString(double time) => $"bt: {Global.TRACKED_TEXTURES.Count} brt: {Global.TRACKED_RENDER_TARGETS.Count}";
}