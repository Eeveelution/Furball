using System.Numerics;
using Furball.Engine;
using Furball.Engine.Engine;
using Furball.Engine.Engine.Graphics.Drawables.UiElements;

namespace Furball.Game.Screens {
    public class TextBoxTest : Screen {
        public override void Initialize() {
            base.Initialize();

            this.Manager.Add(new UiTextBoxDrawable(new Vector2(10, 10), FurballGame.DEFAULT_FONT, "hi", 24, 240));
        }
    }
}
