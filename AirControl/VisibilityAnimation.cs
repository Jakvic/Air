using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace AirControl
{
    public class VisibilityAnimation
    {
        private const int AnimationDuration = 300;

        private static readonly Dictionary<FrameworkElement, bool> hookedElements =
            new();

        static VisibilityAnimation()
        {
            UIElement.VisibilityProperty.AddOwner(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                Visibility.Visible,
                null, CoerceVisibility));
        }

        private static void HookVisibilityChanges(FrameworkElement frameworkElement)
        {
            hookedElements.Add(frameworkElement, false);
        }

        private static void UnHookVisibilityChanges(FrameworkElement frameworkElement)
        {
            if (hookedElements.ContainsKey(frameworkElement))
            {
                hookedElements.Remove(frameworkElement);
            }
        }

        private static object CoerceVisibility(DependencyObject dependencyObject, object baseValue)
        {
            var frameworkElement = dependencyObject as FrameworkElement;
            if (frameworkElement is null)
            {
                return baseValue;
            }

            var visibility = (Visibility) baseValue;
            if (visibility == frameworkElement.Visibility)
            {
                return baseValue;
            }

            if (!IsHookedElement(frameworkElement))
            {
                return baseValue;
            }

            if (UpdateAnimationStartedFlag(frameworkElement))
            {
                return baseValue;
            }

            var doubleAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(AnimationDuration))
            };

            doubleAnimation.Completed += (s, e) =>
            {
                if (visibility is Visibility.Visible)
                {
                    UpdateAnimationStartedFlag(frameworkElement);
                }
                else
                {
                    if (BindingOperations.IsDataBound(frameworkElement, UIElement.VisibilityProperty))
                    {
                        var binding = BindingOperations.GetBinding(frameworkElement, UIElement.VisibilityProperty);
                        BindingOperations.SetBinding(frameworkElement, UIElement.VisibilityProperty, binding!);
                    }
                    else
                    {
                        frameworkElement.Visibility = visibility;
                    }
                }
            };

            if (visibility is Visibility.Collapsed or Visibility.Hidden)
            {
                doubleAnimation.From = 1.0;
                doubleAnimation.To = 0.0;
            }
            else
            {
                doubleAnimation.From = 0.0;
                doubleAnimation.To = 1.0;
            }

            frameworkElement.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
            return Visibility.Visible;
        }

        private static bool IsHookedElement(FrameworkElement frameworkElement)
        {
            return hookedElements.ContainsKey(frameworkElement);
        }

        private static bool UpdateAnimationStartedFlag(FrameworkElement frameworkElement)
        {
            var started = hookedElements[frameworkElement];
            hookedElements[frameworkElement] = !started;
            return started;
        }
    }
}