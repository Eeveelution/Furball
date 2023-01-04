using System;
using Furball.Engine.Engine.Input;
using Silk.NET.Input;

namespace Furball.Engine.Engine.Input.Events; 

public class MouseButtonEventArgs : EventArgs {
    public MouseButton  Button;
    public FurballMouse Mouse;
    
    public MouseButtonEventArgs(MouseButton button, FurballMouse mouse) {
        this.Button = button;
        this.Mouse  = mouse;
    }
}
