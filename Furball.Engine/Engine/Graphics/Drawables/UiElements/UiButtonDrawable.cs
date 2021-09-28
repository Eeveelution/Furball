using Furball.Engine.Engine.Graphics.Drawables.Tweens;
using Furball.Engine.Engine.Graphics.Drawables.Managers;
using Furball.Engine.Engine.Graphics.Drawables.Tweens.TweenTypes;
using Microsoft.Xna.Framework;
using SpriteFontPlus;

namespace Furball.Engine.Engine.Graphics.Drawables.UiElements {
    /// <summary>
    /// Creates a Simple Button Object
    /// </summary>
    public class UiButtonDrawable : ManagedDrawable {
        private string _text;
        public string Text => this._text;
        public TextDrawable TextDrawable;

        private float _margin;
        public float Margin => this._margin;

        public Color OutlineColor;
        public Color ButtonColor;

        public Color TextColor {
            get => this.TextDrawable.ColorOverride;
            set => this.TextDrawable.ColorOverride = value;
        }
        public float OutlineThickness = 2f;

        public Vector2 ButtonSize = new();

        public override Vector2 Size {
            get {
                if(this.ButtonSize == Vector2.Zero) {
                    (float textDrawableSizeX, float textDrawableSizeY) = this.TextDrawable.Size;

                    return new Vector2(textDrawableSizeX + this._margin * 2f, textDrawableSizeY + this._margin * 2f) * this.Scale;
                } 
                
                return this.ButtonSize;
            }
        }

        /// <summary>
        /// Creates a button
        /// </summary>
        /// <param name="position">Where to Draw the Button</param>
        /// <param name="text">What text should the button display?</param>
        /// <param name="font">What SpriteFont to use</param>
        /// <param name="textSize">What size to Draw at</param>
        /// <param name="buttonColor">Button Background Color</param>
        /// <param name="textColor">Button Text Color</param>
        /// <param name="outlineColor">Button Outline Color</param>
        /// <param name="buttonSize">The size of the button, set to Vector2.Zero for it to auto calculate</param>
        /// <param name="margin">Beyley explain</param>
        /// <param name="charRange">SpriteFont character range</param>
        public UiButtonDrawable(Vector2 position, string text, byte[] font, float textSize, Color buttonColor, Color textColor, Color outlineColor, Vector2 buttonSize, float margin = 5f, CharacterRange[] charRange = null) {
            this.Position     = position;
            this.TextDrawable = new TextDrawable(Vector2.Zero, font, text, textSize, charRange);
            this._margin      = margin;
            this._text        = text;

            this.TextColor     = textColor;
            this.OutlineColor  = outlineColor;
            this.ButtonColor   = buttonColor;
            this.ColorOverride = buttonColor;
            this.ButtonSize    = buttonSize;

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

        public override void Draw(GameTime time, DrawableBatch batch, DrawableManagerArgs args) {
            batch.ShapeBatch.DrawRectangle(
                args.Position * FurballGame.VerticalRatio, 
                this.Size * FurballGame.VerticalRatio, 
                args.Color, 
                this.OutlineColor, 
                this.OutlineThickness * FurballGame.VerticalRatio
            );

            // FIXME: this is a bit of a hack, it should definitely be done differently
            DrawableManagerArgs tempArgs = args;
            if(this.ButtonSize == Vector2.Zero) {
                tempArgs.Position.X += this._margin;
                tempArgs.Position.Y += this._margin;
            } else {
                (float textX, float textY) = this.TextDrawable.Size;

                tempArgs.Position.X += this.ButtonSize.X / 2 - textX / 2;
                tempArgs.Position.Y += this.ButtonSize.Y / 2 - textY / 2;
            }
            tempArgs.Color      =  this.TextDrawable.ColorOverride;
            this.TextDrawable.Draw(time, batch, tempArgs);
        }
    }
}
