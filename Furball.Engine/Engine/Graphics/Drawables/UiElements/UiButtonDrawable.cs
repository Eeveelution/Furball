using Furball.Engine.Engine.Graphics.Drawables.Tweens;
using Furball.Engine.Engine.Graphics.Drawables.Managers;
using Furball.Engine.Engine.Graphics.Drawables.Tweens.TweenTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using SpriteFontPlus;

namespace Furball.Engine.Engine.Graphics.Drawables.UiElements {
    public class UiButtonDrawable : ManagedDrawable {
        private string _text;
        public string Text => this._text;
        public TextDrawable TextDrawable;

        private float _margin;
        public float Margin => this._margin;

        public Color OutlineColor;
        public Color ButtonColor;
        public float OutlineThickness = 1f;

        public override Vector2 Size {
            get {
                (float textDrawableSizeX, float textDrawableSizeY) = this.TextDrawable.Size;

                return new Vector2(textDrawableSizeX + this._margin * 2f, textDrawableSizeY + this._margin * 2f);
            }
        }

        public UiButtonDrawable(string text, byte[] font, float size, Color buttonColor, Color textColor, Color outlineColor, float margin = 5f, CharacterRange[] charRange = null) {
            this.TextDrawable = new TextDrawable(font, text, size, charRange);
            this._margin      = margin;
            this._text        = text;

            this.TextDrawable.ColorOverride = textColor;
            this.OutlineColor               = outlineColor;
            this.ButtonColor                = buttonColor;
            this.ColorOverride              = buttonColor;

            this.OnHover += delegate {
                if (!this.Clickable) return;
                
                this.Tweens.Add(
                    new ColorTween(
                        TweenType.Color,
                        this.ButtonColor,
                        new Color(this.ButtonColor.R + 50, this.ButtonColor.G + 50, this.ButtonColor.B + 50),
                        this.TimeSource.GetCurrentTime(),
                        this.TimeSource.GetCurrentTime() + 150
                    )
                );
            };
            this.OnHoverLost += delegate {
                if (!this.Clickable) return;
            
                this.Tweens.Add(
                    new ColorTween(TweenType.Color, this.ColorOverride, this.ButtonColor, this.TimeSource.GetCurrentTime(), this.TimeSource.GetCurrentTime() + 150)
                );
            };
        }

        public override void Draw(GameTime time, SpriteBatch batch, DrawableManagerArgs args) {
            batch.FillRectangle(args.Position - args.Origin, this.Size, args.Color);
            batch.DrawRectangle(args.Position - args.Origin, this.Size, this.OutlineColor, this.OutlineThickness, args.LayerDepth);

            // FIXME: this is a bit of a hack, it should definitely be done differently
            DrawableManagerArgs tempArgs = args;
            tempArgs.Position.X += this._margin * args.Scale.X;
            tempArgs.Position.Y += this._margin * args.Scale.Y;
            tempArgs.Color      =  this.TextDrawable.ColorOverride;
            tempArgs.Scale      /= FurballGame.VerticalRatio;
            this.TextDrawable.Draw(time, batch, tempArgs);
        }
    }
}
