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
    /// Computes absolute difference between image and the specific color.
    /// </summary>
    /// <seealso cref="EmguCV.Workbench.Processors.ImageProcessor" />
    [Export(typeof(IImageProcessor))]
    public class AbsDiff : ImageProcessor
    {
        private Color _color = Colors.Black;
        [Category("Abs Diff")]
        [PropertyOrder(0)]
        [DisplayName(@"Color")]
        [Description(@"The color to compute absolute different with.")]
        public Color Color
        {
            get { return _color; }
            set { Set(ref _color, value); }
        }

        public override void Process(ref Image<Bgr, byte> image)
        {
            image = image.AbsDiff(new Bgr(_color.Color()));
        }
    }
}
