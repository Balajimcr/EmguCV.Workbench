﻿using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Media;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using EmguCV.Workbench.Util;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Color = System.Windows.Media.Color;

namespace EmguCV.Workbench.Processors
{
    /// <summary>
    /// Applies an affine transformation to the image.
    /// </summary>
    /// <seealso cref="EmguCV.Workbench.Processors.ImageProcessor" />
    [Export(typeof(IImageProcessor))]
    public class WarpAffine : ImageProcessor
    {
        private float _p1X;
        [Category("Warp Affine")]
        [PropertyOrder(0)]
        [DisplayName(@"P1X")]
        [Description(@"The X coordinate of the upper left corner.")]
        public float P1X
        {
            get { return _p1X; }
            set { Set(ref _p1X, value); }
        }

        private float _p1Y;
        [Category("Warp Affine")]
        [PropertyOrder(1)]
        [DisplayName(@"P1Y")]
        [Description(@"The Y coordinate of the upper left corner.")]
        public float P1Y
        {
            get { return _p1Y; }
            set { Set(ref _p1Y, value); }
        }

        private float _p2X = 640;
        [Category("Warp Affine")]
        [PropertyOrder(2)]
        [DisplayName(@"P2X")]
        [Description(@"The X coordinate of the upper right corner.")]
        public float P2X
        {
            get { return _p2X; }
            set { Set(ref _p2X, value); }
        }

        private float _p2Y;
        [Category("Warp Affine")]
        [PropertyOrder(3)]
        [DisplayName(@"P2Y")]
        [Description(@"The Y coordinate of the upper right corner.")]
        public float P2Y
        {
            get { return _p2Y; }
            set { Set(ref _p2Y, value); }
        }

        private float _p3X;
        [Category("Warp Affine")]
        [PropertyOrder(4)]
        [DisplayName(@"P3X")]
        [Description(@"The X coordinate of the lower left corner.")]
        public float P3X
        {
            get { return _p3X; }
            set { Set(ref _p3X, value); }
        }

        private float _p3Y = 480;
        [Category("Warp Affine")]
        [PropertyOrder(5)]
        [DisplayName(@"P3Y")]
        [Description(@"The Y coordinate of the lower left corner.")]
        public float P3Y
        {
            get { return _p3Y; }
            set { Set(ref _p3Y, value); }
        }

        private int _width = 640;
        [Category("Warp Affine")]
        [PropertyOrder(6)]
        [DisplayName(@"Width")]
        [Description(@"The width of the resulting image.")]
        public int Width
        {
            get { return _width; }
            set { Set(ref _width, value); }
        }

        private int _height = 480;
        [Category("Warp Affine")]
        [PropertyOrder(7)]
        [DisplayName(@"Height")]
        [Description(@"The height of the resulting image.")]
        public int Height
        {
            get { return _height; }
            set { Set(ref _height, value); }
        }

        private Inter _interpolationType = Inter.Nearest;
        [Category("Warp Affine")]
        [PropertyOrder(8)]
        [DisplayName(@"Interpolation Type")]
        public Inter InterpolationType
        {
            get { return _interpolationType; }
            set { Set(ref _interpolationType, value); }
        }

        private Warp _warpType = Warp.Default;
        [Category("Warp Affine")]
        [PropertyOrder(9)]
        [DisplayName(@"Warp Type")]
        public Warp WarpType
        {
            get { return _warpType; }
            set { Set(ref _warpType, value); }
        }

        private BordType _borderType = BordType.Constant;
        [Category("Warp Affine")]
        [PropertyOrder(10)]
        [DisplayName(@"Border Type")]
        [Description(@"Pixel extrapolation method.")]
        public BordType BorderType
        {
            get { return _borderType; }
            set { Set(ref _borderType, value); }
        }

        private Color _backgroundColor = Colors.Black;
        [Category("Warp Affine")]
        [PropertyOrder(11)]
        [DisplayName(@"Background Color")]
        [Description(@"A value used to fill outliers.")]
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { Set(ref _backgroundColor, value); }
        }

        public override void Process(ref Image<Bgr, byte> image)
        {
            // the source perspective
            var src = new[] {new PointF(0, 0), new PointF(image.Width, 0), new PointF(0, image.Height)};

            // the transformed perspective
            var dst = new[] {new PointF(_p1X, _p1Y), new PointF(_p2X, _p2Y), new PointF(_p3X, _p3Y)};

            // get the perspective transformation matrix
            using (var mat = CvInvoke.GetAffineTransform(src, dst))
            {
                // apply the matrix
                image = image.WarpAffine(
                    mat,
                    _width,
                    _height,
                    _interpolationType,
                    _warpType,
                    (BorderType) _borderType,
                    new Bgr(_backgroundColor.Color()));
            }
        }
    }
}
