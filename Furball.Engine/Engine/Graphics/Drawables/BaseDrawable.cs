using System;
using System.Collections.Generic;
using Furball.Engine.Engine.Timing;
using Furball.Engine.Engine.Graphics.Drawables.Tweens;
using Furball.Engine.Engine.Graphics.Drawables.Tweens.TweenTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Furball.Engine.Engine.Graphics.Drawables {
    public enum OriginType {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        Center
    }

    public abstract class BaseDrawable {
        private Vector2 _position = Vector2.Zero;

        /// <summary>
        /// Unprocessed Position where the Drawable is expected to be drawn
        /// <remarks>This variable does not get changed as the DrawableManager translates the Drawable to be Scaled to be properly visible on all resolutions</remarks>
        /// </summary>
        public Vector2 Position {
            get => this._position;
            set {
                if (value == this._position)
                    return;
                this.OnMove?.Invoke(this, value);
                this._position = value;
            }
        }

        public event EventHandler<Vector2> OnMove;
        /// <summary>
        /// Unprocessed Size of the Drawable in Pixels
        /// <remarks>This variable does not get changed as the DrawableManager translates the Drawable to be Scaled to be properly visible on all resolutions</remarks>
        /// </summary>
        public virtual Vector2 Size { get; } = new();
        /// <summary>
        /// Is the Drawable Circular? (Used for Click detection and other hitboxes)
        /// </summary>
        public bool Circular = false;
        /// <summary>
        /// Radius of the Circle (Used for Click detection and other hitboxes)
        /// </summary>
        public float CircleRadius = 0f;
        /// <summary>
        /// Unprocessed Color Override of the Drawable, if a White Texture gets drawn with a red override, voila its red
        /// <remarks>This variable does not get changed as the DrawableManager translates the Drawable to be Scaled to be properly visible on all resolutions</remarks>
        /// </summary>
        public Color ColorOverride = Color.White;
        /// <summary>
        /// Unprocessed Rotation of the Drawable in Radians
        /// <remarks>This variable does not get changed as the DrawableManager translates the Drawable to be Scaled to be properly visible on all resolutions</remarks>
        /// </summary>
        public float Rotation = 0f;
        /// <summary>
        /// Unprocessed Scale of the Drawable, new Vector(1, 1) draws the Drawable at full scale
        /// <remarks>This variable does not get changed as the DrawableManager translates the Drawable to be Scaled to be properly visible on all resolutions</remarks>
        /// </summary>
        public Vector2 Scale = Vector2.One;
        /// <summary>
        /// Basic SpriteEffect, was provided by SpriteBatch so might aswell put it here
        /// </summary>
        public SpriteEffects SpriteEffect = SpriteEffects.None;
        /// <summary>
        /// What time does the Drawable go by? Used for Tweens
        /// </summary>
        public ITimeSource TimeSource = FurballGame.GameTimeSource;
        /// <summary>
        /// The draw depth of the Drawable
        /// </summary>
        public float Depth;
        /// <summary>
        /// List of Tweens
        /// </summary>
        public List<Tween> Tweens = new();
        /// <summary>
        /// The position of the Origin to render at
        /// </summary>
        public OriginType OriginType = OriginType.TopLeft;
        /// <summary>
        /// Whether to scale and move the drawable when the resolution changes
        /// </summary>
        public bool ResolutionScale = true;
        /// <summary>
        /// Whether a cursor is hovering over the drawable
        /// </summary>
        public bool IsHovered;
        /// <summary>
        /// Whether the drawable is being clicked
        /// </summary>
        public bool IsClicked;
        /// <summary>
        /// Is the Drawable being dragged?
        /// </summary>
        public bool IsDragging;
        /// <summary>
        /// Whether the drawable is able to be clicked
        /// </summary>
        public bool Clickable = true;
        /// <summary>
        /// Whether the drawable is able to be hovered on
        /// </summary>
        public bool Hoverable = true;
        /// <summary>
        /// Whether the drawable is visible or not
        /// </summary>
        public bool Visible = true;
        /// <summary>
        /// Called whenever a cursor hovers over the drawable
        /// </summary>
        public event EventHandler OnHover;
        /// <summary>
        /// Invokes the OnHover event
        /// </summary>
        /// <param name="sender">The sender of the OnHover event</param>
        public void InvokeOnHover(object sender) {
            this.OnHover?.Invoke(sender, null!);
        }
        /// <summary>
        /// Called whenever a cursor moves off of the drawable
        /// </summary>
        public event EventHandler OnHoverLost;
        /// <summary>
        /// Invokes the OnUnHover event
        /// </summary>
        /// <param name="sender">The sender of the OnUnHover event</param>
        public void InvokeOnHoverLost(object sender) {
            this.OnHoverLost?.Invoke(sender, null!);
        }
        /// <summary>
        /// Called when the drawable is clicked
        /// </summary>
        public event EventHandler<Point> OnClick;
        /// <summary>
        /// Invokes the OnClick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="position">The position of the click</param>
        public void InvokeOnClick(object sender, Point position) {
            this.OnClick?.Invoke(sender, position);
        }
        /// <summary>
        /// Called when the drawable is no longer being clicked
        /// </summary>
        public event EventHandler<Point> OnClickUp;
        /// <summary>
        /// Invokes the OnUnClick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="position">The position when the click was released</param>
        public void InvokeOnClickUp(object sender, Point position) {
            this.OnClickUp?.Invoke(sender, position);
        }
        /// <summary>
        /// Gets fired when the Drawable is first getting started to Drag
        /// </summary>
        public event EventHandler<Point> OnDragBegin;
        /// <summary>
        /// Invokes the OnDragBegin event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="position">The position when dragging began</param>
        public void InvokeOnDragBegin(object sender, Point position) {
            this.OnDragBegin?.Invoke(sender, position);
        }
        /// <summary>
        /// Gets fired every Input Frame for the duration of the drag
        /// </summary>
        public event EventHandler<Point> OnDrag;
        /// <summary>
        /// Invokes the OnDrag event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="position">Position where the cursor currently is</param>
        public void InvokeOnDrag(object sender, Point position) {
            this.OnDrag?.Invoke(sender, position);
        }
        /// <summary>
        /// Gets Fired when the Dragging stops
        /// </summary>
        public event EventHandler<Point> OnDragEnd;
        /// <summary>
        /// Invokes the OnDragEnd event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="position">Where dragging stopped</param>
        public void InvokeOnDragEnd(object sender, Point position) {
            this.OnDragEnd?.Invoke(sender, position);
        }
        /// <summary>
        /// Updates the pDrawables Tweens
        /// </summary>
        public void UpdateTweens() {
            for (int i = 0; i != this.Tweens.Count; i++) {
                Tween currentTween = this.Tweens[i];

                currentTween.Update(this.TimeSource.GetCurrentTime());

                switch (currentTween.TweenType) {
                    case TweenType.Color:
                        ColorTween colorTween = currentTween as ColorTween;

                        if (colorTween != null)
                            this.ColorOverride = colorTween.GetCurrent();
                        break;
                    case TweenType.Movement:
                        VectorTween vectorTween = currentTween as VectorTween;

                        if (vectorTween != null)
                            this.Position = vectorTween.GetCurrent();
                        break;
                    case TweenType.Scale:
                        VectorTween scaleTween = currentTween as VectorTween;

                        if (scaleTween != null)
                            this.Scale = scaleTween.GetCurrent();
                        break;
                    case TweenType.Rotation:
                        FloatTween rotationTween = currentTween as FloatTween;

                        if (rotationTween != null)
                            this.Rotation = rotationTween.GetCurrent();
                        break;
                    case TweenType.Fade:
                        FloatTween fadeTween = currentTween as FloatTween;

                        if (fadeTween != null)
                            this.ColorOverride.A = (byte)(255 * fadeTween.GetCurrent());
                        break;
                }
            }
        }
    }
}
