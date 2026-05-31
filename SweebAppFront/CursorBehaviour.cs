
#if WINDOWS
using SweebAppFront.Platforms.Windows;
#endif

using SweebAppFront.Enum;

namespace SweebAppFront.Behaviors
{
    public class CursorBehavior : Behavior<VisualElement>
    {
        public CursorIcon Cursor { get; set; } = CursorIcon.Hand;

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);

#if WINDOWS
            bindable.Loaded += (s, e) =>
            {
                bindable.SetCustomCursor(Cursor, bindable.Handler?.MauiContext);
            };
#endif
        }
    }
}

