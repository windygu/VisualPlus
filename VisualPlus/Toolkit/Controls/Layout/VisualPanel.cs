﻿namespace VisualPlus.Toolkit.Controls.Layout
{
    #region Namespace

    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    using VisualPlus.Enumerators;
    using VisualPlus.Localization.Category;
    using VisualPlus.Renders;
    using VisualPlus.Structure;
    using VisualPlus.Toolkit.Components;
    using VisualPlus.Toolkit.VisualBase;

    #endregion

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Panel))]
    [DefaultEvent("Paint")]
    [DefaultProperty("Enabled")]
    [Description("The Visual Panel")]
    public class VisualPanel : NestedControlsBase
    {
        #region Variables

        private Border _border;

        #endregion

        #region Constructors

        /// <inheritdoc />
        /// <summary>Initializes a new instance of the <see cref="T:VisualPlus.Toolkit.Controls.Layout.VisualPanel" /> class.</summary>
        public VisualPanel()
        {
            Size = new Size(187, 117);
            Padding = new Padding(5, 5, 5, 5);
            _border = new Border();

            UpdateTheme(Settings.DefaultValue.DefaultStyle);
        }

        #endregion

        #region Properties

        [TypeConverter(typeof(BorderConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(Propertys.Appearance)]
        public Border Border
        {
            get
            {
                return _border;
            }

            set
            {
                _border = value;
                Invalidate();
            }
        }

        #endregion

        #region Events

        public void UpdateTheme(Styles style)
        {
            StyleManager = new VisualStyleManager(style);

            ForeColor = StyleManager.FontStyle.ForeColor;
            ForeColorDisabled = StyleManager.FontStyle.ForeColorDisabled;

            BackColorState.Enabled = StyleManager.ControlStyle.Background(0);
            BackColorState.Disabled = StyleManager.ControlStyle.Background(0);

            _border.Color = StyleManager.BorderStyle.Color;
            _border.HoverColor = StyleManager.BorderStyle.HoverColor;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;
            graphics.Clear(Parent.BackColor);
            graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            ControlGraphicsPath = VisualBorderRenderer.GetBorderShape(ClientRectangle, _border.Type, _border.Rounding);
            Color _backColor = Enabled ? BackColorState.Enabled : BackColorState.Disabled;
            VisualBackgroundRenderer.DrawBackground(e.Graphics, ClientRectangle, _backColor, BackgroundImage, Border, MouseState);
        }

        #endregion
    }
}