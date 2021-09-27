using Furball.Engine.Engine.Graphics.Drawables.Managers;
using Microsoft.Xna.Framework;

namespace Furball.Engine.Engine {
    public class Screen : DrawableGameComponent {
        protected DrawableManager Manager = new();
        public Screen() : base(FurballGame.Instance) {}

        public override void Draw(GameTime gameTime) {
            this.Manager.Draw(gameTime, FurballGame.DrawableBatch);

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime) {
            this.Manager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Dispose(bool disposing) {
            this.Manager.Dispose(disposing);
            
            base.Dispose(disposing);
        }
    }
}
