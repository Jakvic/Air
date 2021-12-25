//-----------------------------------------------------------------------
// <copyright file="ClippingBorder.cs" company="Microsoft Corporation copyright 2008.">
// (c) 2008 Microsoft Corporation. All rights reserved.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// </copyright>
// <date>07-Oct-2008</date>
// <author>Martin Grayson</author>
// <summary>A border that clips its contents.</summary>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AirControl
{
    /// <summary>
    ///     A border that clips its contents.
    /// </summary>
    public class AirBorder : ContentControl
    {
        /// <summary>
        ///     The corner radius property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(AirBorder),
                new PropertyMetadata(CornerRadius_Changed));

        /// <summary>
        ///     The clip content property.
        /// </summary>
        public static readonly DependencyProperty ClipContentProperty =
            DependencyProperty.Register("ClipContent", typeof(bool), typeof(AirBorder),
                new PropertyMetadata(ClipContent_Changed));

        /// <summary>
        ///     Stores the main border.
        /// </summary>
        private Border m_border;

        /// <summary>
        ///     Stores the clip responsible for clipping the bottom left corner.
        /// </summary>
        private RectangleGeometry m_bottomLeftClip;

        /// <summary>
        ///     Stores the bottom left content control.
        /// </summary>
        private ContentControl m_bottomLeftContentControl;

        /// <summary>
        ///     Stores the clip responsible for clipping the bottom right corner.
        /// </summary>
        private RectangleGeometry m_bottomRightClip;

        /// <summary>
        ///     Stores the bottom right content control.
        /// </summary>
        private ContentControl m_bottomRightContentControl;

        /// <summary>
        ///     Stores the clip responsible for clipping the top left corner.
        /// </summary>
        private RectangleGeometry m_topLeftClip;

        /// <summary>
        ///     Stores the top left content control.
        /// </summary>
        private ContentControl m_topLeftContentControl;

        /// <summary>
        ///     Stores the clip responsible for clipping the top right corner.
        /// </summary>
        private RectangleGeometry m_topRightClip;

        /// <summary>
        ///     Stores the top right content control.
        /// </summary>
        private ContentControl m_topRightContentControl;

        /// <summary>
        ///     ClippingBorder constructor.
        /// </summary>
        public AirBorder()
        {
            DefaultStyleKey = typeof(AirBorder);
            SizeChanged += ClippingBorder_SizeChanged;
        }

        /// <summary>
        ///     Gets or sets the border corner radius.
        ///     This is a thickness, as there is a problem parsing CornerRadius types.
        /// </summary>
        [Category("Appearance")]
        [Description("Sets the corner radius on the border.")]
        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the content is clipped.
        /// </summary>
        [Category("Appearance")]
        [Description("Sets whether the content is clipped or not.")]
        public bool ClipContent
        {
            get => (bool) GetValue(ClipContentProperty);
            set => SetValue(ClipContentProperty, value);
        }

        /// <summary>
        ///     Gets the UI elements out of the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            m_border = GetTemplateChild("PART_Border") as Border;
            m_topLeftContentControl = GetTemplateChild("PART_TopLeftContentControl") as ContentControl;
            m_topRightContentControl = GetTemplateChild("PART_TopRightContentControl") as ContentControl;
            m_bottomRightContentControl = GetTemplateChild("PART_BottomRightContentControl") as ContentControl;
            m_bottomLeftContentControl = GetTemplateChild("PART_BottomLeftContentControl") as ContentControl;

            if (m_topLeftContentControl != null)
            {
                m_topLeftContentControl.SizeChanged += ContentControl_SizeChanged;
            }

            m_topLeftClip = GetTemplateChild("PART_TopLeftClip") as RectangleGeometry;
            m_topRightClip = GetTemplateChild("PART_TopRightClip") as RectangleGeometry;
            m_bottomRightClip = GetTemplateChild("PART_BottomRightClip") as RectangleGeometry;
            m_bottomLeftClip = GetTemplateChild("PART_BottomLeftClip") as RectangleGeometry;

            UpdateClipContent(ClipContent);

            UpdateCornerRadius(CornerRadius);
        }

        /// <summary>
        ///     Sets the corner radius.
        /// </summary>
        /// <param name="newCornerRadius">The new corner radius.</param>
        internal void UpdateCornerRadius(CornerRadius newCornerRadius)
        {
            if (m_border != null)
            {
                m_border.CornerRadius = newCornerRadius;
            }

            if (m_topLeftClip != null)
            {
                m_topLeftClip.RadiusX = m_topLeftClip.RadiusY =
                    newCornerRadius.TopLeft - Math.Min(BorderThickness.Left, BorderThickness.Top) / 2;
            }

            if (m_topRightClip != null)
            {
                m_topRightClip.RadiusX = m_topRightClip.RadiusY =
                    newCornerRadius.TopRight - Math.Min(BorderThickness.Top, BorderThickness.Right) / 2;
            }

            if (m_bottomRightClip != null)
            {
                m_bottomRightClip.RadiusX = m_bottomRightClip.RadiusY = newCornerRadius.BottomRight -
                                                                        Math.Min(BorderThickness.Right,
                                                                            BorderThickness.Bottom) / 2;
            }

            if (m_bottomLeftClip != null)
            {
                m_bottomLeftClip.RadiusX = m_bottomLeftClip.RadiusY = newCornerRadius.BottomLeft -
                                                                      Math.Min(BorderThickness.Bottom,
                                                                          BorderThickness.Left) / 2;
            }

            UpdateClipSize(new Size(ActualWidth, ActualHeight));
        }

        /// <summary>
        ///     Updates whether the content is clipped.
        /// </summary>
        /// <param name="clipContent">Whether the content is clipped.</param>
        internal void UpdateClipContent(bool clipContent)
        {
            if (clipContent)
            {
                if (m_topLeftContentControl != null)
                {
                    m_topLeftContentControl.Clip = m_topLeftClip;
                }

                if (m_topRightContentControl != null)
                {
                    m_topRightContentControl.Clip = m_topRightClip;
                }

                if (m_bottomRightContentControl != null)
                {
                    m_bottomRightContentControl.Clip = m_bottomRightClip;
                }

                if (m_bottomLeftContentControl != null)
                {
                    m_bottomLeftContentControl.Clip = m_bottomLeftClip;
                }

                UpdateClipSize(new Size(ActualWidth, ActualHeight));
            }
            else
            {
                if (m_topLeftContentControl != null)
                {
                    m_topLeftContentControl.Clip = null;
                }

                if (m_topRightContentControl != null)
                {
                    m_topRightContentControl.Clip = null;
                }

                if (m_bottomRightContentControl != null)
                {
                    m_bottomRightContentControl.Clip = null;
                }

                if (m_bottomLeftContentControl != null)
                {
                    m_bottomLeftContentControl.Clip = null;
                }
            }
        }

        /// <summary>
        ///     Updates the corner radius.
        /// </summary>
        /// <param name="dependencyObject">The clipping border.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args</param>
        private static void CornerRadius_Changed(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs eventArgs)
        {
            AirBorder airBorder = (AirBorder) dependencyObject;
            airBorder.UpdateCornerRadius((CornerRadius) eventArgs.NewValue);
        }

        /// <summary>
        ///     Updates the content clipping.
        /// </summary>
        /// <param name="dependencyObject">The clipping border.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args</param>
        private static void ClipContent_Changed(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs eventArgs)
        {
            AirBorder airBorder = (AirBorder) dependencyObject;
            airBorder.UpdateClipContent((bool) eventArgs.NewValue);
        }

        /// <summary>
        ///     Updates the clips.
        /// </summary>
        /// <param name="sender">The clipping border</param>
        /// <param name="e">Size Changed Event Args.</param>
        private void ClippingBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ClipContent)
            {
                UpdateClipSize(e.NewSize);
            }
        }

        /// <summary>
        ///     Updates the clip size.
        /// </summary>
        /// <param name="sender">A content control.</param>
        /// <param name="e">Size Changed Event Args</param>
        private void ContentControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ClipContent)
            {
                UpdateClipSize(new Size(ActualWidth, ActualHeight));
            }
        }

        /// <summary>
        ///     Updates the clip size.
        /// </summary>
        /// <param name="size">The control size.</param>
        private void UpdateClipSize(Size size)
        {
            if (size.Width > 0 || size.Height > 0)
            {
                var contentWidth = Math.Max(0, size.Width - BorderThickness.Left - BorderThickness.Right);
                var contentHeight = Math.Max(0, size.Height - BorderThickness.Top - BorderThickness.Bottom);

                if (m_topLeftClip != null)
                {
                    m_topLeftClip.Rect = new Rect(0, 0, contentWidth + CornerRadius.TopLeft * 2,
                        contentHeight + CornerRadius.TopLeft * 2);
                }

                if (m_topRightClip != null)
                {
                    m_topRightClip.Rect = new Rect(0 - CornerRadius.TopRight, 0, contentWidth + CornerRadius.TopRight,
                        contentHeight + CornerRadius.TopRight);
                }

                if (m_bottomRightClip != null)
                {
                    m_bottomRightClip.Rect = new Rect(0 - CornerRadius.BottomRight, 0 - CornerRadius.BottomRight,
                        contentWidth + CornerRadius.BottomRight, contentHeight + CornerRadius.BottomRight);
                }

                if (m_bottomLeftClip != null)
                {
                    m_bottomLeftClip.Rect = new Rect(0, 0 - CornerRadius.BottomLeft,
                        contentWidth + CornerRadius.BottomLeft, contentHeight + CornerRadius.BottomLeft);
                }
            }
        }
    }
}