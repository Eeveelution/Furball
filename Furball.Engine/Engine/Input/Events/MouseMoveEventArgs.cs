using System;
using System.Numerics;

namespace Furball.Engine.Engine.Input.Events; 

public class MouseMoveEventArgs : EventArgs {
    public Vector2 Position;
    public int     MouseId;

    public MouseMoveEventArgs(Vector2 position, int mouse) {
        this.Position = position;
        this.MouseId  = mouse;
    }
}
