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
    /// Threshold the image such that dst(x,y) = threshold if src(x,y) > threshold, otherwise src(x,y).
    /// </summary>
    /// <seealso cref="EmguCV.Workbench.Processors.ImageProcessor" />
    [Export(typeof(IImageProcessor))]
    public class ThresholdTrunc : ImageProcessor
    {
        private Color _threshold = Colors.White;
        [Category("Threshold Trunc")]
        [PropertyOrder(0)]
        [DisplayName(@"Threshold")]
        [Description(@"The threshold value.")]
        public Color Threshold
        {
            get { return _threshold; }
            set { Set(ref _threshold, value); }
        }

        public override void Process(ref Image<Bgr, byte> image)
        {
            image = image.ThresholdTrunc(new Bgr(_threshold.Color()));
        }
    }
}
