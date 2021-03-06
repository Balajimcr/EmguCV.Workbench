﻿using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Emgu.CV;
using Emgu.CV.Structure;
using EmguCV.Workbench.Util;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace EmguCV.Workbench.Processors
{
    /// <summary>
    /// Non-inverted: threshold the image such that dst(x,y) = src(x,y) if src(x,y) > threshold, otherwise 0.
    /// Inverted: threshold the image such that dst(x,y) = 0 if src(x,y) > threshold, otherwise src(x,y).
    /// </summary>
    /// <seealso cref="EmguCV.Workbench.Processors.ImageProcessor" />
    [Export(typeof(IImageProcessor))]
    public class ThresholdToZero : ImageProcessor
    {
        private Color _threshold = Colors.Black;
        [Category("Threshold To Zero")]
        [PropertyOrder(0)]
        [DisplayName(@"Threshold")]
        [Description(@"The threshold value.")]
        public Color Threshold
        {
            get { return _threshold; }
            set { Set(ref _threshold, value); }
        }

        private bool _invert;
        [Category("Threshold To Zero")]
        [PropertyOrder(1)]
        [DisplayName(@"Invert")]
        [Description(@"Select to invert threshold.")]
        public bool Invert
        {
            get { return _invert; }
            set { Set(ref _invert, value); }
        }

        public override void Process(ref Image<Bgr, byte> image)
        {
            image = !_invert
                ? image.ThresholdToZero(new Bgr(_threshold.Color()))
                : image.ThresholdToZeroInv(new Bgr(_threshold.Color()));
        }
    }
}
